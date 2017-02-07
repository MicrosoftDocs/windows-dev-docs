---
author: mtoepke
title: Games and DirectX
description: Universal Windows Platform (UWP) offers new opportunities to create, distribute, and monetize games. Learn about starting a new game or porting an existing game.
ms.assetid: 4073b835-c900-4ff2-9fc5-da52f9432a1f
ms.author: mtoepke
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, games, directx
---

# Games and DirectX


\[ Updated for UWP apps on Windows 10. For Windows 8.x articles, see the [archive](http://go.microsoft.com/fwlink/p/?linkid=619132) \]

Universal Windows Platform (UWP) offers new opportunities to create, distribute, and monetize games. Learn about starting a new game or porting an existing game.

| Topic | Description |
|---------------------------------------------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Windows 10 game development guide](e2e.md) | An end-to-end guide to resources and information for developing UWP games. |
| [Game technologies for Universal Windows Platform apps](game-development-platform-guide.md) | In this guide, you'll learn about the technologies available for developing UWP games. |
| [Project templates and tools for games](prepare-your-dev-environment-for-windows-store-directx-game-development.md) | Shows you what you need to start programming DirectX games for the UWP. |
| [The app object and DirectX](about-the-metro-style-user-interface-and-directx.md) | UWP with DirectX games don't use many of the Windows UI user interface elements and objects. Rather, because they run at a lower level in the Windows Runtime stack, they must interoperate with the user interface framework in a more fundamental way: by accessing and interoperating with the app object directly. Learn when and how this interoperation occurs, and how you, as a DirectX developer, can effectively use this model in the development of your UWP app. |
| [Launching and resuming apps](launching-and-resuming-apps-directx-and-cpp.md) | Learn how to launch, suspend, and resume your UWP DirectX app. |
| [2D graphics for DirectX games](working-with-2d-graphics-in-your-directx-game.md) | We discuss the use of 2D bitmap graphics and effects, and how to use them in your game. |
| [Basic 3D graphics for DirectX games](an-introduction-to-3d-graphics-with-directx.md) | We show how to use DirectX programming to implement the fundamental concepts of 3D graphics. |
| [Load resources in your DirectX game](load-a-game-asset.md) | Most games, at some point, load resources and assets (such as shaders, textures, predefined meshes or other graphics data) from local storage or some other data stream. Here, we walk you through a high-level view of what you must consider when loading these files to use in your UWP game. |
| [Create a simple UWP game with DirectX](tutorial--create-your-first-metro-style-directx-game.md) | In this set of tutorials, you learn how to create a basic UWP game with DirectX and C++. We cover all the major parts of a game, including the processes for loading assets such as arts and meshes, creating a main game loop, implementing a simple rendering pipeline, and adding sound and controls. |
| [Developing Marble Maze, a Universal Windows Platform game in C++ and DirectX](developing-marble-maze-a-windows-store-game-in-cpp-and-directx.md) | This section of the documentation describes how to use DirectX and Visual C++ to create a 3-D UWP game. This documentation shows how to create a 3-D game named Marble Maze that embraces new form factors such as tablets and also works on traditional desktop and laptop PCs. |
| [Supporting screen orientation](supporting-screen-rotation-directx-and-cpp.md) | Here, we'll discuss best practices for handling screen rotation in your UWP DirectX app, so that the Windows 10 device's graphics hardware are used efficiently and effectively. |
| [Audio for games](working-with-audio-in-your-directx-game.md) | Learn how to develop and incorporate music and sounds into your DirectX game, and how to process the audio signals to create dynamic and positional sounds. |
| [Touch controls for games](tutorial--adding-touch-controls-to-your-directx-game.md) | Learn how to add basic touch controls to your UWP C++ game with DirectX. We show you how to add touch-based controls to move a fixed-plane camera in a Direct3D environment, where dragging with a finger or stylus shifts the camera perspective. |
| [Move-look controls for games](tutorial--adding-move-look-controls-to-your-directx-game.md) | Learn how to add traditional mouse and keyboard move-look controls (also known as mouselook controls) to your DirectX game. |
| [Relative mouse movement](relative-mouse-movement.md) | Learn how to add relative mouse controls, which don't use the system cursor and don't return absolute screen coordinates; instead, they track the pixel delta between mouse movements. |
| [Optimize input and rendering loop](optimize-performance-for-windows-store-direct3d-11-apps-with-coredispatcher.md) | Input latency can significantly impact the experience of a game, and optimizing it can make a game feel more polished. Additionally, proper input event optimization can improve battery life. Learn how to choose the right [CoreDispatcher](optimize-performance-for-windows-store-direct3d-11-apps-with-coredispatcher.md) input event processing options to make sure your game handles input as smoothly as possible. |
| [Swap chain scaling and overlays](multisampling--scaling--and-overlay-swap-chains.md) | Learn how to create scaled swap chains for faster rendering on mobile devices, and use overlay swap chains (when available) to increase the visual quality. |
| [Reduce latency with DXGI 1.3 swap chains](reduce-latency-with-dxgi-1-3-swap-chains.md) | Use DXGI 1.3 to reduce the effective frame latency by waiting for the swap chain to signal the appropriate time to begin rendering a new frame. |
| [Multisampling in UWP apps](multisampling--multi-sample-anti-aliasing--in-windows-store-apps.md) | Learn how to use multisampling in UWP apps built with Direct3D. |
| [Handle device removed scenarios in Direct3D 11](handling-device-lost-scenarios.md) | This topic explains how to recreate the Direct3D and DXGI device interface chain when the graphics adapter is removed or reinitialized. |
| [Asynchronous programming for games](asynchronous-programming-directx-and-cpp.md) | This topic covers various points to consider when you are using asynchronous programming and threading with DirectX. |
| [Networking for games](work-with-networking-in-your-directx-game.md) | Learn how to develop and incorporate networking features into your DirectX game. |
| [Accessibility for games](accessibility-for-games.md) | Learn how to make games more accessible. |
| [Cloud for games](cloud-for-games.md) | Learn how to make use of cloud technologies for game development. |
| [Monetization for games](monetization-for-games.md) | Learn how your game can be monetized. |
| [DirectX and XAML interop](directx-and-xaml-interop.md) | You can use Extensible Application Markup Language (XAML) and Microsoft DirectX together in your UWP game. |
| [Package your game](package-your-windows-store-directx-game.md) | Larger UWP games, especially those that support multiple languages with region-specific assets or feature optional high-definition assets, can easily balloon to large sizes. In this topic, learn how to use app packages and app bundles to customize your app so that your customers only receive the resources they actually need. |
| [Concept approval](concept-approval.md) | Learn how to submit your product for concept approval, which you will need if your product runs on Xbox or uses Xbox Live. |
| [Game porting guides](porting-guides.md) | Provides guides for porting your existing games to Direct3D 11, UWP, and Windows 10. |
| [Game programming resources](additional-directx-game-programming-resources.md) | For more info about game programming on Windows, check out the following resources. |

 

> **Note**  
This article is for Windows 10 developers writing Universal Windows Platform (UWP) apps. If you’re developing for Windows 8.x or Windows Phone 8.x, see the [archived documentation](http://go.microsoft.com/fwlink/p/?linkid=619132).

 

To make the best use of the game development overviews and tutorials, you should be familiar with the following subjects:

-   Microsoft C++ with Component Extensions (C++/CX). This is an update to Microsoft C++ that incorporates automatic reference counting, and is the language for developing UWP games with DirectX 11.1 or later versions.
-   Basic graphics programming terminology.
-   Basic Windows programming concepts.
-   Basic familiarity with the Direct3D 9 or 11 APIs.

 

 




