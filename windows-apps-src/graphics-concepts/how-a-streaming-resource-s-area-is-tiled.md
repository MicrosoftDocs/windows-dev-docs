---
title: How a streaming resource's area is tiled
description: When you create a streaming resource, the dimensions, format element size, and number of mipmaps and/or array slices (if applicable) determine the number of tiles that are required to back the entire surface area.
ms.assetid: 3485FD8D-2A06-4B0A-8810-ECF37736F94B
keywords:
- How a streaming resource's area is tiled
- resource area, tiled
- size tables, resources, tiled
author: michaelfromredmond
ms.author: mithom
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
---

# How a streaming resource's area is tiled


When you create a streaming resource, the dimensions, format element size, and number of mipmaps and/or array slices (if applicable) determine the number of tiles that are required to back the entire surface area. The pixel/byte layout within tiles is determined by the implementation. The number of pixels that fit in a tile, depending on the format element size, is fixed and identical whether you use a standard swizzle or not.

The number of tiles that will be used by a given surface size and format element width is well defined and predictable based on the tables in the following sections. For resources that contain mipmaps or cases where surface dimensions don't fill a tile, some constraints exist; see [Mipmap packing](mipmap-packing.md).

Different streaming resources can point to identical memory with different formats as long as applications don't rely on the results of writing to the memory with one format and reading with another. But applications can rely on the results of writing to the memory with one format and reading with another if the formats are in the same format family (that is, they have the same typeless parent format). For example, DXGI\_FORMAT\_R8G8B8A8\_UNORM and DXGI\_FORMAT\_R8G8B8A8\_UINT are compatible with each other but not with DXGI\_FORMAT\_R16G16\_UNORM.

An exception is where bleeding data from one format aliasing to another is well defined: if a tile completely contains 0 for all its bits, that tile can be used with any format that interprets those memory contents as 0 (regardless of memory layout). So, a tile could be cleared to 0x00 with the format DXGI\_FORMAT\_R8\_UNORM and then used with a format like DXGI\_FORMAT\_R32G32\_FLOAT and it would appear the contents are still (0.0f,0.0f).

The layout of data within a tile doesn't depend on where the tile is mapped in a resource overall. So, for example, a tile can be reused in different locations of a surface at once with consistent behavior in all locations.

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
<td align="left"><p>[Texture2D and Texture2DArray subresource tiling](texture2d-and-texture2darray-subresource-tiling.md)</p></td>
<td align="left"><p>These tables show how [<strong>Texture2D</strong>](https://msdn.microsoft.com/library/windows/desktop/ff471525) and [<strong>Texture2DArray</strong>](https://msdn.microsoft.com/library/windows/desktop/ff471526) subresources are tiled.</p></td>
</tr>
<tr class="even">
<td align="left"><p>[Texture3D subresource tiling](texture3d-subresource-tiling.md)</p></td>
<td align="left"><p>This table shows how [<strong>Texture3D</strong>](https://msdn.microsoft.com/library/windows/desktop/ff471562) subresources are tiled.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>[Buffer tiling](buffer-tiling.md)</p></td>
<td align="left"><p>A [Buffer](introduction-to-buffers.md) resource is divided into 64KB tiles, with some empty space in the last tile if the size is not a multiple of 64KB.</p></td>
</tr>
<tr class="even">
<td align="left"><p>[Mipmap packing](mipmap-packing.md)</p></td>
<td align="left"><p>Some number of mips (per array slice) can be packed into some number of tiles, depending on a streaming resource's dimensions, format, number of mipmaps, and array slices.</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Creating streaming resources](creating-streaming-resources.md)

 

 




