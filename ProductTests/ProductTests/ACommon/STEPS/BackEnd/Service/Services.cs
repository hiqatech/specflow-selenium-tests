using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductTests.Common;
using ProductTests.DartaBusinessServices;
using System.ServiceModel.Channels;
using ProductTests.ACommon.SRC;
using System.ServiceModel;

namespace ProdutcTests.Common.Steps.BackEnd
{
    [Binding]
    static class Services
    {

        [TestMethod]
        [Given(@"I ping the (.*) with")]
        [When(@"I ping the (.*) with")]
        [Then(@"I ping the (.*) with")]
        public static void GetMessageData(string serviceName, Table setConfigTable)
        {
            BusinessServiceClient service = new BusinessServiceClient();
            //BaseMessageData MessageData = Config.SetConfigHeaders(serviceName,setConfigTable);
            //service.Endpoint.Behaviors.Add(new MessageInspectorCustomBehavior(ref MessageData));
            //service.Endpoint.Behaviors.Add(new InspectorBehavior());

            DateTime SystemDate = new DateTime();

            SystemDate = service.GetSystemDate();

            Console.WriteLine(SystemDate);
        }


    }

}