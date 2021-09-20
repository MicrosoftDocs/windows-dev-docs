---
title: Tier 2
description: Tier 2 support for streaming resources adds capabilities beyond Tier 1, such as guaranteeing nonpacked texture mipmap when the size is at least one standard tile shape; shader instructions for clamping level-of-detail (LOD) and for obtaining status about the shader operation; also, reading from NULL-mapped tiles treat that sampled value as zero.
ms.assetid: 111A28EA-661A-4D29-921A-F2E376A46DC5
keywords:
- Tier 2
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Tier 2


Tier 2 support for streaming resources adds capabilities beyond Tier 1, such as guaranteeing nonpacked texture mipmap when the size is at least one standard tile shape; shader instructions for clamping level-of-detail (LOD) and for obtaining status about the shader operation; also, reading from NULL-mapped tiles treat that sampled value as zero.

## <span id="Tier_2_general_support"></span><span id="tier_2_general_support"></span><span id="TIER_2_GENERAL_SUPPORT"></span>Tier 2 general support


Tier 2 support includes the following.

-   Hardware at Feature Level 11.1 minimum.
-   All features of the previous tier (without [Tier 1](tier-1.md) specific limitations) plus the additions in these following items:
-   Shader instructions for clamping LOD and mapped status feedback are available. See [HLSL streaming resources exposure](hlsl-streaming-resources-exposure.md).

In addition to these, there are some specific support issues that follow.

## <span id="Non-mapped_tiles"></span><span id="non-mapped_tiles"></span><span id="NON-MAPPED_TILES"></span>Non-mapped tiles


Reads from non-mapped tiles return 0 in all non-missing components of the format, and the default for missing components.

Writes to non-mapped tiles are stopped from going to memory but might end up in caches that subsequent reads to the same address might or might not pick up.

## <span id="Texture_filtering"></span><span id="texture_filtering"></span><span id="TEXTURE_FILTERING"></span>Texture filtering


Texture filtering with a footprint that straddles **NULL** and non-**NULL** tiles contributes 0 (with defaults for missing format components) for texels on **NULL** tiles into the overall filter operation. Some early hardware don't meet this requirement and returns 0 (with defaults for missing format components) for the full filter result if any texels (with nonzero weight) fall on a **NULL** tile. No other hardware will be allowed to miss the requirement to include all (nonzero weighted) texels in the filter operation.

**NULL** texel accesses cause the [**CheckAccessFullyMapped**](/windows/desktop/direct3dhlsl/checkaccessfullymapped) operation on the status feedback for a texture read to return false. This is regardless of how the texture access result might get write masked in the shader and how many components are in the texture format (the combination of which might make it appear that the texture does not need to be accessed).

## <span id="Alignment_constraints"></span><span id="alignment_constraints"></span><span id="ALIGNMENT_CONSTRAINTS"></span>Alignment constraints


Alignment constraints for standard tile shapes: Mipmaps that fill at least one standard tile in all dimensions are guaranteed to use the standard tiling, with the remainder considered packed as a **unit** into N tiles (N reported to the application). The application can map the N tiles into arbitrarily disjoint locations in a tile pool, but must either map all or none of the packed tiles. The mip packing is a unique set of packed tiles per array slice.

## <span id="Min_Max_reduction_filtering"></span><span id="min_max_reduction_filtering"></span><span id="MIN_MAX_REDUCTION_FILTERING"></span>Min/Max reduction filtering


Min/Max reduction filtering is supported. See [Streaming resources texture sampling features](streaming-resources-texture-sampling-features.md).

## <span id="Limitations"></span><span id="limitations"></span><span id="LIMITATIONS"></span>Limitations


Streaming resources with any mipmaps less than standard tile size in any dimension are not allowed to have an array size larger than 1.

Limitations on how tiles can be accessed when there are duplicate mappings continue to apply. See [Tile access limitations with duplicate mappings](tile-access-limitations-with-duplicate-mappings.md).

## <span id="related-topics"></span>Related topics


[Streaming resources features tiers](streaming-resources-features-tiers.md)

 

 