---
title: AI-assisted Windows development
description: Use AI coding assistants with Windows development tools to create WinUI apps, add features, and generate UI Automation tests.
ms.topic: overview
ms.date: 09/20/2026
ms.author: jken
author: GrantMeStrength
---

# AI-assisted Windows development

Use AI coding assistants to scaffold a Windows app, add features, and generate UI tests. You can use these workflows with Visual Studio, Visual Studio Code, or the command line.

> [!TIP]
> New to Windows development? Start with the [Quickstart: Build and publish a Windows app with AI](quickstart.md) — you can have a working app in under 30 minutes using only free tools.

---

## Choose a workflow

:::row:::
    :::column:::
        ### Build a new app
        Use the `winui-dev` agent and `dotnet new` templates to scaffold, build, run, and publish a new Windows app.

        → [Quickstart](quickstart.md)
        → [WinUI agent plugin](winui-agent-plugin.md)
    :::column-end:::
    :::column:::
        ### Test your app
        Use the `winui-ui-testing` skill to inspect your app, generate UI Automation tests, and run them from your coding assistant.

        → [AI-assisted testing](testing.md)
    :::column-end:::
:::row-end:::

---

## Tools in this section

The Windows App Development CLI handles project and packaging tasks, while the WinUI Agent gives your coding assistant Windows-specific development guidance.

| Tool | What it does |
|------|-------------|
| **[Windows App Development CLI](../../dev-tools/winapp-cli/index.md)** | Create, run, package, sign, and publish Windows apps from the command line |
| **[WinApp extension for Visual Studio Code](vs-code-tools.md#winapp-vs-code-extension)** | Run Windows App Development CLI commands from the Visual Studio Code Command Palette |
| **[WinUI agent plugin](winui-agent-plugin.md)** | 8 skills for end-to-end WinUI development in GitHub Copilot or Claude Code |

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

Yes. The Windows App Development CLI, WinApp extension, and `dotnet new` templates are free and open source. GitHub Copilot offers a free tier and paid plans.

### Does this work with Claude Code as well as GitHub Copilot?

Yes. You can use the `winui@awesome-copilot` plugin with GitHub Copilot or Claude Code.

---

## Related content

- [Windows App Development CLI](../../dev-tools/winapp-cli/index.md)
- [AI-assisted testing](testing.md)
- [WinApp extension for Visual Studio Code](vs-code-tools.md#winapp-vs-code-extension)
- [Modernize an existing app](../../windows-app-sdk/migrate-to-windows-app-sdk/overall-migration-strategy.md)
- [AI-powered Windows features](../ai-powered/ai-powered.md)
