using System;
using System.Data;
using System.Xml;
using TechTalk.SpecFlow;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ZolCo.ProductTests.Utilities;

namespace ZolCo.ProductTests.Common
{
    [Binding]
    [TestClass]
    public class ServiceSteps
    {
        public static DataTable testCaptureDataTable = new DataTable();
        public static DataTable testVerifyDataTable = new DataTable();

        [Given(@"I read data from the (.*) (.*) capture table for the requests")]
        [When(@"I read data from the (.*) (.*) capture table for the requests")]
        [Then(@"I read data from the (.*) (.*) capture table for the requests")]
        public static void IReadTestCaptureDataBase(string source, string transaction)
        {
            string captureSqlQuery = null;

            if (source == "Excel")
            {
                captureSqlQuery = "SELECT * FROM [" + transaction + "Capture$]";
                string excelPath = SetUp.testProjectDirectory + SetUp.scenarioTitleSections["client"] + "Inputs.xlsx";
                testCaptureDataTable = DataHelp.ExcelSheetToDataTable(excelPath, captureSqlQuery);
            }
            else if (source == "DataBase")
            {
                string dataBase = "Products" + SetUp.scenarioTitleSections["client"] + "TestsInputs";
                string connection = "Data Source = WINDEVAD0400; Initial Catalog =" + dataBase + "; Integrated Security = True";
                captureSqlQuery = "SELECT * FROM [" + dataBase + "].[dbo].[" + transaction + "Capture]";
                testCaptureDataTable = DBHelp.RunQueryToDataTable(connection, dataBase, captureSqlQuery);
            }
        }

        [Given(@"I create (.*) requests from this data and send to the services")]
        [When(@"I create (.*) requests from this data and send to the services")]
        [Then(@"I create (.*) requests from this data and send to the services")]
        public static void ICreateAndSendRequests(string requestName)
        {
            XmlDocument serviceRequestObjectXML = null;
            Dictionary<string, string> requestDictionary;
            object[] paramsArray = null;
            string requestPath = SetUp.testProjectDirectory + @"\ProductTestsSolution\Test\Darta\Templates\" + requestName + ".xml";

            foreach (DataRow captureDataRow in testCaptureDataTable.Rows)
            {
                if (!(captureDataRow[1].ToString() == ""))
                {
                    requestDictionary = new Dictionary<string, string>();
                    requestDictionary = DataHelp.DataTableToDictionary(testCaptureDataTable, captureDataRow);
                    requestDictionary = DataHelp.AddDataToDisctionary(DataBaseRWCD.systemDate,requestDictionary);

                    if (requestName == "AddProposal")
                    {
                        string sqlQuery = "DELETE " + SetUp.actualDBSQLPrefix + "[EL_ExternalReference] WHERE Reference = '" + requestDictionary["ProposalNumber"] + "'";
                        DBHelp.DeleteFromDataBase(sqlQuery, SetUp.testDBConnectionString);
                    }
                    serviceRequestObjectXML = DataHelp.GetRequestXMLWithData(requestPath, requestDictionary);
                    paramsArray = ServHelp.GetServiceObject("ServiceConfig.Configuration.currentServiceName", "ServiceConfig.Configuration._currentConfiguration", "ServiceConfig.Configuration.MessageData", requestName, serviceRequestObjectXML);
                    ServHelp.SendServiceObject(paramsArray);
                }
            }

        }

        [Given(@"I run the batch process for (.*) days")]
        [When(@"I run the batch process for (.*) days")]
        [Then(@"I run the batch process for (.*) days")]
        public static void IRunTheBatchProcessWithService(string days)
        {
            if (!(days == "0"))
            {
                string batchStartDateQuery = "Select Top 1 ThisRunDate FROM " + SetUp.actualDBSQLPrefix + "[RU_RunControl]";
                string batchStartDate = DBHelp.RunQueryToString(batchStartDateQuery, SetUp.testDBConnectionString);
                DateTime batchRunDateFrom = Convert.ToDateTime(batchStartDate);

                string regionName = null;
                if (SetUp.currentEnvironment["DataBase"] == "R6")
                    regionName = "UA6BATCH";
                else if (SetUp.currentEnvironment["DataBase"] == "?")
                    regionName = "?";
                string clientName = SetUp.scenarioTitleSections["client"];
                string userName = "X495";

                string outputMessage = ServHelp.StartRegion(clientName, regionName, userName);


                string dataBase = SetUp.currentEnvironment["DataBase"];
                DateTime RunFromDate = Convert.ToDateTime(batchRunDateFrom.ToString("MM/dd/yyyy"));
                DateTime RunToDate = RunFromDate.AddDays(double.Parse(days));
                var batchResponse = ServHelp.RunBatch(clientName, regionName, dataBase, RunFromDate, RunToDate, userName);
                int jobNumber = batchResponse.JobResponses[0].JobNumber;

                ServHelp.WaitBatchToComplete(clientName, regionName, jobNumber, userName);
            }
        }


