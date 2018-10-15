using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nimble.Automation.FunctionalTest.RegressionTest.Milestone6
{
    [TestFixture, Parallelizable, Category("Milestone6"), Category("Manual DNQed Client")]
    class TC195_Verify_Manual_DNQed_Client : TestEngine
    {
        private HomeDetails _homeDetails = null;
        private GenerateRandom _randomVal = new GenerateRandom();
        private PersonalDetails _personalDetails = null;
        private LoanSetUpDetails _loanSetUpDetails = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        private BankDetails _bankDetails = null;
        private IWebDriver _driver = null; string strMessage, strUserType; DateTime starttime { get; set; } = DateTime.Now; ResultDbHelper _result = new ResultDbHelper();

        [TearDown]
        public void Cleanup()
        {
            _driver.Quit();
            _result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
        }

        [TestCase(1000, "android", TestName = "TC195_Verify_Manual_DNQed_Client_android_RL"), Category("RL"), Retry(2)]
        [TestCase(2500, "ios", TestName = "TC195_Verify_Manual_DNQed_Client_ios_RL")]
        public void TC195_Verify_Manual_DNQed_Client_RL(int loanamout, string strmobiledevice)
        {
            strMessage += string.Format("\r\n\t " + TestContext.CurrentContext.Test.Name + " Starts");
            strUserType = "RL";
            try
            {
                _driver = TestSetup(strmobiledevice, "RL");
                _homeDetails = new HomeDetails(_driver, "RL");
                _personalDetails = new PersonalDetails(_driver, "RL");
                _loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");
                _loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
                _bankDetails = new BankDetails(_driver, "RL");

                // Login with existing user
                _homeDetails.LoginExistingUser(TestData.RandomPassword, loanamout, TestData.ClientType.NimbleStatus, TestData.Feature.ManualDNQ);

                if (GetPlatform(_driver))
                {
                    //Mobile
                    //Verify message
                    string expectedMessage = "You currently don't qualify for a Nimble loan.";
                    string actualMessage = _homeDetails.getManualDNQMessage();
                    Assert.AreEqual(expectedMessage, actualMessage);

                    //Click on More button
                    _loanSetUpDetails.ClickMoreBtn();

                    //Click on your profile link
                    _homeDetails.ClickMemberAreaEditProfileLnk();

                    //Click on contact details link
                    _personalDetails.ClickContactDetails();

                    //Fetch street name
                    string streetNumber = _personalDetails.FetchStreetNumber();
                    string updateStreetNumber = streetNumber + "1";

                    //Re enter street name 
                    _personalDetails.enterStreetName(updateStreetNumber);

                    //Click on save button
                    _personalDetails.clickContactSaveButtonMob();

                    //Click on To loan dashboard button
                    _loanSetUpDetails.clickDashboardMob();
                }
                else
                {
                    //Desktop
                    string expectedMessage = "Sorry, you currently don't qualify for a Nimble loan.";
                    string actualMessage = _homeDetails.getManualDNQMessage();
                    Assert.AreEqual(expectedMessage, actualMessage);

                    //Click on Edit profile link
                    _homeDetails.ClickMemberAreaEditProfileLnk();

                    //Click on contact details link
                    _personalDetails.ClickContactDetails();

                    //Fetch street name
                    string streetNumber = _personalDetails.FetchStreetNumber();
                    string updateStreetNumber = streetNumber + "1";

                    //Re enter street name 
                    _personalDetails.enterStreetName(updateStreetNumber);

                    //Click on save button
                    _personalDetails.clickSaveButton();

                    //Click on To loan dashboard button
                    _loanSetUpDetails.ToLoanDashboard();
                }

                // Click on Request Money Button
                _homeDetails.ClickRequestMoneyBtn();

                //Click on Start Application Button
                _homeDetails.ClickExistinguserStartApplictionBtn();

                // Select Loan Value from Slide bar
                _loanPurposeDetails.SelectLoanValueRL(loanamout);

                //Click on Select First POL Lst
                _loanPurposeDetails.ClickSelectFirstPurposeBtn();

                // Select Purpose of loan
                _loanPurposeDetails.SelectLoanPurposeRL(TestData.POL.Households);

                // Enter FirstPOLLoan Amount
                _loanPurposeDetails.EnterFirstPOLAmountTxt(loanamout.ToString());

                // Click on Continue Button
                _loanPurposeDetails.ClickLoanPOLContinueBtnRL();

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

                // select Bank Name  
                _bankDetails.SelectBankLst(TestData.BankDetails.Dagbank);
                Thread.Sleep(2000); // wait to click continue button

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
                _bankDetails.EnterBankDetailsTxt();

                // Click on Confirm account details Continue Button  
                _bankDetails.ClickAcctDetailsBtn();

                // Select Category 
                _bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "0");

                // click on Confirm Income Button
                _bankDetails.ClickConfirmIncomeBtn();

                // select  other debt repayments option No 
                _bankDetails.SelectOtherDebtRepaymentsOptionBtn();

                // select dependents 
                _bankDetails.SelectDependantsLst(TestData.Dependents.Zero);

                // Click on continue
                _bankDetails.ClickConfirmExpensesBtn();

                // select Governments benefits option No
                _bankDetails.ClickNoGovtBenefitsbtn();

                // click on Agree that information True
                _bankDetails.ClickAgreeAppSubmitBtn();
                
                // click on confirm Submit button
                _bankDetails.ClickConfirmSummaryBtn();
                Thread.Sleep(3000);

                // Verify set up page            
                if ((loanamout > 2000 && FinalReviewEnabled == "true") && (strUserType == FinalReviewLoanType || FinalReviewLoanType == "ALL"))
                {
                    if (GetPlatform(_driver))
                    {
                        //if (!_loanSetUpDetails.verifyVerifyButton())
                        //{
                        //    //click on Refresh button Approve
                        //    _loanSetUpDetails.clickRefreshButton();

                        //    //Click on verify button
                        //    _loanSetUpDetails.clickVerifyButton();
                        //}
                        //else
                        //{
                            //Click on verify button
                            _loanSetUpDetails.clickVerifyButton();
                        //}

                        Thread.Sleep(5000);
                        // enter sms input as OTP 
                        if (_bankDetails.VerifySMSOTP())
                            _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);

                        //ClickOn Loan Dashboard...Manual Approval
                        _loanSetUpDetails.ClickLoanDashboardManual();

                        // click on More Button from Bottom Menu
                        _loanSetUpDetails.ClickMoreBtn();

                        // click on Approve button
                        _loanSetUpDetails.ClickApproveBtn();

                        //Click Setup Button
                        _loanSetUpDetails.ClickSetup();
                    }
                    else
                    {
                        if (!_loanSetUpDetails.verifyVerifyButton())
                        {
                            //click on Refresh button Approve
                            _loanSetUpDetails.clickRefreshButton();

                            //Click on verify button
                            _loanSetUpDetails.clickVerifyButton();
                        }
                        else
                        {
                            //Click on verify button
                            _loanSetUpDetails.clickVerifyButton();
                        }

                        Thread.Sleep(5000);
                        // enter sms input as OTP 
                        if (_bankDetails.VerifySMSOTP())
                            _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);

                        //ClickOn Loan Dashboard...Manual Approval
                        _loanSetUpDetails.ClickLoanDashboardManual();

                        //click on Final Approve
                        _loanSetUpDetails.FinalApprove();

                        //Click Setup Button
                        _loanSetUpDetails.ClickSetup();
                    }
                }
                else
                {
                    if (GetPlatform(_driver))
                    {
                        //Click on More button
                        _loanSetUpDetails.ClickMoreBtn();

                        //click on Refresh button Approve
                        _loanSetUpDetails.clickRefreshButton();

                        //Click Setup Button
                        _loanSetUpDetails.ClickSetup();
                    }
                    else
                    {
                        //if (!_loanSetUpDetails.verifySetupButton())
                        //{
                        //    Thread.Sleep(2000);
                        //    //click on Refresh button Approve
                        //    _loanSetUpDetails.clickRefreshButton();

                        //    //Click Setup Button
                        //    _loanSetUpDetails.ClickSetup();
                        //}
                        //else
                        //{
                        //    //Click Setup Button
                        //    _loanSetUpDetails.ClickSetup();
                        //}
                    }

                }

                // click on Buton Submit
                _loanSetUpDetails.ClickSubmitBtn();

                //  Scrolling the Loan Contract
                _loanSetUpDetails.Loancontract();

                // Confirming accepting contract
                _loanSetUpDetails.ConfirmAcceptingContract();

                // click on I Agree button
                _loanSetUpDetails.ClickOnAgreeBtn();

                // click on No thanks Button
                _loanSetUpDetails.ClickNothanksBtn();

                if (GetPlatform(_driver))
                {
                    // Click on To Loan Dashboard Button
                    _loanSetUpDetails.ClickMobileLoanDashboardBtn();

                    // click on More Button from Bottom Menu
                    _loanSetUpDetails.ClickMoreBtn();

                    //Logout
                    _loanSetUpDetails.Logout();
                    strMessage += string.Format("\r\n\t Ends");
                }
                else
                {
                    // Click on Loan Dashboard Button
                    _loanSetUpDetails.ClickLoanDashboard();

                    //Logout
                    _loanSetUpDetails.Logout();
                    strMessage += string.Format("\r\n\t Ends");
                }
            }
            catch (Exception ex)
            {
                strMessage += ex.Message; Assert.Fail(ex.Message);
            }
        }
    }
}
