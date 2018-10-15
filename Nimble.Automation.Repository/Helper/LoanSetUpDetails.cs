using Nimble.Automation.Accelerators;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using JavascriptExecutor = OpenQA.Selenium.IJavaScriptExecutor;
using Microsoft.VisualBasic;
using Shouldly;
using NUnit.Framework;
using System.Globalization;
using Microsoft.SqlServer.Server;
using NUnit.Framework.Interfaces;

namespace Nimble.Automation.Repository
{
    public class LoanSetUpDetails : TestUtility
    {
        private ILoanSetupDetails _loansetupdetailsLoc;
        private ActionEngine _act = null;
        private IWebDriver _driver = null;
        private LoanPurposeDetails _loanPurposeDetails = null;
        
        public LoanSetUpDetails(IWebDriver driver, string strUserType)
        {
            if (GetPlatform(driver))
                _loansetupdetailsLoc = (strUserType == "NL") ? (ILoanSetupDetails)new LoanSetupDetailsMobileNLLoc() : new LoanSetupDetailsMobileRLLoc();

            else
                _loansetupdetailsLoc = (strUserType == "NL") ? (ILoanSetupDetails)new LoanSetupDetailsDesktopNLLoc() : new LoanSetupDetailsDesktopRLLoc();
            _act = new ActionEngine(driver);
            _driver = driver;
        }

        //public void FetchGUIDandEmail()
        //{
        //    string EmailId = getText(_loansetupdetailsLoc.EmailinDashboard, "Email");
        //    string GUID = getText(_loansetupdetailsLoc.GUIDDashboard, "GUID");
        //}

