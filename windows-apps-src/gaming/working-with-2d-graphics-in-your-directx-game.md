---
title: 2D graphics for DirectX games
description: We discuss the use of 2D bitmap graphics and effects, and how to use them in your game.
ms.assetid: ad69e680-d709-83d7-4a4c-7bbfe0766bc7
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, directx, 2d, graphics
ms.localizationpriority: medium
---
# 2D graphics for DirectX games



We discuss the use of 2D bitmap graphics and effects, and how to use them in your game.

2D graphics are a subset of 3D graphics that deal with 2D primitives or bitmaps. More generally, they don't use a z-coordinate in the way a 3D game might, since the game play is usually confined to the x-y plane. They sometimes use 3D graphics techniques to create their visual components, and they are generally simpler to develop. If you are new to gaming, a 2D game is a great place to start, and 2D graphics development can be a good place for you to get a handle on DirectX.

You can develop 2D gaming graphics in DirectX using either Direct2D or Direct3D, or some combination. Many of the more useful classes for 2D game development are in Direct3D, such as the [**Sprite**](/windows/desktop/direct3d10/id3dx10sprite) class. Direct2D is a set of APIs that primarily target user interfaces and apps that require support for drawing primitives (such as circles, lines, and flat polygon shapes). With that in mind, it still provides a powerful and performant set of classes and methods for creating game graphics as well, especially when creating game overlays, interfaces, and heads-up displays (HUDs) -- or for creating a variety of 2D games, from simple to reasonably detailed. The most effective approach when creating 2D games, though, is to use elements from both libraries, and that's the way we will approach 2D graphics development in this topic.

## Concepts at a glance


Before the advent of modern 3D graphics and the hardware that supports it, games were primarily 2D, and many of their graphics techniques involved moving blocks of memory around -- usually arrays of color data that would be translated or transformed to pixels on the screen in a 1:1 fashion.

In DirectX, 2D graphics are part of the 3D pipeline. There is a much greater variety of screen resolutions and graphics hardware available, and your 2D graphics engine must be able to support them without a significant change in fidelity.

Here are a few of the basic concepts you should be familiar with when starting 2D graphics development.

-   Pixels and raster coordinates. A pixel is a single point on a raster display, and has its own (x, y) coordinate pair indicating its location on the display. (The term "pixel" is often used interchangeably between the physical pixels that comprise the display and the addressable memory elements used to hold the color and alpha values of the pixels before they are sent to the display.) The raster is treated by APIs as a rectangular grid of pixel elements, which often has a 1:1 correspondence with the physical pixel grid of a display. Raster coordinate systems start from the upper left, with the pixel at (0, 0) in the upper leftmost corner of the grid.
-   Bitmap graphics (sometimes called raster graphics) are graphic elements represented as a rectangular grid of pixel values. Sprites -- computed pixel arrays managed independent of the raster -- are one type of bitmap graphic, commonly used for the active characters or background-independent animated objects in a game. The various frames of animation for a sprite are represented as collections of bitmaps called "sheets" or "batches." Backgrounds are larger bitmap objects that are the same resolution or greater than that of the screen raster, and often serve as the backdrop(s) for a game's playfield.
-   Vector graphics are graphics that use geometric primitives, such as points, lines, circles, and polygons to define 2D objects. They are represented not as arrays of pixels, but as the mathematical equations that define them in a 2D space. They do not necessarily have a 1:1 correspondence with the pixel grid of the display, and must be transformed from the coordinate system that you rendered them in into the raster coordinate system of the display.
-   Translation is when you take a point or vertex and calculate its new location in the same coordinate system.
-   Scaling is when you enlarge or shrink an object by a specified scale factor. With a vector image, you shrink and enlarge its component vertices; with a bitmap, you enlarge the pixel elements or diminish them. With bitmap images, you lose pixel data when the image shrinks, and you enlarge the individual pixels when the image is scaled closer. For the latter, you can use pixel color interpolation operations, like bilinear filtering, to smooth out the harsh color boundaries between the enlarged pixels.
-   Rotation is when you rotate an object about a specified axis or axes. With a vector image, the vertices of the geometry are multiplied against a rotation matrix to obtain the rotated vertex; with a bitmap image, different algorithms can be employed, each with a lesser or greater degree of fidelity in the results. As with scaling and translation, there are APIs specifically for rotation operations.
-   Transformation is when you take one point or vertex in one coordinate system and calculate its corresponding point or vertex in another coordinate system. This includes translation, scaling, and rotation, as well as other coordinate calculation operations.
-   Clipping is when you remove portions of bitmaps or geometry that are not within the viewable area of the display, or are hidden by objects with higher view priority.
-   The frame buffer is an area in memory -- often in the memory of the graphics hardware itself -- that contains the final raster map that you will draw to the screen. The swap chain is a collection of buffers, where you draw in a back buffer and, when the image is ready, you "swap" it to the front and display it.

## Design considerations


2D graphics development is a great way to get accustomed to developing with Direct3D, and will allow you to spend more time on other critical aspects of game development: audio, controls, and the game mechanics.

Always draw to a back buffer. Drawing directly to your frame buffer means that your image will be displayed when the signal for display is received (usually every 1/60th of second), even if your drawing operation hasn't completed!

Design your graphics engine to support a good selection of resolutions, from 1024x600 to 1920x1080 (or higher). Your audience will thank you if you support their LCD monitor's native resolution, especially with 2D graphics.

Great artwork will be your greatest asset, when it comes to visuals. While your bitmap graphics may lack the punch of 3D photorealistic visuals using the latest shader model features, great high-resolution artwork can often convey as much or more style and personality -- and with far less of a performance penalty.

## Reference


-   [Direct2D overview](/windows/desktop/Direct2D/direct2d-overview)
-   [Direct2D quickstart](/windows/desktop/Direct2D/getting-started-with-direct2d)
-   [Direct2D and Direct3D interoperability overview](/windows/desktop/Direct2D/direct2d-and-direct3d-interoperation-overview)