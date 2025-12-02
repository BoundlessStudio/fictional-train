using Microsoft.Azure.Functions.Worker.ApplicationInsights;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Azure.Functions.Worker.Extensions.Http.AspNetCore;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder
    .ConfigureMcpTool("get_snippets")
    .WithProperty("snippetname", "string", "The name of the snippet.", required: true);

builder.Services
    .AddApplicationInsightsTelemetryWorkerService();

builder.Build().Run();
