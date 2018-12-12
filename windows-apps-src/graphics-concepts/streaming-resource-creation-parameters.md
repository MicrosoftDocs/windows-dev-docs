---
title: Streaming resource creation parameters
description: There are some constraints on the type of Direct3D resources that you can create as a streaming resource.
ms.assetid: 6FC5AD93-6F47-479E-947C-895C99B427BC
keywords:
- Streaming resource creation parameters
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Streaming resource creation parameters


There are some constraints on the type of Direct3D resources that you can create as a streaming resource.

<span id="Supported-Resource-Type"></span><span id="supported-resource-type"></span><span id="SUPPORTED-RESOURCE-TYPE"></span>**Supported Resource Type**  
Texture2D\[Array\] (including TextureCube\[Array\], which is a variant of Texture2D\[Array\]) or Buffer.

**NOT supported:  **Texture1D\[Array\] .

<span id="Supported-Resource-Usage"></span><span id="supported-resource-usage"></span><span id="SUPPORTED-RESOURCE-USAGE"></span>**Supported Resource Usage**  
Default usage.

**NOT supported:  **Dynamic, staging, or immutable.

<span id="Supported-Resource-Misc-Flags"></span><span id="supported-resource-misc-flags"></span><span id="SUPPORTED-RESOURCE-MISC-FLAGS"></span>**Supported Resource Misc Flags**  
Tiled; that is, streaming (by definition), texture cube, draw indirect arguments, buffer allow raw views, structured buffer, resource clamp, or generate mips.

**NOT supported:  **shared, shared keyed mutex, GDI compatible, shared NT handle, restricted content, restrict shared resource, restrict shared resource driver, guarded, or tile pool.

<span id="Supported-Bind-Flags"></span><span id="supported-bind-flags"></span><span id="SUPPORTED-BIND-FLAGS"></span>**Supported Bind Flags**  
Bind as shader resource, render target, depth stencil, or unordered access.

**NOT supported:  **Bind as constant buffer, vertex buffer (binding a tiled Buffer as an SRV/UAV/RTV is supported), index buffer, stream output, decoder, or video encoder.

<span id="Supported-Formats"></span><span id="supported-formats"></span><span id="SUPPORTED-FORMATS"></span>**Supported Formats**  
All formats that would be available for the given configuration regardless of it being tiled, with some exceptions.

<span id="Supported-Sample-Description--Multisample-count--quality-"></span><span id="supported-sample-description--multisample-count--quality-"></span><span id="SUPPORTED-SAMPLE-DESCRIPTION--MULTISAMPLE-COUNT--QUALITY-"></span>**Supported Sample Description (Multisample count, quality)**  
Whatever would be supported for the given configuration regardless of it being tiled, with some exceptions.

<span id="Supported-Width-Height-MipLevels-ArraySize"></span><span id="supported-width-height-miplevels-arraysize"></span><span id="SUPPORTED-WIDTH-HEIGHT-MIPLEVELS-ARRAYSIZE"></span>**Supported Width/Height/MipLevels/ArraySize**  
Full extents supported by Direct3D. Streaming resources don't have the restriction on total memory size imposed on non-streaming resources. Streaming resources are only constrained by overall virtual address space limits. See [Address space available for streaming resources](address-space-available-for-streaming-resources.md).

The initial contents of tile pool memory are undefined.

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
<td align="left"><p><a href="address-space-available-for-streaming-resources.md">Address space available for streaming resources</a></p></td>
<td align="left"><p>This section specifies the virtual address space that is available for streaming resources.</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Creating streaming resources](creating-streaming-resources.md)

 

 




