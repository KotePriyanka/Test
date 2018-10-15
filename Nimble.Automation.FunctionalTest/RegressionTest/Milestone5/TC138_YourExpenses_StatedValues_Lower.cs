using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest.Milestone5
{
    // Your Expenses screen - stated values - lower
    // By default we pre-fill in the Your Expenses values based upon the bank
    // data we scrap from the user's accounts (identified values). If the user chooses to 
    // increase these Your Expense values, we capture these and use them to evaluate their app
    // (stated values). If the user decreases the values, we capture this data but disregard the
    // user's input and use the identified values to process their app.

    [TestFixture, Parallelizable, Category("Milestone5"), Category("Expenses")]
    class TC138_YourExpenses_StatedValues_Low : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(700, "android", TestName = "TC138_YourExpenses_StatedValues_Lower_NL_700"), Category("NL"), Retry(2)]
        [TestCase(2300, "ios", TestName = "TC138_YourExpenses_StatedValues_Lower_NL_2300")]
        public void TC138_YourExpenses_StatedValues_Lower_NL(int loanamout, string strmobiledevice)
        {
            strUserType = "NL";
            try
            {
                _driver = _testengine.TestSetup(strmobiledevice);
                _homeDetails = new HomeDetails(_driver, "NL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "NL");
                _personalDetails = new PersonalDetails(_driver, "NL");
                _bankDetails = new BankDetails(_driver, "NL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "NL");

                // Home page
                _homeDetails.HomeDetailsPage();

                // Purpose of Loan page
                _loanPurposeDetails.LoanPurposeFunction(loanamout, TestData.POL.Households);

                // Personal Details page
                _personalDetails.PersonalDetailsFunction();

                // Bank page
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);
                _bankDetails.BankSelectContinueBtn();
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.UID, TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.PWD);
                _bankDetails.ClickAutoContinueBtn();
                _bankDetails.BankAccountSelectBtn();
                _bankDetails.ClickBankAccountContBtn();
                _bankDetails.EnterBankDetailsTxt();
                _bankDetails.ClickAcctDetailsBtn();

                // Your Income page
                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "0");
                _bankDetails.ClickConfirmIncomeBtn();

                // Your Expenses page
                _bankDetails.EnterExpenseEverydayLivingTxt("500");
                _bankDetails.EnterExpenseOtherDebtRepaymentsTxt("50");
                _bankDetails.EnterExpenseMortgageTxt("800");
                _bankDetails.EnterExpenseLargeOngoingTxt("200");
                _bankDetails.SelectDependantsLst(TestData.Dependents.Zero);
                _bankDetails.ClickConfirmExpensesBtn();

                // Your Summary page
                Assert.IsTrue(_bankDetails.CompareSummaryLivingExpensesTxt("500.00"));
                Assert.IsTrue(_bankDetails.CompareSummaryOtherDebtTxt("50.00"));
                Assert.IsTrue(_bankDetails.CompareSummaryRentMortgageTxt("800.00"));
                Assert.IsTrue(_bankDetails.CompareSummaryLargeOngoingBillsTxt("200.00"));
                _bankDetails.ClickNoGovtBenefitsbtn();
                _bankDetails.ClickAgreeAppSubmitBtn();
                _bankDetails.ClickConfirmSummaryBtn();

                // SMS Pin page (if applicable)
                if (loanamout > 2000)
                {
                    // enter sms input as OTP 
                    if (_bankDetails.VerifySMSOTP())
                        _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);
                }

                // verify final review enabled and process setup functionality
                _loanSetUpDetails.loanSetupFunction(loanamout, strUserType);
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }

    [TestFixture, Parallelizable, Category("Milestone5"), Category("Expenses")]
    class TC138_YourExpenses_StatedValues_Low_RL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(800, "android", TestName = "TC138_YourExpenses_StatedValues_Lower_RL_800"), Category("RL"), Retry(2)]
        [TestCase(2400, "ios", TestName = "TC138_YourExpenses_StatedValues_Lower_RL_2400")]
        public void TC138_YourExpenses_StatedValues_Lower_RL(int loanamout, string strmobiledevice)
        {
            strUserType = "RL";
            _driver = _testengine.TestSetup(strmobiledevice, "RL");
            _homeDetails = new HomeDetails(_driver, "RL");
            _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
            _personalDetails = new PersonalDetails(_driver, "RL");
            _bankDetails = new BankDetails(_driver, "RL");
            _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

            try
            {
                /// Generate debug client and log in
                _homeDetails.homeFunctions_RL(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.NewProductAdvancePaidClean);

                // Purpose of Loan page
                _loanPurposeDetails.LoanPurposeFunction_RL(loanamout, TestData.POL.Insurance);

                // Edit the personal details and change the Rmsrv Code
                _personalDetails.PersonalDetailsFunction_RL(TestData.YourEmployementStatus.FullTime, TestData.ReturnerLoaner, TestData.OverrideCodes.PassAll_RL);

                // Bank page  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);
                _bankDetails.BankSelectContinueBtn();
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.UID, TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.PWD);
                _bankDetails.ClickAutoContinueBtn();
                _bankDetails.BankAccountSelectBtn();
                _bankDetails.ClickBankAccountContBtn();
                _bankDetails.EnterBankDetailsTxt();
                _bankDetails.ClickAcctDetailsBtn();

                // Your Income page
                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "0");
                _bankDetails.ClickConfirmIncomeBtn();

                // Your Expenses page
                _bankDetails.EnterExpenseEverydayLivingTxt("500");
                _bankDetails.EnterExpenseOtherDebtRepaymentsTxt("50");
                _bankDetails.EnterExpenseMortgageTxt("800");
                _bankDetails.EnterExpenseLargeOngoingTxt("200");
                _bankDetails.SelectDependantsLst(TestData.Dependents.Zero);
                _bankDetails.ClickConfirmExpensesBtn();

                // Your Summary page
                Assert.IsTrue(_bankDetails.CompareSummaryLivingExpensesTxt("500.00"));
                Assert.IsTrue(_bankDetails.CompareSummaryOtherDebtTxt("50.00"));
                Assert.IsTrue(_bankDetails.CompareSummaryRentMortgageTxt("800.00"));
                Assert.IsTrue(_bankDetails.CompareSummaryLargeOngoingBillsTxt("200.00"));
                _bankDetails.ClickNoGovtBenefitsbtn();
                _bankDetails.ClickAgreeAppSubmitBtn();
                _bankDetails.ClickConfirmSummaryBtn();

                // Set Up Loan page
                if ((loanamout > 2000 && FinalReviewEnabled == "true") && (strUserType == FinalReviewLoanType || FinalReviewLoanType == "ALL"))
                {
                    if (GetPlatform(_driver))
                    {
                        //ClickOn Loan Dashboard...Manual Approval
                        _loanSetUpDetails.ClickLoanDashboardManual();

                        // click on More Button from Bottom Menu
                        _loanSetUpDetails.ClickMoreBtn();

                        // click on Approve button
                        _loanSetUpDetails.ClickApproveBtn();

                        //Click Setup Button
                        _loanSetUpDetails.ClickSetup();
                    }
                    else
                    {
                        //ClickOn Loan Dashboard...Manual Approval
                        _loanSetUpDetails.ClickLoanDashboardManual();
                        //ClickLoanDashboard();

                        //click on Final Approve
                        _loanSetUpDetails.FinalApprove();

                        //Click Setup Button
                        _loanSetUpDetails.ClickSetup();
                    }
                }

                if (GetPlatform(_driver))
                {
                    _loanSetUpDetails.ClickSubmitBtn();
                  //  _bankDetails.ClicksixtyMinuteButton();
                    _bankDetails.ClickSubmitPaymentButton();
                }
                else
                {
                   // _bankDetails.ClicksixtyMinuteButton();
                    _loanSetUpDetails.ClickSubmitBtn();
                }

                // Loan Contract page
                _loanSetUpDetails.Loancontract();
                _loanSetUpDetails.ConfirmAcceptingContract();
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

                }
                else
                {
                    // Click on Loan Dashboard Button
                    _loanSetUpDetails.ClickLoanDashboard();

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