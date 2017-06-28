using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace ZolCo.ProductTests.Utilities
{
    public class DBHelp
    {

        public static DateTime GetSystemDate(string connectionString, string sqlQuery)
        {
            DateTime systemDate = new DateTime();
            string systemDateString = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        using (DataSet dataSet = new DataSet())
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter(sqlQuery, connectionString))
                            {
                                sda.Fill(dataSet);
                            }
                            systemDateString = dataSet.Tables[0].Rows[0]["SystemDate"].ToString();
                        }
                        systemDateString = systemDateString.Remove(systemDateString.Length - 12);
                        systemDate = DateTime.ParseExact(systemDateString, new[] { "M/d/yyyy", "MM/d/yyyy", "M/dd/yyyy", "MM/dd/yyyy" },
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None);
                        connection.Close();
                        return systemDate;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Could not get/set database system time " + ex.ToString());
                        connection.Close();
                        return systemDate;
                    }                  
                }
            }
        }

        public static string ReadDataBase(string sqlQuery, string connection, string searchFor, string saveToPath)
        {
            string resultString = null;
            DataSet dataSet = new DataSet();
            dataSet = RunQueryToDataSet(sqlQuery, connection);

            if (!(saveToPath == null))
                dataSet.WriteXml(saveToPath);
            try
            {
                resultString = dataSet.Tables[0].Rows[0][searchFor].ToString();
            }
            catch
            {
                try
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        resultString = dataSet.Tables[0].Rows[i][searchFor].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error at ReadDataBase " + ex.ToString() + "\n" + sqlQuery);
                }
            }
            return resultString;
        }

        public static DataSet RunQueryToDataSet(string sqlQuery, string connectionString)
        {
            DataSet dataSet = new DataSet();
            try
            {
                using (SqlDataAdapter sqladapter = new SqlDataAdapter(sqlQuery, connectionString))
                {
                    sqladapter.Fill(dataSet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test error reading DataBase Entrie" + ex.ToString() + "\n" + sqlQuery);
            }
            return dataSet;
        }

        public static string RunQueryToString(string sqlQuery, string connectionString)
        {   
            string result = ""; 
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            connection.Open();
            try
            {
                result = command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                result = ex.ToString();
                if (result.Contains("NullReferenceException"))
                    result = "";
            }
            connection.Close();           
            return result;
        }

        public static DataTable RunQueryToDataTable(string connectionString, string dataBase, string sqlQuery)
        {
            DataTable dataTable = new DataTable();
            DataSet dataSet = new DataSet();
            dataSet = RunQueryToDataSet(sqlQuery, connectionString);
            dataTable = dataSet.Tables[0];
            return dataTable;
        }

        public static void WriteDataBase(string connectionString, string sqlQuery)
        {
            try
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(sqlQuery, connectionString));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void DeleteFromDataBase(string cmdString, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = cmdString;

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                        Console.WriteLine("DB History CleanUp - " + cmdString);
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("SQL Database Entry Delete problem " + cmdString + "\n" + ex.ToString());
                    }
                    conn.Close();
                }
            }
        }

        public static void ExcelToDataBase(string connectionString, string dataBaseTable, string excelPath, string sheetName)
        {
            SqlConnection connection = new SqlConnection();
            DataTable excelTable = new DataTable();
            string query = "SELECT * FROM [" + sheetName + "$]";
            excelTable = DataHelp.ExcelSheetToDataTable(excelPath, query);

            try
            {
                using (var bulkCopy = new SqlBulkCopy(connection.ConnectionString, SqlBulkCopyOptions.KeepIdentity))
                {
                    foreach (DataColumn col in excelTable.Columns)
                    {
                        bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                    }

                    bulkCopy.BulkCopyTimeout = 600;
                    bulkCopy.DestinationTableName = sheetName;

                    string alterSingle = "DELETE FROM " + dataBaseTable;
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = alterSingle;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        bulkCopy.WriteToServer(excelTable);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void CreateNewDataTable(string connectionString, string tableName, string commandString)
        {
            using (SqlConnection conn1 = new SqlConnection(connectionString))
            {
                using (SqlCommand comm1 = new SqlCommand())
                {
                    comm1.Connection = conn1;
                    comm1.CommandText = commandString;
                    try
                    {
                        conn1.Open();
                        comm1.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("SQL Database Table Create problem when Creating " + tableName + ex.ToString());
                    }
                    conn1.Close();
                }
            }
        }

        public static void RestoreDataBase(string Comm1, string Comm2, string Comm3, string Comm4, string connectionString)
        {
            using (SqlConnection conn1 = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = conn1;
                    command.CommandTimeout = 100000;
                    conn1.Open();
                    try
                    {
                        command.CommandText = Comm1;
                        Console.WriteLine(Comm1);
                        command.ExecuteNonQuery();
                        command.CommandText = Comm2;
                        Console.WriteLine(Comm2);
                        command.ExecuteNonQuery();
                        command.CommandText = Comm3;
                        Console.WriteLine(Comm3);
                        command.ExecuteNonQuery();
                        command.CommandText = Comm4;
                        Console.WriteLine(Comm4);
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("SQL problem when takeRestoreFileCommand" + ex.ToString());
                    }
                    conn1.Close();
                }
            }
        }

        public static bool DataBaseTableExists(string connection, string tableName)
        {
                string ifTableExistsQuery = "select 1 from " + tableName + " WHERE 1=2";

                using (SqlConnection conn1 = new SqlConnection(connection))
                {
                    using (SqlCommand comm1 = new SqlCommand())
                    {
                        comm1.Connection = conn1;
                        comm1.CommandText = ifTableExistsQuery;
                        try
                        {
                            conn1.Open();
                            comm1.ExecuteNonQuery();
                            conn1.Close();
                            return true;
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine(ex.ToString());
                            conn1.Close();
                            return false;
                        }
                    }
                }   
        }

    }
}
