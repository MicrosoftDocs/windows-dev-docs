---
title: HLSL streaming resources exposure
description: A specific Microsoft High Level Shader Language (HLSL) syntax is required to support streaming resources in Shader Model 5.
ms.assetid: 00A40D82-0565-43DC-82AB-0675B7E772E3
keywords:
- HLSL streaming resources exposure
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# HLSL streaming resources exposure


A specific Microsoft High Level Shader Language (HLSL) syntax is required to support streaming resources in [Shader Model 5](/windows/desktop/direct3dhlsl/d3d11-graphics-reference-sm5).

The HLSL syntax for Shader Model 5 is allowed only on devices with streaming resources support. Each relevant HLSL method for streaming resources in the following table accepts either one (feedback) or two (clamp and feedback in this order) additional optional parameters. For example, a **Sample** method is:

**Sample(sampler, location \[, offset \[, clamp \[, feedback\] \] \])**

An example of a **Sample** method is [**Texture2D.Sample(S,float,int,float,uint)**](/windows/desktop/direct3dhlsl/t2darray-sample-s-float-int-float-uint-).

The offset, clamp and feedback parameters are optional. You must specify all optional parameters up to the one you need, which is consistent with the C++ rules for default function arguments. For example, if the feedback status is needed, both offset and clamp parameters need to be explicitly supplied to **Sample**, even though they may not be logically needed.

The clamp parameter is a scalar float value. The literal value of clamp=0.0f indicates that the clamp operation isn't performed.

The feedback parameter is a **uint** variable that you can supply to the memory-access querying intrinsic [**CheckAccessFullyMapped**](/windows/desktop/direct3dhlsl/checkaccessfullymapped) function. You must not modify or interpret the value of the feedback parameter; but, the compiler doesn't provide any advanced analysis and diagnostics to detect whether you modified the value.

Here is the syntax of [**CheckAccessFullyMapped**](/windows/desktop/direct3dhlsl/checkaccessfullymapped):

**bool CheckAccessFullyMapped(in uint FeedbackVar);**

[**CheckAccessFullyMapped**](/windows/desktop/direct3dhlsl/checkaccessfullymapped) interprets the value of *FeedbackVar* and returns true if all data being accessed was mapped in the resource; otherwise, **CheckAccessFullyMapped** returns false.

If either the clamp or feedback parameter is present, the compiler emits a variant of the basic instruction. For example, sample of a streaming resource generates the `sample_cl_s` instruction.

If neither clamp nor feedback is specified, the compiler emits the basic instruction, so that there is no change from the current behavior.

The clamp value of 0.0f indicates that no clamp is performed; thus, the driver compiler can further tailor the instruction to the target hardware. If feedback is a NULL register in an instruction, the feedback is unused; thus, the driver compiler can further tailor the instruction to the target architecture.

If the HLSL compiler infers that clamp is 0.0f and feedback is unused, the compiler emits the corresponding basic instruction (for example, `sample` rather than `sample_cl_s`).

If a streaming resource access consists of several constituent byte code instructions, for example, for structured resources, the compiler aggregates individual feedback values via the OR operation to produce the final feedback value. Therefore, you see a single feedback value for such a complex access.

This is the summary table of HLSL methods that are changed to support feedback and/or clamp. These all work on tiled and non-streaming resources of all dimensions. Non-streaming resources always appear to be fully mapped.

<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left"><a href="/windows/desktop/direct3dhlsl/d3d11-graphics-reference-sm5-objects">HLSL objects</a> </th>
<th align="left">Intrinsic methods with feedback option (*) - also has clamp option</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p>[RW]Texture2D</p>
<p>[RW]Texture2DArray</p>
<p>TextureCUBE</p>
<p>TextureCUBEArray</p></td>
<td align="left"><p>Gather</p>
<p>GatherRed</p>
<p>GatherGreen</p>
<p>GatherBlue</p>
<p>GatherAlpha</p>
<p>GatherCmp</p>
<p>GatherCmpRed</p>
<p>GatherCmpGreen</p>
<p>GatherCmpBlue</p>
<p>GatherCmpAlpha</p></td>
</tr>
<tr class="even">
<td align="left"><p>[RW]Texture1D</p>
<p>[RW]Texture1DArray</p>
<p>[RW]Texture2D</p>
<p>[RW]Texture2DArray</p>
<p>[RW]Texture3D</p>
<p>TextureCUBE</p>
<p>TextureCUBEArray</p></td>
<td align="left"><p>Sample*</p>
<p>SampleBias*</p>
<p>SampleCmp*</p>
<p>SampleCmpLevelZero</p>
<p>SampleGrad*</p>
<p>SampleLevel</p></td>
</tr>
<tr class="odd">
<td align="left"><p>[RW]Texture1D</p>
<p>[RW]Texture1DArray</p>
<p>[RW]Texture2D</p>
<p>Texture2DMS</p>
<p>[RW]Texture2DArray</p>
<p>Texture2DArrayMS</p>
<p>[RW]Texture3D</p>
<p>[RW]Buffer</p>
<p>[RW]ByteAddressBuffer</p>
<p>[RW]StructuredBuffer</p></td>
<td align="left">Load</td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Pipeline access to streaming resources](pipeline-access-to-streaming-resources.md)

 

 