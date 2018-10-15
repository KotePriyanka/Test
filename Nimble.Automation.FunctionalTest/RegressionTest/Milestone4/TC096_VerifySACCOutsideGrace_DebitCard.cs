using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest.Milestone4
{
    [TestFixture, Parallelizable, Category("Milestone4"), Category("Outside Grace")]
    class TC096_VerifySACCOutsideGrace_DebitCard : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private IWebDriver _driver = null;
        string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();

        private string email = "";
        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, email, starttime);
            email = "";
        }

        [TestCase(1100, "android", TestName = "TC096_VerifySACCOutsideGrace_DebitCarAndroid_RL_1100"), Category("NL"), Category("Mobile"), Retry(2)]
        [TestCase(1100, "ios", TestName = "TC096_VerifySACCOutsideGraceIOS_DebitCar_RL_1100")]
        public void TC096_VerifySACCOutsideGrace_DebitCar_RL(int loanamout, string strmobiledevice)
        {
            strUserType = "RL";
            _driver = TestSetup(strmobiledevice, "RL");
            _homeDetails = new HomeDetails(_driver, "RL");
            _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

            try
            {
                // Login with existing user
                _homeDetails.LoginExistingUser_SACCOutGrace(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.MissedRepaymentinContract);

                email = _homeDetails.RLEmailID;
                
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
