---
title: Tile access limitations with duplicate mappings
description: There are limitations on tile access with duplicate mappings, such as when copying streaming resources with overlapping source and destination, or when rendering to tiles shared within the render area.
ms.assetid: 6E40B1DC-BCF1-4B09-82A8-7B2D9B209A61
keywords:
- Tile access limitations with duplicate mappings
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Tile access limitations with duplicate mappings


There are limitations on tile access with duplicate mappings, such as when copying streaming resources with overlapping source and destination, or when rendering to tiles shared within the render area.

## <span id="Copying_streaming_resources_with_overlapping_source_and_destination"></span><span id="copying_streaming_resources_with_overlapping_source_and_destination"></span><span id="COPYING_STREAMING_RESOURCES_WITH_OVERLAPPING_SOURCE_AND_DESTINATION"></span>Copying streaming resources with overlapping source and destination


If tiles in the source and destination area of a Copy\* operation have duplicated mappings in the copy area that would have overlapped even if both resources were not streaming resources and the Copy\* operation supports overlapping copies, the Copy\* operation will behave fine (as if the source is copied to a temporary location before going to the destination). But if the overlap is not obvious (like the source and destination resources are different but share mappings or mappings are duplicated over a given surface), results of the copy operation on the tiles that are shared are undefined.

## <span id="Copying_to_streaming_resource_with_duplicated_tiles_in_destination_area"></span><span id="copying_to_streaming_resource_with_duplicated_tiles_in_destination_area"></span><span id="COPYING_TO_STREAMING_RESOURCE_WITH_DUPLICATED_TILES_IN_DESTINATION_AREA"></span>Copying to streaming resource with duplicated tiles in destination area


Copying to a streaming resource with duplicated tiles in the destination area produces undefined results in these tiles unless the data itself is identical; different tiles might write the tiles in different orders.

## <span id="UAV_accesses_to_duplicate_tiles_mappings"></span><span id="uav_accesses_to_duplicate_tiles_mappings"></span><span id="UAV_ACCESSES_TO_DUPLICATE_TILES_MAPPINGS"></span>UAV accesses to duplicate tiles mappings


Suppose an unordered access view (UAV) on a streaming resource has duplicate tile mappings in its area or with other resources bound to the pipeline. Ordering of accesses to these duplicated tiles is undefined if performed by different threads, just as any ordering of memory access to UAVs in general is unordered.

## <span id="Rendering_after_tile_mapping_changes_or_content_updates_from_outside_mappings"></span><span id="rendering_after_tile_mapping_changes_or_content_updates_from_outside_mappings"></span><span id="RENDERING_AFTER_TILE_MAPPING_CHANGES_OR_CONTENT_UPDATES_FROM_OUTSIDE_MAPPINGS"></span>Rendering after tile mapping changes or content updates from outside mappings


If a streaming resource's tile mappings have changed or content in mapped tiled pool tiles have changed via another streaming resource's mappings, and the streaming resource is going to be rendered via render target view or depth stencil view, the application must Clear (using the fixed function Clear APIs) or fully copy over using Copy\*/Update\* APIs the tiles that have changed within the area being rendered (mapped or not).

Failure of an application to clear or copy in these cases results in hardware optimization structures for the given render target view or depth stencil view being stale, and will result in garbage rendering results on some hardware and inconsistency across different hardware. These hidden optimization data structures used by hardware might be local to individual mappings and not visible to other mappings to the same memory.

When you clear a resource view (setting all the elements in a resource view to one value), you can clear render target views with rectangles. For hardware that supports streaming resources, clearing a resource view must also support clearing of depth stencil views with rectangles, for depth only surfaces (without stencil). This operation allows applications to clear only the necessary area of a surface.

If an application needs to preserve existing memory contents of areas in a streaming resource where mappings have changed, that application must work around the clear requirement. The application can accomplish this work-around by first saving the contents where tile mappings have changed (by copying them to a temporary surface), issuing the required clear command, and then copying the contents back. While this would accomplish the task of preserving surface contents for incremental rendering, the downside is that subsequent rendering performance on the surface might suffer because rendering optimizations might be lost.

