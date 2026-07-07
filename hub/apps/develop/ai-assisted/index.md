---
title: AI-assisted Windows development
description: Build Windows apps faster using AI agents, GitHub Copilot, Claude Code, and the Windows AI development toolkit — free tools that work in VS Code.
ms.topic: overview
ms.date: 07/05/2026
ms.author: jken
author: GrantMeStrength
---

# AI-assisted Windows development

Windows has a complete set of AI-ready tools that take you from idea to published app. Whether you prefer the command line and VS Code or a full IDE like Visual Studio, AI agents can do the heavy lifting. This section covers both paths, and will help you no matter if you're building a new app from scratch or modernizing one you've already written.

:::image type="content" source="images/arch.png" alt-text="Architecture diagram showing a developer writing prompts into VS Code, where GitHub Copilot and Claude Code AI agents use the WinUI Agent Plugin and winui-ui-testing skill from the Knowledge Layer, and query the Microsoft Learn MCP Server for live API docs, to generate correct WinUI 3 code targeting the Windows App SDK and .NET 10.":::

> [!TIP]
> New to Windows development? Start with the [Quickstart: Build and publish a Windows app with AI](quickstart.md) — you can have a working app in under 30 minutes using only free tools.

---

## What path are you on?

:::row:::
    :::column:::
        ### I'm starting fresh
        Use the `winui-dev` agent and `dotnet new` templates to scaffold, build, run, and publish a new Windows app — no Windows experience required.

        → [Quickstart](quickstart.md)
        → [WinUI agent plugin](winui-agent-plugin.md)
    :::column-end:::
    :::column:::
        ### I have an existing app
        AI tools can help you migrate WPF or UWP apps to modern WinUI 3, or add Windows capabilities to apps built with Electron, Flutter, Tauri, or Rust.

        → [Migrate from WPF](migrate/wpf-to-winui.md)
        → [Migrate from UWP](migrate/uwp-to-winui.md)
        → [Cross-framework apps](migrate/cross-framework.md)
    :::column-end:::
:::row-end:::

---

## Tools in this section

Many developers will use all three: the winapp CLI to scaffold and publish, the WinUI agent plugin to keep Copilot accurate, and the Microsoft Learn MCP Server for live doc access.

| Tool | What it does |
|------|-------------|
| **[WinUI agent plugin](winui-agent-plugin.md)** | 8 skills for end-to-end WinUI development in GitHub Copilot or Claude Code |
| **[AI-assisted testing](testing.md)** | Generate and run UI tests using Windows UI Automation |
| **[Publish to the Store](quickstart.md#step-5-publish-to-the-microsoft-store)** | Submit to the Microsoft Store from the command line using `winapp store` |

---

## Frequently asked questions

### Can I build a WinUI 3 app without Visual Studio?

Yes. Three commands are all you need:

```powershell
dotnet new winui-navview -n MyApp
cd MyApp
dotnet run
```

Build, debug, package, and publish from VS Code or the terminal. Visual Studio is still best for complex XAML debugging, but it's no longer required. See the [Quickstart](quickstart.md).

### Are these tools free?

Yes — the WinApp CLI, VS Code extension, and `dotnet new` templates are free and open source. GitHub Copilot requires a [subscription](https://github.com/features/copilot) (free tier available). The [Microsoft Learn MCP Server](vs-code-tools.md#microsoft-learn-mcp-server) is free with no sign-in required.

### Does this work with Claude Code as well as GitHub Copilot?

Yes. The `winui@awesome-copilot` plugin and the Microsoft Learn MCP Server both work with any MCP-compatible agent.

---

## Related content

- [Windows App Development CLI](../../dev-tools/winapp-cli/index.md)
- [Security and responsible AI](security-and-responsible-ai.md)
- [AI-assisted testing](testing.md)
- [VS Code tools for Windows development](vs-code-tools.md)
- [Migration overview](migrate/index.md)
- [AI-powered Windows features](../ai-powered/ai-powered.md)
