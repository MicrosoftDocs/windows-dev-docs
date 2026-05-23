---
title: "Migrate and port apps to WinUI 3"
description: Modernize existing Windows apps by migrating from WPF, UWP, or other frameworks to WinUI 3 and the Windows App SDK using AI assistance.
ms.topic: overview
ms.date: 05/13/2026
ms.author: jken
author: GrantMeStrength
---

# Migrate and port apps to WinUI 3

WinUI 3 and the Windows App SDK are the modern foundation for Windows apps. AI tools can automate much of the mechanical migration work — namespace substitutions, API replacements, project file updates — but they need accurate guidance to avoid reproducing outdated patterns.

The main risk is drift toward older stacks. AI models have more training data for UWP and WPF than for WinUI 3, so provide exact API mappings when prompting for migration code.

## Choose your migration path

| From | To | AI skill | Guide |
|------|----|----------|-------|
| WPF (.NET) | WinUI 3 | `winui-wpf-migration` | [Migrate from WPF](wpf-to-winui.md) |
| UWP | WinUI 3 | — | [Migrate from UWP](uwp-to-winui.md) |
| iOS / SwiftUI | WinUI 3 | — | [Migrate from iOS](ios-to-winui.md) |
| React Native / Electron / MAUI / Flutter | WinUI 3 | — | [Cross-framework considerations](cross-framework.md) |

## Before you start

- Work in a branch before running large AI-assisted rewrites.
- Verify your dependencies support .NET 10 and Windows App SDK packaging.
- Review the [Windows App SDK migration guide](../../../windows-app-sdk/migrate-to-windows-app-sdk/migrate-to-windows-app-sdk-ovw.md).
- Prepare API mapping tables before prompting — the per-framework pages have them ready to paste.

## Related content

- [AI-assisted development overview](../index.md)
- [Quickstart: Build and publish a Windows app with AI](../quickstart.md)
- [WinUI agent plugin](../winui-agent-plugin.md)
