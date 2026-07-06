---
title: Get started with WinUI
description: Choose your development path to build Windows apps with WinUI 3 and the Windows App SDK — using Visual Studio or the command line.
ms.topic: overview
ms.date: 07/06/2026
author: GrantMeStrength
ms.author: jken
---

# Get started with WinUI

WinUI 3 is the native UI framework for building Windows desktop apps. It is part of the Windows App SDK — a set of APIs and tools that are separate from the Windows operating system itself and must be installed on the target machine or packaged alongside your app. WinUI 3 supports both C# and C++ and is not a cross-platform or web-based framework.

> [!IMPORTANT]
> WinUI 3 is distinct from UWP. WinUI 3 uses the `Microsoft.UI.Xaml` namespace and runs as a desktop process; UWP uses `Windows.UI.Xaml` and runs in an app container. WinUI 3 supports packaged (MSIX), packaged with external location, and unpackaged distribution — the right choice depends on your deployment scenario. See [Choose a distribution method](../package-and-deploy/choose-distribution-path.md) for guidance.

## Choose your path

Two supported paths exist for creating a WinUI 3 project.

| | Visual Studio | Command line |
|---|---|---|
| **Best for** | Developers new to Windows development, visual UI designers, or anyone who wants a full IDE experience | Developers comfortable with the terminal, CI/CD pipelines, or scripted environment setup |
| **Tooling** | Visual Studio 2026 with the Windows App SDK and WinUI workloads installed | .NET 10 SDK with `dotnet new` WinUI templates |
| **Project creation** | New Project wizard — select the **Blank App, Packaged (WinUI 3 in Desktop)** template | `dotnet new winui -n MyApp` |
| **Build and run** | Press F5 — Visual Studio builds, signs, deploys the MSIX package, and launches the app | `dotnet run` — builds and launches the app with package identity (requires Developer Mode) |
| **Debugging** | Full Visual Studio debugger with XAML live preview | Attach a debugger manually or open the generated `.sln` in Visual Studio |

Both paths produce equivalent WinUI 3 projects. The Visual Studio path handles MSIX signing and deployment automatically. The command-line path uses `dotnet run` for local development (the included `Microsoft.Windows.SDK.BuildTools.WinApp` package handles debug identity automatically); packaging for distribution requires additional steps.

## Prerequisites

Both paths share the same minimum requirements.

- **Windows 10 version 1809** (build 17763) or later — Windows 11 is recommended
- **Developer Mode** enabled on your device (`ms-settings:developers`)
- **Visual Studio 2026** (Visual Studio path) or **.NET 10 SDK** (command-line path)

The Windows App SDK is a separate install from Windows. Visual Studio installs it as part of the workload setup. For the command-line path, the `dotnet new winui` templates bring in the required NuGet packages automatically.

## Next steps

> [!div class="nextstepaction"]
> [Quick start: Visual Studio](start-here.md)

> [!div class="nextstepaction"]
> [Quick start: Command line](start-here.md#tab/command-line)
