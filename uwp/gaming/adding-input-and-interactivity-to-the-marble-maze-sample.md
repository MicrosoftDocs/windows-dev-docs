---
title: Adding input and interactivity to the Marble Maze sample
description: Learn about key practices to keep in mind when you work with input devices.
ms.assetid: b946bf62-c0ca-f9ec-1a87-8195b89a5ab4
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, input, sample
ms.localizationpriority: medium
---
# Adding input and interactivity to the Marble Maze sample




Universal Windows Platform (UWP) games run on a wide variety of devices, such as desktop computers, laptops, and tablets. A device can have a plethora of input and control mechanisms. This document describes the key practices to keep in mind when you work with input devices and shows how Marble Maze applies these practices.

> [!NOTE]
> The sample code that corresponds to this document is found in the [DirectX Marble Maze game sample](https://github.com/microsoft/Windows-appsample-marble-maze).

 
Here are some of the key points that this document discusses for when you work with input in your game:

-   When possible, support multiple input devices to enable your game to accommodate a wider range of preferences and abilities among your customers. Although game controller and sensor usage is optional, we strongly recommend it to enhance the player experience. We designed the game controller and sensor APIs to help you more easily integrate these input devices.

-   To initialize touch, you must register for window events such as when the pointer is activated, released, and moved. To initialize the accelerometer, create a [Windows::Devices::Sensors::Accelerometer](/uwp/api/Windows.Devices.Sensors.Accelerometer) object when you initialize the application. A game controller doesn't require initialization.

-   For single-player games, consider whether to combine input from all possible controllers. This way, you don’t have to track what input comes from which controller. Or, simply track input only from the most recently added controller, as we do in this sample.

-   Process Windows events before you process input devices.

-   Game controller and accelerometer support polling. That is, you can poll for data when you need it. For touch, record touch events in data structures that are available to your input processing code.

-   Consider whether to normalize input values to a common format. Doing so can simplify how input is interpreted by other components of your game, such as physics simulation, and can make it easier to write games that work on different screen resolutions.

## Input devices supported by Marble Maze


Marble Maze supports the game controller, mouse, and touch to select menu items, and the game controller, mouse, touch, and the accelerometer to control game play. Marble Maze uses the [Windows::Gaming::Input](/uwp/api/windows.gaming.input) APIs to poll the controller for input. Touch enables applications to track and respond to fingertip input. An accelerometer is a sensor that measures the force that is applied along the X, Y, and Z axes. By using the Windows Runtime, you can poll the current state of the accelerometer device, as well as receive touch events through the Windows Runtime event-handling mechanism.

> [!NOTE]
> This document uses touch to refer to both touch and mouse input and pointer to refer to any device that uses pointer events. Because touch and the mouse use standard pointer events, you can use either device to select menu items and control game play.

 

> [!NOTE]
> The package manifest sets **Landscape** as the only supported rotation for the game to prevent the orientation from changing when you rotate the device to roll the marble. To view the package manifest, open **Package.appxmanifest** in the **Solution Explorer** in Visual Studio.

 

## Initializing input devices

The game controller does not require initialization. To initialize touch, you must register for windowing events such as when the pointer is activated (for example, the player presses the mouse button or touches the screen), released, and moved. To initialize the accelerometer, you have to create a [Windows::Devices::Sensors::Accelerometer](/uwp/api/Windows.Devices.Sensors.Accelerometer) object when you initialize the application.

The following example shows how the **App::SetWindow** method registers for the [Windows::UI::Core::CoreWindow::PointerPressed](/uwp/api/windows.ui.core.corewindow.PointerPressed), [Windows::UI::Core::CoreWindow::PointerReleased](/uwp/api/windows.ui.core.corewindow.PointerReleased), and [Windows::UI::Core::CoreWindow::PointerMoved](/uwp/api/windows.ui.core.corewindow.PointerMoved) pointer events. These events are registered during application initialization and before the game loop.

These events are handled in a separate thread that invokes the event handlers.

For more information about how the application is initialized, see [Marble Maze application structure](marble-maze-application-structure.md).

```cpp
window->PointerPressed += ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(
    this, 
    &App::OnPointerPressed);

window->PointerReleased += ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(
    this, 
    &App::OnPointerReleased);

window->PointerMoved += ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(
    this, 
    &App::OnPointerMoved);
```

The **MarbleMazeMain** class also creates a **std::map** object to hold touch events. The key for this map object is a value that uniquely identifies the input pointer. Each key maps to the distance between every touch point and the center of the screen. Marble Maze later uses these values to calculate the amount by which the maze is tilted.

```cpp
typedef std::map<int, XMFLOAT2> TouchMap;
TouchMap        m_touches;
```

The **MarbleMazeMain** class also holds an [Accelerometer](/uwp/api/Windows.Devices.Sensors.Accelerometer) object.

```cpp
Windows::Devices::Sensors::Accelerometer^           m_accelerometer;
```

The **Accelerometer** object is initialized in the **MarbleMazeMain** constructor, as shown in the following example. The [Windows::Devices::Sensors::Accelerometer::GetDefault](/uwp/api/Windows.Devices.Sensors.Accelerometer.GetDefault) method returns an instance of the default accelerometer. If there is no default accelerometer, **Accelerometer::GetDefault** returns **nullptr**.

```cpp
// Returns accelerometer ref if there is one; nullptr otherwise.
m_accelerometer = Windows::Devices::Sensors::Accelerometer::GetDefault();
```

##  Navigating the menus

You can use the mouse, touch, or a game controller to navigate the menus, as follows:

-   Use the directional pad to change the active menu item.
-   Use touch, the A button, or the Menu button to pick a menu item or close the current menu, such as the high-score table.
-   Use the Menu button to pause or resume the game.
-   Click on a menu item with the mouse to choose that action.

###  Tracking game controller input

To keep track of the gamepads currently connected to the device, **MarbleMazeMain** defines a member variable, **m_myGamepads**, which is a collection of [Windows::Gaming::Input::Gamepad](/uwp/api/windows.gaming.input.gamepad) objects. This is initialized in the constructor like so:

```cpp
m_myGamepads = ref new Vector<Gamepad^>();

for (auto gamepad : Gamepad::Gamepads)
{
    m_myGamepads->Append(gamepad);
}
```

Additionally, the **MarbleMazeMain** constructor registers events for when gamepads are added or removed:

```cpp
Gamepad::GamepadAdded += 
    ref new EventHandler<Gamepad^>([=](Platform::Object^, Gamepad^ args)
{
    m_myGamepads->Append(args);
    m_currentGamepadNeedsRefresh = true;
});

Gamepad::GamepadRemoved += 
    ref new EventHandler<Gamepad ^>([=](Platform::Object^, Gamepad^ args)
{
    unsigned int indexRemoved;

    if (m_myGamepads->IndexOf(args, &indexRemoved))
    {
        m_myGamepads->RemoveAt(indexRemoved);
        m_currentGamepadNeedsRefresh = true;
    }
});
```

When a gamepad is added, it is added to **m_myGamepads**; when a gamepad is removed, we check if the gamepad is in **m_myGamepads**, and if it is, we remove it. In both cases, we set **m_currentGamepadNeedsRefresh** to **true**, indicating that we need to reassign **m_gamepad**.

Finally, we assign a gamepad to **m_gamepad** and set **m_currentGamepadNeedsRefresh** to **false**:

```cpp
m_gamepad = GetLastGamepad();
m_currentGamepadNeedsRefresh = false;
```

In the **Update** method, we check if **m_gamepad** needs to be reassigned:

```cpp
if (m_currentGamepadNeedsRefresh)
{
    auto mostRecentGamepad = GetLastGamepad();

    if (m_gamepad != mostRecentGamepad)
    {
        m_gamepad = mostRecentGamepad;
    }

    m_currentGamepadNeedsRefresh = false;
}
```

If **m_gamepad** does need to be reassigned, we assign to it the most recently added gamepad, using **GetLastGamepad**, which is defined like so:

```cpp
Gamepad^ MarbleMaze::MarbleMazeMain::GetLastGamepad()
{
    Gamepad^ gamepad = nullptr;

    if (m_myGamepads->Size > 0)
    {
        gamepad = m_myGamepads->GetAt(m_myGamepads->Size - 1);
    }

    return gamepad;
}
```

This method simply returns the last gamepad in **m_myGamepads**.

You can connect up to four game controllers to a Windows 10 device. To avoid having to figure out which controller is the active one, we simply only track the most recently added gamepad. If your game supports more than one player, you have to track input for each player separately.

The **MarbleMazeMain::Update** method polls the gamepad for input:

```cpp
if (m_gamepad != nullptr)
{
    m_oldReading = m_newReading;
    m_newReading = m_gamepad->GetCurrentReading();
}
```

We keep track of the input reading we got in the last frame with **m_oldReading**, and the latest input reading with **m_newReading**, which we get by calling [Gamepad::GetCurrentReading](/uwp/api/windows.gaming.input.gamepad.GetCurrentReading). This returns a [GamepadReading](/uwp/api/windows.gaming.input.gamepadreading) object, which contains information about the current state of the gamepad.

To check if a button was just pressed or released, we define **MarbleMazeMain::ButtonJustPressed** and **MarbleMazeMain::ButtonJustReleased**, which compare button readings from this frame and the last frame. This way, we can perform an action only at the time when a button is initially pressed or released, and not when it's held:

```cpp
bool MarbleMaze::MarbleMazeMain::ButtonJustPressed(GamepadButtons selection)
{
    bool newSelectionPressed = (selection == (m_newReading.Buttons & selection));
    bool oldSelectionPressed = (selection == (m_oldReading.Buttons & selection));
    return newSelectionPressed && !oldSelectionPressed;
}

bool MarbleMaze::MarbleMazeMain::ButtonJustReleased(GamepadButtons selection)
{
    bool newSelectionReleased = 
        (GamepadButtons::None == (m_newReading.Buttons & selection));

    bool oldSelectionReleased = 
        (GamepadButtons::None == (m_oldReading.Buttons & selection));

    return newSelectionReleased && !oldSelectionReleased;
}
```

[GamepadButtons](/uwp/api/windows.gaming.input.gamepadbuttons) readings are compared using bitwise operations&mdash;we check if a button is pressed using *bitwise and* (&). We determine whether a button was just pressed or released by comparing the old reading and the new reading.

Using the above methods, we check if certain buttons have been pressed and perform any corresponding actions that must happen. For example, when the Menu button (**GamepadButtons::Menu**) is pressed, the game state changes from active to paused or paused to active.

```cpp
if (ButtonJustPressed(GamepadButtons::Menu) || m_pauseKeyPressed)
{
    m_pauseKeyPressed = false;

    if (m_gameState == GameState::InGameActive)
    {
        SetGameState(GameState::InGamePaused);
    }  
    else if (m_gameState == GameState::InGamePaused)
    {
        SetGameState(GameState::InGameActive);
    }
}
```

We also check if the player presses the View button, in which case we restart the game or clear the high score table:

```cpp
if (ButtonJustPressed(GamepadButtons::View) || m_homeKeyPressed)
{
    m_homeKeyPressed = false;

    if (m_gameState == GameState::InGameActive ||
        m_gameState == GameState::InGamePaused ||
        m_gameState == GameState::PreGameCountdown)
    {
        SetGameState(GameState::MainMenu);
        m_inGameStopwatchTimer.SetVisible(false);
        m_preGameCountdownTimer.SetVisible(false);
    }
    else if (m_gameState == GameState::HighScoreDisplay)
    {
        m_highScoreTable.Reset();
    }
}
```

If the main menu is active, the active menu item changes when the directional pad is pressed up or down. If the user chooses the current selection, the appropriate UI element is marked as being chosen.

```cpp
// Handle menu navigation.
bool chooseSelection = 
    (ButtonJustPressed(GamepadButtons::A) 
    || ButtonJustPressed(GamepadButtons::Menu));

bool moveUp = ButtonJustPressed(GamepadButtons::DPadUp);
bool moveDown = ButtonJustPressed(GamepadButtons::DPadDown);

switch (m_gameState)
{
case GameState::MainMenu:
    if (chooseSelection)
    {
        m_audio.PlaySoundEffect(MenuSelectedEvent);
        if (m_startGameButton.GetSelected())
        {
            m_startGameButton.SetPressed(true);
        }
        if (m_highScoreButton.GetSelected())
        {
            m_highScoreButton.SetPressed(true);
        }
    }
    if (moveUp || moveDown)
    {
        m_startGameButton.SetSelected(!m_startGameButton.GetSelected());
        m_highScoreButton.SetSelected(!m_startGameButton.GetSelected());
        m_audio.PlaySoundEffect(MenuChangeEvent);
    }
    break;

case GameState::HighScoreDisplay:
    if (chooseSelection || anyPoints)
    {
        SetGameState(GameState::MainMenu);
    }
    break;

case GameState::PostGameResults:
    if (chooseSelection || anyPoints)
    {
        SetGameState(GameState::HighScoreDisplay);
    }
    break;

case GameState::InGamePaused:
    if (m_pausedText.IsPressed())
    {
        m_pausedText.SetPressed(false);
        SetGameState(GameState::InGameActive);
    }
    break;
}
```

### Tracking touch and mouse input

For touch and mouse input, a menu item is chosen when the user touches or clicks it. The following example shows how the **MarbleMazeMain::Update** method processes pointer input to select menu items. The **m\_pointQueue** member variable tracks the locations where the user touched or clicked on the screen. The way in which Marble Maze collects pointer input is described in greater detail later in this document in the section [Processing pointer input](#processing-pointer-input).

```cpp
// Check whether the user chose a button from the UI. 
bool anyPoints = !m_pointQueue.empty();
while (!m_pointQueue.empty())
{
    UserInterface::GetInstance().HitTest(m_pointQueue.front());
    m_pointQueue.pop();
}
```

The **UserInterface::HitTest** method determines whether the provided point is located in the bounds of any UI element. Any UI elements that pass this test are marked as being touched. This method uses the **PointInRect** helper function to determine whether the provided point is located in the bounds of each UI element.

```cpp
void UserInterface::HitTest(D2D1_POINT_2F point)
{
    for (auto iter = m_elements.begin(); iter != m_elements.end(); ++iter)
    {
        if (!(*iter)->IsVisible())
            continue;

        TextButton* textButton = dynamic_cast<TextButton*>(*iter);
        if (textButton != nullptr)
        {
            D2D1_RECT_F bounds = (*iter)->GetBounds();
            textButton->SetPressed(PointInRect(point, bounds));
        }
    }
}
```

### Updating the game state

After the **MarbleMazeMain::Update** method processes controller and touch input, it updates the game state if any button was pressed.

```cpp
// Update the game state if the user chose a menu option. 
if (m_startGameButton.IsPressed())
{
    SetGameState(GameState::PreGameCountdown);
    m_startGameButton.SetPressed(false);
}
if (m_highScoreButton.IsPressed())
{
    SetGameState(GameState::HighScoreDisplay);
    m_highScoreButton.SetPressed(false);
}
```

##  Controlling game play


The game loop and the **MarbleMazeMain::Update** method work together to update the state of game objects. If your game accepts input from multiple devices, you can accumulate the input from all devices into one set of variables so that you can write code that's easier to maintain. The **MarbleMazeMain::Update** method defines one set of variables that accumulates movement from all devices.

```cpp
float combinedTiltX = 0.0f;
float combinedTiltY = 0.0f;
```

The input mechanism can vary from one input device to another. For example, pointer input is handled by using the Windows Runtime event-handling model. Conversely, you poll for input data from the game controller when you need it. We recommend that you always follow the input mechanism that is prescribed for a given device. This section describes how Marble Maze reads input from each device, how it updates the combined input values, and how it uses the combined input values to update the state of the game.

###  Processing pointer input

When you work with pointer input, call the [Windows::UI::Core::CoreDispatcher::ProcessEvents](/uwp/api/windows.ui.core.coredispatcher.processevents) method to process window events. Call this method in your game loop before you update or render the scene. Marble Maze calls this in the **App::Run** method: 

```cpp
while (!m_windowClosed)
{
    if (m_windowVisible)
    {
        CoreWindow::GetForCurrentThread()->
            Dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessAllIfPresent);

        m_main->Update();

        if (m_main->Render())
        {
            m_deviceResources->Present();
        }
    }
    else
    {
        CoreWindow::GetForCurrentThread()->
            Dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessOneAndAllPending);
    }
}
```

If the window is visible, we pass **CoreProcessEventsOption::ProcessAllIfPresent** to **ProcessEvents** to process all queued events and return immediately; otherwise, we pass **CoreProcessEventsOption::ProcessOneAndAllPending** to process all queued events and wait for the next new event. After events are processed, Marble Maze renders and presents the next frame.

The Windows Runtime calls the registered handler for each event that occurred. The **App::SetWindow** method registers for events and forwards pointer information to the **MarbleMazeMain** class.

```cpp
void App::OnPointerPressed(
    Windows::UI::Core::CoreWindow^ sender, 
    Windows::UI::Core::PointerEventArgs^ args)
{
    m_main->AddTouch(args->CurrentPoint->PointerId, args->CurrentPoint->Position);
}

void App::OnPointerReleased(
    Windows::UI::Core::CoreWindow^ sender, 
    Windows::UI::Core::PointerEventArgs^ args)
{
    m_main->RemoveTouch(args->CurrentPoint->PointerId);
}

void App::OnPointerMoved(
    Windows::UI::Core::CoreWindow^ sender, 
    Windows::UI::Core::PointerEventArgs^ args)
{
    m_main->UpdateTouch(args->CurrentPoint->PointerId, args->CurrentPoint->Position);
}
```

The **MarbleMazeMain** class reacts to pointer events by updating the map object that holds touch events. The **MarbleMazeMain::AddTouch** method is called when the pointer is first pressed, for example, when the user initially touches the screen on a touch-enabled device. The **MarbleMazeMain::UpdateTouch** method is called when the pointer position moves. The **MarbleMazeMain::RemoveTouch** method is called when the pointer is released, for example, when the user stops touching the screen.

```cpp
void MarbleMazeMain::AddTouch(int id, Windows::Foundation::Point point)
{
    m_touches[id] = PointToTouch(point, m_deviceResources->GetLogicalSize());

    m_pointQueue.push(D2D1::Point2F(point.X, point.Y));
}

void MarbleMazeMain::UpdateTouch(int id, Windows::Foundation::Point point)
{
    if (m_touches.find(id) != m_touches.end())
        m_touches[id] = PointToTouch(point, m_deviceResources->GetLogicalSize());
}

void MarbleMazeMain::RemoveTouch(int id)
{
    m_touches.erase(id);
}
```

The **PointToTouch** function translates the current pointer position so that the origin is in the center of the screen, and then scales the coordinates so that they range approximately between -1.0 and +1.0. This makes it easier to calculate the tilt of the maze in a consistent way across different input methods.

```cpp
inline XMFLOAT2 PointToTouch(Windows::Foundation::Point point, Windows::Foundation::Size bounds)
{
    float touchRadius = min(bounds.Width, bounds.Height);
    float dx = (point.X - (bounds.Width / 2.0f)) / touchRadius;
    float dy = ((bounds.Height / 2.0f) - point.Y) / touchRadius;

    return XMFLOAT2(dx, dy);
}
```

The **MarbleMazeMain::Update** method updates the combined input values by incrementing the tilt factor by a constant scaling value. This scaling value was determined by experimenting with several different values.

```cpp
// Account for touch input.
for (TouchMap::const_iterator iter = m_touches.cbegin(); 
    iter != m_touches.cend(); 
    ++iter)
{
    combinedTiltX += iter->second.x * m_touchScaleFactor;
    combinedTiltY += iter->second.y * m_touchScaleFactor;
}
```

### Processing accelerometer input

To process accelerometer input, the **MarbleMazeMain::Update** method calls the [Windows::Devices::Sensors::Accelerometer::GetCurrentReading](/uwp/api/windows.devices.sensors.accelerometer.getcurrentreading) method. This method returns a [Windows::Devices::Sensors::AccelerometerReading](/uwp/api/Windows.Devices.Sensors.AccelerometerReading) object, which represents an accelerometer reading. The **Windows::Devices::Sensors::AccelerometerReading::AccelerationX** and **Windows::Devices::Sensors::AccelerometerReading::AccelerationY** properties hold the g-force acceleration along the X and Y axes, respectively.

The following example shows how the **MarbleMazeMain::Update** method polls the accelerometer and updates the combined input values. As you tilt the device, gravity causes the marble to move faster.

```cpp
// Account for sensors.
if (m_accelerometer != nullptr)
{
    Windows::Devices::Sensors::AccelerometerReading^ reading =
        m_accelerometer->GetCurrentReading();

    if (reading != nullptr)
    {
        combinedTiltX += 
            static_cast<float>(reading->AccelerationX) * m_accelerometerScaleFactor;

        combinedTiltY += 
            static_cast<float>(reading->AccelerationY) * m_accelerometerScaleFactor;
    }
}
```

Because you can't be sure that an accelerometer is present on the user’s computer, always ensure that you have a valid [Accelerometer](/uwp/api/Windows.Devices.Sensors.Accelerometer) object before you poll the accelerometer.

### Processing game controller input

In the **MarbleMazeMain::Update** method, we use **m_newReading** to process input from the left analog stick:

```cpp
float leftStickX = static_cast<float>(m_newReading.LeftThumbstickX);
float leftStickY = static_cast<float>(m_newReading.LeftThumbstickY);

auto oppositeSquared = leftStickY * leftStickY;
auto adjacentSquared = leftStickX * leftStickX;

if ((oppositeSquared + adjacentSquared) > m_deadzoneSquared)
{
    combinedTiltX += leftStickX * m_controllerScaleFactor;
    combinedTiltY += leftStickY * m_controllerScaleFactor;
}
```

We check if the input from the left analog stick is outside of the dead zone, and if it is, we add it to **combinedTiltX** and **combinedTiltY** (multiplied by a scale factor) to tilt the stage.

> [!IMPORTANT]
> When you work with a game controller, always account for the dead zone. The dead zone refers to the variance among gamepads in their sensitivity to initial movement. In some controllers, a small movement may generate no reading, but in others it may generate a measurable reading. To account for this in your game, create a zone of non-movement for initial thumbstick movement. For more information about the dead zone, see [Reading the thumbsticks](gamepad-and-vibration.md#reading-the-thumbsticks).

 

###  Applying input to the game state

Devices report input values in different ways. For example, pointer input might be in screen coordinates, and controller input might be in a completely different format. One challenge with combining input from multiple devices into one set of input values is normalization, or converting values to a common format. Marble Maze normalizes values by scaling them to the range \[-1.0, 1.0\]. The **PointToTouch** function, which is previously described in this section, converts screen coordinates to normalized values that range approximately between -1.0 and +1.0.

> [!TIP]
> Even if your application uses one input method, we recommend that you always normalize input values. Doing so can simplify how input is interpreted by other components of your game, such as physics simulation, and makes it easier to write games that work on different screen resolutions.

 

After the **MarbleMazeMain::Update** method processes input, it creates a vector that represents the effect of the tilt of the maze on the marble. The following example shows how Marble Maze uses the [XMVector3Normalize](/windows/desktop/api/directxmath/nf-directxmath-xmvector3normalize) function to create a normalized gravity vector. The **maxTilt** variable constrains the amount by which the maze tilts and prevents the maze from tilting on its side.

```cpp
const float maxTilt = 1.0f / 8.0f;

XMVECTOR gravity = XMVectorSet(
    combinedTiltX * maxTilt, 
    combinedTiltY * maxTilt, 
    1.0f, 
    0.0f);

gravity = XMVector3Normalize(gravity);
```

To complete the update of scene objects, Marble Maze passes the updated gravity vector to the physics simulation, updates the physics simulation for the time that has elapsed since the previous frame, and updates the position and orientation of the marble. If the marble has fallen through the maze, the **MarbleMazeMain::Update** method places the marble back at the last checkpoint that the marble touched and resets the state of the physics simulation.

```cpp
XMFLOAT3A g;
XMStoreFloat3(&g, gravity);
m_physics.SetGravity(g);

if (m_gameState == GameState::InGameActive)
{
    // Only update physics when gameplay is active.
    m_physics.UpdatePhysicsSimulation(static_cast<float>(m_timer.GetElapsedSeconds()));

    // ...Code omitted for simplicity...

}

// ...Code omitted for simplicity...

// Check whether the marble fell off of the maze. 
const float fadeOutDepth = 0.0f;
const float resetDepth = 80.0f;
if (marblePosition.z >= fadeOutDepth)
{
    m_targetLightStrength = 0.0f;
}
if (marblePosition.z >= resetDepth)
{
    // Reset marble.
    memcpy(&marblePosition, &m_checkpoints[m_currentCheckpoint], sizeof(XMFLOAT3));
    oldMarblePosition = marblePosition;
    m_physics.SetPosition((const XMFLOAT3&)marblePosition);
    m_physics.SetVelocity(XMFLOAT3(0, 0, 0));
    m_lightStrength = 0.0f;
    m_targetLightStrength = 1.0f;

    m_resetCamera = true;
    m_resetMarbleRotation = true;
    m_audio.PlaySoundEffect(FallingEvent);
}
```

This section does not describe how the physics simulation works. For details about that, see **Physics.h** and **Physics.cpp** in the Marble Maze sources.

## Next steps


Read [Adding audio to the Marble Maze sample](adding-audio-to-the-marble-maze-sample.md) for information about some of the key practices to keep in mind when you work with audio. The document discusses how Marble Maze uses Microsoft Media Foundation and XAudio2 to load, mix, and play audio resources.

## Related topics


* [Adding audio to the Marble Maze sample](adding-audio-to-the-marble-maze-sample.md)
* [Adding visual content to the Marble Maze sample](adding-visual-content-to-the-marble-maze-sample.md)
* [Developing Marble Maze, a UWP game in C++ and DirectX](developing-marble-maze-a-windows-store-game-in-cpp-and-directx.md)

 

 