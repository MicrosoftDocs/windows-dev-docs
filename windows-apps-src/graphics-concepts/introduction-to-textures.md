---
title: Introduction to textures
description: A texture resource is a data structure to store texels, which are the smallest unit of a texture that can be read or written to. When the texture is read by a shader, it can be filtered by texture samplers.
ms.assetid: 6F3C76A8-F762-4296-AE02-BFBD6476A5A8
keywords:
- Introduction to textures
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Introduction to textures


A texture resource is a data structure to store texels, which are the smallest unit of a texture that can be read or written to. When the texture is read by a shader, it can be filtered by texture samplers.

A texture resource is a structured collection of data designed to store texels. A texel represents the smallest unit of a texture that can be read or written to by the pipeline. Unlike buffers, textures can be filtered by texture samplers as they are read by shader units. The type of texture impacts how the texture is filtered. Each texel contains 1 to 4 components, arranged in one of the DXGI formats defined by the DXGI\_FORMAT enumeration.

Textures are created as a structured resource with a known size. However, each texture may be typed or typeless when the resource is created as long as the type is fully specified using a view when the texture is bound to the pipeline.

## <span id="Texture_Types"></span><span id="texture_types"></span><span id="TEXTURE_TYPES"></span>Texture Types


Direct3D supports several floating-point representations. All floating-point computations operate under a defined subset of the IEEE 754 32-bit single precision floating-point rules.

There are several types of textures: 1D, 2D, 3D, each of which can be created with or without mipmaps. Direct3D also supports texture arrays and multisampled textures.

-   [1D Textures](#texture1d-resource)
-   [1D Texture Arrays](#texture1d-array-resource)
-   [2D Textures and 2D Texture Arrays](#texture2d-resource)
-   [3D Textures](#texture3d-resource)

### <span id="Texture1D_Resource"></span><span id="texture1d_resource"></span><span id="TEXTURE1D_RESOURCE"></span><span id="texture1d-resource"></span>1D Textures

A 1D texture in its simplest form contains texture data that can be addressed with a single texture coordinate; it can be visualized as an array of texels, as shown in the following illustration.

The following illustration shows a 1D texture:

![a 1d texture](images/d3d10-1d-texture.png)

Each texel contains a number of color components depending on the format of the data stored. Adding more complexity, you can create a 1D texture with mipmap levels, as shown in the following illustration.

![a 1d texture with mipmap levels](images/d3d10-resource-texture1d.png)

A mipmap level is a texture that is a power-of-two smaller than the level above it. The topmost level contains the most detail, each subsequent level is smaller. For a 1D mipmap, the smallest level contains one texel. Furthermore, MIP levels always reduce down to 1:1.

When mipmaps are generated for an odd sized texture, the next lower level is always even size (except when the lowest level reaches 1). For example, the diagram illustrates a 5x1 texture whose next lowest level is a 2x1 texture, whose next (and last) mip level is a 1x1 sized texture. The levels are identified by an index called a LOD (level-of-detail) which is used to access the smaller texture when rendering geometry that is not as close to the camera.

### <span id="Texture1D_Array_Resource"></span><span id="texture1d_array_resource"></span><span id="TEXTURE1D_ARRAY_RESOURCE"></span><span id="texture1d-array-resource"></span>1D Texture Arrays

Direct3D also supports arrays of textures. An array of 1D textures looks conceptually like the following illustration.

![an array of 1d textures](images/d3d10-resource-texture1darray.png)

This texture array contains three textures. Each of the three textures has a texture width of 5 (which is the number of elements in the first layer). Each texture also contains a 3 layer mipmap.

All texture arrays in Direct3D are a homogeneous array of textures; this means that every texture in a texture array must have the same data format and size (including texture width and number of mipmap levels). You may create texture arrays of different sizes, as long as all the textures in each array match in size.

### <span id="Texture2D_Resource"></span><span id="texture2d_resource"></span><span id="TEXTURE2D_RESOURCE"></span><span id="texture2d-resource"></span>2D Textures and 2D Texture Arrays

A Texture2D resource contains a 2D grid of texels. Each texel is addressable by a u, v vector. Since it is a texture resource, it may contain mipmap levels, and subresources. A fully populated 2D texture resource looks like the following illustration.

![a 2d texture resource](images/d3d10-resource-texture2d.png)

This texture resource contains a single 3x5 texture with three mipmap levels.

A 2D texture array resource is a homogeneous array of 2D textures; that is, each texture has the same data format and dimensions (including mipmap levels). It has a similar layout as the 1D texture array except that the textures now contain 2D data, as shown in the following illustration.

![an array of 2d textures](images/d3d10-resource-texture2darray.png)

This texture array contains three textures; each texture is 3x5 with two mipmap levels.

### <span id="Texture2DArray_Resource_as_a_Texture_Cube"></span><span id="texture2darray_resource_as_a_texture_cube"></span><span id="TEXTURE2DARRAY_RESOURCE_AS_A_TEXTURE_CUBE"></span>Using a 2D Texture Array as a Texture Cube

A texture cube is a 2D texture array that contains 6 textures, one for each face of the cube. A fully populated texture cube looks like the following illustration.

![an array of 2d textures that represent a texture cube](images/d3d10-resource-texturecube.png)

A 2D texture array that contains 6 textures may be read from within shaders with the cube map intrinsic functions, after they are bound to the pipeline with a cube-texture view. Texture cubes are addressed from the shader with a 3D vector pointing out from the center of the texture cube.

### <span id="Texture3D_Resource"></span><span id="texture3d_resource"></span><span id="TEXTURE3D_RESOURCE"></span><span id="texture3d-resource"></span>3D Textures

A 3D texture resource (also known as a volume texture) contains a 3D volume of texels. Because it is a texture resource, it may contain mipmap levels. A fully populated 3D texture looks like the following illustration.

![a 3d texture resource](images/d3d10-resource-texture3d.png)

When a 3D texture mipmap slice is bound as a render target output (with a render-target view), the 3D texture behaves identically to a 2D texture array with n slices. The particular render slice is chosen from the geometry-shader stage.

There is no concept of a 3D texture array; therefore a 3D texture subresource is a single mipmap level.

Coordinate systems for Direct3D are defined for pixels and texels.

## <span id="Pixel"></span><span id="pixel"></span><span id="PIXEL"></span>Pixel Coordinate System


The pixel coordinate system in Direct3D defines the origin of a render target at the upper-left corner, as shown in the following diagram. Pixel centers are offset by (0.5f,0.5f) from integer locations.

![diagram of the pixel coordinate system in direct3d 10](images/d3d10-coordspix10.png)

## <span id="Texel"></span><span id="texel"></span><span id="TEXEL"></span>Texel Coordinate System


The texel coordinate system has its origin at the top-left corner of the texture, as shown in the following diagram. This makes rendering screen-aligned textures trivial, as the pixel coordinate system is aligned with the texel coordinate system.

![diagram of the texel coordinate system](images/d3d10-coordstex10.png)

Texture coordinates are represented with either a normalized or a scaled number; each texture coordinate is mapped to a specific texel as follows:

For a normalized coordinate:

-   Point sampling: Texel \# = floor(U \* Width)
-   Linear sampling: Left Texel \# = floor(U \* Width), Right Texel \# = Left Texel \# + 1

For a scaled coordinate:

-   Point sampling: Texel \# = floor(U)
-   Linear sampling: Left Texel \# = floor(U - 0.5), Right Texel \# = Left Texel \# + 1

Where the width, is the width of the texture (in texels).

Texture address wrapping occurs after the texel location is computed.

## <span id="related-topics"></span>Related topics


[Textures](textures.md)
