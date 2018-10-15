using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest.Milestone4
{
    //<Summary>
    // Member Area Make a Repayment inside client's grace period using BPAY
    //</Summary>

    [TestFixture, Parallelizable, Category("Milestone4"), Category("Inside Grace")]
    class TC102_VerifySACCInsideGrace_BPAY : TestEngine
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

        [TestCase(1300, "android", TestName = "TC102_VerifySACCInsideGrace_BPAY_android_RL"), Category("NL"), Category("Mobile"), Retry(2)]
        [TestCase(1300, "ios", TestName = "TC102_VerifySACCInsideGrace_BPAY_ios_RL")]
        public void TC102_VerifySACCInsideGrace_BPAY_RL(int loanamout, string strmobiledevice)
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

                if (onlineBpaymentsIsEnabled == "true")
                {

                    // Select BPAY as the payment option and Continue
                    _homeDetails.CheckRepaymentBPAYChkbx();
                    _homeDetails.ClickRepaymentContinueBtn();

                    // Confirm you want to repay by Direct Debit
                    _homeDetails.ClickRepaymentBPAYBtn();

                    // Confirm payment on popup window
                    _homeDetails.ClickRepaymentConfirmBtn();

                    // Payment submitted + email sent to client at this point
                    Assert.IsTrue(_loanSetUpDetails.GetConfirmedTxtSetUp().Contains("We'll be in touch to confirm your payment has been made."), "Incorrect message");

                    //Click on logout
                    _loanSetUpDetails.Logout();
                }
                else
                {
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
