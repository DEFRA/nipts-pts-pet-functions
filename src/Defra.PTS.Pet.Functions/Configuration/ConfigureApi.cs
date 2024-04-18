using Defra.PTS.Pet.ApiServices.Implementation;
using Defra.PTS.Pet.ApiServices.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;


namespace Defra.PTS.Pet.Functions.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class ConfigureApi
    {
        public static IServiceCollection AddDefraApiServices(this IServiceCollection services)
        {
            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IBreedService, BreedService>();
            return services;
        }
    }
}
