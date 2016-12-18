using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealYourBikeBank
{
    [Serializable]
    public class LoanResponse
    {

        public string ssn;
        public double interestRate;

        public LoanResponse()
        {

        }
        public LoanResponse(string ssn, double interestRate)
        {
            this.ssn = ssn;
            this.interestRate = interestRate;
        }
    }
}
