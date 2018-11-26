---
title: Vertex and index buffers
description: Vertex buffers are memory buffers that contain vertex data; vertices in a vertex buffer are processed to perform transformation, lighting, and clipping.
ms.assetid: 8A39CD23-85FB-4424-9AC3-37919704CD68
keywords:
- Vertex and index buffers
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Vertex and index buffers


*Vertex buffers* are memory buffers that contain vertex data; vertices in a vertex buffer are processed to perform transformation, lighting, and clipping. *Index buffers* are memory buffers that contain index data, which are integer offsets into vertex buffers, used to render primitives.

Vertex buffers can contain any vertex type - transformed or untransformed, lit or unlit - that can be rendered. You can process the vertices in a vertex buffer to perform operations such as transformation, lighting, or generating clipping flags. Transformation is always performed.

The flexibility of vertex buffers make them ideal staging points for reusing transformed geometry. You could create a single vertex buffer, transform, light, and clip the vertices in it, and render the model in the scene as many times as needed without re-transforming it, even with interleaved render state changes. This is useful when rendering models that use multiple textures: the geometry is transformed only once, and then portions of it can be rendered as needed, interleaved with the required texture changes. Render state changes made after vertices are processed take effect the next time the vertices are processed.

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
<td align="left"><p><a href="introduction-to-buffers.md">Introduction to buffers</a></p></td>
<td align="left"><p>A buffer resource is a collection of fully typed data, grouped into elements. Buffers store data, such as texture coordinates in a <em>vertex buffer</em>, indexes in an <em>index buffer</em>, shader constants data in a <em>constant buffer</em>, position vectors, normal vectors, or device state.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="index-buffers.md">Index buffers</a></p></td>
<td align="left"><p><em>Index buffers</em> are memory buffers that contain index data, which are integer offsets into vertex buffers, used to render primitives.</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Direct3D Graphics Learning Guide](index.md)

 

 




