using System;
using TechTalk.SpecFlow;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace ProductTests.Common
{

    [Binding]

    class DataBase
    {
        public static string myServerAddress = null;
        public static string myDataBase = null;
        public static string myUsername = null;
        public static string myPassword = null;
        public static string connectionString = null;
        public static DateTime system_date;


        [TestMethod]
        [Given (@"I connect to the (.*) server (.*) database")]
        [When(@"I connect to the (.*) server (.*) database")]
        [Then(@"I connect to the (.*) server (.*) database")]
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

            string system_date_string;
            string sqlQuery = "SELECT TOP 1[SystemDate] FROM [DartaUATR1].[schedule].[OnlineControl]";
            using (DataSet dataSet = new DataSet())
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(sqlQuery, DataBase.connectionString))
                {
                    sda.Fill(dataSet);
                }

                system_date_string = dataSet.Tables[0].Rows[0]["SystemDate"].ToString();

            }

            system_date_string = system_date_string.Remove(system_date_string.Length - 12);

            system_date = DateTime.ParseExact(system_date_string, new[] { "M/d/yyyy", "MM/d/yyyy", "M/dd/yyyy", "MM/dd/yyyy" },
            CultureInfo.InvariantCulture,
            DateTimeStyles.None);

        }



        [TestMethod]
        [Given (@"I run the batch process for (.*) days")]
        [When(@"I run the batch process for (.*) days")]
        [Then(@"I run the batch process for (.*) days")]
        public void IRunTheBatchProcessDays(int days)
        {
            Console.WriteLine( "Need to implement" );
        }


        [TestMethod]
        [Given(@"I restore the (.*) database on the (.*) server")]
        [When(@"I restore the (.*) database on the (.*) server")]
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


