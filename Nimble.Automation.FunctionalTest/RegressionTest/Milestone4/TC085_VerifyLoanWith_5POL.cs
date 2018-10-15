using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest
{
    //<Summary>
    //Verifying loan application by Applying a Loan With 5 different purpose of Loan
    //</Summary>


    [TestFixture, Parallelizable, Category("Milestone4"), Category("POL")]
    class TC085_VerifyLoanWith_5POL_NL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage,strUserType=""; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(700, "android", TestName = "TC085_VerifyLoanWith_5POL_NL_700"), Category("NL"), Retry(2)]
        [TestCase(2600, "android", TestName = "TC085_VerifyLoanWith_5POL_NL_2600")]
        public void TC085_VerifyLoanwith_5POL_NL(int loanamout, string strmobiledevice)
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

                bool hideshow = _homeDetails.CheckHideShow();
                if (hideshow == true)
                {
                    if (GetPlatform(_driver))
                    {
                        _loanPurposeDetails.RequestLoanAmountWith5POL(loanamout, TestData.POL.Eventcosts.Birthdayparty, TestData.POL.Households, TestData.POL.EducationFees, TestData.POL.Eventcosts.Christmasparty, TestData.POL.Eventcosts.Weddingattire);
                    }
                    else
                    {
                        _loanPurposeDetails.RequestLoanAmountWith5POL(loanamout, TestData.POL.Eventcosts.Birthdayparty, TestData.POL.Households, TestData.POL.EducationFees, TestData.POL.Eventcosts.Christmasparty, TestData.POL.Eventcosts.Weddingattire);
                    }
                }
                else
                {
                    // Select Loan Value from Slide bar
                    _loanPurposeDetails.SelectLoanValue(loanamout);

                    //select 5 pol
                    _loanPurposeDetails.EnterFivePOLRL(TestData.POL.Eventcosts.Birthdayparty, TestData.POL.EducationFees, TestData.POL.MedicalDentalexpenses, TestData.POL.Households, TestData.POL.Homerepairsorimprovements, loanamout);

                    // Click on Continue Button
                    _loanPurposeDetails.ClickLoanPOLContinueBtn();
                }

                // entering personal details with overwrite values
                _personalDetails.PopulatePersonalDetails();

                // Click on checks out Continue Button
                _personalDetails.ClickCheckoutContinueBtn();

                //Fill Up all the required bank details and submits the application
                _bankDetails.bankFunctions(TestData.BankDetails.Dagbank, TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.UID, TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.PWD, TestData.IncomeCategory.PrimaryIncome, TestData.Dependents.Zero, TestData.SMSCode, loanamout);

                // verify final review enabled and process setup functionality
                _loanSetUpDetails.loanSetupFunction(loanamout, strUserType);
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }

            #region commented old scenario
            //// click on Buton Submit
            //_loanSetUpDetails.ClickSubmitBtn();

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
            //catch (Exception ex)
            //{
            //    strMessage += ex.Message; Assert.Fail(ex.Message);
            //}
            #endregion
        }
    }

    [TestFixture, Parallelizable, Category("Milestone4"), Category("POL")]
    class TC085_VerifyLoanWith_5POL_RL : TestEngine
    {

        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage,strUserType=""; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1450, "android", TestName = "TC085_VerifyLoanWith_5POL_RL_1450"), Category("RL"), Retry(2)]
        [TestCase(2900, "android", TestName = "TC085_VerifyLoanWith_5POL_RL_2900")]
        public void TC085_VerifyLoanwith_5POL_RL(int loanamout, string strmobiledevice)
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

                // Select Loan Value from Slide bar
                _loanPurposeDetails.SelectLoanValueRL(loanamout);

                _loanPurposeDetails.EnterFivePOLRL(TestData.POL.Eventcosts.Birthdayparty, TestData.POL.EducationFees, TestData.POL.MedicalDentalexpenses, TestData.POL.Households, TestData.POL.Homerepairsorimprovements, loanamout);

                // Click on Continue Button
                _loanPurposeDetails.ClickLoanPOLContinueBtnRL();

                string streetname = "At:N Cr:A Id:100 Rr1:A Rr2:A Rr3:A Rr:A Rt:8 Rmsrv:0.9999";
                //Edit the personal details and change the Rmsrv Code
                _personalDetails.PersonalDetailsFunction_RL(TestData.YourEmployementStatus.FullTime, TestData.ReturnerLoaner, streetname);

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
