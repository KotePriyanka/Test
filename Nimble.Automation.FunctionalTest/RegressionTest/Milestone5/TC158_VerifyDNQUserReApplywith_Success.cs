using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;


namespace Nimble.Automation.FunctionalTest.Milestone5
{
    [TestFixture, Parallelizable, Category("Milestone5"), Category("Misc")]
    class TC158_VerifyDNQUserReApplywith_Success : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();
        private string email = "";

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, email, starttime);
        }

        [TestCase(1400, "chrome", TestName = "TC158_VerifyDNQUserReApplywithSuccess_1400"), Category("NL"), Retry(2)]
        [TestCase(2700, "chrome", TestName = "TC158_VerifyDNQUserReApplywithSuccess_2700")]
        public void TC158_VerifyDNQUserReApplywithSuccess_NL(int loanamout, string strmobiledevice)
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
                _loanPurposeDetails.RequestLoanAmount(loanamout, TestData.POL.Partlyfullyrepayacurrentshorttermloan);
                //_loanPurposeDetails.LoanPurposeFunction(loanamout, TestData.POL.Households);

                //populate the personal details and proceed
                PersonalDetailsDataObj _obj = _personalDetails.PersonalDetailsFunction1();

                // Verify unsuccessful message
                string UnsuccessMsg = "Application unsuccessful";
                Assert.IsTrue(_personalDetails.GetUnsuccessMessage().Contains(UnsuccessMsg));

                //verify DNQ Message
                string ActualDNQMessage = "You currently don't qualify for a Nimble loan";
                Assert.IsTrue(_personalDetails.GetDNQMessage().Contains(ActualDNQMessage));
                email = _personalDetails.EmailID;
                // quiting the driver
                _driver.Quit();

                _driver = _testengine.TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
                _personalDetails = new PersonalDetails(_driver, "RL");
                _bankDetails = new BankDetails(_driver, "RL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

                _homeDetails.ClickLoginBtn();

                _homeDetails.LoginLogoutUser(_obj.Email, "password");

                //click on update profile
                _loanSetUpDetails.UpdateYourProfile();

                //click on save Button
                _loanSetUpDetails.UpdateSaveBtn();

                //click on Loan dashboard 
                _loanSetUpDetails.ToLoanDashboard();

                //click on request money 
                _loanSetUpDetails.RequestMoney();

                // Click on Start Your Application Button
                _homeDetails.ClickStartApplictionBtn();

                // Select Loan Value from Slide bar
                _loanPurposeDetails.SelectLoanValueRL(loanamout);

                //Click on Select First POL Lst
                _loanPurposeDetails.ClickSelectFirstPurposeBtn();

                // Select Purpose of loan
                _loanPurposeDetails.SelectLoanPurposeRL(TestData.POL.Households);

                // Enter FirstPOLLoan Amount
                _loanPurposeDetails.EnterFirstPOLAmountTxt(loanamout.ToString());

                // Click on Continue Button
                _loanPurposeDetails.ClickLoanPOLContinueBtnRL();

                // select Employement Status
                _personalDetails.SelectEmploymentStatusLst(TestData.YourEmployementStatus.FullTime);

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
                }

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
                        _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);
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

