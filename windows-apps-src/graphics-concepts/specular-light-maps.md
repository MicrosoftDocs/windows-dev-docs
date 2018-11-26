---
title: Specular light maps
description: When illuminated by a light source, shiny objects that use highly reflective materials receive specular highlights.
ms.assetid: 9B3AC5EC-DDAA-4671-BC81-0E3C79D87A81
keywords:
- Specular light maps
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Specular light maps


When illuminated by a light source, shiny objects that use highly reflective materials receive specular highlights. Sometimes you can get more accurate highlights by applying specular light maps to primitives, rather than using the specular highlights produced by the lighting module.

To perform specular light mapping, add the specular light map to the primitive's texture, then modulate (multiply the result by) the RGB light map.

## <span id="related-topics"></span>Related topics


[Light mapping with textures](light-mapping-with-textures.md)

 

 




