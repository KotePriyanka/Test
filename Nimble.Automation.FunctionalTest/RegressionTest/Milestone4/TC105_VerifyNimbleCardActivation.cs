using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest
{
    //<Summary>
    //Verifying the nimble card Activation
    //</Summary>


    [TestFixture, Parallelizable, Category("Milestone4"), Category("Nimble Card")]
    class TC105_VerifyNimbleCardActivation_NL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        [TestCase(1050, "android", TestName = "TC105_VerifyNimbleCardActivation_NL_1050"), Category("NL"), Ignore("Nimble card removed from application"), Retry(2)]
        [TestCase(2850, "ios", TestName = "TC105_VerifyNimbleCardActivation_NL_2850")]
        public void TC105_VerifyNimblecardActivation_NL(int loanamout, string strmobiledevice)
        {
            strUserType = "NL";  
            _driver = _testengine.TestSetup(strmobiledevice);
            _homeDetails = new HomeDetails(_driver, "NL");
            _loanPurposeDetails = new LoanPurposeDetails(_driver, "NL");
            _personalDetails = new PersonalDetails(_driver, "NL");
            _bankDetails = new BankDetails(_driver, "NL");
            _loanSetUpDetails = new LoanSetUpDetails(_driver, "NL");

            try
            {
                //Go to the homepage and click the start application button
                _homeDetails.HomeDetailsPage();

                //Select the loan amount and purpose and click on continue button
                _loanPurposeDetails.LoanPurposeFunction(loanamout, TestData.POL.Households);

                //populate the personal details and proceed
                _personalDetails.PersonalDetailsFunction();

                //Fill Up all the required bank details and submits the application
                _bankDetails.bankFunctions(TestData.BankDetails.Dagbank, TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.UID, TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.PWD, TestData.IncomeCategory.PrimaryIncome, TestData.Dependents.Zero, TestData.SMSCode, loanamout);

                if (loanamout > 2000 && FinalReviewEnabled == "true")
                {
                    if (GetPlatform(_driver))
                    {
                        //ClickOn Loan Dashboard...Manual Approval
                        _loanSetUpDetails.ClickLoanDashboardManual();

                        // click on More Button from Bottom Menu
                        _loanSetUpDetails.ClickMoreBtn();

                        // click on Approve button
                        _loanSetUpDetails.ClickApproveBtn();

                        //Click Setup Button
                        _bankDetails.ClickSetup();

                        // Verify ApprovedAmount
                        Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout), "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());

                        // click on Buton Submit
                        _loanSetUpDetails.ClickSubmitBtn();
                    }
                    else
                    {
                        //ClickOn Loan Dashboard...Manual Approval
                        _loanSetUpDetails.ClickLoanDashboardManual();

                        //click on Final Approve
                        _loanSetUpDetails.FinalApprove();

                        //Click Setup Button
                        _bankDetails.ClickSetup();

                        // Verify ApprovedAmount
                        Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout), "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());

                        // click on Buton Submit
                        _loanSetUpDetails.ClickSubmitBtn();
                    }
                }
                else
                {
                    // Verify ApprovedAmount
                    Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout), "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());

                    // click on Buton Submit
                    _loanSetUpDetails.ClickSubmitBtn();
                }

                //  Scrolling the Loan Contract
                _loanSetUpDetails.Loancontract();

                // Confirming accepting contract
                _loanSetUpDetails.ConfirmAcceptingContract();

                // click on I Agree button
                _loanSetUpDetails.ClickOnAgreeBtn();

                // click on wants a visacard Rbtn
                _loanSetUpDetails.ClickAndSaveVisaCardRbtn();

                //click on Nimble card submit button
                _loanSetUpDetails.ClickOnNimbleCardSubmit();

                if (GetPlatform(_driver))
                {
                    // Click on To Loan Dashboard Button
                    _loanSetUpDetails.ClickMobileLoanDashboardBtn();

                    // click on More Button from Bottom Menu
                    _loanSetUpDetails.ClickMoreBtn();

                    string email = _loanSetUpDetails.getEmail();
                    string password = "password";

                    //Logout
                    _loanSetUpDetails.Logout();

                    //login again
                    _homeDetails.LoginLogoutUser(email, password);

                    //activate the card
                    _loanSetUpDetails.ActivateCard();

                   // Assert.IsTrue(_loanSetUpDetails.ActivateCard(), "Card Not Activated");

                    //logout
                    _loanSetUpDetails.Logout();
                }
                else
                {
                    // Click on Loan Dashboard Button
                    _loanSetUpDetails.ClickLoanDashboard();

                     string email = _loanSetUpDetails.getEmail();
                    string password = "password";

                    //Logout
                    _loanSetUpDetails.Logout();

                    //login again
                    _homeDetails.ReLoginUser(email, password);

                    //activate the card
                    _loanSetUpDetails.ActivateCard();

                    //logout
                    _loanSetUpDetails.Logout();
                }
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }

    [TestFixture, Parallelizable, Category("Milestone4"), Category("Nimble Card")]
    class TC105_VerifyNimbleCardActivation_RL : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private BankDetails _bankDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private IWebDriver _driver = null;string strMessage,strUserType;  DateTime starttime { get; set; } = DateTime.Now;   ResultDbHelper _result = new ResultDbHelper();
        public TestEngine _testengine = new TestEngine();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(950, "android", TestName = "TC105_VerifyNimbleCardActivation_RL_950"), Category("RL"),Ignore("Nimble card removed from application"), Retry(2)]
        [TestCase(2950, "ios", TestName = "TC105_VerifyNimbleCardActivation_RL_2950")]
        public void TC105_VerifyNimblecardActivation_RL(int loanamout, string strmobiledevice)
        {
             strUserType = "RL";
            _driver = _testengine.TestSetup(strmobiledevice,"RL");
            _homeDetails = new HomeDetails(_driver, "RL");
            _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
            _personalDetails = new PersonalDetails(_driver, "RL");
            _bankDetails = new BankDetails(_driver, "RL");
            _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

            try
            {
                //Go to the homepage and click the start application button and then the Request money button
                _homeDetails.homeFunctions_RL(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.NewProductAdvancePaidClean);

                //Select the loan amount and purpose and click on continue button
                _loanPurposeDetails.LoanPurposeFunction_RL(loanamout, TestData.POL.Households);

                //Edit the personal details and change the Rmsrv Code
                _personalDetails.PersonalDetailsFunction_RL(TestData.YourEmployementStatus.FullTime, TestData.ReturnerLoaner, TestData.OverrideCodes.PassAll_RL);

               //Fill the bank details and reach the setup page
                _bankDetails.bankFunctions_RL(TestData.BankDetails.Dagbank, TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.UID, TestData.BankDetails.AUTOTriggerAllNoSACC.Yodlee.PWD, TestData.IncomeCategory.PrimaryIncome, TestData.Dependents.Zero);              

                //  Scrolling the Loan Contract
                _loanSetUpDetails.Loancontract();

                // Confirming accepting contract
                _loanSetUpDetails.ConfirmAcceptingContract();

                // click on I Agree button
                _loanSetUpDetails.ClickOnAgreeBtn();

                // click on wants a visacard Rbtn
                _loanSetUpDetails.ClickAndSaveVisaCardRbtn();

                //click on Nimble card submit button
                _loanSetUpDetails.ClickOnNimbleCardSubmit();

                if (GetPlatform(_driver))
                {
                    // Click on To Loan Dashboard Button
                    _loanSetUpDetails.ClickMobileLoanDashboardBtn();

                    // click on More Button from Bottom Menu
                    _loanSetUpDetails.ClickMoreBtn();

                    string email = _loanSetUpDetails.getEmail();
                    string password = "password";

                    //Logout
                    _loanSetUpDetails.Logout();

                    //login again
                    _homeDetails.LoginLogoutUser(email, password);

                    //activate the card
                    _loanSetUpDetails.ActivateCard();

                    //logout
                    _loanSetUpDetails.Logout();
                }
                else
                {
                    // Click on Loan Dashboard Button
                    _loanSetUpDetails.ClickLoanDashboard();

                    string email = _loanSetUpDetails.getEmail();
                    string password = "password";

                    //Logout
                    _loanSetUpDetails.Logout();

                    //login again
                    _homeDetails.ReLoginUser(email, password);

                    //activate the card                 
                   // Assert.IsTrue(_loanSetUpDetails.ActivateCard(), "Card Not Activated");

                    //logout
                    _loanSetUpDetails.Logout();                   
                }
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}
