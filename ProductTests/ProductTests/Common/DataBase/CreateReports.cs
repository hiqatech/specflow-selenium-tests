using System;
using System.Data;
using TechTalk.SpecFlow;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ZolCo.ProductTests.Utilities;

namespace ZolCo.ProductTests.Common
{
    [Binding]
    [TestClass]
    public class CreateReports
    {
        public static DataTable testReportDataTable = new DataTable();

        public static void CreateLowLevelReport(string newReportName, string connectionString, string createCommand, string populateCommand)
        {
            if (!(DBHelp.DataBaseTableExists(connectionString, newReportName)))
            {
                DBHelp.CreateNewDataTable(connectionString, newReportName, createCommand);
            }
            if (SetUp.currentEnvironment["AppToTest"] == "BusinessServices")
            {
                foreach (DataRow testReportDataRow in testReportDataTable.Rows)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = connection;
                            command.CommandText = populateCommand;
                            command.Parameters.AddWithValue("@TestEnvironment", testReportDataRow["TestEnvironment"]);
                            command.Parameters.AddWithValue("@TestSuiteRunDate", testReportDataRow["TestSuiteRunDate"]);
                            command.Parameters.AddWithValue("@TestScenarioRunDate", testReportDataRow["TestScenarioRunDate"]);
                            command.Parameters.AddWithValue("@TestSuiteType", testReportDataRow["TestSuiteType"]);
                            command.Parameters.AddWithValue("@TestFeature", testReportDataRow["TestFeature"]);
                            command.Parameters.AddWithValue("@TestScenario", testReportDataRow["TestScenario"]);
                            command.Parameters.AddWithValue("@TransactionName", testReportDataRow["TransactionName"]);
                            command.Parameters.AddWithValue("@PolicyNumber", testReportDataRow["PolicyNumber"]);
                            command.Parameters.AddWithValue("@DataTable", testReportDataRow["DataTable"]);
                            command.Parameters.AddWithValue("@TestedField", testReportDataRow["TestedField"]);
                            command.Parameters.AddWithValue("@BatchRunId", testReportDataRow["BatchRunId"]);
                            command.Parameters.AddWithValue("@ExpectedValue", testReportDataRow["ExpectedValue"]);
                            command.Parameters.AddWithValue("@ActualValue", testReportDataRow["ActualValue"]);
                            command.Parameters.AddWithValue("@TestResult", testReportDataRow["TestResult"]);
                            command.Parameters.AddWithValue("@Info", testReportDataRow["Info"]);

                            try
                            {
                                connection.Open();
                                command.ExecuteNonQuery();
                            }
                            catch (SqlException ex)
                            {
                                Console.WriteLine("SQL Database Write problem when Writing ProductTestReports Details " + ex.ToString());
                            }
                        }
                        connection.Close();
                    }
                }
            }
        }


        public static void CreateHighLevelReport(string successRate)
        {
            DataTable highReportDataTable = new DataTable();
            highReportDataTable = testReportDataTable.Clone();
            DataRow reportRow = highReportDataTable.NewRow();
            reportRow = testReportDataTable.Rows[1];
            highReportDataTable.ImportRow(reportRow);
            highReportDataTable.Columns.Remove("PolicyNumber");
            highReportDataTable.Columns.Remove("DataTable");
            highReportDataTable.Columns.Remove("TestedField");
            highReportDataTable.Columns.Remove("BatchRunId");
            highReportDataTable.Columns.Remove("ExpectedValue");
            highReportDataTable.Rows[0]["TransactionName"] = SetUp.scenarioTitleSections["transaction"];
            highReportDataTable.Rows[0]["TestResult"] = successRate;
            highReportDataTable.Rows[0]["Info"] = "Success Rate is " + successRate;
            highReportDataTable.Rows[0]["TestResult"] = "PASSED";

            foreach (DataRow resultRow in testReportDataTable.Rows)
            {
                if (resultRow["TestResult"].ToString() == "FAILED")
                    highReportDataTable.Rows[0]["TestResult"] = "FAILED";
            }

            string newReportName = "[Products" + SetUp.scenarioTitleSections["client"] + "TestsReports].[dbo].[TestReports " + SetUp.testStartTime + "-HighLevel]";
            string commandString = "CREATE TABLE " + newReportName + " (TestEnvironment nvarchar(MAX),TestSuiteRunDate nvarchar(MAX),TestScenarioRunDate nvarchar(MAX),"
                        + "TestSuiteType nvarchar(MAX),TestFeature nvarchar(MAX), TestScenario nvarchar(MAX), TransactionName nvarchar(MAX), TestResult nvarchar(MAX), Info nvarchar(MAX));";
            
            if (!(DBHelp.DataBaseTableExists(SetUp.testDBConnectionString, newReportName)))
            {
                    DBHelp.CreateNewDataTable(SetUp.testDBConnectionString, newReportName, commandString);
            }

            string insertIntoDataTable = "INSERT INTO" + newReportName + " (TestEnvironment,TestSuiteRunDate,TestScenarioRunDate,TestSuiteType,TestFeature," +
                "TestScenario,TransactionName,TestResult,Info)"
                + "VALUES (@TestEnvironment,@TestSuiteRunDate,@TestSuiteType,@TestScenarioRunDate, @TestFeature, @TestScenario,@TransactionName,@TestResult,@Info)";

            using (SqlConnection connection = new SqlConnection(SetUp.testDBConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = insertIntoDataTable;
                    command.Parameters.AddWithValue("@TestEnvironment", highReportDataTable.Rows[0]["TestEnvironment"]);
                    command.Parameters.AddWithValue("@TestSuiteRunDate", highReportDataTable.Rows[0]["TestSuiteRunDate"]);
                    command.Parameters.AddWithValue("@TestScenarioRunDate", highReportDataTable.Rows[0]["TestScenarioRunDate"]);
                    command.Parameters.AddWithValue("@TestSuiteType", highReportDataTable.Rows[0]["TestSuiteType"]);
                    command.Parameters.AddWithValue("@TestFeature", highReportDataTable.Rows[0]["TestFeature"]);
                    command.Parameters.AddWithValue("@TestScenario", highReportDataTable.Rows[0]["TestScenario"]);
                    command.Parameters.AddWithValue("@TransactionName", highReportDataTable.Rows[0]["TransactionName"]);
                    command.Parameters.AddWithValue("@TestResult", highReportDataTable.Rows[0]["TestResult"]);
                    command.Parameters.AddWithValue("@Info", highReportDataTable.Rows[0]["Info"]);
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("SQL Database Write problem when Writing ProductTestReports Details " + ex.ToString());
                    }

                    
                }
            connection.Close();
            }
        }

        public static string calculateTestResults(string newReportName, string connectionString)
        {
            newReportName = "[ProductsDartaTestsReports].[dbo].[" + newReportName + "]";
            string transactionSuccessRate = "TestSuccessRate : ";
            List<string> transactionsList = new List<string>();
            string transactionsQuery = "Select DISTINCT TransactionName FROM " + newReportName;
            DataSet transactionsDataSet = new DataSet();
            transactionsDataSet = DBHelp.RunQueryToDataSet(transactionsQuery, connectionString);

            foreach (DataTable table in transactionsDataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        object item = row[column];
                        transactionsList.Add(item.ToString());
                    }
                }
            }

            foreach (string transaction in transactionsList)
            {
                string allQuery = "Select COUNT(TestResult) FROM " + newReportName + "WHERE TransactionName = '" + transaction + "'";
                decimal all = decimal.Parse(DBHelp.RunQueryToString(allQuery, connectionString));
                string successQuery = "Select COUNT(TestResult) FROM " + newReportName + "WHERE TestResult = 'PASSED' AND TransactionName = '" + transaction + "'";
                decimal success = decimal.Parse(DBHelp.RunQueryToString(successQuery, connectionString));
                string successRate = ((success / all) * 100).ToString().Substring(0, 5);
                transactionSuccessRate = transactionSuccessRate + transaction + " = " + successRate + "; ";
            }
            return transactionSuccessRate;
        }

        public static void CreateReportTable()
        {
            if (testReportDataTable.Columns.Count == 0)
            {
                testReportDataTable.Clear();
                testReportDataTable.Columns.Add("TestEnvironment");
                testReportDataTable.Columns.Add("TestSuiteRunDate");
                testReportDataTable.Columns.Add("TestScenarioRunDate");
                testReportDataTable.Columns.Add("TestSuiteType");                
                testReportDataTable.Columns.Add("TestFeature");
                testReportDataTable.Columns.Add("TestScenario");
                testReportDataTable.Columns.Add("TransactionName");
                testReportDataTable.Columns.Add("PolicyNumber");
                testReportDataTable.Columns.Add("DataTable");
                testReportDataTable.Columns.Add("TestedField");
                testReportDataTable.Columns.Add("BatchRunId");
                testReportDataTable.Columns.Add("ExpectedValue");
                testReportDataTable.Columns.Add("ActualValue");
                testReportDataTable.Columns.Add("TestResult");
                testReportDataTable.Columns.Add("Info");
            }
        }

        public static DataRow PopulateReportRow(DataRow reportDataRow, DataRow verifyDataRow,string transaction)
        {
            reportDataRow["TestEnvironment"] = SetUp.currentEnvironment["EnvironmentString"] + "-" + SetUp.currentEnvironment["AppToTest"];
            reportDataRow["TestSuiteRunDate"] = SetUp.testStartTime;
            reportDataRow["TestSuiteType"] = verifyDataRow["TestType"];
            reportDataRow["TestScenarioRunDate"] = DateTime.Now.ToString("yyyy-MM-ddTHHmmss");
            reportDataRow["TestFeature"] = SetUp.scenarioTitleSections["feature"];
            reportDataRow["TestScenario"] = SetUp.scenarioTitleSections["scenario"];
            reportDataRow["TransactionName"] = transaction;
            reportDataRow["PolicyNumber"] = verifyDataRow["WherePolicyNumber"];
            reportDataRow["DataTable"] = verifyDataRow["From"];
            reportDataRow["ExpectedValue"] = verifyDataRow["ExpectedValue"];
            reportDataRow["TestedField"] = verifyDataRow["Select"];
            reportDataRow["BatchRunId"] = verifyDataRow["BatchRunId"];
            reportDataRow["Info"] = "";
            return reportDataRow;
        }

    }
}

