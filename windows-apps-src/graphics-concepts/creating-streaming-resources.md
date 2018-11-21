---
title: Creating streaming resources
description: Streaming resources are created by specifying a flag when you create a resource, indicating that the resource is a streaming resource.
ms.assetid: B3F3E43C-54D4-458C-9E16-E13CB382C83F
keywords:
- Creating streaming resources
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Creating streaming resources


Streaming resources are created by specifying a flag when you create a resource, indicating that the resource is a streaming resource.

Restrictions on when you can create a resource as a streaming resource are described in [Streaming resource creation parameters](streaming-resource-creation-parameters.md).

A non-streaming resource's storage is allocated in the graphics system when the resource is created, such as allocation for an array of 2D textures.

When a streaming resource is created, the graphics system doesn't allocate the storage for the resource contents. Instead, when an application creates a streaming resource, the graphics system makes an address space reservation for the tiled surface's area only, and then allows the mapping of the tiles to be controlled by the application. The "mapping" of a tile is simply the physical location in memory that a logical tile in a resource points to (or **NULL** for an unmapped tile).

Don't confuse this concept with the notion of mapping a Direct3D resource for CPU access, which despite using the same name is completely independent. You will be able to define and change the mapping of each tile individually as needed, knowing that all tiles for a surface don't need to be mapped at a time, thereby making effective use of the amount of memory available.

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
<td align="left"><p><a href="mappings-are-into-a-tile-pool.md">Mappings are into a tile pool</a></p></td>
<td align="left"><p>When a resource is created as a streaming resource, the tiles that make up the resource come from pointing at locations in a tile pool. A tile pool is a pool of memory (backed by one or more allocations behind the scenes - unseen by the application).</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="streaming-resource-creation-parameters.md">Streaming resource creation parameters</a></p></td>
<td align="left"><p>There are some constraints on the type of Direct3D resources that you can create as a streaming resource.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="tile-pool-creation-parameters.md">Tile pool creation parameters</a></p></td>
<td align="left"><p>Use the parameters in this section to define tile pools when creating a buffer.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="streaming-resource-cross-process-and-device-sharing.md">Streaming resource cross-process and device sharing</a></p></td>
<td align="left"><p>Tile pools can be shared with other processes just like traditional resources. Streaming resources that reference tile pools can't be shared across devices and processes.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="operations-available-on-streaming-resources.md">Operations available on streaming resources</a></p></td>
<td align="left"><p>This section lists operations that you can perform on streaming resources.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="operations-available-on-tile-pools.md">Operations available on tile pools</a></p></td>
<td align="left"><p>Operations on tile pools include resizing a tile pool, offering resources (yielding memory temporarily to the system for the entire tile pool), and reclaiming resources.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="how-a-streaming-resource-s-area-is-tiled.md">How a streaming resource's area is tiled</a></p></td>
<td align="left"><p>When you create a streaming resource, the dimensions, format element size, and number of mipmaps and/or array slices (if applicable) determine the number of tiles that are required to back the entire surface area.</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Streaming resources](streaming-resources.md)

 

 




