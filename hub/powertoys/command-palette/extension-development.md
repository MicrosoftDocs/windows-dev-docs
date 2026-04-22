---
title: Command Palette extension development
description: Build extensions for PowerToys Command Palette and bring your tools, workflows, and ideas to millions of Windows power users.
ms.date: 04/10/2026
ms.topic: concept-article
no-loc: [PowerToys, Windows, Insider]
# Customer intent: Understand the value proposition of building Command Palette extensions and find where to get started.
---

# Welcome to Command Palette extension development

Command Palette is the go-to launcher for Windows power users — a fast, extensible interface for launching apps, running commands, and getting things done. With the Command Palette extension platform, you can build tools that plug directly into that workflow — putting your ideas right where people are already working.

:::image type="content" source="../images/command-palette/gallery.png" alt-text="A screenshot of the Command Palette Extension Gallery showing a list of available extensions.":::

Whether you want to build something for yourself, share it with the community, or distribute it to your team, the platform is designed to get you from idea to working extension quickly.

## Why build for Command Palette?

- **Reach users where they are.** Your extension shows up right in the Command Palette — no separate app to open, no context switching.
- **Built on .NET.** Use C# and the tools you already know. Leverage the full NuGet ecosystem to build what you imagine.
- **Rich UI without the overhead.** Use built-in page types — lists, forms, markdown, detail views, and grids — to create polished experiences without designing UI from scratch.
- **Scaffold and go.** Run the `Create extension` command directly from Command Palette to generate a ready-to-build project in seconds.
- **Share with the community.** Publish your extension to the built-in Gallery and make it available to every Command Palette user.

## How to build and publish an extension

Here's the high-level process for creating and sharing a Command Palette extension:

1. **[Understand how it works](extensibility-overview.md)** — Learn how the extension model works, including page types, commands, and how extensions communicate with Command Palette.
2. **[Build your extension](creating-an-extension.md)** — Set up your project, add commands and pages, and test your extension locally.
3. **[Publish your extension](publish-extension.md)** — Distribute your extension through the Microsoft Store, WinGet, or your own hosting.

## What's in the docs

- **[How it works](extensibility-overview.md)** — Understand the extension architecture, page types, and core concepts.
- **[Getting started](creating-an-extension.md)** — Step-by-step guide to building your first extension.
- **[Building your extension](adding-commands.md)** — Deep dives into commands, lists, forms, markdown, and Dock support.
- **[Publishing your extension](publish-extension.md)** — Learn how to package and publish to the Gallery.
- **[Extension samples](samples.md)** — Open-source examples to learn from and build on.
- **[API reference](sdk-namespaces.md)** — Detailed API reference for the Command Palette SDK.

Now, let's build. 💪
