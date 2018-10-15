using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest
{
    //<Summary>
    //Verifying the DNQ scenario if the DSC of the user is less than 5..
    //</Summary>

    [TestFixture, Parallelizable, Category("Milestone3"), Category("DSC")]
    class TC073_VerifyDNQ_DSC_NL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(1100, "android", TestName = "TC073_VerifyDNQ_DSC_NL_1100"), Category("NL"), Retry(2)]
        [TestCase(3800, "ios", TestName = "TC073_VerifyDNQ_DSC_NL_3800")]
        public void TC073_VerifyDNQ_DSC5_NL(int loanamout, string mobiledevice)
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
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.LowIncomeLowExpenses.Yodlee.UID, TestData.BankDetails.LowIncomeLowExpenses.Yodlee.PWD);

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


    [TestFixture, Parallelizable, Category("Milestone3"), Category("DSC")]
    class TC073_VerifyDNQ_DSC_RL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }


        [TestCase(1500, "android", TestName = "TC073_VerifyDNQ_DSC_RL_1500"), Category("RL"), Retry(2)]
        [TestCase(2450, "ios", TestName = "TC073_VerifyDNQ_DSC_RL_2450")]
        public void TC073_VerifyDNQ_DSC5_RL(int loanamout, string mobiledevice)
        {
            strUserType = "RL";
            _driver = _testengine.TestSetup(mobiledevice, "RL");
            _homeDetails = new HomeDetails(_driver, "RL");
            _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
            _personalDetails = new PersonalDetails(_driver, "RL");
            _bankDetails = new BankDetails(_driver, "RL");

            try
            {
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
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.LowIncomeLowExpenses.Yodlee.UID, TestData.BankDetails.LowIncomeLowExpenses.Yodlee.PWD);

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













