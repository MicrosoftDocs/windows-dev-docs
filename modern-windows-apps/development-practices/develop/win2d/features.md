---
title: Features
description: A high level overview of all the main features in Win2D.
ms.date: 05/28/2023
ms.topic: article
keywords: windows 10, windows 11, uwp, xaml, windows app sdk, winui, windows ui, graphics, games, effect win2d d2d d2d1 direct2d interop cpp csharp
ms.localizationpriority: medium
---

# Features

Win2D includes an extensive set of features to support lots of different scenarios. Here's a list of most of them, with useful links to related docs and API references to learn more.

## Bitmap graphics

- Load, save and draw [bitmap images](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_CanvasBitmap.htm)
- [Render to texture](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_CanvasRenderTarget.htm)
- Use bitmaps as [opacity masks](https://microsoft.github.io/Win2D/WinUI2/html/M_Microsoft_Graphics_Canvas_CanvasDrawingSession_CreateLayer.htm)
- [Sprite batch](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_CanvasSpriteBatch.htm) API for efficiently drawing large numbers of bitmaps
- Use [block compressed bitmap](./bitmap-block-compression.md) formats to save memory
- Load, [save](https://microsoft.github.io/Win2D/WinUI2/html/M_Microsoft_Graphics_Canvas_CanvasImage_SaveAsync.htm), and draw [virtual bitmaps](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_CanvasVirtualBitmap.htm), which can be larger than the maximum GPU texture size and are automatically split into tiles

## Vector graphics

- [Draw](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_CanvasDrawingSession.htm) primitive shapes (lines, rectangles, circles, etc.) or arbitrarily complex geometry
- Fill shapes using solid colors, [image brushes](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Brushes_CanvasImageBrush.htm), or [linear](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Brushes_CanvasLinearGradientBrush.htm) and [radial](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Brushes_CanvasRadialGradientBrush.htm) gradients
- Draw lines of any width with flexible [stroke styles](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Geometry_CanvasStrokeStyle.htm) (dotted, dashed, etc.)
- High quality antialiasing
- Rich [geometry manipulation](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Geometry_CanvasGeometry.htm) (union, intersect, compute point on path, tessellate, etc.)
- [Clip drawing](https://microsoft.github.io/Win2D/WinUI2/html/M_Microsoft_Graphics_Canvas_CanvasDrawingSession_CreateLayer_6.htm) to arbitrary geometric regions
- Capture drawing operations in [command lists](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_CanvasCommandList.htm) for later replay
- Rasterize [ink strokes](https://microsoft.github.io/Win2D/WinUI2/html/M_Microsoft_Graphics_Canvas_CanvasDrawingSession_DrawInk.htm) (from a stylus)
- Load, draw, and manipulate [SVG](https://microsoft.github.io/Win2D/WinUI2/html/M_Microsoft_Graphics_Canvas_Svg_CanvasSvgDocument_LoadFromXml.htm) vector graphics

## Powerful image processing effects

- [Blurs](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Effects_GaussianBlurEffect.htm)
- [Blends](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Effects_BlendEffect.htm)
- Color adjustments ([brightness](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Effects_BrightnessEffect.htm), [contrast](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Effects_ContrastEffect.htm), [exposure](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Effects_ExposureEffect.htm), [highlights & shadows](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Effects_HighlightsAndShadowsEffect.htm), etc.)
- Filters ([convolve](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Effects_ConvolveMatrixEffect.htm), [edge detection](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Effects_EdgeDetectionEffect.htm), [emboss](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Effects_EmbossEffect.htm), [sharpen](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Effects_SharpenEffect.htm))
- [Lighting](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Effects_SpotSpecularEffect.htm)
- [Custom pixel shaders](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Effects_PixelShaderEffect.htm)
- [Fully custom effects](./custom-effects.md)
- [And many more](https://microsoft.github.io/Win2D/WinUI2/html/N_Microsoft_Graphics_Canvas_Effects.htm)...

## Text

- Fully internationalized [Unicode text rendering](https://microsoft.github.io/Win2D/WinUI2/html/N_Microsoft_Graphics_Canvas_Text.htm)
- [Text layouts](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Text_CanvasTextLayout.htm) can be drawn, measured, or hit-tested against
- Convert [text outlines to geometry](https://microsoft.github.io/Win2D/WinUI2/html/M_Microsoft_Graphics_Canvas_Geometry_CanvasGeometry_CreateText.htm)
- [Enumerate fonts](https://microsoft.github.io/Win2D/WinUI2/html/M_Microsoft_Graphics_Canvas_Text_CanvasFontSet_GetSystemFontSet.htm) and query their metrics
- Draw or manipulate individual [glyph runs](https://microsoft.github.io/Win2D/WinUI2/html/T_Microsoft_Graphics_Canvas_Text_ICanvasTextRenderer.htm) to create [custom text layouts](https://github.com/Microsoft/Win2D-Samples/blob/master/ExampleGallery/CustomTextLayouts.xaml.cs)