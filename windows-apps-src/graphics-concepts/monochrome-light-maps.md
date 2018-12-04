---
title: Monochrome light maps
description: Monochrome light mapping enables older adapters to perform multipass texture blending, when an older 3D accelerator board doesn't support texture blending using the alpha value of the destination pixel.
ms.assetid: 60F8F8F6-9DB7-452B-8DC0-407FFAA4BFE1
keywords:
- Monochrome light maps
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Monochrome light maps


Monochrome light mapping enables older adapters to perform multipass texture blending, when an older 3D accelerator board doesn't support texture blending using the alpha value of the destination pixel.

Some older 3D accelerator boards do not support texture blending using the alpha value of the destination pixel. These adapters also generally do not support multiple texture blending. If your application is running on an adapter such as this, it can use multipass texture blending to perform monochrome light mapping.

To perform monochrome light mapping, an application stores the lighting information in the alpha data of its light map textures. The application uses the texture filtering capabilities of Direct3D to perform a mapping from each pixel in the primitive's image to a corresponding texel in the light map. It sets the source blending factor to the alpha value of the corresponding texel.

## <span id="related-topics"></span>Related topics


[Light mapping with textures](light-mapping-with-textures.md)

 

 




