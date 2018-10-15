using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;
using System.Configuration;

namespace Nimble.Automation.FunctionalTest.Milestone4
{
    //<Summary>
    // Member Area Make a Repayment inside client's grace period using Debit Card
    // Has the following requirements to pass:
    // 1. Access to ezidebit api
    // 2. Our internal environment config enabled : 'online_cardpayments_enabled = true'
    // 3. The following override : 'Cp:P'
    //</Summary>

    [TestFixture, Parallelizable, Category("Milestone5"), Category("Debit Card")]
    class TC148_VerifySACCInsideGrace_DebitCard_CloseSite : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();
        private GenerateRandom _randomVal = new GenerateRandom();
        private LoanSetUpDetails _loanSetUpDetails = null;
        private string strEmail = "";

       [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, strEmail, starttime);
            strEmail = "";
        }
        
        [TestCase(1200, "android", TestName = "TC148_VerifySACCInsideGrace_DebitCard_CloseSite_android_RL"), Category("RL"), Retry(2)]
        [TestCase(2200, "ios", TestName = "TC148_VerifySACCInsideGrace_DebitCard_CloseSite_ios_RL")]
        public void TC148_VerifySACCInsideGrace_DebitCard_CloseSite_RL(int loanamout, string strmobiledevice)
        {
            strUserType = "RL";
            try
            {
                _driver = _testengine.TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
                _personalDetails = new PersonalDetails(_driver, "RL");
                _bankDetails = new BankDetails(_driver, "RL");
                //Go to the homepage
                strEmail = _homeDetails.homeFunctions_RL(TestData.ClientType.NewProduct, TestData.Feature.MissedRepaymentinGrace);

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
                _homeDetails.EnterRepaymentExpiryTxt("12/17");
                _homeDetails.EnterRepaymentSecurityTxt("300");
                _homeDetails.ClickRepaymentDebitCardBtn();

                Thread.Sleep(5000);

                //LogOut
                _driver.Quit(); 

                _driver = _testengine.TestSetup(strmobiledevice,"RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
                _personalDetails = new PersonalDetails(_driver, "RL");
                _bankDetails = new BankDetails(_driver, "RL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

                _homeDetails.ClickLoginBtn();

                _homeDetails.LoginLogoutUser(strEmail, "password");
                
                //Check that payment is successful
                Assert.IsTrue(_bankDetails.GetCheckLoanPaidTxt().Contains("Loan Repaid"));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                strMessage += ex.Message;
            }

        }
    }

}

