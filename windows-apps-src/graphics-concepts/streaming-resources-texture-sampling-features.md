---
title: Streaming resources texture sampling features
description: Streaming resources texture sampling features include getting shader status feedback about mapped areas, checking whether all data being accessed was mapped in the resource, clamping to help shaders avoid areas in mipmapped streaming resources that are known to be non-mapped, and discovering what the minimum LOD that is fully mapped for an entire texture filter footprint will be.
ms.assetid: C2B2DD69-8354-417A-894D-6235A8B48B53
keywords:
- Streaming resources texture sampling features
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Streaming resources texture sampling features


Streaming resources texture sampling features include getting shader status feedback about mapped areas, checking whether all data being accessed was mapped in the resource, clamping to help shaders avoid areas in mipmapped streaming resources that are known to be non-mapped, and discovering what the minimum LOD that is fully mapped for an entire texture filter footprint will be.

## <span id="Requirements_of_streaming_resources_texture_sampling_features"></span><span id="requirements_of_streaming_resources_texture_sampling_features"></span><span id="REQUIREMENTS_OF_STREAMING_RESOURCES_TEXTURE_SAMPLING_FEATURES"></span>Requirements of streaming resources texture sampling features


The texture sampling features described here require [Tier 2](tier-2.md) level of streaming resources support.

## <span id="Shader_status_feedback_about_mapped_areas"></span><span id="shader_status_feedback_about_mapped_areas"></span><span id="SHADER_STATUS_FEEDBACK_ABOUT_MAPPED_AREAS"></span>Shader status feedback about mapped areas


Any shader instruction that reads and/or writes to a streaming resource causes status information to be recorded. This status is exposed as an optional extra return value on every resource access instruction that goes into a 32-bit temp register. The contents of the return value are opaque. That is, direct reading by the shader program is disallowed. But, you can use the [**CheckAccessFullyMapped**](/windows/desktop/direct3dhlsl/checkaccessfullymapped) function to extract the status info.

## <span id="Fully_mapped_check"></span><span id="fully_mapped_check"></span><span id="FULLY_MAPPED_CHECK"></span>Fully mapped check


The [**CheckAccessFullyMapped**](/windows/desktop/direct3dhlsl/checkaccessfullymapped) function interprets the status returned from a memory access and indicates whether all data being accessed was mapped in the resource. **CheckAccessFullyMapped** returns true (0xFFFFFFFF) if data was mapped or false (0x00000000) if data was unmapped.

During filter operations, sometimes the weight of a given texel ends up being 0.0. An example is a linear sample with texture coordinates that fall directly on a texel center: 3 other texels (which ones they are can vary by hardware) contribute to the filter but with 0 weight. These 0 weight texels don't contribute to the filter result at all, so if they happen to fall on **NULL** tiles, they don't count as an unmapped access. Note the same guarantee applies for texture filters that include multiple mip levels; if the texels on one of the mipmaps isn't mapped but the weight on those texels is 0, those texels don't count as an unmapped access.

When sampling from a format that has fewer than 4 components (such as DXGI\_FORMAT\_R8\_UNORM), any texels that fall on **NULL** tiles result in the a **NULL** mapped access being reported, regardless of which components the shader actually looks at in the result. For example, reading from R8\_UNORM and masking the read result in the shader with .gba/.yzw wouldn't appear to need to read the texture at all. But if the texel address is a **NULL** mapped tile, the operation still counts as a **NULL** map access.

The shader can check the status and pursue any desired course of action on failure. For example, a course of action can be logging 'misses' (say via UAV write) and/or issuing another read clamped to a coarser LOD known to be mapped. An application might want to track successful accesses as well in order to get a sense of what portion of the mapped set of tiles got accessed.

One complication for logging is no mechanism exists for reporting the exact set of tiles that would have been accessed. The application can make conservative guesses based on knowing the coordinates it used for access, as well as using the LOD instruction; for example, [**tex2Dlod**](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-tex2dlod)) returns the hardware LOD calculation.

Another complication is that lots of accesses will be to the same tiles, so a lot of redundant logging will occur and possibly contention on memory. It could be convenient if the hardware could be given the option to not bother to report tile accesses if they were reported elsewhere before. Perhaps the state of such tracking could be reset from the API (likely at frame boundaries).

## <span id="Per-sample_MinLOD_clamp"></span><span id="per-sample_minlod_clamp"></span><span id="PER-SAMPLE_MINLOD_CLAMP"></span>Per-sample MinLOD clamp


To help shaders avoid areas in mipmapped streaming resources that are known to be non-mapped, most shader instructions that involve using a sampler (filtering) have a mode that allows the shader to pass an additional float32 MinLOD clamp parameter to the texture sample. This value is in the view's mipmap number space, as opposed to the underlying resource.

The hardware performs` max(fShaderMinLODClamp,fComputedLOD) `in the same place in the LOD calculation where the per-resource MinLOD clamp occurs, which is also a [**max**](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-max)().

If the result of applying the per-sample LOD clamp and any other LOD clamps defined in the sampler is an empty set, the result is the same out of bounds access result as the per-resource minLOD clamp: 0 for components in the surface format and defaults for missing components.

The the LOD instruction (for example, [**tex2Dlod**](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-tex2dlod)), which predates the per-sample minLOD clamp described here, returns both a clamped and unclamped LOD. The clamped LOD returned from this LOD instruction reflects all clamping including the per-resource clamp, but not a per-sample clamp. Per-sample clamp is controlled and known by the shader anyway, so the shader author can manually apply that clamp to the LOD instruction's return value if desired.

## <span id="Min_Max_reduction_filtering"></span><span id="min_max_reduction_filtering"></span><span id="MIN_MAX_REDUCTION_FILTERING"></span>Min/Max reduction filtering


Applications can choose to manage their own data structures that inform them of what the mappings looks like for a streaming resource. An example would be a surface that contains a texel to hold information for every tile in a streaming resource. One might store the first LOD that is mapped at a given tile location. By careful sampling of this data structure in a similar way that the streaming resource is intended to be sampled, one might discover what the minimum LOD that is fully mapped for an entire texture filter footprint will be. To help make this process easier, Direct3D 11.2 introduces a new general purpose sampler mode, min/max filtering.

The utility of min/max filtering for LOD tracking might be useful for other purposes, such as, perhaps the filtering of depth surfaces.

Min/max reduction filtering is a mode on samplers that fetches the same set of texels that a normal texture filter would fetch. But instead of blending the values to produce an answer, it returns the min() or max() of the texels fetched, on a per-component basis (for example, the min of all the R values, separately from the min of all the G values and so on).

The min/max operations follow Direct3D arithmetic precision rules. The order of comparisons doesn't matter.

During filter operations that aren't min/max, sometimes the weight of a given texel ends up being 0.0. An example is a linear sample with texture coordinates that fall directly on a texel center; in that case, 3 other texels (which ones they are may vary by hardware) contribute to the filter, but with 0 weight. For any of these texels that would be 0 weight on a non-min/max filter, if the filter is min/max, these texels still do not contribute to the result (and the weights do not otherwise affect the min/max filter operation).

Support for this feature depends on [Tier 2](tier-2.md) support for streaming resources.

## <span id="related-topics"></span>Related topics


[Pipeline access to streaming resources](pipeline-access-to-streaming-resources.md)

 

 