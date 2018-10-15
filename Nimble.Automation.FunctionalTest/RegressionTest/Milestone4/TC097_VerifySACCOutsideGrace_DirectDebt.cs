using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest.Milestone4
{
    [TestFixture, Parallelizable, Category("Milestone4"), Category("Outside Grace")]
    class TC097_VerifySACCOutsideGrace_DirectDebt : TestEngine
    {

        private HomeDetails _homeDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1100, "android", TestName = "TC097_VerifySACCOutsideGrace_DirectDebt_Android_RL_1100"), Category("NL"), Retry(2)]
        [TestCase(1100, "ios", TestName = "TC097_VerifySACCOutsideGrace_DirectDebt_IOS_RL_1100")]
        public void TC097_VerifySACCOutsideGrace_DirectDebt_RL(int loanamout, string strmobiledevice)
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

                // Select Direct Debit as the payment option and Continue
                _homeDetails.CheckRepaymentDirectDebitChkbx();

                _homeDetails.ClickRepaymentContinueBtn();

                // Confirm you want to repay by Direct Debit
                _homeDetails.ClickRepaymentDirectDebitBtn();

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
