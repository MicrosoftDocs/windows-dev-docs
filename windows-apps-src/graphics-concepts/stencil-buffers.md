---
title: Stencil buffers
description: A stencil buffer is used to mask pixels in an image, to produce special effects.
ms.assetid: 544B3B9E-31E3-41DA-8081-CC3477447E94
keywords:
- Stencil buffers
ms.date: 02/08/2017
ms.topic: article
ms.localizationpriority: medium
---

# Stencil buffers

A *stencil buffer* is used to mask pixels in an image, to produce special effects. The mask controls whether the pixel is drawn or not. These special effects include compositing; decaling; dissolves, fades, and swipes; outlines and silhouettes; and two-sided stencil. Some of the more common effects are shown below.

The stencil buffer enables or disables drawing to the rendering target surface on a pixel-by-pixel basis. At its most fundamental level, it enables applications to mask sections of the rendered image so that they are not displayed. Applications often use stencil buffers for special effects such as dissolves, decaling, and outlining.

Stencil buffer information is embedded in the z-buffer data.

## <span id="How_the_Stencil_Buffer_Works"></span><span id="how_the_stencil_buffer_works"></span><span id="HOW_THE_STENCIL_BUFFER_WORKS"></span>How the stencil buffer works

Direct3D performs a test on the contents of the stencil buffer on a pixel-by-pixel basis. For each pixel in the target surface, it performs a test using the corresponding value in the stencil buffer, a stencil reference value, and a stencil mask value. If the test passes, Direct3D performs an action. The test is performed using the following steps.

1.  Perform a bitwise AND operation of the stencil reference value with the stencil mask.
2.  Perform a bitwise AND operation of the stencil buffer value for the current pixel with the stencil mask.
3.  Compare the result of step 1 to the result of step 2, using the comparison function.

The above steps are shown in the following line of code:

```cpp
(StencilRef & StencilMask) CompFunc (StencilBufferValue & StencilMask)
```

-   StencilRef represents the stencil reference value.
-   StencilMask represents the value of the stencil mask.
-   CompFunc is the comparison function.
-   StencilBufferValue is the contents of the stencil buffer for the current pixel.
-   The ampersand (&) symbol represents the bitwise AND operation.

The current pixel is written to the target surface if the stencil test passes, and is ignored otherwise. The default comparison behavior is to write the pixel, no matter how each bitwise operation turns out. You can change this behavior by changing the value of an enumerated type to identify the desired comparison function.

Your application can customize the operation of the stencil buffer. It can set the comparison function, the stencil mask, and the stencil reference value. It can also control the action that Direct3D takes when the stencil test passes or fails.

## <span id="Compositing"></span><span id="compositing"></span><span id="COMPOSITING"></span>Compositing


Your application can use the stencil buffer to composite 2D or 3D images onto a 3D scene. A mask in the stencil buffer is used to occlude an area of the rendering target surface. Stored 2D information, such as text or bitmaps, can then be written to the occluded area. Alternately, your application can render additional 3D primitives to the stencil-masked region of the rendering target surface. It can even render an entire scene.

Games often composite multiple 3D scenes together. For instance, driving games typically display a rear-view mirror. The mirror contains the view of the 3D scene behind the driver. It is essentially a second 3D scene composited with the driver's forward view.

## <span id="Decaling"></span><span id="decaling"></span><span id="DECALING"></span>Decaling


Direct3D applications use decaling to control which pixels from a particular primitive image are drawn to the rendering target surface. Applications apply decals to the images of primitives to enable coplanar polygons to render correctly.

For instance, when applying tire marks and yellow lines to a roadway, the markings should appear directly on top of the road. However, the z values of the markings and the road are the same. Therefore, the depth buffer might not produce a clean separation between the two. Some pixels in the back primitive may be rendered on top of the front primitive and vice versa. The resulting image appears to shimmer from frame to frame. This effect is called *z-fighting* or *flimmering*.

To solve this problem, use a stencil to mask the section of the back primitive where the decal will appear. Turn off z-buffering and render the image of the front primitive into the masked-off area of the render-target surface.

Although multiple texture blending can be used to solve this problem, doing so limits the number of other special effects that your application can produce. Using the stencil buffer to apply decals frees up texture blending stages for other effects.

