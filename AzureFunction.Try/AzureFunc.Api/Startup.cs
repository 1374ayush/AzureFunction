using AzureFunc.Api;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(Startup))]

namespace AzureFunc.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //we can add services here.

            // Register custom middleware
            builder.Services.AddSingleton<IFunctionsWorkerMiddleware, CustomLoggingMiddleware>();

            // Add other services here, e.g., logging
            builder.Services.AddLogging();
        }
    }
}
