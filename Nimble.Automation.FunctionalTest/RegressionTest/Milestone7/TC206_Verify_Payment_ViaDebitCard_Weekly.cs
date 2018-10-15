using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest.RegressionTest.Milestone7
{
    [TestFixture, Parallelizable, Category("Milestone7"), Category("Prefail")]
    class TC206_Verify_Payment_ViaDebitCard_Weekly : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        private LoanSetUpDetails _loanSetUpDetails = null;

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1200, "android", TestName = "TC206_VerifyPaymentViaDebitCard_android_RL"), Category("RL")]//, Ignore("Functionality not available"), Retry(2)]
        [TestCase(1200, "ios", TestName = "TC206_VerifyPaymentViaDebitCard_ios_RL")]
        public void TC206_VerifyPaymentViaDebitCard_RL(int loanamout, string strmobiledevice)
        {
            strUserType = "RL";
            try
            {
                _driver = TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

                // Login with existing user
                _homeDetails.LoginExistingUser(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.ReturnerSACCActive);

                // Edit Profile to add success override (Cp:P)
                if (GetPlatform(_driver))
                {
                    // Mobile Site flow
                    _homeDetails.ClickMobileMoreBtn();
                    _homeDetails.ClickMobileYourProfileLnk();
                    _homeDetails.ClickMobileYourProfileContactLnk();
                    _homeDetails.EnterMobileYourProfileStreetNameTxt("Cp:P");
                    _homeDetails.ClickMobileYourProfileSaveBtn();
                    _homeDetails.ClickMobileDashboardLnk();
                }
                else
                {
                    // Desktop flow
                    _homeDetails.ClickMemberAreaEditProfileLnk();
                    _homeDetails.ClickEditProfileContactDetailsBtn();
                    _homeDetails.EnterEditProfileStreetNameTxt("Cp:P");
                    _homeDetails.ClickEditProfileSaveBtn();
                    _homeDetails.ClickEditProfileLoanDashboardBtn();
                }

                if (PrefailReschedule)
                {
                    // Click Make a Payment button
                    _homeDetails.ClickMakeRepaymentBtn();

                    // Select Direct Card as the payment option and Continue
                    _homeDetails.CheckRepaymentDebitCardChkbx();
                    _homeDetails.ClickRepaymentContinueBtn();

                    // Pay via Debit Card page
                    // Reference page for testing valid card numbers: 
                    // http://www.braemoor.co.uk/software/creditcard.shtml
                    _homeDetails.EnterRepaymentNameOnCardTxt("MR TEST APPLE");
                    _homeDetails.EnterRepaymentCardNumberTxt("4111 1111 1111 1111");
                    _homeDetails.EnterRepaymentExpiryTxt("12/18");
                    _homeDetails.EnterRepaymentSecurityTxt("300");
                    _homeDetails.ClickRepaymentDebitCardBtn();

                    // Confirm payment on popup window
                    _homeDetails.ClickRepaymentDebitCardDoneBtn();

                    //Click on logout
                    _loanSetUpDetails.Logout();
                }
                else
                {
                    // prefail functionality disabled
                    //Verify request money button
                    Assert.IsTrue(_homeDetails.verifyRequestMoneyBtn(), "Request Button");

                    //Click on logout
                    _loanSetUpDetails.Logout();
                }
                // Payment submitted + email sent to client at this point
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}
