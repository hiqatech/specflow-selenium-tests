using System;
using TechTalk.SpecFlow;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using ProductTests.Common.STEPS.BackEnd.Service;
using System.Linq;
using ProductTests.Common.SRC;
using ProductTests.Common;

namespace ProdutcTests.Common.Steps.BackEnd
{

    [Binding]

    class DataBase
    {
        
        public static DateTime system_date;
        public static string currentEnvironment = null;
        public static string currentDataServer = null;
        public static string currentDataBase = null;


        [TestMethod]
        [Given (@"I connect to the (.*) server (.*) database")]
        [When(@"I connect to the (.*) server (.*) database")]
        [Then(@"I connect to the (.*) server (.*) database")]
        public void IConnectToTheDatabase(string server, string database)
        {
            currentDataServer = server;
            currentDataBase = database;
            ServiceInputFrom.currentDataBase = currentDataBase;
            ServiceInputFrom.currentDataServer = currentDataServer;
           

            string connectionString = null;

            switch (server)
            {
                case "SERV8604":
                    {
                        currentEnvironment = "UAT/QA";

                        connectionString = "Data Source = serv8604; Initial Catalog = "+ database + "; Integrated Security = True";

                        SqlConnection connection = new SqlConnection(connectionString);
                        try
                        {
                            connection.Open();
                            Console.WriteLine("connected" + connectionString);
                        }
                        catch (Exception ex)
                        { Console.WriteLine("Could not connect to the DataBase " + ex.ToString()); }

                        connection.Close();

                        break;
                    }
                case "WINDEVAD0376":
                    {
                        currentEnvironment = "DEV";

                        connectionString = "Data Source = WINDEVAD0376; Initial Catalog = " + database + "; Integrated Security = True";

                        SqlConnection connection = new SqlConnection(connectionString);
                        try
                        {
                        connection.Open();
                        Console.WriteLine("connected" + connectionString);
                        }
                        catch ( Exception ex)
                        { Console.WriteLine("Could not connect to the DataBase " + ex.ToString()); }

                        connection.Close();
                        
                        break;
                    }
                default:
                    {
                        Console.WriteLine(database + " database has not defined yet in the test steps");
                        break;
                    }
            }

            string company = DataBase.currentEnvironment + "-" + SetUp.client;
            Config.SetConfigs(company, database);

            string system_date_string;
            string sqlQuery = "SELECT TOP 1[SystemDate] FROM [DartaUATR1].[schedule].[OnlineControl]";
            using (DataSet dataSet = new DataSet())
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(sqlQuery, connectionString))
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


