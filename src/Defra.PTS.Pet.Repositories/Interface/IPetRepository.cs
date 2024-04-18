using Defra.PTS.Pet.Domain.Entities;

namespace Defra.PTS.Pet.Repositories.Interface
{
    public interface IPetRepository : IRepository<PetEntity>
    {
        Task<bool> PerformHealthCheckLogic();
        Task CreatePet(PetEntity pet);
    }
}
