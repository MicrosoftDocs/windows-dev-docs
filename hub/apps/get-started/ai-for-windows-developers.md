---
title: AI-assisted Windows development
description: Discover how GitHub Copilot, MCP servers, and Windows-specific AI tools make it faster and easier to build and modernize Windows apps.
ms.topic: overview
ms.date: 03/10/2026
ms.author: jken
author: GrantMeStrength
keywords: windows, github copilot, ai development, winui, copilot, mcp server
ms.localizationpriority: medium
---

# AI-assisted Windows development

GitHub Copilot and a growing set of Windows-specific AI tools have changed what it means to build for Windows. Whether you're starting a brand-new WinUI app, integrating Windows AI APIs, or migrating a legacy UWP project, AI can handle the scaffolding, suggest the right APIs, explain unfamiliar patterns, and keep your code on the right track — all without leaving your IDE.

This article introduces the tools and what they make possible. When you're ready, follow the links to set up your environment and start building.

## What AI tools are available for Windows developers?

### GitHub Copilot

[GitHub Copilot](https://github.com/features/copilot) is an AI coding assistant built into Visual Studio and VS Code. It provides inline completions as you type, a chat panel for asking questions and generating code, and an **agent mode** that can autonomously complete multi-step tasks — creating files, running commands, and iterating until the task is done.

For Windows developers, the key is giving Copilot accurate Windows-specific knowledge. Out of the box, Copilot knows a lot about general C# and .NET — but it can struggle with WinUI 3 specifics, confusing newer APIs with deprecated UWP equivalents. The tools below fix that.

### WinUI 3 development plugin and Microsoft Learn MCP Server

Two tools give Copilot the Windows-specific context it needs: the **WinUI 3 development plugin** (which teaches Copilot correct modern API patterns) and the **Microsoft Learn MCP Server** (which gives Copilot live access to official documentation). Full details and setup are in [Agentic AI tools for Windows development](../dev-tools/agentic-tools.md).

### Windows App Development CLI (winapp CLI)

The [winapp CLI](../dev-tools/winapp-cli/index.md) is a command-line tool that adds Windows packaging, app identity, and SDK setup to any project — WinUI, .NET, Electron, Flutter, Rust, and more. Package identity is what unlocks powerful Windows features:

- On-device AI APIs (Phi Silica, text recognition, image generation)
- Interactive notifications and Windows shell integration
- MSIX packaging for the Microsoft Store or enterprise deployment

Copilot can help you understand the manifest and adapt your code for Windows APIs — while winapp CLI handles the packaging plumbing. Note that winapp CLI itself is not an AI tool; it's a command-line utility that Copilot can guide you through using.

---

## What can you do with these tools?

Here are some real things you can accomplish:

### Start a new Windows app in minutes

> *"Create a WinUI 3 app with a NavigationView, three pages, and an MVVM architecture. Use the Windows App SDK."*

With the WinUI 3 plugin installed and the Learn MCP server connected, Copilot generates a complete, modern scaffold — correct namespaces, proper threading patterns, no legacy UWP confusion.

### Add Windows features to any app

> *"Add a Windows notification that fires when a background task completes. My app is packaged with MSIX."*

Copilot can look up the right `AppNotification` APIs via the Learn MCP server and generate working notification code, while winapp CLI handles the identity and manifest setup that makes notifications available to your app.

### Migrate a UWP app to WinUI 3

> `/winui3-development:winui3-migration-guide`

Run the migration guide skill against your existing UWP files and Copilot produces a step-by-step migration plan with API substitutions, XAML changes, and packaging updates tailored to your specific codebase.

### Modernize a WPF or WinForms app

> *"I have a WPF app targeting .NET 4.8. Show me how to add push notifications using the Windows App SDK."*

With the Learn MCP server, Copilot finds the right Windows App SDK documentation and generates the notification code — without you needing to dig through docs manually.

### Add Windows to a cross-platform app

Electron, Flutter, React Native, and Rust developers can use Copilot to adapt their existing code for Windows-specific features, and use winapp CLI to add MSIX packaging and package identity — turning a cross-platform app into a first-class Windows citizen.

---

## Next steps

> [!div class="nextstepaction"]
> [Set up GitHub Copilot for Windows development](ai-setup.md)

Or jump straight to:

- [Tutorial: Build a Windows app with GitHub Copilot](ai-build.md)
- [Modernize or port a Windows app with Copilot](../windows-app-sdk/migrate-to-windows-app-sdk/ai-modernize.md)
- [Agentic AI tools for Windows development](../dev-tools/agentic-tools.md)
