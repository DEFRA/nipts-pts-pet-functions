using Defra.PTS.Pet.Domain.Entities;
using Defra.PTS.Pet.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Defra.PTS.Pet.Repositories.Implementation
{
    public class BreedRepository(PetDbContext context) : IBreedRepository
    {
        private readonly PetDbContext _context = context;

        public async Task<IEnumerable<BreedEntity>> GetBreedsBySpeciesIdAsync(int speciesId)
        {
            return await _context.Breed!
                .Where(b => b.SpeciesId == speciesId)
                .OrderBy(b => b.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<ColourEntity>> GetColoursBySpeciesIdAsync(int speciesId)
        {
            return await _context.Colour!
                .Where(c => c.SpeciesId == speciesId)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<string?> GetMicrochipNumberAsync(string microchipNumber)
        {
            var pet = await _context.Pet!
                .Where(p => p.MicrochipNumber == microchipNumber)
                .Select(p => p.MicrochipNumber)
                .FirstOrDefaultAsync();
            return pet;
        }
    }
}
