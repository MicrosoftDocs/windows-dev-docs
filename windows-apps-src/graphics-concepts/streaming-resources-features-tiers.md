---
title: Streaming resources features tiers
description: Direct3D supports streaming resources in three tiers of capabilities.
ms.assetid: 6AE7EA72-3929-4BB4-8780-F0CF26192D87
keywords:
- Streaming resources features tiers
author: michaelfromredmond
ms.author: mithom
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: medium
---

# Streaming resources features tiers


Direct3D supports streaming resources in three tiers of capabilities.

Tier 1 provides basic capabilities for streaming resources.

Tier 2 adds capabilities beyond Tier 1, such as guaranteeing non-packed texture mipmap when the size is at least one standard tile shape; shader instructions for clamping level-of-detail (LOD) and for obtaining status about the shader operation; also, reading from NULL-mapped tiles treat that sampled value as zero.

Tier 3 adds Texture3D capabilities, beyond Tier 2.

Query functions are available in the versions of Direct3D, to validate hardware and driver support for streaming resources, and at what tier level.

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
<td align="left"><p>[Tier 1](tier-1.md)</p></td>
<td align="left"><p>This section describes tier 1 support.</p></td>
</tr>
<tr class="even">
<td align="left"><p>[Tier 2](tier-2.md)</p></td>
<td align="left"><p>Tier 2 support for streaming resources adds capabilities beyond Tier 1, such as guaranteeing nonpacked texture mipmap when the size is at least one standard tile shape; shader instructions for clamping level-of-detail (LOD) and for obtaining status about the shader operation; also, reading from NULL-mapped tiles treat that sampled value as zero.</p></td>
</tr>
<tr class="odd">
<td align="left"><p>[Tier 3](tier-3.md)</p></td>
<td align="left"><p>Tier 3 adds support for Texture3D for streaming resources, in addition to the [Tier 2](tier-2.md) capabilities.</p></td>
</tr>
</tbody>
</table>

 

## <span id="related-topics"></span>Related topics


[Streaming resources](streaming-resources.md)

 

 




