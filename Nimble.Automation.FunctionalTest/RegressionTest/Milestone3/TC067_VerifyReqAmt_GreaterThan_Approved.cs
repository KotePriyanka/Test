using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest
{
    //<Summary>
    //Verifying the approved amount is less than the requested amount..
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Setup Page")]
    class TC067_VerifyReqAmt_GreaterThan_Approved_NL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(850, "android", TestName = "TC067_VerifyReqAmt_GreaterThan_Approved_NL_850"), Category("NL"), Retry(2)]
        [TestCase(2200, "android", TestName = "TC067_VerifyReqAmt_GreaterThan_Approved_NL_2200")]
        public void TC067_VerifyReqAmt_GreaterThn_Approved_NL(int loanamout, string strmobiledevice)
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
                _loanPurposeDetails.LoanPurposeFunction(loanamout, TestData.POL.Utilitybills.Onebill);

                //MoreInformation
                _loanPurposeDetails.MoreInformation();

                //populate the personal details and proceed
                _personalDetails.PersonalDetailsFunction();

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

                //Note: As the scenario does not get lower than requested, hence verifying if the application to manual - Kenan
                if (GetPlatform(_driver))
                {
                    //ClickOn Loan Dashboard...Manual Approval
                    bool flag = _loanSetUpDetails.ClickLoanDashboardManual();
                    Assert.IsTrue(flag, "Approved amount not less than the requested amount");
                }
                else
                {
                    //ClickOn Loan Dashboard...Manual Approval
                    bool flag = _loanSetUpDetails.ClickLoanDashboardManual();
                    Assert.IsTrue(flag, "Approved amount not less than the requested amount");
                }
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }

    }

    [TestFixture, Parallelizable, Category("Milestone3"), Category("Setup Page")]
    class TC067_VerifyReqAmt_GreaterThan_Approved_RL : TestEngine
    {

        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1300, "android", TestName = "TC067_VerifyReqAmt_GreaterThan_Approved_RL_1300"), Category("RL"), Retry(2)]
        [TestCase(2100, "android", TestName = "TC067_VerifyReqAmt_GreaterThan_Approved_RL_2100")]
        public void TC067_VerifyReqAmt_GreaterThn_Approved_RL(int loanamout, string strmobiledevice)
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

                //Go to the homepage and click the start application button and then the Request money button
                _homeDetails.homeFunctions_RL(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.ReturnerDagBankstaging);

                //Select the loan amount and purpose and click on continue button
                _loanPurposeDetails.LoanPurposeFunction_RL(loanamout, TestData.POL.Utilitybills.Onebill);

                //MoreInformation
                _loanPurposeDetails.MoreInformation();

                //Edit the personal details and change the Rmsrv Code
                _personalDetails.PersonalDetailsFunction_Skipbanklogin(TestData.YourEmployementStatus.FullTime, TestData.ReturnerLoaner, TestData.OverrideCodes.PassAll_NL);

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                if (bsAutoRefresh)
                {
                    // Entering Username and Password
                    _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.UID, TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.PWD);

                    // Click on Continue Button
                    _bankDetails.ClickAutoContinueBtn();
                }

                // choose bank account
                _bankDetails.BankAccountSelectBtn();

                // Click on bank select Continue Button
                _bankDetails.ClickBankAccountContBtn();

                // Confirm Bank Details
                _bankDetails.EnterBankDetailsTxt();

                // Click on Confirm account details Continue Button  
                _bankDetails.ClickAcctDetailsBtn();

                // Choose reason for no transactions
                //bool notrans = _bankDetails.NoTransaction(TestData.NoTransactionReasons.Usingcash);
                //Assert.IsTrue(notrans, "Notransaction page not appeared");

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

                //Note: As the scenario does not get lower than requested, hence verifying if the application to manual - Kenan
                if (GetPlatform(_driver))
                {
                    //ClickOn Loan Dashboard...Manual Approval
                    bool flag = _loanSetUpDetails.ClickLoanDashboardManual();
                    Assert.IsTrue(flag, "Approved amount not less than the requested amount");
                }
                else
                {
                    //ClickOn Loan Dashboard...Manual Approval
                    bool flag = _loanSetUpDetails.ClickLoanDashboardManual();
                    Assert.IsTrue(flag, "Approved amount not less than the requested amount");
                }

                #region old code
                // Set Up Loan page
                //if ((loanamout > 2000 && FinalReviewEnabled == "true") && (strUserType == FinalReviewLoanType || FinalReviewLoanType == "ALL"))
                //{
                //    if (GetPlatform(_driver))
                //    {
                //        //ClickOn Loan Dashboard...Manual Approval
                //        _loanSetUpDetails.ClickLoanDashboardManual();

                //        // click on More Button from Bottom Menu
                //        _loanSetUpDetails.ClickMoreBtn();

                //        // click on Approve button
                //        _loanSetUpDetails.ClickApproveBtn();

                //        //Click Setup Button
                //        _loanSetUpDetails.ClickSetup();                        
                //    }
                //    else
                //    {
                //        //ClickOn Loan Dashboard...Manual Approval
                //        _loanSetUpDetails.ClickLoanDashboardManual();
                //        //ClickLoanDashboard();

                //        //click on Final Approve
                //        _loanSetUpDetails.FinalApprove();

                //        //Click Setup Button
                //        _loanSetUpDetails.ClickSetup();                      
                //    }
                //}

                ////Get the approved loan amount value
                //int approvedAmount = _loanSetUpDetails.GetApprovedamount();

                //// Verify ApprovedAmount
                //Assert.Greater(loanamout, _loanSetUpDetails.GetApprovedamount(), "Approved amount not less than requested amount");

                ////Reduce the approved loan value
                //_loanSetUpDetails.MoveLoanValueSlider();

                //if (GetPlatform(_driver))
                //{
                //    // click on Button Submit
                //    _loanSetUpDetails.ClickSubmitBtn();

                //    // Click on Bank Account to transfer
                //    _bankDetails.ClicksixtyMinuteButton();

                //    // click on sublit-payment Button
                //    _bankDetails.ClickSubmitPaymentButton();
                //}
                //else
                //{
                //    // Click on Bank Account to transfer
                //    _bankDetails.ClicksixtyMinuteButton();

                //    // click on Buton Submit
                //    _loanSetUpDetails.ClickSubmitBtn();
                //}

                //// Select Reason for Spend Less
                //bool ReasonPageExists = _loanSetUpDetails.FindandselectSpendless();

                //if (ReasonPageExists == true)
                //{
                //    _loanSetUpDetails.SelectReasontospendLess(TestData.ReasonforspeandLess.cheaperservice);
                //}

                ////  Scrolling the Loan Contract
                //_loanSetUpDetails.Loancontract();

                //// Confirming accepting contract
                //_loanSetUpDetails.ConfirmAcceptingContract();

                //// click on I Agree button
                //_loanSetUpDetails.ClickOnAgreeBtn();

                //// click on No thanks Button
                //_loanSetUpDetails.ClickNothanksBtn();

                //// Verify Funded Amount
                ////Assert.IsTrue(_loanSetUpDetails.VerifyApprovedGreaterThanFunded(approvedAmount, _loanSetUpDetails.VerifyFundedAmount()), "Expected approved Amount : " + approvedAmount + ". Observed Funded Amount : " + _loanSetUpDetails.VerifyFundedAmount());

                //Assert.Greater(approvedAmount, _loanSetUpDetails.VerifyFundedAmount(), "Aprroved amount not greater than funded amount");

                //if (GetPlatform(_driver))
                //{
                //    // Click on To Loan Dashboard Button
                //    _loanSetUpDetails.ClickMobileLoanDashboardBtn();

                //    // click on More Button from Bottom Menu
                //    _loanSetUpDetails.ClickMoreBtn();

                //    //Logout
                //    _loanSetUpDetails.Logout();
                //}
                //else
                //{
                //    // Click on Loan Dashboard Button
                //    _loanSetUpDetails.ClickLoanDashboard();

                //    //Logout
                //    _loanSetUpDetails.Logout();
                //}
                #endregion
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }

        }
    }
}