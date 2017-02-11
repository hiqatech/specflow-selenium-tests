using TechTalk.SpecFlow;
using System.Collections.Generic;

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
            return myDictionary;
        }


    }
}
