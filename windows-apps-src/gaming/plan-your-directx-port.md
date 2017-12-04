---
author: mtoepke
title: Plan your DirectX port
description: Plan your game porting project from DirectX 9 to DirectX 11 and Universal Windows Platform (UWP)-- upgrade your graphics code, and put your game in the Windows Runtime environment.
ms.assetid: 3c0c33ca-5d15-ae12-33f8-9b5d8da08155
ms.author: mtoepke
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, directx, port
ms.localizationpriority: medium
---

# Plan your DirectX port



**Summary**

-   Plan your DirectX port
-   [Important changes from Direct3D 9 to Direct3D 11](understand-direct3d-11-1-concepts.md)
-   [Feature mapping](feature-mapping.md)


Plan your game porting project from DirectX 9 to DirectX 11 and Universal Windows Platform (UWP): upgrade your graphics code, and put your game in the Windows Runtime environment.

## Plan to port graphics code


Before you begin porting your game to UWP, it's important to ensure that your game does not have any holdovers from Direct3D 8. Ensure that your game doesn't have any remnants of the fixed function pipeline. For a complete list of deprecated features, including fixed pipeline functionality, see [Deprecated Features](https://msdn.microsoft.com/library/windows/desktop/cc308047).

Upgrading from Direct3D 9 to Direct3D 11 is more than a search-and-replace change. You need to know the difference between the Direct3D device, device context, and graphics infrastructure, and learn about other important changes since Direct3D 9. You can start this process by reading the other topics in this section.

You must replace the D3DX and DXUT helper libraries with your own helper libraries, or with community tools. See the [Feature mapping](feature-mapping.md) section for more info.

> **Note**   You can use the [DirectX Tool Kit](http://go.microsoft.com/fwlink/p/?LinkID=248929) or [DirectXTex](http://go.microsoft.com/fwlink/p/?LinkID=248926) to replace some functionality that was formerly provided by D3DX and DXUT.

 

Shaders written in assembly language should be upgraded to HLSL using shader model 4 level 9\_1 or 9\_3 functionality, and shaders written for the Effects library will need to be updated to a more recent version of HLSL syntax. See the [Feature mapping](feature-mapping.md) section for more info.

Get familiar with the different [Direct3D feature levels](https://msdn.microsoft.com/library/windows/desktop/ff476876). Feature levels classify a wide range of video hardware by defining sets of known functionality. Each set roughly corresponds to versions of Direct3D, from 9.1 through 11.2. All feature levels use the DirectX 11 API.

## Plan to port Win32 UI code to CoreWindow


UWP apps run in a window created for an app container, called a [**CoreWindow**](https://msdn.microsoft.com/library/windows/apps/br208225). Your game controls the window by inheriting from [**IFrameworkView**](https://msdn.microsoft.com/library/windows/apps/hh700478), which requires less implementation details than a desktop window. Your game's main loop will be in the [**IFrameworkView::Run**](https://msdn.microsoft.com/library/windows/apps/hh700505) method.

The lifecycle of a UWP app is very different from a desktop app. You'll need to save the game often, because when a suspend event happens your app only has a limited amount of time to stop running code, and you want to make sure the player can get back to where they were right away when your app resumes. Games should save just often enough to maintain a continuous gameplay experience from resume, but not so often that the game saves impact framerate or cause the game to stutter. Your game will potentially need to load game state when the game resumes from a terminated state.

[DirectXMath](https://msdn.microsoft.com/library/windows/desktop/ee415571) can be used as a replacement for D3DXMath and XNAMath, and it can come in handy if you need a math library. DirectXMath has fast, portable data types, and types that are aligned and packed for use with shaders.

Native libraries such as the [Interlocked API](https://msdn.microsoft.com/library/windows/desktop/dd405529) have been expanded to support ARM intrinsics. If your game uses interlocked APIs, you can keep using them in DirectX 11 and UWP.

Our templates and code samples use new C++ features that you might not be familiar with yet. For example, asynchronous methods are used with [**lambda expressions**](https://msdn.microsoft.com/library/windows/apps/dd293608.aspx) to load Direct3D resources without blocking the UI thread.

There are two concepts you'll use often:

-   Managed references ([**^ operator**](https://msdn.microsoft.com/library/windows/apps/yk97tc08.aspx)) and [**managed classes**](https://msdn.microsoft.com/library/windows/apps/6w96b5h7.aspx) (ref classes) are a fundamental part of the Windows Runtime. You will need to use managed ref classes to interface with Windows Runtime components, for example [**IFrameworkView**](https://msdn.microsoft.com/library/windows/apps/hh700478) (more on that in the walkthrough).
-   When working with Direct3D 11 COM interfaces, use the [**Microsoft::WRL::ComPtr**](https://msdn.microsoft.com/library/windows/apps/br244983.aspx) template type to make COM pointers easier to use.

 

 