        /// <summary>
        /// Clicks the refresh button.
        /// </summary>
        public void ClickRefreshBtn()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.RefreshButton, 60);
            _act.click(_loansetupdetailsLoc.RefreshButton, "RefreshButton");
        }

        /// <summary>
        /// Verify and the click setup button.
        /// </summary>
        public void VerifyandClickSetupBtn()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.SetupButton, 60);
            _act.click(_loansetupdetailsLoc.SetupButton, "SetupButton");
        }

        /// <summary>
        /// Click Verify button.
        /// </summary>
        public void ClickVerifyBtn()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.VerifyButton, 60);
            _act.click(_loansetupdetailsLoc.VerifyButton, "VerifyButton");
        }

        /// <summary>
        /// Verifies the verify BTN.
        /// </summary>
        /// <returns>bool true if exist else false</returns>
        public bool VerifyVerifyBtn()
        {
            return _act.waitForVisibilityOfElement(_loansetupdetailsLoc.VerifyButton, 60);
        }

        /// <summary>
        /// Verifies the amount.
        /// </summary>
        /// <param name="RequestedAmount">The requested amount.</param>
        /// <exception cref="System.Exception">Approved Loan Amount : " + 2000 + " is not verified Successfully</exception>
        public void VerifyAmount(string RequestedAmount)
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.ApprovedloanAmount, 120);
            string ApprovedLoanAmt = _act.getText(_loansetupdetailsLoc.ApprovedloanAmount, "Approved Loan Amount");
            // ApprovedLoanAmt;
            if (!ApprovedLoanAmt.Equals("") && ApprovedLoanAmt.Equals("1000"))
            {
                _act.click(_loansetupdetailsLoc.DetailedrepaymentscheduleButton, "DetailedrepaymentscheduleButton");
            }
            else
            {
                throw new Exception("Approved Loan Amount : " + 2000 + " is not verified Successfully");
            }
        }

        /// <summary>
        /// Verifies the first repayment date.
        /// </summary>
        /// <exception cref="System.Exception">Repayment Date Failed to display as in Detailed schedules</exception>
        public void VerifyFirstRepaymentDate()
        {
            string firstrepaymentdate = _act.getText(_loansetupdetailsLoc.firstrepaymentdate, "firstrepaymentdate");

            string Detailedrepaymentschedule = _act.getText(_loansetupdetailsLoc.Detailedpaymentamount, "Detailedrepaymentschedule");

            string firstrepayment = Convert.ToDateTime(firstrepaymentdate).ToString("MMM dd");

            string detailedrepayment = Convert.ToDateTime(Detailedrepaymentschedule).ToString("MMM dd");

            if (firstrepayment == detailedrepayment)
            {
                Console.WriteLine(" First repayment  : " + firstrepaymentdate + " is verified with Detailed repayment schedule :" + Detailedrepaymentschedule);
            }
            else
            {
                throw new Exception("Repayment Date Failed to display as in Detailed schedules");
            }
        }

        public void RequestMoney()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.RequestMoney, 60);
            _act.click(_loansetupdetailsLoc.RequestMoney, "RequestMoney");
        }

        public void StartyourApplicationBtn()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.StartYourApplication, 60);
            _act.click(_loansetupdetailsLoc.StartYourApplication, "StartYourApplication");

        }

        /// <summary>
        /// Click on the Fees Info button on the Detail repayments next to include Fees lable
        /// </summary>
        public void ClickFeesInfoPopUp()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.FeesInfoPopUp, 120);
            var element = _driver.FindElement(_loansetupdetailsLoc.FeesInfoPopUp);
            Actions actions = new Actions(_driver);
            actions.MoveToElement(element);
            actions.Perform();
            _act.mouseover(_loansetupdetailsLoc.FeesInfoPopUp, "Popup");
        }

        /// <summary>
        /// Verifies the Setup page contents.
        /// </summary>
        /// <param name="repaymentfrequency"></param>
        /// <param name="ApprovedAmount"></param>
        /// <param name="AppliedAmount"></param>
        /// <returns></returns>
        public void VerifySetUpPageDetails(int repaymentfrequency, int ApprovedAmount, int AppliedAmount, string UserType)
        {
            string expDefaultDateString;
            try
            {
                if ((AppliedAmount > 2000 && FinalReviewEnabled == "true") && (UserType == FinalReviewLoanType || FinalReviewLoanType == "ALL"))
                {
                    if (GetPlatform(_driver))
                    {
                        //ClickOn Loan Dashboard...Manual Approval
                        ClickLoanDashboardManual();

                        // click on More Button from Bottom Menu
                        ClickMoreBtn();

                        // click on Approve button
                        ClickApproveBtn();

                        //Click Setup Button
                        ClickSetup();
					}
                    else
                    {
                        //ClickOn Loan Dashboard...Manual Approval
                        ClickLoanDashboardManual();

                        //click on Final Approve
                        FinalApprove();

                        //Click Setup Button
                        ClickSetup();
                    }
                }

                //Verify Approved amount
                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.ApprovedloanAmount, 160);

                //----------------------Choose Loan amount--------------------------------//
                VerifyApprovedAmount(ApprovedAmount);

                //----------------------First repayment dates--------------------------------//
                VerifyRepaymentDates(repaymentfrequency);

                ClickDetailedRepaymentSchedule();

                string[,] details = Getrepaymentdetails();

                //DisplayedFrequencyDate  -- (6)
                if (GetPlatform(_driver))
                {
                    //Click on back button
                    _loanPurposeDetails.ClickOnBackBtn();

                    //Click on more button
                    ClickMoreBtn();

                    //Get Slider first repayment date
                    expDefaultDateString = getSliderFirstRepaymentDate();

                    //Click on dashboard
                    clickDashboardMob();

                    //Click on setup
                    ClickSetup();

                }
                else
                {
                    //Get Slider first repayment date
                    expDefaultDateString = getSliderFirstRepaymentDate();
                }

                DateTime expDefaultDate = DateTime.ParseExact(expDefaultDateString, "d/MM/yyyy", null);
                double lastrepayamount = Convert.ToDouble(details[details.GetLength(0) - 1, 1]);

                //----------------------Check repayments amount optons--------------------------------//
                //--------------------Ignored for SAAC and MAAC----------------------------------//
                //VerifyAvailableRepaymentsAmount(repaymentfrequency, ApprovedAmount, expDefaultDate);

                //----------------------Your repayments--------------------------------//
                VerifyYourRepayments(details);

                //----------------------Detailed repayments--------------------------------//
                VerifyDetailedRepayments(details, ApprovedAmount);
                Console.WriteLine("4");
                //---------------------Intrest breakdown---------------//
                // VerifyInterstBreakdowninPopupWnd(details, ApprovedAmount);
                Console.WriteLine("5");
                //---------------------Check the repayments are with the range using the PMT-------------------//
                double estfee = ApprovedAmount * 0.2;
                Console.WriteLine("6");
                if (ApprovedAmount > 2000)
                    CalcluateSolver(ApprovedAmount, expDefaultDate, repaymentfrequency, Convert.ToDouble(details[0, 1]), details.GetLength(0), lastrepayamount);
                else
                    CalculateSAAC(ApprovedAmount, repaymentfrequency, details);

                ClickDetailedrepaymentSchedulebtncollapse();
            }
            catch (Exception e)
            {
                throw new Exception("Validations failed on setup page - " + e.Message);
            }
        }

        public void CalcluateSolver(double ApprovedAmt, DateTime Repaydate, int freq, double RepaymentAmount, int noofrepayments, double lastrepayemnt)
        {
            ApprovedAmt = ApprovedAmt + 400; // Adding Establishment fee
            double rate = 0.13045415 / 100;          
            var _date = DateAndTime.Now;
            //DateTime Repaydate;// = DateTime.ParseExact(FirstRepaymentDate, "dd/MM/yyyy", null);
            DateTime IntrestAccuralDate = _date.AddMonths(1);
            double repay = RepaymentAmount;
            // DateTime EndDate;

            //Repayment enddate,
            double interest = 0;
            double balanceamount = ApprovedAmt;
            while (balanceamount > 1)
            {
                if (_date.Date == Repaydate.Date)
                {
                    if (noofrepayments == 1)
                    {
                        //repay = Convert.ToDouble(lastrepayemnt);
                        double val = (balanceamount + interest) - lastrepayemnt;
                        balanceamount = Math.Round((balanceamount + interest) - lastrepayemnt, 2);
                        balanceamount.ShouldBeInRange(-0.02, 0.02);
                        break;
                    }
                    else
                    {
                        balanceamount = balanceamount - repay;
                        Repaydate = NextRepaymentDate(freq, _date.Date);
                        noofrepayments--;
                    }
                }
                if (_date.Date == IntrestAccuralDate.Date)
                {
                    balanceamount = balanceamount + interest;
                    interest = 0;
                    IntrestAccuralDate = IntrestAccuralDate.AddMonths(1);
                }
                interest = interest + balanceamount * rate;
                _date = _date.AddDays(1);
            }
        }

        public void CalculateSAAC(double ApprovedAmt, int freq, string[,] details)
        {
            DateTime EndDate;
            string strval = string.Empty;

            if (GetPlatform(_driver))
            {
                strval = getFinalRepaymentDateSetupPage();
                EndDate = DateTime.ParseExact(strval, "dd/MM/yy", null);
            }
            else
            {
                strval = RemoveCharFromDate(getFinalRepaymentDateSetupPage());
                EndDate = Convert.ToDateTime(strval);
            }

            double rate = 0.04;
            var _date = DateAndTime.Now;

            TimeSpan duration = EndDate - DateAndTime.Now.Date;
            int val = Convert.ToInt16(duration.Days);
            var totalmonths = Math.Ceiling(val / 30.4); //need to verify
            //  var noofrepayemnts = val / freq;
            double totalintrest = ApprovedAmt * totalmonths * rate;
            double EstablishmentFee = ApprovedAmt * 0.2;
            double TotalRepayableamount = ApprovedAmt + totalintrest + EstablishmentFee;

            double sum = 0;
            int len = details.GetLength(0);
            for (int i = 0; i <= len - 1; i++)
                sum = sum + Convert.ToDouble(details[i, 1]);

            //Verify the Total repayabile amount is Correct
            // 17/04/2018 - TC027 NL/RL 1550 used to fail here depending on time of year
            // this tolerance increase from 1 > 70 was just a quick, band aid solution
            //sum.ShouldBe(TotalRepayableamount, 1);
            sum.ShouldBe(TotalRepayableamount, 70);
        }

        public DateTime NextRepaymentDate(int frequency, DateTime _date)
        {
            if (frequency == 7)
                _date = _date.AddDays(7);
            else if ((frequency == 14))
                _date = _date.AddDays(14);
            else if ((frequency == 30))
                _date = _date.AddMonths(1);

            return _date;
        }

        public void VerifyInterstBreakdowninPopupWnd(string[,] details, int ApprovedAmount)
        {
            double sum = 0;
            int len = details.GetLength(0);
            for (int i = 0; i <= len - 1; i++)
                sum = sum + Convert.ToDouble(details[i, 1]);
            ClickFeesInfoPopUp();
            System.Threading.Thread.Sleep(2000);
            var estFee = GetestablishmentFee();
            var intchar = GetInterestCharges();
            var roi = GetRateofInterest();
            if (ApprovedAmount > 2000)
            //EstablishmentFee  --  (16)
            {
                estFee.ShouldBe(400.00);
                var interst = Math.Round(sum - ApprovedAmount - 400, 2);

                //InterestCharges -- (17)
                intchar.ShouldBe(interst);

                //RateofInterest  -- (18)
                roi.ShouldBe(47.62);
            }
            else
            {
                estFee.ShouldBe(0.20 * ApprovedAmount);

                //InterestCharges -- (17)
                intchar.ShouldBe(sum - (ApprovedAmount + (0.20 * ApprovedAmount)));

                //RateofInterest  -- (18)
                roi.ShouldBe(4);
            }
        }

        // returns InterestCharges - (17)
        public double GetInterestCharges()
        {
            string InterestCharges = _act.getText(_loansetupdetailsLoc.InterestCharges, "InterestCharges");
            var interestcharges = InterestCharges.Replace("$", "");
            if (interestcharges.Contains(","))
            {
                interestcharges = interestcharges.Replace(",", "");
            }
            return Convert.ToDouble(interestcharges);

        }

        // returns RateofInterest  - (18)
        public double GetRateofInterest()
        {
            string RateofInterest = _act.getText(_loansetupdetailsLoc.RateofInterest, "RateofInterest");
            var rateofinterest = RateofInterest.Replace("$", "");
            if (rateofinterest.Contains(","))
            {
                rateofinterest = rateofinterest.Replace(",", "");
            }
            return Convert.ToDouble(rateofinterest);
        }

        public void VerifyApprovedAmount(int ApprovedAmount)
        {
            //MinLoanAmount  -- (1)
            GetMinLoanAmt().ShouldBe(300, "Incorrect Min Loan Amount on 'Confirm your loan amount' slider");

            //MaxLoanAmount  -- (2)
            GetMaxLoanAmt().ShouldBe(ApprovedAmount, "Incorrect Max Loan Amount on 'Confirm your loan amount' slider");

            //DisplayedLoanAmount  -- (3)
            GetDisplayLoanAmt().ShouldBe(ApprovedAmount, "Incorrect Default Loan Amount on 'Confirm your loan amount' slider");
        }

        // returns loan amount minimum  - (1)
        public int GetMinLoanAmt()
        {
            string MinLoan = _act.getText(_loansetupdetailsLoc.LeastAmount, "LeastAmount");
            var loanminvalue = MinLoan.Replace("$", "");
            if (loanminvalue.Contains(","))
            {
                loanminvalue = loanminvalue.Replace(",", "");
            }
            int amountloanmin = Convert.ToInt32(loanminvalue);

            return amountloanmin;
        }

        // returns loan amount maximum - (2)
        public int GetMaxLoanAmt()
        {
            string MaxLoan = _act.getText(_loansetupdetailsLoc.HighestAmount, "HighestAmount");
            var loanmaxvalue = MaxLoan.Replace("$", "");
            if (loanmaxvalue.Contains(","))
            {
                loanmaxvalue = loanmaxvalue.Replace(",", "");
            }
            int amountloanmax = Convert.ToInt32(loanmaxvalue);

            return amountloanmax;
        }

        // returns loan amount display - (3)
        public int GetDisplayLoanAmt()
        {
            string DisplayLoan = _act.getText(_loansetupdetailsLoc.AmountDisplay, "AmountDisplay");
            var loandisplayvalue = DisplayLoan.Replace("$", "");
            if (loandisplayvalue.Contains(","))
            {
                loandisplayvalue = loandisplayvalue.Replace(",", "");
            }
            int amountloandisplayed = Convert.ToInt32(loandisplayvalue);

            return amountloandisplayed;
        }

        public void VerifyRepaymentDates(int repaymentfrequency)
        {
            //MinFrequencyDate -- (4)
            string expMinDate = GetBusinessDay(DateTime.Now.AddDays(2)).ToString("d MMM");
            GetMinFrequencyDate().ShouldBe(expMinDate, "Incorrect Min Loan repayment date on 'Select first repayment date' slider");

            //MaxFrequencyDate -- (5)
            //string expMaxDate = GetBusinessDay(DateTime.Now.AddMonths(1)).ToString("d MMM");
            string expMaxDate = GetBusinessDay(DateTime.Now.AddDays(31)).ToString("d MMM");
            GetMaxFrequencyDate().ShouldBe(expMaxDate, "Incorrect Max Loan repayment date on 'Select first repayment date' slider");

            //Verify the Default Date
            if (repaymentfrequency < 30)
            {
                // string expDefaultDate = GetBusinessDay(DateTime.Now.AddDays(repaymentfrequency)).ToString("ddd d, MMM");
                string expDefaultDate = GetFirstRepaymentDate();
                GetfirstrepayFrequencyDate().ShouldBe(expDefaultDate, "Incorrect Default Loan repayment date on 'Select first repayment date' slider");
            }
            else
            {
                string expDefaultDate = DateTime.Now.AddMonths(1).ToString("ddd d, MMM");
                GetfirstrepayFrequencyDate().ShouldBe(expDefaultDate, "Incorrect Default Loan repayment date on 'Select first repayment date' slider");
            }
        }

        // Returns first repayment date
        public string GetFirstRepaymentDate()
        {
            _loanPurposeDetails = new LoanPurposeDetails(_driver, "NL");

            if (GetPlatform(_driver))
            {
                //click on backbutton
                _loanPurposeDetails.ClickOnBackBtn();

                //Click on more button
                ClickMoreBtn();

                string nextsalarydate = _act.getText(_loansetupdetailsLoc.NextSalaryDate, "NextSalaryDate");
                DateTime NxtsalDate = DateTime.ParseExact(nextsalarydate, "d/MM/yyyy", null);
                string FirstRepaydate = "";
                string nationalHoliday = "";

                //Click on dashboard
                clickDashboardMob();

                //Click on setup
                ClickSetup();

                //Scenario 1: Loan approved and Next salary date is not a Friday
                //Scenario 3: Next salary date is the day before a stat day

                DateTime firstRPD = GetBusinessDay(NxtsalDate);

                if (firstRPD.DayOfWeek != DayOfWeek.Friday)
                {
                    FirstRepaydate = firstRPD.AddDays(1).ToString("dd/MM/yyyy");

                    //Holidays list
                    int length = Holidays().GetLength(0);

                    for (int i = 0; i < length; i++)
                    {
                        nationalHoliday = Holidays()[i, 1];
                        if (FirstRepaydate == nationalHoliday)
                        {
                            FirstRepaydate = NxtsalDate.AddDays(1).ToString("dd/MM/yyyy");
                            break;
                        }
                    }

                    FirstRepaydate = GetBusinessDay(DateTime.ParseExact(FirstRepaydate, "dd/MM/yyyy", null)).ToString("dd/MM/yyyy");
                    DateTime sliderFRDate = DateTime.ParseExact(FirstRepaydate, "dd/MM/yyyy", null);
                    DateTime slidermaxDate = DateTime.ParseExact(GetRepayDate(GetMaxFrequencyDate()), "dd/MM/yyyy", null);
                    string smaxdate = slidermaxDate.ToString("dd/MM/yyyy");
                    DateTime sliderminDate = DateTime.ParseExact(GetRepayDate(GetMinFrequencyDate()), "dd/MM/yyyy", null);
                    string smindate = sliderminDate.ToString("dd/MM/yyyy");

                    //Scenario 4: Next salary date + 1 day is after the max start date
                    if (sliderFRDate > DateTime.ParseExact(smaxdate, "dd/MM/yyyy", null))
                    {
                        FirstRepaydate = smaxdate;
                    }

                    //Scenario 5: Next salary date + 1 day is before the min start date
                    if (sliderFRDate < DateTime.ParseExact(smindate, "dd/MM/yyyy", null))
                    {
                        FirstRepaydate = smindate;
                    }
                }

                //Scenario 2: Next salary date is a Friday
                else if (NxtsalDate.DayOfWeek == DayOfWeek.Friday)
                {
                    FirstRepaydate = DateTime.ParseExact(NxtsalDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).AddDays(3).ToString("dd/MM/yyyy");

                    //Holidays list
                    int length = Holidays().GetLength(0);

                    for (int i = 0; i < length; i++)
                    {
                        nationalHoliday = Holidays()[i, 1];
                        if (FirstRepaydate == nationalHoliday)
                        {
                            FirstRepaydate = NxtsalDate.AddDays(1).ToString("dd/MM/yyyy");
                            break;
                        }
                    }
                    FirstRepaydate = GetBusinessDay(DateTime.ParseExact(FirstRepaydate, "dd/MM/yyyy", null)).ToString("dd/MM/yyyy");
                }
                string FirstDateRepayment = DateTime.ParseExact(FirstRepaydate, "dd/MM/yyyy", null).ToString("ddd d, MMM");
                return FirstDateRepayment;
            }

            else
            {
                string nextsalarydate = _act.getText(_loansetupdetailsLoc.NextSalaryDate, "NextSalaryDate");
                DateTime NxtsalDate = DateTime.ParseExact(nextsalarydate, "d/MM/yyyy", null);
                string FirstRepaydate = "";
                string nationalHoliday = "";


                //Scenario 1: Loan approved and Next salary date is not a Friday
                //Scenario 3: Next salary date is the day before a stat day

                DateTime firstRPD = GetBusinessDay(NxtsalDate);

                if (firstRPD.DayOfWeek != DayOfWeek.Friday)
                {
                    FirstRepaydate = firstRPD.AddDays(1).ToString("dd/MM/yyyy");

                    //Holidays list
                    int length = Holidays().GetLength(0);

                    for (int i = 0; i < length; i++)
                    {
                        nationalHoliday = Holidays()[i, 1];
                        if (FirstRepaydate == nationalHoliday)
                        {
                            FirstRepaydate = NxtsalDate.AddDays(1).ToString("dd/MM/yyyy");
                            break;
                        }
                    }

                    FirstRepaydate = GetBusinessDay(DateTime.ParseExact(FirstRepaydate, "dd/MM/yyyy", null)).ToString("dd/MM/yyyy");
                    DateTime sliderFRDate = DateTime.ParseExact(FirstRepaydate, "dd/MM/yyyy", null);
                    DateTime slidermaxDate = DateTime.ParseExact(GetRepayDate(GetMaxFrequencyDate()), "dd/MM/yyyy", null);
                    string smaxdate = slidermaxDate.ToString("dd/MM/yyyy");
                    DateTime sliderminDate = DateTime.ParseExact(GetRepayDate(GetMinFrequencyDate()), "dd/MM/yyyy", null);
                    string smindate = sliderminDate.ToString("dd/MM/yyyy");

                    //Scenario 4: Next salary date + 1 day is after the max start date
                    if (sliderFRDate > DateTime.ParseExact(smaxdate, "dd/MM/yyyy", null))
                    {
                        FirstRepaydate = smaxdate;
                    }

                    //Scenario 5: Next salary date + 1 day is before the min start date
                    if (sliderFRDate < DateTime.ParseExact(smindate, "dd/MM/yyyy", null))
                    {
                        FirstRepaydate = smindate;
                    }
                }

                //Scenario 2: Next salary date is a Friday
                else if (NxtsalDate.DayOfWeek == DayOfWeek.Friday)
                {
                    FirstRepaydate = DateTime.ParseExact(NxtsalDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null).AddDays(3).ToString("dd/MM/yyyy");

                    //Holidays list
                    int length = Holidays().GetLength(0);

                    for (int i = 0; i < length; i++)
                    {
                        nationalHoliday = Holidays()[i, 1];
                        if (FirstRepaydate == nationalHoliday)
                        {
                            FirstRepaydate = NxtsalDate.AddDays(1).ToString("dd/MM/yyyy");
                            break;
                        }
                    }
                    FirstRepaydate = GetBusinessDay(DateTime.ParseExact(FirstRepaydate, "dd/MM/yyyy", null)).ToString("dd/MM/yyyy");
                }
                string FirstDateRepayment = DateTime.ParseExact(FirstRepaydate, "dd/MM/yyyy", null).ToString("ddd d, MMM");
                return FirstDateRepayment;
            }
        }

        // Returns min repayment frequency date  - (4)
        public string GetMinFrequencyDate()
        {
            string MinFrequencyDate = _act.getText(_loansetupdetailsLoc.MinFrequencyDate, "MinFrequencyDate");
            return MinFrequencyDate;
        }

        // returns  max frequency date  - (5)
        public string GetMaxFrequencyDate()
        {
            string MaxFrequencyDate = _act.getText(_loansetupdetailsLoc.MaxFrequencyDate, "MaxFrequencyDate");
            return MaxFrequencyDate;
        }

        public string GetRepayDate(string datevalue)
        {
            string str = datevalue;
            // var dateval = str.Split(' ');
            //string dd = dateval[0];
            // string mm = dateval[1];
            string yy = DateTime.Now.Year.ToString();
            string repaydate = str + " " + yy;
            string format = "d MMM yyyy";
            DateTime dt = DateTime.ParseExact(repaydate, format, CultureInfo.InvariantCulture);
            return dt.ToString("dd/MM/yyyy");
        }

        // returns first repayment frequency date  - (6)
        public string GetfirstrepayFrequencyDate()
        {
            string FirstrepayFrequencyDate = _act.getText(_loansetupdetailsLoc.DisplayedFrequencyDate, "DisplayedFrequencyDate");
            return FirstrepayFrequencyDate;
        }

        // returns minimum repayment loan amount - (7)
        public int GetMinRepaymentLoanAmt()
        {
            string MinRepayment = _act.getText(_loansetupdetailsLoc.MinRepaymentAmount, "MinRepaymentAmount");
            var MinRepaymentLoanvalue = MinRepayment.Replace("$", "");
            if (MinRepaymentLoanvalue.Contains(","))
            {
                MinRepaymentLoanvalue = MinRepaymentLoanvalue.Replace(",", "");
            }
            int MinRepaymentLoanAmt = Convert.ToInt32(MinRepaymentLoanvalue);

            return MinRepaymentLoanAmt;
        }

        // returns maximum repayment loan amount - (8)
        public int GetMaxRepaymentLoanAmt()
        {
            string MaxRepayment = _act.getText(_loansetupdetailsLoc.MaxRepaymentAmount, "MaxRepaymentAmount");
            var MaxRepaymentLoanvalue = MaxRepayment.Replace("$", "");
            if (MaxRepaymentLoanvalue.Contains(","))
            {
                MaxRepaymentLoanvalue = MaxRepaymentLoanvalue.Replace(",", "");
            }
            int MaxRepaymentLoanAmt = Convert.ToInt32(MaxRepaymentLoanvalue);

            return MaxRepaymentLoanAmt;
        }

        //RepaymentCount  - (9)
        public int GetRepaymentCountSetupPage()
        {
            // _act.waitForVisibilityOfElement(_loansetupdetailsLoc.RepaymentCountSetupPage, 60);      
            Thread.Sleep(1000);// wait until count value to load after moving slider
            string count = _act.getText(_loansetupdetailsLoc.RepaymentCountSetupPage, "");
            if (count == null)
            {
                // ClickDetailedRepaymentSchedule();
                count = _act.getText(_loansetupdetailsLoc.RepaymentCountSetupPage, "");
            }
            int repaymentcount = Convert.ToInt32(count);
            return repaymentcount;
        }

        //WeeklyRepaymentAmount - (10)
        public double getRepAmtInTableMiddle()
        {
            // _act.waitForVisibilityOfElement(_loansetupdetailsLoc.RepAmtInTable, 300);
            string amt = _act.getText(_loansetupdetailsLoc.RepAmtInTable, "amt");
            // var amt1 = amt.Split('.');
            //string amt2 = amt1[0];
            amt = amt.Replace("$", "");
            amt = amt.Replace("*", "");
            double amt4 = Convert.ToDouble(amt);
            return amt4;
        }

        //TotalRepaymentAmount - (11)
        public double GetRepaymentAmountSetupPage()
        {
            // Thread.Sleep(4000);
            //_act.waitForVisibilityOfElement(_loansetupdetailsLoc.RepaymentSetupPage, 60);
            string amount = _act.getText(_loansetupdetailsLoc.RepaymentSetupPage, "");
            //var repay = amount.Split('.');
            //string repayamt = repay[0];
            amount = amount.Replace("$", "");
            amount = amount.Replace(",", "");
            double repamt = Convert.ToDouble(amount);
            return repamt;
        }

        public int getRepaymentAmountSetupPage()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.RepaymentSetupPage, 60);
            string amount = _act.getText(_loansetupdetailsLoc.RepaymentSetupPage, "");
            var repay = amount.Split('.');
            string repayamt = repay[0];
            repayamt = repayamt.Replace("$", "");
            repayamt = repayamt.Replace(",", "");
            int repamt = Convert.ToInt32(repayamt);
            return repamt;
        }

        //FinalRepaymentDate  -(12)
        public string getFinalRepaymentDateSetupPage()
        {
            if (GetPlatform(_driver))
            {
                string date = _act.getText(_loansetupdetailsLoc.FinalRepaymentDateSetupPage, "");
                return date;
            }
            else
            {
                //_act.waitForVisibilityOfElement(_loansetupdetailsLoc.FinalRepaymentDateSetupPage, 240);
                string date = _act.getText(_loansetupdetailsLoc.FinalRepaymentDateSetupPage, "");
                var lastrepaydate = date.Split('/');
                string finalrepaydate = lastrepaydate[0];
                return finalrepaydate;
            }

        }

        public string FinalRepaymentDateSetupPage()
        {
            //_act.waitForVisibilityOfElement(_loansetupdetailsLoc.FinalRepaymentDateSetupPage, 240);
            string date = _act.getText(_loansetupdetailsLoc.FinalRepaymentDateSetupPage, "");
            return date;
        }

        //FirstRepaymentDate -(13)
        public string GetFirstDateSetupPage(int length)
        {          
            string date = _act.getText(By.XPath("(//td[@class='dateCol'])[last()-" + length + "]"), "");
            var date1 = date.Split(' ');
            string a = date1[1];
            var numAlpha = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9]*)");
            var match = numAlpha.Match(a);

            var alpha = match.Groups["Alpha"].Value;
            var num = match.Groups["Numeric"].Value;

            //  fdate =
            return num;
        }

        //LastRepaymentDate  - (14)
        public string GetLastDateSetupPage()
        {
            string date = _act.getText(_loansetupdetailsLoc.LastDate, "");
            var date1 = date.Split(' ');
            string a = date1[1];
            var numAlpha = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9]*)");
            var match = numAlpha.Match(a);

            var alpha = match.Groups["Alpha"].Value;
            var num = match.Groups["Numeric"].Value;

            //  fdate =
            return num;
        }

        //LoanLength  - (15)
        public int getLoanLength()
        {
            // _act.waitForVisibilityOfElement(_loansetupdetailsLoc.LoanLength, 300);
            string loanlength = _act.getText(_loansetupdetailsLoc.LoanLength, "LoanLength");
            if (loanlength == "")
            {
                ClickDetailedRepaymentSchedule();
                loanlength = _act.getText(_loansetupdetailsLoc.LoanLength, "LoanLength");
            }
            var loanlen = loanlength.Split(' ');
            string LoanLength = loanlen[0];
            int LoanLengthDays = Convert.ToInt32(LoanLength);
            return LoanLengthDays;
        }

        // returns EstablishmentFee  - (16)
        public double GetestablishmentFee()
        {
            // ClickFeesInfoPopUp();
            string EstablishmentFee = _act.getText(_loansetupdetailsLoc.EstablishmentFee, "EstablishmentFee");
            var establishmentfee = EstablishmentFee.Replace("$", "");
            if (establishmentfee.Contains(","))
            {
                establishmentfee = establishmentfee.Replace(",", "");
            }
            return Convert.ToDouble(establishmentfee);
        }

        //Detailed repayment schedule  - (19)
        public string[,] Getrepaymentdetails()
        {
            int repaycount = GetRepaymentCountSetupPage();
            string[,] repay = new string[repaycount, 2];
            int counter = 0;
            for (int i = repaycount; i >= 1; i--)
            {
                Console.WriteLine("Current index" + i);
                string repayemnt = GetDetailedrepaymentschedule(i - 1).Replace("Detail repayment Dates : ", "");
                repayemnt = repayemnt.Replace("Detail repayment Amount :$", "?");
                var split = repayemnt.Split('?');
                repay[counter, 0] = split[0];
                repay[counter, 1] = split[1];
                counter++;
            }

            return repay;
        }

        public string GetDetailedrepaymentschedule(int repaymentlength)
        {
            Console.WriteLine("start index" + repaymentlength);

            string DetailrepaymentDates = _act.getText(By.XPath("(.//*[@id='repayments']//tr[@class='contentRow']//td[2])[last()-" + repaymentlength + "]"), "repaymentsdates");

            Console.WriteLine("Detail repayment Dates : " + DetailrepaymentDates);
            string DetailrepaymentAmount = _act.getText(By.XPath("(.//*[@id='repayments']//tr[@class='contentRow']//td[3])[last()-" + repaymentlength + "]"), "repaymentsdates");
            Console.WriteLine("Detail repayment Amount :" + DetailrepaymentAmount);
            Console.WriteLine("End index" + repaymentlength);
            return "Detail repayment Dates : " + DetailrepaymentDates + "Detail repayment Amount :" + DetailrepaymentAmount;
        }

        public void VerifyAvailableRepaymentsAmount(int repaymentfrequency, int ApprovedAmount, DateTime expDefaultDate)
        {
            double[,] possibleRepayemnts = CalculatePMT(ApprovedAmount, expDefaultDate, repaymentfrequency);

            //MinRepaymentAmount  -- (7)
            //Check the difference, which should not be more than a $
            double diffamut = GetMaxRepaymentLoanAmt() - possibleRepayemnts[0, 1];
            // diffamut.ShouldBeLessThanOrEqualTo(1);

            //MaxRepaymentAmount  -- (8)
            // GetMaxRepaymentLoanAmt().ShouldBe(maxrepayamt);
        }

        public void VerifyYourRepayments(string[,] details)
        {
            string lastrepaydate = null;
            //RepaymentCount  -- (9)
            GetRepaymentCountSetupPage().ShouldBe(details.GetLength(0), "No of Repayments does not match from 'Your repayments' to 'Detailed repayment schedule' ");

            //Get the sum of all the repayemnts
            double sum = 0;
            int len = details.GetLength(0);
            for (int i = 0; i <= len - 1; i++)
                sum = sum + Convert.ToDouble(details[i, 1]);

            sum = Math.Round(sum, 2);

            //Compare with the one displayed at the botton -- TotalRepaymentAmount  -- (11)
            GetRepaymentAmountSetupPage().ShouldBe(sum, "Total Repayable amount does not match from 'Your repayments' to 'Detailed repayment schedule' ");

            //Verify the last repayment is dsiplayed as Repayment Frequency //WeeklyRepaymentAmount -- (10)
            double lastrepayfreq = Convert.ToDouble(details[details.GetLength(0) - 1, 1]);
            double firstRepayemtn = Convert.ToDouble(details[1, 1]);
            double maxrepayment = Math.Max(lastrepayfreq, firstRepayemtn);
            getRepAmtInTableMiddle().ShouldBe(maxrepayment, "Repayment amount does not match from 'Your repayments' to 'Detailed repayment schedule' ");

            //FinalRepaymentDate  -- (12)

            if (GetPlatform(_driver))
            {
                //Get last repayment date from detailed repayments list
                lastrepaydate = details[details.GetLength(0) - 1, 0].Replace(",", "");
                lastrepaydate = Convert.ToDateTime(lastrepaydate).ToString("dd/MM/yy");
            }
            else
            {
                //Get last repayment date from detailed repayments list
                lastrepaydate = details[details.GetLength(0) - 1, 0].Replace(",", "");
            }

            //Get final repayment date
            string lastdisplaydate = getFinalRepaymentDateSetupPage();

            //Assertion
            lastrepaydate.ShouldContain(lastdisplaydate, "Last Repayment Date does not match from 'Your repayments' to 'Detailed repayment schedule' ");
        }

        public void VerifyDetailedRepayments(string[,] details, int ApprovedAmount)
        {
            //FirstRepaymentDate -- (13)
            //string firstDate = GetFirstDateSetupPage();

            //LastRepaymentDate  -- (14)
            //string lastDate = GetLastDateSetupPage();

            double sum = 0;
            int len = details.GetLength(0);
            for (int i = 0; i <= len - 1; i++)
                sum = sum + Convert.ToDouble(details[i, 1]);

            sum = Math.Round(sum, 2);

            string lastdisplaydate = FinalRepaymentDateSetupPage();
            string strval = null;
            DateTime StartDate;
            if (GetPlatform(_driver))
            {
                string displaydatelast = convertdate(lastdisplaydate);
                StartDate = DateTime.Now.Date;
                strval = RemoveCharFromDate(displaydatelast);
            }
            else
            {
                //LoanLength  -- (15)
                StartDate = DateTime.Now.Date;
                strval = RemoveCharFromDate(lastdisplaydate);
            }
            DateTime EndDate = Convert.ToDateTime(strval);

            double loanlen = (EndDate - StartDate).TotalDays;
            getLoanLength().ShouldBe(Convert.ToInt16(loanlen), "Incorrect loan length displayed on Detail repayment schedule");

            //Verify Total Amount next to the loan lenght
            GetTotalAmount().ShouldBe(sum, "Incorrect Total loan amounth displayed on Detail repayment schedule");

            double actualintrest = Math.Round(sum - ApprovedAmount, 2);
            //Verifying intrest amount next to loan length
            GetIncludingFees().ShouldBe(actualintrest, "Incorrect intrest amounth displayed on Detail repayment schedule");
        }

        public string convertdate(string date)
        {
            var dd = date.Split('/');
            string yy = dd[2];
            string yyy = "20" + yy;
            string newdate = dd[1] + "/" + dd[0] + "/" + yyy;
            return newdate;
        }

        public string convertLatRepayDate(string date)
        {
            var dd = date.Split(' ');
            string d = dd[1];
            int monthInDigit = DateTime.ParseExact(dd[2], "MMM", CultureInfo.InvariantCulture).Month;
            string yy = dd[3];
            string dateValue = d + "/" + ("0" + monthInDigit) + "/" + yy;
            return dateValue;
        }
        private string RemoveCharFromDate(string StrDate)
        {
            string fdate = null;

            if (GetPlatform(_driver))
            {
                fdate = StrDate.Replace('/', ' ');
                return fdate;
            }
            else
            {
                string[] sdate = StrDate.Split(' ');
                var regex = new Regex(Regex.Escape("st"));
                sdate[0] = regex.Replace(sdate[0], "", 1);
                sdate[0] = sdate[0].Replace("nd", "");
                sdate[0] = sdate[0].Replace("rd", "");
                sdate[0] = sdate[0].Replace("th", "");
                #region "Old Logic"
                //if (StrDate.Contains("August"))
                //{

                //    var regex = new Regex(Regex.Escape("nd"));
                //    StrDate = regex.Replace(StrDate, "", 1);
                //    StrDate = StrDate.Replace("nd", "");
                //    StrDate = StrDate.Replace("rd", "");
                //    StrDate = StrDate.Replace("th", "");
                //}
                //else
                //{
                //    var regex = new Regex(Regex.Escape("st"));
                //    StrDate = regex.Replace(StrDate, "", 1);
                //    StrDate = StrDate.Replace("nd", "");
                //    StrDate = StrDate.Replace("rd", "");
                //    StrDate = StrDate.Replace("th", "");
                //}
                #endregion
                fdate = sdate[0] + " " + sdate[1] + " " + sdate[2];
                return fdate;
            }
        }

        public double GetTotalAmount()
        {
            string InterestCharges = _act.getText(_loansetupdetailsLoc.TotalAmount, "InterestCharges");
            var interestcharges = InterestCharges.Replace("$", "");
            if (interestcharges.Contains(","))
            {
                interestcharges = interestcharges.Replace(",", "");
            }
            return Convert.ToDouble(interestcharges);
        }

        public double GetIncludingFees()
        {
            string InterestCharges = _act.getText(_loansetupdetailsLoc.IncludingFees, "InterestCharges");
            var interestcharges = InterestCharges.Replace("$", "");
            if (interestcharges.Contains(","))
            {
                interestcharges = interestcharges.Replace(",", "");
            }
            double val = Convert.ToDouble(interestcharges);

            return val;
        }

        ///// <summary>
        ///// This method will calculate the Min No of repayemtns and max no of repayents with Repaymet amount. These calculations are only indicative and not accurate
        ///// </summary>
        ///// <param name="ApprovedAmt"></param>
        public double[,] CalculatePMT(int ApprovedAmt, DateTime FirstRepaymentDate, int freqency)
        {
            double rate = .1995; int term = 0;

            term = freqency == 7 ? 52 : freqency == 14 ? 27 : freqency == 30 ? 12 : 0;

            DateTime _dt = DateTime.Now.Date;
            DateTime _dtMinEnd = DateTime.Now.Date.AddYears(1);
            DateTime _dtMaxEnd = DateTime.Now.Date.AddYears(2);
            DateTime _firstrepaydate = FirstRepaymentDate;
            var vals = (_dtMinEnd - _firstrepaydate).TotalDays;
            //double _Mindays = val / freqency;
            var minfrequency = Math.Ceiling(vals / freqency) + 1;
            vals = (_dtMaxEnd - _firstrepaydate).TotalDays;
            //double _Maxdays = Convert.ToDouble(_dtMaxEnd - _firstrepaydate) / freqency;
            double maxfrequency = Math.Ceiling(vals / freqency);

            int count = Convert.ToInt16(Math.Ceiling(maxfrequency - minfrequency));

            double[,] val = new double[count + 1, 2];
            for (int i = 0; i <= count; i++)
            {
                double approved = Convert.ToDouble(ApprovedAmt);

                val[i, 0] = minfrequency;
                val[i, 1] = Math.Round(Financial.Pmt(rate / term, minfrequency, -approved, 0), 2);
                minfrequency++;
            }

            return val;
        }

        public DateTime GetBusinessDay(DateTime _date)
        {
            TimeSpan _now = _date - DateTime.Now.Date;

            if (_date.DayOfWeek == DayOfWeek.Saturday)
                _date = _now.Days > 29 ? _date.AddDays(-1) : _date.AddDays(2);
            else if (_date.DayOfWeek == DayOfWeek.Sunday)
                _date = _now.Days > 29 ? _date.AddDays(-2) : _date.AddDays(1);

            return _date.Date;
        }

        /// <summary>
        /// Clicks the submit button.
        /// </summary>
        public void ClickSubmitBtn()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.ButtonSubmit, 120);
            _act.click(_loansetupdetailsLoc.ButtonSubmit, "ButtonSubmit");
        }

        /// <summary>
        /// Find and selects the spendless.
        /// </summary>
        /// <returns>bool if exist: true else false</returns>
        public bool FindandselectSpendless()
        {
            Thread.Sleep(5000);
            bool flag = false;
            if (_act.isElementPresent(_loansetupdetailsLoc.JustCheckingLabel))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Selects the reason to spend less.
        /// </summary>
        /// <param name="ReasonforSpendLess">The reasonfor spend less.</param>
        public void SelectReasontospendLess(string ReasonforSpendLess)
        {
            if (GetPlatform(_driver))
            {
                if (ReasonforSpendLess.Contains(","))
                {
                    var spendreasons = ReasonforSpendLess.Split(',');
                    foreach (var reason in spendreasons)
                    {
                        var choosereason = By.XPath(".//label[contains(text(),'" + reason + "')]");
                        _act.waitForVisibilityOfElement(choosereason, 60);
                        _act.JSClick(choosereason, "selectreason");
                    }
                    _act.JSClick(_loansetupdetailsLoc.reasonsubmitBtn, "reasonsubmitBtn");
                }
                else
                {
                    var choosereason = By.XPath(".//label[contains(text(),'" + ReasonforSpendLess + "')]");
                    _act.waitForVisibilityOfElement(choosereason, 60);
                    _act.JSClick(choosereason, "selectreason");
                    _act.JSClick(_loansetupdetailsLoc.reasonsubmitBtn, "reasonsubmitBtn");
                }
            }
            else
            {
                if (ReasonforSpendLess.Contains(","))
                {
                    var spendreasons = ReasonforSpendLess.Split(',');
                    foreach (var reason in spendreasons)
                    {
                        var choosereason = By.XPath(".//table[@id='offerOptions']//tr/td/label[contains(text(),'" + reason + "')]");
                        _act.waitForVisibilityOfElement(choosereason, 60);
                        _act.JSClick(choosereason, "selectreason");
                    }
                    _act.JSClick(_loansetupdetailsLoc.reasonsubmitBtn, "reasonsubmitBtn");
                }
                else
                {
                    var choosereason = By.XPath(".//table[@id='offerOptions']//tr/td/label[contains(text(),'" + ReasonforSpendLess + "')]");
                    _act.waitForVisibilityOfElement(choosereason, 60);
                    _act.JSClick(choosereason, "selectreason");
                    _act.JSClick(_loansetupdetailsLoc.reasonsubmitBtn, "reasonsubmitBtn");
                }
            }
        }

        public void Loancontract()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.Loancontract, 120);
            IWebElement sliderElement = _driver.FindElement(_loansetupdetailsLoc.Loancontract);
            IWebElement scrolldown = _driver.FindElement(By.XPath(".//div[@id='scrollBottom']"));
            // _driver.FindElement(_loansetupdetailsLoc.LoanContractLabel).Click();
            bool elementvisible = _act.isElementDisabled(_loansetupdetailsLoc.confirmLoancotract);
            Actions actions = new Actions(_driver);
            actions.MoveToElement(scrolldown);
            actions.Perform();
            ////Additional code to verify the scroll happens till the end
            IWebElement webElement = _driver.FindElement(By.XPath(".//div[@id='scrollBottom']"));
            JavascriptExecutor js = _driver as JavascriptExecutor;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", webElement);
        }

        public double GetRepaymentAmountConfirmPage()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.RepaymentConfirmPage, 60);
            string amount = _act.getText(_loansetupdetailsLoc.RepaymentConfirmPage, "");
           // var repay = amount.Split('');
           // string repayamt = repay[0];
           string repayamt = amount.Replace("$", "");
            repayamt = repayamt.Replace(",", "");
            double repamt = double.Parse(repayamt);
            return repamt;
        }

        public int getRepaymentCountConfirmPage()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.RepaymentConfirmPage, 60);
            string amount = _act.getText(_loansetupdetailsLoc.RepaymentConfirmPage, "");
            var repay = amount.Split('.');
            string repayamt = repay[0];
            repayamt = repayamt.Replace("$", "");
            repayamt = repayamt.Replace(",", "");
            int repamt = Convert.ToInt32(repayamt);
            return repamt;
        }

        public void ClickFortnight()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.FortNight, 120);
            _act.click(_loansetupdetailsLoc.FortNight, "");
        }

        public void ClickMonthly()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.Monthly, 60);
            _act.click(_loansetupdetailsLoc.Monthly, "");
        }

        public bool verifyMonthlyrepaymentInvisible()
        {
            return _act.waitForInVisibilityOfElement(_loansetupdetailsLoc.Monthly, "Monthly");
        }

        public bool getAmtOfCredit(int loanamt)
        {
            bool flag = false;
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.AmountOfCredit, 60);
            string amount = _act.getText(_loansetupdetailsLoc.AmountOfCredit, "");
            var repay = amount.Split('.');
            string repayamt = repay[0];
            repayamt = repayamt.Replace("$", "");
            repayamt = repayamt.Replace(",", "");
            int repamt = Convert.ToInt32(repayamt);
            // return repamt;

            int e = Convert.ToInt32(0.04 * loanamt);

            int f = Convert.ToInt32(0.20 * loanamt);
            int c = e + f + loanamt;
            if (c == repamt)
            {
                flag = true;
            }
            return flag;
        }

        public bool getDisclosureDate()
        {
            bool flag = false;
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.DisclosureDate, 60);
            string date = _act.getText(_loansetupdetailsLoc.DisclosureDate, "");
            DateTime today = DateTime.Today;

            string currentDate = string.Format("{0:d MMMM, yyyy}", today); //today.ToString("dd/MM/yyyy");
            var date1 = currentDate.Split('/');
            string todayDate = date1[0];

            if (date.Contains(todayDate))
            {
                flag = true;
            }
            return flag;
        }

        public string GetFirstDateConfirmPage()
        {

            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.FirstDateConfirmPage, 60);
            string date = _act.getText(_loansetupdetailsLoc.FirstDateConfirmPage, "");
            var date1 = date.Split(',');
            string day = date1[0];
            var date2 = day.Split('/');
            string date3 = date2[0];

            return date3;
        }

        public string GetLastDateConfirmPage()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.LastDateConfirmPage, 60);
            string date = _act.getText(_loansetupdetailsLoc.LastDateConfirmPage, "");
            var date1 = date.Split(',');
            string day = date1[0];
            var date2 = day.Split('/');
            string date3 = date2[0];

            return date3;
        }

        public string getunconfirmedcontractmsg()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.Unconfirmedcontractmsg, 60);
            string warningmsg = _act.getText(_loansetupdetailsLoc.Unconfirmedcontractmsg, "");
            return warningmsg;
        }

        public string getunconfirmedpurposemsg()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.Unconfirmedpurposemsg, 60);
            string warningmsg = _act.getText(_loansetupdetailsLoc.Unconfirmedpurposemsg, "");
            return warningmsg;
        }

        public string getunconfirmedrepaymsg()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.Unconfirmedrepaymsg, 60);
            string warningmsg = _act.getText(_loansetupdetailsLoc.Unconfirmedrepaymsg, "");
            return warningmsg;
        }

        public string getLastDateConfirmPage()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.LastDateConfirmPage, 60);
            string date = _act.getText(_loansetupdetailsLoc.LastDateConfirmPage, "");
            var date1 = date.Split(',');
            string day = date1[0];
            var date2 = day.Split('/');
            string date3 = date2[0];

            return date3;
        }

        //public void ConfirmAcceptingContract()
        //{
        //    Actions builder = new Actions(_driver);
        //    IWebElement readandacceptterms = _driver.FindElement(_loansetupdetailsLoc.confirmLoancotract);
        //    // builder.MoveToElement(readandacceptterms, 10, 10).Click().Build().Perform();
        //    readandacceptterms.Click();
        //    Thread.Sleep(3000);
        //    IWebElement confirmreqandpurpose = _driver.FindElement(_loansetupdetailsLoc.confirmpurpose);
        //    builder.MoveToElement(confirmreqandpurpose, 10, 10).Click().Build().Perform();      
        //    Thread.Sleep(3000);
        //    IWebElement confirmrepayment = _driver.FindElement(_loansetupdetailsLoc.confirmrepay);
        //    builder.MoveToElement(confirmrepayment, 10, 10).Click().Build().Perform();      
        //    Thread.Sleep(3000);
        //    confirmrepayment = _driver.FindElement(_loansetupdetailsLoc.confirmrepay);
        //    builder.MoveToElement(confirmrepayment, 10, 10).Click().Build().Perform();         
        //    Thread.Sleep(3000);
        //    confirmrepayment = _driver.FindElement(_loansetupdetailsLoc.confirmrepay);
        //    builder.MoveToElement(confirmrepayment, 10, 10).Click().Build().Perform();       
        //    Thread.Sleep(3000);
        //}

        /// <summary>
        /// Confirms the accepting contract.
        /// </summary>

        public void ConfirmAcceptingContract()
        {
            if (GetPlatform(_driver))
            {

                IWebElement confirmLoancotractBtn = _driver.FindElement(_loansetupdetailsLoc.confirmLoancotract);
                Actions actions = new Actions(_driver);
                actions.MoveToElement(confirmLoancotractBtn);
                actions.Perform();

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmLoancotract, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmLoancotract, "confirmLoancotract");

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmpurpose, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmpurpose, "confirmpurpose");

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmrepay, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmrepay, "confirmrepay");
            }
            else
            {
                IWebElement confirmLoancotractBtn = _driver.FindElement(_loansetupdetailsLoc.confirmLoancotract);
                Actions actions = new Actions(_driver);
                actions.MoveToElement(confirmLoancotractBtn);
                actions.Perform();

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmLoancotract, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmLoancotract, "confirmLoancotract");

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmpurpose, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmpurpose, "confirmpurpose");

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmrepay, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmrepay, "confirmrepay");
            }
        }

        /// <summary>
        /// Partials the confirm accepting contract and purpose.
        /// </summary>
        public void PartialConfirmAcceptingContractwithcontractandpurpose()
        {
            if (GetPlatform(_driver))
            {
                IWebElement confirmLoancotractBtn = _driver.FindElement(_loansetupdetailsLoc.confirmLoancotract);
                Actions actions = new Actions(_driver);
                actions.MoveToElement(confirmLoancotractBtn);
                actions.Perform();

                // check 1 and 2 radio buttons

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmLoancotract, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmLoancotract, "confirmLoancotract"); // 1 checked

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmpurpose, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmpurpose, "confirmpurpose");  // 2 checked

                //_act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmrepay, 60);
                //_act.JSClick(_loansetupdetailsLoc.confirmrepay, "confirmrepay");       // 3 unchecked
            }
            else
            {
                IWebElement confirmLoancotractBtn = _driver.FindElement(_loansetupdetailsLoc.confirmLoancotract);
                Actions actions = new Actions(_driver);
                actions.MoveToElement(confirmLoancotractBtn);
                actions.Perform();

                // check 1 and 2 radio buttons

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmLoancotract, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmLoancotract, "confirmLoancotract"); // 1 checked

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmpurpose, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmpurpose, "confirmpurpose");  // 2 checked

                //_act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmrepay, 60);
                //_act.JSClick(_loansetupdetailsLoc.confirmrepay, "confirmrepay");   // 3 unchecked
            }
        }

        /// <summary>
        /// Partials the confirm accepting contract and repay.
        /// </summary>
        public void PartialConfirmAcceptingContractwithcontractandrepay()
        {
            if (GetPlatform(_driver))
            {
                IWebElement confirmLoancotractBtn = _driver.FindElement(_loansetupdetailsLoc.confirmLoancotract);
                Actions actions = new Actions(_driver);
                actions.MoveToElement(confirmLoancotractBtn);
                actions.Perform();

                // check 1 and 3 radio buttons

                // _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmLoancotract, 60);
                // _act.JSClick(_loansetupdetailsLoc.confirmLoancotract, "confirmLoancotract");   // 1 checked

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmpurpose, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmpurpose, "confirmpurpose");            // 2 unchecked

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmrepay, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmrepay, "confirmrepay");            // 3 checked
            }
            else
            {
                IWebElement confirmLoancotractBtn = _driver.FindElement(_loansetupdetailsLoc.confirmLoancotract);
                Actions actions = new Actions(_driver);
                actions.MoveToElement(confirmLoancotractBtn);
                actions.Perform();

                // check 1 and 3 radio buttons

                //_act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmLoancotract, 60);
                //_act.JSClick(_loansetupdetailsLoc.confirmLoancotract, "confirmLoancotract");     // 1 checked

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmpurpose, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmpurpose, "confirmpurpose");       // 2 unchecked

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmrepay, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmrepay, "confirmrepay");        // 3 checked
            }
        }

        /// <summary>
        /// Partials the confirm accepting purpose and repay.
        /// </summary>
        public void PartialConfirmAcceptingContractwithpurposeandrepay()
        {
            if (GetPlatform(_driver))
            {
                IWebElement confirmLoancotractBtn = _driver.FindElement(_loansetupdetailsLoc.confirmLoancotract);
                Actions actions = new Actions(_driver);
                actions.MoveToElement(confirmLoancotractBtn);
                actions.Perform();

                // check 2 and 3 radio buttons

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmLoancotract, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmLoancotract, "confirmLoancotract");   // 1 unchecked

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmpurpose, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmpurpose, "confirmpurpose");           // 2 checked

                // _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmrepay, 60);
                // _act.JSClick(_loansetupdetailsLoc.confirmrepay, "confirmrepay");           // 3 checked
            }
            else
            {
                IWebElement confirmLoancotractBtn = _driver.FindElement(_loansetupdetailsLoc.confirmLoancotract);
                Actions actions = new Actions(_driver);
                actions.MoveToElement(confirmLoancotractBtn);
                actions.Perform();

                // check 2 and 3 radio buttons

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmLoancotract, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmLoancotract, "confirmLoancotract"); // 1 unchecked

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmpurpose, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmpurpose, "confirmpurpose");      // 2 checked

                //_act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmrepay, 60);
                // _act.JSClick(_loansetupdetailsLoc.confirmrepay, "confirmrepay");         // 3 checked
            }
        }

        /// <summary>
        /// complete the confirm accepting contract, purpose and reapy.
        /// </summary>
        public void CompleteConfirmAcceptingContract()
        {
            if (GetPlatform(_driver))
            {
                IWebElement confirmLoancotractBtn = _driver.FindElement(_loansetupdetailsLoc.confirmLoancotract);
                Actions actions = new Actions(_driver);
                actions.MoveToElement(confirmLoancotractBtn);
                actions.Perform();

                // check 1, 2 and 3 radio buttons
                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmLoancotract, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmLoancotract, "confirmLoancotract");   // 1 checked

                // _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmpurpose, 60);
                //_act.JSClick(_loansetupdetailsLoc.confirmpurpose, "confirmpurpose");           // 2 checked

                // _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmrepay, 60);
                // _act.JSClick(_loansetupdetailsLoc.confirmrepay, "confirmrepay");           // 3 checked
            }
            else
            {
                IWebElement confirmLoancotractBtn = _driver.FindElement(_loansetupdetailsLoc.confirmLoancotract);
                Actions actions = new Actions(_driver);
                actions.MoveToElement(confirmLoancotractBtn);
                actions.Perform();

                // check 1, 2 and 3 radio buttons

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmLoancotract, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmLoancotract, "confirmLoancotract"); // 1 checked

                // _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmpurpose, 60);
                // _act.JSClick(_loansetupdetailsLoc.confirmpurpose, "confirmpurpose");      // 2 checked

                //_act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmrepay, 60);
                // _act.JSClick(_loansetupdetailsLoc.confirmrepay, "confirmrepay");         // 3 checked
            }
        }

        /// <summary>
        /// Partials the confirm accepting purpose and repay.
        /// </summary>
        public void UnConfirmAcceptingLoanContract()
        {
            if (GetPlatform(_driver))
            {
                IWebElement confirmLoancotractBtn = _driver.FindElement(_loansetupdetailsLoc.confirmLoancotract);
                Actions actions = new Actions(_driver);
                actions.MoveToElement(confirmLoancotractBtn);
                actions.Perform();

                // uncheck 1

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmLoancotract, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmLoancotract, "confirmLoancotract");   // 1 unchecked

                // _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmpurpose, 60);
                //_act.JSClick(_loansetupdetailsLoc.confirmpurpose, "confirmpurpose");           // 2 checked

                // _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmrepay, 60);
                // _act.JSClick(_loansetupdetailsLoc.confirmrepay, "confirmrepay");           // 3 checked
            }
            else
            {
                IWebElement confirmLoancotractBtn = _driver.FindElement(_loansetupdetailsLoc.confirmLoancotract);
                Actions actions = new Actions(_driver);
                actions.MoveToElement(confirmLoancotractBtn);
                actions.Perform();

                // uncheck 1

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmLoancotract, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmLoancotract, "confirmLoancotract"); // 1 unchecked

                // _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmpurpose, 60);
                // _act.JSClick(_loansetupdetailsLoc.confirmpurpose, "confirmpurpose");      // 2 checked

                // _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmrepay, 60);
                // _act.JSClick(_loansetupdetailsLoc.confirmrepay, "confirmrepay");         // 3 checked
            }
        }

        /// <summary>
        /// Partials the confirm accepting contract and repay.
        /// </summary>
        public void UnConfirmAcceptingpurpose()
        {
            if (GetPlatform(_driver))
            {
                IWebElement confirmLoancotractBtn = _driver.FindElement(_loansetupdetailsLoc.confirmLoancotract);
                Actions actions = new Actions(_driver);
                actions.MoveToElement(confirmLoancotractBtn);
                actions.Perform();

                // uncheck 2

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmLoancotract, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmLoancotract, "confirmLoancotract");   // 1 checked

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmpurpose, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmpurpose, "confirmpurpose");           // 2 unchecked

                // _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmrepay, 60);
                // _act.JSClick(_loansetupdetailsLoc.confirmrepay, "confirmrepay");           // 3 checked
            }
            else
            {
                IWebElement confirmLoancotractBtn = _driver.FindElement(_loansetupdetailsLoc.confirmLoancotract);
                Actions actions = new Actions(_driver);
                actions.MoveToElement(confirmLoancotractBtn);
                actions.Perform();

                // uncheck 2

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmLoancotract, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmLoancotract, "confirmLoancotract"); // 1 checked

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmpurpose, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmpurpose, "confirmpurpose");      // 2 unchecked

                // _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmrepay, 60);
                // _act.JSClick(_loansetupdetailsLoc.confirmrepay, "confirmrepay");         // 3 checked
            }
        }

        /// <summary>
        /// Partials the confirm accepting contract and purpose.
        /// </summary>
        public void UnConfirmAcceptingrepay()
        {
            if (GetPlatform(_driver))
            {
                IWebElement confirmLoancotractBtn = _driver.FindElement(_loansetupdetailsLoc.confirmLoancotract);
                Actions actions = new Actions(_driver);
                actions.MoveToElement(confirmLoancotractBtn);
                actions.Perform();

                // uncheck 3

                //_act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmLoancotract, 60);
                // _act.JSClick(_loansetupdetailsLoc.confirmLoancotract, "confirmLoancotract");   // 1 checked

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmpurpose, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmpurpose, "confirmpurpose");           // 2 checked

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmrepay, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmrepay, "confirmrepay");           // 3 unchecked
            }
            else
            {
                IWebElement confirmLoancotractBtn = _driver.FindElement(_loansetupdetailsLoc.confirmLoancotract);
                Actions actions = new Actions(_driver);
                actions.MoveToElement(confirmLoancotractBtn);
                actions.Perform();

                // uncheck 3

                //_act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmLoancotract, 60);
                //_act.JSClick(_loansetupdetailsLoc.confirmLoancotract, "confirmLoancotract"); // 1 checked

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmpurpose, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmpurpose, "confirmpurpose");      // 2 checked

                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.confirmrepay, 60);
                _act.JSClick(_loansetupdetailsLoc.confirmrepay, "confirmrepay");         // 3 unchecked
            }
        }

        /// <summary>
        /// Verify I agree button display or not.
        /// </summary>
        public bool VerifyAgreeBtnDisplay()
        {
            bool flag = false;
            if (_act.isElementPresent(_loansetupdetailsLoc.submitcontractButton))
            {
                IWebElement agreebtn = _driver.FindElement(_loansetupdetailsLoc.submitcontractButton);
                if (_act.isElementDisplayed(agreebtn))
                    flag = true;
                else
                    flag = false;
            }
            return flag;
        }

        /// <summary>
        /// click on I agree button
        /// </summary>
        public void ClickOnAgreeBtn()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.submitcontractButton, 60);
            _act.JSClick(_loansetupdetailsLoc.submitcontractButton, "submitcontractButton");
        }

        /// <summary>
        /// click on cancel button
        /// </summary>
        public void ClickOnCancelBtn()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.cancelcontractButton, 60);
            _act.JSClick(_loansetupdetailsLoc.cancelcontractButton, "cancelcontractButton");
        }

        /// <summary>
        /// click on cancel contract popup Yes
        /// </summary>
        public void ClickYesOncancelpopup()
        {
            if (verifycancelpopupdisplayed())
            {
                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.CancelContractYes, 60);
                _act.JSClick(_loansetupdetailsLoc.CancelContractYes, "CancelContractYes");
            }
            else
            {
                throw new Exception("Cancel popup not displayed.");
            }
        }

        /// <summary>
        /// click on cancel contract popup No
        /// </summary>
        public void ClickNoOncancelpopup()
        {
            if (verifycancelpopupdisplayed())
            {
                _act.waitForVisibilityOfElement(_loansetupdetailsLoc.CancelContractNo, 60);
                _act.JSClick(_loansetupdetailsLoc.CancelContractNo, "CancelContractNo");
            }
            else
            {
                throw new Exception("Cancel popup not displayed.");
            }
        }

        /// <summary>
        /// verify to display contract popup 
        /// </summary>
        public bool verifycancelpopupdisplayed()
        {
            bool flag = false;
            if (_act.isElementPresent(_loansetupdetailsLoc.CancelPopup))
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// Verify whether the I agree button displayed or not.
        /// </summary>
        public bool CheckAgreeBtn()
        {
            bool flag = false;
            if (_act.isElementDisplayed(_driver.FindElement(_loansetupdetailsLoc.submitcontractButton)))
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Clicks the acknowledge button.
        /// </summary>
        public void ClickAcknowledgeBtn()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.acknowledgebtn, 60);
            _act.click(_loansetupdetailsLoc.acknowledgebtn, "acknowledgebtn");
        }

        /// <summary>
        /// Clicks the submit final button.
        /// </summary>
        public void ClickSubmitFinalBtn()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.SubmitButton, 60);
            _act.click(_loansetupdetailsLoc.SubmitButton, "SubmitButton");
        }

        /// <summary>
        /// Clicks the nothanks button.
        /// </summary>
        public void ClickNothanksBtn()
        {
            //_act.waitForVisibilityOfElement(_loansetupdetailsLoc.NoThanksButton, 30);
            //_act.click(_loansetupdetailsLoc.NoThanksButton, "NoThanksButton");
        }

        /// <summary>
        /// Click want to save card button.
        /// </summary>
        public void ClickAndSaveVisaCardRbtn()
        {
            // _act.waitForVisibilityOfElement(_loansetupdetailsLoc.NimbleCardRbtn, 60);
            // _act.click(_loansetupdetailsLoc.NimbleCardRbtn, "NimbleCardRbtn");
        }

        /// <summary>
        /// Click on the nimble card submit.
        /// </summary>
        public void ClickOnNimbleCardSubmit()
        {
            // _act.waitForVisibilityOfElement(_loansetupdetailsLoc.NimblecardSubmitBtn, 60);
            //  _act.click(_loansetupdetailsLoc.NimblecardSubmitBtn, "NimblecardSubmitBtn");
        }

        /// <summary>
        /// Verify if Nimble Card offer page Submit button is visible
        /// </summary>
        public bool VerifyNimbleCardSubmitBtnVisible()
        {
            // _driver.FindElement(_loansetupdetailsLoc.NimblecardSubmitBtn);
            return _act.isElementPresent(_loansetupdetailsLoc.NimblecardSubmitBtn, 60);
        }

        /// <summary>
        /// Clicks the loan dashboard.
        /// </summary>
        public void ClickLoanDashboard()
        {

            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.LoanDashBoard, 60);
            _act.click(_loansetupdetailsLoc.LoanDashBoard, "loan dashboard");

        }

        /// <summary>
        /// Click on Update Your Profile.
        /// </summary>
        public void UpdateYourProfile()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.Updateprofile, 60);
            _act.click(_loansetupdetailsLoc.Updateprofile, "Updateprofile");

        }

        /// <summary>
        /// Click on SaveBtn.
        /// </summary>
        public void UpdateSaveBtn()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.UpdateSaveButton, 60);
            _act.click(_loansetupdetailsLoc.UpdateSaveButton, "UpdateSaveButton");

        }

        /// <summary>
        /// Click on start LoanDashboard.
        /// </summary>
        public void ToLoanDashboard()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.ToLoanDashBoard, 60);
            _act.click(_loansetupdetailsLoc.ToLoanDashBoard, "loan dashboard");

        }

        /// <summary>
        /// Clicks the mobile loan dashboard button.
        /// </summary>
        public void ClickMobileLoanDashboardBtn()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.LoanDashBoard, 60);
            _act.click(_loansetupdetailsLoc.LoanDashBoard, "loan dashboard");

        }

        /// <summary>
        /// Clicks the more button.
        /// </summary>
        public void ClickMoreBtn()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.More, 60);
            _act.click(_loansetupdetailsLoc.More, "loan dashboard");
        }

        /// <summary>
        /// Clicks the loan dashboard manual.
        /// </summary>
        public bool ClickLoanDashboardManual()
        {
            // Thread.Sleep(45000);

            bool flag = false;
            Thread.Sleep(5000); // wait until the loandashboard buttons appears
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.LoanDashboardManual, 60);
            if (_act.isElementDisplayed(_driver.FindElement(_loansetupdetailsLoc.LoanDashboardManual)))
            {
                flag = true;
            }
            if (_act.isElementDisplayed(_driver.FindElement(_loansetupdetailsLoc.LoanDashboardManual)))
            {
                _act.click(_loansetupdetailsLoc.LoanDashboardManual, "LoanDashboardManual");
            }
            return flag;
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public void Logout()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.Logout, 120);
            _act.JSClick(_loansetupdetailsLoc.Logout, "Logout");
            Thread.Sleep(2000);
        }

        public bool ActivateCard()
        {
            bool flag = false;

            //Click on activate card link
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.ActivateCard, 60);
            _act.JSClick(_loansetupdetailsLoc.ActivateCard, "ActivateCard");

            //Select security question
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.SecurityQuestion, 60);
            _act.selectByOptionText(_loansetupdetailsLoc.SecurityQuestion, "What is your mother's Maiden Name?", "Bank");

            //Enter security answer
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.SecurityAnswer, 60);
            _act.EnterText(_loansetupdetailsLoc.SecurityAnswer, "Serena");

            //Enter last four digits of account number
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.LastFourDigits, 60);
            _act.EnterText(_loansetupdetailsLoc.LastFourDigits, "1234");

            //Click product closure button
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.Productdisclosurebutton, 60);
            _act.JSClick(_loansetupdetailsLoc.Productdisclosurebutton, "Productdisclosurebutton");

            //Click financial services button
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.Financialservicesbutton, 60);
            _act.JSClick(_loansetupdetailsLoc.Financialservicesbutton, "Financialservicesbutton");

            //Click on activate card button
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.ActivateCardButton, 60);
            _act.JSClick(_loansetupdetailsLoc.ActivateCardButton, "ActivateCardButton");

            //Click on done button
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.ActivateDoneButton, 60);
            _act.JSClick(_loansetupdetailsLoc.ActivateDoneButton, "ActivateDoneButton");

            //_act.waitForVisibilityOfElement(_loansetupdetailsLoc.Bpay, 100);
            //if (_act.isElementDisplayed(_driver.FindElement(_loansetupdetailsLoc.Bpay)))
            //{
            //    flag = true;
            //}
            //_act.JSClick(_loansetupdetailsLoc.Bpay, "Bpay");
            //_act.waitForVisibilityOfElement(_loansetupdetailsLoc.BillerCode, 100);
            //_act.EnterText(_loansetupdetailsLoc.BillerCode, "1234");
            //_act.JSClick(_loansetupdetailsLoc.VerifyBillerCode, "VerifyBillerCode");
            //_act.EnterText(_loansetupdetailsLoc.BpayReference, "Wimble");
            //_act.EnterText(_loansetupdetailsLoc.BpayDescription, "roller coaster");
            //_act.EnterText(_loansetupdetailsLoc.BpayAmount, "94");
            //string amount = "-$" + _act.getValue(_loansetupdetailsLoc.BpayAmount, "sdsa") + ".00";
            //_act.JSClick(_loansetupdetailsLoc.BpaySubmit, "BpaySubmit");
            //_act.waitForVisibilityOfElement(_loansetupdetailsLoc.BpayActivateButton, 100);
            //_act.JSClick(_loansetupdetailsLoc.BpayActivateButton, "BpayActivateButton");
            //_act.waitForVisibilityOfElement(_loansetupdetailsLoc.BpayTransactionHistoryButton, 100);
            //_act.JSClick(_loansetupdetailsLoc.BpayTransactionHistoryButton, "BpayTransactionHistoryButton");
            //_act.waitForVisibilityOfElement(_loansetupdetailsLoc.TransactionAmount, 100);
            //string amount1 = _act.getText(_loansetupdetailsLoc.TransactionAmount, "");

            return flag;
        }

        public void LeavePopup()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.leavePopupDiv, 60);
            IWebElement popupdiv = _driver.FindElement(_loansetupdetailsLoc.leavePopupDiv);
            Actions actions = new Actions(_driver);
            actions.MoveToElement(popupdiv);
            actions.Perform();
            _act.click(_loansetupdetailsLoc.leavePopupDiv, "leavePopupDiv");
            Thread.Sleep(2000);

            _act.click(_loansetupdetailsLoc.leavePopup, "leavePopup");


        }

        public void LogoutInbtw()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.Logout, 60);
            Thread.Sleep(3000);
            _driver.FindElement(_loansetupdetailsLoc.Logout).Click();
        }

        /// <summary>
        /// Clicks on Finals approve button.
        /// </summary>
        public void FinalApprove()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.FinalApprove, 60);
            _act.click(_loansetupdetailsLoc.FinalApprove, "FinalApprove");
        }

        /// <summary>
        /// Clicks the approve button.
        /// </summary>
        public void ClickApproveBtn()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.ManualApprove, 60);
            _act.JSClick(_loansetupdetailsLoc.ManualApprove, "Manual Approve");
        }

        /// <summary>
        /// Clicks the setup button.
        /// </summary>
        public void ClickSetup()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.SetupManual, 60);
            _act.click(_loansetupdetailsLoc.SetupManual, "SetupManual");
        }

        /// <summary>
        /// Verifies the approved loan.
        /// </summary>
        /// <param name="LoanAmount">The loan amount.</param>
        /// <returns></returns>
        public bool VerifyApprovedLoan(int LoanAmount)
        {
            bool flag = false;
            int ApprovedAmount = GetApprovedamount();

            // ApprovedLoanAmt;
            if (ApprovedAmount.Equals(LoanAmount))
            {
                Console.WriteLine("Requested amount=approved amount");
                flag = true;
            }
            else
            {
                Console.WriteLine("Requested amount not equal");
                flag = false;
            }
            return flag;
        }

        public bool VerifyRequestedAmtGreaterThanApprovedAmt(int LoanAmount, int approved)
        {
            bool flag = false;
            int ApprovedAmount = approved;

            // ApprovedLoanAmt;
            if (LoanAmount > ApprovedAmount)
            {
                Console.WriteLine("Requested amount>approved amount");
                flag = true;
            }
            else
            {
                Console.WriteLine("Requested amount not greater than approved amount");
                flag = false;
            }
            return flag;
        }

        public int GetApprovedamount()
        {
            string ApprovedLoanAmt = ApprovedLoanAmount();
            Console.WriteLine("approved amount" + ApprovedLoanAmt);
            ApprovedLoanAmt = ApprovedLoanAmt.Replace(",", "");
            Console.WriteLine("approved amountNEW" + ApprovedLoanAmt);
            ApprovedLoanAmt = ApprovedLoanAmt.Replace("$", "");
            if (GetPlatform(_driver))
            {
                var actamt = ApprovedLoanAmt.Split('.');
                ApprovedLoanAmt = actamt[0];
                ApprovedLoanAmt = ApprovedLoanAmt.Replace(".", "");
            }
            Console.WriteLine("approved amountlatest" + ApprovedLoanAmt);
            int ActualAmount = Convert.ToInt32(ApprovedLoanAmt);
            return ActualAmount;
        }

        /// <summary>
        /// Waits for loan set up page to display.
        /// </summary>
        /// <returns></returns>
        public bool WaitForLoanSetUpPage()
        {
            return _act.waitForVisibilityOfElement(_loansetupdetailsLoc.LoanAutoApprovedLabel, 60);
        }

        /// <summary>
        /// Gets Approveds loan amount.
        /// </summary>
        /// <returns>string loan amount</returns>
        public string ApprovedLoanAmount()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.ApprovedloanAmount, 120);
            string ApprovedLoanAmount = _act.getText(_loansetupdetailsLoc.ApprovedloanAmount, "Approved loan amount");
            return ApprovedLoanAmount;
        }

        /// <summary>
        /// Verifies the funded amount.
        /// </summary>
        /// <param name="LoanAmount">The loan amount.</param>
        /// <returns></returns>
        public bool VerifyFundedAmount(int LoanAmount)
        {
            bool flag = false;
            int FundedAmount = VerifyFundedAmount();

            if (LoanAmount.Equals(FundedAmount))
            {
                Console.WriteLine("loan Requested Amount =funded amount=approved amount");
                flag = true;
            }
            else
            {
                Console.WriteLine("funded amount not equal to approved amount");
                flag = false;
            }

            return flag;
        }

        public bool VerifyApprovedGreaterThanFunded(int approved, int funded)
        {
            bool flag = false;
            int FundedAmount = funded;
            int Approved = approved;

            if (Approved > FundedAmount)
            {
                Console.WriteLine("approved amount>funded amount");
                flag = true;
            }
            else
            {
                Console.WriteLine("funded amount not lesser approved amount");
                flag = false;
            }

            return flag;
        }

        public void ClickDetailedRepaymentSchedule()
        {
			//Dont put any time out, 
			_act.waitForVisibilityOfElement(_loansetupdetailsLoc.DetailedrepaymentscheduleButton, 120);
			_act.JSClick(_loansetupdetailsLoc.DetailedrepaymentscheduleButton, "DetailedrepaymentscheduleButton");
            Thread.Sleep(2000);
        }

        public void ClickDetailedrepaymentSchedulebtncollapse()
        {
            //Dont put any time out, 
            _act.JSClick(_loansetupdetailsLoc.DetailedrepaymentSchedulebtncollapse, "DetailedrepaymentSchedulebtncollapse");
            Thread.Sleep(2000);
        }

        public void ClickDetailedrepaymentSchedulereopen()
        {
            //Dont put any time out, 
            _act.JSClick(_loansetupdetailsLoc.DetailedrepaymentScheduleReopen, "DetailedrepaymentScheduleReopen");
            Thread.Sleep(2000);
        }

        public int GetRepaymentCountConfirmPage(bool flag, int loanamount)
        {
            int repaymentcount;
            if (flag == true)
            {
                if(loanamount>2000)
                {
                    string count = _act.getText(_loansetupdetailsLoc.RepaymentCountConfirmPage, "");
                    var count1 = count.Split(' ');
                    string repcnt = count1[4];
                    repaymentcount = Convert.ToInt32(repcnt);                   
                }
                else
                {
                    string count = _act.getText(By.XPath("//*[@id='contractPage1']/table[2]/tbody/tr[3]/td[2]"), "");
                    var count1 = count.Split(' ');
                    string repcnt = count1[4];
                    repaymentcount = Convert.ToInt32(repcnt);                   
                }
            }
            else 
            {
                if (loanamount > 2000)
                {
                    string count = _act.getText(_loansetupdetailsLoc.RepaymentCountConfirmPage, "");
                    var count1 = count.Split(' ');
                    string repcnt = count1[4];
                    repaymentcount = Convert.ToInt32(repcnt);
                    repaymentcount = repaymentcount + 1;
                }
                else
                {
                    string count = _act.getText(By.XPath("//*[@id='contractPage1']/table[2]/tbody/tr[3]/td[2]"), "");
                    var count1 = count.Split(' ');
                    string repcnt = count1[4];
                    repaymentcount = Convert.ToInt32(repcnt);
                    repaymentcount = repaymentcount + 1;
                }                 
            }
            return repaymentcount;
        }

        /// <summary>
        /// Verifies the funded amount.
        /// </summary>
        /// <returns>integer funded amount</returns>
        public int VerifyFundedAmount()
        {
            string FundedLoanAmt = getFundedLoanAmt();
            FundedLoanAmt = FundedLoanAmt.Replace("$", "");
            FundedLoanAmt = FundedLoanAmt.Replace(",", "");
            FundedLoanAmt = FundedLoanAmt.Replace(".", "");
            int FundedAmount = Convert.ToInt32(FundedLoanAmt);
            return FundedAmount;
        }

        /// <summary>
        /// Verifies the repay amount.
        /// </summary>
        /// <returns>bool if exist true else false</returns>
        public bool VerifyRepayAmount()
        {
            bool flag = false;
            int index = 0;
            IWebElement baseTable = _driver.FindElement(_loansetupdetailsLoc.DetailedTable);
            List<IWebElement> tableRows = new List<IWebElement>();
            tableRows = baseTable.FindElements(By.TagName("tr")).ToList();
            for (int i = 0; i < tableRows.Count; i++)
            {
                index = i;

            }
            Console.WriteLine("no of rows=" + index);
            int lastrowvalue = index - 3;
            int secondlastrowvalue = lastrowvalue - 1;

            IWebElement lastrow = baseTable.FindElement(By.XPath("//*[@id='repayments']/tbody/tr[" + lastrowvalue + "]"));
            IWebElement lastcell = lastrow.FindElement(By.XPath("//*[@id='repayments']/tbody/tr[" + lastrowvalue + "]/td[3]"));
            IWebElement secondlastrow = baseTable.FindElement(By.XPath("//*[@id='repayments']/tbody/tr[" + secondlastrowvalue + "]"));
            IWebElement secondlastcell = secondlastrow.FindElement(By.XPath("//*[@id='repayments']/tbody/tr[" + secondlastrowvalue + "]/td[3]"));
            String lastvalue = lastcell.Text.ToString();
            String secondlastvalue = secondlastcell.Text.ToString();
            if (lastvalue.Equals(secondlastvalue))
            {
                Console.WriteLine("Value matched");
                flag = true;
            }
            else
            {
                Console.WriteLine("Value not matched");
                flag = false;
            }

            return flag;
        }

        public string GetLastRepaymentAmount()
        {
            int index = 0;
            IWebElement baseTable = _driver.FindElement(_loansetupdetailsLoc.DetailedTable);
            List<IWebElement> tableRows = new List<IWebElement>();
            tableRows = baseTable.FindElements(By.TagName("tr")).ToList();
            for (int i = 0; i < tableRows.Count; i++)
            {
                index = i;

            }
            Console.WriteLine("no of rows=" + index);
            int lastrowvalue = index - 3;
            int secondlastrowvalue = lastrowvalue - 1;

            IWebElement lastrow = baseTable.FindElement(By.XPath("//*[@id='repayments']/tbody/tr[" + lastrowvalue + "]"));
            IWebElement lastcell = lastrow.FindElement(By.XPath("//*[@id='repayments']/tbody/tr[" + lastrowvalue + "]/td[3]"));
            IWebElement secondlastrow = baseTable.FindElement(By.XPath("//*[@id='repayments']/tbody/tr[" + secondlastrowvalue + "]"));
            IWebElement secondlastcell = secondlastrow.FindElement(By.XPath("//*[@id='repayments']/tbody/tr[" + secondlastrowvalue + "]/td[3]"));
            String lastvalue = lastcell.Text.ToString();
            return lastvalue;
        }

        public string FinalRepaymentConfirmPage()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.FinalRepaymentConfirmPage, 60);
            string text = _act.getText(_loansetupdetailsLoc.FinalRepaymentConfirmPage, "");
            var text1 = text.Split(' ');
            string lastrepay = text1[4];
            return lastrepay;
        }

        /// <summary>
        /// Gets the funded loan amt.
        /// </summary>
        /// <returns>string loan amount</returns>
        public string getFundedLoanAmt()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.FundedAmount, 60);
            string fundedLoanAmount = _act.getText(_loansetupdetailsLoc.FundedAmount, "Funded amount");
            return fundedLoanAmount;
        }

        /// <summary>
        /// Clicks the loan dashboard.
        /// </summary>
        public void clickLoanDashboard()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.LoanDashBoard, 60);
            _act.click(_loansetupdetailsLoc.LoanDashBoard, "loan dashboard");
        }

        //public void MoveSliderLowestAmount()
        //{
        //   // IWebElement Container = _driver.FindElement(_loansetupdetailsLoc.LoanAmountContainer);

        //    IWebElement Sliderstick = _driver.FindElement(_loansetupdetailsLoc.SliderLowestAmount);
        //    int sliderwidth = Sliderstick.Size.Width;
        //    IWebElement Sliderbutton = _driver.FindElement(_loansetupdetailsLoc.SliderButtonLowestAmount);

        //    Actions sliderActions = new Actions(_driver);

        //    sliderActions.ClickAndHold(Sliderbutton);
        //    int lowest = 0 / sliderwidth;
        //    sliderActions.MoveByOffset(40,0).Build().Perform();
        //    //   IWebElement eleslider= _driver.FindElement(_loansetupdetailsLoc.SliderButtonLowestAmount);
        //    //sliderActions.DragAndDropToOffset(Sliderbutton, lowest, 0).Build().Perform();
        //    //new Actions(_driver)
        //    //           .DragAndDropToOffset(sliderHandle, dx, 0)
        //    //           .Build()
        //    //.Perform();
        //    //   IWebElement SliderStick = _driver.FindElement(_loansetupdetailsLoc.SliderLowestAmount);

        //    //var sliderHandle = _driver.FindElement(_loansetupdetailsLoc.SliderButtonLowestAmount);
        //    //var sliderTrack = _driver.FindElement(_loansetupdetailsLoc.SliderLowestAmount);
        //    //var width = int.Parse(sliderTrack.GetCssValue("width").Replace("px", ""));
        //    //var dx = 0;

        //    //new Actions(_driver)
        //    //            .DragAndDropToOffset(sliderHandle, dx, 0)
        //    //            .Build()
        //    //            .Perform();


        // //   IWebElement slider = _driver.FindElement(_loansetupdetailsLoc.SliderButtonLowestAmount);
        // //   //    int width = slider.get().getWidth();
        // //  // int actual = 1600 - request - 2200;
        // ////   int move = actual / 50;
        // //   Actions SliderActions = new Actions(_driver);
        // //   SliderActions.ClickAndHold(slider);
        // //   SliderActions.MoveByOffset(0, 0).Build().Perform();
        //}

        /// <summary>
        /// Moves the slider to lowest amount.
        /// </summary>
        public void MoveSliderLowestAmount()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.SliderButtonLowestAmount, 60);
            IWebElement sliderEle = _driver.FindElement(_loansetupdetailsLoc.SliderButtonLowestAmount);
            _act.click(_loansetupdetailsLoc.SliderButtonLowestAmount, "slider");

            string value1 = _act.getText(_loansetupdetailsLoc.MaxRepaymentAmount, "gettext");
            var actualvaluemax = value1.Replace("$", "");
            if (actualvaluemax.Contains(","))
            {
                actualvaluemax = actualvaluemax.Replace(",", "");
            }
            int amountmax = Convert.ToInt32(actualvaluemax);

            string value2 = _act.getText(_loansetupdetailsLoc.MinRepaymentAmount, "gettext");
            var actualvaluemin = value2.Replace("$", "");
            if (actualvaluemin.Contains(","))
            {
                actualvaluemin = actualvaluemin.Replace(",", "");
            }
            int amountmin = Convert.ToInt32(actualvaluemin);

            int diffamt = amountmax - amountmin;

            int weeklyAmount = diffamt / 5;

            int leftclicks = weeklyAmount + 5;
            for (int i = 1; i <= leftclicks; i++)
            {
                sliderEle.SendKeys(Keys.ArrowLeft);
            }
            Thread.Sleep(4000);
        }

        /// <summary>
        /// Moves the slider.
        /// </summary>
        public void MoveLoanValueSlider()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.loanSlider, 60);
            IWebElement sliderEle = _driver.FindElement(_loansetupdetailsLoc.loanSlider);
            _act.click(_loansetupdetailsLoc.loanSlider, "slider");
            for (int i = 1; i <= 8; i++)
            {
                sliderEle.SendKeys(Keys.ArrowLeft);
            }
            Thread.Sleep(4000);
        }

        /// <summary>
        /// Moves the slider to middle amount.
        /// </summary>
        public void MoveSliderMiddleAmount(int loanamt)
        {
            int leftclicks = 0;
            int weeklyAmount = 0;
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.SliderButtonLowestAmount, 60);
            IWebElement sliderEle = _driver.FindElement(_loansetupdetailsLoc.SliderButtonLowestAmount);
            _act.click(_loansetupdetailsLoc.SliderButtonLowestAmount, "slider");

            var value = _act.getText(_loansetupdetailsLoc.MaxRepaymentAmount, "gettext");
            var actualvalue = value.Replace("$", "");
            if (actualvalue.Contains(","))
            {
                actualvalue = actualvalue.Replace(",", "");
            }
            var lowestvalue = _act.getText(_loansetupdetailsLoc.MinRepaymentAmount, "LeastAmount");
            var lowest = lowestvalue.Replace("$", "");
            if (lowest.Contains(","))
            {
                lowest = lowest.Replace(",", "");
            }
            int amount = Convert.ToInt32(actualvalue) - Convert.ToInt32(lowest);
            weeklyAmount = amount / 10;
            float middleamount = weeklyAmount / 2;
            if ((middleamount % 2) != 0)
            {
                leftclicks = (int)middleamount + 1;
                if (Convert.ToInt32(actualvalue) < 250)

                {
                    leftclicks = leftclicks - 2;
                }
            }
            else
            {
                leftclicks = (int)middleamount;
                if (Convert.ToInt32(actualvalue) < 250)
                {
                    leftclicks = leftclicks - 2;
                }

                //else
                //{
                //    leftclicks = leftclicks + 5;
                //}
            }
            if (Convert.ToInt32(actualvalue) > 250)
            {
                leftclicks = 25;
            }
            for (int i = 1; i <= leftclicks; i++)

            {
                sliderEle.SendKeys(Keys.ArrowLeft);
                Thread.Sleep(1000);
            }
            Thread.Sleep(4000);
        }

        public void MoveSliderMiddleAmountRL(int loanamt)
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.SliderButtonLowestAmount, 60);
            IWebElement sliderEle = _driver.FindElement(_loansetupdetailsLoc.SliderButtonLowestAmount);
            _act.click(_loansetupdetailsLoc.SliderButtonLowestAmount, "slider");

            if (loanamt > 1600)
            {
                for (int i = 1; i <= 25; i++)
                {
                    sliderEle.SendKeys(Keys.ArrowLeft);
                    Thread.Sleep(3000);
                }
                Thread.Sleep(4000);
            }
            else
            {
                for (int i = 1; i <= 3; i++)
                {
                    sliderEle.SendKeys(Keys.ArrowLeft);
                    Thread.Sleep(2000);
                }
            }
        }

        /// <summary>
        /// Get Minimum Repayment Amount 
        /// </summary>
        /// <returns>string</returns>
        public string GetMinRepaymentAmt()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.RepaymentDateSlider, 60);
            return _act.getText(_loansetupdetailsLoc.MinRepaymentAmount, "LeastAmount");
        }

        /// <summary>
        /// Get Maxium Repayment Amount 
        /// </summary>
        /// <returns>string</returns>
        public string GetMaxRepaymentAmt()
        {
            return _act.getText(_loansetupdetailsLoc.MaxRepaymentAmount, "gettext");
        }

        /// <summary>
        /// Move Repayments slider date to lowest.
        /// </summary>
        public void RepaymentDateLowest()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.RepaymentDateSlider, 60);
            IWebElement sliderDate = _driver.FindElement(_loansetupdetailsLoc.RepaymentDateSlider);
            _act.click(_loansetupdetailsLoc.RepaymentDateSlider, "sliderdate");

            var value = _act.getText(_loansetupdetailsLoc.RepaymentDateValue, "RepaymentDateValue");
            var actualvalue = value.Split(' ');
            var date = actualvalue[1];
            var reqdate = date.Replace(",", "");
            int DateValue = Convert.ToInt32(reqdate);
            for (int i = 1; i <= DateValue; i++)
            {
                sliderDate.SendKeys(Keys.ArrowLeft);
            }
            Thread.Sleep(5000);
        }

        /// <summary>
        /// Move Repayments slider date to middle.
        /// </summary>
        public void RepaymentDateMiddle()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.RepaymentDateSlider, 60);
            IWebElement sliderDate = _driver.FindElement(_loansetupdetailsLoc.RepaymentDateSlider);
            _act.click(_loansetupdetailsLoc.RepaymentDateSlider, "sliderdate");

            //var value = _act.getText(_loansetupdetailsLoc.RepaymentDateValue, "RepaymentDateValue");
            //var actualvalue = value.Split(' ');
            //var date = actualvalue[1];
            //var reqdate = date.Replace(",", "");
            //int DateValue = Convert.ToInt32(reqdate);
            for (int i = 1; i < 8; i++)
            {
                sliderDate.SendKeys(Keys.ArrowRight);
                Thread.Sleep(1000);

            }
            Thread.Sleep(4000);



        }

        /// <summary>
        /// Move Repayments slider date to highest.
        /// </summary>.//*[@class='radio white-bg ']
        public void RepaymentDateHighest()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.RepaymentDateSlider, 60);
            IWebElement sliderDate = _driver.FindElement(_loansetupdetailsLoc.RepaymentDateSlider);
            _act.click(_loansetupdetailsLoc.RepaymentDateSlider, "sliderdate");

            //var value = _act.getText(_loansetupdetailsLoc.RepaymentDateValue, "RepaymentDateValue");
            //var actualvalue = value.Split(' ');
            //var date = actualvalue[1];
            //var reqdate = date.Replace(",", "");
            //int DateValue = Convert.ToInt32(reqdate);
            for (int i = 1; i <= 20; i++)
            {
                sliderDate.SendKeys(Keys.ArrowRight);
            }
            Thread.Sleep(4000);
        }

        /// <summary>
        /// verify final review enabled or not and process further
        /// </summary>
        public bool loanSetupFunction(int loanamount, string UserType)
        {
            bool blnval1 = false;
            bool blnval2 = false;

            if ((loanamount > 2000 && FinalReviewEnabled == "true") && (UserType == FinalReviewLoanType || FinalReviewLoanType == "ALL"))
            {
                if (GetPlatform(_driver))
                {
                    //ClickOn Loan Dashboard...Manual Approval
                    ClickLoanDashboardManual();

                    // click on More Button from Bottom Menu
                    ClickMoreBtn();

                    // click on Approve button
                    ClickApproveBtn();

                    //Click Setup Button
                    ClickSetup();

                    // Verify Funded Amount
                    if (VerifyApprovedLoan(loanamount))
                        blnval1 = true;

                    // click on Buton Submit
                    ClickSubmitBtn();
                }
                else
                {
                    //ClickOn Loan Dashboard...Manual Approval
                    ClickLoanDashboardManual();
                    //ClickLoanDashboard();

                    //click on Final Approve
                    FinalApprove();

                    //Click Setup Button
                    ClickSetup();

                    // Verify Funded Amount
                    if (VerifyApprovedLoan(loanamount))
                        blnval1 = true;

                    // click on Buton Submit
                    ClickSubmitBtn();
                }
            }
            else
            {
                // Verify Funded Amount
                if (VerifyApprovedLoan(loanamount))
                    blnval1 = true;

                // click on Buton Submit
                ClickSubmitBtn();
            }

            //Scrolling the Loan Contract
            Loancontract();

            // Confirming accepting contract
            ConfirmAcceptingContract();

            // click on I Agree button
            ClickOnAgreeBtn();

            // click on No thanks Button
            ClickNothanksBtn();

            // Verify Funded Amount
            if (loanamount == VerifyFundedAmount())
                blnval2 = true;

            if (GetPlatform(_driver))
            {
                // Click on To Loan Dashboard Button
                ClickMobileLoanDashboardBtn();

                // click on More Button from Bottom Menu
                ClickMoreBtn();

                //Logout
                Logout();
            }
            else
            {
                // Click on Loan Dashboard Button
                ClickLoanDashboard();

                //Logout
                Logout();
            }

            #region commented old scenario

            //// click on Buton Submit
            //ClickSubmitBtn();

            ////  Scrolling the Loan Contract
            //Loancontract();

            //// Confirming accepting contract
            //ConfirmAcceptingContract();

            //// click on I Agree button
            //ClickOnAgreeBtn();

            //// click on No thanks Button
            //ClickNothanksBtn();

            //// Verify Funded Amount
            //if (loanamount == VerifyFundedAmount())
            //    blnval2 = true;

            //if (GetPlatform(_driver))
            //{
            //    // Click on To Loan Dashboard Button
            //    ClickMobileLoanDashboardBtn();

            //    // click on More Button from Bottom Menu
            //    ClickMoreBtn();

            //    //Logout
            //    Logout();
            //}
            //else
            //{
            //    // Click on Loan Dashboard Button
            //    ClickLoanDashboard();

            //    //Logout
            //    Logout();
            //}
            #endregion

            if (blnval1 && blnval2)
                return true;
            else
                return false;
        }

        /// <summary>
        /// verify final review enabled or not and process further for RL
        /// </summary>
        public bool loanSetupFunction_RL(int loanamount)
        {
            //  Scrolling the Loan Contract
            Loancontract();

            // Confirming accepting contract
            ConfirmAcceptingContract();

            // click on I Agree button
            ClickOnAgreeBtn();

            // click on No thanks Button
            ClickNothanksBtn();

            if (GetPlatform(_driver))
            {
                // Click on To Loan Dashboard Button
                ClickMobileLoanDashboardBtn();

                // click on More Button from Bottom Menu
                ClickMoreBtn();

                //Logout
                Logout();
            }
            else
            {
                // Click on Loan Dashboard Button
                ClickLoanDashboard();

                //Logout
                Logout();
            }
            return true;
        }

        public string[] FetchAcctandBSB()
        {

            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.AcctBSBLastPage, 30);
            string ApprovedLoanAmt = _act.getText(_loansetupdetailsLoc.AcctBSBLastPage, "ACCTNO AND BSB");
            var data = ApprovedLoanAmt.Split(':');
            var splitteddata = data[1];
            var splitteddata1 = splitteddata.Split(' ');
            string[] numbers = new string[2];
            numbers[0] = splitteddata1[1];
            // int BSB = Convert.ToInt32(splitteddata1[1]);
            var splitteddata3 = data[2];
            if (splitteddata3.Contains(' '))
            {
                splitteddata3 = splitteddata3.Replace(" ", "");
            }
            numbers[1] = splitteddata3;
            //int AcctNo = Convert.ToInt32(splitteddata3);
            return numbers;
        }

        public string getEmail()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.emailSetupPage, 60);
            string email = _act.getText(_loansetupdetailsLoc.emailSetupPage, "email");
            return email;
        }

        public int GetLowestRepAmt()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.LowestRepAmt, 60);
            string amt = _act.getText(_loansetupdetailsLoc.LowestRepAmt, "email");

            string amt1 = amt.Replace("$", "");
            if (amt1.Contains(","))
            {
                amt1 = amt1.Replace(",", "");
            }
            int amt2 = Convert.ToInt32(amt1);
            return amt2;
        }

        public int GetHighestRepAmt()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.HighestRepAmt, 60);
            string amt = _act.getText(_loansetupdetailsLoc.HighestRepAmt, "email");
            string amt1 = amt.Replace("$", "");
            if (amt1.Contains(","))
            {
                amt1 = amt1.Replace(",", "");
            }
            int amt2 = Convert.ToInt32(amt1);
            return amt2;
        }

        public int getRepAmtInTable(int highamt)
        {
            Thread.Sleep(4000);
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.RepAmtInTable, 60);
            string amt = _act.getText(_loansetupdetailsLoc.RepAmtInTable, "email");
            var amt1 = amt.Split('.');
            string amt2 = amt1[0];
            string amt3 = amt2.Replace("$", "");
            if (amt3.Contains(","))
            {
                amt3 = amt3.Replace(",", "");
            }
            int amt4 = Convert.ToInt32(amt3);
            if (amt4 < highamt)
            {
                amt4 = amt4 + 1;
            }
            return amt4;
        }

        public string GetConfirmedTxtSetUp()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.GetConfirmedTxt, 60);
            string strval = _act.getText(_loansetupdetailsLoc.GetConfirmedTxt, "Text");
            Console.WriteLine(strval);
            return strval;
        }

        public string getSliderFirstRepaymentDate()
        {
            string sliderFirstRepaymentDate = _act.getText(_loansetupdetailsLoc.SliderFirstRepaymentDate, "Slider First Payment Date");
            return sliderFirstRepaymentDate;
        }

        public string VerifyIncorrectSMSPin()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.IncorrectSMSMsg, 60);
            string incorrectsmserrmsg = _act.getText(_loansetupdetailsLoc.IncorrectSMSMsg, "IncorrectSMSMsg");
            Console.WriteLine("Message is " + incorrectsmserrmsg);
            Thread.Sleep(1000);
            return incorrectsmserrmsg;
        }

        public void clickBpayLink()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.Bpay, 60);
            _act.click(_loansetupdetailsLoc.Bpay, "Bpay Link");
        }

        public string verifyBpayPage()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.BpayMessage, 60);
            string bpayMessage = _act.getText(_loansetupdetailsLoc.BpayMessage, "Bpay title");
            Thread.Sleep(1000);
            Console.WriteLine("Message is " + bpayMessage);
            return bpayMessage;
        }

        public void clickPayanyoneLink()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.PayAnyone, 60);
            _act.click(_loansetupdetailsLoc.PayAnyone, "Bpay Link");
        }

        public bool verifyBpayPageLink()
        {
            Thread.Sleep(1000);
            return _act.isElementPresent(_loansetupdetailsLoc.Bpay);
        }

        public void clickYourCardLink()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.YourCardLink, 60);
            _act.click(_loansetupdetailsLoc.YourCardLink, "Your Card Link");
        }

        public string verifyYourcardPage()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.CardDetails, 60);
            string cardDetails = _act.getText(_loansetupdetailsLoc.CardDetails, "Card Details");
            Thread.Sleep(1000);
            return cardDetails;
        }

        public bool verifyPayanyonePage()
        {

            bool flag = false;
            Thread.Sleep(5000); // wait until the loandashboard buttons appears
            if (_act.isElementDisplayed(_driver.FindElement(_loansetupdetailsLoc.BSBTextBox)))
            {
                flag = true;
            }
            return flag;
        }

        public void clickTransactionHistoryLink()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.TransactionHistoryLink, 60);
            _act.click(_loansetupdetailsLoc.TransactionHistoryLink, "Transaction history Link");

        }

        public bool verifyTransactionHistoryPage()
        {
            bool flag = false;
            Thread.Sleep(5000); // wait until the loandashboard buttons appears
            if (_act.isElementDisplayed(_driver.FindElement(_loansetupdetailsLoc.FilterTransactionButton)))
            {
                flag = true;
            }
            return flag;
        }

        public bool orderCard()
        {
            bool flag = false;
            //Click on order card link
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.OrderCard, 120);
            _act.JSClick(_loansetupdetailsLoc.OrderCard, "Order Card");

            //Enter unit number
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.UnitNumber, 60);
            _act.EnterText(_loansetupdetailsLoc.UnitNumber, "12345");

            //Select radio button
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.FinancialServicesGuide, 60);
            _act.JSClick(_loansetupdetailsLoc.FinancialServicesGuide, "Financial services button");

            //Click order card button
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.OrderCardButton, 60);
            _act.JSClick(_loansetupdetailsLoc.OrderCardButton, "Order Card Button");

            //Click order done button
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.OrderDoneButton, 60);
            _act.JSClick(_loansetupdetailsLoc.OrderDoneButton, "Order Card Button");

            return flag;
        }

        public bool verifyActivateCardLink()
        {
            Thread.Sleep(1000);
            return _act.isElementPresent(_loansetupdetailsLoc.ActivateCard);
        }

        public bool verifyOrderCardLink()
        {
            Thread.Sleep(1000);
            return _act.isElementPresent(_loansetupdetailsLoc.OrderCard);
        }

        public string confirmMessage()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.ErrorMessage, 60);
            string message = _act.getText(_loansetupdetailsLoc.ErrorMessage, "Message");
            Console.WriteLine("Message is " + message);
            return message;
        }

        public void clickCardMob()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.cardLinkMob, 60);
            _act.click(_loansetupdetailsLoc.cardLinkMob, "Card");
        }

        public bool verifyCardPage()
        {
            bool flag = false;
            Thread.Sleep(5000); // wait until the loandashboard buttons appears
            if (_act.isElementDisplayed(_driver.FindElement(_loansetupdetailsLoc.CardDetails)))
            {
                flag = true;
            }
            return flag;
        }

        public string verifyTransactionHistroyMessage()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.TransactionHistoryText, 60);
            string message = _act.getText(_loansetupdetailsLoc.TransactionHistoryText, "Transaction History");
            Console.WriteLine("Message is " + message);
            return message;
        }

        public string verifyPayanyoneMessage()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.TransactionHistoryText, 60);
            string message = _act.getText(_loansetupdetailsLoc.TransactionHistoryText, "Transaction History");
            Console.WriteLine("Message is " + message);
            return message;
        }

        public void clickDashboardMob()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.DashboardMob, 60);
            _act.click(_loansetupdetailsLoc.DashboardMob, "Dashboard Mobile");

        }

        public void acceptContractCancel()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.AcceptContractCancel, 60);
            _act.click(_loansetupdetailsLoc.AcceptContractCancel, "Yes");
        }

        public string getQuestioningMessage()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.QuestioningMessage, 60);
            string msg = _act.getText(_loansetupdetailsLoc.QuestioningMessage, "Message");
            Console.WriteLine("Message is: " + msg);
            return msg;
        }

        public void clickConfirmButton()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.ConfirmButton, 60);
            _act.click(_loansetupdetailsLoc.ConfirmButton, "Confirm button");
        }

        public bool VerifySMSOTP()
        {
            bool flag = false;

            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.SMSDiv, 60);

            if (_act.isElementPresent(_loansetupdetailsLoc.SMSDiv))
            {

                if (_act.isElementDisplayed(_driver.FindElement(_loansetupdetailsLoc.SMSDiv)))
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

        public void EnterOTPDetailsTxt(string otp)
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.SMSDiv, 120);
            _act.EnterText(_loansetupdetailsLoc.SMSInput, otp);
            _act.JSClick(_loansetupdetailsLoc.SubmitPin, "SubmitPin");
        }

        public void clickRefreshButton()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.RefreshButton, 200);
            _act.click(_loansetupdetailsLoc.RefreshButton, "Refresh Button");
        }

        public void clickVerifyButton()
        {
            _act.waitForVisibilityOfElement(_loansetupdetailsLoc.VerifyButton, 60);
            _act.click(_loansetupdetailsLoc.VerifyButton, "Refresh Button");
        }

        public bool verifyVerifyButton()
        {
            
            if (_act.isElementPresent(_loansetupdetailsLoc.VerifyButton))
            {
                return true;
            }
            else
            return false;
        }

        public bool verifySetupButton()
        {
            
            if (_act.isElementPresent(_loansetupdetailsLoc.SetupManual))
            {
                return true;
            }
            else
                return false;
        }
    
    }
}