---
title: World transform
description: A world transform changes coordinates from model space, where vertices are defined relative to a model's local origin, to world space.
ms.assetid: 767B032C-69D0-4583-8FEB-247F4C41E31D
keywords:
- World transform
ms.date: 02/08/2017
ms.topic: article


ms.localizationpriority: medium
---
# World transform


A world transform changes coordinates from model space, where vertices are defined relative to a model's local origin, to world space. In world space, vertices are defined relative to an origin common to all the objects in a scene. The world transform places a model into the world.

## <span id="What_Is_a_World_Transform"></span><span id="what_is_a_world_transform"></span><span id="WHAT_IS_A_WORLD_TRANSFORM"></span>


The following diagram shows the relationship between the world coordinate system and a model's local coordinate system.

![diagram of how world coordinates and local coordinates are related](images/worldcrd.png)

The world transform can include any combination of translations, rotations, and scalings.

## <span id="SETTING_UP_A_WORLD_MATRIX.XML"></span>Setting Up a World Matrix


As with any other transform, create the world transform by concatenating a series of matrices into a single matrix that contains the sum total of their effects. In the simplest case, when a model is at the world origin and its local coordinate axes are oriented the same as world space, the world matrix is the identity matrix. More commonly, the world matrix is a combination of a translation into world space and possibly one or more rotations to turn the model as needed.

Direct3D uses the world and view matrices that you set to configure several internal data structures. Each time you set a new world or view matrix, the system recalculates the associated internal structures. Setting these matrices frequently-for example, thousands of times per frame-is computationally time-consuming. You can minimize the number of required calculations by concatenating your world and view matrices into a world-view matrix that you set as the world matrix, and then setting the view matrix to the identity. Keep cached copies of individual world and view matrices so that you can modify, concatenate, and reset the world matrix as needed.

## <span id="related-topics"></span>Related topics


[Transforms](transforms.md)

 

 




