---
description: Desktop applications can pin secondary tiles thanks to the Desktop Bridge!
title: Pin secondary tiles from desktop apps
label: Pin secondary tiles from desktop apps
template: detail.hbs
ms.date: 05/25/2017
ms.topic: article
keywords: windows 10, desktop bridge, secondary tiles, pin, pinning, quickstart, code sample, example, secondarytile, desktop application, win32, winforms, wpf
ms.localizationpriority: medium
---
# Pin secondary tiles from desktop apps

Desktop applications such as Windows App SDK (using WinUI 3), WPF, and Windows Forms can pin secondary tiles by using an [MSIX package](https://developer.microsoft.com/windows/bridges/desktop). This is also previously known as Desktop Bridge.

![Screenshot of secondary tiles](images/secondarytiles.png)

> [!IMPORTANT]
> **Requires Fall Creators Update**: You must target SDK 16299 and be running build 16299 or later to pin secondary tiles from Desktop Bridge apps.

Adding a secondary tile from your Windows App SDK, WPF, or WinForms application is very similar to a pure UWP app. The only difference is that you must specify your main window handle (HWND). This is because when pinning a tile, Windows displays a modal dialog asking the user to confirm whether they would like to pin the tile. If the desktop application doesn't configure the SecondaryTile object with the owner window, Windows doesn't know where to draw the dialog and the operation will fail.

## Package your app

If you are creating a Windows App SDK application with WinUI 3, you must use a packaged application. There are no extra steps required to package your app if you start with the packaged app template.

If you are using WPF or WinForms and have not packaged your app with the Desktop Bridge, [you must do so first](/windows/msix/desktop/source-code-overview) before you can use any Windows Runtime APIs.

## Initialize and pin the secondary tile using the IInitializeWithWindow interface

#### [C# (.NET 5 or later)](#tab/csharpnet5)

1. Make sure that the `TargetFramework` property in the project file is [set to a specific Windows SDK version to access the Windows Runtime APIs](/windows/apps/desktop/modernize/desktop-to-uwp-enhance#net-5-and-later-use-the-target-framework-moniker-option), which provides access to the **WinRT.Interop** namespace. For example:

    ```xml
    <PropertyGroup>
      <!-- You can also target other versions of the Windows SDK and .NET, e.g. "net5.0-windows10.0.19041.0" -->
      <TargetFramework>net6.0-windows10.0.22000.0</TargetFramework>
    </PropertyGroup>
    ```

2. Initialize a new secondary tile object exactly like you would with a normal UWP app. To learn more about creating and pinning secondary tiles, see [Pin secondary tiles](secondary-tiles-pinning.md).

    ```csharp
    // Initialize the tile with required arguments
    SecondaryTile tile = new SecondaryTile(
        "myTileId5391",
        "Display name",
        "myActivationArgs",
        new Uri("ms-appx:///Assets/Square150x150Logo.png"),
        TileSize.Default);
    ```

3. Obtain the window handle using the **WinRT.Interop.WindowNative.GetWindowHandle** method, where **this** is a pointer to the Window object (whether a WinUI 3 window, WPF window, or WinForms window). Use the **WinRT.Interop.InitializeWithWindow.Initialize** method to initialize the secondary tile object with the specified window handle.

    ```csharp
    var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
    WinRT.Interop.InitializeWithWindow.Initialize(tile, hwnd);
    ```

    See [Call WinRT COM interop interfaces from .NET 5 or later apps](/windows/apps/desktop/modernize/winrt-com-interop-csharp#configure-a-net-5-desktop-project-to-use-the-c-interop-classes) for more details on these methods.

4. Finally, request to pin the tile as you would a normal UWP app.

    ```csharp
    // Pin the tile
    bool isPinned = await tile.RequestCreateAsync();

    // TODO: Update UI to reflect whether user can now either unpin or pin
    ```
#### [C# (Earlier versions of .NET)](#tab/csharp)

1. Declare the IInitializeWithWindow interface in your app's code with the [ComImport](/dotnet/api/system.runtime.interopservices.comimportattribute) and Guid attribute as shown in the following C# example. This example assumes that your code file has a using statement for the System.Runtime.InteropServices namespace.

    ```csharp
    [ComImport]
    [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IInitializeWithWindow
    {
        void Initialize(IntPtr hwnd);
    }
    ```

2. Initialize a new secondary tile object exactly like you would with a normal UWP app. To learn more about creating and pinning secondary tiles, see [Pin secondary tiles](secondary-tiles-pinning.md).

    ```csharp
    // Initialize the tile with required arguments
    SecondaryTile tile = new SecondaryTile(
        "myTileId5391",
        "Display name",
        "myActivationArgs",
        new Uri("ms-appx:///Assets/Square150x150Logo.png"),
        TileSize.Default);
    ```

3. Assign the window handle. This is the key step for desktop applications. Cast the object to an [IInitializeWithWindow](/windows/desktop/api/shobjidl_core/nn-shobjidl_core-iinitializewithwindow) object. Then, call the [IInitializeWithWindow.Initialize](/windows/desktop/api/shobjidl_core/nf-shobjidl_core-iinitializewithwindow-initialize) method, and pass the handle of the window that you want to be the owner for the modal dialog. The following C# example shows how to pass the handle of your app’s main window to the method.

    ```csharp
    // Assign the window handle
    IInitializeWithWindow initWindow = (IInitializeWithWindow)(object)tile;
    initWindow.Initialize(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle);
    ```

4. Finally, request to pin the tile as you would a normal UWP app.

    ```csharp
    // Pin the tile
    bool isPinned = await tile.RequestCreateAsync();

    // TODO: Update UI to reflect whether user can now either unpin or pin
    ```

#### [C++](#tab/cpp)

1. Add a reference to the following header files in your code:
- **shobjidl.h**: contains the declaration of the *IInitializeWithWindow* interface. 
- **winrt/Windows.Foundation.h**: contains *Windows::Foundation::Uri*
- **Windows.UI.StartScreen.h**: contains *Windows::UI::StartScreen*

    ```cpp
    #include <shobjidl.h>
    #include <Microsoft.UI.Xaml.Window.h>
    #include <winrt/Windows.UI.StartScreen.h>
    #include <winrt/Windows.Foundation.h> 
    ```

2. Initialize a new secondary tile object exactly like you would with a normal UWP app. To learn more about creating and pinning secondary tiles, see [Pin secondary tiles](secondary-tiles-pinning.md).

    ```cpp
    auto tile = Windows::UI::StartScreen::SecondaryTile(
            L"myTileId5391",
            L"Display name",
            L"myActivationArgs",
            Windows::Foundation::Uri(L"ms-appx:///Assets/Square150x150Logo.png"),
            Windows::UI::StartScreen::TileSize::Default);
    ```
3. Retrieve and pass in the window handle to pop the modal dialog. The code snippet below is an example of a helper method in a Windows App SDK app to get an HWND from a [WinUI 3 Window](windows/winui/api/microsoft.ui.xaml.window?view=winui-3.0&preserve-view=true) object.

    ```cpp
    // Helper method to retrieve the window handle a WinUI 3 Window object 
    HWND GetHWNDFromWindow(winrt::Microsoft::UI::Xaml::Window const& window)
    {
        HWND nativeWindow{ nullptr };
        winrt::check_hresult(window.as<IWindowNative>()->get_WindowHandle(&nativeWindow));
        return nativeWindow;
    }
    ```

    The following code casts the secondary tile object to an [IInitializeWithWindow](/windows/desktop/api/shobjidl_core/nn-shobjidl_core-iinitializewithwindow) object and calls the [IInitializeWithWindow.Initialize](/windows/desktop/api/shobjidl_core/nf-shobjidl_core-iinitializewithwindow-initialize) method, passing the handle of the window that you want to be the owner for the modal dialog.

    ```cpp
    // Pass in a Window object (this) to the helper method to get the HWND
    HWND hwnd = GetHWNDFromWindow(*this);
    tile.as<IInitializeWithWindow>()->Initialize(hwnd);
    ```

4. Finally, request to pin the tile as you would a normal UWP app.

    ```cpp
    // Pin the tile
    bool isPinned = co_await tile.RequestCreateAsync();

    // TODO: Update UI to reflect whether user can now either unpin or pin
    ```

## Send tile notifications

> [!IMPORTANT]
> **Requires April 2018 version 17134.81 or later**: You must be running build 17134.81 or later to send tile or badge notifications to secondary tiles from Desktop Bridge apps. Before this .81 servicing update, a 0x80070490 *Element not found* exception would occur when sending tile or badge notifications to secondary tiles from Desktop Bridge apps.

Sending tile or badge notifications is the same as UWP apps. See [Send a local tile notification](sending-a-local-tile-notification.md) to get started.

## Resources

* [Full code sample](https://github.com/Microsoft/DesktopBridgeToUWP-Samples/tree/master/Samples/SecondaryTileSample)
* [Secondary tiles overview](secondary-tiles.md)
* [Pin secondary tiles (UWP)](secondary-tiles-pinning.md)
* [Desktop Bridge](https://developer.microsoft.com/windows/bridges/desktop)
* [Desktop Bridge code samples](https://github.com/Microsoft/DesktopBridgeToUWP-Samples)
