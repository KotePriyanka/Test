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

namespace Nimble.Automation.FunctionalTest.RegressionTest.Milestone7
{
	[TestFixture, Parallelizable, Category("Milestone7")]
	class TC200_Verify_Calculator_Duration_NL : TestEngine
	{
		[TearDown]
		public void Aftermethod()
		{
			_driver.Quit();
			_result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _personalDetails.EmailID, starttime);
		}

		private HomeDetails _homeDetails = null;
		private LoanPurposeDetails _loanPurposeDetails = null;
		private PersonalDetails _personalDetails = null;
		private LoanCalculatorDetails _loanCalculatorDetails = null;
		private LoanSetUpDetails _loanSetUpDetails = null;
		private BankDetails _bankDetails = null;
		private IWebDriver _driver = null;
		public TestEngine _testengine = new TestEngine();
		string strMessage, strUserType;
		ResultDbHelper _result = new ResultDbHelper();
		DateTime starttime { get; set; } = DateTime.Now;

		[TestCase(550, "android", TestName = "TC200_Verify_Calculator_Duration_NL_550"), Category("NL"), Category("Mobile"), Retry(2)]
		[TestCase(1600, "android", TestName = "TC200_Verify_Calculator_Duration_NL_1600")]
		[TestCase(1950, "android", TestName = "TC200_Verify_Calculator_Duration_NL_1950")]
		[TestCase(2650, "ios", TestName = "TC200_Verify_Calculator_Duration_NL_2650")]
		[TestCase(3800, "ios", TestName = "TC200_Verify_Calculator_Duration_NL_3800")]
		[TestCase(4500, "ios", TestName = "TC200_Verify_Calculator_Duration_NL_4500")]

