﻿using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace Nimble.Automation.FunctionalTest.Milestone5
{
    // Your Expenses screen - No Rent - Other
    // On the Your Expenses screen, if we identify $0 of rent or the user
    // states their rent/mortage value is $0, we will ask them to state why
    // This test scenario covers selecting the option 'Other'
    // THe user is required to enter some free text to continue when selecting this option

    [TestFixture, Parallelizable, Category("Milestone5"), Category("Expenses")]
    class TC145_YourExpenses_NoRent_OtherReason_NL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(700, "android", TestName = "TC145_YourExpenses_NoRent_Other_NL_700"), Category("NL"), Category("Mobile"), Retry(2)]
        [TestCase(2700, "ios", TestName = "TC145_YourExpenses_NoRent_Other_NL_2700")]
        public void TC145_YourExpenses_NoRent_Other_NL(int loanamout, string strmobiledevice)
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

                // Your Expenses page - set rent to $0, select Other as a response
                _bankDetails.SelectOtherDebtRepaymentsOptionBtn();
                _bankDetails.SelectDependantsLst(TestData.Dependents.Zero);
                _bankDetails.EnterExpenseMortgageTxt("0");
                _bankDetails.SelectExpenseNoRentLst("Other");
                _bankDetails.EnterNoRentOtherTxt("I won a house");
                _bankDetails.ClickConfirmExpensesBtn();

                // Your Summary page
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

                // Manual approval flow
                // Need a slight delay otherwise the Loan Dashboard does not get clicked
                Thread.Sleep(2000);
                _bankDetails.ClickLoanDashboardManual();
                _bankDetails.ClickFinalApproval();
                _bankDetails.ClickSetup();

                // Submit application
                _loanSetUpDetails.ClickSubmitBtn();

                // Loan Contract page
                _loanSetUpDetails.Loancontract();
                _loanSetUpDetails.ConfirmAcceptingContract();
                _loanSetUpDetails.ClickOnAgreeBtn();

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

    [TestFixture, Parallelizable, Category("Milestone5"), Category("Expenses")]
    class TC145_YourExpenses_NoRent_OtherReason_RL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(600, "android", TestName = "TC145_YourExpenses_NoRent_Other_RL_600"), Category("RL"), Retry(2)]
        [TestCase(2600, "ios", TestName = "TC145_YourExpenses_NoRent_Other_RL_2600")]
        public void TC145_YourExpenses_NoRent_Other_RL(int loanamout, string strmobiledevice)
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

                // Your Expenses page - set rent to $0, select Other as a response
                _bankDetails.SelectOtherDebtRepaymentsOptionBtn();
                _bankDetails.SelectDependantsLst(TestData.Dependents.Zero);
                _bankDetails.EnterExpenseMortgageTxt("0");
                _bankDetails.ClickConfirmExpensesBtn();
                _bankDetails.SelectExpenseNoRentLst("Other");
                _bankDetails.EnterNoRentOtherTxt("I won a house");
                _bankDetails.ClickConfirmExpensesBtn();

                // Your Summary page
                _bankDetails.ClickNoGovtBenefitsbtn();
                _bankDetails.ClickAgreeAppSubmitBtn();
                _bankDetails.ClickConfirmSummaryBtn();

                // Manual approval flow
                _bankDetails.ClickLoanDashboardManual();
                _bankDetails.ClickFinalApproval();
                _bankDetails.ClickSetup();

                // Set Up Loan page
                if (GetPlatform(_driver))
                {
                    _loanSetUpDetails.ClickSubmitBtn();
                    _bankDetails.ClicksixtyMinuteButton();
                    _bankDetails.ClickSubmitPaymentButton();
                }
                else
                {
                    _bankDetails.ClicksixtyMinuteButton();
                    _loanSetUpDetails.ClickSubmitBtn();
                }

                // Loan Contract page
                _loanSetUpDetails.Loancontract();
                _loanSetUpDetails.ConfirmAcceptingContract();
                _loanSetUpDetails.ClickOnAgreeBtn();

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
