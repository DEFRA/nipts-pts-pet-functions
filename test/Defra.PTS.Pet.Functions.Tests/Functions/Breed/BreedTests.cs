using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Microsoft.Extensions.Logging;
using Defra.PTS.Pet.ApiServices.Interface;
using PetBreed = Defra.PTS.Pet.Functions.Functions.Breed;
using Defra.PTS.Pet.Domain.Entities;
using Defra.PTS.Pet.Domain.Models;
using Microsoft.AspNetCore.Routing;

namespace Defra.PTS.Pet.Functions.Tests.Functions.Breed
{
    [TestFixture]
    public class BreedTests
    {
        private readonly Mock<HttpRequest> _mockRequest = new();        
        private readonly Mock<IBreedService> _mockBreedService = new();
        PetBreed.Breed? _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new PetBreed.Breed(_mockBreedService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _mockRequest.Reset();            
            _mockBreedService.Reset();
        }

        [TestCase(1)]
        public void GetBreed_WhenBreedExist_Then_ReturnsValidBreed(int speciesId)
        {
            List<BreedEntity> entityPets = new List<BreedEntity>() { new BreedEntity() { Id = 1, Name = "Test", SpeciesId = 1 } };
            IEnumerable<PetBreedViewModel> modelPets = new List<PetBreedViewModel>() { new PetBreedViewModel() { BreedId = 1, BreedName ="Test" } };
            IEnumerable<PetBreedViewModel> taskPets = modelPets;        

            _mockBreedService.Setup(a => a.GetBreeds(It.IsAny<IEnumerable<BreedEntity>>())).Returns(taskPets);            

            var result = _sut!.GetBreed(_mockRequest.Object, entityPets);
            var okResult = result as OkObjectResult; 

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult?.StatusCode);
            Assert.AreEqual(modelPets, okResult?.Value);

            _mockBreedService.Verify(a => a.GetBreeds(It.IsAny<IEnumerable<BreedEntity>>()), Times.Once);
        }

        [TestCase(1)]
        public void GetBreed_WhenResultDoesntExist_Then_ReturnsNotFoundObjectResult(int speciesId)
        {
            var expectedResult = $"Cannot get breed for species Id [{speciesId}]";

            List<BreedEntity> entityPets = new();
            IEnumerable<PetBreedViewModel> modelPets = new List<PetBreedViewModel>() { new PetBreedViewModel() { BreedId = 1, BreedName = "Test" } };
            IEnumerable<PetBreedViewModel> taskPets = modelPets;

            _mockBreedService!.Setup(a => a.GetBreeds(It.IsAny<IEnumerable<BreedEntity>>())).Returns(taskPets);

            var routeDict = new RouteValueDictionary
            {
                ["speciesId"] = 1
            };
            _mockRequest!.Setup(a => a.RouteValues).Returns(routeDict);

            var result = _sut!.GetBreed(_mockRequest.Object, entityPets);
            var notFoundResult = result as NotFoundObjectResult;

            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult?.StatusCode);
            Assert.AreEqual(expectedResult, notFoundResult?.Value);

            _mockBreedService.Verify(a => a.GetBreeds(It.IsAny<IEnumerable<BreedEntity>>()), Times.Never);
        }

        [TestCase(1)]
        public void GetPetColour_WhenBreedExist_Then_ReturnsColours(int speciesId)
        {
            List<ColourEntity> entityPetColours = new() { new ColourEntity() { Id = 1, Name = "Brown", SpeciesId = 1 } };                   

            var result = _sut!.GetColours(_mockRequest!.Object, entityPetColours);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult?.StatusCode);
            Assert.AreEqual(entityPetColours, okResult?.Value);            
        }

        [TestCase(1)]
        public void GetColours_WhenResultDoesntExist_Then_ReturnsNotFoundObjectResult(int speciesId)
        {
            var expectedResult = $"Cannot get pet colours for species Id [{speciesId}]";

            List<ColourEntity> entityPetColours = new List<ColourEntity>();

            var routeDict = new RouteValueDictionary
            {
                ["speciesId"] = 1
            };
            _mockRequest!.Setup(a => a.RouteValues).Returns(routeDict);            

            var result = _sut!.GetColours(_mockRequest.Object, entityPetColours);
            var notFoundResult = result as NotFoundObjectResult;

            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult?.StatusCode);
            Assert.AreEqual(expectedResult, notFoundResult?.Value);            
        }
    }
}
