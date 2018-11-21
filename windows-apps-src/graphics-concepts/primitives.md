---
title: Primitives
description: A 3D primitive is a collection of vertices that form a single 3D entity.
ms.assetid: A1FE6F49-B0D4-4665-90E1-40AD98E668B1
keywords:
- Primitives
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Primitives


A 3D *primitive* is a collection of vertices that form a single 3D entity.

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
<td align="left"><p><a href="point-lists.md">Point lists</a></p></td>
<td align="left"><p>A point list is a collection of vertices that are rendered as isolated points. Your application can use point lists in 3D scenes for star fields, or dotted lines on the surface of a polygon.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="line-lists.md">Line lists</a></p></td>
<td align="left"><p>A line list is a list of isolated, straight line segments. Line lists are useful for such tasks as adding sleet or heavy rain to a 3D scene. Applications create a line list by filling an array of vertices.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="line-strips.md">Line strips</a></p></td>
<td align="left"><p>A line strip is a primitive that is composed of connected line segments. Your application can use line strips for creating polygons that are not closed. A closed polygon is a polygon whose last vertex is connected to its first vertex by a line segment.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="triangle-lists.md">Triangle lists</a></p></td>
<td align="left"><p>A triangle list is a list of isolated triangles. The isolated triangles might or might not be near each other. A triangle list must have at least three vertices and the total number of vertices must be divisible by three.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="triangle-strips.md">Triangle strips</a></p></td>
<td align="left"><p>A triangle strip is a series of connected triangles. Because the triangles are connected, the application does not need to repeatedly specify all three vertices for each triangle.</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Coordinate systems and geometry](coordinate-systems-and-geometry.md)

 

 




