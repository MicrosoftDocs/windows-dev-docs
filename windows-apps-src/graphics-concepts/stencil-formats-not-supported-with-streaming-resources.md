---
title: Unsupported stencil formats with streaming resources
description: Formats that contain stencil aren't supported with streaming resources.
ms.assetid: 90A572A4-3C76-4795-BAE9-FCC72B5F07AD
keywords:
- Stencil formats not supported with streaming resources
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Stencil formats not supported with streaming resources


Formats that contain stencil aren't supported with streaming resources.

Formats that contain stencil include DXGI\_FORMAT\_D24\_UNORM\_S8\_UINT (and related formats in the R24G8 family) and DXGI\_FORMAT\_D32\_FLOAT\_S8X24\_UINT (and related formats in the R32G8X24 family).

Some implementations store depth and stencil in separate allocations while others store them together. Tile management for the two schemes would have to be different, and no single API can abstract or rationalize the differences. We recommend for future hardware to support independent depth and stencil surfaces, each independently tiled.

32-bit depth would have 128x128 tiles, and 8-bit stencil would have 256x256 tiles. Therefore, applications would have to live with tile shape misalignment between depth and stencil. But the same problem exists with different render target surface formats already.

## <span id="related-topics"></span>Related topics


[Streaming resource cross-process and device sharing](streaming-resource-cross-process-and-device-sharing.md)

 

 




