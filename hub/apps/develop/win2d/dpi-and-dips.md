---
title: DPI and DIPs
description: An explanation of the difference between physical pixels and device independent pixels, and how they are handled in Win2D.
ms.date: 05/26/2023
ms.topic: article
keywords: windows 10, windows 11, uwp, xaml, windows app sdk, winui, windows ui, graphics, games, effect win2d d2d d2d1 direct2d interop cpp csharp
ms.localizationpriority: medium
---

# DPI and DIPs

This article explains the difference between physical pixels and device independent pixels (DIPs), and how DPI (dots per inch) is handled in Win2D.

Win2D is designed in such a way that many apps can ignore this distinction, as it provides sensible default behaviors that will do the right thing when run on both low and high DPI devices. If your app has more specialized needs, or if you have a different opinion about what "sensible default" means, read on for the gory details...

## What is DPI?

DPI stands for "dots per inch". This is an approximate measure of the pixel density of an output display such as a computer monitor or phone screen. The higher the DPI, the more, smaller dots make up the display.

DPI is only an approximate measure because not all display hardware can be relied on to report accurate information. Some computer monitors do not report DPI to the operating system at all, or the user may have configured their system to render using a different DPI from the actual hardware (for instance to change the size of UI text elements). Applications can use DPI to choose how large things should be drawn, but should not rely on it as an exact physical measurement of the display size.

A DPI value of 96 is considered to be a neutral default.

## What is a pixel?

A pixel is a single colored dot. Images in computer graphics are made up of many pixels arranged in a two dimensional grid. You can think of pixels as the atoms out of which all images are built.

The physical size of a pixel can vary greatly from one display to another. When a computer is connected to a large but low resolution monitor or external display, pixels can be quite large, but on a phone with a 1080p display only a few inches across, pixels are tiny.

In Win2D, whenever you see an API that specifies a position or size using integer data types (or a struct such as BitmapSize that contains integers), this means the API is operating in pixel units.

Most Win2D APIs work with DIPs rather than pixels.

## What is a DIP?

DIP stands for "device independent pixel". This is a virtualized unit that may be the same as, larger, or smaller than a physical pixel.

The ratio between pixels and DIPs is determined by DPI:

```
pixels = dips * dpi / 96
```

When DPI is 96, pixels and DIPs are the same. When using higher DPI, a single DIP may correspond to more than one pixel (or parts of pixels in the common case where DPI is not an exact multiple of 96).

Most Windows Runtime APIs, including Win2D, use DIPs rather than pixels. This has the advantage of keeping graphics approximately the same physical size no matter what display an app is run on. For instance if an app specifies that a button is 100 DIPs wide, when run on a high DPI device such as a phone or 4k monitor this button will automatically scale to be more than 100 pixels in width, so it remains a sensible size that is possible for the user to click on. If the button size was specified in pixels, on the other hand, it would appear ridiculously small on this kind of high DPI display, so the app would have to do more work to adjust layouts differently for each kind of screen.

In Win2D, whenever you see an API that specifies a position or size using floating point data types (or structs such as Vector2 or Size that contain floating point values), this means the API is operating in DIPs.

