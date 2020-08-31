---
Description: Learn how to get started building desktop apps for Windows PCs, including how to choose the right app platform for new apps and how to modernize existing apps for Windows 10.
title: Build desktop apps for Windows PCs
ms.topic: article
ms.date: 11/04/2019
keywords: windows win32, desktop development
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
---

# Build desktop apps for Windows PCs

This article provides the info you need to get started building desktop apps for Windows or updating existing desktop apps to adopt the latest experiences in Windows 10.

## Platforms for desktop apps

There are four main platforms for building desktop apps for Windows PCs. Each platform provides an app model that defines the lifecycle of the app, a complete UI framework and set of UI controls that let you create desktop apps like Word, Excel, and Photoshop, and access to a comprehensive set of managed or native APIs for using Windows features. For an in-depth comparison of these platforms along with additional resources for each platform, see [Choose your app platform](choose-your-platform.md).

<br/>

<table>
<colgroup>
<col width="20%" />
<col width="60%" />
<col width="20%" />
</colgroup>
<thead>
<tr class="header">
<th>Platform</th>
<th>Description</th>
<th>Docs and resources</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td><a href="https://docs.microsoft.com/windows/uwp/">Universal Windows Platform (UWP)</a></td>
<td><p>The leading-edge platform for Windows 10 apps and games. You can build UWP apps that exclusively use UWP controls and APIs, or you can use UWP controls and APIs in desktop apps that are built using one of the other platforms.</p></td>
<td><a href="/windows/uwp/get-started/">Get started</a><br/><a href="/uwp/">API reference</a><br/><a href="https://github.com/Microsoft/Windows-universal-samples">Samples</a></td>
</tr>
<tr class="even">
<td><a href="https://docs.microsoft.com/windows/win32/">C++/Win32</a></td>
<td><p>The platform of choice for native Windows apps that require direct access to Windows and hardware.</p></td>
<td><a href="/windows/win32/desktop-programming/">Get started</a><br/><a href="/windows/win32/apiindex/windows-api-list/">API reference</a><br/><a href="https://github.com/Microsoft/Windows-classic-samples">Samples</a></td>
</tr>
<tr class="odd">
<td><a href="/dotnet/framework/wpf/">WPF</a></td>
<td><p>The established .NET-based platform for graphically-rich managed Windows apps with a XAML UI model. These apps can target <a href="https://docs.microsoft.com/dotnet/core/whats-new/dotnet-core-3-0">.NET Core 3</a> or the full .NET Framework.</p></td>
<td><a href="/dotnet/framework/wpf/getting-started/">Get started</a><br/><a href="https://docs.microsoft.com/dotnet/api/index">API reference (.NET)</a><br/><a href="https://github.com/Microsoft/WPF-Samples">Samples</a></td>
</tr>
<tr class="even">
<td><a href="/dotnet/framework/winforms/">Windows Forms</a></td>
<td><p>A .NET-based platform that is designed for managed line-of-business apps with a lightweight UI model. These apps can target <a href="https://docs.microsoft.com/dotnet/core/whats-new/dotnet-core-3-0">.NET Core 3</a> or the full .NET Framework.</p></td>
<td><a href="/dotnet/framework/winforms/getting-started-with-windows-forms">Get started</a><br/><a href="https://docs.microsoft.com/dotnet/api/index">API reference (.NET)</a></td>
</tr>
</tbody>
</table>

> [!NOTE]
> On Windows 10, each these platforms also support using the Windows UI (WinUI) Library to create user interfaces. For more information about WinUI for desktop apps, see [this section](choose-your-platform.md#windows-ui-library).

## Update existing desktop apps for Windows 10

If you have an existing WPF, Windows Forms, or native Win32 desktop app, Windows 10 and the Universal Windows Platform (UWP) offer many features you can use to deliver a modern experience in your app. Most of these features are available as modular components that you can adopt in your app at your own pace without having to rewrite your app for a different platform.

Here are just a few of the features available to enhance your existing desktop apps:

* Use [MSIX](/windows/msix/) to package and deploy your desktop apps. MSIX is a modern Windows app package format that provides a universal packaging experience for all Windows apps. MSIX brings together the best aspects of MSI, .appx, App-V and ClickOnce installation technologies and is built to be safe, secure, and reliable.
* Integrate your desktop app with Windows 10 experiences by using [package extensions](./modernize/desktop-to-uwp-extensions.md). For example, point Start tiles to your app, make your app a share target, or send toast notifications from your app.
* Use [XAML Islands](./modernize/xaml-islands.md) to host UWP XAML controls in your desktop app. Many of the latest Windows 10 UI features are only available to UWP XAML controls.

For more information, see these articles.

<br/>

| Article | Description |
|---------|-------------|
| [Modernize desktop apps](./modernize/index.md) | Describes the latest Windows 10 and UWP development features you can use in any desktop app, including WPF, Windows Forms, and C++ Win32 apps. |
| [Tutorial: Modernize a WPF app](./modernize/modernize-wpf-tutorial.md) | Follow step-by-step instructions to modernize an existing WPF line-of-business sample app by adding UWP Ink and calendar controls to the app and packaging it in an MSIX package.  |

## Create new desktop apps

If you are creating a new desktop app for Windows, here are some resources to help get you started.

<br/>

| Article | Description |
|---------|-------------|
| [Choose your app platform](choose-your-platform.md) | Provides an in-depth comparison of the main desktop app platforms and can help you choose the right platform for your needs. This article also provides useful links to docs for each platform. |
| [Visual Studio project templates for Windows apps](visual-studio-templates.md) | Describes the project and item templates that Visual Studio provides to help you build apps for Windows 10 devices by using C\# or C++. |
| [Modernize desktop apps](./modernize/index.md) | Describes the latest Windows 10 and UWP development features you can use in any desktop app, including WPF, Windows Forms, and C++ Win32 apps. |
| [Features and technologies](../features-and-technologies.md) | Provides an overview of Windows features that are accessible via each of the main desktop app platforms and links to the related docs. |

## Related documentation and technologies

| Resource | Description |
|---------|-------------|
| [.NET Core 3.0](/dotnet/core/whats-new/dotnet-core-3-0) | Learn about the latest features of .NET Core 3.0, including enhancements for WPF and Windows Forms apps. |
| [Desktop guide for WPF and .NET Core 3.0](/dotnet/desktop-wpf/overview/index) | Develop WPF apps that target .NET Core 3.0 instead of the full .NET Framework.  |
| [Azure](/azure/) | Extend the reach of your apps with Azure cloud services. |
| [Visual Studio](/visualstudio/) | Learn how to use Visual Studio to develop apps and services. |
| [MSIX](/windows/msix/) | Package and deploy any Windows app in a modern and universal packaging format. |
| [Windows AI](/windows/ai/) | Use Windows AI to build intelligent solutions for complex problems in your apps. |
| [Windows Containers](/virtualization/windowscontainers/) | Package your applications with their dependencies in fast, fully isolated Windows environments. |
| [Progressive Web Apps](/microsoft-edge/progressive-web-apps) | Convert your web apps into Progressive Web Apps that can be distributed and run as UWP apps on Windows 10. |
| [Xamarin](/xamarin/) | Build cross-platform apps for Windows, Android, iOS, and macOS using .NET code and platform-specific user interfaces. |
| [Docs archive for Windows 8.x and earlier](/previous-versions/windows/) | Access archived documentation about building apps for Windows 8.x and earlier versions. |