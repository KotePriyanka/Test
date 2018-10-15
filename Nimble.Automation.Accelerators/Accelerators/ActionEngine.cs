using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Threading;
using Actions = OpenQA.Selenium.Interactions.Actions;
using Alert = OpenQA.Selenium.IAlert;
using Assert = NUnit.Framework.Assert;
using By = OpenQA.Selenium.By;
using ExpectedConditions = OpenQA.Selenium.Support.UI.ExpectedConditions;
using JavascriptExecutor = OpenQA.Selenium.IJavaScriptExecutor;
using NoAlertPresentException = OpenQA.Selenium.NoAlertPresentException;
using Select = OpenQA.Selenium.Support.UI.SelectElement;
using WebDriverWait = OpenQA.Selenium.Support.UI.WebDriverWait;
//using OutputType = OpenQA.Selenium.OutputType;
using WebElement = OpenQA.Selenium.IWebElement;
using System.IO;
using OpenQA.Selenium;
using System.Globalization;

namespace Nimble.Automation.Accelerators
{
    public class ActionEngine
    {
        IWebDriver driver = null;
        string projectLoc = Path.Combine(Directory.GetCurrentDirectory(), "ScreenShots");

        //string screenshotpath = ConfigurationManager.AppSettings["ScreenShotPath"].ToString();
        //string projectLoc = System.IO.Path.Combine("C:\\", "ScreenShots");
        // string projectLoc = @"D:\ScreenShots"; 

        public ActionEngine()
        {

        }

        public ActionEngine(IWebDriver _driver)
        {
            driver = _driver;
        }

        public ActionEngine(IWebDriver _driver, string strLog)
        {
            driver = _driver;
        }

        public delegate void ReportingHandler(string status);
        public event ReportingHandler ClickEventLog;
        public static WebDriverWait wait;
        internal static bool b = true;

        protected int OnClick(string message)
        {
            if (ClickEventLog != null)
            {
                ClickEventLog(message);
            }

            return 1;
        }

        public bool click(By locator, string locatorName)
        {
            bool flag = true;
            try
            {
                /*Highlight element*/
                WebElement webElement = driver.FindElement(locator);
                JavascriptExecutor js = driver as JavascriptExecutor;
                js.ExecuteScript("arguments[0].style.border='2px solid yellow'", webElement);
                js.ExecuteScript("arguments[0].click();", webElement);
                /*Highlight code ends*/
                //Thread.Sleep(500);
                // driver.FindElement(locator).Click();
                //js.ExecuteScript("arguments[0].click();", webElement);
                //flag = true;
            }
            catch (Exception)
            {
                System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Jpeg;
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(Path.Combine(projectLoc, TestContext.CurrentContext.Test.Name + "-" + DateTime.Now.ToString("dd-M-yyyy", CultureInfo.InvariantCulture) + "." + format), format);
                return flag;
            }
            return flag;
        }

        public bool click1(By locator, string locatorName)
        {
            bool flag = true;
            try
            {
                /*Highlight element*/
                WebElement webElement = driver.FindElement(locator);
                JavascriptExecutor js = driver as JavascriptExecutor;
                js.ExecuteScript("arguments[0].style.border='4px solid yellow'", webElement);
                /*Highlight code ends*/
                Thread.Sleep(500);
                driver.FindElement(locator).Click();
                //js.ExecuteScript("arguments[0].click();", webElement);
                //flag = true;
            }
            catch (Exception)
            {
                return flag;
            }
            return flag;
        }

        public void Sync1()
        {
            int timer = 5;
            bool isLoadingDisplayed = false;
            do
            {
                timer--;
                //  isLoadingDisplayed = waitForVisibilityOfElement(By.XPath(".//*[@class='k-loading-image']"),60);
                System.Threading.Thread.Sleep(2000);
            } while (isLoadingDisplayed & timer < 0);

        }

