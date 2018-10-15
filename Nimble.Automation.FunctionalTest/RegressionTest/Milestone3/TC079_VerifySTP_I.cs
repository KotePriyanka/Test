using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest
{
    [TestFixture, Parallelizable, Category("Milestone3"), Category("STP 1")]
    class TC079_VerifySTP_D : TestEngine
    {
        private HomeDetails _homedetails = null;
        private LoanPurposeDetails _loanpurposedetails = null;
        private PersonalDetails _personaldetails = null;
        private BankDetails _bankdetails = null;

        [TearDown]
        public void Aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personaldetails.EmailID, starttime);
        }

        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();

        [TestCase(1500, "ios", TestName = "TC079_VerifySTP_D_NL_1500"), Category("NL"), Retry(2)]
        [TestCase(2300, "android", TestName = "TC079_VerifySTP_D_NL_2300")]
        public void TC079_VerifySTP_D_NL(int loanamount, string strdevice)
        {
            strUserType = "NL";
            _driver = TestSetup(strdevice);

            _homedetails = new HomeDetails(_driver, "NL");
            _loanpurposedetails = new LoanPurposeDetails(_driver, "NL");
            _personaldetails = new PersonalDetails(_driver, "NL");
            _bankdetails = new BankDetails(_driver, "NL");

            try
            {
                _homedetails.ClickApplyBtn();

                _homedetails.ClickStartApplictionBtn();

                bool hideshow = _homedetails.CheckHideShow();
                if (hideshow == true)
                {
                    if (GetPlatform(_driver))
                    {
                        _loanpurposedetails.RequestLoanAmountMobile(loanamount, TestData.POL.Households);
                    }
                    else
                    {
                        _loanpurposedetails.RequestLoanAmount(loanamount, TestData.POL.Households);
                    }
                }
                else
                {
                    // Select Loan Value from Slide bar
                    _loanpurposedetails.SelectLoanValue(loanamount);

                    //Click on First POL to select
                    _loanpurposedetails.ClickSelectFirstPurposeBtn();

                    if (GetPlatform(_driver))
                    {
                        // Select Purpose of loan
                        _loanpurposedetails.SelectLoanPurposeMobile(TestData.POL.Households);
                    }
                    else
                    {
                        // Select Purpose of loan
                        _loanpurposedetails.SelectLoanPurpose(TestData.POL.Households);
                    }

                    // Enter FirstPOLLoan Amount
                    _loanpurposedetails.EnterFirstPOLAmountTxt(loanamount.ToString());

                    // Click on Continue Button
                    _loanpurposedetails.ClickLoanPOLContinueBtn();
                }

                PersonalDetailsDataObj _personalDetailsData = new PersonalDetailsDataObj();

                TestUtility _testutility = new TestUtility();

                _personalDetailsData.StreetName = "At:N Cr:A Id:100 Bs1:P Rr1:A Rr2:D Rjs2:S Rr3:A Bsp:Y";

                _personalDetailsData.FirstName = _testutility.RandomString(8);

                _personaldetails.PopulatePersonalDetails(_personalDetailsData);

                _personaldetails.ClickCheckoutContinueBtn();

                _bankdetails.SelectBankLst(TestData.BankDetails.Dagbank);

                _bankdetails.BankSelectContinueBtn();

                _bankdetails.EnterBankCredentialsTxt(TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.UID, TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.PWD);

                _bankdetails.ClickAutoContinueBtn();

                _bankdetails.BankAccountSelectBtn();

                _bankdetails.ClickBankAccountContBtn();

                _bankdetails.EnterBankDetailsTxt();

                _bankdetails.ClickAcctDetailsBtn();

                _bankdetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "0");

                _bankdetails.ClickConfirmIncomeBtn();

                _bankdetails.SelectOtherDebtRepaymentsOptionBtn();

                _bankdetails.SelectDependantsLst(TestData.Dependents.Zero);

                _bankdetails.ClickConfirmExpensesBtn();

                _bankdetails.ClickNoGovtBenefitsbtn();

                _bankdetails.ClickAgreeAppSubmitBtn();

                _bankdetails.ClickConfirmSummaryBtn();

                string strval = _personaldetails.GetDNQTxt();

                // Verify unsuccessful message
                string UnsuccessMsg = "Application unsuccessful";
                Assert.IsTrue(_personaldetails.GetUnsuccessMessage().Contains(UnsuccessMsg));

                //verify DNQ Message
                string ActualDNQMessage = "You currently don't qualify for a Nimble loan";
                Assert.IsTrue(_personaldetails.GetDNQMessage().Contains(ActualDNQMessage));
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
            //finally
            //{
            //    _driver.Quit();
            //    _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personaldetails.EmailID, starttime);
            //}
        }
    }
}
