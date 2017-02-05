using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using CrossEndSpecFlowTests.Steps;

namespace CrossEndSpecFlowTests.Steps
{
    [Binding]
    class CompareResults
    {
        
        [TestMethod]
        [Given(@"I compare the (.*) and (.*) query result files")]
        public void GivenIComareTheFiles(string file1, string file2)
        {
            IComareTheFiles(file1, file2);
        }

        [TestMethod]
        [When(@"I compare the (.*) and (.*) query result files")]
        public void WhenIComareTheFiles(string file1, string file2)
        {
            IComareTheFiles(file1, file2);
        }

        [TestMethod]
        [Then(@"I compare the (.*) and (.*) query result files")]
        public void ThenIComareTheFiles(string file1, string file2)
        {
            IComareTheFiles(file1, file2);
        }

        public void IComareTheFiles(string file1, string file2)
        {
            file1 = Path.Combine(DataBaseRead.queryResultSubFolderName, file1);
            file2 = Path.Combine(DataBaseRead.queryResultSubFolderName, file2);

            String[] lines1 = File.ReadAllLines(file1);
            String[] lines2 = File.ReadAllLines(file2);

            IEnumerable<String> CompareResult = lines2.Except(lines1);

            if (!File.Exists(DataBaseRead.compareResultPath))
                File.WriteAllLines(DataBaseRead.compareResultPath, CompareResult);
        }


        [TestMethod]
        [Given(@"The differencial comparation file by the query results should be empty")]
        public void GivenTheComparationDifferencialFileShouldBeEmpty()
        {
            TheComparationDifferencialFileShouldBeEmpty();
        }

        [TestMethod]
        [When(@"The differencial comparation file by the query results should be empty")]
        public void WhenTheComparationDifferencialFileShouldBeEmpty()
        {
            TheComparationDifferencialFileShouldBeEmpty();
        }

        [TestMethod]
        [Then(@"The differencial comparation file by the query results should be empty")]
        public void ThenTheComparationDifferencialFileShouldBeEmpty()
        {
            TheComparationDifferencialFileShouldBeEmpty();
        }

        [TestMethod]
        [Then(@"The differencial comparation file by the query results should be empty")]
        public void TheComparationDifferencialFileShouldBeEmpty()
        {

            StreamReader sr = new StreamReader(DataBaseRead.compareResultPath);
            string contents = sr.ReadToEnd();
            if (!(contents.Length == 0))
                Console.WriteLine("Compared files are not identical on " + contents);
            Assert.IsTrue(contents.Length == 0);

        }

    }
}
