using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest.Milestone4
{
    //<Summary>
    // Member Area Make a Repayment inside client's grace period using EFT
    //</Summary>

    [TestFixture, Parallelizable, Category("Milestone4"), Category("Inside Grace")]
    class TC103_VerifySACCInsideGrace_DebitCard : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null;string strMessage,strUserType="";  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1400, "android", TestName = "TC103_VerifySACCInsideGrace_EFT_android_RL"), Category("NL"), Ignore("Repayment through EFT removed from application")]
        [TestCase(1400, "ios", TestName = "TC103_VerifySACCInsideGrace_EFT_ios_RL")]
        public void TC103_VerifySACCInsideGrace_EFT_RL(int loanamout, string strmobiledevice)
        {
            strUserType = "RL";
            try
            {
                _driver = TestSetup(strmobiledevice,"RL");
                _homeDetails = new HomeDetails(_driver, "RL");

                // Login with existing user
                _homeDetails.LoginExistingUser_SACCOutGrace(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.MissedRepaymentinGrace);

                // Click Make a Payment button
                _homeDetails.ClickMakeRepaymentBtn();

                // Select EFT as the payment option and Continue
                _homeDetails.CheckRepaymentEFTChkbx();

                _homeDetails.ClickRepaymentContinueBtn();

                // Confirm you want to repay by Direct Debit
                _homeDetails.ClickRepaymentEFTBtn();

                // Confirm payment on popup window
                _homeDetails.ClickRepaymentConfirmBtn();

                // Payment submitted + email sent to client at this point
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}
