using Defra.PTS.Pet.Repositories;
using Defra.PTS.Pet.Repositories.Implementation;
using Defra.PTS.Pet.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defra.PTS.Pet.ApiServices.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class ConfigureRepositories
    {
        public static IServiceCollection AddDefraRepositoryServices(this IServiceCollection services, string conn)
        {
            services.AddDbContext<PetDbContext>((context) =>
            {
                context.UseSqlServer(conn);
            });
            services.AddScoped<DbContext, PetDbContext>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IPetDocumentEvidenceRepository, PetDocumentEvidenceRepository>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
