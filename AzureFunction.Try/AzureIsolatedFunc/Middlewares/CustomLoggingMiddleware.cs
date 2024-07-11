using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureIsolatedFunc.Middlewares
{
    public class CustomLoggingMiddleware : IFunctionsWorkerMiddleware
    {
        private readonly ILogger _logger;

        public CustomLoggingMiddleware(ILogger<CustomLoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            _logger.LogInformation("Executing custom middleware before function execution.");

            await next(context);

            _logger.LogInformation("Executing custom middleware after function execution.");
        }
    }
}
