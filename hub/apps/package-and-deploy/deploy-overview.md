---
title: Windows App SDK deployment overview
description: There are two ways in which you can deploy the Windows App SDK&mdash;framework-dependent or self-contained.
ms.topic: concept-article
ms.date: 07/25/2026
ms.localizationpriority: medium
keywords: windows app sdk deployment, framework-dependent, self-contained, deploy winui 3, windows app sdk self-contained
---

# Windows App SDK deployment overview

There are two ways in which you can deploy the Windows App SDK:

* **Framework-dependent**. Your app depends on the Windows App SDK runtime and/or Framework package being present on the target machine. Framework-dependent deployment is the default deployment mode of the Windows App SDK for its efficient use of machine resources and serviceability.
* **Self-contained**. Your app carries the Windows App SDK dependencies with it, eliminating the need for a separate runtime installation on the target machine.

This topic also uses the terms *packaged app*, *packaged app with external location*, and *unpackaged app*. For explanations of those terms, see the [Deployment overview](./index.md).

| | Deploy framework-dependent | Deploy self-contained |
| - | - | - |
| **Advantages** | *Small deployment*. Only your app and its other dependencies are distributed. The Windows App SDK runtime and Framework package are installed automatically by framework-dependent apps that are packaged; or as part of the Windows App SDK runtime installer by framework-dependent apps that are either packaged with external location or unpackaged.<br/><br/>*Serviceable*. Servicing updates to the Windows App SDK are installed automatically via the Windows App SDK Framework package without any action required of the app. | *Control Windows App SDK version*. You control which version of the Windows App SDK is deployed with your app. Servicing updates of the Windows App SDK won't impact your app unless you rebuild and redistribute it.<br/><br/>*Isolated from other apps*. Apps and users can't uninstall your Windows App SDK dependency without uninstalling your entire app.<br/><br/>*Xcopy deployment*. Since the Windows App SDK dependencies are carried by your app, you can deploy your app by simply xcopy-ing your build output, without any additional installation requirements. |
| **Disadvantages** | *Additional installation dependencies*. Requires installation of the Windows App SDK runtime and/or Framework package, which can add complexity to app installation.<br/><br/>*Shared dependencies*. Risk that shared dependencies are uninstalled. Apps or users uninstalling the shared components can impact the user experience of other apps that share the dependency.<br/><br/>*Compatibility risk*. Risk that servicing updates to the Windows App SDK introduce breaking changes. While servicing updates should provide backward compatibility, it's possible that regressions are introduced. | *Larger deployments (unpackaged apps only)*. Because your app includes the Windows App SDK, the download size and hard drive space required are greater than would be the case for a framework-dependent version.<br/><br/>*Performance (unpackaged apps only)*. Slower to load, and uses more memory since code pages aren't shared with other apps.<br/><br/>*Not serviceable*. The Windows App SDK version distributed with your app can be updated only by releasing a new version of your app. You're responsible for integrating servicing updates of the Windows App SDK into your app. |

Also see [Create your first WinUI 3 project](/windows/apps/get-started/start-here), and [Use the Windows App SDK in an existing project](../windows-app-sdk/use-windows-app-sdk-in-existing-project.md).

