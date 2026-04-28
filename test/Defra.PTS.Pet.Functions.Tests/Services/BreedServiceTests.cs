using Defra.PTS.Pet.ApiServices.Implementation;
using Defra.PTS.Pet.Repositories.Interface;
using Moq;
using Defra.PTS.Pet.Domain.Entities;
using Defra.PTS.Pet.Domain.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defra.PTS.Pet.Functions.Tests.Services
{
    [TestFixture]
    public class BreedServiceTests
    {
        private BreedService? _sut;
        private Mock<IBreedRepository>? _mockRepo;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IBreedRepository>();
            _sut = new BreedService(_mockRepo.Object);
        }

        [Test]
        public void GetBreeds_ReturnBreedsList()
        {
            var breedEntities = new List<BreedEntity>()
            {
                new BreedEntity() { Id = 1, Name = "Pug" },
                new BreedEntity() { Id = 2, Name = "Labrador" }
            };

            var expectedResult = new List<PetBreedViewModel>()
            {
                new PetBreedViewModel { BreedId = 1, BreedName = "Pug" },
                new PetBreedViewModel { BreedId = 2, BreedName = "Labrador" }
            };

            var actualResult = _sut!.GetBreeds(breedEntities);
            var orderedActual = actualResult.OrderBy(x => x.BreedId).ToList();
            Assert.AreEqual(expectedResult[0].BreedId, orderedActual[0].BreedId);
            Assert.AreEqual(expectedResult[0].BreedName, orderedActual[0].BreedName);
            Assert.AreEqual(expectedResult[1].BreedId, orderedActual[1].BreedId);
            Assert.AreEqual(expectedResult[1].BreedName, orderedActual[1].BreedName);
        }

        [Test]
        public void GetBreeds_WithMixedBreedOrUnknown_MovesItToFirst()
        {
            var breedEntities = new List<BreedEntity>()
            {
                new BreedEntity() { Id = 1, Name = "Pug" },
                new BreedEntity() { Id = 2, Name = "Mixed breed or unknown" },
                new BreedEntity() { Id = 3, Name = "Labrador" }
            };

            var result = _sut!.GetBreeds(breedEntities).ToList();

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("Mixed breed or unknown", result[0].BreedName);
            Assert.AreEqual(2, result[0].BreedId);
        }

        [Test]
        public void GetBreeds_WithEmptyList_ReturnsEmptyList()
        {
            var breedEntities = new List<BreedEntity>();

            var result = _sut!.GetBreeds(breedEntities).ToList();

            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetBreedsBySpeciesIdAsync_ReturnsBreeds()
        {
            var expectedBreeds = new List<BreedEntity>
            {
                new BreedEntity() { Id = 1, Name = "Pug", SpeciesId = 1 }
            };

            _mockRepo!.Setup(x => x.GetBreedsBySpeciesIdAsync(1)).ReturnsAsync(expectedBreeds);

            var result = await _sut!.GetBreedsBySpeciesIdAsync(1);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Pug", result.First().Name);
            _mockRepo.Verify(x => x.GetBreedsBySpeciesIdAsync(1), Times.Once);
        }

        [Test]
        public async Task GetColoursBySpeciesIdAsync_ReturnsColours()
        {
            var expectedColours = new List<ColourEntity>
            {
                new ColourEntity() { Id = 1, Name = "Brown", SpeciesId = 1 }
            };

            _mockRepo!.Setup(x => x.GetColoursBySpeciesIdAsync(1)).ReturnsAsync(expectedColours);

            var result = await _sut!.GetColoursBySpeciesIdAsync(1);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Brown", result.First().Name);
            _mockRepo.Verify(x => x.GetColoursBySpeciesIdAsync(1), Times.Once);
        }
    }
}