        public void Sync(By Locator)
        {
            int timer = 5;
            bool isLoadingDisplayed = false;
            do
            {
                timer--;
                WebElement ele = driver.FindElement(Locator);
                isLoadingDisplayed = isElementPresent(Locator, 30);
                Thread.Sleep(1000);
            } while (!isLoadingDisplayed & timer < 0);
        }

        private void on(string status)
        {
            throw new NotImplementedException();
        }

        public bool type(By locator, string testdata, string locatorName)
        {

            bool flag = false;
            try
            {
                /*Highlight element*/

                WebElement webElement = driver.FindElement(locator);
                JavascriptExecutor js = driver as JavascriptExecutor;
                js.ExecuteScript("arguments[0].style.border='4px solid yellow'", webElement);
                /*Highlight code ends*/
                driver.FindElement(locator).Clear();

                driver.FindElement(locator).SendKeys(testdata);
                flag = true;

            }
            catch (Exception)
            {
                return flag;
            }

            return flag;
        }

        public bool Alert()
        {
            bool presentFlag = false;
            Alert alert = null;

            try
            {

                // Check the presence of alert
                alert = driver.SwitchTo().Alert();
                // if present consume the alert
                alert.Accept();
                presentFlag = true;
            }
            catch (NoAlertPresentException ex)
            {
                // Alert present; set the flag

                // Alert not present
                Console.WriteLine(ex.ToString());
                Console.Write(ex.StackTrace);
            }


            return presentFlag;
        }

        public bool assertElementPresent(By by, string locatorName)
        {

            bool flag = false;
            try
            {
                Assert.True(isElementPresent(by, locatorName));
                flag = true;
            }
            catch (Exception)
            {

            }

            return flag;

        }

        public string Title
        {
            get
            {

                string text = driver.Title;
                if (b)
                {
                    //Reporter.SuccessReport("Title", "Title of the page is" + text);
                }
                return text;
            }
        }

        public bool assertText(By by, string text)
        {
            bool flag = false;
            try
            {
                Assert.AreEqual(getText(by, text).Trim(), text.Trim());
                flag = true;
                return flag;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                return flag;
            }

        }

        public void EnterText(By Locator, string Value)
        {
            try
            {
                Thread.Sleep(1000);
                setValueToObject(Locator, Value, 60);
            }
            catch (Exception e)
            {
                System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Jpeg;
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(Path.Combine(projectLoc, TestContext.CurrentContext.Test.Name + "-" + DateTime.Now.ToString("dd-M-yyyy", CultureInfo.InvariantCulture) + "." + format), format);
                throw new Exception("Error Occured in Entering Text " + e.Message);
            }
        }

