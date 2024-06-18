---
description: How to run the Microsoft Store Developer CLI (preview) Publish command.
title: Microsoft Store Developer CLI (preview) - Publish Command
ms.date: 12/02/2022
ms.topic: article
ms.localizationpriority: medium
---

# Publish Command

Publishes your Application to the Microsoft Store.

## Usage Examples

### Windows App SDK/WinUI 3

```console
msstore publish "C:\path\to\winui3_app"
```

### UWP

```console
msstore publish "C:\path\to\uwp_app"
```

### .NET MAUI

```console
msstore publish "C:\path\to\maui_app"
```

### Flutter

```console
msstore publish "C:\path\to\flutter_app"
```

### Electron

```console
msstore publish "C:\path\to\electron_app"
```

### React Native for Windows

```console
msstore publish "C:\path\to\react_native_app"
```

### PWA

```console
msstore publish "C:\path\to\pwa_app"
```

## Arguments

| Option      | Description |
|-------------|-------------|
| `pathOrUrl` | The root directory path where the project file is, or a public URL that points to a PWA. |

## Options

| Option               | Description |
|----------------------|-------------|
| -i, --inputDirectory | The directory where the '.msix' or '.msixupload' file to be used for the publishing command. If not provided, the cli will try to find the best candidate based on the 'pathOrUrl' argument. |
| -id, --appId | Specifies the Application Id. Only needed if the project has not been initialized before with the 'init' command. |
| -nc, --noCommit | Disables committing the submission, keeping it in draft state. |