        [Given(@"I read data from the (.*) (.*) verify table where the BatchRunId (.*) to verify")]
        [When(@"I read data from the (.*) (.*) verify table where the BatchRunId (.*) to verify")]
        [Then(@"I read data from the (.*) (.*) verify table where the BatchRunId (.*) to verify")]
        public static void IReadTestVerifyDataBase(string source, string transaction, string batchRunId)
        {
            CreateReports.CreateReportTable();
            string verifySqlQuery = null;

            if (source == "Excel")
            {
                verifySqlQuery = "SELECT * FROM [" + transaction + "Verify$] WHERE BatchRunId ='" + batchRunId + "'";
                string excelPath = SetUp.testProjectDirectory + "ProductTestsInputs.xlsx";
                testVerifyDataTable = DataHelp.ExcelSheetToDataTable(excelPath, verifySqlQuery);
            }
            else if (source == "DataBase")
            {
                string dataBase = "Products" + SetUp.scenarioTitleSections["client"] + "TestsInputs";
                string connection = "Data Source = WINDEVAD0400; Initial Catalog =" + dataBase + "; Integrated Security = True";
                verifySqlQuery = "SELECT * FROM [" + dataBase + "].[dbo].[" + transaction + "Verify] WHERE BatchRunId ='" + batchRunId + "'";
                testVerifyDataTable = DBHelp.RunQueryToDataTable(connection, dataBase, verifySqlQuery);
            }
            foreach (DataRow verifyDataRow in testVerifyDataTable.Rows)
            {
                if (!(verifyDataRow[1].ToString() == ""))
                {
                    DataRow reportDataRow = CreateReports.testReportDataTable.NewRow();
                    reportDataRow = CreateReports.PopulateReportRow(reportDataRow, verifyDataRow, transaction);
                    CreateReports.testReportDataTable.Rows.Add(reportDataRow);
                }
            }
        }


        [Given(@"I verify the values in the database by the test expected values")]
        [When(@"I verify the values in the database by the test expected values")]
        [Then(@"I verify the values in the database by the test expected values")]
        public static void VerifyDataBaseEntries()
        {
            for (int i = 0; i < testVerifyDataTable.Rows.Count; i++)
            {
                DataRow verifyDataRow = testVerifyDataTable.Rows[i];
                verifyDataRow = DataHelp.AddDataToDataRow(DataBaseRWCD.systemDate, testVerifyDataTable, verifyDataRow);
                DataRow reportDataRow = CreateReports.testReportDataTable.Rows[i];
                reportDataRow = DataHelp.AddDataToDataRow(DataBaseRWCD.systemDate, CreateReports.testReportDataTable, reportDataRow);

                if (reportDataRow["Info"].ToString() == "")
                {
                    string actualSqlQuery = "SELECT " + verifyDataRow["Select"] + " FROM " + SetUp.actualDBSQLPrefix + verifyDataRow["From"] +
                        " WHERE PolicyNumber ='" + verifyDataRow["WherePolicyNumber"] + "' " + verifyDataRow["ExtraWhere"] + " ";

                    if (actualSqlQuery.Contains("MEMO"))
                        actualSqlQuery = "SELECT CONVERT (VARCHAR (MAX),MEMO) as MEMO FROM " + SetUp.actualDBSQLPrefix + verifyDataRow["From"] +
                        " WHERE PolicyNumber ='" + verifyDataRow["WherePolicyNumber"] + "' " + verifyDataRow["ExtraWhere"] + " ";

                    else if (verifyDataRow["From"].ToString().Contains("JOIN"))
                    {
                        actualSqlQuery = "SELECT " + verifyDataRow["Select"] + " FROM " + SetUp.actualDBSQLPrefix + verifyDataRow["From"] +
                        " WHERE " + verifyDataRow["WherePolicyNumber"] + " " + verifyDataRow["ExtraWhere"] + " ";
                        string sqlpart1 = actualSqlQuery.Substring(0, (actualSqlQuery.IndexOf("JOIN") + "JOIN ".Length)) + SetUp.actualDBSQLPrefix;
                        string sqlpart2 = actualSqlQuery.Substring(actualSqlQuery.IndexOf("JOIN") + "JOIN ".Length);
                        actualSqlQuery = sqlpart1 + sqlpart2;
                    }

                    reportDataRow["ActualValue"] = DBHelp.RunQueryToString(actualSqlQuery, SetUp.testDBConnectionString);

                    if (reportDataRow["ActualValue"].ToString().Contains(reportDataRow["ExpectedValue"].ToString()))
                    {
                        reportDataRow["TestResult"] = "PASSED";
                        reportDataRow["Info"] = "Verification PASSED";
                    }
                    else if (reportDataRow["ActualValue"].ToString().Contains("exception"))
                    {
                        reportDataRow["TestResult"] = "FAILED";
                        SetUp.testResultStringsList.Add(reportDataRow["TestResult"].ToString());
                        reportDataRow["ActualValue"] = "No Result Of Query";
                        reportDataRow["Info"] = "Verification Failed with No Result Of SQL Query - " + actualSqlQuery;
                    }
                    else if ((reportDataRow["ActualValue"].ToString() == ""))
                    {
                        reportDataRow["TestResult"] = "FAILED";
                        SetUp.testResultStringsList.Add(reportDataRow["TestResult"].ToString());
                        reportDataRow["Info"] = "Verification Failed with SQL Exception by SQL Query - " + actualSqlQuery;
                    }
                    else
                    {
                        reportDataRow["TestResult"] = "FAILED";
                        SetUp.testResultStringsList.Add(reportDataRow["TestResult"].ToString());
                        reportDataRow["Info"] = "Verification Failed on Verification - " + actualSqlQuery;
                    }
                    Console.WriteLine(reportDataRow["Info"]);
                }
            }
        }


