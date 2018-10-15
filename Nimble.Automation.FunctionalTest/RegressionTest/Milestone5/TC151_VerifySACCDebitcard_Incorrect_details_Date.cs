using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest.Milestone4
{
    [TestFixture, Parallelizable, Category("Milestone5"), Category("Debit Card")]
    class TC151_VerifySACCDebitcard_Incorrect_details_Date
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1200, "android", TestName = "TC151_VerifySACCDebitcard_Incorrect_Date_android_RL"), Category("RL"), Retry(2)]
        [TestCase(2200, "ios", TestName = "TC151_VerifySACCDebitcard_Incorrect_Date_ios_RL")]
        public void TC151_VerifySACCDebitcard_Incorrect_Date_RL(int loanamout, string strmobiledevice)
        {
            strUserType = "RL";
            try
            {
                _driver = _testengine.TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
                _personalDetails = new PersonalDetails(_driver, "RL");
                _bankDetails = new BankDetails(_driver, "RL");

                //Go to the homepage and click the start application button and then the Request money button
                string strEmail = _homeDetails.homeFunctions_RL(TestData.ClientType.NewProduct, TestData.Feature.MissedRepaymentinGrace);

                // Click Make a Payment button
                _homeDetails.ClickMakeRepaymentBtn();
                
                // Select Direct Card as the payment option and Continue
                _homeDetails.CheckRepaymentDebitCardChkbx();
                _homeDetails.ClickRepaymentContinueBtn();

                // Pay via Debit Card page using incorrect expiry date
                // Reference page for testing valid card numbers: 
                // http://www.braemoor.co.uk/software/creditcard.shtml
                _homeDetails.EnterRepaymentNameOnCardTxt("MR TEST APPLE");
                _homeDetails.EnterRepaymentCardNumberTxt("4111 1111 1111 1111");
                _homeDetails.EnterRepaymentExpiryTxt("02/17");
                _homeDetails.EnterRepaymentSecurityTxt("300");
                //_homeDetails.ClickRepaymentDebitCardBtn();

                //Payment failed 
                Assert.IsTrue(_bankDetails.GetIncorrectDateTxt().Contains("This card has now expired."));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                strMessage += ex.Message;
            }
        }
    }
}
