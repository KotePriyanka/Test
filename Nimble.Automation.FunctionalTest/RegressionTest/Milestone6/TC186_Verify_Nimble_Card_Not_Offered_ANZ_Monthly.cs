using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimble.Automation.FunctionalTest.RegressionTest.Milestone6
{
    [TestFixture, Parallelizable, Category("Milestone6"), Category("Nimble Card Not Offered ANZ Monthly")]
    class TC186_Verify_Nimble_Card_Not_Offered_ANZ_Monthly : TestEngine
    { 
        private HomeDetails _homeDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private IWebDriver _driver = null; string strMessage, strUserType=""; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1500, "android", TestName = "TC186_Verify_Nimble_Card_Not_Offered_ANZ_Monthly_android_RL"), Category("RL"), Retry(2)]
        [TestCase(3800, "ios", TestName = "TC186_Verify_Nimble_Card_Not_Offered_ANZ_Monthly_ios_RL")]
        public void TC186_Verify_Nimble_Card_Not_Offered_ANZ_Monthly_RL(int loanamout, string strmobiledevice)
        {
            strUserType = "RL";
            try
            {
                _driver = TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _personalDetails = new PersonalDetails(_driver, "RL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

                // Login with existing user
                _homeDetails.LoginExistingUser(TestData.RandomPassword, loanamout, TestData.ClientType.NimbleCardEligibility, TestData.Feature.ANZactivemonthlycardinEligible);

                if (GetPlatform(_driver))
                {
                    // Mobile Site flow
                    //Click on more button
                    _loanSetUpDetails.ClickMoreBtn();

                    //Check for order card link
                    Assert.IsFalse(_loanSetUpDetails.verifyOrderCardLink(), "Order card link");

                    //Click on more button
                    _loanSetUpDetails.ClickMoreBtn();

                    //Click on logout
                    _loanSetUpDetails.Logout();
                }
                else
                {
                    //Desktop flow
                    //Check availability of Bpay link
                    Assert.IsFalse(_loanSetUpDetails.verifyBpayPageLink(), "Bpay page link");

                    //Check for order card link
                    Assert.IsFalse(_loanSetUpDetails.verifyOrderCardLink(), "Order card link");

                    //Check for activate card link
                    Assert.IsFalse(_loanSetUpDetails.verifyActivateCardLink(), "Activate card link");

                    //Click on logout
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
