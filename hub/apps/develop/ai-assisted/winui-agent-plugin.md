---
title: WinUI agent plugin for GitHub Copilot CLI and Claude Code
description: Use the winui@awesome-copilot plugin to give the GitHub Copilot CLI or Claude Code accurate WinUI 3 knowledge — including 8 specialized skills and a dedicated winui-dev agent for end-to-end Windows app development.
ms.topic: how-to
ms.date: 05/13/2026
ms.author: jken
author: GrantMeStrength
---

# WinUI agent plugin for GitHub Copilot CLI and Claude Code

The `winui@awesome-copilot` plugin gives the GitHub Copilot CLI and Claude Code accurate, up-to-date knowledge of WinUI 3 and the Windows App SDK. It includes 8 specialized skills and a dedicated `winui-dev` agent that guides AI through the full development loop — scaffold, build, run, test, package, and migrate.

> [!NOTE]
> This plugin works with the **GitHub Copilot CLI** (a terminal tool) and **Claude Code**. It does not currently integrate with VS Code Copilot Chat. For VS Code-based AI assistance without the plugin, see the [quickstart](quickstart.md).

## Why do I need this plugin?

Without the plugin, AI coding agents frequently suggest outdated UWP patterns for Windows development. UWP has far more training data (Stack Overflow answers, GitHub samples, tutorials) than WinUI 3, so agents default to deprecated APIs:

| Without plugin | With plugin |
|---|---|
| `Windows.UI.Xaml.Controls` | `Microsoft.UI.Xaml.Controls` |
| `CoreDispatcher` | `DispatcherQueue` |
| `MessageDialog` | `ContentDialog` |
| `Windows.UI.Xaml.Window` | `Microsoft.UI.Xaml.Window` |

The plugin fixes this by injecting explicit WinUI 3 rules as custom instructions that override the agent's training data defaults.

## Install the plugin

**Requires:** The [Windows App Development CLI](../../dev-tools/winapp-cli/index.md) (`winget install Microsoft.winappcli --source winget`).

### GitHub Copilot CLI

**Requires:** [GitHub Copilot CLI](https://gh.io/copilot-cli) (`winget install GitHub.Copilot`).

```powershell
gh copilot plugin install winui@awesome-copilot
```

This installs the plugin user-globally to `~\.copilot\installed-plugins\`. Verify with:

```powershell
gh copilot plugin list
```

### Claude Code

Claude Code uses its own plugin registry:

```powershell
claude plugin marketplace add microsoft/win-dev-skills
claude plugin install winui@win-dev-skills
```

## Use with GitHub Copilot CLI

The GitHub Copilot CLI runs in your terminal. Use the `@winui-dev` agent by including it in your prompt:

```powershell
gh copilot -p "@winui-dev Build me a WinUI 3 app that shows a list of files in a folder"
```

For an interactive session where you can ask follow-up questions:

```powershell
copilot -i
```

Then type your requests directly, for example: *@winui-dev Add a search box to my file list app*.

To set up your machine for WinUI 3 development, run the `winui-setup` skill first:

```powershell
gh copilot -p "/winui-setup"
```

## Use with Claude Code

After installing the plugin, use the `@winui-dev` agent in Claude Code's chat interface the same way — prefix your request with `@winui-dev`.

## The winui-dev agent

The `winui-dev` agent orchestrates the full development loop. It knows how to drive each stage, recognize common failure patterns that get generic agents stuck in loops, and steer toward successful WinUI 3 patterns.

The agent loads `winui-design` and `winui-dev-workflow` by default, which covers most "build me a WinUI 3 app" requests end-to-end. It pulls in the other skills as needed based on your request.

## The 8 skills

The plugin includes 8 specialized skills. The `winui-dev` agent selects the appropriate skill automatically based on your request.

| Skill | What it does |
|---|---|
| **winui-setup** | Installs and verifies machine prerequisites — .NET SDK, WinApp CLI, WinUI 3 templates, Developer Mode. Run explicitly with `/winui-setup`; the agent won't load it automatically |
| **winui-dev-workflow** | Guides the scaffold → build → run → iterate loop |
| **winui-design** | Generates XAML layouts using WinUI 3 controls and Fluent Design. Includes a grounded control lookup tool against the WinUI Gallery and Community Toolkit catalogue |
| **winui-code-review** | Reviews your code for WinUI 3 correctness and anti-patterns |
| **winui-ui-testing** | Generates UI tests using Windows UI Automation |
| **winui-packaging** | Guides MSIX packaging, signing, and Store submission |
| **winui-wpf-migration** | Migrates WPF code to WinUI 3 with API-level mappings |
| **winui-session-report** | Summarizes what was built in a session and suggests next steps |

## Browse and discover more plugins

You can browse and install Copilot plugins directly from VS Code using the [Awesome Copilot extension](https://marketplace.visualstudio.com/items?itemName=TimHeuer.awesome-copilot), which lets you preview and install resources from the community repository into your workspace.

## Related content

- [Quickstart: Build and publish a Windows app with AI](quickstart.md)
- [AI-assisted testing](testing.md) — using the `winui-ui-testing` skill
- [Migrate from WPF with AI](migrate/wpf-to-winui.md) — using the `winui-wpf-migration` skill
- [Migrate from UWP with AI](migrate/uwp-to-winui.md)
- [Microsoft Learn MCP Server](vs-code-tools.md#microsoft-learn-mcp-server) — give your agent live docs access
