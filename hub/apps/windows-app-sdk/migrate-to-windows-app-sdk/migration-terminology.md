---
title: Migration terminology - upgrade, migrate, port, modernize
description: Canonical definitions for the terms upgrade, migrate, port, and modernize as they apply to Windows desktop app development.
ms.topic: concept-article
ms.date: 07/09/2026
author: GrantMeStrength
ms.author: jken
---

# Migration terminology for Windows desktop apps

The terms *upgrade*, *migrate*, *port*, and *modernize* describe different activities when you move a Windows desktop app to a newer platform. This page defines each term so you can find the right guidance for your scenario.

## Definitions

| Term | Definition | Example |
|---|---|---|
| **Upgrade** | Move an existing project to a newer version of the same platform without changing the app model or UI framework. | WPF on .NET Framework 4.8 → WPF on .NET 8. The project file format and NuGet dependencies change, but the UI framework remains WPF. |
| **Migrate** | Move an app from one UI framework or app model to another, typically rewriting portions of the UI layer. | WPF → WinUI 3, or UWP → WinUI 3. Namespaces, controls, and app lifecycle code change significantly. |
| **Port** | Adapt source code so it compiles and runs on a different runtime or operating system. Porting is often a subset of upgrading or migrating. | Porting a .NET Framework class library to .NET Standard so it can run on .NET 8. |
| **Modernize** | Add modern platform features to an existing app without changing its UI framework. The app stays on WPF, WinForms, or Win32 and gains new capabilities incrementally. | Adding WinRT APIs, Windows App SDK features, or MSIX packaging to an existing WPF app. |

> [!TIP]
> You can combine these activities. For example, you might **upgrade** a WPF app from .NET Framework to .NET 8, then **modernize** it by adding Windows App SDK features, and later **migrate** specific views to WinUI 3.

## How the terms map to documentation

| What you want to do | Term | Where to start |
|---|---|---|
| Move a WPF or WinForms app from .NET Framework to .NET 8+ | Upgrade | [.NET Upgrade Assistant](/dotnet/core/porting/upgrade-assistant-overview) |
| Move a UWP app to Windows App SDK and WinUI 3 | Migrate | [Migrate from UWP to the Windows App SDK](migrate-to-windows-app-sdk-ovw.md) |
| Rewrite a desktop app's UI in WinUI 3 | Migrate | [Migrate WPF app patterns to WinUI 3](wpf-patterns-winui3.md) |
| Add Windows features to an existing WPF, WinForms, or Win32 app | Modernize | [Modernize your desktop apps](../../desktop/modernize/index.md) |
| Make a .NET Framework library available on .NET 8 | Port | [.NET porting overview](/dotnet/core/porting/) |
| Decide which approach is right for your app | — | [Choose your migration path](migration-decision-guide.md) |

## See also

- [Choose your migration path](migration-decision-guide.md)
- [Migrate from UWP to the Windows App SDK](migrate-to-windows-app-sdk-ovw.md)
- [Modernize your desktop apps](../../desktop/modernize/index.md)
