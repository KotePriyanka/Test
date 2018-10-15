using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;
using Nimble.Automation.FunctionalTest;

namespace Nimble.Automation.FunctionalTest.Milestone5
{
    [TestFixture, Parallelizable, Category("Milestone5"), Category("Misc")]
    class TC159_VerifyUserwithexistingOneSAACLoan : TestEngine
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

        [TestCase(1100, "android", false, true, false, TestName = "TC159_VerifyExistingOne_SACC_Repaid_NO_Uptodate_Yes_NimblePay_NO_NL_1100"), Category("NL"), Retry(2)]
        [TestCase(1150, "android", false, true, true, TestName = "TC159_VerifyExistingOne_SACC_Repaid_NO_Uptodate_Yes_NimblePay_Yes_NL_1150")]
        [TestCase(1200, "android", false, false, false, TestName = "TC159_VerifyExistingOne_SACC_Repaid_NO_Uptodate_NO_NL_1200")]

        [TestCase(3800, "ios", false, true, false, TestName = "TC159_VerifyExistingOne_SACC_Repaid_NO_Uptodate_Yes_NimblePay_NO_NL_3800")]
        [TestCase(3850, "ios", false, true, true, TestName = "TC159_VerifyExistingOne_SACC_Repaid_NO_Uptodate_Yes_NimblePay_Yes_NL_3850")]
        [TestCase(3900, "ios", false, false, false, TestName = "TC159_VerifyExistingOne_SACC_Repaid_NO_Uptodate_NO_NL_3900")]

