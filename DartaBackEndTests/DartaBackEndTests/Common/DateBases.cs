using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace BackEndSpecflowTest.Common
{
    [Binding]
    class DataBase
    {
        public static string myServerAddress = null;
        public static string myDataBase = null;
        public static string myUsername = null;
        public static string myPassword = null;
        public static string connectionString = null;
        public static string queryResultSubFolderName = SetUp.currentTestRestultDirectory + "\\QueryResults" + SetUp.systemTime + "\\";
        public static int queryCount = 1;
        public static string queryResultFileName = null;
        public static string queryResultPath = null;
        public static string compareResultName = "compareResult.txt";
        public static string compareResultPath = queryResultSubFolderName + compareResultName;
        public static string XML_FILE = null;
        

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
        [Given(@"I run the (.*) query")]
        public void IQueryInTheSQLDB(string searchfor)
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

            string sqlQuery = searchfor;
            Console.WriteLine(sqlQuery);

            using (DataSet dataSet = new DataSet())
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(sqlQuery, connectionString))
                {
                    sda.Fill(dataSet);
                }

                dataSet.WriteXml(XML_FILE);
                
            }

        }



        [TestMethod]
        [Given(@"I update the DB data with the (.*) query")]
        public void GivenIUpdateTheDBData(string queryString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = queryString;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }


        [TestMethod]
        [When(@"I compare the (.*) and (.*) query result files")]
        public void WhenIComareTheFiles(string file1, string file2)
        {
            file1 = Path.Combine(queryResultSubFolderName, file1);
            file2 = Path.Combine(queryResultSubFolderName, file2);

            String[] lines1 = File.ReadAllLines(file1);
            String[] lines2 = File.ReadAllLines(file2);

            IEnumerable<String> CompareResult = lines2.Except(lines1);

            if (!File.Exists(compareResultPath))
                File.WriteAllLines(compareResultPath, CompareResult);
        }

        [TestMethod]
        [Then(@"The differencial comparation file by the query results should be empty")]
        public void TheComparationDifferencialFileShouldBeEmpty()
        {

            StreamReader sr = new StreamReader(compareResultPath);
            string contents = sr.ReadToEnd();
            if (!(contents.Length == 0))
            Console.WriteLine("Compared files are not identical on " + contents);
            Assert.IsTrue(contents.Length == 0);

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

