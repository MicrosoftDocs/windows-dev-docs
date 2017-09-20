---
title: Transforms
description: The part of Direct3D that pushes geometry through the fixed function geometry pipeline is the transform engine.
ms.assetid: 0DF2A99A-335C-4D14-9720-6D7996DD635A
keywords:
- Transforms
author: michaelfromredmond
ms.author: mithom
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
---

# Transforms


The part of Direct3D that pushes geometry through the fixed function geometry pipeline is the transform engine. It locates the model and viewer in the world, projects vertices for display on the screen, and clips vertices to the viewport. The transform engine also performs lighting computations to determine diffuse and specular components at each vertex.

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
<td align="left"><p>[Transform overview](transform-overview.md)</p></td>
<td align="left"><p>Matrix transformations handle a lot of the low level math of 3D graphics.</p></td>
</tr>
<tr class="even">
<td align="left"><p>[World transform](world-transform.md)</p></td>
<td align="left"><p>A world transform changes coordinates from model space, where vertices are defined relative to a model's local origin, to world space. In world space, vertices are defined relative to an origin common to all the objects in a scene. The world transform places a model into the world.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>[View transform](view-transform.md)</p></td>
<td align="left"><p>A <em>view transform</em> locates the viewer in world space, transforming vertices into camera space. In camera space, the camera, or viewer, is at the origin, looking in the positive z-direction. The view matrix relocates the objects in the world around a camera's position - the origin of camera space - and orientation.</p></td>
</tr>
<tr class="even">
<td align="left"><p>[Projection transform](projection-transform.md)</p></td>
<td align="left"><p>A <em>projection transformation</em> controls the camera's internals, like choosing a lens for a camera. This is the most complicated of the three transformation types.</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Coordinate systems and geometry](coordinate-systems-and-geometry.md)

 

 




