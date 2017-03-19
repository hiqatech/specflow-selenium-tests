using System;
using TechTalk.SpecFlow;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductTests.Common;
using ProductTests.Utils;
using System.Collections.Generic;

namespace ProdutcTests.Common.Steps.BackEnd
{
    [Binding]
    class DataBaseVerify
    {

        [TestMethod]
        [Given(@"The (.*) table (.*) should be (.*)")]
        [When(@"The (.*) table (.*) should be (.*)")]
        [Then(@"The (.*) table (.*) should be (.*)")]
        public void GivenTheTableShouldBe(string table, string searchfor, string value)
        {
            string where = "PolicyNumber" + DataBaseRead.curentPolicyNumber;
            DataBaseRead.ISelectInTheSQLDB(null, searchfor, where, table);
            string queryresult = DataBaseRead.query_result_string;

            Console.WriteLine(queryresult);
            Assert.IsTrue(queryresult.Contains(value));

        }

        [TestMethod]
        [Given(@"The policy (.*) database entries should be")]
        [When(@"The policy (.*) database entries should be")]
        [Then(@"The policy (.*) database entries should be")]
        public static void VerifyDBEntrys(string policyNo, Table table)
        {
            Dictionary<string, string> dataValueDictionary = new Dictionary<string, string>();
            Dictionary<string, string> dataTableDictionay = new Dictionary<string, string>();
            Dictionary<string, string> PolicyNumberDictionary = new Dictionary<string, string>();

                dataValueDictionary = TableExtensions.DataToDictionary(table, 0);

                dataTableDictionay = TableExtensions.DataToDictionary(table, 1);

                TableExtensions.fillPolicyNumberDictionary();
                PolicyNumberDictionary = TableExtensions.PolicyNumberDictionary;

                List<string> policykeys = new List<string>(PolicyNumberDictionary.Keys);
                List<string> datakeys = new List<string>(dataValueDictionary.Keys);

                string where = "PolicyNumber='"+ PolicyNumberDictionary[policyNo] + "'";

                foreach (var datakey in datakeys)
                {
                    string searchfor = datakey;
                    string databasetable = dataTableDictionay[datakey];
                    string value = dataValueDictionary[datakey];

                    //DataBaseRead.ISelectInTheSQLDB(null, searchfor, where, databasetable);
                    //string queryresult = DataBaseRead.query_result_string;
                    Console.WriteLine("I search for " + searchfor + " where " + where + " in the tabledata "+ databasetable+" and this should be "+ value);
                    //Console.WriteLine(queryresult);
                    //Assert.IsTrue(queryresult.Contains(value));

                }

            }


        [TestMethod]
        [Given(@"I have a (.*) table")]
        [When(@"I have a (.*) table")]
        [Then(@"I have a (.*) table")]
        public static void IHaveATable( Table table)
        {

            Dictionary<string, string> Dictionary = new Dictionary<string, string>();

                Dictionary = TableExtensions.DataToDictionary(table, 1);
                TableExtensions.printDictionary(Dictionary);
        }
    

   }


}


