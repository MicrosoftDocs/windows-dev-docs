---
description: This article describes how to host UWP XAML UI in your desktop application.
title: Using the UWP XAML hosting API in a desktop application
ms.date: 07/26/2019
ms.topic: article
keywords: windows 10, uwp, windows forms, wpf, win32, xaml islands
ms.author: mcleans
author: mcleanbyron
ms.localizationpriority: medium
ms.custom: 19H1
---

# Using the UWP XAML hosting API in a desktop application

Starting in Windows 10, version 1903, non-UWP desktop applications (including WPF, Windows Forms, and C++ Win32 applications) can use the *UWP XAML hosting API* to host UWP controls in any UI element that is associated with a window handle (HWND). This API enables non-UWP desktop applications to use the latest Windows 10 UI features that are only available via UWP controls. For example, non-UWP desktop applications can use this API to host UWP controls that use the [Fluent Design System](/windows/uwp/design/fluent-design-system/index) and support [Windows Ink](/windows/uwp/design/input/pen-and-stylus-interactions).

The UWP XAML hosting API provides the foundation for a broader set of controls that we are providing to enable developers to bring Fluent UI to non-UWP desktop applications. This feature is called *XAML Islands*. For an overview of this feature, see [UWP controls in desktop applications](xaml-islands.md).

> [!NOTE]
> If you have feedback about XAML Islands, create a new issue in the [Microsoft.Toolkit.Win32 repo](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/issues) and leave your comments there. If you prefer to submit your feedback privately, you can send it to XamlIslandsFeedback@microsoft.com. Your insights and scenarios are critically important to us.

## Should you use the UWP XAML hosting API?

The UWP XAML hosting API provides the low-level infrastructure for hosting UWP controls in desktop applications. Some types of desktop applications have the option of using alternative, more convenient APIs to accomplish this goal.  

* If you have a C++ Win32 desktop application and you want to host UWP controls in your application, you must use the UWP XAML hosting API. There are no alternatives for these types of applications.

