using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Defra.PTS.Pet.ApiServices.Interface;
using Defra.PTS.Pet.Domain.Entities;
using Defra.PTS.Pet.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Defra.PTS.Pet.Functions.Functions.Breed
{
    public class Breed
    {
        private readonly IBreedService _breedService;
        public Breed(IBreedService breedService)
        {
            _breedService = breedService;
        }

        /// <summary>
        /// Get Breed By SpeciesId
        /// </summary>
        /// <param name="req"></param>
        /// <param name="result"></param>        
        /// <returns></returns>
        [FunctionName("GetBreed")]
        [OpenApiOperation(operationId: "GetBreed", tags: new[] { "Breeds" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "speciesId", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **SpeciesId** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public IActionResult GetBreed(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "breed/{speciesId}")] HttpRequest req
            , [Sql("SELECT [Id], [Name] FROM [dbo].[Breed] Where [SpeciesId] = @SpeciesId ORDER BY [Name] ASC"
            , "sql_db"
            , System.Data.CommandType.Text
            , parameters: "@SpeciesId={speciesId}")] IEnumerable<BreedEntity> result)
        {
            if (result.ToList().Count < 1)
            {
                var speciesId = req.RouteValues["speciesId"];
                return new NotFoundObjectResult($"Cannot get breed for species Id [{speciesId}]");
            }

            var breeds = _breedService.GetBreeds(result);
            return new OkObjectResult(breeds);
        }

        /// <summary>
        /// Get Colours By SpeciesId
        /// </summary>
        /// <param name="req"></param>
        /// <param name="petColours"></param>        
        /// <returns></returns>
        [FunctionName("GetColours")]
        [OpenApiOperation(operationId: "GetColours", tags: new[] { "Colours" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "speciesId", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **SpeciesId** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public IActionResult GetColours(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "colour/{speciesId}")] HttpRequest req
            , [Sql(" SELECT [Id], [Name], [SpeciesId] FROM [dbo].[Colour] WHERE [SpeciesId] = @SpeciesId ORDER BY [Name] ASC"
            , "sql_db"
            , System.Data.CommandType.Text
            , parameters: "@SpeciesId={speciesId}")] IEnumerable<ColourEntity> petColours)
        {
            if (petColours.ToList().Count < 1)
            {
                var speciesId = req.RouteValues["speciesId"];                
                return new NotFoundObjectResult($"Cannot get pet colours for species Id [{speciesId}]");
            }
            
            return new OkObjectResult(petColours);
        }
    }
}

