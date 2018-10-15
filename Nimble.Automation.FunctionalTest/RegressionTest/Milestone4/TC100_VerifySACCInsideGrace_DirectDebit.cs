using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest.Milestone4
{
    //<Summary>
    // Member Area Make a Repayment inside client's grace period using Direct Debit
    //</Summary>

    [TestFixture, Parallelizable, Category("Milestone4"), Category("Inside Grace")]
    class TC100_VerifySACCInsideGrace_DirectDebit : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private LoanSetUpDetails _loanSetUpDetails = null;
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1100, "android", TestName = "TC100_VerifySACCInsideGrace_DirectDebit_android_RL"), Category("NL"), Retry(2)]
        [TestCase(1100, "ios", TestName = "TC100_VerifySACCInsideGrace_DirectDebit_ios_RL")]
        public void TC100_VerifySACCInsideGrace_DirectDebit_RL(int loanamout, string strmobiledevice)
        {
             strUserType = "RL";
            try
            {
                _driver = TestSetup(strmobiledevice,"RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

                // Login with existing user
                _homeDetails.LoginExistingUser_SACCOutGrace(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.MissedRepaymentinGrace);

                // Click Make a Payment button
                _homeDetails.ClickMakeRepaymentBtn();

                // Select Direct Debit as the payment option and Continue
                _homeDetails.CheckRepaymentDirectDebitChkbx();

                //Click on continue button
                _homeDetails.ClickRepaymentContinueBtn();

                //Click on payout button
                _homeDetails.clickPayoutButton();      

                // Confirm you want to repay by Direct Debit
                //_homeDetails.ClickRepaymentDirectDebitBtn();

                // Confirm payment on popup window
                _homeDetails.ClickRepaymentConfirmBtn();

                //Verify Confirmed Message
                Assert.IsTrue(_loanSetUpDetails.GetConfirmedTxtSetUp().Contains("We'll be in touch to confirm your payment has been made."), "Incorrect message");

                //Logout
                _loanSetUpDetails.Logout();

                // Payment submitted + email sent to client at this point
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}
