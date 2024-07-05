﻿using AzureIsolatedFunc.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AzureIsolatedFunc
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

            var errorResponse = new ErrorResponsee
            {
                Success = false,
                Message = exception.Message
            };

            response.StatusCode = HttpStatusCode.InternalServerError;

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
