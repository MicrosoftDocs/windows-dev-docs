---
title: Create a DirectX Universal Windows Platform (UWP) game
description: In this set of tutorials, you'll learn how to use DirectX and [C++/WinRT](../cpp-and-winrt-apis/index.md) to create the basic Universal Windows Platform (UWP) sample game named **Simple3DGameDX**.
ms.assetid: 9edc5868-38cf-58cc-1fb3-8fb85a7ab2c9
keywords: DirectX sample game, sample game, Universal Windows Platform (UWP), Direct3D 11 game
ms.date: 06/24/2020
ms.topic: article
ms.localizationpriority: medium
---

# Create a simple Universal Windows Platform (UWP) game with DirectX

In this set of tutorials, you'll learn how to use DirectX and [C++/WinRT](../cpp-and-winrt-apis/index.md) to create the basic Universal Windows Platform (UWP) sample game named **Simple3DGameDX**. The gameplay takes place in a simple first-person 3D shooting gallery.

> [!NOTE]
> The link from which you can download the **Simple3DGameDX** sample game itself is [Direct3D sample game](/samples/microsoft/windows-universal-samples/simple3dgamedx/). The C++/WinRT source code is in the folder named `cppwinrt`. For info about other UWP sample apps, see [Get UWP app samples](../get-started/get-app-samples.md).

These tutorials cover all of the major parts of a game, including the processes for loading assets such as arts and meshes, creating a main game loop, implementing a simple rendering pipeline, and adding sound and controls.

You'll also see UWP game development techniques and considerations. We'll focus on key UWP DirectX game development concepts, and call out Windows-Runtime-specific considerations around those concepts.

## Objective

To learn about the basic concepts and components of a UWP DirectX game, and to become more comfortable designing UWP games with DirectX.

## What you need to know

For this tutorial, you need to be familiar with these subjects.

- [C++/WinRT](../cpp-and-winrt-apis/index.md). C++/WinRT is a standard modern C++17 language projection for Windows Runtime (WinRT) APIs, implemented as a header-file-based library, and designed to provide you with first-class access to the modern Windows APIs.
- Basic linear algebra and Newtonian physics concepts.
- Basic graphics programming terminology.
- Basic Windows programming concepts.
- Basic familiarity with the [Direct2D](/windows/desktop/Direct2D/direct2d-portal) and [Direct3D 11](/windows/desktop/direct3d11/how-to-use-direct3d-11) APIs.

##  Direct3D UWP shooting gallery sample

The **Simple3DGameDX** sample game implements a simple first-person 3D shooting gallery, where the player fires balls at moving targets. Hitting each target awards a set number of points, and the player can progress through 6 levels of increasing challenge. At the end of the levels, the points are tallied, and the player is awarded a final score.

The sample demonstrates these game concepts.

- Interoperation between DirectX 11.1 and the Windows Runtime
- A first-person 3D perspective and camera
- Stereoscopic 3D effects
- Collision-detection between objects in 3D
- Handling player input for mouse, touch, and Xbox controller controls
- Audio mixing and playback
- A basic game state-machine

![the sample game in action](images/simple-dx-game-overview.png)

|Topic|Description|
|-------|-------------|
|[Set up the game project](tutorial--setting-up-the-games-infrastructure.md)|The first step in developing your game is to set up a project in Microsoft Visual Studio. After you've configured a project specifically for game development, you could later re-use it as a kind of template.|
|[Define the game's UWP app framework](tutorial--building-the-games-uwp-app-framework.md)|The first step in coding a Universal Windows Platform (UWP) game is building the framework that lets the app object interact with Windows.|
|[Game flow management](tutorial-game-flow-management.md)|Define the high-level state machine to enable player and system interaction. Learn how UI interacts with the overall game's state machine and how to create event handlers for UWP games.|
|[Define the main game object](tutorial--defining-the-main-game-loop.md)|Now, we look at the details of the sample game's main object and how the rules it implements translate into interactions with the game world.|
|[Rendering framework I: Intro to rendering](tutorial--assembling-the-rendering-pipeline.md)|Learn how to develop the rendering pipeline to display graphics. Intro to rendering.|
|[Rendering framework II: Game rendering](tutorial-game-rendering.md)|Learn how to assemble the rendering pipeline to display graphics. Game rendering, set up and prepare data.|
|[Add a user interface](tutorial--adding-a-user-interface.md)|Learn how to add a 2D user interface overlay to a DirectX UWP game.|
|[Add controls](tutorial--adding-controls.md)|Now, we take a look at how the sample game implements move-look controls in a 3-D game, and how to develop basic touch, mouse, and game controller controls.|
|[Add sound](tutorial--adding-sound.md)|Develop a simple sound engine using XAudio2 APIs to playback game music and sound effects.|
|[Extend the sample game](tutorial-resources.md)|Learn how to implement a XAML overlay for a UWP DirectX game.|