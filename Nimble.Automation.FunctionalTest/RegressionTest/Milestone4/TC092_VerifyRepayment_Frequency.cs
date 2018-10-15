using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest
{
    //<Summary>
    //Verifying the loan application by changing the repayment frequency in the Loan Setup Page
    //</Summary>

    [TestFixture, Parallelizable, Category("Milestone4"), Category("Setup Page")]
    class TC092_VerifyRepayment_Frequency_NL : TestEngine
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
        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();


        [TestCase(1250, "android", 235, TestName = "TC092_ChangeRepaymentFrequency_NL_SACC_1250"), Category("NL"), Retry(2)]
        [TestCase(2700, "ios", 373, TestName = "TC092_ChangeRepaymentFrequency_NL_MACC_2700")]
        public void TC092_ChangeRepaymntFrequency_NL(int loanamout, string strmobiledevice, int fortamt)
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

                //IncomeCategory
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

                // Verify ApprovedAmount
                _loanSetUpDetails.VerifySetUpPageDetails(7, loanamout, loanamout, strUserType);

                // Verify ApprovedAmount
                //Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout), "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());

                //Get Repayment amount on weekly repay

                int FortnigntRepaymentAmount = fortamt;

                //Click FortNight
                _loanSetUpDetails.ClickFortnight();

                //Get Repayment amount on fortnight repay

                int RepaymentFortnightInTable = _loanSetUpDetails.getRepAmtInTable(fortamt);

                // Assert.AreEqual(FortnigntRepaymentAmount, RepaymentFortnightInTable, "Values not matched");

                //_loanSetUpDetails.loanSetupFunction(loanamout, strUserType);
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }

    [TestFixture, Parallelizable, Category("Milestone4"), Category("Setup Page")]
    class TC092_VerifyRepayment_Frequency_RL : TestEngine
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
        private IWebDriver _driver = null; string strMessage, strUserType="";
        DateTime starttime { get; set; } = DateTime.Now;
        ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();


        [TestCase(1250, "android", 533, TestName = "TC092_ChangeRepaymentFrequency_RL_SACC_1250"), Category("RL"), Retry(2)]
        [TestCase(2700, "ios", 803, TestName = "TC092_ChangeRepaymentFrequency_RL_MACC_2700")]
        public void TC092_ChangeRepaymNtFrequency_RL(int loanamout, string strmobiledevice, int fortamt)
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

                //Select the loan amount and purpose and click on continue button
                _loanPurposeDetails.LoanPurposeFunction_RL(loanamout, TestData.POL.Households);

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
                // _bankDetails.SelectSevenIncome(TestData.IncomeCategory.PrimaryIncome, TestData.IncomeCategory.NotIncome, TestData.IncomeCategory.OtherEmployment, TestData.IncomeCategory.PartnerSalary, TestData.IncomeCategory.SharedRentUtilities, TestData.IncomeCategory.ChildSupport, TestData.IncomeCategory.InvestmentIncome);

                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "0");

                //_bankDetails.SelectIncomecategory(TestData.IncomeCategory.NotIncome, "5");

                //_bankDetails.SelectIncomecategory(TestData.IncomeCategory.OtherEmployment, "4");

                //_bankDetails.SelectIncomecategory(TestData.IncomeCategory.PartnerSalary, "3");

                //_bankDetails.SelectIncomecategory(TestData.IncomeCategory.SharedRentUtilities, "2");

                //_bankDetails.SelectIncomecategory(TestData.IncomeCategory.ChildSupport, "1");

                //_bankDetails.SelectIncomecategory(TestData.IncomeCategory.InvestmentIncome, "0");

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

                        //click on Final Approve
                        _loanSetUpDetails.FinalApprove();

                        //Click Setup Button
                        _loanSetUpDetails.ClickSetup();

                    }
                }

                //Get Repayment amount on weekly repay
                int FortnigntRepaymentAmount = fortamt;

                //Click FortNight
                _loanSetUpDetails.ClickFortnight();

                //Get Repayment amount on fortnight repay
                int RepaymentFortnightInTable = _loanSetUpDetails.getRepAmtInTable(fortamt);

                if (GetPlatform(_driver))
                {
                    // click on Button Submit
                    _bankDetails.ClickSubmitBtn();

                    // Click on Bank Account to transfer
                    _bankDetails.ClicksixtyMinuteButton();

                    // click on sublit-payment Button
                    _bankDetails.ClickSubmitPaymentButton();
                }
                else
                {
                    // Click on Bank Account to transfer
                    _bankDetails.ClicksixtyMinuteButton();

                    // click on Buton Submit
                    _bankDetails.ClickSubmitBtn();
                }

                //Assert.AreEqual(FortnigntRepaymentAmount, RepaymentFortnightInTable, "Values not matched");

                _loanSetUpDetails.loanSetupFunction_RL(loanamout);
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}


















