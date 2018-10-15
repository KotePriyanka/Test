using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimble.Automation.FunctionalTest.RegressionTest.Milestone6
{
    [TestFixture, Parallelizable, Category("Milestone6"), Category("Nimble Card not Offered for other banks")]
    class TC197_Verify_Nimble_Card_Not_Offered_For_Other_Banks_NL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1500, "android", TestName = "TC197_Verify_Nimble_Card_Not_Offered_For_Other_Banks_android_NL"), Category("NL"), Category("Mobile"), Retry(2)]
        [TestCase(3800, "ios", TestName = "TC197_Verify_Nimble_Card_Not_Offered_For_Other_Banks_ios_NL")]
        public void TC197_Verifying_Nimble_Card_Not_Offered_For_Other_Banks_NL(int loanamout, string strmobiledevice)
        {
            strUserType = "NL";
            try
            {
                _driver = TestSetup(strmobiledevice, "NL");
                _homeDetails = new HomeDetails(_driver, "NL");
                _personalDetails = new PersonalDetails(_driver, "NL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "NL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "NL");
                _bankDetails = new BankDetails(_driver, "NL");
                                
                //Click on Apply Button
                _homeDetails.ClickApplyBtn();

                // Click on Start Your Application Button
                _homeDetails.ClickStartApplictionBtn();

                bool hideshow = _homeDetails.CheckHideShow();
                if (hideshow == true)
                {
                    if (GetPlatform(_driver))
                    {
                        _loanPurposeDetails.RequestLoanAmountMobile(loanamout, TestData.POL.Households);
                    }
                    else
                    {
                        _loanPurposeDetails.RequestLoanAmount(loanamout, TestData.POL.Households);
                    }
                }
                else
                {
                    // Select Loan Value from Slide bar
                    _loanPurposeDetails.SelectLoanValue(loanamout);

                    //Click on First POL to select
                    _loanPurposeDetails.ClickSelectFirstPurposeBtn();

                    if (GetPlatform(_driver))
                    {
                        // Select Purpose of loan
                        _loanPurposeDetails.SelectLoanPurposeMobile(TestData.POL.Households);
                    }
                    else
                    {
                        // Select Purpose of loan
                        _loanPurposeDetails.SelectLoanPurpose(TestData.POL.Households);
                    }

                    // Enter FirstPOLLoan Amount
                    _loanPurposeDetails.EnterFirstPOLAmountTxt(loanamout.ToString());

                    // Click on Continue Button
                    _loanPurposeDetails.ClickLoanPOLContinueBtn();
                }
                // entering personal details with overwrite values
                _personalDetails.PopulatePersonalDetails();

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
                _bankDetails.enterBankDetailsTxtForWestpec();

                // Click on Confirm account details Continue Button  
                _bankDetails.ClickAcctDetailsBtn();

                strMessage += string.Format("\r\n\t Dag bank Launched");
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

                // Verify ApprovedAmount
                _loanSetUpDetails.VerifySetUpPageDetails(7, loanamout, loanamout, strUserType);

                // Verify ApprovedAmount
                Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout), "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());

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

                // Verify Funded Amount
                Assert.IsTrue(_loanSetUpDetails.VerifyFundedAmount(loanamout), " Expected Requested Amount : " + loanamout + ". Observed Funded Amount : " + _loanSetUpDetails.VerifyFundedAmount());

                if (GetPlatform(_driver))
                {
                    // Click on To Loan Dashboard Button
                    _loanSetUpDetails.ClickMobileLoanDashboardBtn();

                    //Check for order card link
                    Assert.IsFalse(_loanSetUpDetails.verifyOrderCardLink(), "Order card link");

                    // click on More Button from Bottom Menu
                    _loanSetUpDetails.ClickMoreBtn();

                    //Logout
                    _loanSetUpDetails.Logout();
                    strMessage += string.Format("\r\n\t Ends");
                }
                else
                {
                    // Click on Loan Dashboard Button
                    _loanSetUpDetails.ClickLoanDashboard();

                    //Check for activate card link
                    Assert.IsFalse(_loanSetUpDetails.verifyActivateCardLink(), "Activate card link");

                    //Logout
                    _loanSetUpDetails.Logout();
                    strMessage += string.Format("\r\n\t Ends");
                }

            }

            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }

        }
    }

    [TestFixture, Parallelizable, Category("Milestone6"), Category("Nimble Card not Offered for other banks")]
    class TC197_Verify_Nimble_Card_Not_Offered_For_Other_Banks_RL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1500, "android", TestName = "TC197_Verify_Nimble_Card_Not_Offered_For_Other_Banks_android_RL"), Category("RL"), Retry(2)]
        [TestCase(3800, "ios", TestName = "TC197_Verify_Nimble_Card_Not_Offered_For_Other_Banks_ios_RL")]
        public void TC197_Verifying_Nimble_Card_Not_Offered_For_Other_Banks_RL(int loanamout, string strmobiledevice)
        {
            strUserType = "RL";
            try
            {
                _driver = TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _personalDetails = new PersonalDetails(_driver, "RL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");
                _loanPurposeDetails = new LoanPurposeDetails(driver, "RL");
                _bankDetails = new BankDetails(driver, "RL");

                // Login with existing user
                _homeDetails.LoginExistingUser(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.NewProductAdvancePaidClean);

                // Click on Request Money Button
                _homeDetails.ClickRequestMoneyBtn();

                //Click on Start Application Button
                _homeDetails.ClickExistinguserStartApplictionBtn();

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

                    // Click on checks out Continue Button
                    _personalDetails.ClickAutomaticVerificationBtn();
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
                _bankDetails.enterBankDetailsTxtForWestpec();

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

                // Verify ApprovedAmount
                _loanSetUpDetails.VerifySetUpPageDetails(7, loanamout, loanamout, strUserType);

                if (GetPlatform(_driver))
                {
                    // click on Button Submit
                    _loanSetUpDetails.ClickSubmitBtn();
                }
                else
                {
                    // Click on Bank Account to transfer
                    _bankDetails.ClicksixtyMinuteButton();

                    // click on Buton Submit
                    _loanSetUpDetails.ClickSubmitBtn();
                }

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

                    //Check for order card link
                    Assert.IsFalse(_loanSetUpDetails.verifyOrderCardLink(), "Order card link");

                    // click on More Button from Bottom Menu
                    _loanSetUpDetails.ClickMoreBtn();

                    //Logout
                    _loanSetUpDetails.Logout();
                    strMessage += string.Format("\r\n\t Ends");
                }
                else
                {
                    // Click on Loan Dashboard Button
                    _loanSetUpDetails.ClickLoanDashboard();

                    //Check for activate card link
                    Assert.IsFalse(_loanSetUpDetails.verifyActivateCardLink(), "Activate card link");

                    //Logout
                    _loanSetUpDetails.Logout();
                    strMessage += string.Format("\r\n\t Ends");
                }

            }

            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}
