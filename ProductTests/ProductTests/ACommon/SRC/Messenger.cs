using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ProductTests.ACommon.SRC
{
    class Messenger
    {

        public static XmlDocument getMessenger(string method, string operation, string xmlPayload)
        {
            string result = "";
            string CREDENTIALS = "PASSword123";
            string URL_ADDRESS = "http://www.client.com/_ws/" + method + "?sso=" + CREDENTIALS + "&o=" + operation +;  
            
            HttpWebRequest request = WebRequest.Create(new Uri(URL_ADDRESS)) as HttpWebRequest;

            request.Method = "POST";
            request.ContentType = "application/xml";

            StringBuilder data = new StringBuilder();
            data.Append(xmlPayload);
            byte[] byteData = Encoding.UTF8.GetBytes(data.ToString());      
            request.ContentLength = byteData.Length;

            using (Stream postStream = request.GetRequestStream())
            {
                postStream.Write(byteData, 0, byteData.Length);
            }

            XmlDocument xmlResult = new XmlDocument();
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    result = reader.ReadToEnd();
                    reader.Close();
                }
                xmlResult.LoadXml(result);
            }
            catch (Exception e)
            {
                //xmlResult = CreateErrorXML(e.Message, "");  //TODO: returns an XML with the error message
            }
            return xmlResult;
        }
    }
}
