using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductTests.Utils;
using ProdutcTests.Common.Steps.BackEnd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TechTalk.SpecFlow;

namespace ProductTests.Common.STEPS.BackEnd.Service
{
    [Binding]

    class ServiceInputFromDB
    {

        public static DataTable testCaptureDataTable = new DataTable();
        public static DataTable testVerifyDataTable = new DataTable();
        public static DataTable testReportDataTable = new DataTable();
        public static Dictionary<string, string> policyNumberDictionary;
        public static XmlDocument serviceRequestObjectXML = null;
        public static string sytemTime = null;
        public static string testScenarioRunDate = null;
        public static string testFailureString = null;
        public static string currentDataServer = null;
        public static string currentDataBase = null;
        public static string scenarioTitle = null;
        public static List<string> scenarioTitleSections = null;
        public static string feature = null;
        public static string client = null;
        public static string transaction = null;
        public static string scenario = null;

        [Given(@"I read data from the database capture table for the requests")]
        [When(@"I read data from the database capture table for the requests")]
        [Then(@"I read data from the database capture table for the requests")]
        public static void IReadTestRequestDataBase()
        {
            if (testReportDataTable.Columns.Count == 0)
            {
                testReportDataTable.Clear();
                testReportDataTable.Columns.Add("TestSuiteRunDate");
                testReportDataTable.Columns.Add("TestSuiteType");
                testReportDataTable.Columns.Add("TestScenarioRunDate");
                testReportDataTable.Columns.Add("TestFeature");
                testReportDataTable.Columns.Add("TestScenario");
                testReportDataTable.Columns.Add("TransactionName");
                testReportDataTable.Columns.Add("PolicyNumber");
                testReportDataTable.Columns.Add("DataTable");
                testReportDataTable.Columns.Add("TestedField");
                testReportDataTable.Columns.Add("ExpectedValue");
                testReportDataTable.Columns.Add("ActualValue");
                testReportDataTable.Columns.Add("TestResult");
                testReportDataTable.Columns.Add("Info");
            }
            try
            {
                sytemTime = DateTime.Now.ToString("yyyy-MM-ddTHHmmss");
                testScenarioRunDate = sytemTime;
                string requestSQLQuery = "SELECT * FROM " + transaction + "Capture";
                testCaptureDataTable = DataBaseRW.GetDataFromDBToDataTable(requestSQLQuery, "ProductTestInput");
            }
            catch (Exception ex)
            {
                testFailureString = "Test error at " + ScenarioContext.Current.StepContext.StepInfo.Text + ex.ToString();
                string testResultString = "FAILED";
                Helper.testResultStringList.Add(testResultString);
                IWriteTheTestReport();
                Helper.verifyScenario();
            }
        }


        [Given(@"I read data from the database verify table where the BatchRunId (.*) to verify")]
        [When(@"I read data from the database verify table where the BatchRunId (.*) to verify")]
        [Then(@"I read data from the database verify table where the BatchRunId (.*) to verify")]
        public static void IReadTestRequestDataBase(string batchRunId)
        {
            try
            {
                string verifySQLQuery = "SELECT * FROM " + transaction + "Verify WHERE BatchRunId = '" + batchRunId + "'";
                testVerifyDataTable = DataBaseRW.GetDataFromDBToDataTable(verifySQLQuery, "ProductTestInput");

                foreach (DataRow verifyDataRow in testVerifyDataTable.Rows)
                {
                    DataRow reportDataRow = testReportDataTable.NewRow();
                    reportDataRow["TestSuiteRunDate"] = SetUp.testStartTime;
                    reportDataRow["TestSuiteType"] = SetUp.testSuiteType;
                    reportDataRow["TestScenarioRunDate"] = testScenarioRunDate;
                    reportDataRow["TestFeature"] = feature;
                    reportDataRow["TestScanario"] = scenario;
                    reportDataRow["TransactionName"] = transaction;
                    reportDataRow["PolicyNumber"] = verifyDataRow["WherePolicyNumber"];
                    reportDataRow["DataTable"] = verifyDataRow["From"];
                    reportDataRow["ExpectedValue"] = verifyDataRow["ExpectedValue"];
                    reportDataRow["TestedFiled"] = verifyDataRow["TestedFiled"];
                    reportDataRow["Info"] = testFailureString;
                    testReportDataTable.Rows.Add(reportDataRow);
                }
            }
            catch (Exception ex)
            {
                testFailureString = "Test error at " + ScenarioContext.Current.StepContext.StepInfo.Text + ex.ToString();
                string testResultString = "FAILED";
                Helper.testResultStringList.Add(testResultString);
                IWriteTheTestReport();
                Helper.verifyScenario();
            }
        }