## <span id="Dissolves__fades__and_swipes"></span><span id="dissolves__fades__and_swipes"></span><span id="DISSOLVES__FADES__AND_SWIPES"></span>Dissolves, fades, and swipes


Increasingly, applications employ special effects that are commonly used in movies and video, such as dissolves, swipes, and fades.

In a dissolve, one image is gradually replaced by another in a smooth sequence of frames. Although Direct3D provides methods of using multiple texture blending to achieve the same effect, applications that use the stencil buffer for dissolves can use texture-blending capabilities for other effects while they do a dissolve.

When your application performs a dissolve, it must render two different images. It uses the stencil buffer to control which pixels from each image are drawn to the rendering target surface. You can define a series of stencil masks and copy them into the stencil buffer on successive frames. Alternately, you can define a base stencil mask for the first frame and alter it incrementally.

At the beginning of the dissolve, your application sets the stencil function and stencil mask so that most of the pixels from the starting image pass the stencil test. Most of the pixels from the ending image should fail the stencil test. On successive frames, the stencil mask is updated so that fewer and fewer of the pixels in the starting image pass the test. As the frames progress, fewer and fewer of the pixels in the ending image fail the test. In this manner, your application can perform a dissolve using any arbitrary dissolve pattern.

Fading in or fading out is a special case of dissolving. When fading in, the stencil buffer is used to dissolve from a black or white image to a rendering of a 3D scene. Fading out is the opposite, your application starts with a rendering of a 3D scene and dissolves to black or white. The fade can be done using any arbitrary pattern you want to employ.

Direct3D applications use a similar technique for swipes. For example, when an application performs a left-to-right swipe, the ending image appears to slide gradually on top of the starting image from left to right. As in a dissolve, you must define a series of stencil masks that are loaded into the stencil buffer on successive frames, or successively modify the starting stencil mask. The stencil masks are used to disable the writing of pixels from the starting image and to enable the writing of pixels from the ending image.

A swipe is somewhat more complex than a dissolve in that your application must read pixels from the ending image in the reverse order of the swipe. That is, if the swipe is moving from left to right, your application must read pixels from the ending image from right to left.

## <span id="Outlines_and_silhouettes"></span><span id="outlines_and_silhouettes"></span><span id="OUTLINES_AND_SILHOUETTES"></span>Outlines and silhouettes


You can use the stencil buffer for more abstract effects, such as outlining and silhouetting.

If your application applies a stencil mask to the image of a primitive that is the same shape but slightly smaller, the resulting image contains only the primitive's outline. The application can then fill the stencil-masked area of the image with a solid color, giving the primitive an embossed look.

If the stencil mask is the same size and shape as the primitive you are rendering, the resulting image contains a hole where the primitive should be. Your application can then fill the hole with black to produce a silhouette of the primitive.

## <span id="Two-sided_stencil"></span><span id="two-sided_stencil"></span><span id="TWO-SIDED_STENCIL"></span>Two-sided stencil


Shadow Volumes are used for drawing shadows with the stencil buffer. The application computes the shadow volumes cast by occluding geometry, by computing the silhouette edges and extruding them away from the light into a set of 3D volumes. These volumes are then rendered twice into the stencil buffer.

The first render draws forward-facing polygons, and increments the stencil-buffer values. The second render draws the back-facing polygons of the shadow volume, and decrements the stencil buffer values. Normally, all incremented and decremented values cancel each other out. However, the scene was already rendered with normal geometry causing some pixels to fail the z-buffer test as the shadow volume is rendered. Values left in the stencil buffer correspond to pixels that are in the shadow. These remaining stencil-buffer contents are used as a mask, to alpha-blend a large, all-encompassing black quad into the scene. With the stencil buffer acting as a mask, the result is to darken pixels that are in the shadows.

This means that the shadow geometry is drawn twice per light source, hence putting pressure on the vertex throughput of the GPU. The two-sided stencil feature has been designed to mitigate this situation. In this approach, there are two sets of stencil state (named below), one set each for the front-facing triangles and the other for the back-facing triangles. This way, only a single pass is drawn per shadow volume, per light.

## <span id="related-topics"></span>Related topics


[Depth and stencil buffers](depth-and-stencil-buffers.md)

 

 




