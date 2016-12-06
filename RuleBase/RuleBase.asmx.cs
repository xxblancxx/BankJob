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
        public string GetBanks(int creditscore, bool isInRKI)
        {
            return "Den falske bank, den falske bank på kortet!";
        }
    }
}
