---
title: Texture3D subresource tiling
description: See a table that shows how Texture3D subresources are tiled, based on the bits per pixel of the Texture2D tiling.
ms.assetid: 210D03E4-CF12-47E0-BA2F-C8D059B17D3E
keywords:
- Texture3D subresource tiling
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Texture3D subresource tiling


This table shows how [**Texture3D**](/windows/desktop/direct3dhlsl/sm5-object-texture3d) subresources are tiled. The values in this table don't count tail mip packing.

This table takes the [**Texture2D**](/windows/desktop/direct3dhlsl/sm5-object-texture2d) tiling and divides the x/y dimensions by 4 each and adds 16 layers of depth. All the tiles for the first plane (2D plane of tiles defining the first 16 layers of depth) appear before the subsequent planes.

**Note**  [**Texture3D**](/windows/desktop/direct3dhlsl/sm5-object-texture3d) support in streaming resources isn't exposed in the initial implementation of streaming resources, but the desired tile shapes are listed here for possible support in a future release.

 

| Bits/Pixel (1 sample/pixel) | Tile Dimensions (Pixels, WxHxD) |
|-----------------------------|---------------------------------|
| 8                           | 64x32x32                        |
| 16                          | 32x32x32                        |
| 32                          | 32x32x16                        |
| 64                          | 32x16x16                        |
| 128                         | 16x16x16                        |
| BC1,4                       | 128x64x16                       |
| BC2,3,5,6,7                 | 64x64x16                        |

 

Format bit counts not supported with streaming resources are 96 bpp formats, video formats, DXGI\_FORMAT\_R1\_UNORM, DXGI\_FORMAT\_R8G8\_B8G8\_UNORM, and DXGI\_FORMAT\_R8R8\_G8B8\_UNORM.

## <span id="related-topics"></span>Related topics


[How a streaming resource's area is tiled](how-a-streaming-resource-s-area-is-tiled.md)

 

 