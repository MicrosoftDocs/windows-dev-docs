---
author: scottmill
ms.assetid: 03dd256f-78c0-e1b1-3d9f-7b3afab29b2f
title: Composition brushes
description: A brush paints the area of a Visual with its output. Different brushes have different types of output.
ms.author: scotmi
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---
# Composition brushes

\[ Updated for UWP apps on Windows 10. For Windows 8.x articles, see the [archive](http://go.microsoft.com/fwlink/p/?linkid=619132) \]

A brush paints the area of a [**Visual**](https://msdn.microsoft.com/library/windows/apps/Dn706858) with its output. Different brushes have different types of output. The Composition API provides three brush types:

-   [**CompositionColorBrush**](https://msdn.microsoft.com/library/windows/apps/Mt589399) paints a visual with a solid color
-   [**CompositionSurfaceBrush**](https://msdn.microsoft.com/library/windows/apps/Mt589415) paints a visual with the contents of a composition surface
-   [**CompositionEffectBrush**](https://msdn.microsoft.com/library/windows/apps/Mt589406) paints a visual with the contents of a composition effect

All brushes inherit from [**CompositionBrush**](https://msdn.microsoft.com/library/windows/apps/Mt589398); they are created directly or indirectly by the [**Compositor**](https://msdn.microsoft.com/library/windows/apps/Dn706789) and are device-independent resources. Although brushes are device-independent, [**CompositionSurfaceBrush**](https://msdn.microsoft.com/library/windows/apps/Mt589415) and [**CompositionEffectBrush**](https://msdn.microsoft.com/library/windows/apps/Mt589406) paint a [**Visual**](https://msdn.microsoft.com/library/windows/apps/Dn706858) with contents from a composition surface which are device-dependent.

-   [Prerequisites](./composition-brushes.md#prerequisites)
-   [Color Basics](./composition-brushes.md#color-basics)
    -   [Alpha Modes](./composition-brushes.md#alpha-modes)
-   [Using Color Brush](./composition-brushes.md#using-color-brush)
-   [Using Surface Brush](./composition-brushes.md#using-surface-brush)
-   [Configuring Stretch and Alignment](./composition-brushes.md#configuring-stretch-and-alignment)

## Prerequisites

This overview assumes that you are familiar with the structure of a basic Composition application, as described in [Composition UI](visual-layer.md).

## Color Basics

Before you paint with a [**CompositionColorBrush**](https://msdn.microsoft.com/library/windows/apps/Mt589399), you need to choose colors. The Composition API uses the Windows Runtime structure, Color, to represent a color. The Color structure uses sRGB encoding. sRGB encoding divides colors into four channels: alpha, red, green, and blue. Each component is represented by a floating point value with a normal range of 0.0 to 1.0. A value of 0.0 indicates the complete absence of that color, while a value of 1.0 indicates that the color is fully present. For the alpha component, 0.0 represents a fully transparent color and 1.0 represents a fully opaque color.

### Alpha Modes

Color values in [**CompositionColorBrush**](https://msdn.microsoft.com/library/windows/apps/Mt589399) are always interpreted as straight alpha.

## Using Color Brush

To create a color brush, call the Compositor.[**CreateColorBrush**](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.compositor.createcolorbrush.aspx) method, which returns a [**CompositionColorBrush**](https://msdn.microsoft.com/library/windows/apps/Mt589399). The default color for **CompositionColorBrush** is \#00000000. The following illustration and code shows a small visual tree to create a rectangle that is stroked with a black color brush and painted with a solid color brush that has the color value of 0x9ACD32.

![CompositionColorBrush](images/composition-compositioncolorbrush.png)
```cs

Compositor _compositor;
ContainerVisual _container;
SpriteVisual visual1, visual2;
CompositionColorBrush _blackBrush, _greenBrush; 

_compositor = new Compositor();
_container = _compositor.CreateContainerVisual();

_blackBrush = _compositor.CreateColorBrush(Colors.Black);
visual1 = _compositor.CreateSpriteVisual();
visual1.Brush = _blackBrush;
visual1.Size = new Vector2(156, 156);
visual1.Offset = new Vector3(0, 0, 0);

_ greenBrush = _compositor.CreateColorBrush(Color.FromArgb(0xff, 0x9A, 0xCD, 0x32));
Visual2 = _compositor.CreateSpriteVisual();
Visual2.Brush = _greenBrush;
Visual2.Size = new Vector2(150, 150);
Visual2.Offset = new Vector3(3, 3, 0);
```

Unlike other brushes, creating a [**CompositionColorBrush**](https://msdn.microsoft.com/library/windows/apps/Mt589399) is a relatively inexpensive operation. You may create **CompositionColorBrush** objects each time you render with little to no performance impact.

## Using Surface Brush

A [**CompositionSurfaceBrush**](https://msdn.microsoft.com/library/windows/apps/Mt589415) paints a visual with a composition surface (represented by a [**ICompositionSurface**](https://msdn.microsoft.com/library/windows/apps/Dn706819) object). The following illustration shows a square visual painted with a bitmap of licorice rendered onto a **ICompositionSurface** using D2D.

![CompositionSurfaceBrush](images/composition-compositionsurfacebrush.png)
The example initializes a composition surface for use with the brush. The composition surface is created using a [**LoadedImageSurface**](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.media.loadedimagesurface) object, which loads an image onto a composition surface. The **LoadedImageSurface** downloads, decodes and loads the image onto an underlying [**ICompositionSurface**](https://msdn.microsoft.com/library/windows/apps/Dn706819) using a Uniform Resource Identifier (URI) that points to a local file. The **LoadedImageSurface** can then be set as the content for the **CompositionSurfaceBrush**. Note, **ICompositionSurface** is exposed in Native code only, hence **LoadedImageSurface** is implemented in native code.

To create the surface brush, call the Compositor.[**CreateSurfaceBrush**](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.compositor.createsurfacebrush.aspx) method. The method returns a [**CompositionSurfaceBrush**](https://msdn.microsoft.com/library/windows/apps/Mt589415) object. The code below illustrates the code that can be used to paint a visual with contents of a **CompositionSurfaceBrush**.

```cs
Compositor _compositor;
ContainerVisual _container;
SpriteVisual visual;
CompositionSurfaceBrush _surfaceBrush;

_surfaceBrush = _compositor.CreateSurfaceBrush();
LoadedImageSurface _loadedSurface = LoadedImageSurface.StartLoadFromUri(new Uri("ms-appx:///Assets/liqorice.png"));
_surfaceBrush.Surface = _loadedSurface;
visual.Brush = _surfaceBrush;
```

> **Note:** [**LoadedImageSurface**](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.media.loadedimagesurface) is not available prior to Windows 10, version 1703.

## Configuring Stretch and Alignment

Sometimes, the contents of the [**ICompositionSurface**](https://msdn.microsoft.com/library/windows/apps/Dn706819) for a [**CompositionSurfaceBrush**](https://msdn.microsoft.com/library/windows/apps/Mt589415) doesn’t completely fill the areas of the visual that is being painted. When this happens, the Composition API uses the brush’s [**HorizontalAlignmentRatio**](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.compositionsurfacebrush.horizontalalignmentratio.aspx), [**VerticalAlignmentRatio**](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.compositionsurfacebrush.verticalalignmentratio) and [**Stretch**](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.compositionsurfacebrush.stretch) mode settings to determine how to fill the remaining area.

-   [**HorizontalAlignmentRatio**](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.compositionsurfacebrush.horizontalalignmentratio.aspx) and [**VerticalAlignmentRatio**](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.compositionsurfacebrush.verticalalignmentratio) are of type float and can be used to control the positioning of the brush inside the visual bounds.
    -   Value 0.0 aligns the left/top corner of the brush with the left/top corner of the visual
    -   Value of 0.5 aligns the center of the brush with the center of the visual
    -   Value of 1.0 aligns the right/bottom corner of the brush with the right/bottom corner of the visual
-   The [**Stretch**](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.compositionsurfacebrush.stretch) property accepts these values, which the [**CompositionStretch**](https://msdn.microsoft.com/library/windows/apps/Dn706786) enumeration defines:
    -   None: The brush doesn't stretch to fill the visual bounds. Be careful with this Stretch setting: if the brush is larger than the visual bounds, the contents of the brush will be clipped. The portion of brush used to paint the visual bounds can be controlled by using the [**HorizontalAlignmentRatio**](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.compositionsurfacebrush.horizontalalignmentratio.aspx) and [**VerticalAlignmentRatio**](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.compositionsurfacebrush.verticalalignmentratio) properties.
    -   Uniform: The brush is scaled to fit the visual bounds; the aspect ratio of the brush is preserved. This is the default value.
    -   UniformToFill: The brush is scaled so that it completely fills the visual bounds; the aspect ratio of the brush is preserved.
    -   Fill: The brush is scaled to fit the visual bounds. Because the brush’s height and width are scaled independently, the original aspect ratio of the brush might not be preserved. That is, the brush might be distorted to completely fill the visual bounds.

 

## Related Topics
[Composition native DirectX and Direct2D interoperation with BeginDraw and EndDraw](composition-native-interop.md)




