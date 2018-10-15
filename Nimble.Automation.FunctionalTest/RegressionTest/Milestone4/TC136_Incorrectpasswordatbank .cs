using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimble.Automation.FunctionalTest
{
    //<Summary>
    //Incorrect password at Bank
    //</Summary>

    [TestFixture, Parallelizable, Category("Milestone4"), Category("Misc")]
    class TC136_Incorrectpasswordatbank_NL : TestEngine
    {
        string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();

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
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null;
        public TestEngine _testengine = new TestEngine();

        [TestCase(1200, "android", TestName = "TC136_Incorrectpasswordatbank_NL_SACC_1200"), Category("NL"), Category("Mobile"), Retry(2)]
        [TestCase(2900, "ios", TestName = "TC136_Incorrectpasswordatbank_NL_MACC_2900")]
        public void TC136_VerifyIncorrectpasswordatbank_NL(int loanamout, string strmobiledevice)
        {
            strMessage += string.Format("\r\n\t" + TestContext.CurrentContext.Test.Name + " Starts");
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

                // Entering Username and IncorrectPassword
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.UID, TestData.BankDetails.IncorrectPWD);

                // Click on Continue Button
                _bankDetails.ClickAutoContinueBtn();

                // Verify login details incorrect
                string errormsg = "It appears your login details are incorrect.";
                Assert.IsTrue(_bankDetails.GetInvalidloginmsg().Contains(errormsg));
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }

    [TestFixture, Parallelizable, Category("Milestone4"), Category("Misc")]
    class TC136_Incorrectpasswordatbank_RL : TestEngine
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
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TestCase(1500, "android", TestName = "TC136_Incorrectpasswordatbank_RL_SACC_1500"), Category("RL"), Retry(2)]
        [TestCase(2850, "ios", TestName = "TC136_Incorrectpasswordatbank_RL_MACC_2850")]
        public void TC136_VerifyIncorrectpasswordatbank_RL(int loanamout, string strmobiledevice)
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

                //Edit the personal details and change the Rmsrv Code
                _personalDetails.PersonalDetailsFunction_RL(TestData.YourEmployementStatus.FullTime, TestData.ReturnerLoaner, TestData.OverrideCodes.PassAll_RL);

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.UID, TestData.BankDetails.IncorrectPWD);

                // Click on Continue Button
                _bankDetails.ClickAutoContinueBtn();

                // Verify login details incorrect
                string errormsg = "It appears your login details are incorrect.";
                Assert.IsTrue(_bankDetails.GetInvalidloginmsg().Contains(errormsg));
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}
