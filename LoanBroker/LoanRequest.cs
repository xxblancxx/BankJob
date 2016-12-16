using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanBroker
{
    [Serializable]
    public class LoanRequest
    {
        //public int ssn;
        public int Ssn { get; set; }
       // public int creditScore;
        public int CreditScore { get; set; }
       // public double loanAmount;
        public double LoanAmount { get; set; }
        //public string loanDuration; // duration = time betwen 01-01-1970 and this field.
        public string LoanDuration { get; set; }// duration = time betwen 01-01-1970 and this field.

        public LoanRequest()
        {

        }
        public LoanRequest(int ssn, int creditScore, double loanAmount, DateTime loanDuration)
        {
            Ssn = ssn;
            CreditScore = creditScore;
            LoanAmount = loanAmount;
            LoanDuration = loanDuration.ToString() + ".0 CET";
        }
    }
}