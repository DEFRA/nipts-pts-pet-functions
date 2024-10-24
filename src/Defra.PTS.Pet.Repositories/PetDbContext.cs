using Microsoft.EntityFrameworkCore;
using Defra.PTS.Pet.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Defra.PTS.Pet.Repositories
{
    [ExcludeFromCodeCoverage]
    public class PetDbContext(DbContextOptions<PetDbContext> options) : DbContext(options)
    {
        public DbSet<PetEntity>? Pet { get; set; }
        public DbSet<PetDocumentEvidenceEntity>? PetDocumentEvidence { get; set; }
    }
}