        public bool selectByOptionText(By locator, string value, string locatorName)
        {
            bool flag = false;
            try
            {
                WebElement ListBox = driver.FindElement(locator);
                IReadOnlyCollection<WebElement> options = ListBox.FindElements(By.TagName("option"));
                foreach (var option in options)
                {
                    string opt = option.Text.Trim();
                    if (opt.ToLower().Equals(value.ToLower().Trim()))
                    {
                        option.Click();
                        flag = true;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Jpeg;
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(Path.Combine(projectLoc, TestContext.CurrentContext.Test.Name + "-" + DateTime.Now.ToString("dd-M-yyyy", CultureInfo.InvariantCulture) + "." + format), format);
                throw new Exception("Option with value attribute " + value + " is Not Select from the DropDown " + locatorName + " " + e.Message);
            }
            return flag;
        }

        public string getText(By locator, string locatorName)
        {
            string text = "";
            try
            {
                if (isElementPresent(locator, locatorName))
                {
                    text = driver.FindElement(locator).Text;
                }
            }
            catch (Exception e)
            {
                System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Jpeg;
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(Path.Combine(projectLoc, TestContext.CurrentContext.Test.Name + "-" + DateTime.Now.ToString("dd-M-yyyy", CultureInfo.InvariantCulture) + "." + format), format);
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
            return text;
        }

        public string getValue(By lookupBy, string locatorName)
        {
            string value = "";

            try
            {
                if (isElementPresent(lookupBy, locatorName))
                {
                    value = driver.FindElement(lookupBy).GetAttribute("value");
                }
            }
            catch (Exception ex)
            {
                System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Jpeg;
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(Path.Combine(projectLoc, TestContext.CurrentContext.Test.Name + "-" + DateTime.Now.ToString("dd-M-yyyy", CultureInfo.InvariantCulture) + "." + format), format);
                throw new Exception("Unable to get Text from " + locatorName + "<br />" + ex.Message);
            }
            return value;
        }

        public bool assertTextMatching(By by, string text, string locatorName)
        {
            bool flag = false;
            try
            {
                string ActualText = getText(by, text).Trim();
                if (ActualText.Contains(text))
                {
                    flag = true;
                    return flag;
                }
                else
                {
                    return flag;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                return false;
            }

        }

        public bool assertTextPresent(string text)
        {
            bool flag = false;
            try
            {
                Assert.True(isTextPresent(text));
                flag = true;
            }
            catch (Exception)
            {

            }

            return flag;
        }

        public bool assertValue(string expvalue, By locator, string attribute, string locatorName)
        {

            bool flag = false;
            try
            {
                Assert.AreEqual(expvalue, getAttribute(locator, attribute, locatorName));
                flag = true;
            }
            catch (Exception)
            {

            }

            return flag;
        }

        public string getAttribute(By by, string attribute, string locatorName)
        {
            string value = "";
            if (isElementPresent(by, locatorName))
            {
                value = driver.FindElement(by).GetAttribute(attribute);
            }
            return value;
        }

        public bool click1(WebElement locator, string locatorName)
        {
            bool flag = false;
            try
            {
                locator.Click();
                flag = true;
            }
            catch (Exception)
            {
                return false;
                //throw ex;
            }
            return flag;
        }

        public bool clickAndWaitForElementPresent(By locator, By waitElement, string locatorName)
        {
            bool flag = false;
            try
            {
                click(locator, locatorName);
                waitForElementPresent(waitElement);
                flag = true;
                return flag;
            }
            catch (Exception)
            {
                return flag;
            }
        }

        public void closeBrowser()
        {
            driver.Close();
            driver.Quit();
        }

        public void ImplicitWait()
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.MaxValue);
        }

        public bool isChecked(By locator, string locatorName)
        {
            bool bvalue = false;
            try
            {
                if (driver.FindElement(locator).Selected)
                {
                    bvalue = true;
                }

            }
            catch (Exception)
            {
                bvalue = false;
            }
            return bvalue;
        }

        public bool isElementDisplayed(WebElement element)
        {
            bool flag = false;
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    if (element.Displayed)
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(5);
                    }
                }
            }
            catch (Exception ex)
            {
                flag = false;
                System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Jpeg;
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(Path.Combine(projectLoc, TestContext.CurrentContext.Test.Name + "-" + DateTime.Now.ToString("dd-M-yyyy", CultureInfo.InvariantCulture) + "." + format), format);
                throw new Exception(ex.Message);
            }
            return flag;
        }

        public bool isElementPresent(By by, string locatorName)
        {
            bool flag = false;
            try
            {
                driver.FindElement(by);
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
                System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Jpeg;
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(Path.Combine(projectLoc, TestContext.CurrentContext.Test.Name + "-" + DateTime.Now.ToString("dd-M-yyyy", CultureInfo.InvariantCulture) + "." + format), format);
                throw new Exception(ex.Message);
                // Console.WriteLine(e.Message);                             
            }
            return flag;
        }

        public bool isElementPresent(By by)
        {
            bool flag = false;
            try
            {
                driver.FindElement(by);
                flag = true;
            }
            catch (Exception)
            {
                //flag = false;
                //System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Jpeg;
                //((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(Path.Combine(projectLoc, TestContext.CurrentContext.Test.Name + "-" + DateTime.Now.ToString("dd-M-yyyy", CultureInfo.InvariantCulture) + "." + format), format);
                //throw new Exception(ex.Message);
            }
            return flag;
        }

