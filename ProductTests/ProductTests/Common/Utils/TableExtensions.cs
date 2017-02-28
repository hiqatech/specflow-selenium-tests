using TechTalk.SpecFlow;
using System.Collections.Generic;
using ProductTests.Common;
using System;
using System.Linq;

namespace ProductTests.Utils
{
    class TableExtensions
    {

        public static Dictionary<string, string> ToDictionary(Table table)
        {
            var myDictionary = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                myDictionary.Add(row[0], row[1]);
            }

            List<string> keys = new List<string>(myDictionary.Keys);
            for (int i = 0; i < keys.Count; i++)
            {
                string key = keys[i];
                string value = myDictionary[key];
                if (value.Contains("system_date"))
                {
                myDictionary[key] = Helper.GetDynamicDate(value);
                }
            }


            return myDictionary;
        }
    }
}
