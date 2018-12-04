---
title: Texture views
description: In Direct3D, texture resources are accessed with a view, which is a mechanism for hardware interpretation of a resource in memory.
ms.assetid: 18DABFCE-8A36-4C4E-B08E-10428B05D701
keywords:
- Texture views
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# Texture views


In Direct3D, texture resources are accessed with a view, which is a mechanism for hardware interpretation of a resource in memory. A view allows a particular pipeline stage to access only the [subresources](resource-types.md) it needs, in the representation desired by the application.

A view supports the notion of a typeless resource. A typeless resource is a resource created with a specific size but not a specific data type. The data is interpreted dynamically when it is bound to the pipeline.

The following illustration shows an example of binding a 2D texture array with 6 textures as a shader resource by creating a shader resource view for it. The resource is then addressed as an array of textures. (Note: a subresource cannot be bound as both input and output to the pipeline simultaneously.)

![illustration of a texture array with six textures](images/d3d10-cube-texture-faces.png)

When using a 2D texture array as a render target, the resource can be viewed as an array of 2D textures (6 in this example) with mipmap levels (3 in this example).

Create a view object for a render target by calling CreateRenderTargetView. Then call OMSetRenderTargets to set the render target view to the pipeline. Render into the render targets by calling Draw and using the RenderTargetArrayIndex to index into the proper texture in the array. You can use a subresource (a mipmap level, array index combination) to bind to any array of subresources. So you could bind to the second mipmap level and only update this particular mipmap level if you wanted, as in the following illustration.

![illustration of binding only to the second mipmap level of a texture array](images/d3d10-cube-texture-faces-subresource.png)

## <span id="related-topics"></span>Related topics


[Resources](resources.md)

 

 




