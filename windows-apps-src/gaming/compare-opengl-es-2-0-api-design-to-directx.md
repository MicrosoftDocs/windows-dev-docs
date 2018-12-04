---
title: Plan your port from OpenGL ES 2.0 to Direct3D
description: If you are porting a game from the iOS or Android platforms, you have probably made a significant investment in OpenGL ES 2.0.
ms.assetid: a31b8c5a-5577-4142-fc60-53217302ec3a
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, opengl, direct3d
ms.localizationpriority: medium
---
# Plan your port from OpenGL ES 2.0 to Direct3D




**Important APIs**

-   [Direct3D 11](https://msdn.microsoft.com/library/windows/desktop/ff476080)
-   [Visual C++](https://msdn.microsoft.com/library/windows/apps/60k1461a.aspx)

If you are porting a game from the iOS or Android platforms, you have probably made a significant investment in OpenGL ES 2.0. When preparing to move your graphics pipeline codebase to Direct3D 11 and the Windows Runtime, there are a few things you should consider before you start.

Most porting efforts usually involving initially walking the codebase and mapping common APIs and patterns between the two models. You'll find this process a bit easier if you take some time to read and review this topic.

Here are some things to be aware of when porting graphics from OpenGL ES 2.0 to Direct3D 11.

## Notes on specific OpenGL ES 2.0 providers


The porting topics in this section reference the Windows implementation of the OpenGL ES 2.0 specification created by the Khronos Group. All OpenGL ES 2.0 code samples were developed using Visual Studio 2012 and basic Windows C syntax. If you are coming from an Objective-C (iOS) or Java (Android) codebase, be aware that the provided OpenGL ES 2.0 code samples may not use similar API calling syntax or parameters. This guidance tries to stay as platform agnostic as possible.

This documentation only uses the 2.0 specification APIs for the OpenGL ES code and reference. If you are porting from OpenGL ES 1.1 or 3.0, this content can still prove useful, although some of the OpenGL ES 2.0 code examples and context may be unfamiliar.

The Direct3D 11 samples in these topics use Microsoft Windows C++ with Component Extensions (CX). For more info on this version of the C++ syntax, read [Visual C++](https://msdn.microsoft.com/library/windows/apps/60k1461a.aspx), [Component Extensions for Runtime Platforms](https://msdn.microsoft.com/library/windows/apps/xey702bw.aspx), and [Quick Reference (C++\\CX)](https://msdn.microsoft.com/library/windows/apps/br212455.aspx).

## Understand your hardware requirements and resources


The set of graphics processing features supported by OpenGL ES 2.0 roughly maps to the features provided in Direct3D 9.1. If you want to take advantage of the more advanced features provided in Direct3D 11, review the [Direct3D 11](https://msdn.microsoft.com/library/windows/desktop/ff476080) documentation when planning your port, or review the [Port from DirectX 9 to Universal Windows Platform (UWP)](porting-your-directx-9-game-to-windows-store.md) topics when you're done with the initial effort.

To make your initial porting effort simple, start with a Visual Studio Direct3D template. It provides a basic renderer already configured for you, and supports UWP app features like recreating resources on window changes and Direct3D feature levels.

## Understand Direct3D feature levels


Direct3D 11 provides support for hardware "feature levels" from 9\_1 (Direct3D 9.1) for 11\_1. These feature levels indicate the availability of certain graphics features and resources. Typically, most OpenGL ES 2.0 platforms support a Direct3D 9.1 (feature level 9\_1) set of features.

## Review DirectX graphics features and APIs


| API Family                                                | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 |
|-----------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [DXGI](https://msdn.microsoft.com/library/windows/desktop/hh404534)                     | The DirectX Graphics Infrastructure (DXGI) provides an interface between the graphics hardware and Direct3D. It sets the device adapter and hardware configuration using the [**IDXGIAdapter**](https://msdn.microsoft.com/library/windows/desktop/bb174523) and [**IDXGIDevice1**](https://msdn.microsoft.com/library/windows/desktop/hh404543) COM interfaces. Use it to create and configure your buffers and other window resources. Notably, the [**IDXGIFactory2**](https://msdn.microsoft.com/library/windows/desktop/hh404556) factory pattern iis used to acquire the graphics resources, including the swap chain (a set of frame buffers). Since DXGI owns the swap chain, the [**IDXGISwapChain1**](https://msdn.microsoft.com/library/windows/desktop/hh404631) interface is used to present frames to the screen. |
| [Direct3D](https://msdn.microsoft.com/library/windows/desktop/ff476080)       | Direct3D is the set of APIs that provide a virtual representation of the graphics interface and allow you to draw graphics using it. Version 11, is roughly comparable, feature-wise, to OpenGL 4.3. (OpenGL ES 2.0, on the other hand, is similar to DirectX9, feature-wise, and OpenGL 2.0, but with OpenGL 3.0's unified shader pipeline.) Most of the heavy lifting is done with the ID3D11Device1 and ID3D11DeviceContext1 interfaces which provide access to individual resources and subresources, and the rendering context, respectively.                                                                                                                                          |
| [Direct2D](https://msdn.microsoft.com/library/windows/desktop/dd370990)                      | Direct2D provides a set of APIs for GPU-accelerated 2D rendering. It can be considered similar in purpose to OpenVG.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
| [DirectWrite](https://msdn.microsoft.com/library/windows/desktop/dd368038)            | DirectWrite provides a set of APIs for GPU-accelerated, high-quality font rendering.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
| [DirectXMath](https://msdn.microsoft.com/library/windows/desktop/hh437833)                  | DirectXMath provides a set of APIs and macros for handling common linear algebra and trigonometric types, values, and functions. These types and functions are designed to work well with Direct3D and its shader operations.                                                                                                                                                                                                                                                                                                                                                                                                                                                               |
| [DirectX HLSL](https://msdn.microsoft.com/library/windows/desktop/bb509580) | The current HLSL syntax used by Direct3D shaders. It implements Direct3D Shader Model 5.0.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |

 

## Review the Windows Runtime APIs and template library


The Windows Runtime APIs provide the overall infrastructure for UWP apps. Review them [here](https://msdn.microsoft.com/library/windows/apps/br211377).

Key Windows Runtime APIs used in porting your graphics pipeline include:

-   [**Windows::UI::Core::CoreWindow**](https://msdn.microsoft.com/library/windows/apps/br208225)
-   [**Windows::UI::Core::CoreDispatcher**](https://msdn.microsoft.com/library/windows/apps/br208211)
-   [**Windows::ApplicationModel::Core::IFrameworkView**](https://msdn.microsoft.com/library/windows/apps/hh700478)
-   [**Windows::ApplicationModel::Core::CoreApplicationView**](https://msdn.microsoft.com/library/windows/apps/br225017)

Additionally, the Windows Runtime C++ Template Library (WRL) is a template library that provides a low-level way to author and use Windows Runtime components. The Direct3D 11 APIs for UWP apps are best used in conjunctions with the interfaces and types in this library, such as smart pointers ([ComPtr](https://msdn.microsoft.com/library/windows/apps/br244983.aspx)). For more info on the WRL, read [Windows Runtime C++ Template Library (WRL)](https://msdn.microsoft.com/library/windows/apps/hh438466.aspx).

## Change your coordinate system


One difference that sometimes confuses early port efforts is the change from OpenGL's traditional right-handed coordinate system to Direct3D's default left-handed coordinate system. This change in coordinate modeling affects many parts of your game, from the setup and configuration of your vertex buffers to many of your matrix math functions. The two most important changes to make are:

-   Flip the order of triangle vertices so that Direct3D traverses them clockwise from the front. For example, if your vertices are indexed as 0, 1, and 2 in your OpenGL pipeline, pass them to Direct3D as 0, 2, 1 instead.
-   Use the view matrix to scale world space by -1.0f in the z direction, effectively reversing the z-axis coordinates. To do this, flip the sign of the values at positions M31, M32, and M33 in your view matrix (when porting it to the [**Matrix**](https://msdn.microsoft.com/library/windows/desktop/bb147180) type). If M34 is not 0, flip its sign as well.

However, Direct3D can support a right-handed coordinate system. DirectXMath provides a number of functions that operate on and across both left-handed and right-handed coordinate systems. They can be used to preserve some of your original mesh data and matrix processing. They include:

| DirectXMath matrix function                                                   | Description                                                                                                                 |
|-------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------|
| [**XMMatrixLookAtLH**](https://msdn.microsoft.com/library/windows/desktop/ee419969)                               | Builds a view matrix for a left-handed coordinate system using a camera position, an up direction, and a focal point.       |
| [**XMMatrixLookAtRH**](https://msdn.microsoft.com/library/windows/desktop/ee419970)                               | Builds a view matrix for a right-handed coordinate system using a camera position, an up direction, and a focal point.      |
| [**XMMatrixLookToLH**](https://msdn.microsoft.com/library/windows/desktop/ee419971)                               | Builds a view matrix for a left-handed coordinate system using a camera position, an up direction, and a camera direction.  |
| [**XMMatrixLookToRH**](https://msdn.microsoft.com/library/windows/desktop/ee419972)                               | Builds a view matrix for a right-handed coordinate system using a camera position, an up direction, and a camera direction. |
| [**XMMatrixOrthographicLH**](https://msdn.microsoft.com/library/windows/desktop/ee419975)                   | Builds an orthogonal projection matrix for a left-handed coordinate system.                                                 |
| [**XMMatrixOrthographicOffCenterLH**](https://msdn.microsoft.com/library/windows/desktop/ee419976) | Builds a custom orthogonal projection matrix for a left-handed coordinate system.                                           |
| [**XMMatrixOrthographicOffCenterRH**](https://msdn.microsoft.com/library/windows/desktop/ee419977) | Builds a custom orthogonal projection matrix for a right-handed coordinate system.                                          |
| [**XMMatrixOrthographicRH**](https://msdn.microsoft.com/library/windows/desktop/ee419978)                   | Builds an orthogonal projection matrix for a right-handed coordinate system.                                                |
| [**XMMatrixPerspectiveFovLH**](https://msdn.microsoft.com/library/windows/desktop/ee419979)               | Builds a left-handed perspective projection matrix based on a field of view.                                                |
| [**XMMatrixPerspectiveFovRH**](https://msdn.microsoft.com/library/windows/desktop/ee419980)               | Builds a right-handed perspective projection matrix based on a field of view.                                               |
| [**XMMatrixPerspectiveLH**](https://msdn.microsoft.com/library/windows/desktop/ee419981)                     | Builds a left-handed perspective projection matrix.                                                                         |
| [**XMMatrixPerspectiveOffCenterLH**](https://msdn.microsoft.com/library/windows/desktop/ee419982)   | Builds a custom version of a left-handed perspective projection matrix.                                                     |
| [**XMMatrixPerspectiveOffCenterRH**](https://msdn.microsoft.com/library/windows/desktop/ee419983)   | Builds a custom version of a right-handed perspective projection matrix.                                                    |
| [**XMMatrixPerspectiveRH**](https://msdn.microsoft.com/library/windows/desktop/ee419984)                     | Builds a right-handed perspective projection matrix.                                                                        |

 

## OpenGL ES2.0-to-Direct3D 11 porting Frequently Asked Questions


-   Question: "In general, can I search for certain strings or patterns in my OpenGL code and replace them with the Direct3D equivalents?"
-   Answer: No. OpenGL ES 2.0 and Direct3D 11 come from different generations of graphics pipeline modeling. While there are some surface similarities between concepts and APIs, such as the rendering context and the instancing of shaders, you should review this guidance as well as the Direct3D 11 reference so you can make the best choices when recreating your pipeline instead of attempting a 1-to-1 mapping. However, if you are porting from GLSL to HLSL, creating a set of common aliases for GLSL variables, intrinsincs, and functions can not only make porting easier, it allows you to maintain only one set of shader code files.

 

 




