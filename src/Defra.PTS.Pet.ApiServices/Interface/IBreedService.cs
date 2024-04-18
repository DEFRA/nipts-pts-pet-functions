using Defra.PTS.Pet.Domain.Entities;
using Defra.PTS.Pet.Domain.Models;

namespace Defra.PTS.Pet.ApiServices.Interface
{
    public interface IBreedService
    {
        IEnumerable<PetBreedViewModel> GetBreeds(IEnumerable<BreedEntity> breeds);
    }
}
