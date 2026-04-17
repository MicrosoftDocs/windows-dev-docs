---
title: Agentic AI tools for Windows development
description: Learn how to enhance AI coding agents with Windows-specific context using MCP servers, custom instructions, and community plugins.
ms.date: 03/11/2026
ms.topic: how-to
---

# Agentic AI tools for Windows development

AI coding agents work best when they have access to accurate, up-to-date context about the frameworks and APIs you're using. By connecting your agent to the right tools and resources, you can significantly improve the quality of the code it generates for Windows apps.

The following resources help you give your AI coding agent deeper knowledge of Windows development, from official Microsoft documentation to community-contributed best practices.

## Microsoft Learn MCP Server

The [Microsoft Learn MCP Server](/training/support/mcp) gives AI coding agents direct access to official Microsoft documentation. It's a remote [Model Context Protocol (MCP)](https://modelcontextprotocol.io/) server that lets agents search documentation, fetch complete articles, and find code samples from Microsoft Learn.

This means your agent can look up the latest API references, find working examples, and verify its suggestions against official docs — all without you having to copy and paste documentation into your chat context.

**Key details:**

- **Free to use**, no authentication required
- Works with MCP-compatible clients like VS Code, Visual Studio, and other agentic development environments
- Powered by the same knowledge service behind [Copilot for Azure](https://azure.microsoft.com/products/copilot) and Ask Learn

**Get started:**

Add the following MCP server endpoint to your agent or IDE configuration:

```
https://learn.microsoft.com/api/mcp
```

For step-by-step setup instructions, see [Get started with the Learn MCP Server in VS Code](/training/support/mcp-get-started) or [in Foundry](/training/support/mcp-get-started-foundry).

## WinUI 3 development plugin for GitHub Copilot

The [Awesome Copilot](https://github.com/github/awesome-copilot) repository is a community-driven collection of custom instructions, agents, skills, and plugins for GitHub Copilot. These resources teach Copilot about specific frameworks so it generates more accurate and idiomatic code.

The [WinUI 3 Development plugin](https://github.com/github/awesome-copilot/tree/main/plugins/winui3-development) is built specifically for Windows App SDK developers. It prevents common mistakes — like using legacy UWP APIs that no longer work in WinUI 3 — and guides Copilot toward correct, modern patterns.

The plugin includes:

- **WinUI 3 Expert agent** — an expert agent that covers UWP-to-WinUI 3 API migration rules, XAML controls, MVVM patterns, windowing, threading, app lifecycle, dialogs, and deployment
- **Migration guide skill** — a slash command (`/winui3-development:winui3-migration-guide`) with API namespace mappings, before/after code snippets, and a step-by-step migration checklist
- **Custom instructions** — rules applied to XAML, C#, and `.csproj` files that prevent the most common Copilot code generation mistakes, such as using `CoreDispatcher` instead of `DispatcherQueue`, or `MessageDialog` instead of `ContentDialog`

**Install the plugin:**

```bash
copilot plugin install winui3-development@awesome-copilot
```

This copies the plugin's agents, skills, and custom instructions into your project's `.github/` directory, where Copilot picks them up automatically.

> [!NOTE]
> WinUI 3 apps require **Visual Studio** for building and debugging — VS Code does not have the required MSIX build tooling or WinUI project templates. However, if you're using VS Code to work on other Windows frameworks (Electron, Flutter, .NET, or Rust with winapp CLI), this plugin still provides useful Windows App SDK context for those scenarios.

**Browse and discover more plugins:**

You can browse all available Copilot customizations using the [Awesome Copilot extension for VS Code](https://marketplace.visualstudio.com/items?itemName=TimHeuer.awesome-copilot), which lets you preview and install resources directly into your workspace.

## Why does Copilot need these tools?

Copilot's code suggestions are shaped by its training data. Because UWP has many more years of community samples, tutorials, and Stack Overflow answers than WinUI 3, Copilot may default to UWP patterns — such as `Windows.UI.Xaml` namespaces, `CoreDispatcher`, or `MessageDialog` — when you ask about Windows UI development. This isn't a quality problem with WinUI 3; it reflects a training data distribution gap that's common with any newer framework.

The WinUI 3 plugin addresses this directly: its custom instructions give Copilot explicit, up-to-date rules about current Windows App SDK patterns, so it generates correct code without you needing to prompt around its knowledge gaps. Combined with the Learn MCP Server's live documentation access, Copilot has the context it needs to be genuinely useful for Windows development.

## Set up step-by-step

For a complete walkthrough — including IDE setup, agent mode, MCP server configuration, and verification — see [Set up GitHub Copilot for Windows development](../how-tos/ai-setup.md).

## Related content

- [Set up GitHub Copilot for Windows development](../how-tos/ai-setup.md)
- [Tutorial: Build a Windows app with GitHub Copilot](../how-tos/ai-build.md)