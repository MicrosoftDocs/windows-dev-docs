---
title: Set up WinGet MCP Server
description: Learn how to set up the WinGet MCP server for use with AI agents in Visual Studio Code and GitHub Copilot CLI, including prerequisites, finding the executable path, and configuration.
ms.date: 03/24/2026
ms.topic: how-to
---

# Set up WinGet MCP Server

This guide walks through configuring the WinGet MCP server for use with AI agents in Visual Studio Code and GitHub Copilot CLI.

## Prerequisites

Before using the WinGet MCP server integration, ensure you have:

- Windows 11 *(or Windows 10 version 1809, build 17763, or later)*
- VS Code v1.104 or later with [GitHub Copilot extension](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot) enabled
- Access to [Copilot in VS Code](https://code.visualstudio.com/docs/copilot/overview)
- [WinGet with MCP server support installed on your system](#find-the-winget-mcp-server-executable-path)
- Extended features enabled: `winget configure --enable`

## Find the WinGet MCP server executable path

To set up the WinGet MCP server, you must first locate the path to the executable file. Use one of the following options to find the path:

### Option 1: Use the `winget mcp` command

Open a command prompt and enter the `winget mcp` command. The result displays the JSON configuration fragment that shows the path to the `WindowsPackageManagerMCPServer.exe` file.

```powershell
winget mcp
```

If the following error occurs: **Unrecognized command: 'mcp'**

Verify that the App Installer application is up to date on the device. This can be verified in the Store application.

### Option 2: Use a PowerShell script

Open PowerShell and use the following script to locate the MCP server executable:

```powershell
# Find the WinGet executable path
$wingetPath = (Get-Command winget).Source
# Get the directory containing WinGet
$wingetDir = Split-Path $wingetPath -Parent
# The MCP server executable is in the same directory
$mcpServerPath = Join-Path $wingetDir "Microsoft.DesktopAppInstaller_8wekyb3d8bbwe\WindowsPackageManagerMCPServer.exe"
Write-Host "WinGet MCP Server path: $mcpServerPath"
```

The typical location is:`C:\Users\<username>\AppData\Local\Microsoft\WindowsApps\Microsoft.DesktopAppInstaller_8wekyb3d8bbwe\WindowsPackageManagerMCPServer.exe`

## Configure the MCP server in Visual Studio Code

The recommended way to configure the WinGet MCP server is through an `mcp.json` configuration file. You need to find (you might need to show hidden files) or create a `.vscode` folder in the root directory of your project. In this directory, create a `mcp.json` file (or update it if this file already exists) with the following content:

```json
{
  "servers": {
    "winget-mcp": {
      "type": "stdio",
      "command": "C:\\Users\\<username>\\AppData\\Local\\Microsoft\\WindowsApps\\Microsoft.DesktopAppInstaller_8wekyb3d8bbwe\\WindowsPackageManagerMCPServer.exe"
    }
  },
  "inputs": []
}
```

Replace `<username>` with your actual Windows username or use the WinGet MCP server executable path that you previously identified.

This configuration tells MCP clients to:

- Use the Windows Package Manager (WinGet) MCP server executable as the command
- Use standard I/O communication between the client and server
- Register the server with the identifier `winget-mcp`

For detailed information about MCP configuration and setup in VS Code, see [Use MCP servers in VS Code](https://code.visualstudio.com/docs/copilot/customization/mcp-servers).

## Configure the MCP server in GitHub Copilot CLI

[GitHub Copilot CLI](https://docs.github.com/copilot/concepts/agents/about-copilot-cli) brings AI-powered assistance directly to your terminal. After registering the WinGet MCP server with Copilot CLI, you can search for and install packages using natural language prompts — without leaving the command line.

### Copilot CLI prerequisites

- [GitHub Copilot CLI installed](https://docs.github.com/copilot/how-tos/set-up/install-copilot-cli) and authenticated
- The WinGet MCP server executable path (see [Find the WinGet MCP server executable path](#find-the-winget-mcp-server-executable-path))

### Option 1: Use the interactive `/mcp add` command (recommended)

1. Start Copilot CLI in your terminal:

   ```powershell
   copilot
   ```

1. At the prompt, enter the `/mcp add` slash command:

   ```
   /mcp add
   ```

1. Fill in the MCP server details in the interactive form. Use **Tab** to move between fields:

   | Field | Value |
   |---|---|
   | Name | `winget-mcp` |
   | Type | `stdio` |
   | Command | Full path to `WindowsPackageManagerMCPServer.exe` |

1. Press **Ctrl+S** to save. Copilot CLI writes the configuration to `~/.copilot/mcp-config.json`.

### Option 2: Edit `mcp-config.json` directly

Open (or create) `~/.copilot/mcp-config.json` and add the following entry, replacing `<username>` with your Windows username:

```json
{
  "mcpServers": {
    "winget-mcp": {
      "type": "stdio",
      "command": "C:\\Users\\<username>\\AppData\\Local\\Microsoft\\WindowsApps\\Microsoft.DesktopAppInstaller_8wekyb3d8bbwe\\WindowsPackageManagerMCPServer.exe",
      "args": [],
      "tools": ["*"]
    }
  }
}
```

> [!NOTE]
> If `mcp-config.json` already exists and contains other servers, add the `winget-mcp` entry inside the existing `mcpServers` object rather than replacing the file.

### Verify the server is registered

After adding the server, confirm it is loaded:

1. Start or restart Copilot CLI:

   ```powershell
   copilot
   ```

1. At the prompt, enter:

   ```
   /mcp
   ```

   The output lists all configured MCP servers. Verify that **winget-mcp** appears and shows a connected status.

## Manual command-line testing

To start the WinGet MCP server manually for testing or development purposes, run the executable directly by entering the following command in PowerShell:

```powershell
& "C:\Users\<username>\AppData\Local\Microsoft\WindowsApps\Microsoft.DesktopAppInstaller_8wekyb3d8bbwe\WindowsPackageManagerMCPServer.exe"
```

Replace `<username>` with your actual Windows username.

The server starts and waits for MCP protocol messages on standard input. The server continues running until you terminate it (Ctrl+C) or close the input stream.

## Related content

- [WinGet MCP Server overview](mcp-server-overview.md)
- [Use WinGet MCP Server](mcp-server-usage.md)
