using System;
using System.Xml;
using System.Threading;
using System.Reflection;
using System.Collections.Generic;

using ZolCo.ProductTests.BatchRunService;

namespace ZolCo.ProductTests.Utilities
{
    public class ServHelp
    {
        public static object _proxy = null;
        public static MethodInfo method = null;

        public static object[] GetServiceObject(string currentServiceName, string currentConfiguration, string MessageData, string requestName, XmlDocument serviceRequestObjectXML)
        {//ConfigurationsConfiguration configuration, BaseMessageData MessageData,
            ParameterInfo[] paramList = null;
            List<object> wcfparams = null;
            object[] paramsArray = null;
            object serviceObj = null;

            try
            {
                var node = serviceRequestObjectXML.SelectSingleNode("//*[contains(name()," + requestName + ")]");
                wcfparams = new List<object>();
                //_proxy = BusinessServicesObject.SetProxy(serviceName, configuration.Services, MessageData);
                method = _proxy.GetType().GetMethod(requestName);
                //paramList = BusinessServicesObject.GetServiceParameters(serviceName, requestName, configuration.Services, _proxy);

                foreach (ParameterInfo paramItem in paramList)
                {
                    if (paramItem.ParameterType.Name == typeof(string).Name)
                    {
                        serviceObj = node.InnerText.Trim();
                    }
                    else
                    {
                        serviceObj = Activator.CreateInstance(paramItem.ParameterType);
                    }
                    //wcfparams.Add(BusinessServicesObject.PopulateParamObj(serviceObj, paramItem.ParameterType.FullName, (XmlElement)node));
                    paramsArray = wcfparams.ToArray();
                }
                return paramsArray;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return paramsArray;
            }
        }

        public static object SendServiceObject(object[] paramsArray)
        {
            object serviceObjectResponse = null;

            serviceObjectResponse = Activator.CreateInstance(method.ReturnType);
            try
            {
                Console.WriteLine(DataHelp.DumpAsYaml(paramsArray));
                serviceObjectResponse = method.Invoke(_proxy, paramsArray);
                Console.WriteLine(DataHelp.DumpAsYaml(serviceObjectResponse));
                return serviceObjectResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Service Request Exception - " + ex.ToString());
                return serviceObjectResponse;
            }
        }

        public static string StartRegion(string clientName, string regionName, string userName)
        {
            var outputMessage = string.Empty;

            BatchServiceClient batchServiceClient = new BatchServiceClient();
            BatchRequest batchRequest = new BatchRequest();

            Console.WriteLine("Start Region " + regionName + " with userName " + userName + " for client " + clientName);

            try
            {
                batchServiceClient.StartRegion(clientName, regionName, userName, out outputMessage);
                Console.WriteLine("OutputMessage = " + outputMessage);
                return outputMessage;
            }
            catch (Exception ex)
            {
                Console.WriteLine("OutputMessage = " + outputMessage + "exception starting region : " + ex.ToString());
                return outputMessage;
            }
        }

        public static BatchResponse RunBatch(string clientName, string regionName, string dataBase, DateTime FromRunDate, DateTime ToRunDate, string userName)
        {
            BatchServiceClient batchServiceClient = new BatchServiceClient();
            BatchRequest batchRequest = new BatchRequest();

            batchRequest.ClientName = clientName;
            batchRequest.RegionName = regionName;
            batchRequest.Database = dataBase;
            batchRequest.FromRunDate = FromRunDate;
            batchRequest.FromRunNumber = 1;
            batchRequest.ToRunDate = ToRunDate;
            batchRequest.ToRunNumber = 1;
            batchRequest.Cycle = 9;
            batchRequest.Run = 9;
            batchRequest.Phase = 99;
            batchRequest.IncludeInternals = true;
            batchRequest.BatchType = BatchRunService.BatchType.Full;
            batchRequest.PricingType = BatchRunService.PricingType.None;
            batchRequest.IncludeOutputProcessing = false;
            batchRequest.UserId = userName;
            batchRequest.DoNotQueue = true;

            Console.WriteLine("Batch Service Request :");
            DataHelp.DumpAsYaml(batchRequest);

            Thread.Sleep(2000);

            var batchResponse = batchServiceClient.InvokeBatch(batchRequest);

            try
            {       
                Console.WriteLine("Batch Service Response :");
                DataHelp.DumpAsYaml(batchResponse);
                return batchResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception invoking batch : " + ex.ToString());
                return batchResponse;
            }
        }

        public static string GetBatchStatus(string clientName, string regionName, int jobNumber, string userName)
        {
            var outputMessage = string.Empty;
            string jobStatus = null;

            BatchServiceClient batchServiceClient = new BatchServiceClient();
            BatchRequest batchRequest = new BatchRequest();
            try
            {                          
                jobStatus = batchServiceClient.GetJobStatus(clientName, regionName, jobNumber, userName, out outputMessage).ToString();
                return jobStatus;
            }
            catch (Exception ex)
            {
                Console.WriteLine(outputMessage.ToString() + "exception getting batch status: " + ex.ToString());
                return jobStatus;
            }
        }

        public static void WaitBatchToComplete(string clientName, string regionName, int jobNumber, string userName)
        {
            BatchServiceClient batchServiceClient = new BatchServiceClient();
            BatchRequest batchRequest = new BatchRequest();
            var outputMessage = string.Empty;
            string jobStatus = GetBatchStatus(clientName, regionName, jobNumber, userName);
            int wait = 600;   
            
            try
            {
                while (jobStatus.ToString().Contains("Processing"))
                {
                    Thread.Sleep(2000);
                    jobStatus = GetBatchStatus(clientName, regionName, jobNumber, userName);
                    wait = wait - 2;
                    if (wait == 0)
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(outputMessage.ToString() + "exception getting batch status: " + ex.ToString());
            }

            if (jobStatus.ToString().Contains("Completed"))
            {
                Console.WriteLine("... Batch Running Completed");
            }
            else if (jobStatus.ToString().Contains("Processing"))
            {
                Console.WriteLine("Batch Running DID NOT Finished in 600 sec ... so Killed");
                batchServiceClient.KillJob(clientName, regionName, jobNumber, userName, out outputMessage);
            }
            else if (jobStatus.ToString().Contains("Failed"))
            {
                Console.WriteLine("Batch Running Failed");
            }
        }

    }
}
