---
Description: Windows desktop applications can pin secondary tiles thanks to the Desktop Bridge!
title: Pin secondary tiles from desktop application
label: Pin secondary tiles from desktop application
template: detail.hbs
ms.date: 05/25/2017
ms.topic: article
keywords: windows 10, desktop bridge, secondary tiles, pin, pinning, quickstart, code sample, example, secondarytile, desktop application, win32, winforms, wpf
ms.localizationpriority: medium
---
# Pin secondary tiles from desktop application


Thanks to the [Desktop Bridge](https://developer.microsoft.com/windows/bridges/desktop), Windows desktop applications (like Win32, Windows Forms, and WPF) can pin secondary tiles!

![Screenshot of secondary tiles](images/secondarytiles.png)

> [!IMPORTANT]
> **Requires Fall Creators Update**: You must target SDK 16299 and be running build 16299 or later to pin secondary tiles from Desktop Bridge apps.

Adding a secondary tile from your WPF or WinForms application is very similar to a pure UWP app. The only difference is that you must specify your main window handle (HWND). This is because when pinning a tile, Windows displays a modal dialog asking the user to confirm whether they would like to pin the tile. If the desktop application doesn't configure the SecondaryTile object with the owner window, Windows doesn't know where to draw the dialog and the operation will fail.


## Package your app with Desktop Bridge

If you have not packaged your app with the Desktop Bridge, [you must do so first](/windows/msix/desktop/source-code-overview) before you can use any Windows Runtime APIs.


## Enable access to IInitializeWithWindow interface

If your application is written in a managed language such as C# or Visual Basic, declare the IInitializeWithWindow interface in your app's code with the [ComImport](/dotnet/api/system.runtime.interopservices.comimportattribute) and Guid attribute as shown in the following C# example. This example assumes that your code file has a using statement for the System.Runtime.InteropServices namespace.

```csharp
[ComImport]
[Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IInitializeWithWindow
{
    void Initialize(IntPtr hwnd);
}
```

Alternatively, if you are using C++, add a reference to the **shobjidl.h** header file in your code. This header file contains the declaration of the *IInitializeWithWindow* interface.


## Initialize the secondary tile

Initialize a new secondary tile object exactly like you would with a normal UWP app. To learn more about creating and pinning secondary tiles, see [Pin secondary tiles](secondary-tiles-pinning.md).

```csharp
// Initialize the tile with required arguments
SecondaryTile tile = new SecondaryTile(
    "myTileId5391",
    "Display name",
    "myActivationArgs",
    new Uri("ms-appx:///Assets/Square150x150Logo.png"),
    TileSize.Default);
```


## Assign the window handle

This is the key step for desktop applications. Cast the object to an [IInitializeWithWindow](/windows/desktop/api/shobjidl_core/nn-shobjidl_core-iinitializewithwindow) object. Then, call the [IInitializeWithWindow.Initialize](/windows/desktop/api/shobjidl_core/nf-shobjidl_core-iinitializewithwindow-initialize) method, and pass the handle of the window that you want to be the owner for the modal dialog. The following C# example shows how to pass the handle of your app’s main window to the method.

```csharp
// Assign the window handle
IInitializeWithWindow initWindow = (IInitializeWithWindow)(object)tile;
initWindow.Initialize(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle);
```


## Pin the tile

Finally, request to pin the tile as you would a normal UWP app.

```csharp
// Pin the tile
bool isPinned = await tile.RequestCreateAsync();

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