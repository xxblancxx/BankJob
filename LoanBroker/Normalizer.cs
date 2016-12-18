using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanBroker
{
    public class Normalizer
    {
        public static Dictionary<string, LoanResponse> Normalize(Dictionary<string, string> rawResponses)
        {
            Dictionary<string, LoanResponse> responses = new Dictionary<string, LoanResponse>();
            foreach (var response in rawResponses)
            {
                // Look for XML style closing tag
                if (response.Value.Contains("</"))
                { // its XML
                    var loanResponse = XMLConverter.GetResponseFromXML(response.Value);
                    responses.Add(response.Key, loanResponse);
                }
                // Look for JSON style closing tag
                else if (response.Value.Contains("}"))
                { // its JSON
                    var loanResponse = JSONConverter.GetResponseFromJSON(response.Value);
                    responses.Add(response.Key, loanResponse);
                }
            }
            return responses;
        }
    }
}