---
title: Packaging overview
description: Understand the differences between packaged and unpackaged apps and how packaging affects installation, updates, and access to Windows features.
ms.topic: concept-article
ms.date: 12/15/2025
ms.localizationpriority: medium
---


# Packaging overview

Packaging defines how your app is installed, updated, and integrated with Windows. WinUI apps are packaged by default, while many desktop apps, such as traditional Win32 applications, run unpackaged. Packaged apps benefit from a clean installation model, automatic updates, and access to Windows features that require package identity, including background tasks, notifications, context menu extensions, and other extensibility points. Unpackaged apps can still access many Windows App SDK capabilities, but may require additional setup to enable certain features.

## Why app packaging matters

Packaging determines how your app is installed, updated, identified, and integrated with Windows. Choosing between a **packaged** or **unpackaged** app affects the features you can use, the deployment model you rely on, and the overall experience your customers get. The goal of this overview is to help you quickly understand the trade-offs so you can choose the model that best matches your app’s architecture and requirements.

## Packaged vs. unpackaged apps

### Packaged apps  
Packaged apps use MSIX and have **package identity**, which is required for many Windows extensibility points—including background tasks, notifications, custom context menu extensions, and share targets. Package identity allows Windows to reliably identify the caller of platform APIs, which is why these features depend on it. For more information, see [Features that require package identity](../../desktop/modernize/modernize-packaged-apps.md).
- Packaged apps typically run in a lightweight app container with file system and registry virtualization (see [AppContainer for legacy apps](/windows/win32/secauthz/appcontainer-for-legacy-applications-) and [MSIX AppContainer apps](/windows/msix/msix-container)).  
- Apps can also be configured **not** to run in an app container if needed.  
- MSIX is used both for packaging and installation (see [What is MSIX?](/windows/msix/overview)).

#### Packaged with external location  
Some existing desktop apps aren't yet ready for all their content to live inside an MSIX package. **Packaging with external location** gives these apps package identity while allowing most of their content to remain outside the package.  
- This option still requires an installer: think of it as a hybrid model between packaged and unpackaged.  
- See [Grant package identity by packaging with external location](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps).

### Unpackaged apps  
Unpackaged apps don't use MSIX and **don't have package identity**, which means they cannot access the [features that require it](../../desktop/modernize/modernize-packaged-apps.md)..  
- They remain fully unrestricted in terms of API surface, file system access, registry access, elevation, and process model.  
- Installation and updates rely on `.exe`, `.msi`, custom installers, ClickOnce, or xcopy deployment.  
- See [Features that require package identity](/windows/apps/desktop/modernize/modernize-packaged-apps).

---

### Summary comparison

|  | **Packaged** (optional app container) | **Packaged with external location / Unpackaged** |
|---|---|---|
| **Key benefits** | Modern install/uninstall, automatic/incremental updates, clean removal with no leftover artifacts, optimized Microsoft Store experience, and access to features requiring package identity. | Full control over process model, elevation, IPC, registry and file system access. External-location packaging allows gaining package identity without fully adopting MSIX. |
| **Key limitations** | Some system-level scenarios aren't supported (e.g., NT Services). IPC options can be limited, Store publication restricts elevated access, and virtualization applies in many cases (see [Flexible virtualization](/windows/msix/desktop/flexible-virtualization)). Enterprise policies may disable Store-driven updates. | Higher risk of stale files or configuration after uninstall. Installation/update must be handled manually via `.exe`, `.msi`, or custom mechanisms. Unpackaged apps lack features that require package identity. |

---

> [!IMPORTANT]
> For most apps, using MSIX and running in an app container provides the most seamless, secure, and modern installation and update experience.

For more details about install location, working directory, virtualization, and runtime behavior, see  
[Understanding how packaged desktop apps run on Windows](/windows/msix/desktop/desktop-to-uwp-behind-the-scenes).


## Get started with MSIX

If you build a Win32 desktop app (sometimes called a *classic desktop app*) or a .NET app&mdash;including Windows Presentation Foundation (WPF) and Windows Forms (WinForms)&mdash;then you can package and deploy your app using MSIX.

- [Create an MSIX package from an existing installer](/windows/msix/packaging-tool/create-an-msix-overview)
- [Build an MSIX package from source code](/windows/msix/desktop/source-code-overview)
- [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview)

## Other installation technologies
You can also package and deploy these types of apps using other installation technologies.

- [Application installation and servicing](/windows/desktop/application-installing-and-servicing)
- [Windows Installer](/windows/desktop/msi/windows-installer-portal)
- [.NET application publishing overview](/dotnet/core/deploying/)
- [Deploying the .NET Framework and applications](/dotnet/framework/deployment/)
- [Deploying a WPF application](/dotnet/framework/wpf/app-development/deploying-a-wpf-application-wpf)
- [ClickOnce Deployment for Windows Forms](/dotnet/framework/winforms/clickonce-deployment-for-windows-forms)


