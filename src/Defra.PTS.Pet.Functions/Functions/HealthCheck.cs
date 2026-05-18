using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Defra.PTS.Pet.ApiServices.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace Defra.PTS.Pet.Functions.Functions;

public class HealthCheck(IPetService petService)
{
    private readonly IPetService _petService = petService;

    [Function("HealthCheck")]
    [OpenApiOperation(operationId: "HealthCheck", tags: new[] { "Health" }, Summary = "Health check", Description = "Check the health of the service")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "Service is healthy")]
    public async Task<IActionResult> Run(
#pragma warning disable IDE0060 // Remove unused parameter
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")] HttpRequest req, ILogger log)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        // Perform health check logic here
        bool isHealthy = await _petService.PerformHealthCheckLogic();

        if (isHealthy)
        {
            return new OkResult();
        }
        else
        {
            return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
        }
    }
}

