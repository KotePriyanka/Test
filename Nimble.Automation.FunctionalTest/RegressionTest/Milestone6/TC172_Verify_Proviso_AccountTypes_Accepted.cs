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

    // Proviso aka BankStatement test
    // Bank accounts we scrape can have different accountTypes other than the default 'savings'
    // Currently Proviso only supports loans that are new and up to $1600
    // Certain Proviso accounTypes will be accepted, while others will fail with an error on the Bank Login screen

    [TestFixture, Parallelizable, Category("Milestone6"), Category("Proviso")]
    class TC172_Verify_Proviso_AccountTypes_Accepted : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(300, "android", "TestBank317", "qtpgjB5%", "", TestName = "TC172_Verify_Proviso_AccountType_Blank_NL_300"), Category("NL"), Retry(2)]
        [TestCase(500, "android", "TestBank318", "NoxnpwIH", "CD", TestName = "TC172_Verify_Proviso_AccountType_CD_NL_500")]
        [TestCase(700, "android", "TestBank319", "JVWhveLQ", "checking", TestName = "TC172_Verify_Proviso_AccountType_Checking_NL_700")]
        [TestCase(900, "android", "TestBank324", "ghTXTjP@", "ira", TestName = "TC172_Verify_Proviso_AccountType_Ira_NL_900")]
        [TestCase(1100, "android", "TestBank325", "OLhlOtEU", "Not Available", TestName = "TC172_Verify_Proviso_AccountType_NotAvailable_NL_1100")]
        [TestCase(1300, "android", "TestBank326", "04efJwSP", "other", TestName = "TC172_Verify_Proviso_AccountType_Other_NL_1300"), Ignore("Proviso Accounts not used")]
        [TestCase(400, "android", "TestBank328", "kRhzwIUN", "prepaid", TestName = "TC172_Verify_Proviso_AccountType_Prepaid_NL_400")]
        [TestCase(600, "android", "TestBank330", "pe5ti@ag", "transaction", TestName = "TC172_Verify_Proviso_AccountType_Transaction_NL_600")]
        [TestCase(800, "android", "TestBank331", "JsOuKWDS", "unknown", TestName = "TC172_Verify_Proviso_AccountType_Unknown_NL_800")]

        public void TC172_Verify_Proviso_AccountTypes_Accepted_NL(int loanamout, string strmobiledevice, string BankUsername, string BankPwd, string AccountType)
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

                PersonalDetailsDataObj _obj = new PersonalDetailsDataObj();

                _obj.StreetName = "At:N Cr:A Id:100 Rr1:A Rr2:A Rr3:A Bsp:BS Rmsrv:1";

                //populate the personal details and proceed
                _personalDetails.PopulatePersonalDetails(_obj);

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
                //Assert.IsTrue(_bankDetails.GetMaskedAccountType().Contains(AccountType));

                // Click on Bank select Continue Button
                _bankDetails.ClickBankAccountContBtn();

                // Confirm Bank Details
                // _bankDetails.EnterBankDetailsTxt();
                _bankDetails.EnterBankDetailsUploadCSV("012004", "01234567", "Testuser");

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
}
