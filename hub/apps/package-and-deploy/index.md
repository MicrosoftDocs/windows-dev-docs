---
title: Deployment overview
description: The topics in this section introduce options and guidance around deploying different types of Windows apps. Your first decision will be whether or not to package your app.
ms.topic: article
ms.date: 10/03/2022
ms.localizationpriority: medium
---

# Deployment overview

The topics in this section introduce options and guidance around deploying different types of Windows apps.

## Advantages and disadvantages of packaging your app

Your first decision will be whether or not to package your app.

* **Packaged app**. Packaged apps are the only kind that have *package identity* at runtime. Package identity is needed for certain Windows features, such as custom context menu extensions (see [Features that require package identity](../desktop/modernize/modernize-packaged-apps.md)). A packaged app is packaged by using MSIX technology (see [What is MSIX?](/windows/msix/overview)).
  * Commonly, a packaged app's process runs inside a lightweight app container; and is isolated using file system and registry virtualization.
  * **Packaged app with external location**. But you can opt out of those restrictions and still be a packaged app (still benefit from package identity). You do that by building and registering a *package with external location* with your app. A packaged app with external location uses MSIX to package, but it's not installed by using MSIX (instead, it's a "bring-your-own-installer" model). It's essentially a hybrid option between a packaged and an unpackaged app. See [Grant package identity by packaging with external location](../desktop/modernize/grant-identity-to-nonpackaged-apps.md).
* **Unpackaged app**. Another way to opt out of the restrictions described above is to create an unpackaged app, and not use MSIX at all. But be aware that an unpackaged app *doesn't* have package identity at runtime; so it misses out on certain Windows features.

Each type of app can be published to the Microsoft Store, and installed that way or via Windows App Installer.

> [!IMPORTANT]
> We recommend that you package your app to run in an app container. It'll be a seamless, modern, and reliable installation and update experience for your customers.

| | Packaged to run in an app container | Packaged with external location or unpackaged |
| - | - | - |
| **Advantages** | Gives your users an easy way to install, uninstall, and update your app. Uninstall is clean&mdash;when your app is uninstalled, the system is restored to the same state it was in before installation&mdash;no artifacts are left behind. This kind of app also supports incremental and automatic updates. And the Microsoft Store optimizes for apps of this kind (although they can be used in or out of the Store).<br/><br/>You get the benefits of having package identity. | With these options, your app is unrestricted in terms of the the kind of app it is, the APIs it can call, and its access to the Registry and file system.<br/><br/>Packaging with external location means that you get the benefits of having package identity. |
| **Disadvantages** | Your app is limited in terms of the kind of app it can be, and the agency it can have within the system. For example, an NT Service isn't possible. Inter-process communication (IPC) options are limited; privileged/elevated access is restricted if you're publishing to the Microsoft Store; file/Registry access is virtualized (but also see [Flexible virtualization](/windows/msix/desktop/flexible-virtualization)). And in some situations enterprise policies can disable updates by disabling the Microsoft Store. | With these options, an app that is at risk of causing stale configuration data and software to accumulate after the app has been uninstalled. That can be an issue for the customer and for the system.<br/><br/>Your app will typically be installed and updated using `.exe` or `.msi` files, or via other installation and update solutions; using a custom installer, ClickOnce, or xcopy deployment.<br/><br/>An unpackaged app lacks the benefits of having package identity. |

## Use the Windows App SDK

After deciding whether or not to package your app, you can next decide whether or not to use the [Windows App SDK](../windows-app-sdk/index.md) in your app. See [Windows App SDK deployment overview](deploy-overview.md).

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
