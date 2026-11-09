---
title: WinApp CLI framework guides
description: Step-by-step guides for using the WinApp CLI with WinUI, .NET, .NET MAUI, C++, Electron, Rust, Tauri, Flutter, and other frameworks.
ms.date: 11/07/2026
ms.topic: overview
---

# Framework guides

These guides walk you through using the WinApp CLI with your app framework — from project setup to debugging with package identity to packaging as MSIX.

| Framework | Guide |
|-----------|-------|
| WinUI | [Get started with WinUI](winui.md) |
| .NET / WPF / WinForms | [Get started with .NET](dotnet.md) |
| .NET MAUI | [Get started with .NET MAUI](maui.md) |
| C++ (CMake) | [Get started with C++](cpp.md) |
| Electron | [Get started with Electron](electron-index.md) |
| Rust | [Get started with Rust](rust.md) |
| Tauri | [Get started with Tauri](tauri.md) |
| Flutter | [Get started with Flutter](flutter.md) |

Additional guides:
- [Packaging an EXE/CLI](packaging-cli.md): step-by-step guide for packaging an existing EXE/CLI as MSIX
- [.NET file-based apps](dotnet-file-based.md): run a single `.cs` file with package identity, no project file or manifest required
- [Sparse packaging](sparse.md): give an unpackaged app package identity with an identity-only (sparse) MSIX and external content
- [Shell Completion](shell-completion.md): enable tab completion for commands, options, and values in PowerShell, bash, zsh, and fish
- [DevTools](https://github.com/microsoft/WinAppCli/blob/main/docs/guides/devtools.md): inspect a running WinUI 3 app, try live property changes, and leave comments for an agent ([advanced](https://github.com/microsoft/WinAppCli/blob/main/docs/guides/devtools-advanced.md))
- [Security guidance](../security.md): what development certificates and Developer Mode change on your machine, how to handle `devcert.pfx`, and how to sign for production

## Electron deep-dive guides

After completing the [Electron setup guide](electron-setup.md):

| Guide | Description |
|-------|-------------|
| [Package for distribution](electron-packaging.md) | Create an MSIX package for your Electron app |
| [Phi Silica addon](electron-phi-silica-addon.md) | On-device AI with the Phi Silica model |
| [WinML addon](electron-winml-addon.md) | Machine learning inference with Windows ML |
| [C++ notification addon](electron-cpp-notification-addon.md) | Native Windows notifications from Electron |