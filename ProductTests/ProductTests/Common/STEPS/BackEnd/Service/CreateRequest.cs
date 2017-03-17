using TechTalk.SpecFlow;
using ProductTests.Utils;
using System.Xml;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System;
using System.Collections.Generic;

namespace ProdutcTests.Common.Steps.BackEnd
{
    [Binding]
    public class CreateRequest

    {
        
        public static IClientChannel channel;
        private static object serviceobj;
        private static object paramItem;
        public static string xmlpath;

        public static XmlDocument GetEmptyRequestXML(string requestName)
        {
            //BusinessServiceClient service = new BusinessServiceClient();
            //
            //MethodInfo method = service.GetType().GetMethod(requestName);
            //ParameterInfo[] paramList =BusinessServicesObject.GetServiceParameters(method, requestName);
            //
            //
            //XmlDocument objectXML=null;
            //XmlNodeList nodelist = objectXML.DocumentElement.SelectNodes("/" + requestName);
            //
            //if (paramItem.ParameterType.Name = GetType(String).Name)
            //obj = Activator.CreateInstance(paramItem.ParameterType);
            //
            //wcfparams.Add(PopulateParamObj(obj, paramItem.ParameterType.FullName, node))
            //
            //object[] paramsArray = null;
            //if (wcfparams.Count != 0)
            //{
            //    paramsArray = wcfparams.ToArray();
            //}
            //
            //object result = null;
            //result = Activator.CreateInstance(method.ReturnType);
            //result = method.Invoke(service, paramsArray);

            XmlDocument xml = new XmlDocument();

            xmlpath =@"C:\Dev\IPSI\ProductTests\ProductTestsProject\ProductTestsSolution\IPSIClients\Darta\Templates\example.xml";
            Console.WriteLine(xmlpath);

            if (requestName == "NewBusiness")
                xml.Load(xmlpath);
            XmlDocument EmptyRequestXML = xml;

            return EmptyRequestXML;
        }


        public static XmlDocument GetRequestXMLWithData(Table requestTable, string requestName, XmlDocument EmptyRequestXML)
        {

            Dictionary<string, string> myDictionary = TableExtensions.DataToDictionary(requestTable,"vertical",0,1);

            XmlNodeList nodeList;
            
            Console.WriteLine(requestName);
            Console.WriteLine(requestTable.ToString());
            Console.WriteLine(EmptyRequestXML.OuterXml.ToString());
            
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

    }



}
