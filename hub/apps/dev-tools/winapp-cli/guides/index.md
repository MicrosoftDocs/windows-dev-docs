---
title: Windows App Development CLI framework guides
description: Step-by-step guides for using the Windows App Development CLI with various app frameworks including .NET, C++, Electron, Flutter, Rust, and Tauri.
ms.date: 02/20/2026
ms.topic: overview
---

# Windows App Development CLI framework guides

These guides walk you through using the [winapp CLI](../index.md) with your app framework. Each guide covers initializing your project, debugging with package identity, using Windows App SDK APIs, and packaging as MSIX.

## Choose your framework

| Framework | Description |
|-----------|-------------|
| [.NET / WPF / WinForms](dotnet.md) | Use winapp CLI with .NET projects, including console, WPF, and WinForms apps |
| [C++ (CMake)](cpp.md) | Use winapp CLI with C++ applications built with CMake |
| [Electron](electron-setup.md) | Add Windows-native capabilities to Electron apps, including native addons for AI and notifications |
| [Flutter](flutter.md) | Use winapp CLI with Flutter desktop applications |
| [Rust](rust.md) | Use winapp CLI with Rust applications |
| [Tauri](tauri.md) | Use winapp CLI with Tauri cross-platform applications |

## Additional guides

| Guide | Description |
|-------|-------------|
| [Packaging a CLI executable](packaging-cli.md) | Package an existing command-line executable as MSIX for distribution |

## Electron deep-dive guides

The Electron guides are organized into a series covering setup, native addon creation, and packaging:

1. **[Setup](electron-setup.md)** - Install tools, initialize SDKs, configure build pipeline
2. **[C++ notification addon](electron-cpp-notification-addon.md)** - Create a C++ addon that calls Windows notification APIs
3. **[Phi Silica addon](electron-phi-silica-addon.md)** - Create a C# addon for on-device AI text summarization
4. **[WinML addon](electron-winml-addon.md)** - Create a C# addon for machine learning with ONNX models
5. **[Packaging](electron-packaging.md)** - Create a signed MSIX package for distribution

## Related topics

- [winapp CLI overview](../index.md)
- [CLI reference](../usage.md)