To convert between DIPs and pixels, use the methods [`ConvertDipsToPixels(Single, CanvasDpiRounding)`](https://microsoft.github.io/Win2D/WinUI3/html/M_Microsoft_Graphics_Canvas_ICanvasResourceCreatorWithDpi_ConvertDipsToPixels.htm) and [`ConvertPixelsToDips(Int32)`](https://microsoft.github.io/Win2D/WinUI3/html/M_Microsoft_Graphics_Canvas_ICanvasResourceCreatorWithDpi_ConvertPixelsToDips.htm).

## Win2D resources that have DPI

All Win2D resources that contain a bitmap image also have an associated DPI property:
- [`CanvasBitmap`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasBitmap.htm)
- [`CanvasRenderTarget`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasRenderTarget.htm)
- [`CanvasSwapChain`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasSwapChain.htm)
- [`CanvasControl`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasControl.htm)
- [`CanvasVirtualControl`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasVirtualControl.htm)
- [`CanvasAnimatedControl`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasAnimatedControl.htm)
- [`CanvasImageSource`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasImageSource.htm)

All other resource types are independent of DPI. For instance a single [`CanvasDevice`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasDevice.htm) instance can be used to draw to controls or rendertargets of many different DPIs, therefore the device has no DPI of its own.

Similarly, [`CanvasCommandList`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasCommandList.htm) does not have a DPI, because it contains vector drawing instructions rather than a bitmap image. DPI only comes into play during the rasterization process, when the command list is drawn to a rendertarget or control (which do have DPI).

## Control DPI

The Win2D controls ([`CanvasControl`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasControl.htm), [`CanvasVirtualControl`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasVirtualControl.htm) and [`CanvasAnimatedControl`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasAnimatedControl.htm)) automatically use the same DPI as the display the app is running on. This matches the coordinate system used by XAML, CoreWindow, and other Windows Runtime APIs.

If the DPI changes (for instance if the app is moved to a different display), the control will raise the `CreateResources` event and pass a [`CanvasCreateResourcesReason`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_UI_CanvasCreateResourcesReason.htm) of [`DpiChanged`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_UI_CanvasCreateResourcesReason.htm). Apps should respond to this event by recreating any resources (such as rendertargets) that depend on the DPI of the control.

## Rendertarget DPI

Things that can be drawn onto (which includes not just [`CanvasRenderTarget`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasRenderTarget.htm) but also the rendertarget-like types [`CanvasSwapChain`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasSwapChain.htm) and [`CanvasImageSource`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_UI_Xaml_CanvasImageSource.htm)) have a DPI of their own, but unlike the controls these types are not directly connected to a display, so Win2D cannot automatically determine what the DPI should be. If you are drawing to a rendertarget which will later be copied to the screen, you probably want that rendertarget to use the same DPI as the screen, but if you are drawing for some other purpose (eg. generating images for upload to a website) a default 96 DPI would be more appropriate.

To make both these usage patterns easy, Win2D provides two types of constructor overload:

```
CanvasRenderTarget(ICanvasResourceCreator, width, height, dpi)
CanvasRenderTarget(ICanvasResourceCreatorWithDpi, width, height)
```

The [`ICanvasResourceCreator`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_ICanvasResourceCreator.htm) interface is implemented by `CanvasDevice` as well as the Win2D controls. Because a device does not have any specific DPI of its own, you must explicitly specify the DPI when creating a rendertarget from one.

For instance to create a default DPI rendertarget where DIPs and pixels will always be the same thing:

```csharp
const float defaultDpi = 96;
var rtWithFixedDpi = new CanvasRenderTarget(canvasDevice, width, height, defaultDpi);
```

[`ICanvasResourceCreatorWithDpi`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_ICanvasResourceCreatorWithDpi.htm) extends `ICanvasResourceCreator` by adding a DPI property. This interface is implemented by the Win2D controls, and makes it easy to create a rendertarget which will automatically inherit the same DPI as the control it was created from:

```csharp
var rtWithSameDpiAsDisplay = new CanvasRenderTarget(canvasControl, width, height);
```

## Bitmap DPI

[`CanvasBitmap`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasBitmap.htm), unlike a rendertarget, does not automatically inherit DPI from a control. The methods for creating and loading bitmaps include overloads to explicitly specify DPI, but if you leave this out, bitmap DPI defaults to 96 regardless of the current display configuration.

The reason bitmaps are different to other types is that they are a source of input data, rather than an output which will be drawn onto. So the important thing for bitmaps is not the DPI of where that output will end up, but the DPI of the source image, which is entirely unrelated to the current display settings.

If you load say a 100x100 default DPI bitmap and then draw it onto a rendertarget, the bitmap will be scaled from 100 DIPs at 96 DPI (which is 100 pixels) to 100 DIPs at the DPI of the destination rendertarget (which could be a larger number of pixels if it is a high DPI rendertarget). The resulting image will always be 100 DIPs in size (so there will be no unpleasant layout surprises), but it may suffer some blurring if a low DPI source bitmap was scaled up to a higher DPI destination.

For maximum clarity at high DPI, some applications may wish to provide multiple sets of bitmap images at different resolutions, and at load time select whichever version most closely matches the DPI of the destination control. Other apps may prefer to ship only high DPI bitmaps, and let Win2D scale these down when running on lower DPI displays (scaling down can often look better than scaling up). In either case, the bitmap DPI can be specified as a parameter to [`LoadAsync(ICanvasResourceCreator, String, Single)`](https://microsoft.github.io/Win2D/WinUI3/html/M_Microsoft_Graphics_Canvas_CanvasBitmap_LoadAsync_1.htm).

Note that some bitmap file formats contain DPI metadata of their own, but Win2D ignores this since it is often set incorrectly. Instead, DPI must be explicitly specified when loading the bitmap.

## CanvasDrawingSession DPI

[`CanvasDrawingSession`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasDrawingSession.htm) inherits its DPI from whatever control, rendertarget, swapchain, etc, it is drawing onto.

By default, all drawing operations operate in DIPs. If you prefer to work in pixels, this can be changed via the [`Units`](https://microsoft.github.io/Win2D/WinUI3/html/P_Microsoft_Graphics_Canvas_CanvasDrawingSession_Units.htm) property.

## Effect DPI

The image effect pipeline inherits its DPI from whatever [`CanvasDrawingSession`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasDrawingSession.htm) an effect is being drawn onto. Internally, effect processing always operates in pixels. Parameter values such as sizes or positions are specified in DIPs, but these units are converted to pixels before any actual image manipulation takes place.

When a bitmap of different DPI than the target drawing session is used as an effect source image, an internal [`DpiCompensationEffect`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Effects_DpiCompensationEffect.htm) is automatically inserted in between the bitmap and the effect. This scales the bitmap to match the target DPI, which is usually what you want. If it's not what you want, you can insert your own instance of [`DpiCompensationEffect`](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Effects_DpiCompensationEffect.htm) to customize the behavior.

> [!NOTE]
> If implementing a custom effect, consider applying an equivalent DPI handling scheme to ensure consistent behavior when used together with built-in Win2D effects.

## Composition API

The `Microsoft.Graphics.Canvas.Composition` APIs operate at a lower level than Win2D XAML controls, so they do not attempt to automatically handle DPI on your behalf. It is up to you to decide what units you prefer to operate in, and set whatever transforms are necessary to achieve that as part of your composition visual tree.

`Windows.UI.Composition` APIs such as `CreateDrawingSurface` always specify sizes in pixel units. When using Win2D to draw onto a composition surface, you can specify whatever DPI you want to use when calling [`CreateDrawingSession(CompositionDrawingSurface, Rect, Single)`](https://microsoft.github.io/Win2D/WinUI3/html/M_Microsoft_Graphics_Canvas_UI_Composition_CanvasComposition_CreateDrawingSession_2.htm). All drawing performed through the returned `CanvasDrawingSession` will be scaled up or down accordingly.

## How to test DPI handling

The easiest way to test that your app will do the right thing in response to changing display DPI is to run on Windows 10 or Windows 11 and change display settings while the app is running:

- Right-click on the desktop background and choose 'Display settings'
- Move the slider labeled 'Change the size of text, apps, and other items'
- Click the 'Apply' button
- Choose 'Sign out later'

If you do not have Windows 10 or Windows 11, you can also test with the Windows Simulator. In the Visual Studio toolbar, change the "Local Machine" setting to "Simulator", then use the Change Resolution icon to switch the simulated display between:

- 100% (DPI = 96)
- 140% (DPI = 134.4)
- 180% (DPI = 172.8)