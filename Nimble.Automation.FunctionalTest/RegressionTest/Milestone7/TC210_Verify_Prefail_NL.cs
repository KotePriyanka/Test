using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest.RegressionTest.Milestone7
{
    [TestFixture, Parallelizable, Category("Milestone7"), Category("Prefail")]
    class TC210_Verify_Prefail_NL : TestEngine
    {
        [TearDown]
        public void Aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null;
        public TestEngine _testengine = new TestEngine();
        private string email = "";

        string strMessage, strUserType;
        ResultDbHelper _result = new ResultDbHelper();

        DateTime starttime { get; set; } = DateTime.Now;

        [TestCase(1100, "android", TestName = "TC210_Verify_Prefail_NL_1100"), Category("NL")]//, Ignore("Functionality not available"), Retry(2)]//android
        [TestCase(3800, "ios", TestName = "TC210_Verify_Prefail_NL_3800")]
        public void TC210_Verify_Prefail_NewLoan(int loanamout, string strmobiledevice)
        {
            starttime = DateTime.Now;
            strMessage = string.Format("\r\n\t " + TestContext.CurrentContext.Test.Name + " Starts");
            strUserType = "NL";

            try
            {
                _driver = TestSetup(strmobiledevice);
                _homeDetails = new HomeDetails(_driver, "NL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "NL");
                _personalDetails = new PersonalDetails(_driver, "NL");
                _bankDetails = new BankDetails(_driver, "NL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "NL");

                //Go to the homepage and click the start application button
                _homeDetails.HomeDetailsPage();

                //Select the loan amount and purpose and click on continue button
                _loanPurposeDetails.LoanPurposeFunction(loanamout, TestData.POL.Eventcosts.Anniversary);

                //populate the personal details and proceed
                PersonalDetailsDataObj _obj = _personalDetails.PersonalDetailsFunction();

                email = _personalDetails.EmailID;

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.UID, TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.PWD);

                // Click on Continue Button
                _bankDetails.ClickAutoContinueBtn();

                // choose bank account
                _bankDetails.BankAccountSelectBtn();

                // Click on bank select Continue Button
                _bankDetails.ClickBankAccountContBtn();

                // Confirm Bank Details
                _bankDetails.EnterBankDetailsTxt();

                // Click on Confirm account details Continue Button  
                _bankDetails.ClickAcctDetailsBtn();

                strMessage += string.Format("\r\n\t Dag bank Launched");
                // Select Category 
                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "0");

                // Select Just checking option 
                //_bankDetails.SelectJustCheckingOptionLst("Yes, it will stay the same (or more)");

                // click on Confirm Income Button
                _bankDetails.ClickConfirmIncomeBtn();

                // select  other debt repayments option No 
                _bankDetails.SelectOtherDebtRepaymentsOptionBtn();

                // select dependents 
                _bankDetails.SelectDependantsLst(TestData.Dependents.Zero);

                // Click on continue
                _bankDetails.ClickConfirmExpensesBtn();

                // select Governments benefits option No
                _bankDetails.ClickNoGovtBenefitsbtn();

                // click on Agree that information True
                _bankDetails.ClickAgreeAppSubmitBtn();

                // click on confirm Submit button
                _bankDetails.ClickConfirmSummaryBtn();

                if (loanamout > 2000)
                {
                    // enter sms input as OTP 
                    if (_bankDetails.VerifySMSOTP())
                        _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);
                }

                // verify final review enabled and process setup functionality
                _loanSetUpDetails.loanSetupFunction(loanamout, strUserType);

                //Relogin with same user to check visibility of reschedule or make a repayment button
                _homeDetails.ReLoginUser(_obj.Email, "password");

                Thread.Sleep(2000);

                //Check availability of make a payment button
                Assert.IsFalse(_homeDetails.verifyMakeaPaymentBtn(), "make a payment button");

                //Check reschedule button
                Assert.IsFalse(_bankDetails.verifyRescheduleBtn(), "Reschedule button");

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
                strMessage += ex.Message;
                Assert.Fail(ex.Message);
            }
        }
    }
}
