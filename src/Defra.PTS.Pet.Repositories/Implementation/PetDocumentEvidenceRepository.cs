using Defra.PTS.Pet.Domain.Entities;
using Defra.PTS.Pet.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defra.PTS.Pet.Repositories.Implementation
{
    [ExcludeFromCodeCoverage]
    public class PetDocumentEvidenceRepository : Repository<PetDocumentEvidenceEntity>, IPetDocumentEvidenceRepository
    {
        private PetDbContext petContext
        {
            get
            {
                return _dbContext as PetDbContext;
            }
        }

        public PetDocumentEvidenceRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> SavePetDocumentEvidence(List<PetDocumentEvidenceEntity> petDocumentEvidences)
        {
            petContext.PetDocumentEvidence.AddRange(petDocumentEvidences);
            await petContext.SaveChangesAsync();
            return true; 
        }
    }
}
