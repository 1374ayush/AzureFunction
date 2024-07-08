using AzureIsolatedFunc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RepoLayer;
using System.Reflection;

var host = new HostBuilder()
     .ConfigureFunctionsWebApplication((worker) =>
     {
         // The worker.UseMiddleware is required to add middleware to the pipeline
         worker.UseMiddleware<globalExceptionMiddleware>();
     })
    .ConfigureServices(services =>
    {
        services.AddScoped<ICrudRepo, CrudRepo>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        /*services.AddSingleton<IFunctionsWorkerMiddleware, CustomLoggingMiddleware>();*/
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
