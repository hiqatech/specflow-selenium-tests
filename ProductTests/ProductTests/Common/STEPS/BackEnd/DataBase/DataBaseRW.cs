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
    class DataBaseRW
    {
        public static string queryResultSubFolderName = SetUp.currentTestRestultDirectory + 
            "\\QueryResults" + SetUp.systemTime + "\\";
        public static int queryCount = 1;
        public static string queryResultFileName = null;
        public static string queryResultPath = null;
        public static string compareResultName = "compareResult.txt";
        public static string compareResultPath = queryResultSubFolderName + compareResultName;
        public static string XML_FILE = null;
        public static string queryResultString;
        public static string queryResultKeyValue;
        public static string curentPolicyNumber;

        [TestMethod]
        [Given(@"I select (.*) way (.*) where (.*) from the (.*) table")]
        [When(@"I select (.*) way (.*) where (.*) from the (.*) table")]
        [Then(@"I select (.*) way (.*) where (.*) from the (.*) table")]
        public void GivenISelectInTheSQLDB(string how, string searchfor, string where, string table)
        {
            ISelectInTheSQLDB(how, searchfor, where, table);
        }

        public static void ISelectInTheSQLDB(string how, string searchfor, string where, string table)
        {
            if (!Directory.Exists(queryResultSubFolderName))
            {
                Directory.CreateDirectory(queryResultSubFolderName);
            }

            string answerFileName = "answer" + queryCount + ".xml";
            queryResultPath = queryResultSubFolderName + answerFileName;

                XML_FILE = queryResultPath;
                Console.WriteLine(XML_FILE);
                queryCount = queryCount + 1;

            //if (where.Contains("current") && where.Contains("PolicyNumber") )
            //{ where = where.Replace("current", "PolicyNumber='"policy_query_result_string'); }

            string sqlQuery = null;

            sqlQuery = "SELECT TOP 1 [" + searchfor + "] FROM ["+DataBase.currentDataBase+"].[dbo].[" + table + "] WHERE "
                + where;

            if (how == "random")
                sqlQuery = "SELECT TOP 1 [" + searchfor + "] FROM [DartaUATR1].[dbo].[" + table + "] WHERE "
                + where + " ORDER BY NEWID()";

            //Console.WriteLine(sqlQuery);

            queryResultString = DataBaseRunQuery(sqlQuery, searchfor, XML_FILE);

            if (searchfor == "PolicyNumber")
                curentPolicyNumber = queryResultString;

            //Console.WriteLine(queryResultString);
        }

        [TestMethod]
        [Given(@"I update (.*) with (.*) value where (.*) is (.*) from the (.*) table")]
        [When(@"I update (.*) with (.*) value where (.*) is (.*) from the (.*) table")]
        [Then(@"I update (.*) with (.*) value where (.*) is (.*) from the (.*) table")]
        public void GivenIUpdateInTheSQLDB(string update, string to, string where, string value, string table)
        {
            IUpdateInTheSQLDB(update, to, where, value, table);
        }

        public void IUpdateInTheSQLDB(string update, string to, string where, string value, string table)
        {
            string connectionString = "Data Source = " + DataBase.currentDataServer + "; Initial Catalog = " + DataBase.currentDataBase + "; Integrated Security = True";


            string sqlQuery = "UPDATE [RegressionTests].[dbo].[" + table + "] SET " + update + "= '" +
                to + "'  WHERE " + where + "='" + value + "'";

            using (SqlDataAdapter sda = new SqlDataAdapter(sqlQuery, connectionString))
                Console.WriteLine(sqlQuery);

        }

        public static string DataBaseRunQuery(string sqlQuery, string searchFor, string saveToPath)
        {
            string connectionString = "Data Source = " + DataBase.currentDataServer + "; Initial Catalog = " + DataBase.currentDataBase + "; Integrated Security = True";
            string resultString = null;

            using (DataSet dataSet = new DataSet())
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(sqlQuery, connectionString))
                {
                    sda.Fill(dataSet);
                }
                if (!(saveToPath == null))
                {
                    dataSet.WriteXml(saveToPath);
                }
                try
                {
                    resultString = dataSet.Tables[0].Rows[0][searchFor].ToString();
                    queryResultKeyValue = searchFor + " " + resultString;
                }
                catch
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        resultString = dataSet.Tables[0].Rows[i][searchFor].ToString();
                        queryResultKeyValue = searchFor + " " + resultString;
                    }

                }
            }

            return resultString;
        }


        public static DataTable GetDataFromDBToDataTable(string sqlQuery, string dataBase)
        {
            string connectionString = "Data Source = " + DataBase.currentDataServer + "; Initial Catalog = " + DataBase.currentDataBase + "; Integrated Security = True";
            DataTable dataTable = new DataTable();

            using (DataSet dataSet = new DataSet())
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(sqlQuery, connectionString))
                {
                    sda.Fill(dataSet);
                }

                dataTable = dataSet.Tables[0];

            }
            return dataTable;        
        }

    }
}
