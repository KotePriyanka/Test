using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest
{
    [TestFixture, Parallelizable, Category("Milestone4"), Category("Employment")]
    class TC112_Employment_Selfemployed_NL : TestEngine
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
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();

        [TestCase(1600, "android", "Once-off product purchase,Computer", TestName = "TC112_Employment_Selfemployed_NL_1600"), Category("NL"), Retry(2)]
        [TestCase(1900, "android", "Basic living/work expenses,Emergency Repairs", TestName = "TC112_Employment_Selfemployed_NL_1900")]
        public void Employment_Selfemployed_NL(int loanamout, string strmobiledevice, string POL)
        {
            strUserType = "NL";  
            try
            {
                _driver = TestSetup(strmobiledevice);
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
                        _loanPurposeDetails.RequestLoanAmountMobile(loanamout, POL);
                    }
                    else
                    {
                        _loanPurposeDetails.RequestLoanAmount(loanamout, POL);
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
                        _loanPurposeDetails.SelectLoanPurposeMobile(POL);
                    }
                    else
                    {
                        // Select Purpose of loan
                        _loanPurposeDetails.SelectLoanPurpose(POL);
                    }

                    // Enter FirstPOLLoan Amount
                    _loanPurposeDetails.EnterFirstPOLAmountTxt(loanamout.ToString());

                    // Click on Continue Button
                    _loanPurposeDetails.ClickLoanPOLContinueBtn();
                }
                PersonalDetailsDataObj _personalDetailsData = new PersonalDetailsDataObj();
                _personalDetailsData.EmploymentStatus = "Self Employed";
                // entering personal details with overwrite values
                _personalDetails.PopulatePersonalDetails(_personalDetailsData);

                //Click on No for personal use
                _personalDetails.ClickNoForPersonalUseBtn();

                // Selenium needs to click Continue button again to proceed, else it fails after popping extra questions
                _personalDetails.ClickPersonaldetailsContinueBtn();

                string strval = _personalDetails.GetDNQTxt();
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

        #region commented

        //    // select Bank Name  
        //    _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

        //            // Click on Continue Button
        //            _bankDetails.BankSelectContinueBtn();

        //            // Entering Username and Password
        //            _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.UID, TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.PWD);

        //            // Click on Continue Button
        //            _bankDetails.ClickAutoContinueBtn();

        //            // choose bank account
        //            _bankDetails.BankAccountSelectBtn();

        //            // Click on bank select Continue Button
        //            _bankDetails.ClickBankAccountContBtn();

        //            // Confirm Bank Details
        //            _bankDetails.EnterBankDetailsTxt();

        //            // Click on Confirm account details Continue Button  
        //            _bankDetails.ClickAcctDetailsBtn();

        //            // Select Category 
        //            _bankDetails.SelectIncomeCategoryLst(TestData.IncomeCategory.PrimaryIncome);

        //            // Select Just checking option 
        //            //_bankDetails.SelectJustCheckingOptionLst("Yes, it will stay the same (or more)");

        //            // click on Confirm Income Button
        //            _bankDetails.ClickConfirmIncomeBtn();

        //            // select  other debt repayments option No 
        //            _bankDetails.SelectOtherDebtRepaymentsOptionBtn();

        //            // select dependents 
        //            _bankDetails.SelectDependantsLst(TestData.Dependents.Zero);

        //            // Click on continue
        //            _bankDetails.ClickConfirmExpensesBtn();

        //            // select Governments benefits option No
        //            _bankDetails.ClickGovtBenefitsOptionLst();

        //            // click on Agree that information True
        //            _bankDetails.ClickAgreeAppSubmitBtn();

        //            // click on confirm Submit button
        //            _bankDetails.ClickConfirmSummaryBtn();

        //            if (loanamout > 2000)
        //            {
        //                // enter sms input as OTP 
        //                if (_bankDetails.VerifySMSOTP())
        //                    _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);
        //            }

        //            // Verify ApprovedAmount
        //            Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout), "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());

        //            // click on Buton Submit
        //            _loanSetUpDetails.ClickSubmitBtn();

        //            // Select Reason for Spend Less
        //            //bool ReasonPageExists = _loanSetUpDetails.FindandselectSpendless();
        //            //if (ReasonPageExists == true)
        //            //{
        //            //    _loanSetUpDetails.SelectReasontospendLess(TestData.ReasonforspeandLess.cheaperproduct);
        //            //}

        //            //  Scrolling the Loan Contract
        //            _loanSetUpDetails.Loancontract();

        //            // Confirming accepting contract
        //            _loanSetUpDetails.ConfirmAcceptingContract();

        //            // click on I Agree button
        //            _loanSetUpDetails.ClickOnAgreeBtn();

        //            // click on No thanks Button
        //            _loanSetUpDetails.ClickNothanksBtn();

        //            // Verify Funded Amount
        //            Assert.IsTrue(_loanSetUpDetails.VerifyFundedAmount(loanamout), "Expected Requested Amount : " + loanamout + ". Observed Funded Amount : " + _loanSetUpDetails.VerifyFundedAmount());

        //            if (GetPlatform(_driver))
        //            {
        //                // Click on To Loan Dashboard Button
        //                _loanSetUpDetails.ClickMobileLoanDashboardBtn();

        //                // click on More Button from Bottom Menu
        //                _loanSetUpDetails.ClickMoreBtn();

        //                //Logout
        //                _loanSetUpDetails.Logout();

        //            }
        //            else
        //            {
        //                // Click on Loan Dashboard Button
        //                _loanSetUpDetails.ClickLoanDashboard();

        //                //Logout
        //                _loanSetUpDetails.Logout();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            strMessage += ex.Message; Assert.Fail(ex.Message);
        //        }
        //        //finally
        //        //{
        //        //    _driver.Quit(); _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        //        //    _driver.Quit(); _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        //        //}
        //    }

        //}

        #endregion       
    }
    [TestFixture, Parallelizable, Category("Milestone4"), Category("Employment")]
    class TC112_Employment_Selfemployed_RL : TestEngine
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
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();

        [TestCase(1100, "android", "Vehicle,Registration/insurance", TestName = "TC112_Employment_Selfemployed_RL_1100"), Category("RL"), Retry(2)]
        [TestCase(2800, "android", "Vehicle,Maintenance", TestName = "TC112_Employment_Selfemployed_RL_2800")]
        public void Employment_Selfemployed_RL(int loanamout, string strmobiledevice, string POL)
        {
            strUserType = "RL";
            try
            {
                _driver = TestSetup(strmobiledevice, "RL");
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
                _loanPurposeDetails.SelectLoanPurposeRL(POL);

                // Enter FirstPOLLoan Amount
                _loanPurposeDetails.EnterFirstPOLAmountTxt(loanamout.ToString());

                // Click on Continue Button
                _loanPurposeDetails.ClickLoanPOLContinueBtnRL();

                // select Employement Status
                _personalDetails.SelectEmploymentStatusLst(TestData.YourEmployementStatus.SelfEmployed);

                // Click yes for personal use
                _personalDetails.ClickYesForPersonalUseBtn();

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


















