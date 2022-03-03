---
title: Pin secondary tiles from desktop apps
description: A desktop application such as a Windows App SDK (using WinUI 3), Windows Presentation Foundation (WPF), or Windows Forms (WinForms) app can pin a secondary tile by using an [MSIX package](https://developer.microsoft.com/windows/bridges/desktop). This is formerly known as Desktop Bridge.
label: Pin secondary tiles from desktop apps
template: detail.hbs
ms.date: 03/03/2022
ms.topic: article
keywords: windows 10, desktop bridge, secondary tiles, pin, pinning, quickstart, code sample, example, secondarytile, desktop application, win32, winforms, wpf
ms.localizationpriority: medium
---

# Pin secondary tiles from desktop apps

A desktop application such as a Windows App SDK (using WinUI 3), Windows Presentation Foundation (WPF), or Windows Forms (WinForms) app can pin a secondary tile by using an [MSIX package](https://developer.microsoft.com/windows/bridges/desktop). This is formerly known as Desktop Bridge.

![Screenshot of secondary tiles](images/secondarytiles.png)

> [!IMPORTANT]
> **Requires Fall Creators Update**: You must target SDK 16299 and be running build 16299 or later to pin secondary tiles from Desktop Bridge apps.

Adding a secondary tile from your Windows App SDK, WPF, or WinForms application is very similar to a pure UWP app. The only difference is that you must specify your main window handle (HWND). This is because when pinning a tile, Windows displays a modal dialog asking the user to confirm whether they would like to pin the tile. If the desktop application doesn't configure the SecondaryTile object with the owner window, Windows doesn't know where to draw the dialog and the operation will fail.

## Package your app

If you're creating a Windows App SDK application with WinUI 3, you must use a packaged application to pin secondary tiles. There are no extra steps required to package your app if you start with the packaged app template.

If you're using WPF or WinForms, and you haven't packaged your app with the Desktop Bridge, then you'll need to do that before you can use any Windows Runtime APIs (see [Building an MSIX package from your code](/windows/msix/desktop/source-code-overview)).

## Initialize and pin a secondary tile using the IInitializeWithWindow interface

### [C# (.NET 5 or later)](#tab/csharpnet5)

1. In the project file, set the **TargetFramework** property to a value that gives you access to the Windows Runtime APIs (see [.NET 5 and later: Use the Target Framework Moniker option](/windows/apps/desktop/modernize/desktop-to-uwp-enhance#net-5-and-later-use-the-target-framework-moniker-option)). That includes access to the **WinRT.Interop** namespace (see [Call interop APIs from a .NET 5+ app](/windows/apps/desktop/modernize/winrt-com-interop-csharp#available-via-target-framework-moniker)). For example:

    ```xml
    <PropertyGroup>
      <!-- You can also target other versions of the Windows SDK and .NET; for example, "net5.0-windows10.0.19041.0" -->
      <TargetFramework>net6.0-windows10.0.22000.0</TargetFramework>
    </PropertyGroup>
    ```

1. Initialize a new secondary tile object exactly like you would with a normal UWP app. To learn more about creating and pinning secondary tiles, see [Pin secondary tiles](secondary-tiles-pinning.md).

    ```csharp
    // Initialize the tile with required arguments
    SecondaryTile tile = new SecondaryTile(
        "myTileId5391",
        "Display name",
        "myActivationArgs",
        new Uri("ms-appx:///Assets/Square150x150Logo.png"),
        TileSize.Default);
    ```

1. Retrieve a window handle, and initialize the secondary tile object with that handle. In the code below, `this` is a reference to the Window object (whether a WinUI 3 window, a WPF window, or a WinForms window). For more info, see [Retrieve a window handle (HWND)](/windows/apps/develop/ui-input/retrieve-hwnd) and [Display Windows.\*-namespace UI objects](/windows/apps/develop/ui-input/display-ui-objects).

    ```csharp
    var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
    WinRT.Interop.InitializeWithWindow.Initialize(tile, hWnd);
    ```

1. Finally, request to pin the tile as you would in a normal UWP app.

    ```csharp
    // Pin the tile
    bool isPinned = await tile.RequestCreateAsync();

    // Here, update UI to reflect whether user can now either unpin or pin
    ```

### [C# (Earlier versions of .NET)](#tab/csharp)

1. Declare the **IInitializeWithWindow** interface in your app's code with the [**ComImport**](/dotnet/api/system.runtime.interopservices.comimportattribute) and **Guid** attributes, as shown in the following C# example. For this example to compile, your code file will need a `using` directive for the **System.Runtime.InteropServices** namespace.

    ```csharp
    [ComImport]
    [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IInitializeWithWindow
    {
        void Initialize(IntPtr hWnd);
    }
    ```

1. Initialize a new secondary tile object exactly like you would with a normal UWP app. To learn more about creating and pinning secondary tiles, see [Pin secondary tiles](secondary-tiles-pinning.md).

    ```csharp
    // Initialize the tile with required arguments
    SecondaryTile tile = new SecondaryTile(
        "myTileId5391",
        "Display name",
        "myActivationArgs",
        new Uri("ms-appx:///Assets/Square150x150Logo.png"),
        TileSize.Default);
    ```

1. Assign the window handle. This is the key step for desktop applications. Cast the object to an [IInitializeWithWindow](/windows/desktop/api/shobjidl_core/nn-shobjidl_core-iinitializewithwindow) object. Then, call the [IInitializeWithWindow.Initialize](/windows/desktop/api/shobjidl_core/nf-shobjidl_core-iinitializewithwindow-initialize) method, and pass the handle of the window that you want to be the owner for the modal dialog. The following C# example shows how to pass the handle of your app’s main window to the method.

    ```csharp
    // Assign the window handle
    IInitializeWithWindow initWindow = (IInitializeWithWindow)(object)tile;
    initWindow.Initialize(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle);
    ```

1. Finally, request to pin the tile as you would a normal UWP app.

    ```csharp
    // Pin the tile
    bool isPinned = await tile.RequestCreateAsync();

    // TODO: Update UI to reflect whether user can now either unpin or pin
    ```

### [C++](#tab/cpp)

1. Add the following includes to your `pch.h` file. `shobjidl.h` provides access to the **IInitializeWithWindow** interface; `Microsoft.UI.Xaml.Window.h` provides access to the WinUI 3 **Window** class.

    ```cppwinrt
    #include <shobjidl.h>
    #include <winrt/Windows.Foundation.h> 
    #include <winrt/Windows.UI.StartScreen.h>
    #include <Microsoft.UI.Xaml.Window.h>
    ```

1. Initialize a new secondary tile object exactly like you would with a normal UWP app. To learn more about creating and pinning secondary tiles, see [Pin secondary tiles](secondary-tiles-pinning.md).

    ```cppwinrt
    auto tile = Windows::UI::StartScreen::SecondaryTile(
            L"myTileId5391",
            L"Display name",
            L"myActivationArgs",
            Windows::Foundation::Uri(L"ms-appx:///Assets/Square150x150Logo.png"),
            Windows::UI::StartScreen::TileSize::Default);
    ```

1. Retrieve a window handle, and initialize the secondary tile object with that handle. In the code below, `this` is a pointer to a [WinUI 3 Window](/windows/winui/api/microsoft.ui.xaml.window) object. For more info, see [Retrieve a window handle (HWND)](/windows/apps/develop/ui-input/retrieve-hwnd) and [Display Windows.\*-namespace UI objects](/windows/apps/develop/ui-input/display-ui-objects).

    ```cppwinrt
    auto windowNative{ this->try_as<::IWindowNative>() };
    winrt::check_bool(windowNative);
    HWND hWnd{ 0 };
    windowNative->get_WindowHandle(&hWnd);

    tile.as<IInitializeWithWindow>()->Initialize(hWnd);
    ```

1. Finally, request to pin the tile as you would in a normal UWP app.

    ```cppwinrt
    // Pin the tile
    bool isPinned = co_await tile.RequestCreateAsync();

    // Here, update UI to reflect whether user can now either unpin or pin
    ```

---

## Send tile notifications

> [!IMPORTANT]
> **Requires April 2018 version 17134.81 or later**: You must be running build 17134.81 or later to send tile or badge notifications to secondary tiles from Desktop Bridge apps. Before this .81 servicing update, a 0x80070490 *Element not found* exception would occur when sending tile or badge notifications to secondary tiles from Desktop Bridge apps.

Sending tile or badge notifications is the same as UWP apps. See [Send a local tile notification](sending-a-local-tile-notification.md) to get started.

## Resources

* [Secondary tile sample app](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/SecondaryTileSample)
* [Secondary tiles overview](secondary-tiles.md)
* [Pin secondary tiles (UWP)](secondary-tiles-pinning.md)
* [Desktop Bridge](https://developer.microsoft.com/windows/bridges/desktop)
* [Desktop Bridge code samples](https://github.com/Microsoft/DesktopBridgeToUWP-Samples)
