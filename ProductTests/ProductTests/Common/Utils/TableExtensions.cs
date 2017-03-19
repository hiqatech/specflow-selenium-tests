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
        public static Dictionary<string,string> PolicyNumberDictionary = null;

        public static void fillPolicyNumberDictionary()
        {
            PolicyNumberDictionary = new Dictionary<string, string>();
            PolicyNumberDictionary.Add("0","0100000001");
            PolicyNumberDictionary.Add("1","0100000002");
        }

        public static Dictionary<string, string> DataToDictionary(Table table, int valueto)
        {
            var VDictionary = new Dictionary<string, string>();
            var HDictionary = new Dictionary<string, string>();

            if (table.Header.Contains("Key"))
                {
                foreach (var row in table.Rows)
                {
                    VDictionary.Add(row[0], row[valueto+1]);
                }

               // List<string> keys = new List<string>(VDictionary.Keys);
               // foreach (var key in keys)
               // {
               //     if (VDictionary[key].Contains("system_date"))
               //         VDictionary[key] = Helper.GetDynamicDate(VDictionary[key]);
               // }

                return VDictionary;
            }

            else
            {
                int i = 0;
                int j = table.Header.Count;
                while (!(i==j))
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

        
   }
}
