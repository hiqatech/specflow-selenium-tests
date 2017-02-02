using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndSpecflowTest.Drafts
{
    class Class1
    {


        // [TestMethod]
        // [Given(@"I run the (.*) query")]
        // public void IQueryInTheSQLDB(string searchfor)
        // {
        //     string CSV_FILE = SetUp.currentTestRestultDirectory + @"\queryResult.csv";
        //
        //     DataTable dataTable = new DataTable();
        //     string sqlQuery = null;
        //     Console.WriteLine(sqlQuery);
        //
        //     using (SqlConnection connection = new SqlConnection(connectionString))
        //     {
        //         connection.Open();
        //         using (SqlCommand command = connection.CreateCommand())
        //         {
        //             command.CommandText = searchfor;
        //             using (SqlDataReader reader = command.ExecuteReader())
        //             {
        //                 using (StreamWriter csvFile = new StreamWriter(File.Create(CSV_FILE)))
        //                 {
        //                     StringBuilder outputLine = new StringBuilder();
        //                     DataTable schema = reader.GetSchemaTable();
        //                     List<int> ordinals = new List<int>();
        //
        //                     foreach (DataRow row in schema.Rows)
        //                     {
        //                         outputLine.AppendFormat("{0},", row["ColumnName"]);
        //                         ordinals.Add((int)row["ColumnOrdinal"]);
        //                     }
        //
        //                     csvFile.WriteLine(outputLine.ToString().TrimEnd(','));
        //                     while (reader.Read())
        //                     {
        //                         outputLine.Clear();
        //                         foreach (int ordinal in ordinals)
        //                             outputLine.AppendFormat("{0},", reader[ordinal]);
        //                         csvFile.WriteLine(outputLine.ToString().TrimEnd(','));
        //                     }
        //
        //                 }
        //
        //             }
        //
        //         }
        //
        //         connection.Close();
        //
        //
        //     }
        //
        // }



        // [TestMethod]
        // [Given(@"I run the (.*) query")]
        // public void IQueryInTheSQLDB(string searchfor)
        // {
        //     DataTable dataTable = new DataTable();
        //     string sqlQuery = null;
        //     Console.WriteLine(sqlQuery);
        //
        //     using (SqlConnection connection = new SqlConnection(connectionString))
        //     using (SqlCommand command = connection.CreateCommand())
        //     using (SqlDataAdapter adapter = new SqlDataAdapter(command))
        //     {
        //         command.CommandText = sqlQuery;
        //         command.CommandType = CommandType.Text;
        //         connection.Open();
        //         int rows_returned = adapter.Fill(dataTable);
        //         connection.Close();
        //     }
        //
        //     if (dataTable.Rows.Count == 0)
        //     {
        //         Console.WriteLine("query returned no rows");
        //     }
        //     else
        //     {
        //
        //         //write semicolon-delimited header
        //         string[] columnNames = dataTable.Columns
        //                                  .Cast<DataColumn>()
        //                                  .Select(c => c.ColumnName)
        //                                  .ToArray()
        //                                  ;
        //         string header = string.Join(",", columnNames);
        //         Console.WriteLine(header);
        //         // write each row
        //
        //         foreach (DataRow dr in dataTable.Rows)
        //         {
        //
        //             // get each rows columns as a string (casting null into the nil (empty) string
        //             string[] values = new string[dataTable.Columns.Count];
        //             for (int i = 0; i < dataTable.Columns.Count; ++i)
        //             {
        //                 values[i] = dr[i].ToString(); // we'll treat nulls as the nil string for the nonce
        //             }
        //
        //             // construct the string to be dumped, quoting each value and doubling any embedded quotes.
        //             string data = string.Join(";", values.Select(s => "\"" + s.Replace("\"", "\"\"") + "\""));
        //             Console.WriteLine(data);
        //
        //         }
        //     }
        // }


    }
}
