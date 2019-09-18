---
title: Define the main game object
description: Now, we look at the details of the game sample's main object and how the rules it implements translate into interactions with the game world.
ms.assetid: 6afeef84-39d0-cb78-aa2e-2e42aef936c9
ms.date: 10/24/2017
ms.topic: article
keywords: windows 10, uwp, games, main object
ms.localizationpriority: medium
---
# Define the main game object

Once you’ve laid out the basic framework of the sample game and implemented a state machine that handles the high-level user and system behaviors, you’ll want to examine the rules and mechanics that turn the game sample into a game. Let’s look at the details of the game sample's main object, and how to translate game rules into interactions with the game world.

>[!Note]
>If you haven't downloaded the latest game code for this sample, go to [Direct3D game sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Simple3DGameDX). This sample is part of a large collection of UWP feature samples. For instructions on how to download the sample, see [Get the UWP samples from GitHub](https://docs.microsoft.com/windows/uwp/get-started/get-uwp-app-samples).

## Objective

Learn how to apply basic development techniques to implement game rules and mechanics for a UWP DirectX game.

## Main game object

In this sample game, __Simple3DGame__ is the main game object class. An instance of __Simple3DGame__ object is constructed in the __App::Load__ method.

The __Simple3DGame__ class object:
* Specifies implementation of the gameplay logic
* Contains methods that communicate:
    * Changes in the game state to the state machine defined in the app framework.
    * Changes in the game state from the app to the game object itself.
    * Details for updating the game's UI (overlay and heads-up display), animations, and physics (the dynamics).

    >[!Note]
    >Updating of graphics is handled by the __GameRenderer__ class, which contains methods to obtain and use graphics device resources used by the game. For more info, see [Rendering framework I: Intro to rendering](tutorial--assembling-the-rendering-pipeline.md).

* Serves as a container for the data that defines a game session, level, or lifetime, depending on how you define your game at a high level. In this case, the game state data is for the lifetime of the game, and is initialized one time when a user launches the game.

