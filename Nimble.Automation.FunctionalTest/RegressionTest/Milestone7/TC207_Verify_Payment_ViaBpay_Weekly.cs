using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest.RegressionTest.Milestone7
{
    [TestFixture, Parallelizable, Category("Milestone7"), Category("Prefail")]
    class TC207_Verify_Payment_ViaBpay_Weekly : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private IWebDriver _driver = null;
        private BankDetails _bankDetails = null;
        string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1100, "android", TestName = "TC207_VerifyPaymentViaBpay_Android_RL_1100"), Category("RL")]//, Ignore("Functionality not available"), Retry(2)]
        [TestCase(1100, "ios", TestName = "TC207_VerifyPaymentViaBpay_IOS_RL_1100")]
        public void TC207_VerifyPaymentViaBpay_RL(int loanamout, string strmobiledevice)
        {
            strUserType = "RL";
            _driver = TestSetup(strmobiledevice, "RL");
            _homeDetails = new HomeDetails(_driver, "RL");
            _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");
            _bankDetails = new BankDetails(_driver, "RL");

            try
            {
                // Login with existing user
                _homeDetails.LoginExistingUser(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.ReturnerSACCActive);

                if (PrefailReschedule)
                {
                    // Click Make a Payment button
                    _homeDetails.ClickMakeRepaymentBtn();

                    // Select BPAY as the payment option and Continue
                    _homeDetails.CheckRepaymentBPAYChkbx();

                    _homeDetails.ClickRepaymentContinueBtn();

                    // Confirm you want to repay by Direct Debit
                    _homeDetails.ClickRepaymentBPAYBtn();                   

                    if (GetPlatform(_driver))
                    {
                        //Click confirm Bpay
                        _homeDetails.ClickBpayConfirmBtn();

                        //Click on finish button
                        _bankDetails.clickFinishBtn();

                        // click on More Button from Bottom Menu
                        _loanSetUpDetails.ClickMoreBtn();

                        //Logout
                        _loanSetUpDetails.Logout();
                    }
                    else
                    {
                        // Confirm payment on popup window
                        _homeDetails.ClickRepaymentConfirmBtn();

                        //Verify Confirmed Message
                        Assert.IsTrue(_loanSetUpDetails.GetConfirmedTxtSetUp().Contains("We'll be in touch to confirm your payment has been made."), "Incorrect message");

                        //Click on logout
                        _loanSetUpDetails.Logout();
                    }

                }

                else
                {
                    // prefail functionality disabled
                    //Verify request money button
                    Assert.IsTrue(_homeDetails.verifyRequestMoneyBtn(), "Request Button");

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
