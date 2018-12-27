---
title: Operations available on tile pools
description: Operations on tile pools include resizing a tile pool, offering resources (yielding memory temporarily to the system for the entire tile pool), and reclaiming resources.
ms.assetid: 90347F7F-C991-4287-BD70-494533ECDC8A
keywords:
- Operations available on tile pools
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Operations available on tile pools


Operations on tile pools include resizing a tile pool, offering resources (yielding memory temporarily to the system for the entire tile pool), and reclaiming resources.

-   The lifetime of tile pools works like any other Direct3D Resource, backed by reference counting, including in this case tracking of mappings from streaming resources. When the application no longer references a tile pool and any tile mappings to the memory are gone and graphics processing unit (GPU) accesses completed, the operating system will deallocate the tile pool.
-   APIs related to surface sharing and synchronization work for tile pools (but not directly on streaming resources). Similar to the behavior for offered tile pools, Direct3D commands that access streaming resources that point to a tile pool are dropped if the tile pool has been shared and is currently acquired by another device and process.
-   Resizing a tile pool.
-   Offering resources and reclaiming resources - These operations for yielding memory temporarily to the system operate on the entire tile pool (and aren't available for individual streaming resources). If a streaming resource points to a tile in an offered tile pool, the streaming resource behaves as if it is offered (for example, the runtime drops commands that reference it).

Data can't be copied to and from tile pool memory directly. Accesses to the memory are always done through streaming resources.

## <span id="related-topics"></span>Related topics


[Creating streaming resources](creating-streaming-resources.md)

 

 




