---
description: Apply rounded corners in your desktop application.
title: Apply rounded corners in desktop apps
ms.topic: article
ms.date: 10/02/2020
ms.author: jimwalk
author: jwmsft
ms.localizationpriority: medium
---

# Apply rounded corners in desktop apps for Windows 11

Rounded corners are the most immediately noticeable feature of [Windows 11 Geometry](../../design/signature-experiences/geometry.md). On Windows 11, the system automatically rounds top-level window corners for all inbox apps, including all UWP apps, and most other apps. However, some Win32 apps might not be rounded. This topic describes how to round your Win32 app's main window corners if the system does not round them automatically.

> [!NOTE]
> By design, apps are not rounded when maximized, snapped, running in a Virtual Machine (VM), or running on a Windows Virtual Desktop (WVD).

:::image type="content" source="./images/apply-design/notepad-rounded.png" alt-text="The Notepad app on Windows 11 with rounded corners.":::

## Why isn't my app rounded?

If your app's main window doesn't receive automatic rounding, it's because you've customized your frame in a way that prevents it. Apps fall into three main categories from the perspective of the Desktop Window Manager (DWM):

1. Apps that are rounded by default.

    This includes apps that want a complete system-provided frame and caption-controls (min/max/close buttons), like Notepad. It also includes apps that provide enough information to the system so it can properly round them, such as setting the WS_THICKFRAME and WS_CAPTION window styles or providing a 1-pixel non-client area border that the system can use to round the corners.
1. Apps that are not rounded by policy, but *can* be rounded.

    Apps in this category generally want to customize the majority of the window frame but still want the system-drawn border and shadow, such as Microsoft Office. If your app is not rounded by policy, it could be caused by one of the following things:

    - Lack of frame styles
    - Empty non-client area
    - Other customizations, such as extra non-child windows used for custom shadows

    Changing one of these things will break automatic rounding. Although we did try to round as many apps as possible with our system heuristics, there are some combinations of customizations that we can't predict so we provided a manual opt-in API for those cases. If you address these issues in your app or call the opt-in API, described in the following section, then it's possible for the system to round you. Note, however, that the API is a hint to the system and does not guarantee rounding, depending on the customizations.
1. Apps that cannot ever be rounded, even if they call the opt-in API.

    These apps have no frame or borders, and typically have heavily customized UI. If your app does one of the following, it cannot be rounded:

    - Per-pixel alpha layering
    - Window regions

    For example, an app might use per-pixel alpha layering to draw transparent pixels around its main window to achieve a custom shadow effect, which makes the window no longer a rectangle and therefore the system cannot round it.

## How to opt in to rounded corners

### API definition

