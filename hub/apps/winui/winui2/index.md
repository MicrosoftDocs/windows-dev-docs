---
title: Windows UI library (WinUI) 2
description: Provides info for WinUI 2 and Windows app development with the Windows App SDK.  
ms.topic: article
ms.date: 11/10/2021
---

# Windows UI Library (WinUI) 2

> [!NOTE]
> For more information on building Windows desktop apps with the latest version of WinUI, see [Windows UI Library 3](../index.md).

![WinUI controls](images/winui-hero1.png)

Windows UI Library (WinUI) 2 is tightly integrated with [Windows 10 and later SDKs](https://developer.microsoft.com/windows/downloads/windows-10-sdk/) and provides official native Windows UI controls and other user interface elements for UWP applications (and desktop applications using [XAML Islands](../../desktop/modernize/xaml-islands.md)).

![WinUI 2 platform support](../../images/platforms-winui2.png)

Maintaining down-level compatibility with earlier versions of Windows 10 enables WinUI 2 controls to work even if users don't have the latest OS.

**See the latest [WinUI 2 Release Notes](release-notes/index.md).**

## Features

For details on the work planned for the next release and to provide feedback, see the [WinUI 2.8 milestone](https://github.com/microsoft/microsoft-ui-xaml/milestone/14).

- **New controls**: The Windows UI Library contains new controls that aren't shipped as part of the default Windows platform.

- **Updated versions of existing controls**: The library also contains updated versions of existing Windows platform controls that you can use with earlier versions of Windows 10.

- **Support for earlier versions of Windows 10**: Windows UI Library APIs work on earlier versions of Windows 10, so you don't have to include version checks or conditional XAML to support users who might not be running the very latest OS.

- **Support for XamlDirect**: The Xaml Direct APIs, designed for middleware developers, gives you access to a lower-level Xaml features which provide better CPU and working set performance. XamlDirect enables you to use XamlDirect APIs on earlier versions of Windows 10 without needing to write special code to handle multiple target Windows 10 versions.

## Examples

> [!TIP]
> For more info, design guidance, and code examples, see [Design and code Windows apps](../../design/index.md).
>
> The **WinUI 3 Gallery** and **WinUI 2 Gallery** apps include interactive examples of most WinUI 3 and WinUI 2 controls, features, and functionality.
>
> If installed already, open them by clicking the following links: [**WinUI 3 Gallery**](winui3gallery:/item/AnimatedIcon) or [**WinUI 2 Gallery**](winui2gallery:/item/AnimatedIcon).
>
> If they are not installed, you can download the [**WinUI 3 Gallery**](https://www.microsoft.com/store/productId/9P3JFPWWDZRC) and the [**WinUI 2 Gallery**](https://www.microsoft.com/store/productId/9MSVH128X2ZT) from the Microsoft Store.
>
> You can also get the source code for both from [GitHub](https://github.com/Microsoft/WinUI-Gallery) (use the *main* branch for WinUI 3 and the *winui2* branch for WinUI 2).

## Documentation

How-to articles for Windows UI Library controls are included with the [Universal Windows Platform controls documentation](/windows/uwp/design/controls-and-patterns/).

API reference docs are located at [Windows UI Library APIs](/windows/winui/api/).

## Install and use the Windows UI Library

For instructions on installing and using the WinUI 2 library, see [Getting started with the Windows UI Library](getting-started.md).

## Developer roadmap

WinUI is hosted in the [Windows UI Library repo](https://aka.ms/winui) on GitHub where we welcome bug reports, feature requests, and community code contributions.

We are continuing to develop and evolve WinUI to support more developer scenarios. For the latest details about our plans for WinUI, see our [roadmap](https://github.com/microsoft/microsoft-ui-xaml/blob/master/docs/roadmap.md) on the Windows UI Library repo.

## NuGet package list

For details on the Windows UI Library NuGet packages, see [Windows UI Library NuGet package list](nuget-packages.md).

## See also

- [API docs](/windows/winui/api/)
- [Source code](https://aka.ms/winui)
- [XAML Controls Gallery app](https://www.microsoft.com/p/xaml-controls-gallery/9msvh128x2zt)
