using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RabbitTestClient
{
    class XMLConverter
    {

        public static string GetXMLFromLoanRequest(LoanRequest request)
        {
           
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(LoanRequest));
            string xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    serializer.Serialize(writer, request);
                    xml = sww.ToString(); // Your XML

                    return xml;
                }
            }
        }

    }
}
