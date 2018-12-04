---
title: UAV behavior with non-mapped tiles
description: Behavior of unordered access view (UAV) reads and writes depends on the level of hardware support.
ms.assetid: CDB224E2-CC07-4568-9AAC-C8DC74536561
keywords:
- UAV behavior with non-mapped tiles
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# <span id="direct3dconcepts.uav_behavior_with_non-mapped_tiles"></span>UAV behavior with non-mapped tiles


Behavior of unordered access view (UAV) reads and writes depends on the level of hardware support. For a breakdown of requirements, see overall read and write behavior for [Streaming resources features tiers](streaming-resources-features-tiers.md). This section summarizes the ideal behavior.

Shader operations that read from a non-mapped tile in a UAV return 0 in all non-missing components of the format, and the default for missing components.

Shader operations that attempt to write to a non-mapped tile cause nothing to be written to the non-mapped area (while writes to a mapped area proceed). This ideal definition for write handling is not required by [Tier 2](tier-2.md); writes to non-mapped tiles can end up in a cache that subsequent reads could pick up.

## <span id="related-topics"></span>Related topics


[Pipeline access to streaming resources](pipeline-access-to-streaming-resources.md)

 

 




