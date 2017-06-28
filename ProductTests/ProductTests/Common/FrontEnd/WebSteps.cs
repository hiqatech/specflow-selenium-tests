using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ZolCo.ProductTests.Utilities;

namespace ZolCo.ProductTests.Common
{
    [Binding]
    [TestClass]
    public class WebSteps
    {
        [Given(@"I start the WebDriver on the (.*) browser")]
        [When(@"I start the WebDriver on the (.*) browser")]
        [Then(@"I start the WebDriver on the (.*) browser")]
        public static void IStartTheWebDriverOnBrowser(string browser)
        {
           WebHelp.startDriver(browser);
           if (SetUp.scenarioTitleSections["application"].Contains("PI"))
                WebHelp.NavigateTo(SetUp.currentEnvironment["WebPIURL"]);
        }

        [Given(@"I navigate to the (.*) website")]
        [When (@"I navigate to the (.*) website")]
        [Then(@"I navigate to the (.*) website")]
        public static void INavigateTo(string URL)
        {
            WebHelp.NavigateTo(URL);         
        }     

        [Given(@"I am on the (.*) page")]
        [When(@"I am on the (.*) page")]
        [Then(@"I am on the (.*) page")]
        public static void IAmOnThePage(string pageName)
        {
            AllPages.CurrentPageName = pageName;
        }

        [Given(@"I enter (.*) as the (.*)")]
        [When(@"I enter (.*) as the (.*)")]
        [Then(@"I enter (.*) as the (.*)")]
        public static void GivenIEnterAsThe(string entry, string elementName)
        {
            string elementLocator = AllPages.GetElementLocator(elementName);

            if (entry.Contains("system"))
                Thread.Sleep(1000);

            if (entry.Contains("PassWord"))
                entry = SetUp.currentEnvironment["WebPIPassword"];

            Assert.IsTrue(WebHelp.WaitToAppear(elementLocator));
            IWebElement webElement = WebHelp.webdriver.FindElement(By.CssSelector(elementLocator));

            WebHelp.SafeSelect(elementLocator, "safeclick");

            if (elementName.Contains("amount"))
            { for (int i = 0; i < 4; i++) { webElement.SendKeys(Keys.ArrowLeft); } }

            if (entry.Contains("system_date"))
            {
                entry = entry.Replace("system_date", "system_date_start");
                entry = DataHelp.GetDynamicDate(DataBaseRWCD.systemDate, entry, "ddMMyyyy");
            }

            if (elementName.Contains("date"))
            {
                for (int i = 0; i < 10; i++)
                {
                    webElement.SendKeys(Keys.ArrowLeft);
                }
                if (entry.Contains("-"))
                    entry = entry.Replace("-", "");
                else if (entry.Contains("."))
                    entry = entry.Replace(".", "");
                else if (entry.Contains("/"))
                    entry = entry.Replace("/", "");
            }

            if (entry.Contains("random") && elementName.Contains("proposal"))
            {
                if (SetUp.scenarioTitleSections["distribution"].Contains("Allianz"))
                    entry = DataHelp.GenerateRandomString(10, "01");
            }

            webElement.Clear();
            webElement.SendKeys(entry);
            Console.WriteLine(entry + " value entered into the field with " + elementLocator + " elementLocator");

            if (elementName.Contains("city"))
            {
                WebHelp.SafeSelect("#main", "safeclick");
                Thread.Sleep(2000);
            }
        }

        [Given(@"I select the (.*) element")]
        [When(@"I select the (.*) element")]
        [Then(@"I select the (.*) element")]
        public static void ISelectTheElement(string elementName)
        {
            string elementLocator = AllPages.GetElementLocator(elementName);
            if (elementName.Contains("alert"))
                WebHelp.AlertHandling("accept");
            else
            {
                Assert.IsTrue(WebHelp.WaitToAppear(elementLocator));

                if (elementName.Contains("selection") || (elementName.Contains("login")))
                    Thread.Sleep(1000);

                WebHelp.SafeSelect(elementLocator, "safeclick");

                if (elementName.Contains("confirm"))
                    Thread.Sleep(2000);
                else if (elementName.Contains("search"))
                    Thread.Sleep(1000);
            }
        }

        [Given(@"I select the (.*) element as the (.*)")]
        [When(@"I select the (.*) element as the (.*)")]
        [Then(@"I select the (.*) element as the (.*)")]
        public static void ISelectTheElementAsThe(string value, string elementName)
        {
            string elementLocator = AllPages.GetDartaElementLocator(elementName);
            WebHelp.SelectFromDropDown(elementLocator, value);
        }

