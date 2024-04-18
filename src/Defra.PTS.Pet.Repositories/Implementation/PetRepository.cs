using Defra.PTS.Pet.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Extensions.Logging;
using Defra.PTS.Pet.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Defra.PTS.Pet.Repositories.Implementation
{
    [ExcludeFromCodeCoverage]
    public class PetRepository : Repository<PetEntity>, IPetRepository
    {        
        private PetDbContext petContext
        {
            get
            {
                return _dbContext as PetDbContext;
            }
        }

        public PetRepository(DbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task CreatePet(PetEntity pet)
        {
            await petContext.Pet.AddAsync(pet);
            await petContext.SaveChangesAsync();            
        }

        public async Task<bool> PerformHealthCheckLogic()
        {
            // Attempt to open a connection to the database
            await petContext.Database.OpenConnectionAsync();

            // Check if the connection is open
            if (petContext.Database.GetDbConnection().State == ConnectionState.Open)
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