		public void TC200_Verifying_Calculator_Duration_NL(int loanamout, string strmobiledevice)
		{
			starttime = DateTime.Now;
			strMessage = string.Format("\r\n\t " + TestContext.CurrentContext.Test.Name + " Starts");
			strUserType = "NL";

			try
			{
				_driver = _testengine.TestSetup(strmobiledevice);
				_homeDetails = new HomeDetails(_driver, "NL");
				_loanPurposeDetails = new LoanPurposeDetails(_driver, "NL");
				_loanCalculatorDetails = new LoanCalculatorDetails(_driver, "NL");
				_personalDetails = new PersonalDetails(_driver, "NL");
				_bankDetails = new BankDetails(_driver, "NL");
				_loanSetUpDetails = new LoanSetUpDetails(_driver, "NL");

				ResultDbHelper _resul = new ResultDbHelper();

				// TestContext _testcontext;
				strMessage += string.Format("\r\n\t Browser Launched");

				//Click on Apply Button
				_homeDetails.ClickApplyBtn();

                // Validating ONline calculator Visibility
                if (calculatorIsEnabled == "true")				
				Assert.IsTrue(_loanCalculatorDetails.OnlineCalculatorVisibility(), "Expected Visibility of Online Calculator");                

				//Selecting the loan amount in calculator and verify returned values on calculator
				_loanCalculatorDetails.RequestLoanAmountDurationCalculation(loanamout);

                //Click on Start application
                _homeDetails.ClickStartApplictionBtn();

                // select POL and Amount
                _loanCalculatorDetails.SelectPOLandAmount(loanamout, TestData.POL.Households);

                // entering personal details with overwrite values
                _personalDetails.PopulatePersonalDetails();

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
				_bankDetails.EnterBankDetailsTxt();

				// Click on Confirm account details Continue Button  
				_bankDetails.ClickAcctDetailsBtn();

				strMessage += string.Format("\r\n\t Dag bank Launched");
				// Select Category 
				_bankDetails.SelectIncomecategory(TestData.IncomeCategory.PrimaryIncome, "0");

				// Select Just checking option 
				//_bankDetails.SelectJustCheckingOptionLst("Yes, it will stay the same (or more)");

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

				if (loanamout > 2000)
				{
					// enter sms input as OTP 
					if (_bankDetails.VerifySMSOTP())
						_bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);
				}

                // Verify set up page
                if ((loanamout > 2000 && FinalReviewEnabled == "true") && (strUserType == FinalReviewLoanType || FinalReviewLoanType == "ALL"))
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
                        _loanSetUpDetails.ClickSetup();
                    }
                    else
                    {
                        //ClickOn Loan Dashboard...Manual Approval
                        _loanSetUpDetails.ClickLoanDashboardManual();

                        //click on Final Approve
                        _loanSetUpDetails.FinalApprove();

                        //Click Setup Button
                        _loanSetUpDetails.ClickSetup();
                    }
                }

                // Verify ApprovedAmount
                Assert.IsTrue(_loanSetUpDetails.VerifyApprovedLoan(loanamout), "Expected Requested Amount : " + loanamout + ". Observed Approved Amount : " + _loanSetUpDetails.GetApprovedamount());

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

				// Verify Funded Amount
				Assert.IsTrue(_loanSetUpDetails.VerifyFundedAmount(loanamout), " Expected Requested Amount : " + loanamout + ". Observed Funded Amount : " + _loanSetUpDetails.VerifyFundedAmount());

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
				strMessage += ex.Message;
				Assert.Fail(ex.Message);
			}
		}

	}

	[TestFixture, Parallelizable, Category("Milestone7")]
	class TC200_Verify_Calculator_Duration_RL : TestEngine
	{
		[TearDown]
		public void aftermethod()
		{
			_driver.Quit();
			_result.SendTestResultToDb(TestContext.CurrentContext, strMessage, _homeDetails.RLEmailID, starttime);
		}

		private HomeDetails _homeDetails = null;
		private LoanPurposeDetails _loanPurposeDetails = null;
		private PersonalDetails _personalDetails = null;
		private LoanCalculatorDetails _loanCalculatorDetails = null;
		private LoanSetUpDetails _loanSetUpDetails = null;
		private BankDetails _bankDetails = null;
		private GenerateRandom _randomVal = new GenerateRandom();
		private IWebDriver _driver = null;
		public TestEngine _testengine = new TestEngine();

		string strMessage, strUserType;
		ResultDbHelper _result = new ResultDbHelper();

		DateTime starttime { get; set; } = DateTime.Now;
		[TestCase(550, "android", TestName = "TC200_Verify_Calculator_Duration_RL_550"), Category("RL"), Retry(2)]
		[TestCase(1600, "android", TestName = "TC200_Verify_Calculator_Duration_RL_1600")]
		[TestCase(1950, "android", TestName = "TC200_Verify_Calculator_Duration_RL_1950")]
		[TestCase(2650, "ios", TestName = "TC200_Verify_Calculator_Duration_RL_2650")]
		[TestCase(3800, "ios", TestName = "TC200_Verify_Calculator_Duration_RL_3800")]
		[TestCase(4500, "ios", TestName = "TC200_Verify_Calculator_Duration_RL_4500")]
		public void TC200_Verifying_Calculator_Duration_RL(int loanamout, string strmobiledevice)
		{
			strMessage += string.Format("\r\n\t " + TestContext.CurrentContext.Test.Name + " Starts");
			strUserType = "RL";
			try
			{
				_driver = _testengine.TestSetup(strmobiledevice, "RL");
				_homeDetails = new HomeDetails(_driver, "RL");
				_loanPurposeDetails = new LoanPurposeDetails(_driver, "RL");
				_personalDetails = new PersonalDetails(_driver, "RL");
				_loanCalculatorDetails = new LoanCalculatorDetails(_driver, "RL");
				_bankDetails = new BankDetails(_driver, "RL");
				_loanSetUpDetails = new LoanSetUpDetails(_driver, "RL");

				// Login with existing user
				_homeDetails.LoginExistingUser(TestData.RandomPassword, loanamout, TestData.ClientType.NewProduct, TestData.Feature.NewProductAdvancePaidClean);

				// Click on Request Money Button
				_homeDetails.ClickRequestMoneyBtn();

                // Validating ONline calculator Visibility
                if (calculatorIsEnabled == "true")					
					Assert.IsTrue(_loanCalculatorDetails.OnlineCalculatorVisibility(), "Expected Visibility of Online Calculator");

				//Selecting the loan amount in online calculator
				_loanCalculatorDetails.RequestLoanAmountDurationCalculationRL(loanamout);

				//Click on Start Application Button
				_homeDetails.ClickExistinguserStartApplictionBtn();				

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

					// Click on checks out Continue Button
					_personalDetails.ClickAutomaticVerificationBtn();
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

				// Select Just checking option 
				//_bankDetails.SelectJustCheckingOptionLst("Yes, it will stay the same (or more)");

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

                // Verify set up apge
                if ((loanamout > 2000 && FinalReviewEnabled == "true") && (strUserType == FinalReviewLoanType || FinalReviewLoanType == "ALL"))
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
                        _loanSetUpDetails.ClickSetup();


                    }
                    else
                    {
                        //ClickOn Loan Dashboard...Manual Approval
                        _loanSetUpDetails.ClickLoanDashboardManual();

                        //click on Final Approve
                        _loanSetUpDetails.FinalApprove();

                        //Click Setup Button
                        _loanSetUpDetails.ClickSetup();
                    }
                }

                if (GetPlatform(_driver))
				{
					// click on Button Submit
					_loanSetUpDetails.ClickSubmitBtn();

					// Click on Bank Account to transfer
					// _bankDetails.ClicksixtyMinuteButton();

					// click on sublit-payment Button
					//  _bankDetails.ClickSubmitPaymentButton();
				}
				else
				{
					// Click on Bank Account to transfer
					_bankDetails.ClicksixtyMinuteButton();

					// click on Buton Submit
					_loanSetUpDetails.ClickSubmitBtn();
				}

				//  Scrolling the Loan Contract
				_loanSetUpDetails.Loancontract();

				// Confirming accepting contract
				_loanSetUpDetails.ConfirmAcceptingContract();

				//click on I Agree button
				_loanSetUpDetails.ClickOnAgreeBtn();

				//click on No thanks Button
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

	

