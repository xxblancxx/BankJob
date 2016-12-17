using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Xml;
using System.Threading.Tasks;

namespace LoanBroker
{
    public class DynamicSoapRequestHandler
    {
        public static async Task<string> SendSoapMessage(string url, string method, LoanRequest request)
        {
            HttpClientHandler handler = new HttpClientHandler();
            //Creates a new HttpClientHandler.
            handler.UseDefaultCredentials = true;
            //true if the default credentials are used; otherwise false. will use authentication credentials from the logged on user on your pc.
            using (HttpClient client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(url);
                var values = new Dictionary<string, string>
                     {
                    { "ssn", request.ssn},
                    { "creditScore",request.creditScore.ToString() },
                    { "loanAmount", request.loanAmount.ToString()},
                    { "loanDuration", request.loanDuration}
                     };


                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync(method,content).ConfigureAwait(continueOnCapturedContext: false); ;

                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();

                return responseString;
            }
        }

    }
}