---
title: Camera space transformations
description: Vertices in the camera space are computed by transforming the object vertices with the world view matrix.
ms.assetid: 86EDEB95-8348-4FAA-897F-25251B32B076
keywords:
- Camera space transformations
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Camera space transformations


Vertices in the camera space are computed by transforming the object vertices with the world view matrix.

V = V \* wvMatrix

Vertex normals, in camera space, are computed by transforming the object normals with the inverse transpose of the world view matrix. The world view matrix may or may not be orthogonal.

N = N \* (wvMatrix⁻¹)<sup>T</sup>

The matrix inversion and matrix transpose operate on a 4x4 matrix. The multiply combines the normal with the 3x3 portion of the resulting 4x4 matrix.

If the render state is set to normalize normals, vertex normal vectors are normalized after transformation to camera space as follows:

N = norm(N)

Light position in camera space is computed by transforming the light source position with the view matrix.

Lₚ = Lₚ \* vMatrix

The direction to the light in camera space for a directional light is computed by multiplying the light source direction by the view matrix, normalizing, and negating the result.

L<sub>dir</sub> = -norm(L<sub>dir</sub> \* wvMatrix)

For a point light and a spotlight, the direction to light is computed as follows:

L<sub>dir</sub> = norm(V \* Lₚ), where the parameters are defined in the following table.

| Parameter       | Default value | Type                                          | Description                                               |
|-----------------|---------------|-----------------------------------------------|-----------------------------------------------------------|
| L<sub>dir</sub> | N/A           | 3D vector (x, y, and z floating-point values) | Direction vector from object vertex to the light          |
| V               | N/A           | 3D vector (x, y, and z floating-point values) | Vertex position in camera space                           |
| wvMatrix        | Identity      | 4x4 matrix of floating-point values           | Composite matrix containing the world and view transforms |
| N               | N/A           | 3D vector (x, y, and z floating-point values) | Vertex normal                                             |
| Lₚ              | N/A           | 3D vector (x, y, and z floating-point values) | Light position in camera space                            |
| vMatrix         | Identity      | 4x4 matrix of floating-point values           | Matrix containing the view transform                      |

 

## <span id="related-topics"></span>Related topics


[Mathematics of lighting](mathematics-of-lighting.md)

 

 




