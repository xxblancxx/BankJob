using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RuleBase
{
    [Serializable]
    public class Bank
    {
        private string _exchange;
        public int MinimumCreditscore { get; set; }
        public int MaximumCreditscore { get; set; }
        public double MaximumAmount { get; set; }
        public DateTime MaximumDuration { get; set; }
        public bool AllowsRKI { get; set; }
        public string Host { get; set; }
        public bool UsesMessaging { get; set; } // Otherwise its webservice
        public string Exchange
        {
            get
            {
                if (UsesMessaging)
                {
                    return _exchange;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                _exchange = value;
            }
        }


        public Bank(int minimumCreditscore, double maxLoan, DateTime maxDuration, bool allowsRKI, string host)
        {
            // This is webservice bank.
            UsesMessaging = false;
            MaximumDuration = maxDuration;
            MinimumCreditscore = minimumCreditscore;
            MaximumCreditscore = 800;
            MaximumAmount = maxLoan;
            AllowsRKI = allowsRKI;
            Host = host;
        }

        public Bank(int minimumCreditscore, double maxLoan,  DateTime maxDuration, bool allowsRKI, string host, string exchange)
        {
            // This is RabbitMQ bank.
            UsesMessaging = true;

            MinimumCreditscore = minimumCreditscore;
            MaximumCreditscore = 800;
            MaximumAmount = maxLoan;
            AllowsRKI = allowsRKI;
            Host = host;
            Exchange = exchange;
        }
        public Bank(int minimumCreditscore, int maximumCreditscore, DateTime maxDuration, double maxLoan, bool allowsRKI, string host)
        {
            // This is webservice bank.
            UsesMessaging = false;

            MinimumCreditscore = minimumCreditscore;
            MaximumCreditscore = maximumCreditscore;
            MaximumAmount = maxLoan;
            AllowsRKI = allowsRKI;
            Host = host;
        }

        public Bank(int minimumCreditscore,int maximumCreditscore, DateTime maxDuration, double maxLoan, bool allowsRKI, string host, string exchange)
        {
            // This is RabbitMQ bank.
            UsesMessaging = true;

            MinimumCreditscore = minimumCreditscore;
            MaximumCreditscore = maximumCreditscore;
            MaximumAmount = maxLoan;
            AllowsRKI = allowsRKI;
            Host = host;
            Exchange = exchange;
        }
        public Bank()
        {

        }
    }
}