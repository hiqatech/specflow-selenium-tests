using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProductTests.Common.Steps.FrontEnd
{

    [Binding]

    public class ShouldNSees
    {
        public IWebElement webelement = null;
        public string element_locator;

        [TestMethod]
        [Given (@"I should not see the (.*) element")]
        [When(@"I should not see the (.*) element")]
        [Then(@"I should not see the (.*) element")]
        public void GivenIShouldNotSeeTheElement(string element_name)
        {
            IShouldNotSeeTheElement(element_name);
        }

        
        public void IShouldNotSeeTheElement(string element_name)
        {
            //Helper.WaitToDisappear("", "loading_message_locator");
            element_locator = AllPages.GetElementLocator(element_name);
            Assert.IsTrue(Helper.WaitToDisappear(element_locator, element_name));
            Helper.TakeScreenShot();

        }

    }
}
