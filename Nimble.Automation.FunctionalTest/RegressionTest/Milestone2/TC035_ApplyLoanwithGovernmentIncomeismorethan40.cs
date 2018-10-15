using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest
{
    [TestFixture, Parallelizable, Category("Milestone2"), Category("Income Govt")]
    class TC035_ApplyLoanwithGovernmentIncomeismorethan40NL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(1100, "android", TestName = "TC035_ApplyLoanwithGovernmentIncomeismorethan40_NL_1100"), Category("NL"), Category("Mobile"), Retry(2)]
        [TestCase(3800, "ios", TestName = "TC035_ApplyLoanwithGovernmentIncomeismorethan40_NL_3800")]
        public void TC035_ApplyLoanwithGovernmentIncomeismorethan40_NL(int loanamout, string mobiledevice)
        {
            strUserType = "NL"; 
            _driver = _testengine.TestSetup(mobiledevice);
            _homeDetails = new HomeDetails(_driver, "NL");
            _loanPurposeDetails = new LoanPurposeDetails(_driver, "NL");
            _personalDetails = new PersonalDetails(_driver, "NL");
            _bankDetails = new BankDetails(_driver, "NL");

            try
            {
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

                // select Governments benefits option YES
                _bankDetails.ClickYesGovtBenefitsbtn();

                // click on Agree that information True
                _bankDetails.ClickAgreeAppSubmitBtn();

                // click on confirm Submit button
                _bankDetails.ClickConfirmSummaryBtn();

                // Verify unsuccessful message
                string UnsuccessMsg = "Application unsuccessful";
                Assert.IsTrue(_personalDetails.GetUnsuccessMessage().Contains(UnsuccessMsg));

                //verify DNQ Message
                string ActualDNQMessage = "You currently don't qualify for a Nimble loan";
                Assert.IsTrue(_personalDetails.GetDNQMessage().Contains(ActualDNQMessage));

            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }

    }


    [TestFixture, Parallelizable, Category("Milestone2"), Category("Income Govt")]
    class TC035_ApplyLoanwithGovernmentIncomeismorethan40RL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();
        private LoanSetUpDetails _loanSetUpDetails = null;

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }


        [TestCase(1500, "android", TestName = "TC035_ApplyLoanwithGovernmentIncomeismorethan40_RL_1500"), Category("RL"), Category("Mobile"), Retry(2)]
        [TestCase(2450, "ios", TestName = "TC035_ApplyLoanwithGovernmentIncomeismorethan40_RL_2450")]
        public void TC035_ApplyLoanwithGovernmentIncomeismorethan40_RL(int loanamout, string mobiledevice)
        {
             strUserType = "RL";
            _driver = _testengine.TestSetup(mobiledevice,"RL");
            _homeDetails = new HomeDetails(_driver, "RL");
            _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
            _personalDetails = new PersonalDetails(_driver, "RL");
            _bankDetails = new BankDetails(_driver, "RL");
            _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

            try
            {
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
                _bankDetails.EnterBankCredentialsTxt("WebUITest.bank43", "bank43");

                // Click on Continue Button
                _bankDetails.ClickAutoContinueBtn();

                // choose bank account
                _bankDetails.BankAccountSelectBtn();

                // Click on bank select Continue Button
                _bankDetails.ClickBankAccountContBtn();

                // Verify unsuccessful message
                string UnsuccessMsg = "Application unsuccessful";
                Assert.IsTrue(_personalDetails.GetUnsuccessMessage().Contains(UnsuccessMsg));

                //verify DNQ Message
                string ActualDNQMessage = "You currently don't qualify for a Nimble loan";
                Assert.IsTrue(_personalDetails.GetDNQMessage().Contains(ActualDNQMessage));

                #region Commented code as it is not needed

                //// Confirm Bank Details
                //_bankDetails.EnterBankDetailsTxt();

                //// Click on Confirm account details Continue Button  
                //_bankDetails.ClickAcctDetailsBtn();

                //// Select Category 
                //_bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "1");

                //// click on Confirm Income Button
                //_bankDetails.ClickConfirmIncomeBtn();

                //// select  other debt repayments option No 
                //_bankDetails.SelectOtherDebtRepaymentsOptionBtn();

                //// select dependents 
                //_bankDetails.SelectDependantsLst(TestData.Dependents.Zero);

                //// Click on continue
                //_bankDetails.ClickConfirmExpensesBtn();

                //// select Governments benefits option YES
                //_bankDetails.ClickYesGovtBenefitsbtn();

                //// click on Agree that information True
                //_bankDetails.ClickAgreeAppSubmitBtn();

                //// click on confirm Submit button
                //_bankDetails.ClickConfirmSummaryBtn();

                //if (loanamout > 2600)
                //{
                //    // enter sms input as OTP 
                //    if (_bankDetails.VerifySMSOTP())
                //        _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);
                //}

                //// verify final review enabled and process setup functionality
                //_loanSetUpDetails.loanSetupFunction(loanamout, strUserType);
                #endregion

            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}