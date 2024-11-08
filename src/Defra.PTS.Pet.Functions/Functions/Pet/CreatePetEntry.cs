using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Defra.PTS.Pet.ApiServices.Interface;
using Defra.PTS.Pet.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Defra.PTS.Pet.Functions.Functions.Pet
{
    public class CreatePetEntry(IPetService petService)
    {
        private readonly IPetService _petService = petService;

        /// <summary>
        /// Create Pet By Pet Details
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("CreatePet")]
        [OpenApiOperation(operationId: "CreatePet", tags: ["Create"])]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(PetViewModel), Description = "Create Pet")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> CreatePet(
             [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "createpet")] HttpRequest req,
             ILogger log)
        {
            try
            {
                var input = (req?.Body) ?? throw new InvalidDataException("Invalid pet input, is NUll or Empty");
                string pet = await new StreamReader(input).ReadToEndAsync();

#pragma warning disable CA1869 // Cache and reuse 'JsonSerializerOptions' instances
                var petViewModel = JsonSerializer.Deserialize<PetViewModel>(pet, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
#pragma warning restore CA1869 // Cache and reuse 'JsonSerializerOptions' instances

                if (petViewModel.PetBreed == null)
                {
                    throw new JsonException("Cannot create Pet as Pet Model Cannot be Deserialized from malformed json or null requsest body");
                }

                Guid petId = await _petService.CreatePet(petViewModel);

                return new ObjectResult(petId) { StatusCode = 201, Value = petId};
            }
            catch(InvalidDataException ex)
            {
                log.LogError(ex,"An exception occurred");

                return new BadRequestObjectResult("Invalid pet input, is NUll or Empty");
            }
            catch(JsonException ex)
            {
                log.LogError(ex, "An exception occurred");

                return new BadRequestObjectResult("Cannot create Pet as Pet Model Cannot be Deserialized from malformed json or null requsest body");
            }
            catch (Exception ex)
            {
                log.LogError(ex, "An exception occurred");

                throw;
            }    
        }
    }
}

