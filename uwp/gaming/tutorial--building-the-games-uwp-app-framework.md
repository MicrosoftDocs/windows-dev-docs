---
title: Define the game's UWP app framework
description: The first step in coding a Universal Windows Platform (UWP) game is building the framework that lets the app object interact with Windows.
ms.assetid: 7beac1eb-ba3d-e15c-44a1-da2f5a79bb3b
ms.date: 06/24/2020
ms.topic: article
keywords: windows 10, uwp, games, directx
ms.localizationpriority: medium
---

#  Define the game's UWP app framework

> [!NOTE]
> This topic is part of the [Create a simple Universal Windows Platform (UWP) game with DirectX](tutorial--create-your-first-uwp-directx-game.md) tutorial series. The topic at that link sets the context for the series.

The first step in coding a Universal Windows Platform (UWP) game is building the framework that lets the app object interact with Windows, including Windows Runtime features such as suspend-resume event handling, changes in window visibility, and snapping.

## Objectives

* Set up the framework for a Universal Windows Platform (UWP) DirectX game, and implement the state machine that defines the overall game flow.

> [!NOTE]
> To follow along with this topic, look in the source code for the [Simple3DGameDX](/samples/microsoft/windows-universal-samples/simple3dgamedx/) sample game that you downloaded.

## Introduction

In the [Set up the game project](tutorial--setting-up-the-games-infrastructure.md) topic, we introduced the **wWinMain** function as well as the [**IFrameworkViewSource**](/uwp/api/windows.applicationmodel.core.iframeworkviewsource) and [**IFrameworkView**](/uwp/api/windows.applicationmodel.core.iframeworkviewsource) interfaces. We learned that the **App** class (which you can see defined in the `App.cpp` source code file in the **Simple3DGameDX** project) serves as both *view-provider factory* and *view-provider*.

This topic picks up from there, and goes into much more detail about how the **App** class in a game should implement the methods of **IFrameworkView**.

## The App::Initialize method

Upon application launch, the first method that Windows calls is our implementation of [**IFrameworkView::Initialize**](/uwp/api/windows.applicationmodel.core.iframeworkview.initialize).

Your implementation should handle the most fundamental behaviors of a UWP game, such as making sure that the game can handle a suspend (and a possible later resume) event by subscribing to those events. We also have access to the display adapter device here, so we can create graphics resources that depend on the device.

```cppwinrt
void Initialize(CoreApplicationView const& applicationView)
{
    applicationView.Activated({ this, &App::OnActivated });

    CoreApplication::Suspending({ this, &App::OnSuspending });

    CoreApplication::Resuming({ this, &App::OnResuming });

    // At this point we have access to the device. 
    // We can create the device-dependent resources.
    m_deviceResources = std::make_shared<DX::DeviceResources>();
}
```

