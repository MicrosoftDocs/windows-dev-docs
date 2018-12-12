---
title: Mipmap packing
description: Some number of mips (per array slice) can be packed into some number of tiles, depending on a streaming resource's dimensions, format, number of mipmaps, and array slices.
ms.assetid: 906C3CAC-4E84-4947-B508-06788551BE85
keywords:
- Mipmap packing
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Mipmap packing


Some number of mips (per array slice) can be packed into some number of tiles, depending on a streaming resource's dimensions, format, number of mipmaps, and array slices.

Depending on the [tier](streaming-resources-features-tiers.md) of streaming resources support, mipmaps with certain dimensions don't follow the standard tile shapes and are considered to all be packed together with one another in a manner that is opaque to the application. Higher tiers of support have broader guarantees about what types of surface dimensions fit in the standard tile shapes (and can therefore be individually mapped by applications).

What can vary between implementations is that—given a streaming resource's dimensions, format, number of mipmaps, and array slices—some number M of mips (per array slice) can be packed into some number N tiles. When you get the resource tiling information for a device, the driver reports to the application what M and N are (among other details about the surface that are standard and don't vary by hardware vendor). The set of tiles for the packed mips are still 64KB and can be individually mapped into disparate locations in a tile pool.

But the pixel shape of the tiles and how the mipmaps fit across the set of tiles is specific to a hardware vendor and too complex to expose. So, applications are required to either map all of the tiles that are designated as packed, or none of them, at a time. Otherwise, the behavior for accessing the streaming resource is undefined.

For arrayed surfaces, the set of packed mips and the number of packed tiles storing those mips (M and N described preceding) applies individually for each array slice.

Dedicated APIs for copying tiles can't access packed mips. Applications that want to copy data to and from packed mips can do so using all the non-streaming resource-specific APIs for copying and rendering to surfaces.

## <span id="related-topics"></span>Related topics


[How a streaming resource's area is tiled](how-a-streaming-resource-s-area-is-tiled.md)

 

 




