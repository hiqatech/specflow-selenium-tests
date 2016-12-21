using OpenQA.Selenium;
using System;
using System.Threading;
using OpenQA.Selenium.Interactions;
using System.Reflection;
using System.IO;
using System.Drawing.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkedInSpecFlowTest.Steps.Env
{
    class Helper
    {
        static int startTime = 0;
        static int timeSteps = 1000;
        static int maxTime = 17000;
        public static int shortSleepTime = 20;
        public static int longSleepTime = 10000;

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
            Thread.Sleep(sleepTime*1000);
            Console.WriteLine("waiting");
        }


        public static void SafeClick(IWebElement webelement, string to)
        {

            if (SetUp.current_driver == "Firefox")
            {
                webelement.Click();
                goto firefox_action_down;
            }

            Actions actions = new Actions(SetUp.driver);
            actions.MoveToElement(webelement);
            actions.Perform();

            if (to=="click")
                webelement.Click();
            else if (to=="return")
                webelement.SendKeys(Keys.Return);
            else
                webelement.SendKeys(Keys.Enter);

            firefox_action_down: ;
        }

        public static bool isDisplayed(IWebElement webelement,string element_name)
        {
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


        public static void WaitToAppear(IWebElement webelement, string element_name)
        {
            startTime = 0;
            while (startTime < maxTime)
            {
                if (isDisplayed(webelement, element_name))
                {
                    Thread.Sleep(shortSleepTime);
                    goto element_appeared;
                } 
                Thread.Sleep(timeSteps);
                startTime = startTime + timeSteps;
                Console.WriteLine(element_name + " element is still not visible after " + startTime + " msec");
            }
            Assert.Fail(element_name + " element did not appear in " + maxTime + " msec");
        element_appeared:; 
        }


        public static void WaitToDisappear(IWebElement webelement, string element_name)
        {
            startTime = 0;
            while (startTime < maxTime)
            {
                if (!(isDisplayed(webelement, element_name)))
                {
                    Thread.Sleep(shortSleepTime);
                    goto element_disappeared;
                }
                Thread.Sleep(timeSteps);
                startTime = startTime + timeSteps;
                Console.WriteLine(element_name + " element is still visible after " + startTime + " msec");
            }
            Assert.Fail(element_name + " element did not disappear in " + maxTime + " msec");
        element_disappeared:;
        }


        public static void TakeScreenShot()
        {   
            string testImage = "testImage" + SetUp.systemTime + ".jpg";
            var screenshotdriver = SetUp.driver as ITakesScreenshot;
            var screenshot = screenshotdriver.GetScreenshot();
            string imagefullpath = Path.Combine(SetUp.currentTestRestultDirectory, testImage);
            
            screenshot.SaveAsFile(imagefullpath, ImageFormat.Jpeg);

            Console.WriteLine("Test Evidence ScreenShoot saved with " + testImage.ToString() + " name at " + SetUp.systemTime + " to " + SetUp.currentTestRestultDirectory);
        }

}

}