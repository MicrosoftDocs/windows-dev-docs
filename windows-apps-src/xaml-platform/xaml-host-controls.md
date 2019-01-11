---
description: This guide helps you to create Fluent-based UWP UIs directly in your WPF and Windows Forms applications
title: UWP controls in desktop applications
ms.date: 09/21/2018
ms.topic: article
keywords: windows 10, uwp, windows forms, wpf
ms.localizationpriority: medium
ms.custom: RS5
---
# UWP controls in desktop applications

> [!NOTE]
> XAML islands are currently available as a developer preview. Although we encourage you to try them out in your own prototype code now, we do not recommend that you use them in production code at this time. These APIs and controls will continue to mature and stabilize in future Windows releases. Microsoft makes no warranties, express or implied, with respect to the information provided here.
>
> If you have feedback about XAML islands, send your feedback to XamlIslandsFeedback@microsoft.com. Your insights and scenarios are critically important to us.

Windows 10 now enables you to use UWP controls in non-UWP desktop applications so that you can enhance the look, feel, and functionality of your existing desktop applications with the latest Windows 10 UI features that are only available via UWP controls. This means that you can use UWP features such as [Windows Ink](../design/input/pen-and-stylus-interactions.md) and controls that support the [Fluent Design System](../design/fluent-design-system/index.md) in your existing WPF, Windows Forms, and C++ Win32 applications. This developer scenario is sometimes called *XAML islands*.

We provide several ways to use XAML islands in your WPF, Windows Forms, and C++ Win32 applications, depending on the technology or framework you are using.

## Wrapped controls

WPF and Windows Forms applications can use a selection of wrapped UWP controls in the [Windows Community Toolkit](https://docs.microsoft.com/windows/uwpcommunitytoolkit/). We refer to these controls as *wrapped controls* because they wrap the interface and functionality of a specific UWP control. You can add these controls directly to the design surface of your WPF or Windows Forms project and then use them like any other WPF or Windows Forms control in your designer.

> [!NOTE]
> Wrapped controls are not available for C++ Win32 desktop applications. These types of applications must use the [UWP XAML hosting API](#uwp-xaml-hosting-api).

The following wrapped UWP controls are currently available for WPF and Windows Forms applications. More UWP wrapped controls are planned for future releases of the Windows Community Toolkit.

| Control | Minimum supported OS | Description |
|-----------------|-------------------------------|-------------|
| [WebView](https://docs.microsoft.com/windows/communitytoolkit/controls/wpf-winforms/webview) | Windows 10, version 1803 | Uses the Microsoft Edge rendering engine to show web content. |
| [WebViewCompatible](https://docs.microsoft.com/windows/communitytoolkit/controls/wpf-winforms/webviewcompatible) | Windows 7 | Provides a version of **WebView** that is compatible with more OS versions. This control uses the Microsoft Edge rendering engine to show web content on Windows 10 version 1803 and later, and the Internet Explorer rendering engine to show web content on earlier versions of Windows 10, Windows 8.x, and Windows 7. |
| [InkCanvas](https://docs.microsoft.com/windows/communitytoolkit/controls/wpf-winforms/inkcanvas)<br>[InkToolbar](https://docs.microsoft.com/windows/communitytoolkit/controls/wpf-winforms/inktoolbar) | Windows 10, version 1809 (build 17763) | Provide a surface and related toolbars for Windows Ink-based user interaction in your Windows Forms or WPF desktop application. |
| [MediaPlayerElement](https://docs.microsoft.com/windows/communitytoolkit/controls/wpf-winforms/mediaplayerelement) | Windows 10, version 1809 (build 17763) | Embeds a view that streams and renders media content such as video in your Windows Forms or WPF desktop application. |
| [MapControl](https://docs.microsoft.com/en-us/windows/communitytoolkit/controls/wpf-winforms/mapcontrol) | Windows 10, version 1809 (build 17763) | Enables you to display a symbolic or photorealistic map in your Windows Forms or WPF desktop application. |

## Host controls

For scenarios beyond those covered by the available wrapped controls, WPF and Windows Forms applications can also use the [WindowsXamlHost](https://docs.microsoft.com/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) control in the [Windows Community Toolkit](https://docs.microsoft.com/windows/uwpcommunitytoolkit/). This control can host any UWP control that derives from [**Windows.UI.Xaml.UIElement**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement), including any UWP control provided by the Windows SDK as well as custom user controls. This control supports Windows 10 Insider Preview SDK build 17709 and later releases.

> [!NOTE]
> Host controls are not available for C++ Win32 desktop applications. These types of applications must use the [UWP XAML hosting API](#uwp-xaml-hosting-api).

## UWP XAML hosting API

If you have a C++ Win32 application, you can use the *UWP XAML hosting API* to host any UWP control that derives from [**Windows.UI.Xaml.UIElement**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement) in any UI element in your application that has an associated window handle (HWND). This API was introduced in Windows 10 Insider Preview SDK build 17709. For more information about using this API, see [Using the XAML hosting API in a desktop application](using-the-xaml-hosting-api.md).

> [!NOTE]
> C++ Win32 desktop applications must use the UWP XAML hosting API to host UWP controls. Wrapped controls and host controls are not available for these types of applications. For WPF and Windows Forms applications, we recommend that you use the wrapped controls and host controls in the Windows Community Toolkit instead of the UWP XAML hosting API. These controls use the UWP XAML hosting API internally and provide a simpler development experience. However, you can use the UWP XAML hosting API directly in WPF and Windows Forms applications if you choose.

## Architecture overview

Here's a quick look at how these controls are organized architecturally. The names used in this diagram are subject to change.  

![Host control Architecture](images/host-controls.png)

The APIs that appear at the bottom of this diagram ship with the Windows SDK. The controls that you'll add to your designer ship as Nuget packages in the Windows Community Toolkit.

These new controls have limitations so before you use them, please take a moment to review what's not yet supported, and what's functional only with workarounds.

## Limitations

### What's supported

For the most part, everything is supported unless explicitly called out in the list below.

### What's supported only with workarounds

:heavy_check_mark: Hosting multiple inbox controls inside of multiple windows. You'll have to place each window in its own thread.

:heavy_check_mark: Using ``x:Bind`` with hosted controls. You'll have to declare the data model in a .NET Standard library.

:heavy_check_mark: C#-based third-party controls. If you have the source code to a third-party control, you can compile against it.

### What's not yet supported

:no_entry_sign: Accessibility tools that work seamlessly across the application and hosted controls.

:no_entry_sign: Localized content in controls that you add to applications which don't contain a Windows app package.

:no_entry_sign: Asset references made in XAML within applications that don't contain a Windows app package.

:no_entry_sign: Controls properly responding to changes in DPI and scale.

:no_entry_sign: Adding a **WebView** control to a custom user control, (Either on-thread, off-thread, or out of proc).

:no_entry_sign: The [Reveal highlight](https://docs.microsoft.com/windows/uwp/design/style/reveal) Fluent effect.

:no_entry_sign: Inline inking, @Places, and @People for input controls.

:no_entry_sign: Assigning accelerator keys.

:no_entry_sign: C++-based third-party controls.

:no_entry_sign: Hosting custom user controls.

The items in this list will likely change as we continue to improve the experience of bringing Fluent to the desktop.  
