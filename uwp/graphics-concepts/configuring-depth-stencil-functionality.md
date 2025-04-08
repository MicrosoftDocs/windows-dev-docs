---
title: Configuring depth-stencil functionality
description: This section covers the steps for setting up the depth-stencil buffer, and depth-stencil state for the output-merger stage.
ms.assetid: B3F6CDAA-93ED-4DC1-8E69-972C557C7920
keywords:
- Configuring depth-stencil functionality
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# <span id="direct3dconcepts.configuring_depth-stencil_functionality"></span>Configuring depth-stencil functionality


This section covers the steps for setting up the depth-stencil buffer, and depth-stencil state for the output-merger stage.

Once you know how to use the depth-stencil buffer and the corresponding depth-stencil state, refer to [advanced-stencil techniques](#advanced-stencil-techniques).

## <span id="Create_Depth_Stencil_State"></span><span id="create_depth_stencil_state"></span><span id="CREATE_DEPTH_STENCIL_STATE"></span>Create Depth-Stencil State


The depth-stencil state tells the output-merger stage how to perform the [depth-stencil test](/windows/desktop/direct3d11/d3d10-graphics-programming-guide-output-merger-stage). The depth-stencil test determines whether or not a given pixel should be drawn.

## <span id="Bind_Depth_Stencil_to_the_OM_Stage"></span><span id="bind_depth_stencil_to_the_om_stage"></span><span id="BIND_DEPTH_STENCIL_TO_THE_OM_STAGE"></span>Bind Depth-Stencil Data to the OM Stage


Bind the depth-stencil state.

Bind the depth-stencil resource using a view.

Render targets must all be the same type of resource. If multisample antialiasing is used, all bound render targets and depth buffers must have the same sample counts.

When a buffer is used as a render target, depth-stencil testing and multiple render targets are not supported.

-   As many as 8 render targets can be bound simultaneously.
-   All render targets must have the same size in all dimensions (width and height, and depth for 3D or array size for \*Array types).
-   Each render target may have a different data format.
-   Write masks control what data gets written to a render target. The output write masks control on a per-render target, per-component level what data gets written to the render target(s).

## <span id="Advanced_Stencil_Techniques"></span><span id="advanced_stencil_techniques"></span><span id="ADVANCED_STENCIL_TECHNIQUES"></span><span id="advanced-stencil-techniques"></span>Advanced Stencil Techniques


The stencil portion of the depth-stencil buffer can be used for creating rendering effects such as compositing, decaling, and outlining.

-   [Compositing](#compositing)
-   [Decaling](#decaling)
-   [Outlines and Silhouettes](#outlines-and-silhouettes)
-   [Two-Sided Stencil](#two-sided-stencil)
-   [Reading the Depth-Stencil Buffer as a Texture](#reading-the-depth-stencil-buffer-as-a-texture)

### <span id="Compositing"></span><span id="compositing"></span><span id="COMPOSITING"></span>Compositing

Your application can use the stencil buffer to composite 2D or 3D images onto a 3D scene. A mask in the stencil buffer is used to occlude an area of the rendering target surface. Stored 2D information, such as text or bitmaps, can then be written to the occluded area. Alternately, your application can render additional 3D primitives to the stencil-masked region of the rendering target surface. It can even render an entire scene.

Games often composite multiple 3D scenes together. For instance, driving games typically display a rear-view mirror. The mirror contains the view of the 3D scene behind the driver. It is essentially a second 3D scene composited with the driver's forward view.

### <span id="Decaling"></span><span id="decaling"></span><span id="DECALING"></span>Decaling

Direct3D applications use decaling to control which pixels from a particular primitive image are drawn to the rendering target surface. Applications apply decals to the images of primitives to enable coplanar polygons to render correctly.

For instance, when applying tire marks and yellow lines to a roadway, the markings should appear directly on top of the road. However, the z values of the markings and the road are the same. Therefore, the depth buffer might not produce a clean separation between the two. Some pixels in the back primitive may be rendered on top of the front primitive and vice versa. The resulting image appears to shimmer from frame to frame. This effect is called z-fighting or flimmering.

To solve this problem, use a stencil to mask the section of the back primitive where the decal will appear. Turn off z-buffering and render the image of the front primitive into the masked-off area of the render-target surface.

Multiple texture blending can be used to solve this problem.

### <span id="Outlines_and_Silhouettes"></span><span id="outlines_and_silhouettes"></span><span id="OUTLINES_AND_SILHOUETTES"></span><span id="outlines-and-silhouettes">Outlines and Silhouettes

You can use the stencil buffer for more abstract effects, such as outlining and silhouetting.

If your application does two render passes - one to generate the stencil mask and second to apply the stencil mask to the image, but with the primitives slightly smaller on the second pass - the resulting image will contain only the primitive's outline. The application can then fill the stencil-masked area of the image with a solid color, giving the primitive an embossed look.

If the stencil mask is the same size and shape as the primitive you are rendering, the resulting image contains a hole where the primitive should be. Your application can then fill the hole with black to produce a silhouette of the primitive.

### <span id="Two_Sided_Stencil"></span><span id="two_sided_stencil"></span><span id="TWO_SIDED_STENCIL"></span><span id="two-sided-stencil">Two-Sided Stencil

Shadow Volumes are used for drawing shadows with the stencil buffer. The application computes the shadow volumes cast by occluding geometry, by computing the silhouette edges and extruding them away from the light into a set of 3D volumes. These volumes are then rendered twice into the stencil buffer.

The first render draws forward-facing polygons, and increments the stencil-buffer values. The second render draws the back-facing polygons of the shadow volume, and decrements the stencil buffer values.

Normally, all incremented and decremented values cancel each other out. However, the scene was already rendered with normal geometry causing some pixels to fail the z-buffer test as the shadow volume is rendered. Values left in the stencil buffer correspond to pixels that are in the shadow. These remaining stencil-buffer contents are used as a mask, to alpha-blend a large, all-encompassing black quad into the scene. With the stencil buffer acting as a mask, the result is to darken pixels that are in the shadows.

This means that the shadow geometry is drawn twice per light source, hence putting pressure on the vertex throughput of the GPU. The two-sided stencil feature has been designed to mitigate this situation. In this approach, there are two sets of stencil state (named below), one set each for the front-facing triangles and the other for the back-facing triangles. This way, only a single pass is drawn per shadow volume, per light.

### <span id="Reading_the_Depth-Stencil_Buffer_as_a_Texture"></span><span id="reading_the_depth-stencil_buffer_as_a_texture"></span><span id="READING_THE_DEPTH-STENCIL_BUFFER_AS_A_TEXTURE"></span><span id="reading-the-depth-stencil-buffer-as-a-texture"></span>Reading the Depth-Stencil Buffer as a Texture

An inactive depth-stencil buffer can be read by a shader as a texture. An application that reads a depth-stencil buffer as a texture renders in two passes, the first pass writes to the depth-stencil buffer and the second pass reads from the buffer. This allows a shader to compare depth or stencil values previously written to the buffer against the value for the pixel currently being rendered. The result of the comparison can be used to create effects such as shadow mapping or soft particles in a particle system.

## <span id="related-topics"></span>Related topics


[Output Merger (OM) stage](output-merger-stage--om-.md)

[Graphics pipeline](graphics-pipeline.md)

[Output-Merger Stage](/windows/desktop/direct3d11/d3d10-graphics-programming-guide-output-merger-stage)