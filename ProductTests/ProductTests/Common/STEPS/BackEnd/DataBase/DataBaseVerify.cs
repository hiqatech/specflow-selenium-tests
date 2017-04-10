using System;
using TechTalk.SpecFlow;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductTests.Utils;
using System.Collections.Generic;
using System.Linq;

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
            string where = "PolicyNumber" + DataBaseRW.curentPolicyNumber;
            DataBaseRW.ISelectInTheSQLDB(null, searchfor, where, table);
            string queryresult = DataBaseRW.queryResultString;

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

                dataValueDictionary = TableExtensions.TableToDictionary(table, 0);

                dataTableDictionay = TableExtensions.TableToDictionary(table, 1);

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

                Dictionary = TableExtensions.TableToDictionary(table, 1);
                TableExtensions.printDictionary(Dictionary);
        }

        [TestMethod]
        [Given(@"I compare the (.*) and (.*) query result files")]
        [When(@"I compare the (.*) and (.*) query result files")]
        [Then(@"I compare the (.*) and (.*) query result files")]
        public void GivenIComareTheFiles(string file1, string file2)
        {
            IComareTheFiles(file1, file2);
        }


        public void IComareTheFiles(string file1, string file2)
        {
            file1 = Path.Combine(DataBaseRW.queryResultSubFolderName, file1);
            file2 = Path.Combine(DataBaseRW.queryResultSubFolderName, file2);

            String[] lines1 = File.ReadAllLines(file1);
            String[] lines2 = File.ReadAllLines(file2);

            IEnumerable<String> CompareResult = lines2.Except(lines1);

            if (!File.Exists(DataBaseRW.compareResultPath))
                File.WriteAllLines(DataBaseRW.compareResultPath, CompareResult);
        }


        [TestMethod]
        [Given(@"The differencial comparation file by the query results should be empty")]
        [When(@"The differencial comparation file by the query results should be empty")]
        [Then(@"The differencial comparation file by the query results should be empty")]
        public void GivenTheComparationDifferencialFileShouldBeEmpty()
        {
            TheComparationDifferencialFileShouldBeEmpty();
        }

        public void TheComparationDifferencialFileShouldBeEmpty()
        {
            StreamReader sr = new StreamReader(DataBaseRW.compareResultPath);
            string contents = sr.ReadToEnd();
            if (!(contents.Length == 0))
                Console.WriteLine("Compared files are not identical on " + contents);
            Assert.IsTrue(contents.Length == 0);
        }


    }


}


