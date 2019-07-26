---
description: This guide helps you to create Fluent-based UWP UIs directly in your WPF and Windows Forms applications
title: UWP controls in desktop apps
ms.date: 07/17/2019
ms.topic: article
keywords: windows 10, uwp, windows forms, wpf, xaml islands
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
ms.custom: RS5, 19H1
---

# Host UWP XAML controls in desktop apps (XAML Islands)

Starting in Windows 10, version 1903, you can host UWP controls in non-UWP desktop applications using a feature called *XAML Islands*. This feature enables you to enhance the look, feel, and functionality of your existing desktop applications with the latest Windows 10 UI features that are only available via UWP controls. This means that you can use UWP features such as [Windows Ink](/windows/uwp/design/input/pen-and-stylus-interactions) and controls that support the [Fluent Design System](/windows/uwp/design/fluent-design-system/index) in your existing WPF, Windows Forms, and C++ Win32 applications.

We provide several ways to use XAML Islands in your WPF, Windows Forms, and C++ Win32 applications, depending on the technology or framework you are using. 

> [!NOTE]
> If you have feedback about XAML Islands, create a new issue in the [Microsoft.Toolkit.Win32 repo](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/issues) and leave your comments there. If you prefer to submit your feedback privately, you can send it to XamlIslandsFeedback@microsoft.com. Your insights and scenarios are critically important to us.

## How do XAML Islands work?

Starting in Windows 10, version 1903, we provide two ways to use XAML Islands in your WPF, Windows Forms, and C++ Win32 applications:

* The Windows SDK provides several Windows Runtime classes and COM interfaces that your application can use to host any UWP control that derives from [**Windows.UI.Xaml.UIElement**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement). Collectively, these classes and interfaces are called the *UWP XAML hosting API*, and they enable you to host UWP controls in any UI element in your application that has an associated window handle (HWND). For more information about this API, see [Using the XAML hosting API](using-the-xaml-hosting-api.md).

