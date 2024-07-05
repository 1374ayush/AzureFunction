using AzureIsolatedFunc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
     .ConfigureFunctionsWebApplication((worker) =>
     {
         // The worker.UseMiddleware is required to add middleware to the pipeline
         worker.UseMiddleware<globalExceptionMiddleware>();
     })
    .ConfigureServices(services =>
    {
        /*services.AddSingleton<IFunctionsWorkerMiddleware, CustomLoggingMiddleware>();*/
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
