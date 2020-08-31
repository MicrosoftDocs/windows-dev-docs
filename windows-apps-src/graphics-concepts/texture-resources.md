---
title: Texture resources
description: Learn about rendering with Direct3D texture resources, and how to support multiple texture blending by using texture stages.
ms.assetid: 016F6CDA-D361-4E6B-BA99-49E915A19364
keywords:
- Texture resources
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Texture resources


Textures are a type of resource used for rendering.

## <span id="Rendering_with_Texture_Resources"></span><span id="rendering_with_texture_resources"></span><span id="RENDERING_WITH_TEXTURE_RESOURCES"></span>Rendering with texture resources


Direct3D supports multiple texture blending through the concept of texture stages. Each texture stage contains a texture and operations that can be performed on the texture. The textures in the texture stages form the set of current textures. See [Texture blending](texture-blending.md). The state of each texture is encapsulated in its texture stage.

Your application can also set the texture perspective and texture filtering states. See [Texture filtering](texture-filtering.md).

## <span id="related-topics"></span>Related topics


[Textures](textures.md)

 

 




