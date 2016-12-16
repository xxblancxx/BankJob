using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;

namespace LoanBroker
{
    public class JSONConverter
    {
        public static string GetJSONFromRequest(LoanRequest request)
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(LoanRequest));
            ser.WriteObject(stream1, request);

            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string jsonRequest = sr.ReadToEnd();
            return jsonRequest;
        }
    }
}