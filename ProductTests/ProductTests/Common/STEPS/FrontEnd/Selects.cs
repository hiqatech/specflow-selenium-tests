using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductTests.Common.Steps.FrontEnd;

namespace ProductTests.Common.Steps.FrontEnd
{

    [Binding]
    public class Selects
    {
        public IWebElement webelement;

        [TestMethod]
        [Given(@"I select the (.*) element")]
        [When(@"I select the (.*) element")]
        [Then(@"I select the (.*) element")]
        public void GivenISelectTheElement(string element_name)
        {
            ISelectTheElement(element_name);
        }

        public void ISelectTheElement(string element_name)
        {

            //Helper.WaitToDisappear("", "loading_message_locator");

            if (element_name.Contains("alert"))
            {
                SetUp.webDriver.SwitchTo().Alert().Accept();
                goto element_select_done;
            }


            if (element_name.Contains("selection") || (element_name.Contains("login")))
                Helper.Sleep(1000);

            webelement = AllPages.GetWebElement(element_name);

            Helper.SafeClick(webelement, "safeclick");
            if (element_name.Contains("confirm"))
                Helper.Sleep(2000);

            if (element_name.Contains("submit"))
                Helper.Sleep(1000);

            element_select_done:;

        }


    }

}