> [!NOTE]
> **`PublishSingleFile` (single-file EXE)** is supported for **unpackaged, self-contained** WinUI 3 apps (Windows App SDK 1.5 and later). Packaged apps and framework-dependent apps do not support `PublishSingleFile`. See [Single-file EXE](./unpackage-winui-app.md#single-file-exe) for required MSBuild properties.

## More info about framework-dependent deployment

Before configuring your framework-dependent app for deployment, to learn more about the dependencies your app takes when it uses the Windows App SDK, review [Deployment architecture for the Windows App SDK](../windows-app-sdk/deployment-architecture.md).

### Packaged apps

If you've chosen to go with a framework-dependent packaged app (see [Deployment overview](./index.md)), then here are instructions on how to deploy the Windows App SDK runtime with the app:

* [Windows App SDK deployment guide for framework-dependent packaged apps](../windows-app-sdk/deploy-packaged-apps.md)
* [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview)

### Packaged with external location or unpackaged apps

If you've chosen to go with a framework-dependent packaged app with external location, or a framework-dependent unpackaged app (see [Deployment overview](./index.md)), then here are instructions on how to deploy the Windows App SDK runtime with the app:

* [Windows App SDK deployment guide for framework-dependent apps packaged with external location or unpackaged](../windows-app-sdk/deploy-unpackaged-apps.md)
* [Tutorial: Use the bootstrapper API in an app packaged with external location or unpackaged that uses the Windows App SDK](../windows-app-sdk/tutorial-unpackaged-deployment.md)

## More info about self-contained deployment

See [Windows App SDK deployment guide for self-contained apps](./self-contained-deploy/deploy-self-contained-apps.md).

> [!NOTE]
> `PublishSingleFile` (single-file EXE) requires the app to be both **unpackaged** and **self-contained**. See [Single-file EXE](../package-and-deploy/unpackage-winui-app.md#single-file-exe) for the full list of required MSBuild properties.

## Initialize the Windows App SDK

The way that you should initialize the Windows App SDK depends on whether, and how, you package your app; and on the way in which you deploy relative to the Windows App SDK runtime. Use the section below that applies to your app.

### Packaged apps

|How your app deploys|How to initialize|
|-|-|
|Framework-dependent|See [Call the Deployment API](../windows-app-sdk/deploy-packaged-apps.md#call-the-deployment-api).|
|Self-contained|No initialization necessary.|

### Unpackaged apps, and apps packaged with external location

|How your app deploys|How to initialize|
|-|-|
|Framework-dependent|See [Use the bootstrapper API in an app packaged with external location or unpackaged](../windows-app-sdk/tutorial-unpackaged-deployment.md).|
|Self-contained|See [Opting out of (or into) automatic UndockedRegFreeWinRT support](./self-contained-deploy/deploy-self-contained-apps.md#opting-out-of-or-into-automatic-undockedregfreewinrt-support).|

## Architecture considerations (x64, ARM64)

When you deploy your app, you must include binaries for each processor architecture your users need. This applies to both framework-dependent and self-contained deployment modes.

### ARM64 support

Windows on ARM devices (including Surface Pro X, Surface Pro 11, and Copilot+ PCs) run ARM64 natively. While x64 emulation is available on Windows 11 ARM64 devices, native ARM64 binaries provide better performance and battery life — and are recommended when you want the best experience for on-device AI workloads on Copilot+ PCs.

#### Native ARM64 deployment

- **MSIX bundles** — Create an `.msixbundle` that includes both `x64` and `ARM64` architectures. Visual Studio generates these automatically when you build for multiple platforms. The Store and App Installer select the correct architecture at install time.
- **Self-contained publish** — Specify the runtime identifier (RID) for each architecture:

  ```console
  dotnet publish -c Release -r win-x64 --self-contained true
  dotnet publish -c Release -r win-arm64 --self-contained true
  ```

- **C++/WinRT** — Build separate configurations for `x64` and `ARM64` in your Visual Studio solution.
- **Framework-dependent apps** — When using the Windows App SDK runtime installer, ensure you provide the correct architecture-specific installer. The Windows App SDK ships separate installers for x64 and ARM64.

#### Arm64EC — gradual migration for large C/C++ codebases

If your app has a large native (C/C++) codebase, a full recompile to ARM64 may not be practical in a single step. [Arm64EC](/windows/arm/arm64ec) (Emulation Compatible) lets you mix x64 and ARM64 code in the same process. You recompile performance-critical modules to native ARM64 while the remaining x64 modules run under emulation — all within a single binary.

| Approach | Best for | Trade-off |
|---|---|---|
| Full ARM64 recompile | Pure .NET apps, small C++ projects | Best performance; requires all dependencies to be ARM64-compatible |
| Arm64EC | Large C/C++ apps, apps with x64-only plugins or third-party DLLs | Incremental migration; emulated portions run slower than native |
| x64 only (emulated) | Apps that cannot be recompiled and don't need peak performance | Simplest; reduced battery life and higher latency on ARM64 devices |

For more information, see [Arm64EC — Build and port apps for native performance on Arm](/windows/arm/arm64ec).

#### Emulation (Prism)

Windows 11 on ARM uses an emulation layer called **Prism** to run x64 and x86 apps on ARM64 hardware. Prism translates x86/x64 instructions to ARM64 at runtime, providing broad app compatibility without requiring a recompile.

- **x64 emulation** — Available on Windows 11 ARM64 devices only (not Windows 10 on ARM).
- **x86 emulation** — Available on both Windows 10 and Windows 11 ARM64 devices.
- **Performance** — Emulated apps typically run with acceptable performance for productivity workloads, but graphics-intensive or compute-heavy apps benefit significantly from a native ARM64 or Arm64EC build.

> [!TIP]
> If you target only x64, your app still runs on ARM64 devices via Prism emulation (Windows 11 only). However, native ARM64 builds are strongly recommended for production apps — emulated apps use more battery and have higher latency. On Copilot+ PCs, native ARM64 can provide the best performance for on-device AI workloads.

For Store submissions, upload architecture-specific packages or a bundle containing both. The Store delivers only the matching architecture to each device.

## Related topics

* [Deployment overview](./index.md)
* [Deployment architecture for the Windows App SDK](../windows-app-sdk/deployment-architecture.md)
* [Windows App SDK deployment guide for framework-dependent packaged apps](../windows-app-sdk/deploy-packaged-apps.md)
* [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview)
* [Windows App SDK deployment guide for framework-dependent apps packaged with external location or unpackaged](../windows-app-sdk/deploy-unpackaged-apps.md)
* [Tutorial: Use the bootstrapper API in an app packaged with external location or unpackaged that uses the Windows App SDK](../windows-app-sdk/tutorial-unpackaged-deployment.md)
* [Windows App SDK deployment guide for self-contained apps](./self-contained-deploy/deploy-self-contained-apps.md)
* [Create your first WinUI project](/windows/apps/get-started/start-here)
* [Use the Windows App SDK in an existing project](../windows-app-sdk/use-windows-app-sdk-in-existing-project.md)
