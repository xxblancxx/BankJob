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


        public static double CalculateInterestRate(LoanRequest request)
        {
            string durationRaw = request.loanDuration.Replace("CET", "").Trim();
            var duration = Convert.ToDateTime(durationRaw) - new DateTime(1970, 01, 01);

            if (duration.TotalDays < 0 || request.loanAmount < 0 || request.creditScore < 0)
            {
                // No negative numbers.
                throw new ArgumentOutOfRangeException();
            }

            if (request.loanAmount <= 50000 && request.loanAmount > 0 && request.creditScore < 500 && duration.TotalDays < 365)
            {

                if (request.creditScore < 500 && request.creditScore > 250)
                {
                    // bad rate
                    if (duration.TotalDays == 365)
                    { // best we can do.
                        return 10;
                    }
                    else if (duration.TotalDays <= 183 && duration.TotalDays > 30)
                    { // still not bad
                        return 15;
                    }
                    else
                    { // You must be in a bad situation...
                        return 20;
                    }
                }
                else if (request.creditScore <= 250)
                {
                    if (duration.TotalDays == 365)
                    { // best we can do.
                        return 25;
                    }
                    else if (duration.TotalDays <= 183 && duration.TotalDays > 30)
                    { // still not bad
                        return 35;
                    }
                    else
                    { // You must be in a bad situation...
                        return 50;
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                // Hunndi vil ikke låne til folk med andet end miserable credit score.
                // og max et år.
                throw new ArgumentOutOfRangeException();
            }

        }
    }
}
