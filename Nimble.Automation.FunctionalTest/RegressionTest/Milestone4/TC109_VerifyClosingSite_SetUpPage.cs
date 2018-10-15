using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest.Milestone4
{
    //<Summary>
    // On the Set Up Loan page if the user tries to leave (clicking in the URL bar),
    // we will present them a marketing survey about why they are leaving.
    // Applies to desktop NL and RL flow only
    // Was only able to simulate this in RL flow for now
    //</Summary>

    [TestFixture, Parallelizable, Category("Milestone4"), Category("Misc")]
    class TC109_VerifyClosingSite_SetUpPage : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();
        private TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(800, "android", TestName = "TC109_VerifyClosingSite_SetUpPage_RL_android_800"), Category("NL"), Retry(2)]
        [TestCase(800, "ios", TestName = "TC109_VerifyClosingSite_SetUpPage_RL_ios_800")]
        public void TC109_VerifyClosingSite_SetUpPage_RL(int loanamout, string strmobiledevice)
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

                // Create new debug client
                _homeDetails.LoginExistingUser(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.NewProductAdvancePaidClean);

                // Member Area - click Request Money
                _homeDetails.ClickRequestMoneyBtn();

                // Member Area - Let's Get Started
                _homeDetails.ClickExistinguserStartApplictionBtn();

                // Purpose of Loan page
                _loanPurposeDetails.SelectLoanValueRL(loanamout);
                _loanPurposeDetails.ClickSelectFirstPurposeBtn();
                _loanPurposeDetails.SelectLoanPurposeRL(TestData.POL.Homerepairsorimprovements);
                _loanPurposeDetails.EnterFirstPOLAmountTxt(loanamout.ToString());
                _loanPurposeDetails.ClickLoanPOLContinueBtnRL();

                // Personal Details page
                _personalDetails.SelectEmploymentStatusLst(TestData.YourEmployementStatus.FullTime);
                _personalDetails.ClickNoShortTermLoanStatusBtn();
                _personalDetails.CheckReadPrivacyBtn(TestData.ReturnerLoaner);
                _personalDetails.CheckReadCreditBtn(TestData.ReturnerLoaner);

                if (GetPlatform(_driver))
                {
                    // Mobile Personal Details Continue button
                    _personalDetails.ClickPersonaldetailsContinueBtnRLMobile();
                }
                else
                {
                    // Desktop Personal Details Continue button
                    _personalDetails.ClickPersonaldetailsRequestBtnRLDesktop();
                    _personalDetails.ClickAutomaticVerificationBtn();
                }

                // Bank Details page
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);
                _bankDetails.BankSelectContinueBtn();
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.UID, TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.PWD);
                _bankDetails.ClickAutoContinueBtn();
                _bankDetails.BankAccountSelectBtn();
                _bankDetails.ClickBankAccountContBtn();
                _bankDetails.EnterBankDetailsTxt();
                _bankDetails.ClickAcctDetailsBtn();

                // Your Income page
                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome,"0");
                _bankDetails.ClickConfirmIncomeBtn();

                // Your Expenses page
                _bankDetails.SelectOtherDebtRepaymentsOptionBtn();
                _bankDetails.SelectDependantsLst(TestData.Dependents.Zero);
                _bankDetails.ClickConfirmExpensesBtn();
                _bankDetails.ClickNoGovtBenefitsbtn();
                _bankDetails.ClickAgreeAppSubmitBtn();
                _bankDetails.ClickConfirmSummaryBtn();

                // Loan Setup page
                // Trigger marketing survey by clicking 'Your Dashboard' link
                Thread.Sleep(3000); // need this otherwise we try to load the wrong survey
                _homeDetails.ClickDesktopYourDashboardLnk();
                Thread.Sleep(1000); // need this otherwise we won't close the survey correctly
                _homeDetails.ClickMarketSurveyCloseBtn();
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}