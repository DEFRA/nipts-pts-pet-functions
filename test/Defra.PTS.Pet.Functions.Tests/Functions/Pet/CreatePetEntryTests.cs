using Defra.PTS.Pet.ApiServices.Interface;
using Defra.PTS.Pet.Domain.Models;
using Defra.PTS.Pet.Functions.Functions.Breed;
using Defra.PTS.Pet.Functions.Functions.Pet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Management.ContainerService.Fluent.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defra.PTS.Pet.Functions.Tests.Functions.Pet
{
    [TestFixture]
    public class CreatePetEntryTests
    {
        private Mock<HttpRequest>? _mockRequest;
        private Mock<ILogger>? _mockLogger;
        private Mock<IPetService>? _mockPetService;
        private CreatePetEntry? _sut;

        [SetUp]
        public void SetUp()
        {
            _mockRequest = new Mock<HttpRequest>();
            _mockLogger = new Mock<ILogger>();
            _mockPetService = new Mock<IPetService>();

            _sut = new CreatePetEntry(_mockPetService.Object);
        }

        [Test]
        public async Task CreatePet_201_Response()
        {
            var json = JsonConvert.SerializeObject(new { PetBreed = new { BreedId = 1, BreedName = "Pug" } });
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var createdPetId = Guid.NewGuid();

            _mockRequest!.Setup(x => x.Body).Returns(stream);
            _mockPetService!.Setup(x => x.CreatePet(It.IsAny<PetViewModel>()))
                .ReturnsAsync(createdPetId);

            var result = await _sut!.CreatePet(_mockRequest.Object, _mockLogger!.Object);
            var objectResult = result as ObjectResult;

            Assert.IsNotNull(objectResult);
            Assert.AreEqual(201, objectResult!.StatusCode);
            Assert.AreEqual(createdPetId, objectResult.Value);            
        }

        [Test]
        public async Task CreatePet_400_Response_EmptyRequest()
        {                        
            var result = await _sut!.CreatePet(_mockRequest!.Object, _mockLogger!.Object);
            var objectResult = result as ObjectResult;

            Assert.IsNotNull(objectResult);
            Assert.AreEqual(400, objectResult!.StatusCode);
            Assert.AreEqual("Invalid pet input, is NUll or Empty", objectResult.Value);
        }

        [Test]
        public async Task CreatePet_400_Response_InvalidJson()
        {
            var json = JsonConvert.SerializeObject(new { Name = "Bob" });
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

            _mockRequest!.Setup(x => x.Body).Returns(stream);

            var result = await _sut!.CreatePet(_mockRequest.Object, _mockLogger!.Object);
            var objectResult = result as ObjectResult;

            Assert.IsNotNull(objectResult);
            Assert.AreEqual(400, objectResult!.StatusCode);
            Assert.AreEqual("Cannot create Pet as Pet Model Cannot be Deserialized from malformed json or null requsest body", objectResult.Value);
        }

        [Test]
        public void CreatePet_ThrowException()
        {
            var json = JsonConvert.SerializeObject(new { PetBreed = new { BreedId = 1, BreedName = "Pug" } });
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));            

            _mockRequest!.Setup(x => x.Body).Returns(stream);           
            _mockPetService!.Setup(x => x.CreatePet(It.IsAny<PetViewModel>()))
                .ThrowsAsync(new Exception("Unable to create Pet"));
            
            Assert.ThrowsAsync<Exception>(async () => await _sut!.CreatePet(_mockRequest.Object, _mockLogger!.Object));
        }
    }
}
