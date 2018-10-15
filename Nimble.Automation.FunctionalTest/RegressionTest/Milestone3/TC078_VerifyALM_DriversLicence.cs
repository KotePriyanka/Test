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
    class TC078_VerifyAML_DriversLicence : TestEngine
    {
        public string strMessage { get; set; } = null;
        ResultDbHelper _result = new ResultDbHelper();
        DateTime starttime { get; set; } = DateTime.Now;
        IWebDriver _driver = null;
        PersonalDetails _personalDetails = null;

        [TearDown]
        public void Aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        // Debug menu button: "AML 1 Reject, AML2 Accept" button
        [TestCase(1100, "android", TestName = "TC078_VerifyAML_DriversLicense_NL_AML1Reject_AML2Accept_NL_1100"), Category("NL"), Retry(2)]
        [TestCase(2500, "ios", TestName = "TC078_VerifyAML_DriversLicense_NL_AML1Reject_AML2Accept_NL_2500")]
        public void TC078_VerifyAML_DriversLicense_NL_AML1Reject_AML2Accept(int loanamount, string strdevice)
        {

            _driver = TestSetup(strdevice);

            HomeDetails _homeDetails = new HomeDetails(_driver, "NL");
            LoanPurposeDetails _loanPurposeDetails = new LoanPurposeDetails(_driver, "NL");
            _personalDetails = new PersonalDetails(driver, "NL");
            PersonalDetailsDataObj _personalDetailsData = new PersonalDetailsDataObj();
            BankDetails _bankDetails = new BankDetails(driver, "NL");
            LoanSetUpDetails _loanSetUpDetails = new LoanSetUpDetails(driver, "NL");
            starttime = DateTime.Now;

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
                _personalDetailsData.StreetName = "Cr:A Id:0 A2:S Rr:A Rr2:A Rr3:A He:A Rmsrv:0.9844 Bsp:Y";
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
                _bankDetails.SelectIdTypeLst("Drivers Licence");
                _bankDetails.EnterIdDriversLicenceNameTxt("Brian123");
                _bankDetails.SelectIdDriversLicenceStateLst("QLD");
                _bankDetails.EnterIdDriversLicenceNumberTxt();
                _bankDetails.ClickIdSubmitBtn();

                // SMS Verification page
                if (loanamount > 2000)
                {
                    // Enter sms input
                    if (_bankDetails.VerifySMSOTP())
                        _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);
                }

                // verify final review enabled and process setup functionality
                _loanSetUpDetails.loanSetupFunction(loanamount, "NL");

                #region old code
                //// Application Finished page
                //_bankDetails.ClickLoanDashboardManual();

                //// Member Area page
                //_bankDetails.ClickFinalApproval();
                //_bankDetails.ClickSetup();

                //// Setup Loan page
                //_loanSetUpDetails.ClickSubmitBtn();

                //// Contract page
                //_loanSetUpDetails.Loancontract();
                //_loanSetUpDetails.ConfirmAcceptingContract();
                //_loanSetUpDetails.ClickOnAgreeBtn();

                //// Nimble Visa Prepaid Card page
                //_loanSetUpDetails.ClickNothanksBtn();

                //// Contract Signed page
                //_loanSetUpDetails.ClickLoanDashboard();

                //// Member Area page
                //_loanSetUpDetails.Logout();
                #endregion
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
            //finally
            //{
            //    _driver.Quit();
            //    _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
            //}
        }

        // Debug menu button: "AML 1 and 2 Fail" (system unable to confirm user identity)
        [TestCase(1400, "android", TestName = "TC078_VerifyAML_DriversLicense_NL_AML1Reject_AML2Reject_NL_1400"), Category("NL"), Retry(2)]
        [TestCase(2400, "ios", TestName = "TC078_VerifyAML_DriversLicense_NL_AML1Reject_AML2Reject_NL_2400")]
        public void TC078_VerifyAML_DriversLicense_NL_AML1Reject_AML2Reject(int loanamount, string strdevice)
        {
            _driver = TestSetup(strdevice);

            HomeDetails _homeDetails = new HomeDetails(_driver, "NL");
            LoanPurposeDetails _loanPurposeDetails = new LoanPurposeDetails(_driver, "NL");
            _personalDetails = new PersonalDetails(driver, "NL");
            PersonalDetailsDataObj _personalDetailsData = new PersonalDetailsDataObj();
            BankDetails _bankDetails = new BankDetails(driver, "NL");
            LoanSetUpDetails _loanSetUpDetails = new LoanSetUpDetails(driver, "NL");

            ResultDbHelper _result = new ResultDbHelper();

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
                _bankDetails.SelectIdTypeLst("Drivers Licence");
                _bankDetails.EnterIdDriversLicenceNameTxt("Brian123");
                _bankDetails.SelectIdDriversLicenceStateLst("QLD");
                _bankDetails.EnterIdDriversLicenceNumberTxt();
                _bankDetails.ClickIdSubmitBtn();

                //Check for oops message
                string expectedErrorMessage = "Oops. Our system had trouble confirming your identity.";
                string actualErrorMessage = _bankDetails.getALMErrorMessage();
                Assert.AreEqual(expectedErrorMessage, actualErrorMessage);

                // Member Area page
                // System could not ID person, person added to outbound contact queue to confirm details
                _loanSetUpDetails.Logout();

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                strMessage += ex.Message;
            }
            //finally
            //{
            //    _driver.Quit();
            //    _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
            //}
        }

    }
}
