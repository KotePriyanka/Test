using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimble.Automation.FunctionalTest.RegressionTest.Milestone7
{
    [TestFixture, Parallelizable, Category("Milestone7"), Category("Prefail")]
    class TC208_Verify_Prefail_Reschedule_DivideOver_Weekly : TestEngine
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

        [TestCase(1000, "android", TestName = "TC208_Verify_Prefail_Reschedule_DivideOver_android_RL"), Category("RL"), Category("Mobile")]//, Ignore("Functionality not available"), Retry(2)]
        [TestCase(1000, "ios", TestName = "TC208_Verify_Prefail_Reschedule_DivideOver_ios_RL")]
        public void TC208_Verify_Prefail_Reschedule_DivideOver_RL(int loanamout, string strmobiledevice)
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
                _homeDetails.LoginExistingUser(TestData.Password, loanamout, TestData.ClientType.NewProduct, TestData.Feature.ReturnerSACCActive);

                if (PrefailReschedule)
                {
                    //click on Reschedule button
                    _bankDetails.ClickRescheduleButton();

                    //click on Divide CheckBox
                    _bankDetails.ClickDivideCheckBox();

                    //get upcoming repayment from first page
                    string UpcomingFirstPage = _bankDetails.getPrefailUpcomingRepaymentFirstPage("3");

                    //Click continue button after reschedule
                    _bankDetails.ClickRescheduleContinueButton();

                    //Fetch Reschedule message
                    string RescheduleMessage = _bankDetails.VerifyRescheduleMessage();

                    Assert.IsTrue(RescheduleMessage.Contains("Thanks!"), "Message not displayed");

                    //Get upcoming repayment from last page
                    string UpcomingLastPage = _bankDetails.GetPrefailUpcomingRepaymentLastPage("5");

                    //Assert.AreEqual(missedRepayment, missedRepayment1, "Missed repayments are not matching");
                    Assert.AreEqual(UpcomingFirstPage, UpcomingLastPage, "Missed repayments are not matching");

                    //Logout
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
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}
