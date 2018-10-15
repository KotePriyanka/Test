using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimble.Automation.FunctionalTest.RegressionTest.Milestone6
{
    [TestFixture, Parallelizable, Category("Milestone6"), Category("Manual DNQed Client")]
    class TC194_Verify_Final_Approval_More_Info_Loan_Continuation : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1000, "android", TestName = "TC194_Verify_Final_Approval_More_Info_Loan_Continuation_android_RL"), Category("RL"), Category("Mobile"), Retry(2)]
        [TestCase(3000, "ios", TestName = "TC194_Verify_Final_Approval_More_Info_Loan_Continuation_ios_RL")]
        public void TC194_Verify_Final_Approval_More_Info_Loan_Continuation_RL(int loanamout, string strmobiledevice)
        {
            strMessage += string.Format("\r\n\t " + TestContext.CurrentContext.Test.Name + " Starts");
            strUserType = "RL";
            try
            {
                _driver = TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _personalDetails = new PersonalDetails(_driver, "RL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
                _bankDetails = new BankDetails(_driver, "RL");

                // Login with existing user
                _homeDetails.LoginExistingUser(TestData.RandomPassword, loanamout, TestData.ClientType.NimbleStatus, TestData.Feature.FinalApprovalMoreInfo);
                                
                //Click final approve link
                //_bankDetails.ClickFinalApproval();

                //Click on setup button
                //_bankDetails.ClickSetup();

                if (GetPlatform(_driver))
                {
                    //Click on More button
                    _loanSetUpDetails.ClickMoreBtn();

                    //Click on final approve link
                    _bankDetails.ClickFinalApproval();

                    //Click on setup button
                    _bankDetails.ClickSetup();

                    // click on Button Submit
                    _loanSetUpDetails.ClickSubmitBtn();

                    // Click on Bank Account to transfer
                    // _bankDetails.ClicksixtyMinuteButton();

                    // click on sublit-payment Button
                    //  _bankDetails.ClickSubmitPaymentButton();
                }
                else
                {
                    //Click final approve link
                    _bankDetails.ClickFinalApproval();

                    //Click on setup button
                    _bankDetails.ClickSetup();

                    // Click on Bank Account to transfer
                    _bankDetails.ClicksixtyMinuteButton();

                    // click on Buton Submit
                    _loanSetUpDetails.ClickSubmitBtn();
                }

                //  Scrolling the Loan Contract
                _loanSetUpDetails.Loancontract();

                // Confirming accepting contract
                _loanSetUpDetails.ConfirmAcceptingContract();

                // click on I Agree button
                _loanSetUpDetails.ClickOnAgreeBtn();

                // click on No thanks Button
                _loanSetUpDetails.ClickNothanksBtn();

                if (GetPlatform(_driver))
                {
                    // Click on To Loan Dashboard Button
                    _loanSetUpDetails.ClickMobileLoanDashboardBtn();

                    // click on More Button from Bottom Menu
                    _loanSetUpDetails.ClickMoreBtn();

                    //Logout
                    _loanSetUpDetails.Logout();
                    strMessage += string.Format("\r\n\t Ends");
                }
                else
                {
                    // Click on Loan Dashboard Button
                    _loanSetUpDetails.ClickLoanDashboard();

                    //Logout
                    _loanSetUpDetails.Logout();
                    strMessage += string.Format("\r\n\t Ends");
                }
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}
