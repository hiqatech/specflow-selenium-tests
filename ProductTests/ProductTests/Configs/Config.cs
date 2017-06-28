using System.Linq;
using System.Collections.Generic;

namespace ZolCo.ProductTests.Configs
{
    public class Config
    {
        public static string configurationsFilePath = null;
        //public static ConfigurationsConfiguration _currentConfiguation = null;
        //public static Configurations _configurations = null;
        public static Dictionary<string, string> configDictionary;
        public static string currentServiceName = null;
        //public static BaseMessageData MessageData = null;

        public static void SetConfigs(string company, string database)
        {

            //TODO: HOW TO MOVE CONFIGURATION.XML TO DATABASE AND USE IT IN THE ConfigurationHelper AND ConfigurationsConfigurationDatabase ??? BALAJI

            //configurationsFilePath = SetUp.testProjectDirectory + @"\ProductTestsSolution\Config\configurations.xml";
            //_configurations = ConfigurationHelper.Load(configurationsFilePath);
            //
            //_currentConfiguation = _configurations.Configuration.Single(x => x.Name == company);
            //
            //var currentDatabase = _currentConfiguation.Databases.Single(x => x.ID == database);
            //_currentConfiguation.Databases = new List<ConfigurationsConfigurationDatabase>();
            //_currentConfiguation.Databases.Add(currentDatabase);
            //
            //var currentDistribution = _currentConfiguation.Distributions.Single(x => x.ID == "01");
            //_currentConfiguation.Distributions = new List<ConfigurationsConfigurationDistribution>();
            //_currentConfiguation.Distributions.Add(currentDistribution);
            //
            //var currentService = _currentConfiguation.Services.Single(x => x.Name == currentServiceName);
            //_currentConfiguation.Services = new List<ConfigurationsConfigurationService>();
            //_currentConfiguation.Services.Add(currentService);
            //
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //
            //MessageData = new BaseMessageData();
            //MessageData.Database = database;
            //MessageData.Distribution = "01";
            //MessageData.Username = "johndoe";
            //MessageData.Locale = "IT";
            //MessageData.Agent = string.Empty;
            //string companyName = null;
            //if (company.Contains("Darta"))
            //    companyName = "DAR";
            //MessageData.Company = companyName;
            //MessageData.TimeOut = "60";

        }
    }

       
}
