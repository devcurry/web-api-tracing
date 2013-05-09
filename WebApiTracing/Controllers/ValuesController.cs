
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Tracing;

namespace WebApiTracing.Controllers
{
    public class ValuesController : ApiController
    {
        private void WriteToTrace(string category, string log, System.Web.Http.Tracing.TraceLevel level)
        {
            ITraceWriter writer = Configuration.Services.GetTraceWriter();
            if (writer != null)
            {
                writer.Trace(
                    Request, category, level,
                    (traceRecord) =>
                    {
                        traceRecord.Message =
                            String.Format("Inside CustomCode with param = {0}", log);
                    });
            }
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            WriteToTrace("Controllers", "There are 2 elements in the return array", TraceLevel.Info);
            return new string[] { "value1", "value2" };

        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}