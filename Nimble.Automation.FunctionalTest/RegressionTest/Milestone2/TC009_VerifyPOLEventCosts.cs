﻿using NUnit.Framework;
using Nimble.Automation.Accelerators;
using System;
using Nimble.Automation.Repository;
using OpenQA.Selenium;

namespace Nimble.Automation.FunctionalTest
{
    [TestFixture, Parallelizable, Category("Milestone2"), Category("POL")]
    class TC009_VerifyPOLEventCosts_NL : TestEngine
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


        [TearDown]
        public void Aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(1550, "android", TestName = "TC009_VerifyPOLEventCosts_NL_1550"), Category("NL"), Category("Mobile"), Retry(2)]
        [TestCase(2600, "ios", TestName = "TC009_VerifyPOLEventCosts_NL_2600")]
        public void TC009_VerifyPOLEventCost_NL(int loanamout, string strmobiledevice)
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

                //Go to the homepage and click the start application button
                _homeDetails.HomeDetailsPage();

                //Select the loan amount and purpose and click on continue button
                _loanPurposeDetails.LoanPurposeFunction(loanamout, TestData.POL.Eventcosts.Anniversary);

                //MoreInformation
                _loanPurposeDetails.MoreInformation();
                PersonalDetailsDataObj _per = new PersonalDetailsDataObj();
                _per.StreetName = "At:N Cr:A Id:100 Rr1:A Rr2:A Rr3:A Bsp:Y";

                //entering personal details
                _personalDetails.PopulatePersonalDetails(_per);

                // Click on checks out Continue Button
                _personalDetails.ClickCheckoutContinueBtn();

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

                if (loanamout > 2000)
                {
                    // enter sms input as OTP 
                    if (_bankDetails.VerifySMSOTP())
                    {
                        _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);
                    }
                }

                // verify final review enabled and process setup functionality
                _loanSetUpDetails.loanSetupFunction(loanamout, strUserType);
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }

            #region commented old scenario

            //   // click on Buton Submit
            //   _loanSetUpDetails.ClickSubmitBtn();

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
            //finally
            //{

            //    _driver.Quit();
            //}
            #endregion
        }
    }

    [TestFixture, Parallelizable, Category("Milestone2"), Category("POL")]
    class TC009_VerifyPOLEventCosts_RL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null;
        public string strMessage = "", strUserType;
        DateTime starttime { get; set; } = DateTime.Now;
        ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1550, "android", TestName = "TC009_VerifyPOLEventCosts_RL_1550"), Category("RL"), Category("Mobile"), Retry(2)]
        [TestCase(2600, "ios", TestName = "TC009_VerifyPOLEventCosts_RL_2600")]
        public void TC009_VerifyPOLEventCost_RL(int loanamout, string strmobiledevice)
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
                _loanPurposeDetails.LoanPurposeFunction_RL(loanamout, TestData.POL.Eventcosts.Anniversary);

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

                //// Choose reason for no transactions
                //bool notrans = _bankDetails.NoTransaction(TestData.NoTransactionReasons.Usingcash);
                //Assert.IsTrue(notrans, "No transaction page not appeared");

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

                if (loanamout > 2000)
                {
                    // enter sms input as OTP 
                    if (_bankDetails.VerifySMSOTP())
                    {
                        _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);
                    }
                }

                // Verify ApprovedAmount
                _loanSetUpDetails.VerifySetUpPageDetails(7, loanamout, loanamout, strUserType);

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
















