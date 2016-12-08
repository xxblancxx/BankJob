using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunndiBankInc.MakeLoan
{
    public class HBI_MakeLoan
    {
        public HBI_MakeLoan()
        {

        }

        public HBI_MakeLoan(int _ssn, int _creditScore, double _loanAmount, string _loanDuration)
        {

            Random makeIntrest = new Random();
            int ran = makeIntrest.Next(5);

            double interest = makeIntrest.NextDouble()+ran;

            Console.WriteLine("The interest rate is going to be "+interest);

            //HBI_XMLConverter messege = HBI_XMLConverter.GetXMLFromLoanRequest()

        }
    }
}
