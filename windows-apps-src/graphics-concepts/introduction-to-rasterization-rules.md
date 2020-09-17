---
title: Introduction to rasterization rules
description: Often, the points specified for vertices do not precisely match the pixels on the screen. When this happens, Direct3D applies triangle rasterization rules to decide which pixels apply to a given triangle.
ms.assetid: 4232CDBA-F669-4417-9378-F9013E83462C
keywords:
- Introduction to rasterization rules
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Introduction to rasterization rules


Often, the points specified for vertices do not precisely match the pixels on the screen. When this happens, Direct3D applies triangle rasterization rules to decide which pixels apply to a given triangle.

This is a simplified introduction to rasterization rules. For more details, see [Rasterization rules](rasterization-rules.md). See also [Rasterizer (RS) stage](rasterizer-stage--rs-.md).

## <span id="Triangle_Rasterization_Rules"></span><span id="triangle_rasterization_rules"></span><span id="TRIANGLE_RASTERIZATION_RULES"></span>Triangle Rasterization Rules


Direct3D uses a top-left filling convention for filling geometry. This is the same convention that is used for rectangles in GDI and OpenGL. In Direct3D, the center of the pixel is the decisive point. If the center is inside a triangle, the pixel is part of the triangle. Pixel centers are at integer coordinates.

This description of triangle-rasterization rules used by Direct3D does not necessarily apply to all available hardware. Your testing may uncover minor variations in the implementation of these rules.

The following illustration shows a rectangle whose upper-left corner is at (0, 0) and whose lower-right corner is at (5, 5). This rectangle fills 25 pixels, just as you would expect. The width of the rectangle is defined as right minus left. The height is defined as bottom minus top.

![a numbered square divided into six rows and columns](images/pixmap.png)

In the top-left filling convention, *top* refers to the vertical location of horizontal spans, and *left* refers to the horizontal location of pixels within a span. An edge cannot be a top edge unless it is horizontal. In general, most triangles have only left and right edges. The following illustration shows a top edge and a right edge.

![a numbered square that contains two triangles](images/triedge.png)

The top-left filling convention determines the action taken by Direct3D when a triangle passes through the center of a pixel. The following illustration shows two triangles, one at (0, 0), (5, 0), and (5, 5), and the other at (0, 5), (0, 0), and (5, 5). The first triangle in this case gets 15 pixels (shown in black), whereas the second gets only 10 pixels (shown in gray) because the shared edge is the left edge of the first triangle.

![a numbered square that shows two triangles](images/twotris.png)

If you define a rectangle with its upper-left corner at (0.5, 0.5) and its lower-right corner at (2.5, 4.5), the center point of this rectangle is at (1.5, 2.5). When the Direct3D rasterizer tessellates this rectangle, the center of each pixel is unambiguously inside each of the four triangles, and the top-left filling convention is not needed. The following illustration shows this. The pixels in the rectangle are labeled according to the triangle in which Direct3D includes them.

![a numbered square that contains a rectangle that is divided into four triangles](images/noambig.png)

If you move the rectangle in the preceding illustration so that its upper-left corner is at (1.0, 1.0), its lower-right corner at (3.0, 5.0), and its center point at (2.0, 3.0), Direct3D applies the top-left filling convention. Most pixels in this rectangle straddle the border between two or more triangles, as the following illustration shows.

![a numbered square that contains a rectangle that is divided into four triangles](images/fillrule.png)

For both rectangles, the same pixels are affected, as shown in the following illustration.

![pixels that are affected by the preceding two numbered squares](images/samepix.png)

## <span id="Point_and_Line_Rules"></span><span id="point_and_line_rules"></span><span id="POINT_AND_LINE_RULES"></span>Point and Line Rules


Points are rendered the same as point sprites, which are both rendered as screen-aligned quadrilaterals and thus adhere to the same rules as polygon rendering.

Non-antialiased line rendering rules are exactly the same as those for [GDI lines](/windows/desktop/gdi/lines).

## <span id="Point_Sprite_Rules"></span><span id="point_sprite_rules"></span><span id="POINT_SPRITE_RULES"></span>Point Sprite Rules


Point sprites and patch primitives are rasterized as if the primitives were first tessellated into triangles and the resulting triangles rasterized.

## <span id="related-topics"></span>Related topics


[Devices](devices.md)

[Rasterizer (RS) stage](rasterizer-stage--rs-.md)

[Rasterization rules](rasterization-rules.md)

 

 