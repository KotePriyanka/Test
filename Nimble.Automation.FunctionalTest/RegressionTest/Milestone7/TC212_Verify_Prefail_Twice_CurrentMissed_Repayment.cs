using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest.RegressionTest.Milestone7
{
    [TestFixture, Parallelizable, Category("Milestone7"), Category("Prefail")]
    class TC212_Verify_Prefail_Twice_CurrentMissed_Repayment : TestEngine
    {
        [TearDown]
        public void Aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null;
        public TestEngine _testengine = new TestEngine();
        private string email = "";

        string strMessage, strUserType;
        ResultDbHelper _result = new ResultDbHelper();

        DateTime starttime { get; set; } = DateTime.Now;

        [TestCase(1100, "android", TestName = "TC212_Verify_Prefail_Twice_CurrentMissed_Repayment_android_RL"), Category("RL"), Ignore("Functionality not available"), Retry(2)]//android
        [TestCase(3800, "ios", TestName = "TC212_Verify_Prefail_Twice_CurrentMissed_Repayment_ios_RL")]
        public void TC212_Verify_Prefail_Twice_CurrentMissed_Repayment_RL(int loanamout, string strmobiledevice)
        {
            starttime = DateTime.Now;
            strMessage = string.Format("\r\n\t " + TestContext.CurrentContext.Test.Name + " Starts");
            strUserType = "RL";

            try
            {
                _driver = TestSetup(strmobiledevice);
                _homeDetails = new HomeDetails(_driver, "RL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
                _personalDetails = new PersonalDetails(_driver, "RL");
                _bankDetails = new BankDetails(_driver, "RL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

                ///login user
                _homeDetails.LoginExistingUser(TestData.Password, loanamout, TestData.ClientType.NewProduct, TestData.Feature.ReturnerSACCActive2prefailsmissedrepayment);

                if (GetPlatform(_driver))
                {
                    // click on More Button from Bottom Menu
                    _loanSetUpDetails.ClickMoreBtn();

                    //Logout
                    _loanSetUpDetails.Logout();
                }

                else
                {
                    //Logout
                    _loanSetUpDetails.Logout();
                }

            }

            catch (Exception ex)
            {
                strMessage += ex.Message;
                Assert.Fail(ex.Message);
            }
        }
    }
}
