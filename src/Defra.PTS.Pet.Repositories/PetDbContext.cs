using Microsoft.EntityFrameworkCore;
using Defra.PTS.Pet.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Defra.PTS.Pet.Repositories
{
    [ExcludeFromCodeCoverage]
    public class PetDbContext : DbContext
    {
        //Configuration from settings
        public PetDbContext(DbContextOptions<PetDbContext> options) : base(options)
        {

        }

        public DbSet<PetEntity> Pet { get; set; }
        public DbSet<PetDocumentEvidenceEntity> PetDocumentEvidence { get; set; }
    }
}