        [Given(@"I write the test results into the server TestsReports database")]
        [When(@"I write the test results into the server TestsReports database")]
        [Then(@"I write the test results into the server TestsReports database")]
        public static void IWriteTheTestReports()
        {
            string newReportName = "[Products" + SetUp.scenarioTitleSections["client"] + "TestsReports].[dbo].[TestReports " + SetUp.testStartTime + "-LowLevel]";
            string createCommand = "CREATE TABLE " + newReportName + " (TestEnvironment nvarchar(MAX),TestSuiteRunDate nvarchar(MAX),TestScenarioRunDate nvarchar(MAX),TestSuiteType nvarchar(MAX),"
                    + "TestFeature nvarchar(MAX), TestScenario nvarchar(MAX), TransactionName nvarchar(MAX), PolicyNumber nvarchar(MAX), DataTable nvarchar(MAX), TestedField nvarchar(MAX),"
                    + "BatchRunId nvarchar(MAX),ExpectedValue nvarchar(MAX), ActualValue nvarchar(MAX), TestResult nvarchar(MAX), Info nvarchar(MAX));";
            string populateCommand = "INSERT INTO" + newReportName + " (TestEnvironment,TestSuiteRunDate,TestScenarioRunDate,TestSuiteType,TestFeature,"
                    + "TestScenario,TransactionName,PolicyNumber,DataTable,TestedField,BatchRunId,ExpectedValue,ActualValue,TestResult,Info)"
                    + "VALUES (@TestEnvironment,@TestSuiteRunDate,@TestScenarioRunDate,@TestSuiteType, @TestFeature, @TestScenario,@TransactionName,@PolicyNumber,"
                    + "@DataTable,@TestedField,@BatchRunId,@ExpectedValue,@ActualValue,@TestResult,@Info)";

            CreateReports.CreateLowLevelReport(newReportName, SetUp.testDBConnectionString, createCommand, populateCommand);
            CreateReports.CreateHighLevelReport(CreateReports.calculateTestResults(newReportName, SetUp.testDBConnectionString));

        }


        [Given(@"I clear the database FmTransaction and TransactionSet tables")]
        [When(@"I clear the database FmTransaction and TransactionSet tables")]
        [Then(@"I clear the database FmTransaction and TransactionSet tables")]
        public static void ClearDataBaseTable()
        {
            List<string> cmdStrings = new List<string>();
            cmdStrings.Add("DELETE [" + SetUp.actualDBPrefix + SetUp.currentEnvironment["DataBase"] + "].[tcdb].[FmTransaction]");
            cmdStrings.Add("DELETE [" + SetUp.actualDBPrefix + SetUp.currentEnvironment["DataBase"] + "].[tcdb].[TransactionSet]");

            foreach (string cmdString in cmdStrings)
            {
                try
                {
                    DBHelp.DeleteFromDataBase(cmdString, SetUp.testDBConnectionString);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
