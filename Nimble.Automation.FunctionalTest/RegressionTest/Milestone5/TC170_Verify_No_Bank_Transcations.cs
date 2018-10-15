using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System.Threading;
using System;

namespace Nimble.Automation.FunctionalTest.RegressionTest.Milestone5
{

    [TestFixture, Parallelizable, Category("Milestone5"), Category("Bank")]
    class TC170_Verify_No_Bank_Transcations_NL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(1000, "android", TestName = "TC170_Verify_No_Bank_Transcations_NL_1000"), Category("NL"), Category("Mobile"), Retry(2)]
        [TestCase(2500, "ios", TestName = "TC170_Verify_No_Bank_Transcations_NL_2500")]
        public void TC_170_Verify_No_Bank_Transcations(int loanamout, string strmobiledevice)
        {
            try
            {
                strUserType = "NL";  
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

                _obj.StreetName = "At:N Cr:A Id:100 Rr1:A Rr2:A Rr3:A Bsp:Y Rmsrv:1";

                //populate the personal details and proceed
                _personalDetails.PopulatePersonalDetails(_obj);

                // Click on checks out Continue Button
                _personalDetails.ClickCheckoutContinueBtn();

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.NoTranscations.Yodlee.UID, TestData.BankDetails.NoTranscations.Yodlee.PWD);

                // Click on Continue Button
                _bankDetails.ClickAutoContinueBtn();

                // choose bank account
                _bankDetails.BankAccountSelectBtn();

                // Click on bank select Continue Button
                _bankDetails.ClickBankAccountContBtn();

                //Verify Oops Error
                Assert.AreEqual("Oops! Something went wrong", _bankDetails.GetErrorMessage());

                //Verify Second line of messaged
                Assert.AreEqual("Sorry, little hiccup reaching your bank account. Please try again.", _bankDetails.GetBSErrorMessage());

                //Proviso Error Message
                //// Verify unsuccessful message
                //string UnsuccessMsg = "Oops! Something went wrong";
                //Assert.IsTrue(_personalDetails.GetUnsuccessMessage().Contains(UnsuccessMsg));

                ////verify DNQ Message
                //string ActualDNQMessage = "You currently don" + "'" + "t qualify for a Nimble loan.";
                //Console.WriteLine(_personalDetails.GetDNQMessage());
                //Console.WriteLine(ActualDNQMessage);

                //Assert.IsTrue(_personalDetails.GetDNQMessage().Contains(ActualDNQMessage));//Sorry, you currently don't qualify for a Nimble loan.
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message); strMessage += ex.Message;
            }
        }

    }

    [TestFixture, Parallelizable, Category("Milestone5"), Category("Bank")]
    class TC170_Verify_No_Bank_Transcations_RL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage,strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1000, "android", TestName = "TC170_Verify_No_Bank_Transcations_RL_1000"), Category("RL"), Category("Mobile"), Retry(2)]
        [TestCase(2500, "ios", TestName = "TC170_Verify_No_Bank_Transcations_RL_2500")]
        public void TC_170_Verify_No_Bank_Transcations(int loanamout, string strmobiledevice)
        {
            try
            {
                strUserType = "RL";
                _driver = TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
                _personalDetails = new PersonalDetails(_driver, "RL");
                _bankDetails = new BankDetails(_driver, "RL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

                //Go to the homepage and click the start application button and then the Request money button
                _homeDetails.homeFunctions_RL(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.NewProductAdvancePaidClean);

                //Select the loan amount and purpose and click on continue button
                _loanPurposeDetails.LoanPurposeFunction_RL(loanamout, TestData.POL.Eventcosts.Anniversary);

                //Edit the personal details and change the Rmsrv Code
                _personalDetails.PersonalDetailsFunction_RL(TestData.YourEmployementStatus.FullTime, TestData.ReturnerLoaner, TestData.OverrideCodes.PassAll_RL);

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);

                // Click on Continue Button
                _bankDetails.BankSelectContinueBtn();
                Thread.Sleep(3000);

                // Entering Username and Password
                _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.NoTranscations.Yodlee.UID, TestData.BankDetails.NoTranscations.Yodlee.PWD);

                // Click on Continue Button
                _bankDetails.ClickAutoContinueBtn();

                // choose bank account
                _bankDetails.BankAccountSelectBtn();

                // Click on bank select Continue Button
                _bankDetails.ClickBankAccountContBtn();

                //Verify Oops Error
                Assert.AreEqual("Oops! Something went wrong", _bankDetails.GetErrorMessage());

                //Verify Second line of messaged
                Assert.AreEqual("Sorry, little hiccup reaching your bank account. Please try again.", _bankDetails.GetBSErrorMessage());

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message); strMessage += ex.Message;
            }
        }
    }
}
