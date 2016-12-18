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
        public static LoanResponse GetResponseFromJSON(string recievedResponse)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(LoanResponse));
            // Read string into memorystream first, so JSONSerializer can read it as bytes
            MemoryStream stream1 = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream1);
            writer.Write(recievedResponse);
            writer.Flush();
            stream1.Position = 0; // Rest stream to first position, so we read from beginning.

            LoanResponse handledRequest = (LoanResponse)ser.ReadObject(stream1);
            return handledRequest;
        }
        public static string GetJSONFromRequest(LoanRequest request)
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(LoanRequest));
            // Save original string before we work.
            string originalDurationString = request.loanDuration;

            // As JSON banks use days instead of Date from 1970, we convert it.
            string durationString = request.loanDuration.Replace("CET", "").Trim();
            DateTime durationDate = Convert.ToDateTime(durationString);
            var duration = durationDate - new DateTime(1970, 01, 01);
            request.loanDuration = duration.TotalDays.ToString();

            ser.WriteObject(stream1, request);

            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string jsonRequest = sr.ReadToEnd();
            request.loanDuration = originalDurationString;
            jsonRequest = jsonRequest.Replace(":\"", ":").Replace("\",", ",").Replace("\"}", "}");
            return jsonRequest;
        }
    }
}