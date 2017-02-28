using System;
using TechTalk.SpecFlow;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductTests.Common;

namespace ProdutcTests.Common.Steps.BackEnd
{
    [Binding]
    class DataBaseVerify
    {

            public static string verifyResultSubFolderName = SetUp.currentTestRestultDirectory +
                "\\QueryResults" + SetUp.systemTime + "\\";
            public static int queryCount = 1;
            public static string verifyResultFileName = null;
            public static string verifyResultPath = null;
            public static string XML_FILE = null;
            public static string verify_result_string;


            [TestMethod]
            [Given(@"The (.*) table (.*) should be (.*)")]
            [When(@"The (.*) table (.*) should be (.*)")]
            [Then(@"The (.*) table (.*) should be (.*)")]
        public void GivenTheTableShouldBe(string table, string searchfor, string value)
            {
                TheTableShouldBe(table, searchfor, value);
            }

           
            public void TheTableShouldBe(string table, string searchfor, string value)
            {
                if (!Directory.Exists(verifyResultSubFolderName))
                {
                    Directory.CreateDirectory(verifyResultSubFolderName);
                }

                string answerFileName = "result" + queryCount + ".xml";
                verifyResultPath = verifyResultSubFolderName + answerFileName;

                    XML_FILE = verifyResultPath;
                    Console.WriteLine(XML_FILE);
                    queryCount = queryCount + 1;

                string policyNumber = DataBaseRead.policy_query_result_string;

                string sqlQuery = "SELECT [" + searchfor + "] FROM [DartaUATR1].[dbo].[" + table + "] WHERE PolicyNumber=" + policyNumber;

                Console.WriteLine(sqlQuery);

                using (DataSet dataSet = new DataSet())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(sqlQuery, DataBase.connectionString))
                    {
                        sda.Fill(dataSet);
                    }

                    dataSet.WriteXml(XML_FILE);

                for (int i = 0; i< dataSet.Tables[0].Rows.Count; i++)
                {
                    string rowvalue = dataSet.Tables[0].Rows[i][searchfor].ToString();

                    verify_result_string = verify_result_string + rowvalue;

                }
            }

            Console.WriteLine(verify_result_string);
            Assert.IsTrue(verify_result_string.Contains(value));

            }

        [TestMethod]
        [Given(@"The query_result should be (.*)")]
        [When(@"The query_result should be (.*)")]
        [Then(@"The query_result should be (.*)")]
        public void QueryResultShouldBe(string value)
        {
            Assert.IsTrue(value == DataBaseRead.query_result_string);
        }

    }
    }
