using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimble.Automation.FunctionalTest.Milestone5
{
    [TestFixture, Parallelizable, Category("ANR"), Category("Milestone5")]
    class TC166_Verify_ANR1_NL : TestEngine
    {
        [TearDown]
        public void aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TestCase(1000, "android", "WebUITest.bank51", "bank51", TestName = "TC166_Verify_ANR1_NL_1000"), Category("NL"), Retry(2)]
        [TestCase(1100, "android", "WebUITest.bank53", "bank53", TestName = "TC166_Verify_ANR2_NL_1100")]
        [TestCase(3800, "android", "WebUITest.bank51", "bank51", TestName = "TC166_Verify_ANR3_NL_3800")]
        [TestCase(3900, "android", "WebUITest.bank53", "bank53", TestName = "TC166_Verify_ANR4_NL_3900")]
        public void TC166_Verifying_ANR1_NL(int loanamout, string strmobiledevice, string BankUsername, string BankPwd)
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
                _loanPurposeDetails.LoanPurposeFunction(loanamout, TestData.POL.Eventcosts.Anniversary);

                //populate the personal details and proceed
                _personalDetails.PersonalDetailsFunction();

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(BankUsername, BankPwd);

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
                
                if (GetPlatform(_driver))
                {
                    //ClickOn Loan Dashboard...Manual Approval
                    _loanSetUpDetails.ClickLoanDashboardManual();

                    // click on More Button from Bottom Menu
                  //  _loanSetUpDetails.ClickMoreBtn();

                    // click on Approve button
                  //  _loanSetUpDetails.ClickApproveBtn();

                    //Click Setup Button
                   // _bankDetails.ClickSetup();

                    // click on Buton Submit
                  //  _loanSetUpDetails.ClickSubmitBtn();
                }
                else
                {
                    //ClickOn Loan Dashboard...Manual Approval
                    _loanSetUpDetails.ClickLoanDashboardManual();

                    //click on Final Approve
                  //  _loanSetUpDetails.FinalApprove();

                    //Click Setup Button
                    //_bankDetails.ClickSetup();

                    // click on Buton Submit
                   // _loanSetUpDetails.ClickSubmitBtn();
                }

                ////  Scrolling the Loan Contract
                //_loanSetUpDetails.Loancontract();

                //// Confirming accepting contract
                //_loanSetUpDetails.ConfirmAcceptingContract();

                //// click on I Agree button
                //_loanSetUpDetails.ClickOnAgreeBtn();

                //// click on No thanks Button
                //_loanSetUpDetails.ClickNothanksBtn();

                //if (GetPlatform(_driver))
                //{
                //    // Click on To Loan Dashboard Button
                //    _loanSetUpDetails.ClickMobileLoanDashboardBtn();

                //    // click on More Button from Bottom Menu
                //    _loanSetUpDetails.ClickMoreBtn();

                //    //Logout
                //    _loanSetUpDetails.Logout();
                //}
                //else
                //{
                //    // Click on Loan Dashboard Button
                //    _loanSetUpDetails.ClickLoanDashboard();

                //    //Logout
                //    _loanSetUpDetails.Logout();
                //}
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }

    [TestFixture, Parallelizable, Category("ANR"), Category("Milestone5")]
    class TC166_Verify_ANR1_RL : TestEngine
    {
        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TestCase(1000, "android", "WebUITest.bank51", "bank51", TestName = "TC166_Verify_ANR1_RL_1000"), Category("RL"), Retry(2)]
        [TestCase(1100, "android", "WebUITest.bank53", "bank53", TestName = "TC166_Verify_ANR2_RL_1100")]
        [TestCase(2600, "android", "WebUITest.bank51", "bank51", TestName = "TC166_Verify_ANR3_RL_2600")]
        [TestCase(2700, "android", "WebUITest.bank53", "bank53", TestName = "TC166_Verify_ANR4_RL_2700")]
        public void TC166_Verifying_ANR1_RL(int loanamout, string strmobiledevice, string BankUsername, string BankPwd)
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

                //Click on Select First POL Lst
                _loanPurposeDetails.ClickSelectFirstPurposeBtn();

                // Select Purpose of loan
                _loanPurposeDetails.SelectLoanPurposeRL(TestData.POL.Homerepairsorimprovements);

                // Enter FirstPOLLoan Amount
                _loanPurposeDetails.EnterFirstPOLAmountTxt(loanamout.ToString());

                // Click on Continue Button
                _loanPurposeDetails.ClickLoanPOLContinueBtnRL();

                //Edit the personal details and change the Rmsrv Code
                _personalDetails.PersonalDetailsFunction_RL(TestData.YourEmployementStatus.FullTime, TestData.ReturnerLoaner, TestData.OverrideCodes.PassAll_RL);

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(BankUsername,BankPwd);

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
                
                if (GetPlatform(_driver))
                {
                    //ClickOn Loan Dashboard...Manual Approval
                    _loanSetUpDetails.ClickLoanDashboardManual();

                    // click on More Button from Bottom Menu
                   // _loanSetUpDetails.ClickMoreBtn();

                    // click on Approve button
                   // _loanSetUpDetails.ClickApproveBtn();

                    //Click Setup Button
                  //  _bankDetails.ClickSetup();

                    // click on Buton Submit
                  //  _loanSetUpDetails.ClickSubmitBtn();
                }
                else
                {
                    //ClickOn Loan Dashboard...Manual Approval
                    _loanSetUpDetails.ClickLoanDashboardManual();

                    //click on Final Approve
                   // _loanSetUpDetails.FinalApprove();

                    //Click Setup Button
                   // _bankDetails.ClickSetup();

                    // click on Buton Submit
                 //   _loanSetUpDetails.ClickSubmitBtn();
                }

                ////  Scrolling the Loan Contract
                //_loanSetUpDetails.Loancontract();

                //// Confirming accepting contract
                //_loanSetUpDetails.ConfirmAcceptingContract();

                //// click on I Agree button
                //_loanSetUpDetails.ClickOnAgreeBtn();

                //// click on No thanks Button
                //_loanSetUpDetails.ClickNothanksBtn();

                //if (GetPlatform(_driver))
                //{
                //    // Click on To Loan Dashboard Button
                //    _loanSetUpDetails.ClickMobileLoanDashboardBtn();

                //    // click on More Button from Bottom Menu
                //    _loanSetUpDetails.ClickMoreBtn();

                //    //Logout
                //    _loanSetUpDetails.Logout();
                //}
                //else
                //{
                //    // Click on Loan Dashboard Button
                //    _loanSetUpDetails.ClickLoanDashboard();

                //    //Logout
                //    _loanSetUpDetails.Logout();
                //}
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}