If your app is not rounded by policy, you can optionally call our new API to allow your app to opt in to rounded corners. This API is expressed as enumeration values to be passed to the [**DwmSetWindowAttribute**](/windows/win32/api/dwmapi/nf-dwmapi-dwmsetwindowattribute) API, as shown in the new **DWM_WINDOW_CORNER_PREFERENCE** enumeration. **DWM_WINDOW_CORNER_PREFERENCE** is defined in the [dwmapi.h header](/windows/win32/api/dwmapi/) and is available in the latest [Insider Preview SDK](https://www.microsoft.com/software-download/windowsinsiderpreviewSDK).

| Enum value | Description |
| --- | --- |
| **DWMWCP_DEFAULT** | Let the system decide whether or not to round window corners. |
| **DWMWCP_DONOTROUND** | Never round window corners. |
| **DWMWCP_ROUND** | Round the corners if appropriate. |
| **DWMWCP_ROUNDSMALL** | Round the corners if appropriate, with a small radius. |

A pointer to the appropriate value from this enum is passed to the third parameter of DwmSetWindowAttribute. For the second parameter, which specifies which attribute you are setting, pass the new **DWMWA_WINDOW_CORNER_PREFERENCE** value defined in the [**DWMWINDOWATTRIBUTE**](/windows/win32/api/dwmapi/ne-dwmapi-dwmwindowattribute) enumeration.

### For C# apps

DwmSetWindowAttribute is a native C++ API. If your app is based on .NET and uses C#, you'll need to use [P/Invoke](/dotnet/standard/native-interop/pinvoke) to import dwmapi.dll and the DwmSetWindowAttribute function signature. All standard WinForms and WPF apps are rounded automatically like any other app, but if you customize your window frame or use a third party framework, you might need to opt-in to rounded corners if doing so results in losing the default rounding. See the Examples section for further details.

## Examples

The following examples show how you can call [**DwmSetWindowAttribute**](/windows/win32/api/dwmapi/nf-dwmapi-dwmsetwindowattribute) or [**DwmGetWindowAttribute**](/windows/win32/api/dwmapi/nf-dwmapi-dwmgetwindowattribute) to control your app's rounding experience if your app is not rounded by policy.

> [!Note]
> Error handling has been left out of these examples for brevity and clarity.

### Example 1 - Rounding an app's main window in C# - WPF

To call DwmSetWindowAttribute in a C# WPF desktop app, you'll need to import dwmapi.dll and the DwmSetWindowAttribute function signature with [P/Invoke](/dotnet/standard/native-interop/pinvoke). First you'll need to redefine the required enum values from the native dwmapi.h header, then declare the function using C# types equivalent to the original native function. Because the original takes a pointer for the third parameter, make sure to use the *ref* keyword so you can pass the address of a variable when you call the function. You can do this in your MainWindow class in MainWindow.xaml.cs.

```CSharp
using System.Runtime.InteropServices;
using System.Windows.Interop;

public partial class MainWindow : Window
{
    // The enum flag for DwmSetWindowAttribute's second parameter, which tells the function what attribute to set.
    public enum DWMWINDOWATTRIBUTE
    {
        DWMWA_WINDOW_CORNER_PREFERENCE = 33
    }

    // The DWM_WINDOW_CORNER_PREFERENCE enum for DwmSetWindowAttribute's third parameter, which tells the function
    // what value of the enum to set.
    public enum DWM_WINDOW_CORNER_PREFERENCE
    {
        DWMWCP_DEFAULT      = 0,
        DWMWCP_DONOTROUND   = 1,
        DWMWCP_ROUND        = 2,
        DWMWCP_ROUNDSMALL   = 3
    }

    // Import dwmapi.dll and define DwmSetWindowAttribute in C# corresponding to the native function.
    [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern long DwmSetWindowAttribute(IntPtr hwnd,
                                                     DWMWINDOWATTRIBUTE attribute,
                                                     ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute,
                                                     uint cbAttribute);

    // ...
    // Various other definitions
    // ...
}
```

Next, in your MainWindow constructor, after the call to InitalizeComponent, create a new instance of the [WindowInteropHelper](/dotnet/api/system.windows.interop.windowinterophelper) class to acquire a pointer to the underlying HWND (window handle). Make sure to use the [EnsureHandle](/dotnet/api/system.windows.interop.windowinterophelper.ensurehandle) method to force the system to create an HWND for the window before it's shown, because normally the system only does so after exiting the constructor.

```CSharp
public MainWindow()
{
    InitializeComponent();

    IntPtr hWnd = new WindowInteropHelper(GetWindow(this)).EnsureHandle();
    var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
    var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
    DwmSetWindowAttribute(hWnd, attribute, ref preference, sizeof(uint));
    
    // ...
    // Perform any other work necessary
    // ...
}
```

### Example 2 - Rounding an app's main window in C# - WinForms

Like with WPF, for a WinForms app you'll first need to import dwmapi.dll and the DwmSetWindowAttribute function signature with [P/Invoke](/dotnet/standard/native-interop/pinvoke). You can do this in your primary Form class.

```Csharp
using System;
using System.Runtime.InteropServices;

public partial class Form1 : Form
{
    // The enum flag for DwmSetWindowAttribute's second parameter, which tells the function what attribute to set.
    public enum DWMWINDOWATTRIBUTE
    {
        DWMWA_WINDOW_CORNER_PREFERENCE = 33
    }            

    // The DWM_WINDOW_CORNER_PREFERENCE enum for DwmSetWindowAttribute's third parameter, which tells the function
    // what value of the enum to set.
    public enum DWM_WINDOW_CORNER_PREFERENCE
    {
        DWMWCP_DEFAULT      = 0,
        DWMWCP_DONOTROUND   = 1,
        DWMWCP_ROUND        = 2,
        DWMWCP_ROUNDSMALL   = 3
    }               

    // Import dwmapi.dll and define DwmSetWindowAttribute in C# corresponding to the native function.
    [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern long DwmSetWindowAttribute(IntPtr hwnd, 
                                                     DWMWINDOWATTRIBUTE attribute, 
                                                     ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute, 
                                                     uint cbAttribute);
    
    // ...
    // Various other definitions
    // ...
}
```

Calling DwmSetWindowAttribute is also the same as with a WPF app, but you don't have to use a helper class to get the HWND because it's simply a property of the Form. Call it from within your Form constructor, after the call to InitializeComponent.

```CSharp
public Form1()
{
    InitializeComponent();

    var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
    var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
    DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));
    
    // ...
    // Perform any other work necessary
    // ...
}
```

### Example 3 - Rounding an app's main window in C++

For a native C++ app, you can call DwmSetWindowAttribute in your message processing function after window creation to ask the system to round you.

```cpp
LRESULT ExampleWndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) 
{
    switch (message)
    {
    // ...
    // Handle various window messages...
    // ...

    case WM_CREATE:
        // ...
        // Perform app resource initialization after window creation
        // ...
        
        if(hWnd)
        {
            DWM_WINDOW_CORNER_PREFERENCE preference = DWMWCP_ROUND;
            DwmSetWindowAttribute(hWnd, DWMWA_WINDOW_CORNER_PREFERENCE, &preference, sizeof(preference));
        }
        break;

    // ...
    // Handle various other window messages...
    // ...
    }

    return 0;
}
```

### Example 4 – Rounding the corners of a menu with a small radius - C++

By default, menus are pop-up windows, which do not get rounded. If your app creates a custom menu and you want it to follow the rounding policy of other standard menus, you can call the API to let the system know that this window should be rounded, even though it doesn't appear to match the default rounding policy.

```cpp
HWND CreateCustomMenu()
{
    // Call an app-specific helper to make the window, using traditional APIs.
    HWND hWnd = CreateMenuWindowHelper();

    if (hWnd)
    {
        // Make sure we round the window, using the small radius 
        // because menus are auxiliary UI.
        DWM_WINDOW_CORNER_PREFERENCE preference = DWMWCP_ROUNDSMALL;
        DwmSetWindowAttribute(hWnd, DWMWA_WINDOW_CORNER_PREFERENCE, &preference, sizeof(preference));
    }

    return hWnd;
}
```

### Example 5 – Customizing window contents for rounding - C++

When the corners of a window are rounded, a little bit of the window's client area is clipped. Some apps may want to adjust their contents to account for this.

For example, the thumb of a vertical scrollbar may be placed right at the bottom-right corner of a window, so it may be clipped by rounding. If that is visually undesirable, you might position the scroll bar differently when the window is rounded. However, because the window is not always rounded – for example, it is never rounded when it is maximized – you should call [**DwmGetWindowAttribute**](/windows/win32/api/dwmapi/nf-dwmapi-dwmgetwindowattribute) to determine whether or not the UI needs to be adjusted, and by how much.

```cpp
LRESULT ExampleWndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam) 
{
    switch (message)
    {
    // ...
    // Handle various window messages...
    // ...

    case WM_SIZE:
        // Whenever an interesting size change takes place, there may be a corresponding rounding change.
        int radius = 0;
        if (SUCCEEDED(DwmGetWindowAttribute(hWnd, DWMWA_WINDOW_CORNER_RADIUS, &radius, sizeof(radius))))
        {
            AdjustScrollbarForCornerRadius(radius);
        }
        break;

    // ...
    // Handle various other window messages...
    // ...
    }

    return 0;
}
```
