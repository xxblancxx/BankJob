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

        public int Ssn { get; set; }
        public double IntRate { get; set; }

        public LoanResponse()
        {

        }
        public LoanResponse(int ssn, double intRate)
        {
            Ssn = ssn;
            IntRate = intRate;
        }
    }
}
