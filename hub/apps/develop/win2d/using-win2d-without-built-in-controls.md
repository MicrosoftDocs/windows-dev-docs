---
title: Using Win2D without built-in controls
description: A guide on how to use the lower level APIs of Win2D, without any of its built-in XAML controls.
ms.date: 05/28/2023
ms.topic: article
keywords: windows 10, windows 11, uwp, xaml, windows app sdk, winui, windows ui, graphics, games, effect win2d d2d d2d1 direct2d interop cpp csharp
ms.localizationpriority: medium
---

# Using Win2D without built-in controls

[`CanvasControl`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasControl.htm), [`CanvasVirtualControl`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasVirtualControl.htm) and [`CanvasAnimatedControl`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasAnimatedControl.htm) are XAML controls - they extend `UserControl` and can exist alongside other controls in an app's XAML tree. They are good choice for many WinRT apps that use XAML and produce graphical content using Win2D. While these controls are versatile, they do impose policies pertaining to layout, resource re-creation, and device lost. Apps may want to implement their own XAML controls, or not use XAML at all.

Win2D is built to support this. This document describes how to use Win2D to draw graphics without use of `CanvasControl`, `CanvasVirtualControl` or `CanvasAnimatedControl`.

## Layering

The Win2D XAML controls are built of top of a low level Win2D type. Each control contains an instance of a lower-level type:

| Control | Low-level type |
| -- | -- |
| `CanvasControl` | [`CanvasImageSource`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasImageSource.htm) |
| `CanvasVirtualControl` | [`CanvasVirtualImageSource`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasVirtualImageSource.htm) |
| `CanvasAnimatedControl` | [`CanvasSwapChainPanel`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasSwapChainPanel.htm) and [`CanvasSwapChain`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_CanvasSwapChain.htm) |

The controls consume only the public interfaces of these lower-level types. This implementation detail lends some confidence that apps can implement their own XAML controls which are equivalently as powerful as the built-in Win2D controls.

## Choosing a lower-level type

An application is free to bypass the provided Win2D XAML controls and use the lower-level types directly. This section provides some guidance on choosing the appropriate lower-level type.

If there is existing C++ code that works with `IDXGISwapChain` then [C++ interop](./interop.md) can be used to wrap this with a `CanvasSwapChain`. `CanvasSwapChainPanel` can be used to display a `CanvasSwapChain` in a XAML app.

If the app needs to add graphics to a XAML element which expects an ImageSource, it should use `CanvasImageSource` or `CanvasVirtualImageSource`.

Aside from that, it's worth considering:

- Swap chains are suited for content which will animate very frequently and the animation should be smooth. Direct3D's swap chains are designed for this purpose. The content of a swap chain can be re-drawn with a low latency, as it is not tied to the XAML framework refresh timer.
- `CanvasSwapChain` has heavier resource costs than `CanvasImageSource`. It is generally not desirable to have more than one or two swap chains onscreen at a time. For example, if an application has a page full of widgets, where each widget is a standalone graphical element, it is more appropriate to make each widget use a `CanvasImageSource` resource than a `CanvasSwapChain`.
- `CanvasImageSource` can be manipulated by other XAML UI elements such as transforms or opacity changes, while `CanvasSwapChain` cannot.
- `CanvasVirtualImageSource` can be used to display images that are much larger than the screen (for example in a `ScrollViewer`).

## CanvasSwapChain and CoreWindow

