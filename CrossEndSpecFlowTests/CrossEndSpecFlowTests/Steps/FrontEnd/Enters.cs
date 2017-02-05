using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using CrossEndSpecFlowTests.Common;
using OpenQA.Selenium;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;

namespace CrossEndSpecFlowTests.Steps
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
                entry = entry.Replace("system_date", "");
                entry = entry + ".00";
                double days = double.Parse(entry, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint);
                string system_date_string = null;

                string sqlQuery = "SELECT TOP 1[SystemDate] FROM [DartaUATR1].[schedule].[OnlineControl]";
                using (DataSet dataSet = new DataSet())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(sqlQuery, DataBase.connectionString))
                    {
                        sda.Fill(dataSet);
                    }

                    system_date_string = dataSet.Tables[0].Rows[0]["SystemDate"].ToString();

                }

                system_date_string = system_date_string.Remove(system_date_string.Length - 12);

                DateTime system_date = DateTime.ParseExact(system_date_string, new[] { "M/d/yyyy", "MM/d/yyyy", "M/dd/yyyy", "MM/dd/yyyy" },
                CultureInfo.InvariantCulture,
                DateTimeStyles.None);

                Console.Write(system_date_string);
                entry = system_date.AddDays(days).ToString("ddMMyyyy");
                for (int i = 0; i < 10; i++) { webelement.SendKeys(Keys.ArrowLeft); }
            }

            if (entry.Contains("random"))
                entry = Common.random_string;

            if (entry.Contains("query_result"))
            entry = DataBaseRead.query_result_string;

            Console.WriteLine(DataBaseRead.query_result_string);

            webelement.SendKeys(entry);

        }
   
                    
    }
}
