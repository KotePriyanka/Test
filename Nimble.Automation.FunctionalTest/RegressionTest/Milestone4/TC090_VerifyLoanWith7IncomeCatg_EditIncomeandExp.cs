using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest
{
    //<Summary>
    //Verifying loan application by Applying a Loan With Existing 7 Income Categories And Edit both Income and Expenses in the Your Summary Screen by using Edit Income Button
    //</Summary>

    [TestFixture, Parallelizable, Category("Milestone4"), Category("Income Categories"), Category("Expenses")]
    class TC090_VerifyLoanWith7IncomeCatg_EditIncomeandExp_NL : TestEngine
    {
        [TearDown]
        public void aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TestCase(400, "android", TestName = "TC090_VerifyLoanWith7IncomeCatg_EditIncomeandExp_NL_400"), Category("NL"), Retry(2)]
        [TestCase(2700, "ios", TestName = "TC090_VerifyLoanWith7IncomeCatg_EditIncomeandExp_NL_2700")]
        public void TC090_VerifyLoanWith7IncomeCatg_EditIncomeNExp_NL(int loanamout, string strmobiledevice)
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

                //Go to the homepage and click the start application button
                _homeDetails.HomeDetailsPage();

                //Select the loan amount and purpose and click on continue button
                _loanPurposeDetails.LoanPurposeFunction(loanamout, TestData.POL.Households);

                //populate the personal details and proceed
                _personalDetails.PersonalDetailsFunction();

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.Seven7IncomeCategories.Yodlee.UID, TestData.BankDetails.Seven7IncomeCategories.Yodlee.PWD);

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

                // Select Category 
                // _bankDetails.SelectSevenIncome(TestData.IncomeCategory.PrimaryIncome, TestData.IncomeCategory.NotIncome, TestData.IncomeCategory.OtherEmployment, TestData.IncomeCategory.PartnerSalary, TestData.IncomeCategory.SharedRentUtilities, TestData.IncomeCategory.ChildSupport, TestData.IncomeCategory.InvestmentIncome);

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "6");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.NotIncome, "5");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.OtherEmployment, "4");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PartnerSalary, "3");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.SharedRentUtilities, "2");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.ChildSupport, "1");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.InvestmentIncome, "0");

                // click on Confirm Income Button
                _bankDetails.ClickConfirmIncomeBtn();

                // select  other debt repayments option No 
                _bankDetails.SelectOtherDebtRepaymentsOptionBtn();

                // select dependents 
                _bankDetails.SelectDependantsLst(TestData.Dependents.Zero);

                // Click on continue
                _bankDetails.ClickConfirmExpensesBtn();

                //Click Edit Income
                _bankDetails.ClickEditIncome();

                // Select Category 
                // _bankDetails.SelectSevenIncome(TestData.IncomeCategory.PrimaryIncome, TestData.IncomeCategory.InvestmentIncome, TestData.IncomeCategory.PrimaryIncome, TestData.IncomeCategory.SharedRentUtilities, TestData.IncomeCategory.ChildSupport, TestData.IncomeCategory.PartnerSalary, TestData.IncomeCategory.OtherEmployment);

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "6");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.NotIncome, "5");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.OtherEmployment, "4");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PartnerSalary, "3");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.SharedRentUtilities, "2");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.ChildSupport, "1");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.InvestmentIncome, "0");

                // click on Confirm Income Button
                _bankDetails.ClickConfirmIncomeBtn();

                //Edit Expenses
                _bankDetails.EditExpenses();

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

    [TestFixture, Parallelizable, Category("Milestone4"), Category("Income Categories"), Category("Expenses")]
    class TC090_VerifyLoanWith7IncomeCatg_EditIncomeandExp_RL : TestEngine
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

        [TestCase(1250, "android", TestName = "TC090_VerifyLoanWith7IncomeCatg_EditIncomeandExp_RL_1250"), Category("RL"), Retry(2)]
        [TestCase(2750, "ios", TestName = "TC090_VerifyLoanWith7IncomeCatg_EditIncomeandExp_RL_2750")]
        public void TC090_VerifyLoanWith7IncomeCatg_EditIncomeNExp_RL(int loanamout, string strmobiledevice)
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

                //Go to the homepage and click the start application button and then the Request money button

                _homeDetails.homeFunctions_RL(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.NewProductAdvancePaidClean);


                //Select the loan amount and purpose and click on continue button
                _loanPurposeDetails.LoanPurposeFunction_RL(loanamout, TestData.POL.Households);

                string streetname = "At:N Cr:A Id:100 Rr1:A Rr2:A Rr3:A Rr:A Rt:8 Rmsrv:0.9999";
                //Edit the personal details and change the Rmsrv Code
                _personalDetails.PersonalDetailsFunction_RL(TestData.YourEmployementStatus.FullTime, TestData.ReturnerLoaner, streetname);

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.Seven7IncomeCategories.Yodlee.UID, TestData.BankDetails.Seven7IncomeCategories.Yodlee.PWD);

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

                // Select Category 
                // _bankDetails.SelectSevenIncome(TestData.IncomeCategory.PrimaryIncome, TestData.IncomeCategory.NotIncome, TestData.IncomeCategory.OtherEmployment, TestData.IncomeCategory.PartnerSalary, TestData.IncomeCategory.SharedRentUtilities, TestData.IncomeCategory.ChildSupport, TestData.IncomeCategory.InvestmentIncome);

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "6");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.NotIncome, "5");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.OtherEmployment, "4");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PartnerSalary, "3");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.SharedRentUtilities, "2");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.ChildSupport, "1");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.InvestmentIncome, "0");

                // click on Confirm Income Button
                _bankDetails.ClickConfirmIncomeBtn();

                // select  other debt repayments option No 
                _bankDetails.SelectOtherDebtRepaymentsOptionBtn();

                // select dependents 
                _bankDetails.SelectDependantsLst(TestData.Dependents.Zero);

                // Click on continue
                _bankDetails.ClickConfirmExpensesBtn();

                //Click Edit Income
                _bankDetails.ClickEditIncome();

                // Select Category 
                //_bankDetails.SelectSevenIncome(TestData.IncomeCategory.PrimaryIncome, TestData.IncomeCategory.InvestmentIncome, TestData.IncomeCategory.PrimaryIncome, TestData.IncomeCategory.SharedRentUtilities, TestData.IncomeCategory.ChildSupport, TestData.IncomeCategory.PartnerSalary, TestData.IncomeCategory.OtherEmployment);

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "6");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.InvestmentIncome, "5");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "4");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.SharedRentUtilities, "3");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.ChildSupport, "2");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PartnerSalary, "1");

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.OtherEmployment, "0");

                // click on Confirm Income Button
                _bankDetails.ClickConfirmIncomeBtn();

                //Edit Expenses
                _bankDetails.EditExpenses();

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

                // verify final review enabled and process setup functionality
                _loanSetUpDetails.loanSetupFunction(loanamout, strUserType);
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}


















