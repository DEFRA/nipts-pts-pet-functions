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
    public class PetDocumentEvidenceRepository(DbContext dbContext) : Repository<PetDocumentEvidenceEntity>(dbContext), IPetDocumentEvidenceRepository
    {
        private PetDbContext? PetContext
        {
            get
            {
                return _dbContext as PetDbContext;
            }
        }

        public async Task<bool> SavePetDocumentEvidence(List<PetDocumentEvidenceEntity> petDocumentEvidences)
        {
            PetContext!.PetDocumentEvidence!.AddRange(petDocumentEvidences);
            await PetContext.SaveChangesAsync();
            return true; 
        }
    }
}
