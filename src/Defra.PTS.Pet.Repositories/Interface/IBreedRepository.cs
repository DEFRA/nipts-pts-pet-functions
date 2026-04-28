using Defra.PTS.Pet.Domain.Entities;

namespace Defra.PTS.Pet.Repositories.Interface
{
    public interface IBreedRepository
    {
        Task<IEnumerable<BreedEntity>> GetBreedsBySpeciesIdAsync(int speciesId);
        Task<IEnumerable<ColourEntity>> GetColoursBySpeciesIdAsync(int speciesId);
        Task<string?> GetMicrochipNumberAsync(string microchipNumber);
    }
}
