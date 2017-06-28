using System;
using System.Data;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ZolCo.ProductTests.Utilities;
using ZolCo.ProductTests.Configs;

namespace ZolCo.ProductTests.Common
{
    [Binding]
    [TestClass]
    public class SetUp
    {
        
        public static string testStartTime = systemTime;
        public static string systemTime = DateTime.Now.ToString("yyyy-MM-ddTHHmmss");       
        public static string testProjectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\";
        public static string defaultTestRestultDirectory = testProjectDirectory + "TestResults\\";
        //public static Tools.Common.ConfigurationsConfiguration BusinessServiceConfig;
        public static string testSuiteType = null;
        public static Dictionary<string, string> scenarioTitleSections = null;
        public static Dictionary<string, string> scenarioTags = null;
        public static Dictionary<string, string> currentEnvironment = null;
        public static DataTable EnvironmentConfigurations = new DataTable();
        public static string testDBConnectionString = null;
        public static string actualDBPrefix = null;
        public static string actualDBSQLPrefix = null;
        public static List<string> testResultStringsList = new List<string>();

        [BeforeTestRun]
        public static void TestSetup()
        {
            Console.WriteLine("Tests Start...");
        }

        [BeforeScenario]
        public static void ScenarioSetup()
        {
            string scenarioTitle = ScenarioContext.Current.ScenarioInfo.Title.ToString();
            string feature = FeatureContext.Current.FeatureInfo.Title.ToString();

            scenarioTitleSections = new Dictionary<string, string>();
            List<string>scenarioTitleSectionList = scenarioTitle.Split('-').ToList();
            List<string> clientDistributionList = scenarioTitleSectionList[0].Split('/').ToList();
            scenarioTitleSections.Add("application" , ScenarioContext.Current.ScenarioInfo.Tags.ToList()[2].ToString());
            scenarioTitleSections.Add("client", clientDistributionList[0].Replace(" ", ""));
            scenarioTitleSections.Add("distribution", clientDistributionList[1].Replace(" ", ""));     
            scenarioTitleSections.Add("feature", feature.Replace(" ", ""));
            scenarioTitleSections.Add("transaction", scenarioTitleSectionList[1].Replace(" ", ""));
            scenarioTitleSections.Add("scenario", scenarioTitleSectionList[2]);   
            
            currentEnvironment = new Dictionary<string, string>();
            currentEnvironment.Add("EnvironmentString", ScenarioContext.Current.ScenarioInfo.Tags.ToList()[1].ToString());
            currentEnvironment.Add("TestType", ScenarioContext.Current.ScenarioInfo.Tags.ToList()[0].ToString());
            currentEnvironment.Add("Environment", ScenarioContext.Current.ScenarioInfo.Tags.ToList()[1].ToString().Split('_').ToList()[1]);            
            currentEnvironment.Add("AppToTest", scenarioTitleSections["application"]);

            try
            {
                string connection = "Data Source = WINDEVAD0400; Initial Catalog = ProductTestsConfigs; Integrated Security = True";

                currentEnvironment.Add("DataServer", DBHelp.RunQueryToString("SELECT Top 1 Server FROM [ProductTestsConfigs].[dbo].[" + scenarioTitleSections["client"] + "] WHERE Environment = '" + currentEnvironment["Environment"] + "'", connection));
                currentEnvironment.Add("DataBase", DBHelp.RunQueryToString("SELECT Top 1 [DataBase] FROM [ProductTestsConfigs].[dbo].[" + scenarioTitleSections["client"] + "] WHERE Environment = '" + currentEnvironment["Environment"] + "'", connection));
                currentEnvironment.Add("WebPIURL", DBHelp.RunQueryToString("SELECT Top 1 PIUrl FROM [ProductTestsConfigs].[dbo].[" + scenarioTitleSections["client"] + "] WHERE Environment = '" + currentEnvironment["Environment"] + "'", connection));
                currentEnvironment.Add("WebPIUser", DBHelp.RunQueryToString("SELECT Top 1 PIUserName FROM [ProductTestsConfigs].[dbo].[" + scenarioTitleSections["client"] + "] WHERE Environment = '" + currentEnvironment["Environment"] + "'", connection));
                currentEnvironment.Add("WebPIPassword", DBHelp.RunQueryToString("SELECT Top 1 PIPassword FROM [ProductTestsConfigs].[dbo].[" + scenarioTitleSections["client"] + "] WHERE Environment = '" + currentEnvironment["Environment"] + "'", connection));
                currentEnvironment.Add("WebPISecurity", DBHelp.RunQueryToString("SELECT Top 1 PISecurity FROM [ProductTestsConfigs].[dbo].[" + scenarioTitleSections["client"] + "] WHERE Environment = '" + currentEnvironment["Environment"] + "'", connection));

                if (currentEnvironment["Environment"] == "TA")
                {
                    actualDBPrefix = "DartaUAT";
                    actualDBSQLPrefix = "[" + actualDBPrefix + currentEnvironment["DataBase"] + "].[dbo]. ";
                }
                else
                {
                    actualDBPrefix = "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            testDBConnectionString = "Data Source = " + currentEnvironment["DataServer"] + "; Initial Catalog =" + actualDBPrefix + currentEnvironment["DataBase"] + "; Integrated Security = True";
            DataBaseRWCD.IConnectToTheDatabase(); // Set Sytsem Date Time

            Console.WriteLine("Tests start ... With Parameters: \n" +
                " Application - " + scenarioTitleSections["application"] +
                "; \n Client - " + scenarioTitleSections["client"] +
                "; \n Feature - " + scenarioTitleSections["feature"] +
                "; \n Transaction - " + scenarioTitleSections["transaction"] +
                "; \n Scenario - " + scenarioTitleSections["scenario"] +
                "; \n EnvironmentString - " + currentEnvironment["EnvironmentString"] +
                "; \n TestType - " + currentEnvironment["TestType"] +
                "; \n Environment - " + currentEnvironment["Environment"] +
                "; \n AppToTest - " + currentEnvironment["AppToTest"] +
                "; \n DataServer - " + currentEnvironment["DataServer"] +
                "; \n DataBase - " + currentEnvironment["DataBase"] +
                "; \n TestDataBaseConnection - " + testDBConnectionString);

            if (currentEnvironment["AppToTest"] == "BusinessServices")
            {
                Config.SetConfigs(currentEnvironment["EnvironmentString"].Replace("_", " - "), currentEnvironment["DataBase"]);
                ServiceSteps.ClearDataBaseTable();
            }
        }


        [AfterScenario]
        public static void ScenarioCleanUp()
        {
            try
            {
                if (scenarioTitleSections["application"].Contains("Web"))
                {
                    if (ScenarioContext.Current.TestError != null)
                    {
                        string fileName = "ProductTests_Default_" + SetUp.systemTime + "_failed.jpg";
                        WebHelp.TakeScreenShot(fileName, SetUp.defaultTestRestultDirectory);
                    }
                    WebHelp.webdriver.Close();
                    WebHelp.webdriver.Quit();
                }
                if (currentEnvironment["AppToTest"] == "BusinessServices")
                {
                ServiceSteps.ClearDataBaseTable();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            VerifyScenario();

            ServiceSteps.testCaptureDataTable.Clear();
            ServiceSteps.testVerifyDataTable.Clear();
            CreateReports.testReportDataTable.Clear();
        }

        public static void VerifyScenario()
        {
            Console.WriteLine("Verifying the test scenario...");

            if (testResultStringsList.Count != 0)
            {
                try
                {
                    foreach (string test_result_string in testResultStringsList)
                    {
                        Assert.IsTrue(test_result_string == "PASSED");
                    }
                }
                catch
                {
                    Assert.IsTrue(false);
                }
            }
        }

        [AfterTestRun]
        public static void TestCleanUp()
        {
            Console.WriteLine("... Tests Finish");
        }

       
    }
}