        public bool isElementPresent(By by, int timeout)
        {
            bool flag = false;
            TimeSpan time = new TimeSpan(timeout);
            WebDriverWait wait = new WebDriverWait(driver, time);
            try
            {
                driver.FindElement(by);
                flag = true;
            }
            catch (Exception)
            {
                //flag = false;
                //System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Jpeg;
                //((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(Path.Combine(projectLoc, TestContext.CurrentContext.Test.Name + "-" + DateTime.Now.ToString("dd-M-yyyy", CultureInfo.InvariantCulture) + "." + format), format);
                //throw new Exception(ex.Message);
            }
            return flag;

        }
                
        public bool isEnabled(By locator, string locatorName)
        {
            bool? value = false;
            try
            {
                if (driver.FindElement(locator).Enabled)
                {
                    value = true;
                }
            }
            catch (Exception)
            {
                value = false;
            }

            return value.Value;
        }

        public bool isElementDisabled(By lookupby)
        {
            bool flag = false;
            try
            {
                if (isElementPresent(lookupby, ""))
                {
                    flag = driver.FindElement(lookupby).Enabled;
                    if (flag)
                        flag = false;
                    else
                        flag = true;
                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            return flag;
        }

        public bool isDisabled(By locator, string locatorName)
        {
            bool? value = true;

            try
            {
                if (driver.FindElement(locator).Enabled)
                {
                    value = false;
                }
            }
            catch (Exception)
            {
                value = true;
            }

            return value.Value;
        }

        public bool isPopUpElementPresent(By by, string locatorName)
        {
            bool flag = false;
            try
            {
                if (driver.FindElement(by).Displayed)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
                return flag;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }

        }

        public bool isTextPresent(string text)
        {

            bool value = driver.PageSource.Contains(text);
            Console.WriteLine("is text " + text + " present  " + value);
            bool flag = false;
            if (!value)
            {
                return false;
            }
            else if (b && flag)
            {
                return true;
            }
            return value;
        }

        public bool isVisible(By locator, string locatorName)
        {
            bool? value = false;
            try
            {
                value = driver.FindElement(locator).Displayed;
                value = true;
            }
            catch (Exception)
            {
                value = false;
            }
            return value.Value;
        }

        public bool JSClick(By locator, string locatorName)
        {
            bool flag = false;
            try
            {
                WebElement element = driver.FindElement(locator);
                JavascriptExecutor executor = (JavascriptExecutor)driver;
                executor.ExecuteScript("arguments[0].click();", element);
                //driver.executeAsyncScript("arguments[0].click();", element);
                flag = true;
            }
            catch (Exception ex)
            {
                System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Jpeg;
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(Path.Combine(projectLoc, TestContext.CurrentContext.Test.Name + "-" + DateTime.Now.ToString("dd-M-yyyy", CultureInfo.InvariantCulture) + "." + format), format);
                throw new Exception(ex.Message);
            }

            return flag;
        }

        public bool launchUrl(string url)
        {
            bool flag = false;
            try
            {

                // getResponseCode(url);
                driver.Manage().Cookies.DeleteAllCookies();
                driver.Navigate().GoToUrl(url);
                flag = true;
                return flag;
            }
            catch (Exception)
            {
                return flag;
            }

        }

        public bool mouseHoverByJavaScript(By locator, string locatorName)
        {
            bool flag = false;
            try
            {
                WebElement mo = driver.FindElement(locator);
                string javaScript = "var evObj = document.createEvent('MouseEvents');" + "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" + "arguments[0].dispatchEvent(evObj);";
                JavascriptExecutor js = (JavascriptExecutor)driver;
                js.ExecuteScript(javaScript, mo);
                flag = true;
                return flag;
            }

            catch (Exception)
            {
                return flag;
            }

        }

        public bool mouseover(By locator, string locatorName)
        {
            bool flag = false;
            try
            {
                WebElement mo = driver.FindElement(locator);
                (new Actions(driver)).MoveToElement(mo).Build().Perform();
                flag = true;
                return flag;
            }
            catch (Exception)
            {
                return flag;
            }

        }

        public void mouseOverElement(WebElement element, string locatorName)
        {
            try
            {
                (new Actions(driver)).MoveToElement(element).Build().Perform();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }
        }

        public bool rightclick(By by, string locatorName)
        {

            bool flag = false;
            try
            {
                WebElement elementToRightClick = driver.FindElement(by);
                Actions clicker = new Actions(driver);
                clicker.ContextClick(elementToRightClick).Perform();
                flag = true;
                return flag;
                // driver.findElement(by1).sendKeys(Keys.DOWN);
            }
            catch (Exception)
            {
                return flag;
            }

        }

        public bool selectByIndex(By locator, int index, string locatorName)
        {
            bool flag = false;
            try
            {
                Select s = new Select(driver.FindElement(locator));
                s.SelectByIndex(index);
                flag = true;
                return flag;
            }
            catch (Exception)
            {

                return flag;
            }

        }

        public bool selectBySendkeys(By locator, string value, string locatorName)
        {

            bool flag = false;
            try
            {
                driver.FindElement(locator).SendKeys(value);
                flag = true;
                return flag;
            }
            catch (Exception)
            {

                return flag;
            }

        }

        public bool selectByValue(By locator, string value, string locatorName)
        {
            bool flag = false;
            try
            {
                Select s = new Select(driver.FindElement(locator));
                s.SelectByValue(value);
                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
                System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Jpeg;
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(Path.Combine(projectLoc, TestContext.CurrentContext.Test.Name + "-" + DateTime.Now.ToString("dd-M-yyyy", CultureInfo.InvariantCulture) + "." + format), format);
                throw new Exception(ex.Message);
            }
            return flag;

        }

        public bool selectByVisibleText(By locator, string visibletext, string locatorName)
        {
            bool flag = false;
            try
            {
                /*Highlight element*/

                WebElement webElement = driver.FindElement(locator);
                JavascriptExecutor js = driver as JavascriptExecutor;
                js.ExecuteScript("arguments[0].style.border='4px solid yellow'", webElement);
                /*Highlight code ends*/
                Select s = new Select(driver.FindElement(locator));
                s.SelectByText(visibletext);
                flag = true;
                return flag;
            }
            catch (Exception)
            {
                return flag;
            }

        }

        public bool slider(By slider, int x, int y, string locatorName)
        {
            bool flag = false;
            try
            {
                WebElement dragitem = driver.FindElement(slider);
                // new Actions(driver).dragAndDropBy(dragitem, 400, 1).build()
                // .perform();
                (new Actions(driver)).DragAndDropToOffset(dragitem, x, y).Build().Perform(); // 150,0
                Thread.Sleep(5000);
                flag = true;
                return flag;
            }
            catch (Exception)
            {
                return flag;
            }

        }

        public bool switchToDefaultFrame()
        {
            bool flag = false;
            try
            {
                driver.SwitchTo().DefaultContent();
                flag = true;
                return flag;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                return flag;
            }

        }

        public bool switchToFrameById(string idValue)
        {
            bool flag = false;
            try
            {
                driver.SwitchTo().Frame(idValue);
                flag = true;
                return flag;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                return flag;
            }

        }

        public bool switchToFrameByIndex(int index)
        {
            bool flag = false;
            try
            {
                driver.SwitchTo().Frame(index);
                flag = true;
                return flag;
            }
            catch (Exception)
            {
                return flag;
            }

        }

        public bool switchToFrameByLocator(By lacator, string locatorName)
        {
            bool flag = false;
            try
            {
                driver.SwitchTo().Frame(driver.FindElement(lacator));
                flag = true;
                return flag;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                return flag;
            }

        }

        public bool switchToFrameByName(string nameValue)
        {
            bool flag = false;
            try
            {
                driver.SwitchTo().Frame(nameValue);
                flag = true;
                return flag;
            }
            catch (Exception)
            {

                return flag;
            }

        }

        public bool switchWindowByTitle(string windowTitle, int count)
        {
            bool flag = false;
            try
            {
                //			Set<String> windowList = driver.getWindowHandles();
                //			int windowCount = windowList.size();
                // Calendar calendar = new GregorianCalendar();
                // int second = calendar.get(Calendar.SECOND); // /to get current
                // time
                // int timeout = second + 40;
                /*
                 * while (windowCount != count && second < timeout) {
                 * Thread.sleep(500); windowList = driver.getWindowHandles();
                 * windowCount = windowList.size();
                 * 
                 * }
                 */

                //			String[] array = windowList.toArray(new String[0]);

                //			for (int i = 0; i <= windowCount; i++) {
                //
                //				driver.switchTo().window(array[count - 1]);
                //
                //				// if (driver.getTitle().contains(windowTitle))
                //				flag = true;
                //				return true;
                //			}
                return flag;
            }
            catch (Exception)
            {
                return flag;
            }

        }

        public bool verifyText(By by, string text, string locatorName)
        {
            bool flag = false;

            try
            {

                string vtxt = getText(by, locatorName).Trim();
                vtxt.Equals(text.Trim());
                flag = true;
                return flag;
            }
            catch (Exception)
            {
                return flag;
            }

        }

        public bool verifyTextPresent(string text)
        {
            bool flag = false;
            ;
            if (!(driver.PageSource).Contains(text))
            {
                flag = false;
            }
            else if (b && flag)
            {
                flag = true;
            }
            return flag;
        }

        public bool verifyTitle(string title)
        {

            bool flag = false;

            try
            {
                Title.Equals(title);
                flag = true;
                return flag;
            }
            catch (Exception)
            {
                return flag;
            }


        }

        public bool waitForElementPresent(By by)
        {
            bool flag = false;
            try
            {
                for (int i = 0; i < 600; i++)
                {
                    if (driver.FindElement(by).Displayed)
                    {
                        flag = true;
                        return true;
                    }
                    else
                    {
                        Thread.Sleep(100);
                    }
                }

            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                return false;
            }


            return flag;

        }

        public bool waitForInVisibilityOfElement(By by, string locator)
        {
            bool flag = false;
            TimeSpan time = new TimeSpan(30);
            WebDriverWait wait = new WebDriverWait(driver, time);
            try
            {
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
                flag = true;
                return flag;
            }
            catch (Exception)
            {
                return flag;
            }
        }

        public bool waitForTitlePresent(By locator)
        {
            bool bValue = false;
            try
            {
                //for (int i = 0; i < 200; i++)
                //{
                //    if (Convert.ToDouble(driver.FindElement(locator).Size) > 0)
                //    {
                //        flag = true;
                //        bValue = true;
                //        break;
                //    }
                //    else
                //    {
                //        Monitor.Wait(driver, TimeSpan.FromMilliseconds(50));
                //    }
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
            }

            return bValue;
        }

        public void clickWhenReady(By locator, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            WebElement element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            element.Click();
        }

        public bool waitForVisibilityOfElement(By lookupBy, double timeout)
        {
            bool flag = false;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            try
            {
                WebElement ele = wait.Until(ExpectedConditions.ElementIsVisible(lookupBy));
                flag = true;
            }
            catch (Exception ex)
            {
                System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Jpeg;
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(Path.Combine(projectLoc, TestContext.CurrentContext.Test.Name + "-" + DateTime.Now.ToString("dd-M-yyyy", CultureInfo.InvariantCulture) + "." + format), format);
                Assert.Fail(lookupBy + " is not visible for " + timeout + " seconds." + " with message :" + ex.Message);
                flag = false;
            }

            return flag;
        }

        public WebElement WaitUntilElementExists(By lookupBy, int timeout = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(ExpectedConditions.ElementExists(lookupBy));
            }
            catch (NoSuchElementException ex)
            {
                System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Jpeg;
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(Path.Combine(projectLoc, TestContext.CurrentContext.Test.Name + "-" + DateTime.Now.ToString("dd-M-yyyy", CultureInfo.InvariantCulture) + "." + format), format);
               // Assert.Fail(lookupBy + " is not visible for " + timeout + " seconds." + " with message :" + ex.Message);
                throw new Exception(lookupBy + " is not visible for " + timeout + " seconds." + " with message :" + ex.Message);
            }
        }

        public bool waitForVisibilityOfElementUntillTimeout(By lookupBy, double timeout)
        {
            bool bln = false;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            try
            {
                WebElement ele = wait.Until(ExpectedConditions.ElementIsVisible(lookupBy));//sunil
                bln = true;
            }
            catch (Exception ex)
            {
                System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Jpeg;
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(Path.Combine(projectLoc, TestContext.CurrentContext.Test.Name + "-" + DateTime.Now.ToString("dd-M-yyyy", CultureInfo.InvariantCulture) + "." + format), format);
                Assert.Fail(lookupBy + " is not visible for " + timeout + " seconds." + " with message :" + ex.Message);
                throw new Exception(ex.Message);
            }

            return bln;
        }

        public void waitUntilTextPresents(By by, string text)
        {
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(by, text));
        }

