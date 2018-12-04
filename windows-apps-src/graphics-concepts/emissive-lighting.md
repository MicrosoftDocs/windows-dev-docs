---
title: Emissive lighting
description: Emissive lighting is light that is emitted by an object; for example, a glow.
ms.assetid: 262EB9E2-DF96-401F-93D6-51A7BB60074B
keywords:
- Emissive lighting
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Emissive lighting


*Emissive lighting* is light that is emitted by an object; for example, a glow. Emission makes a rendered object appear to be self-luminous. Emission affects an object's color and can, for example, make a dark material brighter and take on part of the emitted color.

Emissive lighting is described by a single term.

Emissive Lighting = Cₑ

where:

| Parameter | Default value | Type                                                                 | Description     |
|-----------|---------------|----------------------------------------------------------------------|-----------------|
| Cₑ        | (0,0,0,0)     | Red, green, blue, and alpha transparency (all floating-point values) | Emissive color. |

 

The value for Cₑ is either color 1 or color 2. If the vertex color is not provided, the material emissive color is used.

## <span id="Example"></span><span id="example"></span><span id="EXAMPLE"></span>Example


In this example, the object is colored using the scene ambient light and a material ambient color.

According to the equation, the resulting color for the object vertices is the material color.

The following illustration shows the material color, which is green. Emissive light lights all object vertices with the same color. It is not dependent on the vertex normal or the light direction. As a result, the sphere looks like a 2D circle because there is no difference in shading around the surface of the object.

![illustration of a green sphere](images/lighte.jpg)

The following illustration shows how the emissive light blends with the other three types of lights. On the right side of the sphere, there is a blend of the green emissive and the red ambient light. On the left side of the sphere, the green emissive light blends with red ambient and diffuse light producing a red gradient. The specular highlight is white in the center and creates a yellow ring as the specular light value falls off sharply leaving the ambient, diffuse and emissive light values which blend together to make yellow.

![illustration of a green sphere with emissive light](images/lightadse.jpg)

## <span id="related-topics"></span>Related topics


[Mathematics of lighting](mathematics-of-lighting.md)

 

 




