using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using CrossEndSpecFlowTests.Pages;
using System;
using OpenQA.Selenium;
using CrossEndSpecFlowTests.Common;

namespace CrossEndSpecFlowTests.Steps.Test
{

        [Binding]

        public class ShouldSee
        {
            public IWebElement webelement = null;
            public string element_locator;

        [TestMethod]
        [Given(@"I should see the (.*) element")]
        public void GivenIShouldSeeTheElement(string element_name)
        {
            IShoulSeeTheElement(element_name);
        }

        [TestMethod]
        [When(@"I should see the (.*) element")]
        public void WhenIShouldSeeTheElement(string element_name)
        {
            IShoulSeeTheElement(element_name);
        }

        [TestMethod]
        [Then(@"I should see the (.*) element")]
        public void ThenIShouldSeeTheElement(string element_name)
        {
            IShoulSeeTheElement(element_name);
        }

        public void IShoulSeeTheElement(string element_name)
        {
            //Helper.WaitToDisappear("", "loading_message_locator");
            element_locator = AllPages.GetElementLocator(element_name);
            Assert.IsTrue(Helper.WaitToAppear(element_locator, element_name));

            Helper.TakeScreenShot();

            if (element_name.Contains("message"))
            {
                webelement = AllPages.GetWebElement(element_name);
                string message = webelement.Text;
                Console.WriteLine(message);

                if (element_name.Contains("success"))
                    Assert.IsTrue(message.Contains("Success:"));
            }

        }

        }

}
