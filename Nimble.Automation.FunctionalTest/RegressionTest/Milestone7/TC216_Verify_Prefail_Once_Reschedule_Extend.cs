using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest.RegressionTest.Milestone7
{
    [TestFixture, Parallelizable, Category("Milestone7"), Category("Prefail")]
    class TC216_Verify_Prefail_Once_Reschedule_Extend:TestEngine
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
        private static string UpcomingLastPage = string.Empty;
        private static string UpcomingFirstPage = string.Empty;

        [TestCase(1000, "android", TestName = "TC216_Verify_Prefail_Once_Reschedule_Extend_android_RL")]//, Ignore("Functionality not available"), Category("RL"), Category("Mobile"), Retry(2)]
        [TestCase(1000, "ios", TestName = "TC216_Verify_Prefail_Once_Reschedule_Extend_ios_RL")]
        public void TC216_Verify_Prefail_Once_Reschedule_Extend_RL(int loanamout, string strmobiledevice)
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
                _homeDetails.LoginExistingUser(TestData.Password, loanamout, TestData.ClientType.NewProduct, TestData.Feature.ReturnerSACCActive1prefail);

                //click on Reschedule button
                _bankDetails.ClickRescheduleButton();

                //Click on extend checkbox
                _bankDetails.ClickExtentCheckBox();

                //Get upcoming repayment from first page
                UpcomingFirstPage = _bankDetails.GetPrefailUpcomingRepaymentFirstPageExtend("1");

                //Click continue button after reschedule
                _bankDetails.ClickRescheduleContinueButton();

                if (GetPlatform(_driver))
                {
                    var amount = UpcomingFirstPage.Split('$');
                    UpcomingFirstPage = amount[1];
                    decimal upComingFirstPage = int.Parse(UpcomingFirstPage);
                    string upComingFirstPageValue = "$" + upComingFirstPage;

                    //Click on finish button
                    _bankDetails.clickFinishBtn();

                    //Click on time line
                    _bankDetails.clickTimeLine();

                    UpcomingLastPage = _bankDetails.GetPrefailUpcomingRepaymentLastPageExtend("5");

                    Assert.AreEqual(upComingFirstPageValue, UpcomingLastPage, "Missed repayments are not matching");
                }

                else
                {
                    //Fetch Reschedule message
                    string RescheduleMessage = _bankDetails.VerifyRescheduleMessage();
                    Assert.IsTrue(RescheduleMessage.Contains("Thanks!"), "Message not displayed");

                    UpcomingLastPage = _bankDetails.GetPrefailUpcomingRepaymentLastPageExtend("5");
                    Assert.AreEqual(UpcomingFirstPage, UpcomingLastPage, "Missed repayments are not matching");
                }

                //Get upcoming repayment from last page
                // string UpcomingLastPage = _bankDetails.GetPrefailUpcomingRepaymentLastPageExtend();
                //var value = UpcomingLastPage.Split('$');
                //double  UpcomingLast = Convert.ToDouble(value[1]);
                //string UpcomingLastPageValue = "$" + UpcomingLast;
                //   Assert.AreEqual(UpcomingFirstPage, UpcomingLastPage, "Missed repayments are not matching");

                if (GetPlatform(_driver))
                {
                    // click on More Button from Bottom Menu
                    _loanSetUpDetails.ClickMoreBtn();

                    //Logout
                    _loanSetUpDetails.Logout();
                }

                else
                {
                    //Logout
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

