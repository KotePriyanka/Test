using System;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Nimble.Automation.FunctionalTest.Milestone4
{
    [TestFixture, Parallelizable, Category("Milestone4"), Category("Two or More SAAC")]
    class TC120_VerifyHasSACCYes_RePayNo_UptoDateYes_NimblePay_Yes_NL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null;string strMessage,strUserType="";  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(1200, "android", TestName = "TC120_VerifyHasSACCYes_RePayNo_UptoDateYes_NimblePay_Yes_NL_1200"), Category("NL"), Category("Mobile"), Retry(2)]
        [TestCase(3300, "ios", TestName = "TC120_VerifyHasSACCYes_RePayNo_UptoDateYes_NimblePay_Yes_NL_3300")]
        public void TC120_VerifyingHasSACCYes_RePayNo_UptoDateYes_NimblePay_Yes_NL(int loanamout, string strmobiledevice)
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

                _obj.Have2SACCLoan = "Yes";

                //_personalDetails.PopulatePersonalDetails();
                _personalDetails.PopulatePersonalDetails(_obj);

                // Click on checks out Continue Button
                _personalDetails.ClickCheckoutContinueBtn();

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.TwoOrmoreSACCLoans.Yodlee.UID, TestData.BankDetails.TwoOrmoreSACCLoans.Yodlee.PWD);

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

               // Verify Other short-term loans and select POL's
                _bankDetails.SelectPOLExistingSACC(TestData.POL.Households, TestData.POL.EducationFees);

                // Select Have you fully repaid previos loan - "No"
                _bankDetails.SelectFullyRepaidLoanbtn(false);

                //Select are you uptodate with repayment- "Yes"
                _bankDetails.SelectAreyouUpdatewithLoanBtn(true);

                //Select To use any of this Nimble loan to make these repayments - "Yes"
                _bankDetails.SelectUseNimbletoRepayLoanBtn(true);

                // click on Confirm SACC continue button
                _bankDetails.ClickConfirmSACCNamesBtn();

                // Verify unsuccessful message
                string UnsuccessMsg = "Application unsuccessful";
                Assert.IsTrue(_personalDetails.GetUnsuccessMessage().Contains(UnsuccessMsg));

                //verify DNQ Message
                string ActualDNQMessage = "You currently don't qualify for a Nimble loan";
                Assert.IsTrue(_personalDetails.GetDNQMessage().Contains(ActualDNQMessage));

            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }

    [TestFixture, Parallelizable, Category("Milestone4"), Category("Two or More SAAC")]
    class TC120_VerifyHasSACCYes_RePayNo_UptoDateYes_NimblePay_Yes_RL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null;string strMessage,strUserType="";  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1550, "android", TestName = "TC120_VerifyHasSACCYes_RePayNo_UptoDateYes_NimblePay_Yes_RL_1550"), Category("RL"), Retry(2)]
        [TestCase(2750, "ios", TestName = "TC120_VerifyHasSACCYes_RePayNo_UptoDateYes_NimblePay_Yes_RL_2750")]
        public void TC120_VerifyingHasSACCYes_RePayNo_UptoDateYes_NimblePay_Yes_RL(int loanamout, string strmobiledevice)
        {
            strUserType = "RL";
            try
            {
                _driver = _testengine.TestSetup(strmobiledevice,"RL");
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

                // Fetching First Name
                string Firstname = _loanPurposeDetails.GetFirstName();

                // select Employement Status
                _personalDetails.SelectEmploymentStatusLst(TestData.YourEmployementStatus.FullTime);

                // select short term loans value as YES
                _personalDetails.ClickYesShortTermLoanStatusBtn();

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
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.TwoOrmoreSACCLoans.Yodlee.UID, TestData.BankDetails.TwoOrmoreSACCLoans.Yodlee.PWD);

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

               // Verify Other short-term loans and select POL's
                _bankDetails.SelectPOLExistingSACC(TestData.POL.Households, TestData.POL.EducationFees);

                // Select Have you fully repaid previos loan - "No"
                _bankDetails.SelectFullyRepaidLoanbtn(false);

                //Select are you uptodate with repayment- "Yes"
                _bankDetails.SelectAreyouUpdatewithLoanBtn(true);

                //Select To use any of this Nimble loan to make these repayments - "Yes"
                _bankDetails.SelectUseNimbletoRepayLoanBtn(true);

                // click on Confirm SACC continue button
                _bankDetails.ClickConfirmSACCNamesBtn();

                // Verify unsuccessful message
                string UnsuccessMsg = "Application unsuccessful";
                Assert.IsTrue(_personalDetails.GetUnsuccessMessage().Contains(UnsuccessMsg));

                //verify DNQ Message
                string ActualDNQMessage = "You currently don't qualify for a Nimble loan";
                Assert.IsTrue(_personalDetails.GetDNQMessage().Contains(ActualDNQMessage));
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}
