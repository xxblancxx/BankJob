using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace LoanBroker
{
    public class DynamicSoapRequestHandler
    {

        public static void SendRequestSoap(string url, string method, string xmlPayload)
        {
            //string result = "";
            string URL_ADDRESS = url + method+ "&o=POST";  //TODO: customize to your needs


            // Create the web request
            HttpWebRequest request = WebRequest.Create(new Uri(URL_ADDRESS)) as HttpWebRequest;

            // Set type to POST
            request.Method = "POST";
            request.ContentType = "application/xml";

            // Create the data we want to send
            StringBuilder data = new StringBuilder();
            data.Append(xmlPayload);
            byte[] byteData = Encoding.UTF8.GetBytes(data.ToString());      // Create a byte array of the data we want to send
            request.ContentLength = byteData.Length;                        // Set the content length in the request headers

            // Write data to request
            using (Stream postStream = request.GetRequestStream())
            {
                postStream.Write(byteData, 0, byteData.Length);
            }

            //// Get response and return it
            //try
            //{
            //    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            //    {
            //        StreamReader reader = new StreamReader(response.GetResponseStream());
            //        result = reader.ReadToEnd();
            //        reader.Close();
            //    }
            //}
            //catch (Exception e)
            //{
            //    result = e.Message;  //TODO: returns an XML with the error message
            //}
            //return result;
        }
    }
}