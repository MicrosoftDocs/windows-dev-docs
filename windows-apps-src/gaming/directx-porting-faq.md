---
title: DirectX 11 porting FAQ
description: Answers to frequently-asked questions about porting games to Universal Windows Platform (UWP).
ms.assetid: 79c3b4c0-86eb-5019-97bb-5feee5667a2d
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, directx 11
ms.localizationpriority: medium
---
# DirectX 11 porting FAQ




Answers to frequently-asked questions about porting games to Universal Windows Platform (UWP).

## Is porting my game going to be a set of search-and-replace operations on API methods, or do I need to plan for a more thoughtful porting process?


Direct3D 11 is a significant upgrade from Direct3D 9. There are several paradigm shifts, including separate APIs for the virtualized graphics adapter and its context as well as a new layer of polymorphism for device resources. Your game can still use graphics hardware in essentially the same way, but you'll need to learn about the new Direct3D 11 API architecture and update each part of your graphics code to use the correct API components. See [Porting concepts and considerations](porting-considerations.md).

## What is the new device context for? Am I supposed to replace my Direct3D 9 device with the Direct3D 11 device, the device context, or both?


The Direct3D device is now used to create resources in video memory, while the device context is used to set pipeline state and generate rendering commands. For more info see: [What are the most important changes since Direct3D 9?](understand-direct3d-11-1-concepts.md)

##  Do I have to update my game timer for UWP?


[**QueryPerformanceCounter**](/windows/desktop/api/profileapi/nf-profileapi-queryperformancecounter), along with [**QueryPerformanceFrequency**](/windows/desktop/api/profileapi/nf-profileapi-queryperformancefrequency), is still the best way to implement a game timer for UWP apps.

You should be aware of a nuance with timers and the UWP app lifecycle. Suspend/resume is different from a player re-launching a desktop game because your game will resume a snapshot in time from when the game was last played. If a large amount of time has passed - for example, a few weeks - some game timer implementations might not behave gracefully. You can use app lifecycle events to reset your timer when the game resumes.

Games that still use the RDTSC instruction need to upgrade. See [Game Timing and Multicore Processors](/windows/desktop/DxTechArts/game-timing-and-multicore-processors).

## My game code is based on D3DX and DXUT. Is there anything available that can help me migrate my code?


