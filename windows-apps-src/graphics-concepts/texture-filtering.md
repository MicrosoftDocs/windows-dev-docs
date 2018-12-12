---
title: Texture filtering
description: Texture filtering produces a color for each pixel in the primitive's 2D rendered image when a primitive is rendered by mapping a 3D primitive onto a 2D screen.
ms.assetid: 1CCF4138-5D48-4B07-9490-996844F994D8
keywords:
- Texture filtering
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Texture filtering


Texture filtering produces a color for each pixel in the primitive's 2D rendered image when a primitive is rendered by mapping a 3D primitive onto a 2D screen.

When Direct3D renders a primitive, it maps the 3D primitive onto a 2D screen. If the primitive has a texture, Direct3D must use that texture to produce a color for each pixel in the primitive's 2D rendered image. For every pixel in the primitive's on-screen image, it must obtain a color value from the texture. This process is called *texture filtering*.

When a texture filter operation is performed, the texture being used is typically also being magnified or minified. In other words, it is being mapped into a primitive image that is larger or smaller than itself. Magnification of a texture can result in many pixels being mapped to one texel. The result can be a chunky appearance. Minification of a texture often means that a single pixel is mapped to many texels. The resulting image can be blurry or aliased. To resolve these problems, some blending of the texel colors must be performed to arrive at a color for the pixel.

Direct3D simplifies the complex process of texture filtering. It provides you with three types of texture filtering - linear filtering, anisotropic filtering, and mipmap filtering. If you select no texture filtering, Direct3D uses a technique called nearest-point sampling.

Each type of texture filtering has advantages and disadvantages. For instance, linear texture filtering can produce jagged edges or a chunky appearance in the final image. However, it is a computationally low-overhead method of texture filtering. Filtering with mipmaps usually produces the best results, especially when combined with anisotropic filtering. However, it requires the most memory of the techniques that Direct3D supports.

## <span id="Types-of-texture-filtering"></span><span id="types-of-texture-filtering"></span><span id="TYPES-OF-TEXTURE-FILTERING"></span>Types of texture filtering


Direct3D supports the following texture filtering approaches.

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
<td align="left"><p><a href="nearest-point-sampling.md">Nearest-point sampling</a></p></td>
<td align="left"><p>Applications are not required to use texture filtering. Direct3D can be set so that it computes the texel address, which often does not evaluate to integers, and copies the color of the texel with the closest integer address. This process is called <em>nearest-point sampling</em>.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="bilinear-texture-filtering.md">Bilinear texture filtering</a></p></td>
<td align="left"><p><em>Bilinear filtering</em> calculates the weighted average of the 4 texels closest to the sampling point. This filtering approach is more accurate and common than nearest-point filtering. This approach is efficient because it is implemented in modern graphics hardware.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="anisotropic-texture-filtering.md">Anisotropic texture filtering</a></p></td>
<td align="left"><p><em>Anisotropy</em> is the distortion visible in the texels of a 3D object whose surface is oriented at an angle with respect to the plane of the screen. When a pixel from an anisotropic primitive is mapped to texels, its shape is distorted.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="texture-filtering-with-mipmaps.md">Texture filtering with mipmaps</a></p></td>
<td align="left"><p>A <em>mipmap</em> is a sequence of textures, each of which is a progressively lower resolution representation of the same image. The height and width of each image, or level, in the mipmap is a power-of-two smaller than the previous level.</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Textures](textures.md)

 

 