        public void TC159_VerifyUserwithexistingOneSAACLoan_NL(int loanamout, string strmobiledevice, bool repaid, bool uptodate, bool nimblepay)
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
                        _loanPurposeDetails.RequestLoanAmountMobile(loanamout, TestData.POL.Households);
                    }
                    else
                    {
                        _loanPurposeDetails.RequestLoanAmount(loanamout, TestData.POL.Households);
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
                        _loanPurposeDetails.SelectLoanPurposeMobile(TestData.POL.Households);
                    }
                    else
                    {
                        // Select Purpose of loan
                        _loanPurposeDetails.SelectLoanPurpose(TestData.POL.Households);
                    }

                    // Enter FirstPOLLoan Amount
                    _loanPurposeDetails.EnterFirstPOLAmountTxt(loanamout.ToString());

                    // Click on Continue Button
                    _loanPurposeDetails.ClickLoanPOLContinueBtn();
                }
                // entering personal details with overwrite values
                _personalDetails.PopulatePersonalDetails();

                // Click on checks out Continue Button
                _personalDetails.ClickCheckoutContinueBtn();

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.OneSAAC.Yodlee.UID, TestData.BankDetails.OneSAAC.Yodlee.PWD);

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
                _bankDetails.SelectPOLExistingSACC(TestData.POL.Households);

                // Select Have you fully repaid previos loan - "repaid"
                _bankDetails.SelectFullyRepaidLoanbtn(repaid);

                //Select are you uptodate with repayment- "uptodate"
                _bankDetails.SelectAreyouUpdatewithLoanBtn(uptodate);

                if (uptodate)
                {
                    //Select To use any of this Nimble loan to make these repayments - "nimblepay"
                    _bankDetails.SelectUseNimbletoRepayLoanBtn(nimblepay);
                }

                // click on Confirm SACC continue button
                _bankDetails.ClickConfirmSACCNamesBtn();

                if (nimblepay || !uptodate)
                {
                    // Verify unsuccessful message
                    string UnsuccessMsg = "Application unsuccessful";
                    Assert.IsTrue(_personalDetails.GetUnsuccessMessage().Contains(UnsuccessMsg));

                    //verify DNQ Message
                    string ActualDNQMessage = "You currently don't qualify for a Nimble loan";
                    Assert.IsTrue(_personalDetails.GetDNQMessage().Contains(ActualDNQMessage));
                }
                else
                {
                    // Select Category 
                    _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "0");

                    // Select Just checking option 
                    _bankDetails.SelectJustCheckingOptionLst("Yes, it will stay the same (or more)");

                    // click on Confirm Income Button
                    _bankDetails.ClickConfirmIncomeBtn();

                    // select  other debt repayments option No 
                    // _bankDetails.SelectOtherDebtRepaymentsOptionBtn();

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
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }

    [TestFixture, Parallelizable, Category("Milestone5"), Category("Misc")]
    class TC159_VerifyUserwithexistingOneSAACLoan_RL : TestEngine
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

        [TestCase(1000, "android", false, true, false, TestName = "TC159_VerifyExistingOne_SACC_Repaid_NO_Uptodate_Yes_NimblePay_NO_RL_1000"), Category("RL"), Retry(2)]
        [TestCase(1050, "android", false, true, true, TestName = "TC159_VerifyExistingOne_SACC_Repaid_NO_Uptodate_Yes_NimblePay_Yes_RL_1050")]
        [TestCase(1300, "android", false, false, false, TestName = "TC159_VerifyExistingOne_SACC_Repaid_NO_Uptodate_NO_RL_1300")]

        [TestCase(3700, "ios", false, true, false, TestName = "TC159_VerifyExistingOne_SACC_Repaid_NO_Uptodate_Yes_NimblePay_NO_RL_3700")]
        [TestCase(3750, "ios", false, true, true, TestName = "TC159_VerifyExistingOne_SACC_Repaid_NO_Uptodate_Yes_NimblePay_Yes_RL_3750")]
        [TestCase(3600, "ios", false, false, false, TestName = "TC159_VerifyExistingOne_SACC_Repaid_NO_Uptodate_NO_RL_3600")]

        public void TC159_VerifyingUserwithexistingOneSAACLoan_RL(int loanamout, string strmobiledevice, bool repaid, bool uptodate, bool nimblepay)
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

                // Login with existing user
                _homeDetails.LoginExistingUser(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.NewProductAdvancePaidClean);

                // Click on Request Money Button
                _homeDetails.ClickRequestMoneyBtn();

                //Click on Start Application Button
                _homeDetails.ClickExistinguserStartApplictionBtn();

                // Select Loan Value from Slide bar
                _loanPurposeDetails.SelectLoanValueRL(loanamout);

                //Click on Select First POL Lst
                _loanPurposeDetails.ClickSelectFirstPurposeBtn();

                // Select Purpose of loan
                _loanPurposeDetails.SelectLoanPurposeRL(TestData.POL.Households);

                // Enter FirstPOLLoan Amount
                _loanPurposeDetails.EnterFirstPOLAmountTxt(loanamout.ToString());

                // Click on Continue Button
                _loanPurposeDetails.ClickLoanPOLContinueBtnRL();

                // select Employement Status
                _personalDetails.SelectEmploymentStatusLst(TestData.YourEmployementStatus.FullTime);

                // select short term loans value as NO
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
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.OneSAAC.Yodlee.UID, TestData.BankDetails.OneSAAC.Yodlee.PWD);

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
                _bankDetails.SelectPOLExistingSACC(TestData.POL.Households);

                // Select Have you fully repaid previos loan - "repaid"
                _bankDetails.SelectFullyRepaidLoanbtn(repaid);

                //Select are you uptodate with repayment- "uptodate"
                _bankDetails.SelectAreyouUpdatewithLoanBtn(uptodate);

                if (uptodate)
                {
                    //Select To use any of this Nimble loan to make these repayments - "nimblepay"
                    _bankDetails.SelectUseNimbletoRepayLoanBtn(nimblepay);
                }

                // click on Confirm SACC continue button
                _bankDetails.ClickConfirmSACCNamesBtn();

                if (nimblepay || !uptodate)
                {
                    // Verify unsuccessful message
                    string UnsuccessMsg = "Application unsuccessful";
                    Assert.IsTrue(_personalDetails.GetUnsuccessMessage().Contains(UnsuccessMsg));

                    //verify DNQ Message
                    string ActualDNQMessage = "You currently don't qualify for a Nimble loan";
                    Assert.IsTrue(_personalDetails.GetDNQMessage().Contains(ActualDNQMessage));

                }
                else
                {
                    // Select Category 
                    _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "0");

                    // Select Just checking option 
                    _bankDetails.SelectJustCheckingOptionLst("Yes, it will stay the same (or more)");

                    // click on Confirm Income Button
                    _bankDetails.ClickConfirmIncomeBtn();

                    // select  other debt repayments option No 
                    // _bankDetails.SelectOtherDebtRepaymentsOptionBtn();

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

                            // click on Buton Submit
                            _loanSetUpDetails.ClickSubmitBtn();
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

                            // click on Buton Submit
                            _loanSetUpDetails.ClickSubmitBtn();
                        }
                    }
                    else
                    {
                        // click on  SubmitBtn;
                        _loanSetUpDetails.ClickSubmitBtn();
                    }
                    
                    // Select Reason for Spend Less
                    bool ReasonPageExists = _loanSetUpDetails.FindandselectSpendless();

                    if (ReasonPageExists == true)
                    {
                        _loanSetUpDetails.SelectReasontospendLess(TestData.ReasonforspeandLess.cheaperservice);
                    }

                    //  Scrolling the Loan Contract
                    _loanSetUpDetails.Loancontract();

                    // Confirming accepting contract
                    _loanSetUpDetails.ConfirmAcceptingContract();

                    // click on I Agree button
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
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }

    }
}