        [Given(@"I create (.*) requests from this data and send to the services")]
        [When(@"I create (.*) requests from this data and send to the services")]
        [Then(@"I create (.*) requests from this data and send to the services")]
        public static void IcreateAndSendRequests(string requestName)
        {
            Dictionary<string, string> requestDictionary;

            foreach (DataRow requestDataRow in testCaptureDataTable.Rows)
            {
                requestDictionary = new Dictionary<string, string>();
                requestDictionary = TableExtensions.DataTableToDictionary(testCaptureDataTable, requestDataRow);
                serviceRequestObjectXML = Services.GetRequestXMLWithData(requestName, requestDictionary);
                ServiceObject.GetServiceObject(requestName, serviceRequestObjectXML);
                ServiceObject.SendServiceObject();

                //Console.WriteLine("Service Request response " + ServiceObject.requestResponseString);

                if (ServiceObject.requestResponseString.Contains("PolicyNumber"))
                {
                    Assert.IsTrue(true);
                    if (transaction == "NewBusiness")
                    {
                        XmlDocument response = null;
                        response.Load(ServiceObject.requestResponseString);
                        string policyNumber = response.SelectSingleNode("//*[contains(name(),PolicyNumber)]").ToString();
                        requestDataRow["ProposalNumber"] = policyNumber;
                        policyNumberDictionary = new Dictionary<string, string>();
                        policyNumberDictionary.Add(requestDictionary["ProposalNumber"], policyNumber);
                    }
                }
                else
                {
                    testFailureString = "Test error at " + ScenarioContext.Current.StepContext.StepInfo.Text;
                    string testResultString = "FAILED";
                    Helper.testResultStringList.Add(testResultString);


                    try
                    {
                        foreach (DataRow reportDataRow in testReportDataTable.Rows)
                        {
                            if (requestDataRow["PolicyNumber"].ToString() == reportDataRow["PolicyNumber"].ToString())
                            {
                                requestDataRow["TestResult"] = testResultString;
                                reportDataRow["Info"] = testFailureString;
                            }
                        }
                    }
                    catch
                    {
                        foreach (DataRow reportDataRow in testReportDataTable.Rows)
                        {
                            if (requestDataRow["ProposalNumber"].ToString() == reportDataRow["PolicyNumber"].ToString())
                            {
                                requestDataRow["TestResult"] = testResultString;
                                reportDataRow["Info"] = testFailureString;
                            }
                        }
                    }
                }
            }
        }

        [Given(@"I verify the values in the database by the test expected database")]
        [When(@"I verify the values in the database by the test expected database")]
        [Then(@"I verify the values in the database by the test expected database")]
        public static void IVerifyDataBaseEntries(string requestName)
        {
            try
            {
                string testResultString;
                int iterator = 0;

                foreach (DataRow verifyDataRow in testVerifyDataTable.Rows)
                {
                    DataRow reportDataRow = testReportDataTable.Rows[iterator];

                    if (reportDataRow["Info"].ToString() == "")
                    {
                        string actualSQLQuery = "SELECT [" + verifyDataRow["Select"] + "] FROM [" + currentDataBase +
                            "].[dbo].[" + verifyDataRow["From"] + "] WHERE PolicyNumber = '" +
                            verifyDataRow["PolicyNumber"] + "'" + verifyDataRow["ExtraWhere"];

                        reportDataRow["ActualValue"] = DataBaseRW.DataBaseRunQuery(actualSQLQuery, verifyDataRow["Select"].ToString(), null);

                        if (reportDataRow["ExpectedValue"].ToString() == reportDataRow["ActualValue"].ToString())
                        {
                            testResultString = "PASSED";
                            reportDataRow["TestResult"] = testResultString;
                            reportDataRow["Info"] = "Test has been PASSED";
                        }
                        else
                        {
                            testResultString = "FAILED";
                            reportDataRow["TestResult"] = testResultString;
                            Helper.testResultStringList.Add(testResultString);
                            reportDataRow["Info"] = "Test has been FAILED on " + ScenarioContext.Current.StepContext.StepInfo.Text;
                        }
                    }
                    iterator++;
                }
                iterator = 0;
            }
            catch (Exception ex)
            {
                testFailureString = "Test error at " + ScenarioContext.Current.StepContext.StepInfo.Text + ex.ToString();
                string testResultString = "FAILED";
                Helper.testResultStringList.Add(testResultString);
                IWriteTheTestReport();
                Helper.verifyScenario();
            }
        }