[`CanvasSwapChain`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_CanvasSwapChain.htm) wraps a Direct3D swap chain. `CanvasSwapChain` is not a XAML type, but the fact that it has a swap chain means it has a built-in mechanism for being displayed. That said, [`CoreWindow`](https://msdn.microsoft.com/library/windows.ui.core.corewindow.aspx) apps may use `CanvasSwapChain` for displaying graphical content.

To create a `CanvasSwapChain` for use with a `CoreWindow`, in C#:

```csharp
float currentDpi = DisplayInformation.GetForCurrentView().LogicalDpi;

SwapChain = CanvasSwapChain.CreateForCoreWindow(device, window, currentDpi);
```

To draw content using a `CanvasSwapChain`, call its [`CreateDrawingSession(Color)`](https://microsoft.github.io/Win2D/WinUI2/html/M_Microsoft_Graphics_Canvas_CanvasSwapChain_CreateDrawingSession.htm) method:

```csharp
using (CanvasDrawingSession ds = swapChain.CreateDrawingSession(Colors.Black))
{
    ds.FillRectangle(100, 200, 3, 5);
}

swapChain.Present();
```

The size of the swap chain should match the size of the `CoreWindow`. If the size of the window changes, call [`ResizeBuffers(Size)`](https://microsoft.github.io/Win2D/WinUI2/html/M_Microsoft_Graphics_Canvas_CanvasSwapChain_ResizeBuffers_3.htm) on the swap chain with the new size. For more information, see the `CoreWindow` Win2D sample.

> [!NOTE]
> `CoreWindow` is only supported on UWP. For WinAppSDK apps, use either composition or HWND swapchains.

## CanvasSwapChainPanel

[`CanvasSwapChainPanel`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasSwapChainPanel.htm) is a XAML type, and a relatively thin wrapper around `CanvasSwapChain`. It is suitable for XAML apps that require swap chain rendering, but do not want to use the policies that exist in `CanvasAnimatedControl`. To create a swap chain in XAML, use the namespace:

```XAML
xmlns:canvas="using:Microsoft.Graphics.Canvas.UI.Xaml"
```

And declare:

```XAML
<canvas:CanvasSwapChainPanel x:Name="canvasSwapChainPanel"/>
```

A `CanvasSwapChainPanel` does not have a swap chain automatically assigned to it. An example, to assign one:

```csharp
CanvasDevice device = CanvasDevice.GetSharedDevice();
CanvasSwapChain swapChain = new CanvasSwapChain(device, width, height, 96);

canvasSwapChainPanel.SwapChain = swapChain;
```

To draw to the swap chain panel, in C#, use its `SwapChain` property:

```csharp
using (CanvasDrawingSession ds = canvasSwapChainPanel.SwapChain.CreateDrawingSession(Colors.Black))
{
    ds.FillRectangle(200, 300, 5, 6, Colors.Blue);
}

canvasSwapChainPanel.SwapChain.Present();
```

The application decides on the frequency of redrawing. In the same manner as using `CanvasSwapChain` directly, it is up to the app to resize the swap chain when the control is resized. For example, in C#:

```csharp
canvasSwapChainPanel.SizeChanged += canvasSwapChainPanel_SizeChanged;

void canvasSwapChainPanel_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
{
    canvasSwapChainPanel.SwapChain.ResizeBuffers(e.NewSize);
}
```

## Image Sources

[`CanvasImageSource`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasImageSource.htm) and [`CanvasVirtualImageSource`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasVirtualImageSource.htm) provide a way of integrating Win2D graphical content with XAML. They are suitable for content which does not require swap chain rendering.

### CanvasImageSource

[`CanvasImageSource`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasImageSource.htm) extends XAML's [`SurfaceImageSource`](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.imaging.surfaceimagesource.aspx). Apps can create an instance of `CanvasImageSource`, and reference it from a XAML type that consumes an `ImageSource`, such as an [`Image`](https://msdn.microsoft.com/library/windows/apps/br242752.aspx) or [`ImageBrush`](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.imagebrush). For example, in XAML markup:

```XAML
<Image x:Name="image"/>
```

Then, in C#:

```csharp
CanvasDevice device = CanvasDevice.GetSharedDevice();
CanvasImageSource imageSource = new CanvasImageSource(device, width, height);

image.Source = imageSource;
```

Drawing sessions are created directly on the image source object. For example:

```csharp
using (CanvasDrawingSession ds = imageSource.CreateDrawingSession(Colors.Black))
{
    ds.FillRectangle(200, 300, 5, 6, Colors.Blue);
}
```

Note that a clear color must be passed to `CreateDrawingSession`. Whenever a drawing session is created on a `CanvasImageSource`, the `CanvasImageSource` is cleared.

For an example demonstrating how to use `CanvasImageSource`, see the `ImageSourceUpdateRegion` Win2D ExampleGallery page.

### CanvasVirtualImageSource

[`CanvasVirtualImageSource`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasVirtualImageSource.htm) wraps XAML's [`VirtualSurfaceImageSource`](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.imaging.virtualsurfaceimagesource.aspx).

The wrapped `VirtualSurfaceImageSource` can be obtained by the [`Source`](https://microsoft.github.io/Win2D/WinUI2/html/P_Microsoft_Graphics_Canvas_UI_Xaml_CanvasVirtualImageSource_Source.htm) property. Apart from this difference it can be used in much the same way as `CanvasImageSource`:

```XAML
<Image x:Name="image"/>
```

```csharp
var virtualImageSource = new CanvasVirtualImageSource(device, width, height);

image.Source = virtualImageSource.Source;
```

## CanvasRenderTarget

`CanvasRenderTarget` represents a drawable bitmap, and does not have any built-in association with XAML. It is suitable for XAML or non-XAML apps which need to use an intermediate bitmap- for example, for saving image data to a file, reading back pixel data, or to be used as an intermediate for another operation.

`CanvasRenderTarget` doesn't have an automatic mechanism to cause it to be displayed. To display the content of a `CanvasRenderTarget` in your app, draw it in the drawing session created from a displayed control, image source, or swap chain.

For more information about using `CanvasRenderTarget`, see [Offscreen drawing](./offscreen-drawing.md).