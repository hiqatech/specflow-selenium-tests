using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using SpecFlowFrontEndTest.Common;
using OpenQA.Selenium;
using SpecFlowFrontEndTest.Pages;

namespace SpecFlowFrontEndTest.Steps
{

    [Binding]

    
    public class ShouldNSees
    {
        public IWebElement webelement = null;
        public string element_locator;

        [TestMethod]
        [Given(@"I should not see the (.*) element")]
        public void GivenIShouldNotSeeTheElement(string element_name)
        {
            IShouldNotSeeTheElement(element_name);
        }

        [TestMethod]
        [When(@"I should not see the (.*) element")]
        public void WhenIShouldNotSeeTheElement(string element_name)
        {
            IShouldNotSeeTheElement(element_name);
        }

        [TestMethod]
        [Then(@"I should not see the (.*) element")]
        public void ThenIShouldNotSeeTheElement(string element_name)
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
