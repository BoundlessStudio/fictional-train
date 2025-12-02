using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using Microsoft.Azure.Functions.Worker.Extensions.Storage;
using Microsoft.Extensions.Logging;

namespace SampleSnippetTool;

public class SnippetTool
{
    private const string BlobPath = "snippets/{mcptoolargs.snippetname}.json";

    private readonly ILogger<SnippetTool> _logger;

    public SnippetTool(ILogger<SnippetTool> logger)
    {
        _logger = logger;
    }

    [Function(nameof(SaveSnippet))]
    [BlobOutput(BlobPath)]
    public string SaveSnippet(
        [McpToolTrigger("save_snippet", "Saves a code snippet into your snippet collection.")]
            ToolInvocationContext context,
        [McpToolProperty("snippetname", "The name of the snippet.", isRequired: true)]
            string name,
        [McpToolProperty("snippet", "The code snippet.", isRequired: true)]
            string snippet)
    {
        _logger.LogInformation(
            "Saving snippet {SnippetName} via MCP tool call {ToolName}.",
            name,
            context.Name);

        return snippet;
    }

    [Function(nameof(GetSnippet))]
    public object GetSnippet(
        [McpToolTrigger("get_snippets", "Gets code snippets from your snippet collection.")]
            ToolInvocationContext context,
        [BlobInput(BlobPath)] string? snippetContent)
    {
        var snippetName = context.Arguments is not null &&
            context.Arguments.TryGetValue("snippetname", out var snippetValue)
                ? snippetValue?.ToString() ?? string.Empty
                : string.Empty;

        _logger.LogInformation(
            "Retrieving snippet {SnippetName} via MCP tool call {ToolName}.",
            snippetName,
            context.Name);

        return snippetContent ?? $"Snippet '{snippetName}' was not found.";
    }
}
