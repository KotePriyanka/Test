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

    // Proviso aka BankStatement test
    // Bank accounts we scrape can have different accountTypes other than the default 'savings'
    // Currently Proviso only supports loans that are new and up to $1600
    // Certain Proviso accounTypes will be accepted, while others will fail with an error on the Bank Login screen

    [TestFixture, Parallelizable, Category("Milestone6"), Category("Proviso")]
    class TC173_Verify_Proviso_AccountTypes_Rejected : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage,strUserType=""; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(400, "android", "TestBank320", "mS4KJLki", TestName = "TC173_Verify_Proviso_AccountType_HomeLoan_NL_400"), Category("NL"), Retry(2)]
        [TestCase(600, "android", "TestBank321", "APfKDGU6", TestName = "TC173_Verify_Proviso_AccountType_Insurance_NL_600")]
        [TestCase(800, "android", "TestBank323", "NuSbGxjl", TestName = "TC173_Verify_Proviso_AccountType_Investments_NL_800")]
        [TestCase(1000, "android", "TestBank327", "5tH3YEtE", TestName = "TC173_Verify_Proviso_AccountType_PersonalLoan_NL_1000")]
        [TestCase(1200, "android", "TestBank329", "vpd1Qj%t", TestName = "TC173_Verify_Proviso_AccountType_Superannuation_NL_1200")]

        public void TC173_Verify_Proviso_AccountTypes_Rejected_NL(int loanamout, string strmobiledevice, string BankUsername, string BankPwd)
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

                PersonalDetailsDataObj _obj = new PersonalDetailsDataObj();

                _obj.StreetName = "At:N Cr:A Id:100 Rr1:A Rr2:A Rr3:A Bsp:BS Rmsrv:1";

                //populate the personal details and proceed
                _personalDetails.PopulatePersonalDetails(_obj);

                // Click on checks out Continue Button
                _personalDetails.ClickCheckoutContinueBtn();

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(BankUsername, BankPwd);

                // Click on Continue Button
                _bankDetails.ClickAutoContinueBtn();

                // Bank Details - check account type is invalid message
                Assert.IsTrue(_bankDetails.CheckBankLoginFailedErrMsgTxt().Contains("It seems the system is experiencing some technical hiccups."));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message); strMessage += ex.Message;
            }
        }
    }
}
