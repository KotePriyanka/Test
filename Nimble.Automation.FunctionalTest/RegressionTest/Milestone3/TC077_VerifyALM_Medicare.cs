using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimble.Automation.FunctionalTest.Milestone3
{
    [TestFixture, Parallelizable, Category("Milestone3"), Category("ALM")]
    class TC077_VerifyAML_Medicare : TestEngine
    {
        private IWebDriver _driver = null;
        public string strMessage { get; set; } = null;
        public string strUserType;


        ResultDbHelper _result = new ResultDbHelper();
        DateTime starttime { get; set; } = DateTime.Now;
        PersonalDetails _personalDetails = null;

        [TearDown]
        public void Aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        // Debug menu button: "AML 1 Reject, AML2 Accept" button
        [TestCase(700, "android", TestName = "TC077_VerifyAML_Medicare_AML1Reject_AML2Accept_NL_700"), Category("NL"), Retry(2)]
        [TestCase(2200, "ios", TestName = "TC077_VerifyAML_Medicare_AML1Reject_AML2Accept_NL_2200")]
        public void TC077_VerifyAML_Medicare_NL_AML1Reject_AML2Accept(int loanamount, string strdevice)
        {
            strUserType = "NL";
            _driver = TestSetup(strdevice, "NL");
            HomeDetails _homeDetails = new HomeDetails(_driver, "NL");
            LoanPurposeDetails _loanPurposeDetails = new LoanPurposeDetails(_driver, "NL");
            _personalDetails = new PersonalDetails(driver, "NL");
            PersonalDetailsDataObj _personalDetailsData = new PersonalDetailsDataObj();
            BankDetails _bankDetails = new BankDetails(driver, "NL");
            LoanSetUpDetails _loanSetUpDetails = new LoanSetUpDetails(driver, "NL");

            try
            {
                //Go to the homepage and click the start application button
                _homeDetails.HomeDetailsPage();

                //Select the loan amount and purpose and click on continue button
                _loanPurposeDetails.LoanPurposeFunction(loanamount, TestData.POL.Households);

                // Personal Details page
                _personalDetailsData.StreetName = "Cr:A Id:0 A2:S Rr:A Rr2:A Rr3:A He:A Bsp:Y Rmsrv:0.9999";
                // _personalDetailsData.StreetName = "At:N Cr:A Id:0 Rr1:A Rr2:A Rr3:A Bsp:Y Rmsrv:1";
                _personalDetails.PopulatePersonalDetails(_personalDetailsData);
                _personalDetails.ClickCheckoutContinueBtn();

                // Bank Details page
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);
                _bankDetails.BankSelectContinueBtn();
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.UID, TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.PWD);
                _bankDetails.ClickAutoContinueBtn();
                _bankDetails.BankAccountSelectBtn();
                _bankDetails.ClickBankAccountContBtn();
                _bankDetails.EnterBankDetailsTxt();
                _bankDetails.ClickAcctDetailsBtn();

                // Your Income page
                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "0");
                _bankDetails.ClickConfirmIncomeBtn();
                _bankDetails.SelectOtherDebtRepaymentsOptionBtn();

                // Your Expenses page
                _bankDetails.SelectDependantsLst(TestData.Dependents.Zero);
                _bankDetails.ClickConfirmExpensesBtn();
                _bankDetails.ClickNoGovtBenefitsbtn();

                // Your Summary page
                _bankDetails.ClickAgreeAppSubmitBtn();
                _bankDetails.ClickConfirmSummaryBtn();

                // Id pages
                _bankDetails.CheckIdAuthorisationChkbx();
                _bankDetails.SelectIdTypeLst("Medicare");
                _bankDetails.EnterIdMedicareCardNumberTxt();
                _bankDetails.EnterIdMedicareRefNoTxt("1");
                _bankDetails.SelectIdMedicareCardColourLst("Green");
                _bankDetails.EnterIdMedicareCardNameTxt("Brian123");
                _bankDetails.SelectIdMedicareExpiryDateMonthLst("Jul");
                _bankDetails.SelectIdMedicareExpiryDateYearLst("2020");
                _bankDetails.ClickIdSubmitBtn();

                // SMS Verification page
                if (loanamount > 2000)
                {
                    // Enter sms input
                    if (_bankDetails.VerifySMSOTP())
                        _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);
                }

                // verify final review enabled and process setup functionality
                _loanSetUpDetails.loanSetupFunction(loanamount, strUserType);

            }

            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }   

        // Debug menu button: "AML 1 and 2 Fail" (system unable to confirm user identity)
        [TestCase(1100, "android", TestName = "TC077_VerifyAML_Medicare_AML1Reject_AML2Reject_NL_1100"), Category("NL"), Retry(2)]
        [TestCase(2100, "ios", TestName = "TC077_VerifyAML_Medicare_AML1Reject_AML2Reject_NL_2100")]
        public void TC077_VerifyAML_Medicare_NL_AML1Reject_AML2Reject(int loanamount, string strdevice)
        {
            strUserType = "NL";

            _driver = TestSetup(strdevice);

            HomeDetails _homeDetails = new HomeDetails(_driver, "NL");
            LoanPurposeDetails _loanPurposeDetails = new LoanPurposeDetails(_driver, "NL");
            _personalDetails = new PersonalDetails(driver, "NL");
            PersonalDetailsDataObj _personalDetailsData = new PersonalDetailsDataObj();
            BankDetails _bankDetails = new BankDetails(driver, "NL");
            LoanSetUpDetails _loanSetUpDetails = new LoanSetUpDetails(driver, "NL");

            try
            {
                // Home page
                _homeDetails.ClickApplyBtn();
                _homeDetails.ClickStartApplictionBtn();

                // Purpose of Loan page
                bool hideshow = _homeDetails.CheckHideShow();
                if (hideshow == true)
                {
                    if (GetPlatform(_driver))
                    {
                        _loanPurposeDetails.RequestLoanAmountMobile(loanamount, TestData.POL.Households);
                    }
                    else
                    {
                        _loanPurposeDetails.RequestLoanAmount(loanamount, TestData.POL.Households);
                    }
                }
                else
                {
                    _loanPurposeDetails.SelectLoanValue(loanamount);

                    _loanPurposeDetails.ClickSelectFirstPurposeBtn();

                    if (GetPlatform(_driver))
                    {
                        _loanPurposeDetails.SelectLoanPurposeMobile(TestData.POL.Households);
                    }
                    else
                    {
                        _loanPurposeDetails.SelectLoanPurpose(TestData.POL.Households);
                    }

                    _loanPurposeDetails.EnterFirstPOLAmountTxt(loanamount.ToString());
                    _loanPurposeDetails.ClickLoanPOLContinueBtn();
                }

                // Personal Details page
               _personalDetailsData.StreetName = "At:N Cr:A Id:0 A2:F Rr1:A Rr2:A Rr3:A Bsp:Y Rmsrv:0.9999";
                //_personalDetailsData.StreetName = "At:N Cr:A Id:0 Rr1:A Rr2:A Rr3:A Bsp:Y Rmsrv:1";
                _personalDetails.PopulatePersonalDetails(_personalDetailsData);
                _personalDetails.ClickCheckoutContinueBtn();

                // Bank Details page
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);
                _bankDetails.BankSelectContinueBtn();
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.UID, TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.PWD);
                _bankDetails.ClickAutoContinueBtn();
                _bankDetails.BankAccountSelectBtn();
                _bankDetails.ClickBankAccountContBtn();
                _bankDetails.EnterBankDetailsTxt();
                _bankDetails.ClickAcctDetailsBtn();

                // Your Income page
                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "0");
                _bankDetails.ClickConfirmIncomeBtn();
                _bankDetails.SelectOtherDebtRepaymentsOptionBtn();

                // Your Expenses page
                _bankDetails.SelectDependantsLst(TestData.Dependents.Zero);
                _bankDetails.ClickConfirmExpensesBtn();
                _bankDetails.ClickNoGovtBenefitsbtn();

                // Your Summary page
                _bankDetails.ClickAgreeAppSubmitBtn();
                _bankDetails.ClickConfirmSummaryBtn();

                // Id pages
                _bankDetails.CheckIdAuthorisationChkbx();
                _bankDetails.SelectIdTypeLst("Medicare");
                _bankDetails.EnterIdMedicareCardNumberTxt();
                _bankDetails.EnterIdMedicareRefNoTxt("1");
                _bankDetails.SelectIdMedicareCardColourLst("Green");
                _bankDetails.EnterIdMedicareCardNameTxt("Brian123");
                _bankDetails.SelectIdMedicareExpiryDateMonthLst("Jul");
                _bankDetails.SelectIdMedicareExpiryDateYearLst("2020");
                _bankDetails.ClickIdSubmitBtn();

                // SMS Verification page
                //if (loanamount > 2000)
                //{
                //    // Enter sms input
                //    if (_bankDetails.VerifySMSOTP())
                //        _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);
                //}

                //Check for oops message
                string expectedErrorMessage = "Oops. Our system had trouble confirming your identity.";
                string actualErrorMessage = _bankDetails.getALMErrorMessage();
                Assert.AreEqual(expectedErrorMessage, actualErrorMessage);

                // Member Area page              
                _loanSetUpDetails.Logout();
            }

            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
            //finally
            //{
            //    _driver.Quit();
            //    _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
            //}
        }
    }
}
