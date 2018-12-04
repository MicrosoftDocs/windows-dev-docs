---
title: Multipass texture blending
description: Direct3D applications can achieve numerous special effects by applying various textures to a primitive over the course of multiple rendering passes.
ms.assetid: FB4D6E3F-4EF5-4D20-BF7E-1008E790E30C
keywords:
- Multipass texture blending
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Multipass texture blending


Direct3D applications can achieve numerous special effects by applying various textures to a primitive over the course of multiple rendering passes. The common term for this is *multipass texture blending*. A typical use for multipass texture blending is to emulate the effects of complex lighting and shading models by applying multiple colors from several different textures. One such application is called *light mapping*. See [Light mapping with textures](light-mapping-with-textures.md).

**Note**   Some devices are capable of applying multiple textures to primitives in a single pass. See [Texture blending](texture-blending.md).

 

If the user's hardware does not support multiple texture blending, your application can use multipass texture blending to achieve the same visual effects. However, the application cannot sustain the frame rates that are possible when using multiple texture blending.

To perform multipass texture blending in a C/C++ application:

1.  Set a texture in texture stage 0.
2.  Select the desired color and alpha blending arguments and operations. The default settings are well-suited for multipass texture blending.
3.  Render the appropriate objects in the scene.
4.  Set the next texture in texture stage 0.
5.  Set the render states to adjust the source and destination blending factors as needed. The system blends the new textures with the existing pixels in the render-target surface according to these parameters.
6.  Repeat Steps 3, 4, and 5 with as many textures as needed.

## <span id="related-topics"></span>Related topics


[Texture blending](texture-blending.md)

 

 




