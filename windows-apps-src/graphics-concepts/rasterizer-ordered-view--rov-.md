---
title: Rasterizer ordered view (ROV)
description: Rasterizer ordered views enable some of the limitations of a depth buffer to be addressed, in particular having multiple textures containing transparency all applying to the same pixels.
ms.assetid: BCB1EE0D-4C1D-4E17-BDB7-173F448E0A7B
keywords:
- Rasterizer ordered view (ROV)
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Rasterizer ordered view (ROV)


Rasterizer ordered views enable some of the limitations of a depth buffer to be addressed, in particular having multiple textures containing transparency all applying to the same pixels.

Rasterizer ordered views enable "Order Independent Transparency" (OIT) algorithms to be applied to the rendering of pixels. A depth buffer only enables a pixel to be drawn or occluded, there is no concept of partial occlusion through transparency. OIT algorithms apply transparent textures in the correct order, so if, for example, a transparent glass object should appear behind a glass window that is behind some vegetation that uses transparent textures, then the final result is drawn predictably correct. Without ROVs and OIT algorithms, the order these transparent objects would be drawn was unpredictable and the rendered scene could simply be confusing and wrong.

## <span id="related-topics"></span>Related topics


[Views](views.md)

 

 




