using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest
{
    [TestFixture, Parallelizable, Category("Milestone2"), Category("Setup Page")]
    class TC031_ApplyLoanwithRepaymentDateMiddleNL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();
        
        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }
        
        [TestCase(2000, "android", TestName = "TC031_ApplyLoanwithRepaymentDateMiddle_NL_2000"), Category("NL"), Retry(2)]
        [TestCase(3600, "ios", TestName = "TC031_ApplyLoanwithRepaymentDateMiddle_NL_3600")]
        public void TC031_ApplyLoanwithRepaymentDateMiddle_NL(int loanamout, string strmobiledevice)
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

                //Fill Up all the required bank details and submits the application
                _bankDetails.bankFunctions(TestData.BankDetails.Dagbank, TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.UID, TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.PWD, TestData.IncomeCategory.PrimaryIncome, TestData.Dependents.Zero, TestData.SMSCode, loanamout);

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
                    }
                }

                string RepaymentDateThen = _loanSetUpDetails.GetfirstrepayFrequencyDate();

                _loanSetUpDetails.RepaymentDateMiddle();

                string RepaymentDateNow = _loanSetUpDetails.GetfirstrepayFrequencyDate();

                // Assert.AreNotEqual(RepaymentDateThen, RepaymentDateNow, "Repayment date unchanged even after the slider is moved to middle date");

                double RepaymentAmountNow = _loanSetUpDetails.getRepAmtInTableMiddle();

                _loanSetUpDetails.ClickDetailedRepaymentSchedule();

                string[,] details = _loanSetUpDetails.Getrepaymentdetails();

                double lastrepay = Convert.ToDouble(details[details.GetLength(0) - 1, 1]);

                DateTime startdt = Convert.ToDateTime(RepaymentDateNow);

                if (loanamout > 2000)
                    _loanSetUpDetails.CalcluateSolver(loanamout, startdt, 7, Convert.ToDouble(details[0, 1]), details.GetLength(0), lastrepay);
                else
                    _loanSetUpDetails.CalculateSAAC(loanamout, 7, details);

                // click on Buton Submit
                _loanSetUpDetails.ClickSubmitBtn();

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

            //        //  Scrolling the Loan Contract
            //        _loanSetUpDetails.Loancontract();

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
            #endregion

        }
    }

    [TestFixture, Parallelizable, Category("Milestone2"), Category("Setup Page")]
    class TC031_ApplyLoanwithRepaymentDateMiddleRL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1150, "android", TestName = "TC031_ApplyLoanwithRepaymentDateMiddle_RL_1150"), Category("RL"), Retry(2)]
        [TestCase(2100, "ios", TestName = "TC031_ApplyLoanwithRepaymentDateMiddle_RL_2100")]
        public void TC031_ApplyLoanwithRepaymentDateMiddle_RL(int loanamout, string mobiledevice)
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
                //Assert.IsTrue(notrans, "Notransaction page not appeared");

                // Select Category 
                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "0");

                // Select Just checking option 
                //  _bankDetails.SelectJustCheckingOptionLst("Yes, it will stay the same (or more)");

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
                        _loanSetUpDetails.ClickSetup();

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
                    }
                }

                string RepaymentDateThen = _loanSetUpDetails.GetfirstrepayFrequencyDate();

                _loanSetUpDetails.RepaymentDateMiddle();

                string RepaymentDateNow = _loanSetUpDetails.GetfirstrepayFrequencyDate();

                Assert.AreNotEqual(RepaymentDateThen, RepaymentDateNow, "Repayment date unchanged even after the slider is moved to middle date");

                double RepaymentAmountNow = _loanSetUpDetails.getRepAmtInTableMiddle();

                _loanSetUpDetails.ClickDetailedRepaymentSchedule();

                string[,] details = _loanSetUpDetails.Getrepaymentdetails();

                double lastrepay = Convert.ToDouble(details[details.GetLength(0) - 1, 1]);

                DateTime startdt = Convert.ToDateTime(RepaymentDateNow);

                if (loanamout > 2000)
                    _loanSetUpDetails.CalcluateSolver(loanamout, startdt, 7, Convert.ToDouble(details[0, 1]), details.GetLength(0), lastrepay);
                else
                    _loanSetUpDetails.CalculateSAAC(loanamout, 7, details);

                // click on Buton Submit
                _loanSetUpDetails.ClickSubmitBtn();

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