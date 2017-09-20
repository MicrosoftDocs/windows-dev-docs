---
title: Pipeline access to streaming resources
description: Streaming resources can be used in shader resource views (SRV), render target views (RTV), depth stencil views (DSV) and unordered access views (UAV), as well as some bind points where views aren't used, such as vertex buffer bindings.
ms.assetid: 18DA5D61-930D-4466-8EFE-0CED566EA4A6
keywords:
- Pipeline access to streaming resources
author: michaelfromredmond
ms.author: mithom
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
---

# Pipeline access to streaming resources


Streaming resources can be used in shader resource views (SRV), render target views (RTV), depth stencil views (DSV) and unordered access views (UAV), as well as some bind points where views aren't used, such as vertex buffer bindings. For the list of supported bindings, see [Streaming resource creation parameters](streaming-resource-creation-parameters.md). The various D3D Copy operations also work on streaming resources.

If multiple tile coordinates in one or more views is bound to the same memory location, reads and writes from different paths to the same memory will occur in a non-deterministic and non-repeatable order of memory accesses.

If all tiles behind a memory access footprint from a shader are mapped to unique tiles, behavior is identical on all implementations to the surface having the same memory contents in a non-tiled fashion.

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
<td align="left"><p>[SRV behavior with non-mapped tiles](srv-behavior-with-non-mapped-tiles.md)</p></td>
<td align="left"><p>Behavior of shader resource view (SRV) reads that involve non-mapped tiles depends on the level of hardware support.</p></td>
</tr>
<tr class="even">
<td align="left"><p>[UAV behavior with non-mapped tiles](uav-behavior-with-non-mapped-tiles.md)</p></td>
<td align="left"><p>Behavior of unordered access view (UAV) reads and writes depends on the level of hardware support.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>[Rasterizer behavior with non-mapped tiles](rasterizer-behavior-with-non-mapped-tiles.md)</p></td>
<td align="left"><p>This section describes rasterizer behavior with non-mapped tiles.</p></td>
</tr>
<tr class="even">
<td align="left"><p>[Tile access limitations with duplicate mappings](tile-access-limitations-with-duplicate-mappings.md)</p></td>
<td align="left"><p>There are limitations on tile access with duplicate mappings, such as when copying streaming resources with overlapping source and destination, or when rendering to tiles shared within the render area.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>[Streaming resources texture sampling features](streaming-resources-texture-sampling-features.md)</p></td>
<td align="left"><p>Streaming resources texture sampling features include getting shader status feedback about mapped areas, checking whether all data being accessed was mapped in the resource, clamping to help shaders avoid areas in mipmapped streaming resources that are known to be non-mapped, and discovering what the minimum LOD that is fully mapped for an entire texture filter footprint will be.</p></td>
</tr>
<tr class="even">
<td align="left"><p>[HLSL streaming resources exposure](hlsl-streaming-resources-exposure.md)</p></td>
<td align="left"><p>A specific Microsoft High Level Shader Language (HLSL) syntax is required to support streaming resources in [Shader Model 5](https://msdn.microsoft.com/library/windows/desktop/ff471356).</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Streaming resources](streaming-resources.md)

 

 




