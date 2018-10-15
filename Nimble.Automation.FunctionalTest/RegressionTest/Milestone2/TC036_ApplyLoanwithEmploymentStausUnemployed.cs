using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest
{
    [TestFixture, Parallelizable, Category("Milestone2"), Category("Employment")]
    class TC036_ApplyLoanwithEmploymentStausUnemployedNL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(1100, "android", "Student", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_NL_1100"), Category("NL"), Ignore("Need to fix unemployment issue"), Retry(2)]
        [TestCase(1250, "android", "Looking for a job", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_NL_1250")]
        [TestCase(1300, "android", "Stay at home parent", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_NL_1300")]
        [TestCase(1350, "android", "Disability or health issue", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_NL_1350")]
        [TestCase(1400, "android", "Retired", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_NL_1400")]
        [TestCase(3800, "ios", "Student", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_NL_3800")]
        [TestCase(3850, "ios", "Looking for a job", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_NL_3850")]
        [TestCase(3900, "ios", "Stay at home parent", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_NL_3900")]
        [TestCase(3950, "ios", "Disability or health issue", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_NL_3950")]
        [TestCase(4000, "ios", "Retired", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_NL_4000")]
        public void TC036_ApplyLoanwithEmploymentStausUnemployed_NL(int loanamout, string strmobiledevice, string UnEmpDesc)
        {
            strUserType = "NL";
            _driver = _testengine.TestSetup(strmobiledevice);
            _homeDetails = new HomeDetails(_driver, "NL");
            _loanPurposeDetails = new LoanPurposeDetails(_driver, "NL");
            _personalDetails = new PersonalDetails(_driver, "NL");
            _bankDetails = new BankDetails(_driver, "NL");
            _loanSetUpDetails = new LoanSetUpDetails(_driver, "NL");

            try
            {
                //Go to the homepage and click the start application button
                _homeDetails.HomeDetailsPage();

                //Select the loan amount and purpose and click on continue button
                _loanPurposeDetails.LoanPurposeFunction(loanamout, TestData.POL.Households);

                // entering personal details with overwrite values
                PersonalDetailsDataObj obj = new PersonalDetailsDataObj();
                obj.EmploymentStatus = TestData.YourEmployementStatus.Unemployed;
                obj.UnemploymentDesc = UnEmpDesc;

                //_personalDetails.PopulatePersonalDetails();
                _personalDetails.PopulatePersonalDetails(obj);

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

    [TestFixture, Parallelizable, Category("Milestone2"), Category("Employment")]
    class TC036_ApplyLoanwithEmploymentStausUnemployedRL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1200, "android", "Student", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_RL_1200"), Category("RL"), Ignore("Need to fix unemployment issue"), Retry(2)]
        [TestCase(1250, "android", "Looking for a job", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_RL_1250")]
        [TestCase(1300, "android", "Stay at home parent", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_RL_1300")]
        [TestCase(1350, "android", "Disability or health issue", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_RL_1350")]
        [TestCase(1400, "android", "Retired", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_RL_1400")]
        [TestCase(2300, "ios", "Student", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_RL_2300")]
        [TestCase(2400, "ios", "Looking for a job", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_RL_2400")]
        [TestCase(2500, "ios", "Stay at home parent", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_RL_2500")]
        [TestCase(2600, "ios", "Disability or health issue", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_RL_2600")]
        [TestCase(2700, "ios", "Retired", TestName = "TC036_ApplyLoanwithEmploymentStausUnemployed_RL_2700")]
        public void TC036_ApplyLoanwithEmploymentStausUnemployed_RL(int loanamout, string strmobiledevice, string UnEmpDesc)
        {
            strUserType = "RL";
            _driver = _testengine.TestSetup(strmobiledevice, "RL");
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

                // select Employement Status
                string Employmentstatus = TestData.YourEmployementStatus.Unemployed;
                _personalDetails.SelectEmploymentStatusLst(Employmentstatus);

                if (Employmentstatus == "Unemployed")
                {
                    _personalDetails.SelectUnEmploymentDescLst(UnEmpDesc);
                }

                // select short term loans value as NO
                _personalDetails.ClickNoShortTermLoanStatusBtn();

                // Check Read Privacy and Electronic Authorisation
                _personalDetails.CheckReadPrivacyBtn(TestData.ReturnerLoaner);

                // Check Read Credit Guide
                _personalDetails.CheckReadCreditBtn(TestData.ReturnerLoaner);

                if (GetPlatform(_driver))
                {
                    // Click on Personal Details Continue Button
                    _personalDetails.ClickPersonaldetailsContinueBtnRLMobile();
                }
                else
                {
                    // Click on Personal Details Continue Button
                    _personalDetails.ClickPersonaldetailsRequestBtnRLDesktop();

                    // Click on checks out Continue Button
                    //_personalDetails.ClickAutomaticVerificationBtn();
                }

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

                // Select Just checking option 
                // _bankDetails.SelectJustCheckingOptionLst("Yes, it will stay the same (or more)");

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
}


















