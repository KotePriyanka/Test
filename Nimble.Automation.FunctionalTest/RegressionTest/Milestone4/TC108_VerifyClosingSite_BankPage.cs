using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest.Milestone4
{
    //<Summary>
    // On the Bank Detail pages if the user tries to leave (clicking in the URL bar),
    // we will present them a marketing survey about why they are leaving.
    // Applies to desktop NL and RL flow only
    // Can be done only on Returner Loaner
    //</Summary>

    [TestFixture, Parallelizable, Category("Milestone4"), Category("Misc")]
    class TC108_VerifyClosingSite_BankPage : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
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

        [TestCase(900, "android", TestName = "TC108_VerifyClosingSite_BankPage_RL_android_900"), Category("NL"), Retry(2)]
        [TestCase(900, "ios", TestName = "TC108_VerifyClosingSite_BankPage_RL_ios_900")]
        public void TC108_VerifyClosingSite_BankPage_RL(int loanamout, string strmobiledevice)
        {
              strUserType = "RL";
            try
            {
                _driver = _testengine.TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
                _personalDetails = new PersonalDetails(_driver, "RL");
                _bankDetails = new BankDetails(_driver, "RL");

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

                // Click 'Your Dashboard' link to trigger market survey
                // Close 'Bank marketing survey'
                _homeDetails.ClickDesktopYourDashboardLnk();
                _homeDetails.ClickMarketSurveyCloseBtn();

            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}