The [DirectX Tool Kit (DirectXTK)](https://github.com/Microsoft/DirectXTK) community project offers helper classes for use with Direct3D 11.

##  How do I maintain code paths for the desktop and the Microsoft Store?


Chuck Walbourn's article series titled [Dual-use Coding Techniques for Games](https://blogs.msdn.com/b/chuckw/archive/2012/09/17/dual-use-coding-techniques-for-games.aspx) offers guidance on sharing code between the desktop and the Microsoft Store code paths.

##  How do I load image resources in my DirectX UWP app?


There are two API paths for loading images:

-   The content pipeline converts images into DDS files used as Direct3D texture resources. See [Using 3-D Assets in Your Game or App](/visualstudio/designers/using-3-d-assets-in-your-game-or-app?view=vs-2015).
-   The [Windows Imaging Component](/windows/desktop/wic/-wic-lh) can be used to load images from a variety of formats, and can be used for Direct2D bitmaps as well as Direct3D texture resources.

You can also use the DDSTextureLoader, and the WICTextureLoader, from the [DirectXTK](https://github.com/Microsoft/DirectXTK) or [DirectXTex](https://github.com/Microsoft/DirectXTex).

## Where is the DirectX SDK?


The DirectX SDK is included as part of the Windows SDK. The most recent DirectX SDK that was separate from the Windows SDK was in June 2010. Direct3D samples are in the Code Gallery along with the rest of the Windows app samples.

## What about DirectX redistributables?


The vast majority of components in the Windows SDK are already included in supported versions of the OS, or have no DLL component (such as DirectXMath). All Direct3D API components that UWP apps can use will already available to your game; you don't need to be redistribute them.

Win32 desktop applications still use DirectSetup, so if you are also upgrading the desktop version of your game see [Direct3D 11 Deployment for Game Developers](/windows/desktop/direct3darticles/direct3d11-deployment).

## Is there any way I can update my desktop code to DirectX 11 before moving away from Effects?


See the [Effects for Direct3D 11 Update](https://github.com/Microsoft/FX11). Effects 11 helps remove dependencies on legacy DirectX SDK headers; it's intended for use as a porting aid and can only be used with desktop apps.

##  Is there a path for porting my DirectX 8 game to UWP?


Yes:

-   Read [Converting to Direct3D 9](/windows/desktop/direct3d9/converting-to-directx-9).
-   Make sure your game has no remnants of the fixed pipeline - see [Deprecated Features](/windows/desktop/direct3d10/d3d10-graphics-programming-guide-api-features-deprecated).
-   Then take the DirectX 9 porting path: [Port from D3D 9 to UWP](walkthrough--simple-port-from-direct3d-9-to-11-1.md).

##  Can I port my DirectX 10 or 11 game to UWP?


DirectX 10.x and 11 desktop games are easy to port to UWP. See [Migrating to Direct3D 11](/windows/desktop/direct3d11/d3d11-programming-guide-migrating).

## How do I choose the right display device in a multi-monitor system?


The user selects which monitor your app is displayed on. Let Windows provide the correct adapter by calling [**D3D11CreateDevice**](/windows/desktop/api/d3d11/nf-d3d11-d3d11createdevice) with the first parameter set to **nullptr**. Then get the device's [**IDXGIDevice interface**](/windows/desktop/api/dxgi/nn-dxgi-idxgidevice), call [**GetAdapter**](/windows/desktop/api/dxgi/nf-dxgi-idxgidevice-getadapter) and use the DXGI adapter to create the swap chain.

## How do I turn on antialiasing?


Antialiasing (multisampling) is enabled when you create the Direct3D device. Enumerate multisampling support by calling [**CheckMultisampleQualityLevels**](/windows/desktop/api/d3d11/nf-d3d11-id3d11device-checkmultisamplequalitylevels), then set multisample options in the [**DXGI\_SAMPLE\_DESC structure**](/windows/desktop/api/dxgicommon/ns-dxgicommon-dxgi_sample_desc) when you call [**CreateSurface**](/windows/desktop/api/dxgi/nf-dxgi-idxgidevice-createsurface).

## My game renders using multithreading and/or deferred rendering. What do I need to know for Direct3D 11?


Visit [Introduction to Multithreading in Direct3D 11](/windows/desktop/direct3d11/overviews-direct3d-11-render-multi-thread-intro) to get started. For a list of key differences, see [Threading Differences between Direct3D Versions](/windows/desktop/direct3d11/overviews-direct3d-11-render-multi-thread-differences). Note that deferred rendering uses a device *deferred context* instead of an *immediate context*.

## Where can I read more about the programmable pipeline since Direct3D 9?


Visit the following topics:

-   [Programming Guide for HLSL](/windows/desktop/direct3dhlsl/dx-graphics-hlsl-pguide)
-   [Direct3D 10 Frequently Asked Questions](/windows/desktop/DxTechArts/direct3d10-frequently-asked-questions)

## What should I use instead of the .x file format for my models?


While we don’t have an official replacement for the .x file format, many of the samples utilize the SDKMesh format. Visual Studio also has a [content pipeline](/visualstudio/designers/using-3-d-assets-in-your-game-or-app?view=vs-2015) that compiles several popular formats into CMO files that can be loaded with code from the Visual Studio 3D starter kit, or loaded using the [DirectXTK](https://github.com/Microsoft/DirectXTK).

## How do I debug my shaders?


Microsoft Visual Studio 2015 includes diagnostic tools for DirectX graphics. See [Debugging DirectX Graphics](/visualstudio/debugger/visual-studio-graphics-diagnostics?view=vs-2015).

##  What is the Direct3D 11 equivalent for *x* function?


See the [function mapping](feature-mapping.md#function-mapping) provided in Map DirectX 9 features to DirectX 11 APIs.

##  What is the DXGI\_FORMAT equivalent of *y* surface format?


See the [surface format mapping](feature-mapping.md#surface-format-mapping) provided in Map DirectX 9 features to DirectX 11 APIs.

 

 