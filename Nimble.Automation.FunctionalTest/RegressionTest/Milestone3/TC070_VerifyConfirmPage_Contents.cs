using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using static NUnit.Framework.Assert;

namespace Nimble.Automation.FunctionalTest.Milestone3
{
    //<Summary>
    //To verify the Financial Table,Payment of Loan,Other provisions content in the "Confirm Page"
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Contract Page")]
    class TC070_VerifyConfirmPage_Contents_NL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; public string strMessage = "", strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();
        private ActionEngine _act = null;
        private ILoanSetupDetails _loansetupdetailsLoc;

        [TearDown]
        public void Aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(1250, "android", TestName = "TC070_VerifyConfirmPage_Contents_NL_1250"), Category("NL"), Category("Mobile"), Retry(2)]
        [TestCase(2700, "android", TestName = "TC070_VerifyConfirmPage_Contents_NL_2700")]
        public void TC070_VerifyingConfirmPage_Contents_NL(int loanamout, string strmobiledevice)
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
                string firstDate = "";
                string lastDate = "";
                double repaymentAmt = 0;
                int repaymentcount = 0;
                string firstLoanAmount = "";
                string lastLoanAmount = "";
                int repaymentcountConfirm;
                bool flag = false;


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
                    if (_bankDetails.VerifySMSOTP())
                        _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);
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
                        _bankDetails.ClickSetup();

                        // Verify ApprovedAmount
                        Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout),
                                "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " +
                                _loanSetUpDetails.GetApprovedamount());

                        _loanSetUpDetails.ClickDetailedRepaymentSchedule();
                        string[,] details = _loanSetUpDetails.Getrepaymentdetails();
                        //Gives first repay amount
                        firstLoanAmount = details[0, 1];

                        //Gives last repay amount
                        int minrepayLength = details.GetLength(0) - 1;
                        lastLoanAmount = details[minrepayLength, 1];

                        int firstdatelength = details.GetLength(0) - 1;
                        firstDate = _loanSetUpDetails.GetFirstDateSetupPage(firstdatelength);
                        lastDate = _loanSetUpDetails.GetLastDateSetupPage();
                        repaymentAmt = _loanSetUpDetails.GetRepaymentAmountSetupPage();
                        repaymentcount = _loanSetUpDetails.GetRepaymentCountSetupPage();

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

                        _loanSetUpDetails.ClickDetailedRepaymentSchedule();
                        string[,] details = _loanSetUpDetails.Getrepaymentdetails();

                        //Gives first repay amount
                        firstLoanAmount = details[0, 1];

                        //Gives last repay amount
                        int minrepayLength = details.GetLength(0) - 1;
                        lastLoanAmount = details[minrepayLength, 1];
                        int firstdatelength = details.GetLength(0) - 1;
                        firstDate = _loanSetUpDetails.GetFirstDateSetupPage(firstdatelength);
                        lastDate = _loanSetUpDetails.GetLastDateSetupPage();
                        repaymentAmt = _loanSetUpDetails.GetRepaymentAmountSetupPage();

                        repaymentcount = _loanSetUpDetails.GetRepaymentCountSetupPage();

                        // click on Buton Submit
                        _loanSetUpDetails.ClickSubmitBtn();
                    }
                }
                else
                {
                    // Verify ApprovedAmount
                    Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout), "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());

                    // Verify ApprovedAmount
                    Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout),
                            "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " +
                            _loanSetUpDetails.GetApprovedamount());

                    _loanSetUpDetails.ClickDetailedRepaymentSchedule();
                    string[,] details = _loanSetUpDetails.Getrepaymentdetails();
                    //Gives first repay amount
                    firstLoanAmount = details[0, 1];

                    //Gives last repay amount
                    int minrepayLength = details.GetLength(0) - 1;
                    lastLoanAmount = details[minrepayLength, 1];

                    int firstdatelength = details.GetLength(0) - 1;
                    firstDate = _loanSetUpDetails.GetFirstDateSetupPage(firstdatelength);
                    lastDate = _loanSetUpDetails.GetLastDateSetupPage();
                    repaymentAmt = _loanSetUpDetails.GetRepaymentAmountSetupPage();
                    repaymentcount = _loanSetUpDetails.GetRepaymentCountSetupPage();

                    // click on Buton Submit
                    _loanSetUpDetails.ClickSubmitBtn();
                }

                //  Scrolling the Loan Contract
                _loanSetUpDetails.Loancontract();

                //verify repayment details

                string firstDateConfirm = _loanSetUpDetails.GetFirstDateConfirmPage();
                string lastDateConfirm = _loanSetUpDetails.GetLastDateConfirmPage();

                double repaymentAmtConfirm = _loanSetUpDetails.GetRepaymentAmountConfirmPage();
                if (firstLoanAmount == lastLoanAmount)
                {
                    flag = true;
                    repaymentcountConfirm = _loanSetUpDetails.GetRepaymentCountConfirmPage(flag, loanamout);
                }
                else
                {
                    repaymentcountConfirm = _loanSetUpDetails.GetRepaymentCountConfirmPage(flag, loanamout);
                }
                //int repaymentcountConfirm = _loanSetUpDetails.GetRepaymentCountConfirmPage();
                if (repaymentAmt == repaymentAmtConfirm)
                { Console.WriteLine(""); }
                else { Console.WriteLine(""); }

                Assert.IsTrue(repaymentAmt == repaymentAmtConfirm, "repayment amount didn't matched");
                Assert.IsTrue(firstDateConfirm.Contains(firstDate), "first date not matched");
                Assert.IsTrue(lastDateConfirm.Contains(lastDate), "last date not matched");
               // Assert.IsTrue(_loanSetUpDetails.getAmtOfCredit(loanamout));
                Assert.IsTrue(_loanSetUpDetails.getDisclosureDate(), "DISCLOSURE DATE MATCHED");
                Assert.IsTrue(repaymentcount == repaymentcountConfirm, "repayment count unmatched");

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
            //        // Verify ApprovedAmount
            //        Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout),
            //                "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " +
            //                _loanSetUpDetails.GetApprovedamount());

            //        _loanSetUpDetails.ClickDetailedRepaymentSchedule();
            //        string firstDate = _loanSetUpDetails.GetFirstDateSetupPage();
            //        string lastDate = _loanSetUpDetails.GetLastDateSetupPage();
            //        double repaymentAmt = _loanSetUpDetails.GetRepaymentAmountSetupPage();
            //        int repaymentcount = _loanSetUpDetails.GetRepaymentCountSetupPage();


            //        // click on Buton Submit
            //        _loanSetUpDetails.ClickSubmitBtn();

            //        _loanSetUpDetails.Loancontract();

            //        string firstDateConfirm = _loanSetUpDetails.GetFirstDateConfirmPage();
            //        string lastDateConfirm = _loanSetUpDetails.GetLastDateConfirmPage();
            //        int repaymentAmtConfirm = _loanSetUpDetails.GetRepaymentAmountConfirmPage();
            //        int repaymentcountConfirm = _loanSetUpDetails.GetRepaymentCountConfirmPage();
            //        if (repaymentAmt == repaymentAmtConfirm)
            //        { Console.WriteLine(""); }
            //        else { Console.WriteLine(""); }

            //        Assert.IsTrue(repaymentAmt == repaymentAmtConfirm, "repayment amount didn't matched");
            //        Assert.IsTrue(firstDateConfirm.Contains(firstDate), "first date not matched");
            //        Assert.IsTrue(lastDateConfirm.Contains(lastDate), "last date not matched");
            //        Assert.IsTrue(_loanSetUpDetails.getAmtOfCredit(loanamout));
            //        Assert.IsTrue(_loanSetUpDetails.getDisclosureDate(), "DISCLOSURE DATE MATCHED");
            //        Assert.IsTrue(repaymentcount == repaymentcountConfirm, "repayment count unmatched");


            //        // Confirming accepting contract
            //        _loanSetUpDetails.ConfirmAcceptingContract();

            //        // click on I Agree button
            //        _loanSetUpDetails.ClickOnAgreeBtn();

            //        // click on No thanks Button
            //        _loanSetUpDetails.ClickNothanksBtn();

            //        if (GetPlatform(_driver))
            //        {
            //            // Click on To Loan Dashboard Button
            //            _loanSetUpDetails.ClickMobileLoanDashboardBtn();

            //            // click on More Button from Bottom Menu
            //            _loanSetUpDetails.ClickMoreBtn();

            //            //Logout
            //            _loanSetUpDetails.Logout();
            //        }
            //        else
            //        {
            //            // Click on Loan Dashboard Button
            //            _loanSetUpDetails.ClickLoanDashboard();

            //            //Logout
            //            _loanSetUpDetails.Logout();
            //        }
            //    }
            //        catch (Exception ex)
            //        {
            //            Fail(ex.Message);
            //}
            #endregion
        }
    }

    //<Summary>
    //To verify the Financial Table,Payment of Loan,Other provisions content in the "Confirm Page"
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Contract Page")]
    class TC070_VerifyConfirmPage_Contents_RL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null;
        public string strMessage = "", strUserType;
        DateTime starttime { get; set; } = DateTime.Now;
        ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }


        [TestCase(1400, "android", TestName = "TC070_VerifyConfirmPage_Contents_RL_1400"), Category("RL"), Retry(2)]
        [TestCase(2700, "android", TestName = "TC070_VerifyConfirmPage_Contents_RL_2700")]

        public void TC070_VerifyingConfirmPage_Contents_RL(int loanamout, string strmobiledevice)
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
                string firstDate = "";
                string lastDate = "";
                double repaymentAmt = 0;
                int repaymentcount = 0;
                string firstLoanAmount = "";
                string lastLoanAmount = "";
                int repaymentcountConfirm;
                bool flag = false;

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
                        _bankDetails.ClickSetup();

                        // Verify ApprovedAmount
                        Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout),
                                "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " +
                                _loanSetUpDetails.GetApprovedamount());

                        _loanSetUpDetails.ClickDetailedRepaymentSchedule();
                        string[,] details = _loanSetUpDetails.Getrepaymentdetails();
                        //Gives first repay amount
                        firstLoanAmount = details[0, 1];

                        //Gives last repay amount
                        int minrepayLength = details.GetLength(0) - 1;
                        lastLoanAmount = details[minrepayLength, 1];

                        int firstdatelength = details.GetLength(0) - 1;
                        firstDate = _loanSetUpDetails.GetFirstDateSetupPage(firstdatelength);
                        lastDate = _loanSetUpDetails.GetLastDateSetupPage();
                        repaymentAmt = _loanSetUpDetails.GetRepaymentAmountSetupPage();
                        repaymentcount = _loanSetUpDetails.GetRepaymentCountSetupPage();

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

                        //click on Final Approve
                        _loanSetUpDetails.FinalApprove();

                        //Click Setup Button
                        _bankDetails.ClickSetup();

                        // Verify ApprovedAmount
                        Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout),
                                "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " +
                                _loanSetUpDetails.GetApprovedamount());

                        _loanSetUpDetails.ClickDetailedRepaymentSchedule();
                        string[,] details = _loanSetUpDetails.Getrepaymentdetails();

                        //Gives first repay amount
                        firstLoanAmount = details[0, 1];

                        //Gives last repay amount
                        int minrepayLength = details.GetLength(0) - 1;
                        lastLoanAmount = details[minrepayLength, 1];
                        int firstdatelength = details.GetLength(0) - 1;
                        firstDate = _loanSetUpDetails.GetFirstDateSetupPage(firstdatelength);
                        lastDate = _loanSetUpDetails.GetLastDateSetupPage();
                        repaymentAmt = _loanSetUpDetails.GetRepaymentAmountSetupPage();

                        repaymentcount = _loanSetUpDetails.GetRepaymentCountSetupPage();

                        // click on Buton Submit
                        _loanSetUpDetails.ClickSubmitBtn();
                    }
                }
                else
                {
                    // Verify ApprovedAmount
                    Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout), "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());

                    // Verify ApprovedAmount
                    Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout),
                            "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " +
                            _loanSetUpDetails.GetApprovedamount());

                    _loanSetUpDetails.ClickDetailedRepaymentSchedule();
                    string[,] details = _loanSetUpDetails.Getrepaymentdetails();
                    //Gives first repay amount
                    firstLoanAmount = details[0, 1];

                    //Gives last repay amount
                    int minrepayLength = details.GetLength(0) - 1;
                    lastLoanAmount = details[minrepayLength, 1];

                    int firstdatelength = details.GetLength(0) - 1;
                    firstDate = _loanSetUpDetails.GetFirstDateSetupPage(firstdatelength);
                    lastDate = _loanSetUpDetails.GetLastDateSetupPage();
                    repaymentAmt = _loanSetUpDetails.GetRepaymentAmountSetupPage();
                    repaymentcount = _loanSetUpDetails.GetRepaymentCountSetupPage();

                    // click on Buton Submit
                    _loanSetUpDetails.ClickSubmitBtn();
                }

                //  Scrolling the Loan Contract
                _loanSetUpDetails.Loancontract();

                //verify repayment details

                string firstDateConfirm = _loanSetUpDetails.GetFirstDateConfirmPage();
                string lastDateConfirm = _loanSetUpDetails.GetLastDateConfirmPage();

                double repaymentAmtConfirm = _loanSetUpDetails.GetRepaymentAmountConfirmPage();
                if (firstLoanAmount == lastLoanAmount)
                {
                    flag = true;
                    repaymentcountConfirm = _loanSetUpDetails.GetRepaymentCountConfirmPage(flag, loanamout);
                }
                else
                {
                    repaymentcountConfirm = _loanSetUpDetails.GetRepaymentCountConfirmPage(flag, loanamout);
                }
                //int repaymentcountConfirm = _loanSetUpDetails.GetRepaymentCountConfirmPage();
                if (repaymentAmt == repaymentAmtConfirm)
                { Console.WriteLine(""); }
                else { Console.WriteLine(""); }

                Assert.IsTrue(repaymentAmt == repaymentAmtConfirm, "repayment amount didn't matched");
                Assert.IsTrue(firstDateConfirm.Contains(firstDate), "first date not matched");
                Assert.IsTrue(lastDateConfirm.Contains(lastDate), "last date not matched");
                //Assert.IsTrue(_loanSetUpDetails.getAmtOfCredit(loanamout));
                Assert.IsTrue(_loanSetUpDetails.getDisclosureDate(), "DISCLOSURE DATE MATCHED");
                Assert.IsTrue(repaymentcount == repaymentcountConfirm, "repayment count unmatched");

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
                Fail(ex.Message);
            }

        }
    }
}
