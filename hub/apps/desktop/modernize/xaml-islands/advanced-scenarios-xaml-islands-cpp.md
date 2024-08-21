---
description: This article discusses advanced XAML Island hosting scenarios for C++ desktop (Win32) apps.
title: Advanced scenarios for XAML Islands in C++ desktop (Win32) apps
ms.date: 03/23/2020
ms.topic: article
keywords: windows 10, uwp, cpp, win32, xaml islands, wrapped controls, standard controls
ms.localizationpriority: medium
ms.custom: 19H1
---

# Advanced scenarios for XAML Islands in C++ desktop (Win32) apps

> [!IMPORTANT]
> This topic uses or mentions types from the [CommunityToolkit/Microsoft.Toolkit.Win32](https://github.com/CommunityToolkit/Microsoft.Toolkit.Win32) GitHub repo. For important info about XAML Islands support, please see the [XAML Islands Notice](https://github.com/CommunityToolkit/Microsoft.Toolkit.Win32#xaml-islands-notice) in that repo.

The [host a standard UWP control](host-standard-control-with-xaml-islands-cpp.md) and [host a custom UWP control](host-custom-control-with-xaml-islands-cpp.md) articles provide instructions and examples for hosting XAML Islands in a C++ desktop (Win32) app. However, the code examples in these articles do not handle many advanced scenarios that desktop applications may need to handle to provide a smooth user experience. This article provides guidance for some of these scenarios and pointers to related code samples.

## Keyboard input

To properly handle keyboard input for each XAML Island, your application must pass all Windows messages to the UWP XAML framework so that certain messages can be processed correctly. To do this, in some place in your application that can access the message loop, cast the **DesktopWindowXamlSource** object for each XAML Island to an **IDesktopWindowXamlSourceNative2** COM interface. Then, call the **PreTranslateMessage** method of this interface and pass in the current message.

  * **C++ desktop (Win32):**: The app can call **PreTranslateMessage** directly in its main message loop. For an example, see the [XamlBridge.cpp](https://github.com/microsoft/Xaml-Islands-Samples/blob/master/Samples/Win32/SampleCppApp/XamlBridge.cpp#L16) file.

  * **WPF:** The app can call **PreTranslateMessage** from the event handler for the [ComponentDispatcher.ThreadFilterMessage](/dotnet/api/system.windows.interop.componentdispatcher.threadfiltermessage) event. For an example, see the [WindowsXamlHostBase.Focus.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Wpf.UI.XamlHost/WindowsXamlHostBase.Focus.cs#L177) file in the Windows Community Toolkit.

  * **Windows Forms:** The app can call **PreTranslateMessage** from an override for the [Control.PreprocessMessage](/dotnet/api/system.windows.forms.control.preprocessmessage) method. For an example, see the [WindowsXamlHostBase.KeyboardFocus.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Forms.UI.XamlHost/WindowsXamlHostBase.KeyboardFocus.cs#L100) file in the Windows Community Toolkit.

## Keyboard focus navigation

When the user navigates through the UI elements in your application using the keyboard (for example, by pressing **Tab** or direction/arrow key), you'll need to programmatically move focus into and out of the **DesktopWindowXamlSource** object. When the user's keyboard navigation reaches the **DesktopWindowXamlSource**, move focus into the first [Windows.UI.Xaml.UIElement](/uwp/api/windows.ui.xaml.uielement) object in the navigation order for your UI, continue to move focus to the following **Windows.UI.Xaml.UIElement** objects as the user cycles through the elements, and then move focus back out of the **DesktopWindowXamlSource** and into the parent UI element.  

The UWP XAML hosting API provides several types and members to help you accomplish these tasks.

* When the keyboard navigation enters your **DesktopWindowXamlSource**, the [GotFocus](/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource.gotfocus) event is raised. Handle this event and programmatically move focus to the first hosted **Windows.UI.Xaml.UIElement** by using the [NavigateFocus](/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource.navigatefocus) method.

* When the user is on the last focusable element in your **DesktopWindowXamlSource** and presses the **Tab** key or an arrow key, the [TakeFocusRequested](/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource.takefocusrequested) event is raised. Handle this event and programmatically move focus to the next focusable element in the host application. For example, in a WPF application where the **DesktopWindowXamlSource** is hosted in a [System.Windows.Interop.HwndHost](/dotnet/api/system.windows.interop.hwndhost), you can use the [MoveFocus](/dotnet/api/system.windows.frameworkelement.movefocus) method to transfer focus to the next focusable element in the host application.

For examples that demonstrate how to do this in the context of a working sample application, see the following code files:

  * **C++ desktop (Win32)**: See the [XamlBridge.cpp](https://github.com/microsoft/Xaml-Islands-Samples/blob/master/Samples/Win32/SampleCppApp/XamlBridge.cpp) file.

  * **WPF:** See the [WindowsXamlHostBase.Focus.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Wpf.UI.XamlHost/WindowsXamlHostBase.Focus.cs) file in the Windows Community Toolkit.  

  * **Windows Forms:** See the [WindowsXamlHostBase.KeyboardFocus.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Forms.UI.XamlHost/WindowsXamlHostBase.KeyboardFocus.cs) file in the Windows Community Toolkit.

## Handle layout changes

When the user changes the size of the parent UI element, you'll need to handle any necessary layout changes to make sure your UWP controls display as you expect. Here are some important scenarios to consider.

* In a C++ desktop application, when your application handles the WM_SIZE message it can reposition the hosted XAML Island by using the [SetWindowPos](/windows/desktop/api/winuser/nf-winuser-setwindowpos) function. For an example, see the [SampleApp.cpp](https://github.com/microsoft/Xaml-Islands-Samples/blob/master/Samples/Win32/SampleCppApp/SampleApp.cpp#L170) code file.

* When the parent UI element needs to get the size of the rectangular area needed to fit the **Windows.UI.Xaml.UIElement** that you are hosting on the **DesktopWindowXamlSource**, call the [Measure](/uwp/api/windows.ui.xaml.uielement.measure) method of the **Windows.UI.Xaml.UIElement**. For example:

    * In a WPF application you might do this from the [MeasureOverride](/dotnet/api/system.windows.frameworkelement.measureoverride) method of the [HwndHost](/dotnet/api/system.windows.interop.hwndhost) that hosts the **DesktopWindowXamlSource**. For an example, see the [WindowsXamlHostBase.Layout.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Wpf.UI.XamlHost/WindowsXamlHostBase.Layout.cs) file in the Windows Community Toolkit.

    * In a Windows Forms application you might do this from the [GetPreferredSize](/dotnet/api/system.windows.forms.control.getpreferredsize) method of the [Control](/dotnet/api/system.windows.forms.control) that hosts the **DesktopWindowXamlSource**. For an example, see the [WindowsXamlHostBase.Layout.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Forms.UI.XamlHost/WindowsXamlHostBase.Layout.cs) file in the Windows Community Toolkit.

* When the size of the parent UI element changes, call the [Arrange](/uwp/api/windows.ui.xaml.uielement.arrange) method of the root **Windows.UI.Xaml.UIElement** that you are hosting on the **DesktopWindowXamlSource**. For example:

    * In a WPF application you might do this from the [ArrangeOverride](/dotnet/api/system.windows.frameworkelement.arrangeoverride) method of the [HwndHost](/dotnet/api/system.windows.interop.hwndhost) object that hosts the **DesktopWindowXamlSource**. For an example, see the [WindowsXamlHost.Layout.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Wpf.UI.XamlHost/WindowsXamlHostBase.Layout.cs) file in the Windows Community Toolkit.

    * In a Windows Forms application you might do this from the handler for the [SizeChanged](/dotnet/api/system.windows.forms.control.sizechanged) event of the [Control](/dotnet/api/system.windows.forms.control) that hosts the **DesktopWindowXamlSource**. For an example, see the [WindowsXamlHost.Layout.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Forms.UI.XamlHost/WindowsXamlHostBase.Layout.cs) file in the Windows Community Toolkit.

## Handle DPI changes

The UWP XAML framework handles DPI changes for hosted UWP controls automatically (for example, when the user drags the window between monitors with different screen DPI). For the best experience, we recommend that your Windows Forms, WPF, or C++ desktop application is configured to be per-monitor DPI aware.

To configure your application to be per-monitor DPI aware, add a [side-by-side assembly manifest](/windows/desktop/SbsCs/application-manifests) to your project and set the **\<dpiAwareness\>** element to **PerMonitorV2**. For more information about this value, see the description for [DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2](/windows/desktop/hidpi/dpi-awareness-context).

```xml
<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<assembly xmlns="urn:schemas-microsoft-com:asm.v1" manifestVersion="1.0">
    <application xmlns="urn:schemas-microsoft-com:asm.v3">
        <windowsSettings>
            <dpiAwareness xmlns="http://schemas.microsoft.com/SMI/2016/WindowsSettings">PerMonitorV2</dpiAwareness>
        </windowsSettings>
    </application>
</assembly>
```

## Related topics

* [Host UWP XAML controls in desktop apps (XAML Islands)](xaml-islands.md)
* [Using the UWP XAML hosting API in a C++ desktop app](using-the-xaml-hosting-api.md)
* [Host a standard UWP control in a C++ desktop app](host-standard-control-with-xaml-islands-cpp.md)
* [Host a custom UWP control in a C++ desktop app](host-custom-control-with-xaml-islands-cpp.md)
* [XAML Islands code samples](https://github.com/microsoft/Xaml-Islands-Samples)
* [C++ desktop XAML Islands sample](https://github.com/microsoft/Xaml-Islands-Samples/tree/master/Samples/Win32/SampleCppApp)