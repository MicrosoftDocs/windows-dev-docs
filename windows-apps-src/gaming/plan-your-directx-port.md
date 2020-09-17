---
title: Plan your DirectX port
description: Plan your game porting project from DirectX 9 to DirectX 11 and Universal Windows Platform (UWP)-- upgrade your graphics code, and put your game in the Windows Runtime environment.
ms.assetid: 3c0c33ca-5d15-ae12-33f8-9b5d8da08155
ms.date: 02/08/2017
ms.topic: article
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


Before you begin porting your game to UWP, it's important to ensure that your game does not have any holdovers from Direct3D 8. Ensure that your game doesn't have any remnants of the fixed function pipeline. For a complete list of deprecated features, including fixed pipeline functionality, see [Deprecated Features](/windows/desktop/direct3d10/d3d10-graphics-programming-guide-api-features-deprecated).

Upgrading from Direct3D 9 to Direct3D 11 is more than a search-and-replace change. You need to know the difference between the Direct3D device, device context, and graphics infrastructure, and learn about other important changes since Direct3D 9. You can start this process by reading the other topics in this section.

You must replace the D3DX and DXUT helper libraries with your own helper libraries, or with community tools. See the [Feature mapping](feature-mapping.md) section for more info.

> **Note**   You can use the [DirectX Tool Kit](https://github.com/Microsoft/DirectXTK) or [DirectXTex](https://github.com/Microsoft/DirectXTex) to replace some functionality that was formerly provided by D3DX and DXUT.

 

Shaders written in assembly language should be upgraded to HLSL using shader model 4 level 9\_1 or 9\_3 functionality, and shaders written for the Effects library will need to be updated to a more recent version of HLSL syntax. See the [Feature mapping](feature-mapping.md) section for more info.

Get familiar with the different [Direct3D feature levels](/windows/desktop/direct3d11/overviews-direct3d-11-devices-downlevel-intro). Feature levels classify a wide range of video hardware by defining sets of known functionality. Each set roughly corresponds to versions of Direct3D, from 9.1 through 11.2. All feature levels use the DirectX 11 API.

## Plan to port Win32 UI code to CoreWindow


UWP apps run in a window created for an app container, called a [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow). Your game controls the window by inheriting from [**IFrameworkView**](/uwp/api/Windows.ApplicationModel.Core.IFrameworkView), which requires less implementation details than a desktop window. Your game's main loop will be in the [**IFrameworkView::Run**](/uwp/api/windows.applicationmodel.core.iframeworkview.run) method.

The lifecycle of a UWP app is very different from a desktop app. You'll need to save the game often, because when a suspend event happens your app only has a limited amount of time to stop running code, and you want to make sure the player can get back to where they were right away when your app resumes. Games should save just often enough to maintain a continuous gameplay experience from resume, but not so often that the game saves impact framerate or cause the game to stutter. Your game will potentially need to load game state when the game resumes from a terminated state.

[DirectXMath](/windows/desktop/dxmath/ovw-xnamath-progguide) can be used as a replacement for D3DXMath and XNAMath, and it can come in handy if you need a math library. DirectXMath has fast, portable data types, and types that are aligned and packed for use with shaders.

Native libraries such as the [Interlocked API](/windows/desktop/Sync/what-s-new-in-synchronization) have been expanded to support ARM intrinsics. If your game uses interlocked APIs, you can keep using them in DirectX 11 and UWP.

Our templates and code samples use new C++ features that you might not be familiar with yet. For example, asynchronous methods are used with [**lambda expressions**](/cpp/cpp/lambda-expressions-in-cpp) to load Direct3D resources without blocking the UI thread.

There are two concepts you'll use often:

-   Managed references ([**^ operator**](/cpp/windows/handle-to-object-operator-hat-cpp-component-extensions)) and [**managed classes**](/cpp/windows/classes-and-structs-cpp-component-extensions) (ref classes) are a fundamental part of the Windows Runtime. You will need to use managed ref classes to interface with Windows Runtime components, for example [**IFrameworkView**](/uwp/api/Windows.ApplicationModel.Core.IFrameworkView) (more on that in the walkthrough).
-   When working with Direct3D 11 COM interfaces, use the [**Microsoft::WRL::ComPtr**](/cpp/windows/comptr-class) template type to make COM pointers easier to use.

 

 