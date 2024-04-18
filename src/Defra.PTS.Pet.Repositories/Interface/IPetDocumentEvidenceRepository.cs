using Defra.PTS.Pet.Domain.Entities;

namespace Defra.PTS.Pet.Repositories.Interface
{
    public interface IPetDocumentEvidenceRepository : IRepository<PetDocumentEvidenceEntity>
    {
        Task<bool> SavePetDocumentEvidence(List<PetDocumentEvidenceEntity> petDocumentEvidences);
    }
}
