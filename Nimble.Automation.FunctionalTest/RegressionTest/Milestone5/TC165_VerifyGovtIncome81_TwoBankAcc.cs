using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest.Milestone5
{
    // class TC165_VerifyGovtIncome81_TwoBankAcc_NL
    [TestFixture, Parallelizable, Category("Milestone5"), Category("GovtIncSAACRL80")]
    class TC165_VerifyGovtIncome81_TwoBankAcc : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(1500, "android", TestName = "TC165_VerifyGovtIncome81_TwoBankAcc_NL_1500"), Category("NL"), Retry(2)]
        [TestCase(3500, "ios", TestName = "TC165_VerifyGovtIncome81_TwoBankAcc_NL_3500")]
        public void TC_164_VerifyGovtIncome81_TwoBankAcc_NL(int loanamout, string strmobiledevice)
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

                // Click on Apply Button
                _homeDetails.ClickApplyBtn();

                // Click on Start Your Application Button
                _homeDetails.ClickStartApplictionBtn();

                bool hideshow = _homeDetails.CheckHideShow();
                if (hideshow == true)
                {
                    if (GetPlatform(_driver))
                    {
                        _loanPurposeDetails.RequestLoanAmountMobile(loanamout, TestData.POL.Eventcosts.Anniversary);
                    }
                    else
                    {
                        _loanPurposeDetails.RequestLoanAmount(loanamout, TestData.POL.Eventcosts.Anniversary);
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
                        _loanPurposeDetails.SelectLoanPurposeMobile(TestData.POL.Eventcosts.Anniversary);
                    }
                    else
                    {
                        // Select Purpose of loan
                        _loanPurposeDetails.SelectLoanPurpose(TestData.POL.Eventcosts.Anniversary);
                    }

                    // Enter FirstPOLLoan Amount
                    _loanPurposeDetails.EnterFirstPOLAmountTxt(loanamout.ToString());

                    // Click on Continue Button
                    _loanPurposeDetails.ClickLoanPOLContinueBtn();
                }

                //_personalDetails.PopulatePersonalDetails();
                _personalDetails.PersonalDetailsFunction();

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.GovtInc48.Yodlee.UID, TestData.BankDetails.GovtInc48.Yodlee.PWD);

                // Click on Continue Button
                _bankDetails.ClickAutoContinueBtn();

                //Click on Add Another BAnk Button
                _bankDetails.ClickAddAnotherBankBtn();

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.GovtInc41.Yodlee.UID, TestData.BankDetails.GovtInc41.Yodlee.PWD);

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
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }

    [TestFixture, Parallelizable, Category("Milestone5"), Category("GovtIncSAACRL80")]
    class TC165_VerifyGovtIncome81_TwoBankAcc_RL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1250, "android", TestName = "TC165_VerifyGovtIncome81_TwoBankAcc_RL_1250"), Category("RL"), Ignore("Need to create two new bank accounts"), Retry(2)]
        [TestCase(2550, "ios", TestName = "TC165_VerifyGovtIncome81_TwoBankAcc_RL_2550")]
        public void TC_164_VerifyGovtIncome81_TwoBankAcc_NL(int loanamout, string strmobiledevice)
        {
            try
            {
                strUserType = "RL";
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
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.GovtInc81.Yodlee.UID, TestData.BankDetails.GovtInc81.Yodlee.PWD);

                // Click on Continue Button
                _bankDetails.ClickAutoContinueBtn();

                //Click on Add Another BAnk Button
                _bankDetails.ClickAddAnotherBankBtn();

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.GovtInc81.Yodlee.UID, TestData.BankDetails.GovtInc81.Yodlee.PWD);

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
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}
