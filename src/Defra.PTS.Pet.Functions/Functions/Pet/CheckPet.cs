using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Defra.PTS.Pet.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Defra.PTS.Pet.Functions.Functions.Pet;

public static class CheckPet
{
    /// <summary>
    /// Check Microchip by Microchipnumber
    /// </summary>
    /// <param name="req"></param>
    /// <param name="result"></param>        
    /// <returns></returns>
    [FunctionName("CheckMicrochip")]
    [OpenApiOperation(operationId: "CheckMicrochip", tags: ["Check"])]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiParameter(name: "microchipnumber", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string), Description = "The OK response")]
    public static IActionResult CheckMicrochip(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "microchip/{microchipnumber}")] HttpRequest req,
        [Sql("SELECT [MicrochipNumber] FROM [dbo].[Pet] where [MicrochipNumber] = @Microchipnumber"
        , "sql_db"
        , System.Data.CommandType.Text
        , parameters: "@Microchipnumber={microchipnumber}")] IEnumerable<PetEntity> result)
    {
        if (result == null)
        {
            var microchipNumber = req.Path.Value.Split("/")[3];
            return new NotFoundObjectResult($"Cannot get pets for Microchip [{microchipNumber}]");
        }            

        var microchipData = (result.FirstOrDefault() != null) ? result.First().MicrochipNumber : "";
        return new OkObjectResult(microchipData);
    }
}