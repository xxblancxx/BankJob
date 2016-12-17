using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StealYourBikeBank
{
    class XMLConverter
    {

        public static LoanRequest GetRequestFromXML(string receievedRequest)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(LoanRequest));
            using (TextReader reader = new StringReader(receievedRequest))
            {
                LoanRequest handledRequest = (LoanRequest)serializer.Deserialize(reader);
                return handledRequest;
            }
        }

    }
}
