using TechTalk.SpecFlow;
using System;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProductTests.Common.Steps.FrontEnd
{

        [Binding]
        public class ShouldSee
        {
            public IWebElement webelement = null;
            public string element_locator;

        [TestMethod]
        [Given(@"I should see the (.*) element")]
        [When(@"I should see the (.*) element")]
        [Then(@"I should see the (.*) element")]
        public void GivenIShouldSeeTheElement(string element_name)
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
