using AzureIsolatedFunc.CQRS.QueryCommandClasses;
using AzureIsolatedFunc.FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RepoLayer;

namespace AzureIsolatedFunc.Functions
{
    public class CRUD
    {
        private readonly ILogger<CRUD> _logger;
        private readonly IMediator _medi;

        public CRUD(ILogger<CRUD> logger, IMediator medi)
        {
            _logger = logger;
            _medi = medi;
        }

        [Function("Get")]
        public async Task<IActionResult> Get(
             [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            _logger.LogInformation("Get Request");

            string name = req.Query["name"];

            //calling mediator to send requ to handler
            var res = await _medi.Send(new GetQuery());

            return new OkObjectResult(res);
        }

        [Function("Create")]
        public async Task<IActionResult> Create(
             [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
        {
            _logger.LogInformation("Create request");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            TestModel user = JsonConvert.DeserializeObject<TestModel>(requestBody);

            var res = await _medi.Send(new AddCommand(user.Id, user.Name, user.Description));

            return new OkObjectResult(res);
        }

        /*   [Function("Update")]
           public async Task<IActionResult> UpdateUser([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req, int id)
           {
               _logger.LogInformation($"Processing update request for user with ID {id}.");

               string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
               SignInModel user = JsonConvert.DeserializeObject<SignInModel>(requestBody);

               // Validate the user model
               if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
               {
                   return new BadRequestObjectResult("error");
               }

               //updation logic for inmemory, similarly we can create a endpoint for deleting the data of specific id

               *//*  SignInModel existingUser = users.FirstOrDefault(u => u.Id == id);
                 if (existingUser == null)
                 {
                     return new NotFoundObjectResult("User not found.");
                 }

                 existingUser.Username = updatedUser.Username;
                 existingUser.Password = updatedUser.Password;*//*

               return new OkObjectResult($"User updated successfully. {id}");
           }*/
    }
}
