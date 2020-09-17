---
title: Define the main game object
description: Now, we look at the details of the sample game's main object and how the rules it implements translate into interactions with the game world.
ms.assetid: 6afeef84-39d0-cb78-aa2e-2e42aef936c9
ms.date: 06/24/2020
ms.topic: article
keywords: windows 10, uwp, games, main object
ms.localizationpriority: medium
---

# Define the main game object

> [!NOTE]
> This topic is part of the [Create a simple Universal Windows Platform (UWP) game with DirectX](tutorial--create-your-first-uwp-directx-game.md) tutorial series. The topic at that link sets the context for the series.

Once you've laid out the basic framework of the sample game, and implemented a state machine that handles the high-level user and system behaviors, you'll want to examine the rules and mechanics that turn the sample game into a game. Let's look at the details of the sample game's main object, and how to translate game rules into interactions with the game world.

## Objectives

- Learn how to apply basic development techniques to implement game rules and mechanics for a UWP DirectX game.

## Main game object

In the **Simple3DGameDX** sample game, **Simple3DGame** is the main game object class. An instance of **Simple3DGame** is constructed, indirectly, via the **App::Load** method.

Here are some of the features of the **Simple3DGame** class.

- Contains implementation of the gameplay logic.
- Contains methods that communicate these details.
  - Changes in the game state to the state machine defined in the app framework.
  - Changes in the game state from the app to the game object itself.
  - Details for updating the game's UI (overlay and heads-up display), animations, and physics (the dynamics).
  > [!NOTE]
  > Updating of graphics is handled by the **GameRenderer** class, which contains methods to obtain and use graphics device resources used by the game. For more info, see [Rendering framework I: Intro to rendering](tutorial--assembling-the-rendering-pipeline.md).
- Serves as a container for the data that defines a game session, level, or lifetime, depending on how you define your game at a high level. In this case, the game state data is for the lifetime of the game, and is initialized one time when a user launches the game.

