---
title: Hull Shader (HS) stage
description: The Hull Shader (HS) stage is one of the tessellation stages, which efficiently break up a single surface of a model into many triangles.
ms.assetid: C62F6F15-CAD7-4C72-9735-00762E346C4C
keywords:
- Hull Shader (HS) stage
ms.date: 02/08/2017
ms.topic: article
ms.localizationpriority: medium
---

# Hull Shader (HS) stage

The Hull Shader (HS) stage is one of the tessellation stages, which efficiently break up a single surface of a model into many triangles. The Hull Shader (HS) stage produces a geometry patch (and patch constants) that correspond to each input patch (quad, triangle, or line). A hull shader is invoked once per patch, and it transforms input control points that define a low-order surface into control points that make up a patch. It also does some per-patch calculations to provide data for the [Tessellator (TS) stage](tessellator-stage--ts-.md) and the [Domain Shader (DS) stage](domain-shader-stage--ds-.md).

## <span id="Purpose_and_uses"></span><span id="purpose_and_uses"></span><span id="PURPOSE_AND_USES"></span>Purpose and uses


![diagram of the hull-shader stage](images/d3d11-hull-shader.png)

The three tessellation stages work together to convert higher-order surfaces (which keep the model simple and efficient) to many triangles for detailed rendering within the graphics pipeline. The tessellation stages include the Hull Shader (HS) stage, [Tessellator (TS) stage](tessellator-stage--ts-.md), and [Domain Shader (DS) stage](domain-shader-stage--ds-.md).

The Hull Shader (HS) stage is a programmable shader stage. A hull shader is implemented with an HLSL function.

A hull shader operates in two phases: a control-point phase and a patch-constant phase, which are run in parallel by the hardware. The HLSL compiler extracts the parallelism in a hull shader and encodes it into bytecode that drives the hardware.

-   The control-point phase operates once for each control-point, reading the control points for a patch, and generating one output control point (identified by a **ControlPointID**).
-   The patch-constant phase operates once per patch to generate edge tessellation factors and other per-patch constants. Internally, many patch-constant phases may run at the same time. The patch-constant phase has read-only access to all input and output control points.

## <span id="Input"></span><span id="input"></span><span id="INPUT"></span>Input


Between 1 and 32 input control points, which together define a low-order surface.

-   The hull shader declares the state required by the [Tessellator (TS) stage](tessellator-stage--ts-.md). This includes information such as the number of control points, the type of patch face and the type of partitioning to use when tessellating. This information appears as declarations typically at the front of the shader code.
-   Tessellation factors determine how much to subdivide each patch.

## <span id="Output"></span><span id="output"></span><span id="OUTPUT"></span>Output


Between 1 and 32 output control points, which together make up a patch.

-   The shader output is between 1 and 32 control points, regardless of the number of tessellation factors. The control-points output from a hull shader can be consumed by the domain-shader stage. Patch constant data can be consumed by a domain shader. Tessellation factors can be consumed by the [Tessellator (TS) stage](tessellator-stage--ts-.md) and the [Domain Shader (DS) stage](domain-shader-stage--ds-.md).
-   If the hull shader sets any edge tessellation factor to ≤ 0 or NaN, the patch will be culled (omitted). As a result, the tessellator stage may or may not run, the domain shader will not run, and no visible output will be produced for that patch.

## <span id="Example"></span><span id="example"></span><span id="EXAMPLE"></span>Example


```hlsl
[patchsize(12)]
[patchconstantfunc(MyPatchConstantFunc)]
MyOutPoint main(uint Id : SV_ControlPointID,
     InputPatch<MyInPoint, 12> InPts)
{
     MyOutPoint result;
     
     ...
     
     result = TransformControlPoint( InPts[Id] );

     return result;
}
```

See [How To: Create a Hull Shader](/windows/desktop/direct3d11/direct3d-11-advanced-stages-hull-shader-create).

## <span id="related-topics"></span>Related topics


[Graphics pipeline](graphics-pipeline.md)

 

 