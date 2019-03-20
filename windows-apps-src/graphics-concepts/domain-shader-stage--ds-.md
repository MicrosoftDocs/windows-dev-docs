---
title: Domain Shader (DS) stage
description: The Domain Shader (DS) stage calculates the vertex position of a subdivided point in the output patch; it calculates the vertex position that corresponds to each domain sample.
ms.assetid: 673CC04A-A74F-495F-AFB7-49157538749C
keywords:
- Domain Shader (DS) stage
ms.date: 02/08/2017
ms.topic: article
ms.localizationpriority: medium
---

# Domain Shader (DS) stage

The Domain Shader (DS) stage calculates the vertex position of a subdivided point in the output patch; it calculates the vertex position that corresponds to each domain sample. A domain shader is run once per tessellator stage output point and has read-only access to the hull shader output patch and output patch constants, and the tessellator stage output UV coordinates.

## <span id="Purpose_and_uses"></span><span id="purpose_and_uses"></span><span id="PURPOSE_AND_USES"></span>Purpose and uses


The Domain Shader (DS) stage outputs the vertex position of a subdivided point in the output patch, based on input from the [Hull Shader (HS) stage](hull-shader-stage--hs-.md) and the [Tessellator (TS) stage](tessellator-stage--ts-.md).

![diagram of the domain-shader stage](images/d3d11-domain-shader.png)

## <span id="Input"></span><span id="input"></span><span id="INPUT"></span>Input


-   A domain shader consumes output control points from the [Hull Shader (HS) stage](hull-shader-stage--hs-.md). The hull shader outputs include:
    -   Control points.
    -   Patch constant data.
    -   Tessellation factors. The tessellation factors can include the values used by the fixed-function tessellator as well as the raw values (before rounding by integer tessellation, for example), which facilitates geomorphing, for example.
-   A domain shader is invoked once per output coordinate from the [Tessellator (TS) stage](tessellator-stage--ts-.md).

## <span id="Output"></span><span id="output"></span><span id="OUTPUT"></span>Output


-   The Domain Shader (DS) stage outputs the vertex position of a subdivided point in the output patch.

After the domain shader completes, tessellation is finished and pipeline data continues to the next pipeline stage, such as the [Geometry Shader (GS) stage](geometry-shader-stage--gs-.md) and the [Pixel Shader (PS) stage](pixel-shader-stage--ps-.md). A geometry shader that expects primitives with adjacency (for example, 6 vertices per triangle) is not valid when tessellation is active (this results in undefined behavior, which the debug layer will complain about).

## <span id="Example"></span><span id="example"></span><span id="EXAMPLE"></span>Example


```hlsl
void main( out    MyDSOutput result, 
           float2 myInputUV : SV_DomainPoint, 
           MyDSInput DSInputs,
           OutputPatch<MyOutPoint, 12> ControlPts, 
           MyTessFactors tessFactors)
{
     ...

     result.Position = EvaluateSurfaceUV(ControlPoints, myInputUV);
}
```

## <span id="related-topics"></span>Related topics


[Graphics pipeline](graphics-pipeline.md)

 

 




