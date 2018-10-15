using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest.Milestone4
{
    [TestFixture, Parallelizable, Category("Milestone4"), Category("Outside Grace")]
    class TC099_VerifySACCOutsideGrace_EFT : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1100, "android", TestName = "TC099_VerifySACCOutsideGrace_EFT_Android_RL_1100"), Category("NL"), Category("Mobile"), Ignore("Repayment through EFT removed from application")]
        [TestCase(1100, "ios", TestName = "TC099_VerifySACCOutsideGrace_EFT_IOS_RL_1100")]
        public void TC099_VerifySACCOutsideGrace_EFT_RL(int loanamout, string strmobiledevice)
        {
            strUserType = "RL";
            _driver = TestSetup(strmobiledevice, "RL");
            _homeDetails = new HomeDetails(_driver, "RL");
            _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

            try
            {
                // Login with existing user
                _homeDetails.LoginExistingUser_SACCOutGrace(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.MissedRepaymentinContract);

                // Click Make a Payment button
                _homeDetails.ClickMakeRepaymentBtn();

                // Select EFT as the payment option and Continue
                _homeDetails.CheckRepaymentEFTChkbx();

                _homeDetails.ClickRepaymentContinueBtn();

                // Confirm you want to repay by Direct Debit
                _homeDetails.ClickRepaymentEFTBtn();

                // Confirm payment on popup window
                _homeDetails.ClickRepaymentConfirmBtn();

                //Verify Confirmed Message
                Assert.IsTrue(_loanSetUpDetails.GetConfirmedTxtSetUp().Contains("We'll be in touch to confirm your payment has been made."), "Incorrect message");
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}
