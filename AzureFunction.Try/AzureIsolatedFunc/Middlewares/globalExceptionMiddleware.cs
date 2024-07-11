using AzureIsolatedFunc.CustomErrors;
using AzureIsolatedFunc.Model;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace AzureIsolatedFunc.Middlewares
{
    public class globalExceptionMiddleware : IFunctionsWorkerMiddleware
    {
        private readonly ILogger _logger;

        public globalExceptionMiddleware(ILogger<CustomLoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task Invoke(FunctionContext httpContext, FunctionExecutionDelegate next)
        {
            _logger.LogInformation("Global exception handler executed.....");

            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(FunctionContext context, Exception exception)
        {
            _logger.LogInformation("HandleExceptionAsync method called.....");

            var httpRequestData = await context.GetHttpRequestDataAsync();
            var response = httpRequestData.CreateResponse();

            response.Headers.Add("Content-Type", "application/json");


            var errorResponse = new ErrorResponsee<object>
            {
                Status = "fail"
            };

            switch (exception)
            {
                case ValidationException validationException:
                    response.StatusCode = HttpStatusCode.BadRequest;
                    errorResponse.Message = validationException.Errors;
                    break;


                default:
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    errorResponse.Message = "Internal server error!";
                    break;
            }

            using (var stringWriter = new StringWriter())
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(stringWriter, errorResponse);
                string errorJson = stringWriter.ToString();

                await response.WriteStringAsync(errorJson);
            }

            context.GetInvocationResult().Value = response;

        }
    }
    //await context.Response.WriteAsync("error occured");
}
