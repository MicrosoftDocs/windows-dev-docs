---
description: Add modern XAML user interfaces, create MSIX packages, and incorporate other modern components into your desktop application.
title: Modernize your desktop apps for Windows
ms.topic: article
ms.date: 09/21/2021
ms.localizationpriority: medium
---

# Modernize your desktop apps

Windows 11 and Windows 10 offer many features you can use to deliver a modern experience in your desktop apps. Most of these features are available as modular components that you can adopt in your desktop apps at your own pace without having to rewrite your application for a different platform. You can enhance your existing desktop apps by choosing which Windows features to adopt.

This topic describes the features that you can use in your desktop apps today. For a tutorial that demonstrates how to modernize an existing app to use many of the features described in this topic, see the [Modernize a WPF app](modernize-wpf-tutorial.md) tutorial.

> [!NOTE]
> Do you need assistance migrating desktop apps to Windows 10 or later? The [App Assure](/fasttrack/products-and-capabilities#app-assure) service provides direct, no-cost support to developers who are porting their apps to Windows 10 and later versions. This program is available to all ISVs and eligible enterprises. For more details on eligibility and about the program itself, visit [/fasttrack/win-10-app-assure-assistance-offered](/fasttrack/win-10-app-assure-assistance-offered). To get started now, [submit your request](https://fasttrack.microsoft.com/dl/daa).

## Apply Windows 11 features

Windows 11 marks a visual evolution of the Windows operating system, and introduces new features that improve app fundamentals and user experience. Many of these features are enabled by default for apps, but desktop apps may require updates to integrate with some new features. These features include rounded corners of top-level windows, snap layouts, and the updated context menu in File Explorer.

For more information, see [Top 11 things you can do to make your app great on Windows 11](../../get-started/make-apps-great-for-windows.md).

## Windows App SDK

The Windows App SDK is a set of new developer components and tools that represent the next evolution in the Windows app development platform. The Windows App SDK provides a unified set of APIs and tools that can be used in a consistent way by any desktop app on Windows 11 and many versions of Windows 10. You can use project templates to create new desktop apps that use the Windows App SDK with a WinUI 3-based UI, or you can use the Windows App SDK in existing desktop apps.

For more information, see [Windows App SDK](../../windows-app-sdk/index.md).

## WinUI

WinUI is a native user experience framework for both Windows desktop and UWP applications. WinUI started as a toolkit that provided new and updated versions of WinRT XAML controls for UWP apps that target down-level versions of Windows. The latest version, WinUI 3, has grown in scope and is now the modern native UI platform for Windows desktop apps.

You can use WinUI in the following ways in desktop apps:

* Use [WinUI 3](../../winui/index.md) to create desktop apps (see [Create your first WinUI 3 project](../../winui/winui3/create-your-first-winui3-app.md)) with an entirely WinUI 3-based user interface. WinUI 3 is one of many features provided by the [Windows App SDK](../../windows-app-sdk/index.md).
* You can update existing WPF, Windows Forms, and C++ desktop (Win32) apps with [XAML Islands](xaml-islands.md) and host WinUI 2 controls.

For more information, see [WinUI](../../winui/index.md).

## Windows Runtime APIs

You can call many Windows Runtime APIs directly in your WPF, Windows Forms, or C++ desktop app to integrate modern experiences that light up for users. For example, you can call Windows Runtime APIs to add toast notifications to your desktop app.

For more information, see [Use Windows Runtime APIs in desktop apps](desktop-to-uwp-enhance.md).

## MSIX deployment

MSIX is a modern Windows app package format that provides a universal packaging experience for all Windows apps, including UWP, WPF, Windows Forms and Win32 apps. MSIX brings together the best aspects of MSI, .appx, App-V and ClickOnce installation technologies to provide a modern and reliable packaging experience.

Packaging your desktop Windows apps in MSIX packages gets you access to a robust installation and updating experience, a managed security model with a flexible capability system, support for the Microsoft Store, enterprise management, and many custom distribution models.

For more information, see [Building an MSIX package from your code](/windows/msix/desktop/source-code-overview).

## Use MSIX framework packages dynamically at run time

The *dynamic dependencies* feature in the Windows App SDK and in the Windows 11 OS enables your apps to reference MSIX framework packages at run time. This feature is intended to be used primarily by unpackaged desktop apps to call APIs that are provided by MSIX framework packages.

For more information, see [Use MSIX framework packages dynamically from your desktop app](framework-packages/index.md).

## .NET

.NET (previously known as .NET Core) supports Windows desktop apps, including WinUI 3 apps created with the Windows App SDK (see [Create your first WinUI 3 project](../../winui/winui3/create-your-first-winui3-app.md)). .NET also supports [Windows Presentation Foundation (WPF)](/dotnet/desktop/wpf/) and [Windows Forms (WinForms)](/dotnet/desktop/winforms/) apps. You can run new and existing Windows desktop apps on .NET, and enjoy all the benefits that .NET has to offer.

For more information, see [What's new in .NET 6](/dotnet/core/whats-new/dotnet-6).

## Host WinRT XAML controls (XAML Islands)

Starting with the Windows 10, version 1903, you can add [UWP XAML controls](/windows/uwp/design/controls-and-patterns/controls-by-function) directly to any UI element in a WPF, Windows Forms, or C++ desktop app that is associated with a window handle (HWND). This means that you can fully integrate the latest UWP features such as [Windows Ink](/windows/uwp/design/input/pen-and-stylus-interactions) and controls that support the [Fluent Design System](/windows/uwp/design/fluent-design-system/index) into windows and other display surfaces in your desktop apps. This developer scenario is sometimes called *XAML islands*.

For more information, see [WinRT XAML controls in desktop apps](xaml-islands.md)

## Use the Visual layer in desktop apps

You can now use Windows Runtime APIs in non-UWP desktop apps to enhance the look, feel, and functionality of your WPF, Windows Forms, and C++ desktop apps, and take advantage of the latest Windows UI features that are only available via UWP. This is useful when you need to create custom experiences that go beyond the built-in WinRT XAML controls you can host by using XAML Islands.

For more information, see [Modernize your desktop app using the Visual layer](visual-layer-in-desktop-apps.md).

## Additional features available to apps with package identity

Some modern Windows experiences are available only to desktop apps that have [package identity](/uwp/schemas/appxpackage/uapmanifestschema/element-identity) at runtime. These features include certain Windows Runtime APIs, package extensions, and UWP components. For more info, see [Features that require package identity](modernize-packaged-apps.md).

<a id="desktop-uwp-controls"></a>

## WinRT XAML controls optimized for desktop apps

Whether you're building a UWP app that exclusively targets the desktop device family or you want to use WinRT XAML controls in a WPF, Windows Forms, or C++ desktop app, the following new and updated WinRT XAML controls are designed to offer desktop-optimized experiences with the [Fluent Design System](/windows/uwp/design/fluent-design-system/index). These controls were introduced in Windows 10, version 1809 (the October 2018 Update, or version 10.0.17763).

| Control |  Description |
|------ |--------------|
| [MenuBar](/windows/uwp/design/controls-and-patterns/menus#create-a-menu-bar) | Provides a quick and simple way to expose a set of commands for apps that might need more organization or grouping than a **CommandBar** allows. |
| [DropDownButton](/windows/uwp/design/controls-and-patterns/buttons#create-a-drop-down-button) | Shows a chevron as a visual indicator that it has an attached flyout that contains more options.  |
| [SplitButton](/windows/uwp/design/controls-and-patterns/buttons#create-a-split-button) | Provides a button has two parts that can be invoked separately. One part behaves like a standard button and invokes an immediate action. The other part invokes a flyout that contains additional options that the user can choose from.|
| [ToggleSplitButton](/windows/uwp/design/controls-and-patterns/buttons#create-a-toggle-split-button) | Provides a button has two parts that can be invoked separately. One part behaves like a toggle button that can be on or off. The other part invokes a flyout that contains additional options that the user can choose from. |
| [CommandBarFlyout](/windows/uwp/design/controls-and-patterns/command-bar-flyout) |  Lets you show common user tasks in the context of an item on your UI canvas. |
| [ComboBox](/windows/uwp/design/controls-and-patterns/combo-box#make-a-combo-box-editable) | You can now make a combo box editable so the user can enter values that aren't listed in the control.  |
| [TreeView](/windows/uwp/design/controls-and-patterns/tree-view) | You can now configure a tree view to enable data binding, item templates, and drag and drop.  |
| [DataGridView](/windows/communitytoolkit/controls/datagrid) |   Provides a flexible way to display a collection of data in rows and columns. This control is available in the [Windows Community Toolkit](/windows/uwpcommunitytoolkit/).  |

## Other technologies for modern desktop apps

### Microsoft Graph

Microsoft Graph is a collection of APIs you can use to build apps for organizations and consumers that interact with the data of millions of users. Microsoft Graph exposes REST APIs and client libraries to access data on the following:
* Azure Active Directory
* Microsoft 365 Office apps: SharePoint, OneDrive, Outlook/Exchange, Microsoft Teams, OneNote, Planner, and Excel
* Enterprise Mobility and Security services: Identity Manager, Intune, Advanced Threat Analytics, and Advanced Threat Protection.
* Windows services: activities and devices

For more information, see the [Microsoft Graph docs](/graph/overview).

### Adaptive Cards

Adaptive Cards is an open, cross-platform framework that you can use to exchange card-based UI content in a common and consistent way across devices and platforms.

For more information, see the [Adaptive Cards docs](/adaptive-cards/).