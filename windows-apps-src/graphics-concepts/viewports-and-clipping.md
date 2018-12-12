---
title: Viewports and clipping
description: A viewport is a two-dimensional (2D) rectangle into which a 3D scene is projected.
ms.assetid: D0DD646E-13AE-452A-AD22-8C35000D0BA9
keywords:
- Viewports and clipping
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Viewports and clipping


A *viewport* is a two-dimensional (2D) rectangle into which a 3D scene is projected. In Direct3D, the rectangle exists as coordinates within a Direct3D surface that the system uses as a rendering target. The projection transformation converts vertices into the coordinate system used for the viewport. A viewport is also used to specify the range of depth values on a render-target surface into which a scene will be rendered (usually 0.0 to 1.0).

## <span id="The_Viewing_Frustum"></span><span id="the_viewing_frustum"></span><span id="THE_VIEWING_FRUSTUM"></span>The Viewing Frustum


A viewing frustum is 3D volume in a scene positioned relative to the viewport's camera. The shape of the volume affects how models are projected from camera space onto the screen. The most common type of projection, a perspective projection, is responsible for making objects near the camera appear bigger than objects in the distance. For perspective viewing, the viewing frustum can be visualized as a pyramid, with the camera positioned at the tip as shown in the following illustration. This pyramid is intersected by a front and back clipping plane. The volume within the pyramid between the front and back clipping planes is the viewing frustum. Objects are visible only when they are in this volume.

![illustration of a viewing frustrum with a front and back clipping plan](images/frustum.png)

If you imagine that you are standing in a dark room and looking through a square window, you are visualizing a viewing frustum. In this analogy, the near clipping plane is the window, and the back clipping plane is whatever finally interrupts your view - the skyscraper across the street, the mountains in the distance, or nothing at all. You can see everything inside the truncated pyramid that starts at the window and ends with whatever interrupts your view, and you can see nothing else.

The viewing frustum is defined by fov (field of view) and by the distances of the front and back clipping planes, specified in z-coordinates, as shown in the following diagram.

![diagram of the viewing frustum](images/fovdiag.png)

In this diagram, the variable D is the distance from the camera to the origin of the space that was defined in the last part of the geometry pipeline - the viewing transformation. This is the space around which you arrange the limits of your viewing frustum. For information about how this D variable is used to build the projection matrix, see the [Projection transform](projection-transform.md)

## <span id="Viewport_Rectangle"></span><span id="viewport_rectangle"></span><span id="VIEWPORT_RECTANGLE"></span>Viewport Rectangle


A viewport struct contains four members (X, Y, Width, Height) that define the area of the render-target surface into which a scene will be rendered. These values correspond to the destination rectangle, or viewport rectangle, as shown in the following diagram.

![diagram of the viewport rectangle](images/destrect.png)

The values you specify for the X, Y, Width, Height members are screen coordinates relative to the upper-left corner of the render-target surface. The structure defines two additional members (MinZ and MaxZ) that indicate the depth-ranges into which the scene will be rendered.

Direct3D assumes that the viewport clipping volume ranges from -1.0 to 1.0 in X, and from 1.0 to -1.0 in Y. These were the settings used most often by applications in the past. You can adjust the viewport aspect ratio before clipping using the [projection transform](projection-transform.md).

**Note**   MinZ and MaxZ indicate the depth-ranges into which the scene will be rendered and are not used for clipping. Most applications will set these values to 0.0 and 1.0, to enable the system to render to the entire range of depth values in the depth buffer. In some cases, you can achieve special effects by using other depth ranges. For instance, to render a heads-up display in a game, you can set both values to 0.0 to force the system to render objects in a scene in the foreground, or you might set them both to 1.0 to render an object that should always be in the background.

 

The dimensions used in the X, Y, Width, Height members of a viewport struct define the location and dimensions of the viewport on the render-target surface. These values are in screen coordinates, relative to the upper-left corner of the surface.

Direct3D uses the viewport location and dimensions to scale the vertices to fit a rendered scene into the appropriate location on the target surface. Internally, Direct3D inserts these values into the following matrix that is applied to each vertex.

![equation of the matrix that is applied to each vertex](images/vpscale.png)

This matrix scales vertices according to the viewport dimensions and desired depth range and translates them to the appropriate location on the render-target surface. The matrix also flips the y-coordinate to reflect a screen origin at the top-left corner with y increasing downward. After this matrix is applied, vertices are still homogeneous - that is, they still exist as \[x,y,z,w\] vertices - and they must be converted to non-homogeneous coordinates before being sent to the rasterizer.

**Note**   Applications typically set MinZ and MaxZ to 0.0 and 1.0 respectively to cause the system to render to the entire depth range. However, you can use other values to achieve certain affects. For example, you might set both values to 0.0 to force all objects into the foreground, or set both to 1.0 to render all objects into the background.

 

## <span id="Clearing_a_Viewport"></span><span id="clearing_a_viewport"></span><span id="CLEARING_A_VIEWPORT"></span>Clearing a Viewport


Clearing the viewport resets the contents of the viewport rectangle on the render-target surface. It can also clear the rectangle in the depth and stencil buffer surfaces.

## <span id="Set_Up_the_Viewport_for_Clipping"></span><span id="set_up_the_viewport_for_clipping"></span><span id="SET_UP_THE_VIEWPORT_FOR_CLIPPING"></span>Set Up the Viewport for Clipping


The results of the projection matrix determine the clipping volume in projection space as:

-w<sub>c</sub>&lt;= x<sub>c</sub>&lt;= w<sub>c</sub>

-w<sub>c</sub>&lt;= y<sub>c</sub>&lt;= w<sub>c</sub>

0 &lt;= z<sub>c</sub>&lt;= w<sub>c</sub>

Where: x, y, z, and w represent the vertex coordinates after the projection transformation is applied. Any vertices that have an x-, y-, or z-component outside these ranges are clipped, if clipping is enabled (the default behavior).

## <span id="related-topics"></span>Related topics


[Coordinate systems and geometry](coordinate-systems-and-geometry.md)

 

 




