using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Defra.PTS.Pet.ApiServices.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Defra.PTS.Pet.Functions.Functions.Pet;

public class CheckPet(IPetService petService)
{
    private readonly IPetService _petService = petService;

    /// <summary>
    /// Check Microchip by Microchipnumber
    /// </summary>
    [Function("CheckMicrochip")]
    [OpenApiOperation(operationId: "CheckMicrochip", tags: new[] { "Pet" }, Summary = "Check microchip", Description = "Check if a microchip number exists")]
    [OpenApiParameter(name: "microchipnumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The microchip number to check")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "Microchip check result")]
    public async Task<IActionResult> CheckMicrochip(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "microchip/{microchipnumber}")] HttpRequest req)
    {
        var microchipNumber = req.RouteValues["microchipnumber"]?.ToString();
        if (string.IsNullOrEmpty(microchipNumber))
        {
            return new BadRequestObjectResult("Microchip number is required");
        }

        var result = await _petService.CheckMicrochipAsync(microchipNumber);
        return new OkObjectResult(result ?? "");
    }
}