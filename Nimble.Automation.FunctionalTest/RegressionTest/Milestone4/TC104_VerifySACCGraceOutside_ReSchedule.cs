using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest
{

    //<Summary>
    //Verifying loan application by reschediling the loan payment
    //</Summary>

    [TestFixture, Parallelizable, Category("Milestone4"), Category("Outside Grace")]
    class TC104_VerifySACCGraceOutside_ReSchedule_Add : TestEngine
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

        [TestCase(1000, "android", TestName = "TC104_VerifySACCGraceOutside_ReSchedule_AddPayment_RL"), Category("NL"), Category("Mobile"), Retry(2)]
        public void TC104_VerifySACCGraceOutside_ReSchedule_AddPayment(int loanamout, string strmobiledevice)
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
                _homeDetails.LoginGracePeriodUser(TestData.Password, TestData.ClientType.NewProduct, TestData.Feature.MissedRepaymentinContract);

                //Fetching the missed repayment message
                string MiissedRepaymentmessage = _bankDetails.VerifyMissedRepaymentMessage();

                Assert.IsTrue(MiissedRepaymentmessage.Contains("Oops"), "Message not displayed");

                //Fetching missed repayment value from first page
                string missedRepayment = _bankDetails.GetMissedRepaymentFirstPage();

                //click on Reschedule button
                _bankDetails.ClickRescheduleButton();

                //Click on extend checkbox
                _bankDetails.ClickExtentCheckBox();

                //get upcoming repayment from first page
                string UpcomingFirstPage = _bankDetails.GetUpcomingRepaymentFirstPageExtend();

                //Click continue button after reschedule
                _bankDetails.ClickRescheduleContinueButton();

                //Fetch Reschedule message
                string RescheduleMessage = _bankDetails.VerifyRescheduleMessage();

                Assert.IsTrue(RescheduleMessage.Contains("Thanks!"), "Message not displayed");

                //Get missed repayment from last page
                string missedRepayment1 = _bankDetails.GetMissedRepaymentLastPage();

                //Get upcoming repayment from last page
                string UpcomingLastPage = _bankDetails.GetUpcomingRepaymentLastPageExtend();

                Assert.AreEqual(missedRepayment, missedRepayment1, "Missed repayments are not matching");

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

    [TestFixture, Parallelizable, Category("Milestone4"), Category("Outside Grace")]
    class TC104_VerifySACCGraceOutside_ReSchedule : TestEngine
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

        [TestCase(1000, "android", TestName = "TC104_VerifySACCGraceOutside_ReSchedule_ReSchedulePayment"), Category("RL"), Category("Mobile"), Retry(2)]
        public void TC104_VerifySACCGraceOutside_ReSchedulePayment(int loanamout, string strmobiledevice)
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
                _homeDetails.LoginGracePeriodUser(TestData.Password, TestData.ClientType.NewProduct, TestData.Feature.MissedRepaymentinContract);

                //Fetching the missed repayment message
                string MiissedRepaymentmessage = _bankDetails.VerifyMissedRepaymentMessage();

                Assert.IsTrue(MiissedRepaymentmessage.Contains("Oops"), "Message not displayed");

                //Fetching missed repayment value from first page
                string missedRepayment = _bankDetails.GetMissedRepaymentFirstPage();

                //click on Reschedule button
                _bankDetails.ClickRescheduleButton();

                //click on Divide CheckBox
                _bankDetails.ClickDivideCheckBox();

                //get upcoming repayment from first page
                string UpcomingFirstPage = _bankDetails.getUpcomingRepaymentFirstPage();

                //Click continue button after reschedule
                _bankDetails.ClickRescheduleContinueButton();

                //Fetch Reschedule message
                string RescheduleMessage = _bankDetails.VerifyRescheduleMessage();

                Assert.IsTrue(RescheduleMessage.Contains("Thanks!"), "Message not displayed");

                //Get missed repayment from last page
                string missedRepayment1 = _bankDetails.GetMissedRepaymentLastPage();

                //Get upcoming repayment from last page
                string UpcomingLastPage = _bankDetails.PrefailUpcomingRepaymentFinalPage();

                Assert.AreEqual(missedRepayment, missedRepayment1, "Missed repayments are not matching");

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


















