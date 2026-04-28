using Defra.PTS.Pet.ApiServices.Interface;
using Defra.PTS.Pet.Domain.Entities;
using Defra.PTS.Pet.Domain.Models;
using Defra.PTS.Pet.Repositories.Interface;
using System.Collections.Generic;

namespace Defra.PTS.Pet.ApiServices.Implementation
{
    public class BreedService(IBreedRepository breedRepository) : IBreedService
    {
        private readonly IBreedRepository _breedRepository = breedRepository;

        public IEnumerable<PetBreedViewModel> GetBreeds(IEnumerable<BreedEntity> breeds)
        {
            var dbBreeds = breeds.Select(GetBreed()).ToList();
            var breed = dbBreeds.Find(r => r.BreedName == "Mixed breed or unknown");
            if(breed != null)
            {
                _ = dbBreeds.Remove(breed);
            }
            
            if (breed != null)
            {
                dbBreeds.Insert(0, breed);
            }
                
            return dbBreeds;
        }

        public async Task<IEnumerable<BreedEntity>> GetBreedsBySpeciesIdAsync(int speciesId)
        {
            return await _breedRepository.GetBreedsBySpeciesIdAsync(speciesId);
        }

        public async Task<IEnumerable<ColourEntity>> GetColoursBySpeciesIdAsync(int speciesId)
        {
            return await _breedRepository.GetColoursBySpeciesIdAsync(speciesId);
        }
       
        private static Func<BreedEntity, PetBreedViewModel> GetBreed()
        {
            return a => new PetBreedViewModel()
            {
                BreedId = a.Id,
                BreedName = a.Name,
            };
        }
    }
}
