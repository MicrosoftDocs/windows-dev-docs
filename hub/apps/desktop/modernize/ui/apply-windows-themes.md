---
description: Learn how to detect system theme changes for dark or light mode.
title: Support Dark and Light themes in Win32 apps
ms.topic: article
ms.date: 05/05/2022
ms.localizationpriority: medium
---

# Support Dark and Light themes in Win32 apps

Windows supports _Light_ and _Dark_ themes as a personalization option in Windows settings. Windows uses Light mode by default, but users can choose Dark mode, which changes much of the UI to a dark color. Users might prefer this setting because it's easier on the eyes in lower-light environments, or they might simply prefer a darker interface in general. Also, darker UI colors can reduce the battery usage on some types of computer displays, such as OLED screens.

:::image type="content" source="../images/apply-design/themes.png" alt-text="A split image of an app in light theme on the left, and dark theme on the right.":::

We are working hard to broaden support for Dark mode without breaking existing applications, and to that end we're providing technical guidance for updating a Win32 desktop Windows app to support both Light and Dark modes.

## Dark mode vs. Light mode

The Color Mode in settings (which includes Light and Dark modes) is a setting that defines the overall _foreground_ and _background_ colors for the operating system and apps.

| Mode | Description | Example |
|--|---------|---------|
| **Light** | A light background with a contrasting dark foreground.<br/><br/>In Light Mode, you will generally see black or dark text on white or light backgrounds. | :::image type="content" source="../images/apply-design/clock-app-light.png" alt-text="A screenshot of the Alarms & Clock app in light mode"::: |
| **Dark** | A dark background with a contrasting light foreground.<br/><br/>In Dark mode, you will generally see white or light text on black or dark backgrounds. | :::image type="content" source="../images/apply-design/clock-app-dark.png" alt-text="A screenshot of the Alarms & Clocks app in Dark mode"::: |

> [!NOTE]
> The reason we use "_black or dark_" and "_white or light_" is because there are additional colors such as the Accent color that can tint various foreground and background colors. So you might in fact see light blue text on a dark blue background in some parts of the UI, and that would still be considered acceptable Dark mode UI.

Due to the wide diversity of UI in different apps, the color mode, and foreground and background colors are meant as more of a directional guideline than a hard rule:

- Foreground elements, highlights, and text should be closer to the foreground color than the background color.
- Large, solid background areas and text backgrounds should generally be closer to the background color than the foreground color.

In practice, this means that in Dark mode, most of the UI will be dark, and in Light mode most of the UI will be light. The concept of a background in Windows is the large area of colors in an app, or the page color. The concept of a foreground in Windows is the text color.

> [!TIP]
> If you find it confusing that the _foreground_ color is light in Dark mode and dark in Light mode, it may help to think of the foreground color as "the default text color".

## Enable support for switching color modes

There are many approaches to implementing Dark mode support in an application. Some apps contain two sets of UIs (one with a light color and one with a dark color). Some Windows UI frameworks, such as [WinUI 3](../../../winui/winui3/index.md), automatically detect a system's theme and adjust the UI to follow the system theme. To fully support Dark mode, the entirety of an app's surface must follow the dark theme.

There are two main things you can do in your Win32 app to support both Light and Dark themes.

- **Know when Dark mode is enabled**

  Knowing when Dark mode is enabled in the system settings can help you know when to switch your app UI to a Dark mode-themed UI.

- **Enable a Dark mode title bar for Win32 applications**

  Not all Win32 applications support Dark mode, so Windows gives Win32 apps a light title bar by default. If you are prepared to support Dark mode, you can ask Windows to draw the dark title bar instead when Dark mode is enabled.

> [!NOTE]
> This article provides examples of ways to detect system theme changes, and request a light or dark title bar for your Win32 application's window. It does not cover specifics of how to repaint and render your app UI using a Dark mode color set.

## Know when Dark mode is enabled

The first step is to keep track of the color mode setting itself. This will let you adjust your application's painting and rendering code to use a Dark mode color set. Doing this requires the app to read the color setting at startup and to know when the color setting changes during an app session.

To do this in a Win32 application, use [Windows::UI::Color](/uwp/api/windows.ui.color) and detect if a color can be classified as _light_ or _dark_. To use `Windows::UI::Color`, you need to import (in `pch.h`) the `Windows.UI.ViewManagement` header from winrt.

