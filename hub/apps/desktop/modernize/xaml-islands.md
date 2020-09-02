---
description: This guide helps you to create Fluent-based UWP UIs directly in your WPF and Windows Forms applications
title: UWP controls in desktop apps
ms.date: 03/23/2020
ms.topic: article
keywords: windows 10, uwp, windows forms, wpf, xaml islands
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: high
ms.custom: 19H1
---

# Host UWP XAML controls in desktop apps (XAML Islands)

Starting in Windows 10, version 1903, you can host UWP controls in non-UWP desktop applications using a feature called *XAML Islands*. This feature enables you to enhance the look, feel, and functionality of your existing WPF, Windows Forms, and C++ Win32 applications with the latest Windows 10 UI features that are only available via UWP controls. This means that you can use UWP features such as [Windows Ink](/windows/uwp/design/input/pen-and-stylus-interactions) and controls that support the [Fluent Design System](/windows/uwp/design/fluent-design-system/index) in your existing WPF, Windows Forms, and C++ Win32 applications.

You can host any UWP control that derives from [Windows.UI.Xaml.UIElement](/uwp/api/windows.ui.xaml.uielement), including:

* Any first-party UWP control provided by the Windows SDK.
* Any custom UWP control (for example, a user control that consists of several UWP controls that work together). You must have the source code for the custom control so you can compile it with your application.

Fundamentally, XAML Islands are created by using the *UWP XAML hosting API*. This API consists of several Windows Runtime classes and COM interfaces that were introduced in the Windows 10, version 1903 SDK. We also provide a set of XAML Island .NET controls in the [Windows Community Toolkit](/windows/uwpcommunitytoolkit/) that use the UWP XAML hosting API internally and provide a more convenient development experience for WPF and Windows Forms apps.

The way you use XAML Islands depends on your application type and the types of UWP controls you want to host.

> [!NOTE]
> If you have feedback about XAML Islands, create a new issue in the [Microsoft.Toolkit.Win32 repo](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/issues) and leave your comments there. If you prefer to submit your feedback privately, you can send it to XamlIslandsFeedback@microsoft.com. Your insights and scenarios are critically important to us.

## Requirements

XAML Islands have these run time requirements:

