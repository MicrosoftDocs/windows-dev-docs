---
title: Mathematics of lighting
description: The Direct3D Light Model covers ambient, diffuse, specular, and emissive lighting. This is enough flexibility to solve a wide range of lighting situations. The total amount of light in a scene is called the global illumination.
ms.assetid: D0521F56-050D-4EDF-9BD1-34748E94B873
keywords:
- Mathematics of lighting
ms.date: 02/08/2017
ms.topic: article
ms.localizationpriority: medium
---

# Mathematics of lighting

The Direct3D Light Model covers ambient, diffuse, specular, and emissive lighting. This is enough flexibility to solve a wide range of lighting situations. The total amount of light in a scene is called the *global illumination*.

The global illumination is computed as follows:

```cpp
global_illumination = ambient_lighting + diffuse_lighting + specular_lighting + emissive_lighting;
```

[Ambient lighting](ambient-lighting.md) is constant lighting. Ambient lighting is constant in all directions and it colors all pixels of an object the same. It is fast to calculate but leaves objects looking flat and unrealistic.

[Diffuse lighting](diffuse-lighting.md) depends on both the light direction and the object surface normal. Diffuse lighting varies across the surface of an object as a result of the changing light direction and the changing surface numeral vector. It takes longer to calculate diffuse lighting because it changes for each object vertex, however the benefit of using it is that it shades objects and gives them three-dimensional (3D) depth.

[Specular lighting](specular-lighting.md) identifies the bright specular highlights that occur when light hits an object surface and reflects back toward the camera. Specular lighting is more intense than diffuse light and falls off more rapidly across the object surface. It takes longer to calculate specular lighting than diffuse lighting, however the benefit of using it is that it adds significant detail to a surface.

[Emissive lighting](emissive-lighting.md) is light that is emitted by an object; for example, a glow. Emission makes a rendered object appear to be self-luminous. Emission affects an object's color and can, for example, make a dark material brighter and take on part of the emitted color.

Realistic lighting can be accomplished by applying each of these types of lighting to a 3D scene. The values calculated for ambient, emissive, and diffuse components are output as the diffuse vertex color; the value for the specular lighting component is output as the specular vertex color. Ambient, diffuse, and specular light values can be affected by a given light's attenuation and spotlight factor. See [Attenuation and spotlight factor](attenuation-and-spotlight-factor.md).

To achieve a more realistic lighting effect, you add more lights; however, the scene takes longer to render. To achieve all the effects a designer wants, some games use more CPU power than is commonly available. In this case, it is typical to reduce the number of lighting calculations to a minimum by using lighting maps and environment maps to add lighting to a scene while using texture maps.

Lighting is computed in the camera space. See [Camera space transformations](camera-space-transformations.md). Optimized lighting can be computed in model space, when special conditions exist: normal vectors are already normalized, vertex blending is not needed, and transformation matrices are orthogonal.

All lighting computations are made in model space by transforming the light source's position and direction, along with the camera position, to model space using the inverse of the world matrix. As a result, if the world or view matrices introduce non-uniform scaling, the resultant lighting might be inaccurate.

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
<td align="left"><p><a href="ambient-lighting.md">Ambient lighting</a></p></td>
<td align="left"><p>Ambient lighting provides constant lighting for a scene. It lights all object vertices the same because it is not dependent on any other lighting factors such as vertex normals, light direction, light position, range, or attenuation. Ambient lighting is constant in all directions and it colors all pixels of an object the same. It is fast to calculate but leaves objects looking flat and unrealistic.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="diffuse-lighting.md">Diffuse lighting</a></p></td>
<td align="left"><p><em>Diffuse lighting</em> depends on both the light direction and the object surface normal. Diffuse lighting varies across the surface of an object as a result of the changing light direction and the changing surface numeral vector. It takes longer to calculate diffuse lighting because it changes for each object vertex, however the benefit of using it is that it shades objects and gives them three-dimensional (3D) depth.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="specular-lighting.md">Specular lighting</a></p></td>
<td align="left"><p><em>Specular lighting</em> identifies the bright specular highlights that occur when light hits an object surface and reflects back toward the camera. Specular lighting is more intense than diffuse light and falls off more rapidly across the object surface. It takes longer to calculate specular lighting than diffuse lighting, however the benefit of using it is that it adds significant detail to a surface.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="emissive-lighting.md">Emissive lighting</a></p></td>
<td align="left"><p><em>Emissive lighting</em> is light that is emitted by an object; for example, a glow. Emission makes a rendered object appear to be self-luminous. Emission affects an object's color and can, for example, make a dark material brighter and take on part of the emitted color.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="camera-space-transformations.md">Camera space transformations</a></p></td>
<td align="left"><p>Vertices in the camera space are computed by transforming the object vertices with the world view matrix.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="attenuation-and-spotlight-factor.md">Attenuation and spotlight factor</a></p></td>
<td align="left"><p>The diffuse and specular lighting components of the global illumination equation contain terms that describe light attenuation and the spotlight cone.</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Lights and materials](lights-and-materials.md)

 

 




