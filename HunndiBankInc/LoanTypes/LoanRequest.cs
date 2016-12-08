using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunndiBankInc
{
    [Serializable]
  public  class LoanRequest
    {
        public int ssn;
        public int creditScore;
        public double loanAmount;
        public string loanDuration; // duration = time betwen 01-01-1970 and this field.

        public LoanRequest()
        {

        }
        public LoanRequest(int _ssn, int _creditScore, double _loanAmount, DateTime _loanDuration)
        {
            ssn = _ssn;
            creditScore = _creditScore;
            loanAmount = _loanAmount;
            loanDuration = _loanDuration.ToString() + ".0 CET";
        }


    }
}
