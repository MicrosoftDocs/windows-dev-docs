---
title: Triangle lists
description: A triangle list is a list of isolated triangles. The isolated triangles might or might not be near each other. A triangle list must have at least three vertices and the total number of vertices must be divisible by three.
ms.assetid: BC50D532-9E9C-4AAE-B466-9E8C4AD1862A
keywords:
- Triangle lists
ms.date: 02/08/2017
ms.topic: article
ms.localizationpriority: medium
---

# Triangle lists

A triangle list is a list of isolated triangles. The isolated triangles might or might not be near each other. A triangle list must have at least three vertices and the total number of vertices must be divisible by three.

## <span id="Example"></span><span id="example"></span><span id="EXAMPLE"></span>Example


Use triangle lists to create an object that is composed of disjoint pieces. For instance, one way to create a force-field wall in a 3D game is to specify a large list of small, unconnected triangles. Then apply a material and texture that appears to emit light to the triangle list. Each triangle in the wall appears to glow. The scene behind the wall becomes partially visible through the gaps between the triangles, as a player might expect when looking at a force field.

Triangle lists are also useful for creating primitives that have sharp edges and are shaded with Gouraud shading. See [Face and vertex normal vectors](face-and-vertex-normal-vectors.md).

The following illustration depicts a rendered triangle list.

![illustration of a rendered triangle list](images/trilist.png)

The following code shows how to create vertices for this triangle list.

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

The code example below shows how to render this triangle list in Direct3D.

```cpp
//
// It is assumed that d3dDevice is a valid
// pointer to a device interface.
//
d3dDevice->DrawPrimitive( D3DPT_TRIANGLELIST, 0, 2 );
```

## <span id="related-topics"></span>Related topics


[Primitives](primitives.md)

 

 




