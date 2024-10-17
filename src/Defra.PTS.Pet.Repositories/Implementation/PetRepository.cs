using Defra.PTS.Pet.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Extensions.Logging;
using Defra.PTS.Pet.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Defra.PTS.Pet.Repositories.Implementation
{
    [ExcludeFromCodeCoverage]
    public class PetRepository(DbContext dbContext) : Repository<PetEntity>(dbContext), IPetRepository
    {        
        private PetDbContext? PetContext
        {
            get
            {
                return _dbContext as PetDbContext;
            }
        }

        public async Task CreatePet(PetEntity pet)
        {
            await PetContext!.Pet!.AddAsync(pet);
            await PetContext.SaveChangesAsync();            
        }

        public async Task<bool> PerformHealthCheckLogic()
        {
            // Attempt to open a connection to the database
            await PetContext!.Database.OpenConnectionAsync();

            // Check if the connection is open
            if (PetContext.Database.GetDbConnection().State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
