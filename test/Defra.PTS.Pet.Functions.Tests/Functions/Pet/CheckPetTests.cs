using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Microsoft.Extensions.Logging;
using Defra.PTS.Pet.Functions.Functions.Pet;
using Microsoft.AspNetCore.Mvc;
using Defra.PTS.Pet.Domain.Entities;
using Defra.PTS.Pet.ApiServices.Interface;
using Microsoft.AspNetCore.Routing;

namespace Defra.PTS.Pet.Functions.Tests.Functions.Pet
{
    [TestFixture]
    public class CheckPetTests
    {
        private readonly Mock<HttpRequest> _requestMoq = new();
        private readonly Mock<IPetService> _mockPetService = new();

        [TearDown]
        public void TearDown()
        {
            _requestMoq.Reset();
            _mockPetService.Reset();
        }

        [TestCase("123456789012345", "123456789012345")]
        public async Task CheckMicrochip_WhenMicroChipExist_Then_ReturnsValidMicroChip(string microChipNumber, string expectedResult)
        {
            var routeDict = new RouteValueDictionary { ["microchipnumber"] = microChipNumber };
            _requestMoq.Setup(a => a.RouteValues).Returns(routeDict);
            _mockPetService.Setup(a => a.CheckMicrochipAsync(microChipNumber)).ReturnsAsync(microChipNumber);

            var checkPet = new CheckPet(_mockPetService.Object);
            var result = await checkPet.CheckMicrochip(_requestMoq.Object);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult?.StatusCode);
            Assert.AreEqual(expectedResult, okResult?.Value);
        }

        [TestCase("123456789012345", "")]
        public async Task CheckMicrochip_WhenMicroChipDoesntExist_Then_ReturnsEmptyMicroChip(string microChipNumber, string expectedResult)
        {
            var routeDict = new RouteValueDictionary { ["microchipnumber"] = microChipNumber };
            _requestMoq.Setup(a => a.RouteValues).Returns(routeDict);
            _mockPetService.Setup(a => a.CheckMicrochipAsync(microChipNumber)).ReturnsAsync((string?)null);

            var checkPet = new CheckPet(_mockPetService.Object);
            var result = await checkPet.CheckMicrochip(_requestMoq.Object);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult?.StatusCode);
            Assert.AreEqual(expectedResult, okResult?.Value);
        }

        [Test]
        public async Task CheckMicrochip_WhenMicrochipNumberIsNull_Then_ReturnsBadRequest()
        {
            var routeDict = new RouteValueDictionary { ["microchipnumber"] = null };
            _requestMoq.Setup(a => a.RouteValues).Returns(routeDict);

            var checkPet = new CheckPet(_mockPetService.Object);
            var result = await checkPet.CheckMicrochip(_requestMoq.Object);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult?.StatusCode);
            Assert.AreEqual("Microchip number is required", badRequestResult?.Value);
        }

        [TestCase("123456789012345")]
        public async Task CheckMicrochip_WhenMicrochipNumberIsEmpty_Then_ReturnsBadRequest(string microChipNumber)
        {
            var routeDict = new RouteValueDictionary { ["microchipnumber"] = "" };
            _requestMoq.Setup(a => a.RouteValues).Returns(routeDict);

            var checkPet = new CheckPet(_mockPetService.Object);
            var result = await checkPet.CheckMicrochip(_requestMoq.Object);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult?.StatusCode);
        }
    }
}
