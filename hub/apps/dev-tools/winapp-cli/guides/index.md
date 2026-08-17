---
title: winapp CLI Framework Guides
description: Step-by-step guides for using the winapp CLI with .NET, C++, Electron, Rust, Tauri, Flutter, and other frameworks to add package identity and MSIX packaging.
ms.date: 08/14/2026
ms.topic: article
---

# Framework guides for the winapp CLI

These guides walk you through using the winapp CLI with your app framework — from project setup to debugging with package identity to packaging as MSIX.

## Get started by framework

| Framework | Guide |
|-----------|-------|
| .NET / WPF / WinForms | [Get started with .NET](dotnet.md) |
| C++ (CMake) | [Get started with C++](cpp.md) |
| Electron | [Get started with Electron](electron-index.md) |
| Rust | [Get started with Rust](rust.md) |
| Tauri | [Get started with Tauri](tauri.md) |
| Flutter | [Get started with Flutter](flutter.md) |

## Additional guides

- [Packaging an EXE/CLI](packaging-cli.md): step-by-step guide for packaging an existing EXE/CLI as MSIX
- [Sparse packaging](sparse.md): give an unpackaged app package identity with an identity-only (sparse) MSIX and external content
- [Shell completion](shell-completion.md): enable tab completion for commands, options, and values in PowerShell, bash, zsh, and fish

## Electron deep-dive guides

After completing the [Electron setup guide](electron-setup.md):

| Guide | Description |
|-------|-------------|
| [Package for distribution](electron-packaging.md) | Create an MSIX package for your Electron app |
| [Phi Silica addon](electron-phi-silica-addon.md) | On-device AI with the Phi Silica model |
| [WinML addon](electron-winml-addon.md) | Machine learning inference with Windows ML |
| [C++ notification addon](electron-cpp-notification-addon.md) | Native Windows notifications from Electron |
