---
title: Rasterizer (RS) stage
description: The rasterizer clips primitives that aren't in view, prepares primitives for the Pixel Shader (PS) stage, and determines how to invoke pixel shaders.
ms.assetid: 7E80724B-5696-4A99-91AF-49744B5CD3A9
keywords:
- Rasterizer (RS) stage
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Rasterizer (RS) stage


The rasterizer clips primitives that aren't in view, prepares primitives for the [Pixel Shader (PS) stage](pixel-shader-stage--ps-.md), and determines how to invoke pixel shaders. The rasterization stage converts vector information (composed of shapes or primitives) into a raster image (composed of pixels) for the purpose of displaying real-time 3D graphics.

## <span id="Purpose_and_uses"></span><span id="purpose_and_uses"></span><span id="PURPOSE_AND_USES"></span>Purpose and uses


During rasterization, each primitive is converted into pixels, while interpolating per-vertex values across each primitive. Rasterization includes clipping vertices to the view frustum, performing a divide by z to provide perspective, mapping primitives to a 2D viewport, and determining how to invoke the pixel shader. While using a pixel shader is optional, the rasterizer stage always performs clipping, a perspective divide to transform the points into homogeneous space, and maps the vertices to the viewport.

You may disable rasterization by telling the pipeline there is no pixel shader (set the [Pixel Shader (PS) stage](pixel-shader-stage--ps-.md) to **NULL** and disable depth and stencil testing). While disabled, rasterization-related pipeline counters will not update.

On hardware that implements hierarchical Z-buffer optimizations, you may enable preloading the z-buffer by setting the Pixel Shader (PS) stage to **NULL** while enabling depth and stencil testing.

See [Rasterization rules](rasterization-rules.md).

## <span id="Input"></span><span id="input"></span><span id="INPUT"></span>Input


Vertices (x,y,z,w), coming into the Rasterizer stage are assumed to be in homogeneous clip-space. In this coordinate space the X axis points right, Y points up and Z points away from camera.

The fixed-function Rasterizer (RS) stage is fed by the Stream Output (SO) stage and/or by the previous pipeline stage, such as the [Geometry Shader (GS) stage](geometry-shader-stage--gs-.md). If GS isn't used, RS is fed by the [Domain Shader (DS) stage](domain-shader-stage--ds-.md). If DS also isn't used, RS is fed by the [Vertex Shader (VS) stage](vertex-shader-stage--vs-.md).

## <span id="Output"></span><span id="output"></span><span id="OUTPUT"></span>Output


Using the Pixel Shader (PS) stage is optional; the rasterizer stage can output directly to the [Output Merger (OM) stage](output-merger-stage--om-.md) instead.

## <span id="related-topics"></span>Related topics


[Rasterization rules](rasterization-rules.md)

[Graphics pipeline](graphics-pipeline.md)

 

 




