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
        public async Task GetBreed_WhenBreedExist_Then_ReturnsValidBreed(int speciesId)
        {
            List<BreedEntity> entityPets = new List<BreedEntity>() { new BreedEntity() { Id = 1, Name = "Test", SpeciesId = 1 } };
            IEnumerable<PetBreedViewModel> modelPets = new List<PetBreedViewModel>() { new PetBreedViewModel() { BreedId = 1, BreedName ="Test" } };
            IEnumerable<PetBreedViewModel> taskPets = modelPets;

            var routeDict = new RouteValueDictionary { ["speciesId"] = speciesId.ToString() };
            _mockRequest.Setup(a => a.RouteValues).Returns(routeDict);

            _mockBreedService.Setup(a => a.GetBreedsBySpeciesIdAsync(speciesId)).ReturnsAsync(entityPets);
            _mockBreedService.Setup(a => a.GetBreeds(It.IsAny<IEnumerable<BreedEntity>>())).Returns(taskPets);            

            var result = await _sut!.GetBreed(_mockRequest.Object);
            var okResult = result as OkObjectResult; 

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult?.StatusCode);
            Assert.AreEqual(modelPets, okResult?.Value);

            _mockBreedService.Verify(a => a.GetBreeds(It.IsAny<IEnumerable<BreedEntity>>()), Times.Once);
        }

        [Test]
        public async Task GetBreed_WhenSpeciesIdIsInvalid_Then_ReturnsBadRequest()
        {
            var routeDict = new RouteValueDictionary { ["speciesId"] = "abc" };
            _mockRequest.Setup(a => a.RouteValues).Returns(routeDict);

            var result = await _sut!.GetBreed(_mockRequest.Object);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult?.StatusCode);
            Assert.AreEqual("Invalid speciesId", badRequestResult?.Value);
        }

        [Test]
        public async Task GetBreed_WhenSpeciesIdIsNull_Then_ReturnsBadRequest()
        {
            var routeDict = new RouteValueDictionary { ["speciesId"] = null };
            _mockRequest.Setup(a => a.RouteValues).Returns(routeDict);

            var result = await _sut!.GetBreed(_mockRequest.Object);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult?.StatusCode);
        }

        [TestCase(1)]
        public async Task GetBreed_WhenResultDoesntExist_Then_ReturnsNotFoundObjectResult(int speciesId)
        {
            var expectedResult = $"Cannot get breed for species Id [{speciesId}]";

            List<BreedEntity> entityPets = new();

            var routeDict = new RouteValueDictionary { ["speciesId"] = speciesId.ToString() };
            _mockRequest!.Setup(a => a.RouteValues).Returns(routeDict);

            _mockBreedService.Setup(a => a.GetBreedsBySpeciesIdAsync(speciesId)).ReturnsAsync(entityPets);

            var result = await _sut!.GetBreed(_mockRequest.Object);
            var notFoundResult = result as NotFoundObjectResult;

            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult?.StatusCode);
            Assert.AreEqual(expectedResult, notFoundResult?.Value);

            _mockBreedService.Verify(a => a.GetBreeds(It.IsAny<IEnumerable<BreedEntity>>()), Times.Never);
        }

        [TestCase(1)]
        public async Task GetPetColour_WhenBreedExist_Then_ReturnsColours(int speciesId)
        {
            List<ColourEntity> entityPetColours = new() { new ColourEntity() { Id = 1, Name = "Brown", SpeciesId = 1 } };

            var routeDict = new RouteValueDictionary { ["speciesId"] = speciesId.ToString() };
            _mockRequest!.Setup(a => a.RouteValues).Returns(routeDict);

            _mockBreedService.Setup(a => a.GetColoursBySpeciesIdAsync(speciesId)).ReturnsAsync(entityPetColours);

            var result = await _sut!.GetColours(_mockRequest!.Object);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult?.StatusCode);
            Assert.AreEqual(entityPetColours, okResult?.Value);            
        }

        [Test]
        public async Task GetColours_WhenSpeciesIdIsInvalid_Then_ReturnsBadRequest()
        {
            var routeDict = new RouteValueDictionary { ["speciesId"] = "abc" };
            _mockRequest.Setup(a => a.RouteValues).Returns(routeDict);

            var result = await _sut!.GetColours(_mockRequest.Object);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult?.StatusCode);
            Assert.AreEqual("Invalid speciesId", badRequestResult?.Value);
        }

        [Test]
        public async Task GetColours_WhenSpeciesIdIsNull_Then_ReturnsBadRequest()
        {
            var routeDict = new RouteValueDictionary { ["speciesId"] = null };
            _mockRequest.Setup(a => a.RouteValues).Returns(routeDict);

            var result = await _sut!.GetColours(_mockRequest.Object);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult?.StatusCode);
        }

        [TestCase(1)]
        public async Task GetColours_WhenResultDoesntExist_Then_ReturnsNotFoundObjectResult(int speciesId)
        {
            var expectedResult = $"Cannot get pet colours for species Id [{speciesId}]";

            List<ColourEntity> entityPetColours = new List<ColourEntity>();

            var routeDict = new RouteValueDictionary { ["speciesId"] = speciesId.ToString() };
            _mockRequest!.Setup(a => a.RouteValues).Returns(routeDict);

            _mockBreedService.Setup(a => a.GetColoursBySpeciesIdAsync(speciesId)).ReturnsAsync(entityPetColours);

            var result = await _sut!.GetColours(_mockRequest.Object);
            var notFoundResult = result as NotFoundObjectResult;

            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult?.StatusCode);
            Assert.AreEqual(expectedResult, notFoundResult?.Value);            
        }
    }
}
