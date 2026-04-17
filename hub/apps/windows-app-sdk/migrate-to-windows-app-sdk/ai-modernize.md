---
title: Modernize or port a Windows app with GitHub Copilot
description: Use GitHub Copilot, the WinUI 3 migration plugin, and winapp CLI to migrate UWP apps to WinUI 3, modernize WPF/WinForms apps, and add Windows features to cross-platform apps.
ms.topic: how-to
ms.date: 03/10/2026
ms.author: jken
author: GrantMeStrength
keywords: windows, github copilot, uwp migration, winui 3, wpf, winforms, modernization, winapp cli
ms.localizationpriority: medium
---

# Modernize or port a Windows app with GitHub Copilot

If you have an existing Windows app — a UWP project, a WPF or WinForms desktop app, or a cross-platform Electron or Flutter app — GitHub Copilot can significantly speed up the process of updating it for modern Windows. This article shows the key scenarios and the tools that make them practical.

## Before you start

Make sure you have the key tools configured:

- **GitHub Copilot** with the **WinUI 3 development plugin** — handles API migration guidance and generates correct modern code
- **Microsoft Learn MCP Server** — gives Copilot access to current Windows App SDK documentation
- **winapp CLI** — adds package identity and Windows packaging to any framework

If you haven't set these up yet, start with [Set up GitHub Copilot for Windows development](../../how-tos/ai-setup.md).

---

## Migrate a UWP app to WinUI 3

WinUI 3 is the modern successor to UWP's UI layer. The APIs are similar but not identical — namespaces have moved, some controls have changed, and the app model is different. The **WinUI 3 migration guide skill** makes this tractable.

### Run the migration guide

In Copilot Chat, run the migration guide skill:

```
/winui3-development:winui3-migration-guide
```

This produces a structured migration plan tailored to your project, including:

- A checklist of project file and manifest changes
- A table of API namespace mappings (e.g. `Windows.UI.Xaml` → `Microsoft.UI.Xaml`)
- Common control substitutions and before/after code samples
- Packaging and deployment differences

### Migrate a file

Open a UWP source file and ask Copilot to migrate it:

> *"Migrate this file from UWP to WinUI 3. Replace all deprecated UWP APIs with their WinUI 3 equivalents. Flag anything that needs manual review."*

The WinUI 3 plugin's custom instructions guide Copilot to make correct substitutions. Common ones include:

| UWP | WinUI 3 |
|---|---|
| `Windows.UI.Xaml.*` | `Microsoft.UI.Xaml.*` |
| `CoreDispatcher` / `RunAsync` | `DispatcherQueue` / `TryEnqueue` |
| `MessageDialog` | `ContentDialog` (with `XamlRoot`) |
| `Windows.UI.Popups` | `Microsoft.UI.Xaml.Controls` |
| `BackgroundTaskBuilder` | Windows App SDK background task APIs |

### What Copilot can and can't do automatically

Copilot handles most namespace remapping and straightforward API substitutions well. There are areas that need human review:

- **App model changes** — UWP lifecycle events (`Suspending`, `Resuming`) don't map directly to WinUI 3; Copilot can suggest alternatives but you'll need to verify behavior
- **XAML resource dictionaries** — theme resources and custom styles may need manual adjustment
- **Background tasks** — the packaging model is different; use winapp CLI to set up the new manifest entries

---

## Modernize a WPF or WinForms app

WPF and WinForms apps can adopt Windows App SDK features without a full rewrite. Copilot + the Learn MCP Server makes it easy to find and integrate specific features.

### Add push notifications

> *"I have a WPF app targeting .NET 8. Show me how to add Windows push notifications using the Windows App SDK. The app is not currently packaged."*

With the Learn MCP Server, Copilot fetches the current Windows App SDK notification documentation and generates integration code. It will also note that notifications require package identity — and suggest using winapp CLI to add it:

```bash
winapp init
winapp create-debug-identity --publisher "CN=MyApp"
```

### Modernize the app's look and feel

> *"My WPF app looks dated. Make it look modern with a dark mode option and a navigation sidebar like modern Windows apps use."*

### Add a file picker using Windows App SDK

> *"Replace my WPF OpenFileDialog usage with the Windows App SDK StorageFilePicker for a better modern Windows experience."*

### Add Windows notifications

> *"Add a notification that tells the user when a background task completes."*

Copilot will recognize this requires Windows package identity and suggest using winapp CLI to add MSIX packaging — explaining what it is and why it's needed. Once packaging is set up, it will provide the Windows notification code.

Ask Copilot to walk you through it:

> *"I want my app to show a Windows toast notification. Explain what I need to set up first."*

---

## Add Windows features to a cross-platform app

Electron, Flutter, React Native, and Rust apps can all become first-class Windows citizens with [winapp CLI](../../dev-tools/winapp-cli/index.md). Copilot helps you adapt your code for Windows APIs once you have package identity.

### Electron

```bash
npm install @microsoft/winappcli --save-dev
npx winapp init
npx winapp node create-addon --feature notifications
```

Once the addon is scaffolded, ask Copilot to integrate it:

> *"I've scaffolded a Windows notifications addon for my Electron app using winapp CLI. Show me how to send a notification from the main process when a download completes."*

For a complete walkthrough, see the [Electron setup guide](../../dev-tools/winapp-cli/guides/electron-setup.md).

### Flutter

```bash
winapp init
```

> *"I have a Flutter app for Windows. Show me how to use the Windows App SDK to add a system tray icon with a context menu."*

See the [Flutter guide](../../dev-tools/winapp-cli/guides/flutter.md) for Windows-specific setup.

### WPF / .NET

> *"I have a WPF app. Walk me through using winapp CLI to add MSIX packaging and then add an on-device AI feature using the Windows AI APIs."*

See the [.NET guide](../../dev-tools/winapp-cli/guides/dotnet.md).

---

## What gets unlocked by package identity

Many powerful Windows features require [package identity](../../dev-tools/winapp-cli/index.md#why-package-identity) — which winapp CLI can add to any framework. Once you have it, Copilot can help you use:

| Feature | What to ask Copilot |
|---|---|
| App notifications | *"Add a Windows notification when my background job completes"* |
| On-device AI (Phi Silica, text recognition) | *"Add local AI text summarization using Windows AI APIs"* |
| Windows shell integration (share sheet, taskbar) | *"Add a share target so users can share to my app from Explorer"* |
| Background tasks | *"Run a background task every hour even when my app is closed"* |
| File type associations | *"Register my app to open .notes files"* |

---

## Next steps

- [Agentic AI tools for Windows development](../../dev-tools/agentic-tools.md) — full details on the WinUI 3 plugin and Learn MCP Server
- [Windows App Development CLI (winapp CLI)](../../dev-tools/winapp-cli/index.md) — full reference for adding Windows features to any framework
- [Windows App SDK documentation](../index.md)
- [Windows AI APIs](/windows/ai/) — on-device AI features available once you have package identity
