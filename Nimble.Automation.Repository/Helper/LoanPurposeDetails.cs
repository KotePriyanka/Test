using Nimble.Automation.Accelerators;
using Nimble.Automation.Repository;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;

using System.Configuration;
using System.Threading;

namespace Nimble.Automation.Repository
{
    public class LoanPurposeDetails : TestEngine
    {
        private ILoanPurposeDetails _loanpurposedetailsLoc;
        private ActionEngine _act = null;
        private IWebDriver _driver = null;
        // public static string MobileMode = ConfigurationManager.AppSettings.Get("MobileMode");

        //public bool GetPlatform(IWebDriver driver)
        //{
        //    bool flag = false;
        //    if (MobileMode == "true")
        //    {
        //        flag = true;
        //    }
        //    else
        //    {
        //        string strval = ((OpenQA.Selenium.Remote.DesiredCapabilities)((OpenQA.Selenium.Remote.RemoteWebDriver)driver).Capabilities).Platform.PlatformType.ToString();
        //        if (strval == "Android" || strval == "Mac")
        //            flag = true;
        //        else flag = false;
        //    }
        //    return flag;
        //}

        public LoanPurposeDetails(IWebDriver driver, string strUserType)
        {
            if (GetPlatform(driver))
                _loanpurposedetailsLoc = (strUserType == "NL") ? (ILoanPurposeDetails)new LoanPurposeDetailsMobileNLLoc() : new LoanPurposeDetailsMobileRLLoc();
            else
                _loanpurposedetailsLoc = (strUserType == "NL") ? (ILoanPurposeDetails)new LoanPurposeDetailsDesktopNLLoc() : new LoanPurposeDetailsDesktopRLLoc();
            _act = new ActionEngine(driver);
            _driver = driver;
        }

        /// <summary>
        /// Selects the loan purpose for returner loaner.
        /// </summary>
        /// <param name="loanpurpose">The loanpurpose.</param>
        public void SelectLoanPurposeRL(string loanpurpose)
        {
            if (GetPlatform(_driver))
            {
                if (loanpurpose.Contains(","))
                {
                    var purpose = loanpurpose.Split(',');
                    foreach (var loan in purpose)
                    {
                        IWebElement selectedPOL = _driver.FindElement(By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div"));
                        string classtextwhenchecked = selectedPOL.GetAttribute("class");
                        if (!classtextwhenchecked.Contains("checked"))
                        {
                            var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div");
                            _act.JSClick(polpurpose, "selectpurpose");
                        }
                    }
                    if (loanpurpose == "Other,Anything else")
                    {
                        _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                    }
                    //_act.click(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
                    _act.waitForVisibilityOfElement(_loanpurposedetailsLoc.DonePurposeLoanDialog, 200);
                    _driver.FindElement(_loanpurposedetailsLoc.DonePurposeLoanDialog).Click();
                }
                else
                {
                    IWebElement selectedPOL = _driver.FindElement(By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose + "')]/preceding-sibling::div"));
                    string classtextwhenchecked = selectedPOL.GetAttribute("class");
                    if (!classtextwhenchecked.Contains("checked"))
                    {
                        var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose + "')]/preceding-sibling::div");
                        _act.JSClick(polpurpose, "selectpurpose");
                    }
                    if (loanpurpose == "Other,Anything else")
                    {
                        _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                    }
                    //_act.click(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
                    _act.waitForVisibilityOfElement(_loanpurposedetailsLoc.DonePurposeLoanDialog, 200);
                    _driver.FindElement(_loanpurposedetailsLoc.DonePurposeLoanDialog).Click();

                }
            }
            else
            {
                if (loanpurpose.Contains(","))
                {
                    var purpose = loanpurpose.Split(',');
                    foreach (var loan in purpose)
                    {
                        // IWebElement selectedPOL = _driver.FindElement(By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div"));
                        // string classtextwhenchecked = selectedPOL.GetAttribute("class");
                        var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div");
                        _act.JSClick(polpurpose, "selectpurpose");
                    }
                    if (loanpurpose == "Other,Anything else")
                    {
                        _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                    }
                    _act.JSClick(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
                    //driver.FindElement(_loanpurposedetailsLoc.DonePurposeLoanDialog).Click();
                }
                else
                {
                    //IWebElement selectedPOL = _driver.FindElement(By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose + "')]/preceding-sibling::div"));
                    //string classtextwhenchecked = selectedPOL.GetAttribute("class");
                    var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose + "')]/preceding-sibling::div");
                    _act.JSClick(polpurpose, "selectpurpose");
                    if (loanpurpose == "Other,Anything else")
                    {
                        _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                    }
                    _act.JSClick(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");

                }
            }
        }

        // without debug button

        //public void SelectLoanPurpose(string loanpurpose)
        //{
        //    _act.waitForVisibilityOfElement(_loanpurposedetailsLoc.PurposeLoanDialog, 5);

        //    if (GetPlatform(_driver))
        //    {
        //        if (loanpurpose.Contains(","))
        //        {
        //            var purpose = loanpurpose.Split(',');
        //            foreach (var loan in purpose)
        //            {
        //                //.//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div
        //                IWebElement selectedPOL = _driver.FindElement(By.XPath(".//div[@class='polContent']/div//label//span[contains(text(),'" + loan + "')]"));
        //                string classtextwhenchecked = selectedPOL.GetAttribute("class");
        //                if (!classtextwhenchecked.Contains("checked"))
        //                {

        //                    var polpurpose = By.XPath(".//div[@class='polContent']/div//label//span[contains(text(),'" + loan + "')]");
        //                    _act.click(polpurpose, "selectpurpose");
        //                    Thread.Sleep(1000);
        //                }
        //            }
        //            _act.click(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
        //        }
        //        else
        //        {
        //            IWebElement selectedPOL = _driver.FindElement(By.XPath(".//div[@class='polContent']/div//label//span[contains(text(),'" + loanpurpose + "')]"));
        //            string classtextwhenchecked = selectedPOL.GetAttribute("class");
        //            if (!classtextwhenchecked.Contains("checked"))
        //            {
        //                var polpurpose = By.XPath(".//div[@class='polContent']/div//label//span[contains(text(),'" + loanpurpose + "')]");
        //                Thread.Sleep(1000);
        //                _act.click(polpurpose, "selectpurpose");
        //                Thread.Sleep(1000);
        //            }
        //            _act.click(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
        //        }
        //    }
        //    else
        //    {
        //        if (loanpurpose.Contains(","))
        //        {
        //            var purpose = loanpurpose.Split(',');
        //            foreach (var loan in purpose)
        //            {
        //                IWebElement selectedPOL = _driver.FindElement(By.XPath(".//div[@class='polContent']/div//label//span[contains(text(),'" + loan + "')]"));
        //                string classtextwhenchecked = selectedPOL.GetAttribute("class");
        //                if (!classtextwhenchecked.Contains("checked"))
        //                {
        //                    var polpurpose = By.XPath(".//div[@class='polContent']/div//label//span[contains(text(),'" + loan + "')]");
        //                    Thread.Sleep(1000);
        //                    _act.click(polpurpose, "selectpurpose");
        //                    Thread.Sleep(1000);
        //                }
        //            }
        //            _act.click(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
        //        }
        //        else
        //        {
        //            IWebElement selectedPOL = _driver.FindElement(By.XPath(".//div[@class='polContent']/div//label//span[contains(text(),'" + loanpurpose + "')]"));
        //            string classtextwhenchecked = selectedPOL.GetAttribute("class");
        //            if (!classtextwhenchecked.Contains("checked"))
        //            {
        //                var polpurpose = By.XPath(".//div[@class='polContent']/div//label//span[contains(text(),'" + loanpurpose + "')]");
        //                Thread.Sleep(1000);
        //                _act.click(polpurpose, "selectpurpose");
        //                Thread.Sleep(1000);
        //            }                  
        //            _act.click(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
        //        }
        //    }
        //}

