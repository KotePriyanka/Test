using System;
using Nimble.Automation.Accelerators;
using NUnit.Framework;
using Nimble.Automation.Repository;
using OpenQA.Selenium;

namespace Nimble.Automation.FunctionalTest
{
    [TestFixture, Parallelizable, Category("Milestone5"), Category("Misc")]
    class TC147_Verify_JointAcc : TestEngine
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
        private IWebDriver _driver = null; string strMessage,strUserType=""; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TestCase(1400, "android", TestName = "TC147_Verify_JointAcc_NL_1400"), Category("NL"), Retry(2)]
        [TestCase(2300, "ios", TestName = "TC147_Verify_JointAcc_NL_2300")]
        public void TC147_Verify_JointAcc_NL(int loanamout, string strmobiledevice)
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
                _personalDetails.PersonalDetailsFunction();

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.DSC15.Yodlee.UID, TestData.BankDetails.DSC15.Yodlee.PWD);

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

                //_bankDetails.SelectSevenIncome(TestData.IncomeCategory.PrimaryIncome, TestData.IncomeCategory.PrimaryIncome, TestData.IncomeCategory.OtherEmployment, TestData.IncomeCategory.PartnerSalary, TestData.IncomeCategory.SharedRentUtilities, TestData.IncomeCategory.ChildSupport, TestData.IncomeCategory.InvestmentIncome);
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
                    _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);
                }

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

                        if (loanamout == 1600)
                        {
                            int AcutalApprovedamount = 1000;

                            // Verify ApprovedAmount
                            Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(AcutalApprovedamount), "Expected Requested Amount : " + AcutalApprovedamount + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());
                        }
                        else if (loanamout == 1800)
                        {
                            int AcutalApprovedamount = 1800;

                            // Verify ApprovedAmount
                            Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(AcutalApprovedamount), "Expected Requested Amount : " + AcutalApprovedamount + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());
                        }                       

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

                        if (loanamout == 1600)
                        {
                            int AcutalApprovedamount = 1000;

                            // Verify ApprovedAmount
                            Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(AcutalApprovedamount), "Expected Requested Amount : " + AcutalApprovedamount + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());
                        }
                        else if (loanamout == 1800)
                        {
                            int AcutalApprovedamount = 1800;

                            // Verify ApprovedAmount
                            Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(AcutalApprovedamount), "Expected Requested Amount : " + AcutalApprovedamount + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());
                        }


                        // click on Buton Submit
                        _loanSetUpDetails.ClickSubmitBtn();
                    }
                }
                else
                {
                    if (loanamout == 1600)
                    {
                        int AcutalApprovedamount = 1000;

                        // Verify ApprovedAmount
                        Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(AcutalApprovedamount), "Expected Requested Amount : " + AcutalApprovedamount + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());
                    }
                    else if (loanamout == 1800)
                    {
                        int AcutalApprovedamount = 1800;

                        // Verify ApprovedAmount
                        Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(AcutalApprovedamount), "Expected Requested Amount : " + AcutalApprovedamount + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());
                    }

                    // click on Buton Submit
                    _loanSetUpDetails.ClickSubmitBtn();
                }

                //Select Reason for Spend Less
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
    }

    [TestFixture, Parallelizable, Category("Milestone5"), Category("Misc")]
    class TC147_Verify_JointAcc_RL : TestEngine
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
        public TestEngine _testengine = new TestEngine();

        [TestCase(400, "android", TestName = "TC147_Verify_JointAcc_RL_400"), Category("RL"), Retry(2)]
        [TestCase(4300, "ios", TestName = "TC147_Verify_JointAcc_RL_4300")]
        public void TC_147_Verify_JointAcc_RL(int loanamout, string strmobiledevice)
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

                //Go to the homepage and click the start application button and then the Request money button
                _homeDetails.homeFunctions_RL(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.NewProductAdvancePaidClean);

                //Select the loan amount and purpose and click on continue button
                _loanPurposeDetails.LoanPurposeFunction_RL(loanamout, TestData.POL.Households);

                //Edit the personal details and change the Rmsrv Code
                _personalDetails.PersonalDetailsFunction_RL(TestData.YourEmployementStatus.FullTime, TestData.ReturnerLoaner, TestData.OverrideCodes.PassAll_RL);

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.DSC15.Yodlee.UID, TestData.BankDetails.DSC15.Yodlee.PWD);

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

                //if (loanamout > 2000)
                //{
                //    // enter sms input as OTP                   
                //    _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);
                //}
                if (loanamout == 1600)
                {
                    int AcutalApprovedamount = 1000;

                    // Verify ApprovedAmount
                    Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(AcutalApprovedamount), "Expected Requested Amount : " + AcutalApprovedamount + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());
                }
                else if (loanamout == 1800)
                {
                    int AcutalApprovedamount = 1800;

                    // Verify ApprovedAmount
                    Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(AcutalApprovedamount), "Expected Requested Amount : " + AcutalApprovedamount + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());
                }

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

                //if (GetPlatform(_driver))
                //{
                //    // click on Button Submit
                //    _bankDetails.ClickSubmitBtn();

                //    // Click on Bank Account to transfer
                //    _bankDetails.ClicksixtyMinuteButton();

                //    // click on sublit-payment Button
                //    _bankDetails.ClickSubmitPaymentButton();
                //}
                //else
                //{
                //    // Click on Bank Account to transfer
                //    //_bankDetails.ClicksixtyMinuteButton();

                //    // click on Buton Submit
                //    _bankDetails.ClickSubmitBtn();
                //}

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

                // Verify Funded Amount
                //Assert.IsTrue(_loanSetUpDetails.VerifyFundedAmount(loanamout));

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