To view methods and data defined in this class object, go to [Simple3DGame object](#simple3dgame-object).

## Initialize and start the game

When a player starts the game, the game object must initialize its state, create and add the overlay, set the variables that track the player's performance, and instantiate the objects that it will use to build the levels. In this sample, this is done when the new __GameMain__ instance is created in [__App::Load__](https://github.com/Microsoft/Windows-universal-samples/blob/5f0d0912214afc1c2a7c7470203933ddb46f7c89/Samples/Simple3DGameDX/cpp/App.cpp#L115-L123). 

The game object, __Simple3DGame__, is created in the __GameMain__ constructor. It is then initialized using the [__Simple3DGame::Initialize__](https://github.com/Microsoft/Windows-universal-samples/blob/5f0d0912214afc1c2a7c7470203933ddb46f7c89/Samples/Simple3DGameDX/cpp/Simple3DGame.cpp#L54-L250) method during the [async create task in the __GameMain__ constructor](https://github.com/Microsoft/Windows-universal-samples/blob/5f0d0912214afc1c2a7c7470203933ddb46f7c89/Samples/Simple3DGameDX/cpp/GameMain.cpp#L65-L74).

### Simple3DGame::Initialize method

The game sample sets up the following components in the game object:

* A new audio playback object is created.
* Arrays for the game's graphic primitives are created, including arrays for the level primitives, ammo, and obstacles.
* A location for saving game state data is created, named *Game*, and placed in the app data settings storage location specified by [**ApplicationData::Current**](https://docs.microsoft.com/uwp/api/windows.storage.applicationdata.current).
* A game timer and the initial in-game overlay bitmap are created.
* A new camera is created with a specific set of view and projection parameters.
* The input device (the controller) is set to the same starting pitch and yaw as the camera, so the player has a 1-to-1 correspondence between the starting control position and the camera position.
* The player object is created and set to active. We use a sphere object to detect the player's proximity to walls and obstacles and to keep the camera from getting placed in a position that might break immersion.
* The game world primitive is created.
* The cylinder obstacles are created.
* The targets (**Face** objects) are created and numbered.
* The ammo spheres are created.
* The levels are created.
* The high score is loaded.
* Any prior saved game state is loaded.

The game now has instances of all the key components: the world, the player, the obstacles, the targets, and the ammo spheres. It also has instances of the levels, which represent configurations of all of the above components and their behaviors for each specific level. Let's see how the game builds the levels.

## Build and load game levels

Most of the heavy lifting for the level construction is done in the __Level.h/.cpp__ files found in the __GameLevels__ folder of the sample solution. Because it focuses on a very specific implementation, we won't be covering them here. The important thing is that the code for each level is run as a separate __LevelN__ object. If you'd like to extend the game, you can create a **Level** object that takes an assigned number as a parameter and randomly places the obstacles and targets. Or, you can have it load level configuration data from a resource file, or even the Internet.

## Define the game play

At this point, we have all the components we need to assemble the game. The levels have been constructed in memory from the primitives, and are ready for the player to start interacting with.

Tthe best games react instantly to player input, and provide immediate feedback. This is true for any type of a game, from twitch-action, real-time First-person shooters to thoughtful, turn-based strategy games.

### Simple3DGame::RunGame method

When playing a level, the game is in the __Dynamics__ state. 

[__GameMain::Update__](https://github.com/Microsoft/Windows-universal-samples/blob/5f0d0912214afc1c2a7c7470203933ddb46f7c89/Samples/Simple3DGameDX/cpp/GameMain.cpp#L261-L329) is the main update loop that updates the application state once per frame as shown below. In the update loop, it calls the [__Simple3DGame::RunGame__](https://github.com/Microsoft/Windows-universal-samples/blob/5f0d0912214afc1c2a7c7470203933ddb46f7c89/Samples/Simple3DGameDX/cpp/Simple3DGame.cpp#L337-L418) method to handle the work if the game is in the __Dynamics__ state.

```cpp
// Updates the application state once per frame.
void GameMain::Update()
{
    m_controller->Update(); //the controller instance has its own update loop.

    switch (m_updateState)
    {
        //...

    case UpdateEngineState::Dynamics:
        if (m_controller->IsPauseRequested())
        {
            //...
        }
        else
        {
            GameState runState = m_game->RunGame(); //when playing a level, the game is in the Dynamics state. Work is handled by Simple3DGame::RunGame method.
            switch (runState)
            {
                
      //...
```
          
[__Simple3DGame::RunGame__](https://github.com/Microsoft/Windows-universal-samples/blob/5f0d0912214afc1c2a7c7470203933ddb46f7c89/Samples/Simple3DGameDX/cpp/Simple3DGame.cpp#L337-L418) handles the set of data that defines the current state of the game play for the current iteration of the game loop.

Game flow logic in __RunGame__:
*  The method updates the timer that counts down the seconds until the level is completed, and tests to see if the level's time has expired. This is one of the rules of the game: when time runs out and all the targets have not been shot, it's game over.
*  If time has run out, the method sets the **TimeExpired** game state, and returns to the **Update** method in the previous code.
*  If time remains, the move-look controller is polled for an update to the camera position; specifically, an update to the angle of the view normal projecting from the camera plane (where the player is looking), and the distance that angle has moved from the previous time the controller was polled.
*  The camera is updated based on the new data from the move-look controller.
*  The dynamics, or the animations and behaviors of objects in the game world independent of player control, are updated. In this game sample, the [__UpdateDynamics()__](https://github.com/Microsoft/Windows-universal-samples/blob/5f0d0912214afc1c2a7c7470203933ddb46f7c89/Samples/Simple3DGameDX/cpp/Simple3DGame.cpp#L436-L856) method is called to update the motion of the ammo spheres that have been fired, the animation of the pillar obstacles and the movement of the targets. For more information, see [Update the game world](#update-the-game-world).
*  The method checks to see if the criteria for the successful completion of a level have been met. If so, it finalizes the score for the level and checks to see if this is the last level (of 6). If it's the last level, the method returns the **GameComplete** game state; otherwise, it returns the __LevelComplete__ game state.
*  If the level isn't complete, the method sets the game state to __Active__ and returns.

## Update the game world

In this sample, when the game is running, the [__Simple3DGame::UpdateDynamics()__](https://github.com/Microsoft/Windows-universal-samples/blob/5f0d0912214afc1c2a7c7470203933ddb46f7c89/Samples/Simple3DGameDX/cpp/Simple3DGame.cpp#L436-L856) method is called from the [__Simple3DGame::RunGame__](https://github.com/Microsoft/Windows-universal-samples/blob/5f0d0912214afc1c2a7c7470203933ddb46f7c89/Samples/Simple3DGameDX/cpp/Simple3DGame.cpp#L337-L418) method (which is called from [__GameMain::Update__](https://github.com/Microsoft/Windows-universal-samples/blob/5f0d0912214afc1c2a7c7470203933ddb46f7c89/Samples/Simple3DGameDX/cpp/GameMain.cpp#L261-L329)) to update objects that are rendered in a game scene.

In the __UpdateDynamics__ loop, call methods that are used to set the game world in motion, independent of the player input, create an immersive game experience and make the level come *alive*. This includes graphics that needs to be rendered and running animation loops to bring about a living, breathing world even when there's no player input. For example, trees swaying in the wind, waves cresting along shore lines, machinery smoking, and alien monsters stretching and moving around. It also encompasses the interaction between objects, including collisions between the player sphere and the world, or between the ammo and the obstacles and targets.

The game loop should always keep updating the game world whether it's based on game logic, physical algorithms, or whether it's just plain random, except when the game is specifically paused. 

In the game sample, this principle is called *dynamics*, and it encompasses the rise and fall of the pillar obstacles, and the motion and physical behaviors of the ammo spheres as they are fired. 

### Simple3DGame::UpdateDynamics method 

This method deals with four sets of computations:

* The positions of the fired ammo spheres in the world.
* The animation of the pillar obstacles.
* The intersection of the player and the world boundaries.
* The collisions of the ammo spheres with the obstacles, the targets, other ammo spheres, and the world.

The animation of the obstacles is a loop defined in **Animate.h/.cpp**. The behavior of the ammo and any collisions are defined by simplified physics algorithms, supplied in the code and parameterized by a set of global constants for the game world, including gravity and material properties. This is all computed in the game world coordinate space.

### Review the flow

Now that we've updated all the objects in the scene and calculated any collisions, we need to use this info to draw the corresponding visual changes. 

After __GameMain::Update()__ completes the current iteration of the game loop, the sample immediately calls __Render()__ to take the updated object data and generate a new scene to present to the player, as shown here. Next, let's take a look at the rendering.

```cpp
void GameMain::Run()
{
    while (!m_windowClosed)
    {
        if (m_visible)
        {
            switch (m_updateState)
            {
            case UpdateEngineState::Deactivated:
            case UpdateEngineState::TooSmall:
                // ...
                // otherwise fall through and do normal processing to get the rendering handled.
            default:
                CoreWindow::GetForCurrentThread()->Dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessAllIfPresent);
                Update(); // GameMain::Update calls Simple3DGame::RunGame() if game is in Dynamics state, uses Simple3DGame::UpdateDynamics() to update game world.
                m_renderer->Render(); //Render() is called immediately after the Update() loop
                m_deviceResources->Present();
                m_renderNeeded = false;
            }
        }
        else
        {
            CoreWindow::GetForCurrentThread()->Dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessOneAndAllPending);
        }
    }
    m_game->OnSuspending();  // exiting due to window close.  Make sure to save state.
}
```

## Render the game world's graphics

We recommend that the graphics in a game update as often as possible, which, at maximum, is every time the main game loop iterates. As the loop iterates, the game is updated, with or without player input. This allows the animations and behaviors that are calculated to be displayed smoothly. Imagine if we had a simple scene of water that only moved when the player pressed a button. That would make for terribly boring visuals. A good game looks smooth and fluid.

Recall the sample game's loop as shown above in [__GameMain::Run__](https://github.com/Microsoft/Windows-universal-samples/blob/5f0d0912214afc1c2a7c7470203933ddb46f7c89/Samples/Simple3DGameDX/cpp/GameMain.cpp#L143-L202). If the game's main window is visible, and isn't snapped or deactivated, the game continues to update and render the results of that update. The [__Render__](https://github.com/Microsoft/Windows-universal-samples/blob/5f0d0912214afc1c2a7c7470203933ddb46f7c89/Samples/Simple3DGameDX/cpp/GameRenderer.cpp#L474-L624) method we're examining now renders a representation of that state. This is done immediately after a call to **Update**, which includes **RunGame** to update states, which was discussed in the previous section.

This method draws the projection of the 3D world, and then draws the Direct2D overlay on top of it. When completed, it presents the final swap chain with the combined buffers for display.

>[!Note]
>There are two states for the sample game's Direct2D overlay: one where the game displays the game info overlay that contains the bitmap for the pause menu, and one where the game displays the cross hairs along with the rectangles for the touchscreen move-look controller. The score text is drawn in both states. For more information, see [Rendering framework I: Intro to rendering](tutorial--assembling-the-rendering-pipeline.md).

### GameRenderer::Render method

```cpp
void GameRenderer::Render()
{
    bool stereoEnabled = m_deviceResources->GetStereoState();

    auto d3dContext = m_deviceResources->GetD3DDeviceContext();
    auto d2dContext = m_deviceResources->GetD2DDeviceContext();
   
        // ...
        if (m_game != nullptr && m_gameResourcesLoaded && m_levelResourcesLoaded)
        {
            // This section is only used after the game state has been initialized and all device
            // resources needed for the game have been created and associated with the game objects.
            //...
            auto objects = m_game->RenderObjects();
            for (auto object = objects.begin(); object != objects.end(); object++)
            {
                (*object)->Render(d3dContext, m_constantBufferChangesEveryPrim.Get()); // Renders the 3D objects
            }

        //...
        d3dContext->BeginEventInt(L"D2D BeginDraw", 1);
        d2dContext->BeginDraw(); //Start drawing the overlays
        
        // To handle the swapchain being pre-rotated, set the D2D transformation to include it.
        d2dContext->SetTransform(m_deviceResources->GetOrientationTransform2D());

        if (m_game != nullptr && m_gameResourcesLoaded)
        {
            // This is only used after the game state has been initialized.
            m_gameHud->Render(m_game); // Renders number of hits, shots, and time
        }

        if (m_gameInfoOverlay->Visible())
        {
            d2dContext->DrawBitmap(     // Renders the game overlay
                m_gameInfoOverlay->Bitmap(),
                m_gameInfoOverlayRect
                );
        }
        //...
    }
}
```

## Simple3DGame object

These are the methods and data that are defined in the __Simple3DGame__ object class.

### Methods

The internal methods defined on **Simple3DGame** include:

-   **Initialize**: Sets the starting values of the global variables and initializes the game objects. This is covered in the [Initialize and start the game](#initialize-and-start-the-game) section.
-   **LoadGame**: Initializes a new level and starts loading it.
-   **LoadLevelAsync**: Starts an async task (if you're unfamiliar with async tasks, see [Parallel Patterns Library](https://docs.microsoft.com/cpp/parallel/concrt/parallel-patterns-library-ppl)) to initialize the level and then invoke an async task on the renderer to load the device specific level resources. This method runs in a separate thread; as a result, only [**ID3D11Device**](https://docs.microsoft.com/windows/desktop/api/d3d11/nn-d3d11-id3d11device) methods (as opposed to [**ID3D11DeviceContext**](https://docs.microsoft.com/windows/desktop/api/d3d11/nn-d3d11-id3d11devicecontext) methods) can be called from this thread. Any device context methods are called in the **FinalizeLoadLevel** method.
-   **FinalizeLoadLevel**: Completes any work for level loading that needs to be done on the main thread. This includes any calls to Direct3D 11 device context ([**ID3D11DeviceContext**](https://docs.microsoft.com/windows/desktop/api/d3d11/nn-d3d11-id3d11devicecontext)) methods.
-   **StartLevel**: Starts the game play for a new level.
-   **PauseGame**: Pauses the game.
-   **RunGame**: Runs an iteration of the game loop. It's called from **App::Update** one time every iteration of the game loop if the game state is **Active**.
-   **OnSuspending** and **OnResuming**: Suspends and resumes the game's audio, respectively.

And the private methods:

-   **LoadSavedState** and **SaveState**: Loads and saves the current state of the game, respectively.
-   **SaveHighScore** and **LoadHighScore**: Saves and loads the high score across games, respectively.
-   **InitializeAmmo**: Resets the state of each sphere object used as ammunition back to its original state for the beginning of each round.
-   **UpdateDynamics**: This is an important method, because it updates all the game objects based on canned animation routines, physics, and control input. This is the heart of the interactivity that defines the game. This is covered in the [Update the game world](#update-the-game-world) section.

The other public methods are property getters that return game play and overlay specific information to the app framework for display.

### Data

At the top of the code example, there are four objects whose instances are updated as the game loop runs.

-   **MoveLookController** object: Represents the player input. For more information, see [Adding controls](tutorial--adding-controls.md).
-   **GameRenderer** object: Represents the Direct3D 11 renderer derived from the **DirectXBase** class that handles all the device-specific objects and their rendering. For more information, see [Rendering framework I](tutorial--assembling-the-rendering-pipeline.md).
-   **Audio** object: Controls the audio playback for the game. For more information, see [Adding sound](tutorial--adding-sound.md).

The rest of the game variables contain the lists of the primitives and their respective in-game amounts, and game play specific data and constraints.

## Next steps

By now, you're probably curious about the actual rendering engine: how calls to the __Render__ methods on the updated primitives get turned into pixels on your screen. This is covered in two parts &mdash; [Rendering framework I: Intro to rendering](tutorial--assembling-the-rendering-pipeline.md) and [Rendering framework II: Game rendering](tutorial-game-rendering.md). If you're more interested in how the player controls update the game state, then check out [Adding controls](tutorial--adding-controls.md).
