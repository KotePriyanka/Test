using System;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using NUnit.Framework;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Data;
using System.Drawing;
using System.Globalization;

namespace Nimble.Automation.Accelerators
{
    /// <summary>
    /// Result Helper to store results to the result database. 
    /// </summary>
    public class ResultDbHelper
    {
        //Test RUN Property for Results database
        private string testRunGuid;

        string projectLoc = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "ScreenShots");

        //string projectLoc = System.IO.Path.Combine("C:\\", "ScreenShots");
        //  string projectLoc = @"D:\ScreenShots";

        System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Jpeg;
        //  string screenshotpath = ConfigurationManager.AppSettings["ScreenShotPath"].ToString();
        bool ResultFlag = Convert.ToBoolean(ConfigurationManager.AppSettings["ResultFlag"]);      

        public string TestRunGuid
        {
            get { return testRunGuid; }
            set { testRunGuid = value; }
        }

        //Test AUT Property for Results database
        private string testAutGuid;

        public string TestAutGuid
        {
            get { return testAutGuid; }
            set { testAutGuid = value; }
        }

        //Test Guid Property for Results database
        private string testGuid;

        public string TestGuid
        {
            get { return testGuid; }
            set { testGuid = value; }
        }

        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["ResultRepositoryConnStr"].ToString());

        /// <summary>
        /// Default Constructor. Must be initialized only ONCE per test project. Populates RUN Guid & AutGuid for the current Test Run.
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="productVersion"></param>
        /// <param name="productRelease"></param>
        /// <param name="runComments"></param> 
        public ResultDbHelper(string productName, string productVersion, string productRelease, string runComments)
        {
            TestAutGuid = PopulateAutGuid(productName, productVersion, productRelease);
            TestRunGuid = PopulateRunGuid(runComments);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ResultDbHelper()
        {

        }

        //Generic method to execute a sql command.
        private object ExecuteSql(string _sql)
        {
            object _result = "";
            //var _result = null;
            var sendresultstoDB = Convert.ToBoolean(ConfigurationManager.AppSettings["ResultFlag"]);
            if (sendresultstoDB)
            {
                SqlCommand sqlCommand = new SqlCommand(_sql, sqlConnection);
                sqlConnection.Open();
                _result = sqlCommand.ExecuteScalar();
                sqlConnection.Close();

                if (_result == null)
                    return _result = "NoData";
                else
                    return _result;
            }

            return _result;
        }

        //Populate Run Guid
        private string PopulateRunGuid(string _runComments)
        {
            string sql = @"Execute sp_TestRunInsertPr";
            sql += "'" + TestAutGuid + "'" + ",";
            sql += "'" + _runComments + "'";

            var _runGuid = ExecuteSql(sql);

            return _runGuid.ToString();
        }

        //Populate Aut Guid
        private string PopulateAutGuid(string _productName, string _productVersion, string _productRelease)
        {
            string sql = @"Execute sp_AutInsertPr";
            sql += "'" + _productName + "'" + ",";
            sql += "'" + _productVersion + "'" + ",";
            sql += "'" + _productRelease + "'";

            var _autGuid = ExecuteSql(sql);

            return _autGuid.ToString();
        }

        public string UpdateEndTime(string RunGUID)
        {
            string sql = @"UPDATE TestRun SET EndTime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE RunGUID = '" + RunGUID + "'";

            var _autGuid = ExecuteSql(sql);

            return _autGuid.ToString();
        }

        public void UpdateEnvdetails(string Env, string Browser, string Build)
        {
            string _testGuid = null;
            string fileName = TestUtility.GetAssemblyDirectory() + @"\RunGUID.txt";
            using (StreamReader sr = File.OpenText(fileName))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                    _testGuid = s;
                }
            }
            string sql = @"UPDATE TestRun SET Environment = '" + Env + "'," + "Browser ='" + Browser + "'," + "Build ='" + Build + "' WHERE RunGUID = '" + _testGuid + "'";

            ExecuteSql(sql);

        }

        /// <summary>
        /// Saves the current test result/outcome to the database.
        /// </summary>
        /// <param name="testContext"></param>
        public void SendTestResultToDb(TestContext testContext)
        {
            //RunGuid Check, if RunGuid is NULL, then calling test project didn't configure settings correct.
            CheckRunGuid();

            var _testGuid = ExecuteTestGuidSelectPr(testContext);

            if (_testGuid.ToString() != "NoData")
            {
                string sql = @"Execute sp_TestResultInsertPr";
                sql += "'" + TestRunGuid + "'" + ",";
                sql += "'" + _testGuid.ToString() + "'" + ",";
                sql += "'" + testContext.Result.Outcome + "'" + ",";
                sql += "''";

                ExecuteSql(sql);
            }
            else
            {
                SystemException ex = new SystemException("No Test Id found in the test result db");
                throw ex;
            }

        }

        /// <summary>
        /// Saves the current test result/outcome to the database.
        /// </summary>
        /// <param name="testContext"></param>
        public void SendTestResultToDb(TestContext testContext, string comments)
        {
            string _testGuid = null;
            //Read RunGUID
            string fileName = TestUtility.GetAssemblyDirectory() + @"\RunGUID.txt";

            using (StreamReader sr = File.OpenText(fileName))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                    _testGuid = s;
                }
            }

            if (_testGuid != null)
            {
                string sql = @"Execute sp_ResultInsertPr ";
                sql += "'" + _testGuid + "'" + ",";
                sql += "'" + comments + "'" + ",";
                sql += "'" + testContext.Result.Outcome + "'";

                ExecuteSql(sql);
            }
        }

        public byte[] imageToByteArray(System.Drawing.Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Saves the current test result/outcome to the database.
        /// </summary>
        /// <param name="testContext"></param>
        public void SendTestResultToDb(TestContext testContext, string comments, string EmailID, DateTime starttime)
        {
            if (ResultFlag)
            {
                string path = TestContext.CurrentContext.Test.Name + "-" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "." + format;
                // string path = projectLoc + TestContext.CurrentContext.Test.Name + "-" + DateTime.Now.ToString("dd-mm-yyyy") + "." + format;
                // string image = path;
                //Image img = new Bitmap(image); 
                //Bitmap img = new Bitmap(image);
                //byte[] screenshot = imageToByteArray(img);

                comments = RemoveSpecialCharacters(comments);
               // DateTime starttimeexe = DateTime.ParseExact(starttime.ToString("MM/dd/yyyy hh:mm:ss tt"), "MM /dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                //DateTime endtimeexe = DateTime.ParseExact(DateTime.Now.ToString(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                string _runguid = GetLatestRunGUID();

                string sql = @"Execute sp_ResultInsertPr ";
                sql += "'" + _runguid + "'" + ",";
                sql += "'" + comments + "'" + ",";
                sql += "'" + testContext.Result.Outcome + "'" + ",";
                sql += "'" + testContext.Test.Name + "'" + ",";
                sql += "'" + EmailID + "'" + ",";
                sql += "'" + starttime.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ",";
                sql += "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" + ",";
                sql += "'" + path + "'";
                ExecuteSql(sql);
            }
        }

        //Generic method to execute "TestGuidSelectPr"
        private object ExecuteTestGuidSelectPr(TestContext testContext)
        {
            //string[] testProject = testContext.FullyQualifiedTestClassName.Split(new Char[] { '.' });
            //int arrayLenth = testProject.Length - 1;
            //Fixed a bug with project names containing multiple "."
            //int index = 0;
            string project = "Nimble";
            string classname = testContext.Test.FullName;

            //Check if owner property exists and set it.
            string owner = string.Empty;
            //if (testContext..Contains("Owner"))
            //    owner = testContext.Properties["Owner"].ToString();
            //else
            //    owner = "";

            //Check if AssociatedWorkItems property exists and set it.
            string AssociatedWorkItems = string.Empty;
            //if (testContext.Properties.Contains("AssociatedWorkItems"))
            //    AssociatedWorkItems = testContext.Properties["AssociatedWorkItems"].ToString();
            //else
            //    AssociatedWorkItems = "";

            string sql = @"Execute sp_TestInsertPr";
            sql += "'" + testContext.Test.Name + "'" + ",";
            sql += "'" + project + "'" + ",";  //--> This is TestProject Name
            sql += "'" + "PlaceHolder" + "'" + ",";
            sql += "'" + classname + "'" + ","; //--> This is the TestClass Name (will be the last element from Array)
            sql += "'" + owner + "'" + ",";
            sql += "'" + AssociatedWorkItems + "'";

            var _testGuid = ExecuteSql(sql);

            return _testGuid;
        }

        public object ExecuteTestRun(string Env, string browser, string BuildID)
        {
            //var val = SqlDbType.DateTime;          
            int productID = Convert.ToInt32(TestEngine.ProductID);
            int TestSuitID = Convert.ToInt32(TestEngine.TestSuitID);
            // Get Build No
            string starttime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string sql = @"Execute sp_RunInsertPr ";
            sql += productID + ",";
            sql += TestSuitID + ",";
            sql += "'" + BuildID + "'" + ","; // Build ID 
            sql += "'" + starttime + "'" + ","; // Start time
            sql += "'" + "'" + ",";//End Time
            sql += "'" + Env + "'" + ","; // Env
            sql += "'" + browser + "'" + ","; //Browser
            sql += "'" + "'" + ","; // Passed scripts count
            sql += "'" + "'" + ","; // Failed Scripts count
            sql += "'" + "'";//Total Scripts Count
            var _testGuid = ExecuteSql(sql);

            return _testGuid;
        }

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            string strng = "";
            if (str != null)

                foreach (char c in str)
                {
                    if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == ' ')
                    {
                        sb.Append(c);
                    }
                }
            strng = sb.ToString();
            return strng;
        }

        //Error handling
        private void CheckRunGuid()
        {
            if (string.IsNullOrEmpty(TestRunGuid))
            {
                SystemException resultEx = new SystemException("TestRun GUID not initialized. Please make sure test settings are correct!");
                throw resultEx;
            }
        }

        /// <summary>
        /// Run query to get the latest run GUID
        /// </summary>
        public string GetLatestRunGUID()
        {
            int productID = Convert.ToInt32(TestEngine.ProductID);
            int TestSuitID = Convert.ToInt32(TestEngine.TestSuitID);

            var _testGuid = ExecuteSql("SELECT TOP 1 RunGUID FROM [TestRun] where ProjectID ='" + productID + "' and SuitID = '" + TestSuitID + "' order by StartTime DESC");

            return _testGuid.ToString();
        }

        public void UpdateScriptPassCount()
        {
            string RunGUID = GetLatestRunGUID();

            //Get Failed Scripts Count
            string sql = @"select count(0) from ( select Name, Status, ROW_NUMBER() Over(partition by Name ORDER BY[StartTime] desc) as row_num from[TestResults] where RunGuid = '" + RunGUID + "') a where row_num = 1 AND status = 'Failed'";
            var failcount = ExecuteSql(sql);

            //Get Failed Scripts Count
            sql = @"select count(0)  from ( select Name, Status, ROW_NUMBER() Over(partition by Name ORDER BY[StartTime] desc) as row_num from[TestResults] where RunGuid = '" + RunGUID + "') a where row_num = 1 AND status = 'Passed'";
            var passcountcount = ExecuteSql(sql);

            //Get Total Scripts Count
            var totalcount = Convert.ToInt32(failcount) + Convert.ToInt32(passcountcount);

            //Update
            sql = @"UPDATE TestRun SET Passed = '" + passcountcount.ToString() + "', Failed = '" + failcount.ToString() + "', Total = '" + totalcount.ToString() + "'  WHERE RunGUID = '" + RunGUID + "'";
            ExecuteSql(sql);
        }
    }
}
