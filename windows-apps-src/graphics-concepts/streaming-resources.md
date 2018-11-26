---
title: Streaming resources
description: Streaming resources are large logical resources that use small amounts of physical memory. Instead of passing an entire large resource, small parts of the resource are streamed as needed. Streaming resources were previously called tiled resources.
ms.assetid: 04F0486E-4B71-4073-88DA-2AF505F4F0C1
keywords:
- Streaming resources
- resources, streaming
- resources, tiled
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Streaming resources


*Streaming resources* are large logical resources that use small amounts of physical memory. Instead of passing an entire large resource, small parts of the resource are streamed as needed. Streaming resources were previously called *tiled resources*.

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
<td align="left"><p><a href="the-need-for-streaming-resources.md">The need for streaming resources</a></p></td>
<td align="left"><p>Streaming resources are needed so GPU memory isn't wasted storing regions of surfaces that won't be accessed, and to tell the hardware how to filter across adjacent tiles.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="creating-streaming-resources.md">Creating streaming resources</a></p></td>
<td align="left"><p>Streaming resources are created by specifying a flag when you create a resource, indicating that the resource is a streaming resource.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="pipeline-access-to-streaming-resources.md">Pipeline access to streaming resources</a></p></td>
<td align="left"><p>Streaming resources can be used in shader resource views (SRV), render target views (RTV), depth stencil views (DSV) and unordered access views (UAV), as well as some bind points where views aren't used, such as vertex buffer bindings.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="streaming-resources-features-tiers.md">Streaming resources features tiers</a></p></td>
<td align="left"><p>Direct3D supports streaming resources in three tiers of capabilities.</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Direct3D Graphics Learning Guide](index.md)

[Resources](resources.md)

 

 




