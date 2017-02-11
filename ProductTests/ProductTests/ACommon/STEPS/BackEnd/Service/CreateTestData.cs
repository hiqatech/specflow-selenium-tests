using TechTalk.SpecFlow;
using ProductTests.Utils;
using ProductTests.DartaBusinessServices;
using Ipsi.Common.Utilities;
using ProductTests.ACommon.SRC;
using System.Collections.Generic;
using System.Xml;

namespace ProdutcTests.Common.Steps.BackEnd
{
    [Binding]
    public class CreateTestData
    {

        public static XmlDocument RequestXML = null;

        [Given(@"I have a (.*) to (.*)")]
        public static void GetTestData(string datatype, string requestOperation, Table datatable)
        {
            var keyvaluepairs = TableExtensions.ToDictionary(datatable);
            string serviceName = Config.serviceName;

            BusinessServiceClient service = new BusinessServiceClient();
            BaseMessageData MessageData = Config.SetConfigHeaders(Config.serviceName,Config.configTable);
            service.Endpoint.Behaviors.Add(new MessageInspectorCustomBehavior(ref MessageData));
            service.Endpoint.Behaviors.Add(new InspectorBehavior());

            XmlDocument soapXML = null;
            soapXML = MyMessageInspector.BeforeSendRequest(ref MessageData, channel);

            RequestXML = GetRequestByData(datatable, soapXML);

        }



        public static XmlDocument GetRequestByData(Table createRequestTable, XmlDocument soapXML)
        {
            Dictionary<string, string> myDictionary = TableExtensions.ToDictionary(createRequestTable);

            XmlDocument requestXML = new XmlDocument();

            soapXML[] = myDictionary[];



            return requestXML;
        }




    }
}
