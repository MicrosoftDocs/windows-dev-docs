---
title: Constant buffer view (CBV)
description: Constant buffers contain shader constant data. The value of them is that the data persists, and can be accessed by any GPU shader, until it is necessary to change the data.
ms.assetid: 99AEC6B0-A43B-4B61-8C3A-ECC8DE1B69A7
keywords:
- Constant buffer view (CBV)
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Constant buffer view (CBV)


Constant buffers contain shader constant data. The value of them is that the data persists, and can be accessed by any GPU shader, until it is necessary to change the data.

Typical data for a constant buffer would be world, projection and view matrices, which remain constant throughout the drawing of one frame.

Constant buffer layout should match the HLSL layout (refer to [Packing Rules for Constant Variables](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-packing-rules)).

## <span id="related-topics"></span>Related topics


[Views](views.md)

 

 