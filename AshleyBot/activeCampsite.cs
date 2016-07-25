using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CampsiteBot
{
    class activeCampsite
    {
        public static XmlDocument CampgroundsInState(string state)
        {
            if (string.IsNullOrWhiteSpace(state))
                return null;

            string url = $" http://api.amp.active.com/camping/campgrounds?{state}&api_key=bt8dfnusu24wdpecmcpzc4sy";

            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(response.GetResponseStream());
                return xmlDoc;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.Read();
                return null;
            }


        }
    }

}
