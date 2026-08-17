---
title: Packaging and Deployment Options
description: Compare Windows app packaging models, distribution paths, and Windows App SDK deployment options to choose the right approach for your app.
ms.topic: concept-article
ms.date: 08/14/2026
ms.localizationpriority: medium
---

# Package and deploy Windows apps overview

:::image type="icon" source="images/header-packaging.png" border="false":::

App packaging provides your app with a predictable installation, update, and servicing model on Windows. WinUI 3 apps are packaged by default, while many other app types aren't. Features that depend on package identity include background tasks, push notifications, Windows 10 live tiles, custom context menu extensions, share targets, and other extensibility points. Packaging also supports cleaner deployments, reliable updates, and distribution through channels such as the Microsoft Store and enterprise deployment tools.

Deployment determines how you distribute the app and any runtime dependencies to users. This overview helps you choose a packaging model, a distribution path, and, for apps that use the Windows App SDK, a framework-dependent or self-contained deployment model.

## Choose packaging, distribution, and deployment options

- To compare packaged, packaged-with-external-location, and unpackaged models, see [Packaging overview](packaging/index.md).
- To compare the Microsoft Store, enterprise, and direct-download paths, see [Choose a distribution path](choose-distribution-path.md).
- To compare framework-dependent and self-contained Windows App SDK deployment, see [Windows App SDK deployment overview](deploy-overview.md).
- For the current status of distribution features, including the `ms-appinstaller` protocol, see [Current status of Windows app distribution features](distribution-feature-status.md).

## Windows app deployment scenario quick reference

The following table maps common distribution goals to packaging and Windows App SDK runtime options. For code-signing costs and mobile device management (MDM) support, see [Choose a distribution path](choose-distribution-path.md).

| Distribution goal | Packaging mode | Runtime mode | Windows App SDK runtime on target machine |
|---|---|---|---|
| **Publish to Microsoft Store** | [Packaged (MSIX)](packaging/index.md) | Framework-dependent | Auto-installed by Store |
| **Direct MSIX download from website (with [App Installer](/windows/msix/app-installer/app-installer-file-overview))** | [Packaged (MSIX)](packaging/index.md) | Either | Bundled if self-contained; the framework package must be [distributed with the app](../windows-app-sdk/deploy-packaged-apps.md) if framework-dependent |
| **Enterprise deployment via Microsoft Intune or Microsoft Configuration Manager (ConfigMgr)** | [Packaged (MSIX)](packaging/index.md) or [packaged with external location](packaging/index.md) (Windows 10, version 2004, build 19041, or later) | Either | Packaged apps can declare framework dependencies; framework-dependent apps packaged with external location must deploy the runtime separately |
| **Direct download with a WiX or Inno installer** | [Packaged with external location](packaging/index.md) or unpackaged | Self-contained recommended | Bundled by self-contained deployment |
| **Xcopy or zip archive (no installer)** | Unpackaged | Self-contained | Bundled by self-contained deployment |
| **Framework-dependent download (smallest footprint)** | Unpackaged | Framework-dependent | Must be pre-installed or [deployed separately](../windows-app-sdk/deploy-unpackaged-apps.md) |
| **Continuous integration artifact or internal test** | Either | Self-contained simplifies machine setup | Bundled if self-contained; requires runtime installation if framework-dependent |

> [!NOTE]
> **`PublishSingleFile` (single-file EXE)** works with **unpackaged, self-contained .NET WinUI 3** apps (Windows App SDK 1.5 and later). It produces a single distributable EXE that extracts dependencies to a temp directory at first launch. Packaged apps (MSIX or packaged with external location) don't support `PublishSingleFile`. For required MSBuild properties and build-time validation, see [`WindowsAppSDKSingleFileVerifyConfiguration`](project-properties.md).

## Packaging and deployment resources

[!INCLUDE [apps-packaging-overview](../../includes/apps-packaging-overview.md)]

## Related content

- [Publish your first Windows app](publish-first-app.md)
- [Code signing options for Windows app developers](code-signing-options.md)
- [MSIX on Windows 10 and Windows 11](msix-windows10-windows11.md)
