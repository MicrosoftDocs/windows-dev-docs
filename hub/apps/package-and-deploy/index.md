---
title: Deployment overview
description: The topics in this section introduce options and guidance around deploying different types of Windows apps. Your first decision will be whether or not to MSIX-package your app.
ms.topic: article
ms.date: 03/14/2022
ms.localizationpriority: medium
---

# Deployment overview

The topics in this section introduce options and guidance around deploying different types of Windows apps.

## Advantages and disadvantages of MSIX-packaging

Your first decision will be whether or not to MSIX-package your app.

* **MSIX-packaging**. This is the process of packaging an app using MSIX technology (see [What is MSIX?](/windows/msix/overview)). MSIX-packaging gives your app a *package identity* (see the table below for why that's a benefit).
* **Sparse-packaging**. A way to opt out of MSIX-packaging (so that your app less restricted) while retaining package identity. For instructions on how to sparse-package your app, see [Tutorial: Build and deploy an unpackaged app that uses the Windows App SDK](/windows/apps/windows-app-sdk/tutorial-unpackaged-deployment).
* **No packaging**. Another way to opt out of MSIX-packaging (for the reason given above), but without package identity.

> [!IMPORTANT]
> We recommend that you *do* MSIX-package your app. It'll be a modern and reliable packaging and deployment experience for your customers. Other ways of deploying your app involve other installation technologies, such as `.exe` or `.msi` files.

| | MSIX-packaging | Sparse-packaging or no packaging |
| - | - | - |
| **Advantages** | MSIX-packaging gives your users an easy way to install, uninstall, and update your app. Uninstall is clean&mdash;when your app is uninstalled, the system is restored to the same state it was in before installation&mdash;no artifacts are left behind. MSIX also supports incremental and automatic updates. And the Microsoft Store optimizes for MSIX packages (MSIX can be used in or out of the Store).<br/><br/>MSIX-packaging also gives your app a *package identity*, which is needed for certain Windows features (for example, custom context menu extensions). | If you choose not to go with MSIX-packaging, then your app is unrestricted in terms of the the kind of app it is, the APIs it can call, and its access to the Registry and file system.<br/><br/>Sparse-packaging means that it's still possible to get the same benefits from having package identity that MSIX-packaging gives you.<br/><br/>Your app will typically be installed and updated using `.exe` or `.msi` files; using a custom installer, ClickOnce, or xcopy deployment. |
| **Disadvantages** | Your app is limited in terms of the kind of app it can be, and the agency it can have within the system. An NT Service isn't possible, for example. Inter-process communication (IPC) options are limited; privileged/elevated access is restricted if you're publishing to the Microsoft Store; file/Registry access are virtualized (but also see [Flexible virtualization](/windows/msix/desktop/flexible-virtualization)). And in some situations enterprise policies can disable MSIX updates by disabling the Microsoft Store. | An app that doesn't use MSIX is at risk of causing stale configuration data and software to accumulate after the app has been uninstalled. That can be an issue for the customer and for the system. |

## Use the Windows App SDK

After deciding whether or not to MSIX-package your app, you can next decide whether or not to use the [Windows App SDK](/windows/apps/windows-app-sdk/) in your app. See [Windows App SDK deployment overview](deploy-overview.md).

## Win32 and .NET desktop apps

If you build a Win32 desktop app (sometimes called a *classic desktop app*) or a .NET app&mdash;including Windows Presentation Foundation (WPF) and Windows Forms (WinForms)&mdash;then you can package and deploy your app using MSIX.

- [Create an MSIX package from an existing installer](/windows/msix/packaging-tool/create-an-msix-overview)
- [Build an MSIX package from source code](/windows/msix/desktop/source-code-overview)
- [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview)

You can also package and deploy these types of apps using other installation technologies.

- [Application installation and servicing](/windows/desktop/application-installing-and-servicing)
- [Windows Installer](/windows/desktop/msi/windows-installer-portal)
- [.NET application publishing overview](/dotnet/core/deploying/)
- [Deploying the .NET Framework and applications](/dotnet/framework/deployment/)
- [Deploying a WPF application](/dotnet/framework/wpf/app-development/deploying-a-wpf-application-wpf)
- [ClickOnce Deployment for Windows Forms](/dotnet/framework/winforms/clickonce-deployment-for-windows-forms)

## UWP apps

UWP apps are packaged and deployed using MSIX.

- [Overview of packaging UWP apps](/windows/uwp/packaging)
- [Package a UWP app in Visual Studio](/windows/msix/package/packaging-uwp-apps)
- [Manage your MSIX deployment](/windows/msix/desktop/managing-your-msix-deployment-overview)

## Related topics

- [Set up your development environment](../windows-app-sdk/set-up-your-development-environment.md)
- [Get started developing apps](../get-started/index.md)
