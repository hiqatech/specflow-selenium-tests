using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductTests.ACommon.SRC;
using ProdutcTests.Common.Steps.BackEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TechTalk.SpecFlow;

namespace ProductTests.ACommon.STEPS.BackEnd
{
    [Binding]
    class NewBusiness
    {

        [TestMethod]
        [Given(@"I send (.*) request to (.*)")]
        [When(@"I send (.*) request to (.*)")]
        [Then(@"I send (.*) request to (.*)")]
        public static void SendRequest(string method, string operation)
        {
            string xmlpayload = CreateTestData.RequestXML.ToString();

            XmlDocument response = Messenger.getMessenger(method, operation,xmlpayload);
        }


    }

}
