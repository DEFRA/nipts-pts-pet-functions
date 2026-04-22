using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace Defra.PTS.Pet.Functions.Functions;

#if DEBUG
[ExcludeFromCodeCoverage]
public class Swagger
{
    private const string SwaggerUIHtml = """
        <!DOCTYPE html>
        <html lang="en">
        <head>
            <meta charset="UTF-8">
            <title>PTS Pet Functions API - Swagger UI</title>
            <link rel="stylesheet" href="https://unpkg.com/swagger-ui-dist@5/swagger-ui.css">
        </head>
        <body>
            <div id="swagger-ui"></div>
            <script src="https://unpkg.com/swagger-ui-dist@5/swagger-ui-bundle.js"></script>
            <script>
                SwaggerUIBundle({
                    url: '/swagger.json',
                    dom_id: '#swagger-ui',
                    presets: [SwaggerUIBundle.presets.apis, SwaggerUIBundle.SwaggerUIStandalonePreset],
                    layout: 'BaseLayout'
                });
            </script>
        </body>
        </html>
        """;

    [Function("SwaggerUI")]
    public IActionResult SwaggerUI(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger")] HttpRequest req)
    {
        return new ContentResult
        {
            Content = SwaggerUIHtml,
            ContentType = "text/html",
            StatusCode = 200
        };
    }

    [Function("SwaggerJson")]
    public async Task<IActionResult> SwaggerJson(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "swagger.json")] HttpRequest req)
    {
        var basePath = AppContext.BaseDirectory;
        var swaggerPath = Path.Combine(basePath, "swagger.json");

        if (!File.Exists(swaggerPath))
        {
            swaggerPath = Path.Combine(Directory.GetCurrentDirectory(), "swagger.json");
        }

        if (!File.Exists(swaggerPath))
        {
            return new NotFoundObjectResult("swagger.json not found");
        }

        var json = await File.ReadAllTextAsync(swaggerPath);
        return new ContentResult
        {
            Content = json,
            ContentType = "application/json",
            StatusCode = 200
        };
    }
}
#endif
