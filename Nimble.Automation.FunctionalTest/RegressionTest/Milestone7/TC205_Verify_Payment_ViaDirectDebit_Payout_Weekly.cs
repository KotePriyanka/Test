using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest.RegressionTest.Milestone7
{
    [TestFixture, Parallelizable, Category("Milestone7"), Category("Prefail")]
    class TC205_Verify_Payment_ViaDirectDebit_Payout_Weekly : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1100, "android", TestName = "TC205_VerifyPaymentViaDirectDebit_Payout_android_RL"), Category("RL")]//, Ignore("Functionality not available"), Retry(2)]
        [TestCase(1100, "ios", TestName = "TC205_VerifyPaymentViaDirectDebit_Payout_ios_RL")]
        public void TC205_VerifyPaymentViaDirectDebit_Payout_RL(int loanamout, string strmobiledevice)
        {
            strUserType = "RL";
            try
            {
                _driver = TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");
                _bankDetails = new BankDetails(_driver, "RL");

                // Login with existing user
                _homeDetails.LoginExistingUser(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.ReturnerSACCActive);

                if (PrefailReschedule)
                {
                    // Click Make a Payment button
                    _homeDetails.ClickMakeRepaymentBtn();

                    // Select Direct Debit as the payment option and Continue
                    _homeDetails.CheckRepaymentDirectDebitChkbx();

                    //Click confirm button
                    _homeDetails.ClickRepaymentContinueBtn();

                    // Confirm you want to payout
                    _homeDetails.clickPayoutButton();

                    // Confirm payment on popup window
                    _homeDetails.ClickRepaymentConfirmBtn();

                    if (GetPlatform(_driver))
                    {
                        //Click on finish button
                        _bankDetails.clickFinishBtn();

                        // click on More Button from Bottom Menu
                        _loanSetUpDetails.ClickMoreBtn();

                        //Logout
                        _loanSetUpDetails.Logout();
                    }
                    else
                    {
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
