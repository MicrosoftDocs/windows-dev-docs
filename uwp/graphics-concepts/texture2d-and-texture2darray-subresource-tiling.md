---
title: Texture2D and Texture2DArray subresource tiling
description: Find Direct3D 11 tile dimensions, multisample dimension divisors, and format restrictions for Texture2D resources in a UWP app.
ms.assetid: 2DC14DFC-5299-44D9-895F-5A223D3FD530
keywords:
- Texture2D and Texture2DArray subresource tiling
ms.date: 09/08/2026
ms.topic: article
ms.localizationpriority: medium
---
# Texture2D and Texture2DArray subresource tiling

For Direct3D 11 tiled resources in a UWP app, use the shared [Texture2D and Texture2DArray subresource tiling tables](/windows/win32/direct3d11/texture2d-and-texture2darray-subresource-tiling). The UWP graphics topics refer to tiled resources as *streaming resources*. The tile dimensions and format restrictions depend on Direct3D, not the app model.

## <span id="Subresources-with-multisample-counts-of-1"></span><span id="subresources-with-multisample-counts-of-1"></span><span id="SUBRESOURCES-WITH-MULTISAMPLE-COUNTS-OF-1"></span>Subresources with multisample counts of 1

Use the first table in [Texture2D and Texture2DArray subresource tiling (Direct3D 11)](/windows/win32/direct3d11/texture2d-and-texture2darray-subresource-tiling) to find the tile dimensions for each pixel format. The article also lists unsupported formats. These dimensions exclude tail mip packing.

## <span id="Subresources-with-various-multisample-counts"></span><span id="subresources-with-various-multisample-counts"></span><span id="SUBRESOURCES-WITH-VARIOUS-MULTISAMPLE-COUNTS"></span>Subresources with various multisample counts

Use the second table in [Texture2D and Texture2DArray subresource tiling (Direct3D 11)](/windows/win32/direct3d11/texture2d-and-texture2darray-subresource-tiling) to find the **multisample count** and the factors by which to **divide the tile dimensions**. The article also explains which sample counts and formats tiled resources support.

## <span id="related-topics"></span>Related topics

[How a streaming resource's area is tiled](how-a-streaming-resource-s-area-is-tiled.md)
