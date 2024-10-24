using Defra.PTS.Pet.ApiServices.Configuration;
using Defra.PTS.Pet.Functions.Configuration;
using Defra.Trade.Common.AppConfig;
using Defra.Trade.Common.Sql.Infrastructure;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Namotion.Reflection;
using System.Diagnostics.CodeAnalysis;
using System.IO;

[assembly: FunctionsStartup(typeof(Defra.PTS.Pet.Functions.Startup))]
namespace Defra.PTS.Pet.Functions
{
    [ExcludeFromCodeCoverage]
    public class Startup : FunctionsStartup
    {
        private static IConfiguration Configuration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(IFunctionsHostBuilder builder)
        {
            Configuration = builder.GetContext().Configuration;

            var connection = string.Empty;
#if DEBUG

            connection = Configuration.GetConnectionStringOrSetting("sql_db");
#else
            connection = Configuration.GetConnectionString("sql_db");
#endif

            builder.Services.AddDefraRepositoryServices(connection);
            builder.Services.AddDefraApiServices();
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            builder.ConfigurationBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        }
    }
}
