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


[**QueryPerformanceCounter**](https://msdn.microsoft.com/library/windows/desktop/ms644904), along with [**QueryPerformanceFrequency**](https://msdn.microsoft.com/library/windows/desktop/ms644905), is still the best way to implement a game timer for UWP apps.

You should be aware of a nuance with timers and the UWP app lifecycle. Suspend/resume is different from a player re-launching a desktop game because your game will resume a snapshot in time from when the game was last played. If a large amount of time has passed - for example, a few weeks - some game timer implementations might not behave gracefully. You can use app lifecycle events to reset your timer when the game resumes.

Games that still use the RDTSC instruction need to upgrade. See [Game Timing and Multicore Processors](https://msdn.microsoft.com/library/windows/desktop/ee417693).

## My game code is based on D3DX and DXUT. Is there anything available that can help me migrate my code?


The [DirectX Tool Kit (DirectXTK)](http://go.microsoft.com/fwlink/p/?LinkID=248929) community project offers helper classes for use with Direct3D 11.

##  How do I maintain code paths for the desktop and the Microsoft Store?


Chuck Walbourn's article series titled [Dual-use Coding Techniques for Games](http://go.microsoft.com/fwlink/p/?LinkID=286210) offers guidance on sharing code between the desktop and the Microsoft Store code paths.

##  How do I load image resources in my DirectX UWP app?


There are two API paths for loading images:

-   The content pipeline converts images into DDS files used as Direct3D texture resources. See [Using 3-D Assets in Your Game or App](https://msdn.microsoft.com/library/windows/apps/hh972446.aspx).
-   The [Windows Imaging Component](https://msdn.microsoft.com/library/windows/desktop/ee719902) can be used to load images from a variety of formats, and can be used for Direct2D bitmaps as well as Direct3D texture resources.

You can also use the DDSTextureLoader, and the WICTextureLoader, from the [DirectXTK](http://go.microsoft.com/fwlink/p/?LinkID=248929) or [DirectXTex](http://go.microsoft.com/fwlink/p/?LinkID=248926).

## Where is the DirectX SDK?


The DirectX SDK is included as part of the Windows SDK. The most recent DirectX SDK that was separate from the Windows SDK was in June 2010. Direct3D samples are in the Code Gallery along with the rest of the Windows app samples.

## What about DirectX redistributables?


The vast majority of components in the Windows SDK are already included in supported versions of the OS, or have no DLL component (such as DirectXMath). All Direct3D API components that UWP apps can use will already available to your game; you don't need to be redistribute them.

Win32 desktop applications still use DirectSetup, so if you are also upgrading the desktop version of your game see [Direct3D 11 Deployment for Game Developers](https://msdn.microsoft.com/library/windows/desktop/ee416644).

## Is there any way I can update my desktop code to DirectX 11 before moving away from Effects?


See the [Effects for Direct3D 11 Update](http://go.microsoft.com/fwlink/p/?LinkId=271568). Effects 11 helps remove dependencies on legacy DirectX SDK headers; it's intended for use as a porting aid and can only be used with desktop apps.

##  Is there a path for porting my DirectX 8 game to UWP?


Yes:

-   Read [Converting to Direct3D 9](https://msdn.microsoft.com/library/windows/desktop/bb204851).
-   Make sure your game has no remnants of the fixed pipeline - see [Deprecated Features](https://msdn.microsoft.com/library/windows/desktop/cc308047).
-   Then take the DirectX 9 porting path: [Port from D3D 9 to UWP](walkthrough--simple-port-from-direct3d-9-to-11-1.md).

##  Can I port my DirectX 10 or 11 game to UWP?


DirectX 10.x and 11 desktop games are easy to port to UWP. See [Migrating to Direct3D 11](https://msdn.microsoft.com/library/windows/desktop/ff476190).

## How do I choose the right display device in a multi-monitor system?


The user selects which monitor your app is displayed on. Let Windows provide the correct adapter by calling [**D3D11CreateDevice**](https://msdn.microsoft.com/library/windows/desktop/ff476082) with the first parameter set to **nullptr**. Then get the device's [**IDXGIDevice interface**](https://msdn.microsoft.com/library/windows/desktop/bb174527), call [**GetAdapter**](https://msdn.microsoft.com/library/windows/desktop/bb174531) and use the DXGI adapter to create the swap chain.

## How do I turn on antialiasing?


Antialiasing (multisampling) is enabled when you create the Direct3D device. Enumerate multisampling support by calling [**CheckMultisampleQualityLevels**](https://msdn.microsoft.com/library/windows/desktop/ff476499), then set multisample options in the [**DXGI\_SAMPLE\_DESC structure**](https://msdn.microsoft.com/library/windows/desktop/bb173072) when you call [**CreateSurface**](https://msdn.microsoft.com/library/windows/desktop/bb174530).

## My game renders using multithreading and/or deferred rendering. What do I need to know for Direct3D 11?


Visit [Introduction to Multithreading in Direct3D 11](https://msdn.microsoft.com/library/windows/desktop/ff476891) to get started. For a list of key differences, see [Threading Differences between Direct3D Versions](https://msdn.microsoft.com/library/windows/desktop/ff476890). Note that deferred rendering uses a device *deferred context* instead of an *immediate context*.

## Where can I read more about the programmable pipeline since Direct3D 9?


Visit the following topics:

-   [Programming Guide for HLSL](https://msdn.microsoft.com/library/windows/desktop/bb509635)
-   [Direct3D 10 Frequently Asked Questions](https://msdn.microsoft.com/library/windows/desktop/ee416643)

## What should I use instead of the .x file format for my models?


While we don’t have an official replacement for the .x file format, many of the samples utilize the SDKMesh format. Visual Studio also has a [content pipeline](https://msdn.microsoft.com/library/windows/apps/hh972446.aspx) that compiles several popular formats into CMO files that can be loaded with code from the Visual Studio 3D starter kit, or loaded using the [DirectXTK](http://go.microsoft.com/fwlink/p/?LinkID=248929).

## How do I debug my shaders?


Microsoft Visual Studio 2015 includes diagnostic tools for DirectX graphics. See [Debugging DirectX Graphics](https://msdn.microsoft.com/library/windows/apps/hh315751.aspx).

##  What is the Direct3D 11 equivalent for *x* function?


See the [function mapping](feature-mapping.md#function-mapping) provided in Map DirectX 9 features to DirectX 11 APIs.

##  What is the DXGI\_FORMAT equivalent of *y* surface format?


See the [surface format mapping](feature-mapping.md#surface-format-mapping) provided in Map DirectX 9 features to DirectX 11 APIs.

 

 




