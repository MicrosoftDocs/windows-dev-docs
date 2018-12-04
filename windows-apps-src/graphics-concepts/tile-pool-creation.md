---
title: Tile pool creation
description: Applications can create one or more tile pools per Direct3D device. The total size of each tile pool is restricted to Direct3D 11's resource size limit, which is roughly 1/4 of graphics processing unit (GPU) RAM.
ms.assetid: BD51EDD3-4AD3-4733-B014-DD77B9D743BB
keywords:
- Tile pool creation
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Tile pool creation


Applications can create one or more tile pools per Direct3D device. The total size of each tile pool is restricted to Direct3D 11's resource size limit, which is roughly 1/4 of graphics processing unit (GPU) RAM.

A tile pool is made of 64KB tiles, but the operating system (display driver) manages the entire pool as one or more allocations behind the scenes—the breakdown is not visible to applications. Streaming resources define content by pointing at tiles within a tile pool. Unmapping a tile from a streaming resource is done by pointing the tile to **NULL**. Such unmapped tiles have rules about the behavior of reads or writes; see [Hazard tracking versus tile pool resources](hazard-tracking-versus-tile-pool-resources.md).

## <span id="related-topics"></span>Related topics


[Mappings are into a tile pool](mappings-are-into-a-tile-pool.md)

 

 




