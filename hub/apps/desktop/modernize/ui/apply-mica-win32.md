---
description: Learn how to apply the Mica material in your Win32 applications.
title: Use Mica material in Win32 apps
ms.topic: article
ms.date: 05/13/2022
ms.localizationpriority: medium
---

# Apply Mica in Win32 desktop apps for Windows 11

Mica is an opaque material that incorporates the user's theme and desktop wallpaper to create a highly personalized appearance. As the user moves the window across the screen, the Mica material dynamically adapts to create a rich visualization using the wallpaper underneath the application. In addition, the material helps users focus on the current task by falling back to a neutral color when the app is inactive.

This article describes how to apply Mica as the base layer of your Win32 app, prioritizing application and visibility in the title bar area. For more information about app layering with Mica, see [Mica material](../../../design/style/mica.md).

## Prerequisites

To apply Mica to a Win32 app for Windows 11, you need to use the [Windows App SDK](../../../windows-app-sdk/index.md). You'll need the following:

- Install the  latest [Windows App SDK](../../../windows-app-sdk/index.md) Visual Studio Extension or Microsoft.WindowsAppSDK NuGet package. See [Downloads for the Windows App SDK](../../../windows-app-sdk/downloads.md).
- For unpackaged apps, reference the Windows App SDK, install WinRT, and have a matching [Windows App Runtime Redistributable (Microsoft.WindowsAppRuntime.Redist)](../../../windows-app-sdk/downloads.md) installed. See [Windows App SDK deployment guide for framework-dependent apps packaged with external location or unpackaged](../../../windows-app-sdk/deploy-unpackaged-apps.md).

## How to use Mica in Win32 Apps

To use mica in your app, you use the [MicaController](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller) class. This class manages both the rendering of the system backdrop material as well as the handling of system policy for the mica material.

The MicaController reacts to the system Light and Dark themes by default. To override this behavior, you can pass the following properties to the MicaController:

- [FallbackColor](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller.fallbackcolor)
- [LuminosityOpacity](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller.luminosityopacity)
- [TintColor](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller.tintcolor)
- [TintOpacity](/windows/windows-app-sdk/api/winrt/microsoft.ui.composition.systembackdrops.micacontroller.tintopacity)

> [!TIP]
> The code in this section is taken from the [Windows App SDK Win32 Mica sample on GitHub](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Mica/cpp-win32). See the GitHub repo for the full code. These examples use [C++/WinRT](/windows/uwp/cpp-and-winrt-apis/).

To enable Mica, you need a reference to the Windows App SDK, a [Compositor](/uwp/api/windows.ui.composition.compositor), and a [DispatcherQueue](/uwp/api/windows.system.dispatcherqueue).

This example shows how to do the following to set up an unpackaged app:

- Initialize WinRT.
- Reference the WindowsAppSDK from an unpackaged app.
  - See [Use the Windows App SDK runtime for apps packaged with external location or unpackaged](../../../windows-app-sdk/use-windows-app-sdk-run-time.md) for more info.
  - See `WindowsAppSDKBootstrapperContext` in [Utilities.h](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Mica/cpp-win32/WinAppSDKMicaSample/Utilities.h) for example code.
- Register the window class.
- Create the Mica dispatcher queue controller
- Initialize a WinRT compositor.

> From [WinMain.cpp](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Mica/cpp-win32/WinAppSDKMicaSample/WinMain.cpp)


```cpp
int __stdcall WinMain(_In_ HINSTANCE, _In_opt_ HINSTANCE,  _In_ PSTR, _In_ int)
{
    // Initialize WinRt Instance
    winrt::init_apartment();

    // Enable referencing the WindowsAppSDK from an unpackaged app.
    Utilities::WindowsAppSDKBootstrapperContext sdkContext;

    // Register Window class before making the window.
    MicaWindow::RegisterWindowClass();

    // Mica requires a compositor, which also requires a dispatcher queue.
    auto controller = Utilities::CreateDispatcherQueueControllerForCurrentThread();
    auto compositor = winrt::Compositor();

    // Create your window...
    ...
}
```

> From [MicaWindow.cpp](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Mica/cpp-win32/WinAppSDKMicaSample/MicaWindow.cpp)

```cpp
void MicaWindow::RegisterWindowClass()
{
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
    winrt::check_bool(RegisterClassExW(&wcex)); // check if the window class was registered successfully
}
```

