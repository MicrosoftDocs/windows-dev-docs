---
title: Point lists
description: A point list is a collection of vertices that are rendered as isolated points. Your application can use point lists in 3D scenes for star fields, or dotted lines on the surface of a polygon.
ms.assetid: 332954AE-019F-4A91-B773-E3A7C92F3297
keywords:
- Point lists
ms.date: 02/08/2017
ms.topic: article
ms.localizationpriority: medium
---

# Point lists

A point list is a collection of vertices that are rendered as isolated points. Your application can use point lists in 3D scenes for star fields, or dotted lines on the surface of a polygon.

## <span id="Example"></span><span id="example"></span><span id="EXAMPLE"></span>Example


The following illustration depicts a rendered point list.

![illustration of a point list](images/pointlst.png)

Your application can apply materials and textures to a point list. The colors in the material or texture appear only at the points drawn, and not anywhere between the points.

The following code shows how to create vertices for this point list.

```cpp
struct CUSTOMVERTEX
{
    float x,y,z;
};

CUSTOMVERTEX Vertices[] = 
{
    {-5.0, -5.0, 0.0},
    { 0.0,  5.0, 0.0},
    { 5.0, -5.0, 0.0},
    {10.0,  5.0, 0.0},
    {15.0, -5.0, 0.0},
    {20.0,  5.0, 0.0}
};
```

The code example below shows how to render this point list in Direct3D.

```cpp
//
// It is assumed that d3dDevice is a valid
// pointer to an IDirect3DDevice interface.
//
d3dDevice->DrawPrimitive( D3DPT_POINTLIST, 0, 6 );
```

## <span id="related-topics"></span>Related topics


[Primitives](primitives.md)

 

 




