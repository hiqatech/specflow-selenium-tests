using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProdutcTests.Common.Steps.BackEnd;
using ProductTests.Pages;

namespace ProductTests.Common.Steps.FrontEnd
{
    [Binding]

    public class Enters
    {
        public IWebElement webelement = null;

        [TestMethod]
        [Given(@"I enter (.*) as the (.*)")]
        [When(@"I enter (.*) as the (.*)")]
        [Then(@"I enter (.*) as the (.*)")]
        public void GivenIEnterAsThe(string entry, string element_name)
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
                entry = entry.Replace("system_date", "");
                entry = entry + ".00";
                double days = double.Parse(entry, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint);
                Console.WriteLine(days);
                Console.WriteLine(DataBase.system_date);
                Console.WriteLine(DataBase.system_date.AddDays(days));
                entry = DataBase.system_date.AddDays(days).ToString("ddMMyyyy");
                for (int i = 0; i < 10; i++) { webelement.SendKeys(Keys.ArrowLeft); }
            }

            if (entry.Contains("random"))
                entry = Helper.random_string;

            if (entry.Contains("query_result"))
            entry = DataBaseRead.query_result_string;

            webelement.SendKeys(entry);

        }
   
                    
    }
}
