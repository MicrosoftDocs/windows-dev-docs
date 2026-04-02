---
title: AI-assisted Windows development
description: Discover how to build better Windows apps faster — using AI coding tools and shipping apps with AI built in, from Foundry Local to Phi Silica and Windows AI APIs.
ms.topic: overview
ms.date: 03/10/2026
ms.author: jken
author: GrantMeStrength
keywords: windows, github copilot, ai development, winui, copilot, mcp server, foundry local, phi silica, windows ai
ms.localizationpriority: medium
---

# AI-assisted Windows development

Windows is where AI development is happening — both for developers writing apps with AI assistance and for apps that ship with AI built in.

This article covers both: the AI coding tools that help you build Windows apps faster, and the Windows AI stack that lets you put intelligence directly into your app. When you're ready, follow the links to set up your environment and start building.

> [!TIP]
> **New to Windows development?** Windows has the deepest local AI stack of any platform: [Foundry Local](/windows/ai/foundry-local/get-started) runs state-of-the-art models on any hardware, [Phi Silica](/windows/ai/apis/phi-silica) uses the NPU on Copilot+ PCs for near-instant inference, and the full [Windows AI API surface](/windows/ai/) is available to any packaged app. If you're coming from Linux or macOS, Windows Subsystem for Linux (WSL) and the GitHub Copilot CLI Terminal mean you don't have to give up your existing workflow to get started.

## Two ways AI changes Windows development

There are two distinct—and complementary—things AI does for Windows developers:

| | What it is | Example |
|---|---|---|
| **AI-assisted development** | Tools that help you *write* your app faster and more accurately | GitHub Copilot generates your WinUI scaffolding; the Learn MCP Server looks up the right API |
| **AI in your app** | AI features you *ship* inside your app for end users | Note summarization via Foundry Local; real-time transcription via Live Captions API; image description via Windows Vision Skills |

Both are first-class scenarios on Windows. The rest of this article covers the tools for each.

---

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

## Build apps with AI built in

The Windows AI stack lets you ship AI features directly in your app — with hardware-accelerated inference, privacy-preserving on-device models, and no cloud dependency required.

### Foundry Local

[Foundry Local](/windows/ai/foundry-local/get-started) runs large language models locally on any Windows PC. It exposes an OpenAI-compatible REST API, so you can use your existing AI code against local models with no rewrite. Foundry Local is the recommended starting point for adding AI to a Windows app — it works on any hardware, requires no Azure subscription, and keeps user data on-device.

```bash
winget install Microsoft.AIFoundry.Local
foundry model run phi-4-mini
```

After the model starts, call it from your app using the OpenAI-compatible endpoint at `http://localhost:5272/openai/v1`.

### Phi Silica

[Phi Silica](/windows/ai/apis/phi-silica) is a compact, highly capable model built into Windows 11 on Copilot+ PCs. It runs entirely on the NPU — no GPU, no cloud, near-instant inference. If your app targets Copilot+ PCs, Phi Silica is the fastest local AI option available.

> [!NOTE]
> Phi Silica requires a Copilot+ PC (with NPU, 40+ TOPS). For apps targeting all Windows hardware, use Foundry Local with a fallback to cloud APIs.

### Windows AI APIs

Beyond language models, Windows exposes a rich set of AI-powered APIs that any packaged app can use:

- **Text recognition** — [Windows.Media.Ocr](/uwp/api/windows.media.ocr) for on-device OCR
- **Live Captions API** — real-time, on-device speech-to-text
- **Image analysis** — vision features via Windows Vision Skills
- **LoRA fine-tuning** — adapt Phi Silica to your domain with [LoRA support](/windows/ai/apis/phi-silica-lora)

All of these run on-device, require no cloud subscription, and become available to your app once you have package identity (which [winapp CLI](../dev-tools/winapp-cli/index.md) can add to any framework).

---

## Next steps

> [!div class="nextstepaction"]
> [Set up GitHub Copilot for Windows development](ai-setup.md)

Or jump straight to:

- [Tutorial: Build a Windows app with GitHub Copilot](ai-build.md)
- [Get started with Foundry Local](/windows/ai/foundry-local/get-started)
- [Modernize or port a Windows app with Copilot](../windows-app-sdk/migrate-to-windows-app-sdk/ai-modernize.md)
- [Agentic AI tools for Windows development](../dev-tools/agentic-tools.md)
- [Windows AI APIs overview](/windows/ai/)
