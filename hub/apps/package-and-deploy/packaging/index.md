---
title: Packaging overview
description: Understand the differences between packaged and unpackaged apps and how packaging affects installation, updates, and access to Windows features.
ms.topic: concept-article
ms.date: 03/27/2026
ms.localizationpriority: medium
---

# Packaging overview

Packaging defines how your app is installed, updated, and integrated with Windows. WinUI 3 apps are packaged by default, while many desktop apps, such as traditional Win32 applications, run unpackaged. Choosing between a **packaged** or **unpackaged** app affects the features you can use, the deployment model you rely on, and the overall experience your customers get.

> [!NOTE]
> **Building a new WinUI 3 app?** You're already packaged by default. The guidance below is most relevant for developers who need to make an explicit choice — typically when porting an existing app, deploying to enterprise machines, or adding Windows features to an app that wasn't originally packaged.

## Why app packaging matters

Packaged apps benefit from a clean installation model, automatic updates, and access to Windows features that require package identity — including background tasks, notifications, context menu extensions, share targets, and other extensibility points. Packaging also helps ensure cleaner deployments, reliable updates, and streamlined distribution through channels such as the Microsoft Store and enterprise deployment tools.

## Features that require package identity

These Windows features only work in apps that have package identity — either through full MSIX packaging or [packaging with external location (Sparse packaging)](#packaging-with-external-location-sparse-packaging).

| Feature | Description |
|---|---|
| **Background tasks** | Run code when your app isn't in the foreground — for example, to sync data, process downloads, or respond to system events. |
| **Windows AI APIs** (Phi Silica, OCR, etc.) | Access on-device AI capabilities such as local language models, text recognition, and image analysis. |
| **Push notifications** (WNS) | Receive real-time notifications from your cloud service through the Windows Notification Service. |
| **Share target** | Let users share content from other apps directly into yours via the system Share sheet. |
| **Custom context menu extensions** | Add your app's actions to the right-click menu in File Explorer and other shell surfaces. |
| **File type and protocol associations** | Register your app as the handler for specific file types or URI protocols (e.g., `yourapp://`). |
| **Startup tasks** | Launch your app automatically when the user signs in to Windows. |
| **App services** | Expose background services that other apps can call into, enabling inter-app communication. |

> [!TIP]
> If you're unpackaged and hitting `E_ILLEGAL_METHOD_CALL` or `APPMODEL_ERROR_NO_PACKAGE` errors when calling Windows APIs, that's the package identity requirement. See [packaging with external location (Sparse packaging)](#packaging-with-external-location-sparse-packaging) as the lowest-friction fix.

For more information, see [Features that require package identity](../../desktop/modernize/modernize-packaged-apps.md).

## Packaging models at a glance

| Model | Package identity | Installer | Store eligible | Best for |
|---|---|---|---|---|
| **Packaged (MSIX)** | ✅ Yes | MSIX replaces installer | ✅ Yes | New apps, Store publishing, enterprise MDM |
| **Packaging with external location** | ✅ Yes | Your existing installer | ❌ No | Existing apps with own installer, ISVs |
| **Unpackaged** | ❌ No | XCopy / script | ❌ No | Internal tools, dev utilities, simple scenarios |

### Packaged apps (MSIX)

Packaged apps use MSIX and have **package identity**, which is required for many Windows extensibility points. Package identity allows Windows to reliably identify the caller of platform APIs, which is why these features depend on it.

- Packaged apps typically run in a lightweight app container with file system and registry virtualization (see [AppContainer for legacy apps](/windows/win32/secauthz/appcontainer-for-legacy-applications-) and [MSIX AppContainer apps](/windows/msix/msix-container)).
- Apps can also be configured **not** to run in an app container if needed.
- MSIX is used both for packaging and installation (see [What is MSIX?](/windows/msix/overview)).

## Packaging with external location (Sparse packaging)

Packaging with external location (also called *sparse packages*) lets you register a small identity package alongside your existing app — without changing your installer, binary locations, or update process. It was introduced in Windows 10 version 2004 (build 19041).

This is the sweet spot for existing Win32/WPF/WinForms apps that ship via their own installer (NSIS, WiX, InstallShield, etc.) and don't want to replace it with MSIX. You register a lightweight identity package, your binaries stay where they are, and you unlock the full set of package-identity-gated Windows features.

| Capability | MSIX | External location |
|---|---|---|
| Replaces your installer | Yes | No |
| Binaries inside the package | Yes | No (external) |
| Store eligible | Yes | No |
| Package identity | Yes | Yes |
| Update mechanism | MSIX update | Your existing mechanism |

→ [Full walkthrough: Grant package identity by packaging with external location](../../desktop/modernize/grant-identity-to-nonpackaged-apps-overview.md)

### Unpackaged apps

Unpackaged apps don't use MSIX and **don't have package identity**, which means they cannot access the features listed above.

- They remain fully unrestricted in terms of API surface, file system access, registry access, elevation, and process model.
- Installation and updates rely on `.exe`, `.msi`, custom installers, ClickOnce, or xcopy deployment.

Before you commit to unpackaged, check the [features table above](#features-that-require-package-identity) against your roadmap. If notifications, background tasks, or AI APIs are on the horizon, consider starting packaged.

## Choose by scenario

| Scenario | Recommended model | Details |
|---|---|---|
| **Indie developer publishing to the Microsoft Store** | Packaged (MSIX) | The Store requires MSIX. WinUI 3 apps are packaged by default — no changes needed. **Code signing is handled free by the Store.** → [Distribute your packaged app](../../distribute-through-store/how-to-distribute-your-win32-app-through-microsoft-store.md) |
| **Enterprise app deployed via Intune or Configuration Manager** | Packaged, or external location for existing installers | New apps should use MSIX. Existing apps with their own installer can use packaging with external location. **Code signing:** use a self-signed cert (trusted via Intune, Group Policy, or Configuration Manager) or [Azure Artifact Signing (formerly Trusted Signing)](/azure/trusted-signing/). → [Deploy packaged apps](../../windows-app-sdk/deploy-packaged-apps.md) |
| **ISV shipping a direct download with own installer** | Packaging with external location | Register a lightweight identity package alongside your existing installer. **Code signing:** a CA-trusted certificate is required for non-Store distribution. [Azure Artifact Signing (formerly Trusted Signing)](/azure/trusted-signing/) is the recommended lower-cost option. → [Grant package identity](../../desktop/modernize/grant-identity-to-nonpackaged-apps-overview.md) |
| **Internal tool or developer utility** | Unpackaged | Simplest to build and deploy. The Windows App SDK works via NuGet, but some features won't be available. |

> [!TIP]
> **Not sure about code signing costs?** Publishing through the Microsoft Store means you don't need to separately obtain or manage a certificate for end-user trust. For other distribution paths, your signing approach depends on deployment context — enterprise environments can trust a self-signed certificate through device management, while broader non-Store distribution typically requires a CA-trusted code signing solution. [Azure Artifact Signing (formerly Trusted Signing)](/azure/trusted-signing/) is Microsoft's recommended option (see [pricing](https://azure.microsoft.com/pricing/details/trusted-signing/)), with no hardware token required.

## Framework-dependent vs self-contained deployment

Separately from packaging model, apps using the Windows App SDK choose how to carry their runtime dependencies:

- **Framework-dependent**: The Windows App SDK runtime must be installed on the user's machine. Smaller app footprint; relies on the runtime being present or auto-installed.
- **Self-contained**: All Windows App SDK binaries ship with your app. Larger footprint; no external runtime requirement. Good for locked-down enterprise environments.

→ [Deploy self-contained apps](../self-contained-deploy/deploy-self-contained-apps.md)

## Get started with MSIX

If you build a Win32 desktop app (sometimes called a *classic desktop app*) or a .NET app — including Windows Presentation Foundation (WPF) and Windows Forms (WinForms) — then you can package and deploy your app using MSIX.

- [Create an MSIX package from an existing installer](/windows/msix/packaging-tool/create-an-msix-overview)
- [Build an MSIX package from source code](/windows/msix/desktop/source-code-overview)
- [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview)

## Other installation technologies

- [Application installation and servicing](/windows/desktop/application-installing-and-servicing)
- [Windows Installer](/windows/desktop/msi/windows-installer-portal)
- [.NET application publishing overview](/dotnet/core/deploying/)
- [Deploying the .NET Framework and applications](/dotnet/framework/deployment/)
- [Deploying a WPF application](/dotnet/framework/wpf/app-development/deploying-a-wpf-application-wpf)
- [ClickOnce Deployment for Windows Forms](/dotnet/framework/winforms/clickonce-deployment-for-windows-forms)

## Related content

- [Package identity overview](../../desktop/modernize/package-identity-overview.md)
- [Deploy packaged apps (Windows App SDK)](../../windows-app-sdk/deploy-packaged-apps.md)
- [Deploy unpackaged apps (Windows App SDK)](../../windows-app-sdk/deploy-unpackaged-apps.md)
- [Tutorial: Unpackage a WinUI app](../unpackage-winui-app.md)
