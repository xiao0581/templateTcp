using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyServer.div
{
    public class ReadConfig
    {
        public int ServerPortNr { get; set; }
        public int shutDownPortNr { get; set; }
        public string ServerNametr { get; set; }

        public void readConfig(string file)
        {
            XmlDocument configDoc = new XmlDocument();
            configDoc.Load(file);
            XmlNode serverPortStr = configDoc.DocumentElement.SelectSingleNode("ServerPort");
            if (serverPortStr != null)
            {
                string xxStr = serverPortStr.InnerText.Trim();
                int xx = Convert.ToInt32(xxStr);
                ServerPortNr = xx;
                
            }
            XmlNode stopServerport = configDoc.DocumentElement.SelectSingleNode("shutDownPort");
            if (stopServerport != null)
            {
                string xxStr = stopServerport.InnerText.Trim();
                int xxx = Convert.ToInt32(xxStr);
                shutDownPortNr = xxx;
               
            }

            XmlNode serverNameStr = configDoc.DocumentElement.SelectSingleNode("ServerName");
            if (serverNameStr != null)
            {
                string xxxx = serverNameStr.InnerText.Trim();
                ServerNametr = xxxx;
            }

        }
    }
}
