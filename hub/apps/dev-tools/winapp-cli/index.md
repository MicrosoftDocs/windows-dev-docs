---
title: Windows App Development CLI (winapp CLI)
description: The Windows App Development CLI (winapp CLI) is a command-line interface for managing Windows SDKs, packaging, generating app identity, manifests, certificates, and using build tools with any app framework.
ms.date: 02/20/2026
ms.topic: overview
---

# Windows App Development CLI (winapp CLI)

> [!IMPORTANT]
> The Windows App Development CLI is currently in **public preview**. Features and commands may change before the final release. Share your feedback by [creating an issue](https://github.com/microsoft/WinAppCli/issues).

The Windows App Development CLI (winapp CLI) is a single command-line interface for managing Windows SDKs, packaging, generating app identity, manifests, certificates, and using build tools with any app framework. This tool bridges the gap between cross-platform development and Windows-native capabilities.

Whether you're building with .NET/Win32, CMake, Electron, or Rust, this CLI gives you access to:

- **Modern Windows APIs** - [Windows App SDK](/windows/apps/windows-app-sdk/) and Windows SDK with automatic setup and code generation
- **Package Identity** - Debug and test by adding package identity without full packaging
- **MSIX Packaging** - App packaging with signing and Store readiness
- **Developer Tools** - Manifests, certificates, assets, and build integration

## Why package identity?

Many powerful Windows APIs require your app to have package identity, enabling you to leverage OS components Windows offers that you wouldn't otherwise have access to. With identity, your app gains access to features like notifications, OS integration, and on-device AI.

Examples of what package identity and MSIX packaging unlocks:

- [Interactive native notifications](/windows/apps/develop/notifications/app-notifications/app-notifications-quickstart?tabs=cs) and notification management
- [Integration with Windows Explorer, Taskbar, Share sheet](/windows/apps/develop/windows-integration/integrate-sharesheet-packaged), and other shell surfaces
- [Protocol handlers](/windows/apps/desktop/modernize/desktop-to-uwp-extensions#start-your-application-in-different-ways) (`yourapp://` URIs)
- [On-device AI](/windows/ai/apis/) (local LLM, text and image AI APIs)
- [Background tasks](/windows/uwp/launch-resume/declare-background-tasks-in-the-application-manifest) (run when app is closed)
- [File type associations](/windows/apps/desktop/modernize/desktop-to-uwp-extensions#integrate-with-file-explorer)
- [Clean install/uninstall and auto-updates](/windows/msix/overview)

## Installation

### WinGet

The easiest way to install the CLI is via WinGet (Windows Package Manager):

```powershell
winget install Microsoft.winappcli --source winget
```

### NPM

For Electron projects, install via NPM:

```powershell
npm install @microsoft/winappcli --save-dev
```

### GitHub Actions / Azure DevOps

For CI/CD pipelines, use the [`setup-WinAppCli`](https://github.com/microsoft/setup-WinAppCli) action to automatically install the CLI on your runners/agents.

### Manual download

Download the latest build from [GitHub Releases](https://github.com/microsoft/WinAppCli/releases/latest).

## Verify installation

Once installed, verify the installation by calling the CLI:

```bash
winapp --help
```

Or if using Electron/Node.js:

```bash
npx winapp --help
```

## Supported frameworks

winapp CLI works with a variety of app frameworks:

| Framework | Guide |
|-----------|-------|
| .NET / WPF / WinForms | [Get started with .NET](guides/dotnet.md) |
| C++ (CMake) | [Get started with C++](guides/cpp.md) |
| Electron | [Get started with Electron](guides/electron-setup.md) |
| Rust | [Get started with Rust](guides/rust.md) |
| Tauri | [Get started with Tauri](guides/tauri.md) |
| Flutter | [Get started with Flutter](guides/flutter.md) |

## Commands overview

| Category | Commands |
|----------|----------|
| **Setup** | [init](usage.md#init), [restore](usage.md#restore), [update](usage.md#update) |
| **Packaging** | [pack](usage.md#pack), [create-debug-identity](usage.md#create-debug-identity) |
| **Manifests** | [manifest generate](usage.md#manifest-generate), [manifest update-assets](usage.md#manifest-update-assets) |
| **Certificates & Signing** | [cert generate](usage.md#cert-generate), [cert install](usage.md#cert-install), [sign](usage.md#sign) |
| **Utilities** | [tool](usage.md#tool), [store](usage.md#store), [get-winapp-path](usage.md#get-winapp-path) |
| **Node.js/Electron** | [node create-addon](usage.md#node-create-addon), [node add-electron-debug-identity](usage.md#node-add-electron-debug-identity), [node clear-electron-debug-identity](usage.md#node-clear-electron-debug-identity) |

For the full CLI reference, see [CLI reference](usage.md).

## Open source

winapp CLI is open source. You can find the source code, file issues, and contribute on [GitHub](https://github.com/microsoft/WinAppCli).

## Related topics

- [CLI reference](usage.md)
- [Framework guides](guides/index.md)
- [Windows App SDK documentation](/windows/apps/windows-app-sdk/)
- [MSIX packaging documentation](/windows/msix/)
