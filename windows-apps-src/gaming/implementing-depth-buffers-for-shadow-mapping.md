---
title: Walkthrough-- Direct3D 11 shadow volume depth buffers
description: This walkthrough demonstrates how to render shadow volumes using depth maps, using Direct3D 11 on devices of all Direct3D feature levels.
ms.assetid: d15e6501-1a1d-d99c-d1d8-ad79b849db90
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, directx, shadow volumes, depth buffers, directx 11
ms.localizationpriority: medium
---
# Walkthrough: Implement shadow volumes using depth buffers in Direct3D 11



This walkthrough demonstrates how to render shadow volumes using depth maps, using Direct3D 11 on devices of all Direct3D feature levels.
## 
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
<td align="left"><p><a href="create-depth-buffer-resource--view--and-sampler-state.md">Create depth buffer device resources</a></p></td>
<td align="left"><p>Learn how to create the Direct3D device resources necessary to support depth testing for shadow volumes.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="render-the-shadow-map-to-the-depth-buffer.md">Render the shadow map to the depth buffer</a></p></td>
<td align="left"><p>Render from the point of view of the light to create a two-dimensional depth map representing the shadow volume.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="render-the-scene-with-depth-testing.md">Render the scene with depth testing</a></p></td>
<td align="left"><p>Create a shadow effect by adding depth testing to your vertex (or geometry) shader and your pixel shader.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="target-a-range-of-hardware.md">Support shadow maps on a range of hardware</a></p></td>
<td align="left"><p>Render higher-fidelity shadows on faster devices and faster shadows on less powerful devices.</p></td>
</tr>
</tbody>
</table>

 

## Shadow mapping application to Direct3D 9 desktop porting


Windows 8 adde d depth comparison functionality to feature level 9\_1 and 9\_3. Now you can migrate rendering code with shadow volumes to DirectX 11, and the Direct3D 11 renderer will be downlevel compatible with feature level 9 devices. This walkthrough shows how any Direct3D 11 app or game can implement traditional shadow volumes using depth testing. The code covers the following process:

1.  Creating Direct3D device resources for shadow mapping.
2.  Adding a rendering pass to create the depth map.
3.  Adding depth testing to the main rendering pass.
4.  Implementing the necessary shader code.
5.  Options for fast rendering on downlevel hardware.

Upon completing this walkthrough, you should be familiar with how to implement a basic compatible shadow volume technique in Direct3D 11 that's compatible with feature level 9\_1 and above.

## Prerequisites


You should [Prepare your dev environment for Universal Windows Platform (UWP) DirectX game development](prepare-your-dev-environment-for-windows-store-directx-game-development.md). You don't need a template yet, but you'll need Microsoft Visual Studio 2015 to build the code sample for this walkthrough.

## Related topics


**Direct3D**

* [Writing HLSL Shaders in Direct3D 9](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-writing-shaders-9)
* [Create a new DirectX 11 project for UWP](user-interface.md)

**Shadow mapping technical articles**

* [Common Techniques to Improve Shadow Depth Maps](/windows/desktop/DxTechArts/common-techniques-to-improve-shadow-depth-maps)
* [Cascaded Shadow Maps](/windows/desktop/DxTechArts/cascaded-shadow-maps)

 

 