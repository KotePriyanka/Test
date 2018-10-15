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

    // Bank accounts we scrape can have different accountTypes other than the default 'savings'
    // Both Proviso and Yodlee have different accounts that will accepted
    // This test will cover the currently acceptable Yodlee account types (except for the default "savings")

    [TestFixture, Parallelizable, Category("Milestone6"), Category("Bank")]
    class TC174_Verify_Yodlee_AccountTypes_Accepted_NL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(300, "android", "WebUITest.bank64", "bank64", "", TestName = "TC174_Verify_Yodlee_AccountType_Blank_NL_300"), Category("NL"), Retry(2)]
        [TestCase(400, "android", "WebUITest.bank65", "bank65", "CD", TestName = "TC174_Verify_Yodlee_AccountType_CD_NL_400")]
        [TestCase(500, "android", "WebUITest.bank66", "bank66", "checking", TestName = "TC174_Verify_Yodlee_AccountType_Checking_NL_500")]
        [TestCase(600, "android", "WebUITest.bank68", "bank68", "ira", TestName = "TC174_Verify_Yodlee_AccountType_Ira_NL_600")]
        [TestCase(700, "android", "WebUITest.bank69", "bank69", "other", TestName = "TC174_Verify_Yodlee_AccountType_Other_NL_700")]
        [TestCase(800, "android", "WebUITest.bank70", "bank70", "prepaid", TestName = "TC174_Verify_Yodlee_AccountType_Prepaid_NL_800")]
        [TestCase(900, "android", "WebUITest.bank71", "bank71", "unknown", TestName = "TC174_Verify_Yodlee_AccountType_Unknown_NL_900")]

        public void TC174_Verify_Yodlee_AccountTypes_Accept_NL(int loanamout, string strmobiledevice, string BankUsername, string BankPwd, string AccountType)
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
                _personalDetails.PopulatePersonalDetails();

                // Click on checks out Continue Button
                _personalDetails.ClickCheckoutContinueBtn();

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(BankUsername, BankPwd);

                // Click on Continue Button
                _bankDetails.ClickAutoContinueBtn();

                // choose bank account
                _bankDetails.BankAccountSelectBtn();

                // Bank Details - check presented masked values
                Assert.IsTrue(_bankDetails.GetMaskedAccountType().Contains(AccountType));

                // Click on Bank select Continue Button
                _bankDetails.ClickBankAccountContBtn();

                // Confirm Bank Details
                _bankDetails.EnterBankDetailsTxt();

                // Click on Confirm account details Continue Button  
                _bankDetails.ClickAcctDetailsBtn();

                // Select Category 
                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "0");

                // click on Confirm Income Button
                _bankDetails.ClickConfirmIncomeBtn();

                // select other debt repayments option No 
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
                Assert.Fail(ex.Message); strMessage += ex.Message;
            }
        }
    }

    [TestFixture, Parallelizable, Category("Milestone6"), Category("Bank")]
    class TC174_Verify_Yodlee_AccountTypes_Accepted_RL : TestEngine
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

        [TestCase(400, "android", "WebUITest.bank64", "bank64", "", TestName = "TC174_Verify_Yodlee_AccountType_Blank_RL_400"), Category("RL"), Retry(2)]
        [TestCase(500, "android", "WebUITest.bank65", "bank65", "CD", TestName = "TC174_Verify_Yodlee_AccountType_CD_RL_500")]
        [TestCase(600, "android", "WebUITest.bank66", "bank66", "checking", TestName = "TC174_Verify_Yodlee_AccountType_Checking_RL_600")]
        [TestCase(700, "android", "WebUITest.bank68", "bank68", "ira", TestName = "TC174_Verify_Yodlee_AccountType_Ira_RL_700")]
        [TestCase(800, "android", "WebUITest.bank69", "bank69", "other", TestName = "TC174_Verify_Yodlee_AccountType_Other_RL_800")]
        [TestCase(900, "android", "WebUITest.bank70", "bank70", "prepaid", TestName = "TC174_Verify_Yodlee_AccountType_Prepaid_RL_900")]
        [TestCase(1000, "android", "WebUITest.bank71", "bank71", "unknown", TestName = "TC174_Verify_Yodlee_AccountType_Unknown_RL_1000")]
        public void TC174_Verify_Yodlee_AccountTypes_Accept_RL(int loanamout, string strmobiledevice, string BankUsername, string BankPwd, string AccountType)
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
                _bankDetails.EnterBankCredentialsTxt(BankUsername, BankPwd);
                _bankDetails.ClickAutoContinueBtn();
                _bankDetails.BankAccountSelectBtn();

                // Select Bank Account - check presented accountType value
                Assert.IsTrue(_bankDetails.GetMaskedAccountType().Contains(AccountType));
                _bankDetails.ClickBankAccountContBtn();
                _bankDetails.EnterBankDetailsTxt();
                _bankDetails.ClickAcctDetailsBtn();

                // Your Income page
                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "0");
                _bankDetails.ClickConfirmIncomeBtn();

                // Your Expenses page
                _bankDetails.SelectOtherDebtRepaymentsOptionBtn();
                _bankDetails.SelectDependantsLst(TestData.Dependents.Zero);
                _bankDetails.ClickConfirmExpensesBtn();

                // Your Summary page
                _bankDetails.ClickNoGovtBenefitsbtn();
                _bankDetails.ClickAgreeAppSubmitBtn();
                _bankDetails.ClickConfirmSummaryBtn();

                //_loanSetUpDetails.WaitForLoanSetUpPage();

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
