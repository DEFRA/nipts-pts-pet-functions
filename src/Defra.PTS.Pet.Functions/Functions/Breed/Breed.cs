using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Defra.PTS.Pet.ApiServices.Interface;
using Defra.PTS.Pet.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Defra.PTS.Pet.Functions.Functions.Breed;

public class Breed(IBreedService breedService)
{
    private readonly IBreedService _breedService = breedService;

    /// <summary>
    /// Get Breed By SpeciesId
    /// </summary>
    [Function("GetBreed")]
    [OpenApiOperation(operationId: "GetBreed", tags: new[] { "Breed" }, Summary = "Get breeds by species", Description = "Get all breeds for a given species ID")]
    [OpenApiParameter(name: "speciesId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The species ID")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<BreedEntity>), Description = "List of breeds")]
    public async Task<IActionResult> GetBreed(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "breed/{speciesId}")] HttpRequest req)
    {
        var speciesIdStr = req.RouteValues["speciesId"]?.ToString();
        if (!int.TryParse(speciesIdStr, out var speciesId))
        {
            return new BadRequestObjectResult("Invalid speciesId");
        }

        var result = await _breedService.GetBreedsBySpeciesIdAsync(speciesId);
        if (!result.Any())
        {
            return new NotFoundObjectResult($"Cannot get breed for species Id [{speciesId}]");
        }

        var breeds = _breedService.GetBreeds(result);
        return new OkObjectResult(breeds);
    }

    /// <summary>
    /// Get Colours By SpeciesId
    /// </summary>
    [Function("GetColours")]
    [OpenApiOperation(operationId: "GetColours", tags: new[] { "Breed" }, Summary = "Get colours by species", Description = "Get all colours for a given species ID")]
    [OpenApiParameter(name: "speciesId", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The species ID")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<ColourEntity>), Description = "List of colours")]
    public async Task<IActionResult> GetColours(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "colour/{speciesId}")] HttpRequest req)
    {
        var speciesIdStr = req.RouteValues["speciesId"]?.ToString();
        if (!int.TryParse(speciesIdStr, out var speciesId))
        {
            return new BadRequestObjectResult("Invalid speciesId");
        }

        var petColours = await _breedService.GetColoursBySpeciesIdAsync(speciesId);
        if (!petColours.Any())
        {
            return new NotFoundObjectResult($"Cannot get pet colours for species Id [{speciesId}]");
        }

        return new OkObjectResult(petColours);
    }
}

