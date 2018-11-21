---
title: Triangle interpolation
description: During rendering, the pipeline interpolates vertex data across each triangle.
ms.assetid: 1A76DD78-CED7-42BE-BA81-B9050CD3AF9B
keywords:
- Triangle interpolation
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Triangle interpolation


During rendering, the pipeline interpolates vertex data across each triangle. Vertex data can be a broad variety of data and can include (but is not limited to): diffuse color, specular color, diffuse alpha (triangle opacity), specular alpha, and a fog factor. For the programmable vertex pipeline, the fog factor is taken from the fog register. For the fixed-function vertex pipeline, the fog factor is taken from specular alpha.

For some vertex data, the interpolation is dependent on the current shading mode, as follows:

| Shading mode | Description                                                                                                                                                                 |
|--------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Flat         | Only the fog factor is interpolated in flat shade mode. For all other interpolated values, the color of the first vertex in the triangle is applied across the entire face. |
| Gouraud      | Linear interpolation is performed between all three vertices.                                                                                                               |

 

The diffuse color and specular color are treated differently, depending on the color model. In the RGB color model, the system uses the red, green, and blue color components in the interpolation.

The alpha component of a color is treated as a separate interpolated value because device drivers can implement transparency in two different ways: by using texture blending or by using stippling.

## <span id="related-topics"></span>Related topics


[Coordinate systems and geometry](coordinate-systems-and-geometry.md)

 

 




