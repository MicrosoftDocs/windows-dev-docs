---
title: Choose a packaging model for your Windows app
description: Decide whether your Windows app should be packaged (MSIX), unpackaged, or use package identity with external location—based on your distribution scenario and feature requirements.
ms.topic: concept-article
ms.date: 03/19/2026
keywords: packaged, unpackaged, MSIX, package identity, sparse package, external location, Windows App SDK, WinUI, distribution
ms.localizationpriority: medium
---

# Choose a packaging model for your Windows app

> [!NOTE]
> **Building a new WinUI 3 app?** You're already packaged by default. This page is for developers who need to make an explicit choice—typically when porting an existing app, deploying to enterprise machines, or adding Windows features to an app that wasn't originally packaged.

Windows apps can be packaged, unpackaged, or somewhere in between. The right choice depends on two things: **how you're distributing your app** and **which Windows features you need**.

## Start with your scenario

### "I'm an indie developer publishing to the Microsoft Store"

**Use a packaged MSIX app.** The Store requires MSIX packaging. WinUI 3 apps created in Visual Studio are packaged by default—you don't need to make any changes. You get clean installation, automatic updates, and access to all package-identity-gated features like notifications and background tasks.

→ [Distribute your packaged app](../desktop/modernize/desktop-to-uwp-distribute.md)

---

### "I'm building an enterprise app deployed via Intune or Configuration Manager"

**Start packaged; consider external location if you have an existing installer.**

- If you're building a new app, use MSIX. It integrates cleanly with Intune and SCCM/ConfigMgr and gives you full package identity.
- If you have an **existing app with its own installer** that you can't replace, use [packaging with external location](#packaging-with-external-location). This gives your app package identity—and access to features like notifications and background tasks—without changing how or where you deploy.
- If your app genuinely needs no Windows-identity-gated features and you control the deployment environment, unpackaged works, but you'll hit a wall the first time you try to add notifications or AI features.

→ [Deploy packaged apps (Windows App SDK)](../windows-app-sdk/deploy-packaged-apps.md)  
→ [Grant package identity by packaging with external location](../desktop/modernize/grant-identity-to-nonpackaged-apps-overview.md)

---

### "I'm an ISV shipping a direct download with my own installer"

**Use packaging with external location (formerly called sparse packages).**

This is the sweet spot for existing Win32/WPF/WinForms apps that ship via their own installer (NSIS, WiX, InstallShield, etc.) and don't want to replace it with MSIX. You register a lightweight identity package alongside your existing installer, your binaries stay where they are, and you unlock the full set of package-identity-gated Windows features.

Your users won't see any change in how they install or update your app. You get the Windows features; they get a familiar experience.

→ [Grant package identity by packaging with external location](../desktop/modernize/grant-identity-to-nonpackaged-apps-overview.md)  
→ [Add an identity package in Visual Studio](../desktop/modernize/grant-identity-to-nonpackaged-apps-visual-studio.md)

---

### "I'm building an internal tool or developer utility"

**Unpackaged is fine—with caveats.**

Unpackaged apps are the simplest to build and deploy: xcopy, robocopy, or a simple script is all you need. The Windows App SDK works in unpackaged apps via NuGet. But some features won't be available, and if you later decide you need them, retrofitting package identity is non-trivial.

Before you commit to unpackaged, check the [features table below](#features-that-require-package-identity) against your roadmap. If notifications, background tasks, or AI APIs are on the horizon, consider starting packaged.

---

## Packaging models at a glance

| Model | Package identity | Installer | Store eligible | Best for |
|---|---|---|---|---|
| **Packaged (MSIX)** | ✅ Yes | MSIX replaces installer | ✅ Yes | New apps, Store publishing, enterprise MDM |
| **Packaging with external location** | ✅ Yes | Your existing installer | ❌ No | Existing apps with own installer, ISVs |
| **Unpackaged** | ❌ No | XCopy / script | ❌ No | Internal tools, dev utilities, simple scenarios |

## Features that require package identity

These Windows features only work in apps that have package identity—either through full MSIX packaging or [packaging with external location](#packaging-with-external-location).

| Feature | Notes |
|---|---|
| **App notifications (toast)** | `AppNotificationManager` requires package identity |
| **Background tasks** | Registration requires package identity |
| **Windows AI APIs** (Phi Silica, OCR, etc.) | Most Windows AI APIs require package identity |
| **Push notifications** (WNS) | Channel registration requires package identity |
| **Share target** | Declared in package manifest |
| **Custom context menu extensions** | Declared in package manifest |
| **File type and protocol associations** | Rich associations require package identity |
| **Startup tasks** | Requires package identity |
| **App services** | Requires package identity |

> [!TIP]
> If you're unpackaged and hitting `E_ILLEGAL_METHOD_CALL` or `APPMODEL_ERROR_NO_PACKAGE` errors when calling Windows APIs, that's the package identity requirement. See [packaging with external location](#packaging-with-external-location) as the lowest-friction fix.

## Packaging with external location

Packaging with external location (also called *sparse packages*) lets you register a small identity package alongside your existing app—without changing your installer, binary locations, or update process. It was introduced in Windows 10 version 2004 (build 19041).

You provide:
- A package manifest (XML file describing your app identity)
- A signed `.msix` containing only the manifest (no binaries)

Your existing installer registers this identity package, and Windows treats your app as having package identity from that point on.

This is distinct from full MSIX packaging:

| | MSIX | External location |
|---|---|---|
| Replaces your installer | Yes | No |
| Binaries inside the package | Yes | No (external) |
| Store eligible | Yes | No |
| Package identity | Yes | Yes |
| Update mechanism | MSIX update | Your existing mechanism |

→ [Full walkthrough: Grant package identity by packaging with external location](../desktop/modernize/grant-identity-to-nonpackaged-apps-overview.md)

## Framework-dependent vs self-contained deployment

Separately from packaging model, apps using the Windows App SDK choose how to carry their runtime dependencies:

- **Framework-dependent**: The Windows App SDK runtime must be installed on the user's machine. Smaller app footprint; relies on the runtime being present or auto-installed.
- **Self-contained**: All Windows App SDK binaries ship with your app. Larger footprint; no external runtime requirement. Good for locked-down enterprise environments.

→ [Deploy self-contained apps](self-contained-deploy/deploy-self-contained-apps.md)

## Related content

- [Features that require package identity](../desktop/modernize/modernize-packaged-apps.md)
- [Package identity overview](../desktop/modernize/package-identity-overview.md)
- [Deploy packaged apps (Windows App SDK)](../windows-app-sdk/deploy-packaged-apps.md)
- [Deploy unpackaged apps (Windows App SDK)](../windows-app-sdk/deploy-unpackaged-apps.md)
- [Tutorial: Unpackage a WinUI app](unpackage-winui-app.md)