* The [Windows Community Toolkit](https://docs.microsoft.com/windows/uwpcommunitytoolkit/) also provides additional XAML Island controls for WPF and Windows Forms. These controls use the UWP XAML hosting API internally and implement all of the behavior you would otherwise need to handle yourself if you used the UWP XAML hosting API directly, including keyboard navigation and layout changes. For WPF and Windows Forms applications, we strongly recommend that you use these controls instead of the UWP XAML hosting API directly because they abstract away many of the implementation details of using the API. Note that as of Windows 10, version 1903, these controls are [available as a developer preview](#feature-roadmap).

> [!NOTE]
> C++ Win32 desktop applications must use the UWP XAML hosting API to host UWP controls. The XAML Island controls in the Windows Community Toolkit are not available for C++ Win32 desktop applications.

There are two types of XAML Island controls provided by the Windows Community Toolkit for WPF and Windows Forms applications: *wrapped controls* and *host controls*. 

### Wrapped controls

WPF and Windows Forms applications can use a selection of wrapped UWP controls in the [Windows Community Toolkit](https://docs.microsoft.com/windows/uwpcommunitytoolkit/). These are called *wrapped controls* because they wrap the interface and functionality of a specific UWP control. You can add these controls directly to the design surface of your WPF or Windows Forms project and then use them like any other WPF or Windows Forms control in your designer.

The following wrapped UWP controls for implementing XAML Islands are currently available for WPF (see the [Microsoft.Toolkit.Wpf.UI.Controls](https://www.nuget.org/packages/Microsoft.Toolkit.Wpf.UI.Controls) package) and Windows Forms applications (see the [Microsoft.Toolkit.Forms.UI.Controls](https://www.nuget.org/packages/Microsoft.Toolkit.Forms.UI.Controls) package).

| Control | Minimum supported OS | Description |
|-----------------|-------------------------------|-------------|
| [InkCanvas](https://docs.microsoft.com/windows/communitytoolkit/controls/wpf-winforms/inkcanvas)<br>[InkToolbar](https://docs.microsoft.com/windows/communitytoolkit/controls/wpf-winforms/inktoolbar) | Windows 10, version 1903 | Provide a surface and related toolbars for Windows Ink-based user interaction in your Windows Forms or WPF desktop application. |
| [MediaPlayerElement](https://docs.microsoft.com/windows/communitytoolkit/controls/wpf-winforms/mediaplayerelement) | Windows 10, version 1903 | Embeds a view that streams and renders media content such as video in your Windows Forms or WPF desktop application. |
| [MapControl](https://docs.microsoft.com/windows/communitytoolkit/controls/wpf-winforms/mapcontrol) | Windows 10, version 1903 | Enables you to display a symbolic or photorealistic map in your Windows Forms or WPF desktop application. |

In addition to the wrapped controls for XAML Islands, the Windows Community Toolkit also provides the following controls for hosting web content in WPF (see the [Microsoft.Toolkit.Wpf.UI.Controls.WebView](https://www.nuget.org/packages/Microsoft.Toolkit.Wpf.UI.Controls.WebView) package) and Windows Forms applications (see the [Microsoft.Toolkit.Forms.UI.Controls.WebView](https://www.nuget.org/packages/Microsoft.Toolkit.Forms.UI.Controls.WebView) package).

| Control | Minimum supported OS | Description |
|-----------------|-------------------------------|-------------|
| [WebView](https://docs.microsoft.com/windows/communitytoolkit/controls/wpf-winforms/webview) | Windows 10, version 1803 | Uses the Microsoft Edge rendering engine to show web content. |
| [WebViewCompatible](https://docs.microsoft.com/windows/communitytoolkit/controls/wpf-winforms/webviewcompatible) | Windows 7 | Provides a version of **WebView** that is compatible with more OS versions. This control uses the Microsoft Edge rendering engine to show web content on Windows 10 version 1803 and later, and the Internet Explorer rendering engine to show web content on earlier versions of Windows 10, Windows 8.x, and Windows 7. |

### Host controls

For scenarios beyond those covered by the available wrapped controls, WPF and Windows Forms applications can also use the [WindowsXamlHost](https://docs.microsoft.com/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) control in the [Windows Community Toolkit](https://docs.microsoft.com/windows/uwpcommunitytoolkit/). This control can host any UWP control that derives from [**Windows.UI.Xaml.UIElement**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement), including any UWP control provided by the Windows SDK as well as custom user controls. This control supports Windows 10 Insider Preview SDK build 17709 and later releases.

These controls are available in the [Microsoft.Toolkit.Wpf.UI.XamlHost](https://www.nuget.org/packages/Microsoft.Toolkit.Wpf.UI.XamlHost) (for WPF) and [Microsoft.Toolkit.Forms.UI.XamlHost](https://www.nuget.org/packages/Microsoft.Toolkit.Forms.UI.XamlHost) (for Windows Forms) packages. These packages are included in the [Microsoft.Toolkit.Wpf.UI.Controls](https://www.nuget.org/packages/Microsoft.Toolkit.Wpf.UI.Controls) and [Microsoft.Toolkit.Forms.UI.Controls](https://www.nuget.org/packages/Microsoft.Toolkit.Forms.UI.Controls) packages that contain the wrapped controls.

### Architecture overview

Here's a quick look at how these controls are organized architecturally.

![Host control Architecture](images/xaml-islands/host-controls.png)

The APIs that appear at the bottom of this diagram ship with the Windows SDK. The wrapped controls and host controls are available via Nuget packages in the Windows Community Toolkit.

<span id="requirements" />

## Configure your project to use XAML Islands

XAML Islands require Windows 10, version 1903, and later. To use XAML Islands in your application, you must first set up your project:

1. Modify your project to use Windows Runtime APIs. For instructions, see [this article](desktop-to-uwp-enhance.md#set-up-your-project).
2. Install one of these NuGet packages in your project. Make sure you install version 6.0.0-preview6.4 or a later version of the package.
  * WPF: Install [Microsoft.Toolkit.Wpf.UI.Controls](https://www.nuget.org/packages/Microsoft.Toolkit.Wpf.UI.Controls)
  * Windows Forms: [Microsoft.Toolkit.Forms.UI.Controls](https://www.nuget.org/packages/Microsoft.Toolkit.Forms.UI.Controls)
  * C++/Win32: [Microsoft.Toolkit.Win32.UI.XamlApplication](https://www.nuget.org/packages/Microsoft.Toolkit.Win32.UI.XamlApplication)

> [!NOTE]
> Earlier versions of these instructions had you add the **maxversiontested** element to an application manifest in your project. As of the latest preview versions of the NuGet packages, you no longer need to add this element to your manifest.

## Feature roadmap

As of the release of Windows 10, version 1903, the wrapped controls and host controls in the Windows Community Toolkit are still in developer preview until the version 1.0 release of the controls is available.

* Version 1.0 of the controls for the .NET Framework 4.6.2 and later are planned to be released in the [6.0 release of the toolkit](https://github.com/windows-toolkit/WindowsCommunityToolkit/milestones).
* Version 1.0 of the controls for .NET Core 3 are planned for a later release of the toolkit.
* If you want to try the latest previews of the version 1.0 releases of these controls for the .NET Framework and .NET Core 3, see the **6.0.0-preview6.4** NuGet packages in the [UWP Community Toolkit](https://dotnet.myget.org/gallery/uwpcommunitytoolkit) gallery.

For more details, see [this blog post](https://blogs.windows.com/windowsdeveloper/2019/06/13/xaml-islands-v1-updates-and-roadmap).

## Additional resources

For more background information and tutorials about using XAML Islands, see the following articles and resources:

* [Modernize a WPF app tutorial](modernize-wpf-tutorial.md): This tutorial provides step-by-step instructions for using the wrapped controls and host controls in the Windows Community Toolkit to add UWP controls to an existing WPF line-of-business application. This tutorial includes the complete code for the WPF application as well as detailed instructions for each step in the process.
* [XAML Islands v1 - Updates and Roadmap](https://blogs.windows.com/windowsdeveloper/2019/06/13/xaml-islands-v1-updates-and-roadmap): This blog post discusses many common questions about XAML Islands and provides a detailed development roadmap.
