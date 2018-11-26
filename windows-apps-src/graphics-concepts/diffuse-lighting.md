---
title: Diffuse lighting
description: Diffuse lighting depends on both the light direction and the object surface normal.
ms.assetid: 8AF78742-76B1-4BBB-86E3-94AE6F48B847
keywords:
- Diffuse lighting
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Diffuse lighting


*Diffuse lighting* depends on both the light direction and the object surface normal. Diffuse lighting varies across the surface of an object as a result of the changing light direction and the changing surface numeral vector. It takes longer to calculate diffuse lighting because it changes for each object vertex, however the benefit of using it is that it shades objects and gives them three-dimensional (3D) depth.

After adjusting the light intensity for any attenuation effects, the lighting engine computes how much of the remaining light reflects from a vertex, given the angle of the vertex normal and the direction of the incident light. The lighting engine skips to this step for directional lights because they do not attenuate over distance. The system considers two reflection types, diffuse and specular, and uses a different formula to determine how much light is reflected for each.

After calculating the amounts of light reflected, Direct3D applies these new values to the diffuse and specular reflectance properties of the current material. The resulting color values are the diffuse and specular components that the rasterizer uses to produce Gouraud shading and specular highlighting.

Diffuse lighting is described by the following equation.

Diffuse Lighting = sum\[C<sub>d</sub>\*L<sub>d</sub>\*(N<sup>.</sup>L<sub>dir</sub>)\*Atten\*Spot\]

| Parameter       | Default value | Type          | Description                                                                                      |
|-----------------|---------------|---------------|--------------------------------------------------------------------------------------------------|
| sum             | N/A           | N/A           | Summation of each light's diffuse component.                                                     |
| C<sub>d</sub>   | (0,0,0,0)     | D3DCOLORVALUE | Diffuse color.                                                                                   |
| L<sub>d</sub>   | (0,0,0,0)     | D3DCOLORVALUE | Light diffuse color.                                                                             |
| N               | N/A           | D3DVECTOR     | Vertex normal                                                                                    |
| L<sub>dir</sub> | N/A           | D3DVECTOR     | Direction vector from object vertex to the light.                                                |
| Atten           | N/A           | FLOAT         | Light attenuation. See [Attenuation and spotlight factor](attenuation-and-spotlight-factor.md). |
| Spot            | N/A           | FLOAT         | Spotlight factor. See [Attenuation and spotlight factor](attenuation-and-spotlight-factor.md).  |

 

To calculate the attenuation (Atten) or the spotlight characteristics (Spot), see [Attenuation and spotlight factor](attenuation-and-spotlight-factor.md).

Diffuse components are clamped to be from 0 to 255, after all lights are processed and interpolated separately. The resulting diffuse lighting value is a combination of the ambient, diffuse and emissive light values.

## <span id="Example"></span><span id="example"></span><span id="EXAMPLE"></span>Example


In this example, the object is colored using the light diffuse color and a material diffuse color.

According to the equation, the resulting color for the object vertices is a combination of the material color and the light color.

The following two illustrations show the material color, which is gray, and the light color, which is bright red.

![illustration of a gray sphere](images/amb1.jpg)![illustration of a red sphere](images/lightred.jpg)

The resulting scene is shown in the following illustration. The only object in the scene is a sphere. The diffuse lighting calculation takes the material and light diffuse color and modifies it by the angle between the light direction and the vertex normal using the dot product. As a result, the backside of the sphere gets darker as the surface of the sphere curves away from the light.

![illustration of a sphere with diffuse lighting](images/lightd.jpg)

Combining the diffuse lighting with the ambient lighting from the previous example shades the entire surface of the object. The ambient light shades the entire surface and the diffuse light helps reveal the object's 3D shape, as shown in the following illustration.

![illustration of a sphere with diffuse lighting and ambient lighting](images/lightad.jpg)

Diffuse lighting is more intensive to calculate than ambient lighting. Because it depends on the vertex normals and light direction, you can see the objects geometry in 3D space, which produces a more realistic lighting than ambient lighting. You can use specular highlights to achieve a more realistic look.

## <span id="related-topics"></span>Related topics


[Mathematics of lighting](mathematics-of-lighting.md)

 

 




