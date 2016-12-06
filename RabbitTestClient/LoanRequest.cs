using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RabbitTestClient
{
    [Serializable]
    public class LoanRequest
    {
        public int ssn;
        public int creditScore;
        public double loanAmount;
        public string loanDuration; // duration = time betwen 01-01-1970 and this field.

        public LoanRequest()
        {

        }
        public LoanRequest(int ssn, int creditScore, double loanAmount, DateTime loanDuration)
        {
            this.ssn = ssn;
            this.creditScore = creditScore;
            this.loanAmount = loanAmount;
            this.loanDuration = loanDuration.ToString() + ".0 CET";
        }
    }
}