using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Reflection;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Nimble.Automation.Accelerators
{
    /// <summary>
    /// Provides Test Helper methods for working with Common Test Utility
    /// </summary>
    public class TestUtility : TestEngine
    {
        public TestUtility()
        {
            Directory.SetCurrentDirectory(GetAssemblyDirectory());
        }

        #region XML Data Helpers, reading data from an XML file
        /// <summary>
        /// This method will hold the key value pair of XMLFile names and corresponding XMLDocuments 
        /// This will enable reading the file only ones making this more efficient calls.
        /// File will be added where filename as key and contents as value
        /// Will need System.Collections.Generic as assembly reference if not already available.
        /// </summary>
        public static Dictionary<string, XPathDocument> MyXMLDataFiles = new Dictionary<string, XPathDocument>();

        /// <summary>
        /// Add XMLFile to MyXMLDataFiles with key value pair of File names and corresponding XMLDocuments 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static XPathDocument GetXMLData(string fileName)
        {
            XPathDocument temp; // lazy loading
            //return if key exists, so no I/O access needed
            if (MyXMLDataFiles.ContainsKey(fileName))
                MyXMLDataFiles.TryGetValue(fileName, out temp);
            else
            {
                // read the file first time and add it to dictionary
                temp = new XPathDocument(fileName);
                MyXMLDataFiles.Add(fileName, temp);
            }

            return temp;
        }

        /// <summary>
        /// Returns the XML element value from the specified FileName, Node hierarchy. All nodes must be inside "TestData" element in the XML file.
        /// </summary>
        /// <param name="FileName">Test Data XML File Name</param>
        /// <param name="Node1"></param>
        /// <returns>string Value: XML element Value</returns>
        public static string XMLDataReader(string FileName, string Node1)
        {
            XPathNavigator nav;
            XPathDocument docNav;
            string nodePath, nodeValue;
            //docNav = new XPathDocument(FileName);
            docNav = GetXMLData(FileName);
            nav = docNav.CreateNavigator();
            nodePath = "/TestData" + "/" + Node1;
            nodeValue = nav.SelectSingleNode(nodePath).Value;
            return nodeValue;
        }

        /// <summary>
        /// Returns the XML element value from the specified FileName, Node hierarchy. All nodes must be inside "TestData" element in the XML file.
        /// </summary>
        /// <param name="FileName">Test Data XML File Name</param>
        /// <param name="Node1"></param>
        /// <param name="Node2"></param>
        /// <returns>string Value: XML element Value</returns>
        public static string XMLDataReader(string FileName, string Node1, string Node2)
        {
            Directory.SetCurrentDirectory(GetAssemblyDirectory() + "//TestData");
            XPathNavigator nav;
            XPathDocument docNav;
            string nodePath, nodeValue;
            //docNav = new XPathDocument(FileName);
            docNav = GetXMLData(FileName);
            nav = docNav.CreateNavigator();
            nodePath = "/TestData" + "/" + Node1 + "/" + Node2;
            nodeValue = nav.SelectSingleNode(nodePath).Value;
            return nodeValue;
        }

        /// <summary>
        /// Returns the XML element value from the specified FileName, Node hierarchy. All nodes must be inside "TestData" element in the XML file.
        /// </summary>
        /// <param name="FileName">Test Data XML File Name</param>
        /// <param name="Node1"></param>
        /// <param name="Node2"></param>
        /// <param name="Node3"></param>
        /// <returns>string Value: XML element Value</returns>        
        public static string XMLDataReader(string FileName, string Node1, string Node2, string Node3)
        {
            XPathNavigator nav;
            XPathDocument docNav;
            String nodePath, nodeValue;
            //docNav = new XPathDocument(FileName);
            docNav = GetXMLData(FileName);
            nav = docNav.CreateNavigator();
            nodePath = "/TestData" + "/" + Node1 + "/" + Node2 + "/" + Node3;
            nodeValue = nav.SelectSingleNode(nodePath).Value;
            return nodeValue;
        }

        /// <summary>
        /// Returns the XML element value from the specified FileName, Node hierarchy. All nodes must be inside "TestData" element in the XML file.
        /// </summary>
        /// <param name="FileName">Test Data XML File Name</param>
        /// <param name="Node1"></param>
        /// <param name="Node2"></param>
        /// <param name="Node3"></param>
        /// <param name="Node4"></param>
        /// <returns>string Value: XML element Value</returns>
        public static string XMLDataReader(string FileName, string Node1, string Node2, string Node3, string Node4)
        {
            XPathNavigator nav;
            XPathDocument docNav;
            String nodePath, nodeValue;
            //docNav = new XPathDocument(FileName);
            docNav = GetXMLData(FileName);
            nav = docNav.CreateNavigator();
            nodePath = "/TestData" + "/" + Node1 + "/" + Node2 + "/" + Node3 + "/" + Node4;
            nodeValue = nav.SelectSingleNode(nodePath).Value;
            return nodeValue;
        }

        /// <summary>
        /// Formats an xml string for indentation, removing xml declaration. 
        /// </summary>
        /// <param name="xmlContent"></param>
        /// <returns></returns>
        public static String GetFormattedXML(String xmlContent)
        {
            XmlDocument oXmlDocument = new XmlDocument();
            oXmlDocument.LoadXml(xmlContent);

            StringBuilder oStringBuilder = new StringBuilder();
            XmlWriterSettings oXmlWriterSettings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };

            oXmlDocument.Save(XmlWriter.Create(oStringBuilder, oXmlWriterSettings));

            return oStringBuilder.ToString();
        }

        #endregion

        public static string GetAssemblyDirectory()
        {
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        public string RandomString(int length)
        {
            Random random = new Random();
            string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string RandomAccName()
        {
            Random random = new Random();
            return RandomString(random.Next(3, 12));
        }

        public DateTime GetRandomDate()
        {
            Random random = new Random();
            DateTime from = new DateTime(1950, 5, 1);
            DateTime to = new DateTime(1995, 5, 1);
            var range = to - from;

            var randTimeSpan = new TimeSpan((long)(random.NextDouble() * range.Ticks));

            return from + randTimeSpan;
        }

        public string GetRandomDOB()
        {
            string _dt = GetRandomDate().ToString("dd/MM/yyyy");
            return _dt.Replace("-", "/");
        }

        public string RandomEmail()
        {
            Random random = new Random();
            string chars = "abcdefghijklm";
            var email = new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            Random _random = new Random();
            string chars1 = "nopqrstuvwxyz";
            email += "_" + new string(Enumerable.Repeat(chars1, 5)
              .Select(s => s[_random.Next(s.Length)]).ToArray());

            email += "_"+ _random.Next(1, 9999).ToString() + "@Cigniti.com";
            return email;
        }

        public string RandomNumber(int length)
        {
            Random random = new Random();
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                output.Append(random.Next(0, 9));
            }
            return output.ToString();
        }

        public string RandomAccNumber()
        {
            Random random = new Random();

            return RandomNumber(random.Next(4, 9));
        }

        public string FixedAccNumber()
        {
            Random random = new Random();

            //To get random account number for first four digits
            string randomAccValue = RandomNumber(4);

            //Account number first 4 digits random and next 4 digits are fixed
            string AccNumber = randomAccValue + "4567";
            return AccNumber;
        }
        
        public string RandomDay()
        {
            Random rnd = new Random();
            int day = rnd.Next(1, 30);
            return day.ToString();
        }

        public string RandomMonth()
        {
            Random rnd = new Random();
            int month = rnd.Next(1, 12);
            return month.ToString();
        }

        public string RandomYear()
        {
            string _dt = GetRandomDate().ToString("yyyy");
            return _dt;
        }

        public string RandomTitle()
        {
            Random random = new Random();
            string[] title = { "Mr", "Mrs", "Miss", "Ms" };
            return title[random.Next(0, 3)];
        }

        public string RandomStreeType()
        {
            Random random = new Random();
            string[] streetype = {  "ACCESS", "ALLEY", "ALLEYWAY", "AMBLE", "ANCHORAGE", "APPROACH", "ARCADE", "ARTERY",
                    "AVENUE", "BASIN", "BEACH", "BEND", "BLOCK", "BOULEVARD", "BOWL", "BRACE", "BRAE", "BREAK", "BRIDGE",
                    "BROADWAY", "BROW", "BYPASS", "BYWAY", "CAUSEWAY", "CENTRE", "CENTREWAY", "CHASE", "CIRCLE", "CIRCLET", "CIRCUIT",
                    "CIRCUS", "CLOSE", "COLONNADE", "COMMON", "CONCOURSE", "COPSE", "CORNER", "CORSO", "COURT","COURTYARD", "COVE",
                    "CRESCENT", "CREST", "CROSS", "CROSSING", "CROSSROAD", "CROSSWAY", "CRUISEWAY", "CUL-DE-SAC", "CUTTING", "DALE",
                    "DELL", "DEVIATION", "DIP", "DISTRIBUTOR", "DRIVE", "DRIVEWAY", "EDGE", "ELBOW", "END", "ENTRANCE", "ESPLANADE",
                    "ESTATE", "EXPRESSWAY", "EXTENSION", "FAIRWAY", "FIRE TRACK", "FIRETRAIL", "FLAT", "FOLLOW", "FOOTWAY", "FORESHORE",
                    "FORMATION", "FREEWAY", "FRONT", "FRONTAGE", "GAP", "GARDEN", "GARDENS", "GATE", "GATES", "GLADE", "GLEN", "GRANGE",
                    "GREEN", "GROUND", "GROVE", "GULLY", "HEIGHTS", "HIGHROAD", "HIGHWAY", "HILL", "INTERCHANGE", "INTERSECTION",
                    "JUNCTION", "KEY", "LANDING", "LANE", "LANEWAY", "LEES", "LINE", "LINK", "LITTLE", "LOOKOUT", "LOOP", "LOWER",
                    "MALL", "MEANDER", "MEW", "MEWS", "MOTORWAY", "MOUNT", "NOOK", "OUTLOOK", "PACKET", "PARADE", "PARK", "PARKLANDS",
                    "PARKWAY", "PART", "PASS", "PATH", "PATHWAY", "PIAZZA", "PLACE", "PLATEAU", "PLAZA", "POCKET", "POINT", "PORT",
                    "PROMENADE", "QUAD", "QUADRANGLE", "QUADRANT", "QUAY", "QUAYS", "RAMBLE", "RAMP", "RANGE", "REACH", "RESERVE",
                    "REST", "RETREAT", "RIDE", "RIDGE", "RIDGEWAY", "RIGHT OF WAY", "RING", "RISE", "RIVER", "RIVERWAY",
                    "RIVIERA", "ROAD", "ROADS", "ROADSIDE", "ROADWAY", "RONDE", "ROSEBOWL", "ROTARY", "ROUND", "ROUTE",
                    "ROW", "RUE", "RUN", "SERVICE WAY", "SIDING", "SLOPE", "SOUND", "SPUR", "SQUARE", "STAIRS", "STATE HIGHWAY",
                    "STEPS", "STRAND", "STREET", "STREET NORTH", "STRIP", "SUBWAY", "TARN", "TERRACE", "THOROUGHFARE",
                    "TOLLWAY", "TOP", "TOR", "TOWERS", "TRACK", "TRAIL", "TRAILER", "TRIANGLE", "TRUNKWAY", "TURN", "UNDERPASS",
                    "UPPER", "VALE", "VIADUCT", "VIEW", "VILLAS", "VISTA", "WADE", "WALK", "WALKWAY", "WAY", "WHARF", "WYND", "YARD"};
            return streetype[random.Next(0, 206)];
        }

        /// <summary>
        /// List of Suburb Name and Postal Codes
        /// </summary>
        /// <param name="x">Max value 183</param>
        /// <param name="y"> 0 For Subrub Name and 1 for Postal Code</param>
        /// <returns>Subrub Name or Postal Code based on Paramater</returns>
        /// 
        public string RandomSubrubPostCode(int x, int y)
        {
            Random random = new Random();
            string[,] _val = new string[,]{{"AVONDALE HEIGHTS","3034"},{"MOONEE PONDS","3039"},{"MELBOURNE AIRPORT","3045"},{"ROYAL MELBOURNE HOSPITAL","3050"},
                                           {"GREENVALE","3059"},{"CAMPBELLFIELD","3061"},{"SOMERTON","3062"},{"ABBOTSFORD","3067"},
                                           {"THORNBURY","3071"},{"THOMASTOWN","3074"},{"MILL PARK","3082"},{"DIAMOND CREEK","3089"},
                                           {"PLENTY","3090"},{"YARRAMBAT","3091"},{"LOWER PLENTY","3093"},{"MONTMORENCY","3094"},
                                           {"WATTLE GLEN","3096"},{"KEW EAST","3102"},{"TEMPLESTOWE","3106"},{"TEMPLESTOWE LOWER","3107"},{"DONCASTER","3108"},
                                           {"NUNAWADING BC","3110"},{"DONVALE","3111"},{"PARK ORCHARDS","3114"},{"WONGA PARK","3115"},
                                           {"CHIRNSIDE PARK","3116"},{"BATHURST STREET PO","7000"},{"RISDON VALE","7016"},{"LAUDERDALE","7021"},
                                           {"SOUTH ARM","7022"},{"OPOSSUM BAY","7023"},{"CREMORNE","7024"},{"CAMPANIA","7026"},
                                           {"COLEBROOK","7027"},{"BLACKMANS BAY","7052"},{"HUNTINGFIELD","7055"},{"FRANKLIN","7113"},
                                           {"DOVER","7117"},{"STONOR","7119"},{"STRATHGORDON","7139"},{"KETTERING","7155"},
                                           {"COPPING","7174"},{"KELLEVIE","7176"},{"MURDUNNA","7178"},{"EAGLEHAWK NECK","7179"},
                                           {"TARANNA","7180"},{"HIGHCROFT","7183"},{"PREMAYDENA","7185"},{"KOONYA","7187"},
                                           {"CAPE BARREN ISLAND","7257"},{"GRAVELLY BEACH","7276"},{"HADSPEN","7290"},{"CARRICK","7291"},
                                           { "SOMERSET","7322"},{ "STANLEY","7331"},{ "GORMANSTON","7466"},
                                           { "GEORGE STREET","4003"},{ "NORTHGATE","4013"},{ "KIPPA-RING","4021"},{ "ROTHWELL","4022"},
                                           { "ROYAL BRISBANE HOSPITAL","4029"},{ "BALD HILLS","4036"},{ "EATONS HILL","4037"},
                                           { "BARDON","4065"},{ "UNIVERSITY OF QUEENSLAND","4072"},{ "YERONGA","4104"},{ "NATHAN","4111"},
                                           { "KURABY","4112"},{ "UNDERWOOD","4119"},{ "BELMONT","4153"},{ "CHANDLER","4155"},
                                           { "THORNESIDE","4158"},{ "BIRKDALE","4159"},{ "ALEXANDRA HILLS","4161"},{ "THORNLANDS","4164"},
                                           { "MURARRIE","4172"},{ "HEMMANT","4174"},{ "BETHANIA","4205"},{ "WEST BURLEIGH","4219"},
                                           { "GRIFFITH UNIVERSITY","4222"},{ "BOND UNIVERSITY","4229"},{ "ROBINA TOWN CENTRE","4230"},
                                           { "TAMBORINE","4270"},{ "EAGLE HEIGHTS","4271"},{ "GATTON COLLEGE","4345"},{ "MARBURG","4346"},
                                           { "ERNABELLA","0872"},{ "WEST LAKES SHORE","5020"},{ "WEST LAKES","5021"},{ "NOVAR GARDENS","5040"},
                                           { "HOLDEN HILL","5088"},{ "HIGHBURY","5089"},{ "HOPE VALLEY","5090"},{ "ANGLE VALE","5117"},
                                           { "WYNN VALE","5127"},{ "PARACOMBE","5132"},{ "INGLEWOOD","5133"},{ "NORTON SUMMIT","5136"},
                                           { "BASKET RANGE","5138"},{ "FOREST RANGE","5139"},{ "GREENHILL","5140"},{ "URAIDLA","5142"},
                                           { "CAREY GULLY","5144"},{ "PICCADILLY","5151"},{ "BRINGENBRONG","2642"},
                                           { "ALDGATE","5154"},{ "UPPER STURT","5156"},{ "CHRISTIE DOWNS","5164"},{ "O'SULLIVAN BEACH","5166"},
                                           { "MASLIN BEACH","5170"},{ "PORT ELLIOT","5212"},{ "MIDDLETON","5213"},{ "PARNDANA","5220"},
                                           { "CUDLEE CREEK","5232"},{ "BIRDWOOD","5234"},{ "TUNGKILLO","5236"},{ "LENSWOOD","5240"},
                                           { "DARWIN","0800"},{ "BERRY SPRINGS","0838"},{ "DARWIN RIVER","0841"},{ "BATCHELOR","0845"},
                                           { "ADELAIDE RIVER","0846"},{ "PINE CREEK","0847"},{ "TINDAL","0853"},{ "ALYANGULA","0885"},
                                           { "JABIRU","0886"},{ "KINGS CROSS","1340"},{ "ATO ACTIVITY STATEMENTS","1391"},{ "HURSTVILLE BC","1481"},
                                           { "WEST CHATSWOOD","1515"},{ "HORNSBY WESTFIELD","1635"},{ "NORTH RYDE BC","1670"},{ "RYDALMERE BC","1701"},
                                           { "SEVEN HILLS MC","1781"},{ "PENRITH SOUTH DC","1797"},{ "WETHERILL PARK DC","1851"},{ "WORLD SQUARE","2002"},
                                           { "PYRMONT","2009"},{ "REDFERN","2016"},{ "BELLEVUE HILL","2023"},{ "ROSE BAY","2029"},
                                           { "ANNANDALE","2038"},{ "ROZELLE","2039"},{ "ERSKINEVILLE","2043"},{ "HABERFIELD","2045"},
                                           { "CAMMERAY","2062"},{ "GORDON","2072"},{ "MOUNT COLAH","2079"},{ "MOUNT KURING-GAI","2080"},
                                           { "HMAS PENGUIN","2091"},{ "SEAFORTH","2092"},{ "FAIRLIGHT","2094"},{ "BAYVIEW","2104"},
                                           { "MACQUARIE UNIVERSITY","2109"},{ "RYDALMERE","2116"},{ "WEST PENNANT HILLS","2125"},{ "AUSTRALIAN NATIONAL UNIVERSITY","0200"},
                                           { "CIVIC SQUARE","2608"},{ "HALL","2618"},{ "TUGGERANONG DC","2901"},{ "GUNGAHLIN","2912"}};
            //int numb = random.Next(0, 99);
            //string[,] _value = new string [,]{ { _val[numb, 0], _val[numb, 1] } };
            return _val[x, y];
        }

        XmlDocument xml = new XmlDocument();

        public void UpdateBankStatementXML(string strPath)
        {
            xml.Load(strPath);
            double diffDays = LatestDate();
            foreach (XmlElement element in xml.SelectNodes("//postDate"))
            {
                DateTime old = Convert.ToDateTime(element.InnerText);
                var latest = old.AddDays(diffDays);
                string currentdate = latest.ToString("yyyy-MM-dd") + "T00:00:00";
                element.InnerText = currentdate;
            }
            xml.Save(strPath);
            foreach (XmlElement element in xml.SelectNodes("//transDate"))
            {
                DateTime old = Convert.ToDateTime(element.InnerText);
                var latest = old.AddDays(diffDays);
                string currentdate = latest.ToString("yyyy-MM-dd") + "T00:00:00";
                element.InnerText = currentdate;
            }
            xml.Save(strPath); // Or whatever
        }

        public double LatestDate()
        {
            List<DateTime> alltimes = new List<DateTime>();
            foreach (XmlElement element in xml.SelectNodes("//postDate"))
            {
                DateTime old = Convert.ToDateTime(element.InnerText);
                alltimes.Add(old);
            }

            alltimes.Sort();

            var _diff = (DateTime.Now - alltimes[alltimes.Count - 1]).TotalDays;
            return _diff;

        }

        public string RandomBSB()
        {
            Random random = new Random();
            string[] streetype = { "012004", "704230", "702389" };
            return streetype[random.Next(0, 2)];
        }

        public string FraudBSB()
        {
            // string fraudbsb = "012346";
            string fraudbsb = "062151";
            return fraudbsb;
        }

        public string IndueBankBSB()
        {
            string induebankbsb = "702389";
            return induebankbsb;
        }

        public string FraudAccountNo()
        {
            string fraudaccno = "45671031";
            return fraudaccno;
        }

        public async Task WriteToFile(string text)
        {
            string Filepath = TestUtility.GetAssemblyDirectory() + "//CaptureEmail.txt";
            Object locker = new Object();

            int timeOut = 100;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (true)
            {
                try
                {
                    //Wait for resource to be free
                    lock (locker)
                    {
                        using (FileStream file = new FileStream(Filepath, FileMode.Append, FileAccess.Write, FileShare.Read))
                        using (StreamWriter writer = new StreamWriter(file, Encoding.Unicode))
                        {
                            writer.Write(text.ToString());
                        }
                    }
                    break;
                }
                catch
                {
                    //File not available, conflict with other class instances or application
                }
                if (stopwatch.ElapsedMilliseconds > timeOut)
                {
                    //Give up.
                    break;
                }
                //Wait and Retry
                await Task.Delay(5);
            }
            stopwatch.Stop();
        }

        /// <summary>
        /// List of Holiday and Date
        /// </summary>
        /// <param name="holiday">national holiday</param>
        /// <param name="_date"> 0 For holiday and 1 date</param>
        /// <returns>holiday or date based on Paramater</returns>
        /// 
        public string[,] Holidays()
        {                     
            string[,] _val1 = new string[,]{{"NewYearDay","01/01/2018"},{"AustraliaDay","26/01/2018"},{"GoodFriday","30/03/2018"},{"EasterMonday","02/04/2018"},
                                           {"AnZacDay","25/04/2018"},{"ChristmasDay","25/12/2018"}};            
            return _val1;
        }
        public static string[,] AusHolidays = new string[,]{
            {"NewYearDay","01/01/2018"},
            {"AustraliaDay","01/26/2018"},
            {"GoodFriday","03/30/2018"},
            {"EasterMonday","04/02/2018"},                             
            {"AnZacDay","04/25/2018"},
            {"ChristmasDay","12/25/2018"}
        };

        public string BSBForWestpecBank()
        {
            string westpecBSB = "734070";
            return westpecBSB;   
        }

        public string nameOnAccountWestpec()
        {
            string westpecAccName = "westpec user";
            return westpecAccName;
        }
    }
}