To view the methods and data defined by this class, see [The Simple3DGame class](#the-simple3dgame-class) below.

## Initialize and start the game

When a player starts the game, the game object must initialize its state, create and add the overlay, set the variables that track the player's performance, and instantiate the objects that it will use to build the levels. In this sample, this is done when the **GameMain** instance is created in **App::Load**.

The game object, of type **Simple3DGame**, is created in the **GameMain::GameMain** constructor. It's then initialized using the **Simple3DGame::Initialize** method during the **GameMain::ConstructInBackground** fire-and-forget coroutine, which is called from **GameMain::GameMain**.

### The Simple3DGame::Initialize method

The sample game sets up these components in the game object.

- A new audio playback object is created.
- Arrays for the game's graphic primitives are created, including arrays for the level primitives, ammo, and obstacles.
- A location for saving game state data is created, named *Game*, and placed in the app data settings storage location specified by [**ApplicationData::Current**](/uwp/api/windows.storage.applicationdata.current).
- A game timer and the initial in-game overlay bitmap are created.
- A new camera is created with a specific set of view and projection parameters.
- The input device (the controller) is set to the same starting pitch and yaw as the camera, so the player has a 1-to-1 correspondence between the starting control position and the camera position.
- The player object is created and set to active. We use a sphere object to detect the player's proximity to walls and obstacles and to keep the camera from getting placed in a position that might break immersion.
- The game world primitive is created.
- The cylinder obstacles are created.
- The targets (**Face** objects) are created and numbered.
- The ammo spheres are created.
- The levels are created.
- The high score is loaded.
- Any prior saved game state is loaded.

The game now has instances of all the key components&mdash;the world, the player, the obstacles, the targets, and the ammo spheres. It also has instances of the levels, which represent configurations of all of the above components and their behaviors for each specific level. Now let's see how the game builds the levels.

## Build and load game levels

Most of the heavy lifting for the level construction is done in the `Level[N].h/.cpp` files found in the **GameLevels** folder of the sample solution. Because it focuses on a very specific implementation, we won't be covering them here. The important thing is that the code for each level is run as a separate **Level[N]** object. If you'd like to extend the game, you can create a **Level[N]** object that takes an assigned number as a parameter and randomly places the obstacles and targets. Or, you can have it load level configuration data from a resource file, or even the internet.

## Define the gameplay

At this point, we have all the components we need to develop the game. The levels have been constructed in memory from the primitives, and are ready for the player to start interacting with.

The best games react instantly to player input, and provide immediate feedback. This is true for any type of a game, from twitch-action, real-time first-person shooters to thoughtful, turn-based strategy games.

### The Simple3DGame::RunGame method

While a game level is in progress, the game is in the **Dynamics** state. 

**GameMain::Update** is the main update loop that updates the application state once per frame, as shown below. The update loop calls the **Simple3DGame::RunGame** method to handle the work if the game is in the **Dynamics** state.

```cppwinrt
// Updates the application state once per frame.
void GameMain::Update()
{
    // The controller object has its own update loop.
    m_controller->Update();

    switch (m_updateState)
    {
    ...
    case UpdateEngineState::Dynamics:
        if (m_controller->IsPauseRequested())
        {
            ...
        }
        else
        {
            // When the player is playing, work is done by Simple3DGame::RunGame.
            GameState runState = m_game->RunGame();
            switch (runState)
            {
                ...
```
          
**Simple3DGame::RunGame** handles the set of data that defines the current state of the game play for the current iteration of the game loop.

Here's the game flow logic in **Simple3DGame::RunGame**.

- The method updates the timer that counts down the seconds until the level is completed, and tests to see whether the level's time has expired. This is one of the rules of the game&mdash;when time runs out, if not all the targets have been shot, then it's game over.
- If time has run out, then the method sets the **TimeExpired** game state, and returns to the **Update** method in the previous code.
- If time remains, then the move-look controller is polled for an update to the camera position; specifically, an update to the angle of the view normal projecting from the camera plane (where the player is looking), and the distance that angle has moved since the controller was polled last.
- The camera is updated based on the new data from the move-look controller.
- The dynamics, or the animations and behaviors of objects in the game world independent of player control, are updated. In this sample game, the **Simple3DGame::UpdateDynamics** method is called to update the motion of the ammo spheres that have been fired, the animation of the pillar obstacles and the movement of the targets. For more information, see [Update the game world](#update-the-game-world).
- The method checks to see whether the criteria for the successful completion of a level have been met. If so, it finalizes the score for the level, and checks to see whether this is the last level (of 6). If it's the last level, then the method returns the **GameState::GameComplete** game state; otherwise, it returns the **GameState::LevelComplete** game state.
- If the level isn't complete, then the method sets the game state to **GameState::Active**, and returns.

## Update the game world

In this sample, when the game is running, the **Simple3DGame::UpdateDynamics** method is called from the **Simple3DGame::RunGame** method (which is called from **GameMain::Update**) to update objects that are rendered in a game scene.

A loop such as **UpdateDynamics** calls any methods that are used to set the game world in motion, independent of the player input, to create an immersive game experience and make the level come alive. This includes graphics that needs to be rendered, and running animation loops to bring about a dynamic world even when there's no player input. In your game, that could include trees swaying in the wind, waves cresting along shore lines, machinery smoking, and alien monsters stretching and moving around. It also encompasses the interaction between objects, including collisions between the player sphere and the world, or between the ammo and the obstacles and targets.

Except when the game is specifically paused, the game loop should continue updating the game world; whether that's based on game logic, physical algorithms, or whether it's just plain random.

In the sample game, this principle is called *dynamics*, and it encompasses the rise and fall of the pillar obstacles, and the motion and physical behaviors of the ammo spheres as they are fired and in motion.

### The Simple3DGame::UpdateDynamics method 

This method deals with these four sets of computations.

- The positions of the fired ammo spheres in the world.
- The animation of the pillar obstacles.
- The intersection of the player and the world boundaries.
- The collisions of the ammo spheres with the obstacles, the targets, other ammo spheres, and the world.

The animation of the obstacles takes place in a loop defined in the **Animate.h/.cpp** source code files. The behavior of the ammo and any collisions are defined by simplified physics algorithms, supplied in the code and parameterized by a set of global constants for the game world, including gravity and material properties. This is all computed in the game world coordinate space.

### Review the flow

Now that we've updated all of the objects in the scene, and calculated any collisions, we need to use this info to draw the corresponding visual changes. 

After **GameMain::Update** has completed the current iteration of the game loop, the sample immediately calls **GameRenderer::Render** to take the updated object data and generate a new scene to present to the player, as shown below.

```cppwinrt
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
                ...
                // Otherwise, fall through and do normal processing to perform rendering.
            default:
                CoreWindow::GetForCurrentThread().Dispatcher().ProcessEvents(
                    CoreProcessEventsOption::ProcessAllIfPresent);
                // GameMain::Update calls Simple3DGame::RunGame. If game is in Dynamics
                // state, uses Simple3DGame::UpdateDynamics to update game world.
                Update();
                // Render is called immediately after the Update loop.
                m_renderer->Render();
                m_deviceResources->Present();
                m_renderNeeded = false;
            }
        }
        else
        {
            CoreWindow::GetForCurrentThread().Dispatcher().ProcessEvents(
                CoreProcessEventsOption::ProcessOneAndAllPending);
        }
    }
    m_game->OnSuspending();  // Exiting due to window close, so save state.
}
```

## Render the game world's graphics

We recommend that the graphics in a game update often, ideally exactly as often as the main game loop iterates. As the loop iterates, the game world's state is updated, with or without player input. This allows the calculated animations and behaviors to be displayed smoothly. Imagine if we had a simple scene of water that moved only when the player pressed a button. That wouldn't be realistic; a good game looks smooth and fluid all the time.

Recall the sample game's loop as shown above in **GameMain::Run**. If the game's main window is visible, and isn't snapped or deactivated, then the game continues to update and render the results of that update. The **GameRenderer::Render** method we examine next renders a representation of that state. This is done immediately after a call to **GameMain::Update**, which includes **Simple3DGame::RunGame** to update states, as discussed in the previous section.

**GameRenderer::Render** draws the projection of the 3D world, and then draws the Direct2D overlay on top of it. When completed, it presents the final swap chain with the combined buffers for display.

> [!NOTE]
> There are two states for the sample game's Direct2D overlay&mdash;one where the game displays the game info overlay that contains the bitmap for the pause menu, and one where the game displays the crosshairs along with the rectangles for the touchscreen move-look controller. The score text is drawn in both states. For more information, see [Rendering framework I: Intro to rendering](tutorial--assembling-the-rendering-pipeline.md).

### The GameRenderer::Render method

```cppwinrt
void GameRenderer::Render()
{
    bool stereoEnabled{ m_deviceResources->GetStereoState() };

    auto d3dContext{ m_deviceResources->GetD3DDeviceContext() };
    auto d2dContext{ m_deviceResources->GetD2DDeviceContext() };

    ...
        if (m_game != nullptr && m_gameResourcesLoaded && m_levelResourcesLoaded)
        {
            // This section is only used after the game state has been initialized and all device
            // resources needed for the game have been created and associated with the game objects.
            ...
            for (auto&& object : m_game->RenderObjects())
            {
                object->Render(d3dContext, m_constantBufferChangesEveryPrim.get());
            }
        }

        d3dContext->BeginEventInt(L"D2D BeginDraw", 1);
        d2dContext->BeginDraw();

        // To handle the swapchain being pre-rotated, set the D2D transformation to include it.
        d2dContext->SetTransform(m_deviceResources->GetOrientationTransform2D());

        if (m_game != nullptr && m_gameResourcesLoaded)
        {
            // This is only used after the game state has been initialized.
            m_gameHud.Render(m_game);
        }

        if (m_gameInfoOverlay.Visible())
        {
            d2dContext->DrawBitmap(
                m_gameInfoOverlay.Bitmap(),
                m_gameInfoOverlayRect
                );
        }
        ...
    }
}
```

## The Simple3DGame class

These are the methods and data members that are defined by the **Simple3DGame** class.

### Member functions

Public member functions defined by **Simple3DGame** include the ones below.

- **Initialize**. Sets the starting values of the global variables, and initializes the game objects. This is covered in the [Initialize and start the game](#initialize-and-start-the-game) section.
- **LoadGame**. Initializes a new level, and starts loading it.
- **LoadLevelAsync**. A coroutine that initializes the level, and then invokes another coroutine on the renderer to load the device-specific level resources. This method runs in a separate thread; as a result, only [**ID3D11Device**](/windows/desktop/api/d3d11/nn-d3d11-id3d11device) methods (as opposed to [**ID3D11DeviceContext**](/windows/desktop/api/d3d11/nn-d3d11-id3d11devicecontext) methods) can be called from this thread. Any device context methods are called in the **FinalizeLoadLevel** method. If you're new to asynchronous programming, then see [Concurrency and asynchronous operations with C++/WinRT](../cpp-and-winrt-apis/concurrency.md).
- **FinalizeLoadLevel**. Completes any work for level loading that needs to be done on the main thread. This includes any calls to Direct3D 11 device context ([**ID3D11DeviceContext**](/windows/desktop/api/d3d11/nn-d3d11-id3d11devicecontext)) methods.
- **StartLevel**. Starts the gameplay for a new level.
- **PauseGame**. Pauses the game.
- **RunGame**. Runs an iteration of the game loop. It's called from **App::Update** one time every iteration of the game loop if the game state is **Active**.
- **OnSuspending** and **OnResuming**. Suspend/resume the game's audio, respectively.

Here are the private member functions.

- **LoadSavedState** and **SaveState**. Load/save the current state of the game, respectively.
- **LoadHighScore** and **SaveHighScore**. Load/save the high score across games, respectively.
- **InitializeAmmo**. Resets the state of each sphere object used as ammunition back to its original state for the beginning of each round.
- **UpdateDynamics**. This is an important method because it updates all the game objects based on canned animation routines, physics, and control input. This is the heart of the interactivity that defines the game. This is covered in the [Update the game world](#update-the-game-world) section.

The other public methods are property accessor that return gameplay- and overlay-specific information to the app framework for display.

### Data members

These objects are updated as the game loop runs.

- **MoveLookController** object. Represents the player input. For more information, see [Adding controls](tutorial--adding-controls.md).
- **GameRenderer** object. Represents a Direct3D 11 renderer, which handles all the device-specific objects and their rendering. For more information, see [Rendering framework I](tutorial--assembling-the-rendering-pipeline.md).
- **Audio** object. Controls the audio playback for the game. For more information, see [Adding sound](tutorial--adding-sound.md).

The rest of the game variables contain the lists of the primitives, and their respective in-game amounts, and game play specific data and constraints.

## Next steps

We have yet to talk about the actual rendering engine&mdash;how calls to the **Render** methods on the updated primitives get turned into pixels on your screen. Those aspects are covered in two parts&mdash;[Rendering framework I: Intro to rendering](tutorial--assembling-the-rendering-pipeline.md) and [Rendering framework II: Game rendering](tutorial-game-rendering.md). If you're more interested in how the player controls update the game state, then see [Adding controls](tutorial--adding-controls.md).