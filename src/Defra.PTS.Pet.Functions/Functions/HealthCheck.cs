using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Defra.PTS.Pet.ApiServices.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Defra.PTS.Pet.Functions.Functions;

public class HealthCheck(IPetService petService)
{
    private readonly IPetService _petService = petService;

    [FunctionName("HealthCheck")]
    [OpenApiOperation(operationId: "Run", tags: ["name"])]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
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

