using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.Tracing;
using System.Xml;

namespace WebApiTracing.Infrastructure
{
    public class CustomTraceWriter : ITraceWriter
    {
        public void Trace(System.Net.Http.HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            TraceRecord rec = new TraceRecord(request, category, level);
            traceAction(rec);
            string path = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), "log.xml");
            using (Stream xmlFile = new FileStream(path, FileMode.Append))
            {
                using (XmlTextWriter writer = new XmlTextWriter(xmlFile, Encoding.UTF8))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartElement("trace");
                    writer.WriteElementString("ts", rec.Timestamp.ToString());
                    writer.WriteElementString("oper", rec.Operation);
                    writer.WriteElementString("user", rec.Operator);
                    writer.WriteElementString("log", rec.Message);
                    writer.WriteElementString("category", rec.Category);
                    writer.WriteEndElement();
                    writer.WriteString(Environment.NewLine);
                }
            }
        }
    }

}