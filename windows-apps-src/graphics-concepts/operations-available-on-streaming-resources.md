---
title: Operations available on streaming resources
description: This section lists operations that you can perform on streaming resources.
ms.assetid: 700D8C54-0E20-4B2B-BEA3-20F6F72B8E24
keywords:
- Operations available on streaming resources
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Operations available on streaming resources


This section lists operations that you can perform on streaming resources.

-   Update Tile Mappings operations that return void, and Copy Tile Mappings operations that return void - These operations point tile locations in a streaming resource to locations in tile pools, or to NULL, or to both. These operations can update a disjoint subset of the tile pointers.
-   Copy and Update operations - All the APIs that can copy data to and from a default pool surface work for streaming resources. Reading from unmapped tiles produces 0 and writes to unmapped tiles are dropped.
-   Copy Tiles and Update Tiles operations - These operations exist for copying tiles at 64KB granularity to and from any streaming resource and a buffer resource in a canonical memory layout. The display driver and hardware perform any memory "swizzling" necessary for the streaming resource.
-   Direct3D pipeline bindings and view creations / bindings that would work on non-streaming resources work on streaming resources as well.

Tile controls are available on immediate or deferred contexts (just like updates to typical resources) and upon execution impact subsequent accesses to the tiles (not previously submitted operations).

## <span id="related-topics"></span>Related topics


[Creating streaming resources](creating-streaming-resources.md)

 

 