        [Given(@"I write the test results into the server TestResults database")]
        [When(@"I write the test results into the server TestResults database")]
        [Then(@"I write the test results into the server TestResults database")]
        public static void IWriteTheTestReport()
        {
            string newReportTableName = "[ProductTestReports].[dbo].[TestReports" + SetUp.testStartTime + "]";
            string createNewReportTable = "CREATE TABLE " + newReportTableName + " (TestSuiteRunDate nvarchar(50),) TestSuiteType nvarchar(50), "
                + "TestScenarioRunDate nvarchar(50), TestFeature nvarchar(50), TestScenario nvarchar(50), TransactionName nvarchar(50), "
                + "PolicyBumber nvarchar(50), DataTable nvarchar(50), TestedField nvarchar(50), ExpectedVale nvarchar(50), ActualValue nvarchar(50), "
                + "TestResult nvarchar(50), Info nvarchar(MAX));";
            string connectionString = "Data Source = " + currentDataServer + "; Initial Catalog = ProductTestReports; Intergrated Security = True";

            using (SqlConnection conn1 = new SqlConnection(connectionString))
            {
                using (SqlCommand comm1 = new SqlCommand())
                {
                    comm1.Connection = conn1;
                    comm1.CommandText = createNewReportTable;
                    try
                    {
                        conn1.Open();
                        comm1.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("SQL Data Table create problem when creating ProductTestReports " + ex.ToString());
                    }
                    conn1.Close();
                }
            }

            string writeNewReportTable = "INSERT INTO " + newReportTableName +
                " (TestSuiteRunDate, TestSuiteType, TestScenarioRunDate, TestFeature, TestScenario, TransactionName, "
                + "PolicyBumber, DataTable, TestedField, ExpectedVale, ActualValue, TestResult, Info)"
                + "VALUES (@TestSuiteRunDate, @TestSuiteType, @TestScenarioRunDate, @TestFeature, @TestScenario, @TransactionName, "
                + "@PolicyNumber, @DataTable, @TestedField, @ExpectedVale, @ActualValue, @TestResult, @Info";

            foreach (DataRow reportDataRow in testReportDataTable.Rows)
            {
                using (SqlConnection conn2 = new SqlConnection(connectionString))
                {
                    using (SqlCommand comm2 = new SqlCommand())
                    {
                        comm2.Connection = conn2;
                        comm2.CommandText = writeNewReportTable;
                        comm2.Parameters.AddWithValue("@TestSuiteRunDate", reportDataRow["TestSuiteRunDate"]);
                        comm2.Parameters.AddWithValue("@TestSuiteType", reportDataRow["TestSuiteType"]);
                        comm2.Parameters.AddWithValue("@TestScenarioRunDate", reportDataRow["TestScenarioRunDate"]);
                        comm2.Parameters.AddWithValue("@TestFeature", reportDataRow["TestFeature"]);
                        comm2.Parameters.AddWithValue("@TestScenario", reportDataRow["TestScenario"]);
                        comm2.Parameters.AddWithValue("@TransactionName", reportDataRow["TransactionName"]);
                        comm2.Parameters.AddWithValue("@PolicyNumber", reportDataRow["PolicyNumber"]);
                        comm2.Parameters.AddWithValue("@DataTable", reportDataRow["DataTable"]);
                        comm2.Parameters.AddWithValue("@TestedField", reportDataRow["TestedField"]);
                        comm2.Parameters.AddWithValue("@ExpectedVale", reportDataRow["ExpectedVale"]);
                        comm2.Parameters.AddWithValue("@ActualValue", reportDataRow["ActualValue"]);
                        comm2.Parameters.AddWithValue("@TestResult", reportDataRow["TestResult"]);
                        comm2.Parameters.AddWithValue("@Info", reportDataRow["Info"]);

                        try
                        {
                            conn2.Open();
                            comm2.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("SQL Data Table write problem when writing ProductTestReports " + ex.ToString());
                        }
                        conn2.Close();
                    }
                }
            }

            testCaptureDataTable.Clear();
            testVerifyDataTable.Clear();
            testReportDataTable.Clear();
            Helper.verifyScenario();

        }

        [Given(@"I clear the database (.*) table")]
        [When(@"I clear the database (.*) table")]
        [Then(@"I clear the database (.*) table")]
        public static void IClearDataBaseTable(string tableName = "FMTransaction")
        {
            string clearDataTable = " DELETE [" + currentDataBase + "].[tcdb].[" + tableName + "]";
            string connectionString = "Data Source = " + currentDataServer + "; Initial Catalog = ProductTestReports; Intergrated Security = True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = clearDataTable;
                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("SQL Data Table clear problem when clearing FMTransaction " + ex.ToString());
                    }
                    conn.Close();
                }
            }

        }

    }

}