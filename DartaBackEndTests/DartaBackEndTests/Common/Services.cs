using System.Net;

namespace BackEndSpecflowTest.Common
{
    class Services
    {

        public static string WeatherService(string country)
        {
            var webProxy = WebProxy.GetDefaultProxy();
            webProxy.UseDefaultCredentials = true;
            WebRequest.DefaultWebProxy = webProxy;

            GlobalWeather.GlobalWeatherSoapClient client = new GlobalWeather.GlobalWeatherSoapClient("GlobalWeatherSoap12");
            var response = client.GetCitiesByCountry(country);
            return response;

        }






    }
}
