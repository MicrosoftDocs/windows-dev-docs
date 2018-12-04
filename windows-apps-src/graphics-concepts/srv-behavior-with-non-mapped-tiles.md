---
title: SRV behavior with non-mapped tiles
description: Behavior of shader resource view (SRV) reads that involve non-mapped tiles depends on the level of hardware support.
ms.assetid: 0E1D64BE-EB09-4F9D-9800-BD23A3B374EE
keywords:
- SRV behavior with non-mapped tiles
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# <span id="direct3dconcepts.srv_behavior_with_non-mapped_tiles"></span>SRV behavior with non-mapped tiles


Behavior of shader resource view (SRV) reads that involve non-mapped tiles depends on the level of hardware support. For a breakdown of requirements, see read behavior for [Streaming resources features tiers](streaming-resources-features-tiers.md). This section summarizes the ideal behavior, which [Tier 2](tier-2.md) requires.

Consider a texture filter operation that reads from a set of texels in an SRV. Texels that fall on non-mapped tiles contribute 0 in all non-missing components of the format (and the default for missing components) into the overall filter operation alongside contributions from mapped texels. The texels are all weighted and combined together independent of whether the data came from mapped or non-mapped tiles.

Some first-generation [Tier 2](tier-2.md) level hardware does not meet this spec requirement, and returns the 0 with defaults described preceding as the overall filter result if any texels (with nonzero weight) fall on non-mapped tiles. No other hardware will be allowed to miss the requirement to include all (nonzero weight) texels in the filter.

## <span id="related-topics"></span>Related topics


[Pipeline access to streaming resources](pipeline-access-to-streaming-resources.md)

 

 




