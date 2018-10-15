using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimble.Automation.FunctionalTest
{
    //<Summary>
    //Verifying the loan application of a user with high income and low expenses.
    //</Summary>

    [TestFixture, Parallelizable, Category("Milestone3"), Category("DSC")]
    class TC075_VerifyHighIncome_LowExpenses_NL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null;string strMessage,strUserType="";  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(1500, "android", TestName = "TC075_VerifyHighIncome_LowExp_NL_1500"), Category("NL"), Category("Mobile"), Retry(2)]
        [TestCase(2900, "ios", TestName = "TC075_VerifyHighIncome_LowExp_NL_2900")]
        public void TC075_VerifyHighIncome_LowEx_NL(int loanamout, string strmobiledevice)
        {
            strUserType = "NL";
            try
            {
                _driver = TestSetup(strmobiledevice, "NL");
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

                //Fill Up all the required bank details and submits the application
                _bankDetails.bankFunctions(TestData.BankDetails.Dagbank, TestData.BankDetails.HighIncome.Yodlee.UID, TestData.BankDetails.HighIncome.Yodlee.PWD, TestData.IncomeCategory.PrimaryIncome, TestData.Dependents.Zero, TestData.SMSCode,loanamout);

                if (GetPlatform(_driver))
                {
                    //ClickOn Loan Dashboard...Manual Approval
                    bool flag = _loanSetUpDetails.ClickLoanDashboardManual();
                    Assert.IsTrue(flag, "Application unable to move to manual approval");
                }
                else
                {
                    //ClickOn Loan Dashboard...Manual Approval
                    bool flag = _loanSetUpDetails.ClickLoanDashboardManual();
                    Assert.IsTrue(flag, "Application unable to move to manual approval");
                }

            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
           

        }
    }

    [TestFixture, Parallelizable, Category("Milestone3"), Category("DSC")]
    class TC075_VerifyHighIncome_LowExpenses_RL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1600, "android", TestName = "TC075_VerifyHighIncome_LowExp_RL_1600"), Category("RL"), Retry(2)]
        [TestCase(2800, "ios", TestName = "TC075_VerifyHighIncome_LowExp_RL_2800")]
        public void TC075_VerifyHighIncome_LowEx_RL(int loanamout, string strmobiledevice)
        {
          strUserType = "RL";
            try
            {
                _driver = TestSetup(strmobiledevice,"RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
                _personalDetails = new PersonalDetails(_driver, "RL");
                _bankDetails = new BankDetails(_driver, "RL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

                //Go to the homepage and click the start application button and then the Request money button
                _homeDetails.homeFunctions_RL(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.NewProductAdvancePaidClean);

                //Select the loan amount and purpose and click on continue button
                _loanPurposeDetails.LoanPurposeFunction_RL(loanamout, TestData.POL.Households);

                //Edit the personal details and change the Rmsrv Code
                _personalDetails.PersonalDetailsFunction_RL(TestData.YourEmployementStatus.FullTime, TestData.ReturnerLoaner,TestData.OverrideCodes.PassAll_RL);
               
                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.HighIncome.Yodlee.UID, TestData.BankDetails.HighIncome.Yodlee.PWD);

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
                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "0");

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

                //Note:Due to high income and low expenses,the application goes to manual approval.

                if (GetPlatform(_driver))
                {
                    //ClickOn Loan Dashboard...Manual Approval
                    bool flag = _loanSetUpDetails.ClickLoanDashboardManual();
                    Assert.IsTrue(flag, "Application unable to move to manual approval");
                }
                else
                {
                    //ClickOn Loan Dashboard...Manual Approval
                    bool flag = _loanSetUpDetails.ClickLoanDashboardManual();
                    Assert.IsTrue(flag, "Application unable to move to manual approval");
                }


            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
           
        }
    }
}