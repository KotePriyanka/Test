using System;
using System.Linq;
using By = OpenQA.Selenium.By;
using System.Configuration;
//using OutputType = OpenQA.Selenium.OutputType;
using OpenQA.Selenium;
using System.Drawing;

namespace Nimble.Automation.Accelerators
{
    public static class Supportmethods 
    {
        /// <summary>
        /// Same as FindElement only returns null when not found instead of an exception.
        /// </summary>
        /// usage -IWebElement element = driver.FindElmentSafe(By.Id("the id"));
        /// <param name="driver">current browser instance</param>
        /// <param name="by">The search string for finding element</param>
        /// <returns>Returns element or null if not found</returns>
        public static IWebElement FindElementSafe(this IWebDriver driver, By by)
        {
            try
            {
                return driver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        /// <summary>
        /// Requires finding element by FindElementSafe(By).
        /// Returns T/F depending on if element is defined or null.
        /// </summary> 
        /// usage - bool exists = element.Exists();
        /// <param name="element">Current element</param>
        /// <returns>Returns T/F depending on if element is defined or null.</returns>
        public static bool Exists(this IWebElement element)
        {
            bool flag = false;
            if (element == null)
            {
                flag = false;
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        public static string GetTestDataFilePath(string filename)
        {
            string TestDataFolder = Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["TestDataFolder"]);
            string path =System.IO.Directory.GetCurrentDirectory() + "\\TestData\\";
           // string path2 = Path.GetDirectoryName();
            //string path2 = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string loginTestDataFileName = System.IO.Path.Combine(TestDataFolder, filename);
            return loginTestDataFileName;
        }

        public static string ConvertRGBToHexCode(int codeFirst,int CodeSecond,int codeThird)
        {
            string hex;


            Color myColor = Color.FromArgb(codeFirst, CodeSecond, codeThird);
            return    hex = myColor.R.ToString("X2") + myColor.G.ToString("X2") + myColor.B.ToString("X2");


        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
