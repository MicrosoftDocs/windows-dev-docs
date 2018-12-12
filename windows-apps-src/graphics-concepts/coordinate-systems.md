---
title: Coordinate systems
description: Typically 3D graphics applications use one of two types of Cartesian coordinate systems left-handed or right-handed. In both coordinate systems, the positive x-axis points to the right, and the positive y-axis points up.
ms.assetid: 138D9B81-146F-4E9F-B742-1EDED8FBF2AE
keywords:
- Coordinate systems
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Coordinate systems


Typically 3D graphics applications use one of two types of Cartesian coordinate systems: left-handed or right-handed. In both coordinate systems, the positive x-axis points to the right, and the positive y-axis points up.

## <span id="Left_and_right_handed_coordinates"></span><span id="left_and_right_handed_coordinates"></span><span id="LEFT_AND_RIGHT_HANDED_COORDINATES"></span>Left and right handed coordinates


You can remember which direction the positive z-axis points by pointing the fingers of either your left or right hand in the positive x-direction and curling them into the positive y-direction. The direction your thumb points, either toward or away from you, is the direction that the positive z-axis points for that coordinate system. The following illustration shows these two coordinate systems.

![illustration of the left-handed and right-handed cartesian coordinate systems](images/leftrght.png)

Direct3D uses a left-handed coordinate system. Although left-handed and right-handed coordinates are the most common systems, there is a variety of other coordinate systems used in 3D software. For example, it is not unusual for 3D modeling applications to use a coordinate system in which the y-axis points toward or away from the viewer, and the z-axis points up.

## <span id="Vertices_and_vectors"></span><span id="vertices_and_vectors"></span><span id="VERTICES_AND_VECTORS"></span>Vertices and vectors


Given the coordinate system, an x, y and z coordinate can define a point in space (a "vertex"), or a 3D direction (a "vector").

A collection of vertices can be used to define lines and shapes. The simplest objects definable by vertices are called [Primitives](primitives.md), and a more complex object defined by a set of primitives is called a "mesh."

The essential operations performed on meshes defined in a 3D coordinate system are translation, rotation, and scaling. You can combine these basic transformations to create a transform matrix. For details, see [Transforms](transforms.md).

When you combine these operations, the results are not commutative; the order in which you multiply matrices is important.

## <span id="Porting_from_a_right-handed_coordinate_system"></span><span id="porting_from_a_right-handed_coordinate_system"></span><span id="PORTING_FROM_A_RIGHT-HANDED_COORDINATE_SYSTEM"></span>Porting from a right-handed coordinate system


If you are porting an application that is based on a right-handed coordinate system, you must make two changes to the data passed to Direct3D:

-   Flip the order of triangle vertices so that the system traverses them clockwise from the front. In other words, if the vertices are v0, v1, v2, pass them to Direct3D as v0, v2, v1.
-   Use the view matrix to scale world space by -1 in the z-direction. To do this, flip the sign of the \_31, \_32, \_33, and \_34 member of the matrix structure that you use for your view matrix.

## <span id="related-topics"></span>Related topics


[Coordinate systems and geometry](coordinate-systems-and-geometry.md)

 

 




