using System;
using System.IO;
using OpenQA.Selenium;
using System.Threading;
using System.Reflection;
using OpenQA.Selenium.Support.UI;
using System.Drawing.Imaging;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ZolCo.ProductTests.Common;

namespace ZolCo.ProductTests.Utilities
{
    public class WebHelp
    {
        public static IWebDriver webdriver = null;

        public static void startDriver(string browser)
        {
            switch (browser)
            {
                case "Chrome":
                    {
                        ChromeOptions options = new ChromeOptions();
                        options.AddArgument("--ignore-certificate-errors");
                        webdriver = new ChromeDriver();
                        break;
                    }
                case "Firefox":
                    {
                        string path = SetUp.testProjectDirectory + "\\ffProfile";
                        FirefoxProfile ffprofile = new FirefoxProfile(path);
                        webdriver = new FirefoxDriver(ffprofile);
                        break;
                    }
                case "IE":
                    {
                        DesiredCapabilities capabilities = new DesiredCapabilities();
                        capabilities.SetCapability(CapabilityType.AcceptSslCertificates, true);
                        webdriver = new InternetExplorerDriver();
                        break;
                    }
            }
            Console.WriteLine("Starting " + browser + " driver");
            webdriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            webdriver.Manage().Window.Maximize();

        }

        public static void NavigateTo(string URL)
        {
            try
            {
                webdriver.Navigate().GoToUrl(URL);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("navigated to " + URL);
        }

        public static void AlertHandling(string action)
        {
            try
            {
                if (action == "accept")
                webdriver.SwitchTo().Alert().Accept();
                else if (action == "dismiss")
                    webdriver.SwitchTo().Alert().Dismiss();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("alert has been" + action + "ed");
        }


        public static void WaitSelect(string elementLocator)
        {
            new WebDriverWait(webdriver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementExists((By.CssSelector(elementLocator))));
            IWebElement webElement = WebHelp.webdriver.FindElement(By.CssSelector(elementLocator));
            webElement.Click();
            Console.WriteLine("element with " + elementLocator + " locator has been selected");
        }

        public static void WaitType(string elementLocator, string value)
        {
            new WebDriverWait(webdriver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementExists((By.CssSelector(elementLocator))));
            IWebElement webElement = webdriver.FindElement(By.CssSelector(elementLocator));
            webElement.SendKeys(value);
            Console.WriteLine("value "+ value + " entered into field with locator " + elementLocator);
        }

        public static string WaitGetText(string elementLocator)
        {
            string value = null;
            new WebDriverWait(webdriver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementExists((By.CssSelector(elementLocator))));
            IWebElement webElement = webdriver.FindElement(By.CssSelector(elementLocator));
            value = webElement.GetAttribute("value");
            Console.WriteLine("value " + value + " presents in field with locator " + elementLocator);
            return value;
        }

        public static void SafeSelect(string elementLocator, string to)
        {
            Assert.IsTrue(WaitToAppear(elementLocator));
            IWebElement webElement = webdriver.FindElement(By.CssSelector(elementLocator));
            if ( to == "click")
            {
                webElement.Click();
            }
            else
            {
                Actions actions = new Actions(WebHelp.webdriver);
                actions.MoveToElement(webElement);
                actions.Perform();

                if (!(to == "focus"))
                {
                    if (to == "safeclick")
                        webElement.Click();
                    else if (to == "return")
                        webElement.SendKeys(Keys.Return);
                    else
                        webElement.SendKeys(Keys.Enter);
                }
            }
            Console.WriteLine("element with locator " + elementLocator + " has been selected");
        }

        public static void SafeType(string elementLocator, string text)
        {
            Assert.IsTrue(WaitToAppear(elementLocator));
            IWebElement webElement = webdriver.FindElement(By.CssSelector(elementLocator));
            SafeSelect(elementLocator, "safeclick");
            webElement.Clear();
            webElement.SendKeys(text);
        }

        public static void SelectFromDropDown(string elementLocator, string value)
        {
            Assert.IsTrue(WaitToAppear(elementLocator));
            IWebElement webElement = webdriver.FindElement(By.CssSelector(elementLocator));
            SelectElement selectElement = new SelectElement(webElement);
            selectElement.SelectByText(value);
            Console.WriteLine(value + " value has been selected from element with locator" + elementLocator);
        }

        public static bool isDisplayed(string elementLocator)
        {
            try
            {
                Thread.Sleep(10);
                IWebElement webElement = webdriver.FindElement(By.CssSelector(elementLocator));
                return (webElement.Displayed && webElement.Enabled);
            }
            catch (NoSuchElementException RequiredPageContentNotPresent)
            {
                return false;
            }
            catch (InvalidElementStateException requiredPageContentNotPresent)
            {
                return false;
            }
            catch (StaleElementReferenceException elementHasDestroyed)
            {
                return false;
            }
            catch (TargetInvocationException elementHasDestroyed)
            {
                return false;
            }
        }

        public static bool WaitToAppear(string elementLocator)
        {  
            int startTime = 0;
            while (startTime < 30000)
            {             
                if (isDisplayed(elementLocator))
                    {
                        return true;         
                    }
                Thread.Sleep(250);
                startTime = startTime + 250;
            }
            Console.WriteLine("element with locator " + elementLocator + " did not appear in " + 30000 + " msec");
            return false;
        }

        public static bool WaitToDisappear(string elementLocator)
        {
            int startTime = 0;
            while (startTime < 30000)
            {
                if (!(isDisplayed(elementLocator)))
                {
                    return true;
                }
                Thread.Sleep(250);
                startTime = startTime + 250;
            }
            Console.WriteLine("element with locator " + elementLocator + " did not disappear in " + 30000 + " msec");
            return false;
        }

        public static void IUploadAFile(string browseButtonCSS, string filePath)
        {
            webdriver.FindElement(By.CssSelector(browseButtonCSS)).SendKeys(filePath);
        }

        public static void TakeScreenShot(string fileName , string fileFolder)
        {
            var screenshotdriver = webdriver as ITakesScreenshot;
            var screenshot = screenshotdriver.GetScreenshot();
            string imagefullpath = Path.Combine(fileFolder, fileName);
            screenshot.SaveAsFile(imagefullpath, ImageFormat.Jpeg);
            Console.WriteLine("Test Evidence ScreenShoot saved with " + fileName + " name to " + fileFolder);
        }

    }
}
