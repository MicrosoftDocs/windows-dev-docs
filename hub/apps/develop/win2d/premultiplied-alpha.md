---
title: Premultiplied alpha
description: An explanation of the different ways alpha values can be represented in pixel colors.
ms.date: 05/28/2023
ms.topic: article
keywords: windows 10, windows 11, uwp, xaml, windows app sdk, winui, windows ui, graphics, games, effect win2d d2d d2d1 direct2d interop cpp csharp
ms.localizationpriority: medium
---

# Premultiplied alpha

In computer graphics there are two different ways to represent the opacity of a color value. Win2D uses both methods. This article explains the difference, and which is used where.

## Straight alpha

When using straight, also known as linear, alpha:
- RGB values specify the color of the thing being drawn
- The alpha value specifies how solid it is

In this world, RGB and alpha are independent. You can change one without affecting the other. To make an object fade out, you would gradually reduce its alpha value while leaving RGB unchanged.

To perform a source-over blend between two colors that use straight alpha format:

```
result = (source.RGB * source.A) + (dest.RGB * (1 - source.A))
```

## Premultiplied alpha

When using premultiplied alpha:
- RGB specifies how much color the thing being drawn contributes to the output
- The alpha value specifies how much it obscures whatever is behind it

In this world, RGB and alpha are linked. To make an object transparent you must reduce both its RGB (to contribute less color) and also its alpha (to obscure less of whatever is behind it). Fully transparent objects no longer have any color at all, so there is only one value that represents 100% transparency: RGB and alpha all zero.

To perform a source-over blend between two colors that use premultiplied alpha format:

```
result = source.RGB + (dest.RGB * (1 - source.A))
```

Premultiplied alpha is used in graphics rendering because it gives better results than straight alpha when filtering images or composing different layers. For more information see these blog posts:
- [Premultiplied alpha](https://shawnhargreaves.com/blog/premultiplied-alpha.html)
- [Premultiplied alpha and image composition](https://shawnhargreaves.com/blog/premultiplied-alpha-and-image-composition.html)

## Alpha in Win2D

Win2D uses straight alpha in its API surface, but premultiplied alpha for internal rendering operations.

`Windows.UI.Color` values use straight alpha. Whenever you pass a color to a `Draw*` or `Fill*` method, set the color of a brush, or clear to a color value, this color is specified using straight alpha.

The pixel values stored in a bitmap or rendertarget, and the drawing or blending operations that operate on these surfaces, use premultiplied alpha. When bitmaps are loaded from a file their contents are automatically converted into premultiplied format. When you call a Win2D drawing method, its color parameter is converted from straight to premultiplied before the actual drawing takes place.

Win2D image effects use a mixture of straight and premultiplied alpha. Some effects operate on one format, some on the other, and some provide a property to choose. The documentation for each effect type describes which alpha mode it uses. Effect input data is always assumed to be premultiplied, so when an effect needs to work with straight alpha it will first apply an unpremultiply transform, compute the effect, and then re-premultiply the output.

The bitmap APIs `GetPixelBytes`, `SetPixelBytes`, `GetPixelColors`, and `SetPixelColors`, do **not** perform any alpha format conversions. They just directly transfer bit values to or from the underlying GPU texture. This allows you to observe what alpha format Win2D is using internally:
- Create a drawing session on a rendertarget
- Call `drawingSession.Clear(Colors.Tranparent)`
- `Colors.Tranparent` is defined as R = 255, G = 255, B = 255, A = 0
- Win2D will convert this value to premultiplied format, yielding R = 0, G = 0, B = 0, A = 0
- Use `GetPixelColors` to read back the contents of the rendertarget
- Observe that it contains premultiplied format RGB = 0, not RGB = 255 like the original straight alpha `Colors.Tranparent` value

## Converting between alpha formats

To convert a straight alpha color value to premultiplied format, multiply its R, G, and B values by A. To convert premultiplied to straight, divide R, G, and B by A.

Note that color information is often represented as byte values ranging from 0 to 255 (for example the `Windows.UI.Color` structure consists of 4 bytes). This representation is scaled up by a factor of 255, so a byte value of 255 actually means 1, while 128 is half intensity. That scaling factor must be taken into account during format conversions, so to convert a `Windows.UI.Color` from straight to premultiplied:

```csharp
premultiplied.R = (byte)(straight.R * straight.A / 255);
premultiplied.G = (byte)(straight.G * straight.A / 255);
premultiplied.B = (byte)(straight.B * straight.A / 255);
premultiplied.A = straight.A;
```

If you have image data that is using the wrong alpha format, [`PremultiplyEffect`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Effects_PremultiplyEffect.htm) or [`UnPremultiplyEffect`](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Effects_UnPremultiplyEffect.htm) can be used to convert it.
