using OpenQA.Selenium;
using System;
using System.Threading;
using OpenQA.Selenium.Interactions;
using System.Reflection;
using System.IO;
using System.Drawing.Imaging;
using ProductTests.Common.Steps.FrontEnd;

namespace ProductTests.Common
{
    class Helper
    {
        static int startTime = 0;
        static int timeSteps = 1000;
        static int maxTime = 32000;
        public static int shortSleepTime = 100;
        public static int longSleepTime = 10000;
        public static IWebElement webelement;


        public static void shortSleep()
        {
            Thread.Sleep(shortSleepTime);
        }

        public static void longSleep()
        {
            Thread.Sleep(longSleepTime);
        }

        public static void Sleep(int sleepTime)
        {
            Thread.Sleep(sleepTime);
            Console.WriteLine("waiting " + sleepTime + " ms");
        }


        public static void SafeClick(IWebElement webelement, string to)
        {
            if (SetUp.current_driver == "Firefox" || to == "click")
            {
                webelement.Click();
                goto firefox_action_done;
            }

            Actions actions = new Actions(SetUp.driver);
            actions.MoveToElement(webelement);
            actions.Perform();

            if (to == "safeclick")
                webelement.Click();
            else if (to == "return")
                webelement.SendKeys(Keys.Return);
            else
                webelement.SendKeys(Keys.Enter);
            
        firefox_action_done:;
        }

        public static bool isDisplayed(IWebElement webelement,string element_name)
        {
            Helper.shortSleep();
            try
            {
                return webelement.Displayed && webelement.Enabled;
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


        public static bool WaitToAppear(string element_locator, string element_name)
        {
            startTime = 0;
            while (startTime < maxTime)
            {
                webelement = SetUp.driver.FindElement(By.CssSelector(element_locator));
                if (isDisplayed(webelement, element_name))
                {
                    Thread.Sleep(shortSleepTime);
                    AllPages.webelement = webelement;
                    return true;    
                } 
                Thread.Sleep(timeSteps);
                startTime = startTime + timeSteps;
                Console.WriteLine(element_name + " element is still not visible after " + startTime + " msec");
            }
            Console.WriteLine(element_name + " element did not appear in " + maxTime + " msec");
            return false;      
        }


        public static bool WaitToDisappear(string element_locator, string element_name)
        {
            startTime = 0;
            while (startTime < maxTime)
            {
                webelement = SetUp.driver.FindElement(By.CssSelector(element_locator));
                if (!(isDisplayed(webelement, element_name)))
                {
                    Thread.Sleep(shortSleepTime);
                    AllPages.webelement = webelement;
                    return true;
                }
                Thread.Sleep(timeSteps);
                startTime = startTime + timeSteps;
                Console.WriteLine(element_name + " element is still visible after " + startTime + " msec");
            }
            Console.WriteLine(element_name + " element did not disappear in " + maxTime + " msec");
            return false;
        }


        public static void TakeScreenShot()
        {
            string testImage = "testEvidenceImage" + SetUp.systemTime + ".jpg"; 
            var screenshotdriver = SetUp.driver as ITakesScreenshot;
            var screenshot = screenshotdriver.GetScreenshot();
            string imagefullpath = Path.Combine(SetUp.currentTestRestultDirectory, testImage);      
            screenshot.SaveAsFile(imagefullpath, ImageFormat.Jpeg);

            Console.WriteLine("Test Evidence ScreenShoot saved with " + testImage.ToString() + " name at " + SetUp.systemTime + " to " + SetUp.currentTestRestultDirectory);
        }


        public static string GenerateRandomString(int length , string startswith) {

            string systemTime = DateTime.Now.ToString("yyyyMMddHHmmss"); 
            double datenumber = double.Parse(systemTime); 
            string datestring = datenumber.ToString(); 
            int random_lenght = length - startswith.Length; 
            int remove = datestring.Length - random_lenght; 
            string random_string = (datestring.Remove(1, remove));
            random_string = startswith + random_string;

            return random_string;

        }



    }

}