---
description: How to run the Microsoft Store Developer CLI (preview) package command.
title: Microsoft Store Developer CLI (preview) - Package Command
ms.date: 12/02/2022
ms.topic: article
ms.localizationpriority: medium
---

# Package Command

Helps you package your Microsoft Store Application as an MSIX.

## Usage Examples

### Windows App SDK/WinUI 3

```console
msstore package "C:\path\to\winui3_app"
```

### UWP

```console
msstore package "C:\path\to\uwp_app"
```

### .NET MAUI

```console
msstore package "C:\path\to\maui_app"
```

### Flutter

```console
msstore package "C:\path\to\flutter_app"
```

### Electron

```console
msstore package "C:\path\to\electron_app"
```

### React Native for Windows

```console
msstore package "C:\path\to\react_native_app"
```

### PWA

```console
msstore package "C:\path\to\pwa_app"
```

## Arguments

| Option      | Description |
|-------------|-------------|
| `pathOrUrl` | The root directory path where the project file is, or a public URL that points to a PWA. |

## Options

| Option | Description |
|--------|-------------|
| -o, --output | The output directory where the packaged app will be stored. If not provided, the default directory for each different type of app will be used. |
| -a, --arch | The architecture(s) to build for. If not provided, the default architecture for the current OS, and project type, will be used. Allowed values: "x86", "x64", "arm64". |
| -ver, --version | The version used when building the app. If not provided, the version from the project file will be used. |