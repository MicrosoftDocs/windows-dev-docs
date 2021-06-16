---
title: IWindowNative.WindowHandle property
description: WinUI COM property to get the requested window HWND. 
ms.topic: reference
ms.date: 03/09/2021
keywords: winui, Windows UI Library
---

# IWindowNative.WindowHandle property (microsoft.ui.xaml.window.h)

Gets the requested handle for the window.

## Syntax

<!--
[
    object,
    uuid( EECDBF0E-BAE9-4CB6-A68E-9598E1CB57BB ),
    local,
    pointer_default(unique)
]
interface IWindowNative: IUnknown
{
    [propget] HRESULT WindowHandle([out, retval] HWND* hWnd);
};
-->

```cpp
HRESULT WindowHandle(
  HWND* hWnd
);
```

## Parameters

*hWnd* [out]

Type: **HWND***

Handle to the window.

## Return value

Type: HRESULT

If this method succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.

## Remarks

## Examples

Before trying the following example, review the following topics:

- To use the WinUI 3 for desktop project templates, configure your development computer and [set up your development environment](../../project-reunion/set-up-your-development-environment.md).
- Confirm your dev environment is functioning as expected by creating and running an initial template app as described in [Get started with WinUI 3 for desktop apps](../winui3/get-started-winui3-for-desktop.md).

### Customized window icon

In the following example, we start with the initial **WinUI in Desktop C#/.NET 5** template code and show how to customize the icon used for an app window using a **WindowHandle**.

#### MainWindow.xaml.cs

1. First we add the following using directives:

    - [System.Runtime.InteropServices](/dotnet/api/system.runtime.interopservices): Provides support for COM interop and platform invoke services. In this example, required for the PInvoke functionality.
    - [WinRT](/uwp/cpp-ref-for-winrt/winrt): Provides custom data types belonging to C++/WinRT. In this example, required for the IWindowNative COM interface.

    ```csharp
    using System.Runtime.InteropServices;
    using WinRT;
    ```

1. We then add an .ico file to our project ("Images/windowIcon.ico") and set the "Build action" (right click the file and select Properties) for this file to "Content".

1. In the MainWindow method, we add a call to a `LoadIcon("Images/windowIcon.ico");` function (described in the next step) with a reference to the .ico file we added in the previous step.

    ```csharp
    public MainWindow()
    {
        LoadIcon("Images/windowIcon.ico");
    
        this.InitializeComponent();
    }
    ```

1. Next, we add a `LoadIcon(string iconName)` function that gets a handle to the application and uses various [PInvoke](/dotnet/standard/native-interop/pinvoke) features, including [LoadImage](/windows/win32/api/winuser/nf-winuser-loadimagew) and [SendMessage](/windows/win32/api/winuser/nf-winuser-sendmessage), to set the application icon.

    ```csharp
    private void LoadIcon(string iconName)
    {
        //Get the Window's HWND
        var hwnd = this.As<IWindowNative>().WindowHandle;
    
        IntPtr hIcon = PInvoke.User32.LoadImage(
            IntPtr.Zero, 
            iconName,
            PInvoke.User32.ImageType.IMAGE_ICON, 
            16, 16, 
            PInvoke.User32.LoadImageFlags.LR_LOADFROMFILE);
    
        PInvoke.User32.SendMessage(
            hwnd, 
            PInvoke.User32.WindowMessage.WM_SETICON, 
            (IntPtr)0, 
            hIcon);
    }    
    ```

1. Finally, we embed the type information from the public IWindowNative interface and create an instance of the class from the runtime assembly. For more details, see [Embed types from managed assemblies](/dotnet/standard/assembly/embed-types-visual-studio).

    ```csharp
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("EECDBF0E-BAE9-4CB6-A68E-9598E1CB57BB")]
    internal interface IWindowNative
    {
        IntPtr WindowHandle { get; }
    }
    ```

1. If you've followed these steps in your own app, build and run the app. You should see an application window similar to the following (with the custom app icon):

    :::image type="content" source="../winui3/images/build-basic/template-app-windowhandle.png" alt-text="Template app with custom application icon.":::<br/>*Template app with custom application icon.*

## Applies to

| Product | Versions |
| --- | --- |
| WinUI | 3.0.0-project-reunion-0.5, 3.0.0-project-reunion-preview-0.5 |

## See also

[IWindowNative interface](iwindownative.md)
