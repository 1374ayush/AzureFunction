using AzureIsolatedFunc.CustomErrors;
using AzureIsolatedFunc.FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RepoLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//////////////////////////////////////ERROR Due to Serialization //////////////////////////////////////////////////////////////

namespace AzureIsolatedFunc.Middlewares
{
    public class FluentValidationMiddleware : IFunctionsWorkerMiddleware
    {
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var httpRequestData = await context.GetHttpRequestDataAsync();

            if (httpRequestData.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                    using (var reader = new StreamReader(httpRequestData.Body))
                    {
                        var requestBody = await reader.ReadToEndAsync();

                    // Deserialize the request 
                    TestModel user = JsonConvert.DeserializeObject<TestModel>(requestBody);

                    var validator = new FluentValidatorr();
                    var validationResult = validator.Validate(user);

                    if (!validationResult.IsValid)
                    {
                        var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                        throw new ValidationException(errorMessages);
                    }
                    //reset the stream position
                    httpRequestData.Body.Position = 0;
                }
            }

            await next(context);

        }
    }
}
