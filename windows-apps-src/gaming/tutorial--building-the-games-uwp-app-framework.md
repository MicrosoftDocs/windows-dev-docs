---
author: joannaleecy
title: Define the game's UWP app framework
description: The first part of coding a Universal Windows Platform (UWP) with DirectX game is building the framework that lets the game object interact with Windows.
ms.assetid: 7beac1eb-ba3d-e15c-44a1-da2f5a79bb3b
ms.author: joanlee
ms.date: 10/24/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, games, directx
ms.localizationpriority: medium
---

#  Define the UWP app framework

Build a framework to let your game object interact with Windows, including Windows Runtime properties like suspend-resume event handling, changes in window focus, and snapping.

To set this framework up, first obtain a view provider so that the app singleton, which is the Windows Runtime object that defines an instance of your running app, can access the graphic resources it needs. Through Windows Runtime, your game also has a direct connection with the graphics interface, allowing you to specify the resources needed and how to handle them.

The view provider object implements the __IFrameworkView__ interface, which consists of a series of methods that needs to be configured to create this game sample.

You'll need to implement these five methods that the app singleton calls:
* [__Initialize__](#initialize-the-view-provider)
* [__SetWindow__](#configure-the-window-and-display-behavior)
* [__Load__](#load-method-of-the-view-provider)
* [__Run__](#run-method-of-the-view-provider)
* [__Uninitialize__](#uninitialize-method-of-the-view-provider)

The __Initialize__ method is called on application launch. __SetWindow__ method is called after __Initialize__. And then the __Load__ method is called. The __Run__ method is when the game is running. When the game ends, the __Uninitialize__ method is called. For more info, see [__IFrameworkView__ API reference](https://docs.microsoft.com/uwp/api/windows.applicationmodel.core.iframeworkview). 

>[!Note]
>If you haven't downloaded the latest game code for this sample, go to [Direct3D game sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/Simple3DGameDX). This sample is part of a large collection of UWP feature samples. For instructions on how to download the sample, see [Get the UWP samples from GitHub](https://docs.microsoft.com/windows/uwp/get-started/get-uwp-app-samples).

## Objective

Set up the framework for a Universal Windows Platform (UWP) DirectX game and implement the state machine that defines the overall game flow.

## Define the view provider factory and view provider object

Let's examine the __main__ loop in __App.cpp__. 

In this step, we create a factory for the view (implements __IFrameworkViewSource__), which in turn creates instances of the view provider object (implements __IFrameworkView__) that defines the view.

### Main method

Create a new __DirectXApplicationSource__ if you have the GitHub sample code loaded. (Use __Direct3DApplicationSource__ if you're using the original DirectX template) This is the view provider factory that implements __IFrameworkViewSource__. The view provider factory's __IFrameworkViewSource__ interface has a single method, __CreateView__, defined.

In __CoreApplication::Run__, the __CreateView__ method is called by the app singleton when __Direct3DApplicationSource__ or __DirectXApplicationSource__ is passed.

__CreateView__ returns a reference to a new instance of your app object that implements __IFrameworkView__, which is the __App__ class object in this sample. The __App__ class object is the view provider object.

```cpp
// The main function is only used to initialize our IFrameworkView class.
[Platform::MTAThread]
int main(Platform::Array<Platform::String^>^)
{
    auto directXApplicationSource = ref new DirectXApplicationSource();
    CoreApplication::Run(directXApplicationSource);
    return 0;
}

//--------------------------------------------------------------------------------------

IFrameworkView^ DirectXApplicationSource::CreateView()
{
    return ref new App();
}
```

## Initialize the view provider

After the view provider object is created, the app singleton calls the [**Initialize**](https://msdn.microsoft.com/library/windows/apps/hh700495) method on application launch. Therefore, it is crucial that this method handles the most fundamental behaviors of a UWP game, such as handling the activation of the main window and making sure that the game can handle a sudden suspend (and a possible later resume) event.

At this point, the game app can handle a suspend (or resume) message. But there's still no window to work with and the game is uninitialized. There's a few more things that need to happen!

### App::Initialize method

In this method, create various event handlers for activating, suspending, and resuming the game.

Get access to the device resources. The __make_shared__ function is used to create a __shared_ptr__ when the memory resource is created for the first time. An advantage of using __make_shared__ is that it's exception-safe. It also uses the same call to allocate the memory for the control block and the resource and therefore reduces the construction overhead.

```cpp
void App::Initialize(
    CoreApplicationView^ applicationView
    )
{
    // Register event handlers for app lifecycle. This example includes Activated, so that we
	// can make the CoreWindow active and start rendering on the window.
    applicationView->Activated +=
        ref new TypedEventHandler<CoreApplicationView^, IActivatedEventArgs^>(this, &App::OnActivated);

    CoreApplication::Suspending +=
        ref new EventHandler<SuspendingEventArgs^>(this, &App::OnSuspending);

    CoreApplication::Resuming +=
        ref new EventHandler<Platform::Object^>(this, &App::OnResuming);

    // At this point we have access to the device. 
    // We can create the device-dependent resources.
    m_deviceResources = std::make_shared<DX::DeviceResources>();
}
```

## Configure the window and display behaviors

Now, let's look at the implementation of [__SetWindow__](https://msdn.microsoft.com/library/windows/apps/hh700509). In the __SetWindow__ method, you configure the window and display behaviors.

### App::SetWindow method

The app singleton provides a [__CoreWindow__](https://msdn.microsoft.com/library/windows/apps/br208225) object that represents the game's main window, and makes its resources and events available to the game. Now that there's a window to work with, the game can now start adding in the basic UI components and events.

Then, create a pointer using __CoreCursor__ method which can be used by both mouse and touch controls.

Lastly, create basic events for window resizing, closing, and DPI changes (if the display device changes). For more info about the events, go to [Event Handling](tutorial-game-flow-management.md#events-handling).

```cpp
void App::SetWindow(
    CoreWindow^ window
    )
{
    // Codes added to modify the original DirectX template project
    window->PointerCursor = ref new CoreCursor(CoreCursorType::Arrow, 0);

    PointerVisualizationSettings^ visualizationSettings = PointerVisualizationSettings::GetForCurrentView();
    visualizationSettings->IsContactFeedbackEnabled = false;
    visualizationSettings->IsBarrelButtonFeedbackEnabled = false;
    // --end of codes added
    
    m_deviceResources->SetWindow(window);

    window->Activated +=
        ref new TypedEventHandler<CoreWindow^, WindowActivatedEventArgs^>(this, &App::OnWindowActivationChanged);

    window->SizeChanged +=
        ref new TypedEventHandler<CoreWindow^, WindowSizeChangedEventArgs^>(this, &App::OnWindowSizeChanged);

    window->VisibilityChanged +=
        ref new TypedEventHandler<CoreWindow^, VisibilityChangedEventArgs^>(this, &App::OnVisibilityChanged);
        
    window->Closed +=
        ref new TypedEventHandler<CoreWindow^, CoreWindowEventArgs^>(this, &App::OnWindowClosed);

    DisplayInformation^ currentDisplayInformation = DisplayInformation::GetForCurrentView();

    currentDisplayInformation->DpiChanged +=
        ref new TypedEventHandler<DisplayInformation^, Platform::Object^>(this, &App::OnDpiChanged);

    currentDisplayInformation->OrientationChanged +=
        ref new TypedEventHandler<DisplayInformation^, Object^>(this, &App::OnOrientationChanged);
    
    // Codes added to modify the original DirectX template project
    currentDisplayInformation->StereoEnabledChanged +=
        ref new TypedEventHandler<DisplayInformation^, Platform::Object^>(this, &App::OnStereoEnabledChanged);
    // --end of codes added
    
    DisplayInformation::DisplayContentsInvalidated +=
        ref new TypedEventHandler<DisplayInformation^, Platform::Object^>(this, &App::OnDisplayContentsInvalidated);
}
```

## Load method of the view provider

After the main window is set, the app singleton calls [__Load__](https://msdn.microsoft.com/library/windows/apps/hh700501). A set of asynchronous tasks is used in this method to create the game objects, load graphics resources, and initialize the game’s state machine. If you want to pre-fetch game data or assets, this is a better place for it than in **SetWindow** or **Initialize**. 

Because Windows imposes restrictions on the time your game can take before it must start processing input, by using the async task pattern, you need to design for the __Load__ method to complete quickly so it can start processing input. If loading takes awhile or if there are lots of resources, provide your users with a regularly updated progress bar. This method is also used to do any necessary preparations before the game begins, like setting any starting states or global values.

If you are new to asynchronous programming and task parallelism concepts, go to [Asynchronous programming in C++](https://docs.microsoft.com/windows/uwp/threading-async/asynchronous-programming-in-cpp-universal-windows-platform-apps).

### App::Load method

Create the __GameMain__ class that contains the loading tasks.

```cpp
void App::Load(
    Platform::String^ entryPoint
    )
{
        if (!m_main)
    {
        m_main = std::unique_ptr<GameMain>(new GameMain(m_deviceResources));
    }
}
````

### GameMain constructor

* Create and initialize the game renderer. For more information, see [Rendering framework I: Intro to rendering](tutorial--assembling-the-rendering-pipeline.md).
* Create the initialize the Simple3Dgame object. For more information, see [Define the main game object](tutorial--defining-the-main-game-loop.md).    
* Create the game UI control object and display game info overlay to show a progress bar as the resource files load. For more information, see [Adding a user interface](tutorial--adding-a-user-interface.md).
* Create the controller so it can read input from the controller (touch, mouse, or XBox wireless controller). For more information, see [Adding controls](tutorial--adding-controls.md).
* After the controller is initialized, we defined two rectangular areas in the lower-left and lower-right corners of the screen 
for the move and camera touch controls, respectively. The player uses the lower-left rectangle, defined by the call to **SetMoveRect**, 
as a virtual control pad for moving the camera forward and backward, and side to side. The lower-right rectangle, defined by the **SetFireRect** 
method, is used as a virtual button to fire the ammo.
* Use __create_task__ and __create_task::then__ to break resource loading into two separate stages. Because access to the Direct3D 11 device context is restricted to the thread the device context was created on while access to the Direct3D 11 device for object creation is free-threaded, this means that the **CreateGameDeviceResourcesAsync** task can run on a separate thread from the completion task (*FinalizeCreateGameDeviceResources*), which runs on the original thread. We use a similar pattern for loading level resources with **LoadLevelAsync** and **FinalizeLoadLevel**.

```cpp
GameMain::GameMain(const std::shared_ptr<DX::DeviceResources>& deviceResources) :
    m_deviceResources(deviceResources),
    m_windowClosed(false),
    m_haveFocus(false),
    m_gameInfoOverlayCommand(GameInfoOverlayCommand::None),
    m_visible(true),
    m_loadingCount(0),
    m_updateState(UpdateEngineState::WaitingForResources)
{
    m_deviceResources->RegisterDeviceNotify(this);

    m_renderer = ref new GameRenderer(m_deviceResources);
    m_game = ref new Simple3DGame();

    m_uiControl = m_renderer->GameUIControl();

    m_controller = ref new MoveLookController(CoreWindow::GetForCurrentThread());

    auto bounds = m_deviceResources->GetLogicalSize();

    m_controller->SetMoveRect(
        XMFLOAT2(0.0f, bounds.Height - GameConstants::TouchRectangleSize),
        XMFLOAT2(GameConstants::TouchRectangleSize, bounds.Height)
        );
    m_controller->SetFireRect(
        XMFLOAT2(bounds.Width - GameConstants::TouchRectangleSize, bounds.Height - GameConstants::TouchRectangleSize),
        XMFLOAT2(bounds.Width, bounds.Height)
        );

    SetGameInfoOverlay(GameInfoOverlayState::Loading);
    m_uiControl->SetAction(GameInfoOverlayCommand::None);
    m_uiControl->ShowGameInfoOverlay();

    create_task([this]()
    {
        // Asynchronously initialize the game class and load the renderer device resources.
        // By doing all this asynchronously, the game gets to its main loop more quickly
        // and in parallel all the necessary resources are loaded on other threads.
        m_game->Initialize(m_controller, m_renderer);

        return m_renderer->CreateGameDeviceResourcesAsync(m_game);

    }).then([this]()
    {
        // The finalize code needs to run in the same thread context
        // as the m_renderer object was created because the D3D device context
        // can ONLY be accessed on a single thread.
        m_renderer->FinalizeCreateGameDeviceResources();

        InitializeGameState();

        if (m_updateState == UpdateEngineState::WaitingForResources)
        {
            // In the middle of a game so spin up the async task to load the level.
            return m_game->LoadLevelAsync().then([this]()
            {
                // The m_game object may need to deal with D3D device context work so
                // again the finalize code needs to run in the same thread
                // context as the m_renderer object was created because the D3D
                // device context can ONLY be accessed on a single thread.
                m_game->FinalizeLoadLevel();
                m_game->SetCurrentLevelToSavedState();
                m_updateState = UpdateEngineState::ResourcesLoaded;

            }, task_continuation_context::use_current());
        }
        else
        {
            // The game is not in the middle of a level so there aren't any level
            // resources to load.  Creating a no-op task so that in both cases
            // the same continuation logic is used.
            return create_task([]()
            {
            });
        }
    }, task_continuation_context::use_current()).then([this]()
    {
        // Since Game loading is an async task, the app visual state
        // may be too small or not have focus.  Put the state machine
        // into the correct state to reflect these cases.

        if (m_deviceResources->GetLogicalSize().Width < GameConstants::MinPlayableWidth)
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
    }, task_continuation_context::use_current());
}
```

## Run method of the view provider

The earlier three methods: __Initialize__, __SetWindow__, and __Load__ have set the stage. Now the game can progress to the **Run** method, starting the fun! The events that it uses to transition between game states are dispatched and processed. Graphics are updated as the game loop cycles.

### App::Run

Start a __while__ loop that terminates when the player closes the game window.

The sample code transitions to one of two states in the game engine state machine:
    * __Deactivated__: The game window gets deactivated (loses focus) or snapped. When this happens, the game suspends event processing and waits for the window to focus or unsnap.
    * __TooSmall__: The game updates its own state and renders the graphics for display.

When your game has focus, you must handle every event in the message queue as it arrives, and so you must call [**CoreWindowDispatch.ProcessEvents**](https://msdn.microsoft.com/library/windows/apps/br208215) with the **ProcessAllIfPresent** option. Other options can cause delays in processing message events, which can make your game feel unresponsive, or result in touch behaviors that feel sluggish and not "sticky".

When the game is not visible, suspended or snapped, you don't want it to consume any resources cycling to dispatch messages that will never arrive. In this case, your game must use **ProcessOneAndAllPending**, which blocks until it gets an event, and then processes that event and any others that arrive in the process queue during the processing of the first. [**ProcessEvents**](https://msdn.microsoft.com/library/windows/apps/br208215) then immediately returns after the queue has been processed.

```cpp
void App::Run()
{
    m_main->Run();
}

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
                    CoreWindow::GetForCurrentThread()->Dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessOneAndAllPending);
                    break;
                }
                // otherwise fall through and do normal processing to get the rendering handled.
            default:
                CoreWindow::GetForCurrentThread()->Dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessAllIfPresent);
                Update();
                m_renderer->Render();
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

## Uninitialize method of the view provider

When the user eventually ends the game session, we need to clean up. This is where **Uninitialize** comes in.

In Windows 10, closing the app window doesn't kill the app's process, but instead it writes the state of the app singleton to memory. If anything special must happen when the system must reclaim this memory, including any special cleanup of resources, then put the code for that cleanup in this method.

### App:: Uninitialize

```cpp
void App::Uninitialize()
{
}
```

## Tips

When developing your own game, design your startup code around these methods. Here's a simple list of basic suggestions for each method:

-   Use **Initialize** to allocate your main classes and connect up the basic event handlers.
-   Use **SetWindow** to create your main window and connect any window-specific events.
-   Use **Load** to handle any remaining setup, and to initiate the async creation of objects and loading of resources. If you need to create any temporary files or data, such as procedurally generated assets, do it here too.


## Next steps

This covers the basic structure of a UWP game with DirectX. Keep these five methods in mind as we'll reference them in other parts of this walkthrough. Next, we'll take an in-depth look at how to manage game states and event handling to keep the game going under [Game flow management](tutorial-game-flow-management.md).