Avoid raw pointers whenever possible (and it's nearly always possible).

- For Windows Runtime types, you can very often avoid pointers altogether and just construct a value on the stack. If you do need a pointer, then use [**winrt::com_ptr**](/uwp/cpp-ref-for-winrt/com-ptr) (we'll see an example of that soon).
- For unique pointers, use [**std::unique_ptr**](/cpp/cpp/how-to-create-and-use-unique-ptr-instances) and **std::make_unique**.
- For shared pointers, use [**std::shared_ptr**](/cpp/cpp/how-to-create-and-use-shared-ptr-instances) and **std::make_shared**.

## The App::SetWindow method

After **Initialize**, Windows calls our implementation of [**IFrameworkView::SetWindow**](/uwp/api/windows.applicationmodel.core.iframeworkview.setwindow), passing a [**CoreWindow**](/uwp/api/windows.ui.core.corewindow) object representing the game's main window.

In **App::SetWindow**, we subscribe to window-related events, and configure some window and display behaviors. For example, we construct a mouse pointer (via the [**CoreCursor**](/uwp/api/windows.ui.core.corecursor) class), which can be used by both mouse and touch controls. We also pass the window object to our device-dependent resources object.

We'll talk more about handling events in the [Game flow management](tutorial-game-flow-management.md#event-handling) topic.

```cppwinrt
void SetWindow(CoreWindow const& window)
{
    //CoreWindow window = CoreWindow::GetForCurrentThread();
    window.Activate();

    window.PointerCursor(CoreCursor(CoreCursorType::Arrow, 0));

    PointerVisualizationSettings visualizationSettings{ PointerVisualizationSettings::GetForCurrentView() };
    visualizationSettings.IsContactFeedbackEnabled(false);
    visualizationSettings.IsBarrelButtonFeedbackEnabled(false);

    m_deviceResources->SetWindow(window);

    window.Activated({ this, &App::OnWindowActivationChanged });

    window.SizeChanged({ this, &App::OnWindowSizeChanged });

    window.Closed({ this, &App::OnWindowClosed });

    window.VisibilityChanged({ this, &App::OnVisibilityChanged });

    DisplayInformation currentDisplayInformation{ DisplayInformation::GetForCurrentView() };

    currentDisplayInformation.DpiChanged({ this, &App::OnDpiChanged });

    currentDisplayInformation.OrientationChanged({ this, &App::OnOrientationChanged });

    currentDisplayInformation.StereoEnabledChanged({ this, &App::OnStereoEnabledChanged });

    DisplayInformation::DisplayContentsInvalidated({ this, &App::OnDisplayContentsInvalidated });
}
```

## The App::Load method

Now that the main window is set, our implementation of [**IFrameworkView::Load**](/uwp/api/windows.applicationmodel.core.iframeworkview.load) is called. **Load** is a better place to pre-fetch game data or assets than **Initialize** and **SetWindow**.

```cppwinrt
void Load(winrt::hstring const& /* entryPoint */)
{
    if (!m_main)
    {
        m_main = winrt::make_self<GameMain>(m_deviceResources);
    }
}
```

As you can see, the actual work is delegated to the constructor of the **GameMain** object that we make here. The **GameMain** class is defined in `GameMain.h` and `GameMain.cpp`.

### The GameMain::GameMain constructor

The **GameMain** constructor (and the other member functions that it calls) begins a set of asynchronous loading operations to create the game objects, load graphics resources, and initialize the game's state machine. We also do any necessary preparations before the game begins, such as setting any starting states or global values.

Windows imposes a limit on the time your game can take before it begins processing input. So using async, as we do here, means that **Load** can return quickly while the work that it has begun continues in the background. If loading takes a long time, or if there are lots of resources, then providing your users with a frequently updated progress bar is a good idea. 

If you're new to asynchronous programming, then see [Concurrency and asynchronous operations with C++/WinRT](../cpp-and-winrt-apis/concurrency.md).

```cppwinrt
GameMain::GameMain(std::shared_ptr<DX::DeviceResources> const& deviceResources) :
    m_deviceResources(deviceResources),
    m_windowClosed(false),
    m_haveFocus(false),
    m_gameInfoOverlayCommand(GameInfoOverlayCommand::None),
    m_visible(true),
    m_loadingCount(0),
    m_updateState(UpdateEngineState::WaitingForResources)
{
    m_deviceResources->RegisterDeviceNotify(this);

    m_renderer = std::make_shared<GameRenderer>(m_deviceResources);
    m_game = std::make_shared<Simple3DGame>();

    m_uiControl = m_renderer->GameUIControl();

    m_controller = std::make_shared<MoveLookController>(CoreWindow::GetForCurrentThread());

    auto bounds = m_deviceResources->GetLogicalSize();

    m_controller->SetMoveRect(
        XMFLOAT2(0.0f, bounds.Height - GameUIConstants::TouchRectangleSize),
        XMFLOAT2(GameUIConstants::TouchRectangleSize, bounds.Height)
        );
    m_controller->SetFireRect(
        XMFLOAT2(bounds.Width - GameUIConstants::TouchRectangleSize, bounds.Height - GameUIConstants::TouchRectangleSize),
        XMFLOAT2(bounds.Width, bounds.Height)
        );

    SetGameInfoOverlay(GameInfoOverlayState::Loading);
    m_uiControl->SetAction(GameInfoOverlayCommand::None);
    m_uiControl->ShowGameInfoOverlay();

    // Asynchronously initialize the game class and load the renderer device resources.
    // By doing all this asynchronously, the game gets to its main loop more quickly
    // and in parallel all the necessary resources are loaded on other threads.
    ConstructInBackground();
}

winrt::fire_and_forget GameMain::ConstructInBackground()
{
    auto lifetime = get_strong();

    m_game->Initialize(m_controller, m_renderer);

    co_await m_renderer->CreateGameDeviceResourcesAsync(m_game);

    // The finalize code needs to run in the same thread context
    // as the m_renderer object was created because the D3D device context
    // can ONLY be accessed on a single thread.
    // co_await of an IAsyncAction resumes in the same thread context.
    m_renderer->FinalizeCreateGameDeviceResources();

    InitializeGameState();

    if (m_updateState == UpdateEngineState::WaitingForResources)
    {
        // In the middle of a game so spin up the async task to load the level.
        co_await m_game->LoadLevelAsync();

        // The m_game object may need to deal with D3D device context work so
        // again the finalize code needs to run in the same thread
        // context as the m_renderer object was created because the D3D
        // device context can ONLY be accessed on a single thread.
        m_game->FinalizeLoadLevel();
        m_game->SetCurrentLevelToSavedState();
        m_updateState = UpdateEngineState::ResourcesLoaded;
    }
    else
    {
        // The game is not in the middle of a level so there aren't any level
        // resources to load.
    }

    // Since Game loading is an async task, the app visual state
    // may be too small or not be activated. Put the state machine
    // into the correct state to reflect these cases.

    if (m_deviceResources->GetLogicalSize().Width < GameUIConstants::MinPlayableWidth)
    {
        m_updateStateNext = m_updateState;
        m_updateState = UpdateEngineState::TooSmall;
        m_controller->Active(false);
        m_uiControl->HideGameInfoOverlay();
        m_uiControl->ShowTooSmall();
        m_renderNeeded = true;
    }
    else if (!m_haveFocus)
    {
        m_updateStateNext = m_updateState;
        m_updateState = UpdateEngineState::Deactivated;
        m_controller->Active(false);
        m_uiControl->SetAction(GameInfoOverlayCommand::None);
        m_renderNeeded = true;
    }
}

void GameMain::InitializeGameState()
{
    // Set up the initial state machine for handling Game playing state.
    ...
}
```

Here's an outline of the sequence of work that's kicked off by the constructor.

- Create and initialize an object of type **GameRenderer**. For more information, see [Rendering framework I: Intro to rendering](tutorial--assembling-the-rendering-pipeline.md).
- Create and initialize an object of type **Simple3DGame**. For more information, see [Define the main game object](tutorial--defining-the-main-game-loop.md).
- Create the game UI control object, and display game info overlay to show a progress bar as the resource files load. For more information, see [Adding a user interface](tutorial--adding-a-user-interface.md).
- Create a controller object to read input from the controller (touch, mouse, or game controller). For more information, see [Adding controls](tutorial--adding-controls.md).
- Define two rectangular areas in the lower-left and lower-right corners of the screen for the move and camera touch controls, respectively. The player uses the lower-left rectangle (defined in the call to **SetMoveRect**) as a virtual control pad for moving the camera forward and backward, and side to side. The lower-right rectangle (defined by the **SetFireRect** method) is used as a virtual button to fire the ammo.
- Use coroutines to break resource loading into separate stages. Access to the Direct3D device context is restricted to the thread on which the device context was created; while access to the Direct3D device for object creation is free-threaded. Consequently, the **GameRenderer::CreateGameDeviceResourcesAsync** coroutine can run on a separate thread from the completion task (**GameRenderer::FinalizeCreateGameDeviceResources**), which runs on the original thread.
- We use a similar pattern for loading level resources with **Simple3DGame::LoadLevelAsync** and **Simple3DGame::FinalizeLoadLevel**.

We'll see more of **GameMain::InitializeGameState** in the next topic ([Game flow management](tutorial-game-flow-management.md)).

## The App::OnActivated method

Next, the [**CoreApplicationView::Activated**](/uwp/api/windows.applicationmodel.core.coreapplicationview.activated) event is raised. So any **OnActivated** event handler that you have (such as our **App::OnActivated** method) is called.

```cppwinrt
void OnActivated(CoreApplicationView const& /* applicationView */, IActivatedEventArgs const& /* args */)
{
    CoreWindow window = CoreWindow::GetForCurrentThread();
    window.Activate();
}
```

The only work we do here is to activate the main [**CoreWindow**](/uwp/api/windows.ui.core.corewindow). Alternatively, you can choose to do that in **App::SetWindow**.

## The App::Run method

**Initialize**, **SetWindow**, and **Load** have set the stage. Now that the game is up and running, our implementation of [**IFrameworkView::Run**](/uwp/api/windows.applicationmodel.core.iframeworkview.run) is called.

```cppwinrt
void Run()
{
    m_main->Run();
}
```

Again, work is delegated to **GameMain**.

### The GameMain::Run method

**GameMain::Run** is the main loop of the game; you can find it in `GameMain.cpp`. The basic logic is that while the window for your game remains open, dispatch all events, update the timer, and then render and present the results of the graphics pipeline. Also here, the events used to transition between game states are dispatched and processed.

The code here is also concerned with two of the states in the game engine state machine.

- **UpdateEngineState::Deactivated**. This specifies that the game window is deactivated (has lost focus) or is snapped. 
- **UpdateEngineState::TooSmall**. This specifies that the client area is too small to render the game in.

In either of these states, the game suspends event processing, and waits for the window to be activated, to unsnap, or to be resized.

While your game window is visible ([Window.Visible](/uwp/api/windows.ui.xaml.window.visible) is `true`), you must handle every event in the message queue as it arrives, and so you must call [**CoreWindowDispatch.ProcessEvents**](/uwp/api/windows.ui.core.coredispatcher.processevents) with the **ProcessAllIfPresent** option. Other options can cause delays in processing message events, which can make your game feel unresponsive, or result in touch behaviors that feel sluggish.

When the game is *not* visible ([Window.Visible](/uwp/api/windows.ui.xaml.window.visible) is `false`), or when it's suspended, or when it's too small (it's snapped), you don't want it to consume any resources cycling to dispatch messages that will never arrive. In this case, your game must use the **ProcessOneAndAllPending** option. That option blocks until it gets an event, and then processes that event (as well as any others that arrive in the process queue during the processing of the first). **CoreWindowDispatch.ProcessEvents** then immediately returns after the queue has been processed.

In the example code shown below, the *m_visible* data member represents the window's visibility. When the game is suspended, its window is not visible. When the window *is* visible, the value of *m_updateState* (an **UpdateEngineState** enum) determines further whether or not the window is deactivated (lost focus), too small (snapped), or the right size.

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
                if (m_updateStateNext == UpdateEngineState::WaitingForResources)
                {
                    WaitingForResourceLoading();
                    m_renderNeeded = true;
                }
                else if (m_updateStateNext == UpdateEngineState::ResourcesLoaded)
                {
                    // In the device lost case, we transition to the final waiting state
                    // and make sure the display is updated.
                    switch (m_pressResult)
                    {
                    case PressResultState::LoadGame:
                        SetGameInfoOverlay(GameInfoOverlayState::GameStats);
                        break;

                    case PressResultState::PlayLevel:
                        SetGameInfoOverlay(GameInfoOverlayState::LevelStart);
                        break;

                    case PressResultState::ContinueLevel:
                        SetGameInfoOverlay(GameInfoOverlayState::Pause);
                        break;
                    }
                    m_updateStateNext = UpdateEngineState::WaitingForPress;
                    m_uiControl->ShowGameInfoOverlay();
                    m_renderNeeded = true;
                }

                if (!m_renderNeeded)
                {
                    // The App is not currently the active window and not in a transient state so just wait for events.
                    CoreWindow::GetForCurrentThread().Dispatcher().ProcessEvents(CoreProcessEventsOption::ProcessOneAndAllPending);
                    break;
                }
                // otherwise fall through and do normal processing to get the rendering handled.
            default:
                CoreWindow::GetForCurrentThread().Dispatcher().ProcessEvents(CoreProcessEventsOption::ProcessAllIfPresent);
                Update();
                m_renderer->Render();
                m_deviceResources->Present();
                m_renderNeeded = false;
            }
        }
        else
        {
            CoreWindow::GetForCurrentThread().Dispatcher().ProcessEvents(CoreProcessEventsOption::ProcessOneAndAllPending);
        }
    }
    m_game->OnSuspending();  // Exiting due to window close, so save state.
}
```

## The App::Uninitialize method

When the game ends, our implementation of [**IFrameworkView::Uninitialize**](/uwp/api/windows.applicationmodel.core.iframeworkview.uninitialize) is called. This is our opportunity to perform cleanup. Closing the app window doesn't kill the app's process; but instead it writes the state of the app singleton to memory. If anything special must happen when the system reclaims this memory, including any special cleanup of resources, then put the code for that cleanup in **Uninitialize**.

In our case, **App::Uninitialize** is a no-op.

```cpp
void Uninitialize()
{
}
```

## Tips

When developing your own game, design your startup code around the methods described in this topic. Here's a simple list of basic suggestions for each method.

- Use **Initialize** to allocate your main classes, and connect up the basic event handlers.
- Use **SetWindow** to subscribe to any window-specific events, and to pass your main window to your device-dependent resources object so that it can use that window when creating a swap chain.
- Use **Load** to handle any remaining setup, and to initiate the asynchronous creation of objects, and loading of resources. If you need to create any temporary files or data, such as procedurally generated assets, then do that here, too.

## Next steps

This topic has covered some of the basic structure of a UWP game that uses DirectX. It's a good idea to keep these methods in mind, because we'll be referring back to some of them in later topics.

In the next topic&mdash;[Game flow management](tutorial-game-flow-management.md)&mdash;we'll take an in-depth look at how to manage game states and event handling in order to keep the game flowing.
