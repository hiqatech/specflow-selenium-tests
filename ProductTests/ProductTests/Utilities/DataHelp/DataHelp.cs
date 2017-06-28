using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Linq;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using TechTalk.SpecFlow;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using YamlDotNet.Serialization;
using System.Text.RegularExpressions;

namespace ZolCo.ProductTests.Utilities
{
    
    public class DataHelp
    {

        public static Dictionary<string, string> TableToDictionary(Table table, int valueto)
        {
            var VDictionary = new Dictionary<string, string>();
            var HDictionary = new Dictionary<string, string>();

            if (table.Header.Contains("Key"))
            {
                foreach (var row in table.Rows)
                {
                    VDictionary.Add(row[0], row[valueto + 1]);
                }
                return VDictionary;
            }
            else
            {
                int i = 0;
                int j = table.Header.Count;
                while (!(i == j))
                    foreach (var header in table.Header)
                    {
                        var row = table.Rows[valueto];
                        HDictionary.Add(header, row[i]);
                        i++;
                    }
                return HDictionary;
            }
        }

        public static void printDictionary(Dictionary<string, string> dictionary)
        {
            foreach (KeyValuePair<string, string> pair in dictionary)
            {
                Console.WriteLine("Key: {0} Values: {1}", pair.Key, pair.Value);
            }
        }

        public static Dictionary<string, string> DataTableToDictionary(DataTable dataTable, DataRow dataRow)
        {
            var dictionary = new Dictionary<string, string>();
            int i = 0;
            int j = dataTable.Columns.Count;

            try
            {
                while (!(i == j))
                    foreach (var column in dataTable.Columns)
                    {
                        if (!(column.ToString() == "TestType"))
                        {
                            dictionary.Add(column.ToString(), dataRow[i].ToString());
                        }
                        i++;
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return dictionary;
        }

        public static DataTable AddRowsToDataTable(DataTable data_table, string id, string add_this)
        {
            DataRow data_row = data_table.NewRow();
            data_row[id] = add_this;
            data_table.Rows.Add(data_row);
            return data_table;
        }

        public static DataTable ExcelSheetToDataTable(string excelPath, string sqlQuery)
        {
            string connection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath + ";Extended Properties='Excel 12.0;IMEX=1;'";
            DataSet dataSet = new DataSet();
            DataTable dataTable = null;

            try
            {
                OleDbConnection conn = new OleDbConnection(connection);
                OleDbDataAdapter adapter = new OleDbDataAdapter(sqlQuery, conn);
                adapter.Fill(dataSet);
                dataTable = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on reading excel sheet to datatable" + ex.ToString() + "\n" + sqlQuery);
            }
            return dataTable;
        }

        public static Dictionary<string, string> AddDataToDisctionary(DateTime systemDate, Dictionary<string, string> datadictionary)
        {
            List<string> dataKeys = new List<string>(datadictionary.Keys);

            foreach (string dataKey in dataKeys)
            {
                if (datadictionary[dataKey].ToString().Contains("system_date_start"))
                    datadictionary[dataKey] = (GetDynamicDate(systemDate,datadictionary[dataKey], "yyyy-MM-ddThh:mm:ss"));
            }
            return datadictionary;
        }

        public static DataRow AddDataToDataRow(DateTime systemDate, DataTable dataTable, DataRow dataRow)
        {
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                try
                {
                    if (dataRow[i].ToString().Contains("system_date_start"))
                    {
                        string srting1 = dataRow[i].ToString().Substring(0, dataRow[i].ToString().IndexOf("system_date_start"));
                        string date = GetDynamicDate(systemDate, dataRow[i].ToString(), "M/d/yyyy");
                        if (dataTable.Columns[i].ColumnName == "ExpectedValue")
                            dataRow[i] = srting1 + date;
                        else
                            dataRow[i] = srting1 + date + "'";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString() + dataRow[i].ToString());
                }
            }
            return dataRow;
        }

        public static string GenerateRandomString(int length, string startswith)
        {
            string systemTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            double datenumber = double.Parse(systemTime);
            string datestring = datenumber.ToString();
            int random_lenght = length - startswith.Length;
            int remove = datestring.Length - random_lenght;
            string random_string = (datestring.Remove(1, remove));
            random_string = startswith + random_string;
            return random_string;
        }

        public static string GetDynamicDate(DateTime systemDate, string value, string pattern)
        {
            if (value.Contains("'"))
                value = value.Replace("'", "");
            value = value.Substring(value.IndexOf("system_date_start") + "system_date_start".Length);
            if (value == "")
                value = value + "0.00";
            else
                value = value + ".00";
            double days = double.Parse(value, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint);
            value = systemDate.AddDays(days).ToString(pattern);
            return value;
        }

        public static string DumpAsYaml(object o)
        {
            var stringBuilder = new StringBuilder();
            var serializer = new Serializer();
            serializer.Serialize(new IndentedTextWriter(new StringWriter(stringBuilder)), o);
            return stringBuilder.ToString();
        }

        public static XmlDocument GetRequestXMLWithData(string requestPath, Dictionary<string, string> requestdata)
        {
            XmlDocument serviceObjectTemplateXML = new XmlDocument();
            serviceObjectTemplateXML.Load(requestPath);
            List<string> requestKeys = new List<string>(requestdata.Keys);
            foreach (string requestKey in requestKeys)
            {
                string currentKey = requestKey;
                string value = requestdata[requestKey];
                XmlNode child = null;
                try
                {
                    if (!(value == ""))
                    {
                        if (requestKey.Contains("/"))
                        {
                            List<string> path = currentKey.Split('/').ToList();
                            string main = path[0];
                            string sub = path[1];
                            if (main.Any(char.IsDigit))
                            {
                                string keyNoString = Regex.Match(main, @"\d+").Value;
                                int keyNo = Int32.Parse(keyNoString) - 1;
                                main = main.Replace(keyNoString, "");
                                XmlNodeList childList = serviceObjectTemplateXML.SelectNodes("//*[local-name()='" + main + "']/*[local-name()='" + sub + "']");
                                child = childList[keyNo];
                            }
                            else child = serviceObjectTemplateXML.SelectSingleNode("//*[local-name()='" + main + "']/*[local-name()='" + sub + "']");
                        }
                        else
                        {
                            child = serviceObjectTemplateXML.SelectSingleNode("//*[local-name()='" + currentKey + "']");
                        }

                        if (!((child.InnerText == "" && (value == "true")) || (child.InnerText == "" && (value == "false"))))
                            child.InnerText = value;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Request data parse error at " + requestKey + ex.ToString());
                }
            }
            return serviceObjectTemplateXML;
        }

        public static bool CompareFiles(string filePath1, string filePath2)
        {
            String[] lines1 = File.ReadAllLines(filePath1);
            String[] lines2 = File.ReadAllLines(filePath2);
            IEnumerable<String> CompareResult = lines2.Except(lines1);

            if (CompareResult == null)
                return true;
            else
            {
                Console.WriteLine(CompareResult.ToString());
                Console.WriteLine("Compared files are not identical on " + CompareResult);
            }
            return false;
        }

    }
}
