---
title: Resource types
description: Different types of resources have a distinct layout (or memory footprint).
ms.assetid: BCDDF227-1837-44DA-ABD4-E39BCFF2B8EF
keywords:
- Resource types
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Resource types


Different types of resources have a distinct layout (or memory footprint). All resources used by the Direct3D pipeline derive from two basic resource types: [buffers](#buffer-resources) and [textures](#texture-resources). A buffer is a collection of raw data (elements); a texture is a collection of texels (texture elements).

There are two ways to fully specify the layout (or memory footprint) of a resource:

<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Item</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p><span id="Typed"></span><span id="typed"></span><span id="TYPED"></span>Typed</p></td>
<td align="left"><p>Fully specify the type when the resource is created.</p></td>
</tr>
<tr class="even">
<td align="left"><p><span id="Typeless"></span><span id="typeless"></span><span id="TYPELESS"></span>Typeless</p></td>
<td align="left"><p>Fully specify the type when the resource is bound to the pipeline.</p></td>
</tr>
</tbody>
</table>

 

## <span id="Buffer_Resources"></span><span id="buffer_resources"></span><span id="BUFFER_RESOURCES"></span><span id="buffer-resources"></span>Buffer resources


A buffer resource is a collection of fully typed data; internally, a buffer contains elements. An element is made up of 1 to 4 components. Examples of element data types include: a packed data value (like R8G8B8A8), a single 8-bit integer, four 32-bit float values. These data types are used to store data, such as a position vector, a normal vector, a texture coordinate in a vertex buffer, an index in an index buffer, or device state.

A buffer is created as an unstructured resource. Because it is unstructured, a buffer cannot contain any mipmap levels, is not filtered when read, and cannot be multisampled.

### <span id="Buffer_Types"></span><span id="buffer_types"></span><span id="BUFFER_TYPES"></span>Buffer types

-   [Vertex buffer](#vertex-buffer)
-   [Index buffer](#index-buffer)
-   [Constant buffer](#shader-constant-buffer)

### <span id="Vertex_Buffer"></span><span id="vertex_buffer"></span><span id="VERTEX_BUFFER"></span><span id="vertex-buffer"></span>Vertex buffer

A buffer is a collection of elements; a vertex buffer contains per-vertex data. The simplest example is a vertex buffer that contains one type of data, such as position data. It can be visualized like the following illustration.

![illustration of a vertex buffer that contains position data](images/d3d10-resources-single-element-vb2.png)

More often, a vertex buffer contains all the data needed to fully specify 3D vertices. An example of this could be a vertex buffer that contains per-vertex position, normal and texture coordinates. This data is usually organized as sets of per-vertex elements, as shown in the following illustration.

![illustration of a vertex buffer that contains position, normal, and texture data](images/d3d10-vertex-buffer-element.png)

This vertex buffer contains per-vertex data for eight vertices; each vertex stores three elements (position, normal, and texture coordinates). The position and normal are each typically specified using three 32-bit floats and the texture coordinates using two 32-bit floats.

To access data from a vertex buffer, you need to know which vertex to access and these other buffer parameters:

-   *Offset* - the number of bytes from the start of the buffer to the data for the first vertex.
-   *BaseVertexLocation* - the number of bytes from the offset to the first vertex used by the appropriate draw call.

Before you create a vertex buffer, you need to define its layout by creating an input-layout object. Once the input-layout object is created, bind it to the Input Assembler (IA) stage.

### <span id="Index_Buffer"></span><span id="index_buffer"></span><span id="INDEX_BUFFER"></span><span id="index-buffer"></span>Index buffer

An index buffer contains a sequential set of 16-bit or 32-bit indices; each index is used to identify a vertex in a vertex buffer. Using an index buffer with one or more vertex buffers to supply data to the IA stage is called indexing. An index buffer can be visualized like the following illustration.

![illustration of an index buffer](images/d3d10-index-buffer.png)

The sequential indices stored in an index buffer are located with the following parameters:

-   *Offset* - the number of bytes from the start of the buffer to the first index.
-   *StartIndexLocation* - the number of bytes from the offset to the first vertex used by the appropriate draw call.
-   *IndexCount* - the number of indices to render.

An index buffer can stitch together multiple line or triangle strips ([primitive topologies](primitive-topologies.md)) by separating each with a strip-cut index. A strip-cut index allows multiple line or triangle strips to be drawn with a single draw call. A strip-cut index is the maximum possible value for the index (0xffff for a 16-bit index, 0xffffffff for a 32-bit index). The strip-cut index resets the winding order in indexed primitives and can be used to remove the need for degenerate triangles that may otherwise be required to maintain proper winding order in a triangle strip. The following illustration shows an example of a strip-cut index.

![illustration of a strip-cut index](images/d3d10-ia-strips-cut-value.png)

### <span id="Shader_Constant_Buffer"></span><span id="shader_constant_buffer"></span><span id="SHADER_CONSTANT_BUFFER"></span><span id="shader-constant-buffer"></span>Constant buffer

Direct3D has a buffer for supplying shader constants called a shader-constant buffer or simply a constant buffer. Conceptually, it looks like a single-element vertex buffer, as shown in the following illustration.

![illustration of a shader-constant buffer](images/d3d10-shader-resource-buffer.png)

Each element stores a 1-to-4 component constant, determined by the format of the data stored.

Constant buffers reduce the bandwidth required to update shader constants by allowing shader constants to be grouped together and committed at the same time rather than making individual calls to commit each constant separately.

A shader continues to read variables in a constant buffer directly by variable name in the same manner variables that are not part of a constant buffer are read.

Each shader stage allows up to 15 shader-constant buffers; each buffer can hold up to 4096 constants.

Use a constant buffer to store the results of the stream-output stage.

See [Shader Constants (DirectX HLSL)](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-constants) for an example of declaring a constant buffer in a shader.

## <span id="Texture_Resources"></span><span id="texture_resources"></span><span id="TEXTURE_RESOURCES"></span><span id="texture-resources"></span>Texture resources


A texture resource is a structured collection of data designed to store texels. Unlike buffers, textures can be filtered by texture samplers as they are read by shader units. The type of texture impacts how the texture is filtered. A texel represents the smallest unit of a texture that can be read or written to by the pipeline. Each texel contains 1 to 4 components, arranged in one of the DXGI formats (see [**DXGI\_FORMAT**](/windows/desktop/api/dxgiformat/ne-dxgiformat-dxgi_format)).

Textures are created as a structured resource so that their size is known. However, each texture may be typed or typeless at resource-creation time, as long as the type is fully specified using a view when the texture is bound to the pipeline.

-   [Texture types](#texture-types)
-   [Subresources](#subresources)
-   [Strong vs. Weak Typing](#typed)

### <span id="Texture_Types"></span><span id="texture_types"></span><span id="TEXTURE_TYPES"></span><span id="texture-types"></span>Texture types

There are several types of textures: 1D, 2D, 3D, each of which can be created with or without mipmaps. Direct3D also supports texture arrays and multisampled textures.

-   [1D Texture](#texture1d-resource)
-   [1D texture array](#texture1d-array-resource)
-   [2D Texture and 2D Texture Array](#texture2d-resource)
-   [3D Texture](#texture3d-resource)

### <span id="Texture1D_Resource"></span><span id="texture1d_resource"></span><span id="TEXTURE1D_RESOURCE"></span><span id="texture1d-resource"></span>1D Texture

A 1D texture in its simplest form contains texture data that can be addressed with a single texture coordinate; it can be visualized as an array of texels, as shown in the following illustration.

![illustration of a 1d texture](images/d3d10-1d-texture.png)

Each texel contains a number of color components depending on the format of the data stored. Adding more complexity, you can create a 1D texture with mipmap levels, as shown in the following illustration.

![illustration of a 1d texture with mipmap levels](images/d3d10-resource-texture1d.png)

A mipmap level is a texture that is a power-of-two smaller than the level above it. The top-most level contains the most detail, each subsequent level is smaller; for a 1D mipmap, the smallest level contains one texel. The differing levels are identified by an index called a LOD (level-of-detail); you can use the LOD to access a smaller texture when rendering geometry that is not as close to the camera.

### <span id="Texture1D_Array_Resource"></span><span id="texture1d_array_resource"></span><span id="TEXTURE1D_ARRAY_RESOURCE"></span><span id="texture1d-array-resource"></span>1D texture array

Direct3D 10 also has a new data structure for an array of textures. An array of 1D textures looks conceptually like the following illustration.

![illustration of a 1d texture array](images/d3d10-resource-texture1darray.png)

This texture array contains three textures. Each of the three textures has a texture width of 5 (which is the number of elements in the first layer). Each texture also contains a 3-layer mipmap.

All texture arrays in Direct3D are a homogeneous array of textures; this means that every texture in a texture array must have the same data format and size (including texture width and number of mipmap levels). You may create texture arrays of different sizes, as long as all the textures in each array match in size.

### <span id="Texture2D_Resource"></span><span id="texture2d_resource"></span><span id="TEXTURE2D_RESOURCE"></span><span id="texture2d-resource"></span>2D Texture and 2D Texture Array

A Texture2D resource contains a 2D grid of texels. Each texel is addressable by a u, v vector. Because it is a texture resource, it may contain mipmap levels, and subresources. A fully populated 2D texture resource looks like the following illustration.

![illustration of a 2d texture resource](images/d3d10-resource-texture2d.png)

This texture resource contains a single 3x5 texture with three mipmap levels.

A Texture2DArray resource is a homogeneous array of 2D textures; that is, each texture has the same data format and dimensions (including mipmap levels). It has a similar layout as the 1D texture array except that the textures now contain 2D data, and therefore looks like the following illustration.

![illustration of an array of 2d texture resources](images/d3d10-resource-texture2darray.png)

This texture array contains three textures; each texture is 3x5 with two mipmap levels.

### <span id="Texture2DArray_Resource_as_a_Texture_Cube"></span><span id="texture2darray_resource_as_a_texture_cube"></span><span id="TEXTURE2DARRAY_RESOURCE_AS_A_TEXTURE_CUBE"></span>Using a Texture2DArray as a Texture Cube

A texture cube is a 2D texture array that contains 6 textures, one for each face of the cube. A fully populated texture cube looks like the following illustration.

![illustration of an array of 2d texture resources that represent a texture cube](images/d3d10-resource-texturecube.png)

A 2D texture array that contains 6 textures may be read from within shaders with the cube map intrinsic functions, after they are bound to the pipeline with a cube-texture view. Texture cubes are addressed from the shader with a 3D vector pointing out from the center of the texture cube.

### <span id="Texture3D_Resource"></span><span id="texture3d_resource"></span><span id="TEXTURE3D_RESOURCE"></span><span id="texture3d-resource"></span>3D Texture

A Texture3D resource (also known as a volume texture) contains a 3D volume of texels. Since it is a texture resource, it may contain mipmap levels. A fully populated 3D texture looks like the following illustration.

![illustration of a 3d texture resource](images/d3d10-resource-texture3d.png)

When a 3D texture mipmap slice is bound as a render target output (with a render-target view), the 3D texture behaves identically to a 2D texture array with n slices. The particular render slice is chosen from the geometry-shader stage.

There is no concept of a 3D texture array; therefore a 3D texture subresource is a single mipmap level.

### <span id="Subresources"></span><span id="subresources"></span><span id="SUBRESOURCES"></span>Subresources

The Direct3D API references entire resources or subsets of resources. To specify portion of resources, Direct3D has coined the term *subresources*, which means a subset of a resource.

A buffer is defined as a single subresource. Textures are a little more complicated as there are several different texture types (1D, 2D, etc.) some of which support mipmap levels and/or texture arrays. Beginning with the simplest case, a 1D texture is defined as a single subresource, as shown in the following illustration.

![illustration of a 1d texture](images/d3d10-1d-texture.png)

This means that the array of texels that make up a 1D texture are contained in a single subresource.

If you expand a 1D texture with three mipmap levels, it can be visualized like this.

![illustration of a 1d texture with mipmap levels](images/d3d10-resource-texture1d.png)

Think of this as a single texture that is made up of three subtextures. Each subtexture is counted as a subresource, so this 1D texture contains 3 subresources. A subtexture (or subresource) can be indexed using the level-of-detail (LOD) for a single texture. When using an array of textures, accessing a particular subtexture requires both the LOD and the particular texture. Alternately, the API combines these two pieces of information into a single zero-based subresource index as shown here.

![illustration of a zero-based subresource index](images/d3d10-resource-texture1darray-sub-indexing.png)

### <span id="Selecting_Subresources"></span><span id="selecting_subresources"></span><span id="SELECTING_SUBRESOURCES"></span>Selecting Subresources

Some APIs access an entire resource, others access a portion of a resource. The APIs that access a portion of a resource generally use a view description to specify the subresources to access.

These figures illustrate the terms used by a view description when accessing an array of textures.

### <span id="Array_Slice"></span><span id="array_slice"></span><span id="ARRAY_SLICE"></span>Array Slice

Given an array of textures, each texture with mipmaps, an array slice (represented by the white rectangle) includes one texture and all of its subtextures, as shown in the following illustration.

![illustration of an array splice](images/d3d10-resource-array-slice.png)

### <span id="Mip_Slice"></span><span id="mip_slice"></span><span id="MIP_SLICE"></span>Mip Slice

A mip slice (represented by the white rectangle) includes one mipmap level for every texture in an array, as shown in the following illustration.

![illustration of a mip splice](images/d3d10-resource-mip-slice.png)

### <span id="Selecting_a_Single_Subresource"></span><span id="selecting_a_single_subresource"></span><span id="SELECTING_A_SINGLE_SUBRESOURCE"></span>Selecting a Single Subresource

You can use these two types of slices to choose a single subresource, as shown in the following illustration.

![illustration of choosing a subresource by using an array slice and a mip splice](images/d3d10-resource-subresources-1.png)

### <span id="Selecting_Multiple_Subresources"></span><span id="selecting_multiple_subresources"></span><span id="SELECTING_MULTIPLE_SUBRESOURCES"></span>Selecting Multiple Subresources

Or you can use these two types of slices with the number of mipmap levels and/or number of textures, to choose multiple subresources.

![illustration of choosing multiple subresources](images/d3d10-resource-subresources-2.png)

Regardless of what texture type you are using, with or without mipmaps, with or without a texture array, there are often helper functions provided to compute the index of a particular subresource.

### <span id="Typed"></span><span id="typed"></span><span id="TYPED"></span>Strong vs. Weak Typing

Creating a fully-typed resource restricts the resource to the format it was created with. This enables the runtime to optimize access, especially if the resource is created with flags indicating that it cannot be mapped by the application. Resources created with a specific type cannot be reinterpreted using the view mechanism.

In a typeless resource, the data type is unknown when the resource is first created. The application must choose from the available typeless formats. You must specify the size of the memory to allocate and whether the runtime will need to generate the subtextures in a mipmap.

However, the exact data format (whether the memory will be interpreted as integers, floating point values, unsigned integers etc.) is not determined until the resource is bound to the pipeline with a view. As the texture format remains flexible until the texture is bound to the pipeline, the resource is referred to as weakly typed storage. Weakly typed storage has the advantage that it can be reused or reinterpreted (in another format) as long as the component bit of the new format matches the bit count of the old format.

A single resource can be bound to multiple pipeline stages as long as each has a unique view, which fully qualifies the formats at each location. For example, a resource created with a typeless format could be used as a FLOAT format and a UINT format at different locations in the pipeline simultaneously.

## <span id="related-topics"></span>Related topics


[Resources](resources.md)