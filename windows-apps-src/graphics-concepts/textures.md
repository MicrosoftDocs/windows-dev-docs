---
title: Textures
description: Textures are a powerful tool in creating realism in computer-generated 3D images. Direct3D supports an extensive texturing feature set, providing developers with easy access to advanced texturing techniques.
ms.assetid: B9E85C9E-B779-4852-9166-6FA2240B7046
keywords:
- Textures
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Textures


Textures are a powerful tool in creating realism in computer-generated 3D images. Direct3D supports an extensive texturing feature set, providing developers with easy access to advanced texturing techniques.

For improved performance, consider using dynamic textures. A dynamic texture can be locked, written to, and unlocked each frame.

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
<td align="left"><p><a href="introduction-to-textures.md">Introduction to textures</a></p></td>
<td align="left"><p>A texture resource is a data structure to store texels, which are the smallest unit of a texture that can be read or written to. When the texture is read by a shader, it can be filtered by texture samplers.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="basic-texturing-concepts.md">Basic texturing concepts</a></p></td>
<td align="left"><p>Early computer-generated 3D images, although generally advanced for their time, tended to have a shiny plastic look. They lacked the types of markings-such as scuffs, cracks, fingerprints, and smudges-that give 3D objects realistic visual complexity. Textures have become popular for enhancing the realism of computer-generated 3D images.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="texture-addressing-modes.md">Texture addressing modes</a></p></td>
<td align="left"><p>Your Direct3D application can assign texture coordinates to any vertex of any primitive. Typically, the u- and v-texture coordinates that you assign to a vertex are in the range of 0.0 to 1.0 inclusive. However, by assigning texture coordinates outside that range, you can create certain special texturing effects.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="texture-filtering.md">Texture filtering</a></p></td>
<td align="left"><p>Texture filtering produces a color for each pixel in the primitive's 2D rendered image when a primitive is rendered by mapping a 3D primitive onto a 2D screen.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="texture-resources.md">Texture resources</a></p></td>
<td align="left"><p>Textures are a type of resource used for rendering.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="texture-wrapping.md">Texture wrapping</a></p></td>
<td align="left"><p>Texture wrapping changes the basic way that Direct3D rasterizes textured polygons using the texture coordinates specified for each vertex. While rasterizing a polygon, the system interpolates between the texture coordinates at each of the polygon's vertices to determine the texels that should be used for every pixel of the polygon.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="texture-blending.md">Texture blending</a></p></td>
<td align="left"><p>Direct3D can blend as many as eight textures onto primitives in a single pass. The use of multiple texture blending can profoundly increase the frame rate of a Direct3D application. An application employs multiple texture blending to apply textures, shadows, specular lighting, diffuse lighting, and other special effects in a single pass.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="light-mapping-with-textures.md">Light mapping with textures</a></p></td>
<td align="left"><p>A light map is a texture or group of textures that contains information about lighting in a 3D scene. Light maps map areas of light and shadow onto primitives. Multipass and multiple texture blending enable your application to render scenes with a more realistic appearance than shading techniques.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="compressed-texture-resources.md">Compressed texture resources</a></p></td>
<td align="left"><p>Texture maps are digitized images drawn on three-dimensional shapes to add visual detail. They are mapped into these shapes during rasterization, and the process can consume large amounts of both the system bus and memory. To reduce the amount of memory consumed by textures, Direct3D supports the compression of texture surfaces. Some Direct3D devices support compressed texture surfaces natively.</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Direct3D Graphics Learning Guide](index.md)

 

 




