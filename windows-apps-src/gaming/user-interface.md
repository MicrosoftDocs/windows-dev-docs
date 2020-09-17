---
title: DirectX game project templates
description: Learn about the templates for creating a Universal Windows Platform (UWP) and DirectX game.
ms.assetid: 41b6cd76-5c9a-e2b7-ef6f-bfbf6ef7331d
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, directx, templates
ms.localizationpriority: medium
---
# DirectX game project templates



The DirectX and Universal Windows Platform (UWP) templates allow you to quickly create a project as a starting point for your game.

## Prerequisites


To create the project you need to:

-   [Download Microsoft Visual Studio 2015](https://visualstudio.microsoft.com/vs/). Visual Studio 2015 has tools for graphics programming, such as debugging tools. For an overview of DirectX graphics and gaming features and tools, see [Visual Studio tools for DirectX game development](set-up-visual-studio-for-game-development.md).

## Choosing a template


Visual Studio 2015 includes three DirectX and UWP templates:

-   DirectX 11 App (Universal Windows) - The DirectX 11 App (Universal Windows) template creates a UWP project, which renders directly to an app window using DirectX 11.
-   DirectX 12 App (Universal Windows) - The DirectX 12 App (Universal Windows) template creates a project UWP, which renders directly to an app window using DirectX 12.
-   DirectX 11 and XAML App (Universal Windows) - The DirectX 11 and XAML App (Universal Windows) template creates a UWP project, which renders inside a XAML control using DirectX 11. This template uses a [**SwapChainPanel**](/uwp/api/Windows.UI.Xaml.Controls.SwapChainPanel), so you can use XAML UI controls. This can make adding user interface elements easier, but using the XAML template may result in lower performance.

Which template you choose depends on the performance and what technologies you want to use.

## Template structure


The DirectX Universal Windows templates contain the following files:

-   pch.h and pch.cpp - Precompiled header support.
-   Package.appxmanifest - The properties of the deployment package for the app.
-   \*.pfx - Certificates for the application.
-   External Dependencies - Links to external files the project use.s
-   \*Main.h and \*Main.cpp - Methods for managing application assets, updating application state, and rendering the frame.
-   App.h and App.cpp - Main entry point for the application. Connects the app with the Windows shell and handles application lifecycle events. These files only appear in the DirectX 11 App (Universal Windows) and DirectX 12 App (Universal Windows) templates.
-   App.xaml, App.xaml.cpp, and App.xaml.h - Main entry point for the application. Connects the app with the Windows shell and handles application lifecycle events. These files only appear in the DirectX 11 and XAML App (Universal Windows) template.
-   DirectXPage.xaml, DirectXPage.xaml.cpp, and DirectXPage.xaml.h - A page that hosts a DirectX SwapChainPanel. These files only appear in the DirectX 11 and XAML App (Universal Windows) template.
-   Content
    -   Sample3DSceneRenderer.h and Sample3DSceneRenderer.cpp - A sample renderer that instantiates a basic rendering pipeline.
    -   SampleFpsTextRenderer.h and SampleFpsTextRenderer.cpp - Renders the current FPS value in the bottom right corner of the screen using Direct2D and DirectWrite. These files only appear in the DirectX 11 App (Universal Windows) and DirectX 11 and XAML App (Universal Windows) templates.
    -   SamplePixelShader.hlsl - A simple example pixel shader.
    -   SampleVertexShader.hlsl - A simple example vertex shader.
    -   ShaderStructures.h - Structures used to send date to the example vertex shader.
-   Common
    -   StepTimer.h - A helper class for animation and simulation timing.
    -   DirectXHelper.h - Misc Helper functions.
    -   DeviceResources.h and Device Resources.cpp - Provides an interface for an application that owns DeviceResources to be notified of the device being lost or created.
    -   d3dx12.h - Contains the D3DX12 utility library. This file only appears in the DirectX 12 App (Universal Windows).
-   Assets - Logo and splashscreen images used by the application.

## Next steps


Now that you have a starting point, add to it to build your game development knowledge and Microsoft Store game development skills.

If you are porting an existing game, see the following topics.

-   [Port from OpenGL ES 2.0 to Direct3D 11.1](port-from-opengl-es-2-0-to-directx-11-1.md)
-   [Port from DirectX 9 to Universal Windows Platform](porting-your-directx-9-game-to-windows-store.md)

If you are creating a new DirectX game, see the following topics.

-   [Create a simple UWP game with DirectX](tutorial--create-your-first-uwp-directx-game.md)
-   [Developing Marble Maze, a Universal Windows Platform game in C++ and DirectX](developing-marble-maze-a-windows-store-game-in-cpp-and-directx.md)