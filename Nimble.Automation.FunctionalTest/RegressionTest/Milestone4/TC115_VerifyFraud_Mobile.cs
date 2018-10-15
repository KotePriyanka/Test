using System;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Nimble.Automation.FunctionalTest.Milestone4
{
    //<Summary>
    //To verify the Fraud suspicious scenario_ fraud mobile number
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone4"), Category("Fraud Suspicious")]
    class TC115_VerifyFraud_Mobile_NL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(1250, "android", TestName = "TC115_VerifyFraud_Mobile_NL_1250"), Category("NL"), Retry(2)]
        [TestCase(4700, "android", TestName = "TC115_VerifyFraud_Mobile_NL_4700")]
        public void TC115_VerifyingFraud_Mobile_NL(int loanamout, string strmobiledevice)
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

                // entering personal details with overwrite values
                PersonalDetailsDataObj _obj = new PersonalDetailsDataObj();

                _obj.MobilePhone = TestData.FraudMobileNo;

                //_personalDetails.PopulatePersonalDetails();
                _personalDetails.PopulatePersonalDetails(_obj);

                //verify DNQ Screen
                Assert.IsTrue(_personalDetails.GetDNQTxt().Contains("Sorry " + _obj.FirstName));

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
    //To verify the Fraud suspicious scenario_ fraud mobile number
    //</Summary>
    [TestFixture, Parallelizable, Category("Milestone4")]
    class TC115_VerifyFraud_Mobile_RL : TestEngine
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

        [TestCase(950, "android", TestName = "TC115_VerifyFraud_Mobile_RL_950"), Category("RL"), Retry(2)]
        [TestCase(2650, "android", TestName = "TC115_VerifyFraud_Mobile_RL_2650")]
        public void TC115_VerifyingFraud_Mobile_RL(int loanamout, string strmobiledevice)
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

                // Fetching First Name
                string Firstname = _loanPurposeDetails.GetFirstName();               

                //Click on ContactDetails
                _personalDetails.ClickContactDetails();

                //enter fraud mobile number
                _personalDetails.EnterFraudMobileNoTxtRL(TestData.FraudMobileNo);

                // select Employement Status
                _personalDetails.SelectEmploymentStatusLst(TestData.YourEmployementStatus.FullTime);

                // select short term loans value as NO
                _personalDetails.ClickNoShortTermLoanStatusBtn();

                // Check Read Privacy and Electronic Authorisation
                _personalDetails.CheckReadPrivacyBtn(TestData.ReturnerLoaner);

                // Check Read Credit Guide
                _personalDetails.CheckReadCreditBtn(TestData.ReturnerLoaner);

                if (GetPlatform(_driver))
                {
                    // Click on Personal Details Continue Button
                    _personalDetails.ClickPersonaldetailsContinueBtnRLMobile();
                }
                else
                {
                    // Click on Personal Details Continue Button
                    _personalDetails.ClickPersonaldetailsRequestBtnRLDesktop();
                }
                
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
