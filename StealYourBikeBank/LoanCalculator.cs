using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StealYourBikeBank
{
    public class LoanCalculator
    {


        public static double CalculateCreditscore(int creditScore, double loanAmount, DateTime loanDuration)
        {
            //string durationRaw = loanDuration.Remove(19);
            var duration = loanDuration - new DateTime(1970, 01, 01, 01, 00, 00);

            if (duration.TotalDays < 0 || loanAmount < 0 || creditScore < 0)
            {
                // No negative numbers.
                throw new ArgumentOutOfRangeException();
            }

            if (loanAmount <= 100000 && loanAmount > 0 && creditScore < 500 && duration.TotalDays < 365)
            {
                Random r = new Random();
                int randomMultiplier = r.Next(1, 10);
                double randomPercentage = (double)randomMultiplier / 10;
                double randomRate = 1+(randomPercentage);
                if (creditScore < 500 && creditScore > 250)
                {
                    // bad rate
                    if (duration.TotalDays == 365)
                    { // best we can do.
                        return 7* randomRate;   
                    }
                    else if (duration.TotalDays <= 183 && duration.TotalDays > 30)
                    { // still not bad
                        return 10 * randomRate;
                    }
                    else
                    { // You must be in a bad situation...
                        return 12 * randomRate;
                    }
                }
                else if (creditScore <= 250)
                {
                    if (duration.TotalDays == 365)
                    { // best we can do.
                        return 15 * randomRate;
                    }
                    else if (duration.TotalDays <= 183 && duration.TotalDays > 30)
                    { // still not bad
                        return 22 * randomRate;
                    }
                    else
                    { // You must be in a bad situation...
                        return 30 * randomRate;
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
