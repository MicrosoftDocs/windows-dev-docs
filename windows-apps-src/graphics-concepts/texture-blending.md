---
title: Texture blending
description: Direct3D can blend as many as eight textures onto primitives in a single pass.
ms.assetid: 9AD388FA-B2B9-44A9-B73E-EDBD7357ACFB
keywords:
- Texture blending
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Texture blending


Direct3D can blend as many as eight textures onto primitives in a single pass. The use of multiple texture blending can profoundly increase the frame rate of a Direct3D application. An application employs multiple texture blending to apply textures, shadows, specular lighting, diffuse lighting, and other special effects in a single pass.

To use texture blending, your application should first check if the user's hardware supports it.

## <span id="Texture-Stages-and-the-Texture-Blending-Cascade"></span><span id="texture-stages-and-the-texture-blending-cascade"></span><span id="TEXTURE-STAGES-AND-THE-TEXTURE-BLENDING-CASCADE"></span>Texture stages and the texture blending cascade


Direct3D supports single-pass multiple texture blending through the use of texture stages. A texture stage takes two arguments and performs a blending operation on them, passing on the result for further processing or for rasterization. You can visualize a texture stage as shown in the following diagram.

![diagram of a texture stage](images/texstg.png)

As the preceding diagram shows, texture stages blend two arguments by using a specified operator. Common operations include simple modulation or addition of the color or alpha components of the arguments, but more than two dozen operations are supported. The arguments for a stage can be an associated texture, the iterated color or alpha (iterated during Gouraud shading), arbitrary color and alpha, or the result from the previous texture stage.

**Note**   Direct3D distinguishes color blending from alpha blending. Applications set blending operations and arguments for color and alpha individually, and the results of those settings are independent of one another.

 

The combination of arguments and operations used by multiple blending stages define a simple flow-based blending language. The results from one stage flow down to another stage, from that stage to the next, and so on. The concept of results flowing from stage to stage to eventually be rasterized on a polygon is often called the texture blending cascade. The following diagram shows how individual texture stages make up the texture blending cascade.

![diagram of texture stages in the texture blending cascade](images/tcascade.png)

Each stage in a device has a zero-based index. Direct3D allows up to eight blending stages, although you should always check device capabilities to determine how many stages the current hardware supports. The first blending stage is at index 0, the second is at 1, and so on, up to index 7. The system blends stages in increasing index order.

Use only the number of stages you need; the unused blending stages are disabled by default. So, if your application only uses the first two stages, it need only set operations and arguments for stage 0 and 1. The system blends the two stages, and ignores the disabled stages.

If your application varies the number of stages it uses for different situations - such as four stages for some objects, and only two for others - you don't need to explicitly disable all previously used stages. One option is to disable the color operation for the first unused stage, then all stages with a higher index will not be applied. Another option is to disable texture mapping altogether by setting the color operation for the first texture stage (stage 0) to a disabled state.

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
<td align="left"><p><a href="blending-stages.md">Blending stages</a></p></td>
<td align="left"><p>A blending stage is a set of texture operations and their arguments that define how textures are blended.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="multipass-texture-blending.md">Multipass texture blending</a></p></td>
<td align="left"><p>Direct3D applications can achieve numerous special effects by applying various textures to a primitive over the course of multiple rendering passes. The common term for this is <em>multipass texture blending</em>. A typical use for multipass texture blending is to emulate the effects of complex lighting and shading models by applying multiple colors from several different textures. One such application is called <em>light mapping</em>.</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Textures](textures.md)

 

 




