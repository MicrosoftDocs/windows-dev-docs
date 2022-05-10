---
description: Learn how to apply the Mica material in your Win32 applications.
title: Use Mica material in Win32 apps
ms.topic: article
ms.date: 05/05/2022
ms.author: jimwalk
author: jwmsft
ms.localizationpriority: medium
---

# Apply Mica in Win32 desktop apps for Windows 11

Mica is an opaque material that incorporates the user's theme and desktop wallpaper to create a highly personalized appearance. As the user moves the window across the screen, the Mica material dynamically adapts to create a rich visualization using the wallpaper underneath the application. In addition, the material helps users focus on the current task by falling back to a neutral color when the app is inactive.

This topic describes how to apply Mica as the base layer of your Win32 app, prioritizing application and visibility in the title bar area. For more information about app layering with Mica, see [Mica material](../../design/style/mica.md).

## Prerequisites
To apply Mica to any Windows 11 app, make sure to install the  latest [Windows App SDK](https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/) and include it in your NuGet packages. For unpackaged apps, reference the Windows App SDK, install [WinRT](#), and have a matching [Microsoft.WindowsAppRuntime.Redist](https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/deploy-unpackaged-apps) installed.

## How to use Mica on Win32 Apps
In this snippet, we will use [C++/WinRT](https://docs.microsoft.com/en-us/windows/uwp/cpp-and-winrt-apis/). When enabling Mica, a compositor and a dispatcher queue are required along with all references to the Windows App SDK. For an unpackaged app, first initiate WinRT, set the SDK context, then register the window class. Next, we will create the Mica dispatcher queue in the form of a controller and initialize a WinRT compositor. 

The Mica controller will react to the system light and dark themes by default. To override this behavior, the following properties can be passed to the MicaController method:

- TintColor
- TintOpacity
- LuminosityOpacity
- FallbackColor

```cpp
int __stdcall WinMain (_In_ HINSTANCE , _In_opt_ HINSTANCE, _In_ PSTR, _In_ int)
{
    winrt::init_apartment();
    // Enable referencing the WindowsAppSDK from an unpackaged app.
    // https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/deploy-unpackaged-apps
    Utilities::WindowsAppSDKBootstrapperContext sdkContext;
    // Register the window class before creating it
    auto instance = winrt::check_pointer(GetModuleHandleW(nullptr));
    WNDCLASSEX wcex = { sizeof(wcex) };
    wcex.style = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc = WndProc;
    wcex.hInstance = instance;
    wcex.hIcon = LoadIconW(instance, IDI_APPLICATION);
    wcex.hCursor = LoadCursorW(nullptr, IDC_ARROW);
    wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
    wcex.lpszClassName = ClassName.c_str();
    wcex.hIconSm = LoadIconW(wcex.hInstance, IDI_APPLICATION);
    winrt::check_bool(RegisterClassExW(&wcex));
    // A dispatcher queue is required to be able to create a compositor.
    auto controller = Utilities::CreateDispatcherQueueControllerForCurrentThread();
    // Create the compositor required for Mica
    auto compositor = winrt::Compositor();
    
    // Create your window...
    ...
}
```

WinRT's init_apartment() method is inherently multi-threaded. If your app requires a single-thread, like the [WebView Sample](#Apply-Mica-in-Win32-WebView2-Applications), you can easily set the type.

```cpp
    winrt::init_apartment(winrt::apartment_type::single_threaded);
```

Now we can create our window with the `CreateWindowEx()` function. Once we do, we must create a window target and set it as the root so we know which layer to apply Mica to. Then, assert that Mica is supported by our window and target. The following snippet can be found in the [Mica C++ Win32 Sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Mica/cpp-win32). The **CompositionWindow** and **DesktopWindow** classes define **ClassName**, **windowTitle**, **m_target**, [**m_micaController**](https://docs.microsoft.com/en-us/windows/winui/api/microsoft.ui.composition.systembackdrops.micacontroller), and **m_isMicaSupported**.

```cpp
HWND MicaWindow = CreateWindowEx(
    WS_EX_COMPOSITED, 
    ClassName.c_str(), 
    windowTitle.c_str(), 
    WS_OVERLAPPEDWINDOW,
    CW_USEDEFAULT, 
    CW_USEDEFAULT, 
    800, 600, 
    (HWND) NULL, 
    (HMENU) NULL, 
    instance, 
    this
)
if (!MicaWindow)
    return FALSE;
ShowWindow(MicaWindow, SW_SHOWDEFAULT);
UpdateWindow(MicaWindow);
m_target = CreateWindowTarget(compositor);
// Need to set a root before we can enable Mica.
m_target.Root(compositor.CreateContainerVisual());
m_micaController = winrt::MicaController();
m_isMicaSupported = m_micaController.SetTarget(winrt::Microsoft::UI::WindowId{ reinterpret_cast<uint64_t>(MicaWindow) }, m_target);
```

Congratulations! Your app should now be able to run Mica.
<br>
<br>

## Apply Mica in Win32 WebView2 Applications
The fundemental principles of applying Mica is consistent across most Win32 applications. The process for WebView2 follows the basic steps from the [Win32 instructions above](#Apply-Mica-in-Win32-desktop-apps-for-Windows-11). However, in this case we will need to specify a single threated process from WinRT's init_apartment feature. 

To get started, set up the needed apartment, controller, compositor, target, and root. By default the WinRT init_apartment functions is multithreated, but WebView2 is inherently a single thread app. To set init_apartment as a single thread, pass the winrt::apartment_type::single_threaded parameter. In the Mica [WebView2 Sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Mica/cpp-WebView2) we simplify the syntax by creating a seperate class for Web View functions, referenced in the following code.

```cpp
int __stdcall WinMain(_In_ HINSTANCE, _In_opt_ HINSTANCE, _In_ PSTR, _In_ int)
{
    winrt::init_apartment(winrt::apartment_type::single_threaded);
    // Enable referencing the WindowsAppSDK from an unpackaged app.
    // Remember to have a matching Microsoft.WindowsAppRuntime.Redist installed.
    // https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/deploy-unpackaged-apps
    Utilities::WindowsAppSDKBootstrapperContext sdkContext;
    CompositionWindow::RegisterWindowClass();
    // A dispatcher queue is required to be able to create a compositor.
    auto controller = Utilities::CreateDispatcherQueueControllerForCurrentThread();
    auto compositor = winrt::Compositor();
    auto window = WebView2Window(compositor, L"Hello, WebView2!");
    
    ...
}
```

For a full explanation of the **WebView2Window class** and its integration with Mica make sure to checkout the sample referenced above. Note how the Composition and WebView classes handle messages, initialize the web view environment, and delete the window controller once the window is closed.
<br><br>

## Related articles

[BackdropMaterial class](https://docs.microsoft.com/en-us/windows/winui/api/microsoft.ui.xaml.controls.backdropmaterial), [NavigationView](../../design/controls/navigationview.md), [Materials](../../design/signature-experiences/materials.md), [Layering and Elevation](../../design/signature-experiences/layering.md)