using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace Nimble.Automation.FunctionalTest.RegressionTest.Milestone7
{
    [TestFixture, Parallelizable, Category("Milestone7"), Category("Prefail")]
    class TC217_Verify_Prefail_One_Remaining_Repayment
    {
        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TestCase(1000, "android", TestName = "TC217_Verify_Prefail_remaining_payment_android_RL"), Category("RL"), Category("Mobile")]//, Ignore("Functionality not available"), Retry(2)]
        [TestCase(1000, "ios", TestName = "TC217_Verify_Prefail_remaining_payment_ios_RL")]
        public void TC217_Verify_Prefail_remaining_payment_RL(int loanamout, string strmobiledevice)
        {
            strUserType = "RL";
            try
            {
                _driver = _testengine.TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
                _personalDetails = new PersonalDetails(_driver, "RL");
                _bankDetails = new BankDetails(_driver, "RL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

                //login user
                _homeDetails.LoginExistingUser(TestData.Password, loanamout, TestData.ClientType.NewProduct, TestData.Feature.ReturnerSACCActive1RemainingPayment);

                //click on Reschedule button
                _bankDetails.ClickRescheduleButton();

                //Verify divide radio btn
                Assert.IsFalse(_bankDetails.verifyDivideRadioBtn(), "Divide button");

                //click on Divide CheckBox
                _bankDetails.ClickExtentCheckBox();

                //get upcoming repayment from first page
                string UpcomingFirstPage = _bankDetails.getPrefailUpcomingRepaymentFirstPage("0");

                //Click continue button after reschedule
                _bankDetails.ClickRescheduleContinueButton();

                //Fetch Reschedule message
                string RescheduleMessage = _bankDetails.VerifyRescheduleMessage();

                Assert.IsTrue(RescheduleMessage.Contains("Thanks!"), "Message not displayed");

                //Get upcoming repayment from last page
                string UpcomingLastPage = _bankDetails.GetPrefailUpcomingRepaymentLastPage("4");

                //Assert.AreEqual(missedRepayment, missedRepayment1, "Missed repayments are not matching");
                Assert.AreEqual(UpcomingFirstPage, UpcomingLastPage, "Missed repayments are not matching");

                //Logout
                _loanSetUpDetails.Logout();
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}