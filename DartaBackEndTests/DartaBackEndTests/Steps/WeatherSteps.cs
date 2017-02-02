using BackEndSpecflowTest.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;

namespace BackEndSpecflowTest.Steps
{
    [Binding]

    [TestClass]
    class WeatherSteps
    {

        public static string currentresponse = null;
        public static string compareResultName = "compareResult.txt";
        public static string compareResultPath = Common.Steps.responseSubFolderName + compareResultName;

        [TestMethod]
            [Given(@"I connect to the (.*) service")]
            public void GivenIConnectToTheService(string service)
            {
                currentresponse = Services.WeatherService("ping");

            }

            [TestMethod]
            [When(@"I request the cities in the (.*) country")]
            public void GivenICheckTheCitiesInCountry(string country)
            {
                currentresponse = Services.WeatherService(country);
                currentresponse = currentresponse.ToString();
            Common.Steps.StoreAnswerAs(currentresponse, ".txt");

            }

            [TestMethod]
            [Then(@"The (.*) city is in the (.*) country")]
            public void ThenTheCityIsInTheCountrys(string city, string country)
            {
                Assert.IsTrue(currentresponse.Contains(city));
                Console.WriteLine(currentresponse);
            }

            [TestMethod]
            [When(@"I compare the (.*) and (.*) response files")]
            public void WhenIComareTheFiles(string file1, string file2)
            {
            file1 = Path.Combine(Common.Steps.responseSubFolderName, file1);
            file1 = Path.Combine(Common.Steps.responseSubFolderName, file1);

            String[] lines1 = File.ReadAllLines(file1);
            String[] lines2 = File.ReadAllLines(file2);

            IEnumerable<String> CompareResult = lines2.Except(lines1);

            if (!File.Exists(compareResultPath))
                File.WriteAllLines(compareResultPath, CompareResult);
        }

            [TestMethod]
            [Then(@"The differencial comparation file should be empty")]
            public void TheComparationDifferencialFileShouldBeEmpty()
            {

            StreamReader sr = new StreamReader(compareResultPath);
                string contents = sr.ReadToEnd();

                Assert.IsTrue(contents.Length == 0);

            }

    }

}