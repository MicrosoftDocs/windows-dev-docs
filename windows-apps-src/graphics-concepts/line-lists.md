---
title: Line lists
description: A line list is a list of isolated, straight line segments. Line lists are useful for such tasks as adding sleet or heavy rain to a 3D scene. Applications create a line list by filling an array of vertices.
ms.assetid: 42BF32A1-3535-42A3-82C5-3945CB309F2C
keywords:
- Line lists
ms.date: 02/08/2017
ms.topic: article
ms.localizationpriority: medium
---

# Line lists

A line list is a list of isolated, straight line segments. Line lists are useful for such tasks as adding sleet or heavy rain to a 3D scene. Applications create a line list by filling an array of vertices. Note that the number of vertices in a line list must be an even number greater than or equal to two.

-   [Example](#example)
-   [Related topics](#related-topics)

## <span id="Example"></span><span id="example"></span><span id="EXAMPLE"></span>Example


The following illustration shows a rendered line list.

![illustration of a line list](images/linelst.png)

You can apply materials and textures to a line list. The colors in the material or texture appear only along the lines drawn, not at any point in between the lines.

The following code shows how to create vertices for this line list.

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

The code example below shows how to render a line list in Direct3D.

```cpp
//
// It is assumed that d3dDevice is a valid
// pointer to an IDirect3DDevice interface.
//
d3dDevice->DrawPrimitive( D3DPT_LINELIST, 0, 3 );
```

## <span id="related-topics"></span>Related topics


[Primitives](primitives.md)

 

 