The `winrt::init_apartment` method is multi-threaded by default. If your app requires a single-thread, like the [WebView2 Sample](#how-to-use-mica-in-win32-webview2-apps), you can easily set the type.

```cpp
winrt::init_apartment(winrt::apartment_type::single_threaded);
```

Now you can use the `CreateWindowEx()` function to create a window. Then, you have to create a window target and set it as the root to specify which layer to apply Mica to. Finally, assert that Mica is supported by the window and target.

The [Win32 Mica sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Mica/cpp-win32) creates the [DesktopWindow](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Mica/cpp-win32/WinAppSDKMicaSample/DesktopWindow.h) and [MicaWindow](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Mica/cpp-win32/WinAppSDKMicaSample/MicaWindow.cpp) classes to do this work. These classes define: `ClassName`, `windowTitle`, `m_target`, `m_micaController`, and `m_isMicaSupported`.

> From [WinMain.cpp](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Mica/cpp-win32/WinAppSDKMicaSample/WinMain.cpp)

```cpp
// Mica window is inherited from the MicaWindow class, which is an extension of the DesktopWindow Class.
// Here, we initialize the main window and set the title.
   auto window = MicaWindow(compositor, L"Hello, Mica!");
```

> From [MicaWindow.cpp](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Mica/cpp-win32/WinAppSDKMicaSample/MicaWindow.cpp)

```cpp
// Create the main window and enable Mica.
MicaWindow::MicaWindow(const winrt::Compositor& compositor, const std::wstring& windowTitle)
{
    auto instance = winrt::check_pointer(GetModuleHandleW(nullptr));
    WINRT_ASSERT(!m_window); // check that window is not initialized
    WINRT_VERIFY(
        // Window Properties
        CreateWindowExW(
            WS_EX_COMPOSITED,
            ClassName.c_str(), // declared in MicaWindow.h and defined above
            windowTitle.c_str(),
            WS_OVERLAPPEDWINDOW,
            CW_USEDEFAULT,
            CW_USEDEFAULT, 
            800, 600, 
            nullptr, 
            nullptr, 
            instance, 
            this
        ));

    // Check that the window was created successfully.
    WINRT_ASSERT(m_window);

    ShowWindow(m_window, SW_SHOWDEFAULT);
    UpdateWindow(m_window);

    // The Mica controller needs to set a target with a root to recognize the visual base layer.
    m_target = CreateWindowTarget(compositor);

    // Need to set a root before we can enable Mica.
    m_target.Root(compositor.CreateContainerVisual());

    m_micaController = winrt::MicaController();
    m_isMicaSupported = m_micaController.SetTarget(winrt::Microsoft::UI::WindowId{ reinterpret_cast<uint64_t>(m_window) }, m_target);
}
```

## How to use Mica in Win32 WebView2 apps

The fundamental principles of applying Mica are consistent across most Win32 applications. The process for [WebView2](/microsoft-edge/webview2/) follows the basic steps from the [Win32 instructions shown previously](#how-to-use-mica-in-win32-apps). However, in this case you will need to specify a single threaded process from WinRT's `init_apartment` feature.

> [!TIP]
> The code in this section is taken from the [Windows App SDK WebView2 Mica sample on GitHub](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Mica/cpp-WebView2). See the GitHub repo for the full code.

To get started, set up the required apartment, controller, compositor, target, and root. By default, the WinRT `init_apartment` function is multi-threated, but WebView2 is inherently single-threaded. To set `init_apartment` as a single thread, pass the `winrt::apartment_type::single_threaded` parameter. In the [Mica WebView2 Sample](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Mica/cpp-WebView2), we simplify the syntax by creating a separate class for web view functions, referenced in the following code.

> From [Main.cpp](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Mica/cpp-WebView2/Mica-WebView2/Main.cpp)

```cpp
int __stdcall WinMain(_In_ HINSTANCE, _In_opt_ HINSTANCE, _In_ PSTR, _In_ int)
{
    winrt::init_apartment(winrt::apartment_type::single_threaded);
    // Enable referencing the WindowsAppSDK from an unpackaged app.
    // Remember to have a matching Microsoft.WindowsAppRuntime.Redist installed.
    // https://learn.microsoft.com/windows/apps/windows-app-sdk/deploy-unpackaged-apps
    Utilities::WindowsAppSDKBootstrapperContext sdkContext;
    CompositionWindow::RegisterWindowClass();
    // A dispatcher queue is required to be able to create a compositor.
    auto controller = Utilities::CreateDispatcherQueueControllerForCurrentThread();
    auto compositor = winrt::Compositor();
    auto window = WebView2Window(compositor, L"Hello, WebView2!");

    ...
}
```

For a full demonstration of the **WebView2Window class** and its integration with Mica, see the [Windows App SDK WebView2 Mica sample on GitHub](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Mica/cpp-WebView2). Note how the [CompositionWindow](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Mica/cpp-WebView2/Mica-WebView2/CompositionWindow.cpp) and [WebView2Window](https://github.com/microsoft/WindowsAppSDK-Samples/blob/main/Samples/Mica/cpp-WebView2/Mica-WebView2/WebView2Window.cpp) classes handle messages, initialize the web view environment, and delete the window controller once the window is closed.

## Related articles

[Materials](../../../design/signature-experiences/materials.md), [Layering and Elevation](../../../design/signature-experiences/layering.md), [Windows App SDK Mica samples](https://github.com/microsoft/WindowsAppSDK-Samples/tree/main/Samples/Mica)