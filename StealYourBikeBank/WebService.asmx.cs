using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace StealYourBikeBank
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public LoanResponse RequestLoan(string ssn, int creditScore, double loanAmount, string loanDuration)
        {
            

            var interestRate = LoanCalculator.CalculateCreditscore(creditScore, loanAmount, loanDuration);
            var response = new LoanResponse(ssn, interestRate);
            return response;
        }
    }
}
