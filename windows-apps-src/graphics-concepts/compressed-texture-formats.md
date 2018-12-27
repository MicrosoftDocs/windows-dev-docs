---
title: Compressed texture formats
description: This section contains information about the internal organization of compressed texture formats.
ms.assetid: 24D17B9F-8CA7-4006-9E0F-178C6B3CAEC9
keywords:
- Compressed texture formats
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Compressed texture formats


This section contains information about the internal organization of compressed texture formats. You do not need these details to use compressed textures, because you can use Direct3D functions for conversion to and from compressed formats. However, this information is useful if you want to operate on compressed surface data directly.

Direct3D uses a compression format that divides texture maps into 4x4 texel blocks. If the texture contains no transparency - is opaque - or if the transparency is specified by a 1-bit alpha, an 8-byte block represents the texture map block. If the texture map does contain transparent texels, using an alpha channel, a 16-byte block represents it.

Any single texture must specify that its data is stored as 64 or 128 bits per group of 16 texels. If 64-bit blocks - that is, format BC1 - are used for the texture, it is possible to mix the opaque and 1-bit alpha formats on a per-block basis within the same texture. In other words, the comparison of the unsigned integer magnitude of color\_0 and color\_1 is performed uniquely for each block of 16 texels.

When 128-bit blocks are used, the alpha channel must be specified in either explicit (format BC2) or interpolated mode (format BC3) for the entire texture. As with color, when interpolated mode is selected, either eight interpolated alphas or six interpolated alphas mode can be used on a block-by-block basis. Again the magnitude comparison of alpha\_0 and alpha\_1 is done uniquely on a block-by-block basis.

The pitch for BCn formats is measured in bytes (not blocks). For example, if you have a width of 16, then you will have a pitch of four blocks (4\*8 for BC1, 4\*16 for BC2 or BC3).

## <span id="related-topics"></span>Related topics


[Compressed texture resources](compressed-texture-resources.md)

 

 




