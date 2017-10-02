---
author: joannaleecy
title: Create a simple Universal Windows Platform (UWP) game with DirectX
description: Learn how to create a basic Universal Windows Platform (UWP) game with DirectX and C++.
ms.assetid: 9edc5868-38cf-58cc-1fb3-8fb85a7ab2c9
keywords: DirectX game sample, game sample, Universal Windows Platform (UWP), Direct3D 11 game
ms.author: joanlee
ms.date: 04/07/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
---

# Create a simple UWP game with DirectX



This set of tutorials is a code walkthrough of a simple UWP DirectX game sample. In this walkthrough, you'll learn how to create a simple UWP game using DirectX and C++.

This first person 3D game is set at a shooting gallery where the player is shooting at moving targets. Hitting each target awards a set number of points. There are 6 levels with increasing difficulty that the player can progress through. At the end of the levels, the points are tallied, and the player is awarded a final score.

## Requirements

This walkthrough is designed for developers who have some basic graphics programming experience. For more info about the APIs and concepts used in this sample, go to [Reference](#reference).

## Objectives

* Understand key UWP DirectX game development concepts, techniques, and considerations
* Learn about the major parts of a UWP DirectX game, including loading assets such as meshes, creating a main game loop, implementing a simple rendering pipeline, and adding sound and controls

Note that this sample is not a complete end-to-end game as the focus here is on UWP DirectX game development concepts and to call out Windows Runtime specific considerations around these concepts.

## Getting started

* Get the latest version of [Visual Studio](https://www.visualstudio.com/downloads/). For more info on getting set up for UWP development, see [Get set up](https://docs.microsoft.com/windows/uwp/get-started/get-set-up).
* [Download the latest sample game code](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Simple3DGameDX). Note that this sample is part of a large collection of UWP feature samples. If you need instructions on how to download the sample, see [Get the UWP samples from GitHub](https://docs.microsoft.com/windows/uwp/get-started/get-uwp-app-samples).
* To run the sample, go to the folder where you've cloned or downloaded the repo. Then go to __Samples__ > __Simple3DGameDX__ > __cpp__ and open the __Simple3DGameDX.sln__ in Visual Studio.

The screenshot below shows the game in action:

![the game sample in action](images/simple-dx-game-overview.png)

## Walkthrough

The sample demonstrates the following game concepts:

-   Interoperation between DirectX 11.1 and the Windows Runtime
-   A first-person 3D perspective and camera
-   Stereoscopic 3D effects
-   Collision detection between objects in 3D
-   Handling player input from mouse, touch, and Xbox controller controls
-   Audio mixing and playback
-   A basic game state machine

| Topic | Description |
|---------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| [Set up the game project](tutorial--setting-up-the-games-infrastructure.md) | This section covers the game project set up and configuration process. Learn how to use the right template in Visual Studio to create the project and how to configure it for UWP DirectX game development. This helps to minimize the amount of infrastructure code work that you need to do. |
| [Define the game's UWP app framework](tutorial--building-the-games-metro-style-app-framework.md) | This section explains how the sample game is structured and how to define the high-level state machine to enable player and system interaction. Learn how to build the framework to allow the game object to interact with Windows. This includes learning about Windows Runtime properties like suspend/resume event handling, window focus, and snapping. Learn how to use events, interactions, and transitions to manage the user interface. |
| [Define the main game object](tutorial--defining-the-main-game-loop.md) | This section dives into the details of the game sample's main object. Understand how implemented rules translate into interactions with the game world. |
| [Assemble the rendering framework](tutorial--assembling-the-rendering-pipeline.md) | This section shows how the sample game uses the structure and state created in the previous sections to display its graphics. Learn how to implement a rendering framework; starting from the initialization of the graphics device through the presentation of the graphics objects for display. |
| [Add a user interface](tutorial--adding-a-user-interface.md) | This section focuses on providing game state feedback to the player. Learn how to add simple menu options and heads-up display (HUD) components on top of 3-D graphics pipeline output. |
| [Add controls](tutorial--adding-controls.md) |This section looks at how the game sample implements move-look controls in a 3-D game. Learn how to add basic touch, mouse, and game controller controls. |
| [Add sound](tutorial--adding-sound.md) | This section examines how the sample creates an object for sound playback using the [XAudio2](https://msdn.microsoft.com/library/windows/desktop/ee415813) APIs. |
| [Extend the game sample](tutorial-resources.md) | This section explains the differences in creating the game UI by using XAML instead of Direct2D. Learn how to extend this sample using [DirectX and XAML interop](directx-and-xaml-interop.md).|

## Reference

To learn about the graphic concepts that Direct3D is built on, go to [Direct3D Graphics Learning Guide](https://docs.microsoft.com/windows/uwp/graphics-concepts/).

To understand the APIs used in this sample, refer to the following topics:
* [DirectX Graphics and Gaming](https://msdn.microsoft.com/library/windows/apps/ee663274)
* [Programming Guide for Direct3D 11](https://msdn.microsoft.com/library/windows/apps/ff476345)
* [How to use Direct3D 11](https://msdn.microsoft.com/library/windows/desktop/hh404569)
* [Direct3D 11 Reference](https://msdn.microsoft.com/library/windows/apps/ff476147)
* [DXGI reference](https://msdn.microsoft.com/library/windows/apps/bb205169)
* [XAudio2](https://msdn.microsoft.com/library/windows/apps/hh405049)
* [Windows.Gaming.Input](https://docs.microsoft.com/uwp/api/Windows.Gaming.Input)
* [Direct2D](https://msdn.microsoft.com/library/windows/apps/dd370990.aspx) 
 
