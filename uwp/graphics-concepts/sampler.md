---
title: Sampler
description: Sampling is the process of reading input values from a texture, or other resource. A \ 0034;sampler \ 0034; is any object that reads from resources.
ms.assetid: 7ECE13BB-9FC5-44A3-B1B2-2FE163F1D627
keywords:
- Sampler
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Sampler


Sampling is the process of reading input values from a texture, or other resource. A "sampler" is any object that reads from resources.

There are many issues and artifacts from sampling from a texture and rendering to a screen area. For example, if the area to be rendered to is 50 by 50 pixels, and the texture is 16 by 16 pixels or 256 by 256 pixels, then some considerable stretching or shrinking of the texture needs to be applied. Because this mismatch in size leads to unwelcome artifacts, filtering techniques are used to mitigate these artifacts. A common filtering approach to using small textures for larger rendering areas is "bilinear" filtering.

Another issue occurs when the area being rendered to is at a very oblique angle to the view (for example, a 256 by 256 texture being rendered to an area 100 pixels wide but only 5 pixels deep because of the viewing angle). In this case "anisotropic" filtering is often applied. Anisotropic filtering provides better image quality than bilinear filtering, as it removes aliasing effects without excessive blurring, but is more computationally expensive.

## <span id="related-topics"></span>Related topics


[Views](views.md)

 

 




