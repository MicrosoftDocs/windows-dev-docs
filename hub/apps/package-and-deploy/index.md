---
description: Learn about how to package and deploy Windows apps.
title: Package and deploy
ms.topic: article
ms.date: 06/17/2021
ms.localizationpriority: medium
ms.author: mcleans
author: mcleanbyron
---

# Package and deploy

For guidance about packaging and deploying different types of Windows apps, see the following articles.

## Apps that use the Windows App SDK

Apps that use the [Windows App SDK](../windows-app-sdk/index.md) (including WinUI 3 apps or existing apps that use the Windows App SDK NuGet package) can be packaged and deployed using MSIX. If you package your app using MSIX, you can publish it to the Microsoft Store.

- [Deploy packaged apps that use the Windows App SDK](../windows-app-sdk/deploy-packaged-apps.md)
- [Publish Windows apps](/windows/uwp/publish/)

You can also deploy these types of apps using non-MSIX installation technologies.

- [Deploy unpackaged apps that use the Windows App SDK](../windows-app-sdk/deploy-unpackaged-apps.md)

## Win32 and .NET desktop apps

Win32 desktop apps (also sometimes called *classic desktop apps*) and .NET apps (including WPF and Windows Forms apps) can be packaged and deployed using MSIX. If you package your app using MSIX, you can publish it to the Microsoft Store.

- [Create an MSIX package from an existing installer](/windows/msix/packaging-tool/create-an-msix-overview)
- [Build an MSIX package from source code](/windows/msix/desktop/source-code-overview)
- [Publish Windows apps](/windows/uwp/publish/)

You can also deploy Win32 and .NET desktop apps using other installation technologies.

- [Application installation and servicing](/windows/desktop/application-installing-and-servicing)
- [Windows Installer](/windows/desktop/msi/windows-installer-portal)
- [.NET application publishing overview](/dotnet/core/deploying/)
- [Deploying the .NET Framework and applications](/dotnet/framework/deployment/)
- [Deploying a WPF application](/dotnet/framework/wpf/app-development/deploying-a-wpf-application-wpf)
- [ClickOnce Deployment for Windows Forms](/dotnet/framework/winforms/clickonce-deployment-for-windows-forms)

## UWP apps

UWP apps must be packaged and deployed using MSIX. You can optionally publish UWP apps to the Microsoft Store.

- [Overview of packaging UWP apps](/windows/uwp/packaging)
- [Package a UWP app in Visual Studio](/windows/msix/package/packaging-uwp-apps)
- [Publish Windows apps](/windows/uwp/publish/)

## Related topics

- [Set up your development environment](../windows-app-sdk/set-up-your-development-environment.md)
- [Get started developing apps](../get-started/index.md)
