---
title: Tier 1
description: Learn the general and specific limitations affecting Tier 1 support for streaming resources features in Direct3D.
ms.assetid: 153DA429-0C99-4691-AEB4-124FD9B17BE2
keywords:
- Tier 1
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Tier 1


This section describes tier 1 support.

## <span id="Tier_1_general_limitations"></span><span id="tier_1_general_limitations"></span><span id="TIER_1_GENERAL_LIMITATIONS"></span>Tier 1 general limitations


-   Hardware at feature level 11.0 minimum.
-   No quilting support.
-   No Texture1D or Texture3D support.
-   No 2, 8 or 16 sample multisample antialiasing (MSAA) support. Only 4x is required, except no 128 bpp formats.
-   No standard swizzle pattern (layout within 64KB tiles and tail mip packing is up to the hardware vendor).
-   Limitations on how tiles can be accessed when there are duplicate mappings. See [Tile access limitations with duplicate mappings](tile-access-limitations-with-duplicate-mappings.md).

## <span id="Specific_limitations_affecting_tier_1_only"></span><span id="specific_limitations_affecting_tier_1_only"></span><span id="SPECIFIC_LIMITATIONS_AFFECTING_TIER_1_ONLY"></span>Specific limitations affecting tier 1 only


### <span id="Reading_writing_to_streaming_resources_that_have_NULL_mappings"></span><span id="reading_writing_to_streaming_resources_that_have_null_mappings"></span><span id="READING_WRITING_TO_STREAMING_RESOURCES_THAT_HAVE_NULL_MAPPINGS"></span>Reading/writing to streaming resources that have NULL mappings

Streaming resources can have **NULL** mappings but reading from them or writing to them produces undefined results, including device removed. Applications can get around this by mapping a single dummy page to all the empty areas. Take care if you write and render to a page that is mapped to multiple render target locations, because the order of writes will be undefined.

### <span id="No_shader_instructions_for_clamping_LOD_and_mapped_status_feedback"></span><span id="no_shader_instructions_for_clamping_lod_and_mapped_status_feedback"></span><span id="NO_SHADER_INSTRUCTIONS_FOR_CLAMPING_LOD_AND_MAPPED_STATUS_FEEDBACK"></span>No shader instructions for clamping LOD and mapped status feedback

Shader instructions for clamping LOD and mapped status feedback are not available. See [HLSL streaming resources exposure](hlsl-streaming-resources-exposure.md).

### <span id="Alignment_constraints_for_standard_tile_shapes"></span><span id="alignment_constraints_for_standard_tile_shapes"></span><span id="ALIGNMENT_CONSTRAINTS_FOR_STANDARD_TILE_SHAPES"></span>Alignment constraints for standard tile shapes

It is only guaranteed that mips (starting from the finest) whose dimensions are all multiples of the standard tile size support the standard tile shapes and can have individual tiles arbitrarily mapped/unmapped. The first mipmap in a streaming resource that has any dimension not a multiple of standard tile size, along with all coarser mipmaps, can have a non-standard tiling shape, fitting into N 64KB tiles for this set of mips at once (N reported to the application). These N tiles are considered packed as one unit, which must be either fully mapped or fully unmapped by the application at any given time, though the mappings of each of the N tiles can be at arbitrarily disjoint locations in a tile pool.

### <span id="Array_of_mipmaps_that_aren_t_a_multiple_of_standard_tile_size"></span><span id="array_of_mipmaps_that_aren_t_a_multiple_of_standard_tile_size"></span><span id="ARRAY_OF_MIPMAPS_THAT_AREN_T_A_MULTIPLE_OF_STANDARD_TILE_SIZE"></span>Array of mipmaps that aren't a multiple of standard tile size

Streaming resources with any mipmaps not a multiple of standard tile size in all dimensions are not allowed to have an array size larger than 1.

### <span id="Switching_between_referencing_tiles_in_a_tile_pool_via_a_Buffer_and_Texture_resource"></span><span id="switching_between_referencing_tiles_in_a_tile_pool_via_a_buffer_and_texture_resource"></span><span id="SWITCHING_BETWEEN_REFERENCING_TILES_IN_A_TILE_POOL_VIA_A_BUFFER_AND_TEXTURE_RESOURCE"></span>Switching between referencing tiles in a tile pool via a Buffer and Texture resource

In order to switch between referencing tiles in a tile pool via a [Buffer](introduction-to-buffers.md) resource to referencing the same tiles via a [Texture](introduction-to-textures.md) resource, or vice-versa, the most recent updating of tile mappings or copying of tile mappings that defines mappings to those tile pool tiles must be for the same resource dimension (Buffer versus Texture\*) as the resource dimension that will be used to access the tiles. Otherwise, behavior is undefined, including the chance of device reset.

So, for example, it is invalid to update the tile mappings to define tile mappings for a Buffer, then update the tile mappings to the same tiles in the tile pool via a [**Texture2D**](/windows/desktop/direct3dhlsl/sm5-object-texture2d) resource, then access the tiles via the Buffer. Work-around operations are to either redefine tile mappings for a resource when switching between Buffer and Texture (or vice versa) sharing tiles or just never sharing tiles in a tile pool between Buffer resources and Texture resources.

### <span id="Min_Max_reduction_filtering"></span><span id="min_max_reduction_filtering"></span><span id="MIN_MAX_REDUCTION_FILTERING"></span>Min/Max reduction filtering

Min/Max reduction filtering is not supported. See [Streaming resources texture sampling features](streaming-resources-texture-sampling-features.md).

## <span id="related-topics"></span>Related topics


[Streaming resources features tiers](streaming-resources-features-tiers.md)

 

 