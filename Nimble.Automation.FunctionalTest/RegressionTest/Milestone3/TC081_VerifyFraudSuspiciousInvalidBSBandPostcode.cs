using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Nimble.Automation.FunctionalTest.Milestone3
{
    //<Summary>
    //To verify the Financial Table,Payment of Loan,Other provisions content in the "Confirm Page"
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Fraud Suspicious")]
    class TC081_VerifyFraudSuspiciousInvalidBSBandPostcode_NL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(1250, "android", TestName = "TC081_VerifyFraudSuspiciousInvalidBSBandPostcode_NL_1250"), Category("NL"), Retry(2)]
        [TestCase(4900, "android", TestName = "TC081_VerifyFraudSuspiciousInvalidBSBandPostcode_NL_4900")]
        public void TC081_VerifyingFraudSuspiciousInvalidBSBandPostcode_NL(int loanamout, string strmobiledevice)
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

                // entering personal details with random values     
                PersonalDetailsDataObj PersonalDetils = _personalDetails.PopulatePersonalDetails();

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
                _bankDetails.EnterInvalidBankDetailsTxt();             

                // Click on Confirm account details Continue Button  
                _bankDetails.ClickAcctDetailsBtn();

                //verify DNQ Screen
                Assert.IsTrue(_personalDetails.GetDNQTxt().Contains("Sorry " + PersonalDetils.FirstName));

                //verify DNQ Message
                string ActualDNQMessage = "We're sorry, you didn't qualify for a Nimble loan today.";
                Assert.IsTrue(_personalDetails.GetDNQMessage().Contains(ActualDNQMessage));
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
          
        }
    }

    //<Summary>
    //To verify the Financial Table,Payment of Loan,Other provisions content in the "Confirm Page"
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone3"), Category("Fraud Suspicious")]
    class TC081_VerifyFraudSuspiciousInvalidBSBandPostcode_RL : TestEngine
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

        [TestCase(900, "android", TestName = "TC081_VerifyFraudSuspiciousInvalidBSBandPostcode_RL_900"), Category("RL"), Retry(2)]
        [TestCase(2200, "android", TestName = "TC081_VerifyFraudSuspiciousInvalidBSBandPostcode_RL_2200")]
        public void TC081_VerifyingFraudSuspiciousInvalidBSBandPostcode_RL(int loanamout, string strmobiledevice)
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
                _homeDetails.homeFunctions_RL(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.ReturnerDagBankstaging);

                //Select the loan amount and purpose and click on continue button
                _loanPurposeDetails.LoanPurposeFunction_RL(loanamout, TestData.POL.Households);

                // Fetching First Name
                string Firstname = _loanPurposeDetails.GetFirstName();

                //Edit the personal details and change the Rmsrv Code
                _personalDetails.PersonalDetailsFraudBSB_RL(TestData.YourEmployementStatus.FullTime, TestData.ReturnerLoaner, TestData.OverrideCodes.PassAll_RL);

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
                _bankDetails.EnterInvalidBankDetailsTxt();

                // Click on Confirm account details Continue Button  
                _bankDetails.ClickAcctDetailsBtn();

                //verify DNQ Screen
                Assert.IsTrue(_personalDetails.GetDNQTxt().Contains("Sorry " + Firstname));

                //verify DNQ Message
                string ActualDNQMessage = "We're sorry, you didn't qualify for a Nimble loan today.";
                Assert.IsTrue(_personalDetails.GetDNQMessage().Contains(ActualDNQMessage));
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
            
        }
    }
}