* For WPF and Windows Forms applications, we strongly recommend that you use the [wrapped controls](xaml-islands.md#wrapped-controls) and [host controls](xaml-islands.md#host-controls) in the Windows Community Toolkit instead of using the UWP XAML hosting API directly. These controls use the UWP XAML hosting API internally and implement all of the behavior you would otherwise need to handle yourself if you used the UWP XAML hosting API directly, including keyboard navigation and layout changes. However, you can use the UWP XAML hosting API directly in these types of applications if you choose.

This article describes how to use the UWP XAML hosting API directly in your application instead of the controls provided by the Windows Community Toolkit.

## Prerequisites

The UWP XAML hosting API has these prerequisites:

* Windows 10, version 1903 (or later) and the corresponding build of the Windows SDK.
* Configure your project to use Windows Runtime APIs and enable XAML Islands by following [these instructions](xaml-islands.md#requirements).

## Architecture of XAML Islands

The UWP XAML hosting API includes these main Windows Runtime types and COM interfaces:

* [**WindowsXamlManager**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.windowsxamlmanager). This class represents the UWP XAML framework. This class provides a single static [**InitializeForCurrentThread**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.windowsxamlmanager.initializeforcurrentthread) method that initializes the UWP XAML framework on the current thread in the desktop application.

* [**DesktopWindowXamlSource**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource). This class represents an instance of UWP XAML content that you are hosting in your desktop application. The most important member of this class is the [**Content**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource.content) property. You assign this property to a [**Windows.UI.Xaml.UIElement**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement) that you want to host. This class also has other members for routing keyboard focus navigation into and out of the XAML Islands.

* **IDesktopWindowXamlSourceNative** and **IDesktopWindowXamlSourceNative2** COM interfaces. **IDesktopWindowXamlSourceNative** provides the **AttachToWindow** method, which you use to attach a XAML Island in your application to a parent UI element. **IDesktopWindowXamlSourceNative2** provides the **PreTranslateMessage** method, which enables the UWP XAML framework to process certain Windows messages correctly.
    > [!NOTE]
    > These interfaces are declared in the **windows.ui.xaml.hosting.desktopwindowxamlsource.h** header file in the Windows SDK. By default, this file is in %programfiles(x86)%\Windows Kits\10\Include\\<build number\>\um. In a C++ Win32 project, you can reference this header file directly. In a WPF or Windows Forms project, you'll need to declare the interfaces in your application code using the [**ComImport**](https://docs.microsoft.com/dotnet/api/system.runtime.interopservices.comimportattribute) attribute. Make sure your interface declarations exactly match the declarations in **windows.ui.xaml.hosting.desktopwindowxamlsource.h**.

The following diagram illustrates the hierarchy of objects in a XAML Island that is hosted in a desktop application.

![DesktopWindowXamlSource architecture](images/xaml-islands/xaml-hosting-api-rev2.png)

* At the base level is the UI element in your application where you want to host the XAML Island. This UI element must have a window handle (HWND). Examples of UI elements in which you can host a XAML Island include [**System.Windows.Interop.HwndHost**](https://docs.microsoft.com/dotnet/api/system.windows.interop.hwndhost) for WPF applications, [**System.Windows.Forms.Control**](https://docs.microsoft.com/dotnet/api/system.windows.forms.control) for Windows Forms applications, and a [window](https://docs.microsoft.com/windows/desktop/winmsg/about-windows) for C++ Win32 applications.

* At the next level is a **DesktopWindowXamlSource** object. This object provides the infrastructure for hosting the XAML Island. Your code is responsible for creating this object and attaching it to the parent UI element.

* When you create a **DesktopWindowXamlSource**, this object automatically creates a native child window to host your UWP control. This native child window is mostly abstracted away from your code, but you can access its handle (HWND) if necessary.

* Finally, at the top level is the UWP control you want to host in your desktop application. This can be any UWP object that derives from [**Windows.UI.Xaml.UIElement**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement), including any UWP control provided by the Windows SDK as well as custom user controls.

## Related samples

The way you use the UWP XAML hosting API in your code depends on your application type, the design of your application, and other factors. To help illustrate how to use this API in the context of a complete application, this article refers to code from the following samples.

### C++ Win32

[C++ Win32 Sample](https://github.com/marb2000/XamlIslands/tree/master/1903_Samples/CppWinRT_Win32_App). This sample demonstrates a complete implementation of hosting a UWP user control in an unpackaged C++ Win32 application (that is, an application that is not built into an MSIX package).

### WPF and Windows Forms

The [WindowsXamlHost](https://docs.microsoft.com/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost) control in the Windows Community Toolkit acts as a reference sample for using the UWP hosting API in WPF and Windows Forms applications. The source code is available at the following locations:

  * For the WPF version of the control, [go here](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/tree/master/Microsoft.Toolkit.Wpf.UI.XamlHost). The WPF version derives from [**System.Windows.Interop.HwndHost**](https://docs.microsoft.com/dotnet/api/system.windows.interop.hwndhost).

  * For the Windows Forms version of the control, [go here](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/tree/master/Microsoft.Toolkit.Forms.UI.XamlHost). The Windows Forms version derives from [**System.Windows.Forms.Control**](https://docs.microsoft.com/dotnet/api/system.windows.forms.control).

## Host UWP XAML controls

Here are the main steps for using the UWP XAML hosting API to host a UWP control in your application.

1. Initialize the UWP XAML framework for the current thread before your application creates any of the [**Windows.UI.Xaml.UIElement**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement) objects that it will host in the [**DesktopWindowXamlSource**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource).

    * If your application creates the **DesktopWindowXamlSource** object before it creates any of the **Windows.UI.Xaml.UIElement** objects, this framework will be initialized for you when you instantiate the **DesktopWindowXamlSource** object. In this scenario, you don't need to add any code of your own to initialize the framework.

    * However, if your application creates the **Windows.UI.Xaml.UIElement** objects before it creates the **DesktopWindowXamlSource** object that will host them, your application must call the static [**WindowsXamlManager.InitializeForCurrentThread**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.windowsxamlmanager.initializeforcurrentthread) method to explicitly initialize the UWP XAML framework before the **Windows.UI.Xaml.UIElement** objects are instantiated. Your application should typically call this method when the parent UI element that hosts the **DesktopWindowXamlSource** is instantiated.

    ```cppwinrt
    Windows::UI::Xaml::Hosting::WindowsXamlManager windowsXamlManager =
        Windows::UI::Xaml::Hosting::WindowsXamlManager::InitializeForCurrentThread();
    ```

    ```csharp
    global::Windows.UI.Xaml.Hosting.WindowsXamlManager windowsXamlManager =
        global::Windows.UI.Xaml.Hosting.WindowsXamlManager.InitializeForCurrentThread();
    ```

    > [!NOTE]
    > This method returns a [**WindowsXamlManager**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.windowsxamlmanager) object that contains a reference to the UWP XAML framework. You can create as many **WindowsXamlManager** objects as you want on a given thread. However, because each object holds a reference to the UWP XAML framework, you should dispose the objects to ensure that XAML resources are eventually released.

2. Create a [**DesktopWindowXamlSource**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource) object and attach it to a parent UI element in your application that is associated with a window handle.

    To do this, you'll need to follow these steps:

    1. Create a **DesktopWindowXamlSource** object and cast it to the **IDesktopWindowXamlSourceNative** or **IDesktopWindowXamlSourceNative2** COM interface.

    2. Call the **AttachToWindow** method of the **IDesktopWindowXamlSourceNative** or **IDesktopWindowXamlSourceNative2** interface, and pass in the window handle of the parent UI element in your application.

    3. Set the initial size of the internal child window contained in the **DesktopWindowXamlSource**. By default, this internal child window is set to a width and height of 0. If you don't set the size of the window, any UWP controls you add to the **DesktopWindowXamlSource** will not be visible. To access the internal child window in the **DesktopWindowXamlSource**, use the **WindowHandle** property of the **IDesktopWindowXamlSourceNative** or **IDesktopWindowXamlSourceNative2** interface. The following examples use the [SetWindowPos](https://docs.microsoft.com/windows/desktop/api/winuser/nf-winuser-setwindowpos) function to set the size of the window.

    Here are some code examples that demonstrate this process.

    ```cppwinrt
    // This example assumes you already have an HWND variable named 'parentHwnd' that
    // contains the handle of the parent window.
    Windows::UI::Xaml::Hosting::DesktopWindowXamlSource desktopWindowXamlSource;
    auto interop = desktopWindowXamlSource.as<IDesktopWindowXamlSourceNative>();
    check_hresult(interop->AttachToWindow(parentHwnd));

    HWND childInteropHwnd = nullptr;
    interop->get_WindowHandle(&childInteropHwnd);

    SetWindowPos(childInteropHwnd, 0, 0, 0, 300, 300, SWP_SHOWWINDOW);
    ```

    ```csharp
    // This WPF example assumes you already have an HwndHost named 'parentHwndHost'
    // that will act as the parent UI element for your XAML Island. It also assumes
    // you have used the DllImport attribute to import SetWindowPos from user32.dll
    // as a static method into a class named NativeMethods.
    Windows.UI.Xaml.Hosting.DesktopWindowXamlSource desktopWindowXamlSource =
        new Windows.UI.Xaml.Hosting.DesktopWindowXamlSource();

    IntPtr iUnknownPtr = System.Runtime.InteropServices.Marshal.GetIUnknownForObject(
        desktopWindowXamlSource);
    IDesktopWindowXamlSourceNative desktopWindowXamlSourceNative =
        System.Runtime.InteropServices.Marshal.Marshal.GetTypedObjectForIUnknown(
            iUnknownPtr, typeof(IDesktopWindowXamlSourceNative))
            as IDesktopWindowXamlSourceNative;

    desktopWindowXamlSourceNative.AttachToWindow(parentHwndHost.Handle);

    var childInteropHwnd = desktopWindowXamlSourceNative.WindowHandle;
    NativeMethods.SetWindowPos(childInteropHwnd, HWND_TOP, 0, 0, 300, 300, SWP_SHOWWINDOW);
    ```

3. Set the **Windows.UI.Xaml.UIElement** you want to host to the [**Content**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource.content) property of your **DesktopWindowXamlSource** object. The following example sets a [**Windows.UI.Xaml.Controls.Grid**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.grid) named ```myGrid``` to the **Content** property.

   ```cppwinrt
   desktopWindowXamlSource.Content(myGrid);
   ```

   ```csharp
   desktopWindowXamlSource.Content = myGrid;
   ```

For complete examples that demonstrate these tasks in the context of a working sample application, see the following code files:

  * **C++ Win32:** See the [XamlBridge.cpp](https://github.com/marb2000/XamlIslands/blob/master/1903_Samples/CppWinRT_Win32_App/SampleCppApp/XamlBridge.cpp) file in the [C++ Win32 sample](https://github.com/marb2000/XamlIslands/tree/master/1903_Samples/CppWinRT_Win32_App).

  * **WPF:** See the [WindowsXamlHostBase.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Wpf.UI.XamlHost/WindowsXamlHostBase.cs) and  [WindowsXamlHost.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Wpf.UI.XamlHost/WindowsXamlHost.cs) files in the Windows Community Toolkit.  

  * **Windows Forms:** See the [WindowsXamlHostBase.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Forms.UI.XamlHost/WindowsXamlHostBase.cs) and  [WindowsXamlHost.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Forms.UI.XamlHost/WindowsXamlHost.cs) files in the Windows Community Toolkit.

## Handle keyboard input and focus navigation

There are several things your app needs to do to properly handle keyboard input and focus navigation when it hosts XAML Islands.

### Keyboard input

To properly handle keyboard input for each XAML Island, your application must pass all Windows messages to the UWP XAML framework so that certain messages can be processed correctly. To do this, in some place in your application that can access the message loop, cast the **DesktopWindowXamlSource** object for each XAML Island to an **IDesktopWindowXamlSourceNative2** COM interface. Then, call the **PreTranslateMessage** method of this interface and pass in the current message.

  * A C++ Win32 application can call **PreTranslateMessage** directly in its main message loop. For an example, see the [XamlBridge.cpp](https://github.com/marb2000/XamlIslands/blob/master/1903_Samples/CppWinRT_Win32_App/SampleCppApp/XamlBridge.cpp#L6) file in the [C++ Win32 sample](https://github.com/marb2000/XamlIslands/tree/master/1903_Samples/CppWinRT_Win32_App).

  * A WPF application can call **PreTranslateMessage** from the event handler for the [**ComponentDispatcher.ThreadFilterMessage**](https://docs.microsoft.com/dotnet/api/system.windows.interop.componentdispatcher.threadfiltermessage?view=netframework-4.7.2) event. For an example, see the [WindowsXamlHostBase.Focus.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Wpf.UI.XamlHost/WindowsXamlHostBase.Focus.cs#L177) file in the Windows Community Toolkit.

  * A Windows Forms application can call **PreTranslateMessage** from an override for the [**Control.PreprocessMessage**](https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.control.preprocessmessage?view=netframework-4.7.2) method. For an example, see the [WindowsXamlHostBase.KeyboardFocus.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Forms.UI.XamlHost/WindowsXamlHostBase.KeyboardFocus.cs#L100) file in the Windows Community Toolkit.

### Keyboard focus navigation

When the user navigates through the UI elements in your application using the keyboard (for example, by pressing **Tab** or direction/arrow key), you'll need to programmatically move focus into and out of the **DesktopWindowXamlSource** object. When the user's keyboard navigation reaches the **DesktopWindowXamlSource**, move focus into the first [**Windows.UI.Xaml.UIElement**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement) object in the navigation order for your UI, continue to move focus to the following **Windows.UI.Xaml.UIElement** objects as the user cycles through the elements, and then move focus back out of the **DesktopWindowXamlSource** and into the parent UI element.  

The UWP XAML hosting API provides several types and members to help you accomplish these tasks.

* When the keyboard navigation enters your **DesktopWindowXamlSource**, the [**GotFocus**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource.gotfocus) event is raised. Handle this event and programmatically move focus to the first hosted **Windows.UI.Xaml.UIElement** by using the [**NavigateFocus**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource.navigatefocus) method.

* When the user is on the last focusable element in your **DesktopWindowXamlSource** and presses the **Tab** key or an arrow key, the [**TakeFocusRequested**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource.takefocusrequested) event is raised. Handle this event and programmatically move focus to the next focusable element in the host application. For example, in a WPF application where the **DesktopWindowXamlSource** is hosted in a [**System.Windows.Interop.HwndHost**](https://docs.microsoft.com/dotnet/api/system.windows.interop.hwndhost), you can use the [**MoveFocus**](https://docs.microsoft.com/dotnet/api/system.windows.frameworkelement.movefocus) method to transfer focus to the next focusable element in the host application.

For examples that demonstrate how to do this in the context of a working sample application, see the following code files:

  * **WPF:** See the [WindowsXamlHostBase.Focus.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Wpf.UI.XamlHost/WindowsXamlHostBase.Focus.cs) file in the Windows Community Toolkit.  

  * **Windows Forms:** See the [WindowsXamlHostBase.KeyboardFocus.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Forms.UI.XamlHost/WindowsXamlHostBase.KeyboardFocus.cs) file in the Windows Community Toolkit.

  * **C++/Win32**: See the [XamlBridge.cpp](https://github.com/marb2000/XamlIslands/blob/master/1903_Samples/CppWinRT_Win32_App/SampleCppApp/XamlBridge.cpp) file in the [C++ Win32 sample](https://github.com/marb2000/XamlIslands/tree/master/1903_Samples/CppWinRT_Win32_App).

## Handle layout changes

When the user changes the size of the parent UI element, you'll need to handle any necessary layout changes to make sure your UWP controls display as you expect. Here are some important scenarios to consider.

* In a C++ Win32 application, when your application handles the WM_SIZE message it can reposition the hosted XAML Island by using the [SetWindowPos](https://docs.microsoft.com/windows/desktop/api/winuser/nf-winuser-setwindowpos) function. For an example, see the [SampleApp.cpp](https://github.com/marb2000/XamlIslands/blob/master/19H1_Insider_Samples/CppWin32App_With_Island/SampleCppApp/SampleApp.cpp#L191) code file in the [C++ Win32 sample](https://github.com/marb2000/XamlIslands/tree/master/19H1_Insider_Samples/CppWin32App_With_Island).

* When the parent UI element needs to get the size of the rectangular area needed to fit the **Windows.UI.Xaml.UIElement** that you are hosting on the **DesktopWindowXamlSource**, call the [**Measure**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement.measure) method of the **Windows.UI.Xaml.UIElement**. For example:

    * In a WPF application you might do this from the [**MeasureOverride**](https://docs.microsoft.com/dotnet/api/system.windows.frameworkelement.measureoverride) method of the [**HwndHost**](https://docs.microsoft.com/dotnet/api/system.windows.interop.hwndhost) that hosts the **DesktopWindowXamlSource**. For an example, see the [WindowsXamlHostBase.Layout.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Wpf.UI.XamlHost/WindowsXamlHostBase.Layout.cs) file in the Windows Community Toolkit.

    * In a Windows Forms application you might do this from the [**GetPreferredSize**](https://docs.microsoft.com/dotnet/api/system.windows.forms.control.getpreferredsize) method of the [**Control**](https://docs.microsoft.com/dotnet/api/system.windows.forms.control) that hosts the **DesktopWindowXamlSource**. For an example, see the [WindowsXamlHostBase.Layout.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Forms.UI.XamlHost/WindowsXamlHostBase.Layout.cs) file in the Windows Community Toolkit.

* When the size of the parent UI element changes, call the [**Arrange**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.uielement.arrange) method of the root **Windows.UI.Xaml.UIElement** that you are hosting on the **DesktopWindowXamlSource**. For example:

    * In a WPF application you might do this from the [**ArrangeOverride**](https://docs.microsoft.com/dotnet/api/system.windows.frameworkelement.arrangeoverride) method of the [**HwndHost**](https://docs.microsoft.com/dotnet/api/system.windows.interop.hwndhost) object that hosts the **DesktopWindowXamlSource**. For an example, see the [WindowsXamlHost.Layout.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Wpf.UI.XamlHost/WindowsXamlHostBase.Layout.cs) file in the Windows Community Toolkit.

    * In a Windows Forms application you might do this from the handler for the [**SizeChanged**](https://docs.microsoft.com/dotnet/api/system.windows.forms.control.sizechanged) event of the [**Control**](https://docs.microsoft.com/dotnet/api/system.windows.forms.control) that hosts the **DesktopWindowXamlSource**. For an example, see the [WindowsXamlHost.Layout.cs](https://github.com/windows-toolkit/Microsoft.Toolkit.Win32/blob/master/Microsoft.Toolkit.Forms.UI.XamlHost/WindowsXamlHostBase.Layout.cs) file in the Windows Community Toolkit.

## Handle DPI changes

The UWP XAML framework handles DPI changes for hosted UWP controls automatically (for example, when the user drags the window between monitors with different screen DPI). For the best experience, we recommend that your Windows Forms, WPF, or C++ Win32 application is configured to be per-monitor DPI aware.

To configure your application to be per-monitor DPI aware, add a [side-by-side assembly manifest](https://docs.microsoft.com/windows/desktop/SbsCs/application-manifests) to your project and set ```<dpiAwareness>``` element to ```PerMonitorV2```. For more information about this value, see the description for [**DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2**](https://docs.microsoft.com/windows/desktop/hidpi/dpi-awareness-context).

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

## Host custom UWP XAML controls

Follow these general steps to host a custom UWP XAML control (either a control you define yourself or a control provided by a 3rd party) in a XAML Island. You must have the source code for the custom control so you can compile it with your application.

1. In your host application solution, integrate the source code for the custom UWP XAML control and build the custom control.

2. Add a UWP application project to your host application solution and add the [Microsoft.Toolkit.Win32.UI.XamlApplication](https://www.nuget.org/packages/Microsoft.Toolkit.Win32.UI.XamlApplication) NuGet package to this project.

3. In the UWP application project, define a custom `Application` type that derives from the **Microsoft.Toolkit.Win32.UI.XamlApplication** class exposed by the [Microsoft.Toolkit.Win32.UI.XamlApplication](https://www.nuget.org/packages/Microsoft.Toolkit.Win32.UI.XamlApplication) NuGet package. This type acts as a root metadata provider for loading metadata for custom UWP XAML types in assemblies in the current directory of your application.

4. In the constructor of your custom `Application` type, call the **Initialize** method of the base **Microsoft.Toolkit.Win32.UI.XamlApplication** class.

5. In your host application project, follow the process described in the [previous section](#host-uwp-xaml-controls) to host the custom control in a XAML Island.

For an example for a C++ Win32 application, see the following projects in the [C++ Win32 sample](https://github.com/marb2000/XamlIslands/blob/master/1903_Samples/CppWinRT_Win32_App):

* [SampleUserControl](https://github.com/marb2000/XamlIslands/tree/master/1903_Samples/CppWinRT_Win32_App/SampleUserControl): This project implements a custom UWP XAML control named `MyUserControl` that contains a text box, several buttons, and a combo box.
* [MyApp](https://github.com/marb2000/XamlIslands/tree/master/1903_Samples/CppWinRT_Win32_App/MyApp): This project includes custom `Application` type and other components mentioned in the previous steps.
* [SampleCppApp](https://github.com/marb2000/XamlIslands/tree/master/1903_Samples/CppWinRT_Win32_App/SampleCppApp): This is the C++ Win32 app project that hosts the custom UWP XAML control in a XAML Island.

For detailed instructions and examples for a WPF or Windows Forms application, see [these instructions](https://docs.microsoft.com/windows/communitytoolkit/controls/wpf-winforms/windowsxamlhost#add-a-custom-uwp-control).

## Troubleshooting

### Error using UWP XAML hosting API in a UWP app

| Issue | Resolution |
|-------|------------|
| Your app receives a **COMException** with the following message: "Cannot activate DesktopWindowXamlSource. This type cannot be used in a UWP app." or "Cannot activate WindowsXamlManager. This type cannot be used in a UWP app." | This error indicates you are trying to use the UWP XAML hosting API (specifically, you are trying to instantiate the [**DesktopWindowXamlSource**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource) or [**WindowsXamlManager**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.windowsxamlmanager) types) in a UWP app. The UWP XAML hosting API is only intended to be used in non-UWP desktop apps, such as WPF, Windows Forms, and C++ Win32 applications. |

### Error attaching to a window on a different thread

| Issue | Resolution |
|-------|------------|
| Your app receives a **COMException** with the following message: "AttachToWindow method failed because the specified HWND was created on a different thread." | This error indicates that your application called the **IDesktopWindowXamlSourceNative::AttachToWindow** method and passed it the HWND of a window that was created on a different thread. You must pass this method the HWND of a window that was created on the same thread as the code from which you are calling the method. |

### Error attaching to a window on a different top-level window

| Issue | Resolution |
|-------|------------|
| Your app receives a **COMException** with the following  message: "AttachToWindow method failed because the specified HWND descends from a different top-level window than the HWND that was previously passed to AttachToWindow on the same thread." | This error indicates that your application called the **IDesktopWindowXamlSourceNative::AttachToWindow** method and passed it the HWND of a window that descends from a different top-level window than a window you specified in a previous call to this method on the same thread.</p></p>After your application calls **IDesktopWindowXamlSourceNative::AttachToWindow** on a particular thread, all other [**DesktopWindowXamlSource**](https://docs.microsoft.com/uwp/api/windows.ui.xaml.hosting.desktopwindowxamlsource) objects on the same thread can only attach to windows that are descendants of the same top-level window that was passed in the first call to **IDesktopWindowXamlSourceNative::AttachToWindow**. When all the **DesktopWindowXamlSource** objects are closed for a particular thread, the next **DesktopWindowXamlSource** is then free to attach to any window again.</p></p>To resolve this issue, either close all **DesktopWindowXamlSource** objects that are bound to other top-level windows on this thread, or create a new thread for this **DesktopWindowXamlSource**. |

## Related topics

* [UWP controls in desktop applications](xaml-islands.md)
* [C++ Win32 XAML Islands sample](https://github.com/marb2000/XamlIslands/tree/master/19H1_Insider_Samples/CppWin32App_With_Island)
