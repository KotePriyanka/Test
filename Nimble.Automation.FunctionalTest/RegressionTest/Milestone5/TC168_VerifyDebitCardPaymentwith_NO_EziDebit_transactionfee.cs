using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimble.Automation.FunctionalTest.RegressionTest.Milestone5
{

    [TestFixture, Parallelizable, Category("Milestone5"), Category("Debit Card")]
    class TC168_VerifyDebitCardPaymentwith_NO_EziDebit_transactionfee : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private BankDetails _bankDetails = null;
        private LoanSetUpDetails _loanSetupDetails = null;
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1100, "android", TestName = "TC168_VerifyDebitCardPaymentwith_NO_EziDebit_transactionfee_android_RL"), Category("NL"), Retry(2)]
        [TestCase(2700, "ios", TestName = "TC168_VerifyDebitCardPaymentwith_NO_EziDebit_transactionfee_ios_RL")]
        public void TC168_VerifyDebitCardPaymentwith_NO_EziDebit_transactionfee_RL(int loanamout, string strmobiledevice)
        {
           strUserType = "RL";
            try
            {
                _driver = _testengine.TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
                _personalDetails = new PersonalDetails(_driver, "RL");
                _bankDetails = new BankDetails(_driver, "RL");
                _loanSetupDetails = new LoanSetUpDetails(_driver, "RL");

                //Go to the homepage and click the start application button and then the Request money button
                string strEmail = _homeDetails.homeFunctions_RL(TestData.ClientType.NewProduct, TestData.Feature.MissedRepaymentinGrace);

                // Click Make a Payment button
                _homeDetails.ClickMakeRepaymentBtn();

                // Select Direct Card as the payment option and Continue
                _homeDetails.CheckRepaymentDebitCardChkbx();
                _homeDetails.ClickRepaymentContinueBtn();

                // enter minimum repayment amount lessthan $10
                _homeDetails.EnterRepaymentAmount("$2");

                // Verify min rules & warning message to "Repayment amount"
                Assert.IsTrue(_homeDetails.GetCheckRepaymentErrorMessage().Contains("Can not accept payment less than $10."));

                // enter maximum repayment amount greaterthan $10100
                _homeDetails.EnterRepaymentAmount("$10100");

                // Verify max rules & warning message to "Repayment amount"
                Assert.IsTrue(_homeDetails.GetCheckRepaymentErrorMessage().Contains("You can only pay up to your current payout amount"));

                // enter correct repayment amount $500
                _homeDetails.EnterRepaymentAmount("$500");

                _homeDetails.EnterRepaymentNameOnCardTxt("MR TEST APPLE");
                _homeDetails.EnterRepaymentCardNumberTxt("4111 1111 1111 1111");
                _homeDetails.EnterRepaymentExpiryTxt("12/18");
                _homeDetails.EnterRepaymentSecurityTxt("300");
                _homeDetails.ClickRepaymentDebitCardBtn();

                //Payment failed 
                Assert.IsTrue(_bankDetails.GetCheckPaymentMessage().Contains("Oops! Your card payment was unsuccessful."));

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

                _homeDetails.EnterRepaymentNameOnCardTxt("MR TEST APPLE");
                _homeDetails.EnterRepaymentCardNumberTxt("4111 1111 1111 1111");
                _homeDetails.EnterRepaymentExpiryTxt("12/18");
                _homeDetails.EnterRepaymentSecurityTxt("300");
                _homeDetails.ClickRepaymentDebitCardBtn();

                // Confirm payment on popup window
                _homeDetails.ClickRepaymentDebitCardDoneBtn();

                //Check that payment is successful
                Assert.IsTrue(_bankDetails.GetCheckLoanPaidTxt().Contains("Loan Repaid"));

                //logout
                _loanSetupDetails.Logout();
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}
