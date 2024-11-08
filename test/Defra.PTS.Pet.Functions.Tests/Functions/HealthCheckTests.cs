using Defra.PTS.Pet.ApiServices.Interface;
using Defra.PTS.Pet.Functions.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Defra.PTS.Pet.Functions.Tests.Functions
{
    [TestFixture]
    public class HealthCheckTests
    {
        private Mock<IPetService>? _mockPetService;        
        private Mock<ILogger>? _mockLogger;
        private HealthCheck? _sut;
        private Mock<HttpRequest>? _httpRequestMock;

        [SetUp]
        public void SetUp()
        {
            _mockPetService = new Mock<IPetService>();            
            _mockLogger = new Mock<ILogger>();
            _httpRequestMock = new Mock<HttpRequest>();

            _sut = new HealthCheck(_mockPetService.Object);
        }

        [Test]
        public async Task HealthCheck_Endpoint_Is_Healthy()
        {
            _mockPetService!.Setup(x => x.PerformHealthCheckLogic()).ReturnsAsync(true);

            var result = await _sut!.Run(_httpRequestMock!.Object, _mockLogger!.Object);
            var okResult = result as OkResult;


            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult!.StatusCode);
        }

        [Test]
        public async Task HealthCheck_Endpoint_Is_Not_Healthy()
        {
            _mockPetService!.Setup(x => x.PerformHealthCheckLogic()).ReturnsAsync(false);

            var result = await _sut!.Run(_httpRequestMock!.Object, _mockLogger!.Object);
            var statusCodeResult = result as StatusCodeResult;


            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(503, statusCodeResult!.StatusCode);
        }
    }
}
