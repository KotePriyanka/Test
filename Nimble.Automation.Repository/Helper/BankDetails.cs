using System;
using OpenQA.Selenium;
using System.Diagnostics;
using System.Threading;
using Nimble.Automation.Accelerators;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.InputDevices;
using TestStack.White.WindowsAPI;
using NUnit.Framework;
using AutoItX3Lib;

namespace Nimble.Automation.Repository
{
    public class BankDetails : TestEngine
    {
        private IBankDetails _bankdetailsLoc;
        private ActionEngine _act = null;
        private IWebDriver _driver = null;

        TestUtility _testUtility = new TestUtility();

        public BankDetails(IWebDriver driver, string strUserType)
        {
            if (GetPlatform(driver))
                _bankdetailsLoc = (strUserType == "NL") ? (IBankDetails)new BankDetailsMobileNLLoc() : new BankDetailsMobileRLLoc();
            else
                _bankdetailsLoc = (strUserType == "NL") ? (IBankDetails)new BankDetailsDesktopNLLoc() : new BankDetailsDesktopRLLoc();
            _act = new ActionEngine(driver);
            _driver = driver;
        }

        /// <summary>
        /// Selects the bank from drop down list.
        /// </summary>
        /// <param name="bankName">Name of the bank.</param>
        public void SelectBankLst(string bankName)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.BankName, 120);
            _act.selectByOptionText(_bankdetailsLoc.BankName, bankName, "Bank");
        }

        /// <summary>
        /// Click continue button.
        /// </summary>
        public void BankSelectContinueBtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.YodleeContinueButton, 200);
            Thread.Sleep(3000);
            _act.click(_bankdetailsLoc.YodleeContinueButton, "YodleeContinueButton");
            //_driver.FindElement(_bankdetailsLoc.YodleeContinueButton).Click();
        }

        /// <summary>
        /// Enters the bank credentials text.
        /// </summary>
        /// <param name="bankUsername">The bank username.</param>
        /// <param name="bankPassword">The bank password.</param>
        public void EnterBankCredentialsTxt(string bankUsername, string bankPassword)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.BankUsername, 30);
            _act.EnterText(_bankdetailsLoc.BankUsername, bankUsername);
            _act.EnterText(_bankdetailsLoc.BankPassword, bankPassword);
        }

        /// <summary>
        /// Clicks the continue button.
        /// </summary>
        public void ClickAutoContinueBtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.verifyautocontinuebutton, 30);
            _act.click(_bankdetailsLoc.verifyautocontinuebutton, "BankContinueBtn");
        }

        public string errorMsg()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.CheckingErrorMsg, 30);
            string errormsg = _act.getText(_bankdetailsLoc.CheckingErrorMsg, "");
            return errormsg;

        }

        public string GetInvalidloginmsg()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.Bankloginerrormsg, 30);
            string errormsg = _act.getText(_bankdetailsLoc.Bankloginerrormsg, "Bankloginerrormsg");
            return errormsg;
        }

        public bool NoTransaction(string reason)
        {
            bool flag = false;
            _act.waitForVisibilityOfElement(_bankdetailsLoc.NoTransactionOptions, 30);
            _act.selectByOptionText(_bankdetailsLoc.NoTransactionOptions, reason, "reason");
            if (_act.isElementDisplayed(_driver.FindElement(_bankdetailsLoc.NoTransactionOptions)))
            {
                flag = true;
            }
            if (reason == "Other")
            {
                _act.EnterText(_bankdetailsLoc.OtherReason, "I was having money from my dad");
            }
            _act.click(_bankdetailsLoc.NoTransactionContinue, "NoTransactionContinue");

            return flag;
        }
        /// Clicks the add another bank button
        /// </summary>
        public void ClickAddAnotherBankBtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.AddAnotherBank, 30);
            _act.click(_bankdetailsLoc.AddAnotherBank, "AddAnotherBank");

        }

        public bool VerifyBankLoginFailedErrMsgTxt()
        {

            bool flag = false;
            Thread.Sleep(5000);
            if (_act.isElementPresent(_bankdetailsLoc.BankLoginFailedErrMsg))
            {
                _act.waitForVisibilityOfElement(_bankdetailsLoc.BankLoginFailedErrMsg, 30);
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;

        }

        /// <summary>
        /// Bank Details page - check returned error message
        /// </summary>
        /// <returns></returns>
        public string CheckBankLoginFailedErrMsgTxt()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.BankLoginFailedErrMsg, 120);
            return _act.getText(_bankdetailsLoc.BankLoginFailedErrMsg, "BankLoginFailedErrMsg");
        }

        /// <summary>
        /// Click on Select bank account button.
        /// </summary>
        public void BankAccountSelectBtn()
        {
            //Added if else condition in case the bank login failed, then re try 
            try
            {
                if (_act.waitForVisibilityOfElementUntillTimeout(_bankdetailsLoc.BankSelectRBtnExact, 300))
                    _act.JSClick(_bankdetailsLoc.BankSelectRBtnExact, "BankSelectRBtnExact");
                else
                {
                    _act.click(_bankdetailsLoc.verifyautocontinuebutton, "BankContinueBtn");
                    _act.JSClick(_bankdetailsLoc.BankSelectRBtnExact, "BankSelectRBtnExact");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(_bankdetailsLoc.BankSelectRBtnExact + " is not visible for " + 120 + " seconds." + " with message :" + ex.Message);
            }
        }

        /// <summary>
        /// Click on BankAccount select for first BS
        /// </summary>
        public void BankAccountselectforfirstBS()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.AccountListOPtion1, 100);
            _act.JSClick(_bankdetailsLoc.AccountListOPtion1, "AccountListOPtion1");

        }

        /// <summary>
        /// Click on BankAccount select for second BS
        /// </summary>
        public void BankAccountselectforsecondBS()
        {
            // _act.waitForVisibilityOfElement(_bankdetailsLoc.AccountListOPtion2, 500);
            _act.JSClick(_bankdetailsLoc.AccountListOPtion2, "AccountListOPtion2");
        }

        /// <summary>
        /// Clicks the bank account continue buton.
        /// </summary>
        public void ClickBankAccountContBtn()
        {
            // _act.waitForVisibilityOfElement(_bankdetailsLoc.CashAdvanceSelect, 120);
            _act.click(_bankdetailsLoc.btnSelectAccount, "btnSelectAccount");
        }

        public void OtherLoanDetails()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.CashAdvanceSelect, 30);
            _act.JSClick(_bankdetailsLoc.CashAdvanceSelect, "CashAdvanceSelect");
            _act.JSClick(_bankdetailsLoc.SunshineLoans, "SunshineLoans");
            Thread.Sleep(1000);
            _act.JSClick(_bankdetailsLoc.SunshineLoansSelect, "SunshineLoansSelect");
            _act.JSClick(_bankdetailsLoc.CashTrain, "CashTrain");
            Thread.Sleep(1000);
            _act.JSClick(_bankdetailsLoc.CashTrainSelect, "CashTrainSelect");
            _act.JSClick(_bankdetailsLoc.PaidLoansNo, "PaidLoansNo");
            _act.JSClick(_bankdetailsLoc.UptoDateNo, "UptoDateNo");
            _act.JSClick(_bankdetailsLoc.PaidLoansContinue, "PaidLoansContinue");
        }

        /// <summary>
        /// Enters the bank details.
        /// </summary>
        /// <param name="bsb">The BSB Number.</param>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="nameonAccount">The nameon account.</param>
        public void EnterBankDetailsTxt(string bsb, string accountNumber, string nameonAccount)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.BSB, 60);
            _act.EnterText(_bankdetailsLoc.BSB, bsb);
            _act.EnterText(_bankdetailsLoc.AccountNumber, accountNumber);
            _act.EnterText(_bankdetailsLoc.NameonAccount, nameonAccount);
        }

        /// <summary>
        /// Enters the bank details.
        /// </summary>
        public void EnterBankDetailsTxt()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.BSB, 30);
            _act.EnterText(_bankdetailsLoc.BSB, _testUtility.RandomBSB());
            if (SelectedAccountCheckEnabled == "true")
                _act.EnterText(_bankdetailsLoc.AccountNumber, _testUtility.FixedAccNumber());
            else
            {
                _act.EnterText(_bankdetailsLoc.AccountNumber, _testUtility.RandomAccNumber());
            }
            _act.EnterText(_bankdetailsLoc.NameonAccount, _testUtility.RandomAccName());
        }

        /// <summary>
        /// Enters the Invalid bank details.
        /// </summary>
        public void EnterInvalidBankDetailsTxt()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.BSB, 30);
            _act.EnterText(_bankdetailsLoc.BSB, _testUtility.FraudBSB());
            //_act.EnterText(_bankdetailsLoc.AccountNumber, _testUtility.FraudAccountNo());
            //_act.EnterText(_bankdetailsLoc.AccountNumber, _testUtility.FixedAccNumber());
            _act.EnterText(_bankdetailsLoc.AccountNumber, "01234567");
            _act.EnterText(_bankdetailsLoc.NameonAccount, _testUtility.RandomAccName());
        }

        public string[] EnterBankDetailsTxtForBSBAcct1()
        {
            TestUtility _testUtility = new TestUtility();
            _act.waitForVisibilityOfElement(_bankdetailsLoc.BSB, 30);
            _act.EnterText(_bankdetailsLoc.BSB, _testUtility.RandomBSB());
            if (SelectedAccountCheckEnabled == "true")
                _act.EnterText(_bankdetailsLoc.AccountNumber, _testUtility.FixedAccNumber());
            else
            {
                _act.EnterText(_bankdetailsLoc.AccountNumber, _testUtility.RandomAccNumber());
            }
            _act.EnterText(_bankdetailsLoc.NameonAccount, _testUtility.RandomAccName());
            string[] numbers = new string[2];
            numbers[0] = _act.getValue(_bankdetailsLoc.BSB, "bsb");
            numbers[1] = _act.getValue(_bankdetailsLoc.AccountNumber, "AccountNumber");
            return numbers;
        }

        public void EnterBankDetailsUploadCSV(string bsb, string accountNumber, string nameonAccount)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.BSB, 30);
            _act.EnterText(_bankdetailsLoc.BSB, bsb);
            _act.EnterText(_bankdetailsLoc.AccountNumber, accountNumber);
            _act.EnterText(_bankdetailsLoc.NameonAccount, nameonAccount);
        }

        /// <summary>
        /// Clicks the upload link in CSV Upload.
        /// </summary>
        public void ClickUploadLnk()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.UploadLink, 120);
            Thread.Sleep(3000);
            // _act.click(_bankdetailsLoc.UploadLink, "Upload Link");
            _driver.FindElement(_bankdetailsLoc.UploadLink).Click();
        }

        public void UploadFile(string filepath)
        {
            AutoItX3 autoIT = new AutoItX3();
            //The winactivate based on selected browser. It is the title of window pop up
            autoIT.WinActivate("Open");
            Thread.Sleep(4000);
            autoIT.Send(filepath);
            Thread.Sleep(2000);
            autoIT.Send("{ENTER}");
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Clicks the upload link in CSV Upload.
        /// </summary>
        public void FileUpload(string filepath)
        {
            Thread.Sleep(1000);
            int i = 0;
            Window FileUpload = null;
            var processes = Process.GetProcessesByName("chrome");
            foreach (var proces in processes)
            {
                var Application = TestStack.White.Application.Attach(processes[i]);
                var wnd = Application.GetWindows();
                if (wnd.Count > 1)
                {
                    FileUpload = wnd[1];
                    break;
                }

                i++;
            }

            var Edit = FileUpload.GetMultiple(SearchCriteria.ByText("File name:"));
            // var Edit = FileUpload.GetMultiple(SearchCriteria.ByControlType(controlType,TestStack.White.UIItems. "combo box");
            Edit[2].Click();
            Edit[2].SetValue(filepath);

            Keyboard.Instance.HoldKey(KeyboardInput.SpecialKeys.RETURN);
            Keyboard.Instance.LeaveAllKeys();
        }

        /// <summary>
        /// update csv with current date
        /// </summary>
        public void UpdateBankstatement()
        {
            string projectLoc = Directory.GetCurrentDirectory();
            string fileLocation = Path.Combine(projectLoc, "TestData\\BankStatementCSVUpload.csv");
            string testdatafile = projectLoc.Replace("\\bin\\DebugLocalChrome", "");
            string testdatafileloc = testdatafile + "\\TestData\\BankStatementCSVUpload.csv";
            string input = File.ReadAllText(testdatafileloc);
            string inputLine = "";
            StringReader reader = new StringReader(input);
            List<List<string>> data = new List<List<string>>();
            while ((inputLine = reader.ReadLine()) != null)
            {
                if (inputLine.Trim().Length > 0)
                {
                    string[] inputArray = inputLine.Split(new char[] { ',' });
                    data.Add(inputArray.ToList());
                }
            }
            //sort data by every column
            for (int sortCol = data[0].Count() - 1; sortCol >= 0; sortCol--)
            {
                data.OrderBy(x => x[sortCol]);
            }
            //update date
            List<DateTime> alltimes = new List<DateTime>();
            for (int count = 0; count < data.Count; count++)
            {
                //DateTime old = Convert.ToDateTime(data[count][0]);
                DateTime old = DateTime.ParseExact(data[count][0], "dd/MM/yyyy", null);
                alltimes.Add(old);
            }
            alltimes.Sort();
            var _diff = (DateTime.Now - alltimes[alltimes.Count - 1]).TotalDays;
            double diffDays = _diff;
            for (int rowCount = 0; rowCount < data.Count; rowCount++)
            {
                //DateTime old = Convert.ToDateTime(data[rowCount][0]);
                DateTime old = DateTime.ParseExact(data[rowCount][0], "dd/MM/yyyy", null);
                DateTime latest = old.AddDays(diffDays - 1);
                string currentdate = latest.ToString("dd/MM/yyyy");
                data[rowCount][0] = currentdate.Replace("-", "/");
            }
            string output = string.Join("\r\n", data.AsEnumerable()
                .Select(x => string.Join(",", x.Select(y => y).ToArray())).ToArray());
            File.WriteAllText(testdatafileloc, output);
            File.Copy(testdatafileloc, fileLocation, true);
        }

        /// <summary>
        /// Clicks the confirm upload radio button.
        /// </summary>
        public void ClickConfirmUploadRBtn()
        {
            _act.Sync(_bankdetailsLoc.ConfirmCSVFile);
            Thread.Sleep(4000);
            _act.waitForVisibilityOfElement(_bankdetailsLoc.ConfirmCSVFile, 30);
            _act.click(_bankdetailsLoc.ConfirmCSVFile, "confirmCSV");
        }

        /// <summary>
        /// Clicks the upload continue button.
        /// </summary>
        public void ClickUploadContinueBtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.UploadContinueBtn, 30);
            _act.click(_bankdetailsLoc.UploadContinueBtn, "uploadcontinueBtn");
        }

        /// <summary>
        /// Clicks the Bank Details Continue button
        /// </summary>
        public void ClickBankDetailsContinueBtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.DetailsContinueBtn, 30);
            _act.click(_bankdetailsLoc.DetailsContinueBtn, "DetailsContinueBtn");
        }

        /// <summary>
        /// Clicks the upload csv bank details continue button
        /// </summary>
        public void ClickVerifyManualOpenBtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.VerifyManualOpen, 30);
            _act.click(_bankdetailsLoc.VerifyManualOpen, "VerifyManualOpen");
        }

        /// <summary>
        /// Clicks the account details button.
        /// </summary>
        public void ClickAcctDetailsBtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.ContinueConfirmAcctDetails, 60);
            _act.click(_bankdetailsLoc.ContinueConfirmAcctDetails, "DetailsContinueButton");
        }

        public bool IncomeDisabled()
        {
            bool flag = false;
            _act.waitForVisibilityOfElement(_bankdetailsLoc.LastIncome, 30);
            if (_act.isDisabled(_bankdetailsLoc.LastIncome, ""))
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        ///  Select_POL_ExistingSACC
        /// </summary>
        /// <param name="loanpurpose">first loan POL</param>         
        public void SelectPOLExistingSACC(string loanpurpose)
        {
            if (GetPlatform(_driver))
            {
                var polpurpose = By.XPath(".//*[@class='purposeRadioGrp']/label[contains(text(),'" + loanpurpose + "')]");
                _act.waitForVisibilityOfElement(polpurpose, 30);
                _act.click(polpurpose, "selectpurpose");
                _act.waitForVisibilityOfElement(By.XPath(".//button[@id='purpose-done-btn2']"), 30);
                _act.click(By.XPath(".//button[@id='purpose-done-btn2']"), "donePOL");
            }
            else
            {
                var Firstpolpurpose = By.XPath(".//*[@id='pol_1']//div/label[contains(text(),'" + loanpurpose + "')]");
                _act.waitForVisibilityOfElement(Firstpolpurpose, 30);
                _act.click(Firstpolpurpose, "selectpurpose");
            }
        }

        /// <summary>
        /// Select_POL_ExistingSACC
        /// </summary>
        /// <param name="firstloanpurpose">first loan POL</param>    
        /// <param name="secondloanpurpose">first loan POL</param>    
        public void SelectPOLExistingSACC(string firstloanpurpose, string secondloanpurpose)
        {
            if (GetPlatform(_driver))
            {
                var polpurpose = By.XPath(".//*[@class='purposeRadioGrp']/label[contains(text(),'" + firstloanpurpose + "')]");
                _act.waitForVisibilityOfElement(polpurpose, 30);
                _act.click(polpurpose, "selectpurpose");
                _act.waitForVisibilityOfElement(By.XPath(".//button[@id='purpose-done-btn2']"), 30);
                _act.click(By.XPath(".//button[@id='purpose-done-btn2']"), "donePOL");
            }
            else
            {
                var Firstpolpurpose = By.XPath(".//*[@id='pol_1']//div/label[contains(text(),'" + firstloanpurpose + "')]");
                _act.waitForVisibilityOfElement(Firstpolpurpose, 30);
                _act.click(Firstpolpurpose, "selectpurpose");
                _act.waitForVisibilityOfElement(By.XPath(".//h3[@aria-controls='pol_2']"), 30);
                _act.click(By.XPath(".//h3[@aria-controls='pol_2']"), "Second POL");
                var secondpolpurpose = By.XPath(".//*[@id='pol_2']//div/label[contains(text(),'" + secondloanpurpose + "')]");
                _act.waitForVisibilityOfElement(secondpolpurpose, 30);
                _act.click(secondpolpurpose, "selectpurpose");
            }
        }

        /// <summary>
        /// Select_Have_you_fully_repaid_Btn
        /// </summary>
        /// <param name="Repaid">first loan POL</param>         
        public void SelectFullyRepaidLoanbtn(bool Repaid)
        {
            if (Repaid)
            {
                _act.waitForVisibilityOfElement(_bankdetailsLoc.FullyrepaidOtherLoanYesBtn, 30);
                _act.click(_bankdetailsLoc.FullyrepaidOtherLoanYesBtn, "FullyrepaidOtherLoanYesBtn");
            }
            else
            {
                _act.waitForVisibilityOfElement(_bankdetailsLoc.FullyrepaidOtherLoanNoBtn, 30);
                _act.click(_bankdetailsLoc.FullyrepaidOtherLoanNoBtn, "FullyrepaidOtherLoanNoBtn");
            }
        }

        /// <summary>
        ///Select_Are_you_UptoDate_Btn
        /// </summary>
        /// <param name="UptoDate">first loan POL</param>         
        public void SelectAreyouUpdatewithLoanBtn(bool UptoDate)
        {
            if (UptoDate)
            {
                _act.waitForVisibilityOfElement(_bankdetailsLoc.UpToDatewithReaymentsYes, 30);
                _act.click(_bankdetailsLoc.UpToDatewithReaymentsYes, "UpToDatewithReaymentsYes");
            }
            else
            {
                _act.waitForVisibilityOfElement(_bankdetailsLoc.UpToDatewithReaymentsNo, 30);
                _act.click(_bankdetailsLoc.UpToDatewithReaymentsNo, "UpToDatewithReaymentsNo");
            }
        }

        /// <summary>
        /// Select_To_use_any_of_this_Nimble_loan_to_make_these_repayments_Btn
        /// </summary>
        /// <param name="NimblePay">first loan POL</param>         
        public void SelectUseNimbletoRepayLoanBtn(bool NimblePay)
        {
            if (NimblePay)
            {
                _act.waitForVisibilityOfElement(_bankdetailsLoc.IntendToUseNimbleLoanYes, 30);
                _act.click(_bankdetailsLoc.IntendToUseNimbleLoanYes, "IntendToUseNimbleLoanYes");
            }
            else
            {
                _act.waitForVisibilityOfElement(_bankdetailsLoc.IntendToUseNimbleLoanNo, 30);
                _act.click(_bankdetailsLoc.IntendToUseNimbleLoanNo, "IntendToUseNimbleLoanNo");
            }
        }

        /// <summary>
        /// Click Confirm SACCNames_Btn
        /// </summary>
        public void ClickConfirmSACCNamesBtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.ConfirmSACCNamesBtn, 30);
            _act.click(_bankdetailsLoc.ConfirmSACCNamesBtn, "ConfirmSACCNamesBtn");
        }

        /// <summary>
        /// Selects the income category from list.
        /// </summary>
        /// <param name="incomeCategory">The income category.</param>
        public void SelectIncomeCategoryLst(string incomeCategory)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IncomeCategory, 30);
            _act.selectByOptionText(_bankdetailsLoc.IncomeCategory, incomeCategory, "IncomeCategory");
        }

        /// <summary>
        /// Selects the seven income.
        /// </summary>
        /// <param name="income1">The income1.</param>
        /// <param name="income2">The income2.</param>
        /// <param name="income3">The income3.</param>
        /// <param name="income4">The income4.</param>
        /// <param name="income5">The income5.</param>
        /// <param name="income6">The income6.</param>
        /// <param name="income7">The income7.</param>
        public void SelectSevenIncome(string income1, string income2, string income3, string income4, string income5, string income6, string income7)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.SevenIncomeSouce1, 60);
            _act.selectByOptionText(_bankdetailsLoc.SevenIncomeSouce1, income1, "Income1");
            Thread.Sleep(1000);
            _act.selectByOptionText(_bankdetailsLoc.SevenIncomeSouce2, income2, "Income2");
            Thread.Sleep(1000);
            _act.selectByOptionText(_bankdetailsLoc.SevenIncomeSouce3, income3, "Income3");
            Thread.Sleep(1000);
            _act.selectByOptionText(_bankdetailsLoc.SevenIncomeSouce4, income4, "Income4");
            Thread.Sleep(1000);
            _act.selectByOptionText(_bankdetailsLoc.SevenIncomeSouce5, income5, "Income5");
            Thread.Sleep(1000);
            _act.selectByOptionText(_bankdetailsLoc.SevenIncomeSouce6, income6, "Income6");
            Thread.Sleep(1000);
            _act.selectByOptionText(_bankdetailsLoc.SevenIncomeSouce7, income7, "Income7");
        }

        /// <summary>
        /// Selects the income categori for deposit 1.
        /// </summary>
        /// <param name="incomeCategory">The income category.</param>
        public void SelectIncomeCategDeposit1(string incomeCategory)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IncomeCategoryDeposit1, 60);
            _act.selectByOptionText(_bankdetailsLoc.IncomeCategoryDeposit1, incomeCategory, "IncomeCategory1");
        }

        /// <summary>
        /// Selects the income categori for deposit 2.
        /// </summary>
        /// <param name="incomeCategory">The income category.</param>IncomeCategoryDeposit1
        public void SelectIncomeCategDeposit2(string incomeCategory)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IncomeCategoryDeposit2, 60);
            _act.selectByOptionText(_bankdetailsLoc.IncomeCategoryDeposit2, incomeCategory, "IncomeCategory2");
        }

        /// <summary>
        /// Selects reason from just checking option List.
        /// </summary>
        /// <param name="option">The option.</param>
        public void SelectJustCheckingOptionLst(string option)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.JustChecking, 60);
            _act.selectByOptionText(_bankdetailsLoc.JustChecking, option, "JustChecking");
        }

        /// <summary>
        /// Selects reason from just checking option List deposit 1.
        /// </summary>
        /// <param name="option">The option.</param>
        public void SelectJustCheckingDeposit1(string option)
        {
            IWebElement justCheckingElement = _driver.FindElement(_bankdetailsLoc.JustCheckingDeposit1);
            if (_act.isElementDisplayed(justCheckingElement))
                _act.selectByOptionText(_bankdetailsLoc.JustCheckingDeposit1, option, "JustCheckingDeposit1");
        }

        // select seven income categories by providing index number
        public void SelectIncomecategory(string incomecategory, string index)
        {
            _act.waitForVisibilityOfElement(By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-'" + index + "']"), 200);
            _act.selectByOptionText(By.XPath("(.//select[contains(@id,'IncomeCategory')][not(ancestor::div[contains(@class,'hidden')])])[last()-'" + index + "']"), incomecategory, "IncomeCategory");
        }

        // select just checking options by providing index number
        public void SelectJustCheckingOption(string justcheckoption, string index)
        {
            IWebElement justCheckingEle = _driver.FindElement(By.XPath("(.//select[contains(@id,'primaryincomeinconsistentordrop')][not(ancestor::div[contains(@class,'hidden')])])[last()-'" + index + "']"));
            if (_act.isElementDisplayed(justCheckingEle))
                _act.selectByOptionText(By.XPath("(.//select[contains(@id,'primaryincomeinconsistentordrop')][not(ancestor::div[contains(@class,'hidden')])])[last()-'" + index + "']"), justcheckoption, "Justcheckingoption");
        }

        // select just checking options by providing index number
        public void SelectJustCheckingforNoPrimaryIncome(string justcheckoption, string index)
        {
            Thread.Sleep(1500);
            IWebElement justCheckingEle = _driver.FindElement(By.XPath("(.//select[contains(@id,'incomeinconsistentordrop')][not(ancestor::div[contains(@class,'hidden')])])[last()-'" + index + "']"));
            if (_act.isElementDisplayed(justCheckingEle))
                _act.selectByOptionText(By.XPath("(.//select[contains(@id,'incomeinconsistentordrop')][not(ancestor::div[contains(@class,'hidden')])])[last()-'" + index + "']"), justcheckoption, "Justcheckingoption");
        }

        /// <summary>
        /// Verifies the just checking field for deposit 2 is displayed.
        /// </summary>
        /// <returns>true if enabled: false</returns>
        public bool VerifySelectJustCheckingDeposit2IsDisplayed()
        {
            IWebElement justCheckingElement = _driver.FindElement(_bankdetailsLoc.JustCheckingDeposit2);
            return _act.isElementDisplayed(justCheckingElement);
        }

        /// <summary>
        /// Selects reason from just checking option List deposit 2.
        /// </summary>
        /// <param name="Option">The option.</param>
        public void SelectJustCheckingDeposit2(string Option)
        {
            IWebElement justCheckingElement = _driver.FindElement(_bankdetailsLoc.JustCheckingDeposit2);
            if (_act.isElementDisplayed(justCheckingElement))
                _act.selectByOptionText(_bankdetailsLoc.JustCheckingDeposit2, Option, "JustCheckingDeposit2");
        }

        /// <summary>
        /// Clicks confirm income button.
        /// </summary>
        public void ClickConfirmIncomeBtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.ConfirmIncomeButton, 30);
            _act.click(_bankdetailsLoc.ConfirmIncomeButton, "ConfirmIncomeButton");
        }

        /// <summary>
        /// Click on continue button
        /// </summary>
        public void NoPrimaryIncomeContinueBtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.NoPrimaryContinueBtn, 30);
            _act.click(_bankdetailsLoc.NoPrimaryContinueBtn, "ContinueBtn");
        }

        /// <summary>
        /// Click on back button
        /// </summary>
        public void NoPrimaryIncome()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.NoPrimaryBackBtn, 30);
            _act.click(_bankdetailsLoc.NoPrimaryBackBtn, "BackBtn");
        }

        /// <summary>
        /// Clicks view transactions button.
        /// </summary>
        public void ClickViewTransactionsBtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.ViewTransactions, 30);
            _act.click(_bankdetailsLoc.ViewTransactions, "ViewTransactions");
        }

        /// <summary>
        ///validate merged bank statements description
        /// </summary>
        public bool ValidateTransactionDesc()
        {
            bool flag = false;
            int countele = _driver.FindElements(By.XPath(".//tr[@class='incomeDeposit-transactions incomeDeposit-transactions-list']//td[@class='incomeDeposit-transactions-description p-b-10']")).Count;
            for (int i = 0; i < countele; i++)
            {
                IWebElement ele =
                    _driver.FindElement(By.XPath(
                        "(.//tr[@class='incomeDeposit-transactions incomeDeposit-transactions-list']//td[@class='incomeDeposit-transactions-description p-b-10'])[last()-" +
                        i + "]"));
                string eletext = ele.Text;
                if (eletext.Contains("PAY JOHNSON AND CO") || eletext.Contains("Salary Cigniti"))
                    flag = true;
                else
                    flag = false;
            }
            return flag;
        }


        /// <summary>
        /// Gets expenses amount.
        /// </summary>
        /// <returns>int expenses amount</returns>
        public int ExpensesAmount()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.ExpenseAmount, 240);
            string ExpensestotalAmount = _act.getText(_bankdetailsLoc.ExpenseAmount, "Expenses Amount");
            ExpensestotalAmount = ExpensestotalAmount.Replace(",", "");
            ExpensestotalAmount = ExpensestotalAmount.Replace("$", "");

            var actamt = ExpensestotalAmount.Split('.');
            ExpensestotalAmount = actamt[0];
            ExpensestotalAmount = ExpensestotalAmount.Replace(".", "");
            int ActualAmount = Convert.ToInt32(ExpensestotalAmount);
            return ActualAmount;
        }

        /// <summary>
        ///validate merged bank statements amount
        /// </summary>
        public int ValidateTransactionAmount()
        {
            int totaltransamount = 0;
            int countele = _driver.FindElements(By.XPath(".//tr[@class='incomeDeposit-transactions incomeDeposit-transactions-list']//td[@class='incomeDeposit-transactions-amount p-b-10']")).Count;
            for (int i = 0; i < countele; i++)
            {
                IWebElement ele =
                    _driver.FindElement(By.XPath(
                        "(.//tr[@class='incomeDeposit-transactions incomeDeposit-transactions-list']//td[@class='incomeDeposit-transactions-amount p-b-10'])[last()-" +
                        i + "]"));
                string Transamount = ele.Text;
                Transamount = Transamount.Replace(",", "");
                Transamount = Transamount.Replace("$", "");
                var actamt = Transamount.Split('.');
                Transamount = actamt[0];
                int ActualtransAmount = Convert.ToInt32(Transamount);
                totaltransamount = totaltransamount + ActualtransAmount;
            }
            return totaltransamount;
        }

        /// <summary>
        /// Specific details are helpful, otherwise we may contact you
        /// </summary>
        public void EnterOtherReasonForSpike(string otherreason)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.OtherReasonSpike, 60);
            _act.EnterText(_bankdetailsLoc.OtherReasonSpike, otherreason);
        }

        /// <summary>
        /// Specific details are helpful, otherwise we may contact you
        /// </summary>
        public void EnterOtherReasonForOOC(string otherreason)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.OtherReasonOOC, 60);
            _act.EnterText(_bankdetailsLoc.OtherReasonOOC, otherreason);
        }

        /// <summary>
        /// Selects the "other debt" repayments option button.
        /// </summary>
        public void SelectOtherDebtRepaymentsOptionBtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.OtherDebtLiabilitiesoption, 120);
            _act.JSClick(_bankdetailsLoc.OtherDebtLiabilitiesoption, "OtherDebtLiabilitiesoption");
        }

        /// <summary>
        /// Selects the option from dependants list.
        /// </summary>
        /// <param name="dependants">The dependants.</param>
        public void SelectDependantsLst(string dependants)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.Dependants, 20);
            _act.selectByOptionText(_bankdetailsLoc.Dependants, dependants, "Dependants");
        }

        /// <summary>
        /// Clicks the confirm expenses button.
        /// </summary>
        public void ClickConfirmExpensesBtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.BtnConfirmExpenses, 10);
            _act.JSClick(_bankdetailsLoc.BtnConfirmExpenses, "BtnConfirmExpenses");
        }

        public void ClickEditIncome()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.EditIncome, 30);
            _act.click(_bankdetailsLoc.EditIncome, "");
        }

        public void ClickEditExpenses()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.EditExpenses, 30);
            _act.click(_bankdetailsLoc.EditExpenses, "");
        }

        public void EditExpenses()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.EnterLivingExpenses, 30);
            _driver.FindElement(_bankdetailsLoc.EnterLivingExpenses).Clear();
            _act.EnterText(_bankdetailsLoc.EnterLivingExpenses, "$2,200.28");
            _driver.FindElement(_bankdetailsLoc.EnterMortgageRepayments).Clear();
            _act.EnterText(_bankdetailsLoc.EnterMortgageRepayments, "$950.28");
        }

        /// <summary>
        /// Your Expenses screen alter Everything Living Value
        /// </summary>
        /// <param name="value"></param>
        public void EnterExpenseEverydayLivingTxt(string value)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.EnterLivingExpenses, 30);
            _driver.FindElement(_bankdetailsLoc.EnterLivingExpenses).Clear();
            _act.EnterText(_bankdetailsLoc.EnterLivingExpenses, value);
        }

        /// <summary>
        /// Your Expenses screen alter Other Debt Repayments value
        /// </summary>
        /// <param name="value"></param>
        public void EnterExpenseOtherDebtRepaymentsTxt(string value)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.EnterOtherDebtRepayments, 30);
            _driver.FindElement(_bankdetailsLoc.EnterOtherDebtRepayments).Clear();
            _act.EnterText(_bankdetailsLoc.EnterOtherDebtRepayments, value);
        }

        /// <summary>
        /// Your Expenses screen alter Mortgage/Rent value
        /// </summary>
        /// <param name="value"></param>
        public void EnterExpenseMortgageTxt(string value)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.EnterMortgageRepayments, 30);
            _driver.FindElement(_bankdetailsLoc.EnterMortgageRepayments).Clear();
            _act.EnterText(_bankdetailsLoc.EnterMortgageRepayments, value);
        }

        /// <summary>
        /// Your Expenses screen select No Rent reason ($0 rent value)
        /// </summary>
        /// <param name="value"></param>
        public void SelectExpenseNoRentLst(string rentReason)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.SelectNoRentReason, 30);
            _act.selectByOptionText(_bankdetailsLoc.SelectNoRentReason, rentReason, "SelectExpenseNoRentReasonLst");
        }

        /// <summary>
        /// Your expenses screen select No Rent ($0 rent value), Other as the reason and enter free text into field
        /// </summary>
        /// <param name="otherReason"></param>
        public void EnterNoRentOtherTxt(string otherReason)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.EnterNoRentOtherTxt, 30);
            _act.EnterText(_bankdetailsLoc.EnterNoRentOtherTxt, otherReason);
        }

        /// <summary>
        /// Your Expenses screen alter Large Ongoing Expenses value
        /// </summary>
        /// <param name="value"></param>
        public void EnterExpenseLargeOngoingTxt(string value)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.EnterLargeOngoingBills, 30);
            _act.EnterText(_bankdetailsLoc.EnterLargeOngoingBills, value);
        }

        /// <summary>
        /// Compare expected Living Expense value on Your Summary screen
        /// </summary>
        /// <param name="expectedValue"></param>
        /// <returns></returns>
        public bool CompareSummaryLivingExpensesTxt(string expectedValue)
        {
            bool flag = false;
            _act.waitForVisibilityOfElement(_bankdetailsLoc.CompareSummaryLivingExpensesTxt, 30);
            string currentValue = _act.getText(_bankdetailsLoc.CompareSummaryLivingExpensesTxt, "CompareSummaryLivingExpensesTxt");
            var tempValue = currentValue.Replace("$", "");
            if (tempValue.Contains(","))
            {
                tempValue = tempValue.Replace(",", "");
            }
            string actualValue = tempValue;
            if (expectedValue == actualValue)
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Compare expected Other Debts value on Your Summary screen
        /// </summary>
        /// <param name="expectedValue"></param>
        /// <returns></returns>
        public bool CompareSummaryOtherDebtTxt(string expectedValue)
        {
            bool flag = false;
            _act.waitForVisibilityOfElement(_bankdetailsLoc.CompareSummaryOtherDebtTxt, 30);
            string currentValue = _act.getText(_bankdetailsLoc.CompareSummaryOtherDebtTxt, "CompareSummaryOtherDebtTxt");
            var tempValue = currentValue.Replace("$", "");
            if (tempValue.Contains(","))
            {
                tempValue = tempValue.Replace(",", "");
            }
            string actualValue = tempValue;
            if (expectedValue == actualValue)
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Compare expected Mortgage/Rent value on Your Summary screen
        /// </summary>
        /// <param name="expectedValue"></param>
        /// <returns></returns>
        public bool CompareSummaryRentMortgageTxt(string expectedValue)
        {
            bool flag = false;
            _act.waitForVisibilityOfElement(_bankdetailsLoc.CompareSummaryRentMortgageTxt, 30);
            string currentValue = _act.getText(_bankdetailsLoc.CompareSummaryRentMortgageTxt, "CompareSummaryRentMortgageTxt");
            var tempValue = currentValue.Replace("$", "");
            if (tempValue.Contains(","))
            {
                tempValue = tempValue.Replace(",", "");
            }
            string actualValue = tempValue;
            if (expectedValue == actualValue)
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Compare expected Large Ongoing Bills value on Your Summary screen
        /// </summary>
        /// <param name="expectedValue"></param>
        /// <returns></returns>
        public bool CompareSummaryLargeOngoingBillsTxt(string expectedValue)
        {
            bool flag = false;
            _act.waitForVisibilityOfElement(_bankdetailsLoc.CompareSummaryLargeOngoingBillsTxt, 30);
            string currentValue = _act.getText(_bankdetailsLoc.CompareSummaryLargeOngoingBillsTxt, "CompareSummaryLargeOngoingBillsTxt");
            var tempValue = currentValue.Replace("$", "");
            if (tempValue.Contains(","))
            {
                tempValue = tempValue.Replace(",", "");
            }
            string actualValue = tempValue;
            if (expectedValue == actualValue)
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Clicks No on govt benefits option List.
        /// </summary>
        public void ClickNoGovtBenefitsbtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.GovtBenefits, 60);
            _act.JSClick(_bankdetailsLoc.GovtBenefits, "GovtBenefits");
        }

        /// <summary>
        /// Clicks Yes on govt benefits option List.
        /// </summary>
        public void ClickYesGovtBenefitsbtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.GovtBenefitsYes, 30);
            _act.click(_bankdetailsLoc.GovtBenefitsYes, "GovtBenefits");
        }

        /// <summary>
        /// Clicks the verify button in Returner Loaner.
        /// </summary>
        public void ClickVerifyButtonRL()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.ClickVerifyButtonRL, 60);
            _act.click(_bankdetailsLoc.ClickVerifyButtonRL, "ClickVerifyButtonRL");
        }

        /// <summary>
        /// Clicks the agree application submit button.
        /// </summary>
        public void ClickAgreeAppSubmitBtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.AgreeApplicationsubmit, 10);
            _act.click(_bankdetailsLoc.AgreeApplicationsubmit, "AgreeApplicationsubmit");
        }

        /// <summary>
        /// Clicks the confirm summary button.
        /// </summary>
        public void ClickConfirmSummaryBtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.BtnConfirmSummary, 10);
            _act.click(_bankdetailsLoc.BtnConfirmSummary, "BtnConfirmSummary");
        }

        public void VerifyGovtDNQ()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.GovtDNQText, 60);
            string DNQ = _act.getText(_bankdetailsLoc.GovtDNQText, "GovtDNQText");
            // Assert.IsTrue(DNQ.Contains("Sorry"));
        }

        /// <summary>
        /// Enters the otp details.
        /// </summary>
        /// <param name="otp">The otp.</param>
        public void EnterOTPDetailsTxt(string otp)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.SMSDiv, 120);
            _act.EnterText(_bankdetailsLoc.SMSInput, otp);
            _act.JSClick(_bankdetailsLoc.SubmitPin, "SubmitPin");
        }

        /// <summary>
        /// Verifies the OPT for SMS.
        /// </summary>
        /// <returns>bool if exist:true else false</returns>
        public bool VerifySMSOTP()
        {
            bool flag = false;

            _act.waitForVisibilityOfElement(_bankdetailsLoc.SMSDiv, 500);

            if (_act.isElementPresent(_bankdetailsLoc.SMSDiv))
            {

                if (_act.isElementDisplayed(_driver.FindElement(_bankdetailsLoc.SMSDiv)))
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            return flag;
        }

        /// <summary>
        /// Clicks the loan dashboard button.
        /// </summary>
        public void ClickLoanDashboardBtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.LoanDashboardButton, 30);
            _act.click(_bankdetailsLoc.LoanDashboardButton, "LoanDashboardButton");

        }

        public void ClickSubmitBtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.ButtonSubmit, 30);
            _act.click(_bankdetailsLoc.ButtonSubmit, "ButtonSubmit");
        }

        /// <summary>
        /// Click sixties minutes button for Returner Loaner.
        /// </summary>
        public void ClicksixtyMinuteButton()
        {
            //Commented as this will be no longer displayed if the user does not have nimble card enabled
            //_act.waitForVisibilityOfElement(_bankdetailsLoc.SixtyMinuteButton, 350);
            //_act.click(_bankdetailsLoc.SixtyMinuteButton, "sixtyMinuteButton");
        }

        /// <summary>
        /// Clicks the submit payment button.
        /// </summary>
        public void ClickSubmitPaymentButton()
        {
            // _act.waitForVisibilityOfElement(_bankdetailsLoc.SubmitPaymentBtn, 200);
            // _act.click(_bankdetailsLoc.SubmitPaymentBtn, "submitpayment");
        }

        /// <summary>
        /// Clicks the loan dashboard manual.
        /// </summary>
        public void ClickLoanDashboardManual()
        {
            Thread.Sleep(2000);
            _act.waitForVisibilityOfElement(_bankdetailsLoc.LoanDashboardManual, 120);
            _act.click(_bankdetailsLoc.LoanDashboardManual, "LoanDashboardManual");
        }

        /// <summary>
        /// Clicks the setup button.
        /// </summary>
        public void ClickSetup()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.SetupManual, 30);
            _act.click(_bankdetailsLoc.SetupManual, "SetupManual");
        }

        /// <summary>
        /// Clicks the final approval from Testing options.
        /// </summary>
        public void ClickFinalApproval()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.FinalApprove, 30);
            _act.click(_bankdetailsLoc.FinalApprove, "FinalApprove");
        }

        /// <summary>
        /// Gets Error Message for No Transcations Error page
        /// </summary>
        public string GetErrorMessage()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.OopsError, 30);
            return _act.getText(_bankdetailsLoc.OopsError, "Oops Error");
        }

        /// <summary>
        /// Gets Error Message for No Transcations Error page
        /// </summary>
        public string GetBSErrorMessage()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.BSError, 30);
            return _act.getText(_bankdetailsLoc.BSError, "Oops Error");
        }

        /// <summary>
        /// Enters the text for other option.
        /// </summary>
        /// <param name="strText">The string text.</param>
        public void EnterTxtForOtherOption(string strText)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.PleaseSelectReasonForOther, 30);
            _act.EnterText(_bankdetailsLoc.PleaseSelectReasonForOther, strText);
        }

        /// <summary>
        /// Fetches the account ID and BSB.
        /// </summary>
        public void FetchAcctandBSB()
        {
            var approvedLoanAmt = _act.getText(_bankdetailsLoc.AcctBSBLastPage, "ACCTNO AND BSB");
            var data = approvedLoanAmt.Split(':');
            var splitteddata = data[1];
            var splitteddata1 = splitteddata.Split(' ');
            var BSB = Convert.ToInt32(splitteddata1[1]);
            var splitteddata3 = data[2];
            splitteddata3 = splitteddata3.Remove(' ');
            int AcctNo = Convert.ToInt32(splitteddata3);
        }

        /// <summary>
        /// Verify Spike in Income and corresponding text.
        /// </summary>
        public bool VerifySpikeQuestionText(string SpikeText)
        {
            bool flag = false;
            _act.waitForVisibilityOfElement(_bankdetailsLoc.SpikeText, 30);
            string obsspiketext = _act.getText(_bankdetailsLoc.SpikeText, "SpikeText");
            if (obsspiketext.Contains(SpikeText))
                flag = true;
            else
                flag = false;
            return flag;
        }

        /// <summary>
        /// Verify OOC in Income and corresponding text.
        /// </summary>
        public bool VerifyOOCQuestionText(string OOCText)
        {
            bool flag = false;
            _act.waitForVisibilityOfElement(_bankdetailsLoc.OOCText, 30);
            string obsooctext = _act.getText(_bankdetailsLoc.OOCText, "OOCText");
            if (obsooctext.Contains(OOCText))
                flag = true;
            else
                flag = false;
            return flag;
        }

        /// <summary>
        /// Select a reason for Spike in Income.
        /// </summary>
        public void SelectReasonforSpikequestion(string spikereason)
        {
            IWebElement reasonforspike = _driver.FindElement(_bankdetailsLoc.SpikeQuestionOption);
            if (_act.isElementDisplayed(reasonforspike))
                _act.selectByOptionText(_bankdetailsLoc.SpikeQuestionOption, spikereason, "SpikeReason");
        }

        /// <summary>
        /// Select a reason for OOC in Income.
        /// </summary>
        public void SelectReasonforOOCquestion(string oocreason)
        {
            IWebElement reasonforooc = _driver.FindElement(_bankdetailsLoc.OOCQuestionOption);
            if (_act.isElementDisplayed(reasonforooc))
                _act.selectByOptionText(_bankdetailsLoc.OOCQuestionOption, oocreason, "OOCReason");
        }

        public void bankFunctions(string bankname, string UID, string password, string primaryIncome, string dependants, string SMSCode, int loanamount)
        {
            // select Bank Name  
            SelectBankLst(bankname);

            // Click on Continue Button
            BankSelectContinueBtn();

            // Entering Username and Password
            EnterBankCredentialsTxt(UID, password);

            // Click on Continue Button
            ClickAutoContinueBtn();

            // choose bank account
            BankAccountSelectBtn();

            // Click on bank select Continue Button
            ClickBankAccountContBtn();

            // Confirm Bank Details
            EnterBankDetailsTxt();

            // Click on Confirm account details Continue Button  
            ClickAcctDetailsBtn();

            // Select Category 
            SelectIncomecategory(primaryIncome, "0");

            // Select Just checking option 
            //_bankDetails.SelectJustCheckingOptionLst("Yes, it will stay the same (or more)");

            // click on Confirm Income Button
            ClickConfirmIncomeBtn();

            // select  other debt repayments option No 
            SelectOtherDebtRepaymentsOptionBtn();

            // select dependents 
            SelectDependantsLst(dependants);

            // Click on continue
            ClickConfirmExpensesBtn();

            // select Governments benefits option No
            ClickNoGovtBenefitsbtn();

            // click on Agree that information True
            ClickAgreeAppSubmitBtn();

            // click on confirm Submit button
            ClickConfirmSummaryBtn();

            if (loanamount > 2000)
            {
                // enter sms input as OTP 
                if (VerifySMSOTP())
                    EnterOTPDetailsTxt(SMSCode);
            }
        }

        public void bankFunctions_RL(string bankname, string UID, string password, string primaryIncome, string dependants)
        {
            // select Bank Name  
            SelectBankLst(bankname);

            // Click on Continue Button
            BankSelectContinueBtn();

            // Entering Username and Password
            EnterBankCredentialsTxt(UID, password);

            // Click on Continue Button
            ClickAutoContinueBtn();

            // choose bank account
            BankAccountSelectBtn();

            // Click on bank select Continue Button
            ClickBankAccountContBtn();

            // Confirm Bank Details
            EnterBankDetailsTxt();

            // Click on Confirm account details Continue Button  
            ClickAcctDetailsBtn();

            // Select Category 
            SelectIncomecategory(primaryIncome, "0");

            // Select Just checking option 
            //_bankDetails.SelectJustCheckingOptionLst("Yes, it will stay the same (or more)");

            // click on Confirm Income Button
            ClickConfirmIncomeBtn();

            // select  other debt repayments option No 
            SelectOtherDebtRepaymentsOptionBtn();

            // select dependents 
            SelectDependantsLst(dependants);

            // Click on continue
            ClickConfirmExpensesBtn();

            // select Governments benefits option No
            ClickNoGovtBenefitsbtn();

            // click on Agree that information True
            ClickAgreeAppSubmitBtn();

            // click on confirm Submit button
            ClickConfirmSummaryBtn();

            if (GetPlatform(_driver))
            {
                // click on Button Submit
                ClickSubmitBtn();

                // Click on Bank Account to transfer
                ClicksixtyMinuteButton();

                // click on sublit-payment Button
                ClickSubmitPaymentButton();
            }
            else
            {
                // Click on Bank Account to transfer
                ClicksixtyMinuteButton();

                // click on Buton Submit
                ClickSubmitBtn();
            }
        }

        /// <summary>
        /// Gets the ooc transaction date text.
        /// </summary>
        /// <returns></returns>
        public string GetOOCTransactionDateTxt()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdentifiedOOCTransactionDate, 60);
            return _act.getText(_bankdetailsLoc.IdentifiedOOCTransactionDate, "Transcation Date");
        }

        // By.XPath(".//tr[@class='incomeDeposit-outofcycle']//div[@class='sand-lighter-bg charcoal-dark font-12']//td[1]");

        /// <summary>
        /// Gets the transaction date from statement text.
        /// </summary>
        /// <param name="rownumber">The rownumber.</param>
        /// <returns></returns>
        public string GetTransactionDateFromStatementTxt(int rownumber)
        {
            By val = By.XPath(".//tr[@class='incomeDeposit-outofcycle']//div[@class='sand-lighter-bg charcoal-dark font-12']//td[" + rownumber.ToString() + "]");

            _act.waitForVisibilityOfElement(val, 60);

            return _act.getText(val, "Transcation Date");
        }

        /// <summary>
        /// Gets the ooc transaction amount text.
        /// </summary>
        /// <returns></returns>
        public string GetOOCTransactionAmountTxt()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdentifiedOOCTransactionAmount, 60);
            string strval = _act.getText(_bankdetailsLoc.IdentifiedOOCTransactionAmount, "Transcation Amount");
            return strval;
        }

        /// <summary>
        /// Gets the ooc transaction description text.
        /// </summary>
        /// <returns></returns>
        public string GetOOCTransactionDescriptionTxt()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdentifiedOOCTransactionDescription, 60);
            return _act.getText(_bankdetailsLoc.IdentifiedOOCTransactionDescription, "Transcation Description");
        }

        /// <summary>
        /// Gets the spike transaction date text.
        /// </summary>
        /// <returns></returns>
        public string GetSpikeTransactionDateTxt()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdentifiedSpikeTransactionDate, 60);
            return _act.getText(_bankdetailsLoc.IdentifiedSpikeTransactionDate, "Transcation Date");
        }

        /// <summary>
        /// Gets the spike transaction amount text.
        /// </summary>
        /// <returns></returns>
        public string GetSpikeTransactionAmountTxt()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdentifiedSpikeTransactionAmount, 60);
            string strval = _act.getText(_bankdetailsLoc.IdentifiedSpikeTransactionAmount, "Transcation Amount");
            return strval;
        }

        /// <summary>
        /// Gets the spike transaction description text.
        /// </summary>
        /// <returns></returns>
        public string GetSpikeTransactionDescriptionTxt()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdentifiedSpikeTransactionDescription, 60);
            return _act.getText(_bankdetailsLoc.IdentifiedSpikeTransactionDescription, "Transcation Description");
        }

        /// <summary>
        /// Agree to identification authorisation text
        /// </summary>
        public void CheckIdAuthorisationChkbx()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.CheckIdAuthorisationChkbx, 120);
            _act.click(_bankdetailsLoc.CheckIdAuthorisationChkbx, "CheckIdAuthorisationChkbx");
        }

        /// <summary>
        /// Identification page - select identify type
        /// </summary>
        /// <param name="identificationType"></param>
        public void SelectIdTypeLst(string identificationType)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdTypeLst, 30);
            _act.selectByOptionText(_bankdetailsLoc.IdTypeLst, identificationType, "SelectIdTypeLst");
        }

        /// <summary>
        /// Identification page - select Medicare ref no
        /// </summary>
        /// <param name="refNo"></param>
        public void EnterIdMedicareRefNoTxt(string refNo)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdMedicareRefNoTxt, 30);
            _act.EnterText(_bankdetailsLoc.IdMedicareRefNoTxt, refNo);
        }

        /// <summary>
        /// Identification page - select Medicare card colour
        /// </summary>
        /// <param name="cardColour"></param>
        public void SelectIdMedicareCardColourLst(string cardColour)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdMedicareCardColourLst, 30);
            _act.selectByOptionText(_bankdetailsLoc.IdMedicareCardColourLst, cardColour, "SelectIdMedicareCardColourLst");
        }

        /// <summary>
        /// Identification page - Medicare card number
        /// </summary>
        public void EnterIdMedicareCardNumberTxt()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdMedicareCardNumberTxt, 30);
            _act.EnterText(_bankdetailsLoc.IdMedicareCardNumberTxt, _testUtility.RandomNumber(10));
        }

        /// <summary>
        /// Identification page - Medicare card number
        /// </summary>
        /// <param name="cardNumber"></param>
        public void EnterIdMedicareCardNumberTxt(string cardNumber)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdMedicareCardNumberTxt, 30);
            _act.EnterText(_bankdetailsLoc.IdMedicareCardNumberTxt, cardNumber);
        }

        /// <summary>
        /// Identification page - Medicare card name
        /// </summary>
        /// <param name="cardName"></param>
        public void EnterIdMedicareCardNameTxt(string cardName)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdMedicareCardNameTxt, 30);
            _act.EnterText(_bankdetailsLoc.IdMedicareCardNameTxt, cardName);
        }

        /// <summary>
        /// Identification page - Medicare Expiry Date Month
        /// </summary>
        /// <param name="expiryDateMonth"></param>
        public void SelectIdMedicareExpiryDateMonthLst(string expiryDateMonth)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdMedicareExpiryDateMonthLst, 30);
            _act.selectByOptionText(_bankdetailsLoc.IdMedicareExpiryDateMonthLst, expiryDateMonth, "SelectIdMedicareExpiryDateMonthLst");
        }

        /// <summary>
        /// Identification page - Medicare Expiry Date Year
        /// </summary>
        /// <param name="expiryDateYear"></param>
        public void SelectIdMedicareExpiryDateYearLst(string expiryDateYear)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdMedicareExpiryDateYearLst, 30);
            _act.selectByOptionText(_bankdetailsLoc.IdMedicareExpiryDateYearLst, expiryDateYear, "SelectIdMedicareExpiryDateYearLst");
        }

        /// <summary>
        /// Identifcation page - Submit button
        /// </summary>
        public void ClickIdSubmitBtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.ClickIdSubmitBtn, 30);
            _act.click(_bankdetailsLoc.ClickIdSubmitBtn, "ClickIdSubmitBtn");
        }

        /// <summary>
        /// Identification page - Australian Passport Number
        /// </summary>
        public void EnterIdAustralianPassportNumberTxt()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdAustralianPassportNumberTxt, 30);
            _act.EnterText(_bankdetailsLoc.IdAustralianPassportNumberTxt, _testUtility.RandomNumber(6));
        }

        /// <summary>
        /// Identification page - Australian Passport Number
        /// </summary>
        /// <param name="passportNumber"></param>
        public void EnterIdAustralianPassportNumberTxt(string passportNumber)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdAustralianPassportNumberTxt, 30);
            _act.EnterText(_bankdetailsLoc.IdAustralianPassportNumberTxt, passportNumber);
        }

        /// <summary>
        /// Identification page - Australian Passport Country of Birth
        /// </summary>
        /// <param name="birthCountry"></param>
        public void SelectIdAustralianPassportCountyBirthLst(string birthCountry)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdAustralianPassportCountyBirthLst, 30);
            _act.selectByOptionText(_bankdetailsLoc.IdAustralianPassportCountyBirthLst, birthCountry, "SelectIdAustralianPassportCountyBirthLst");
        }

        /// <summary>
        /// Identification page - Australian Passport Place of Birth
        /// </summary>
        /// <param name="birthPlace"></param>
        public void EnterIdAustralianPassportPlaceOfBirthTxt(string birthPlace)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdAustralianPassportPlaceOfBirthTxt, 30);
            _act.EnterText(_bankdetailsLoc.IdAustralianPassportPlaceOfBirthTxt, birthPlace);
        }

        /// <summary>
        /// Identification page - Australian Passport Surname at Birth
        /// </summary>
        /// <param name="surname"></param>
        public void EnterIdAustralianPassportSurnameAtBirthTxt(string surname)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdAustralianPassportSurnameAtBirthTxt, 30);
            _act.EnterText(_bankdetailsLoc.IdAustralianPassportSurnameAtBirthTxt, surname);
        }

        /// <summary>
        /// Identification page - Australian Passport Surname at Citizenship
        /// </summary>
        /// <param name="surname"></param>
        public void EnterIdAustralianPassportSurnameAtCitizenshipTxt(string surname)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdAustralianPassportSurnameAtCitizenshipTxt, 30);
            _act.EnterText(_bankdetailsLoc.IdAustralianPassportSurnameAtCitizenshipTxt, surname);
        }

        /// <summary>
        /// Identification page - International Passport Number
        /// </summary>
        public void EnterIdInternationalPassportNumberTxt()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdInternationalPassportNumberTxt, 30);
            _act.EnterText(_bankdetailsLoc.IdInternationalPassportNumberTxt, _testUtility.RandomNumber(6));
        }

        /// <summary>
        /// Identification page - International Passport Number
        /// </summary>
        /// <param name="passportNumber"></param>
        public void EnterIdInternationalPassportNumberTxt(string passportNumber)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdInternationalPassportNumberTxt, 30);
            _act.EnterText(_bankdetailsLoc.IdInternationalPassportNumberTxt, passportNumber);
        }

        /// <summary>
        /// Identification page - International Passport Country
        /// </summary>
        /// <param name="passportCountry"></param>
        public void SelectIdInternationalPassportCountryLst(string passportCountry)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdInternationalPassportCountryLst, 30);
            _act.selectByOptionText(_bankdetailsLoc.IdInternationalPassportCountryLst, passportCountry, "SelectIdInternationalPassportCountryLst");
        }

        /// <summary>
        /// Identification page - Drivers License Name
        /// </summary>
        /// <param name="licenseName"></param>
        public void EnterIdDriversLicenceNameTxt(string licenceName)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdDriversLicenceNameTxt, 30);
            _act.EnterText(_bankdetailsLoc.IdDriversLicenceNameTxt, licenceName);
        }

        /// <summary>
        /// Identification page - Drivers License State
        /// </summary>
        /// <param name="licenseState"></param>
        public void SelectIdDriversLicenceStateLst(string licenceState)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdDriversLicenceStateLst, 30);
            _act.selectByOptionText(_bankdetailsLoc.IdDriversLicenceStateLst, licenceState, "SelectIdDriversLicenceStateLst");
        }

        /// <summary>
        /// Identification page - Drivers License Number
        /// </summary>
        public void EnterIdDriversLicenceNumberTxt()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdDriversLicenceNumberTxt, 30);
            _act.EnterText(_bankdetailsLoc.IdDriversLicenceNumberTxt, _testUtility.RandomNumber(6));
        }

        public bool SessionTimeout()
        {
            bool flag = false;
            _act.waitForVisibilityOfElement(_bankdetailsLoc.SessionTimeout, 500);
            if (_act.isElementDisplayed(_driver.FindElement(_bankdetailsLoc.SessionTimeout)))
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Identification page - Drivers License Number
        /// </summary>
        /// <param name="driversLicence"></param>
        public void EnterIdDriversLicenceNumberTxt(string driversLicence)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IdDriversLicenceNumberTxt, 30);
            _act.EnterText(_bankdetailsLoc.IdDriversLicenceNumberTxt, driversLicence);
        }

        public string VerifyMissedRepaymentMessage()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.MissedRepaymentMessage, 120);
            string message = _act.getText(_bankdetailsLoc.MissedRepaymentMessage, "");
            return message;
        }

        public string GetMissedRepaymentFirstPage()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.MissedRepaymentFirstPage, 60);
            string missed = _act.getText(_bankdetailsLoc.MissedRepaymentFirstPage, "");
            return missed;
        }

        public void ClickRescheduleButton()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.RescheduleButton, 30);
            _act.click(_bankdetailsLoc.RescheduleButton, "selectpurpose");
        }

        public void ClickDivideCheckBox()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.DividePayment, 30);
            _act.JSClick(_bankdetailsLoc.DividePayment, "DividePayment");
        }

        public void ClickExtentCheckBox()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.ExtentPayment, 30);
            _act.JSClick(_bankdetailsLoc.ExtentPayment, "ExtentPayment");
        }

        public string getUpcomingRepaymentFirstPage()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.UpcomingRepaymentFirstPage, 30);
            string upcoming = _act.getText(_bankdetailsLoc.UpcomingRepaymentFirstPage, "");
            var a = upcoming.Split('$');
            string upcoming1 = a[1];

            return "$" + upcoming1;
        }

        public string GetUpcomingRepaymentFirstPageExtend()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.UpcomingRepaymentFirstPageExtend, 30);
            string upcoming = _act.getText(_bankdetailsLoc.UpcomingRepaymentFirstPageExtend, "");
            var a = upcoming.Split('$');
            string upcoming1 = a[1];

            return "$" + upcoming1;

        }

        public void ClickRescheduleContinueButton()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.RescheduleContinue, 30);
            _act.JSClick(_bankdetailsLoc.RescheduleContinue, "RescheduleContinue");
        }

        public string VerifyRescheduleMessage()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.RescheduleMessage, 30);
            string message = _act.getText(_bankdetailsLoc.RescheduleMessage, "");
            return message;
        }

        public string PrefailUpcomingRepaymentFinalPage()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.UpcomingRepaymentFinalPage, 30);
            string upcoming = _act.getText(_bankdetailsLoc.UpcomingRepaymentFinalPage, "");
            return upcoming;
        }

        public string GetUpcomingRepaymentLastPageExtend()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.UpcomingRepaymentFinalPageExtend, 30);
            string upcoming = _act.getText(_bankdetailsLoc.UpcomingRepaymentFinalPageExtend, "");
            return upcoming;
        }

        public string GetMissedRepaymentLastPage()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.MissedRepaymentFinalPage, 30);
            string missed = _act.getText(_bankdetailsLoc.MissedRepaymentFinalPage, "");
            return missed;
        }

        public string GetCheckPaymentMessage()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.PaymentUnsuccessful, 30);
            return _act.getText(_bankdetailsLoc.PaymentUnsuccessful, "PaymentMessage");
        }

        public string GetCheckLoanPaidTxt()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.LoanRepaid, 30);
            return _act.getText(_bankdetailsLoc.LoanRepaid, "LoanRepaidMessage");
        }

        public string GetIncorrectCardNoTxt()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IncorrectCardNo, 30);
            return _act.getText(_bankdetailsLoc.IncorrectCardNo, "Looks like this card number is incorrect. Please check it again before continuing.");
        }

        public string GetIncorrectDateTxt()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.IncorrectDate, 30);
            return _act.getText(_bankdetailsLoc.IncorrectDate, "This card has now expired.");
        }

        /// <summary>
        /// Bank Details - on bank selection screen, check masked account name
        /// e.g. TAxxx7812
        /// </summary>
        /// <returns></returns>
        public string GetMaskedAccountName()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.CheckMaskedAccountName, 30);
            return _act.getText(_bankdetailsLoc.CheckMaskedAccountName, "CheckMaskedAccountBSB");
        }

        /// <summary>
        /// Bank Details - on bank selection screen, check masked account number
        /// e.g. xxxx5xxx
        /// </summary>
        /// <returns></returns>
        public string GetMaskedAccountNumber()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.CheckMaskedAccountNumber, 30);
            return _act.getText(_bankdetailsLoc.CheckMaskedAccountNumber, "CheckMaskedAccountNumber");
        }

        /// <summary>
        /// Bank Details - on bank selection screen, check account type next to masked account details
        /// e.g. savings
        /// </summary>
        /// <returns></returns>
        public string GetMaskedAccountType()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.CheckMaskedAccountType, 30);
            return _act.getText(_bankdetailsLoc.CheckMaskedAccountType, "CheckMaskedAccountType");
        }

        public void enterANZBankCustRegNumber(string registrationNumber)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.ANZRegNumber, 60);
            _act.EnterText(_bankdetailsLoc.ANZRegNumber, registrationNumber);
        }

        public void enterANZBankPassword(string password)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.ANZPassword, 60);
            _act.EnterText(_bankdetailsLoc.ANZPassword, password);
        }

        public void enterANZBankOperatorId(string operatorId)
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.ANZOperatorId, 60);
            _act.EnterText(_bankdetailsLoc.ANZOperatorId, operatorId);
        }

        public void enterBankDetailsTxtForWestpec()
        {
            //Enter BSB
            _act.waitForVisibilityOfElement(_bankdetailsLoc.BSB, 60);
            _act.EnterText(_bankdetailsLoc.BSB, _testUtility.BSBForWestpecBank());

            //Enter Fixed Account number
            _act.EnterText(_bankdetailsLoc.AccountNumber, _testUtility.FixedAccNumber());

            //Enter name on account
            _act.EnterText(_bankdetailsLoc.NameonAccount, _testUtility.nameOnAccountWestpec());
        }

        public string getALMErrorMessage()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.ALMErrorMessage, 120);
            string almErrorMessage = _act.getText(_bankdetailsLoc.ALMErrorMessage, "ALM Error message");
            Console.WriteLine("Error message is: " + almErrorMessage);
            return almErrorMessage;
        }

        public void UpdateBankstatementTransDesc280()
        {
            string projectLoc = Directory.GetCurrentDirectory();
            string fileLocation = Path.Combine(projectLoc, "TestData\\BankStatementCSVUpload280.csv");
            string testdatafile = projectLoc.Replace("\\bin\\DebugLocalChrome", "");
            string testdatafileloc = testdatafile + "\\TestData\\BankStatementCSVUpload280.csv";
            string input = File.ReadAllText(testdatafileloc);
            string inputLine = "";
            StringReader reader = new StringReader(input);
            List<List<string>> data = new List<List<string>>();
            while ((inputLine = reader.ReadLine()) != null)
            {
                if (inputLine.Trim().Length > 0)
                {
                    string[] inputArray = inputLine.Split(new char[] { ',' });
                    data.Add(inputArray.ToList());
                }
            }
            //sort data by every column
            for (int sortCol = data[0].Count() - 1; sortCol >= 0; sortCol--)
            {
                data.OrderBy(x => x[sortCol]);
            }
            //update date
            List<DateTime> alltimes = new List<DateTime>();
            for (int count = 0; count < data.Count; count++)
            {
                //DateTime old = Convert.ToDateTime(data[count][0]);
                DateTime old = DateTime.ParseExact(data[count][0], "dd/MM/yyyy", null);
                alltimes.Add(old);
            }
            alltimes.Sort();
            var _diff = (DateTime.Now - alltimes[alltimes.Count - 1]).TotalDays;
            double diffDays = _diff;
            for (int rowCount = 0; rowCount < data.Count; rowCount++)
            {
                //DateTime old = Convert.ToDateTime(data[rowCount][0]);
                DateTime old = DateTime.ParseExact(data[rowCount][0], "dd/MM/yyyy", null);
                DateTime latest = old.AddDays(diffDays - 1);
                string currentdate = latest.ToString("dd/MM/yyyy");
                data[rowCount][0] = currentdate.Replace("-", "/");
            }
            string output = string.Join("\r\n", data.AsEnumerable()
                .Select(x => string.Join(",", x.Select(y => y).ToArray())).ToArray());
            File.WriteAllText(testdatafileloc, output);
            File.Copy(testdatafileloc, fileLocation, true);
        }

        public void UpdateBankstatementTransDesc2000()
        {
            string projectLoc = Directory.GetCurrentDirectory();
            string fileLocation = Path.Combine(projectLoc, "TestData\\BankStatementCSVUpload2000.csv");
            string testdatafile = projectLoc.Replace("\\bin\\DebugLocalChrome", "");
            string testdatafileloc = testdatafile + "\\TestData\\BankStatementCSVUpload2000.csv";
            string input = File.ReadAllText(testdatafileloc);
            string inputLine = "";
            StringReader reader = new StringReader(input);
            List<List<string>> data = new List<List<string>>();
            while ((inputLine = reader.ReadLine()) != null)
            {
                if (inputLine.Trim().Length > 0)
                {
                    string[] inputArray = inputLine.Split(new char[] { ',' });
                    data.Add(inputArray.ToList());
                }
            }
            //sort data by every column
            for (int sortCol = data[0].Count() - 1; sortCol >= 0; sortCol--)
            {
                data.OrderBy(x => x[sortCol]);
            }
            //update date
            List<DateTime> alltimes = new List<DateTime>();
            for (int count = 0; count < data.Count; count++)
            {
                //DateTime old = Convert.ToDateTime(data[count][0]);
                DateTime old = DateTime.ParseExact(data[count][0], "dd/MM/yyyy", null);
                alltimes.Add(old);
            }
            alltimes.Sort();
            var _diff = (DateTime.Now - alltimes[alltimes.Count - 1]).TotalDays;
            double diffDays = _diff;
            for (int rowCount = 0; rowCount < data.Count; rowCount++)
            {
                //DateTime old = Convert.ToDateTime(data[rowCount][0]);
                DateTime old = DateTime.ParseExact(data[rowCount][0], "dd/MM/yyyy", null);
                DateTime latest = old.AddDays(diffDays - 1);
                string currentdate = latest.ToString("dd/MM/yyyy");
                data[rowCount][0] = currentdate.Replace("-", "/");
            }
            string output = string.Join("\r\n", data.AsEnumerable()
                .Select(x => string.Join(",", x.Select(y => y).ToArray())).ToArray());
            File.WriteAllText(testdatafileloc, output);
            File.Copy(testdatafileloc, fileLocation, true);
        }

        public string getPrefailUpcomingRepaymentFirstPage(string value)
        {
            _act.waitForVisibilityOfElement(By.XPath("(//h3[@class='font-30 text-center'])[last()-" + value + "]"), 30);
            string upcoming = _act.getText(By.XPath("(//h3[@class='font-30 text-center'])[last()-" + value + "]"), "");
            var a = upcoming.Split('$');
            string upcoming1 = a[1];
            return "$" + upcoming1;
        }

        public string GetPrefailUpcomingRepaymentLastPage(string value)
        {
            _act.waitForVisibilityOfElement(By.XPath("(//div[@class='timeline-section-content f-left oauto']//div[@class='timeline-section-content-amount f-right font-20'])[last()-" + value + "]"), 30);
            string upcoming = _act.getText(By.XPath("(//div[@class='timeline-section-content f-left oauto']//div[@class='timeline-section-content-amount f-right font-20'])[last()-" + value + "]"), "");
            return upcoming;
        }

        public string GetPrefailUpcomingRepaymentFirstPageExtend(string value)
        {
            IWebElement ele = FirstExtendRepayment(By.XPath("//div[@id='afterSection']//div[@class='oauto']"));
            string upcomingRepayment = ele.Text;
            var a = upcomingRepayment.Split('$');
            string upcoming1 = a[1];
            return "$" + upcoming1;
        }

        public string GetPrefailUpcomingRepaymentLastPageExtend(string value)
        {
            _act.waitForVisibilityOfElement(By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-" + value + "]"), 30);
            string upcoming = _act.getText(By.XPath("(//div[@class='timeline-section-content-amount f-right font-20'])[last()-" + value + "]"), "");
            return upcoming;
        }

        public void clickFinishBtn()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.finishButton, 120);
            _act.click(_bankdetailsLoc.finishButton, "Finish button");
        }

        public void clickTimeLine()
        {
            _act.waitForVisibilityOfElement(_bankdetailsLoc.Timeline, 120);
            _act.click(_bankdetailsLoc.Timeline, "Time line ");
        }

        public bool verifyRescheduleBtn()
        {
            Thread.Sleep(1000);
            return _act.isElementPresent(_bankdetailsLoc.RescheduleButton);
        }

        public bool verifyDivideRadioBtn()
        {
            Thread.Sleep(1000);
            return _act.isElementPresent(_bankdetailsLoc.DividePayment);
        }

        public bool verifyExtendRadioBtn()
        {
            Thread.Sleep(1000);
            return _act.isElementPresent(_bankdetailsLoc.ExtentPayment);
        }

        public IWebElement FirstExtendRepayment(By locator)
        {
            IWebElement ListBox = _driver.FindElement(locator);
            IReadOnlyCollection<IWebElement> options = ListBox.FindElements(By.XPath("//div[@class='reschedule-section f-left width-33 border-right-thick']/h3"));
            return options.First();        
        }
    }
}
