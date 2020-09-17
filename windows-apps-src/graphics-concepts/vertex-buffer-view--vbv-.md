---
title: Vertex buffer view (VBV) and Index buffer view (IBV)
description: Learn about Vertex buffer view (VBV) and Index buffer view (IBV), which hold data and integer indexes for vertices in Direct3D rendering.
ms.assetid: 695115D2-9DA0-41F2-9416-33BFAB698129
keywords:
- Vertex buffer view (VBV)
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Vertex buffer view (VBV) and Index buffer view (IBV)


A vertex buffer holds data for a list of vertices. The data for each vertex can include position, color, normal vector, texture co-ordinates, and so on. An index buffer holds integer indexes (offsets) into a vertex buffer, and is used to define and render an object made up of a subset of the full list of vertices.

The definition of a single vertex is often up to the application to define, such as:

``` syntax
struct CUSTOMVERTEX { 
        FLOAT x, y, z;      // The position
        FLOAT nx, ny, nz;   // The normal
        DWORD color;        // RGBA color
        FLOAT tu, tv;       // The texture coordinates. 
}; 
```

The definition of CUSTOMVERTEX would then be passed to the graphics driver when creating vertex buffers.

## <span id="related-topics"></span>Related topics


[Views](views.md)

 

 




