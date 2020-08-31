---
title: Direct3D graphics glossary
description: See a glossary that defines, at a high level, general 3D computer graphics terms that are used in Direct3D game and app development.
ms.assetid: c3850a92-4d05-4f72-bf0f-6a0c79e841eb
keywords:
- Direct3D Graphics Glossary
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Direct3D graphics glossary


Defines Microsoft Direct3D graphics terms. This glossary defines, at a high level, general 3D computer graphics terms that are used in Direct3D game and app development.

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
<td align="left"><p><a href="coordinate-systems-and-geometry.md">Coordinate systems and geometry</a></p></td>
<td align="left"><p>Programming Direct3D applications requires a working familiarity with 3D geometric principles. This section introduces the most important geometric concepts for creating 3D scenes.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="vertex-and-index-buffers.md">Vertex and index buffers</a></p></td>
<td align="left"><p><em>Vertex buffers</em> are memory buffers that contain vertex data; vertices in a vertex buffer are processed to perform transformation, lighting, and clipping. <em>Index buffers</em> are memory buffers that contain index data, which are integer offsets into vertex buffers, used to render primitives.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="devices.md">Devices</a></p></td>
<td align="left"><p>A Direct3D device is the rendering component of Direct3D. A device encapsulates and stores the rendering state, performs transformations and lighting operations, and rasterizes an image to a surface.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="lights-and-materials.md">Lighting</a></p></td>
<td align="left"><p>Lights are used to illuminate objects in a scene. The color of each object vertex is based on the current texture map, vertex colors, and light sources.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="depth-and-stencil-buffers.md">Depth and stencil buffers</a></p></td>
<td align="left"><p>A <em>depth buffer</em> stores depth information to control which areas of polygons are rendered rather than hidden from view. A <em>stencil buffer</em> is used to mask pixels in an image, to produce special effects, including compositing; decaling; dissolves, fades, and swipes; outlines and silhouettes; and two-sided stencil.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="textures.md">Textures</a></p></td>
<td align="left"><p>Textures are a powerful tool in creating realism in computer-generated 3D images. Direct3D supports an extensive texturing feature set, providing developers with easy access to advanced texturing techniques.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="graphics-pipeline.md">Graphics pipeline</a></p></td>
<td align="left"><p>The Direct3D graphics pipeline is designed for generating graphics for realtime gaming applications. Data flows from input to output through each of the configurable or programmable stages.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="views.md">Views</a></p></td>
<td align="left"><p>The term &quot;view&quot; is used to mean &quot;data in the required format&quot;. For example, a Constant Buffer View (CBV) would be constant buffer data correctly formatted. This section describes the most common and useful views.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="compute-pipeline.md">Compute pipeline</a></p></td>
<td align="left"><p>The Direct3D compute pipeline is designed to handle calculations that can be done mostly in parallel with the graphics pipeline.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="resources.md">Resources</a></p></td>
<td align="left"><p>A resource is an area in memory that can be accessed by the Direct3D pipeline. In order for the pipeline to access memory efficiently, data that is provided to the pipeline (such as input geometry, shader resources, and textures) must be stored in a resource. There are two types of resources from which all Direct3D resources derive: a buffer or a texture. Up to 128 resources can be active for each pipeline stage.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="streaming-resources.md">Streaming resources</a></p></td>
<td align="left"><p><em>Streaming resources</em> are large logical resources that use small amounts of physical memory. Instead of passing an entire large resource, small parts of the resource are streamed as needed. Streaming resources were previously called <em>tiled resources</em>.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="appendix.md">Appendices</a></p></td>
<td align="left"><p>These sections provide in-depth technical details.</p></td>
</tr>
</tbody>
</table>

 

 

 
