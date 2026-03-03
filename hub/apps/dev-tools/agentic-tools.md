---
title: Agentic AI tools for Windows development
description: Learn how to enhance AI coding agents with Windows-specific context using MCP servers, custom instructions, and community plugins.
ms.date: 03/03/2026
ms.topic: article
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

## Awesome Copilot customizations

The [Awesome Copilot](https://github.com/github/awesome-copilot) repository is a community-driven collection of custom instructions, agents, skills, and plugins for GitHub Copilot. These resources teach Copilot about specific frameworks and coding patterns so it can generate more accurate and idiomatic code.

For Windows developers, the repository offers plugins that bundle together:

- **Custom instructions** — guidelines that shape how Copilot writes code, such as enforcing MVVM patterns, XAML best practices, or specific API usage
- **Agents** — specialized Copilot personas with domain expertise, like expert .NET engineers
- **Skills** — reusable prompts for common tasks like unit testing, code review, or framework upgrades

**Install a plugin:**

You can install a plugin using the Copilot CLI. For example, to install the [C# .NET Development plugin](https://github.com/github/awesome-copilot/tree/main/plugins/csharp-dotnet-development):

```bash
copilot plugin install csharp-dotnet-development@awesome-copilot
```

This copies the plugin's instructions, agents, and skills into your project's `.github/` directory, where Copilot picks them up automatically.

**Browse and discover:**

You can also browse available customizations using the [Awesome Copilot extension for VS Code](https://marketplace.visualstudio.com/items?itemName=TimHeuer.awesome-copilot), which lets you preview and download resources directly into your workspace.