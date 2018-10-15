using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest
{

    [TestFixture, Parallelizable, Category("Milestone2"), Category("Income Inconsistent")]
    class TC017_VerifyInconsistencyIncome : TestUtility
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private string email = "";
        private IWebDriver _driver = null; public string strMessage = "",strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();


        [TearDown]
        public void Aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, email, starttime);
            email = "";
        }

        [TestCase(600, "Yes", "No", "android", TestName = "TC017_VerifyInconsistencyIncome_NL_600"), Category("NL"), Retry(2)]
        [TestCase(3500, "Yes", "No", "ios", TestName = "TC017_VerifyInconsistencyIncome_NL_3500")]
        public void TC017_VerifyingInconsistencyIncome_NL(int loanamount, string reason1, string reason2, string mobiledevice)
        {
            VerifyInconsistencyIncome_NL(loanamount, reason1, reason2, mobiledevice);
        }

        public void VerifyInconsistencyIncome_NL(int loanamout, string income1, string income2, string mobiledevice)
        {
            strUserType = "NL"; 

            _driver = _testengine.TestSetup(mobiledevice);
            _homeDetails = new HomeDetails(_driver, "NL");
            _loanPurposeDetails = new LoanPurposeDetails(_driver, "NL");
            _personalDetails = new PersonalDetails(_driver, "NL");
            _bankDetails = new BankDetails(_driver, "NL");
            _loanSetUpDetails = new LoanSetUpDetails(_driver, "NL");

            try
            {

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
                    _loanPurposeDetails.SelectPOLAndAmount(loanamout, TestData.POL.Households);
                }

                PersonalDetailsDataObj _dataObj = new PersonalDetailsDataObj();
                _dataObj.StreetName = TestData.OverrideCodes.PassAll_NL;

                _personalDetails.PopulatePersonalDetails(_dataObj);

                // Click on checks out Continue Button
                _personalDetails.ClickCheckoutContinueBtn();

                email = _personalDetails.EmailID;

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.IncosIncome_TC017.Yodlee.UID, TestData.BankDetails.IncosIncome_TC017.Yodlee.PWD);

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
                _bankDetails.SelectIncomeCategDeposit1(TestData.IncomeCategory.PrimaryIncome);

                // Select Just checking option 
                SelectIncome1Reason(income1);

                // Select Category 
                _bankDetails.SelectIncomeCategDeposit2(TestData.IncomeCategory.PrimaryIncome);

                // Select Just checking option 
                SelectIncome2Reason(income2);

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

                System.Threading.Thread.Sleep(2000);

                // enter sms input as OTP 
                if (loanamout > 2000)
                    _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);

                if (loanamout > 2000 && FinalReviewEnabled == "true")
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
                        _bankDetails.ClickSetup();

                        // Verify ApprovedAmount
                        Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout), "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());

                        // click on Buton Submit
                        _loanSetUpDetails.ClickSubmitBtn();
                    }
                    else
                    {
                        //ClickOn Loan Dashboard...Manual Approval
                        _loanSetUpDetails.ClickLoanDashboardManual();

                        //click on Final Approve
                        _loanSetUpDetails.FinalApprove();

                        //Click Setup Button
                        _bankDetails.ClickSetup();

                        // Verify ApprovedAmount
                        Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout), "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());

                        // click on Buton Submit
                        _loanSetUpDetails.ClickSubmitBtn();
                    }
                }
                else
                {
                    // Verify ApprovedAmount
                    Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout), "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());

                    // click on Buton Submit
                    _loanSetUpDetails.ClickSubmitBtn();
                }

                // Select Reason for Spend Less
                bool ReasonPageExists = _loanSetUpDetails.FindandselectSpendless();

                if (ReasonPageExists == true)
                {
                    _loanSetUpDetails.SelectReasontospendLess(TestData.ReasonforspeandLess.cheaperproduct);
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
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }

            #region commented old scenario

            //// click on Buton Submit
            //_loanSetUpDetails.ClickSubmitBtn();

            //    // Select Reason for Spend Less
            //    bool ReasonPageExists = _loanSetUpDetails.FindandselectSpendless();

            //    if (ReasonPageExists == true)
            //    {
            //        _loanSetUpDetails.SelectReasontospendLess(TestData.ReasonforspeandLess.cheaperproduct);
            //    }

            //    //  Scrolling the Loan Contract
            //    _loanSetUpDetails.Loancontract();

            //    // Confirming accepting contract
            //    _loanSetUpDetails.ConfirmAcceptingContract();

            //    // click on I Agree button
            //    _loanSetUpDetails.ClickOnAgreeBtn();

            //    // click on No thanks Button
            //    _loanSetUpDetails.ClickNothanksBtn();

            //    if (GetPlatform(_driver))
            //    {
            //        // Click on To Loan Dashboard Button
            //        _loanSetUpDetails.ClickMobileLoanDashboardBtn();

            //        // click on More Button from Bottom Menu
            //        _loanSetUpDetails.ClickMoreBtn();

            //        //Logout
            //        _loanSetUpDetails.Logout();
            //    }
            //    else
            //    {
            //        // Click on Loan Dashboard Button
            //        _loanSetUpDetails.ClickLoanDashboard();

            //        //Logout
            //        _loanSetUpDetails.Logout();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    strMessage += ex.Message; Assert.Fail(ex.Message);
            //}
            #endregion

        }

        public void VerifyInconsistencyIncome_RL(int loanamout, string income1, string income2, string mobiledevice)
        {
             strUserType = "RL";
            _driver = _testengine.TestSetup(mobiledevice, "RL");
            _homeDetails = new HomeDetails(_driver, "RL");
            _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
            _personalDetails = new PersonalDetails(_driver, "RL");
            _bankDetails = new BankDetails(_driver, "RL");
            _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

            try
            {
                // Login with existing user
                _homeDetails.LoginExistingUser(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.NewProductAdvancePaidClean);

                email = _homeDetails.RLEmailID;

                // Click on Request Money Button
                _homeDetails.ClickRequestMoneyBtn();

                //Click on Start Application Button
                _homeDetails.ClickExistinguserStartApplictionBtn();

                // Select Loan Value from Slide bar
                _loanPurposeDetails.SelectLoanValueRL(loanamout);

                //Click on Select First POL Lst
                _loanPurposeDetails.ClickSelectFirstPurposeBtn();

                // Select Purpose of loan
                _loanPurposeDetails.SelectLoanPurposeRL(TestData.POL.Homerepairsorimprovements);

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
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.IncosIncome_TC017.Yodlee.UID, TestData.BankDetails.IncosIncome_TC017.Yodlee.PWD);

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
                _bankDetails.SelectIncomeCategDeposit1(TestData.IncomeCategory.PrimaryIncome);

                // Select Just checking option 
                SelectIncome1Reason(income1);

                // Select Category 
                _bankDetails.SelectIncomeCategDeposit2(TestData.IncomeCategory.PrimaryIncome);

                // Select Just checking option 
                SelectIncome2Reason(income2);

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
                    // click on Buton Submit
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
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }

        }

        public void VerifyInconsistencyIncome_NL(int loanamout, string income1, string income2, string mobiledevice, bool verification)
        {
            strUserType = "NL";  
            _driver = _testengine.TestSetup(mobiledevice);
            _homeDetails = new HomeDetails(_driver, "NL");
            _loanPurposeDetails = new LoanPurposeDetails(_driver, "NL");
            _personalDetails = new PersonalDetails(_driver, "NL");
            _bankDetails = new BankDetails(_driver, "NL");
            _loanSetUpDetails = new LoanSetUpDetails(_driver, "NL");

            try
            {
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
                    _loanPurposeDetails.SelectPOLAndAmount(loanamout, TestData.POL.Households);
                }

                PersonalDetailsDataObj _dataObj = new PersonalDetailsDataObj();
                _dataObj.StreetName = TestData.OverrideCodes.PassAll_RL;

                _personalDetails.PopulatePersonalDetails(_dataObj);

                // Click on checks out Continue Button
                _personalDetails.ClickCheckoutContinueBtn();

                email = _personalDetails.EmailID;

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.IncosIncome_TC017.Yodlee.UID, TestData.BankDetails.IncosIncome_TC017.Yodlee.PWD);

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
                _bankDetails.SelectIncomeCategDeposit1(TestData.IncomeCategory.PrimaryIncome);

                // Select Just checking option 
                SelectIncome1Reason(income1);

                // Select Category 
                _bankDetails.SelectIncomeCategDeposit2(TestData.IncomeCategory.PrimaryIncome);

                // Select Just checking option 
                SelectIncome2Reason(income2);

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

                System.Threading.Thread.Sleep(2000);

                // enter sms input as OTP 
                if (loanamout > 2000)
                    _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);

                //if the application goes to manual verification then approving from Final Approval
                if (verification)
                {
                    //Click on Launch dashboard
                    _bankDetails.ClickLoanDashboardManual();

                    //Click on Final approval
                    _bankDetails.ClickFinalApproval();

                    //Click on setup
                    _bankDetails.ClickSetup();
                }

                // click on Buton Submit
                _loanSetUpDetails.ClickSubmitBtn();

                // Select Reason for Spend Less
                bool ReasonPageExists = _loanSetUpDetails.FindandselectSpendless();

                if (ReasonPageExists == true)
                {
                    _loanSetUpDetails.SelectReasontospendLess(TestData.ReasonforspeandLess.cheaperproduct);
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
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }

        public void VerifyInconsistencyIncome_RL(int loanamout, string income1, string income2, string mobiledevice, bool verification)
        {
             strUserType = "RL";
            _driver = _testengine.TestSetup(mobiledevice, "RL");
            _homeDetails = new HomeDetails(_driver, "RL");
            _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
            _personalDetails = new PersonalDetails(_driver, "RL");
            _bankDetails = new BankDetails(_driver, "RL");
            _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

            try
            {
                // Login with existing user
                _homeDetails.LoginExistingUser(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.NewProductAdvancePaidClean);


                email = _homeDetails.RLEmailID;

                // Click on Request Money Button
                _homeDetails.ClickRequestMoneyBtn();

                //Click on Start Application Button
                _homeDetails.ClickExistinguserStartApplictionBtn();

                // Select Loan Value from Slide bar
                _loanPurposeDetails.SelectLoanValueRL(loanamout);

                //Click on Select First POL Lst
                _loanPurposeDetails.ClickSelectFirstPurposeBtn();

                // Select Purpose of loan
                _loanPurposeDetails.SelectLoanPurposeRL(TestData.POL.Homerepairsorimprovements);

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
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.IncosIncome_TC017.Yodlee.UID, TestData.BankDetails.IncosIncome_TC017.Yodlee.PWD);

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
                _bankDetails.SelectIncomeCategDeposit1(TestData.IncomeCategory.PrimaryIncome);

                // Select Just checking option 
                SelectIncome1Reason(income1);

                // Select Category 
                _bankDetails.SelectIncomeCategDeposit2(TestData.IncomeCategory.PrimaryIncome);

                // Select Just checking option 
                SelectIncome2Reason(income2);

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

                //if the application goes to manual verification then approving from Final Approval
                if (verification)
                {
                    //Click on Launch dashboard
                    _bankDetails.ClickLoanDashboardManual();

                    //Click on Final approval
                    _bankDetails.ClickFinalApproval();

                    //Click on setup
                    _bankDetails.ClickSetup();
                }

                if (GetPlatform(_driver))
                {
                    // click on Button Submit
                    _loanSetUpDetails.ClickSubmitBtn();

                    // Click on Bank Account to transfer
                    _bankDetails.ClicksixtyMinuteButton();

                    // click on sublit-payment Button
                    _bankDetails.ClickSubmitPaymentButton();
                }

                else
                {
                    // Click on Bank Account to transfer
                    _bankDetails.ClicksixtyMinuteButton();

                    // click on Buton Submit
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
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }


        }

        public void SelectIncome1Reason(string income1)
        {
            if (income1 == "Yes")
                _bankDetails.SelectJustCheckingDeposit1(TestData.ConfirmIncomeConsistency.Yes);
            else if (income1 == "No")
                _bankDetails.SelectJustCheckingDeposit1(TestData.ConfirmIncomeConsistency.No);
            else if (income1 == "Other")
            {
                _bankDetails.SelectJustCheckingDeposit1(TestData.ConfirmIncomeConsistency.Other);
                _bankDetails.EnterTxtForOtherOption("sales will impact my wages");
            }
        }

        public void SelectIncome2Reason(string income2)
        {
            if (income2 == "Yes")
                _bankDetails.SelectJustCheckingDeposit2(TestData.ConfirmIncomeConsistency.Yes);
            else if (income2 == "No")
                _bankDetails.SelectJustCheckingDeposit2(TestData.ConfirmIncomeConsistency.No);
            else if (income2 == "Other")
            {
                _bankDetails.SelectJustCheckingDeposit2(TestData.ConfirmIncomeConsistency.Other);
                _bankDetails.EnterTxtForOtherOption("sales will impact my wages");
            }
        }
    }

    [TestFixture, Parallelizable, Category("Milestone2"), Category("Income Inconsistent")]
    class TC017_VerifyingInconsistencyIncome_RL : TestUtility
    {
        public TestEngine _testengine = new TestEngine();

        TC017_VerifyInconsistencyIncome _test = new TC017_VerifyInconsistencyIncome();

        [TearDown]
        public void Cleanup()
        {
            _test.Aftermethod();
        }

        [TestCase(1500, "Yes", "No", "android", TestName = "TC017_VerifyInconsistencyIncome_RL_1500"), Category("RL"), Retry(2)]
        [TestCase(3050, "Yes", "No", "ios", TestName = "TC017_VerifyInconsistencyIncome_RL_3050")]
        public void TC017_VerifyInconsistencyIncome_RL(int loanamount, string reason1, string reason2, string mobiledevice)
        {
            _test.VerifyInconsistencyIncome_RL(loanamount, reason1, reason2, mobiledevice);
        }

    }
}
