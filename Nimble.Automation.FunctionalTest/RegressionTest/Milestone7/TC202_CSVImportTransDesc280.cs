using System;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System.IO;

namespace Nimble.Automation.FunctionalTest.RegressionTest.Milestone7
{
    [TestFixture, Parallelizable, Category("Milestone7"), Category("TransDesc")]
    class TC202_CSVImportTransDesc280_NL : TestEngine
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
            if (_driver != null)
            {
                _driver.Quit();
                _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
            }
        }

        [TestCase(1200, "chrome", TestName = "TC202_CSVImportTransDesc280_NL_1200"), Category("NL"), Ignore("Functionality not yet ready"), Retry(2)]
        [TestCase(3300, "chrome", TestName = "TC202_CSVImportTransDesc280_NL_3300")]
        public void TC202_VerifyingCSVImportTransDesc280_NL(int loanamout, string strmobiledevice)
        {
            try
            {
                strUserType = "NL";
                _driver = _testengine.TestSetup(strmobiledevice);
                if (_driver != null)
                {
                    _homeDetails = new HomeDetails(_driver, "NL");
                    _loanPurposeDetails = new LoanPurposeDetails(_driver, "NL");
                    _personalDetails = new PersonalDetails(_driver, "NL");
                    _bankDetails = new BankDetails(_driver, "NL");
                    _loanSetUpDetails = new LoanSetUpDetails(_driver, "NL");

                    // Click on Apply Button
                    _homeDetails.ClickApplyBtn();

                    // Click on Start Your Application Button
                    _homeDetails.ClickStartApplictionBtn();

                    bool hideshow = _homeDetails.CheckHideShow();
                    if (hideshow == true)
                    {
                        if (GetPlatform(_driver))
                        {
                            _loanPurposeDetails.RequestLoanAmountMobile(loanamout, TestData.POL.Eventcosts.Anniversary);
                        }
                        else
                        {
                            _loanPurposeDetails.RequestLoanAmount(loanamout, TestData.POL.Eventcosts.Anniversary);
                        }
                    }
                    else
                    {
                        // Select Loan Value from Slide bar
                        _loanPurposeDetails.SelectLoanValue(loanamout);

                        //Click on First POL to select
                        _loanPurposeDetails.ClickSelectFirstPurposeBtn();

                        if (GetPlatform(_driver))
                        {
                            // Select Purpose of loan
                            _loanPurposeDetails.SelectLoanPurposeMobile(TestData.POL.Eventcosts.Anniversary);
                        }
                        else
                        {
                            // Select Purpose of loan
                            _loanPurposeDetails.SelectLoanPurpose(TestData.POL.Eventcosts.Anniversary);
                        }

                        // Enter FirstPOLLoan Amount
                        _loanPurposeDetails.EnterFirstPOLAmountTxt(loanamout.ToString());

                        // Click on Continue Button
                        _loanPurposeDetails.ClickLoanPOLContinueBtn();
                    }

                    // entering personal details with overwrite values
                    PersonalDetailsDataObj _obj = new PersonalDetailsDataObj();

                    _obj.Have2SACCLoan = "No";

                    //_personalDetails.PopulatePersonalDetails();
                    _personalDetails.PopulatePersonalDetails(_obj);

                    // Click on checks out Continue Button
                    _personalDetails.ClickCheckoutContinueBtn();

                    // select Bank Name  
                    _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                    // Click on Continue Button
                    _bankDetails.BankSelectContinueBtn();

                    // Entering Username and Password
                    _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.TransDesc280.Yodlee.UID, TestData.BankDetails.TransDesc280.Yodlee.PWD);

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
                }
            }
            catch (Exception ex)
            {
                strMessage += ex.Message;
                if (_driver != null)
                    Assert.Fail(ex.Message);
            }

        }
    }

    [TestFixture, Parallelizable, Category("Milestone7"), Category("TransDesc")]
    class TC202_CSVImportTransDesc280_RL : TestEngine
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
            if (_driver != null)
            {
                _driver.Quit();
                _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
            }
        }

        [TestCase(1550, "chrome", TestName = "TC202_CSVImportTransDesc280_RL_1550"), Category("RL"), Ignore("Functionality not yet ready"), Retry(2)]
        [TestCase(2750, "chrome", TestName = "TC202_CSVImportTransDesc280_RL_2750")]
        public void TC202_VerifyingCSVImportTransDesc280_RL(int loanamout, string strmobiledevice)
        {

            try
            {
                strUserType = "RL";
                _driver = _testengine.TestSetup(strmobiledevice, "RL");
                if (_driver != null)
                {
                    _homeDetails = new HomeDetails(_driver, "RL");
                    _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
                    _personalDetails = new PersonalDetails(_driver, "RL");
                    _bankDetails = new BankDetails(_driver, "RL");
                    _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

                    // Login with existing user
                    _homeDetails.LoginExistingUser(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.NewProductAdvancePaidClean);

                    // Click on Request Money Button
                    _homeDetails.ClickRequestMoneyBtn();

                    //Click on Start Application Button
                    _homeDetails.ClickExistinguserStartApplictionBtn();

                    //Click on Select First POL Lst
                    _loanPurposeDetails.ClickSelectFirstPurposeBtn();

                    // Select Purpose of loan
                    _loanPurposeDetails.SelectLoanPurposeRL(TestData.POL.Eventcosts.Birthdayparty);

                    // Select Loan Value from Slide bar
                    _loanPurposeDetails.SelectLoanValueRL(loanamout);

                    // Enter FirstPOLLoan Amount
                    _loanPurposeDetails.EnterFirstPOLAmountTxt(loanamout.ToString());

                    // Click on Continue Button
                    _loanPurposeDetails.ClickLoanPOLContinueBtnRL();

                    // select Employement Status
                    _personalDetails.SelectEmploymentStatusLst(TestData.YourEmployementStatus.FullTime);

                    // select short term loans value as No
                    _personalDetails.ClickNoShortTermLoanStatusBtn();

                    // Check Read Privacy and Electronic Authorisation
                    _personalDetails.CheckReadPrivacyBtn(TestData.ReturnerLoaner);

                    // Check Read Credit Guide
                    _personalDetails.CheckReadCreditBtn(TestData.ReturnerLoaner);

                    if (GetPlatform(_driver))
                    {
                        // Click on Personal Details Continue Button
                        _personalDetails.ClickPersonaldetailsContinueBtnRLMobile();
                    }
                    else
                    {
                        // Click on Personal Details Continue Button
                        _personalDetails.ClickPersonaldetailsRequestBtnRLDesktop();

                        // Click on checks out Continue Button
                        _personalDetails.ClickAutomaticVerificationBtn();
                    }

                    // select Bank Name  
                    _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                    // Click on Continue Button
                    _bankDetails.BankSelectContinueBtn();

                    // Entering Username and Password
                    _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.TransDesc280.Yodlee.UID, TestData.BankDetails.TransDesc280.Yodlee.PWD);

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

                    // verify final review enabled and process setup functionality
                    _loanSetUpDetails.loanSetupFunction(loanamout, strUserType);
                }
            }
            catch (Exception ex)
            {
                strMessage += ex.Message;
                if (_driver != null)
                    Assert.Fail(ex.Message);
            }
        }
    }
}
