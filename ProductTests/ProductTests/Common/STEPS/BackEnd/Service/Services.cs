using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel.Channels;
using ProductTests.Common.SRC;
using System.Xml;
using ProductTests.Utils;

namespace ProdutcTests.Common.Steps.BackEnd
{
    [Binding]
    static class Services
    {

        public static XmlDocument EmptyRequestXML;
        public static XmlDocument RequestXML;

        [TestMethod]
        [Given(@"I ping the (.*) with")]
        [When(@"I ping the (.*) with")]
        [Then(@"I ping the (.*) with")]
        public static void GetMessageData(string serviceName, Table setConfigTable)
        {
           // BusinessServiceClient service = new BusinessServiceClient();
           // BaseMessageData MessageData = Config.SetConfigHeaders(serviceName, setConfigTable);
           // service.Endpoint.Behaviors.Add(new MessageInspectorCustomBehavior(ref MessageData));

            DateTime SystemDate = new DateTime();

           // SystemDate = service.GetSystemDate();

            Console.WriteLine(SystemDate);
        }
      

        [Given(@"I have a (.*) request to (.*)")]
        [When(@"I have a (.*) request to (.*)")]
        [Then(@"I have a (.*) request to (.*)")]
        public static void IHaveARequest(string requestName, string operation, Table datatable)
        {
            var keyvaluepairs = TableExtensions.DataToDictionary(datatable,"vertical",0,1);
            string serviceName = Config.serviceName;

            EmptyRequestXML = CreateRequest.GetEmptyRequestXML(requestName);
                
            RequestXML = CreateRequest.GetRequestXMLWithData(datatable, requestName, EmptyRequestXML);
            Console.WriteLine(RequestXML.OuterXml.ToString());

        }


        [TestMethod]
        [Given(@"I send (.*) request to (.*)")]
        [When(@"I send (.*) request to (.*)")]
        [Then(@"I send (.*) request to (.*)")]
        public static void SendRequest(string method, string operation)
        {
            string xmlpayload = RequestXML.ToString();

            XmlDocument response = Messenger.getMessenger(method, operation, xmlpayload);
        }


    }

}