---
title: "VS Code tools for Windows development"
description: Use the WinApp VS Code extension and Microsoft Learn MCP Server to build, debug, package, and sign Windows apps with current API guidance — without leaving VS Code.
ms.topic: overview
ms.date: 05/13/2026
ms.author: jken
author: GrantMeStrength
---

# VS Code tools for Windows development

Two VS Code tools round out the AI-assisted Windows development workflow: the **WinApp extension** brings the Windows App Development CLI into the editor, and the **Microsoft Learn MCP Server** gives your AI agent live access to current Windows documentation.

## WinApp VS Code extension

The WinApp extension brings the Windows App Development CLI into VS Code — initialize, run, debug, package, and sign Windows apps without leaving the editor.

> [!NOTE]
> The extension is in prerelease. Features and commands may change. [File feedback](https://github.com/microsoft/WinAppCli/issues).

### Install

```powershell
code --install-extension microsoft-winappcli.winapp
```

Or search **WinApp** in the Extensions panel (**Ctrl+Shift+X**). Requires the [WinApp CLI](quickstart.md#prerequisites) to be installed first.

### Command Palette commands

All commands are available via **Ctrl+Shift+P → WinApp**:

| Command | What it does |
|---------|-------------|
| **WinApp: Initialize Project** | Set up a new project with the Windows SDK and/or Windows App SDK |
| **WinApp: Run Application** | Run your app as a loose-layout package with full package identity |
| **WinApp: Create MSIX Package** | Package your app into an MSIX installer |
| **WinApp: Create Debug Identity** | Add sparse package identity to an existing executable for debugging |
| **WinApp: Unregister Package** | Remove a sideloaded development package |
| **WinApp: Generate Manifest** | Generate an `AppxManifest.xml` from a template |
| **WinApp: Add Manifest Execution Alias** | Add an execution alias to the app manifest |
| **WinApp: Update Manifest Assets** | Generate all required app icon assets from a single source image |
| **WinApp: Generate Certificate** | Create a development signing certificate |
| **WinApp: Certificate Info** | View details about a certificate file |
| **WinApp: Install Certificate** | Install a `.pfx` or `.cer` certificate (requires Administrator) |
| **WinApp: Sign Package** | Sign an MSIX package with a certificate |
| **WinApp: Restore Packages** | Restore project packages and dependencies |
| **WinApp: Update Packages** | Update packages to the latest versions |
| **WinApp: Get WinApp Path** | Show the path to the installed WinApp CLI executable |
| **WinApp: Run SDK Tool** | Run Windows SDK tools directly |

### Workflow

1. `dotnet new winui-navview -n MyApp` — scaffold project
2. `cd MyApp && dotnet run` — build and verify it runs
3. `code .` — open in VS Code
4. **Ctrl+Shift+P → WinApp: Run Application** — run with package identity
5. Edit XAML and C# files with AI assistance
6. **Ctrl+Shift+P → WinApp: Create MSIX Package** — package for distribution
7. `winapp store publish ./*.msix --appId <your-app-id>` — publish to the Store

---

## Microsoft Learn MCP Server

AI models are trained on a snapshot of the web. For Windows development, that means your agent may have learned from WPF and UWP samples written years before WinUI 3 existed — and it can't tell the difference. The Microsoft Learn MCP Server fixes this by giving your agent a tool it can call to retrieve **current, authoritative documentation** at the moment it needs it.

### What is MCP?

The [Model Context Protocol (MCP)](https://modelcontextprotocol.io) is an open standard that lets AI agents call external tools and data sources during a conversation. Instead of relying entirely on training data, an MCP-connected agent can search and read live content — including Microsoft Learn — before generating a response.

### Add the Microsoft Learn MCP Server

The server is hosted by Microsoft and requires no installation or sign-in.

#### VS Code (GitHub Copilot)

Add the following to `.vscode/mcp.json` in your project:

```json
{
  "servers": {
    "microsoft-learn": {
      "type": "http",
      "url": "https://learn.microsoft.com/api/mcp"
    }
  }
}
```

VS Code will prompt you to enable the server the first time you open a Copilot chat session.

#### Claude Code

Add the server to your Claude Code configuration (`~/.claude/mcp_servers.json`):

```json
{
  "microsoft-learn": {
    "type": "http",
    "url": "https://learn.microsoft.com/api/mcp"
  }
}
```

#### Other MCP clients

Any client that supports the MCP HTTP transport can connect using:

```
https://learn.microsoft.com/api/mcp
```

No API key or authentication required.

### What the server can do

Once connected, your agent can search and retrieve pages from Microsoft Learn. For Windows development, this means it can look up:

- Current WinUI 3 control APIs and usage patterns
- Windows App SDK release notes and migration guides
- `winapp` CLI command reference
- Store submission requirements and certification criteria

### Example

Without the MCP server, asking Copilot to add a file picker may produce code using the deprecated UWP `FileOpenPicker` pattern:

```csharp
// ❌ UWP pattern — may be generated without MCP context
var picker = new FileOpenPicker();
picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
```

With the MCP server connected, the agent retrieves the current WinUI 3 guidance and generates the correct pattern:

```csharp
// ✅ WinUI 3 pattern — retrieved from current docs
var picker = new FileOpenPicker();
var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);
picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
var file = await picker.PickSingleFileAsync();
```

> [!TIP]
> For deeper WinUI-specific guidance, combine the MCP server with the [WinUI agent plugin](winui-agent-plugin.md). The plugin handles coding patterns; the MCP server handles documentation retrieval.

---

## Related content

- [Quickstart: Build and publish a Windows app with AI](quickstart.md)
- [WinUI agent plugin](winui-agent-plugin.md)
- [Model Context Protocol specification](https://modelcontextprotocol.io)
- [GitHub Copilot MCP documentation](https://docs.github.com/copilot/customizing-copilot/extending-copilot-with-mcp)
