using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using SoapSpecflowTest.SetUp;

namespace SoapSpecflowTest.Steps
{
    [Binding]
    
    [TestClass]
    public class Connect
    {

        public static string currentresponse = null;
        public static string currentservice = null;

        [TestMethod]
        [Given(@"I connect to the (.*) service")]
        public void GivenIConnectToTheService(string service)
        {
            currentservice = service;
            currentresponse = Services.WeatherService("ping", currentservice);
            
        }

        [TestMethod]
        [When(@"I request the cities in the (.*) country")]
        public void GivenICheckTheCitiesInCountry(string country)
        {
            currentresponse = Services.WeatherService(country, currentservice);
            currentresponse = currentresponse.ToString();
        }

        [TestMethod]
        [Then(@"The (.*) city is in the (.*) country")]
        public void ThenTheCityIsInTheCountrys(string city,string country)
        {
            Assert.IsTrue(currentresponse.Contains(city));
            Console.WriteLine(currentresponse);
        }

       


    }
    }
