---
title: Light properties
description: Light properties describe a light source's type (point, directional, spotlight), attenuation, color, direction, position, and range.
ms.assetid: E832C3FD-9921-41C4-87B8-056E16B61B77
keywords:
- Light properties
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Light properties


Light properties describe a light source's type (point, directional, spotlight), attenuation, color, direction, position, and range. Depending on the type of light being used, a light can have properties for attenuation and range, or for spotlight effects. Not all types of lights use all properties.

The position, range, and attenuation properties define a light's location in world space and how the light it emits behaves over distance.

## <span id="Light_Attenuation"></span><span id="light_attenuation"></span><span id="LIGHT_ATTENUATION"></span>Light Attenuation


Attenuation controls how a light's intensity decreases toward the maximum distance specified by the range property. Three floating point values are sometimes used to represent light attenuation: Attenuation0, Attenuation1, and Attenuation2. These floating-point values ranging from 0.0 through infinity, controlling a light's attenuation. Some applications set the Attenuation1 member to 1.0 and the others to 0.0, resulting in light intensity that changes as 1 / D, where D is the distance from the light source to the vertex. The maximum light intensity is at the source, decreasing to 1 / (Light Range) at the light's range.

Though typically, an application sets Attenuation0 to 0.0, Attenuation1 to a constant value, and Attenuation2 to 0.0, varying light effects can be achieved by changing this. You can combine attenuation values to get more complex attenuation effects. Or, you might set them to values outside the normal range to create even stranger attenuation effects. Negative attenuation values, however, are not allowed. See [Attenuation and spotlight factor](attenuation-and-spotlight-factor.md).

## <span id="Light_Color"></span><span id="light_color"></span><span id="LIGHT_COLOR"></span>Light Color


Lights in Direct3D emit three colors that are used independently in the system's lighting computations: a diffuse color, an ambient color, and a specular color. Each is incorporated by the Direct3D lighting module, interacting with a counterpart from the current material, to produce a final color used in rendering. The diffuse color interacts with the diffuse reflectance property of the current material, the specular color with the material's specular reflectance property, and so on. For specifics about how Direct3D applies these colors, see [Mathematics of lighting](mathematics-of-lighting.md).

In a Direct3D application, typically there are three color values - Diffuse, Ambient, and Specular that defines the color being emitted.

The type of color that applies most heavily to the system's computations is the diffuse color. The most common diffuse color is white (R:1.0 G:1.0 B:1.0), but you can create colors as needed to achieve desired effects. For example, you could use red light for a fireplace, or you could use green light for a traffic signal set to "Go."

Generally, you set the light color components to values between 0.0 and 1.0, inclusive, but this isn't a requirement. For example, you might set all the components to 2.0, creating a light that is "brighter than white." This type of setting can be especially useful when you use attenuation settings other than constant.

Although Direct3D uses RGBA values for lights, the alpha color component is not used.

Usually material colors are used for lighting. However, you can specify that material colors-emissive, ambient, diffuse, and specular-are to be overridden by diffuse or specular vertex colors.

The alpha/transparency value always comes only from the diffuse color's alpha channel.

The fog value always comes only from the specular color's alpha channel.

## <span id="Light_Direction"></span><span id="light_direction"></span><span id="LIGHT_DIRECTION"></span>Light Direction


A light's direction property determines the direction that the light emitted by the object travels, in world space. Direction is used only by directional lights and spotlights, and is described with a vector.

Set the light direction as a vector. Direction vectors are described as distances from a logical origin, regardless of the light's position in a scene. Therefore, a spotlight that points straight into a scene - along the positive z-axis - has a direction vector of &lt;0,0,1&gt; no matter where its position is defined to be. Similarly, you can simulate sunlight shining directly on a scene by using a directional light whose direction is &lt;0,-1,0&gt;. You don't have to create lights that shine along the coordinate axes; you can mix and match values to create lights that shine at more interesting angles.

Although you don't need to normalize a light's direction vector, always be sure that it has magnitude. In other words, don't use a &lt;0,0,0&gt; direction vector.

## <span id="Light_Position"></span><span id="light_position"></span><span id="LIGHT_POSITION"></span>Light Position


Light position is described using a vector structure. The x, y, and z coordinates are assumed to be in world space. Directional lights are the only type of light that don't use the position property.

## <span id="Light_Range"></span><span id="light_range"></span><span id="LIGHT_RANGE"></span>Light Range


A light's range property determines the distance, in world space, at which meshes in a scene no longer receive light emitted by that object. Directional lights don't use the range property.

## <span id="related-topics"></span>Related topics


[Lights and materials](lights-and-materials.md)

 

 




