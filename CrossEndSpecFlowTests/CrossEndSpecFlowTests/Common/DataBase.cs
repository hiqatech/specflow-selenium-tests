using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;
using Microsoft.SqlServer.Management.Smo;
using System.Data.SqlClient;

namespace CrossEndSpecFlowTests.Common
{

    [Binding]

    class DataBase
    {
        public static string myServerAddress = null;
        public static string myDataBase = null;
        public static string myUsername = null;
        public static string myPassword = null;
        public static string connectionString = null;
        


        [TestMethod]
        [Given(@"I connect to the (.*) server (.*) database")]
        public void IConnectToTheDatabase(string server, string database)
        {
            switch (server)
            {
                case "SERV8604":
                    {
                        connectionString = "Data Source = serv8604; Initial Catalog = DartaUATR1; Integrated Security = True";

                        SqlConnection connection = new SqlConnection(connectionString);
                        connection.Open();
                        Console.WriteLine("connected" + connectionString);
                        connection.Close();

                        break;
                    }
                default:
                    {
                        Console.WriteLine(database + " database has not defined yet in the test steps");
                        break;
                    }
            }

        }



        [TestMethod]
        [When(@"I run the batch process for (.*) days")]
        public void IRunTheBatchProcessDays(int days)
        {

        }

        [TestMethod]
        [Then(@"I run the batch process for (.*) days")]
        public void FullSurrenderShouldHappen(int days)
        {

        }


        [TestMethod]
        [Then(@"I restore the (.*) database on the (.*) server")]
        public void IRestoreTheDatabase(string database, string server)
        {
            switch (server)
            {
                case "SERV8604":
                    {
                        Server myServer = new Server("SERV8604");
                        Restore res = new Restore();
                        res.Database = database;
                        res.Action = RestoreActionType.Database;
                        res.Devices.AddDevice(@"\\serv8604\\SQL DB Backups\\DartaUATR1_0901_wSQL_SD.bak", DeviceType.File);
                        res.PercentCompleteNotification = 10;
                        res.ReplaceDatabase = true;
                        res.PercentComplete += new PercentCompleteEventHandler(res_PercentComplete);
                        res.SqlRestore(myServer);
                        break;
                    }
                default:
                    {
                        Console.WriteLine(server + " database has not defined yet in the test steps");
                        break;
                    }
            }
        }

        static void res_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            Console.WriteLine(e.Percent.ToString() + "% backed up");
        }
    }

}


