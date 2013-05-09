using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Tracing;
using WebApiTracing.Infrastructure;


public static class TracingConfig
{
    public static void Register(HttpConfiguration config)
    {
        if (config == null)
        {
            throw new ArgumentNullException("config",
                @"Expected type HttpConfiguration.");
        }
        //SystemDiagnosticsTraceWriter writer = new SystemDiagnosticsTraceWriter()
        //{
        //    MinimumLevel = TraceLevel.Info,
        //    IsVerbose = false
        //};
        CustomTraceWriter writer = new CustomTraceWriter();
        config.Services.Replace(typeof(ITraceWriter), new CustomTraceWriter());
    }

}
