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
    [TestFixture, Parallelizable, Category("Milestone6"), Category("Delinquents MACC3 Score")]
    class TC192_Verify_Delinquents_MACC3_Score : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private LoanSetUpDetails _loanSetUpDetails = null;
        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1000, "android", TestName = "TC192_Verify_Delinquents_MACC3_Score_android_RL"), Category("RL"), Category("Mobile"), Retry(2)]
        [TestCase(3000, "ios", TestName = "TC192_Verify_Delinquents_MACC3_Score_ios_RL")]
        public void TC192_Verify_Delinquents_MACC3_Score_RL(int loanamout, string strmobiledevice)
        {
            string expectedMessage = "Unfortunately, we are unable to make a loan available to you at the moment.";
            strMessage += string.Format("\r\n\t " + TestContext.CurrentContext.Test.Name + " Starts");
            strUserType = "RL";
            try
            {
                _driver = TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");
               
                // Login with existing user
                _homeDetails.LoginExistingUser(TestData.RandomPassword, loanamout, TestData.ClientType.Delinquents, TestData.Feature.MACCdelinquent3);

                if (GetPlatform(_driver))
                {                    
                    //Verify the message
                    Assert.AreEqual(_loanSetUpDetails.confirmMessage(), expectedMessage);

                    // click on More Button from Bottom Menu
                    _loanSetUpDetails.ClickMoreBtn();

                    //Logout
                    _loanSetUpDetails.Logout();
                }
                else
                {
                    //Verify the message
                    Assert.AreEqual(_loanSetUpDetails.confirmMessage(), expectedMessage);

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
