using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimble.Automation.Repository
{
    public interface IBankDetails
    {
        By BankName { get; }

        By ChooseBank { get; }

        By YodleeContinueButton { get; }

        By BankUsername { get; }

        By BankPassword { get; }

        By verifyautocontinuebutton { get; }

        By BankLoginFailedErrMsg { get; }

        By BankSelectRBtn { get; }

        By btnSelectAccount { get; }

        By ExistingEmailError { get; }

        By AddAnotherBank { get; }

        By AccountListOPtion1 { get; }

        By AccountListOPtion2 { get; }

        // By GenerateStatementContinue { get; }
        By BSB { get; }

        By AccountNumber { get; }

        By NameonAccount { get; }

        By VerifyManualOpen { get; }

        By ContinueConfirmAcctDetails { get; }

        By IncomeCategory { get; }

        By JustChecking { get; }

        By ConfirmIncomeButton { get; }

        By OtherDebtLiabilitiesoption { get; }

        By Dependants { get; }

        By BtnConfirmExpenses { get; }

        By GovtBenefits { get; }

        By AgreeApplicationsubmit { get; }

        By BtnConfirmSummary { get; }

        By SMSInput { get; }

        By SMSDiv { get; }

        By SubmitPin { get; }

        By WaitMessage { get; }

        By LoanDashboardButton { get; }

        By SixtyMinuteButton { get; }

        By SubmitPaymentBtn { get; }

        By VisitFullWebsite { get; }

        By DetailsContinueBtn { get; }

        By UploadLink { get; }

        By ConfirmCSVFile { get; }

        By UploadContinueBtn { get; }

        By OtherShortTermLoansLbl { get; }

        By FullyrepaidOtherLoanYesBtn { get; }

        By FullyrepaidOtherLoanNoBtn { get; }

        By UpToDatewithReaymentsYes { get; }

        By UpToDatewithReaymentsNo { get; }

        By IntendToUseNimbleLoanYes { get; }

        By IntendToUseNimbleLoanNo { get; }

        By ConfirmSACCNamesBtn { get; }

        By IncomeCategoryDeposit1 { get; }

        By IncomeCategoryDeposit2 { get; }

        By JustCheckingDeposit1 { get; }

        By JustCheckingDeposit2 { get; }

        By JustCheckingQuestionDepo1Tans1 { get; }

        By JustCheckingQuestionDepo1Tans2 { get; }

        By JustCheckingQuestionDepo1Tans1Amt { get; }

        By JustCheckingQuestionDepo1Tans2Amt { get; }

        By SevenIncomeSouce1 { get; }

        By SevenIncomeSouce2 { get; }

        By SevenIncomeSouce3 { get; }

        By SevenIncomeSouce4 { get; }

        By SevenIncomeSouce5 { get; }

        By SevenIncomeSouce6 { get; }

        By SevenIncomeSouce7 { get; }

        By BankSelectRBtnExact { get; }

        By LoanDashboardManual { get; }

        By FinalApprove { get; }

        By SetupManual { get; }

        By GovtBenefitsYes { get; }

        By GovtDNQText { get; }

        By PleaseSelectReasonForOther { get; }

        By ClickVerifyButtonRL { get; }

        By AcctBSBLastPage { get; }

        By CheckIdAuthorisationChkbx { get; }

        By ButtonSubmit { get; }

        // spike  xpaths

        By SpikeText { get; }

        By SpikeQuestionOption { get; }

        By IdentifiedSpikeTransactionDate { get; }

        By IdentifiedSpikeTransactionDescription { get; }

        By IdentifiedSpikeTransactionAmount { get; }

        By OtherReasonSpike { get; }

        // OOC  xpaths

        By OOCText { get; }

        By OOCQuestionOption { get; }

        By IdentifiedOOCTransactionDate { get; }

        By IdentifiedOOCTransactionDescription { get; }

        By IdentifiedOOCTransactionAmount { get; }

        By OtherReasonOOC { get; }

        By TransactionfromGroup { get; }

        //transaction list
        By RemainTransactionText { get; }

        By IdTypeLst { get; }

        By IdMedicareCardNumberTxt { get; }

        By IdMedicareRefNoTxt { get; }

        By IdMedicareCardColourLst { get; }

        By IdMedicareCardNameTxt { get; }

        By IdMedicareExpiryDateMonthLst { get; }

        By IdMedicareExpiryDateYearLst { get; }

        By ClickIdSubmitBtn { get; }

        By IdAustralianPassportNumberTxt { get; }

        By IdAustralianPassportCountyBirthLst { get; }

        By IdAustralianPassportPlaceOfBirthTxt { get; }

        By IdAustralianPassportSurnameAtBirthTxt { get; }

        By IdAustralianPassportSurnameAtCitizenshipTxt { get; }

        By IdInternationalPassportNumberTxt { get; }

        By IdInternationalPassportCountryLst { get; }

        By IdDriversLicenceNameTxt { get; }

        By IdDriversLicenceStateLst { get; }

        By IdDriversLicenceNumberTxt { get; }

        By EditIncome { get; }

        By EditExpenses { get; }

        By EnterLivingExpenses { get; }

        By EnterOtherDebtRepayments { get; }

        By EnterMortgageRepayments { get; }

        By EnterLargeOngoingBills { get; }

        By SelectNoRentReason { get; }

        By EnterNoRentOtherTxt { get; }

        By CompareSummaryLivingExpensesTxt { get; }

        By CompareSummaryOtherDebtTxt { get; }

        By CompareSummaryRentMortgageTxt { get; }

        By CompareSummaryLargeOngoingBillsTxt { get; }

        By LastIncome { get; }

        By SessionTimeout { get; }

        By UpcomingRepaymentFinalPage { get; }

        By UpcomingRepaymentFirstPage { get; }

        By MissedRepaymentFinalPage { get; }

        By RescheduleMessage { get; }

        By MissedRepaymentMessage { get; }

        By MissedRepaymentFirstPage { get; }

        By RescheduleButton { get; }

        By DividePayment { get; }

        By ExtentPayment { get; }

        By RescheduleContinue { get; }

        By UpcomingRepaymentFinalPageExtend { get; }

        By UpcomingRepaymentFirstPageExtend { get; }

        By NoTransactionOptions { get; }

        By NoTransactionContinue { get; }

        By OtherReason { get; }

        By ViewTransactions { get; }

        By ExpenseAmount { get; }

        By CashAdvanceSelect { get; }

        By SunshineLoans { get; }

        By SunshineLoansSelect { get; }

        By CashTrain { get; }

        By CashTrainSelect { get; }

        By PaidLoansNo { get; }

        By UptoDateNo { get; }

        By PaidLoansContinue { get; }

        By Bankloginerrormsg { get; }

        By LoanRepaid { get; }

        By PaymentUnsuccessful { get; }

        By IncorrectCardNo { get; }

        By IncorrectDate { get; }

        By CheckingErrorMsg { get; }

        By NoPrimaryContinueBtn { get; }

        By NoPrimaryBackBtn { get; }

        By OopsError { get; }

        By BSError { get; }

        By CheckMaskedAccountName { get; }

        By CheckMaskedAccountNumber { get; }

        By CheckMaskedAccountType { get; }

        By ANZRegNumber { get; }

        By ANZPassword { get; }

        By ANZOperatorId { get; }

        By ALMErrorMessage { get; }

        By prefailUpcomingRepaymentFirstPage { get; }

        By PrefailUpcomingRepaymentFinalPage { get; }

        By PrefailUpcomingRepaymentFirstPageExtend { get; }

        By PrefailUpcomingRepaymentLastPageExtend { get; }

        By finishButton { get; }

        By Timeline { get; }
    }

    public class BankDetailsMobileNLLoc : IBankDetails
    {
        public By BankName => By.XPath(".//*[@id='bankid']");

        public By ChooseBank => By.XPath("//*[text()='Dagbank']");

        public By YodleeContinueButton => By.XPath(".//*[@id='btnConfirmYodlee']");

        public By BankUsername => By.XPath("(//div[@id='bankFields']//input)[last()-1]");//rev

        public By BankPassword => By.XPath("(//div[@id='bankFields']//input)[last()]");//rev

        public By verifyautocontinuebutton => By.XPath(".//*[@id='btnVerifyAuto']");

        public By BankLoginFailedErrMsg => By.XPath(".//*[@id='bankFieldsAdditional_error']");

        public By BankSelectRBtn => By.XPath(".//td[@class='formfieldlabel width-10']/div");

        public By BankSelectRBtnExact => By.XPath(".//*[@class='block-center radio large white-bg']");

        public By btnSelectAccount => By.XPath(".//*[@id='btnSelectAccount']");

        public By ExistingEmailError => By.XPath(".//div[@class='error-wrap']/p");

        public By AddAnotherBank => By.XPath(".//button[@id='btnAddAnotherBank']");

        public By AccountListOPtion1 => By.XPath("(.//*[@id='accountList']//input[@name='accountListOptions']/following::div[@class='radio white-bg '])[last()-1]");

        public By AccountListOPtion2 => By.XPath("(.//*[@id='accountList']//input[@name='accountListOptions']/following::div[@class='radio'])[last()]");

        // public By GenerateStatementContinue => By.XPath(".//*[@class='block-center radio large sprite-hp i-40-tick-circle noborder']");
        public By BSB => By.XPath(".//*[@id='bankbsb'][not(ancestor::div[contains(@class,'hidden')])]"); //.//*[@id='bankbsb']

        public By AccountNumber => By.XPath(".//*[@id='bankaccountnumber'][not(ancestor::div[contains(@class,'hidden')])]");

        public By NameonAccount => By.XPath(".//*[@id='bankaccountname'][not(ancestor::div[contains(@class,'hidden')])]");

        public By ContinueConfirmAcctDetails => By.XPath(".//*[@id='btnConfirmAccountDetails']");

        public By VerifyManualOpen => By.XPath(".//button[@id='btnVerifyManualOpen']");

        public By DetailsContinueBtn => By.XPath(".//*[@id='btnDetailsContinue']");

        public By UploadLink => By.XPath(".//*[@id='btnSelectFiles']");

        public By IncomeCategory => By.XPath(".//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IncomeCategoryDeposit1 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-1]");

        public By IncomeCategoryDeposit2 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()]");

        public By SevenIncomeSouce1 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-6]");

        public By SevenIncomeSouce2 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-5]");

        public By SevenIncomeSouce3 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-4]");

        public By SevenIncomeSouce4 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-3]");

        public By SevenIncomeSouce5 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-2]");

        public By SevenIncomeSouce6 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-1]");

        public By SevenIncomeSouce7 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()]");

        public By LastIncome => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()]");

        public By AcctBSBLastPage => By.XPath("//h3[@class='m-b-20']");

        public By JustChecking => By.XPath(".//select[contains(@id,'primaryincomeinconsistentordrop')][not(ancestor::div[contains(@class,'hidden')])]");

        public By JustCheckingDeposit1 => By.XPath("(.//select[contains(@id,'primaryincomeinconsistentordrop')][not(ancestor::div[contains(@class,'hidden')])])[last()-1]");

        public By JustCheckingDeposit2 => By.XPath("(.//select[contains(@id,'primaryincomeinconsistentordrop')][not(ancestor::div[contains(@class,'hidden')])])[last()]");

        public By JustCheckingQuestionDepo1Tans1 => By.XPath("(//*[@id='depositTable_bd39c420-ec7a-4512-a96e-c8be757d56e0']/tbody/tr[5]/td/div/p[1])");

        public By JustCheckingQuestionDepo1Tans2 => By.XPath("(//*[@id='depositTable_bd39c420-ec7a-4512-a96e-c8be757d56e0']/tbody/tr[5]/td/div/p[1])");

        public By JustCheckingQuestionDepo1Tans1Amt => By.XPath("(//*[@id='depositTable_bd39c420-ec7a-4512-a96e-c8be757d56e0']/tbody/tr[5]/td/div/p[1])");

        public By JustCheckingQuestionDepo1Tans2Amt => By.XPath("(//*[@id='depositTable_bd39c420-ec7a-4512-a96e-c8be757d56e0']/tbody/tr[5]/td/div/p[1])");

        public By ConfirmIncomeButton => By.XPath(".//*[@id='btnConfirmIncome']");

        public By OtherDebtLiabilitiesoption => By.XPath(".//*[@id='OtherDebtLiabilitiesToggle']/label[contains(text(),'No')]");

        public By Dependants => By.XPath(".//select[@id='dependants']");

        public By BtnConfirmExpenses => By.XPath(".//button[@id='btnConfirmExpenses-1']");

        public By GovtBenefits => By.XPath(".//div[@id='incomegovbenefits']/label[contains(text(),'No')]");

        public By AgreeApplicationsubmit => By.XPath(".//label[@for='termsandconditions']");

        public By BtnConfirmSummary => By.XPath(".//*[@id='btnConfirmSummary']");

        public By SMSInput => By.XPath(".//*[@id='mobile-smspin']//input");

        public By SMSDiv => By.XPath(".//div[@id='mobile-smspin']");

        public By SubmitPin => By.XPath(".//*[@id='btnNext']");

        public By WaitMessage => By.XPath(".//*[@id='waitMessage']");

        public By LoanDashboardButton => By.XPath("//a[@class='isButton ui-link ui-btn ui-btn-d ui-shadow ui-corner-all']");

        public By SixtyMinuteButton => By.XPath(".//*[@class='radio white-bg large ']");

        public By SubmitPaymentBtn => By.XPath(".//*[@id='submit-payment']");

        public By VisitFullWebsite => By.XPath(".//a[@id='fullWebsite']");

        public By ConfirmCSVFile => By.XPath(".//*[@id='uploadConfirm']/div/label");

        public By UploadContinueBtn => By.XPath(".//*[@id='btnUploadContinue']");

        public By OtherShortTermLoansLbl => By.XPath(".//div[@id='SACCNamesSection']/h2[contains(text(),'Other short-term loans')]");

        public By FullyrepaidOtherLoanYesBtn => By.XPath(".//*[@id='hasFullyPaidShortTermLoans']//label[contains(text(),'Yes')]");

        public By FullyrepaidOtherLoanNoBtn => By.XPath(".//*[@id='hasFullyPaidShortTermLoans']//label[contains(text(),'No')]");

        public By UpToDatewithReaymentsYes => By.XPath(".//*[@id='hasoverduesaccrepayments']//label[contains(text(),'Yes')]");

        public By UpToDatewithReaymentsNo => By.XPath(".//*[@id='hasoverduesaccrepayments']//label[contains(text(),'No')]");

        public By IntendToUseNimbleLoanYes => By.XPath(".//*[@id='hasIntentToUseNimbleForOtherRepayments']//label[contains(text(),'Yes')]");

        public By IntendToUseNimbleLoanNo => By.XPath(".//*[@id='hasIntentToUseNimbleForOtherRepayments']//label[contains(text(),'No')]");

        public By ConfirmSACCNamesBtn => By.XPath(".//button[@id='btnConfirmSACCNames']");

        public By LoanDashboardManual => By.XPath(".//a[@href='/Mobile/Home']");

        public By FinalApprove => By.XPath("(//p[@class='font-20 bold600 gold'])[last()-1]");

        public By SetupManual => By.XPath("(//span[text()='Set up'])[last()-0]");

        public By GovtBenefitsYes => By.XPath("//label[@for='toggle_incomegovbenefits_Yes']");

        public By GovtDNQText => By.XPath("//h2[@class='center p-b-10']");

        public By PleaseSelectReasonForOther => By.XPath(".//div[@class='formfieldtext otherreason']/input[not(ancestor::div[contains(@class,'hidden')])]");

        public By ClickVerifyButtonRL => By.XPath("(//a[@class='pli-button confirmIncomeLink button xsml gold'])[last()]");

        public By CheckIdAuthorisationChkbx => By.XPath("//*[@id='amlCheck']/div");

        public By IdTypeLst => By.XPath(".//select[contains(@id,'amlidtype')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdMedicareCardNumberTxt => By.XPath(".//input[@id='medicarecardnumber']");

        public By IdMedicareRefNoTxt => By.XPath(".//input[@id='medicarecardirn']");

        public By IdMedicareCardColourLst => By.XPath(".//select[contains(@id,'medicarecardcolourid')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdMedicareCardNameTxt => By.XPath(".//input[@id='medicarecardnameoncard']");

        public By IdMedicareExpiryDateMonthLst => By.XPath(".//select[contains(@id,'medicarecardexpirymonth')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdMedicareExpiryDateYearLst => By.XPath(".//select[contains(@id,'medicarecardexpiryyear')][not(ancestor::div[contains(@class,'hidden')])]");

        public By ClickIdSubmitBtn => By.XPath(".//*[@id='amlSubmit']");

        public By IdAustralianPassportNumberTxt => By.XPath(".//input[@id='passportnumber2']");

        public By IdAustralianPassportCountyBirthLst => By.XPath(".//select[contains(@id,'passportbirthcountry')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdAustralianPassportPlaceOfBirthTxt => By.XPath(".//input[@id='passportbirthplace']");

        public By IdAustralianPassportSurnameAtBirthTxt => By.XPath(".//input[@id='passportbirthsurname']");

        public By IdAustralianPassportSurnameAtCitizenshipTxt => By.XPath(".//input[@id='passportcitizenshipsurname']");

        public By IdInternationalPassportNumberTxt => By.XPath(".//input[@id='passportnumber1']");

        public By IdInternationalPassportCountryLst => By.XPath(".//select[contains(@id,'passportcountry')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdDriversLicenceNameTxt => By.XPath(".//input[@id='driverslicencename']");

        public By IdDriversLicenceStateLst => By.XPath(".//select[contains(@id,'driverslicencestateid')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdDriversLicenceNumberTxt => By.XPath(".//input[@id='driverslicencenumber']");

        public By ButtonSubmit => By.XPath(".//*[@id='submit']");

        // transaction list
        public By TransactionfromGroup => By.XPath(".//tr[@class='incomeDeposit-transactions incomeDeposit-transactions-list']//tr[1]//td[1]");
        // we can use this xpath in helper methods directly and pass tr index and td index number to get respective transaction values.

        // spike  xpaths
        public By SpikeText => By.XPath(".//tr[@class='incomeDeposit-spike white-bg border']//p[@class='text-center m-t-10 m-b-10']");

        public By SpikeQuestionOption => By.XPath(".//tr[@class='incomeDeposit-spike white-bg border']//select");

        public By IdentifiedSpikeTransactionDate => By.XPath(".//tr[@class='incomeDeposit-spike white-bg border']//div[@class='incomeDeposit-transactions-wrap sand-lighter-bg radius-5 border']//td[1]");

        public By IdentifiedSpikeTransactionDescription => By.XPath(".//tr[@class='incomeDeposit-spike white-bg border']//div[@class='incomeDeposit-transactions-wrap sand-lighter-bg radius-5 border']//td[2]");

        public By IdentifiedSpikeTransactionAmount => By.XPath(".//tr[@class='incomeDeposit-spike white-bg border']//div[@class='incomeDeposit-transactions-wrap sand-lighter-bg radius-5 border']//td[3]");

        public By OtherReasonSpike => By.XPath(".//input[contains(@id,'incomespike')][not(ancestor::div[contains(@class,'hidden')])]");

        // ooc xpaths
        public By OOCText => By.XPath(".//tr[@class='incomeDeposit-outofcycle white-bg border']//p[@class='text-center m-t-10 m-b-10']");

        public By OOCQuestionOption => By.XPath(".//tr[@class='incomeDeposit-outofcycle white-bg border']//select");

        public By IdentifiedOOCTransactionDate => By.XPath(".//tr[@class='incomeDeposit-outofcycle white-bg border']//div[@class='incomeDeposit-transactions-wrap sand-lighter-bg']//td[1]");

        public By IdentifiedOOCTransactionDescription => By.XPath(".//tr[@class='incomeDeposit-outofcycle white-bg border']//div[@class='incomeDeposit-transactions-wrap sand-lighter-bg']//td[2]");

        public By IdentifiedOOCTransactionAmount => By.XPath(".//tr[@class='incomeDeposit-outofcycle white-bg border']//div[@class='incomeDeposit-transactions-wrap sand-lighter-bg']//td[3]");

        public By OtherReasonOOC => By.XPath(".//input[contains(@id,'incomeoutofcycle')][not(ancestor::div[contains(@class,'hidden')])]");

        // ooc with II
        public By RemainTransactionText => By.XPath(".//div[@class='withspikeoroutofcycle text-center']/p[not(ancestor::div[contains(@class,'hidden')])]");

        public By EditIncome => By.XPath("//a[@id='summary-editIncome']");

        public By EnterLivingExpenses => By.XPath("//input[@id='everydaylivingexpensespermonth']");

        public By EnterMortgageRepayments => By.XPath("//input[@id='rentamt']");

        public By EnterOtherDebtRepayments => By.XPath("//*[@id='otherdebtrepayments']");

        public By EnterLargeOngoingBills => By.XPath("//*[@id='largerrecurringbillsperyear']");

        public By SelectNoRentReason => By.XPath(".//select[contains(@id,'rentlowreason')][not(ancestor::div[contains(@class,'hidden')])]");

        public By EnterNoRentOtherTxt => By.XPath("//*[@id='rentlowreasonother']");

        public By CompareSummaryLivingExpensesTxt => By.XPath("//*[@id='summaryExpenses']/tbody/tr[2]/td/table/tbody/tr[2]/td[2]");

        public By CompareSummaryOtherDebtTxt => By.XPath("//*[@id='summaryExpenses']/tbody/tr[2]/td/table/tbody/tr[3]/td[2]");

        public By CompareSummaryRentMortgageTxt => By.XPath("//*[@id='summaryExpenses']/tbody/tr[2]/td/table/tbody/tr[4]/td[2]");

        public By CompareSummaryLargeOngoingBillsTxt => By.XPath("//*[@id='summaryExpenses']/tbody/tr[2]/td/table/tbody/tr[5]/td[2]");

        public By EditExpenses => By.XPath("//a[@id='summary-editExpenses']");

        public By SessionTimeout => By.XPath("//div[@id='session-timeout-desktop']");

        public By UpcomingRepaymentFinalPage => By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-5]");

        public By UpcomingRepaymentFinalPageExtend => By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-6]");

        public By MissedRepaymentFinalPage => By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-2]");

        public By MissedRepaymentFirstPage => By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-3]");

        public By RescheduleMessage => By.XPath("//*[contains(text(),'Thanks')]");

        public By MissedRepaymentMessage => By.XPath("//h4[contains(text(),'Oops')]");

        public By RescheduleButton => By.XPath("//a[text()='Reschedule']");

        public By DividePayment => By.XPath("//div[@data-attached='rescheduleOption_Divide']");

        public By ExtentPayment => By.XPath("//div[@data-attached='rescheduleOption_After']");

        public By UpcomingRepaymentFirstPage => By.XPath("(//h3[@class='font-30 text-center'])[last()-4]");

        public By UpcomingRepaymentFirstPageExtend => By.XPath("(//h3[@class='font-30 text-center'])[last()-2]");

        public By RescheduleContinue => By.XPath("//button[@id='submitButton']");

        public By NoTransactionOptions => By.XPath("//select[@id='missingTransResponse']");

        public By NoTransactionContinue => By.XPath("//button[@id='btnConfirmMissingTrans']");

        public By OtherReason => By.XPath("//input[@id='missingtransresponseother']");

        public By ViewTransactions => By.XPath(".//tr[@class='incomeDeposit-subheader']//a[contains(text(),'view transactions')]");

        public By ExpenseAmount => By.XPath("(.//td[@class='summaryExpenses-transactions-amount p-b-10 bold text-right'])[last()-3]");

        public By CashAdvanceSelect => By.XPath("//*[@id='1']/div[1]/div");

        public By SunshineLoans => By.XPath("//*[@id='ui-id-8']");

        public By SunshineLoansSelect => By.XPath("//*[@id='2']/div[1]/div");

        public By CashTrain => By.XPath("//*[@id='ui-id-9']");

        public By CashTrainSelect => By.XPath("//*[@id='3']/div[1]/div");

        public By PaidLoansNo => By.XPath("//*[@id='hasFullyPaidShortTermLoans']/label[2]");

        public By UptoDateNo => By.XPath("//*[@id='hasoverduesaccrepayments']/label[2]");

        public By PaidLoansContinue => By.XPath("//*[@id='btnConfirmSACCNames']");

        public By Bankloginerrormsg => By.XPath(".//div[@class='error-wrap']/p");

        public By LoanRepaid => By.XPath("//*[@id='member - timeline']/div[2]/div/div[1]/div[2]/div[1]/h3");

        public By PaymentUnsuccessful => By.XPath("//*[@id='memberccunsuccessful']/div/p[1]");

        public By IncorrectCardNo => By.XPath("//*[@id='cc_number_error']/div/p");

        public By IncorrectDate => By.XPath("//*[@id='cc_expiry_error']/div/p");

        public By CheckingErrorMsg => By.XPath("//p[contains(text(),'It appears your login details are')]");

        public By NoPrimaryContinueBtn => By.XPath("//button[@id='btnNoPrimaryIncomeWarningContinue']");

        public By NoPrimaryBackBtn => By.XPath("//button[@id='btnNoPrimaryIncomeWarningBack']");

        public By OopsError => By.XPath("//*[@id='errorSection']/h2");

        public By BSError => By.XPath("//*[@id='bsError']");

        public By CheckMaskedAccountName => By.XPath("//div[@id='accountList']/table/tbody/tr/td[2]");

        public By CheckMaskedAccountNumber => By.XPath("//div[@id='accountList']/table/tbody/tr/td[3]");

        public By CheckMaskedAccountType => By.XPath("//div[@id='accountList']/table/tbody/tr/td[4]");

        public By ANZRegNumber => By.XPath("(//td[@class='appendedCell formfieldtext i-half'])[last()-2]");

        public By ANZPassword => By.XPath("(//td[@class='appendedCell formfieldtext i-half'])[last()-1]");

        public By ANZOperatorId => By.XPath("(//td[@class='appendedCell formfieldtext i-half'])[last()]");

        public By ALMErrorMessage => By.XPath("//div[@class='whitebox container785']/h2");

        public By prefailUpcomingRepaymentFirstPage => By.XPath("(//h3[@class='font-30 text-center'])[last()-2]");

        public By PrefailUpcomingRepaymentFinalPage => By.XPath("(//div[@class='timeline-section-content f-left oauto']//div[@class='timeline-section-content-amount f-right font-20'])[last()-13]");

        public By PrefailUpcomingRepaymentFirstPageExtend => By.XPath("(//h3[@class='font-30 text-center'])[last()-1]");

        public By PrefailUpcomingRepaymentLastPageExtend => By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-14]");

        public By finishButton => By.XPath("//a[text()='Finish']");

        public By Timeline => By.XPath("//div[@id='timeline']");
    }

    public class BankDetailsDesktopNLLoc : IBankDetails
    {
        public By BankName => By.XPath(".//*[@id='BankId']");

        public By ChooseBank => By.XPath(".//*[@id='BankId']/option[32]");

        public By YodleeContinueButton => By.XPath(".//*[@id='btnConfirmYodlee']");

        public By BankUsername => By.XPath("//table[@id='bankFieldsAdditional']//tr[@class='appendedRow'][2]/td[1]/input");

        public By BankPassword => By.XPath("//table[@id='bankFieldsAdditional']//tr[@class='appendedRow'][2]/td[2]/input");

        public By verifyautocontinuebutton => By.XPath(".//*[@id='btnVerifyAuto']");

        public By BankLoginFailedErrMsg => By.XPath(".//*[@id='bankFieldsAdditional_error']");

        public By BankSelectRBtn => By.XPath(".//td[@class='width-40']/label");

        public By BankSelectRBtnExact => By.XPath(".//*[@class='radio white-bg ']");

        public By btnSelectAccount => By.XPath(".//*[@id='btnSelectAccount']");

        //public By GenerateStatementContinue => By.XPath(".//*[@id='btnContinue']");    

        public By ExistingEmailError => By.XPath(".//div[@class='error-wrap']/p");

        public By AddAnotherBank => By.XPath(".//button[@id='btnAddAnotherBank']");

        public By AccountListOPtion1 => By.XPath(".//*[@id='accountList']//tr//td/label[contains(text(),'TAxxx4595')]/parent::td/preceding-sibling::td/input[@name='accountListOptions']/following::div[@class='radio white-bg ']");

        public By AccountListOPtion2 => By.XPath("(.//*[@id='accountList']//input[@name='accountListOptions']/following::div[@class='radio'])[last()]");

        public By BSB => By.XPath(".//*[@id='bankbsb'][not(ancestor::div[contains(@class,'hidden')])]");

        public By AccountNumber => By.XPath(".//*[@id='bankaccountnumber'][not(ancestor::div[contains(@class,'hidden')])]");

        public By NameonAccount => By.XPath(".//*[@id='bankaccountname'][not(ancestor::div[contains(@class,'hidden')])]");

        public By VerifyManualOpen => By.XPath(".//button[@id='btnVerifyManualOpen']");

        public By ContinueConfirmAcctDetails => By.XPath(".//*[@id='btnConfirmAccountDetails']");

        public By DetailsContinueBtn => By.XPath(".//*[@id='btnDetailsContinue']");

        public By UploadLink => By.XPath(".//*[@id='btnSelectFiles']");

        public By IncomeCategory => By.XPath(".//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IncomeCategoryDeposit1 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-1]");

        public By IncomeCategoryDeposit2 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()]");

        public By SevenIncomeSouce1 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-6]");

        public By SevenIncomeSouce2 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-5]");

        public By SevenIncomeSouce3 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-4]");

        public By SevenIncomeSouce4 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-3]");

        public By SevenIncomeSouce5 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-2]");

        public By SevenIncomeSouce6 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-1]");

        public By SevenIncomeSouce7 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()]");

        public By AcctBSBLastPage => By.XPath("//h3[@class='m-b-20']");

        public By JustChecking => By.XPath(".//select[contains(@id,'primaryincomeinconsistentordrop')][not(ancestor::div[contains(@class,'hidden')])]");

        public By JustCheckingDeposit1 => By.XPath("(.//select[contains(@id,'primaryincomeinconsistentordrop')][not(ancestor::div[contains(@class,'hidden')])])[last()-1]");

        public By JustCheckingDeposit2 => By.XPath("(.//select[contains(@id,'primaryincomeinconsistentordrop')][not(ancestor::div[contains(@class,'hidden')])])[last()]");

        public By JustCheckingQuestionDepo1Tans1 => By.XPath("(//*[@id='depositTable_bd39c420-ec7a-4512-a96e-c8be757d56e0']/tbody/tr[5]/td/div/p[1])");

        public By JustCheckingQuestionDepo1Tans2 => By.XPath("(//*[@id='depositTable_bd39c420-ec7a-4512-a96e-c8be757d56e0']/tbody/tr[5]/td/div/p[1])");

        public By JustCheckingQuestionDepo1Tans1Amt => By.XPath("(//*[@id='depositTable_bd39c420-ec7a-4512-a96e-c8be757d56e0']/tbody/tr[5]/td/div/p[1])");

        public By JustCheckingQuestionDepo1Tans2Amt => By.XPath("(//*[@id='depositTable_bd39c420-ec7a-4512-a96e-c8be757d56e0']/tbody/tr[5]/td/div/p[1])");

        public By ConfirmIncomeButton => By.XPath(".//*[@id='btnConfirmIncome']");

        public By OtherDebtLiabilitiesoption => By.XPath(".//*[@id='OtherDebtLiabilitiesToggle']/label[contains(text(),'No')]");

        public By Dependants => By.XPath(".//select[@id='dependants']");

        public By BtnConfirmExpenses => By.XPath(".//button[@id='btnConfirmExpenses']");

        public By GovtBenefits => By.XPath(".//div[@id='incomegovbenefits']/label[contains(text(),'No')]");

        public By AgreeApplicationsubmit => By.XPath(".//label[@id='lblTermsAndConditions']");

        public By BtnConfirmSummary => By.XPath(".//*[@id='btnConfirmSummary']");

        public By SMSInput => By.XPath(".//input[@id='smsInput']");

        public By SMSDiv => By.XPath("//div[@id='dialog-SMSValidation']");

        public By SubmitPin => By.XPath(".//button[@id='submitpin']");

        public By WaitMessage => By.XPath(".//*[@id='waitMessage']");

        public By LoanDashboardButton => By.XPath("//a[@class='isButton ui-link ui-btn ui-btn-d ui-shadow ui-corner-all']");

        public By SixtyMinuteButton => By.XPath(".//*[@id='PaymentOptions']/tbody/tr[2]/td/div[1]/div");

        public By SubmitPaymentBtn => By.XPath(".//*[@id='submit-payment']");

        public By VisitFullWebsite => By.XPath(".//a[@id='fullWebsite']");

        public By ConfirmCSVFile => By.XPath(".//*[@id='uploadConfirm']/div/label");

        public By UploadContinueBtn => By.XPath(".//*[@id='btnUploadContinue']");

        public By OtherShortTermLoansLbl => By.XPath(".//div[@id='SACCNamesSection']/h2[contains(text(),'Other short-term loans')]");

        public By FullyrepaidOtherLoanYesBtn => By.XPath(".//*[@id='hasFullyPaidShortTermLoans']//label[contains(text(),'Yes')]");

        public By FullyrepaidOtherLoanNoBtn => By.XPath(".//*[@id='hasFullyPaidShortTermLoans']//label[contains(text(),'No')]");

        public By UpToDatewithReaymentsYes => By.XPath(".//*[@id='hasoverduesaccrepayments']//label[contains(text(),'Yes')]");

        public By UpToDatewithReaymentsNo => By.XPath(".//*[@id='hasoverduesaccrepayments']//label[contains(text(),'No')]");

        public By IntendToUseNimbleLoanYes => By.XPath(".//*[@id='hasIntentToUseNimbleForOtherRepayments']//label[contains(text(),'Yes')]");

        public By IntendToUseNimbleLoanNo => By.XPath(".//*[@id='hasIntentToUseNimbleForOtherRepayments']//label[contains(text(),'No')]");

        public By ConfirmSACCNamesBtn => By.XPath(".//button[@id='btnConfirmSACCNames']");

        public By LoanDashboardManual => By.XPath(".//a[@href='/Account/MemberHome']/span");

        public By FinalApprove => By.XPath(".//a[@href='/Account/DoFinalApproval']");

        public By SetupManual => By.XPath("(//span[text()='Set up'])[last()-0]");

        public By GovtBenefitsYes => By.XPath("//label[@for='toggle_incomegovbenefits_Yes']");

        public By GovtDNQText => By.XPath("//h2[@class='center p-b-10']");

        public By PleaseSelectReasonForOther => By.XPath(".//div[@class='formfieldtext otherreason']/input[not(ancestor::div[contains(@class,'hidden')])]");

        public By ClickVerifyButtonRL => By.XPath("(//a[@class='pli-button confirmIncomeLink button xsml gold'])[last()]");

        public By CheckIdAuthorisationChkbx => By.XPath("//*[@id='amlCheck']/div");

        public By IdTypeLst => By.XPath(".//select[contains(@id,'amlidtype')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdMedicareCardNumberTxt => By.XPath(".//input[@id='medicarecardnumber']");

        public By IdMedicareRefNoTxt => By.XPath(".//input[@id='medicarecardirn']");

        public By IdMedicareCardColourLst => By.XPath(".//select[contains(@id,'medicarecardcolourid')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdMedicareCardNameTxt => By.XPath(".//input[@id='medicarecardnameoncard']");

        public By IdMedicareExpiryDateMonthLst => By.XPath(".//select[contains(@id,'medicarecardexpirymonth')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdMedicareExpiryDateYearLst => By.XPath(".//select[contains(@id,'medicarecardexpiryyear')][not(ancestor::div[contains(@class,'hidden')])]");

        public By ClickIdSubmitBtn => By.XPath(".//button[@class='button secureSubmitButton sml']");

        public By IdAustralianPassportNumberTxt => By.XPath(".//input[@id='passportnumber2']");

        public By IdAustralianPassportCountyBirthLst => By.XPath(".//select[contains(@id,'passportbirthcountry')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdAustralianPassportPlaceOfBirthTxt => By.XPath(".//input[@id='passportbirthplace']");

        public By IdAustralianPassportSurnameAtBirthTxt => By.XPath(".//input[@id='passportbirthsurname']");

        public By IdAustralianPassportSurnameAtCitizenshipTxt => By.XPath(".//input[@id='passportcitizenshipsurname']");

        public By IdInternationalPassportNumberTxt => By.XPath(".//input[@id='passportnumber1']");

        public By IdInternationalPassportCountryLst => By.XPath(".//select[contains(@id,'passportcountry')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdDriversLicenceNameTxt => By.XPath(".//input[@id='driverslicencename']");

        public By IdDriversLicenceStateLst => By.XPath(".//select[contains(@id,'driverslicencestateid')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdDriversLicenceNumberTxt => By.XPath(".//input[@id='driverslicencenumber']");

        public By ButtonSubmit => By.XPath(".//*[@id='SubmitButton']");

        // transaction list
        public By TransactionfromGroup => By.XPath(".//tr[@class='incomeDeposit-transactions incomeDeposit-transactions-list']//tr[1]//td[1]");
        // we can use this xpath in helper methods directly and pass tr index and td index number to get respective transaction values.

        // spike  xpaths
        public By SpikeText => By.XPath(".//tr[@class='incomeDeposit-spike']//p[@class='m-b-10 text-center']");

        public By SpikeQuestionOption => By.XPath(".//tr[@class='incomeDeposit-spike']//select");

        public By IdentifiedSpikeTransactionDate => By.XPath(".//tr[@class='incomeDeposit-spike']//div[@class='sand-lighter-bg charcoal-dark font-12']//td[1]");

        public By IdentifiedSpikeTransactionDescription => By.XPath(".//tr[@class='incomeDeposit-spike']//div[@class='sand-lighter-bg charcoal-dark font-12']//td[2]");

        public By IdentifiedSpikeTransactionAmount => By.XPath(".//tr[@class='incomeDeposit-spike']//div[@class='sand-lighter-bg charcoal-dark font-12']//td[3]");

        public By OtherReasonSpike => By.XPath(".//input[contains(@id,'incomespike')][not(ancestor::div[contains(@class,'hidden')])]");

        // ooc xpaths
        public By OOCText => By.XPath(".//tr[@class='incomeDeposit-outofcycle']//p[@class='m-b-10 text-center']");

        public By OOCQuestionOption => By.XPath(".//tr[@class='incomeDeposit-outofcycle']//select");

        public By IdentifiedOOCTransactionDate => By.XPath(".//tr[@class='incomeDeposit-outofcycle']//div[@class='sand-lighter-bg charcoal-dark font-12']//td[1]");

        public By IdentifiedOOCTransactionDescription => By.XPath(".//tr[@class='incomeDeposit-outofcycle']//div[@class='sand-lighter-bg charcoal-dark font-12']//td[2]");

        public By IdentifiedOOCTransactionAmount => By.XPath(".//tr[@class='incomeDeposit-outofcycle']//div[@class='sand-lighter-bg charcoal-dark font-12']//td[3]");

        public By OtherReasonOOC => By.XPath(".//input[contains(@id,'incomeoutofcycle')][not(ancestor::div[contains(@class,'hidden')])]");

        // ooc with II
        public By RemainTransactionText => By.XPath(".//div[@class='withspikeoroutofcycle text-center']/p[not(ancestor::div[contains(@class,'hidden')])]");

        public By EditIncome => By.XPath("//a[@id='summary-editIncome']");

        public By EnterLivingExpenses => By.XPath("//input[@id='everydaylivingexpensespermonth']");

        public By EnterMortgageRepayments => By.XPath("//input[@id='rentamt']");

        public By EnterOtherDebtRepayments => By.XPath("//*[@id='otherdebtrepayments']");

        public By EnterLargeOngoingBills => By.XPath("//*[@id='largerrecurringbillsperyear']");

        public By SelectNoRentReason => By.XPath(".//select[contains(@id,'rentlowreason')][not(ancestor::div[contains(@class,'hidden')])]");

        public By EnterNoRentOtherTxt => By.XPath("//*[@id='rentlowreasonother']");

        public By CompareSummaryLivingExpensesTxt => By.XPath("//*[@id='summaryExpenses']/tbody/tr[2]/td/table/tbody/tr[2]/td[2]");

        public By CompareSummaryOtherDebtTxt => By.XPath("//*[@id='summaryExpenses']/tbody/tr[2]/td/table/tbody/tr[3]/td[2]");

        public By CompareSummaryRentMortgageTxt => By.XPath("//*[@id='summaryExpenses']/tbody/tr[2]/td/table/tbody/tr[4]/td[2]");

        public By CompareSummaryLargeOngoingBillsTxt => By.XPath("//*[@id='summaryExpenses']/tbody/tr[2]/td/table/tbody/tr[5]/td[2]");

        public By EditExpenses => By.XPath("//a[@id='summary-editExpenses']");

        public By LastIncome => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()]");

        public By SessionTimeout => By.XPath("//div[@id='session-timeout-desktop']");

        public By UpcomingRepaymentFinalPage => By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-5]");

        public By UpcomingRepaymentFinalPageExtend => By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-6]");

        public By MissedRepaymentFinalPage => By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-2]");

        public By MissedRepaymentFirstPage => By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-3]");

        public By RescheduleMessage => By.XPath("//*[contains(text(),'Thanks')]");

        public By MissedRepaymentMessage => By.XPath("//h4[contains(text(),'Oops')]");

        public By RescheduleButton => By.XPath("(//a[@href='/Account/MemberRescheduleLoan'])[last()]");

        public By DividePayment => By.XPath("//div[@data-attached='rescheduleOption_Divide']");

        public By ExtentPayment => By.XPath("//div[@data-attached='rescheduleOption_After']");

        public By UpcomingRepaymentFirstPage => By.XPath("(//h3[@class='font-30 text-center'])[last()-4]");

        public By UpcomingRepaymentFirstPageExtend => By.XPath("(//h3[@class='font-30 text-center'])[last()-2]");

        public By RescheduleContinue => By.XPath("//button[@id='submitButton']");

        public By NoTransactionOptions => By.XPath("//select[@id='missingTransResponse']");

        public By NoTransactionContinue => By.XPath("//button[@id='btnConfirmMissingTrans']");

        public By OtherReason => By.XPath("//input[@id='missingtransresponseother']");

        public By ViewTransactions => By.XPath(".//tr[@class='incomeDeposit-subheader']//a[contains(text(),'view transactions')]");

        public By ExpenseAmount => By.XPath("(.//td[@class='summaryExpenses-transactions-amount p-b-10 bold text-right'])[last()-3]");

        public By CashAdvanceSelect => By.XPath("//*[@id='1']/div[1]/div");

        public By SunshineLoans => By.XPath("//*[@id='ui-id-5']");

        public By SunshineLoansSelect => By.XPath("//*[@id='2']/div[1]/div");

        public By CashTrain => By.XPath("//*[@id='ui-id-6']");

        public By CashTrainSelect => By.XPath("//*[@id='3']/div[1]/div");

        public By PaidLoansNo => By.XPath("//*[@id='hasFullyPaidShortTermLoans']/label[2]");

        public By UptoDateNo => By.XPath("//*[@id='hasoverduesaccrepayments']/label[2]");

        public By PaidLoansContinue => By.XPath("//*[@id='btnConfirmSACCNames']");

        public By Bankloginerrormsg => By.XPath(".//div[@class='error-wrap']/p");

        public By LoanRepaid => By.XPath("//h3[text()='Loan Repaid']");
        //*[@id='member - timeline']/div[2]/div/div[1]/div[2]/div[1]/h3");

        public By PaymentUnsuccessful => By.XPath("//*[@id='memberccunsuccessful']/div/p[1]");

        public By IncorrectCardNo => By.XPath("//*[@id='cc_number_error']/div/p");

        public By IncorrectDate => By.XPath("//*[@id='cc_expiry_error']/div/p");

        public By CheckingErrorMsg => By.XPath("//p[contains(text(),'It appears your login details are')]");

        public By NoPrimaryContinueBtn => By.XPath("//button[@id='btnNoPrimaryIncomeWarningContinue']");

        public By NoPrimaryBackBtn => By.XPath("//button[@id='btnNoPrimaryIncomeWarningBack']");

        public By OopsError => By.XPath("//*[@id='errorSection']/h2");

        public By BSError => By.XPath("//*[@id='bsError']");

        public By CheckMaskedAccountName => By.XPath("//div[@id='accountList']/table/tbody/tr/td[2]");

        public By CheckMaskedAccountNumber => By.XPath("//div[@id='accountList']/table/tbody/tr/td[3]");

        public By CheckMaskedAccountType => By.XPath("//div[@id='accountList']/table/tbody/tr/td[4]");

        public By ANZRegNumber => By.XPath("(//td[@class='appendedCell formfieldtext i-half'])[last()-2]");

        public By ANZPassword => By.XPath("(//td[@class='appendedCell formfieldtext i-half'])[last()-1]");

        public By ANZOperatorId => By.XPath("(//td[@class='appendedCell formfieldtext i-half'])[last()]");

        public By ALMErrorMessage => By.XPath("//div[@class='whitebox container785']/h2");

        public By prefailUpcomingRepaymentFirstPage => By.XPath("(//h3[@class='font-30 text-center'])[last()-2]");

        public By PrefailUpcomingRepaymentFinalPage => By.XPath("(//div[@class='timeline-section-content f-left oauto']//div[@class='timeline-section-content-amount f-right font-20'])[last()-13]");

        public By PrefailUpcomingRepaymentFirstPageExtend => By.XPath("(//h3[@class='font-30 text-center'])[last()-1]");

        public By PrefailUpcomingRepaymentLastPageExtend => By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-14]");

        public By finishButton => By.XPath("//a[text()='Finish']");

        public By Timeline => By.XPath("//div[@id='timeline']");
    }

    public class BankDetailsMobileRLLoc : IBankDetails
    {
        public By BankName => By.XPath(".//*[@id='bankid']");

        public By ChooseBank => By.XPath("//*[text()='Dagbank']");

        public By YodleeContinueButton => By.XPath(".//*[@id='btnConfirmYodlee']");

        public By BankUsername => By.XPath("(//div[@id='bankFields']//input)[last()-1]");//rev

        public By BankPassword => By.XPath("(//div[@id='bankFields']//input)[last()]");//rev

        public By verifyautocontinuebutton => By.XPath(".//*[@id='btnVerifyAuto']");

        public By BankLoginFailedErrMsg => By.XPath(".//*[@id='bankFieldsAdditional_error']");

        public By BankSelectRBtn => By.XPath(".//td[@class='formfieldlabel width-10']/div");

        public By BankSelectRBtnExact => By.XPath(".//*[@class='block-center radio large white-bg']");

        public By btnSelectAccount => By.XPath(".//*[@id='btnSelectAccount']");

        public By ExistingEmailError => By.XPath(".//div[@class='error-wrap']/p");

        public By AddAnotherBank => By.XPath(".//button[@id='btnAddAnotherBank']");

        public By AccountListOPtion1 => By.XPath("(.//*[@id='accountList']//input[@name='accountListOptions']/following::div[@class='radio white-bg '])[last()-1]");

        public By AccountListOPtion2 => By.XPath("(.//*[@id='accountList']//input[@name='accountListOptions']/following::div[@class='radio'])[last()]");

        // public By GenerateStatementContinue => By.XPath(".//*[@class='block-center radio large sprite-hp i-40-tick-circle noborder']");
        public By BSB => By.XPath(".//*[@id='bankbsb'][not(ancestor::div[contains(@class,'hidden')])]"); //.//*[@id='bankbsb']

        public By AccountNumber => By.XPath(".//*[@id='bankaccountnumber'][not(ancestor::div[contains(@class,'hidden')])]");

        public By NameonAccount => By.XPath(".//*[@id='bankaccountname'][not(ancestor::div[contains(@class,'hidden')])]");

        public By VerifyManualOpen => By.XPath(".//button[@id='btnVerifyManualOpen']");

        public By ContinueConfirmAcctDetails => By.XPath(".//*[@id='btnConfirmAccountDetails']");

        public By DetailsContinueBtn => By.XPath(".//*[@id='btnDetailsContinue']");

        public By UploadLink => By.XPath(".//*[@id='btnSelectFiles']");

        public By IncomeCategory => By.XPath(".//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IncomeCategoryDeposit1 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-1]");

        public By IncomeCategoryDeposit2 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()]");

        public By JustChecking => By.XPath(".//select[contains(@id,'primaryincomeinconsistentordrop')][not(ancestor::div[contains(@class,'hidden')])]");

        public By JustCheckingDeposit1 => By.XPath("(.//select[contains(@id,'primaryincomeinconsistentordrop')][not(ancestor::div[contains(@class,'hidden')])])[last()-1]");

        public By JustCheckingDeposit2 => By.XPath("(.//select[contains(@id,'primaryincomeinconsistentordrop')][not(ancestor::div[contains(@class,'hidden')])])[last()]");

        public By JustCheckingQuestionDepo1Tans1 => By.XPath("(//*[@id='depositTable_bd39c420-ec7a-4512-a96e-c8be757d56e0']/tbody/tr[5]/td/div/p[1])");

        public By JustCheckingQuestionDepo1Tans2 => By.XPath("(//*[@id='depositTable_bd39c420-ec7a-4512-a96e-c8be757d56e0']/tbody/tr[5]/td/div/p[1])");

        public By JustCheckingQuestionDepo1Tans1Amt => By.XPath("(//*[@id='depositTable_bd39c420-ec7a-4512-a96e-c8be757d56e0']/tbody/tr[5]/td/div/p[1])");

        public By JustCheckingQuestionDepo1Tans2Amt => By.XPath("(//*[@id='depositTable_bd39c420-ec7a-4512-a96e-c8be757d56e0']/tbody/tr[5]/td/div/p[1])");

        public By SevenIncomeSouce1 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-6]");

        public By SevenIncomeSouce2 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-5]");

        public By SevenIncomeSouce3 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-4]");

        public By SevenIncomeSouce4 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-3]");

        public By SevenIncomeSouce5 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-2]");

        public By SevenIncomeSouce6 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-1]");

        public By SevenIncomeSouce7 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()]");

        public By AcctBSBLastPage => By.XPath("//h3[@class='m-b-20']");

        public By ConfirmIncomeButton => By.XPath(".//*[@id='btnConfirmIncome']");

        public By OtherDebtLiabilitiesoption => By.XPath(".//*[@id='OtherDebtLiabilitiesToggle']/label[contains(text(),'No')]");

        public By Dependants => By.XPath(".//select[@id='dependants']");

        public By BtnConfirmExpenses => By.XPath(".//button[@id='btnConfirmExpenses-1']");

        public By GovtBenefits => By.XPath(".//div[@id='incomegovbenefits']/label[contains(text(),'No')]");

        public By AgreeApplicationsubmit => By.XPath(".//label[@for='termsandconditions']");

        public By BtnConfirmSummary => By.XPath(".//*[@id='btnConfirmSummary']");

        public By SMSInput => By.XPath(".//*[@id='mobile-smspin']//input");

        public By SMSDiv => By.XPath(".//div[@id='mobile-smspin']");

        public By SubmitPin => By.XPath(".//*[@id='btnNext']");

        public By WaitMessage => By.XPath(".//*[@id='waitMessage']");

        public By LoanDashboardButton => By.XPath("//a[@class='isButton ui-link ui-btn ui-btn-d ui-shadow ui-corner-all']");

        public By SixtyMinuteButton => By.XPath(".//*[@class='radio white-bg large ']");

        public By SubmitPaymentBtn => By.XPath(".//*[@id='submit-payment']");

        public By VisitFullWebsite => By.XPath(".//a[@id='fullWebsite']");

        public By ConfirmCSVFile => By.XPath(".//*[@id='uploadConfirm']/div/label");

        public By UploadContinueBtn => By.XPath(".//*[@id='btnUploadContinue']");

        public By OtherShortTermLoansLbl => By.XPath(".//div[@id='SACCNamesSection']/h2[contains(text(),'Other short-term loans')]");

        public By FullyrepaidOtherLoanYesBtn => By.XPath(".//*[@id='hasFullyPaidShortTermLoans']//label[contains(text(),'Yes')]");

        public By FullyrepaidOtherLoanNoBtn => By.XPath(".//*[@id='hasFullyPaidShortTermLoans']//label[contains(text(),'No')]");

        public By UpToDatewithReaymentsYes => By.XPath(".//*[@id='hasoverduesaccrepayments']//label[contains(text(),'Yes')]");

        public By UpToDatewithReaymentsNo => By.XPath(".//*[@id='hasoverduesaccrepayments']//label[contains(text(),'No')]");

        public By IntendToUseNimbleLoanYes => By.XPath(".//*[@id='hasIntentToUseNimbleForOtherRepayments']//label[contains(text(),'Yes')]");

        public By IntendToUseNimbleLoanNo => By.XPath(".//*[@id='hasIntentToUseNimbleForOtherRepayments']//label[contains(text(),'No')]");

        public By ConfirmSACCNamesBtn => By.XPath(".//button[@id='btnConfirmSACCNames']");

        public By LoanDashboardManual => By.XPath(".//a[@href='/Mobile/Home']");

        public By FinalApprove => By.XPath("(//p[@class='font-20 bold600 gold'])[last()-1]");

        public By SetupManual => By.XPath("(//span[text()='Set up'])[last()-0]");

        public By GovtBenefitsYes => By.XPath("//label[@for='toggle_incomegovbenefits_Yes']");

        public By GovtDNQText => By.XPath("//h2[@class='c p-b-10']");

        public By PleaseSelectReasonForOther => By.XPath(".//div[@class='formfieldtext otherreason']/input[not(ancestor::div[contains(@class,'hidden')])]");

        public By ClickVerifyButtonRL => By.XPath("(//a[@class='pli-button confirmIncomeLink button xsml gold'])[last()]");


        public By CheckIdAuthorisationChkbx => By.XPath("//*[@id='amlCheck']/div");

        public By IdTypeLst => By.XPath(".//select[contains(@id,'amlidtype')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdMedicareCardNumberTxt => By.XPath(".//input[@id='medicarecardnumber']");

        public By IdMedicareRefNoTxt => By.XPath(".//input[@id='medicarecardirn']");

        public By IdMedicareCardColourLst => By.XPath(".//select[contains(@id,'medicarecardcolourid')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdMedicareCardNameTxt => By.XPath(".//input[@id='medicarecardnameoncard']");

        public By IdMedicareExpiryDateMonthLst => By.XPath(".//select[contains(@id,'medicarecardexpirymonth')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdMedicareExpiryDateYearLst => By.XPath(".//select[contains(@id,'medicarecardexpiryyear')][not(ancestor::div[contains(@class,'hidden')])]");

        public By ClickIdSubmitBtn => By.XPath(".//*[@id='amlSubmit']");

        public By IdAustralianPassportNumberTxt => By.XPath(".//input[@id='passportnumber2']");

        public By IdAustralianPassportCountyBirthLst => By.XPath(".//select[contains(@id,'passportbirthcountry')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdAustralianPassportPlaceOfBirthTxt => By.XPath(".//input[@id='passportbirthplace']");

        public By IdAustralianPassportSurnameAtBirthTxt => By.XPath(".//input[@id='passportbirthsurname']");

        public By IdAustralianPassportSurnameAtCitizenshipTxt => By.XPath(".//input[@id='passportcitizenshipsurname']");

        public By IdInternationalPassportNumberTxt => By.XPath(".//input[@id='passportnumber1']");

        public By IdInternationalPassportCountryLst => By.XPath(".//select[contains(@id,'passportcountry')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdDriversLicenceNameTxt => By.XPath(".//input[@id='driverslicencename']");

        public By IdDriversLicenceStateLst => By.XPath(".//select[contains(@id,'driverslicencestateid')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdDriversLicenceNumberTxt => By.XPath(".//input[@id='driverslicencenumber']");

        public By ButtonSubmit => By.XPath(".//*[@id='submit']");

        // transaction list
        public By TransactionfromGroup => By.XPath(".//tr[@class='incomeDeposit-transactions incomeDeposit-transactions-list']//tr[1]//td[1]");
        // we can use this xpath in helper methods directly and pass tr index and td index number to get respective transaction values.

        // spike  xpaths
        public By SpikeText => By.XPath(".//tr[@class='incomeDeposit-spike white-bg border']//p[@class='text-center m-t-10 m-b-10']");

        public By SpikeQuestionOption => By.XPath(".//tr[@class='incomeDeposit-spike white-bg border']//select");

        public By IdentifiedSpikeTransactionDate => By.XPath(".//tr[@class='incomeDeposit-spike white-bg border']//div[@class='incomeDeposit-transactions-wrap sand-lighter-bg radius-5 border']//td[1]");

        public By IdentifiedSpikeTransactionDescription => By.XPath(".//tr[@class='incomeDeposit-spike white-bg border']//div[@class='incomeDeposit-transactions-wrap sand-lighter-bg radius-5 border']//td[2]");

        public By IdentifiedSpikeTransactionAmount => By.XPath(".//tr[@class='incomeDeposit-spike white-bg border']//div[@class='incomeDeposit-transactions-wrap sand-lighter-bg radius-5 border']//td[3]");

        public By OtherReasonSpike => By.XPath(".//input[contains(@id,'incomespike')][not(ancestor::div[contains(@class,'hidden')])]");

        // ooc xpaths
        public By OOCText => By.XPath(".//tr[@class='incomeDeposit-outofcycle white-bg border']//p[@class='text-center m-t-10 m-b-10']");

        public By OOCQuestionOption => By.XPath(".//tr[@class='incomeDeposit-outofcycle white-bg border']//select");

        public By IdentifiedOOCTransactionDate => By.XPath(".//tr[@class='incomeDeposit-outofcycle white-bg border']//div[@class='incomeDeposit-transactions-wrap sand-lighter-bg']//td[1]");

        public By IdentifiedOOCTransactionDescription => By.XPath(".//tr[@class='incomeDeposit-outofcycle white-bg border']//div[@class='incomeDeposit-transactions-wrap sand-lighter-bg']//td[2]");

        public By IdentifiedOOCTransactionAmount => By.XPath(".//tr[@class='incomeDeposit-outofcycle white-bg border']//div[@class='incomeDeposit-transactions-wrap sand-lighter-bg']//td[3]");

        public By OtherReasonOOC => By.XPath(".//input[contains(@id,'incomeoutofcycle')][not(ancestor::div[contains(@class,'hidden')])]");

        // ooc with II
        public By RemainTransactionText => By.XPath(".//div[@class='withspikeoroutofcycle text-center']/p[not(ancestor::div[contains(@class,'hidden')])]");

        public By EditIncome => By.XPath("//a[@id='summary-editIncome']");

        public By EnterLivingExpenses => By.XPath("//input[@id='everydaylivingexpensespermonth']");

        public By EnterMortgageRepayments => By.XPath("//input[@id='rentamt']");

        public By EnterOtherDebtRepayments => By.XPath("//*[@id='otherdebtrepayments']");

        public By EnterLargeOngoingBills => By.XPath("//*[@id='largerrecurringbillsperyear']");

        public By SelectNoRentReason => By.XPath(".//select[contains(@id,'rentlowreason')][not(ancestor::div[contains(@class,'hidden')])]");

        public By EnterNoRentOtherTxt => By.XPath("//*[@id='rentlowreasonother']");

        public By CompareSummaryLivingExpensesTxt => By.XPath("//*[@id='summaryExpenses']/tbody/tr[2]/td/table/tbody/tr[2]/td[2]");

        public By CompareSummaryOtherDebtTxt => By.XPath("//*[@id='summaryExpenses']/tbody/tr[2]/td/table/tbody/tr[3]/td[2]");

        public By CompareSummaryRentMortgageTxt => By.XPath("//*[@id='summaryExpenses']/tbody/tr[2]/td/table/tbody/tr[4]/td[2]");

        public By CompareSummaryLargeOngoingBillsTxt => By.XPath("//*[@id='summaryExpenses']/tbody/tr[2]/td/table/tbody/tr[5]/td[2]");

        public By EditExpenses => By.XPath("//a[@id='summary-editExpenses']");

        public By LastIncome => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()]");

        public By SessionTimeout => By.XPath("//div[@id='session-timeout-desktop']");

        public By UpcomingRepaymentFinalPage => By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-5]");

        public By UpcomingRepaymentFinalPageExtend => By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-6]");

        public By MissedRepaymentFinalPage => By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-2]");

        public By MissedRepaymentFirstPage => By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-3]");

        public By RescheduleMessage => By.XPath("//*[contains(text(),'Thanks')]");

        public By MissedRepaymentMessage => By.XPath("//h4[contains(text(),'Oops')]");

        public By RescheduleButton => By.XPath("//a[text()='Reschedule']");

        public By DividePayment => By.XPath("//div[@data-attached='rescheduleOption_Divide']");

        public By ExtentPayment => By.XPath("//div[@data-attached='rescheduleOption_After']");

        public By UpcomingRepaymentFirstPage => By.XPath("(//h3[@class='font-30 text-center'])[last()-4]");

        public By UpcomingRepaymentFirstPageExtend => By.XPath("(//h3[@class='font-30 text-center'])[last()-2]");

        public By RescheduleContinue => By.XPath("//button[@id='submitButton']");

        public By NoTransactionOptions => By.XPath("//select[@id='missingTransResponse']");

        public By NoTransactionContinue => By.XPath("//button[@id='btnConfirmMissingTrans']");

        public By OtherReason => By.XPath("//input[@id='missingtransresponseother']");

        public By ViewTransactions => By.XPath(".//tr[@class='incomeDeposit-subheader']//a[contains(text(),'view transactions')]");

        public By ExpenseAmount => By.XPath("(.//td[@class='summaryExpenses-transactions-amount p-b-10 bold text-right'])[last()-3]");

        public By CashAdvanceSelect => By.XPath("//*[@id='1']/div[1]/div");

        public By SunshineLoans => By.XPath("//*[@id='ui-id-7']");

        public By SunshineLoansSelect => By.XPath("//*[@id='2']/div[1]/div");

        public By CashTrain => By.XPath("//*[@id='ui-id-8']");

        public By CashTrainSelect => By.XPath("//*[@id='3']/div[1]/div");

        public By PaidLoansNo => By.XPath("//*[@id='hasFullyPaidShortTermLoans']/label[2]");

        public By UptoDateNo => By.XPath("//*[@id='hasoverduesaccrepayments']/label[2]");

        public By PaidLoansContinue => By.XPath("//*[@id='btnConfirmSACCNames']");

        public By Bankloginerrormsg => By.XPath(".//div[@class='error-wrap']/p");

        public By LoanRepaid => By.XPath("//*[@id='member - timeline']/div[2]/div/div[1]/div[2]/div[1]/h3");

        public By PaymentUnsuccessful => By.XPath("//*[@id='memberccunsuccessful']/div/p[1]");

        public By IncorrectCardNo => By.XPath("//*[@id='cc_number_error']/div/p");

        public By IncorrectDate => By.XPath("//*[@id='cc_expiry_error']/div/p");

        public By CheckingErrorMsg => By.XPath("//p[contains(text(),'It appears your login details are')]");

        public By NoPrimaryContinueBtn => By.XPath("//button[@id='btnNoPrimaryIncomeWarningContinue']");

        public By NoPrimaryBackBtn => By.XPath("//button[@id='btnNoPrimaryIncomeWarningBack']");

        public By OopsError => By.XPath("//*[@id='errorSection']/h2");

        public By BSError => By.XPath("//*[@id='bsError']");

        public By CheckMaskedAccountName => By.XPath("//div[@id='accountList']/table/tbody/tr/td[2]");

        public By CheckMaskedAccountNumber => By.XPath("//div[@id='accountList']/table/tbody/tr/td[3]");

        public By CheckMaskedAccountType => By.XPath("//div[@id='accountList']/table/tbody/tr/td[4]");

        public By ANZRegNumber => By.XPath("(//td[@class='appendedCell formfieldtext i-half'])[last()-2]");

        public By ANZPassword => By.XPath("(//td[@class='appendedCell formfieldtext i-half'])[last()-1]");

        public By ANZOperatorId => By.XPath("(//td[@class='appendedCell formfieldtext i-half'])[last()]");

        public By ALMErrorMessage => By.XPath("//div[@class='whitebox container785']/h2");

        public By prefailUpcomingRepaymentFirstPage => By.XPath("(//h3[@class='font-30 text-center'])[last()-2]");

        public By PrefailUpcomingRepaymentFinalPage => By.XPath("(//div[@class='timeline-section-content f-left oauto']//div[@class='timeline-section-content-amount f-right font-20'])[last()-13]");

        public By PrefailUpcomingRepaymentFirstPageExtend => By.XPath("(//h2[@class='font-30 text-center'])[last()-1]");

        public By PrefailUpcomingRepaymentLastPageExtend => By.XPath("(//div[@class='timeline-section-content-amount f-right font-16'])[last()-35]");

        public By finishButton => By.XPath("//a[text()='Finish']");

        public By Timeline => By.XPath("//div[@id='timeline']");
    }

    public class BankDetailsDesktopRLLoc : IBankDetails
    {
        public By BankName => By.XPath(".//*[@id='BankId']");

        public By ChooseBank => By.XPath(".//*[@id='BankId']/option[32]");

        public By YodleeContinueButton => By.XPath(".//*[@id='btnConfirmYodlee']");

        public By BankUsername => By.XPath("//table[@id='bankFieldsAdditional']//tr[@class='appendedRow'][2]/td[1]/input");

        public By BankPassword => By.XPath("//table[@id='bankFieldsAdditional']//tr[@class='appendedRow'][2]/td[2]/input");

        public By verifyautocontinuebutton => By.XPath(".//*[@id='btnVerifyAuto']");

        public By BankLoginFailedErrMsg => By.XPath(".//*[@id='bankFieldsAdditional_error']");

        public By BankSelectRBtn => By.XPath(".//td[@class='width-40']/label");

        public By BankSelectRBtnExact => By.XPath(".//*[@class='radio white-bg ']");

        public By btnSelectAccount => By.XPath(".//*[@id='btnSelectAccount']");

        //public By GenerateStatementContinue => By.XPath(".//*[@id='btnContinue']");    

        public By ExistingEmailError => By.XPath(".//div[@class='error-wrap']/p");

        public By AddAnotherBank => By.XPath(".//button[@id='btnAddAnotherBank']");

        public By AccountListOPtion1 => By.XPath(".//*[@id='accountList']//tr//td/label[contains(text(),'TAxxx4595')]/parent::td/preceding-sibling::td/input[@name='accountListOptions']/following::div[@class='radio white-bg ']");

        public By AccountListOPtion2 => By.XPath("(.//*[@class='radio white-bg '])[last()]");

        public By BSB => By.XPath(".//*[@id='bankbsb'][not(ancestor::div[contains(@class,'hidden')])]");

        public By AccountNumber => By.XPath(".//*[@id='bankaccountnumber'][not(ancestor::div[contains(@class,'hidden')])]");

        public By NameonAccount => By.XPath(".//*[@id='bankaccountname'][not(ancestor::div[contains(@class,'hidden')])]");

        public By VerifyManualOpen => By.XPath(".//button[@id='btnVerifyManualOpen']");

        public By ContinueConfirmAcctDetails => By.XPath(".//*[@id='btnConfirmAccountDetails']");

        public By DetailsContinueBtn => By.XPath(".//*[@id='btnDetailsContinue']");

        public By UploadLink => By.XPath(".//*[@id='btnSelectFiles']");

        public By IncomeCategory => By.XPath(".//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IncomeCategoryDeposit1 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-1]");

        public By IncomeCategoryDeposit2 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()]");

        public By JustCheckingQuestionDepo1Tans1Amt => By.XPath("(//*[@id='depositTable_bd39c420-ec7a-4512-a96e-c8be757d56e0']/tbody/tr[5]/td/div/p[1])");

        public By JustCheckingQuestionDepo1Tans2Amt => By.XPath("(//*[@id='depositTable_bd39c420-ec7a-4512-a96e-c8be757d56e0']/tbody/tr[5]/td/div/p[1])");

        public By SevenIncomeSouce1 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-6]");

        public By SevenIncomeSouce2 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-5]");

        public By SevenIncomeSouce3 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-4]");

        public By SevenIncomeSouce4 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-3]");

        public By SevenIncomeSouce5 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-2]");

        public By SevenIncomeSouce6 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-1]");

        public By SevenIncomeSouce7 => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()]");

        public By AcctBSBLastPage => By.XPath("//h3[@class='m-b-20']");

        public By JustChecking => By.XPath(".//select[contains(@id,'primaryincomeinconsistentordrop')][not(ancestor::div[contains(@class,'hidden')])]");

        public By JustCheckingDeposit1 => By.XPath("(.//select[contains(@id,'primaryincomeinconsistentordrop')][not(ancestor::div[contains(@class,'hidden')])])[last()-1]");

        public By JustCheckingDeposit2 => By.XPath("(.//select[contains(@id,'primaryincomeinconsistentordrop')][not(ancestor::div[contains(@class,'hidden')])])[last()]");

        public By JustCheckingQuestionDepo1Tans1 => By.XPath("(//*[@id='depositTable_bd39c420-ec7a-4512-a96e-c8be757d56e0']/tbody/tr[5]/td/div/p[1])");

        public By JustCheckingQuestionDepo1Tans2 => By.XPath("(//*[@id='depositTable_bd39c420-ec7a-4512-a96e-c8be757d56e0']/tbody/tr[5]/td/div/p[1])");

        public By ConfirmIncomeButton => By.XPath(".//*[@id='btnConfirmIncome']");

        public By OtherDebtLiabilitiesoption => By.XPath(".//*[@id='OtherDebtLiabilitiesToggle']/label[contains(text(),'No')]");

        public By Dependants => By.XPath(".//select[@id='dependants']");

        public By BtnConfirmExpenses => By.XPath(".//button[@id='btnConfirmExpenses']");

        public By GovtBenefits => By.XPath(".//div[@id='incomegovbenefits']/label[contains(text(),'No')]");

        public By AgreeApplicationsubmit => By.XPath(".//label[@id='lblTermsAndConditions']");

        public By BtnConfirmSummary => By.XPath(".//*[@id='btnConfirmSummary']");

        public By SMSInput => By.XPath(".//input[@id='smsInput']");

        public By SMSDiv => By.XPath("//div[@id='dialog-SMSValidation']");

        public By SubmitPin => By.XPath(".//button[@id='submitpin']");

        public By WaitMessage => By.XPath(".//*[@id='waitMessage']");

        public By LoanDashboardButton => By.XPath("//a[@class='isButton ui-link ui-btn ui-btn-d ui-shadow ui-corner-all']");

        public By SixtyMinuteButton => By.XPath(".//*[@class='payment-method-input']/input[@value='1']/following-sibling::div");

        public By SubmitPaymentBtn => By.XPath(".//*[@id='submit-payment']");

        public By VisitFullWebsite => By.XPath(".//a[@id='fullWebsite']");

        public By ConfirmCSVFile => By.XPath(".//*[@id='uploadConfirm']/div/label");

        public By UploadContinueBtn => By.XPath(".//*[@id='btnUploadContinue']");

        public By OtherShortTermLoansLbl => By.XPath(".//div[@id='SACCNamesSection']/h2[contains(text(),'Other short-term loans')]");

        public By FullyrepaidOtherLoanYesBtn => By.XPath(".//*[@id='hasFullyPaidShortTermLoans']//label[contains(text(),'Yes')]");

        public By FullyrepaidOtherLoanNoBtn => By.XPath(".//*[@id='hasFullyPaidShortTermLoans']//label[contains(text(),'No')]");

        public By UpToDatewithReaymentsYes => By.XPath(".//*[@id='hasoverduesaccrepayments']//label[contains(text(),'Yes')]");

        public By UpToDatewithReaymentsNo => By.XPath(".//*[@id='hasoverduesaccrepayments']//label[contains(text(),'No')]");

        public By IntendToUseNimbleLoanYes => By.XPath(".//*[@id='hasIntentToUseNimbleForOtherRepayments']//label[contains(text(),'Yes')]");

        public By IntendToUseNimbleLoanNo => By.XPath(".//*[@id='hasIntentToUseNimbleForOtherRepayments']//label[contains(text(),'No')]");

        public By ConfirmSACCNamesBtn => By.XPath(".//button[@id='btnConfirmSACCNames']");

        public By LoanDashboardManual => By.XPath(".//a[@href='/Account/MemberHome']/span");

        public By FinalApprove => By.XPath(".//a[@href='/Account/DoFinalApproval']");

        public By SetupManual => By.XPath("(//span[text()='Set up'])[last()-0]");

        public By GovtBenefitsYes => By.XPath("//label[@for='toggle_incomegovbenefits_Yes']");

        public By GovtDNQText => By.XPath("//h2[@class='c p-b-10']");

        public By PleaseSelectReasonForOther => By.XPath(".//div[@class='formfieldtext otherreason']/input[not(ancestor::div[contains(@class,'hidden')])]");

        public By ClickVerifyButtonRL => By.XPath("(//a[@class='pli-button confirmIncomeLink button xsml gold'])[last()]");

        public By CheckIdAuthorisationChkbx => By.XPath("//*[@id='amlCheck']/div");

        public By IdTypeLst => By.XPath(".//select[contains(@id,'amlidtype')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdMedicareCardNumberTxt => By.XPath(".//input[@id='medicarecardnumber']");

        public By IdMedicareRefNoTxt => By.XPath(".//input[@id='medicarecardirn']");

        public By IdMedicareCardColourLst => By.XPath(".//select[contains(@id,'medicarecardcolourid')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdMedicareCardNameTxt => By.XPath(".//input[@id='medicarecardnameoncard']");

        public By IdMedicareExpiryDateMonthLst => By.XPath(".//select[contains(@id,'medicarecardexpirymonth')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdMedicareExpiryDateYearLst => By.XPath(".//select[contains(@id,'medicarecardexpiryyear')][not(ancestor::div[contains(@class,'hidden')])]");

        public By ClickIdSubmitBtn => By.XPath(".//button[@class='button secureSubmitButton sml']");

        public By IdAustralianPassportNumberTxt => By.XPath(".//input[@id='passportnumber2']");

        public By IdAustralianPassportCountyBirthLst => By.XPath(".//select[contains(@id,'passportbirthcountry')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdAustralianPassportPlaceOfBirthTxt => By.XPath(".//input[@id='passportbirthplace']");

        public By IdAustralianPassportSurnameAtBirthTxt => By.XPath(".//input[@id='passportbirthsurname']");

        public By IdAustralianPassportSurnameAtCitizenshipTxt => By.XPath(".//input[@id='passportcitizenshipsurname']");

        public By IdInternationalPassportNumberTxt => By.XPath(".//input[@id='passportnumber1']");

        public By IdInternationalPassportCountryLst => By.XPath(".//select[contains(@id,'passportcountry')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdDriversLicenceNameTxt => By.XPath(".//input[@id='driverslicencename']");

        public By IdDriversLicenceStateLst => By.XPath(".//select[contains(@id,'driverslicencestateid')][not(ancestor::div[contains(@class,'hidden')])]");

        public By IdDriversLicenceNumberTxt => By.XPath(".//input[@id='driverslicencenumber']");

        public By ButtonSubmit => By.XPath(".//*[@id='SubmitButton']");

        // transaction list
        public By TransactionfromGroup => By.XPath(".//tr[@class='incomeDeposit-transactions incomeDeposit-transactions-list']//tr[1]//td[1]");
        // we can use this xpath in helper methods directly and pass tr index and td index number to get respective transaction values.

        // spike  xpaths
        public By SpikeText => By.XPath(".//tr[@class='incomeDeposit-spike']//p[@class='m-b-10 text-center']");

        public By SpikeQuestionOption => By.XPath(".//tr[@class='incomeDeposit-spike']//select");

        public By IdentifiedSpikeTransactionDate => By.XPath(".//tr[@class='incomeDeposit-spike']//div[@class='sand-lighter-bg charcoal-dark font-12']//td[1]");

        public By IdentifiedSpikeTransactionDescription => By.XPath(".//tr[@class='incomeDeposit-spike']//div[@class='sand-lighter-bg charcoal-dark font-12']//td[2]");

        public By IdentifiedSpikeTransactionAmount => By.XPath(".//tr[@class='incomeDeposit-spike']//div[@class='sand-lighter-bg charcoal-dark font-12']//td[3]");

        public By OtherReasonSpike => By.XPath(".//input[contains(@id,'incomespike')][not(ancestor::div[contains(@class,'hidden')])]");

        // ooc xpaths
        public By OOCText => By.XPath(".//tr[@class='incomeDeposit-outofcycle']//p[@class='m-b-10 text-center']");

        public By OOCQuestionOption => By.XPath(".//tr[@class='incomeDeposit-outofcycle']//select");

        public By IdentifiedOOCTransactionDate => By.XPath(".//tr[@class='incomeDeposit-outofcycle']//div[@class='sand-lighter-bg charcoal-dark font-12']//td[1]");

        public By IdentifiedOOCTransactionDescription => By.XPath(".//tr[@class='incomeDeposit-outofcycle']//div[@class='sand-lighter-bg charcoal-dark font-12']//td[2]");

        public By IdentifiedOOCTransactionAmount => By.XPath(".//tr[@class='incomeDeposit-outofcycle']//div[@class='sand-lighter-bg charcoal-dark font-12']//td[3]");

        public By OtherReasonOOC => By.XPath(".//input[contains(@id,'incomeoutofcycle')][not(ancestor::div[contains(@class,'hidden')])]");

        // ooc with II
        public By RemainTransactionText => By.XPath(".//div[@class='withspikeoroutofcycle text-center']/p[not(ancestor::div[contains(@class,'hidden')])]");

        public By EditIncome => By.XPath("//a[@id='summary-editIncome']");

        public By EnterLivingExpenses => By.XPath("//input[@id='everydaylivingexpensespermonth']");

        public By EnterMortgageRepayments => By.XPath("//input[@id='rentamt']");

        public By EnterOtherDebtRepayments => By.XPath("//*[@id='otherdebtrepayments']");

        public By EnterLargeOngoingBills => By.XPath("//*[@id='largerrecurringbillsperyear']");

        public By SelectNoRentReason => By.XPath(".//select[contains(@id,'rentlowreason')][not(ancestor::div[contains(@class,'hidden')])]");

        public By EnterNoRentOtherTxt => By.XPath("//*[@id='rentlowreasonother']");

        public By CompareSummaryLivingExpensesTxt => By.XPath("//*[@id='summaryExpenses']/tbody/tr[2]/td/table/tbody/tr[2]/td[2]");

        public By CompareSummaryOtherDebtTxt => By.XPath("//*[@id='summaryExpenses']/tbody/tr[2]/td/table/tbody/tr[3]/td[2]");

        public By CompareSummaryRentMortgageTxt => By.XPath("//*[@id='summaryExpenses']/tbody/tr[2]/td/table/tbody/tr[4]/td[2]");

        public By CompareSummaryLargeOngoingBillsTxt => By.XPath("//*[@id='summaryExpenses']/tbody/tr[2]/td/table/tbody/tr[5]/td[2]");

        public By EditExpenses => By.XPath("//a[@id='summary-editExpenses']");

        public By LastIncome => By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()]");

        public By SessionTimeout => By.XPath("//div[@id='session-timeout-desktop']");

        public By UpcomingRepaymentFinalPage => By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-5]");

        public By UpcomingRepaymentFinalPageExtend => By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-6]");

        public By MissedRepaymentFinalPage => By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-2]");

        public By MissedRepaymentFirstPage => By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-3]");

        public By RescheduleMessage => By.XPath("//*[contains(text(),'Thanks')]");

        public By MissedRepaymentMessage => By.XPath("//h4[contains(text(),'Oops')]");

        public By RescheduleButton => By.XPath("(//a[@href='/Account/MemberRescheduleLoan'])[last()]");

        public By DividePayment => By.XPath("//div[@data-attached='rescheduleOption_Divide']");

        public By ExtentPayment => By.XPath("//div[@data-attached='rescheduleOption_After']");

        public By UpcomingRepaymentFirstPage => By.XPath("(//h3[@class='font-30 text-center'])[last()-4]");

        public By UpcomingRepaymentFirstPageExtend => By.XPath("(//h3[@class='font-30 text-center'])[last()-2]");

        public By RescheduleContinue => By.XPath("//button[@id='submitButton']");

        public By NoTransactionOptions => By.XPath("//select[@id='missingTransResponse']");

        public By NoTransactionContinue => By.XPath("//button[@id='btnConfirmMissingTrans']");

        public By OtherReason => By.XPath("//input[@id='missingtransresponseother']");

        public By ViewTransactions => By.XPath(".//tr[@class='incomeDeposit-subheader']//a[contains(text(),'view transactions')]");

        public By ExpenseAmount => By.XPath("(.//td[@class='summaryExpenses-transactions-amount p-b-10 bold text-right'])[last()-3]");

        public By CashAdvanceSelect => By.XPath("//*[@id='1']/div[1]/div");

        public By SunshineLoans => By.XPath("//*[@id='ui-id-5']");

        public By SunshineLoansSelect => By.XPath("//*[@id='2']/div[1]/div");

        public By CashTrain => By.XPath("//*[@id='ui-id-6']");

        public By CashTrainSelect => By.XPath("//*[@id='3']/div[1]/div");

        public By PaidLoansNo => By.XPath("//*[@id='hasFullyPaidShortTermLoans']/label[2]");

        public By UptoDateNo => By.XPath("//*[@id='hasoverduesaccrepayments']/label[2]");

        public By PaidLoansContinue => By.XPath("//*[@id='btnConfirmSACCNames']");

        public By Bankloginerrormsg => By.XPath(".//div[@class='error-wrap']/p");

        public By LoanRepaid => By.XPath("//h3[text()='Loan Repaid']");

        public By PaymentUnsuccessful => By.XPath("//*[@id='memberccunsuccessful']/div/p[1]");

        public By IncorrectCardNo => By.XPath("//*[@id='cc_number_error']/div/p");

        public By IncorrectDate => By.XPath("//*[@id='cc_expiry_error']/div/p");

        public By CheckingErrorMsg => By.XPath("//p[contains(text(),'It appears your login details are')]");

        public By NoPrimaryContinueBtn => By.XPath("//button[@id='btnNoPrimaryIncomeWarningContinue']");

        public By NoPrimaryBackBtn => By.XPath("//button[@id='btnNoPrimaryIncomeWarningBack']");

        public By OopsError => By.XPath("//*[@id='errorSection']/h2");

        public By BSError => By.XPath("//*[@id='bsError']");

        public By CheckMaskedAccountName => By.XPath("//div[@id='accountList']/table/tbody/tr/td[2]");

        public By CheckMaskedAccountNumber => By.XPath("//div[@id='accountList']/table/tbody/tr/td[3]");

        public By CheckMaskedAccountType => By.XPath("//div[@id='accountList']/table/tbody/tr/td[4]");

        public By ANZRegNumber => By.XPath("(//td[@class='appendedCell formfieldtext i-half'])[last()-2]");

        public By ANZPassword => By.XPath("(//td[@class='appendedCell formfieldtext i-half'])[last()-1]");

        public By ANZOperatorId => By.XPath("(//td[@class='appendedCell formfieldtext i-half'])[last()]");

        public By ALMErrorMessage => By.XPath("//div[@class='whitebox container785']/h2");

        public By prefailUpcomingRepaymentFirstPage => By.XPath("(//h3[@class='font-30 text-center'])[last()-2]");

        public By PrefailUpcomingRepaymentFinalPage => By.XPath("(//div[@class='timeline-section-content f-left oauto']//div[@class='timeline-section-content-amount f-right font-20'])[last()-5]");

        public By PrefailUpcomingRepaymentFirstPageExtend => By.XPath("(//h3[@class='font-30 text-center'])[last()-1]");

        public By PrefailUpcomingRepaymentLastPageExtend => By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-6]");

        public By finishButton => By.XPath("//a[text()='Finish']");

        public By Timeline => By.XPath("//div[@id='timeline']");
    }
}