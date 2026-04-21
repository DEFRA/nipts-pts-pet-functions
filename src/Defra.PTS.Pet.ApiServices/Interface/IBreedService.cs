using Defra.PTS.Pet.Domain.Entities;
using Defra.PTS.Pet.Domain.Models;

namespace Defra.PTS.Pet.ApiServices.Interface
{
    public interface IBreedService
    {
        IEnumerable<PetBreedViewModel> GetBreeds(IEnumerable<BreedEntity> breeds);
        Task<IEnumerable<BreedEntity>> GetBreedsBySpeciesIdAsync(int speciesId);
        Task<IEnumerable<ColourEntity>> GetColoursBySpeciesIdAsync(int speciesId);
    }
}
