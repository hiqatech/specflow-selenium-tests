using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ZolCo.ProductTests.Utilities;

namespace ZolCo.ProductTests.Common
{
    [Binding]
    [TestClass]
    public class DataBaseRWCD
    {
        public static DateTime systemDate;

        [Given(@"I connect to the (.*) server (.*) database")]
        [When(@"I connect to the (.*) server (.*) database")]
        [Then(@"I connect to the (.*) server (.*) database")]
        public static void IConnectToTheDatabase()
        {
            string actualDBPreference;

            if (SetUp.currentEnvironment["Environment"] == "TA")
            {
                actualDBPreference = "DartaUAT";
            }
            else
            {
                actualDBPreference = "";
            }
            string sqlQuery = "SELECT TOP 1[SystemDate] FROM [" + actualDBPreference + SetUp.currentEnvironment["DataBase"] + "].[schedule].[OnlineControl]";
            string connectionString = SetUp.testDBConnectionString;
            systemDate = DBHelp.GetSystemDate(connectionString, sqlQuery);
        }


        [Given(@"I restore the test database")]
        [When(@"I restore the test database")]
        [Then(@"I restore the test database")]
        public static void IRestoreTheDatabase()
        {
            string Comm1 = null;
            string Comm2 = "ALTER DATABASE " + SetUp.currentEnvironment["DataBase"] + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
            string Comm3 = null;
            string Comm4 = "ALTER DATABASE " + SetUp.currentEnvironment["DataBase"] + " SET MULTI_USER;";

            if (SetUp.currentEnvironment["Environment"] == "DEV")
            {
                Comm1 = @"RESTORE FILELISTONLY FROM DISK = N'M:MSSQL\Backups\" + SetUp.currentEnvironment["DataBase"] + ".bak';";
                Comm3 = "USE MASTER; RESTORE DATABASE " + SetUp.currentEnvironment["DataBase"] + @" FROM DISK = N'M:MSSQL\Backups\" + SetUp.currentEnvironment["DataBase"] + ".bak' WITH REPLACE , RECOVERY;";
            }
            if (SetUp.currentEnvironment["Environment"] == "UAT")
            {
                Comm1 = @"RESTORE FILELISTONLY FROM DISK = N'H:MSSQL\Backups\" + SetUp.currentEnvironment["DataBase"] + ".bak';";
                Comm3 = "USE MASTER; RESTORE DATABASE " + SetUp.currentEnvironment["DataBase"] + @" FROM DISK = N'H:MSSQL\Backups\" + SetUp.currentEnvironment["DataBase"] + ".bak' WITH REPLACE , RECOVERY;";
            }
            if (SetUp.currentEnvironment["Environment"] == "TA")
            {
                Comm1 = @"RESTORE FILELISTONLY FROM DISK = N'M:MSSQL\Backups\" + SetUp.currentEnvironment["DataBase"] + ".bak';";
                Comm2 = "ALTER DATABASE DartaUAT" + SetUp.currentEnvironment["DataBase"] + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                Comm3 = "USE MASTER; RESTORE DATABASE DartaUAT" + SetUp.currentEnvironment["DataBase"] + @" FROM DISK = N'M:MSSQL\Backups\" + SetUp.currentEnvironment["DataBase"] + ".bak' WITH REPLACE , RECOVERY;";
                Comm4 = "ALTER DATABASE DartaUAT" + SetUp.currentEnvironment["DataBase"] + " SET MULTI_USER;";
            }
            string connectionString = SetUp.testDBConnectionString;

            DBHelp.RestoreDataBase(Comm1, Comm2, Comm3, Comm4, connectionString);
        }

        [Given(@"I select (.*) way (.*) where (.*) from the (.*) table")]
        [When(@"I select (.*) way (.*) where (.*) from the (.*) table")]
        [Then(@"I select (.*) way (.*) where (.*) from the (.*) table")]
        public static void GivenISelectFromTheSQLDB(string how, string searchfor, string where, string table)
        {
            string query_result_string = null;
            string queryResultPath = null;
            string XML_FILE_PATH = null;
            int queryCount = 1;

            string sqlQuery = "SELECT TOP 1 [" + searchfor + "] FROM " + SetUp.actualDBSQLPrefix + "[" + table + "] WHERE "
                 + where;

            if (how == "random")
                sqlQuery = "SELECT TOP 1 [" + searchfor + "] FROM " + SetUp.actualDBSQLPrefix + "[" + table + "] WHERE "
                + where + " ORDER BY NEWID()";

            string answerFileName = "answer" + queryCount + ".xml";
            queryResultPath = SetUp.defaultTestRestultDirectory + answerFileName;
            XML_FILE_PATH = queryResultPath;
            queryCount = queryCount + 1;

            if (where.Contains("current"))
            { where = where.Replace("current", query_result_string); }

            query_result_string = DBHelp.ReadDataBase(sqlQuery, SetUp.testDBConnectionString, searchfor, XML_FILE_PATH);
            Console.WriteLine("query_result_string " + query_result_string);
        }
    

        [Given(@"I update (.*) with (.*) value where (.*) is (.*) from the (.*) table")]
        [When(@"I update (.*) with (.*) value where (.*) is (.*) from the (.*) table")]
        [Then(@"I update (.*) with (.*) value where (.*) is (.*) from the (.*) table")]
        public static void GivenIUpdateInTheSQLDB(string update, string to, string where, string value, string table)
        {
            string sqlQuery = "UPDATE " + SetUp.actualDBSQLPrefix + "[" + table + "] SET " + update + "= '" +
                to + "'  WHERE " + where + "='" + value + "'";
            string connectionString = SetUp.testDBConnectionString;
            DBHelp.WriteDataBase(connectionString,sqlQuery);
        }


        [Given(@"I write excel (.*) sheet into the input database")]
        [When(@"I write excel (.*) sheet into the input database")]
        [Then(@"I write excel (.*) sheet into the input database")]
        public static void GivenIWriteSheetToDataBase(string sheetName)
        {      
            string excelPath = SetUp.testProjectDirectory + "ProductTestsInputs.xlsx";
            string dataBaseTable = "[ProductsDartaTestsInputs].[dbo].[" + sheetName + "]";
            DBHelp.ExcelToDataBase(SetUp.testDBConnectionString, dataBaseTable, excelPath, sheetName);
        }

       

    }
}

