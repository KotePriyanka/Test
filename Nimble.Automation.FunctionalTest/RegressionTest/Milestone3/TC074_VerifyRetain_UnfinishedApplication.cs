using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest
{
    //<Summary>
    //Verifying the user is able tp retain his unfinished application at income verify.
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Unfinished Application")]
    class TC074_VerifyRetain_UnfinishedApplication_NL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();
        private string emal = "";

        [TearDown]
        public void Aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, emal, starttime);
        }

        [TestCase(400, "chrome", TestName = "TC074_VerifyRetain_UnfinishedApplication_NL_SACC_400"), Category("NL"), Retry(2)]
        [TestCase(2700, "chrome", TestName = "TC074_VerifyRetain_UnfinishedApplication_NL_MACC_2700")]
        public void TC074_Verifyretain_UnfinishedApplication_NL(int loanamout, string strmobiledevice)
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
                PersonalDetailsDataObj _obj = _personalDetails.PersonalDetailsFunction();

                emal = _personalDetails.EmailID;
                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();
                //----------------------------------------------------------------------------
                _driver.Quit();

                _driver = _testengine.TestSetup(strmobiledevice);
                _homeDetails = new HomeDetails(_driver, "NL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "NL");
                _personalDetails = new PersonalDetails(_driver, "NL");
                _bankDetails = new BankDetails(_driver, "NL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "NL");

                _homeDetails.ClickLoginBtn(); //TODO

                _homeDetails.ReLoginUser(_obj.Email, "password");

                //Verify Verify Button enabled on dasboard screen
                Assert.IsTrue(_loanSetUpDetails.VerifyVerifyBtn(), "Verify button is not visible"); //TODO

                _loanSetUpDetails.ClickVerifyBtn();
                //----------------------------------------------------------------------------

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

    [TestFixture, Parallelizable, Category("Milestone3"), Category("Unfinished Application")]
    class TC074_VerifyRetain_UnfinishedApplication_RL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private string email = "";
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, email, starttime);
        }

        [TestCase(1000, "chrome", TestName = "TC074_VerifyRetain_UnfinishedApplication_RL_SACC_1000"), Category("RL"), Retry(2)]
        [TestCase(2600, "chrome", TestName = "TC074_VerifyRetain_UnfinishedApplication_RL_MACC_2600")]
        public void TC074_Verifyretain_UnfinishedApplication_RL(int loanamout, string strmobiledevice)
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
                string strEmailId = string.Empty;

                // Login with existing user
                _homeDetails.LoginExistingUser(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.ReturnerDagBankstaging);

                email = _homeDetails.RLEmailID;

                // Click on Request Money Button
                _homeDetails.ClickRequestMoneyBtn();

                //Click on Start Application Button
                _homeDetails.ClickExistinguserStartApplictionBtn();

                //Select the loan amount and purpose and click on continue button
                _loanPurposeDetails.LoanPurposeFunction_RL(loanamout, TestData.POL.Households);

                string streetname = "At:N Cr:A Id:100 Rr1:A Rr2:A Rr3:A Rr:A Rt:8 Rmsrv:0.9999";
                //Edit the personal details and change the Rmsrv Code
                strEmailId = _personalDetails.PersonalDetailsFunction_Skipbanklogin(TestData.YourEmployementStatus.FullTime, TestData.ReturnerLoaner, streetname);

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

                //----------------------------------------------------------------------------
                //LogOut
                _driver.Quit();

                _driver = _testengine.TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
                _personalDetails = new PersonalDetails(_driver, "RL");
                _bankDetails = new BankDetails(_driver, "RL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

                _homeDetails.ClickLoginBtn();

                _homeDetails.LoginLogoutUser(strEmailId, "password");

                //Verify Verify Button enabled on dasboard screen
                Assert.IsTrue(_loanSetUpDetails.VerifyVerifyBtn(), "Verify button is not visible");

                // click on verify button
                _loanSetUpDetails.ClickVerifyBtn();

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

        }
    }

}