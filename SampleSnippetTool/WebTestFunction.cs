using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace SampleSnippetTool;

public class WebTestFunction
{
    private readonly ILogger<WebTestFunction> _logger;

    public WebTestFunction(ILogger<WebTestFunction> logger)
    {
        _logger = logger;
    }

    [Function("WebTestFunction")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req, ClaimsPrincipal user)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions" + user.Identity.Name);
    }
}