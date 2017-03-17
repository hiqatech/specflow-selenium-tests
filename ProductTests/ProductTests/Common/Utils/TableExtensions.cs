using TechTalk.SpecFlow;
using System.Collections.Generic;
using ProductTests.Common;
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ProductTests.Utils
{

    [Binding]

    class TableExtensions
    {

        public static Dictionary<string, string> DataToDictionary(Table table, string orientation, int cl1,int cl2)
        {
            var VDictionary = new Dictionary<string, string>();
            var HDictionary = new Dictionary<string, string>();


            if (orientation == "vertical" || orientation=="multi")
            {
                foreach (var row in table.Rows)
                {
                    VDictionary.Add(row[cl1], row[cl2]);
                }

               // List<string> keys = new List<string>(VDictionary.Keys);
               // foreach (var key in keys)
               // {
               //     if (VDictionary[key].Contains("system_date"))
               //         VDictionary[key] = Helper.GetDynamicDate(VDictionary[key]);
               // }

                return VDictionary;
            }

            if (orientation == "horizontal")
            {
                int i = 0;
                int j = table.Header.Count;
                while (!(i==j))
                foreach (var header in table.Header)
                {
               
                    foreach (var row in table.Rows)
                    {

                            HDictionary.Add(header, row[i]);
                    }
                        i++;
                }

                return HDictionary;

             }

            else return VDictionary;

        }
    

        [TestMethod]
        [Given(@"I have a (.*) table")]
        [When(@"I have a (.*) table")]
        [Then(@"I have a (.*) table")]
        public static void IHaveATable(string orientation, Table table)
        {
           
            Dictionary<string, string> Dictionary = new Dictionary<string, string>();
            if (orientation == "multi")
            {
                Dictionary = DataToDictionary(table, orientation, 0,1);
                Console.WriteLine("Table1");
                printDictionary(Dictionary);
               
                Dictionary = DataToDictionary(table, orientation, 0, 2);
                Console.WriteLine("Table2");
                printDictionary(Dictionary);

            }

            if (orientation == "horizontal")
            {
                Dictionary = DataToDictionary(table, orientation, 0, 1);
                printDictionary(Dictionary);
            }
        }

        public static void printDictionary(Dictionary<string, string> dictionary)
        {

            foreach (KeyValuePair<string, string> pair in dictionary)
            {
                Console.WriteLine("Key: {0} Values: {1}", pair.Key, pair.Value);
            }

        }

        
   }
}
