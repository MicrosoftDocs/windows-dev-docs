---
title: Packaging overview
description: Understand the differences between packaged and unpackaged apps and how packaging affects installation, updates, and access to Windows features.
ms.topic: concept-article
ms.date: 08/14/2026
ms.localizationpriority: medium
---

# Packaging overview

Packaging defines how your app is installed, updated, and integrated with Windows. WinUI 3 apps are packaged by default, while many desktop apps, such as traditional Win32 applications, run unpackaged. Choosing between a **packaged** or **unpackaged** app affects the features you can use, the deployment model you rely on, and the overall experience your customers get.

> [!NOTE]
> **Building a new WinUI 3 app?** You're already packaged by default. The guidance below is most relevant for developers who need to make an explicit choice — typically when porting an existing app, deploying to enterprise machines, or adding Windows features to an app that wasn't originally packaged.

## Why app packaging matters

Packaged apps benefit from a clean installation model, automatic updates, and access to Windows features that require package identity — including background tasks, notifications, context menu extensions, share targets, and other extensibility points. Packaging also helps ensure cleaner deployments, reliable updates, and streamlined distribution through channels such as the Microsoft Store and enterprise deployment tools.

## Features that require package identity

Many Windows features only work in apps that have package identity, either through full MSIX packaging or [packaging with external location (Sparse packaging)](#packaging-with-external-location-sparse-packaging). Examples include background tasks, push notifications, share targets, custom context menu extensions, manifest-based file type and protocol associations, and the Windows AI APIs.

For the complete list, see [Features that require package identity](../../desktop/modernize/modernize-packaged-apps.md).

> [!TIP]
> If you're unpackaged and hitting `E_ILLEGAL_METHOD_CALL` or `APPMODEL_ERROR_NO_PACKAGE` errors when calling Windows APIs, that's the package identity requirement. See [packaging with external location (Sparse packaging)](#packaging-with-external-location-sparse-packaging) as the lowest-friction fix.
>
> To detect at runtime whether your process has package identity, use `GetCurrentPackageFullName`. See [Is This a Packaged Process?](https://devblogs.microsoft.com/insidemsix/is-this-a-packaged-process/) on the Inside MSIX blog for canonical C++ and C# samples.

## Packaging models at a glance

| Model | Package identity | Installer | Store eligible | Best for |
|---|---|---|---|---|
| **Packaged (MSIX)** | ✅ Yes | MSIX replaces installer | ✅ Yes (MSIX submission) | New apps, Store publishing, enterprise MDM |
| **Packaging with external location** | ✅ Yes | Your existing installer | ✅ Yes (MSI/EXE submission) | Existing apps with own installer, ISVs |
| **Unpackaged** | ❌ No | MSI or EXE installer (also: XCopy or script for non-Store distribution) | ✅ Yes (MSI/EXE submission — requires an MSI or EXE installer with silent install support) | Broad Win32 distribution, internal tools |

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
| Store eligible | Yes (MSIX submission) | Yes (MSI/EXE submission) |
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
| **Indie developer publishing to the Microsoft Store** | Packaged (MSIX) recommended | MSIX is the recommended path — it enables Store-managed updates, differential downloads, and clean uninstall. WinUI 3 apps are packaged by default. **Code signing is handled free by the Store.** → [Distribute your packaged app](../../distribute-through-store/how-to-distribute-your-win32-app-through-microsoft-store.md)<br><br>Win32 apps with an existing MSI or EXE installer can also publish to the Store via the [MSI/EXE submission path](../../publish/publish-your-app/msi/create-app-submission.md), but the Store does not push updates to existing users — updates must be handled by the app or installer. |
| **Enterprise app deployed via Intune or Configuration Manager** | Packaged, or external location for existing installers | New apps should use MSIX. Existing apps with their own installer can use packaging with external location. **Code signing:** use a self-signed cert (trusted via Intune, Group Policy, or Configuration Manager) or [Azure Artifact Signing (formerly Trusted Signing)](/azure/trusted-signing/). → [Deploy packaged apps](../../windows-app-sdk/deploy-packaged-apps.md) |
| **ISV shipping a direct download with own installer** | Packaging with external location | Register a lightweight identity package alongside your existing installer. **Code signing:** a CA-trusted certificate is required for non-Store distribution. [Azure Artifact Signing (formerly Trusted Signing)](/azure/trusted-signing/) is the recommended lower-cost option. → [Grant package identity](../../desktop/modernize/grant-identity-to-nonpackaged-apps-overview.md)<br><br>Alternatively, submit your existing installer to the Store via the [MSI/EXE submission path](../../publish/publish-your-app/msi/create-app-submission.md). |
| **Internal tool or developer utility** | Unpackaged | Simplest to build and deploy. The Windows App SDK works via NuGet, but some features won't be available. |

> [!TIP]
> Code signing requirements and costs vary by distribution path. For a full breakdown of your options, see [Code signing options for Windows app developers](../code-signing-options.md).

## Framework-dependent vs self-contained deployment

Separately from the packaging model, apps that use the Windows App SDK choose how to carry their runtime dependencies: **framework-dependent** (the Windows App SDK runtime is installed on the user's machine) or **self-contained** (all Windows App SDK binaries ship with your app). This choice is independent of packaging.

For a full comparison and deployment guidance, see the [Windows App SDK deployment overview](../deploy-overview.md).

## Get started with MSIX

If you build a Win32 desktop app (sometimes called a *classic desktop app*) or a .NET app — including Windows Presentation Foundation (WPF) and Windows Forms (WinForms) — then you can package and deploy your app using MSIX.

- [Create an MSIX package from an existing installer](/windows/msix/packaging-tool/create-an-msix-overview)
- [Build an MSIX package from source code](/windows/msix/desktop/source-code-overview)
- [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview)

## Migrating to MSIX from legacy installers

If your app currently uses a legacy installer, you can migrate to MSIX to gain clean install/uninstall, automatic updates, Store distribution, and package identity. The migration path depends on your current installer technology and whether you have access to the source code.

| Current installer | Recommended migration path | Source code required? |
|---|---|---|
| **MSI (Windows Installer)** | Use the [MSIX Packaging Tool](/windows/msix/packaging-tool/tool-overview) to convert the MSI directly to MSIX. Handles most MSI patterns including custom actions. | No |
| **ClickOnce (.NET)** | Rebuild from source using the Visual Studio MSIX packaging project. ClickOnce auto-update can be replaced with Store updates or App Installer. | Yes |
| **InstallShield / Advanced Installer** | Use the MSIX Packaging Tool to capture an installation on a clean VM. Complex custom actions may need manual fixup in the Package Editor. | No |
| **Inno Setup / NSIS** | Use the MSIX Packaging Tool's VM-based capture workflow. Run the EXE installer inside the tool's clean environment. | No |
| **App-V (virtual packages)** | Convert directly using the MSIX Packaging Tool — it supports App-V 5.x packages as input. | No |
| **MSIX with modifications needed** | Use the [Package Support Framework](/windows/msix/psf/package-support-framework-overview) to apply runtime fixes (file/registry redirection) without changing your app code. | No |

> [!TIP]
> For apps that have complex installers with kernel drivers, services running as SYSTEM, or machine-wide COM registrations that MSIX doesn't support, consider **MSIX with external location** (packaged with external location). This gives you package identity for Windows features while using a traditional installer for components that require elevated access. See [Grant package identity by packaging with external location](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps).

### Key considerations

- **Test on a clean VM** — The MSIX Packaging Tool captures all changes during installation. Run it on a clean Windows image to avoid capturing unrelated system changes.
- **Package Support Framework** — If your converted app has runtime issues (file path assumptions, registry writes to HKLM), the [Package Support Framework](/windows/msix/psf/package-support-framework-overview) can fix these without modifying your source.
- **Side-by-side with legacy installer** — You can deploy the MSIX version alongside the legacy installer during transition. Plan an explicit settings/data migration (for example, import on first run) because package identity and storage locations differ between MSIX and MSI/EXE installs.

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
- [App capability declarations](../app-capability-declarations.md) — declare capabilities in your package manifest to access protected APIs, devices, or resources
- [Download and install package updates from the Store](../package-updates-from-store.md) — use `Windows.Services.Store` APIs to programmatically check for and install Store updates
- [Inside MSIX blog](https://devblogs.microsoft.com/insidemsix/) — authoritative deep dives on package identity, deployment architecture, and MSIX internals by the Microsoft MSIX engineering team
