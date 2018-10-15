using System;
using Nimble.Automation.Accelerators;
using NUnit.Framework;
using Nimble.Automation.Repository;
using OpenQA.Selenium;

namespace Nimble.Automation.FunctionalTest.Milestone5
{
    // Create a debug client and then use the Forgot Password feature
    // Only sends a reset password email, it does not reset the debug client's default password
    [TestFixture, Parallelizable, Category("Milestone5"), Category("Misc")]
    class TC154_ForgotPassword
    {

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, strEmailID, starttime);
        }

        private HomeDetails _homeDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();
        private string strEmailID = "";

        [TestCase(800, "android", TestName = "TC154_ForgotPassword_RL_800"), Category("NL"), Retry(2)]
        [TestCase(2900, "ios", TestName = "TC154_ForgotPassword_RL_2900")]
        public void TC154_ForgotPasswordSubmit_RL(int loanamout, string strmobiledevice)
        {
           strUserType = "RL";
            try
            {
                _driver = _testengine.TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");

                // Create debug client + use Forgot Password page
                 strEmailID = _homeDetails.CreateClient(TestData.ClientType.NewProduct,
                    TestData.Feature.NewProductAdvancePaidClean);
                _homeDetails.NimbleExistingUserForgotPassword(strEmailID);

                // Check for a Forgot Password success message
                Assert.IsTrue(_homeDetails.VerifyForgotPasswordSuccessTxt());

            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}