* Windows 10, version 1903, or a later release.
* If your application is not packaged in an [MSIX package](/windows/msix) for deployment, the computer must have the [Visual C++ Runtime](https://support.microsoft.com/en-us/help/2977003/the-latest-supported-visual-c-downloads) installed.

## WPF and Windows Forms applications

We recommend that WPF and Windows Forms applications use the XAML Island .NET controls that are available in the Windows Community Toolkit. These controls provide an object model that mimics (or provides access to) the properties, methods, and events of the corresponding UWP controls. They also handle behavior such as keyboard navigation and layout changes.

There are two sets of XAML Island controls for WPF and Windows Forms applications: *wrapped controls* and *host controls*. 

### Wrapped controls

WPF and Windows Forms applications can use a selection of XAML Island controls that wrap the interface and functionality of a specific UWP control. You can add these controls directly to the design surface of your WPF or Windows Forms project and then use them like any other WPF or Windows Forms control in the designer.

The following wrapped UWP controls are currently available in the Windows Community Toolkit. 

| Control | Minimum supported OS | Description |
|-----------------|-------------------------------|-------------|
| [InkCanvas](/windows/communitytoolkit/controls/wpf-winforms/inkcanvas)<br>[InkToolbar](/windows/communitytoolkit/controls/wpf-winforms/inktoolbar) | Windows 10, version 1903 | Provide a surface and related toolbars for Windows Ink-based user interaction in your Windows Forms or WPF desktop application. |
| [MediaPlayerElement](/windows/communitytoolkit/controls/wpf-winforms/mediaplayerelement) | Windows 10, version 1903 | Embeds a view that streams and renders media content such as video in your Windows Forms or WPF desktop application. |
| [MapControl](/windows/communitytoolkit/controls/wpf-winforms/mapcontrol) | Windows 10, version 1903 | Enables you to display a symbolic or photorealistic map in your Windows Forms or WPF desktop application. |

For a walkthrough that demonstrates how to use the wrapped UWP controls, see [Host a standard UWP control in a WPF app](host-standard-control-with-xaml-islands.md).

### Host controls

For custom controls and other scenarios beyond those covered by the available wrapped controls, WPF and Windows Forms applications can also use the [WindowsXamlHost](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) control that is available in the Windows Community Toolkit.

| Control | Minimum supported OS | Description |
|-----------------|-------------------------------|-------------|
| [WindowsXamlHost](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) | Windows 10, version 1903 | Can host any UWP control that derives from [Windows.UI.Xaml.UIElement](/uwp/api/windows.ui.xaml.uielement), including any first-party UWP control provided by the Windows SDK as well as custom controls. |

For walkthroughs that demonstrate how to use the **WindowsXamlHost** control, see [Host a standard UWP control in a WPF app](host-standard-control-with-xaml-islands.md) and [Host a custom UWP control in a WPF app using XAML Islands](host-custom-control-with-xaml-islands.md).

> [!NOTE]
> Using the **WindowsXamlHost** control to host custom UWP controls is supported only in WPF and Windows Forms apps that target .NET Core 3. Hosting first-party UWP controls provided by the Windows SDK is supported in apps that target either the .NET Framework or .NET Core 3.

<span id="requirements" />

### Configure your project to use the XAML Island .NET controls

The XAML Island .NET controls require Windows 10, version 1903, or a later version. To use these controls, install one of the NuGet packages listed below. These packages provide everything you need to use the XAML Island wrapped controls and host controls, and they include other related NuGet packages that are also required.

| Type of control | NuGet package  | Related articles |
|-----------------|----------------|---------------------|
| [Wrapped controls](#wrapped-controls) | Version 6.0.0 or later of these packages: <ul><li>WPF: [Microsoft.Toolkit.Wpf.UI.Controls](https://www.nuget.org/packages/Microsoft.Toolkit.Wpf.UI.Controls)</li><li>Windows Forms: [Microsoft.Toolkit.Forms.UI.Controls](https://www.nuget.org/packages/Microsoft.Toolkit.Forms.UI.Controls)</li></ul>  | [Host a standard UWP control in a WPF app](host-standard-control-with-xaml-islands.md)  |
| [Host control](#host-controls) | Version 6.0.0 or later of these packages: <ul><li>WPF: [Microsoft.Toolkit.Wpf.UI.XamlHost](https://www.nuget.org/packages/Microsoft.Toolkit.Wpf.UI.XamlHost)</li><li>Windows Forms: [Microsoft.Toolkit.Forms.UI.XamlHost](https://www.nuget.org/packages/Microsoft.Toolkit.Forms.UI.XamlHost)</li></ul>  | [Host a standard UWP control in a WPF app](host-standard-control-with-xaml-islands.md)<br/>[Host a custom UWP control in a WPF app](host-custom-control-with-xaml-islands.md)  |

Be aware of the following details:

* The host control packages are also included in the wrapped control packages. You can install the wrapped control packages if you want to use both sets of controls.

* If you're hosting a custom UWP control, your WPF or Windows Forms project must target .NET Core 3. Hosting custom UWP controls is not supported in apps that target the .NET Framework. You'll also need to perform some additional steps to reference the custom control. For more info, see [Host a custom UWP control in a WPF app using XAML Islands](host-custom-control-with-xaml-islands.md).

### Web view controls

The Windows Community Toolkit also provides the following .NET controls for hosting web content in WPF and Windows Forms applications. These controls are often used in similar desktop app modernization scenarios as the XAML Island controls, and they are maintained in the same [Microsoft.Toolkit.Win32 repo](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32) repo as the XAML Island controls.

| Control | Minimum supported OS | Description |
|-----------------|-------------------------------|-------------|
| [WebView](/windows/communitytoolkit/controls/wpf-winforms/webview) | Windows 10, version 1803 | Uses the Microsoft Edge rendering engine to show web content. |
| [WebViewCompatible](/windows/communitytoolkit/controls/wpf-winforms/webviewcompatible) | Windows 7 | Provides a version of **WebView** that is compatible with more OS versions. This control uses the Microsoft Edge rendering engine to show web content on Windows 10 version 1803 and later, and the Internet Explorer rendering engine to show web content on earlier versions of Windows 10, Windows 8.x, and Windows 7. |

To use these controls, install one of these NuGet packages:

* WPF: [Microsoft.Toolkit.Wpf.UI.Controls.WebView](https://www.nuget.org/packages/Microsoft.Toolkit.Wpf.UI.Controls.WebView)
* Windows Forms: [Microsoft.Toolkit.Forms.UI.Controls.WebView](https://www.nuget.org/packages/Microsoft.Toolkit.Forms.UI.Controls.WebView)

## C++ Win32 applications

The XAML Island .NET controls are not supported in C++ Win32 applications. These applications must instead use the *UWP XAML hosting API* provided by the Windows 10 SDK (version 1903 and later).

The UWP XAML hosting API consists of several Windows Runtime classes and COM interfaces that your C++ Win32 application can use to host any UWP control that derives from [Windows.UI.Xaml.UIElement](/uwp/api/windows.ui.xaml.uielement). You can host UWP controls in any UI element in your application that has an associated window handle (HWND). For more information about this API, see the following articles.

* [Using the UWP XAML hosting API in a C++ Win32 app](using-the-xaml-hosting-api.md)
* [Host a standard UWP control in a C++ Win32 app](host-standard-control-with-xaml-islands-cpp.md)
* [Host a custom UWP control in a C++ Win32 app](host-custom-control-with-xaml-islands-cpp.md)

> [!NOTE]
> The wrapped controls and host controls in the Windows Community Toolkit use the UWP XAML hosting API internally and implement all of the behavior you would otherwise need to handle yourself if you used the UWP XAML hosting API directly, including keyboard navigation and layout changes. For WPF and Windows Forms applications, we strongly recommend that you use these controls instead of the UWP XAML hosting API directly because they abstract away many of the implementation details of using the API.

## Architecture of XAML Islands

Here's a quick look at how the different types of XAML Island controls are organized architecturally on top of the UWP XAML hosting API.

![Host control Architecture](images/xaml-islands/host-controls.png)

The APIs that appear at the bottom of this diagram ship with the Windows SDK. The wrapped controls and host controls are available via NuGet packages in the Windows Community Toolkit.

## Limitations and workarounds

The following sections discuss limitations and workarounds for certain UWP development scenarios in desktop apps that use XAML Islands. 

### Supported only with workarounds

:heavy_check_mark: Hosting controls from the [WinUI 2.x Library](../../winui/index.md) in a XAML Island is supported conditionally in the current release of XAML Islands. If your desktop app uses an [MSIX package](/windows/msix) for deployment, you can host WinUI controls from prerelease or release versions of the [Microsoft.UI.Xaml](https://www.nuget.org/packages/Microsoft.UI.Xaml) NugGet package. If your desktop app is not packaged using MSIX, you can host WinUI controls only if you install a prerelease version of the [Microsoft.UI.Xaml](https://www.nuget.org/packages/Microsoft.UI.Xaml) NuGet package. Support for hosting controls from the [WinUI 3.0 Library](../../winui/winui3/index.md) is coming in a later release.

:heavy_check_mark: To access the root element of a tree of XAML content in a XAML Island and get related information about the context in which it is hosted, do not use the [CoreWindow](/uwp/api/windows.ui.core.corewindow), [ApplicationView](/uwp/api/windows.ui.viewmanagement.applicationview), and [Window](/uwp/api/windows.ui.xaml.window) classes. Instead, use the [XamlRoot](/uwp/api/windows.ui.xaml.xamlroot) class. For more information, see [this section](#window-host-context-for-xaml-islands).

:heavy_check_mark: To support the [Share contract](/windows/uwp/app-to-app/share-data) from a WPF, Windows Forms, or C++ Win32 app, your app must use the [IDataTransferManagerInterop](/windows/win32/api/shobjidl_core/nn-shobjidl_core-idatatransfermanagerinterop) interface to get the [DataTransferManager](/uwp/api/windows.applicationmodel.datatransfer.datatransfermanager) object to initiate the share operation for a specific window. For a sample that demonstrates how to use this interface in a WPF app, see the [ShareSource sample](https://github.com/microsoft/Windows-classic-samples/tree/master/Samples/ShareSource).

:heavy_check_mark: Using `x:Bind` with hosted controls in XAML Islands is not supported. You'll have to declare the data model in a .NET Standard library.

### Not supported

:no_entry_sign: Using the [WindowsXamlHost](/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) control to host C#-based third-party UWP controls in WPF and Windows Forms apps that target the .NET Framework. This scenario is only supported in apps that target .NET Core 3.

:no_entry_sign: UWP XAML content in XAML Islands doesn't respond to Windows theme changes from dark to light or vice versa at run time. Content does respond to high contrast changes at run time.

:no_entry_sign: Adding a **WebView** control to a custom user control (either on-thread, off-thread, or out of process).

:no_entry_sign: The [MediaPlayer](/uwp/api/Windows.Media.Playback.MediaPlayer) control and [MediaPlayerElement](/windows/communitytoolkit/controls/wpf-winforms/mediaplayerelement) host control are not supported in full screen mode.

:no_entry_sign: Text input with the handwriting view. For more information about this feature, see [this article](/windows/uwp/design/controls-and-patterns/text-handwriting-view).

:no_entry_sign: Text controls that use `@Places` and `@People` content links. For more information about this feature, see [this article](/windows/uwp/design/controls-and-patterns/content-links).

:no_entry_sign: XAML Islands do not support hosting a [ContentDialog](/uwp/api/Windows.UI.Xaml.Controls.ContentDialog) that contains a control that accepts text input, such as a [TextBox](/uwp/api/windows.ui.xaml.controls.textbox), [RichEditBox](/uwp/api/windows.ui.xaml.controls.richeditbox), or [AutoSuggestBox](/uwp/api/windows.ui.xaml.controls.autosuggestbox). If you do this, the input control will not properly respond to key presses. To achieve similar functionality using a XAML Island, we recommend that you host a [Popup](/uwp/api/Windows.UI.Xaml.Controls.Primitives.Popup) that contains the input control.

:no_entry_sign: XAML Islands do not currently support displaying SVG files in a hosted [Windows.UI.Xaml.Controls.Image](/uwp/api/Windows.UI.Xaml.Controls.Image) control or by using an [Windows.UI.Xaml.Media.Imaging.SvgImageSource](/uwp/api/windows.ui.xaml.media.imaging.svgimagesource) object. As a workaround, convert the image files you want to display to raster-based formats such as JPG or PNG.

### Window host context for XAML Islands

When you host XAML Islands in a desktop app, you can have multiple trees of XAML content running on the same thread at the same time. To access the root element of a tree of XAML content in a XAML Island and get related information about the context in which it is hosted, use the [XamlRoot](/uwp/api/windows.ui.xaml.xamlroot) class. The [CoreWindow](/uwp/api/windows.ui.core.corewindow), [ApplicationView](/uwp/api/windows.ui.viewmanagement.applicationview), and [Window](/uwp/api/windows.ui.xaml.window) classes won't provide the correct information for XAML Islands. [CoreWindow](/uwp/api/windows.ui.core.corewindow) and [Window](/uwp/api/windows.ui.xaml.window) objects do exist on the thread and are accessible to your app, but they won't return meaningful bounds or visibility (they are always invisible and have a size of 1x1). For more information, see [Windowing hosts](/windows/uwp/design/layout/show-multiple-views#windowing-hosts).

For example, to get the bounding rectangle of the window that contains a UWP control that is hosted in a XAML Island, use the [XamlRoot.Size](/uwp/api/windows.ui.xaml.xamlroot.size) property of the control. Because every UWP control that can be hosted in a XAML Island derives from [Windows.UI.Xaml.UIElement](/uwp/api/windows.ui.xaml.uielement), you can use the [XamlRoot](/uwp/api/windows.ui.xaml.uielement.xamlroot) property of the control to access the **XamlRoot** object.

```csharp
Size windowSize = myUWPControl.XamlRoot.Size;
```

Do not use the [CoreWindows.Bounds](/uwp/api/windows.ui.core.corewindow.bounds) property to get the bounding rectangle.

```csharp
// This will return incorrect information for a UWP control that is hosted in a XAML Island.
Rect windowSize = CoreWindow.GetForCurrentThread().Bounds;
```

For a table of common windowing-related APIs that you should avoid in the context of XAML Islands and the recommended [XamlRoot](/uwp/api/windows.ui.xaml.xamlroot) replacements, see the table in [this section](/windows/uwp/design/layout/show-multiple-views#make-code-portable-across-windowing-hosts).

For a sample that demonstrates how to use this interface in a WPF app, see the [ShareSource](https://github.com/microsoft/Windows-classic-samples/tree/master/Samples/ShareSource) sample.

## Feature roadmap

Here is the current state of XAML Islands-related features:

* **C++ Win32 apps:** The UWP XAML hosting API is considered version 1.0 as of Windows 10, version 1903.
* **Managed apps that target .NET Framework 4.6.2 and later:** The XAML Island controls that are available in the [version 6.0.0 NuGet packages](#configure-your-project-to-use-the-xaml-island-net-controls) are considered version 1.0 for apps that target the .NET Framework 4.6.2 and later.
* **Managed apps that target .NET Core 3.0 and later:** The controls that are available in the [version 6.0.0 NuGet packages](#configure-your-project-to-use-the-xaml-island-net-controls) are still in developer preview for apps that target the .NET Core 3.0 and later. The version 1.0 release of these controls for .NET Core 3.0 and later are planned for a later release.

## Additional resources

For more background information and tutorials about using XAML Islands, see the following articles and resources:

* [Modernize a WPF app tutorial](modernize-wpf-tutorial.md): This tutorial provides step-by-step instructions for using the wrapped controls and host controls in the Windows Community Toolkit to add UWP controls to an existing WPF line-of-business application. This tutorial includes the complete code for the WPF application as well as detailed instructions for each step in the process.
* [XAML Islands code samples](https://github.com/microsoft/Xaml-Islands-Samples): This repo contains Windows Forms, WPF, and C++/Win32 samples that demonstrate how to use XAML Islands.
* [XAML Islands v1 - Updates and Roadmap](https://blogs.windows.com/windowsdeveloper/2019/06/13/xaml-islands-v1-updates-and-roadmap): This blog post discusses many common questions about XAML Islands and provides a detailed development roadmap.