        public void switchToLastWindow()
        {
            IList<string> objNewindow = driver.WindowHandles;

            /*switch to child window*/
            foreach (string window in objNewindow)
            {
                driver.SwitchTo().Window(window);
            }

        }

        public void swithToParentWindow(string strWindowHandle)
        {
            driver.SwitchTo().Window(strWindowHandle);
        }

        public void HandleAlertPopUp()
        {
            try
            {
                OpenQA.Selenium.IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
            }
            catch (NoAlertPresentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void setValueToObject(By lookupBy, string strInputValue, int maxWaitTime = 60)
        {
            IWebElement element = null;
            try
            {
                element = waitForElementVisible(lookupBy, maxWaitTime);
                if (element != null)
                {                    
                    element.Clear();                                      
                    element.SendKeys(strInputValue);
                }
            }
            catch (Exception ex)
            {
                System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Jpeg;
                ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(Path.Combine(projectLoc, TestContext.CurrentContext.Test.Name + "-" + DateTime.Now.ToString("dd-M-yyyy", CultureInfo.InvariantCulture) + "." + format), format);
                Assert.Fail(lookupBy + " is not visible for " + maxWaitTime + " seconds." + " with message :" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        internal WebElement waitForElementVisible(By lookupBy, int maxWaitTime = 60)
        {
            WebElement element = null;
            try
            {
                element = new WebDriverWait(driver, TimeSpan.FromSeconds(maxWaitTime)).Until(ExpectedConditions.ElementIsVisible(lookupBy));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            if (element != null)
            {
                try
                {
                    string script = String.Format(@"arguments[0].style.cssText = ""border-width: 4px; border-style: solid; border-color: {0}"";", "orange");
                    JavascriptExecutor jsExecutor = (JavascriptExecutor)driver;
                    //jsExecutor.ExecuteScript(script, new object[] { element });
                    //jsExecutor.ExecuteScript(String.Format(@"$(arguments[0].scrollIntoView(true));"), new object[] { element });
                }
                catch { }
            }
            return element;
        }

        internal IWebElement waitForElementIsPresent(By lookupBy, int maxWaitTime = 60)
        {
            IWebElement element = null;
            try
            {
                element = new WebDriverWait(driver, TimeSpan.FromSeconds(maxWaitTime)).Until(ExpectedConditions.ElementExists(lookupBy));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            if (element != null)
            {
                try
                {
                    string script = String.Format(@"arguments[0].style.cssText = ""border-width: 4px; border-style: solid; border-color: {0}"";", "orange");
                    JavascriptExecutor jsExecutor = (JavascriptExecutor)driver;
                    jsExecutor.ExecuteScript(script, new object[] { element });
                    jsExecutor.ExecuteScript(String.Format(@"$(arguments[0].scrollIntoView(true));"), new object[] { element });
                }
                catch { }
            }
            return element;
        }

    }
}


