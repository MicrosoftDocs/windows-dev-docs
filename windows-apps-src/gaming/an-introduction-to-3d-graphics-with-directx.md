---
author: mtoepke
title: Basic 3D graphics for DirectX games
description: We show how to use DirectX programming to implement the fundamental concepts of 3D graphics.
ms.assetid: 2989c91f-7b45-7377-4e83-9daa0325e92e
ms.author: mtoepke
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
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


Here, we talk about how to develop 3D graphics with DirectX and C++\\Cx. This five-part tutorial introduces you to the [Direct3D](https://msdn.microsoft.com/library/windows/desktop/hh309466) API and the concepts and code that are also used in many of the other DirectX samples. These parts build upon each other, from configuring DirectX for your UWP C++ app to texturing primitives and adding effects.

> **Note**  This tutorial uses a right-handed coordinate system with column vectors. Many DirectX samples and apps use a left-handed coordinate system with row vectors. For a more complete graphics math solution and one that supports a left-handed coordinate system with row vectors, consider using [DirectXMath](https://msdn.microsoft.com/library/windows/desktop/hh437833). For more info, see [Using DirectXMath with Direct3D](https://msdn.microsoft.com/library/windows/desktop/ff729728#Use_DXMath_with_D3D).

 

We show you how to:

-   Initialize [Direct3D](https://msdn.microsoft.com/library/windows/desktop/hh309466) interfaces by using the Windows Runtime
-   Apply per-vertex shader operations
-   Set up the geometry
-   Rasterize the scene (flattening the 3D scene to a 2D projection)
-   Cull the hidden surfaces

> **Note**  

 

Next, we create a Direct3D device, swap chain, and render-target view, and present the rendered image to the display.

[Quickstart: setting up DirectX resources and displaying an image](setting-up-directx-resources.md)

## Related topics


* [Direct3D 11 Graphics](https://msdn.microsoft.com/library/windows/desktop/ff476080)
* [DXGI](https://msdn.microsoft.com/library/windows/desktop/hh404534)
* [HLSL](https://msdn.microsoft.com/library/windows/desktop/bb509561)

 

 