```cpp
#include <winrt/Windows.UI.ViewManagement.h>
```

Also include that namespace in `main.cpp`.

```cpp
using namespace Windows::UI::ViewManagement;
```

In `main.cpp`, use this function to detect if a color can be classified as _light_.

```cpp
inline bool IsColorLight(Windows::UI::Color& clr)
{
    return (((5 * clr.G) + (2 * clr.R) + clr.B) > (8 * 128));
}
```

This function performs a quick calculation of the _perceived brightness_ of a color, and takes into consideration ways that different channels in an RGB color value contribute to how bright it looks to the human eye. It uses all-integer math for speed on typical CPUs.

> [!NOTE]
> This is not a model for real analysis of color brightness. It is good for quick calculations that require you to determine if a color can be classified as _light_ or _dark_. Theme colors can often be light but not pure white, or dark but not pure black.

Now that you have a function to check whether a color is light, you can use that function to detect if Dark mode is enabled.

Dark mode is defined as a dark background with a contrasting light foreground. Since `IsColorLight` checks if a color is considered light, you can use that function to see if the foreground is light. If the foreground is light, then Dark mode is enabled.

To do this, you need to get the UI color type of the foreground from the system settings. Use this code in `main.cpp`.

```cpp
auto settings = UISettings();
    
auto foreground = settings.GetColorValue(UIColorType::Foreground);
```

[UISettings](/uwp/api/windows.ui.viewmanagement.uisettings) gets all the settings of the UI including color. Call [UISettings.GetColorValue](/uwp/api/windows.ui.viewmanagement.uisettings.getcolorvalue)([UIColorType::Foreground](/uwp/api/windows.ui.viewmanagement.uicolortype)) to get the foreground color value from the UI settings.

Now you can run a check to see if the foreground is considered light (in `main.cpp`).

```cpp
bool isDarkMode = static_cast<bool>(IsColorLight(foreground));

wprintf(L"\nisDarkMode: %u\n", isDarkMode);
```

- If the foreground is light, then `isDarkMode` will evaluate to 1 (`true`) meaning Dark mode is enabled.
- If the foreground is dark, then `isDarkMode` will evaluate to 0 (`false`) meaning Dark mode is not enabled.

To automatically track when the Dark mode setting changes during an app session, you can wrap your checks like this.

```cpp
auto revoker = settings.ColorValuesChanged([settings](auto&&...)
{
    auto foregroundRevoker = settings.GetColorValue(UIColorType::Foreground);
    bool isDarkModeRevoker = static_cast<bool>(IsColorLight(foregroundRevoker));
    wprintf(L"isDarkModeRevoker: %d\n", isDarkModeRevoker);
});

```

Your full code should look like this.

```cpp
inline bool IsColorLight(Windows::UI::Color& clr)
{
    return (((5 * clr.G) + (2 * clr.R) + clr.B) > (8 * 128));
}

int main()
{
    init_apartment();

    auto settings = UISettings();
    auto foreground = settings.GetColorValue(UIColorType::Foreground);

    bool isDarkMode = static_cast<bool>(IsColorLight(foreground));
    wprintf(L"\nisDarkMode: %u\n", isDarkMode);

    auto revoker = settings.ColorValuesChanged([settings](auto&&...)
        {
            auto foregroundRevoker = settings.GetColorValue(UIColorType::Foreground);
            bool isDarkModeRevoker = static_cast<bool>(IsColorLight(foregroundRevoker));
            wprintf(L"isDarkModeRevoker: %d\n", isDarkModeRevoker);
        });
    
    static bool s_go = true;
    while (s_go)
    {
        Sleep(50);
    }
}
```

When this code is run:

If Dark mode is enabled, `isDarkMode` will evaluate to 1.

:::image type="content" source="../images/apply-design/sample-app-dark.png" alt-text="A screenshot of an app in dark mode.":::

Changing the setting from Dark mode to Light mode will make `isDarkModeRevoker` evaluate to 0.

:::image type="content" source="../images/apply-design/sample-app-light.png" alt-text="A screenshot of an app in light mode.":::

## Enable a Dark mode title bar for Win32 applications

Windows doesn't know if an application can support Dark mode, so it assumes that it can't for backwards compatibility reasons. Some Windows development frameworks, such as [Windows App SDK](../../../windows-app-sdk/index.md), support Dark mode natively and change certain UI elements without any additional code. Win32 apps often don't support Dark mode, so Windows gives Win32 apps a light title bar by default.

