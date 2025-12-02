# fictional-train

This repository contains a minimal Azure Functions isolated worker example that exposes Model Content Protocol (MCP) tools for saving and retrieving snippets in blob storage.

## Project layout

- `SampleSnippetTool/SnippetTool.csproj` — Azure Functions project with MCP trigger support.
- `SampleSnippetTool/SnippetTool.cs` — Functions that expose the `save_snippet` and `get_snippets` MCP tools.
- `SampleSnippetTool/Program.cs` — Registers the `get_snippets` tool properties.
- `SampleSnippetTool/host.json` — Enables the MCP extension and configures logging.
- `SampleSnippetTool/local.settings.json` — Local development settings (uses Azurite by default).

## Usage

1. Install the .NET 8 SDK and the Azure Functions Core Tools.
2. Start local storage (for example, `azurite --silent` if you have Azurite installed).
3. From the `SampleSnippetTool` folder, restore packages and run the Functions host:

   ```bash
   dotnet restore
   func start
   ```

The `save_snippet` tool writes snippets to `snippets/{snippetname}.json` in blob storage, and `get_snippets` retrieves them using the same name-based path.
