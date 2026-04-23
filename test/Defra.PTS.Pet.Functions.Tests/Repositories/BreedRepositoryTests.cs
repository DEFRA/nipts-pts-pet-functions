using Defra.PTS.Pet.Domain.Entities;
using Defra.PTS.Pet.Repositories;
using Defra.PTS.Pet.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Defra.PTS.Pet.Functions.Tests.Repositories
{
    [TestFixture]
    public class BreedRepositoryTests
    {
        private PetDbContext _context = null!;
        private BreedRepository _sut = null!;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<PetDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new PetDbContext(options);
            _sut = new BreedRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task GetBreedsBySpeciesIdAsync_ReturnsMatchingBreeds()
        {
            _context.Breed!.AddRange(
                new BreedEntity { Id = 1, Name = "Pug", SpeciesId = 1 },
                new BreedEntity { Id = 2, Name = "Labrador", SpeciesId = 1 },
                new BreedEntity { Id = 3, Name = "Siamese", SpeciesId = 2 });
            await _context.SaveChangesAsync();

            var result = (await _sut.GetBreedsBySpeciesIdAsync(1)).ToList();

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(b => b.SpeciesId == 1));
        }

        [Test]
        public async Task GetBreedsBySpeciesIdAsync_ReturnsOrderedByName()
        {
            _context.Breed!.AddRange(
                new BreedEntity { Id = 1, Name = "Pug", SpeciesId = 1 },
                new BreedEntity { Id = 2, Name = "Bulldog", SpeciesId = 1 },
                new BreedEntity { Id = 3, Name = "Labrador", SpeciesId = 1 });
            await _context.SaveChangesAsync();

            var result = (await _sut.GetBreedsBySpeciesIdAsync(1)).ToList();

            Assert.AreEqual("Bulldog", result[0].Name);
            Assert.AreEqual("Labrador", result[1].Name);
            Assert.AreEqual("Pug", result[2].Name);
        }

        [Test]
        public async Task GetBreedsBySpeciesIdAsync_WhenNoMatch_ReturnsEmpty()
        {
            _context.Breed!.Add(new BreedEntity { Id = 1, Name = "Pug", SpeciesId = 1 });
            await _context.SaveChangesAsync();

            var result = await _sut.GetBreedsBySpeciesIdAsync(99);

            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetColoursBySpeciesIdAsync_ReturnsMatchingColours()
        {
            _context.Colour!.AddRange(
                new ColourEntity { Id = 1, Name = "Brown", SpeciesId = 1 },
                new ColourEntity { Id = 2, Name = "Black", SpeciesId = 1 },
                new ColourEntity { Id = 3, Name = "White", SpeciesId = 2 });
            await _context.SaveChangesAsync();

            var result = (await _sut.GetColoursBySpeciesIdAsync(1)).ToList();

            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(c => c.SpeciesId == 1));
        }

        [Test]
        public async Task GetColoursBySpeciesIdAsync_ReturnsOrderedByName()
        {
            _context.Colour!.AddRange(
                new ColourEntity { Id = 1, Name = "White", SpeciesId = 1 },
                new ColourEntity { Id = 2, Name = "Black", SpeciesId = 1 });
            await _context.SaveChangesAsync();

            var result = (await _sut.GetColoursBySpeciesIdAsync(1)).ToList();

            Assert.AreEqual("Black", result[0].Name);
            Assert.AreEqual("White", result[1].Name);
        }

        [Test]
        public async Task GetColoursBySpeciesIdAsync_WhenNoMatch_ReturnsEmpty()
        {
            _context.Colour!.Add(new ColourEntity { Id = 1, Name = "Brown", SpeciesId = 1 });
            await _context.SaveChangesAsync();

            var result = await _sut.GetColoursBySpeciesIdAsync(99);

            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetMicrochipNumberAsync_WhenPetExists_ReturnsMicrochipNumber()
        {
            _context.Pet!.Add(new PetEntity { Id = Guid.NewGuid(), MicrochipNumber = "123456789012345", Name = "Fido" });
            await _context.SaveChangesAsync();

            var result = await _sut.GetMicrochipNumberAsync("123456789012345");

            Assert.AreEqual("123456789012345", result);
        }

        [Test]
        public async Task GetMicrochipNumberAsync_WhenPetDoesNotExist_ReturnsNull()
        {
            var result = await _sut.GetMicrochipNumberAsync("999999999999999");

            Assert.IsNull(result);
        }
    }
}
