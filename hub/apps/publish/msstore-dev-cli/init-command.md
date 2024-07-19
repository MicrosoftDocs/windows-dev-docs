---
description: How to run the Microsoft Store Developer CLI (preview) init command.
title: Microsoft Store Developer CLI (preview) - Init Command
ms.date: 12/02/2022
ms.topic: article
ms.localizationpriority: medium
---

# Init Command

Helps you setup your application to publish to the Microsoft Store.

## Description

The `init` command helps you setup your application to publish to the Microsoft Store. It currently supports the following application types:
- Windows App SDK/WinUI 3
- UWP
- .NET MAUI
- Flutter
- Electron
- React Native for Windows
- PWA

## Usage Examples

### Windows App SDK/WinUI 3

```console
msstore init "C:\path\to\winui3_app"
```

### UWP

```console
msstore init "C:\path\to\uwp_app"
```

### .NET MAUI

```console
msstore init "C:\path\to\maui_app"
```

### Flutter

```console
msstore init "C:\path\to\flutter_app"
```

### Electron

```console
msstore init "C:\path\to\electron_app"
```

### React Native for Windows

```console
msstore init "C:\path\to\react_native_app"
```

> [!Note]
> For Electron, as well as React Native for Windows projects, both `Npm` and `Yarn` are supported. The presence of the `Yarn` lock file (`yarn.lock`) will be used to determine which package manager to use, so make sure that you check in your lock file into your source control system.

### PWA

```console
msstore init https://contoso.com --output .
```

## Arguments

| Argument    | Description |
|-------------|-------------|
| `pathOrUrl` | The root directory path where the project file is, or a public URL that points to a PWA. |

## Options

| Option | Description |
|--------|-------------|
| -n, --publisherDisplayName | The Publisher Display Name used to configure the application. If provided, avoids an extra APIs call. |
| --package | If supported by the app type, automatically packs the project. |
| --publish | If supported by the app type, automatically publishes the project. Implies '--package true' |
| -f, --flightId | Specifies the Flight Id where the package will be published. |
| -prp, --packageRolloutPercentage | Specifies the rollout percentage of the package. The value must be between 0 and 100. |
| -a, --arch | The architecture(s) to build for. If not provided, the default architecture for the current OS, and project type, will be used. Allowed values: "x86", "x64", "arm64". Only used it used in conjunction with '--package true'. |
| -o, --output | The output directory where the packaged app will be stored. If not provided, the default directory for each different type of app will be used. |
| -ver, --version | The version used when building the app. If not provided, the version from the project file will be used. |