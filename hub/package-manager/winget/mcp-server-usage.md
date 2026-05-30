---
title: Use WinGet MCP Server
description: Learn how to use the WinGet MCP server with AI agents in Visual Studio Code and GitHub Copilot CLI, including available commands, example prompts, tips, troubleshooting, and limitations.
ms.date: 03/24/2026
ms.topic: how-to
---

# Use WinGet MCP Server

Once the WinGet MCP server is [set up](mcp-server-setup.md), you can use it with AI agents in Visual Studio Code and GitHub Copilot CLI to discover and install packages.

## Use WinGet MCP tools in Visual Studio Code

To start using the Windows Package Manager (WinGet) MCP Server in Visual Studio Code:

1. Open the GitHub Copilot extension chat window and select **Agent Mode** to enable MCP tool integration.
1. Access WinGet MCP tools by selecting the tool icon in the GitHub Copilot chat window and search for **MCP Server: winget-mcp** in the available tools list.
1. Verify that the WinGet MCP server tools are available, with a checkmark next to the entry.
1. Begin asking questions or requesting assistance with package management tasks. The AI agent automatically uses the WinGet MCP tools when appropriate to provide accurate, context-aware help.

## Use WinGet MCP in GitHub Copilot CLI

Once the server is registered, Copilot CLI automatically calls WinGet MCP tools when your prompt involves package management. After each search or install request, Copilot asks for your approval before running any command. For example prompts and prompting tips, see [Example prompts](#example-prompts).

## Available commands

The WinGet MCP server currently supports the following commands:

### find

Searches the WinGet repository for packages matching specified criteria. This tool helps discover available software and their details.

The AI agent passes your natural-language intent (for example, "find a PDF reader") to the WinGet MCP server, which translates it into the appropriate `winget search` query. You do not specify named parameters directly — the agent interprets your request and constructs the search.

### install

Initiates the installation of a specified package from the WinGet repository. The agent always asks for your approval before executing any installation command.

The AI agent determines the package identifier, version, and any installation options from your natural-language prompt and constructs the corresponding `winget install` command. You can specify a version or additional options in your prompt (for example, "install Node.js 20 LTS"), and the agent maps them to the appropriate command-line arguments.

## Example prompts

The following prompts work well with both GitHub Copilot (VS Code) and GitHub Copilot CLI when the WinGet MCP server is configured:

| Goal | Example prompt |
|---|---|
| Search for a package | `"Search WinGet for a PDF reader"` |
| Install by name | `"Install the latest version of Git"` |
| Install a specific version | `"Install Node.js 20 LTS from WinGet"` |
| Find packages for a task | `"What WinGet packages are available for container development?"` |
| Find packages for Python development | `"What packages are available for Python development?"` |
| Install a common tool | `"Help me install Visual Studio Code"` |
| Find platform-specific packages | `"Find packages for Docker on Windows"` |

## Tips for effective prompting

To help Copilot reliably invoke the WinGet MCP tools, use language that clearly indicates a package management intent. If Copilot does not use the WinGet MCP tools automatically, add explicit keywords such as **"using WinGet"**, **"from WinGet"**, or **"search WinGet"** to your prompt.

When using VS Code, also verify that Agent Mode is still enabled and that **winget-mcp** is checked in the tools panel. When using Copilot CLI, use `/mcp` to confirm the server is connected.

## Troubleshooting

### Connection issues

If you encounter connection issues between VS Code and the WinGet MCP server:

1. Verify your `mcp.json` configuration file syntax.
1. Check that the path to `WindowsPackageManagerMCPServer.exe` is correct.
1. Ensure the executable has proper permissions to run.
1. Review VS Code's output panel for detailed error messages.
1. Try restarting the MCP integration in VS Code.

### Limited or no response from AI Agent

If the AI agent doesn't seem to be using WinGet MCP tools:

- Use specific prompts that clearly indicate you want package management information.
- Try phrases like "Search for packages" or "Install using WinGet".
- Verify that Agent Mode is enabled in Copilot Chat.
- Check if the WinGet MCP tools are visible in the tools list.

### Package installation issues

If package installation fails or behaves unexpectedly:

- Review the installation command or parameters suggested by the AI agent.
- Check the [WinGet troubleshooting guide](./troubleshooting.md) for common issues.
- Verify you have appropriate permissions to install software.
- Ensure the package source is accessible.

## Limitations

> [!IMPORTANT]
> You are responsible for reviewing all commands generated with AI assistance. **Always validate installation commands and verify the software source before executing them on your system.**

Current limitations of the WinGet MCP server integration include:

- **Agent behavior**: AI agents might not use the WinGet MCP server for every query, though specific prompting can encourage its use.
- **Tool availability**: The integration currently supports only `find` and `install` commands. Future releases might add support for more commands.

## Related content

- [WinGet MCP Server overview](mcp-server-overview.md)
- [Set up WinGet MCP Server](mcp-server-setup.md)
