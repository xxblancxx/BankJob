using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace RuleBase
{
    /// <summary>
    /// Summary description for RuleBase
    /// </summary>
    // Because this is a school assignment, the webservice will not be deployed. only local.
    [WebService(Namespace = "http://localhost/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RuleBase : System.Web.Services.WebService
    {
        [WebMethod]
        public List<Bank> GetBanks(int creditscore, bool isInRKI, double amount)
        {
            var allBanks = new List<Bank>();
            allBanks.Add(new Bank(500,999999999, false, "datdb.cphbusiness.dk", "cphbusiness.bankXML"));
            allBanks.Add(new Bank(500,999999999, false, "datdb.cphbusiness.dk", "cphbusiness.bankJSON"));
            allBanks.Add(new Bank(0,499,50000,true, "datdb.cphbusiness.dk", "HBITestQue"));

            var suitableBanks = new List<Bank>();
            foreach (var bank in allBanks)
            {
                // Creditscore should always be more than minimal.
                // Either banks allow RKI - or if they dont, customer cannot be in RKI.
                if (creditscore >= bank.MinimumCreditscore && creditscore <= bank.MaximumCreditscore && ( bank.AllowsRKI ||(bank.AllowsRKI == false && isInRKI == false)))
                {
                    if (amount <= bank.MaximumAmount)
                    {
                        suitableBanks.Add(bank);
                    }
                }
            }

            return suitableBanks;
        }
    }
}
