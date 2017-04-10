using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using YamlDotNet.Serialization;

namespace ProductTests.Common.STEPS.BackEnd.Service
{
    class ServiceObject
    {
        public static object _proxy = null;
        //public static MethodInfo method = null;
        //public static ParameterInfo[] paramList = null;
        public static List<object> wcfparams = null;
        public static object[] paramsArray = null;
        public static object serviceObj = null;
        public static XmlDocument serviceObjectXml = null;
        public static object serviceObjectResponse = null;
        public static string requestResponseString = null;

        public static void GetServiceObject(string requestName, XmlDocument serviceRequestObjectXml)
        {
            var node = serviceObjectXml.SelectSingleNode("//*[contains(name()," + requestName + ")]");
            wcfparams = new List<object>();
            //_proxy = BusinessServiceObject.SetProxy(Config.currentServiceName, Config._currentConfiguration.Services,Config.MessageData);
            //method = _proxy.GetType.getMethod(requestName);
            //paramList = BusinessServiceObject.GetServiceParameters(Config.currentServiceName,requestName, Config._currentConfiguration.Services,_proxy);

            //foreach (ParameterInfo paramItem in paramList)
            //{
            //    if (paramItem.ParameterType.Name == typeof(string).Name)
            //    {
            //        serviceObj = node.InnerText.Trim();
            //    }
            //    else
            //    {
            //        serviceObj = Activator.CreateInstance(paramItem.parameterType);
            //    }
            //    wcfparams.Add(BusinessServiceObject.PopulateParamObj(serviceObj, paramItem.paramaterType.FullName, (XmlElement)node));
            //    paramsArray = wcfparams.ToArray();
            //}

        }

        public static void SendServiceObject()
        {
            //serviceObjectResponse = Activator.CreateInstance(method.returnType);
            //try
            //{
            //    serviceObjectResponse = method.Invoke(_proxy, paramsArray);
            //    //requestResponseString = XMLServices.Serialize(serviceObjectResponse);
            //}
            //catch (Exception ex)
            //{
            //    requestResponseString = ex.ToString();
            //    DumpAsYaml(paramsArray);
            //}
        }


        public static void DumpAsYaml(object objectToDump)
        {
            var stringBuilder = new StringBuilder();
            var serializer = new Serializer();
            serializer.Serialize(new IndentedTextWriter(new StringWriter(stringBuilder)), objectToDump);
            Console.WriteLine(stringBuilder);
        }


    }
}










