using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoapSpecflowTest.SetUp
{
    class Services
    {

        public static string WeatherService(string country, string service) {

            WeatherWebService.GlobalWeatherSoapClient client = new WeatherWebService.GlobalWeatherSoapClient(service);
            var response = client.GetCitiesByCountry(country);
            return response;
        }


    }
}
