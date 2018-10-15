using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest
{
    //<Summary>
    //Applying the loan to trigger the OOC and Spike income in same transcation and verify only one question is triggered for 
    //OOC and the corresponding user responses to other bonus
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike & OOC")]
    class TC058_VerifySpikeAndOOCIncome_Other_NL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage, strUserType = ""; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(750, "android", "Other (we may contact you)", TestName = "TC058_VerifySpikeAndOOCIncome_Other_NL_750"), Category("NL"), Retry(2)]
        [TestCase(3750, "ios", "Other (we may contact you)", TestName = "TC058_VerifySpikeAndOOCIncome_Other_NL_3750")]
        public void TC058_VerifySpikeAndOOCIncom_Other_NL(int loanamout, string strmobiledevice, string strOOCReason)
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

                // entering personal details with overwrite values
                _personalDetails.PopulatePersonalDetails();

                // Click on checks out Continue Button
                _personalDetails.ClickCheckoutContinueBtn();

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.Spike_OOC_SameTrans.Yodlee.UID, TestData.BankDetails.Spike_OOC_SameTrans.Yodlee.PWD);

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
                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "1");

                //Verify Just Checking Option is visisble
                Assert.IsTrue(_bankDetails.VerifyOOCQuestionText("We've identified that the following transaction is out of cycle."), "OOC income question did not triggered");

                //Verify if its is OOC question triggered for the right amount
                Assert.AreEqual("$2,000.00", _bankDetails.GetOOCTransactionAmountTxt());

                //Verify if its is OOC question triggered for the right amount
                //  Assert.AreEqual(_bankDetails.GetTransactionDateFromStatementTxt(6), _bankDetails.GetOOCTransactionDateTxt());

                //Verify if its is OOC question triggered for the right Date
                Assert.AreEqual("Salary ABC holdings", _bankDetails.GetOOCTransactionDescriptionTxt());

                //Select reason
                _bankDetails.SelectReasonforOOCquestion(strOOCReason);

                //Enter Other Reason
                _bankDetails.EnterOtherReasonForOOC(TestData.OtherReason);

                // Select Category 
                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "0");

                // click on Confirm Income Button
                _bankDetails.ClickConfirmIncomeBtn();

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

                if (((loanamout > 2000 && FinalReviewEnabled == "true") && (strUserType == FinalReviewLoanType || FinalReviewLoanType == "ALL")) || (strOOCReason == "Other (we may contact you)"))
                // if (loanamout > 2000 && FinalReviewEnabled == "true" || strOOCReason == "Other (we may contact you)")
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

                #region commented old scenario
                ////hard code wait as the screen flips during transciton
                //Thread.Sleep(5000);

                ////ClickOn Loan Dashboard...Manual Approval
                //_bankDetails.ClickLoanDashboardManual();

                ////click on Final Approve
                //_loanSetUpDetails.FinalApprove();

                ////Click Setup Button
                //_bankDetails.ClickSetup();
                #endregion              
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }

        }
    }


    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Spike & OOC")]
    class TC058_VerifySpikeAndOOCIncome_Other_RL : TestEngine
    {

        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage, strUserType = ""; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(700, "android", "Other (we may contact you)", TestName = "TC058_VerifySpikeAndOOCIncome_Other_RL_700"), Category("RL"), Retry(2)]
        [TestCase(2800, "ios", "Other (we may contact you)", TestName = "TC058_VerifySpikeAndOOCIncome_Other_RL_2800")]
        public void TC058_VerifySpikeAndOOCIncom_Other_RL(int loanamout, string strmobiledevice, string strOOCReason)
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
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.Spike_OOC_SameTrans.Yodlee.UID, TestData.BankDetails.Spike_OOC_SameTrans.Yodlee.PWD);

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
                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "1");

                //Verify Just Checking Option is visisble
                Assert.IsTrue(_bankDetails.VerifyOOCQuestionText("We've identified that the following transaction is out of cycle."), "OOC income question did not triggered");

                //Verify if its is OOC question triggered for the right amount
                Assert.AreEqual("$2,000.00", _bankDetails.GetOOCTransactionAmountTxt());

                //Verify if its is OOC question triggered for the right amount
                //  Assert.AreEqual(_bankDetails.GetTransactionDateFromStatementTxt(6), _bankDetails.GetOOCTransactionDateTxt());

                //Verify if its is OOC question triggered for the right Date
                Assert.AreEqual("Salary ABC holdings", _bankDetails.GetOOCTransactionDescriptionTxt());

                //Select reason
                _bankDetails.SelectReasonforOOCquestion(strOOCReason);

                //Enter Other Reason
                _bankDetails.EnterOtherReasonForOOC(TestData.OtherReason);

                // Select Category 
                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "0");

                // click on Confirm Income Button
                _bankDetails.ClickConfirmIncomeBtn();

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

                //ClickOn Loan Dashboard...Manual Approval
                _bankDetails.ClickLoanDashboardManual();

                //click on Final Approve
                _loanSetUpDetails.FinalApprove();

                //Click Setup Button
                _bankDetails.ClickSetup();

                // Verify ApprovedAmount
                Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout), "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());

                if (GetPlatform(_driver))
                {
                    // click on Button Submit
                    _bankDetails.ClickSubmitBtn();

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
                    _bankDetails.ClickSubmitBtn();
                }

                _loanSetUpDetails.loanSetupFunction_RL(loanamout);
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }

        }
    }

}
