---
title: Views
description: The term \ 0034;view \ 0034; is used to mean \ 0034;data in the required format \ 0034;. For example, a Constant Buffer View (CBV) would be constant buffer data correctly formatted. This section describes the most common and useful views.
ms.assetid: 0C7FB99F-7391-472F-BA53-576888DFC171
keywords:
- Views
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Views


The term "view" is used to mean "data in the required format". For example, a Constant Buffer View (CBV) would be constant buffer data correctly formatted. This section describes the most common and useful views.

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
<td align="left"><p><a href="constant-buffer-view--cbv-.md">Constant buffer view (CBV)</a></p></td>
<td align="left"><p>Constant buffers contain shader constant data. The value of them is that the data persists, and can be accessed by any GPU shader, until it is necessary to change the data.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="vertex-buffer-view--vbv-.md">Vertex buffer view (VBV) and Index buffer view (IBV)</a></p></td>
<td align="left"><p>A vertex buffer holds data for a list of vertices. The data for each vertex can include position, color, normal vector, texture co-ordinates, and so on. An index buffer holds integer indexes (offsets) into a vertex buffer, and is used to define and render an object made up of a subset of the full list of vertices.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="shader-resource-view--srv-.md">Shader resource view (SRV) and Unordered Access view (UAV)</a></p></td>
<td align="left"><p>Shader resource views typically wrap textures in a format that the shaders can access them. An unordered access view provides similar functionality, but enables the reading and writing to the texture (or other resource) in any order.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="sampler.md">Sampler</a></p></td>
<td align="left"><p>Sampling is the process of reading input values from a texture, or other resource. A &quot;sampler&quot; is any object that reads from resources.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="render-target-view--rtv-.md">Render target view (RTV)</a></p></td>
<td align="left"><p>Render targets enable a scene to be rendered to a temporary intermediate buffer, rather than to the back buffer to be rendered to the screen. This feature enables use of the complex scene that might be rendered, perhaps as a reflection texture or other purpose within the graphics pipeline, or perhaps to add additional pixel shader effects to the scene before rendering.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="depth-stencil-view--dsv-.md">Depth stencil view (DSV)</a></p></td>
<td align="left"><p>A depth stencil view provides the format and buffer for holding depth and stencil information. The depth buffer is used to cull the drawing of pixels that would be invisible to the viewer as they are occluded from view by a closer object. The stencil buffer can be used to cull all drawing outside of a defined shape.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="stream-output-view--sov-.md">Stream output view (SOV)</a></p></td>
<td align="left"><p>Stream output views enable the vertex information that the vertex, tessellation and geometry shaders have come up with to be streamed back out to the application for further use. For example, an object that has been distorted by these shaders could be written back to the application to provide more accurate input to a physics or other engine. In practice though, stream output views are an infrequently used feature of the graphics pipeline.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="rasterizer-ordered-view--rov-.md">Rasterizer ordered view (ROV)</a></p></td>
<td align="left"><p>Rasterizer ordered views enable some of the limitations of a depth buffer to be addressed, in particular having multiple textures containing transparency all applying to the same pixels.</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Direct3D Graphics Learning Guide](index.md)

 

 




