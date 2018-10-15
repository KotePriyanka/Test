using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nimble.Automation.Repository.Locators
{
    public interface ILoanCalculator
    {
		By OnlineCalculator { get; }

		By AmountSlider { get; }

		By DurationSlider { get; }

		By minAmountSliderValue { get; }

        By defaultAmountSliderValue { get; }

        By maxAmountSliderValue { get; }

        By minDurationSliderValue { get; }

        By defaultDurationSliderValue { get; }

        By maxDurationSliderValue { get; }

        By weeklyRepayment { get; }

        By fortNightlyRepayment { get; }

		By defaultPOLamount { get; }

		By defaultduration { get; }

        By WeeklyPayment { get; }

	    By FortNightlyPayment { get; }

		By TotalWeeklyRepaymentAmount { get; }

		By TotalFortnightlyRepaymentAmount { get; }

	}

	public class LoanCalculatorDetailsMobileNLLoc : ILoanCalculator
	{
		public By OnlineCalculator => By.XPath("//div[contains(@id,'calculator')]");

		public By AmountSlider => By.XPath("//div[@id='amount-slider']/span[@class='ui-slider-handle ui-state-default ui-corner-all']");

		public By DurationSlider => By.XPath("//div[@id='durationSlider']//span[@class='ui-slider-handle ui-state-default ui-corner-all']");       

		public By minAmountSliderValue => By.XPath("//div[@id='amount-slider_wrap']//div[@class='minAmount minmax f-left font-14 charcoal']");

        public By defaultAmountSliderValue => By.XPath("//div[@id='amount-slider_wrap']//div[@class='display-value display-value-clear white text-center bold600']/span");

        public By maxAmountSliderValue => By.XPath("//div[@id='amount-slider_wrap']//div[@class='maxAmount minmax f-right font-14 charcoal']");

        public By minDurationSliderValue => By.XPath("//div[@id='durationSlider_wrap']//div[@class='minAmount minmax f-left font-14 charcoal']");

        public By defaultDurationSliderValue => By.XPath("//div[@id='durationSlider_wrap']//div[@class='display-value display-value-clear white text-center bold600']/span");

        public By maxDurationSliderValue => By.XPath("//div[@id='durationSlider_wrap']//div[@class='maxAmount minmax f-right font-14 charcoal']");

        public By weeklyRepayment => By.XPath("//td//p[@class='repaymentSentence text-center']//span[@class='weekly_totalRepayment']");

        public By fortNightlyRepayment => By.XPath("//td//p[@class='repaymentSentence text-center']//span[@class='fortnight_totalRepayment']");

		public By defaultPOLamount => By.XPath("//div[@class='loan-slider-wrapper m-b-30']//div[@class='display-value display-value-clear white text-center bold600']//span[1]");

		public By SetupRequestamount => By.XPath("//div[contains(@id,'section - progress - inner - right')]//b[1]");

		public By defaultduration => By.XPath("//div[@id='durationSlider_wrap']//div[1]//span[1]");

		public By WeeklyPayment => By.XPath("//div[contains(@id,'paymentBox')]//div[1]//span[1]");

		public By FortNightlyPayment => By.XPath("//div[contains(@id,'paymentBox')]//div[2]//span[1]");

		public By TotalWeeklyRepaymentAmount => By.XPath("//table[contains(@id,'paymentTable')]//span[contains(@class, 'weekly_totalRepayment')]");

		public By TotalFortnightlyRepaymentAmount => By.XPath("//table[contains(@id,'paymentTable')]//span[contains(@class,'fortnight_totalRepayment')]");

	}

	public class LoanCalculatorDetailsDesktopNLLoc : ILoanCalculator
	{
		public By OnlineCalculator => By.XPath("//div[contains(@id,'calculator')]");

		public By AmountSlider => By.XPath("//div[@id='amount-slider']/span[@class='ui-slider-handle ui-state-default ui-corner-all']");

        //public By DurationSlider => By.XPath("//div[@id='durationSlider']/span[@class='ui-slider-handle ui-state-default ui-corner-all ui-state-hover']");

        public By DurationSlider => By.XPath("(.//span[@class='ui-slider-handle ui-state-default ui-corner-all'])[2]");

        public By minAmountSliderValue => By.XPath("//div[@id='amount-slider_wrap']//div[@class='minAmount minmax f-left font-14 charcoal']");

        public By defaultAmountSliderValue => By.XPath("//div[@id='amount-slider_wrap']//div[@class='display-value display-value-clear white text-center bold600']/span");

        public By maxAmountSliderValue => By.XPath("//div[@id='amount-slider_wrap']//div[@class='maxAmount minmax f-right font-14 charcoal']");

        public By minDurationSliderValue => By.XPath("//div[@id='durationSlider_wrap']//div[@class='minAmount minmax f-left font-14 charcoal']");

        public By defaultDurationSliderValue => By.XPath("//div[@id='durationSlider_wrap']//div[@class='display-value display-value-clear white text-center bold600']/span");

        public By maxDurationSliderValue => By.XPath("//div[@id='durationSlider_wrap']//div[@class='maxAmount minmax f-right font-14 charcoal']");

        public By weeklyRepayment => By.XPath("//td//p[@class='repaymentSentence text-center']//span[@class='weekly_totalRepayment']");

        public By fortNightlyRepayment => By.XPath("//td//p[@class='repaymentSentence text-center']//span[@class='fortnight_totalRepayment']");

		public By defaultPOLamount => By.XPath("//div[@class='loan-slider-wrapper m-b-30']//div[@class='display-value display-value-clear white text-center bold600']//span[1]");

		public By SetupRequestamount => By.XPath("//div[contains(@id,'section - progress - inner - right')]//b[1]");

		public By defaultduration => By.XPath("//div[@id='durationSlider_wrap']//div[1]//span[1]");

		public By WeeklyPayment => By.XPath("//div[contains(@id,'paymentBox')]//div[1]//span[1]");

		public By FortNightlyPayment => By.XPath("//div[contains(@id,'paymentBox')]//div[2]//span[1]");

		public By TotalWeeklyRepaymentAmount => By.XPath("//table[contains(@id,'paymentTable')]//span[contains(@class, 'weekly_totalRepayment')]");

		public By TotalFortnightlyRepaymentAmount => By.XPath("//table[contains(@id,'paymentTable')]//span[contains(@class,'fortnight_totalRepayment')]");
	}

	public class LoanCalculatorDetailsMobileRLLoc : ILoanCalculator
	{
		public By OnlineCalculator => By.XPath("//div[contains(@id,'calculator')]");

		public By AmountSlider => By.XPath("//div[@id='amount-slider']/span[@class='ui-slider-handle ui-state-default ui-corner-all']");

		public By DurationSlider => By.XPath("//div[@id='durationSlider']/span[@class='ui-slider-handle ui-state-default ui-corner-all']");

		public By minAmountSliderValue => By.XPath("//div[@id='amount-slider_wrap']//div[@class='minAmount minmax f-left font-14 charcoal']");

        public By defaultAmountSliderValue => By.XPath("//div[@id='amount-slider_wrap']//div[@class='display-value display-value-clear white text-center bold600']/span");

        public By maxAmountSliderValue => By.XPath("//div[@id='amount-slider_wrap']//div[@class='maxAmount minmax f-right font-14 charcoal']");

        public By minDurationSliderValue => By.XPath("//div[@id='durationSlider_wrap']//div[@class='minAmount minmax f-left font-14 charcoal']");

        public By defaultDurationSliderValue => By.XPath("//div[@id='durationSlider_wrap']//div[@class='display-value display-value-clear white text-center bold600']/span");

        public By maxDurationSliderValue => By.XPath("//div[@id='durationSlider_wrap']//div[@class='maxAmount minmax f-right font-14 charcoal']");

        public By weeklyRepayment => By.XPath("//td//p[@class='repaymentSentence text-center']//span[@class='weekly_totalRepayment']");

        public By fortNightlyRepayment => By.XPath("//td//p[@class='repaymentSentence text-center']//span[@class='fortnight_totalRepayment']");

		public By defaultPOLamount => By.XPath("//div[@class='loan-slider-wrapper m-b-30']//div[@class='display-value display-value-clear white text-center bold600']//span[1]");

		public By SetupRequestamount => By.XPath("//div[contains(@id,'section - progress - inner - right')]//b[1]");

		public By defaultduration => By.XPath("//div[@id='durationSlider_wrap']//div[1]//span[1]");

		public By WeeklyPayment => By.XPath("//div[contains(@id,'paymentBox')]//div[1]//span[1]");

		public By FortNightlyPayment => By.XPath("//div[contains(@id,'paymentBox')]//div[2]//span[1]");

		public By TotalWeeklyRepaymentAmount => By.XPath("//table[contains(@id,'paymentTable')]//span[contains(@class, 'weekly_totalRepayment')]");

		public By TotalFortnightlyRepaymentAmount => By.XPath("//table[contains(@id,'paymentTable')]//span[contains(@class,'fortnight_totalRepayment')]");
	}

	public class LoanCalculatorDetailsDesktopRLLoc : ILoanCalculator
	{
		public By OnlineCalculator => By.XPath("//div[contains(@id,'calculator')]");
		
		public By AmountSlider => By.XPath("//div[@id='amount-slider']/span[@class='ui-slider-handle ui-state-default ui-corner-all']");

		public By DurationSlider => By.XPath("//div[@id='durationSlider']/span[@class='ui-slider-handle ui-state-default ui-corner-all ui-state-hover']");

		public By minAmountSliderValue => By.XPath("//div[@id='amount-slider_wrap']//div[@class='minAmount minmax f-left font-14 charcoal']");

        public By defaultAmountSliderValue => By.XPath("//div[@id='amount-slider_wrap']//div[@class='display-value display-value-clear white text-center bold600']/span");

        public By maxAmountSliderValue => By.XPath("//div[@id='amount-slider_wrap']//div[@class='maxAmount minmax f-right font-14 charcoal']");

        public By minDurationSliderValue => By.XPath("//div[@id='durationSlider_wrap']//div[@class='minAmount minmax f-left font-14 charcoal']");

        public By defaultDurationSliderValue => By.XPath("//div[@id='durationSlider_wrap']//div[@class='display-value display-value-clear white text-center bold600']/span");

        public By maxDurationSliderValue => By.XPath("//div[@id='durationSlider_wrap']//div[@class='maxAmount minmax f-right font-14 charcoal']");

        public By weeklyRepayment => By.XPath("//td//p[@class='repaymentSentence text-center']//span[@class='weekly_totalRepayment']");

        public By fortNightlyRepayment => By.XPath("//td//p[@class='repaymentSentence text-center']//span[@class='fortnight_totalRepayment']");

		public By defaultPOLamount => By.XPath("//div[@class='loan-slider-wrapper m-b-30']//div[@class='display-value display-value-clear white text-center bold600']//span[1]");

		public By defaultduration => By.XPath("//div[@id='durationSlider_wrap']//div[1]//span[1]");

		public By WeeklyPayment => By.XPath("//div[contains(@id,'paymentBox')]//div[1]//span[1]");

		public By FortNightlyPayment => By.XPath("//div[contains(@id,'paymentBox')]//div[2]//span[1]");

		public By TotalWeeklyRepaymentAmount => By.XPath("//table[contains(@id,'paymentTable')]//span[contains(@class, 'weekly_totalRepayment')]");

		public By TotalFortnightlyRepaymentAmount => By.XPath("//table[contains(@id,'paymentTable')]//span[contains(@class,'fortnight_totalRepayment')]");
	}
}
