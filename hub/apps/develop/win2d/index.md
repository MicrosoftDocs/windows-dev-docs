---
title: Overview of Win2D
description: Win2D is an easy-to-use Windows Runtime API for immediate-mode 2D graphics rendering with GPU acceleration.
ms.date: 07/02/2026
ms.topic: concept-article
author: GrantMeStrength
ms.author: jken
keywords: windows 10, windows 11, uwp, xaml, windows app sdk, winui, windows ui, graphics, games
ms.localizationpriority: medium
---

# Overview of Win2D

Win2D is an easy-to-use Windows Runtime (WinRT) API for immediate-mode 2D graphics rendering with GPU acceleration. It's ideal for creating simple games, displays such as charts, and other simple 2D graphics.

You can use Win2D in your WinUI (Windows App SDK) apps, using either C# or C++. Win2D utilizes the power of Direct2D, and it integrates seamlessly with XAML in WinUI (Windows App SDK).

Win2D is available as a standalone NuGet package, or as source code (for the source code, see the [Win2D](https://github.com/microsoft/Win2D) repo on GitHub).

## Features

Win2D includes an extensive set of features to support lots of different scenarios. Here's a list of most of them, with useful links to related docs and [Win2D API references](https://microsoft.github.io/Win2D/WinUI3/html/APIReference.htm) to learn more.

### Bitmap graphics

- Load, save and draw [bitmap images](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasBitmap.htm)
- [Render to texture](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasRenderTarget.htm)
- Use bitmaps as [opacity masks](https://microsoft.github.io/Win2D/WinUI3/html/M_Microsoft_Graphics_Canvas_CanvasDrawingSession_CreateLayer.htm)
- [Sprite batch](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasSpriteBatch.htm) API for efficiently drawing large numbers of bitmaps
- Use [block compressed bitmap](./bitmap-block-compression.md) formats to save memory
- Load, [save](https://microsoft.github.io/Win2D/WinUI3/html/M_Microsoft_Graphics_Canvas_CanvasImage_SaveAsync.htm), and draw [virtual bitmaps](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasVirtualBitmap.htm), which can be larger than the maximum GPU texture size and are automatically split into tiles

### Vector graphics

- [Draw](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasDrawingSession.htm) primitive shapes (lines, rectangles, circles, etc.) or arbitrarily complex geometry
- Fill shapes using solid colors, [image brushes](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Brushes_CanvasImageBrush.htm), or [linear](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Brushes_CanvasLinearGradientBrush.htm) and [radial](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Brushes_CanvasRadialGradientBrush.htm) gradients
- Draw lines of any width with flexible [stroke styles](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Geometry_CanvasStrokeStyle.htm) (dotted, dashed, etc.)
- High quality antialiasing
- Rich [geometry manipulation](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Geometry_CanvasGeometry.htm) (union, intersect, compute point on path, tessellate, etc.)
- [Clip drawing](https://microsoft.github.io/Win2D/WinUI3/html/M_Microsoft_Graphics_Canvas_CanvasDrawingSession_CreateLayer_6.htm) to arbitrary geometric regions
- Capture drawing operations in [command lists](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_CanvasCommandList.htm) for later replay
- Rasterize [ink strokes](https://microsoft.github.io/Win2D/WinUI3/html/M_Microsoft_Graphics_Canvas_CanvasDrawingSession_DrawInk.htm) (from a stylus)
- Load, draw, and manipulate [SVG](https://microsoft.github.io/Win2D/WinUI3/html/M_Microsoft_Graphics_Canvas_Svg_CanvasSvgDocument_LoadFromXml.htm) vector graphics

### Powerful image processing effects

- [Blurs](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Effects_GaussianBlurEffect.htm)
- [Blends](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Effects_BlendEffect.htm)
- Color adjustments ([brightness](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Effects_BrightnessEffect.htm), [contrast](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Effects_ContrastEffect.htm), [exposure](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Effects_ExposureEffect.htm), [highlights & shadows](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Effects_HighlightsAndShadowsEffect.htm), etc.)
- Filters ([convolve](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Effects_ConvolveMatrixEffect.htm), [edge detection](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Effects_EdgeDetectionEffect.htm), [emboss](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Effects_EmbossEffect.htm), [sharpen](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Effects_SharpenEffect.htm))
- [Lighting](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Effects_SpotSpecularEffect.htm)
- [Custom pixel shaders](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Effects_PixelShaderEffect.htm)
- [Fully custom effects](./custom-effects.md)
- [And many more](https://microsoft.github.io/Win2D/WinUI3/html/N_Microsoft_Graphics_Canvas_Effects.htm)...

### Text

- Fully internationalized [Unicode text rendering](https://microsoft.github.io/Win2D/WinUI3/html/N_Microsoft_Graphics_Canvas_Text.htm)
- [Text layouts](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Text_CanvasTextLayout.htm) can be drawn, measured, or hit-tested against
- Convert [text outlines to geometry](https://microsoft.github.io/Win2D/WinUI3/html/M_Microsoft_Graphics_Canvas_Geometry_CanvasGeometry_CreateText.htm)
- [Enumerate fonts](https://microsoft.github.io/Win2D/WinUI3/html/M_Microsoft_Graphics_Canvas_Text_CanvasFontSet_GetSystemFontSet.htm) and query their metrics
- Draw or manipulate individual [glyph runs](https://microsoft.github.io/Win2D/WinUI3/html/T_Microsoft_Graphics_Canvas_Text_ICanvasTextRenderer.htm) to create [custom text layouts](https://github.com/Microsoft/Win2D-Samples/blob/master/ExampleGallery/CustomTextLayouts.xaml.cs)


## Next steps

Next, to learn about creating a simple app, try out the [Build a simple Win2D app](./quick-start.md) tutorial. You can also consult the [features list](#features) below to discover all the things Win2D can do. To learn more about advanced topics, you can refer to the collection of articles included in the documentation here as well.