using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Microsoft.Extensions.Logging;
using Defra.PTS.Pet.Functions.Functions.Pet;
using Microsoft.AspNetCore.Mvc;
using Defra.PTS.Pet.Domain.Entities;

namespace Defra.PTS.Pet.Functions.Tests.Functions.Pet
{
    [TestFixture]
    public class CheckPetTests
    {
        private readonly Mock<HttpRequest> _requestMoq = new();

        [TearDown]
        public void TearDown()
        {
            _requestMoq.Reset();
        }


        [TestCase("123456789012345", "123456789012345")]
        public void CheckMicrochip_WhenMicroChipExist_Then_ReturnsValidMicroChip(string microChipNumber, string expectedResult)
        {
            var pets = new List<PetEntity>() { new PetEntity() { MicrochipNumber = microChipNumber } };

            var result = CheckPet.CheckMicrochip(_requestMoq.Object, pets);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult?.StatusCode);
            Assert.AreEqual(expectedResult, okResult?.Value);
        }

        [TestCase("123456789012345", "")]
        public void CheckMicrochip_WhenMicroChipDoesntExist_Then_ReturnsEmptyMicroChip(string microChipNumber, string expectedResult)
        {
            var pets = new List<PetEntity>();            

            var result = CheckPet.CheckMicrochip(_requestMoq.Object, pets);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult?.StatusCode);
            Assert.AreEqual(expectedResult, okResult?.Value);
        }


        [TestCase("123456789012345")]
        public void CheckMicrochip_WhenResultDoesntExist_Then_ReturnsNotFoundObjectResult(string microChipNumber)
        {
            var expectedResult = $"Cannot get pets for Microchip [{microChipNumber}]";
            List<PetEntity>? pets = null;
            _requestMoq!.Setup(a => a.Path).Returns($"/api/microchip/{microChipNumber}");

            var result = CheckPet.CheckMicrochip(_requestMoq.Object, pets);
            var notFoundResult = result as NotFoundObjectResult;

            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult?.StatusCode);
            Assert.AreEqual(expectedResult, notFoundResult?.Value);
        }   
    }
}
