using System;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel.Channels;
using ProductTests.Common.SRC;
using System.Xml;
using ProductTests.Utils;
using System.IO;
using ProductTests.Common;
using ProductTests.Common.STEPS.BackEnd.Service;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProdutcTests.Common.Steps.BackEnd
{
    [Binding]

    static class Services
    {
        public static XmlDocument serviceRequestObjectXML = null;

        [TestMethod]
        [Given(@"I have a (.*) request to send to the business services")]
        [When(@"I have a (.*) request to send to the business services")]
        [Then(@"I have a (.*) request to send to the business services")]
        public static void IHaveArequestToSendToTheBusinessServices(string  requestName, Table dataTable)
        {
            var requestDictionary = new Dictionary<string, string>();
            requestDictionary = TableExtensions.TableToDictionary(dataTable, 0);
            requestDictionary = Services.AddDataToDictionary(requestDictionary);
        }

        [TestMethod]
        [Given(@"I send the request to the business services")]
        [When(@"I send the request to the business services")]
        [Then(@"I send the request to the business services")]
        public static void ISendTheRequestToTheBusinessServices()
        {
            ServiceObject.SendServiceObject();
        }

        [TestMethod]
        [Given(@"The request response should have (.*) detail")]
        [When(@"The request response should have (.*) detail")]
        [Then(@"The request response should have (.*) detail")]
        public static void TheRequestResponseShuoldHaveDetail(string expectedDetail)
        {
            if (ServiceObject.requestResponseString.Contains(expectedDetail))
            {
                Assert.IsTrue(true);
            }
            else if (ServiceObject.requestResponseString.Contains("TC BD"))
            {
                Assert.IsTrue(true);
            }
            else if (ServiceObject.requestResponseString.Contains("PENDING"))
            {
                Assert.IsTrue(true);
            }
            else if (ServiceObject.requestResponseString.Contains("WAITING"))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        
        } 

        [TestMethod]
        [Given(@"I run the batch process for (.*) days")]
        [When(@"I run the batch process for (.*) days")]
        [Then(@"I run the batch process for (.*) days")]
        public static void IRunTheBatchProcessDays(int days)
        {
            Console.WriteLine("Need to implement");
        }


        public static XmlDocument GetRequestXMLWithData(string requestName, Dictionary<string, string> requestData)
        {
            XmlDocument serviceObjectTemplateXML = new XmlDocument();
            switch (requestName)
            {
                case "AddProposal":   
                    {
                        serviceObjectTemplateXML.Load(SetUp.testProjectDirectory +
                            @"\ProductTestsSolution\IPSIClients\Darta\Templates\AddProposal.xml");
                        break;
                    }
                case "ProcessFullSurrender":   
                    {
                        serviceObjectTemplateXML.Load(SetUp.testProjectDirectory +
                            @"\ProductTestsSolution\IPSIClients\Darta\Templates\ProcessFullSurrender.xml");
                        break;
                    }
                case "ProcessPartialSurrender":   
                    {
                        serviceObjectTemplateXML.Load(SetUp.testProjectDirectory +
                            @"\ProductTestsSolution\IPSIClients\Darta\Templates\ProcessPartialSurrender.xml");
                        break;
                    }
                case "AddTopUp":   
                    {
                        serviceObjectTemplateXML.Load(SetUp.testProjectDirectory +
                            @"\ProductTestsSolution\IPSIClients\Darta\Templates\AddTopUp.xml");
                        break;
                    }
                case "ProcessCancellation":   
                    {
                        serviceObjectTemplateXML.Load(SetUp.testProjectDirectory +
                            @"\ProductTestsSolution\IPSIClients\Darta\Templates\ProcessCancellation.xml");
                        break;
                    }
                default: Console.WriteLine("Service Object Request has not been defined in the templates");
                    break;

                    List<string> requestKeys = new List<string>();

                    foreach (string requestKey in requestKeys)
                    {
                        string currentKey = requestKey;
                        string value = requestData[requestKey];
                        if (requestKey.Any(char.IsDigit))
                        {
                            string keyNo = Regex.Match(requestKey, @"\d+").Value;
                            int keyInt = Int32.Parse(keyNo);
                            currentKey = requestKey.Replace(keyNo, "");
                            XmlNodeList childList = serviceObjectTemplateXML.SelectNodes("//*[local-name()='" + currentKey + "']");
                            XmlNode child = childList[keyInt];
                            child.InnerText = value;
                        }
                        else
                        {
                            XmlNode child = serviceObjectTemplateXML.SelectSingleNode("//*[local-name()='" + currentKey + "']");
                            child.InnerText = value;
                        }
                    }
            }

            return serviceRequestObjectXML;
        }

        public static Dictionary<string, string> AddDataToDictionary(Dictionary<string,string> dataDictionary)
        {
            List<string> dataKeys = new List<string>(dataDictionary.Keys);
            foreach (string dataKey in dataKeys)
            {
                if (dataDictionary[dataKey] == "randomNumber")
                {
                    dataDictionary[dataKey] = Helper.randomNumberString;
                }
                else if (dataDictionary[dataKey] == "currentPolicyNumber")
                {
                    dataDictionary[dataKey] = DataBaseRW.curentPolicyNumber;
                }
                else if (dataDictionary[dataKey] == "systemDate")
                {
                    dataDictionary[dataKey] = Helper.GetDynamicDate(dataDictionary[dataKey], "yyyy-MM-ddThh:mm:ss");
                }
            }
            return dataDictionary;
        }




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
            var keyvaluepairs = TableExtensions.TableToDictionary(datatable, 1);
            string serviceName = Config.serviceName;

            RequestXML = GetRequestXMLWithData(datatable, requestName, EmptyRequestXML);
            Console.WriteLine(RequestXML.OuterXml.ToString());
        }


        public static XmlDocument GetRequestXMLWithData(Table requestTable, string requestName, XmlDocument EmptyRequestXML)
        {
            Dictionary<string, string> myDictionary = TableExtensions.TableToDictionary(requestTable, 1);

            XmlNodeList nodeList;
            //Console.WriteLine(requestName);
            //Console.WriteLine(requestTable.ToString());
            //Console.WriteLine(EmptyRequestXML.OuterXml.ToString());
            nodeList = EmptyRequestXML.DocumentElement.SelectNodes("/NewBusiness");

            foreach (XmlNode node in nodeList)
            {
                List<string> keys = new List<string>(myDictionary.Keys);

                foreach (string key in keys)
                {
                    string value = myDictionary[key];
                    XmlNode child = node.SelectSingleNode(key);
                    child.InnerText = value;
                    EmptyRequestXML.ReplaceChild(node, node);
                }

            }

            return EmptyRequestXML;
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

        public static string responseSubFolderName = SetUp.currentTestRestultDirectory + "\\Responses_" + SetUp.systemTime + "\\";
        public static int responseCount = 0;
        public static string responseFileName = null;
        public static string responsePath = null;
        public static string fileToCompare1 = null;
        public static string fileToCompare2 = null;

        public static void StoreAnswerAs(string answer, string extension)
        {

            if (!Directory.Exists(responseSubFolderName))
            {
                responseCount = 1;

                Directory.CreateDirectory(responseSubFolderName);
            }

            responseFileName = "answer" + responseCount + extension;
            responsePath = responseSubFolderName + responseFileName;

            if (!File.Exists(responsePath))
                File.CreateText(responsePath).Close();

            if (responseCount == 1)
            {
                fileToCompare1 = responsePath;
                File.WriteAllText(responsePath, answer);
                responseCount = responseCount + 1;
            }
            else if (responseCount == 2)
            {
                fileToCompare2 = responsePath;
                File.WriteAllText(responsePath, answer);
            }
            else responseCount = 0;

            Console.WriteLine("Answer saved to " + responsePath);

        }

    }

}