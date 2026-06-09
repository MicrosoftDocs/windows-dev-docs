---
title: Package a .NET app with MSIX
description: This topic enables you to package a WPF or WinForms app with MSIX.
ms.topic: how-to
ms.date: 05/07/2026
keywords: windows win32, windows app development, Windows App SDK, Windows Forms, WinForms
ms.localizationpriority: medium
---

# Package a .NET app with MSIX

Some Windows features and APIs require your app to have *package identity* at runtime (in other words, your app needs to be *packaged*). For more info, see [Features that require package identity](/windows/apps/desktop/modernize/modernize-packaged-apps) and [WinRT APIs that require package identity](../winrt-api-desktop-app-support.md#apis-that-require-package-identity). [MSIX](/windows/msix/) packaging is also required for MSIX-based submissions to the Microsoft Store.

This article shows the steps to package a WPF or WinForms project in Visual Studio. For more information about app packaging, see [Packaging overview](../../../package-and-deploy/packaging/index.md) and other articles in the app packaging section of the documentation.

To package a .NET (WPF or WinForms) app with MSIX:

1. In **Solution Explorer** in Visual Studio, right-click the solution, and choose **Add** > **New Project...**.
1. In the **Add a new project** dialog box, search for *packaging*, choose the C# **Windows Application Packaging Project** project template, and click **Next**.
1. Name the project, and click **Create**.
1. We want to specify which applications in the solution are to be included in the package. So in the packaging project (*not* the .NET project), right-click the **Dependencies** node, and choose **Add Project Reference...**.
1. In the list of projects in the solution, choose your .NET project, and click **OK**.
1. Expand the packaging project's **Dependencies** > **Applications** node, and confirm that your .NET project is referenced and highlighted in bold. This means that it will be used as a starting point for the package.
1. Right-click the packaging project, and choose **Set As Startup Project**.
1. Right-click the .NET project, and choose **Edit Project File**.
1. Delete `<WindowsPackageType>None</WindowsPackageType>`, save, and close.
1. In the **Solution Platforms** drop-down, pick *x64* (instead of *Any Cpu*).
1. Confirm that you can build and run.

Now that you've packaged your WinForms app, you can call APIs that require package identity.

> [!NOTE]
> The steps in this section showed you how to create a *packaged app*. An alternative is to create an *app packaged with external location*. For a reminder of all these terms, see [Packaging overview](../../../package-and-deploy/packaging/index.md).

## Related topics

- [Packaging overview](../../../package-and-deploy/packaging/index.md)
- [Package your app using single-project MSIX](../../../windows-app-sdk/single-project-msix.md)
- [What is MSIX?](/windows/msix/overview)
- [Windows Presentation Foundation (WPF)](/dotnet/desktop/wpf/)
- [Windows Forms (WinForms)](/dotnet/desktop/winforms/)
- [Call Windows Runtime APIs](../../../desktop/modernize/winrt-apis-desktop-apps.md)
- [Features that require package identity](/windows/apps/desktop/modernize/modernize-packaged-apps)
- [WinRT APIs that require package identity](../winrt-api-desktop-app-support.md#apis-that-require-package-identity)
