using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest.Milestone4
{
    //<Summary>
    // Member Area Make a Repayment inside client's grace period using Debit Card
    // Has the following requirements to pass:
    // 1. Access to ezidebit api
    // 2. Our internal environment config enabled : 'online_cardpayments_enabled = true'
    // 3. The following override : 'Cp:P'
    //</Summary>

    [TestFixture, Parallelizable, Category("Milestone4"), Category("Inside Grace")]
    class TC101_VerifySACCInsideGrace_DebitCard : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1200, "android", TestName = "TC101_VerifySACCInsideGrace_DebitCard_android_RL"), Category("NL"), Retry(2)]
        [TestCase(1200, "ios", TestName = "TC101_VerifySACCInsideGrace_DebitCard_ios_RL")]
        public void TC101_VerifySACCInsideGrace_DebitCard_RL(int loanamout, string strmobiledevice)
        {
            strUserType = "RL";
            try
            {
                _driver = TestSetup(strmobiledevice,"RL");
                _homeDetails = new HomeDetails(_driver, "RL");

                // Login with existing user
                _homeDetails.LoginExistingUser_SACCOutGrace(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.MissedRepaymentinGrace);

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

                // Payment submitted + email sent to client at this point
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}
