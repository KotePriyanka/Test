using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest.Milestone5
{
    /// <summary>
    /// Added as part of new feature where user where existing RL users with govt income >40 till 80% can avail loan
    /// </summary>
    /// <seealso cref="Nimble.Automation.Accelerators.TestEngine" />
    [TestFixture, Parallelizable, Category("Milestone5"), Category("GovtIncSAACRL80")]
    class TC161_VerifyGovtIncome41_AnswerFalse_NL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(1100, "WebUITest.bank39", "bank39", "android", TestName = "TC161_VerifyGovtIncome41_AnswerFalse_NL_1100"), Category("NL"), Retry(2)]
        [TestCase(900, "WebUITest.bank43", "bank43", "android", TestName = "TC161_VerifyGovtIncome41_AnswerFalse_NL_900")]
        [TestCase(1800, "WebUITest.bank41", "bank41", "android", TestName = "TC161_VerifyGovtIncome41_AnswerFalse_NL_1800")]
        [TestCase(5000, "WebUITest.bank39", "bank39", "ios", TestName = "TC161_VerifyGovtIncome41_AnswerFalse_NL_5000")]
        [TestCase(3800, "WebUITest.bank43", "bank43", "ios", TestName = "TC161_VerifyGovtIncome41_AnswerFalse_NL_3800")]
        [TestCase(2200, "WebUITest.bank41", "bank41", "ios", TestName = "TC161_VerifyGovtIncome41_AnswerFalse_NL_2200")]
        public void TC_161_VerifyGovtIncome41_NL(int loanamout, string BankUID, string BankPWD, string mobiledevice)
        {
            strUserType = "NL";
            _driver = _testengine.TestSetup(mobiledevice, "NL");
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
                _bankDetails.EnterBankCredentialsTxt(BankUID, BankPWD);

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

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                strMessage += ex.Message;
            }
        }
    }

    [TestFixture, Parallelizable, Category("Milestone5"), Category("GovtIncSAACRL80")]
    class TC161_VerifyGovtIncome41_AnswerFalse_RL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1100, 1100, "WebUITest.bank39", "bank39", "android", TestName = "TC161_VerifyGovtIncome41_AnswerFalse_RL_1100"), Category("RL"), Retry(2)]
        [TestCase(900, 900, "WebUITest.bank43", "bank43", "android", TestName = "TC161_VerifyGovtIncome41_AnswerFalse_RL_900")]
        [TestCase(1800, 1800, "WebUITest.bank41", "bank41", "android", TestName = "TC161_VerifyGovtIncome41_AnswerFalse_RL_1800")]
        [TestCase(5000, 2000, "WebUITest.bank39", "bank39", "ios", TestName = "TC161_VerifyGovtIncome41_AnswerFalse_RL_5000")]
        [TestCase(3800, 2000, "WebUITest.bank43", "bank43", "ios", TestName = "TC161_VerifyGovtIncome41_AnswerFalse_RL_3800")]
        [TestCase(2200, 2000, "WebUITest.bank41", "bank41", "ios", TestName = "TC161_VerifyGovtIncome41_AnswerFalse_RL_2200")]
        public void TC_161_VerifyGovtIncome41_RL(int loanamout, int ExpectedApprovedamt, string BankUID, string BankPWD, string mobiledevice)
        {
            try
            {
                strUserType = "RL";
                _driver = TestSetup(mobiledevice, "RL");
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
                _bankDetails.EnterBankCredentialsTxt(BankUID, BankPWD);

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


                #region commented old scenario
                //// Confirm Bank Details
                //_bankDetails.EnterBankDetailsTxt();

                //// Click on Confirm account details Continue Button  
                //_bankDetails.ClickAcctDetailsBtn();

                //// Select Category 
                //_bankDetails.SelectIncomeCategoryLst(TestData.IncomeCategory.PrimaryIncome);

                //// click on Confirm Income Button
                //_bankDetails.ClickConfirmIncomeBtn();

                //// select  other debt repayments option No 
                //_bankDetails.SelectOtherDebtRepaymentsOptionBtn();

                //// select dependents 
                //_bankDetails.SelectDependantsLst(TestData.Dependents.Zero);

                //// Click on continue
                //_bankDetails.ClickConfirmExpensesBtn();

                //// select Governments benefits option No
                //_bankDetails.ClickNoGovtBenefitsbtn();

                //// click on Agree that information True
                //_bankDetails.ClickAgreeAppSubmitBtn();

                //// click on confirm Submit button
                //_bankDetails.ClickConfirmSummaryBtn();

                ////Get the approved loan amount value
                //int approvedAmount = _loanSetUpDetails.GetApprovedamount();

                //// Verify ApprovedAmount
                //Assert.AreEqual(ExpectedApprovedamt, approvedAmount, "Incorrect values" + ExpectedApprovedamt + " - " + approvedAmount);

                //if (GetPlatform(_driver))
                //{

                //    // click on Button Submit
                //    _loanSetUpDetails.ClickSubmitBtn();

                //    // Click on Bank Account to transfer
                //    _bankDetails.ClicksixtyMinuteButton();

                //    // click on sublit-payment Button
                //    _bankDetails.ClickSubmitPaymentButton();
                //}
                //else
                //{
                //    // Click on Bank Account to transfer
                //    _bankDetails.ClicksixtyMinuteButton();

                //    // click on Buton Submit
                //    _loanSetUpDetails.ClickSubmitBtn();
                //}

                //if (loanamout > 2000)
                //{
                //    _loanSetUpDetails.SelectReasontospendLess(TestData.ReasonforspeandLess.cheaperservice);
                //}

                ////  Scrolling the Loan Contract
                //_loanSetUpDetails.Loancontract();

                //// Confirming accepting contract
                //_loanSetUpDetails.ConfirmAcceptingContract();

                //// click on I Agree button
                //_loanSetUpDetails.ClickOnAgreeBtn();

                //// click on No thanks Button
                //_loanSetUpDetails.ClickNothanksBtn();

                //Assert.AreEqual(approvedAmount, _loanSetUpDetails.VerifyFundedAmount(), "Aprroved amount greater than funded amount");

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
                #endregion
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}
