---
title: Package and deploy Windows apps overview
description: The topics in this section introduce options and guidance for deploying different types of Windows apps. Your first decision will be whether or not to package your app.
ms.topic: concept-article
ms.date: 05/28/2026
ms.localizationpriority: medium
---

# Package and deploy Windows apps overview

:::image type="content" source="images/header-packaging.png" alt-text="Blue wrench and screwdriver icons on a light gray banner background representing tools for app packaging and deployment." border="false":::

---

App packaging provides your application with a predictable installation, update, and servicing model on Windows. While WinUI 3 apps are packaged by default, many other types of apps aren't. And adding package identity unlocks a wide range of Windows capabilities. Features that depend on package identity to function include background tasks, notifications, live tiles, custom context menu extensions, share targets, and other extensibility points. Packaging also helps ensure cleaner deployments, reliable updates, and streamlined distribution through channels such as the Microsoft Store and enterprise deployment tools.

Not sure which packaging model is right for your app? See [Packaging overview](packaging/index.md).

Choosing how to distribute your app — through the Microsoft Store, enterprise sideloading, or direct download? See [Choose a distribution path](choose-distribution-path.md) for a side-by-side comparison of signing costs, update mechanics, and enterprise management per path. For the current status of distribution features (including the ms-appinstaller protocol change), see [Current status of Windows app distribution features](distribution-feature-status.md).

When deploying apps that use the Windows App SDK, you can choose between framework-dependent and self-contained deployment models. Framework-dependent apps rely on the Windows App SDK runtime and/or framework package being installed on the user’s machine. In contrast, self-contained apps bundle the Windows App SDK dependencies directly with the application, ensuring the app carries everything it needs to run. The right model depends on your distribution scenario, update strategy, and how much control you want over the app’s footprint and dependencies.

## Deployment scenario quick reference

The table below maps common distribution goals to the recommended packaging mode, Windows App SDK runtime mode, and key constraints. For code signing costs and MDM manageability per distribution path, see [Choose a distribution path](choose-distribution-path.md).

| Goal | Packaging mode | Runtime mode | Windows App SDK runtime on target machine |
|---|---|---|---|
| **Publish to Microsoft Store** | [Packaged (MSIX)](packaging/index.md) | Framework-dependent | Auto-installed by Store |
| **Enterprise deploy via Intune / ConfigMgr** | [Packaged (MSIX)](packaging/index.md) or [packaged with external location](packaging/index.md) | Either | Bundled in MSIX dependency or auto-installed |
| **Direct download from website (with WiX / Inno installer)** | [Packaged with external location](packaging/index.md) (recommended for ISVs needing Windows features) or unpackaged | Self-contained recommended | Bundled by self-contained |
| **Xcopy / zip (no installer)** | Unpackaged | Self-contained | Bundled by self-contained |
| **Framework-dependent download (smallest footprint)** | Unpackaged | Framework-dependent | Must be pre-installed or [deployed separately](../windows-app-sdk/deploy-unpackaged-apps.md) |
| **CI artifact / internal test** | Either | Self-contained simplifies machine setup | Bundled if self-contained; requires runtime install if framework-dependent |

> [!NOTE]
> **`PublishSingleFile` (single-file EXE)** is supported for **unpackaged, self-contained** WinUI 3 apps (Windows App SDK 1.5 and later). It produces a single distributable EXE that extracts dependencies to a temp directory at first launch. Packaged apps (MSIX or packaged with external location) do not support `PublishSingleFile`. For required MSBuild properties and build-time validation, see [`WindowsAppSDKSingleFileVerifyConfiguration`](project-properties.md).

---

#### Get started with packaging and deploying your Windows app

[!INCLUDE [apps-packaging-overview](../../includes/apps-packaging-overview.md)]
