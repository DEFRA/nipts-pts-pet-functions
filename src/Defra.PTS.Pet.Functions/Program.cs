using Defra.PTS.Pet.ApiServices.Configuration;
using Defra.PTS.Pet.Functions.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureOpenApi()
    .ConfigureAppConfiguration(builder =>
    {
        builder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

#if DEBUG
        var connection = configuration.GetConnectionString("sql_db")
            ?? configuration["Values:sql_db"] ?? string.Empty;
#else
        var connection = configuration.GetConnectionString("sql_db") ?? string.Empty;
#endif

        services.AddDefraRepositoryServices(connection);
        services.AddDefraApiServices();
    })
    .Build();

await host.RunAsync();
