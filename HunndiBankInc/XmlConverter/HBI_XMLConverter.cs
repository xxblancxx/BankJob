using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HunndiBankInc
{
    class HBI_XMLConverter
    {
        public static LoanRequest GetRequestFromXML(string receievedRequest)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(LoanRequest));
            using (TextReader reader = new StringReader(receievedRequest))
            {
                LoanRequest handledRequest = (LoanRequest) serializer.Deserialize(reader);
                return handledRequest;
            }
        }

        public static string GetXMLFromLoanResponse(LoanResponse response)
        {

            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(LoanResponse));
            string xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    serializer.Serialize(writer, response);
                    xml = sww.ToString(); // Your XML

                    return xml;
                }
            }
        }
  
    }
}
