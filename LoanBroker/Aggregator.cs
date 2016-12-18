using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanBroker
{
    public class Aggregator
    {
        public static string FindBestInterestRate(Dictionary<string,LoanResponse> responses)
        {
            LoanResponse bestResponse = null;
            string bankName = "";
            foreach (var response in responses)
            {
                if (bestResponse == null)
                {
                    bestResponse = response.Value;
                    bankName = response.Key;
                }
                else
                {
                    if (response.Value.interestRate < bestResponse.interestRate)
                    {
                        bestResponse = response.Value;
                        bankName = response.Key;
                    }
                }
            }
            string returnString = "The best option for a loan is offered by: " + bankName + ", who offer an interest rate of: " + bestResponse.interestRate + "%";
            return returnString;
        }
    }
}