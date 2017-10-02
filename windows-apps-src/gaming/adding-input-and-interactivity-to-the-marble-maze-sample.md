---
author: mtoepke
title: Adding input and interactivity to the Marble Maze sample
description: Universal Windows Platform (UWP) app games run on a wide variety of devices, such as desktop computers, laptops, and tablets.
ms.assetid: b946bf62-c0ca-f9ec-1a87-8195b89a5ab4
ms.author: mtoepke
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, games, input, sample
---

# Adding input and interactivity to the Marble Maze sample




Universal Windows Platform (UWP) app games run on a wide variety of devices, such as desktop computers, laptops, and tablets. A device can have a wide variety of input and control mechanisms. Support multiple input devices to enable your game to accommodate a wider range of preferences and abilities among your customers. This document describes the key practices to keep in mind when you work with input devices and shows how Marble Maze applies these practices.

> **Note**   The sample code that corresponds to this document is found in the [DirectX Marble Maze game sample](http://go.microsoft.com/fwlink/?LinkId=624011).

 
Here are some of the key points that this document discusses for when you work with input in your game:

-   When possible, support multiple input devices to enable your game to accommodate a wider range of preferences and abilities among your customers. Although game controller and sensor usage is optional, we strongly recommend it to enhance the player experience. We designed the game controller and sensor API to help you more easily integrate these input devices.
-   To initialize touch, you must register for window events such as when the pointer is activated, released, and moved. To initialize the accelerometer, create a [**Windows::Devices::Sensors::Accelerometer**](https://msdn.microsoft.com/library/windows/apps/br225687) object when you initialize the application. The Xbox 360 controller doesn't require initialization.
-   For single-player games, consider whether to combine input from all possible Xbox 360 controllers. This way, you don’t have to track what input comes from which controller.
-   Process Windows events before you process input devices.
-   The Xbox 360 controller and the accelerometer support polling. That is, you can poll for data when you need it. For touch, record touch events in data structures that are available to your input processing code.
-   Consider whether to normalize input values to a common format. Doing so can simplify how input is interpreted by other components of your game, such as physics simulation, and can make it easier to write games that work on different screen resolutions.

## Input devices supported by Marble Maze


Marble Maze supports Xbox 360 common controller devices, mouse, and touch to select menu items, and the Xbox 360 controller, mouse, touch, and the accelerometer to control game play. Marble Maze uses the XInput API to poll the controller for input. Touch enables applications to track and respond to fingertip input. An accelerometer is a sensor that measures the force that is applied along the x, y, and z axes. By using the Windows Runtime, you can poll the current state of the accelerometer device, as well as receive touch events through the Windows Runtime event-handling mechanism.

> **Note**  This document uses touch to refer to both touch and mouse input and pointer to refer to any device that uses pointer events. Because touch and the mouse use standard pointer events, you can use either device to select menu items and control game play.

 

> **Note**  The package manifest sets Landscape as the supported rotation for the game to prevent the orientation from changing when you rotate the device to roll the marble.

 

## Initializing input devices


The Xbox 360 controller does not require initialization. To initialize touch, you must register for windowing events such as when the pointer is activated (for example, your user presses the mouse button or touches the screen), released, and moved. To initialize the accelerometer, you have to create a [**Windows::Devices::Sensors::Accelerometer**](https://msdn.microsoft.com/library/windows/apps/br225687) object when you initialize the application.

The following example shows how the **DirectXPage** constructor registers for the [**Windows::UI::Core::CoreIndependentInputSource::PointerPressed**](https://msdn.microsoft.com/library/windows/apps/dn298471), [**Windows::UI::Core::CoreIndependentInputSource::PointerReleased**](https://msdn.microsoft.com/library/windows/apps/dn298472), and [**Windows::UI::Core::CoreIndependentInputSource::PointerMoved**](https://msdn.microsoft.com/library/windows/apps/dn298469) pointer events for the [**SwapChainPanel**](https://msdn.microsoft.com/library/windows/apps/dn252834). These events are registered during app initialization and before the game loop.

These events are handled in a separate thread that invokes the event handlers.

For more information about how the application is initialized, see [Marble Maze application structure](marble-maze-application-structure.md).

```cpp
coreInput->PointerPressed += ref new TypedEventHandler<Object^, PointerEventArgs^>(this, &DirectXPage::OnPointerPressed);
coreInput->PointerMoved += ref new TypedEventHandler<Object^, PointerEventArgs^>(this, &DirectXPage::OnPointerMoved);
coreInput->PointerReleased += ref new TypedEventHandler<Object^, PointerEventArgs^>(this, &DirectXPage::OnPointerReleased);
```

The MarbleMaze class also creates a std::map object to hold touch events. The key for this map object is a value that uniquely identifies the input pointer. Each key maps to the distance between every touch point and the center of the screen. Marble Maze later uses these values to calculate the amount by which the maze is tilted.

```cpp
typedef std::map<int, XMFLOAT2> TouchMap;
TouchMap        m_touches;
```

The MarbleMaze class holds an Accelerometer object.

```cpp
Windows::Devices::Sensors::Accelerometer^           m_accelerometer;
```

The Accelerometer object is initialized in the MarbleMaze::Initialize method, as shown in the following example. The Windows::Devices::Sensors::Accelerometer::GetDefault method returns an instance of the default accelerometer. If there is no default accelerometer, Accelerometer::GetDefault the value of m\_accelerometer remains nullptr.

```cpp
// Returns accelerometer ref if there is one; nullptr otherwise.
m_accelerometer = Windows::Devices::Sensors::Accelerometer::GetDefault();
```

##  Navigating the menus


###  Tracking Xbox 360 controller input

You can use the mouse, touch, or the Xbox 360 controller to navigate the menus, as follows:

-   Use the directional pad to change the active menu item.
-   Use touch, the A button, or the Start button to pick a menu item or close the current menu, such as the high-score table.
-   Use the Start button to pause or resume the game.
-   Click on a menu item with the mouse to choose that action.

###  Tracking touch and mouse input

To track Xbox 360 controller input, the **MarbleMaze::Update** method defines an array of buttons that define the input behaviors. XInput provides only the current state of the controller. Therefore, **MarbleMaze::Update** also defines two arrays that track, for each possible Xbox 360 controller, whether each button was pressed during the previous frame and whether each button is currently pressed.

```cpp
// This array contains the constants from XINPUT that map to the 
// particular buttons that are used by the game. 
// The index of the array is used to associate the state of that button in 
// the wasButtonDown, isButtonDown, and combinedButtonPressed variables. 

static const WORD buttons[] = {
    XINPUT_GAMEPAD_A,
    XINPUT_GAMEPAD_START,
    XINPUT_GAMEPAD_DPAD_UP,
    XINPUT_GAMEPAD_DPAD_DOWN,
    XINPUT_GAMEPAD_DPAD_LEFT,
    XINPUT_GAMEPAD_DPAD_RIGHT,
    XINPUT_GAMEPAD_BACK,
};

static const int buttonCount = ARRAYSIZE(buttons);
static bool wasButtonDown[XUSER_MAX_COUNT][buttonCount] = { false, };
bool isButtonDown[XUSER_MAX_COUNT][buttonCount] = { false, };
```

You can connect up to four Xbox 360 controllers to a Windows device. To avoid having to figure out which controller is the active one, the **MarbleMaze::Update** method combines input across all controllers.

```cpp
bool combinedButtonPressed[buttonCount] = { false, };
```

If your game supports more than one player, you have to track input for each player separately.

In a loop, the **MarbleMaze::Update** method polls each controller for input and reads the state of each button.

```cpp
// Account for input on any connected controller.
XINPUT_STATE inputState = {0};
for (DWORD userIndex = 0; userIndex < XUSER_MAX_COUNT; ++userIndex)
{
    DWORD result = XInputGetState(userIndex, &inputState);
    if(result != ERROR_SUCCESS) 
        continue;

    SHORT thumbLeftX = inputState.Gamepad.sThumbLX;
    if (abs(thumbLeftX) < XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE) 
        thumbLeftX = 0;

    SHORT thumbLeftY = inputState.Gamepad.sThumbLY;
    if (abs(thumbLeftY) < XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE) 
        thumbLeftY = 0;

    combinedTiltX += static_cast<float>(thumbLeftX) / 32768.0f;
    combinedTiltY += static_cast<float>(thumbLeftY) / 32768.0f;

    for (int i = 0; i < buttonCount; ++i)
        isButtonDown[userIndex][i] = (inputState.Gamepad.wButtons & buttons[i]) == buttons[i];
}
```

After the **MarbleMaze::Update** method polls for input, it updates the combined input array. The combined input array tracks only which buttons are pressed but were not previously pressed. This enables the game to perform an action only at the time a button is initially pressed, and not when the button is held.

```cpp
bool combinedButtonPressed[buttonCount] = { false, };
for (int i = 0; i < buttonCount; ++i)
{
    for (DWORD userIndex = 0; userIndex < XUSER_MAX_COUNT; ++userIndex)
    {
        bool pressed = !wasButtonDown[userIndex][i] && isButtonDown[userIndex][i];
        combinedButtonPressed[i] = combinedButtonPressed[i] || pressed;
    }
}
```

After the **MarbleMaze::Update** method collects button input, it performs any actions that must happen. For example, when the Start button (XINPUT\_GAMEPAD\_START) is pressed, the game state changes from active to paused or from paused to active.

```cpp
// Check whether the user paused or resumed the game. 
// XINPUT_GAMEPAD_START  
if (combinedButtonPressed[1] || m_pauseKeyPressed)
{
    m_pauseKeyPressed = false;
    if (m_gameState == GameState::InGameActive)
        SetGameState(GameState::InGamePaused);
    else if (m_gameState == GameState::InGamePaused)
        SetGameState(GameState::InGameActive);
}
```

If the main menu is active, the active menu item changes when the directional pad is pressed up or down. If the user chooses the current selection, the appropriate UI element is marked as being chosen.

```cpp
// Handle menu navigation. 

// XINPUT_GAMEPAD_A or XINPUT_GAMEPAD_START 
bool chooseSelection = (combinedButtonPressed[0] || combinedButtonPressed[1]);

// XINPUT_GAMEPAD_DPAD_UP 
bool moveUp = combinedButtonPressed[2];

// XINPUT_GAMEPAD_DPAD_DOWN 
bool moveDown = combinedButtonPressed[3];                                           

switch (m_gameState)
{
case GameState::MainMenu:
    if (chooseSelection)
    {
        m_audio.PlaySoundEffect(MenuSelectedEvent);

        if (m_startGameButton.GetSelected())
            m_startGameButton.SetPressed(true);

        if (m_highScoreButton.GetSelected())
            m_highScoreButton.SetPressed(true);
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
        SetGameState(GameState::MainMenu);
    break;

case GameState::PostGameResults:
    if (chooseSelection || anyPoints)
        SetGameState(GameState::HighScoreDisplay);
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

After the **MarbleMaze::Update** method processes controller input, it saves the current input state for the next frame.

```cpp
// Update the button state for the next frame.
memcpy(wasButtonDown, isButtonDown, sizeof(wasButtonDown));
```

### Tracking touch and mouse input

For touch and mouse input, a menu item is chosen when the user touches or clicks it. The following example shows how the **MarbleMaze::Update** method processes pointer input to select menu items. The **m\_pointQueue** member variable tracks the locations where the user touched or clicked on the screen. The way in which Marble Maze collects pointer input is described in greater detail later in this document in the section Processing pointer input.

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

After the **MarbleMaze::Update** method processes controller and touch input, it updates the game state if any button was pressed.

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


The game loop and the **MarbleMaze::Update** method work together to update the state of game objects. If your game accepts input from multiple devices, you can accumulate the input from all devices into one set of variables so that you can write code that's easier to maintain. The **MarbleMaze::Update** method defines one set of variables that accumulates movement from all devices.

```cpp
float combinedTiltX = 0.0f;
float combinedTiltY = 0.0f;
```

The input mechanism can vary from one input device to another. For example, pointer input is handled by using the Windows Runtime event-handling model. Conversely, you poll for input data from the Xbox 360 controller when you need it. We recommend that you always follow the input mechanism that is prescribed for a given device. This section describes how Marble Maze reads input from each device, how it updates the combined input values, and how it uses the combined input values to update the state of the game.

###  Processing pointer input

When you work with pointer input, call the [**Windows::UI::Core::CoreDispatcher::ProcessEvents**](https://msdn.microsoft.com/library/windows/apps/br208217) method to process window events. Call this method in your game loop before you update or render the scene. Marble Maze passes **CoreProcessEventsOption::ProcessAllIfPresent** to this method to process all queued events, and then immediately return. After events are processed, Marble Maze renders and presents the next frame.

```cpp
// Process windowing events.
CoreWindow::GetForCurrentThread()->Dispatcher->ProcessEvents(CoreProcessEventsOption::ProcessAllIfPresent);
```

The Windows Runtime calls the registered handler for each event that occurred. The **DirectXApp** class registers for events and forwards pointer information to the **MarbleMaze** class.

```cpp
void DirectXApp::OnPointerPressed(
    _In_ Windows::UI::Core::CoreWindow^ sender,
    _In_ Windows::UI::Core::PointerEventArgs^ args
    )
{
    m_renderer->AddTouch(args->CurrentPoint->PointerId, args->CurrentPoint->Position);
}

void DirectXApp::OnPointerReleased(
    _In_ Windows::UI::Core::CoreWindow^ sender,
    _In_ Windows::UI::Core::PointerEventArgs^ args
    )
{
    m_renderer->RemoveTouch(args->CurrentPoint->PointerId);
}

void DirectXApp::OnPointerMoved(
    _In_ Windows::UI::Core::CoreWindow^ sender,
    _In_ Windows::UI::Core::PointerEventArgs^ args
    )
{
    m_renderer->UpdateTouch(args->CurrentPoint->PointerId, args->CurrentPoint->Position);
}
```

The **MarbleMaze** class reacts to pointer events by updating the map object that holds touch events. The **MarbleMaze::AddTouch** method is called when the pointer is first pressed, for example, when the user initially touches the screen on a touch-enabled device. The **MarbleMaze::AddTouch** method is called when the pointer position moves. The **MarbleMaze::RemoveTouch** method is called when the pointer is released, for example, when the user stops touching the screen.

```cpp
void MarbleMaze::AddTouch(int id, Windows::Foundation::Point point)
{
    m_touches[id] = PointToTouch(point, m_windowBounds);

    m_pointQueue.push(D2D1::Point2F(point.X, point.Y));
}

void MarbleMaze::UpdateTouch(int id, Windows::Foundation::Point point)
{
    if (m_touches.find(id) != m_touches.end())
        m_touches[id] = PointToTouch(point, m_windowBounds);
}

void MarbleMaze::RemoveTouch(int id)
{
    m_touches.erase(id);
}
```

The PointToTouch function translates the current pointer position so that the origin is in the center of the screen, and then scales the coordinates so that they range approximately between -1.0 and +1.0. This makes it easier to calculate the tilt of the maze in a consistent way across different input methods.

```cpp
inline XMFLOAT2 PointToTouch(Windows::Foundation::Point point, Windows::Foundation::Rect bounds)
{
    float touchRadius = min(bounds.Width, bounds.Height);
    float dx = (point.X - (bounds.Width / 2.0f)) / touchRadius;
    float dy = ((bounds.Height / 2.0f) - point.Y) / touchRadius;

    return XMFLOAT2(dx, dy);
}
```

The **MarbleMaze::Update** method updates the combined input values by incrementing the tilt factor by a constant scaling value. This scaling value was determined by experimenting with several different values.

```cpp
// Account for touch input. 
const float touchScalingFactor = 2.0f;
for (TouchMap::const_iterator iter = m_touches.cbegin(); iter != m_touches.cend(); ++iter)
{
    combinedTiltX += iter->second.x * touchScalingFactor;
    combinedTiltY += iter->second.y * touchScalingFactor;
}
```

### Processing accelerometer input

To process accelerometer input, the **MarbleMaze::Update** method calls the [**Windows::Devices::Sensors::Accelerometer::GetCurrentReading**](https://msdn.microsoft.com/library/windows/apps/br225699) method. This method returns a [**Windows::Devices::Sensors::AccelerometerReading**](https://msdn.microsoft.com/library/windows/apps/br225688) object, which represents an accelerometer reading. The **Windows::Devices::Sensors::AccelerometerReading::AccelerationX** and **Windows::Devices::Sensors::AccelerometerReading::AccelerationY** properties hold the g-force acceleration along the x and y axes, respectively.

The following example shows how the **MarbleMaze::Update** method polls the accelerometer and updates the combined input values. As you tilt the device, gravity causes the marble to move faster.

```cpp
// Account for sensors. 
const float acceleromterScalingFactor = 3.5f;
if (m_accelerometer != nullptr)
{
    Windows::Devices::Sensors::AccelerometerReading^ reading =
        m_accelerometer->GetCurrentReading();

    if (reading != nullptr)
    {
        combinedTiltX += static_cast<float>(reading->AccelerationX) * acceleromterScalingFactor;
        combinedTiltY += static_cast<float>(reading->AccelerationY) * acceleromterScalingFactor;
    }
}
```

Because you cannot be sure that an accelerometer is present on the user’s computer, always ensure that you have a valid Accelerometer object before you poll the accelerometer.

### Processing Xbox 360 controller input

The following example shows how the **MarbleMaze::Update** method reads from the Xbox 360 controller and updates the combined input values. The **MarbleMaze::Update** method uses a for loop to enable input to be received from any connected controller. The **XInputGetState** method fills an XINPUT\_STATE object with current state of the controller. The **combinedTiltX** and **combinedTiltY** values are updated according to the x and y values of the left thumbstick.

```cpp
// Account for input on any connected controller.
XINPUT_STATE inputState = {0};
for (DWORD userIndex = 0; userIndex < XUSER_MAX_COUNT; ++userIndex)
{
    DWORD result = XInputGetState(userIndex, &inputState);
    if(result != ERROR_SUCCESS) 
        continue;

    SHORT thumbLeftX = inputState.Gamepad.sThumbLX;
    if (abs(thumbLeftX) < XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE) 
        thumbLeftX = 0;

    SHORT thumbLeftY = inputState.Gamepad.sThumbLY;
    if (abs(thumbLeftY) < XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE) 
        thumbLeftY = 0;

    combinedTiltX += static_cast<float>(thumbLeftX) / 32768.0f;
    combinedTiltY += static_cast<float>(thumbLeftY) / 32768.0f;

    for (int i = 0; i < buttonCount; ++i)
        isButtonDown[userIndex][i] = (inputState.Gamepad.wButtons & buttons[i]) == buttons[i];
}
```

XInput defines the **XINPUT\_GAMEPAD\_LEFT\_THUMB\_DEADZONE** constant for the left thumbstick. This is an appropriate dead zone threshold for most games.

> **Important**  When you work with the Xbox 360 controller, always account for the dead zone. The dead zone refers to the variance among gamepads in their sensitivity to initial movement. In some controllers, a small movement may generate no reading, but in others it may generate a measurable reading. To account for this in your game, create a zone of non-movement for initial thumbstick movement. For more information about the dead zone, see [Getting Started With XInput.](https://msdn.microsoft.com/library/windows/desktop/ee417001)

 

###  Applying input to the game state

Devices report input values in different ways. For example, pointer input might be in screen coordinates, and controller input might be in a completely different format. One challenge with combining input from multiple devices into one set of input values is normalization, or converting values to a common format. Marble Maze normalizes values by scaling them to the range \[-1.0, 1.0\]. To normalize Xbox 360 controller input, Marble Maze divides the input values by 32768 because thumbstick input values always fall between -32768 and 32767. The **PointToTouch** function, which is previously described in this section, achieves a similar result by converting screen coordinates to normalized values that range approximately between -1.0 and +1.0.

> **Tip**  Even if your application uses one input method, we recommend that you always normalize input values. Doing so can simplify how input is interpreted by other components of your game, such as physics simulation, and makes it easier to write games that work on different screen resolutions.

 

After the **MarbleMaze::Update** method processes input, it creates a vector that represents the effect of the tilt of the maze on the marble. The following example shows how Marble Maze uses the **XMVector3Normalize** function to create a normalized gravity vector. The *MaxTilt* variable constrains the amount by which the maze tilts and prevents the maze from tilting on its side.

```cpp
const float maxTilt = 1.0f / 8.0f;
XMVECTOR gravity = XMVectorSet(combinedTiltX * maxTilt, combinedTiltY * maxTilt, 1.0f, 0.0f);
gravity = XMVector3Normalize(gravity);
```

To complete the update of scene objects, Marble Maze passes the updated gravity vector to the physics simulation, updates the physics simulation for the time that has elapsed since the previous frame, and updates the position and orientation of the marble. If the marble has fallen through the maze, the **MarbleMaze::Update** method places the marble back at the last checkpoint that the marble touched and resets the state of the physics simulation.

```cpp
XMFLOAT3 g;
XMStoreFloat3(&g, gravity);
m_physics.SetGravity(g);
```

```cpp
// Only update physics when gameplay is active.
m_physics.UpdatePhysicsSimulation(timeDelta);
```

```cpp
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

This section does not describe how the physics simulation works. For details about that, see Physics.h and Physics.cpp in the Marble Maze sources.

## Next steps


Read [Adding audio to the Marble Maze sample](adding-audio-to-the-marble-maze-sample.md) for information about some of the key practices to keep in mind when you work with audio. The document discusses how Marble Maze uses Microsoft Media Foundation and XAudio2 to load, mix, and play audio resources.

## Related topics


* [Adding audio to the Marble Maze sample](adding-audio-to-the-marble-maze-sample.md)
* [Adding visual content to the Marble Maze sample](adding-visual-content-to-the-marble-maze-sample.md)
* [Developing Marble Maze, a UWP game in C++ and DirectX](developing-marble-maze-a-windows-store-game-in-cpp-and-directx.md)

 

 