If a tile is mapped into multiple streaming resources at the same time and tile contents are manipulated by any means (render, copy, and so on) via one of the streaming resources, if the same tile is to be rendered via any other streaming resource, the tile must be cleared first as previously described.

## <span id="Rendering_to_tiles_shared_outside_render_area"></span><span id="rendering_to_tiles_shared_outside_render_area"></span><span id="RENDERING_TO_TILES_SHARED_OUTSIDE_RENDER_AREA"></span>Rendering to tiles shared outside render area


Suppose an area in a streaming resource is being rendered to and the tile pool tiles referenced by the render area are also mapped to from outside the render area (including via other streaming resources, at the same time or not). Data rendered to these tiles isn't guaranteed to appear correctly when viewed through the other mappings, even though the underlying memory layout is compatible. This fact is due to optimization data structures some hardware use that can be local to individual mappings for renderable surfaces and not visible to other mappings to the same memory location.

You can work around this restriction by copying from the rendered mapping to all the other mappings to the same memory that might be accessed (or clearing that memory or copying other data to it if the old contents are no longer needed). While this work-around seems redundant, it makes all other mappings to the same memory correctly understand how to access its contents, and at least the memory savings of having only a single physical memory backing remains intact.

Also, when you switch between using different streaming resources that share mappings (unless only reading), you must specify a data access ordering constraint (a barrier) between multiple tiled resources, in between the switches.

## <span id="Rendering_to_tiles_shared_within_render_area"></span><span id="rendering_to_tiles_shared_within_render_area"></span><span id="RENDERING_TO_TILES_SHARED_WITHIN_RENDER_AREA"></span>Rendering to tiles shared within render area


If an area in a streaming resource is being rendered to and within the render area multiple tiles are mapped to the same tile pool location, rendering results are undefined on those tiles.

## <span id="Data_compatibility_across_streaming_resources_sharing_tiles"></span><span id="data_compatibility_across_streaming_resources_sharing_tiles"></span><span id="DATA_COMPATIBILITY_ACROSS_STREAMING_RESOURCES_SHARING_TILES"></span>Data compatibility across streaming resources sharing tiles


Suppose multiple streaming resources have mappings to the same tile pool locations and each resource is used to access the same data. This scenario is only valid if the other rules about avoiding problems with hardware optimization structures are avoided, appropriate barriers are specified (specifying a data access ordering constraint between multiple tiled resources), and the streaming resources are compatible with each other.

The latter is described here in terms of what it means for streaming resources sharing tiles to be incompatible. The incompatibility conditions of accessing the same data across duplicate tile mappings are the use of different surface dimensions or format, or differences in the presence of render target or depth stencil bind flags on the resources. Writing to the memory with one type of mapping produces undefined results if you subsequently read or render via a mapping from an incompatible resource.

If the other resource sharing mappings are first initialized with new data (recycling the memory for a different purpose), the subsequent read or render operation is fine since data isn't bleeding across incompatible interpretations. But, when you switch between accessing incompatible mappings like this, you must specify barriers (specifying a data access ordering constraint between multiple tiled resources).

If the render target or depth stencil bind flag isn't set on any of the resources sharing mappings with each other, there are far fewer restrictions. As long as the format and surface types (for example, Texture2D) are the same, tiles can be shared. Different formats being compatible are cases such as BC\* surfaces and the equivalent sized uncompressed 32 bit or 16 bit per component format, like BC6H and R32G32B32A32. Many 32 bit per element formats can be aliased with R32\_\* as well (R10G10B10A2\_\*, R8G8B8A8\_\*, B8G8R8A8\_\*,B8G8R8X8\_\*,R16G16\_\*); this operation has always been allowed for non-streaming resources.

Sharing between packed and non-packed tiles is fine if the formats are compatible and the tiles are filled with solid color.

Finally, if nothing is common about the resources sharing tile mappings except that none have render target or depth stencil bind flags, only memory filled with 0 can be shared safely; the mapping will appear as whatever 0 decodes to for the definition of the given resource format (typically just 0).

## <span id="related-topics"></span>Related topics


[Pipeline access to streaming resources](pipeline-access-to-streaming-resources.md)

 

 




