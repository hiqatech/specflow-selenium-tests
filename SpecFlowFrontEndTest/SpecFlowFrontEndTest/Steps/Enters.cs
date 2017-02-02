using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using SpecFlowFrontEndTest.Pages;
using SpecFlowFrontEndTest.Common;
using OpenQA.Selenium;
using System.Globalization;

namespace SpecFlowFrontEndTest.Steps
{
    [Binding]

   
    public class Enters
    {
        public IWebElement webelement = null;

        [TestMethod]
        [Given(@"I enter (.*) as the (.*)")]
        public void GivenIEnterAsThe(string entry, string element_name)
        {
            IEnterAsThe(entry, element_name);
        }

        [TestMethod]
        [When(@"I enter (.*) as the (.*)")]
        public void WhenIEnterAsThe(string entry, string element_name)
        {
            IEnterAsThe(entry, element_name);
        }

        [TestMethod]
        [Then(@"I enter (.*) as the (.*)")]
        public void ThenIEnterAsThe(string entry, string element_name)
        {
            IEnterAsThe(entry, element_name);
        }


        public void IEnterAsThe(string entry, string element_name) {


            //Helper.WaitToDisappear("", "loading_message_locator");
            if (element_name.Contains("date_entry"))
                Helper.Sleep(1000);

            webelement = AllPages.GetWebElement(element_name);
            Helper.SafeClick(webelement, "safeclick");

            if (element_name.Contains("premium"))
            {
                for (int i = 0; i < 4; i++) { webelement.SendKeys(Keys.ArrowLeft); }
            }

            if (element_name.Contains("date_entry"))
            {
                entry = entry.Replace("current_date", "");
                entry = entry + ".00";
                double days = double.Parse(entry, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint);
                entry = DateTime.Now.AddDays(days).ToString("ddMMyyyy");
                for (int i = 0; i < 10; i++) { webelement.SendKeys(Keys.ArrowLeft); }
            }

            if (entry.Contains("random"))
                entry = Common.random_string;

            webelement.SendKeys(entry);

        }
   
                    
    }
}
