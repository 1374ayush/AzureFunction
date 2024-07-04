using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunc.Api.Model;
using System.Collections.Generic;

namespace AzureFunc.Api
{
    public class AuthFunction
    {
        [FunctionName("Login")]
        public async Task<IActionResult> Login(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "login")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP login trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            SignInModel user = JsonConvert.DeserializeObject<SignInModel>(requestBody);

            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return new BadRequestObjectResult("Please provide both username and password.");
            }

           /* string responseMessage = string.IsNullOrEmpty(name)
                ? "Post request hit"
                : $"Get request hit";*/

            return new OkObjectResult(user);
        }

        [FunctionName("Update")]
        public async Task<IActionResult> UpdateUser(
           [HttpTrigger(AuthorizationLevel.Function, "put", Route = "users/{id}")] HttpRequest req,
        ILogger log, int id)
        {
            log.LogInformation($"Processing update request for user with ID {id}.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            SignInModel user = JsonConvert.DeserializeObject<SignInModel>(requestBody);

            // Validate the user model
            else if ( user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return new BadRequestObjectResult("error");
            }

            //updation logic for inmemory, similarly we can create a endpoint for deleting the data of specific id

          /*  SignInModel existingUser = users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return new NotFoundObjectResult("User not found.");
            }

            existingUser.Username = updatedUser.Username;
            existingUser.Password = updatedUser.Password;*/

            return new OkObjectResult($"User updated successfully. {id}");
        }
    }
}
