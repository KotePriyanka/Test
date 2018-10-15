using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest
{
    //<Summary>
    //Incorrect password at Login
    //</Summary>

    [TestFixture, Parallelizable, Category("Milestone4"), Category("Misc")]
    class TC135_IncorrectPasswordatLogin_RL : TestEngine
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
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TestCase(1000, "android", TestName = "TC135_IncorrectPasswordatLogin_RL_SACC_1000"), Category("RL"), Category("Mobile"), Retry(2)]
        [TestCase(2500, "ios", TestName = "TC135_IncorrectPasswordatLogin_RL_MACC_2500")]
        public void TC135_VerifyIncorrectPasswordatLogin_RL(int loanamout, string strmobiledevice)
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

                //Go to the homepage and click the start application button and then the Request money button
                _homeDetails.LoginExistingUser(TestData.IncorrectPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.NewProductAdvancePaidClean);

                //Verify The email or password provided is incorrect message
                string errormsg = "The email or password provided is incorrect.";
                Assert.IsTrue(errormsg.Contains("The email or password provided is incorrect."));
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}
