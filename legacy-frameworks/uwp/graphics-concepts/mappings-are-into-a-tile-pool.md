---
title: Mappings are into a tile pool
description: When a resource is created as a streaming resource, the tiles that make up the resource come from pointing at locations in a tile pool. A tile pool is a pool of memory (backed by one or more allocations behind the scenes - unseen by the application).
ms.assetid: 58B8DBD5-62F5-4B94-8DD1-C7D57A812185
keywords:
- Mappings are into a tile pool
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Mappings are into a tile pool


When a resource is created as a streaming resource, the tiles that make up the resource come from pointing at locations in a tile pool. A tile pool is a pool of memory (backed by one or more allocations behind the scenes - unseen by the application). The operating system and display driver manage this pool of memory, and the memory footprint is easily understood by an application. Streaming resources map 64KB regions by pointing to locations in a tile pool. One fallout of this setup is it allows multiple resources to share and reuse the same tiles, and also for the same tiles to be reused at different locations within a resource if desired.

The cost for the flexibility of populating the tiles for a resource out of a tile pool is that the resource has to do the work of defining and maintaining the mapping of which tiles in the tile pool represent the tiles needed for the resource. Tile mappings can be changed. Also, not all tiles in a resource need to be mapped at a time; a resource can have **NULL** mappings. A **NULL** mapping defines a tile as not being available from the point of view of the resource accessing it.

Multiple tile pools can be created, and any number of streaming resources can map into any given tile pool at the same time. Tile pools can also be grown or shrunk. For more info, see [Tile pool resizing](tile-pool-resizing.md). One constraint that exists to simplify display driver and runtime implementation is that a given streaming resource can only have mappings into at most one tile pool at a time (as opposed to having simultaneous mapping to multiple tile pools).

The amount of storage that is associated with a streaming resource itself (that is, independent tile-pool memory) is roughly proportional to the number of tiles actually mapped to the pool at any given time. In hardware, this fact boils down to scaling the memory footprint for page table storage roughly with the amount of tiles that are mapped (for example, using a multilevel page table scheme as appropriate).

The tile pool can be thought of as an entirely software abstraction that enables Direct3D applications to effectively be able to program the page tables on the graphics processing unit (GPU) without having to know the low level implementation details (or deal with pointer addresses directly). Tile pools don't apply any additional levels of indirection in hardware. Optimizations of a single level page table using constructs like page directories are independent of the tile pool concept.

Let us explore what storage the page table itself could require in the worst case (though in practice implementations only require storage roughly proportional to what is mapped).

Suppose each page table entry is 64 bits.

For the worst-case page table size hit for a single surface, given the resource limits in Direct3D 11, suppose a streaming resource is created with a 128 bit-per-element format (for example, an RGBA float), so a 64KB tile contains only 4096 pixels. The maximum supported [**Texture2DArray**](/windows/desktop/direct3dhlsl/sm5-object-texture2darray) size of 16384\*16384\*2048 (but with only a single mipmap) would require about 1GB of storage in the page table if fully populated (not including mipmaps) using 64 bit table entries. Adding mipmaps would grow the fully-mapped (worst case) page table storage by about a third, to about 1.3GB.

This case would give access to about 10.6 terabytes of addressable memory. There might be a limit on the amount of addressable memory however, which would reduce these amounts, perhaps to around the terabyte range.

Another case to consider is a single [**Texture2D**](/windows/desktop/direct3dhlsl/sm5-object-texture2d) streaming resource of 16384\*16384 with a 32 bit-per-element format, including mipmaps. The space needed in a fully populated page table would be roughly 170KB with 64 bit table entries.

Finally, consider an example using a BC format, say BC7 with 128 bits per tile of 4x4 pixels. That is one byte per pixel. A [**Texture2DArray**](/windows/desktop/direct3dhlsl/sm5-object-texture2darray) of 16384\*16384\*2048 including mipmaps would require roughly 85MB to fully populate this memory in a page table. That is not bad considering this allows one streaming resource to span 550 gigapixels (512 GB of memory in this case).

In practice, nowhere near these full mappings would be defined given that the amount of physical memory available wouldn't allow anywhere near that much to be mapped and referenced at a time. But with a tile pool, applications could choose to reuse tiles (as a simple example, reusing a "black" colored tile for large black regions in an image) - effectively using the tile pool (that is, page table mappings) as a tool for memory compression.

The initial contents of the page table are **NULL** for all entries. Applications also can't pass initial data for the memory contents of the surface since it starts off with no memory backing.

## <span id="in-this-section"></span>In this section


<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Topic</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p><a href="tile-pool-creation.md">Tile pool creation</a></p></td>
<td align="left"><p>Applications can create one or more tile pools per Direct3D device. The total size of each tile pool is restricted to Direct3D 11's resource size limit, which is roughly 1/4 of GPU RAM.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="tile-pool-resizing.md">Tile pool resizing</a></p></td>
<td align="left"><p>Resize a tile pool to grow a tile pool if the application needs more working set for the streaming resources mapping into it, or to shrink if less space is needed.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="hazard-tracking-versus-tile-pool-resources.md">Hazard tracking versus tile pool resources</a></p></td>
<td align="left"><p>For non-streaming resources, Direct3D can prevent certain hazard conditions during rendering, but because hazard tracking would be at a tile level for streaming resources, tracking hazard conditions during rendering of streaming resources might be too expensive.</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Creating streaming resources](creating-streaming-resources.md)

 

 