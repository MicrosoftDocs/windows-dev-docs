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

Rounded corners are the most immediately noticeable feature of [Windows 11 Geometry](../../design/signature-experiences/geometry.md). On Windows 11, the system automatically rounds top-level window corners for all inbox apps, including all UWP apps, and most other apps, but some Win32 apps might not be rounded. This topic describes how to round your app's main window corners if the system does not round them automatically.

> [!NOTE]
> By design, apps are not rounded when maximized, snapped, running in a Virtual Machine (VM), or running on a Windows Virtual Desktop (WVD).

:::image type="content" source="./images/apply-design/notepad-rounded.png" alt-text="The Notepad app on Windows 11 with rounded corners.":::

## Why isn't my app rounded?

If your app's main window doesn't receive automatic rounding, it's because you've customized your frame in a way that prevents it. Apps fall into three main categories from the perspective of the Desktop Window Manager (DWM):

1. Apps that are rounded by default.

    This includes apps that want a complete system-provided frame and caption-controls, like Notepad. It also includes apps that provide enough information to the system so it can properly round them, such as setting the WS_THICKFRAME and WS_CAPTION window styles or providing a 1-pixel non-client area border that the system can use to round the corners.
1. Apps that are not rounded by policy, but can be rounded.

    Apps in this category generally want to customize the majority of the window frame but still want the system-drawn border and shadow, such as Microsoft Office. If your app is not rounded by policy, it could be caused by one of the following things:

    - Lack of frame styles
    - Empty non-client area
    - Other customizations, such as extra non-child windows used for custom shadows

    Changing one of these things will break automatic rounding. Although we did try to round as many apps as possible with our system heuristics, there are some combinations of customizations that we can't predict so we provided a manual opt-in API for those cases. If you address these issues in your app or call the opt-in API, described in the following section, then the system can round you.
1. Apps that cannot ever be rounded, even if they call the opt-in API.

    These apps have no frame or borders, and typically have heavily customized UI. If your app does one of the following, it cannot be rounded:

    - Per-pixel alpha layering
    - Window regions

    For example, an app might use per-pixel alpha layering to draw transparent pixels around its main window to achieve a custom shadow effect, which makes the window no longer a rectangle and therefore the system cannot round it.

## How to opt in to rounded corners

If your app is not rounded by policy, you can optionally call our new API to allow your app to opt in to rounded corners. This API is expressed as enumeration values to be passed to the [**DwmSetWindowAttribute**](/windows/win32/api/dwmapi/nf-dwmapi-dwmsetwindowattribute) API, as shown in the new **DWM_WINDOW_CORNER_PREFERENCE** enumeration. This enumeration is defined in the [dwmapi.h header](/windows/win32/api/dwmapi/) and is available in the latest [Insider Preview SDK](https://www.microsoft.com/software-download/windowsinsiderpreviewSDK).

> [!NOTE]
> DwmSetWindowAttribute is a native API. If your app is based on .NET, you'll need to use [P/Invoke](/dotnet/standard/native-interop/pinvoke) to import dwmapi.dll so you can call it.

| Enum value | Description |
| --- | --- |
| **DWMWCP_DEFAULT** | Let the system decide whether or not to round window corners. |
| **DWMWCP_DONOTROUND** | Never round window corners. |
| **DWMWCP_ROUND** | Round the corners if appropriate. |
| **DWMWCP_ROUNDSMALL** | Round the corners if appropriate, with a small radius. |

## Examples

The following examples show how you can pass these values to [**DwmSetWindowAttribute**](/windows/win32/api/dwmapi/nf-dwmapi-dwmsetwindowattribute) or [**DwmGetWindowAttribute**](/windows/win32/api/dwmapi/nf-dwmapi-dwmgetwindowattribute) to control your app's rounding experience.

### Example 1 - Rounding an app's main window

If your app isn't rounded by policy, you can call the API after window creation to ask the system to round you.

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

### Example 2 – Rounding the corners of a menu with a small radius

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

### Example 3 – Customizing window contents for rounding

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
