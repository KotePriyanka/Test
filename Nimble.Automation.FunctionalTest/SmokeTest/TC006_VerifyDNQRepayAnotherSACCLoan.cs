using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using System;

namespace Nimble.Automation.FunctionalTest
{
    [TestFixture, Parallelizable, Category("SmokeTest")]
    class TC006_VerifyDNQRepayAnotherSACCLoan_NL : TestEngine
    {
        [TearDown]
        public void aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
        }

        string strMessage, strUserType="";
        ResultDbHelper _result = new ResultDbHelper();

        DateTime starttime { get; set; } = DateTime.Now;

        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private IWebDriver _driver = null;

        [TestCase(1050, "android", TestName = "TC006_VerifyDNQRepayAnotherSACCLoan_NL_1050"), Category("NL"), Category("Mobile"), Retry(2)]
        [TestCase(3600, "ios", TestName = "TC006_VerifyDNQRepayAnotherSACCLoan_NL_3600")]
        public void TC006_DNQRepayAnotherSACCLoan_NL(int loanamout, string strmobiledevice)
        {
            strUserType = "NL";
            try
            {
                _driver = TestSetup(strmobiledevice);
                _homeDetails = new HomeDetails(_driver, "NL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "NL");
                _personalDetails = new PersonalDetails(_driver, "NL");
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
                        _loanPurposeDetails.RequestLoanAmountMobile(loanamout, TestData.POL.Partlyfullyrepayacurrentshorttermloan);
                    }
                    else
                    {
                        _loanPurposeDetails.RequestLoanAmount(loanamout, TestData.POL.Partlyfullyrepayacurrentshorttermloan);
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
                        _loanPurposeDetails.SelectLoanPurposeMobile(TestData.POL.Partlyfullyrepayacurrentshorttermloan);
                    }
                    else
                    {
                        // Select Purpose of loan
                        _loanPurposeDetails.SelectLoanPurpose(TestData.POL.Partlyfullyrepayacurrentshorttermloan);
                    }

                    // Enter FirstPOLLoan Amount
                    _loanPurposeDetails.EnterFirstPOLAmountTxt(loanamout.ToString());

                    // Click on Continue Button
                    _loanPurposeDetails.ClickLoanPOLContinueBtn();
                }

                // entering personal details with random values     
                PersonalDetailsDataObj PersonalDetils = _personalDetails.PopulatePersonalDetails();

                // Verify unsuccessful message
                string UnsuccessMsg = "Application unsuccessful";
                Assert.IsTrue(_personalDetails.GetUnsuccessMessage().Contains(UnsuccessMsg));

                //verify DNQ Message                
                string ActualDNQMessage = "You currently don" + "'" + "t qualify for a Nimble loan.";
                Assert.IsTrue(_personalDetails.GetDNQMessage().Contains(ActualDNQMessage));//Sorry, you currently don't qualify for a Nimble loan.

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message); strMessage += ex.Message;

            }
        }
    }

    [TestFixture, Parallelizable, Category("SmokeTest")]
    class TC006_VerifyDNQRepayAnotherSACCLoan_RL : TestEngine
    {
        [TearDown]
        public void aftermethod()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        string strMessage, strUserType;
        ResultDbHelper _result = new ResultDbHelper();
        ResultDbHelper _resul = new ResultDbHelper();
        DateTime starttime { get; set; } = DateTime.Now;

        private HomeDetails _homeDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private IWebDriver _driver = null;

        [TestCase(1550, "android", TestName = "TC006_VerifyDNQRepayAnotherSACCLoan_RL_1550"), Category("RL"), Category("Mobile"), Retry(2)]
        [TestCase(2950, "ios", TestName = "TC006_VerifyDNQRepayAnotherSACCLoan_RL_2950")]
        public void TC006_DNQRepayAnotherSACCLoan_RL(int loanamout, string strmobiledevice)
        {
            strUserType = "RL";
            try
            {
                _driver = TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
                _personalDetails = new PersonalDetails(_driver, "RL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

                // Login with existing user
                _homeDetails.LoginExistingUser(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.NewProductAdvancePaidClean);

                // Click on Request Money Button
                _homeDetails.ClickRequestMoneyBtn();

                //Click on Start Application Button
                _homeDetails.ClickExistinguserStartApplictionBtn();

                // Select Loan Value from Slide bar
                _loanPurposeDetails.SelectLoanValueRL(loanamout);

                //Click on Select First POL Lst
                _loanPurposeDetails.ClickSelectFirstPurposeBtn();

                // Select Purpose of loan
                _loanPurposeDetails.SelectLoanPurposeRL(TestData.POL.Partlyfullyrepayacurrentshorttermloan);

                // Enter FirstPOLLoan Amount
                _loanPurposeDetails.EnterFirstPOLAmountTxt(loanamout.ToString());

                // Click on Continue Button
                _loanPurposeDetails.ClickLoanPOLContinueBtnRL();

                // Fetching First Name
                //string Firstname = _loanPurposeDetails.GetFirstName();

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

                // Verify unsuccessful message
                string UnsuccessMsg = "Application unsuccessful";
                Assert.IsTrue(_personalDetails.GetUnsuccessMessage().Contains(UnsuccessMsg));

                //verify DNQ Message                
                string ActualDNQMessage = "You currently don" + "'" + "t qualify for a Nimble loan.";
                Assert.IsTrue(_personalDetails.GetDNQMessage().Contains(ActualDNQMessage));//Sorry, you currently don't qualify for a Nimble loan.
            }
            catch (Exception ex)
            {

                Assert.Fail(ex.Message); strMessage += ex.Message;

            }
        }
    }
}


















