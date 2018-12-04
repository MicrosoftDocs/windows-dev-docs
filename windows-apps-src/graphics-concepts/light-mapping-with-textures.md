---
title: Light mapping with textures
description: A light map is a texture or group of textures that contains information about lighting in a 3D scene.
ms.assetid: 5C7518D2-AC92-4A97-B7AF-4469D213D7BD
keywords:
- Light mapping with textures
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Light mapping with textures


A light map is a texture or group of textures that contains information about lighting in a 3D scene. Light maps map areas of light and shadow onto primitives. Multipass and multiple texture blending enable your application to render scenes with a more realistic appearance than shading techniques.

For an application to realistically render a 3D scene, it must take into account the effect that light sources have on the appearance of the scene. Although techniques such as flat and Gouraud shading are valuable tools in this respect, they can be insufficient for your needs. Direct3D supports multipass and multiple texture blending. These capabilities enable your application to render scenes with a more realistic appearance than scenes rendered with shading techniques alone. By applying one or more light maps, your application can map areas of light and shadow onto its primitives.

A light map is a texture or group of textures that contains information about lighting in a 3D scene. You can store the lighting information in the alpha values of the light map, in the color values, or in both.

If you implement light mapping using multipass texture blending, your application should render the light map onto its primitives on the first pass. It should use a second pass to render the base texture. The exception to this is specular light mapping. In that case, render the base texture first; then add the light map.

Multiple texture blending enables your application to render the light map and the base texture in one pass. If the user's hardware provides for multiple texture blending, your application should take advantage of it when performing light mapping. This significantly improves your application's performance.

Using light maps, a Direct3D application can achieve a variety of lighting effects when it renders primitives. It can map not only monochrome and colored lights in a scene, but it can also add details such as specular highlights and diffuse lighting.

Information on using Direct3D texture blending to perform light mapping is presented in the following topics.

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
<td align="left"><p><a href="monochrome-light-maps.md">Monochrome light maps</a></p></td>
<td align="left"><p>Monochrome light mapping enables older adapters to perform multipass texture blending, when an older 3D accelerator board doesn't support texture blending using the alpha value of the destination pixel.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="color-light-maps.md">Color light maps</a></p></td>
<td align="left"><p>A colored light map uses the RGB data in the light map for its lighting information. An application usually renders 3D scenes more realistically if it uses colored light maps.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="specular-light-maps.md">Specular light maps</a></p></td>
<td align="left"><p>When illuminated by a light source, shiny objects that use highly reflective materials receive specular highlights. Sometimes you can get more accurate highlights by applying specular light maps to primitives, rather than using the specular highlights produced by the lighting module.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="diffuse-light-maps.md">Diffuse light maps</a></p></td>
<td align="left"><p>Matte surfaces have diffuse light reflection. The brightness of diffuse light depends on the distance from the light source and the angle between the surface normal and the light source direction vector. Texture light maps can simulate complex diffuse lighting.</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Textures](textures.md)

 

 




