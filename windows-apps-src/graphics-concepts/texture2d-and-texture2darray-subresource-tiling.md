---
title: Texture2D and Texture2DArray subresource tiling
description: These tables show how Texture2D and Texture2DArray subresources are tiled.
ms.assetid: 2DC14DFC-5299-44D9-895F-5A223D3FD530
keywords:
- Texture2D and Texture2DArray subresource tiling
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Texture2D and Texture2DArray subresource tiling


These tables show how [**Texture2D**](/windows/desktop/direct3dhlsl/sm5-object-texture2d) and [**Texture2DArray**](/windows/desktop/direct3dhlsl/sm5-object-texture2darray) subresources are tiled. The values in these tables don't count tail mip packing.

## <span id="Subresources-with-multisample-counts-of-1"></span><span id="subresources-with-multisample-counts-of-1"></span><span id="SUBRESOURCES-WITH-MULTISAMPLE-COUNTS-OF-1"></span>Subresources with multisample counts of 1


This table shows how [**Texture2D**](/windows/desktop/direct3dhlsl/sm5-object-texture2d) and [**Texture2DArray**](/windows/desktop/direct3dhlsl/sm5-object-texture2darray) subresources with multisample counts of 1 are tiled.

| Bits/Pixel (1 sample/pixel) | Tile Dimensions (Pixels, WxH) |
|-----------------------------|-------------------------------|
| 8                           | 256x256                       |
| 16                          | 256x128                       |
| 32                          | 128x128                       |
| 64                          | 128x64                        |
| 128                         | 64x64                         |
| BC1,4                       | 512x256                       |
| BC2,3,5,6,7                 | 256x256                       |

 

Format bit counts not supported with streaming resources are 96 bpp formats, video formats, DXGI\_FORMAT\_R1\_UNORM, DXGI\_FORMAT\_R8G8\_B8G8\_UNORM, and DXGI\_FORMAT\_R8R8\_G8B8\_UNORM.

## <span id="Subresources-with-various-multisample-counts"></span><span id="subresources-with-various-multisample-counts"></span><span id="SUBRESOURCES-WITH-VARIOUS-MULTISAMPLE-COUNTS"></span>Subresources with various multisample counts


This table shows how [**Texture2D**](/windows/desktop/direct3dhlsl/sm5-object-texture2d) and [**Texture2DArray**](/windows/desktop/direct3dhlsl/sm5-object-texture2darray) subresources with various multisample counts are tiled.

| Bits/Pixel (1 sample/pixel) | Tile Dimensions (Pixels, WxH) |
|-----------------------------|-------------------------------|
| 1                           | 1x1                           |
| 2                           | 2x1                           |
| 4                           | 2x2                           |
| 8                           | 4x2                           |
| 16                          | 4x4                           |

 

Only sample counts 1 and 4 are required (and allowed) to be supported with streaming resources. Streaming resources don't currently support 2, 8, and 16 even though they are shown.

Implementations can choose to support 2, 8, and/or 16 sample multisample antialiasing (MSAA) mode for non-streaming resources even though streaming resource don't support them.

Streaming resources with sample counts larger than 1 can't use 128 bpp formats.

The constraints on supported sample counts and formats are due to hardware inconsistencies from the specification.

## <span id="related-topics"></span>Related topics


[How a streaming resource's area is tiled](how-a-streaming-resource-s-area-is-tiled.md)

 

 