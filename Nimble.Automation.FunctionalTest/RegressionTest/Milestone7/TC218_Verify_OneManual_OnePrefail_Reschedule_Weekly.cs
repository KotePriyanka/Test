using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;


namespace Nimble.Automation.FunctionalTest.RegressionTest.Milestone7
{
    [TestFixture, Parallelizable, Category("Milestone7"), Category("Prefail")]
    class TC218_Verify_OneManual_OnePrefail_Reschedule_Weekly : TestEngine
    {
        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();
        private static string UpcomingLastPage = string.Empty;

        [TestCase(1000, "android", TestName = "TC218_Verify_OneManual_OnePrefail_Reschedule_Weekly_android_RL")]//, Ignore("Functionality not available"), Category("RL"), Retry(2)]
        [TestCase(1000, "ios", TestName = "TC218_Verify_OneManual_OnePrefail_Reschedule_Weekly_ios_RL")]
        public void TC218_Verify_OneManual_OnePrefail_Reschedule_Weekly_RL(int loanamout, string strmobiledevice)
        {
            strUserType = "RL";
            try
            {
                _driver = _testengine.TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
                _personalDetails = new PersonalDetails(_driver, "RL");
                _bankDetails = new BankDetails(_driver, "RL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

                //login user
                _homeDetails.LoginExistingUser(TestData.Password, loanamout, TestData.ClientType.NewProduct, TestData.Feature.ReturnerSACCActive2prefails);

                //Check availability of make a payment button
                Assert.IsFalse(_homeDetails.verifyMakeaPaymentBtn(), "make a payment button");

                //Check reschedule button
                Assert.IsFalse(_bankDetails.verifyRescheduleBtn(), "Reschedule button");

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
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}
