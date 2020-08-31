---
title: Face and vertex normal vectors
description: Each face in a mesh has a perpendicular unit normal vector. The vector's direction is determined by the order in which the vertices are defined and by whether the coordinate system is right- or left-handed.
ms.assetid: 02333579-9749-4612-B121-23F97898A3E0
keywords:
- Face and vertex normal vectors
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Face and vertex normal vectors


Each face in a mesh has a perpendicular unit normal vector. The vector's direction is determined by the order in which the vertices are defined and by whether the coordinate system is right- or left-handed.

## <span id="Perpendicular_unit_normal_vector_for_a_front_face"></span><span id="perpendicular_unit_normal_vector_for_a_front_face"></span><span id="PERPENDICULAR_UNIT_NORMAL_VECTOR_FOR_A_FRONT_FACE"></span>Perpendicular unit normal vector for a front face


Each face in a mesh has a perpendicular unit normal vector. The vector's direction is determined by the order in which the vertices are defined and by whether the coordinate system is right- or left-handed. The face normal points away from the front side of the face. In Direct3D, only the front of a face is visible. A front face is one in which vertices are defined in clockwise order.

The following illustration shows a normal vector for a front face:

![a normal vector for a front face](images/nrmlvect.png)

## <span id="Culling_back_faces"></span><span id="culling_back_faces"></span><span id="CULLING_BACK_FACES"></span>Culling back faces


Any face that is not a front face is a back face. Direct3D does not always render back faces; back faces are said to be culled. Back face culling means eliminating back faces from rendering. You can change the culling mode to render back faces if you want. See [Culling State](/windows/desktop/direct3d9/culling-state) for more information.

## <span id="Vertex_unit_normals"></span><span id="vertex_unit_normals"></span><span id="VERTEX_UNIT_NORMALS"></span>Vertex unit normals


Direct3D uses the vertex unit normals for Gouraud shading, lighting, and texturing effects.

The following illustration shows vertex normals:

![vertex normals](images/vertnrml.png)

When applying Gouraud shading to a polygon, Direct3D uses the vertex normals to calculate the angle between the light source and the surface. It calculates the color and intensity values for the vertices and interpolates them for every point across all the primitive's surfaces. Direct3D calculates the light intensity value by using the angle. The greater the angle, the less light is shining on the surface.

## <span id="Flat_surfaces"></span><span id="flat_surfaces"></span><span id="FLAT_SURFACES"></span>Flat surfaces


If you are creating an object that is flat, set the vertex normals to point perpendicular to the surface.

The following illustration shows a flat surface composed of two triangles with vertex normals:

![flat surface composed of two triangles with vertex normals](images/flatvert.png)

## <span id="Smooth_shading_on_a_non-flat_object"></span><span id="smooth_shading_on_a_non-flat_object"></span><span id="SMOOTH_SHADING_ON_A_NON-FLAT_OBJECT"></span>Smooth shading on a non-flat object


Rather than flat object, it is more likely that your object is made up of triangle strips and the triangles are not coplanar. One simple way to achieve smooth shading across all the triangles in the strip is to first calculate the surface normal vector for each polygonal face with which the vertex is associated. The vertex normal can be set to make an equal angle with each surface normal. However, this method might not be efficient enough for complex primitives.

This method is illustrated by the following diagram, which shows two surfaces, S1 and S2 seen edge-on from above. The normal vectors for S1 and S2 are shown in blue. The vertex normal vector is shown in red. The angle that the vertex normal vector makes with the surface normal of S1 is the same as the angle between the vertex normal and the surface normal of S2. When these two surfaces are lit and shaded with Gouraud shading, the result is a smoothly shaded, smoothly rounded edge between them.

The following illustration shows two surfaces (S1 and S2) and their normal vectors and vertex normal vector:

![two surfaces (s1 and s2) and their normal vectors and vertex normal vector](images/gvert.png)

If the vertex normal leans toward one of the faces with which it is associated, it causes the light intensity to increase or decrease for points on that surface, depending on the angle it makes with the light source. The following diagram shows an example. These surfaces are seen edge-on. The vertex normal leans toward S1, causing it to have a smaller angle with the light source than if the vertex normal had equal angles with the surface normals.

The following illustration shows two surfaces (S1 and S2) with a vertex normal vector that leans toward one face:

![two surfaces (s1 and s2) with a vertex normal vector that leans toward one face](images/gvert2.png)

## <span id="Sharp_edges"></span><span id="sharp_edges"></span><span id="SHARP_EDGES"></span>Sharp edges


You can use Gouraud shading to display some objects in a 3D scene with sharp edges. To do so, duplicate the vertex normal vectors at any intersection of faces where a sharp edge is required.

The following illustration shows duplicated vertex normal vectors at sharp edges:

![duplicated vertex normal vectors at sharp edges](images/shade1.png)

If you use the DrawPrimitive methods to render your scene, define the object with sharp edges as a triangle list, rather than a triangle strip. When you define an object as a triangle strip, Direct3D treats it as a single polygon composed of multiple triangular faces. Gouraud shading is applied both across each face of the polygon and between adjacent faces.

The result is an object that is smoothly shaded from face to face. Because a triangle list is a polygon composed of a series of disjoint triangular faces, Direct3D applies Gouraud shading across each face of the polygon. However, it is not applied from face to face. If two or more triangles of a triangle list are adjacent, they appear to have a sharp edge between them.

Another alternative is to change to flat shading when rendering objects with sharp edges. This is computationally the most efficient method, but it may result in objects in the scene that are not rendered as realistically as the objects that are Gouraud-shaded.

## <span id="related-topics"></span>Related topics


[Coordinate systems and geometry](coordinate-systems-and-geometry.md)

 

 