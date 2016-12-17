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
        public double intRate;

        public LoanResponse()
        {

        }
        public LoanResponse(string ssn, double intRate)
        {
            this.ssn = ssn;
            this.intRate = intRate;
        }
    }
}
