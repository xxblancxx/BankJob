using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunndiBankInc
{
    [Serializable]
    public class LoanResponse
    {
        public int ssn;
        public double intRate;

        public LoanResponse()
        {

        }
        public LoanResponse(int _ssn, double _intRate)
        {
            this.ssn = _ssn;
            this.intRate = _intRate;
        }
    }
}
