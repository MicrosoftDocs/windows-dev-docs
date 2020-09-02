---
title: Buffer tiling
description: A Buffer resource is divided into 64KB tiles, with some empty space in the last tile if the size is not a multiple of 64KB.
ms.assetid: 577DC6B0-F373-4748-AD80-2784C597C366
keywords:
- Buffer tiling
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Buffer tiling


A [Buffer](introduction-to-buffers.md) resource is divided into 64KB tiles, with some empty space in the last tile if the size is not a multiple of 64KB.

Structured buffers must have no constraint on the stride to be tiled. But possible performance optimizations in hardware for using [**StructuredBuffers**](/windows/desktop/direct3dhlsl/sm5-object-structuredbuffer) can be sacrificed by making them tiled in the first place.

## <span id="related-topics"></span>Related topics


[How a streaming resource's area is tiled](how-a-streaming-resource-s-area-is-tiled.md)

 

 