        [Given(@"I should see the (.*) element")]
        [When(@"I should see the (.*) element")]
        [Then(@"I should see the (.*) element")]
        public static void GivenIShouldSeeTheElement(string elementName)
        {
            string elementLocator = AllPages.GetElementLocator(elementName);
            Assert.IsTrue(WebHelp.WaitToAppear(elementLocator));
            IWebElement webElement = WebHelp.webdriver.FindElement(By.CssSelector(elementLocator));
            WebHelp.SafeSelect(elementLocator, "focus");
            string fileName = "ProductTests_Default_" + DateTime.Now.ToString("yyyy-MM-ddTHHmmss") + "_evidence.jpg";
            WebHelp.TakeScreenShot(fileName, SetUp.defaultTestRestultDirectory);

            if (elementName.Contains("notification"))
            {
                string message = webElement.Text;
                Console.WriteLine(message);

                if (elementName.Contains("success"))
                    Assert.IsTrue(message.Contains("Success:"));
            }
            Assert.IsTrue(WebHelp.isDisplayed(elementLocator));
        }

        [Given(@"I should not see the (.*) element")]
        [When(@"I should not see the (.*) element")]
        [Then(@"I should not see the (.*) element")]
        public static void GivenIShouldNotSeeTheElement(string elementName)
        {
            string elementLocator = AllPages.GetElementLocator(elementName);
            string fileName = "ProductTests_Default_" + DateTime.Now.ToString("yyyy-MM-ddTHHmmss") + "_evidence.jpg";
            WebHelp.TakeScreenShot(fileName, SetUp.defaultTestRestultDirectory);
            Assert.IsTrue(WebHelp.WaitToDisappear(elementLocator));
        }
      
        [Given(@"I should see (.*) at the (.*) element")]
        [When(@"I should see (.*) at the (.*) element")]
        [Then(@"I should see (.*) at the (.*) element")]
        public static void IShouldValueAtSeeTheElement(string value, string elementName)
        {
            string elementLocator = AllPages.GetElementLocator(elementName);
            Assert.IsTrue(WebHelp.WaitToAppear(elementLocator));
            IWebElement webElement = WebHelp.webdriver.FindElement(By.CssSelector(elementLocator));
            WebHelp.SafeSelect(elementLocator, "focus");
            string fileName = "ProductTests_Default_" + DateTime.Now.ToString("yyyy-MM-ddTHHmmss") + "_evidence.jpg";
            WebHelp.TakeScreenShot(fileName, SetUp.defaultTestRestultDirectory);
            Assert.IsTrue(webElement.Text == value);
        }

        [Given(@"I take a test evidence screenshot")]
        [When(@"I take a test evidence screenshot")]
        [Then(@"I take a test evidence screenshot")]
        public static void ITakeScreenShot()
        {
            string fileName = "ProductTests_Default_" + DateTime.Now.ToString("yyyy-MM-ddTHHmmss") + "_evidence.jpg";
            WebHelp.TakeScreenShot(fileName, SetUp.defaultTestRestultDirectory);
        }

        [Given(@"The (.*) element should be selected")]
        [When(@"The (.*) element should be selected")]
        [Then(@"The(.*) element should be selected")]
        public static void ShouldBeSelected(string elementName)
        {
            string elementLocator = AllPages.GetElementLocator(elementName);
            Assert.IsTrue(WebHelp.WaitToAppear(elementLocator));
            IWebElement webElement = WebHelp.webdriver.FindElement(By.CssSelector(elementLocator));
            WebHelp.SafeSelect(elementLocator, "focus");
            string fileName = "ProductTests_Default_" + DateTime.Now.ToString("yyyy-MM-ddTHHmmss") + "_evidence.jpg";
            WebHelp.TakeScreenShot(fileName, SetUp.defaultTestRestultDirectory);
            Assert.IsTrue(webElement.Selected);
            //Assert.IsTrue(webelement.GetAttribute("checked")=="Checked");
        }
      
        [Given(@"I wait (.*) sec")]
        [When(@"I wait (.*) sec")]
        [Then(@"I wait (.*) sec")]
        public static void GivenIWaitSec(int wait)
        {
            Thread.Sleep(wait * 1000);
        }

    }
}
