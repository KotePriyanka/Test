using System;
using NUnit.Framework;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using System.Threading;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;


namespace Nimble.Automation.FunctionalTest
{

    class TC005_VerifyRequestedApprovedFundedAmount : ActionEngine
    {
        HomeDetails _homeDetails = new HomeDetails();
        LoanPurposeDetails _loanpurposedetails = new LoanPurposeDetails();
        PersonalDetails _personaldetails = new PersonalDetails();
        LoanSetUpDetails _LoanSetUpDetails = new LoanSetUpDetails();
        BankDetails _bankDetails = new BankDetails();
        Common _common = new Common();
        GenerateRandomValue _randomVal = new GenerateRandomValue();

        [TestCase(1050)]
        [TestCase(3000)]
        public void TC005_VerifyRequestedApprovedFundedAmount_NL(int loanamout)
        {
            // Click on Apply Button
            _homeDetails.ClickApplyBtn();

            // Click on Start Your Application Button
            _homeDetails.ClickStartApplictionBtn();

            // Click on HideShowDebug Button
            _homeDetails.ClickHideShowDebugBtn();

            // Click on PassAllNobs Link
            _homeDetails.ClickPassAllNobslnk();

            // Select Purpose of loan
            _loanpurposedetails.SelectLoanPurpose(TestData.POL.Eventcosts.Birthdayparty);

            // Select Loan Value from Slide bar
            _loanpurposedetails.SelectLoanValue(loanamout);

            // Enter FirstPOLLoan Amount
            _loanpurposedetails.EnterFirstPOLAmountTxt(loanamout.ToString());

            // Click on Continue Button
            _loanpurposedetails.ClickLoanPOLContinueBtn();

            // Enter Email Details
            _personaldetails.EnterEmailTxt(_randomVal.RandomEmail());

            // Enter StreetName Details
            _personaldetails.EnterStreetNameTxt(TestData.OverrideCodes.Approved);

            // select Employement Status
            _personaldetails.SelectEmploymentStatusLst(TestData.YourEmployementStatus.FullTime);

            // select short term loans value as NO
            _personaldetails.ClickNoShortTermLoanStatusBtn();

            // Check Read Privacy and Electronic Authorisation
            _personaldetails.CheckReadPrivacyBtn(TestData.NewLoaner);

            // Check Read Credit Guide
            _personaldetails.CheckReadCreditBtn(TestData.NewLoaner);

            // Click on Personal Details Continue Button
            _personaldetails.ClickPersonaldetailsContinueBtn();

            // Click on checks out Continue Button
            _personaldetails.ClickCheckoutContinueBtn();

            // select Bank Name  
            _bankDetails.SelectBankLst(TestData.BankDetails.DAGbank.Dagbank);

            // Click on Continue Button
            _bankDetails.BankSelectContinueBtn();

            // Entering Username and Password
            _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.DAGbank.UID, TestData.BankDetails.DAGbank.PWD);

            // Click on Continue Button
            _bankDetails.ClickAutoContinueBtn();

            // choose bank account
            _bankDetails.BankAccountSelectBtn();

            // Click on bank select Continue Button
            _bankDetails.ClickBankAccountContBtn();

            // Confirm Bank Details
            _bankDetails.EnterBankDetailsTxt(TestData.BankDetails.DAGbank.BSB, TestData.BankDetails.DAGbank.AccountNumber, TestData.BankDetails.DAGbank.AccountName);

            // Click on Confirm account details Continue Button  
            _bankDetails.ClickAcctDetailsBtn();

            // Select Category 
            _bankDetails.SelectIncomeCategoryLst(TestData.IncomeCategory.PrimaryIncome);

            // Select Just checking option 
            _bankDetails.SelectJustCheckingOptionLst("Yes, it will stay the same (or more)");

            // click on Confirm Income Button
            _bankDetails.ClickConfirmIncomeBtn();

            // select  other debt repayments option No 
            _bankDetails.SelectOtherDebtRepaymentsOptionBtn();

            // select dependents 
            _bankDetails.SelectDependantsLst(TestData.Dependents.Zero);

            // Click on continue
            _bankDetails.ClickConfirmExpensesBtn();

            // select Governments benefits option No
            _bankDetails.ClickGovtBenefitsOptionLst();

            // click on Agree that information True
            _bankDetails.ClickAgreeAppSubmitBtn();

            // click on confirm Submit button
            _bankDetails.ClickConfirmSummaryBtn();

            // enter sms input as OTP 
            _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);

            // Verify ApprovedAmount
            _LoanSetUpDetails.VerifyApprovedLoan(loanamout);

            // Verify Loan Setup Text displayed
            _LoanSetUpDetails.VerifyDisplayedText();

            // click on Buton Submit
            _LoanSetUpDetails.ClickSubmitBtn();

            //  Scrolling the Loan Contract
            _LoanSetUpDetails.Loancontract();

            // Confirming accepting contract
            _LoanSetUpDetails.ConfirmAcceptingContract();

            // click on I Agree button
            _LoanSetUpDetails.ClickOnAgreeBtn();

            // click on No thanks Button
            _LoanSetUpDetails.ClickNothanksBtn();

