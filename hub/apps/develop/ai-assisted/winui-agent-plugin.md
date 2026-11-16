---
title: WinUI agent plugin for AI coding agents
description: Install the WinUI agent plugin to give your AI coding agent, such as GitHub Copilot, Claude Code, or Codex, accurate WinUI 3 knowledge.
ms.topic: how-to
ms.date: 11/16/2026
ms.author: jken
author: GrantMeStrength
---

# WinUI agent plugin for AI coding agents

The WinUI agent plugin (`winui`) gives your AI coding agent, such as GitHub Copilot, Claude Code, or Codex, accurate and up-to-date knowledge of WinUI 3 and the Windows App SDK. It includes 7 specialized skills and, for GitHub Copilot and Claude Code, a dedicated `winui-dev` agent that guides AI through the full development loop: scaffold, build, run, test, package, and migrate.

> [!NOTE]
> You can also use the plugin in VS Code. VS Code loads plugins that you install with GitHub Copilot CLI, and you can browse for plugins in its Agent Customizations editor. For setup steps, see [Agent plugins in VS Code](https://code.visualstudio.com/docs/agent-customization/agent-plugins).

## Why do I need this plugin?

Without the plugin, AI coding agents may suggest outdated patterns for Windows development. AI training data contains far more historical Windows samples than current WinUI 3 content, so agents can default to deprecated APIs. The plugin fixes this by injecting explicit WinUI 3 rules as custom instructions that override the agent's training data defaults.

## Install the plugin

**Requires:** The [Windows App Development CLI](../../dev-tools/winapp-cli/index.md) (`winget install Microsoft.winappcli --source winget`).

### GitHub Copilot CLI

**Requires:** [GitHub Copilot CLI](https://github.com/github/copilot-cli) (`winget install GitHub.Copilot`) and a [GitHub Copilot subscription](https://github.com/features/copilot). If you use GitHub CLI instead, start each command with `gh copilot`, for example `gh copilot plugin install winui@awesome-copilot`.

```powershell
copilot plugin install winui@awesome-copilot
```

This installs the plugin user-globally to `~\.copilot\installed-plugins\`. Verify with:

```powershell
copilot plugin list
```

### Claude Code

Claude Code installs the plugin from the WinApp CLI repository's marketplace, so the plugin ID is `winui@winappcli` instead of `winui@awesome-copilot`:

```powershell
claude plugin marketplace add microsoft/WinAppCli
claude plugin install winui@winappcli
```

### Codex

Codex also installs the plugin from the WinApp CLI repository's marketplace:

```powershell
codex plugin marketplace add microsoft/WinAppCli
codex plugin add winui@winappcli
```

### Also install the winappcli plugin

We recommend that you also install the `winappcli` plugin. It adds skills for the Windows App Development CLI, such as packaging, code signing, package identity, UI automation, and inspecting a running WinUI 3 app with DevTools:

```powershell
# GitHub Copilot CLI
copilot plugin install winappcli@awesome-copilot

# Claude Code
claude plugin install winappcli@winappcli

# Codex
codex plugin add winappcli@winappcli
```

## Use the plugin

Start your coding agent in your project folder, and then describe what you want to build. The agent uses the plugin's skills when your request needs them.

# [GitHub Copilot CLI](#tab/copilot-cli)

Start an interactive session:

```powershell
copilot
```

To work with the `winui-dev` agent, select it with the `/agent` command, or name it in your request, for example: *Use the winui-dev agent to build a WinUI 3 app that shows a list of files in a folder*.

To set up your machine for WinUI 3 development first, name the `winui-setup` skill in your request, for example: *Use the /winui-setup skill to check that my machine is ready for WinUI 3 development*.

# [Claude Code](#tab/claude-code)

Start an interactive session:

```powershell
claude
```

To work with the `winui-dev` agent, name it in your request, for example: *Use the winui-dev subagent to build a WinUI 3 app that shows a list of files in a folder*.

# [Codex](#tab/codex)

Start an interactive session:

```powershell
codex
```

Describe what you want, for example: *Build a WinUI 3 app that shows a list of files in a folder*. Codex uses the plugin's skills for your request.

---

## The winui-dev agent

In GitHub Copilot and Claude Code, the `winui-dev` agent orchestrates the full development loop. It knows how to drive each stage, recognize common failure patterns that get generic agents stuck in loops, and steer toward successful WinUI 3 patterns.

The agent loads `winui-design` and `winui-dev-workflow` by default, which covers most "build me a WinUI 3 app" requests end-to-end. It pulls in the other skills as needed based on your request.

## The 7 skills

The plugin includes 7 specialized skills. Your coding agent selects the appropriate skill automatically based on your request. Most of the skills are for WinUI 3 apps. A WPF or Windows Forms app that uses the Windows App SDK isn't a WinUI 3 app, so the design, build, and packaging skills don't apply to it unless you're migrating it to WinUI 3.

| Skill | What it does |
|---|---|
| **winui-setup** | Installs and verifies machine prerequisites: .NET SDK, WinApp CLI, and Developer Mode. Also identifies the extra requirements for Native AOT and Windows Sandbox |
| **winui-dev-workflow** | Guides the scaffold → build → run → iterate loop |
| **winui-design** | Generates XAML layouts using WinUI 3 controls and Fluent Design. Finds controls and samples from the WinUI Gallery and Windows Community Toolkit with `winapp find-ui`, and checks APIs with `winapp find-api` |
| **winui-code-review** | Reviews your code for WinUI 3 correctness and anti-patterns |
| **winui-ui-testing** | Writes and runs UI tests with `winapp ui` for WinUI 3, WPF, Windows Forms, and Win32 apps |
| **winui-packaging** | Guides MSIX packaging, signing, and Store submission |
| **winui-wpf-migration** | Migrates WPF code to WinUI 3 with API-level mappings |

## Browse and discover more plugins

You can browse and install Copilot plugins directly from VS Code using the [Awesome Copilot extension](https://marketplace.visualstudio.com/items?itemName=TimHeuer.awesome-copilot), which lets you preview and install resources from the community repository into your workspace.

## Related content

- [Quickstart: Build and publish a Windows app with AI](quickstart.md)
- [AI-assisted testing](testing.md) — using the `winui-ui-testing` skill
- [Migrate from WPF with AI](migrate/wpf-to-winui.md) — using the `winui-wpf-migration` skill
- [Migrate from UWP with AI](migrate/uwp-to-winui.md)
- [Microsoft Learn MCP Server](vs-code-tools.md#microsoft-learn-mcp-server) — give your agent live docs access
