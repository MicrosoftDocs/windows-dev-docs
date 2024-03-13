---
title: Deployment overview
description: The topics in this section introduce options and guidance around deploying different types of Windows apps. Your first decision will be whether or not to package your app.
ms.topic: article
ms.date: 10/05/2023
ms.localizationpriority: medium
---

# Deployment overview

The topics in this section introduce options and guidance around deploying different types of Windows apps.

## Advantages and disadvantages of packaging your app

Your first decision will be whether or not to package your app.

* **Packaged app**. Packaged apps are the only kind that have *package identity* at runtime. Package identity is needed for many Windows extensibility features&mdash;including background tasks, notifications, live tiles, custom context menu extensions, and share targets. That's because the operating system (OS) needs to be able to identify the caller of the corresponding API. See [Features that require package identity](/windows/apps/desktop/modernize/modernize-packaged-apps).
  * Commonly, a packaged app's process runs inside a lightweight app container, and is isolated using file system and registry virtualization (see [AppContainer for legacy apps](/windows/win32/secauthz/appcontainer-for-legacy-applications-) and [MSIX AppContainer apps](/windows/msix/msix-container)). But you can configure a packaged app to *not* run in an app container.
  * A packaged app is packaged by using MSIX technology (see [What is MSIX?](/windows/msix/overview)).
  * **Packaged app with external location**. But because some existing apps aren't yet ready for all of their content to be present inside an MSIX package, there's an option for your app to be *packaged with external location*. That enables your app to have package identity; thereby being able to use those features that require it. For more info, see [Grant package identity by packaging with external location](/windows/apps/desktop/modernize/grant-identity-to-nonpackaged-apps).
  * A packaged app is *installed* by using MSIX, also. But if you choose to *package with external location*, then you can think of that as a "bring-your-own-installer" model. So there *will* be some installer work for you to do with that option. It's essentially a hybrid option between a packaged and an unpackaged app.
* **Unpackaged app**. You can opt out of using MSIX altogether by creating an unpackaged app. But be aware that an unpackaged app *doesn't* have package identity at runtime; so it misses out on certain Windows features (see [Features that require package identity](/windows/apps/desktop/modernize/modernize-packaged-apps)).

Each type of app can be published to the Microsoft Store, and installed that way or via Windows App Installer.

> [!IMPORTANT]
> We recommend that you package your app, and configure it to run in an app container. It'll be a seamless, modern, and reliable installation and update experience for your customers; and it'll be secure at runtime.

| | Packaged (and optionally running in an app container) | Packaged with external location or unpackaged |
| - | - | - |
| **Advantages** | Gives your users an easy way to install, uninstall, and update your app. Uninstall is clean&mdash;when your app is uninstalled, the system is restored to the same state it was in before installation&mdash;no artifacts are left behind. This kind of app also supports incremental and automatic updates. And the Microsoft Store optimizes for apps of this kind (although they can be used in or out of the Store).<br/><br/>You get the benefits of having package identity. | With these options, your app is unrestricted in terms of the kind of app it is, the APIs it can call, and its access to the Registry and file system.<br/><br/>Packaging with external location means that you get the benefits of having package identity. |
| **Disadvantages** | Your app is limited in terms of the kind of app it can be, and the agency it can have within the system. For example, an NT Service isn't possible. Inter-process communication (IPC) options are limited; privileged/elevated access is restricted if you're publishing to the Microsoft Store; file/Registry access is virtualized (but also see [Flexible virtualization](/windows/msix/desktop/flexible-virtualization)). And in some situations enterprise policies can disable updates by disabling the Microsoft Store. | With these options, an app that is at risk of causing stale configuration data and software to accumulate after the app has been uninstalled. That can be an issue for the customer and for the system.<br/><br/>Your app will typically be installed and updated using `.exe` or `.msi` files, or via other installation and update solutions; using a custom installer, ClickOnce, or xcopy deployment.<br/><br/>An unpackaged app lacks the benefits of having package identity. |

For more info about package install location, working directory, and file and registry virtualization, see [Understanding how packaged desktop apps run on Windows](/windows/msix/desktop/desktop-to-uwp-behind-the-scenes).

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
