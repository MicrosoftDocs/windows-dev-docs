---
title: Compressed texture resources
description: Texture maps are digitized images drawn on three-dimensional shapes to add visual detail.
ms.assetid: 2DD5FF94-A029-4694-B103-26946C8DFBC1
keywords:
- Compressed texture resources
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Compressed texture resources


Texture maps are digitized images drawn on three-dimensional shapes to add visual detail. They are mapped into these shapes during rasterization, and the process can consume large amounts of both the system bus and memory. To reduce the amount of memory consumed by textures, Direct3D supports the compression of texture surfaces. Some Direct3D devices support compressed texture surfaces natively. On such devices, when you have created a compressed surface and loaded the data into it, the surface can be used in Direct3D like any other texture surface. Direct3D handles decompression when the texture is mapped to a 3D object.

## <span id="Storage-Efficiency-and-Texture-Compression"></span><span id="storage-efficiency-and-texture-compression"></span><span id="STORAGE-EFFICIENCY-AND-TEXTURE-COMPRESSION"></span>Storage efficiency and texture compression


All texture compression formats are powers of two. While this does not mean that a texture is necessarily square, it does mean that both x and y are powers of two. For example, if a texture is originally 512 by 128 bytes, the next mipmapping would be 256 by 64 and so on, with each level decreasing by a power of two. At lower levels, where the texture is filtered to 16 by 2 and 8 by 1, there will be wasted bits because the compression block is always a 4 by 4 block of texels. Unused portions of the block are padded.

Although there are wasted bits at the lowest levels, the overall gain is still significant. The worst case is, in theory, a 2K by 1 texture (2⁰ power). Here, only a single row of pixels is encoded per block, while the rest of the block is unused.

## <span id="Mixing-Formats-Within-a-Single-Texture"></span><span id="mixing-formats-within-a-single-texture"></span><span id="MIXING-FORMATS-WITHIN-A-SINGLE-TEXTURE"></span>Mixing formats within a single texture


It is important to note that any single texture must specify that its data is stored as 64 or 128 bits per group of 16 texels. If 64-bit blocks - that is, block compression format BC1 - are used for the texture, it is possible to mix the opaque and 1-bit alpha formats on a per-block basis within the same texture. In other words, the comparison of the unsigned integer magnitude of color\_0 and color\_1 is performed uniquely for each block of 16 texels.

Once the 128-bit blocks are used, the alpha channel must be specified in either explicit (format BC2) or interpolated mode (format BC3) for the entire texture. As with color, when interpolated mode (format BC3) is selected, then either eight interpolated alphas or six interpolated alphas mode can be used on a block-by-block basis. Again the magnitude comparison of alpha\_0 and alpha\_1 is done uniquely on a block-by-block basis.

Direct3D provides services to compress surfaces that are used for texturing 3D models. This section provides information about creating and manipulating the data in a compressed texture surface.

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
<td align="left"><p><a href="opaque-and-1-bit-alpha-textures.md">Opaque and 1-bit alpha textures</a></p></td>
<td align="left"><p>Texture format BC1 is for textures that are opaque or have a single transparent color.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="textures-with-alpha-channels.md">Textures with alpha channels</a></p></td>
<td align="left"><p>There are two ways to encode texture maps that exhibit more complex transparency. In each case, a block that describes the transparency precedes the 64-bit block already described. The transparency is either represented as a 4x4 bitmap with 4 bits per pixel (explicit encoding), or with fewer bits and linear interpolation that is analogous to what is used for color encoding.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="block-compression.md">Block compression</a></p></td>
<td align="left"><p>Block compression is a lossy texture-compression technique for reducing texture size and memory footprint, giving a performance increase. A block-compressed texture can be smaller than a texture with 32-bits per color.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="compressed-texture-formats.md">Compressed texture formats</a></p></td>
<td align="left"><p>This section contains information about the internal organization of compressed texture formats. You do not need these details to use compressed textures, because you can use Direct3D functions for conversion to and from compressed formats. However, this information is useful if you want to operate on compressed surface data directly.</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Textures](textures.md)

 

 




