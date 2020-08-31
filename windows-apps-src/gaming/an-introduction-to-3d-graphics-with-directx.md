---
title: Basic 3D graphics for DirectX games
description: We show how to use DirectX programming to implement the fundamental concepts of 3D graphics.
ms.assetid: 2989c91f-7b45-7377-4e83-9daa0325e92e
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, directx, graphics
ms.localizationpriority: medium
---
# Basic 3D graphics for DirectX games



We show how to use DirectX programming to implement the fundamental concepts of 3D graphics.

**Objective:** Learn to program a 3D graphics app.

## Prerequisites


We assume that you are familiar with C++. You also need basic experience with graphics programming concepts.

**Total time to complete:** 30 minutes.

## Where to go from here


Here, we talk about how to develop 3D graphics with DirectX and C++\\Cx. This five-part tutorial introduces you to the [Direct3D](/windows/desktop/direct3d) API and the concepts and code that are also used in many of the other DirectX samples. These parts build upon each other, from configuring DirectX for your UWP C++ app to texturing primitives and adding effects.

> **Note**  This tutorial uses a right-handed coordinate system with column vectors. Many DirectX samples and apps use a left-handed coordinate system with row vectors. For a more complete graphics math solution and one that supports a left-handed coordinate system with row vectors, consider using [DirectXMath](/windows/desktop/dxmath/directxmath-portal). For more info, see [Using DirectXMath with Direct3D](/windows/desktop/dxmath/pg-xnamath-migration-d3dx).

 

We show you how to:

-   Initialize [Direct3D](/windows/desktop/direct3d) interfaces by using the Windows Runtime
-   Apply per-vertex shader operations
-   Set up the geometry
-   Rasterize the scene (flattening the 3D scene to a 2D projection)
-   Cull the hidden surfaces

> **Note**  

 

Next, we create a Direct3D device, swap chain, and render-target view, and present the rendered image to the display.

[Quickstart: setting up DirectX resources and displaying an image](setting-up-directx-resources.md)

## Related topics


* [Direct3D 11 Graphics](/windows/desktop/direct3d11/atoc-dx-graphics-direct3d-11)
* [DXGI](/windows/desktop/direct3ddxgi/dx-graphics-dxgi)
* [HLSL](/windows/desktop/direct3dhlsl/dx-graphics-hlsl)

 

 