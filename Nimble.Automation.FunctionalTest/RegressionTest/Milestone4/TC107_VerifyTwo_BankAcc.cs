using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest.Milestone4
{
    [TestFixture, Parallelizable, Category("Milestone4"), Category("Misc")]
    class TC107_VerifyTwo_BankAcc_NL : TestEngine
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

        [TestCase(1500, "android", TestName = "TC107_VerifyTwo_BankAcc_NL_1500"), Category("NL"), Retry(2)]
        [TestCase(3500, "ios", TestName = "TC107_VerifyTwo_BankAcc_NL_3500")]
        public void TC107_VerifyingTwo_BankAcc_NL(int loanamout, string strmobiledevice)
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

                string justcheckingoption = "Yes, it will stay the same (or more)";

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

                //_personalDetails.PopulatePersonalDetails();
                _personalDetails.PersonalDetailsFunction();

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
               // _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.UID, TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.PWD);

                // Click on Continue Button
               //_bankDetails.ClickAutoContinueBtn();

                //Click on Add Another BAnk Button
                //_bankDetails.ClickAddAnotherBankBtn();

                // select Bank Name  
                //_bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.TwoBankAccounts.Yodlee.UID, TestData.BankDetails.TwoBankAccounts.Yodlee.PWD);

                // Click on Continue Button
                _bankDetails.ClickAutoContinueBtn();

                // choose first bank account
                _bankDetails.BankAccountselectforfirstBS();

                // choose second bank account
                //_bankDetails.BankAccountselectforsecondBS();

                // Click on bank select Continue Button
                _bankDetails.ClickBankAccountContBtn();

                // Confirm Bank Details
                _bankDetails.EnterBankDetailsTxt();

                // Click on Confirm account details Continue Button  
                _bankDetails.ClickAcctDetailsBtn();

                // Select Category 
                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "1");

                // Select Just checking option for first income category
                _bankDetails.SelectJustCheckingOption(justcheckingoption, "1");

                // Click view transactions link
                _bankDetails.ClickViewTransactionsBtn();

                // validate merged 2 bank statements description
                Assert.IsTrue(_bankDetails.ValidateTransactionDesc());

                // validate merged 2 bank statements amount
                int totaltransactionsamt = _bankDetails.ValidateTransactionAmount();

                // Select second Income Category as PrimaryIncome
                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "0");

                // Select Just checking option for first income category
                _bankDetails.SelectJustCheckingOption(justcheckingoption, "0");

                // Enter Other reason for two bank income
                if (justcheckingoption == "Other (we may contact you)")
                {
                    _bankDetails.EnterOtherReasonForSpike(TestData.OtherReason);
                }

                // click on Confirm Income Button
                _bankDetails.ClickConfirmIncomeBtn();

                // select  other debt repayments option No 
                _bankDetails.SelectOtherDebtRepaymentsOptionBtn();

                // select dependents 
                _bankDetails.SelectDependantsLst(TestData.Dependents.Zero);

                // Click on continue
                _bankDetails.ClickConfirmExpensesBtn();

                //Assert the expenses
                int expensesamount = _bankDetails.ExpensesAmount();
                int actualexpensesamount = 777; //726
                int actualexpensesamount2 = 718; //depending on time of year, sometimes Everyday Living Expenses value is 718, so need to check for either
                Assert.IsTrue(expensesamount == actualexpensesamount || expensesamount == actualexpensesamount2);

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

                if(((loanamout > 2000 && FinalReviewEnabled == "true") && (strUserType == FinalReviewLoanType || FinalReviewLoanType == "ALL")) || (justcheckingoption == "Other (we may contact you)"))
                //if (loanamout > 2000 || FinalReviewEnabled == "true" || justcheckingoption == "Other (we may contact you)")
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

    [TestFixture, Parallelizable, Category("Milestone4"), Category("Misc")]
    class TC107_VerifyTwo_BankAcc_RL : TestEngine
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
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1250, "android", TestName = "TC107_VerifyTwo_BankAcc_RL_1250"), Category("RL"), Retry(2)]
        [TestCase(2550, "ios", TestName = "TC107_VerifyTwo_BankAcc_RL_2550")]
        public void TC107_VerifyingTwo_BankAcc_RL(int loanamout, string strmobiledevice)
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

                string justcheckingoption = "Yes, it will stay the same (or more)";

                //Go to the homepage and click the start application button and then the Request money button
                _homeDetails.homeFunctions_RL(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.NewProductAdvancePaidClean);

                //Select the loan amount and purpose and click on continue button
                _loanPurposeDetails.LoanPurposeFunction_RL(loanamout, TestData.POL.Households);

                string streetname = "At:N Cr:A Id:100 Rr1:A Rr2:A Rr3:A Rr:A Rt:8 Rmsrv:0.9999 Bsp:Y";
                //Edit the personal details and change the Rmsrv Code
                _personalDetails.PersonalDetailsFunction_RL(TestData.YourEmployementStatus.FullTime, TestData.ReturnerLoaner, streetname);

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
               // _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.UID, TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.PWD);

                // Click on Continue Button
               // _bankDetails.ClickAutoContinueBtn();

                //Click on Add Another BAnk Button
               // _bankDetails.ClickAddAnotherBankBtn();

                // select Bank Name  
               // _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.TwoBankAccounts.Yodlee.UID, TestData.BankDetails.TwoBankAccounts.Yodlee.PWD);

                // Click on Continue Button
                _bankDetails.ClickAutoContinueBtn();

                // choose first bank account
                _bankDetails.BankAccountselectforfirstBS();

                // choose second bank account
                //_bankDetails.BankAccountselectforsecondBS();

                // Click on bank select Continue Button
                _bankDetails.ClickBankAccountContBtn();

                // Confirm Bank Details
                _bankDetails.EnterBankDetailsTxt();

                // Click on Confirm account details Continue Button  
                _bankDetails.ClickAcctDetailsBtn();

                // Select Category 
                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "1");

                // Select Just checking option for first income category
                _bankDetails.SelectJustCheckingOption(justcheckingoption, "1");

                // Click view transactions link
                _bankDetails.ClickViewTransactionsBtn();

                // validate merged 2 bank statements description
                Assert.IsTrue(_bankDetails.ValidateTransactionDesc());

                // validate merged 2 bank statements amount
                int totaltransactionsamt = _bankDetails.ValidateTransactionAmount();

                // Select second Income Category as PrimaryIncome
                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "0");

                // Select Just checking option for first income category
                _bankDetails.SelectJustCheckingOption(justcheckingoption, "0");

                // Enter Other reason for two bank income
                if (justcheckingoption == "Other (we may contact you)")
                {
                    _bankDetails.EnterOtherReasonForSpike(TestData.OtherReason);
                }

                // click on Confirm Income Button
                _bankDetails.ClickConfirmIncomeBtn();

                // select  other debt repayments option No 
                _bankDetails.SelectOtherDebtRepaymentsOptionBtn();

                // select dependents 
                _bankDetails.SelectDependantsLst(TestData.Dependents.Zero);

                // Click on continue
                _bankDetails.ClickConfirmExpensesBtn();

                //Assert the expenses
                int expensesamount = _bankDetails.ExpensesAmount();
                int actualexpensesamount = 777;// 726;
                int actualexpensesamount2 = 718; //depending on time of year, sometimes Everyday Living Expenses value is 718, so need to check for either
                Assert.IsTrue(expensesamount == actualexpensesamount || expensesamount == actualexpensesamount2);

                // select Governments benefits option No
                _bankDetails.ClickNoGovtBenefitsbtn();

                // click on Agree that information True
                _bankDetails.ClickAgreeAppSubmitBtn();

                // click on confirm Submit button
                _bankDetails.ClickConfirmSummaryBtn();

                if(((loanamout > 2000 && FinalReviewEnabled == "true") && (strUserType == FinalReviewLoanType || FinalReviewLoanType == "ALL")) || (justcheckingoption == "Other (we may contact you)"))
               // if (justcheckingoption == "Other (we may contact you)")
                {
                    //ClickOn Loan Dashboard...Manual Approval
                    _bankDetails.ClickLoanDashboardManual();

                    //click on Final Approve
                    _loanSetUpDetails.FinalApprove();

                    //Click Setup Button
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
}
