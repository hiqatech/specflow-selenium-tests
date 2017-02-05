using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using CrossEndSpecFlowTests.Common;

namespace CrossEndSpecFlowTests.Steps
{

    [Binding]
    class DataBaseRead
    {
        public static string queryResultSubFolderName = SetUp.currentTestRestultDirectory + 
            "\\QueryResults" + SetUp.systemTime + "\\";
        public static int queryCount = 1;
        public static string queryResultFileName = null;
        public static string queryResultPath = null;
        public static string compareResultName = "compareResult.txt";
        public static string compareResultPath = queryResultSubFolderName + compareResultName;
        public static string XML_FILE = null;
        public static string query_result_string;
        public static string policy_query_result_string;

        [TestMethod]
        [Given(@"I select (.*) way (.*) where (.*) from the (.*) table")]
        public void GivenISelectInTheSQLDB(string how, string searchfor, string where, string table)
        {
            ISelectInTheSQLDB(how, searchfor, where, table);
        }

        [TestMethod]
        [When(@"I select (.*) way (.*) where (.*) from the (.*) table")]
        public void WhenISelectInTheSQLDB(string how, string searchfor, string where, string table)
        {
            ISelectInTheSQLDB(how, searchfor, where, table);
        }

        [TestMethod]
        [Then(@"I select (.*) way (.*) where (.*) from the (.*) table")]
        public void ThenISelectInTheSQLDB(string how, string searchfor, string where, string table)
        {
            ISelectInTheSQLDB(how, searchfor, where, table);
        }

        public void ISelectInTheSQLDB(string how, string searchfor, string where, string table)
        {
            if (!Directory.Exists(queryResultSubFolderName))
            {
                Directory.CreateDirectory(queryResultSubFolderName);
            }

            string answerFileName = "answer" + queryCount + ".xml";
            queryResultPath = queryResultSubFolderName + answerFileName;

            if (queryCount == 1)
            {
                XML_FILE = queryResultPath;
                Console.WriteLine(XML_FILE);
                queryCount = queryCount + 1;
            }
            else if (queryCount == 2)
            {
                XML_FILE = queryResultPath;
                Console.WriteLine(XML_FILE);
            }
            else queryCount = 0;

            if (where.Contains("current") && where.Contains("PolicyNumber") )
            { where = where.Replace("current", policy_query_result_string); }

            if (where.Contains("current"))
            { where = where.Replace("current", query_result_string); }

            string sqlQuery = null;

            sqlQuery = "SELECT TOP 1 [" + searchfor + "] FROM [DartaUATR1].[dbo].[" + table + "] WHERE "
                + where;

            if (how == "random")
                sqlQuery = "SELECT TOP 1 [" + searchfor + "] FROM [DartaUATR1].[dbo].[" + table + "] WHERE "
                + where + " ORDER BY NEWID()";

            Console.WriteLine(sqlQuery);

            using (DataSet dataSet = new DataSet())
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(sqlQuery, DataBase.connectionString))
                {
                    sda.Fill(dataSet);
                }

                dataSet.WriteXml(XML_FILE);

                query_result_string = dataSet.Tables[0].Rows[0][searchfor].ToString();
            }

            if (searchfor == "PolicyNumber")
                policy_query_result_string = query_result_string;

            Console.WriteLine(query_result_string);
        }

       

    }
}
