using Ipsi.Common.Utilities;
using ProductTests.DartaBusinessServices;
using ProductTests.Utils;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace ProductTests.ACommon.SRC
{
    class Config
    {
        public static Table configTable ;
        public static string serviceName;
        public static BaseMessageData SetConfigHeaders(string setServiceName,Table setConfigTable)
        {
            configTable = setConfigTable;
            serviceName = setServiceName;
            using (BusinessServiceClient service = new BusinessServiceClient()) {
                BaseMessageData MessageData = new BaseMessageData();
                Dictionary<string, string>configValues = TableExtensions.ToDictionary(setConfigTable);

                    MessageData.Database = configValues["database"];
                    MessageData.Distribution = configValues["distribution"];
                    MessageData.Username = configValues["username"];
                    MessageData.Locale = configValues["locale"];
                    MessageData.Agent = string.Empty;
                    MessageData.Company = configValues["company"];
                    MessageData.TimeOut = configValues["timeout"];


                    return MessageData;

                }

            }

        }
    }
