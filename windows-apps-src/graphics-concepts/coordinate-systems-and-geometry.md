---
title: Coordinate systems and geometry
description: Programming Direct3D applications requires a working familiarity with 3D geometric principles. This section introduces the most important geometric concepts for creating 3D scenes.
ms.assetid: E82EB0A9-0678-496B-96B3-8993BA580099
keywords:
- Coordinate systems and geometry
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Coordinate systems and geometry


Programming Direct3D applications requires a working familiarity with 3D geometric principles. This section introduces the most important geometric concepts for creating 3D scenes.

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
<td align="left"><p><a href="coordinate-systems.md">Coordinate systems</a></p></td>
<td align="left"><p>Typically 3D graphics applications use one of two types of Cartesian coordinate systems: left-handed or right-handed. In both coordinate systems, the positive x-axis points to the right, and the positive y-axis points up.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="primitives.md">Primitives</a></p></td>
<td align="left"><p>A 3D <em>primitive</em> is a collection of vertices that form a single 3D entity.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="face-and-vertex-normal-vectors.md">Face and vertex normal vectors</a></p></td>
<td align="left"><p>Each face in a mesh has a perpendicular unit normal vector. The vector's direction is determined by the order in which the vertices are defined and by whether the coordinate system is right- or left-handed.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="rectangles.md">Rectangles</a></p></td>
<td align="left"><p>Throughout Direct3D and Windows programming, objects on the screen are referred to in terms of bounding rectangles. The sides of a bounding rectangle are always parallel to the sides of the screen, so the rectangle can be described by two points, the upper-left corner and lower-right corner.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="triangle-interpolation.md">Triangle interpolation</a></p></td>
<td align="left"><p>During rendering, the pipeline interpolates vertex data across each triangle. Vertex data can be a broad variety of data and can include (but is not limited to): diffuse color, specular color, diffuse alpha (triangle opacity), specular alpha, and a fog factor.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="vectors--vertices--and-quaternions.md">Vectors, vertices, and quaternions</a></p></td>
<td align="left"><p>Throughout Direct3D, vertices describe position and orientation. Each vertex in a primitive is described by a vector that gives its position, color, texture coordinates, and a normal vector that gives its orientation.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="transforms.md">Transforms</a></p></td>
<td align="left"><p>The part of Direct3D that pushes geometry through the fixed function geometry pipeline is the transform engine. It locates the model and viewer in the world, projects vertices for display on the screen, and clips vertices to the viewport. The transform engine also performs lighting computations to determine diffuse and specular components at each vertex.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="viewports-and-clipping.md">Viewports and clipping</a></p></td>
<td align="left"><p>A <em>viewport</em> is a two-dimensional (2D) rectangle into which a 3D scene is projected. In Direct3D, the rectangle exists as coordinates within a Direct3D surface that the system uses as a rendering target. The projection transformation converts vertices into the coordinate system used for the viewport. A viewport is also used to specify the range of depth values on a render-target surface into which a scene will be rendered (usually 0.0 to 1.0).</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Direct3D Graphics Learning Guide](index.md)

 

 




