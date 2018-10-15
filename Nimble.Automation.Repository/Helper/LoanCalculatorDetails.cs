using Microsoft.VisualBasic;
using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository.Locators;
using NUnit.Framework;
using OpenQA.Selenium;
using Shouldly;
using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Nimble.Automation.Repository
{
	public class LoanCalculatorDetails : TestUtility
	{
		private ILoanCalculator _LoanCalculatorDetailsLoc;
		private ActionEngine _act = null;
		private IWebDriver _driver = null;
		public string NimbleDebugURL = ConfigurationManager.AppSettings.Get("localBaseDebugUrl");
		public string DebugUrlRL = ConfigurationManager.AppSettings.Get("DebugUrlRL");
		TestUtility _testut = new TestUtility();
		private readonly object date;
		private static int actualvalue;


		public string RLEmailID { get; set; }

		public LoanCalculatorDetails(IWebDriver driver, string strUserType)
		{
			if (GetPlatform(driver))
				_LoanCalculatorDetailsLoc = (strUserType == "NL") ? (ILoanCalculator)new LoanCalculatorDetailsMobileNLLoc() : new LoanCalculatorDetailsMobileRLLoc();
			else
				_LoanCalculatorDetailsLoc = (strUserType == "NL") ? (ILoanCalculator)new LoanCalculatorDetailsDesktopNLLoc() : new LoanCalculatorDetailsDesktopRLLoc();
			_act = new ActionEngine(driver);
			_driver = driver;
		}

		public bool OnlineCalculatorVisibility()
		{
			_act.waitForVisibilityOfElement(_LoanCalculatorDetailsLoc.OnlineCalculator, 60);
			bool flag = false;
			Thread.Sleep(5000); // wait until the loandashboard buttons appears
			if (_act.isElementDisplayed(_driver.FindElement(_LoanCalculatorDetailsLoc.OnlineCalculator)))
			{
				flag = true;
			}
			return flag;
		}

		public void RequestLoanCalculatorAmount(int requestedAmount)
		{
			//select loan value from slider
			_act.waitForVisibilityOfElement(_LoanCalculatorDetailsLoc.AmountSlider, 20);
			IWebElement sliderEle = _driver.FindElement(_LoanCalculatorDetailsLoc.AmountSlider);
			_act.click(By.XPath("//div[@id='amount-slider']/span[@class='ui-slider-handle ui-state-default ui-corner-all']"), "click");
			if (requestedAmount < 1600)
			{
				int actualvalue = 1600 - requestedAmount;
				int leftclicks = (actualvalue) / 25;
				for (int i = 1; i <= leftclicks; i++)
					sliderEle.SendKeys(Keys.ArrowLeft);
			}
			else if (requestedAmount > 1600 && requestedAmount <= 2000)
			{
				int actualvalue = requestedAmount - 1600;
				int rightclicks = (actualvalue) / 25;
				for (int i = 1; i <= rightclicks; i++)
					sliderEle.SendKeys(Keys.ArrowRight);
			}
			else if (requestedAmount > 2000)
			{
				int actualValue = 2000 - 1600;
				int rightClicks = (actualValue) / 25;
				for (int i = 1; i <= rightClicks; i++)
				{
					sliderEle.SendKeys(Keys.ArrowRight);
				}
				string sliderValue = "2000";
				if (sliderValue == "2000")
				{
					int value = requestedAmount - 2000;
					int rClicks = (value) / 50;
					for (int i = 1; i <= rClicks; i++)
					{
						sliderEle.SendKeys(Keys.ArrowRight);
					}
				}
			}
		}

		public void verifyloancalculatorselectedamount(int requestedAmount)
		{
			string defaultamount = _driver.FindElement(_LoanCalculatorDetailsLoc.defaultPOLamount).Text;
			string defaultamount2 = defaultamount.Replace("$", string.Empty);
			string defaultamount3 = defaultamount2.Replace(",", string.Empty);
			int defaultVal = Convert.ToInt32(defaultamount3);
			Assert.AreEqual(requestedAmount, defaultVal);
		}

		public void SelectPOLandAmount(int requestedAmount, string loanpurpose)
		{
			// Wait for First POL
			_act.waitForVisibilityOfElement(By.XPath("(//span[text()='- select purpose -'])[last()-2]"), 60);

			// click on first POL dropdown
			_act.click(By.XPath("(//span[text()='- select purpose -'])[last()-2]"), "first purpose");

			// select first POL from popup
			if (loanpurpose.Contains(","))
			{
				var purpose = loanpurpose.Split(',');
				foreach (var loan in purpose)
				{
					IWebElement selectedPOL = _driver.FindElement(By.XPath("//*[@id='POLOptions']//div/label[starts-with(text(),'" + loan + "')]/preceding-sibling::div"));
					string classtextwhenchecked = selectedPOL.GetAttribute("class");
					//  if (!classtextwhenchecked.Contains("checked"))
					//  {
					var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[starts-with(text(),'" + loan + "')]/preceding-sibling::div");
					//    Thread.Sleep(3000);
					_act.JSClick(polpurpose, "selectpurpose");
					//   Thread.Sleep(3000);
					// }
				}
				if (loanpurpose == "Other,Anything else")
				{
					_act.EnterText(By.XPath("//*[@class='polMoreDetails']"), "Secret it is");
				}
				_act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
			}
			else
			{
				IWebElement selectedPol = _driver.FindElement(By.XPath("//*[@id='POLOptions']//div/label[starts-with(text(),'" + loanpurpose + "')]/preceding-sibling::div"));
				string classtextwhenchecked = selectedPol.GetAttribute("class");
				//  if (!classtextwhenchecked.Contains("checked"))
				//  {
				var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[starts-with(text(),'" + loanpurpose + "')]/preceding-sibling::div");
				//   Thread.Sleep(3000);
				_act.JSClick(polpurpose, "selectpurpose");
				// Thread.Sleep(3000);
				//  }
				//  Thread.Sleep(3000);
				if (loanpurpose == "Other,Anything else")
				{
					_act.EnterText(By.XPath("//*[@class='polMoreDetails']"), "Secret it is");
				}
				_act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
			}

			// enter first POL loan amount
			_act.EnterText(By.XPath("(.//*[@type='number'])[last()-2]"), requestedAmount.ToString());

			//click on continue button
			_act.JSClick(By.XPath(".//*[@id='btnPOLsCompleted']"), "continue");

			if ((loanpurpose.Contains("Utility bills") || loanpurpose.Contains("Basic living/work expenses,Emergency Repairs")) &&
				_act.isElementPresent(By.XPath("//span[text()='More information']")))
			{
				_act.waitForVisibilityOfElement(By.XPath("//span[text()='More information']"), 60);
				_act.EnterText(By.XPath("//*[@name='poloverallmoreinfo']"), "hi hello");
				// click on MoreInfo continue Button
				_act.click(By.XPath("//*[@class='button-continue button sml button']"), "MoreInfoContinue");
			}
		}

        public void weeklyfortnightcalc(int requestedAmount, double durationlength)
        {
            double weekscalc = Math.Ceiling(durationlength / 4);
            //double weekscalc1 = Math.Round(weekscalc);
            double NumberofFortnights = Math.Ceiling(durationlength / 4);
            double FortnightlyCalc = Math.Ceiling(durationlength / 2);

            double SaccWeeklyExpected = Math.Round((requestedAmount * 1.2 + requestedAmount * 0.04 * weekscalc) / durationlength);
            double SaccFortnightlyExpected = Math.Round((requestedAmount * 1.2 + requestedAmount * 0.04 * NumberofFortnights) / FortnightlyCalc);

			string Weekly = _driver.FindElement(_LoanCalculatorDetailsLoc.WeeklyPayment).Text;
			string Fortnightly = _driver.FindElement(_LoanCalculatorDetailsLoc.FortNightlyPayment).Text;

			string WeeklyRepay = Weekly.Replace("$", string.Empty);
			int WeeklyActual = Convert.ToInt32(WeeklyRepay);

			string FortnightlyRepay = Fortnightly.Replace("$", string.Empty);
			int FortnightlyActual = Convert.ToInt32(FortnightlyRepay);

			double TotalWeeklyRepayment = SaccWeeklyExpected * durationlength;
			double TotalFortnightlyRepayment = SaccFortnightlyExpected * durationlength / 2;

			Assert.Multiple(() =>
			{
				Assert.AreEqual(WeeklyActual, SaccWeeklyExpected);
				Assert.AreEqual(FortnightlyActual, SaccFortnightlyExpected);
				//Assert.That(TotalWeeklyRepayment, Is.EqualTo("13 weeks"));

			});

		}

		public DateTime MACC_CalWeekly()
		{
			var dateAndTime = DateTime.Now;
			var date = dateAndTime.Date;
			DateTime FirstWeeklyRepayDate = date.AddDays(7);		
            FirstWeeklyRepayDate =  GetBusinessDay(FirstWeeklyRepayDate);

            //Holidays list
            int length = Holidays().GetLength(0);

            for (int i = 0; i < length; i++)
            {
               string nationalHoliday = Holidays()[i, 1];
                if (FirstWeeklyRepayDate.ToString("dd/MM/yyyy") == nationalHoliday)
                {
                    FirstWeeklyRepayDate = FirstWeeklyRepayDate.AddDays(1);
                    break;
                }
            }
            FirstWeeklyRepayDate = GetBusinessDay(FirstWeeklyRepayDate);
            return FirstWeeklyRepayDate;
        }

        public DateTime MACC_CalFortnightly()
        {
            var dateAndTime = DateTime.Now;
            var date = dateAndTime.Date;
            DateTime FirstFortnightlyRepayDate = date.AddDays(14);
            FirstFortnightlyRepayDate = GetBusinessDay(FirstFortnightlyRepayDate);

            //Holidays list
            int length = Holidays().GetLength(0);

            for (int i = 0; i < length; i++)
            {
                string nationalHoliday = Holidays()[i, 1];
                if (FirstFortnightlyRepayDate.ToString("dd/MM/yyyy") == nationalHoliday)
                {
                    FirstFortnightlyRepayDate = FirstFortnightlyRepayDate.AddDays(1);
                    break;
                }
            }
            FirstFortnightlyRepayDate = GetBusinessDay(FirstFortnightlyRepayDate);
            return FirstFortnightlyRepayDate;
        }

        public void DurationSliderMovement(int duration)
        {
			
			_act.waitForVisibilityOfElement(_LoanCalculatorDetailsLoc.DurationSlider, 120);
            IWebElement sliderEle = _driver.FindElement(_LoanCalculatorDetailsLoc.DurationSlider);
            _act.click(_LoanCalculatorDetailsLoc.DurationSlider, "Loan duration");
			if (duration < 10)
			{
				actualvalue = 10 - duration;
				int leftclicks = (actualvalue) / 1;
				for (int i = 1; i <= leftclicks; i++)
					sliderEle.SendKeys(Keys.ArrowLeft);
			}
			else if (duration > 10)
			{
				actualvalue =  duration - 10;
				int rightclicks = (actualvalue) / 1;
				for (int i = 1; i <= rightclicks; i++)
					sliderEle.SendKeys(Keys.ArrowRight);

			}
			
            
        }

        public void RequestLoanAmountDurationCalculation(int requestedAmount)
        {
            //select loan value from slider
            _act.waitForVisibilityOfElement(_LoanCalculatorDetailsLoc.AmountSlider, 20);
            IWebElement sliderEle = _driver.FindElement(_LoanCalculatorDetailsLoc.AmountSlider);
            _act.click(By.XPath("//div[@id='amount-slider']/span[@class='ui-slider-handle ui-state-default ui-corner-all']"), "click");

            if (requestedAmount < 1600)
            {
                int actualvalue = 1600 - requestedAmount;
                int leftclicks = (actualvalue) / 25;
                for (int i = 1; i <= leftclicks; i++)
                    sliderEle.SendKeys(Keys.ArrowLeft);
                Thread.Sleep(2000);
                string duration = _driver.FindElement(_LoanCalculatorDetailsLoc.defaultduration).Text;
                double durationlength = Convert.ToInt32(duration);
                weeklyfortnightcalc(requestedAmount, durationlength);
                string minweeks = _driver.FindElement(_LoanCalculatorDetailsLoc.minDurationSliderValue).Text;
                string maxweeks = _driver.FindElement(_LoanCalculatorDetailsLoc.maxDurationSliderValue).Text;

                Assert.Multiple(() =>
               {
                   Assert.That(duration, Is.EqualTo("10"));
                   Assert.That(minweeks, Is.EqualTo("9 weeks"));
                   Assert.That(maxweeks, Is.EqualTo("13 weeks"));
               });
            }

            else if (requestedAmount >= 1600 && requestedAmount <= 2000)
            {
                int actualvalue = requestedAmount - 1600;
                int rightclicks = (actualvalue) / 25;
                for (int i = 1; i <= rightclicks; i++)
                    sliderEle.SendKeys(Keys.ArrowRight);
                Thread.Sleep(2000);
                string duration = _driver.FindElement(_LoanCalculatorDetailsLoc.defaultduration).Text;
                double durationlength = Convert.ToInt32(duration);
                weeklyfortnightcalc(requestedAmount, durationlength);
                string minweeks = _driver.FindElement(_LoanCalculatorDetailsLoc.minDurationSliderValue).Text;
                string maxweeks = _driver.FindElement(_LoanCalculatorDetailsLoc.maxDurationSliderValue).Text;

                Assert.Multiple(() =>
                {
                    Assert.That(duration, Is.EqualTo("10"));
                    Assert.That(minweeks, Is.EqualTo("9 weeks"));
                    Assert.That(maxweeks, Is.EqualTo("13 weeks"));
                });
            }
            else if (requestedAmount > 2000)
            {
                int actualValue = 2000 - 1600;
                int rightClicks = (actualValue) / 25;
                for (int i = 1; i <= rightClicks; i++)
                {
                    sliderEle.SendKeys(Keys.ArrowRight);
                }
                string sliderValue = "2000";
                if (sliderValue == "2000")
                {
                    int value = requestedAmount - 2000;
                    int rClicks = (value) / 50;
                    for (int i = 1; i <= rClicks; i++)
                    {
                        sliderEle.SendKeys(Keys.ArrowRight);
                    }
                }
                Thread.Sleep(2000);
                if (requestedAmount > 2000 && requestedAmount <= 3000)
                {
                    string duration = _driver.FindElement(_LoanCalculatorDetailsLoc.defaultduration).Text;
                    string minmonths = _driver.FindElement(_LoanCalculatorDetailsLoc.minDurationSliderValue).Text;
                    string maxmonths = _driver.FindElement(_LoanCalculatorDetailsLoc.maxDurationSliderValue).Text;
					int durationlength = Convert.ToInt32(duration);
                    int durationamtweekly = durationlength * 4;
                    int durationamtfortnightly = durationlength * 2;
                    DateTime FirstRepaymentDateweekly = MACC_CalWeekly();
                    DateTime FirstRepaymentDatefortnightly = MACC_CalFortnightly();                  

                    string Weekly = _driver.FindElement(_LoanCalculatorDetailsLoc.WeeklyPayment).Text;
                    string Fortnightly = _driver.FindElement(_LoanCalculatorDetailsLoc.FortNightlyPayment).Text;

                    string WeeklyRepay = Weekly.Replace("$", string.Empty);
                    double WeeklyActual = Convert.ToDouble(WeeklyRepay);

                    string FortnightlyRepay = Fortnightly.Replace("$", string.Empty);
                    double FortnightlyActual = Convert.ToDouble(FortnightlyRepay);

                    LoanCalcluateSolver(requestedAmount, FirstRepaymentDateweekly, 7, WeeklyActual, durationamtweekly, durationamtweekly, WeeklyActual);
                    LoanCalcluateSolver(requestedAmount, FirstRepaymentDatefortnightly, 14, FortnightlyActual, durationamtfortnightly, durationamtfortnightly, FortnightlyActual);

                    Assert.Multiple(() =>
                    {
                        Assert.That(duration, Is.EqualTo("7"));
                        Assert.That(minmonths, Is.EqualTo("2 months"));
                        Assert.That(maxmonths, Is.EqualTo("12 months"));
                    });
                }
                else if (requestedAmount > 3000 && requestedAmount <= 4000)
                {
                    string duration = _driver.FindElement(_LoanCalculatorDetailsLoc.defaultduration).Text;
                    string minmonths = _driver.FindElement(_LoanCalculatorDetailsLoc.minDurationSliderValue).Text;
                    string maxmonths = _driver.FindElement(_LoanCalculatorDetailsLoc.maxDurationSliderValue).Text;
                    int durationlength = Convert.ToInt32(duration);
                    int durationamtweekly = durationlength * 4;
                    int durationamtfortnightly = durationlength * 2;

                    DateTime FirstRepaymentDateweekly = MACC_CalWeekly();
                    DateTime FirstRepaymentDatefortnightly = MACC_CalFortnightly();

                    string Weekly = _driver.FindElement(_LoanCalculatorDetailsLoc.WeeklyPayment).Text;
                    string Fortnightly = _driver.FindElement(_LoanCalculatorDetailsLoc.FortNightlyPayment).Text;

                    string WeeklyRepay = Weekly.Replace("$", string.Empty);
                    double WeeklyActual = Convert.ToDouble(WeeklyRepay);

                    string FortnightlyRepay = Fortnightly.Replace("$", string.Empty);
                    int FortnightlyActual = Convert.ToInt32(FortnightlyRepay);


                    LoanCalcluateSolver(requestedAmount, FirstRepaymentDateweekly, 7, WeeklyActual, durationamtweekly,durationamtweekly, WeeklyActual);
                    LoanCalcluateSolver(requestedAmount, FirstRepaymentDatefortnightly, 14, FortnightlyActual, durationamtfortnightly, durationamtfortnightly, FortnightlyActual);
                    Assert.Multiple(() =>
                    {
                        Assert.That(duration, Is.EqualTo("8"));
                        Assert.That(minmonths, Is.EqualTo("2 months"));
                        Assert.That(maxmonths, Is.EqualTo("16 months"));

                    });
                }
                else if (requestedAmount > 4000 && requestedAmount <= 5000)
                {
                    string duration = _driver.FindElement(_LoanCalculatorDetailsLoc.defaultduration).Text;
                    string minmonths = _driver.FindElement(_LoanCalculatorDetailsLoc.minDurationSliderValue).Text;
                    string maxmonths = _driver.FindElement(_LoanCalculatorDetailsLoc.maxDurationSliderValue).Text;
                    int durationlength = Convert.ToInt32(duration);
                    int durationamtweekly = durationlength * 4;
                    int durationamtfortnightly = durationlength * 2;

                    DateTime FirstRepaymentDateweekly = MACC_CalWeekly();
                    DateTime FirstRepaymentDatefortnightly = MACC_CalFortnightly();

                    string Weekly = _driver.FindElement(_LoanCalculatorDetailsLoc.WeeklyPayment).Text;
                    string Fortnightly = _driver.FindElement(_LoanCalculatorDetailsLoc.FortNightlyPayment).Text;

                    string WeeklyRepay = Weekly.Replace("$", string.Empty);
                    double WeeklyActual = Convert.ToDouble(WeeklyRepay);

                    string FortnightlyRepay = Fortnightly.Replace("$", string.Empty);
                    int FortnightlyActual = Convert.ToInt32(FortnightlyRepay);

                    LoanCalcluateSolver(requestedAmount, FirstRepaymentDateweekly, 7, WeeklyActual, durationamtweekly, durationamtweekly, WeeklyActual);
                    LoanCalcluateSolver(requestedAmount, FirstRepaymentDatefortnightly, 14, FortnightlyActual, durationamtfortnightly, durationamtfortnightly, FortnightlyActual);
                    Assert.Multiple(() =>
                    {
                        Assert.That(duration, Is.EqualTo("11"));
                        Assert.That(minmonths, Is.EqualTo("2 months"));
                        Assert.That(maxmonths, Is.EqualTo("22 months"));
                    });
                }
            }
        }

        public void RequestLoanAmountDurationCalculationRL(int requestedAmount)
        {
            //select loan value from slider
            _act.waitForVisibilityOfElement(_LoanCalculatorDetailsLoc.AmountSlider, 20);
            IWebElement sliderEle = _driver.FindElement(_LoanCalculatorDetailsLoc.AmountSlider);
            _act.click(By.XPath("//div[@id='amount-slider']/span[@class='ui-slider-handle ui-state-default ui-corner-all']"), "click");

            if (requestedAmount < 1600)
            {
                int actualvalue = 1600 - requestedAmount;
                int leftclicks = (actualvalue) / 25;
                for (int i = 1; i <= leftclicks; i++)
                    sliderEle.SendKeys(Keys.ArrowLeft);
                Thread.Sleep(2000);
                string duration = _driver.FindElement(_LoanCalculatorDetailsLoc.defaultduration).Text;
                double durationlength = Convert.ToInt32(duration);
                weeklyfortnightcalc(requestedAmount, durationlength);
                string minweeks = _driver.FindElement(_LoanCalculatorDetailsLoc.minDurationSliderValue).Text;
                string maxweeks = _driver.FindElement(_LoanCalculatorDetailsLoc.maxDurationSliderValue).Text;

                Assert.Multiple(() =>
                {
                    Assert.That(duration, Is.EqualTo("10"));
                    Assert.That(minweeks, Is.EqualTo("3 weeks"));
                    Assert.That(maxweeks, Is.EqualTo("13 weeks"));
                });
            }
            else if (requestedAmount >= 1600 && requestedAmount <= 2000)
            {
                int actualvalue = requestedAmount - 1600;
                int rightclicks = (actualvalue) / 25;
                for (int i = 1; i <= rightclicks; i++)
                    sliderEle.SendKeys(Keys.ArrowRight);
                Thread.Sleep(2000);
                string duration = _driver.FindElement(_LoanCalculatorDetailsLoc.defaultduration).Text;
                double durationlength = Convert.ToInt32(duration);
                weeklyfortnightcalc(requestedAmount, durationlength);
                string minweeks = _driver.FindElement(_LoanCalculatorDetailsLoc.minDurationSliderValue).Text;
                string maxweeks = _driver.FindElement(_LoanCalculatorDetailsLoc.maxDurationSliderValue).Text;

                Assert.Multiple(() =>
                {
                    Assert.That(duration, Is.EqualTo("10"));
                    Assert.That(minweeks, Is.EqualTo("3 weeks"));
                    Assert.That(maxweeks, Is.EqualTo("13 weeks"));
                });
            }
            else if (requestedAmount > 2000)
            {
                int actualValue = 2000 - 1600;
                int rightClicks = (actualValue) / 25;
                for (int i = 1; i <= rightClicks; i++)
                {
                    sliderEle.SendKeys(Keys.ArrowRight);
                }
                string sliderValue = "2000";
                if (sliderValue == "2000")
                {
                    int value = requestedAmount - 2000;
                    int rClicks = (value) / 50;
                    for (int i = 1; i <= rClicks; i++)
                    {
                        sliderEle.SendKeys(Keys.ArrowRight);
                    }
                    Thread.Sleep(2000);

                }
                if (requestedAmount > 2000 && requestedAmount <= 3000)
                {
                    string duration = _driver.FindElement(_LoanCalculatorDetailsLoc.defaultduration).Text;
                    string minmonths = _driver.FindElement(_LoanCalculatorDetailsLoc.minDurationSliderValue).Text;
                    string maxmonths = _driver.FindElement(_LoanCalculatorDetailsLoc.maxDurationSliderValue).Text;
                    int durationlength = Convert.ToInt32(duration);
                    int durationamtweekly = durationlength * 4;
                    int durationamtfortnightly = durationlength * 2;

                    DateTime FirstRepaymentDateweekly = MACC_CalWeekly();
                    DateTime FirstRepaymentDatefortnightly = MACC_CalFortnightly();

                    string Weekly = _driver.FindElement(_LoanCalculatorDetailsLoc.WeeklyPayment).Text;
                    string Fortnightly = _driver.FindElement(_LoanCalculatorDetailsLoc.FortNightlyPayment).Text;

                    string WeeklyRepay = Weekly.Replace("$", string.Empty);
                    double WeeklyActual = Convert.ToDouble(WeeklyRepay);

                    string FortnightlyRepay = Fortnightly.Replace("$", string.Empty);
                    double FortnightlyActual = Convert.ToDouble(FortnightlyRepay);


                    LoanCalcluateSolver(requestedAmount, FirstRepaymentDateweekly, 7, WeeklyActual, durationamtweekly, durationamtweekly, WeeklyActual);
                    LoanCalcluateSolver(requestedAmount, FirstRepaymentDatefortnightly, 14, FortnightlyActual, durationamtfortnightly, durationamtfortnightly, FortnightlyActual);

                    Assert.Multiple(() =>
                    {
                        Assert.That(duration, Is.EqualTo("7"));
                        Assert.That(minmonths, Is.EqualTo("2 months"));
                        Assert.That(maxmonths, Is.EqualTo("12 months"));

                    });

                }
                else if (requestedAmount > 3000 && requestedAmount <= 4000)
                {
                    string duration = _driver.FindElement(_LoanCalculatorDetailsLoc.defaultduration).Text;
                    string minmonths = _driver.FindElement(_LoanCalculatorDetailsLoc.minDurationSliderValue).Text;
                    string maxmonths = _driver.FindElement(_LoanCalculatorDetailsLoc.maxDurationSliderValue).Text;
                    int durationlength = Convert.ToInt32(duration);
                    int durationamtweekly = durationlength * 4;
                    int durationamtfortnightly = durationlength * 2;

                    DateTime FirstRepaymentDateweekly = MACC_CalWeekly();
                    DateTime FirstRepaymentDatefortnightly = MACC_CalFortnightly();


                    string Weekly = _driver.FindElement(_LoanCalculatorDetailsLoc.WeeklyPayment).Text;
                    string Fortnightly = _driver.FindElement(_LoanCalculatorDetailsLoc.FortNightlyPayment).Text;

                    string WeeklyRepay = Weekly.Replace("$", string.Empty);
                    double WeeklyActual = Convert.ToDouble(WeeklyRepay);

                    string FortnightlyRepay = Fortnightly.Replace("$", string.Empty);
                    double FortnightlyActual = Convert.ToDouble(FortnightlyRepay);

                    LoanCalcluateSolver(requestedAmount, FirstRepaymentDateweekly, 7, WeeklyActual, durationamtweekly, durationamtweekly, WeeklyActual);
                    LoanCalcluateSolver(requestedAmount, FirstRepaymentDatefortnightly, 14, FortnightlyActual, durationamtfortnightly, durationamtfortnightly, FortnightlyActual);
                    Assert.Multiple(() =>
                    {
                        Assert.That(duration, Is.EqualTo("8"));
                        Assert.That(minmonths, Is.EqualTo("2 months"));
                        Assert.That(maxmonths, Is.EqualTo("16 months"));
                    });
                }
                else if (requestedAmount > 4000 && requestedAmount <= 5000)
                {
                    string duration = _driver.FindElement(_LoanCalculatorDetailsLoc.defaultduration).Text;
                    string minmonths = _driver.FindElement(_LoanCalculatorDetailsLoc.minDurationSliderValue).Text;
                    string maxmonths = _driver.FindElement(_LoanCalculatorDetailsLoc.maxDurationSliderValue).Text;
                    int durationlength = Convert.ToInt32(duration);
                    int durationamtweekly = durationlength * 4;
                    int durationamtfortnightly = durationlength * 2;

                    DateTime FirstRepaymentDateweekly = MACC_CalWeekly();
                    DateTime FirstRepaymentDatefortnightly = MACC_CalFortnightly();


                    string Weekly = _driver.FindElement(_LoanCalculatorDetailsLoc.WeeklyPayment).Text;
                    string Fortnightly = _driver.FindElement(_LoanCalculatorDetailsLoc.FortNightlyPayment).Text;

                    string WeeklyRepay = Weekly.Replace("$", string.Empty);
                    double WeeklyActual = Convert.ToDouble(WeeklyRepay);

                    string FortnightlyRepay = Fortnightly.Replace("$", string.Empty);
                    double FortnightlyActual = Convert.ToDouble(FortnightlyRepay);

                    LoanCalcluateSolver(requestedAmount, FirstRepaymentDateweekly, 7, WeeklyActual, durationamtweekly, durationamtweekly, WeeklyActual);
                    LoanCalcluateSolver(requestedAmount, FirstRepaymentDatefortnightly, 14, FortnightlyActual, durationamtfortnightly, durationamtfortnightly, FortnightlyActual);

                    Assert.Multiple(() =>
                    {
                        Assert.That(duration, Is.EqualTo("11"));
                        Assert.That(minmonths, Is.EqualTo("2 months"));
                        Assert.That(maxmonths, Is.EqualTo("22 months"));
                    });
                }
            }
        }

        public string sliderMinAmountValue()
        {
            _act.waitForVisibilityOfElement(_LoanCalculatorDetailsLoc.minAmountSliderValue, 60);
            string sliderMinAmountValue = _act.getText(_LoanCalculatorDetailsLoc.minAmountSliderValue, "Min amount");
            return sliderMinAmountValue;
        }

        public string sliderMaxAmountValue()
        {
            _act.waitForVisibilityOfElement(_LoanCalculatorDetailsLoc.maxAmountSliderValue, 60);
            string sliderMaxAmountValue = _act.getText(_LoanCalculatorDetailsLoc.maxAmountSliderValue, "Max amount");
            return sliderMaxAmountValue;
        }

        public string sliderDefaultAmountValue()
        {
            _act.waitForVisibilityOfElement(_LoanCalculatorDetailsLoc.defaultAmountSliderValue, 60);
            string sliderDefaultAmountValue = _act.getText(_LoanCalculatorDetailsLoc.defaultAmountSliderValue, "Default amount");
            return sliderDefaultAmountValue;
        }

        public string sliderMinDurationValue()
        {
            _act.waitForVisibilityOfElement(_LoanCalculatorDetailsLoc.minDurationSliderValue, 60);
            string sliderMinDurationValue = _act.getText(_LoanCalculatorDetailsLoc.minDurationSliderValue, "Min duration");
            return sliderMinDurationValue;
        }

        public string sliderMaxDurationValue()
        {
            _act.waitForVisibilityOfElement(_LoanCalculatorDetailsLoc.maxDurationSliderValue, 60);
            string sliderMaxDurationValue = _act.getText(_LoanCalculatorDetailsLoc.maxDurationSliderValue, "Max duration");
            return sliderMaxDurationValue;
        }

        public string sliderDefaultDurationValue()
        {
            _act.waitForVisibilityOfElement(_LoanCalculatorDetailsLoc.defaultDurationSliderValue, 60);
            string sliderDefaultDurationValue = _act.getText(_LoanCalculatorDetailsLoc.defaultDurationSliderValue, "Default duration");
            return sliderDefaultDurationValue;
        }

        public string getWeeklyRepaymentAmount()
        {
            _act.waitForVisibilityOfElement(_LoanCalculatorDetailsLoc.weeklyRepayment, 60);
            string weeklyRepaymentValue = _act.getText(_LoanCalculatorDetailsLoc.weeklyRepayment, "Weekly Repayment");
            return weeklyRepaymentValue;
        }

        public string getFortnightlyRepaymentAmount()
        {
            _act.waitForVisibilityOfElement(_LoanCalculatorDetailsLoc.fortNightlyRepayment, 60);
            string fortnightlyRepaymentValue = _act.getText(_LoanCalculatorDetailsLoc.fortNightlyRepayment, "Fortnightly Repayment");
            return fortnightlyRepaymentValue;
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

        public void LoanCalcluateSolver(double ApprovedAmt, DateTime Repaydate, int freq, double RepaymentAmount, int totalrepayments, int noofrepayments, double lastrepayemnt)
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
                        balanceamount.ShouldBeInRange(-(totalrepayments * 0.99), (totalrepayments * 0.99));
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
    }
}