            // Verify Funded Amount
            _LoanSetUpDetails.VerifyFundedAmount(loanamout);

            // Click on Loan Dashboard Button
            _LoanSetUpDetails.ClickLoanDashboard();
        }

        [TestCase(600)]
        [TestCase(4000)]
        public void TC005_VerifyRequestedApprovedFundedAmount_RL(int loanamout)
        {
            // Click on Login Button 
            _homeDetails.ClickLoginBtn();

            // Login with existing user
            _homeDetails.LoginExistingUser(TestData.RandomPassword);

            // Click on Request Money Button
            _homeDetails.ClickRequestMoneyBtn();

            //Click on Start Application Button
            _homeDetails.ClickExistinguserStartApplictionBtn();

            //Click on Select First POL Lst
            _homeDetails.ClickandSelectFirstPOL();

            // Select Purpose of loan
            _loanpurposedetails.SelectLoanPurpose(TestData.POL.Eventcosts.Birthdayparty);

            // Select Loan Value from Slide bar
            _loanpurposedetails.SelectLoanValue(loanamout);

            // Enter FirstPOLLoan Amount
            _loanpurposedetails.EnterFirstPOLAmountTxt(loanamout.ToString());

            // Click on Continue Button
            _loanpurposedetails.ClickLoanPOLContinueBtn();

            // select Employement Status
            _personaldetails.SelectEmploymentStatusLst(TestData.YourEmployementStatus.FullTime);

            // select short term loans value as NO
            _personaldetails.ClickNoShortTermLoanStatusBtn();

            // Check Read Privacy and Electronic Authorisation
            _personaldetails.CheckReadPrivacyBtn(TestData.ReturnerLoaner);

            // Check Read Credit Guide
            _personaldetails.CheckReadCreditBtn(TestData.ReturnerLoaner);

            // Click on Personal Details Continue Button
            _personaldetails.ClickPersonaldetailsContinueBtn();

            // Click on checks out Continue Button
            _personaldetails.ClickAutomaticVerificationBtn();

            // select Bank Name  
            _bankDetails.SelectBankLst(TestData.BankDetails.DAGbank.Dagbank);

            // Click on Continue Button
            _bankDetails.BankSelectContinueBtn();

            // Entering Username and Password
            _bankDetails.EnterBankCredentialsTxt(TestData.BankDetails.DAGbank.UID, TestData.BankDetails.DAGbank.PWD);

            // Click on Continue Button
            _bankDetails.ClickAutoContinueBtn();

            // choose bank account
            _bankDetails.BankAccountSelectBtn();

            // Click on bank select Continue Button
            _bankDetails.ClickBankAccountContBtn();

            // Confirm Bank Details
            _bankDetails.EnterBankDetailsTxt(TestData.BankDetails.DAGbank.BSB, TestData.BankDetails.DAGbank.AccountNumber, TestData.BankDetails.DAGbank.AccountName);

            // Click on Confirm account details Continue Button  
            _bankDetails.ClickAcctDetailsBtn();

            // Select Category 
            _bankDetails.SelectIncomeCategoryLst(TestData.IncomeCategory.PrimaryIncome);

            // Select Just checking option 
            _bankDetails.SelectJustCheckingOptionLst("Yes, it will stay the same (or more)");

            // click on Confirm Income Button
            _bankDetails.ClickConfirmIncomeBtn();

            // select  other debt repayments option No 
            _bankDetails.SelectOtherDebtRepaymentsOptionBtn();

            // select dependents 
            _bankDetails.SelectDependantsLst(TestData.Dependents.Zero);

            // Click on continue
            _bankDetails.ClickConfirmExpensesBtn();

            // select Governments benefits option No
            _bankDetails.ClickGovtBenefitsOptionLst();

            // click on Agree that information True
            _bankDetails.ClickAgreeAppSubmitBtn();

            // click on confirm Submit button
            _bankDetails.ClickConfirmSummaryBtn();

            // enter sms input as OTP 
            _bankDetails.EnterOTPDetailsTxt(TestData.SMSCode);

            // Verify ApprovedAmount
            _LoanSetUpDetails.VerifyApprovedLoan(loanamout);

            // Verify Loan Setup Text displayed
            _LoanSetUpDetails.VerifyDisplayedText();

            // Click on Bank Account to transfer
            _bankDetails.ClicksixtyMinuteButton();

            // click on Buton Submit
            _LoanSetUpDetails.ClickSubmitBtn();

            // Select Reason for Spend Less
            _LoanSetUpDetails.SelectReasontospendLess(TestData.ReasonforspeandLess.cheaperproduct);

            //  Scrolling the Loan Contract
            _LoanSetUpDetails.Loancontract();

            // Confirming accepting contract
            _LoanSetUpDetails.ConfirmAcceptingContract();

            // click on I Agree button
            _LoanSetUpDetails.ClickOnAgreeBtn();

            // click on No thanks Button
            _LoanSetUpDetails.ClickNothanksBtn();

            // Verify Funded Amount
            _LoanSetUpDetails.VerifyFundedAmount(loanamout);

            // Click on Loan Dashboard Button
            _LoanSetUpDetails.ClickLoanDashboard();
        }

    }
}

















