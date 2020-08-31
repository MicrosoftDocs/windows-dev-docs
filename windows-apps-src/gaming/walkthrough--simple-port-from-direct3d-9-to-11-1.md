---
title: Walkthrough-- Porting Direct3D 9 to DirectX 11 and UWP
description: This porting exercise shows how to bring a simple rendering framework from Direct3D 9 to Direct3D 11 and Universal Windows Platform (UWP).
ms.assetid: d4467e1f-929b-a4b8-b233-e142a8714c96
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, directx, port, direct3d 9, direct3d 11
ms.localizationpriority: medium
---
# Walkthrough: Port a simple Direct3D 9 app to DirectX 11 and Universal Windows Platform (UWP)



This porting exercise shows how to bring a simple rendering framework from Direct3D 9 to Direct3D 11 and Universal Windows Platform (UWP).
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
<td align="left"><p><a href="simple-port-from-direct3d-9-to-11-1-part-1--initializing-direct3d.md">Initialize Direct3D 11</a></p></td>
<td align="left"><p>Shows how to convert Direct3D 9 initialization code to Direct3D 11, including how to get handles to the Direct3D device and the device context and how to use DXGI to set up a swap chain.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="simple-port-from-direct3d-9-to-11-1-part-2--rendering.md">Convert the rendering framework</a></p></td>
<td align="left"><p>Shows how to convert a simple rendering framework from Direct3D 9 to Direct3D 11, including how to port geometry buffers, how to compile and load HLSL shader programs, and how to implement the rendering chain in Direct3D 11.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="simple-port-from-direct3d-9-to-11-1-part-3--viewport-and-game-loop.md">Port the game loop</a></p></td>
<td align="left"><p>Shows how to implement a window for a UWP game and how to bring over the game loop, including how to build an <a href="https://docs.microsoft.com/uwp/api/Windows.ApplicationModel.Core.IFrameworkView"><strong>IFrameworkView</strong></a> to control a full-screen <a href="https://docs.microsoft.com/uwp/api/Windows.UI.Core.CoreWindow"><strong>CoreWindow</strong></a>.</p></td>
</tr>
</tbody>
</table>

 

This topic walks through two code paths that perform the same basic graphics task: display a rotating vertex-shaded cube. In both cases, the code covers the following process:

1.  Creating a Direct3D device and a swap chain.
2.  Creating a vertex buffer, and an index buffer, to represent a colorful cube mesh.
3.  Creating a vertex shader that transforms vertices to screen space, a pixel shader that blends color values, compiling the shaders, and loading the shaders as Direct3D resources.
4.  Implementing the rendering chain and presenting the drawn cube to the screen.
5.  Creating a window, starting a main loop, and taking care of window message processing.

Upon completing this walkthrough, you should be familiar with the following basic differences between Direct3D 9 and Direct3D 11:

-   The separation of device, device context, and graphics infrastructure.
-   The process of compiling shaders, and loading shader bytecode at runtime.
-   How to configure per-vertex data for the Input Assembler (IA) stage.
-   How to use an [**IFrameworkView**](/uwp/api/Windows.ApplicationModel.Core.IFrameworkView) to create a CoreWindow view.

Note that this walkthrough uses [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) for simplicity, and does not cover XAML interop.

## Prerequisites


You should [Prepare your dev environment for UWP DirectX game development](prepare-your-dev-environment-for-windows-store-directx-game-development.md). You don't need a template yet, but you'll need Microsoft Visual Studio 2015 to load the code samples for this walkthrough.

Visit [Porting concepts and considerations](porting-considerations.md) to gain a better understanding of the DirectX 11 and UWP programming concepts shown in this walkthrough.

## Related topics

**Direct3D**

* [Writing HLSL Shaders in Direct3D 9](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-writing-shaders-9)
* [DirectX game project templates](user-interface.md)

**Microsoft Store**

* [**Microsoft::WRL::ComPtr**](/cpp/windows/comptr-class)
* [**Handle to Object Operator (^)**](/cpp/windows/handle-to-object-operator-hat-cpp-component-extensions)