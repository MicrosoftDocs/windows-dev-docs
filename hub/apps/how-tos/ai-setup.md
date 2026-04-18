---
title: Set up GitHub Copilot for Windows development
description: Install and configure GitHub Copilot, the WinUI 3 plugin, and the Microsoft Learn MCP server for AI-assisted Windows app development.
ms.topic: how-to
ms.date: 03/10/2026
ms.author: jken
author: GrantMeStrength
keywords: windows, github copilot, setup, mcp server, winui 3 plugin, vs code, visual studio
ms.localizationpriority: medium
---

# Set up GitHub Copilot for Windows development

This guide walks you through setting up GitHub Copilot with the tools that make it genuinely useful for Windows development: the **WinUI 3 development plugin** that gives Copilot accurate Windows App SDK context, and the **Microsoft Learn MCP Server** that gives Copilot live access to official Windows documentation.

> [!NOTE]
> Building and debugging WinUI 3 apps requires **Visual Studio** — VS Code doesn't have MSIX build tooling or WinUI project templates. If you're working on other Windows frameworks (Electron, Flutter, .NET, or Rust via winapp CLI), VS Code works well for those. Steps below are marked accordingly.

## Prerequisites

- A [GitHub Copilot subscription](https://github.com/features/copilot) (a free tier is available)
- [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio 2026](/visualstudio/install/install-visual-studio)
- [Node.js](https://nodejs.org/) 18 or later (required for the Copilot plugin install command)

---

## Step 1: Install GitHub Copilot in your IDE

#### [Visual Studio (WinUI 3)](#tab/visualstudio)

GitHub Copilot is built into Visual Studio 2026. Visual Studio is required for WinUI 3 development.

1. Open Visual Studio and go to **Extensions** > **Manage Extensions**.
2. Search for **GitHub Copilot** and install it, or verify it's already installed.
3. Sign in via **Tools** > **Options** > **GitHub** > **Accounts**.

For detailed setup, see [Tutorial: Build a Windows app with GitHub Copilot](ai-build.md).

#### [VS Code (Electron, Flutter, .NET, Rust)](#tab/vscode)

1. Open VS Code and go to the **Extensions** view (`Ctrl+Shift+X`).
2. Search for **GitHub Copilot** and install the extension.
3. Sign in with your GitHub account when prompted.
4. To enable **agent mode** (required for multi-step tasks), open **Settings** (`Ctrl+,`), search for `chat.agent.enabled`, and toggle it on.

For detailed setup, see [GitHub Copilot in VS Code](https://code.visualstudio.com/docs/copilot/overview).

---

## Step 2: Install the WinUI 3 development plugin

The [WinUI 3 development plugin](../dev-tools/agentic-tools.md#winui-3-development-plugin-for-github-copilot) from the [Awesome Copilot](https://github.com/github/awesome-copilot) community repository teaches Copilot the right Windows App SDK patterns — preventing common mistakes like using deprecated UWP APIs.

Run this command in your project root (or any directory where you want Copilot to pick up the plugin):

```bash
copilot plugin install winui3-development@awesome-copilot
```

This copies agents, skills, and custom instructions into your project's `.github/` directory. Copilot automatically picks them up the next time you open the project.

> [!TIP]
> You can also browse and install Copilot plugins directly from VS Code using the [Awesome Copilot extension](https://marketplace.visualstudio.com/items?itemName=TimHeuer.awesome-copilot).

---

## Step 3: Add the Microsoft Learn MCP Server

The [Microsoft Learn MCP Server](../dev-tools/agentic-tools.md#microsoft-learn-mcp-server) gives Copilot live access to official Microsoft documentation — so it can look up current API references and code samples as it helps you code.

#### [VS Code (Electron, Flutter, .NET, Rust)](#tab/vscode)

Add the following to your VS Code `settings.json` (open with `Ctrl+Shift+P` > **Open User Settings (JSON)**):

```json
{
  "mcp": {
    "servers": {
      "microsoft-learn": {
        "type": "http",
        "url": "https://learn.microsoft.com/api/mcp"
      }
    }
  }
}
```

Or, to add it per-project, create `.vscode/mcp.json` in your project root:

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

#### [Visual Studio](#tab/visualstudio)

1. Go to **Tools** > **Options** > **GitHub** > **Copilot** > **MCP Servers**.
2. Add a new server with the URL: `https://learn.microsoft.com/api/mcp`

---

## Step 4: Verify your setup

Open Copilot Chat and try these prompts to confirm everything is working:

**Test the WinUI 3 plugin:**
> *"Add a confirmation dialog to my WinUI 3 app that asks before deleting an item."*

Copilot should respond with a `ContentDialog` implementation including the required `XamlRoot` setup — the plugin's Windows App SDK context guides it to the right modern API without you needing to specify what to avoid.

**Test the Learn MCP Server:**
> *"Look up the latest Windows App SDK release notes and tell me what's new."*

Copilot should fetch the current release notes from Microsoft Learn and summarize them.

---

## Optional: Add more Windows MCP servers

Extend Copilot's context further with additional Windows-specific MCP servers:

| MCP Server | What it gives Copilot | URL / setup |
|---|---|---|
| Azure DevOps | Access work items, PRs, and builds | [Azure DevOps MCP Server](https://github.com/microsoft/azure-devops-mcp) |

---

## Next steps

> [!div class="nextstepaction"]
> [Tutorial: Build a Windows app with GitHub Copilot](ai-build.md)

- [Modernize or port a Windows app with Copilot](../windows-app-sdk/migrate-to-windows-app-sdk/ai-modernize.md)
- [Agentic AI tools for Windows development](../dev-tools/agentic-tools.md) — full details on all tools