        /// <summary>
        /// Selects the loan purpose.
        /// </summary>
        /// <param name="loanpurpose">The loanpurpose.</param>
        public void SelectLoanPurpose(string loanpurpose)
        {
            if (GetPlatform(_driver))
            {
                if (loanpurpose.Contains(","))
                {
                    var purpose = loanpurpose.Split(',');

                    foreach (var loan in purpose)
                    {
                        var polpurpose = By.XPath("//div[@class='polContent']/div//span[contains(text(),'" + loan + "')]/parent::span/preceding-sibling::span");
                        _act.JSClick(polpurpose, "selectpurpose");
                    }
                    if (loanpurpose == "Other,Anything else")
                    {
                        _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                    }
                    if (loanpurpose == "Basic living/work expenses,Emergency Repairs")
                    {
                        _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Need to fix new wires");
                    }

                    _act.JSClick(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
                }
                else
                {
                    var polpurpose = By.XPath("//div[@class='polContent']/div//span[contains(text(),'" + loanpurpose + "')]/parent::span/preceding-sibling::span");
                    _act.JSClick(polpurpose, "selectpurpose");
                    if (loanpurpose == "Other,Anything else")
                    {
                        _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                    }
                    _act.JSClick(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
                }
            }
            else
            {
                if (loanpurpose.Contains(","))
                {
                    var purpose = loanpurpose.Split(',');
                    foreach (var loan in purpose)
                    {
                        var polpurpose = By.XPath("//div[@class='polContent']/div//span[contains(text(),'" + loan + "')]/parent::span/preceding-sibling::span");
                        _act.JSClick(polpurpose, "selectpurpose");
                    }
                    if (loanpurpose == "Other,Anything else")
                    {
                        _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                    }
                    _act.JSClick(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
                }
                else
                {
                    var polpurpose = By.XPath("//div[@class='polContent']/div//span[contains(text(),'" + loanpurpose + "')]/parent::span/preceding-sibling::span");
                    _act.JSClick(polpurpose, "selectpurpose");
                    if (loanpurpose == "Other,Anything else")
                    {
                        _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                    }
                    _act.JSClick(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
                }
            }
        }

        /// <summary>
        /// Clicks on Done button in POL Window
        /// </summary>
        public void ClickDoneBtnInPOL()
        {
            _act.JSClick(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
        }
        /// <summary>
        /// Selects the loan purpose mobile.
        /// </summary>
        /// <param name="loanpurpose">The loanpurpose.</param>
        public void SelectLoanPurposeMobile(string loanpurpose)
        {
            if (GetPlatform(_driver))
            {
                if (loanpurpose.Contains(","))
                {
                    var purpose = loanpurpose.Split(',');
                    foreach (var loan in purpose)
                    {
                        IWebElement selectedPOL = _driver.FindElement(By.XPath(".//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div"));
                        string classtextwhenchecked = selectedPOL.GetAttribute("class");
                        if (!classtextwhenchecked.Contains("checked"))
                        {
                            var polpurpose = By.XPath(".//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div");
                            _act.click(polpurpose, "selectpurpose");
                        }
                    }
                    if (loanpurpose == "Other,Anything else")
                    {
                        _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                    }
                    _act.click(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
                }
                else
                {
                    IWebElement selectedPOL = _driver.FindElement(By.XPath(".//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose + "')]/preceding-sibling::div"));
                    string classtextwhenchecked = selectedPOL.GetAttribute("class");
                    if (!classtextwhenchecked.Contains("checked"))
                    {
                        var polpurpose = By.XPath(".//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose + "')]/preceding-sibling::div");
                        _act.click(polpurpose, "selectpurpose");
                    }
                    if (loanpurpose == "Other,Anything else")
                    {
                        _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                    }
                    _act.click(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
                }
            }
            else
            {
                if (loanpurpose.Contains(","))
                {
                    var purpose = loanpurpose.Split(',');
                    foreach (var loan in purpose)
                    {
                        IWebElement selectedPOL = _driver.FindElement(By.XPath(".//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div"));
                        string classtextwhenchecked = selectedPOL.GetAttribute("class");
                        if (!classtextwhenchecked.Contains("checked"))
                        {
                            var polpurpose = By.XPath(".//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div");
                            Thread.Sleep(3000);
                            _act.click(polpurpose, "selectpurpose");
                            Thread.Sleep(3000);
                        }
                    }
                    if (loanpurpose == "Other,Anything else")
                    {
                        _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                    }
                    _act.click(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
                }
                else
                {
                    IWebElement selectedPOL = _driver.FindElement(By.XPath(".//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose + "')]/preceding-sibling::div"));
                    string classtextwhenchecked = selectedPOL.GetAttribute("class");
                    if (!classtextwhenchecked.Contains("checked"))
                    {
                        var polpurpose = By.XPath(".//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose + "')]/preceding-sibling::div");
                        _act.click(polpurpose, "selectpurpose");
                    }
                    if (loanpurpose == "Other,Anything else")
                    {
                        _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                    }
                    _act.click(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
                }
            }
        }

        /// <summary>
        /// Sets the slider loan amount.
        /// </summary>
        /// <param name="sliderHandleXpath">The slider handle xpath.</param>
        /// <param name="sliderTrackXpath">The slider track xpath.</param>
        /// <param name="LoanAmount">The loan amount.</param>
        public void SetSliderLoanAmount(By sliderHandleXpath, By sliderTrackXpath, int LoanAmount)
        {
            _act.waitForVisibilityOfElement(sliderHandleXpath, 60);
            var sliderHandle = _driver.FindElement(sliderHandleXpath);
            var sliderTrack = _driver.FindElement(sliderTrackXpath);
            var width = int.Parse(sliderTrack.GetCssValue("width").Replace("px", ""));
            var dx = GetSliderPosition(LoanAmount);
            new Actions(_driver)
                        .DragAndDropToOffset(sliderHandle, dx, 0)
                        .Build()
                        .Perform();
        }

        /// <summary>
        /// Gets the slider position.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        public int GetSliderPosition(int amount)
        {
            int pt = -10;
            int diffamt = 1600 - amount;
            if (diffamt >= 50)  // SACC 
            {
                int ab = (diffamt / 50);
                int xt = (ab * 5) - 5;
                int yt = xt + ab + 10;
                pt = yt - (yt * 2);
            }
            else if (diffamt <= -50)  //MACC
            {
                int ab = (diffamt / 50);
                int xt = (ab * 5) + 10;
                int yt = xt + ab;
                pt = yt * (-1);
            }
            return pt;
        }

        /// <summary>
        /// Selects the loan purpose other.
        /// </summary>
        /// <param name="loanpurpose">The loanpurpose.</param>
        public void SelectLoanPurposeOther(string loanpurpose)
        {
            _act.waitForVisibilityOfElement(_loanpurposedetailsLoc.PurposeLoanDialog, 60);

            if (GetPlatform(_driver))
            {
                if (loanpurpose.Contains(","))
                {
                    var purpose = loanpurpose.Split(',');
                    foreach (var loan in purpose)
                    {
                        IWebElement selectedPOL = _driver.FindElement(By.XPath(".//div[@class='polContent']/div//label//span[contains(text(),'" + loan + "')]"));
                        string classtextwhenchecked = selectedPOL.GetAttribute("class");
                        if (!classtextwhenchecked.Contains("checked"))
                        {
                            var polpurpose = By.XPath(".//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div");
                            _act.click(polpurpose, "selectpurpose");
                        }
                    }
                    _act.click(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
                }
                else
                {
                    IWebElement selectedPOL = _driver.FindElement(By.XPath(".//div[@class='polContent']/div//label//span[contains(text(),'" + loanpurpose + "')]"));
                    string classtextwhenchecked = selectedPOL.GetAttribute("class");
                    if (!classtextwhenchecked.Contains("checked"))
                    {
                        var polpurpose = By.XPath(".//div[@class='polContent']/div//label//span[contains(text(),'" + loanpurpose + "')]");
                        _act.click(polpurpose, "selectpurpose");
                    }
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                    _act.click(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
                }
            }
            else
            {
                if (loanpurpose.Contains(","))
                {
                    var purpose = loanpurpose.Split(',');
                    foreach (var loan in purpose)
                    {
                        IWebElement selectedPOL = _driver.FindElement(By.XPath(".//div[@class='polContent']/div//label//span[contains(text(),'" + loan + "')]"));
                        string classtextwhenchecked = selectedPOL.GetAttribute("class");
                        if (!classtextwhenchecked.Contains("checked"))
                        {
                            var polpurpose = By.XPath(".//div[@class='polContent']/div//label//span[contains(text(),'" + loan + "')]");
                            _act.click(polpurpose, "selectpurpose");
                        }
                    }
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                    _act.click(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
                }
                else
                {
                    IWebElement selectedPOL = _driver.FindElement(By.XPath(".//div[@class='polContent']/div//label//span[contains(text(),'" + loanpurpose + "')]"));
                    string classtextwhenchecked = selectedPOL.GetAttribute("class");
                    if (!classtextwhenchecked.Contains("checked"))
                    {
                        var polpurpose = By.XPath(".//div[@class='polContent']/div//label//span[contains(text(),'" + loanpurpose + "')]");
                        _act.click(polpurpose, "selectpurpose");
                    }
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                    _act.click(_loanpurposedetailsLoc.DonePurposeLoanDialog, "BtnPurposeLoan");
                }
            }
        }

        /// <summary>
        /// Click and then select first pol.
        /// </summary>
        public void ClickandSelectFirstPOL()
        {
            _act.waitForVisibilityOfElement(_loanpurposedetailsLoc.SelectFirstPurpose, 60);
            _act.click(_loanpurposedetailsLoc.SelectFirstPurpose, "SelectfirstPurpose");
        }

        /// <summary>
        /// Selects the loan value.
        /// </summary>
        /// <param name="requestedAmount">The requested amount.</param>
        public void SelectLoanValue(int requestedAmount)
        {
            if (GetPlatform(_driver))
            {
                int amtdiff = 1600 - requestedAmount;
                int clicker = amtdiff / 50;
                if (amtdiff >= 50)
                {
                    for (int i = 1; i <= clicker; i++)
                    {
                        _driver.FindElement(_loanpurposedetailsLoc.SliderMinBtn).Click();
                    }
                }
                else if (amtdiff <= 50)
                {
                    clicker = clicker * (-1);
                    for (int i = 1; i <= clicker; i++)
                    {
                        _driver.FindElement(_loanpurposedetailsLoc.SliderMaxBtn).Click();
                    }
                }
            }
            else
            {
                SetSliderLoanAmount(_loanpurposedetailsLoc.SliderHandle, _loanpurposedetailsLoc.SliderTrack, requestedAmount);
            }
        }

        public void SelectLoanValueDNQSuccess(int requestedAmount)
        {
            if (GetPlatform(_driver))
            {
                int amtdiff = 1600 - requestedAmount;
                int clicker = amtdiff / 50;
                if (amtdiff >= 50)
                {
                    for (int i = 1; i <= clicker; i++)
                    {
                        _driver.FindElement(_loanpurposedetailsLoc.SliderMinBtn).Click();
                    }
                }
                else if (amtdiff <= 50)
                {
                    clicker = clicker * (-1);
                    for (int i = 1; i <= clicker; i++)
                    {

                        _driver.FindElement(_loanpurposedetailsLoc.SliderMaxBtn).Click();
                    }
                }
            }
            else
            {
                SetSliderLoanAmount(By.XPath(".//div[@class='display-value white text-center bold600']/span"), By.XPath(".//div[@id='apply-slider']"), requestedAmount);
            }
        }

        //public void SelectLoanValue(int requestedAmount)
        //{
        //    IWebElement sliderEle = _driver.FindElement(_loanpurposedetailsLoc.Slider);
        //    _act.click(_loanpurposedetailsLoc.Slider, "slider");
        //    if (requestedAmount > 1600 && requestedAmount < 5000)
        //    {
        //        int actualvalue = requestedAmount - 1600;
        //        int rightclicks = (actualvalue) / 50;
        //        for (int i = 1; i <= rightclicks; i++)
        //        {
        //            if (GetPlatform(_driver))
        //            {
        //                _act.click(_loanpurposedetailsLoc.SliderMaxBtn, "slidermaxbtn");
        //            }
        //            else
        //            {
        //                sliderEle.SendKeys(Keys.ArrowRight);
        //            }
        //        }
        //    }
        //    else if (requestedAmount < 1600 && requestedAmount > 300)
        //    {
        //        int actualvalue = 1600 - requestedAmount;
        //        int leftclicks = (actualvalue) / 50;
        //        for (int i = 1; i <= leftclicks; i++)
        //        {
        //            if (GetPlatform(_driver))
        //            {
        //                _act.click(_loanpurposedetailsLoc.SliderMinBtn, "sliderMinbtn");
        //            }
        //            else
        //            {
        //                sliderEle.SendKeys(Keys.ArrowLeft);
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Selects the loan value for Returner loaner.
        /// </summary>
        /// <param name="requestedAmount">The requested amount.</param>
        public void SelectLoanValueRL(int requestedAmount)
        {
            _act.waitForVisibilityOfElement(_loanpurposedetailsLoc.Slider, 60);
            IWebElement sliderEle = _driver.FindElement(_loanpurposedetailsLoc.Slider);
            _act.click(_loanpurposedetailsLoc.Slider, "slider");

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



        /// <summary>
        /// Sliders the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        public void Slider(int request)
        {
            IWebElement slider = _driver.FindElement(_loanpurposedetailsLoc.Slider);
            //    int width = slider.get().getWidth();
            int actual = 1600 - request - 2200;
            int move = actual / 50;
            Actions SliderActions = new Actions(_driver);
            SliderActions.ClickAndHold(slider);
            SliderActions.MoveByOffset(move, 0).Build().Perform();
        }

        /// <summary>
        /// Enters the first pol amount text.
        /// </summary>
        /// <param name="firstPolAmount">The first pol amount.</param>
        public void EnterFirstPOLAmountTxt(string firstPolAmount)
        {
            _act.EnterText(_loanpurposedetailsLoc.FirstLoanAmount, firstPolAmount);
        }

        /// <summary>
        /// Enters the second pol amount text.
        /// </summary>
        /// <param name="secondPolAmount">The second pol amount.</param>
        public void EnterSecondPOLAmountTxt(string secondPolAmount)
        {
            _act.EnterText(_loanpurposedetailsLoc.SecondLoanAmount, secondPolAmount);
        }

        public void EnterFiveLoanAmounts(string firstPolAmount, string secondPolAmount, string thirdPOL, string fourthPOL, string fifthPOL)
        {
            _act.EnterText(_loanpurposedetailsLoc.FifthLoanAmount, fifthPOL);

            _act.EnterText(_loanpurposedetailsLoc.FourthLoanAmount, fourthPOL);

            _act.EnterText(_loanpurposedetailsLoc.ThirdLoanAmount, thirdPOL);

            _act.EnterText(_loanpurposedetailsLoc.SecondLoanAmount, secondPolAmount);

            _act.EnterText(_loanpurposedetailsLoc.FirstLoanAmount, firstPolAmount);
        }

        public void EnterFivePOLRL(string firstpurpose, string secondpurpose, string thirdpurpose, string fourthpurpose, string fifthpurpose, int loanamout)
        {
            //Click on Select First POL Lst
            ClickandSelectFirstPOL();

            // Select Purpose of loan
            SelectLoanPurposeRL(firstpurpose);

            // Enter FirstPOLLoan Amount
            // EnterFirstPOLAmountTxt((loanamout / 5).ToString());

            //Select click on second purpose of loan
            ClickSelectSecondPurposeBtn();

            // Select  Second Purpose of loan
            SelectLoanPurposeRL(secondpurpose);

            // Enter Second POL Loan Amount
            //  EnterSecondPOLAmountTxt((loanamout / 5).ToString());


            //click Add another button
            ClickAddPurposeBtn();

            // Select  THIRD Purpose of loan
            SelectLoanPurposeRL(thirdpurpose);

            // Enter THIRD POL Loan Amount
            //  ThirdPOLAmt((loanamout / 5).ToString());

            //click Add another button
            ClickAddPurposeBtn();

            // Select  fourth Purpose of loan
            SelectLoanPurposeRL(fourthpurpose);

            // Enter fourth POL Loan Amount
            //  FourthPOLAmt((loanamout / 5).ToString());

            //click Add another button
            ClickAddPurposeBtn();

            // Select  fifth Purpose of loan
            SelectLoanPurposeRL(fifthpurpose);

            // Enter fifth POL Loan Amount
            //FifthPOLAmt((loanamout / 5).ToString());
            string Loanamt = (loanamout / 5).ToString();
            EnterFiveLoanAmounts(Loanamt, Loanamt, Loanamt, Loanamt, Loanamt);
        }

        /// <summary>
        /// Enters the third pol amount text.
        /// </summary>
        /// <param name="thirdPolAmount">The third pol amount.</param>


        /// <summary>
        /// Clicks Cancels button on pol page.
        /// </summary>
        public void CancelPOL()
        {
            _act.click(_loanpurposedetailsLoc.CancelPOL, "CancelPOL");

        }

        /// <summary>
        /// Clicks the select first purpose button.
        /// </summary>
        public void ClickSelectFirstPurposeBtn()
        {
            _act.click(_loanpurposedetailsLoc.SelectFirstPurpose, "SelectFirstPurpose");
        }

        /// <summary>
        /// Clicks the select second purpose button.
        /// </summary>
        public void ClickSelectSecondPurposeBtn()
        {
            _act.click(_loanpurposedetailsLoc.SelectSecondPurpose, "SelectSecondPurpose");
        }

        public void ClickAddPurposeBtn()
        {
            _act.click(_loanpurposedetailsLoc.BtnAddAnotherpurpose, "BtnAddAnotherpurpose");
        }

        /// <summary>
        /// Clicks the add another purpose button.
        /// </summary>
        public void ClickAddAnotherPurposeBtn()
        {
            _act.click(_loanpurposedetailsLoc.BtnAddAnotherpurpose, "AddAnotherPOL");
        }

        /// <summary>
        /// Enters the more information.
        /// </summary>
        /// <param name="moreInfoTxt">The more information text.</param>
        public void EnterMoreInfo(string moreInfoTxt)
        {
            if (_act.isElementPresent(_loanpurposedetailsLoc.MoreInformationTextArea))
            {
                _act.waitForVisibilityOfElement(_loanpurposedetailsLoc.MoreInformationTextArea, 60);
                _act.EnterText(_loanpurposedetailsLoc.MoreInformationTextArea, moreInfoTxt);
                // click on MoreInfo continue Button
                _act.click(_loanpurposedetailsLoc.MoreInformationContinue, "MoreInfoContinue");
            }
        }

        /// <summary>
        /// Clicks the loan pol continue button.
        /// </summary>
        public void ClickLoanPOLContinueBtn()
        {
            if (_act.isElementPresent(_loanpurposedetailsLoc.ContinueButton))
            {
                _act.waitForVisibilityOfElement(_loanpurposedetailsLoc.ContinueButton, 60);
                _act.click(_loanpurposedetailsLoc.ContinueButton, "LoanPOlContinueBtn");
            }
        }

        /// <summary>
        /// Clicks the loan pol continue button for returner loaner.
        /// </summary>
        public void ClickLoanPOLContinueBtnRL()
        {
            _act.waitForVisibilityOfElement(_loanpurposedetailsLoc.ContinueButtonRL, 60);
            _act.click(_loanpurposedetailsLoc.ContinueButtonRL, "LoanPOlContinueBtn");
        }

        /// <summary>
        /// Clicks the edit my details button.
        /// </summary>
        public void ClickEditMyDetailsBtn()
        {
            _act.waitForVisibilityOfElement(_loanpurposedetailsLoc.EditMyDetailsBtn, 60);
            _act.click(_loanpurposedetailsLoc.EditMyDetailsBtn, "EditMyDetailsBtn");
        }

        /// <summary>
        /// Clicks the done button.
        /// </summary>
        public void ClickPersonalDetailsEditDoneBtn()
        {
            _act.waitForVisibilityOfElement(_loanpurposedetailsLoc.PersonalDetailsEditDoneBtn, 60);
            _act.click(_loanpurposedetailsLoc.PersonalDetailsEditDoneBtn, "PersonalDetailsEditDoneBtn");
        }

        /// <summary>
        /// Clicks the Back button.
        /// </summary>
        public void ClickOnBackBtn()
        {
            _act.waitForVisibilityOfElement(_loanpurposedetailsLoc.BackBtn, 60);
            _act.click(_loanpurposedetailsLoc.BackBtn, "BackBtn");
        }

        /// <summary>
        /// Clicks the personal details pane.
        /// </summary>
        public void ClickPersonalDetailsPane()
        {
            _act.waitForVisibilityOfElement(_loanpurposedetailsLoc.PersonalDetailsHeader, 60);
            _act.click(_loanpurposedetailsLoc.PersonalDetailsHeader, "PersonalDetailsHeader");
        }

        /// <summary>
        /// Retrives the first name.
        /// </summary>
        /// <returns></returns>
        public string RetriveFirstName()
        {
            _act.waitForVisibilityOfElement(_loanpurposedetailsLoc.FirstName, 60);
            return _act.getValue(_loanpurposedetailsLoc.FirstName, "FirstName");
        }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <returns></returns>
        public string GetFirstName()
        {
            ClickEditMyDetailsBtn();

            ClickPersonalDetailsPane();

            string firstName = RetriveFirstName();
            return firstName;

            ClickPersonalDetailsEditDoneBtn();

            ClickOnBackBtn();
        }

        /// <summary>
        /// Mores the information.
        /// </summary>
        public void MoreInformation()
        {
            if (_act.isElementPresent(_loanpurposedetailsLoc.MoreInformationTextArea) &&
                _act.isElementDisplayed(_driver.FindElement(_loanpurposedetailsLoc.MoreInformationTextArea)))
            {
                _act.EnterText(_loanpurposedetailsLoc.MoreInformationTextArea, "I Need it to buy necessary things");
                _act.click(_loanpurposedetailsLoc.MoreInformationContinue, "MoreInformationContinue");
                Thread.Sleep(3000);
            }
        }

        /// <summary>
        /// Anything else text box.
        /// </summary>
        public void AnythingelseTextBox()
        {
            _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
        }

        ///------------------------Temporary methos to handle React pages-----------------///

        public void RequestLoanAmount(int requestedAmount, string loanpurpose)
        {
            //select loan value from slider
            _act.waitForVisibilityOfElement(By.XPath(".//*[@class='ui-slider-handle ui-state-default ui-corner-all']"), 20);
            IWebElement sliderEle = _driver.FindElement(By.XPath(".//*[@class='ui-slider-handle ui-state-default ui-corner-all']"));
            _act.click(By.XPath(".//*[@class='ui-slider-handle ui-state-default ui-corner-all']"), "click");

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

        public void RequestLoanAmount(string loanpurpose)
        {
            ////select loan value from slider
            //_act.waitForVisibilityOfElement(By.XPath(".//*[@class='ui-slider-handle ui-state-default ui-corner-all']"), 20);
            //IWebElement sliderEle = _driver.FindElement(By.XPath(".//*[@class='ui-slider-handle ui-state-default ui-corner-all']"));
            //_act.click(By.XPath(".//*[@class='ui-slider-handle ui-state-default ui-corner-all']"), "click");
            //if (requestedAmount > 1600 && requestedAmount <= 5000)
            //{
            //    int actualvalue = requestedAmount - 1600;
            //    int rightclicks = (actualvalue) / 50;
            //    for (int i = 1; i <= rightclicks; i++)
            //        sliderEle.SendKeys(Keys.ArrowRight);
            //}
            //else if (requestedAmount < 1600 && requestedAmount >= 300)
            //{
            //    int actualvalue = 1600 - requestedAmount;
            //    int leftclicks = (actualvalue) / 50;
            //    for (int i = 1; i <= leftclicks; i++)
            //        sliderEle.SendKeys(Keys.ArrowLeft);
            //}

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
                System.Threading.Thread.Sleep(1000);

            }

            //// enter first POL loan amount
            //_act.EnterText(By.XPath("(.//*[@type='number'])[last()-2]"), requestedAmount.ToString());

            ////click on continue button
            //_act.click(By.XPath(".//*[@id='btnPOLsCompleted']"), "continue");

            //if (loanpurpose.Contains("Utility bills") &&
            //    _act.isElementPresent(By.XPath("//span[text()='More information']")))
            //{
            //    _act.waitForVisibilityOfElement(By.XPath("//span[text()='More information']"), 60);
            //    _act.EnterText(By.XPath("//*[@name='poloverallmoreinfo']"), "hi hello");
            //    // click on MoreInfo continue Button
            //    _act.click(By.XPath("//*[@class='button-continue button sml button']"), "MoreInfoContinue");
            //}
        }

        /// <summary>
        /// Requests the loan amount from mobile.
        /// </summary>
        /// <param name="requestedAmount">The requested amount.</param>
        /// <param name="loanpurpose">The loanpurpose.</param>
        public void RequestLoanAmountMobile(int requestedAmount, string loanpurpose)
        {
            //select loan value from slider
            _act.waitForVisibilityOfElement(By.XPath(".//*[@class='ui-slider-handle ui-btn ui-shadow']"), 20);
            IWebElement sliderEle = _driver.FindElement(By.XPath(".//*[@class='ui-slider-handle ui-btn ui-shadow']"));
            _act.click(By.XPath(".//*[@class='ui-slider-handle ui-btn ui-shadow']"), "click");

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
            Thread.Sleep(1000);//added as it is taking time to click on purpose of loan
            //Click on purpse of loan
           _act.JSClick(By.XPath("(//span[text()='- select purpose -'])[last()-2]"), "first purpose");

            // select first POL from popup
            if (loanpurpose.Contains(","))
            {
                var purpose = loanpurpose.Split(',');
                foreach (var loan in purpose)
                {
                    IWebElement selectedPOL = _driver.FindElement(By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div"));
                    string classtextwhenchecked = selectedPOL.GetAttribute("class");
                    if (!classtextwhenchecked.Contains("checked"))
                    {
                        var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div");
                        // Thread.Sleep(4000);

                        _act.JSClick(polpurpose, "selectpurpose");
                        // Thread.Sleep(4000);
                    }
                }
                if (loanpurpose == "Other,Anything else")
                {
                    _act.EnterText(By.XPath("//*[@class='polMoreDetails']"), "Secret it is");
                }
                Thread.Sleep(1000);
                _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");//added as it is taking time to click on done button

            }
            else
            {
                IWebElement selectedPol = _driver.FindElement(By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose + "')]/preceding-sibling::div"));
                string classtextwhenchecked = selectedPol.GetAttribute("class");
                if (!classtextwhenchecked.Contains("checked"))
                {
                    var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose + "')]/preceding-sibling::div");
                    //   Thread.Sleep(3000);
                    _act.JSClick(polpurpose, "selectpurpose");
                    //   Thread.Sleep(3000);
                }
                if (loanpurpose == "Other,Anything else")
                {
                    _act.EnterText(By.XPath("//*[@class='polMoreDetails']"), "Secret it is");
                }
                //  _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
                _act.click(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
                ClosePOLDialog();
            }
            // enter first POL loan amount
            _act.EnterText(By.XPath("(.//*[@type='number'])[last()-3]"), requestedAmount.ToString());

            //click on continue button
            _act.click(By.XPath(".//*[@id='next1']"), "continue");

            if (loanpurpose.Contains("Utility bills") &&
                _act.isElementPresent(By.XPath("//span[text()='More information']")))
            {
                _act.waitForVisibilityOfElement(By.XPath("//span[text()='More information']"), 60);
                _act.EnterText(By.XPath("//*[@name='poloverallmoreinfo']"), "hi hello");
                // click on MoreInfo continue Button
                _act.click(By.XPath("//*[@class='button-continue button sml button']"), "MoreInfoContinue");
            }
        }

        /// <summary>
        /// Requests the loan amount with multiple pol.
        /// </summary>
        /// <param name="requestedAmount">The requested amount.</param>
        /// <param name="loanpurpose1">The loanpurpose1.</param>
        /// <param name="loanpurpose2">The loanpurpose2.</param>
        public void RequestLoanAmountMultiplePOL(int requestedAmount, string loanpurpose1, string loanpurpose2)
        {
            //select loan value from slider
            _act.waitForVisibilityOfElement(By.XPath(".//*[@class='ui-slider-handle ui-state-default ui-corner-all']"), 20);
            IWebElement sliderEle = _driver.FindElement(By.XPath(".//*[@class='ui-slider-handle ui-state-default ui-corner-all']"));
            _act.click(By.XPath(".//*[@class='ui-slider-handle ui-state-default ui-corner-all']"), "click");
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
            // click on first POL dropdown
            _act.click(By.XPath("(//span[text()='- select purpose -'])[last()-2]"), "first purpose");

            // select first POL from popup
            if (loanpurpose1.Contains(","))
            {
                var purpose = loanpurpose1.Split(',');
                foreach (var loan in purpose)
                {
                    IWebElement selectedPOL = _driver.FindElement(By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div"));
                    string classtextwhenchecked = selectedPOL.GetAttribute("class");
                    //  if (!classtextwhenchecked.Contains("checked"))
                    //  {
                    var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div");
                    //    Thread.Sleep(3000);
                    _act.JSClick(polpurpose, "selectpurpose");
                    //   Thread.Sleep(3000);
                    // }
                }
                if (loanpurpose1 == "Other,Anything else")
                {
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                }
                _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
            }
            else
            {
                IWebElement selectedPol = _driver.FindElement(By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose1 + "')]/preceding-sibling::div"));
                string classtextwhenchecked = selectedPol.GetAttribute("class");
                //  if (!classtextwhenchecked.Contains("checked"))
                //  {
                var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose1 + "')]/preceding-sibling::div");
                //   Thread.Sleep(3000);
                _act.JSClick(polpurpose, "selectpurpose");
                // Thread.Sleep(3000);
                //  }
                //  Thread.Sleep(3000);
                if (loanpurpose1 == "Other,Anything else")
                {
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                }
                _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
            }

            // enter first POL loan amount
            _act.EnterText(By.XPath("(.//*[@class='font-16 inline-block border radius-5'])[last()-2]"), (requestedAmount / 2).ToString());

            //click second POL dropdown
            _act.click(By.XPath(".//*[@id='selPolSections-container']/div[2]//span[contains(text(),'- select purpose -')]"), "second purpose");

            // select second POL from popup
            if (loanpurpose2.Contains(","))
            {
                var purpose = loanpurpose2.Split(',');
                foreach (var loan in purpose)
                {
                    IWebElement selectedPol = _driver.FindElement(By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div"));
                    string classtextwhenchecked = selectedPol.GetAttribute("class");
                    //  if (!classtextwhenchecked.Contains("checked"))
                    //  {
                    var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div");
                    //    Thread.Sleep(3000);
                    _act.JSClick(polpurpose, "selectpurpose");
                    //   Thread.Sleep(3000);
                    // }
                }
                if (loanpurpose2 == "Other,Anything else")
                {
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                }
                _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
            }
            else
            {
                IWebElement selectedPOL = _driver.FindElement(By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose2 + "')]/preceding-sibling::div"));
                string classtextwhenchecked = selectedPOL.GetAttribute("class");
                //  if (!classtextwhenchecked.Contains("checked"))
                //  {
                var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose2 + "')]/preceding-sibling::div");
                //   Thread.Sleep(3000);
                _act.JSClick(polpurpose, "selectpurpose");
                // Thread.Sleep(3000);
                //  }
                //  Thread.Sleep(3000);
                if (loanpurpose2 == "Other,Anything else")
                {
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                }
                _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
            }

            // enter second POL loan amount
            _act.EnterText(By.XPath(".//*[@id='selPolSections-container']/div[2]//div[@class='selPolSection-amountContainer']/input"), (requestedAmount / 2).ToString());

            //click on continue button
            _act.click(By.XPath(".//*[@id='btnPOLsCompleted']"), "continue");

            if (loanpurpose2.Contains("Utility bills") &&
                _act.isElementPresent(By.XPath("//span[text()='More information']")))
            {
                _act.waitForVisibilityOfElement(By.XPath("//span[text()='More information']"), 60);
                _act.EnterText(By.XPath("//*[@name='poloverallmoreinfo']"), "hi hello");
                // click on MoreInfo continue Button
                _act.click(By.XPath("//*[@class='button-continue button sml button']"), "MoreInfoContinue");
            }
        }

        /// <summary>
        /// Requests the loan amount with multiple pol from mobile.
        /// </summary>
        /// <param name="requestedAmount">The requested amount.</param>
        /// <param name="loanpurpose1">The loanpurpose1.</param>
        /// <param name="loanpurpose2">The loanpurpose2.</param>
        public void RequestLoanAmountMultiplePOLMobile(int requestedAmount, string loanpurpose1, string loanpurpose2)
        {
            //select loan value from slider
            _act.waitForVisibilityOfElement(By.XPath(".//*[@class='ui-slider-handle ui-btn ui-shadow']"), 20);
            IWebElement sliderEle = _driver.FindElement(By.XPath(".//*[@class='ui-slider-handle ui-btn ui-shadow']"));
            _act.click(By.XPath(".//*[@class='ui-slider-handle ui-btn ui-shadow']"), "click");
            if (requestedAmount > 1600 && requestedAmount < 5000)
            {
                int actualvalue = requestedAmount - 1600;
                int rightclicks = (actualvalue) / 50;
                for (int i = 1; i <= rightclicks; i++)
                    _driver.FindElement(_loanpurposedetailsLoc.SliderMaxBtn).Click();
            }
            else if (requestedAmount < 1600 && requestedAmount > 300)
            {
                int actualvalue = 1600 - requestedAmount;
                int leftclicks = (actualvalue) / 50;
                for (int i = 1; i <= leftclicks; i++)
                    _driver.FindElement(_loanpurposedetailsLoc.SliderMinBtn).Click();
            }
            // click on first POL dropdown
            _act.click(By.XPath("(//span[text()='- select purpose -'])[last()-2]"), "first purpose");

            // select first POL from popup
            if (loanpurpose1.Contains(","))
            {
                var purpose = loanpurpose1.Split(',');
                foreach (var loan in purpose)
                {
                    IWebElement selectedPOL = _driver.FindElement(By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div"));
                    string classtextwhenchecked = selectedPOL.GetAttribute("class");
                    if (!classtextwhenchecked.Contains("checked"))
                    {
                        var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div");
                        // Thread.Sleep(4000);

                        _act.JSClick(polpurpose, "selectpurpose");
                        // Thread.Sleep(4000);
                    }
                }
                if (loanpurpose1 == "Other,Anything else")
                {
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                }
                _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
            }
            else
            {
                IWebElement selectedPOL = _driver.FindElement(By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose1 + "')]/preceding-sibling::div"));
                string classtextwhenchecked = selectedPOL.GetAttribute("class");
                if (!classtextwhenchecked.Contains("checked"))
                {
                    var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose1 + "')]/preceding-sibling::div");
                    //   Thread.Sleep(3000);
                    _act.JSClick(polpurpose, "selectpurpose");
                    //   Thread.Sleep(3000);
                }
                if (loanpurpose1 == "Other,Anything else")
                {
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                }
                _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
            }

            // enter first POL loan amount
            _act.EnterText(By.XPath("(.//*[@type='number'])[last()-3]"), (requestedAmount / 2).ToString());

            // click on second POL dropdown
            _act.click(By.XPath("(//span[text()='- select purpose -'])[last()-1]"), "second purpose");

            // select second POL from popup
            if (loanpurpose2.Contains(","))
            {
                var purpose = loanpurpose2.Split(',');
                foreach (var loan in purpose)
                {
                    IWebElement selectedPOL = _driver.FindElement(By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div"));
                    string classtextwhenchecked = selectedPOL.GetAttribute("class");
                    if (!classtextwhenchecked.Contains("checked"))
                    {
                        var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div");
                        // Thread.Sleep(4000);

                        _act.JSClick(polpurpose, "selectpurpose");
                        // Thread.Sleep(4000);
                    }
                }
                if (loanpurpose2 == "Other,Anything else")
                {
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                }
                _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
            }
            else
            {
                IWebElement selectedPOL = _driver.FindElement(By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose2 + "')]/preceding-sibling::div"));
                string classtextwhenchecked = selectedPOL.GetAttribute("class");
                if (!classtextwhenchecked.Contains("checked"))
                {
                    var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose2 + "')]/preceding-sibling::div");
                    //   Thread.Sleep(3000);
                    _act.JSClick(polpurpose, "selectpurpose");
                    //   Thread.Sleep(3000);
                }
                if (loanpurpose2 == "Other,Anything else")
                {
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                }
                _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
            }

            // enter second POL loan amount
            _act.EnterText(By.XPath("(.//*[@type='number'])[last()-2]"), (requestedAmount / 2).ToString());

            //click on continue button
            _act.click(By.XPath(".//*[@id='next1']"), "continue");
        }

        public void RequestLoanAmountWith5POL(int requestedAmount, string loanpurpose1, string loanpurpose2, string loanpurpose3, string loanpurpose4, string loanpurpose5)
        {
            //select loan value from slider
            _act.waitForVisibilityOfElement(By.XPath(".//*[@class='ui-slider-handle ui-state-default ui-corner-all']"), 20);
            IWebElement sliderEle = _driver.FindElement(By.XPath(".//*[@class='ui-slider-handle ui-state-default ui-corner-all']"));
            _act.click(By.XPath(".//*[@class='ui-slider-handle ui-state-default ui-corner-all']"), "click");
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

            // click on first POL dropdown
            _act.click(By.XPath("(//span[text()='- select purpose -'])[last()-2]"), "first purpose");

            // select first POL from popup
            if (loanpurpose1.Contains(","))
            {
                var purpose = loanpurpose1.Split(',');
                foreach (var loan in purpose)
                {
                    var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div");
                    _act.JSClick(polpurpose, "selectpurpose");

                }
                if (loanpurpose1 == "Other,Anything else")
                {
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                }
                _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
            }
            else
            {

                var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose1 + "')]/preceding-sibling::div");
                _act.JSClick(polpurpose, "selectpurpose");

                if (loanpurpose1 == "Other,Anything else")
                {
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                }
                _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
            }

            // enter first POL loan amount
            _act.EnterText(By.XPath("(.//*[@class='font-16 inline-block border radius-5'])[last()-2]"), (requestedAmount / 5).ToString());

            //click second POL dropdown
            _act.click(By.XPath(".//*[@id='selPolSections-container']/div[2]//span[contains(text(),'- select purpose -')]"), "second purpose");

            // select second POL from popup
            if (loanpurpose2.Contains(","))
            {
                var purpose = loanpurpose2.Split(',');
                foreach (var loan in purpose)
                {
                    var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div");
                    _act.JSClick(polpurpose, "selectpurpose");
                }
                if (loanpurpose2 == "Other,Anything else")
                {
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                }
                _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
            }
            else
            {
                var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose2 + "')]/preceding-sibling::div");
                _act.JSClick(polpurpose, "selectpurpose");

                if (loanpurpose2 == "Other,Anything else")
                {
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                }
                _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
            }

            // enter second POL loan amount
            _act.EnterText(By.XPath(".//*[@id='selPolSections-container']/div[2]//div[@class='selPolSection-amountContainer']/input"), (requestedAmount / 5).ToString());

            //click on add another POL button
            _act.click(By.XPath(".//*[@id='addAnotherPol']/button"), "add another POL");

            // select third POL from popup
            if (loanpurpose3.Contains(","))
            {
                var purpose = loanpurpose3.Split(',');
                foreach (var loan in purpose)
                {
                    var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div");
                    _act.JSClick(polpurpose, "selectpurpose");
                }
                if (loanpurpose3 == "Other,Anything else")
                {
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                }
                _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
            }
            else
            {
                var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose3 + "')]/preceding-sibling::div");
                _act.JSClick(polpurpose, "selectpurpose");

                if (loanpurpose3 == "Other,Anything else")
                {
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                }
                _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
            }

            // enter third POL loan amount
            _act.EnterText(By.XPath(".//*[@id='selPolSections-container']/div[3]//div[@class='selPolSection-amountContainer']/input"), (requestedAmount / 5).ToString());

            //click on add another POL button
            _act.click(By.XPath(".//*[@id='addAnotherPol']/button"), "add another POL");

            /////////////////

            // select fourth POL from popup
            if (loanpurpose4.Contains(","))
            {
                var purpose = loanpurpose4.Split(',');
                foreach (var loan in purpose)
                {
                    var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div");
                    _act.JSClick(polpurpose, "selectpurpose");
                }
                if (loanpurpose4 == "Other,Anything else")
                {
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                }
                _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
            }
            else
            {
                var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose4 + "')]/preceding-sibling::div");
                _act.JSClick(polpurpose, "selectpurpose");

                if (loanpurpose4 == "Other,Anything else")
                {
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                }
                _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
            }

            // enter fourth POL loan amount
            _act.EnterText(By.XPath(".//*[@id='selPolSections-container']/div[4]//div[@class='selPolSection-amountContainer']/input"), (requestedAmount / 5).ToString());

            //click on add another POL button
            _act.click(By.XPath(".//*[@id='addAnotherPol']/button"), "add another POL");

            // select fifth POL from popup
            if (loanpurpose5.Contains(","))
            {
                var purpose = loanpurpose5.Split(',');
                foreach (var loan in purpose)
                {
                    var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loan + "')]/preceding-sibling::div");
                    _act.JSClick(polpurpose, "selectpurpose");
                }
                if (loanpurpose5 == "Other,Anything else")
                {
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                }
                _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
            }
            else
            {
                var polpurpose = By.XPath("//*[@id='POLOptions']//div/label[contains(text(),'" + loanpurpose5 + "')]/preceding-sibling::div");
                _act.JSClick(polpurpose, "selectpurpose");

                if (loanpurpose5 == "Other,Anything else")
                {
                    _act.EnterText(_loanpurposedetailsLoc.AnythingElseText, "Secret it is");
                }
                _act.JSClick(By.XPath(".//*[@id='purpose-done-btn2']"), "BtnPurposeLoan");
            }

            // enter third POL loan amount
            _act.EnterText(By.XPath(".//*[@id='selPolSections-container']/div[5]//div[@class='selPolSection-amountContainer']/input"), (requestedAmount / 5).ToString());

            //click on continue button
            _act.click(By.XPath(".//*[@id='btnPOLsCompleted']"), "continue");
        }


        public bool CheckHideShow()
        {
            bool flag = false;
            if (_act.isElementPresent(_loanpurposedetailsLoc.BtnHideShowDebug) == true)
            {
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Selects the pol and amount.
        /// </summary>
        /// <param name="loanamout">The loanamout.</param>
        /// <param name="loanpurpose1">The loanpurpose1.</param>
        public void SelectPOLAndAmount(int loanamout, string loanpurpose1)
        {
            // Select Loan Value from Slide bar
            SelectLoanValue(loanamout);

            //Click on First POL to select
            ClickSelectFirstPurposeBtn();

            if (GetPlatform(_driver))
            {
                // Select Purpose of loan
                SelectLoanPurposeMobile(loanpurpose1);
            }
            else
            {
                // Select Purpose of loan
                SelectLoanPurpose(loanpurpose1);
            }

            // Enter FirstPOLLoan Amount
            EnterFirstPOLAmountTxt(loanamout.ToString());

            // Click on Continue Button
            ClickLoanPOLContinueBtn();
        }


        public void LoanPurposeFunction(int loanamount, string POL)
        {
            bool hideshow = CheckHideShow();
            if (hideshow == true)
            {
                if (GetPlatform(_driver))
                {
                    RequestLoanAmountMobile(loanamount, POL);
                }
                else
                {
                    RequestLoanAmount(loanamount, POL);
                }
            }
            else
            {
                // Select Loan Value from Slide bar
                SelectLoanValue(loanamount);

                //Click on First POL to select
                ClickSelectFirstPurposeBtn();

                if (GetPlatform(_driver))
                {
                    // Select Purpose of loan
                    SelectLoanPurposeMobile(POL);
                }
                else
                {
                    // Select Purpose of loan
                    SelectLoanPurpose(POL);
                }

                // Enter FirstPOLLoan Amount
                EnterFirstPOLAmountTxt(loanamount.ToString());

                // Click on Continue Button
                ClickLoanPOLContinueBtn();
            }
        }
        public void LoanPurposeFunction_RL(int loanamount, string POL)
        {
            // Select Loan Value from Slide bar
            SelectLoanValueRL(loanamount);

            //Click on Select First POL Lst
            ClickSelectFirstPurposeBtn();

            // Select Purpose of loan
            SelectLoanPurposeRL(POL);

            // Enter FirstPOLLoan Amount
            EnterFirstPOLAmountTxt(loanamount.ToString());

            // Click on Continue Button
            ClickLoanPOLContinueBtnRL();
        }

        public void ClosePOLDialog()
        {
            _act.waitForVisibilityOfElement(_loanpurposedetailsLoc.cancelPurposeLoanDialog, 30);
            _act.click(_loanpurposedetailsLoc.cancelPurposeLoanDialog, "cancelPOL");
        }

    }
}
