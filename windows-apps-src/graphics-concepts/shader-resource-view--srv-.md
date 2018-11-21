---
title: Shader resource view (SRV) and Unordered Access view (UAV)
description: Shader resource views typically wrap textures in a format that the shaders can access them. An unordered access view provides similar functionality, but enables the reading and writing to the texture (or other resource) in any order.
ms.assetid: 4505BCD2-0EDA-40F2-887C-EC081FE32E8F
keywords:
- Shader resource view (SRV)
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Shader resource view (SRV) and Unordered Access view (UAV)


Shader resource views typically wrap textures in a format that the shaders can access them. An unordered access view provides similar functionality, but enables the reading and writing to the texture (or other resource) in any order.

Wrapping a single texture is probably the simplest form of shader resource view. More complex examples would be a collection of subresources (individual arrays, planes, or colors from a mipmapped texture), 3D textures, 1D texture color gradients, and so on.

Unordered access views are slightly more costly in terms of performance, but allow, for example, a texture to be written to at the same time that it is being read from. This enables the updated texture to be re-used by the graphics pipeline for some other purpose. Shader resource views are for read only use (which is the most common use of resources).

## <span id="related-topics"></span>Related topics


[Views](views.md)

 

 




