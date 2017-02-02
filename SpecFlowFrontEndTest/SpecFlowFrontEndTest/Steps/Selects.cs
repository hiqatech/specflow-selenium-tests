﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using SpecFlowFrontEndTest.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SpecFlowFrontEndTest.Pages;

namespace SpecFlowFrontEndTest.Steps
{

    [Binding]

    
       
    public class Selects
    {
        public IWebElement webelement;

        [TestMethod]
        [Given(@"I select the (.*) element")]
        public void GivenISelectTheElement(string element_name)
        {
            ISelectTheElement(element_name);
        }

        [TestMethod]
        [When(@"I select the (.*) element")]
        public void WhenISelectTheElement(string element_name)
        {
            ISelectTheElement(element_name);
        }

        [TestMethod]
        [Then(@"I select the (.*) element")]
        public void ThenISelectTheElement(string element_name)
        {
            ISelectTheElement(element_name);
        }


        public void ISelectTheElement(string element_name)
        {

            //Helper.WaitToDisappear("", "loading_message_locator");

            if (element_name.Contains("alert"))
            {
                SetUp.driver.SwitchTo().Alert().Accept();
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