using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimble.Automation.FunctionalTest.Milestone3
{
    //<Summary>
    //Applying the loan to trigger the OOC question and the corresponding user responses_Annual Bonus
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Out of Cycle")]
    class TC045_VerifyOOC_AnnualBonus : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();
        private string email = "";

        [TearDown]
        public void Aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
            email = "";
        }

        [TestCase(850, "Annual bonus", "android", TestName = "TC045_VerifyOOC_AnnualBonus_NL_850"), Category("NL"), Retry(2)]
        [TestCase(2700, "Annual bonus", "android", TestName = "TC045_VerifyOOC_AnnualBonus_NL_2700")]
        public void TC045_VerifyingOOC_AnnualBonus_NL(int loanamout, string oocresponse, string strmobiledevice)
        {
            VerifyOOCQuestionIncome_NL(loanamout, oocresponse, strmobiledevice);
        }

        public void VerifyOOCQuestionIncome_NL(int loanamout, string oocresponse, string strmobiledevice)
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

                email = _personalDetails.EmailID;

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.OOCQuestion.Yodlee.UID, TestData.BankDetails.OOCQuestion.Yodlee.PWD);

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

                //verify the spike question text "We've identified that the following transaction is higher than normal" should be read.
                Assert.IsTrue(_bankDetails.VerifyOOCQuestionText(TestData.OOCText), "Expected OOC Text : " + TestData.OOCText + ". Observed OOC Text : " + TestData.OOCText);

                //Verify if its is OOC question triggered for the right Description
                Assert.AreEqual("Salary Jims Mowing", _bankDetails.GetOOCTransactionDescriptionTxt());

                //Verify if its is OOC question triggered for the right amount
                Assert.AreEqual("$910.00", _bankDetails.GetOOCTransactionAmountTxt());

                // Select Just checking option 
                _bankDetails.SelectReasonforOOCquestion(oocresponse);

                // Enter Other reason for spike income
                if (oocresponse == "Other (we may contact you)")
                {
                    _bankDetails.EnterOtherReasonForOOC(TestData.OtherReason);
                }

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

                if (((loanamout > 2000 && FinalReviewEnabled == "true") && (strUserType == FinalReviewLoanType || FinalReviewLoanType == "ALL")) || (oocresponse == "Other (we may contact you)"))
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

        public void VerifyOOCQuestionIncome_RL(int loanamout, string oocresponse, string strmobiledevice)
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

                email = _homeDetails.RLEmailID;

                //Select the loan amount and purpose and click on continue button
                _loanPurposeDetails.LoanPurposeFunction_RL(loanamout, TestData.POL.Households);

                //Edit the personal details and change the Rmsrv Code
                _personalDetails.PersonalDetailsFunction_RL(TestData.YourEmployementStatus.FullTime, TestData.ReturnerLoaner, TestData.OverrideCodes.PassAll_RL);

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.OOCQuestion.Yodlee.UID, TestData.BankDetails.OOCQuestion.Yodlee.PWD);

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

                //verify the spike question text "We've identified that the following transaction is higher than normal" should be read.
                Assert.IsTrue(_bankDetails.VerifyOOCQuestionText(TestData.OOCText), "Expected OOC Text : " + TestData.OOCText + ". Observed OOC Text : " + TestData.OOCText);

                //Verify if its is OOC question triggered for the right Description
                Assert.AreEqual("Salary Jims Mowing", _bankDetails.GetOOCTransactionDescriptionTxt());

                //Verify if its is OOC question triggered for the right amount
                Assert.AreEqual("$910.00", _bankDetails.GetOOCTransactionAmountTxt());

                // Select Just checking option 
                _bankDetails.SelectReasonforOOCquestion(oocresponse);

                // Enter Other reason for spike income
                if (oocresponse == "Other (we may contact you)")
                {
                    _bankDetails.EnterOtherReasonForOOC(TestData.OtherReason);
                }

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

                if (((loanamout > 2000 && FinalReviewEnabled == "true") && (strUserType == FinalReviewLoanType || FinalReviewLoanType == "ALL")) || (oocresponse == "Other (we may contact you)"))
                {
                    // if (oocresponse == "Other (we may contact you)")
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

                #region code commented
                //if (GetPlatform(_driver))
                //{
                //    //ClickOn Loan Dashboard...Manual Approval
                //    _bankDetails.ClickLoanDashboardManual();

                //    //click on Final Approve
                //    _loanSetUpDetails.FinalApprove();

                //    //Click Setup Button
                //    _bankDetails.ClickSetup();
                //}

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
                #endregion

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

    //<Summary>
    //Applying the loan to trigger the OOC question and the corresponding user responses_Annual Bonus
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Income Out of Cycle")]
    class TC045_VerifyOOC_AnnualBonus_RL : TestUtility
    {
        TC045_VerifyOOC_AnnualBonus _test = new TC045_VerifyOOC_AnnualBonus();

        [TearDown]
        public void Cleanup()
        {
            _test.Aftermethod();
        }

        [TestCase(900, "Annual bonus", "android", TestName = "TC045_VerifyOOC_AnnualBonus_RL_900"), Category("RL"), Retry(2)]
        [TestCase(2000, "Annual bonus", "android", TestName = "TC045_VerifyOOC_AnnualBonus_RL_2000")]
        public void TC045_VerifyingOOC_AnnualBonus_RL(int loanamout, string oocresponse, string strmobiledevice)
        {
            _test.VerifyOOCQuestionIncome_RL(loanamout, oocresponse, strmobiledevice);
        }
    }
}
