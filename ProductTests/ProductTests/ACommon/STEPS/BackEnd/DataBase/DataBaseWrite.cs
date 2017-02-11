using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;
using System.Data.SqlClient;
using ProductTests.Common;

namespace ProdutcTests.Common.Steps.BackEnd
{
    class DataBaseWrite
    {

        [TestMethod]
        [Given(@"I update (.*) with (.*) value where (.*) is (.*) from the (.*) table")]
        [When(@"I update (.*) with (.*) value where (.*) is (.*) from the (.*) table")]
        [Then(@"I update (.*) with (.*) value where (.*) is (.*) from the (.*) table")]
        public void GivenIUpdateInTheSQLDB(string update, string to, string where, string value, string table)
        {
            IUpdateInTheSQLDB(update, to, where, value, table);
        }

        public void IUpdateInTheSQLDB(string update, string to, string where, string value, string table)
        {

            string sqlQuery = "UPDATE [RegressionTests].[dbo].[" + table + "] SET " + update + "= '" +
                to + "'  WHERE " + where + "='" + value + "'";

            using (SqlDataAdapter sda = new SqlDataAdapter(sqlQuery, DataBase.connectionString))
                Console.WriteLine(sqlQuery);

        }



    }
}
