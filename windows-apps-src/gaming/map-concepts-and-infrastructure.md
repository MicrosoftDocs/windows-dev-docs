---
title: Map OpenGL ES 2.0 to Direct3D 11
description: When starting the process of porting your graphics architecture from OpenGL ES 2.0 to Direct3D for the first time, familiarize yourself with the key differences between the APIs.
ms.assetid: 7f9b136c-aa22-04b3-d385-6e9e1f38b948
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, opengl, direct3d, porting
ms.localizationpriority: medium
---
# Map OpenGL ES 2.0 to Direct3D 11



When starting the process of porting your graphics architecture from OpenGL ES 2.0 to Direct3D for the first time, familiarize yourself with the key differences between the APIs. The topics in this section help you plan your port strategy and the API changes that you must make when moving your graphics processing to Direct3D.
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
<td align="left"><p><a href="compare-opengl-es-2-0-api-design-to-directx.md">Plan your port from OpenGL ES 2.0 to Direct3D</a></p></td>
<td align="left"><p>If you are porting a game from the iOS or Android platforms, you have probably made a significant investment in OpenGL ES 2.0. When preparing to move your graphics pipeline codebase to Direct3D 11 and the Windows Runtime, there are a few things you should consider before you start.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="moving-from-egl-to-dxgi.md">Compare EGL code to DXGI and Direct3D</a></p></td>
<td align="left"><p>The DirectX Graphics Interface (DXGI) and several Direct3D APIs serve the same role as EGL. This topic helps you understand DXGI and Direct3D 11 from the perspective of EGL.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="porting-uniforms-and-attributes.md">Compare OpenGL ES 2.0 buffers, uniforms, and vertex attributes to Direct3D</a></p></td>
<td align="left"><p>During the process of porting to Direct3D 11 from OpenGL ES 2.0, you must change the syntax and API behavior for passing data between the app and the shader programs.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="change-your-shader-loading-code.md">Compare the OpenGL ES 2.0 shader pipeline to Direct3D</a></p></td>
<td align="left"><p>Conceptually, the Direct3D 11 shader pipeline is very similar to the one in OpenGL ES 2.0. In terms of API design, however, the major components for creating and managing the shader stages are parts of two primary interfaces, <a href="https://docs.microsoft.com/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11device1"><strong>ID3D11Device1</strong></a> and <a href="https://docs.microsoft.com/windows/desktop/api/d3d11_1/nn-d3d11_1-id3d11devicecontext1"><strong>ID3D11DeviceContext1</strong></a>. This topic attempts to map common OpenGL ES 2.0 shader pipeline API patterns to the Direct3D 11 equivalents in these interfaces.</p></td>
</tr>
</tbody>
</table>

 

## Notes on specific OpenGL ES 2.0 providers


These topics use the Khronos OpenGL ES 2.0 specification with platform-agnostic C. Both iOS and Android utilize the same specification and OpenGL ES 2.0 code developed for those platforms is very similar to the code snippets we will walk through, although they are typically exposed as object-oriented APIs. Also, due to the intricacies and language differences of each platform, there may be minor differences, especially in method parameter types, or in general language syntax. iOS, for instance, uses Objective-C. Android has the capability to use C++; however, some developers may have relied on a pure Java implementation. With that in mind, these topics should still be useful as the overall concepts, structure and usage of the OpenGL ES APIs do not differ.

 

 




