using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimble.Automation.FunctionalTest
{
    //<Summary>
    //To verify the users ability to complete the "Confirm Page" without selecting any of the radio buttons
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Contract Page")]
    class TC071_VerifyConfirmPage_ContinueWOSelection_NL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);

        }
        [TestCase(1450, "android", TestName = "TC071_VerifyConfirmPage_NL_1450"), Category("NL"), Retry(2)]
        [TestCase(3900, "android", TestName = "TC071_VerifyConfirmPage_NL_3900")]
        public void TC071_VerifyConfirmPage_NL(int loanamout, string strmobiledevice)
        {
            strUserType = "NL";  
            try
            {
                _driver = _testengine.TestSetup(strmobiledevice, "NL");
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
                    _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);
                }

                // if (loanamout > 2000 && FinalReviewEnabled == "true")
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
                        _bankDetails.ClickSetup();

                        // Verify ApprovedAmount
                        Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout),
                            "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " +
                            _loanSetUpDetails.GetApprovedamount());

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
                        Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout),
                            "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " +
                            _loanSetUpDetails.GetApprovedamount());

                        // click on Buton Submit
                        _loanSetUpDetails.ClickSubmitBtn();
                    }
                }
                else
                {
                    // Verify ApprovedAmount
                    Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout),
                        "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " +
                        _loanSetUpDetails.GetApprovedamount());

                    // click on Buton Submit
                    _loanSetUpDetails.ClickSubmitBtn();
                }

                //  Scrolling the Loan Contract
                _loanSetUpDetails.Loancontract();

                // Confirming accepting contract and purpose
                _loanSetUpDetails.PartialConfirmAcceptingContractwithcontractandpurpose();

                // verify I agree Button not displayed              
                Assert.IsTrue(!_loanSetUpDetails.VerifyAgreeBtnDisplay(),
                    "Expected  : " + "I Agree Buttton Not displaying" + ". Observed : " +
                    "I Agree Buttton Not displaying");

                // Confirming accepting contract and repay
                _loanSetUpDetails.PartialConfirmAcceptingContractwithcontractandrepay();

                // verify I agree Button not displayed              
                Assert.IsTrue(!_loanSetUpDetails.VerifyAgreeBtnDisplay(),
                    "Expected  : " + "I Agree Buttton Not displaying" + ". Observed : " +
                    "I Agree Buttton Not displaying");

                // Confirming accepting purpose and repay
                _loanSetUpDetails.PartialConfirmAcceptingContractwithpurposeandrepay();

                // verify I agree Button not displayed              
                Assert.IsTrue(!_loanSetUpDetails.VerifyAgreeBtnDisplay(),
                    "Expected  : " + "I Agree Buttton Not displaying" + ". Observed : " +
                    "I Agree Buttton Not displaying");

                // Confirming complete acceptance
                _loanSetUpDetails.CompleteConfirmAcceptingContract();

                // verify I agree Button displayed              
                Assert.IsTrue(_loanSetUpDetails.VerifyAgreeBtnDisplay(),
                    "Expected  : " + "I Agree Buttton displaying" + ". Observed : " +
                    "I Agree Buttton displaying");

                // Unconfirm one of the acceptance
                _loanSetUpDetails.UnConfirmAcceptingLoanContract();

                // verify I agree Button displayed              
                Assert.IsTrue(!_loanSetUpDetails.VerifyAgreeBtnDisplay(),
                    "Expected  : " + "I Agree Buttton Not displaying" + ". Observed : " +
                    "I Agree Buttton Not displaying");

                // Confirming complete acceptance
                _loanSetUpDetails.CompleteConfirmAcceptingContract();

                // click on I Agree button
                _loanSetUpDetails.ClickOnAgreeBtn();

                if (GetPlatform(_driver))
                {
                    // Click on To Loan Dashboard Button
                    _loanSetUpDetails.ClickMobileLoanDashboardBtn();

                    // click on More Button from Bottom Menu
                    _loanSetUpDetails.ClickMoreBtn();

                    //Logout
                    _loanSetUpDetails.Logout();

                    strMessage += string.Format("\r\n\t Ends");
                }
                else
                {
                    // Click on Loan Dashboard Button
                    _loanSetUpDetails.ClickLoanDashboard();

                    //Logout
                    _loanSetUpDetails.Logout();
                    strMessage += string.Format("\r\n\t Ends");
                }

                #region "Commented code as there is change in functionality"
                //// verify warning unconfirmed acceptance message
                //string unconfirmcontractmsg = _loanSetUpDetails.getunconfirmedcontractmsg();
                //string actualunconfirmcontractmsg = "You must accept the terms and conditions to proceed.";
                //Assert.AreEqual(unconfirmcontractmsg, actualunconfirmcontractmsg, "unconfirm contract message matched");

                //// Unconfirm one of the acceptance
                //_loanSetUpDetails.UnConfirmAcceptingpurpose();

                //// verify I agree Button displayed              
                //Assert.IsTrue(!_loanSetUpDetails.VerifyAgreeBtnDisplay(),
                //    "Expected  : " + "I Agree Buttton Not displaying" + ". Observed : " +
                //    "I Agree Buttton Not displaying");

                //// click on I Agree button
                //_loanSetUpDetails.ClickOnAgreeBtn();

                //// verify warning unconfirmed acceptance message
                //string unconfirmpurposemsg = _loanSetUpDetails.getunconfirmedpurposemsg();
                //string actualunconfirmpurposemsg = "Please confirm this loan amount meets your selected loan purpose(s)";
                //Assert.AreEqual(unconfirmpurposemsg, actualunconfirmpurposemsg, "unconfirm purpose message matched");

                //// Unconfirm one of the acceptance
                //_loanSetUpDetails.UnConfirmAcceptingrepay();

                //// verify I agree Button displayed              
                //Assert.IsTrue(_loanSetUpDetails.VerifyAgreeBtnDisplay(),
                //    "Expected  : " + "I Agree Buttton displaying" + ". Observed : " +
                //    "I Agree Buttton displaying");

                //// click on I Agree button
                //_loanSetUpDetails.ClickOnAgreeBtn();

                //// verify warning unconfirmed acceptance message
                //string unconfirmrepaymsg = _loanSetUpDetails.getunconfirmedrepaymsg();
                //string actualunconfirmrepaymsg = "You must confirm that you are able to repay this loan.";
                //Assert.AreEqual(unconfirmrepaymsg, actualunconfirmrepaymsg, "unconfirm repay message matched");
                #endregion

            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }

        }
    }

    //<Summary>
    //To verify the users ability to complete the "Confirm Page" without selecting any of the radio buttons
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Contract Page")]
    class TC071_VerifyConfirmPage_ContinueWOSelection_RL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1000, "android", TestName = "TC071_VerifyConfirmPage_RL_1000"), Category("RL"), Retry(2)]
        [TestCase(2400, "android", TestName = "TC071_VerifyConfirmPage_RL_2400")]
        public void TC071_VerifyConfirmPage_RL(int loanamout, string strmobiledevice)
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
                _homeDetails.homeFunctions_RL(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.ReturnerDagBankstaging);

                //Select the loan amount and purpose and click on continue button
                _loanPurposeDetails.LoanPurposeFunction_RL(loanamout, TestData.POL.Households);

                //Edit the personal details and change the Rmsrv Code
                _personalDetails.PersonalDetailsFunction_Skipbanklogin(TestData.YourEmployementStatus.FullTime, TestData.ReturnerLoaner, TestData.OverrideCodes.PassAll_RL);

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
                //Assert.IsTrue(notrans, "No transaction page not appeared");

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

                // Set Up Loan page
                if ((loanamout > 2000 && FinalReviewEnabled == "true") && (strUserType == FinalReviewLoanType || FinalReviewLoanType == "ALL"))
                {
                    if (GetPlatform(_driver))
                    {
                        if (loanamout > 2000)
                        {
                            // enter sms input as OTP 
                            if (_bankDetails.VerifySMSOTP())
                            {
                                _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);
                            }
                        }
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
                        if (loanamout > 2000)
                        {
                            // enter sms input as OTP 
                            if (_bankDetails.VerifySMSOTP())
                            {
                                _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);
                            }
                        }
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

                //  Scrolling the Loan Contract
                _loanSetUpDetails.Loancontract();

                // Confirming accepting contract and purpose
                _loanSetUpDetails.PartialConfirmAcceptingContractwithcontractandpurpose();

                // verify I agree Button not displayed              
                Assert.IsTrue(!_loanSetUpDetails.VerifyAgreeBtnDisplay(),
                    "Expected  : " + "I Agree Buttton Not displaying" + ". Observed : " +
                    "I Agree Buttton Not displaying");

                // Confirming accepting contract and repay
                _loanSetUpDetails.PartialConfirmAcceptingContractwithcontractandrepay();

                // verify I agree Button not displayed              
                Assert.IsTrue(!_loanSetUpDetails.VerifyAgreeBtnDisplay(),
                    "Expected  : " + "I Agree Buttton Not displaying" + ". Observed : " +
                    "I Agree Buttton Not displaying");

                // Confirming accepting purpose and repay
                _loanSetUpDetails.PartialConfirmAcceptingContractwithpurposeandrepay();

                // verify I agree Button not displayed              
                Assert.IsTrue(!_loanSetUpDetails.VerifyAgreeBtnDisplay(),
                    "Expected  : " + "I Agree Buttton Not displaying" + ". Observed : " +
                    "I Agree Buttton Not displaying");

                // Confirming complete acceptance
                _loanSetUpDetails.CompleteConfirmAcceptingContract();

                // verify I agree Button displayed              
                Assert.IsTrue(_loanSetUpDetails.VerifyAgreeBtnDisplay(),
                    "Expected  : " + "I Agree Buttton displaying" + ". Observed : " +
                    "I Agree Buttton displaying");

                // Unconfirm one of the acceptance
                _loanSetUpDetails.UnConfirmAcceptingLoanContract();

                // verify I agree Button displayed              
                Assert.IsTrue(!_loanSetUpDetails.VerifyAgreeBtnDisplay(),
                    "Expected  : " + "I Agree Buttton displaying" + ". Observed : " +
                    "I Agree Buttton displaying");

                // Confirming complete acceptance
                _loanSetUpDetails.CompleteConfirmAcceptingContract();

                // click on I Agree button
                _loanSetUpDetails.ClickOnAgreeBtn();

                if (GetPlatform(_driver))
                {
                    // Click on To Loan Dashboard Button
                    _loanSetUpDetails.ClickMobileLoanDashboardBtn();

                    // click on More Button from Bottom Menu
                    _loanSetUpDetails.ClickMoreBtn();

                    //Logout
                    _loanSetUpDetails.Logout();

                    strMessage += string.Format("\r\n\t Ends");
                }
                else
                {
                    // Click on Loan Dashboard Button
                    _loanSetUpDetails.ClickLoanDashboard();

                    //Logout
                    _loanSetUpDetails.Logout();
                    strMessage += string.Format("\r\n\t Ends");
                }

                #region "Commented as there is change in functionality"
                //// verify warning unconfirmed acceptance message
                //string unconfirmcontractmsg = _loanSetUpDetails.getunconfirmedcontractmsg();
                //string actualunconfirmcontractmsg = "You must accept the terms and conditions to proceed.";
                //Assert.AreEqual(unconfirmcontractmsg, actualunconfirmcontractmsg, "unconfirm contract message matched");

                //// Unconfirm one of the acceptance
                //_loanSetUpDetails.UnConfirmAcceptingpurpose();

                //// verify I agree Button displayed              
                //Assert.IsTrue(_loanSetUpDetails.VerifyAgreeBtnDisplay(),
                //    "Expected  : " + "I Agree Buttton displaying" + ". Observed : " +
                //    "I Agree Buttton displaying");

                //// click on I Agree button
                //_loanSetUpDetails.ClickOnAgreeBtn();

                //// verify warning unconfirmed acceptance message
                //string unconfirmpurposemsg = _loanSetUpDetails.getunconfirmedpurposemsg();
                //string actualunconfirmpurposemsg = "Please confirm this loan amount meets your selected loan purpose(s)";
                //Assert.AreEqual(unconfirmpurposemsg, actualunconfirmpurposemsg, "unconfirm purpose message matched");

                //// Unconfirm one of the acceptance
                //_loanSetUpDetails.UnConfirmAcceptingrepay();

                //// verify I agree Button displayed              
                //Assert.IsTrue(_loanSetUpDetails.VerifyAgreeBtnDisplay(),
                //    "Expected  : " + "I Agree Buttton displaying" + ". Observed : " +
                //    "I Agree Buttton displaying");

                //// click on I Agree button
                //_loanSetUpDetails.ClickOnAgreeBtn();

                //// verify warning unconfirmed acceptance message
                //string unconfirmrepaymsg = _loanSetUpDetails.getunconfirmedrepaymsg();
                //string actualunconfirmrepaymsg = "You must confirm that you are able to repay this loan.";
                //Assert.AreEqual(unconfirmrepaymsg, actualunconfirmrepaymsg, "unconfirm repay message matched");
                #endregion
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }

        }
    }
}