However, for any app that uses the standard Windows title bar, you can enable the dark version of the title bar when the system is in Dark mode. To enable the dark title bar, call a [Desktop Windows Manager](/windows/win32/dwm/dwm-overview) (DWM) function called [DwmSetWindowAttribute](/windows/win32/api/dwmapi/nf-dwmapi-dwmsetwindowattribute) on your top-level window, using the window attribute [DWMWA_USE_IMMERSIVE_DARK_MODE](/windows/win32/api/dwmapi/ne-dwmapi-dwmwindowattribute). (DWM renders attributes for a window.)

The following examples assume you have a window with with a standard title bar, like the one created by this code.

```cpp
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   hInst = hInstance; // Store instance handle in our global variable

   HWND hWnd = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,CW_USEDEFAULT, 0, 
     CW_USEDEFAULT, 0, nullptr, nullptr, hInstance, nullptr);

   if (!hWnd)
   {
      return FALSE;
   }

   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

   return TRUE;
}
```

First, you need to import the DWM API, like this.

```cpp
#include <dwmapi.h>
```

Then, define the `DWMWA_USE_IMMERSIVE_DARK_MODE` macros above your `InitInstance` function.

```cpp
#ifndef DWMWA_USE_IMMERSIVE_DARK_MODE
#define DWMWA_USE_IMMERSIVE_DARK_MODE 20
#endif

BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
…
```

Finally, you can use the DWM API to set the title bar to use a dark color. Here, you create a `BOOL` called `value` and set it to `TRUE`. This `BOOL` is used to trigger this Windows attribute setting. Then, you use `DwmSetWindowAttribute` to change the window attribute to use Dark mode colors.

```cpp
BOOL value = TRUE;
::DwmSetWindowAttribute(hWnd, DWMWA_USE_IMMERSIVE_DARK_MODE, &value, sizeof(value));
```

Here's more explanation of what this call does.

The syntax block for [DwmSetWindowAttribute](/windows/win32/api/dwmapi/nf-dwmapi-dwmsetwindowattribute) looks like this.

```cpp
HRESULT DwmSetWindowAttribute(
       HWND    hwnd,
       DWORD   dwAttribute,
  [in] LPCVOID pvAttribute,
       DWORD   cbAttribute
);
```

After passing `hWnd` (the handle to the window you want to change) as your first parameter, you need to pass in `DWMWA_USE_IMMERSIVE_DARK_MODE` as the `dwAttribute` parameter. This is a constant in the DWM API that lets the Windows frame be drawn in Dark mode colors when the Dark mode system setting is enabled. If you switch to Light mode, you will have to change `DWMWA_USE_IMMERSIVE_DARK_MODE` from 20 to 0 for the title bar to be drawn in light mode colors.

The `pvAttribute` parameter points to a value of type `BOOL` (which is why you made the `BOOL` value earlier). You need `pvAttribute` to be `TRUE` to honor Dark mode for the window. If `pvAttribute` is `FALSE`, the window will use Light Mode.

Lastly, `cbAttribute` needs to have the size of the attribute being set in `pvAttribute`. To do easily do this, we pass in `sizeof(value)`.

Your code to draw a dark windows title bar should look like this.

```cpp
#ifndef DWMWA_USE_IMMERSIVE_DARK_MODE
#define DWMWA_USE_IMMERSIVE_DARK_MODE 20
#endif


BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   hInst = hInstance; // Store instance handle in our global variable

   HWND hWnd = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
      CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, nullptr, nullptr, hInstance, nullptr);

   BOOL value = TRUE;
   ::DwmSetWindowAttribute(hWnd, DWMWA_USE_IMMERSIVE_DARK_MODE, &value, sizeof(value));

   if (!hWnd)
   {
      return FALSE;
   }

   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

   return TRUE;
}
```

When this code is run, the app title bar should be dark:

:::image type="content" source="../images/apply-design/title-bar-dark.png" alt-text="A screenshot of an app with a dark title bar.":::

## See also

- [Make your app great on Windows 11](../../../get-started/make-apps-great-for-windows.md)
- [Accessible text requirements](../../../design/accessibility/accessible-text-requirements.md)
- [Desktop Windows Manager (DWM)](/windows/win32/dwm/dwm-overview)
