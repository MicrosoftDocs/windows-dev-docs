---
author: mtoepke
title: Add controls
description: Now, we take a look at how the game sample implements move-look controls in a 3-D game, and how to develop basic touch, mouse, and game controller controls.
ms.assetid: f9666abb-151a-74b4-ae0b-ef88f1f252f8
ms.author: mtoepke
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, games, controls, input
---

# Add controls


\[ Updated for UWP apps on Windows 10. For Windows 8.x articles, see the [archive](http://go.microsoft.com/fwlink/p/?linkid=619132) \]

Now, we take a look at how the game sample implements move-look controls in a 3-D game, and how to develop basic touch, mouse, and game controller controls.

## Objective


-   To implement mouse/keyboard, touch, and Xbox controller controls in a Universal Windows Platform (UWP) game with DirectX.

## UWP game apps and controls


A good UWP game supports a broad variety of interfaces. A potential player might have Windows 10 on a tablet with no physical buttons, or a media PC with an Xbox controller attached, or the latest desktop gaming rig with a high-performance mouse and gaming keyboard. Your game should support all of these devices if the game design allows it.

This sample supports all three. It's a simple first-person shooting game, and the move-look controls that are standard for this genre are easily implemented for all three types of input.

For more info about controls, and move-look controls specifically, see [Move-look controls for games](tutorial--adding-move-look-controls-to-your-directx-game.md) and [Touch controls for games](tutorial--adding-touch-controls-to-your-directx-game.md).

## Common control behaviors


Touch controls and mouse/keyboard controls have a very similar core implementation. In a UWP app, a pointer is simply a point on the screen. You can move it by sliding the mouse or sliding your finger on the touch screen. As a result, you can register for a single set of events, and not worry about whether the player is using a mouse or a touch screen to move and press the pointer.

When the **MoveLookController** class in the game sample is initialized, it registers for four pointer-specific events and one mouse-specific event:

-   [**CoreWindow::PointerPressed**](https://msdn.microsoft.com/library/windows/apps/br208278). The left or right mouse button was pressed (and held), or the touch surface was touched.
-   [**CoreWindow::PointerMoved**](https://msdn.microsoft.com/library/windows/apps/br208276). The mouse moved, or a drag action was made on the touch surface.
-   [**CoreWindow::PointerReleased**](https://msdn.microsoft.com/library/windows/apps/br208279). The left mouse button was released, or the object contacting the touch surface was lifted.
-   [**CoreWindow::PointerExited**](https://msdn.microsoft.com/library/windows/apps/br208275). The pointer moved out of the main window.
-   [**Windows::Devices::Input::MouseMoved**](https://msdn.microsoft.com/library/windows/apps/hh758356). The mouse moved a certain distance. Be aware that we are only interested in mouse movement delta values, and not the current x-y position.

```cpp
void MoveLookController::Initialize(
    _In_ CoreWindow^ window
    )
{
    window->PointerPressed +=
        ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(this, &MoveLookController::OnPointerPressed);

    window->PointerMoved +=
        ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(this, &MoveLookController::OnPointerMoved);

    window->PointerReleased +=
        ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(this, &MoveLookController::OnPointerReleased);

    window->PointerExited +=
        ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(this, &MoveLookController::OnPointerExited);

    window->KeyDown +=
        ref new TypedEventHandler<CoreWindow^, KeyEventArgs^>(this, &MoveLookController::OnKeyDown);

    window->KeyUp +=
        ref new TypedEventHandler<CoreWindow^, KeyEventArgs^>(this, &MoveLookController::OnKeyUp);

    // A separate handler for mouse only relative mouse movement events.
    Windows::Devices::Input::MouseDevice::GetForCurrentView()->MouseMoved +=
        ref new TypedEventHandler<MouseDevice^, MouseEventArgs^>(this, &MoveLookController::OnMouseMoved);

    ResetState();
    m_state = MoveLookControllerState::None;

    m_pitch = 0.0f;
    m_yaw   = 0.0f;
}
```

The Xbox controller is handled separately, using the [XInput](https://msdn.microsoft.com/library/windows/desktop/hh405053) APIs. We talk about the implementation of game controller controls in a bit.

In the game sample, the **MoveLookController** class has three controller-specific states, regardless of the control type:

-   **None**. This is the initialized state for the controller. The game is not anticipating any controller input.
-   **WaitForInput**. The game is paused and is waiting for the player to continue.
-   **Active**. The game is running, processing player input.

The **Active** state is the state when the player is actively playing the game. During this state, the **MoveLookController** instance is processing input events from all enabled input devices and interpreting the player's intentions based on the aggregated event data. As a result, it updates the velocity and look direction (the view plane normal) of the player's view and shares the updated data with the game after Update is called from the game loop.

Be aware that the player can take more than one action at the same time. For example, he or she could be firing spheres while moving the camera. All of these inputs are tracked in the **Active** state, with different pointer IDs corresponding to different pointer actions. This is necessary because from a player's perspective, a pointer event in the firing rectangle is different from one in the move rectangle or in the rest of the screen.

When a [**PointerPressed**](https://msdn.microsoft.com/library/windows/apps/br208278) event is received, the **MoveLookController** obtains the pointer ID value created by the window. The pointer ID represents a specific type of input. For example, on a multi-touch device, there may be several different active inputs at the same time. The IDs are used to keep track of which input the player is using. If one event is in the move rectangle of the touch screen, a pointer ID is assigned to track any pointer events in move rectangle. Other pointer events in the fire rectangle are tracked separately, with a separate pointer ID. (We talk about this some more in the section on touch controls.)

Input from the mouse has yet another ID and is also handled separately.

After the pointer events have been mapped to a specific game action, it's time to update the data the **MoveLookController** object shares with the main game loop.

When called, the **Update** method in the game sample processes the input and updates the velocity and look direction variables (**m\_velocity** and **m\_lookdirection**), which the game loop then retrieves by calling the public **Velocity** and **LookDirection** methods on the **MoveLookController** instance.

```cpp
void MoveLookController::Update()
{
    UpdateGameController();

    if (m_moveInUse)
    {
        // Move control.
        XMFLOAT2 pointerDelta;

        pointerDelta.x = m_movePointerPosition.x - m_moveFirstDown.x;
        pointerDelta.y = m_movePointerPosition.y - m_moveFirstDown.y;

        // Figure out the command from the virtual joystick.
        XMFLOAT3 commandDirection = XMFLOAT3(0.0f, 0.0f, 0.0f);
        if (fabsf(pointerDelta.x) > 16.0f)         // Leave 32 pixel-wide dead spot for being still.
            m_moveCommand.x -= pointerDelta.x/fabsf(pointerDelta.x);

        if (fabsf(pointerDelta.y) > 16.0f)
            m_moveCommand.y -= pointerDelta.y/fabsf(pointerDelta.y);
    }

    // Poll our state bits set by the keyboard input events.
    if (m_forward)
    {
        m_moveCommand.y += 1.0f;
    }
    if (m_back)
    {
        m_moveCommand.y -= 1.0f;
    }
    if (m_left)
    {
        m_moveCommand.x += 1.0f;
    }
    if (m_right)
    {
        m_moveCommand.x -= 1.0f;
    }
    if (m_up)
    {
        m_moveCommand.z += 1.0f;
    }
    if (m_down)
    {
        m_moveCommand.z -= 1.0f;
    }

    // Make sure that 45deg cases are not faster.
    if (fabsf(m_moveCommand.x) > 0.1f ||
        fabsf(m_moveCommand.y) > 0.1f ||
        fabsf(m_moveCommand.z) > 0.1f)
    {
        XMStoreFloat3(&m_moveCommand, XMVector3Normalize(XMLoadFloat3(&m_moveCommand)));
    }

    // Rotate command to align with our direction (world coordinates).
    XMFLOAT3 wCommand;
    wCommand.x =  m_moveCommand.x * cosf(m_yaw) - m_moveCommand.y * sinf(m_yaw);
    wCommand.y =  m_moveCommand.x * sinf(m_yaw) + m_moveCommand.y * cosf(m_yaw);
    wCommand.z =  m_moveCommand.z;

    // Scale for sensitivity adjustment.
    // Our velocity is based on the command, y is up.
    m_velocity.x = -wCommand.x * MOVEMENT_GAIN;
    m_velocity.z =  wCommand.y * MOVEMENT_GAIN;
    m_velocity.y =  wCommand.z * MOVEMENT_GAIN;

    // Clear movement input accumulator for use during next frame.
    m_moveCommand = XMFLOAT3(0.0f, 0.0f, 0.0f);
}
```

The game loop can test to see if the player is firing by calling the **IsFiring** method on the **MoveLookController** instance. The **MoveLookController** checks to see if the player has pressed the fire button on one of the three input types.

```cpp
bool MoveLookController::IsFiring()
{
    if (m_state == MoveLookControllerState::Active)
    {
        if (m_autoFire)
        {
            return (m_fireInUse || (m_mouseInUse && m_mouseLeftInUse) || m_xinputTriggerInUse);
        }
        else
        {
            if (m_firePressed)
            {
                m_firePressed = false;
                return true;
            }
        }
    }
    return false;
}
```

If the player moves the pointer outside the main window of the game, or presses the pause button (the P key or the Xbox controller start button), the game must be paused. The **MoveLookController** registered the press, and informs the game loop when it calls the **IsPauseRequested** method. At that point, if **IsPauseRequested** returns **true**, the game loop then calls **WaitForPress** on the **MoveLookController** to move the controller into the **WaitForInput** state. Then, the **MoveLookController** waits for the player to select one of the menu items to load, continue, or exit the game, and stop processing gameplay input events until it returns to the **Active** state.

See the [complete code sample for this section](#complete-sample-code-for-this-section).

Now, let's look at the implementation of each of the three control types in a little more detail.

## Implementing relative mouse controls


If mouse movement is detected, we want use that movement to determine the new pitch and yaw of the camera. We do that by implementing relative mouse controls, where we handle the relative distance the mouse has moved—the delta between the start of the movement and the stop—as opposed to recording the absolute x-y pixel coordinates of the motion.

To do that, we obtain the changes in the X (the horizontal motion) and the Y (the vertical motion) coordinates by examining the [**MouseDelta::X**](https://msdn.microsoft.com/library/windows/apps/hh758353) and **MouseDelta::Y** fields on the [**Windows::Device::Input::MouseEventArgs::MouseDelta**](https://msdn.microsoft.com/library/windows/apps/hh758358) argument object returned by the [**MouseMoved**](https://msdn.microsoft.com/library/windows/apps/hh758356) event.

```cpp
void MoveLookController::OnMouseMoved(
    _In_ MouseDevice^ /* mouseDevice */,
    _In_ MouseEventArgs^ args
    )
{
    // Handle Mouse Input via dedicated relative movement handler.

    switch (m_state)
    {
    case MoveLookControllerState::Active:
        XMFLOAT2 mouseDelta;
        mouseDelta.x = static_cast<float>(args->MouseDelta.X);
        mouseDelta.y = static_cast<float>(args->MouseDelta.Y);

        XMFLOAT2 rotationDelta;
        rotationDelta.x  = mouseDelta.x * ROTATION_GAIN;   // scale for control sensitivity
        rotationDelta.y  = mouseDelta.y * ROTATION_GAIN;

        // Update our orientation based on the command.
        m_pitch -= rotationDelta.y;
        m_yaw   += rotationDelta.x;

        // Limit pitch to straight up or straight down.
        float limit = XM_PI / 2.0f - 0.01f;
        m_pitch = __max(-limit, m_pitch);
        m_pitch = __min(+limit, m_pitch);

        // Keep longitude in same range by wrapping.
        if (m_yaw >  XM_PI)
        {
            m_yaw -= XM_PI * 2.0f;
        }
        else if (m_yaw < -XM_PI)
        {
            m_yaw += XM_PI * 2.0f;
        }
        break;
    }
}
```

## Implementing touch controls


Touch controls are the trickiest to develop, because they are the most complex and require the most fine-tuning to be effective. In the game sample, a rectangle in the lower left quadrant of the screen is used as a directional pad, where sliding your thumb left and right in this space slides the camera left and right, and sliding your thumb up and down moves the camera forward and backward. A rectangle in the lower right quadrant of the screen can be pressed to fire the spheres. Aiming (pitch and yaw) are controlled by sliding your finger on the parts of the screen not reserved for moving and firing; as your finger moves, the camera (with fixed cross hairs) moves similarly.

The move and fire rectangles are created by two methods in the sample code:

```cpp
void SetMoveRect(
        _In_ DirectX::XMFLOAT2 upperLeft,
        _In_ DirectX::XMFLOAT2 lowerRight
        );
 void SetFireRect(
        _In_ DirectX::XMFLOAT2 upperLeft,
        _In_ DirectX::XMFLOAT2 lowerRight
        );
```

We treat touch device pointer events for the other regions of the screen as look commands. If the screen is resized, these rectangles must be computed again (and redrawn).

If a touch device pointer event is raised in one of these regions and the game state is set to **Active**, it's assigned a pointer ID, as we discussed earlier.

```cpp
void MoveLookController::OnPointerPressed(
    _In_ CoreWindow^ sender,
    _In_ PointerEventArgs^ args
    )
{
    PointerPoint^ point = args->CurrentPoint;
    UINT32 pointerID = point->PointerId;
    Point pointerPosition = point->Position;
    PointerPointProperties^ pointProperties = point->Properties;
    auto pointerDevice = point->PointerDevice;
    auto pointerDeviceType = pointerDevice->PointerDeviceType;

    XMFLOAT2 position = XMFLOAT2(pointerPosition.X, pointerPosition.Y);     // convert to allow math

    switch (m_state)
    {
    case MoveLookControllerState::WaitForInput:
        // ...
        // Game is paused, wait for click inside the game window.
        // ...
        break;

    case MoveLookControllerState::Active:
        switch (pointerDeviceType)
        {
        case Windows::Devices::Input::PointerDeviceType::Touch:
            // Check to see if this pointer is in the move control.
            if (position.x > m_moveUpperLeft.x &&
                position.x < m_moveLowerRight.x &&
                position.y > m_moveUpperLeft.y &&
                position.y < m_moveLowerRight.y)
            {
                if (!m_moveInUse)         // If no pointer is in this control yet:
                {
                    // Process a DPad touch down event.
                    m_moveFirstDown = position;                 // Save the location of the initial contact.
                    m_movePointerID = pointerID;                // Store the pointer using this control.
                    m_moveInUse = true;
                }
            }
            // Check to see if this pointer is in the fire control.
            else if (position.x > m_fireUpperLeft.x &&
                position.x < m_fireLowerRight.x &&
                position.y > m_fireUpperLeft.y &&
                position.y < m_fireLowerRight.y)
            {
                if (!m_fireInUse)
                {
                    m_fireLastPoint = position;
                    m_firePointerID = pointerID;
                    m_fireInUse = true;
                }
            }
            else
            {
                if (!m_lookInUse)   // If no pointer is in this control yet:
                {
                    m_lookLastPoint = position;                   // Save the pointer for a later move.
                    m_lookPointerID = pointerID;                  // Store the pointer using this control.
                    m_lookLastDelta.x = m_lookLastDelta.y = 0;    // These are for smoothing.
                    m_lookInUse = true;
                }
            }
            break;

        default:
            // ...
            // Handle mouse input here.
                                                // ...
            break;
        }
        break;
    }
    return;
}
```

If a [**PointerPressed**](https://msdn.microsoft.com/library/windows/apps/br208278) event has occurred in one of the three control regions, the move rectangle, the fire rectangle, or the rest of the screen (the look control), the **MoveLookController** assigns the pointer ID for the pointer that fired the event to a specific variable that corresponds to the region of the screen the event was fired in. For example, if the event occurred in the move rectangle, the **m\_movePointerID** variable is set to the pointer ID that fired the event. A Boolean "in use" variable (**m\_lookInUse**, in the example) is also set to indicate that the control has not been released yet.

Now, let's look at how the game sample handles the [**PointerMoved**](https://msdn.microsoft.com/library/windows/apps/br208276) touch screen event.

```cpp
void MoveLookController::OnPointerMoved(
    _In_ CoreWindow^ sender,
    _In_ PointerEventArgs^ args
    )
{
    PointerPoint^ point = args->CurrentPoint;
    UINT32 pointerID = point->PointerId;
    Point pointerPosition = point->Position;
    PointerPointProperties^ pointProperties = point->Properties;
    auto pointerDevice = point->PointerDevice;
    auto pointerDeviceType = pointerDevice->PointerDeviceType;

    XMFLOAT2 position = XMFLOAT2(pointerPosition.X, pointerPosition.Y);     // Convert to allow math.

    switch (m_state)
    {
    case MoveLookControllerState::Active:
        // Decide which control this pointer is operating.
        if (pointerID == m_movePointerID)     // This is the move pointer.
        {
            m_movePointerPosition = position;       // Save the current position.
        }
        else if (pointerID == m_lookPointerID)     // This is the look pointer.
        {
            // Look control
            XMFLOAT2 pointerDelta;
            pointerDelta.x = position.x - m_lookLastPoint.x;        // How far did the pointer move.
            pointerDelta.y = position.y - m_lookLastPoint.y;

            XMFLOAT2 rotationDelta;
            rotationDelta.x = pointerDelta.x * ROTATION_GAIN;       // Scale for control sensitivity.
            rotationDelta.y = pointerDelta.y * ROTATION_GAIN;
            m_lookLastPoint = position;                             // Save for the next time through.

            // Update our orientation based on the command.
            m_pitch -= rotationDelta.y;
            m_yaw   += rotationDelta.x;

            // Limit pitch to straight up or straight down.
            m_pitch = __max(-XM_PI / 2.0f, m_pitch);
            m_pitch = __min(+XM_PI / 2.0f, m_pitch);
        }
        else if (pointerID == m_firePointerID)
        {
            m_fireLastPoint = position;
        }
        else if (pointerID == m_mousePointerID)
        {
            // ...
        }
        break;
    }
}
```

The **MoveLookController** checks the pointer ID to determine where the event occurred, and takes one of the following actions:

-   If the [**PointerMoved**](https://msdn.microsoft.com/library/windows/apps/br208276) event occurred in the move or fire rectangle, update the pointer position for the controller.
-   If the [**PointerMoved**](https://msdn.microsoft.com/library/windows/apps/br208276) event occurred somewhere in the rest of the screen (defined as the look controls), calculate the change in pitch and yaw of the look direction vector.

Lastly, let's look at how the game sample handles the [**PointerReleased**](https://msdn.microsoft.com/library/windows/apps/br208279) touch screen event.

```cpp
void MoveLookController::OnPointerReleased(
    _In_ CoreWindow^ sender,
    _In_ PointerEventArgs^ args
    )
{
    PointerPoint^ point = args->CurrentPoint;
    UINT32 pointerID = point->PointerId;
    Point pointerPosition = point->Position;
    PointerPointProperties^ pointProperties = point->Properties;

    XMFLOAT2 position = XMFLOAT2(pointerPosition.X, pointerPosition.Y);     // Convert to allow math.
    switch (m_state)
    {
    case MoveLookControllerState::WaitForInput:
        if (m_buttonInUse && (pointerID == m_buttonPointerID))
        {
            m_buttonInUse = false;
            m_buttonPressed = true;
        }
        break;

    case MoveLookControllerState::Active:
        if (pointerID == m_movePointerID)
        {
            m_velocity = XMFLOAT3(0, 0, 0);      // Stop on release.
            m_moveInUse = false;
            m_movePointerID = 0;
        }
        else if (pointerID == m_lookPointerID)
        {
            m_lookInUse = false;
            m_lookPointerID = 0;
        }
        else if (pointerID == m_firePointerID)
        {
            m_fireInUse = false;
            m_firePointerID = 0;
        }
        else if (pointerID == m_mousePointerID)
        {
            // ...
        }
        break;
    }
}
```

If the ID of the pointer that fired the [**PointerReleased**](https://msdn.microsoft.com/library/windows/apps/br208279) event is the ID of the previously recorded move pointer, the **MoveLookController** sets the velocity to 0 because the player has stopped touching the move rectangle. If it didn't set the velocity to 0, the player would keep moving! If you want to implement some form of inertia, this is where you add the method that begins returning the velocity to 0 over future calls to **Update** from the game loop.

Otherwise, if the [**PointerReleased**](https://msdn.microsoft.com/library/windows/apps/br208279) event fired in the fire rectangle or the look region, the **MoveLookController** resets the specific pointer IDs.

That's the basics of how touch screen controls are implemented in the game sample. Let's move on to mouse and keyboard controls.

## Implementing mouse and keyboard controls


The game sample implements these mouse and keyboard controls:

-   The W, S, A, and D keys move the player view forward, backward, left, and right, respectively. Pressing X and the space bar move the view up and down, respectively.
-   Pressing the P key pauses the game.
-   Moving the mouse puts the player in control of the rotation (the pitch and yaw) of the camera view.
-   Clicking the left button fires a sphere.

To use the keyboard, the game sample registers for two extra events: [**CoreWindow::KeyUp**](https://msdn.microsoft.com/library/windows/apps/br208271) and [**CoreWindow::KeyDown**](https://msdn.microsoft.com/library/windows/apps/br208270), which handle the press and the release of a key, respectively.

```cpp
window->KeyDown +=
        ref new TypedEventHandler<CoreWindow^, KeyEventArgs^>(this, &MoveLookController::OnKeyDown);

window->KeyUp +=
        ref new TypedEventHandler<CoreWindow^, KeyEventArgs^>(this, &MoveLookController::OnKeyUp);
```

The mouse is treated a little differently from the touch controls, even though it uses a pointer. Obviously, it doesn't use the move and fire rectangles, as that would be very cumbersome for the player: how could they press the move and fire controls at the same time? As noted earlier, the **MoveLookController** controller engages the look controls whenever the mouse is moved, and engages the fire controls when the left mouse button is pressed, as shown here.

```cpp
void MoveLookController::OnPointerPressed(
    _In_ CoreWindow^ /* sender */,
    _In_ PointerEventArgs^ args
    )
{
    PointerPoint^ point = args->CurrentPoint;
    uint32 pointerID = point->PointerId;
    Point pointerPosition = point->Position;
    PointerPointProperties^ pointProperties = point->Properties;
    auto pointerDevice = point->PointerDevice;
    auto pointerDeviceType = pointerDevice->PointerDeviceType;

    XMFLOAT2 position = XMFLOAT2(pointerPosition.X, pointerPosition.Y);     // convert to allow math

    switch (m_state)
    {
    case MoveLookControllerState::WaitForInput:
        if (position.x > m_buttonUpperLeft.x &&
            position.x < m_buttonLowerRight.x &&
            position.y > m_buttonUpperLeft.y &&
            position.y < m_buttonLowerRight.y)
        {
            // Wait until the button is released before setting the variable.
            m_buttonPointerID = pointerID;
            m_buttonInUse = true;

        }
        break;

    case MoveLookControllerState::Active:
        switch (pointerDeviceType)
        {
        case Windows::Devices::Input::PointerDeviceType::Touch:
            // Check to see if this pointer is in the move control.
            if (position.x > m_moveUpperLeft.x &&
                position.x < m_moveLowerRight.x &&
                position.y > m_moveUpperLeft.y &&
                position.y < m_moveLowerRight.y)
            {
                if (!m_moveInUse)         // If no pointer is in this control yet:
                {
                    // Process a DPad touch down event.
                    m_moveFirstDown = position;                 // Save the location of the initial contact.
                    m_movePointerID = pointerID;                // Store the pointer using this control.
                    m_moveInUse = true;
                }
            }
            // Check to see if this pointer is in the fire control.
            else if (position.x > m_fireUpperLeft.x &&
                position.x < m_fireLowerRight.x &&
                position.y > m_fireUpperLeft.y &&
                position.y < m_fireLowerRight.y)
            {
                if (!m_fireInUse)
                {
                    m_fireLastPoint = position;
                    m_firePointerID = pointerID;
                    m_fireInUse = true;
                    if (!m_autoFire)
                    {
                        m_firePressed = true;
                    }
                }
            }
            else
            {
                if (!m_lookInUse)   // If no pointer is in this control yet:
                {
                    m_lookLastPoint = position;                   // Save the pointer for a later move.
                    m_lookPointerID = pointerID;                  // Store the pointer using this control.
                    m_lookLastDelta.x = m_lookLastDelta.y = 0;    // These are for smoothing.
                    m_lookInUse = true;
                }
            }
            break;

        default:
            bool rightButton = pointProperties->IsRightButtonPressed;
            bool leftButton = pointProperties->IsLeftButtonPressed;

            if (!m_autoFire && (!m_mouseLeftInUse && leftButton))
            {
                m_firePressed = true;
            }

            if (!m_mouseInUse)
            {
                m_mouseInUse = true;
                m_mouseLastPoint = position;
                m_mousePointerID = pointerID;
                m_mouseLeftInUse = leftButton;
                m_mouseRightInUse = rightButton;
                m_lookLastDelta.x = m_lookLastDelta.y = 0;          // These are for smoothing.
            }
            else
            {

            }
            break;
        }

        break;
    }

    return;
}
```

Now, let's look at how the game sample handles the [**PointerReleased**](https://msdn.microsoft.com/library/windows/apps/br208279) mouse event.

```cpp
void MoveLookController::OnPointerReleased(
    _In_ CoreWindow^ /* sender */,
    _In_ PointerEventArgs^ args
    )
{
    PointerPoint^ point = args->CurrentPoint;
    uint32 pointerID = point->PointerId;
    Point pointerPosition = point->Position;
    PointerPointProperties^ pointProperties = point->Properties;

    XMFLOAT2 position = XMFLOAT2(pointerPosition.X, pointerPosition.Y);     // Convert to allow math.
    switch (m_state)
    {
    case MoveLookControllerState::WaitForInput:
        if (m_buttonInUse && (pointerID == m_buttonPointerID))
        {
            m_buttonInUse = false;
            m_buttonPressed = true;
        }
        break;

    case MoveLookControllerState::Active:
        if (pointerID == m_movePointerID)
        {
            m_velocity = XMFLOAT3(0, 0, 0);      // Stop on release.
            m_moveInUse = false;
            m_movePointerID = 0;
        }
        else if (pointerID == m_lookPointerID)
        {
            m_lookInUse = false;
            m_lookPointerID = 0;
        }
        else if (pointerID == m_firePointerID)
        {
            m_fireInUse = false;
            m_firePointerID = 0;
        }
        else if (pointerID == m_mousePointerID)
        {
            bool rightButton = pointProperties->IsRightButtonPressed;
            bool leftButton = pointProperties->IsLeftButtonPressed;

            m_mouseInUse = false;

            // Don't clear the mouse pointer ID so that Move events still result in Look changes.
            // m_mousePointerID = 0;
            m_mouseLeftInUse = leftButton;
            m_mouseRightInUse = rightButton;
        }
        break;
    }
}
```

When the player stops pressing one of the mouse buttons, the input is complete: the spheres stop firing. But, because look is always enabled, the game continues to use the same mouse pointer to track the ongoing look events.

Now, let's look at the last of control types: the Xbox controller. It's handled separately from the touch and mouse controls, because it doesn't use the pointer object.

## Implementing Xbox controller controls


In the game sample, Xbox controller support is added by calls to the [XInput](https://msdn.microsoft.com/library/windows/desktop/hh405053) APIs, which are set of APIs designed to simplify programming for game controllers. In the game sample, we use the Xbox controller's left analog stick for player movement, the right analog stick for the look controls, and the right trigger to fire. We use the start button to pause and resume the game.

The **Update** method on the **MoveLookController** instance immediately checks to see if a game controller is connected, and then checks the controller state.

```cpp
void MoveLookController::UpdateGameController()
{
    if (!m_isControllerConnected)
    {
        // Check for controller connection by trying to get the capabilties.
        DWORD capsResult = XInputGetCapabilities(0, XINPUT_FLAG_GAMEPAD, &m_xinputCaps);
        if (capsResult != ERROR_SUCCESS)
        {
            return;
        }
        // Device is connected.
        m_isControllerConnected = true;
        m_xinputStartButtonInUse = false;
        m_xinputTriggerInUse = false;
    }

    DWORD stateResult = XInputGetState(0, &m_xinputState);
    if (stateResult != ERROR_SUCCESS)
    {
        // Device is no longer connected.
        m_isControllerConnected = false;
    }

    switch (m_state)
    {
    case MoveLookControllerState::WaitForInput:
        if (m_xinputState.Gamepad.wButtons & XINPUT_GAMEPAD_START)
        {
            m_xinputStartButtonInUse = true;
        }
        else if (m_xinputStartButtonInUse)
        {
            // Trigger one time only on button release.
            m_xinputStartButtonInUse = false;
            m_buttonPressed = true;
        }
        break;

    case MoveLookControllerState::Active:
        if (m_xinputState.Gamepad.wButtons & XINPUT_GAMEPAD_START)
        {
            m_xinputStartButtonInUse = true;
        }
        else if (m_xinputStartButtonInUse)
        {
            // Trigger one time only on button release.
            m_xinputStartButtonInUse = false;
            m_pausePressed = true;
        }
        // Use the Right Thumb joystick on the XBox controller to control
        // the eye point position control.
        // The controller input goes from -32767 to 32767.   We will normalize
        // this from -1 to 1 and keep a dead spot in the middle to avoid drift.

        if (m_xinputState.Gamepad.sThumbLX > XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE ||
            m_xinputState.Gamepad.sThumbLX < -XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE)
        {
            float x = (float)m_xinputState.Gamepad.sThumbLX/32767.0f;
            m_moveCommand.x -= x / fabsf(x);
        }

        if (m_xinputState.Gamepad.sThumbLY > XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE ||
            m_xinputState.Gamepad.sThumbLY < -XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE)
        {
            float y = (float)m_xinputState.Gamepad.sThumbLY/32767.0f;
            m_moveCommand.y += y / fabsf(y);
        }

        // Use the Left Thumb Joystick on the XBox controller to control
        // the look at control.
        // The controller input goes from -32767 to 32767.   We will normalize
        // this from -1 to 1 and keep a dead spot in the middle to avoid drift.
        XMFLOAT2 pointerDelta;
        if (m_xinputState.Gamepad.sThumbRX > XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE ||
            m_xinputState.Gamepad.sThumbRX < -XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE)
        {
            pointerDelta.x = (float)m_xinputState.Gamepad.sThumbRX/32767.0f;
            pointerDelta.x = pointerDelta.x * pointerDelta.x * pointerDelta.x;
        }
        else
        {
            pointerDelta.x = 0.0f;
        }
        if (m_xinputState.Gamepad.sThumbRY > XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE ||
            m_xinputState.Gamepad.sThumbRY < -XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE)
        {
            pointerDelta.y = (float)m_xinputState.Gamepad.sThumbRY/32767.0f;
            pointerDelta.y = pointerDelta.y * pointerDelta.y * pointerDelta.y;
        }
        else
        {
            pointerDelta.y = 0.0f;
        }

        XMFLOAT2 rotationDelta;
        rotationDelta.x = pointerDelta.x *  0.08f;       // Scale for control sensitivity.
        rotationDelta.y = pointerDelta.y *  0.08f;

        // Update our orientation based on the command.
        m_pitch += rotationDelta.y;
        m_yaw   += rotationDelta.x;

        // Limit pitch to straight up or straight down.
        m_pitch = __max(-XM_PI / 2.0f, m_pitch);
        m_pitch = __min(+XM_PI / 2.0f, m_pitch);

        // Check the state of the A button.  This is used to indicate fire control.

        if (m_xinputState.Gamepad.bRightTrigger > XINPUT_GAMEPAD_TRIGGER_THRESHOLD)
        {
            if (!m_autoFire && !m_xinputTriggerInUse)
            {
                m_firePressed = true;
            }
            m_xinputTriggerInUse = true;
        }
        else
        {
            m_xinputTriggerInUse = false;
        }
        break;
    }
}
```

If the game controller is in the **Active** state, this method checks to see if a user moved the left analog stick in a specific direction. But the movement on the stick in a specific direction must register as larger than the radius of the dead zone; otherwise, nothing will happen. This dead zone radius is necessary to present "drifting," which is when the controller picks up minute movements from the player's thumb as it rests on the stick. If we don't have this dead zone, the player can get annoyed very quickly, as the controls feel very fidgety.

The **Update** method then performs the same check on the right stick, to see if the player has changed the direction the camera is looking, as long as the movement on the stick is longer than another dead zone radius.

**Update** computes the new pitch and yaw, and then checks to see if the user pressed the right analog trigger, our fire button.

And that's how this sample implements a full set of control options. Again, remember that a good UWP app supports a range of control options, so players with different form factors and devices can play in the way they prefer!

## Next steps


We've reviewed every major component of a UWP DirectX game except one: audio! Music and sound effects are important to any game, so let's discuss [adding sound](tutorial--adding-sound.md)!

## Complete sample code for this section


MoveLookController.h

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#pragma once

// Uncomment to print debug tracing.
// #define MOVELOOKCONTROLLER_TRACE 1

enum class MoveLookControllerState
{
    None,
    WaitForInput,
    Active,
};

ref class MoveLookController
{
internal:
    MoveLookController();

    void Initialize(
        _In_ Windows::UI::Core::CoreWindow^ window
        );
    void SetMoveRect(
        _In_ DirectX::XMFLOAT2 upperLeft,
        _In_ DirectX::XMFLOAT2 lowerRight
        );
    void SetFireRect(
        _In_ DirectX::XMFLOAT2 upperLeft,
        _In_ DirectX::XMFLOAT2 lowerRight
        );
    void WaitForPress(
        _In_ DirectX::XMFLOAT2 UpperLeft,
        _In_ DirectX::XMFLOAT2 LowerRight
        );
    void WaitForPress();

    void Update();
    bool IsFiring();
    bool IsPressComplete();
    bool IsPauseRequested();

    DirectX::XMFLOAT3 Velocity();
    DirectX::XMFLOAT3 LookDirection();
    float Pitch();
    void  Pitch(_In_ float pitch);
    float Yaw();
    void  Yaw(_In_ float yaw);
    bool  Active();
    void  Active(_In_ bool active);

    bool AutoFire();
    void AutoFire(_In_ bool AutoFire);

protected:
    void OnPointerPressed(
        _In_ Windows::UI::Core::CoreWindow^ sender,
        _In_ Windows::UI::Core::PointerEventArgs^ args
        );
    void OnPointerMoved(
        _In_ Windows::UI::Core::CoreWindow^ sender,
        _In_ Windows::UI::Core::PointerEventArgs^ args
        );
    void OnPointerReleased(
        _In_ Windows::UI::Core::CoreWindow^ sender,
        _In_ Windows::UI::Core::PointerEventArgs^ args
        );
    void OnPointerExited(
        _In_ Windows::UI::Core::CoreWindow^ sender,
        _In_ Windows::UI::Core::PointerEventArgs^ args
        );
    void OnKeyDown(
        _In_ Windows::UI::Core::CoreWindow^ sender,
        _In_ Windows::UI::Core::KeyEventArgs^ args
        );
    void OnKeyUp(
        _In_ Windows::UI::Core::CoreWindow^ sender,
        _In_ Windows::UI::Core::KeyEventArgs^ args
        );

    void OnMouseMoved(
        _In_ Windows::Devices::Input::MouseDevice^ mouseDevice,
        _In_ Windows::Devices::Input::MouseEventArgs^ args
        );

#ifdef MOVELOOKCONTROLLER_TRACE
    void DebugTrace(const wchar_t *format, ...);
#endif

private:
    // Properties of the controller object
    MoveLookControllerState     m_state;
    DirectX::XMFLOAT3           m_velocity;             // How far we move it this frame
    float                       m_pitch;
    float                       m_yaw;                  // Orientation euler angles in radians

    // Properties of the Move control
    DirectX::XMFLOAT2           m_moveUpperLeft;        // Bounding box where this control will activate
    DirectX::XMFLOAT2           m_moveLowerRight;
    bool                        m_moveInUse;            // The move control is in use.
    uint32                      m_movePointerID;        // The id of the pointer in this control.
    DirectX::XMFLOAT2           m_moveFirstDown;        // The point where the initial contact occurred.
    DirectX::XMFLOAT2           m_movePointerPosition;  // The point where the move pointer is currently located.
    DirectX::XMFLOAT3           m_moveCommand;          // The net command from the move control.

    // Properties of the Look control
    bool                        m_lookInUse;            // The look control is in use.
    uint32                      m_lookPointerID;        // The id of the pointer in this control.
    DirectX::XMFLOAT2           m_lookLastPoint;        // The last point (from last frame)
    DirectX::XMFLOAT2           m_lookLastDelta;        // for smoothing.

    // Properties of the Fire control
    bool                        m_autoFire;
    bool                        m_firePressed;
    DirectX::XMFLOAT2           m_fireUpperLeft;        // The bounding box where this control will activate.
    DirectX::XMFLOAT2           m_fireLowerRight;
    bool                        m_fireInUse;            // The fire control is in use.
    UINT32                      m_firePointerID;        // The id of the pointer in this control.
    DirectX::XMFLOAT2           m_fireLastPoint;        // The last fire position.

    // Properties of the Mouse control. This is a combination of Look and Fire.
    bool                        m_mouseInUse;
    uint32                      m_mousePointerID;
    DirectX::XMFLOAT2           m_mouseLastPoint;
    bool                        m_mouseLeftInUse;
    bool                        m_mouseRightInUse;

    bool                        m_buttonInUse;
    uint32                      m_buttonPointerID;
    DirectX::XMFLOAT2           m_buttonUpperLeft;
    DirectX::XMFLOAT2           m_buttonLowerRight;
    bool                        m_buttonPressed;
    bool                        m_pausePressed;

    // XBox Input related members
    bool                        m_isControllerConnected;  // Do we have a controller connected?
    XINPUT_CAPABILITIES         m_xinputCaps;             // The capabilites of the controller.
    XINPUT_STATE                m_xinputState;            // The current state of the controller.
    bool                        m_xinputStartButtonInUse;
    bool                        m_xinputTriggerInUse;

    // Input states for Keyboard
    bool                        m_forward;
    bool                        m_back;                    // States for movement
    bool                        m_left;
    bool                        m_right;
    bool                        m_up;
    bool                        m_down;
    bool                        m_pause;

private:
    void     ResetState();
    void     UpdateGameController();
};
```

MoveLookController.cpp

```cpp
//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

#include "pch.h"
#include "MoveLookController.h"
#include "DirectXSample.h"

using namespace Windows::UI::Core;
using namespace Windows::UI::Input;
using namespace Windows::UI;
using namespace Windows::Foundation;
using namespace Microsoft::WRL;
using namespace DirectX;
using namespace Windows::Devices::Input;
using namespace Windows::System;

#define ROTATION_GAIN 0.008f        // The sensitivity adjustment for the look controller.
#define MOVEMENT_GAIN 2.f           // The sensitivity adjustment for the move controller.

// A basic Move/Look Controller class such as in an FPS
// horizontal (x-z-plane) movement on left virtual joystick
// also supports WASD keyboard input
// steering and orientation via left mouse down or touch drag.

//----------------------------------------------------------------------

MoveLookController::MoveLookController():
    m_autoFire(true),
    m_isControllerConnected(false)
{
}

//----------------------------------------------------------------------
// Set up the controls supported by this controller.

void MoveLookController::Initialize(
    _In_ CoreWindow^ window
    )
{
    window->PointerPressed +=
        ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(this, &MoveLookController::OnPointerPressed);

    window->PointerMoved +=
        ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(this, &MoveLookController::OnPointerMoved);

    window->PointerReleased +=
        ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(this, &MoveLookController::OnPointerReleased);

    window->PointerExited +=
        ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(this, &MoveLookController::OnPointerExited);

    window->KeyDown +=
        ref new TypedEventHandler<CoreWindow^, KeyEventArgs^>(this, &MoveLookController::OnKeyDown);

    window->KeyUp +=
        ref new TypedEventHandler<CoreWindow^, KeyEventArgs^>(this, &MoveLookController::OnKeyUp);

    // A separate handler for mouse only relative mouse movement events.
    Windows::Devices::Input::MouseDevice::GetForCurrentView()->MouseMoved +=
        ref new TypedEventHandler<MouseDevice^, MouseEventArgs^>(this, &MoveLookController::OnMouseMoved);

    ResetState();
    m_state = MoveLookControllerState::None;

    m_pitch = 0.0f;
    m_yaw   = 0.0f;
}

//----------------------------------------------------------------------

bool MoveLookController::IsPauseRequested()
{
    switch (m_state)
    {
    case MoveLookControllerState::Active:
        UpdateGameController();
        if (m_pausePressed)
        {

            m_pausePressed = false;
            return true;
        }
        else
        {
            return false;
        }
    }
    return false;
}

//----------------------------------------------------------------------

bool MoveLookController::IsFiring()
{
    if (m_state == MoveLookControllerState::Active)
    {
        if (m_autoFire)
        {
            return (m_fireInUse || (m_mouseInUse && m_mouseLeftInUse) || m_xinputTriggerInUse);
        }
        else
        {
            if (m_firePressed)
            {
                m_firePressed = false;
                return true;
            }
        }
    }
    return false;
}

//----------------------------------------------------------------------

bool MoveLookController::IsPressComplete()
{
    switch (m_state)
    {
    case MoveLookControllerState::WaitForInput:
        UpdateGameController();
        if (m_buttonPressed)
        {

            m_buttonPressed = false;
            return true;
        }
        else
        {
            return false;
        }
        break;
    }

    return false;
}

//----------------------------------------------------------------------

void MoveLookController::OnPointerPressed(
    _In_ CoreWindow^ /* sender */,
    _In_ PointerEventArgs^ args
    )
{
    PointerPoint^ point = args->CurrentPoint;
    uint32 pointerID = point->PointerId;
    Point pointerPosition = point->Position;
    PointerPointProperties^ pointProperties = point->Properties;
    auto pointerDevice = point->PointerDevice;
    auto pointerDeviceType = pointerDevice->PointerDeviceType;

    XMFLOAT2 position = XMFLOAT2(pointerPosition.X, pointerPosition.Y);     // Convert to allow math.

    switch (m_state)
    {
    case MoveLookControllerState::WaitForInput:
        if (position.x > m_buttonUpperLeft.x &&
            position.x < m_buttonLowerRight.x &&
            position.y > m_buttonUpperLeft.y &&
            position.y < m_buttonLowerRight.y)
        {
            // Wait until button released before setting variable.
            m_buttonPointerID = pointerID;
            m_buttonInUse = true;

        }
        break;

    case MoveLookControllerState::Active:
        switch (pointerDeviceType)
        {
        case Windows::Devices::Input::PointerDeviceType::Touch:
            // Check to see if this pointer is in the move control.
            if (position.x > m_moveUpperLeft.x &&
                position.x < m_moveLowerRight.x &&
                position.y > m_moveUpperLeft.y &&
                position.y < m_moveLowerRight.y)
            {
                if (!m_moveInUse)         // If no pointer is in this control yet:
                {
                    // Process a DPad touch down event.
                    m_moveFirstDown = position;                 // Save the location of the initial contact.
                    m_movePointerID = pointerID;                // Store the pointer using this control.
                    m_moveInUse = true;
                }
            }
            // Check to see if this pointer is in the fire control.
            else if (position.x > m_fireUpperLeft.x &&
                position.x < m_fireLowerRight.x &&
                position.y > m_fireUpperLeft.y &&
                position.y < m_fireLowerRight.y)
            {
                if (!m_fireInUse)
                {
                    m_fireLastPoint = position;
                    m_firePointerID = pointerID;
                    m_fireInUse = true;
                    if (!m_autoFire)
                    {
                        m_firePressed = true;
                    }
                }
            }
            else
            {
                if (!m_lookInUse)   // If no pointer is in this control yet:
                {
                    m_lookLastPoint = position;                   // Save the point for a later move.
                    m_lookPointerID = pointerID;                  // Store the pointer using this control.
                    m_lookLastDelta.x = m_lookLastDelta.y = 0;    // These are for smoothing.
                    m_lookInUse = true;
                }
            }
            break;

        default:
            bool rightButton = pointProperties->IsRightButtonPressed;
            bool leftButton = pointProperties->IsLeftButtonPressed;

            if (!m_autoFire && (!m_mouseLeftInUse && leftButton))
            {
                m_firePressed = true;
            }

            if (!m_mouseInUse)
            {
                m_mouseInUse = true;
                m_mouseLastPoint = position;
                m_mousePointerID = pointerID;
                m_mouseLeftInUse = leftButton;
                m_mouseRightInUse = rightButton;
                m_lookLastDelta.x = m_lookLastDelta.y = 0;          // These are for smoothing.
            }
            else
            {

            }
            break;
        }

        break;
    }

    return;
}

//----------------------------------------------------------------------

void MoveLookController::OnPointerMoved(
    _In_ CoreWindow^ /* sender */,
    _In_ PointerEventArgs^ args
    )
{
    PointerPoint^ point = args->CurrentPoint;
    uint32 pointerID = point->PointerId;
    Point pointerPosition = point->Position;
    PointerPointProperties^ pointProperties = point->Properties;
    auto pointerDevice = point->PointerDevice;

    XMFLOAT2 position = XMFLOAT2(pointerPosition.X, pointerPosition.Y);     // Convert to allow math.

    switch (m_state)
    {
    case MoveLookControllerState::Active:
        // Decide which control this pointer is operating.
        if (pointerID == m_movePointerID)     // This is the move pointer.
        {
            m_movePointerPosition = position;       // Save the current position.
        }
        else if (pointerID == m_lookPointerID)     // This is the look pointer.
        {
            // Look control
            XMFLOAT2 pointerDelta;
            pointerDelta.x = position.x - m_lookLastPoint.x;        // How far did the pointer move?
            pointerDelta.y = position.y - m_lookLastPoint.y;

            XMFLOAT2 rotationDelta;
            rotationDelta.x = pointerDelta.x * ROTATION_GAIN;       // Scale for control sensitivity.
            rotationDelta.y = pointerDelta.y * ROTATION_GAIN;
            m_lookLastPoint = position;                             // Save for the next time through.

            // Update our orientation based on the command.
            m_pitch -= rotationDelta.y;
            m_yaw   += rotationDelta.x;

            // Limit pitch to straight up or straight down.
            float limit = XM_PI / 2.0f - 0.01f;
            m_pitch = __max(-limit, m_pitch);
            m_pitch = __min(+limit, m_pitch);

            // Keep longitude in the same range by wrapping.
            if (m_yaw >  XM_PI)
            {
                m_yaw -= XM_PI * 2.0f;
            }
            else if (m_yaw < -XM_PI)
            {
                m_yaw += XM_PI * 2.0f;
            }
        }
        else if (pointerID == m_firePointerID)
        {
            m_fireLastPoint = position;
        }
        else if (pointerID == m_mousePointerID)
        {
            m_mouseLeftInUse  = pointProperties->IsLeftButtonPressed;
            m_mouseRightInUse = pointProperties->IsRightButtonPressed;;
            m_mouseLastPoint = position;                            // save for next time through

            // Handle mouse movement via a separate relative mouse movement handler (OnMouseMoved).
        }
        break;
    }
}

//----------------------------------------------------------------------

void MoveLookController::OnMouseMoved(
    _In_ MouseDevice^ /* mouseDevice */,
    _In_ MouseEventArgs^ args
    )
{
    // Handle Mouse Input via dedicated relative movement handler.

    switch (m_state)
    {
    case MoveLookControllerState::Active:
        XMFLOAT2 mouseDelta;
        mouseDelta.x = static_cast<float>(args->MouseDelta.X);
        mouseDelta.y = static_cast<float>(args->MouseDelta.Y);

        XMFLOAT2 rotationDelta;
        rotationDelta.x  = mouseDelta.x * ROTATION_GAIN;   // Scale for control sensitivity.
        rotationDelta.y  = mouseDelta.y * ROTATION_GAIN;

        // Update our orientation based on the command.
        m_pitch -= rotationDelta.y;
        m_yaw   += rotationDelta.x;

        // Limit pitch to straight up or straight down.
        float limit = XM_PI / 2.0f - 0.01f;
        m_pitch = __max(-limit, m_pitch);
        m_pitch = __min(+limit, m_pitch);

        // Keep longitude in same range by wrapping.
        if (m_yaw >  XM_PI)
        {
            m_yaw -= XM_PI * 2.0f;
        }
        else if (m_yaw < -XM_PI)
        {
            m_yaw += XM_PI * 2.0f;
        }
        break;
    }
}

//----------------------------------------------------------------------

void MoveLookController::OnPointerReleased(
    _In_ CoreWindow^ /* sender */,
    _In_ PointerEventArgs^ args
    )
{
    PointerPoint^ point = args->CurrentPoint;
    uint32 pointerID = point->PointerId;
    Point pointerPosition = point->Position;
    PointerPointProperties^ pointProperties = point->Properties;

    XMFLOAT2 position = XMFLOAT2(pointerPosition.X, pointerPosition.Y);     // Convert to allow math.
    switch (m_state)
    {
    case MoveLookControllerState::WaitForInput:
        if (m_buttonInUse && (pointerID == m_buttonPointerID))
        {
            m_buttonInUse = false;
            m_buttonPressed = true;
        }
        break;

    case MoveLookControllerState::Active:
        if (pointerID == m_movePointerID)
        {
            m_velocity = XMFLOAT3(0, 0, 0);      // Stop on release.
            m_moveInUse = false;
            m_movePointerID = 0;
        }
        else if (pointerID == m_lookPointerID)
        {
            m_lookInUse = false;
            m_lookPointerID = 0;
        }
        else if (pointerID == m_firePointerID)
        {
            m_fireInUse = false;
            m_firePointerID = 0;
        }
        else if (pointerID == m_mousePointerID)
        {
            bool rightButton = pointProperties->IsRightButtonPressed;
            bool leftButton = pointProperties->IsLeftButtonPressed;

            m_mouseInUse = false;

            // Don't clear the mouse pointer ID so that Move events still result in Look changes.
            // m_mousePointerID = 0;
            m_mouseLeftInUse = leftButton;
            m_mouseRightInUse = rightButton;
        }
        break;
    }
}

//----------------------------------------------------------------------

void MoveLookController::OnPointerExited(
    _In_ CoreWindow^ /* sender */,
    _In_ PointerEventArgs^ args
    )
{
    PointerPoint^ point = args->CurrentPoint;
    uint32 pointerID = point->PointerId;
    Point pointerPosition = point->Position;
    PointerPointProperties^ pointProperties = point->Properties;

    XMFLOAT2 position  = XMFLOAT2(pointerPosition.X, pointerPosition.Y);        // Convert to allow math.

    switch (m_state)
    {
    case MoveLookControllerState::WaitForInput:
        if (m_buttonInUse && (pointerID == m_buttonPointerID))
        {
            m_buttonInUse = false;
            m_buttonPressed = false;
        }
        break;

    case MoveLookControllerState::Active:
        if (pointerID == m_movePointerID)
        {
            m_velocity = XMFLOAT3(0, 0, 0);      // Stop on release.
            m_moveInUse = false;
            m_movePointerID = 0;
        }
        else if (pointerID == m_lookPointerID)
        {
            m_lookInUse = false;
            m_lookPointerID = 0;
        }
        else if (pointerID == m_firePointerID)
        {
            m_fireInUse = false;
            m_firePointerID = 0;
        }
        else if (pointerID == m_mousePointerID)
        {
            m_mouseInUse = false;
            m_mousePointerID = 0;
            m_mouseLeftInUse = false;
            m_mouseRightInUse = false;
        }
        break;
    }
}

//----------------------------------------------------------------------

void MoveLookController::OnKeyDown(
    _In_ CoreWindow^ /* sender */,
    _In_ KeyEventArgs^ args
    )
{
    Windows::System::VirtualKey Key;
    Key = args->VirtualKey;

    // Figure out the command from the keyboard.
    if (Key == VirtualKey::W)       // forward
        m_forward = true;
    if (Key == VirtualKey::S)       // back
        m_back = true;
    if (Key == VirtualKey::A)       // left
        m_left = true;
    if (Key == VirtualKey::D)       // right
        m_right = true;
    if (Key == VirtualKey::Space)   // up
        m_up = true;
    if (Key == VirtualKey::X)       // down
        m_down = true;
    if (Key == VirtualKey::P)       // Pause
        m_pause = true;
}

//----------------------------------------------------------------------

void MoveLookController::OnKeyUp(
    _In_ CoreWindow^ /* sender */,
    _In_ KeyEventArgs^ args
    )
{
    Windows::System::VirtualKey Key;
    Key = args->VirtualKey;

    // Figure out the command from the keyboard.
    if (Key == VirtualKey::W)       // Forward
        m_forward = false;
    if (Key == VirtualKey::S)       // Back
        m_back = false;
    if (Key == VirtualKey::A)       // Left
        m_left = false;
    if (Key == VirtualKey::D)       // Right
        m_right = false;
    if (Key == VirtualKey::Space)   // Up
        m_up = false;
    if (Key == VirtualKey::X)       // Down
        m_down = false;
    if (Key == VirtualKey::P)
    {
        if (m_pause)
        {
            // Trigger pause only one time on button release.
            m_pausePressed = true;
            m_pause = false;
        }
    }
}

//----------------------------------------------------------------------

void MoveLookController::ResetState()
{
    // Reset the state of the controller.
    // Disable any active pointer IDs to stop all interaction.
    m_buttonPressed = false;
    m_pausePressed = false;
    m_buttonInUse = false;
    m_moveInUse = false;
    m_lookInUse = false;
    m_fireInUse = false;
    m_mouseInUse = false;
    m_mouseLeftInUse = false;
    m_mouseRightInUse = false;
    m_movePointerID = 0;
    m_lookPointerID = 0;
    m_firePointerID = 0;
    m_mousePointerID = 0;
    m_velocity = XMFLOAT3(0.0f, 0.0f, 0.0f);

    m_xinputStartButtonInUse = false;
    m_xinputTriggerInUse = false;

    m_moveCommand = XMFLOAT3(0.0f, 0.0f, 0.0f);
    m_forward = false;
    m_back = false;
    m_left = false;
    m_right = false;
    m_up = false;
    m_down = false;
    m_pause = false;
}

//----------------------------------------------------------------------

void MoveLookController::SetMoveRect (
    _In_ XMFLOAT2 upperLeft,
    _In_ XMFLOAT2 lowerRight
    )
{
    m_moveUpperLeft  = upperLeft;
    m_moveLowerRight = lowerRight;
}

//----------------------------------------------------------------------

void MoveLookController::SetFireRect (
    _In_ XMFLOAT2 upperLeft,
    _In_ XMFLOAT2 lowerRight
    )
{
    m_fireUpperLeft  = upperLeft;
    m_fireLowerRight = lowerRight;
}

//----------------------------------------------------------------------

void MoveLookController::WaitForPress(
    _In_ XMFLOAT2 upperLeft,
    _In_ XMFLOAT2 lowerRight
    )
{

    ResetState();
    m_state = MoveLookControllerState::WaitForInput;
    m_buttonUpperLeft  = upperLeft;
    m_buttonLowerRight = lowerRight;

    // Turn on the mouse cursor.
    CoreWindow::GetForCurrentThread()->PointerCursor = ref new CoreCursor(CoreCursorType::Arrow, 0);
}

//----------------------------------------------------------------------

void MoveLookController::WaitForPress()
{
    ResetState();
    m_state = MoveLookControllerState::WaitForInput;
    m_buttonUpperLeft.x = 0.0f;
    m_buttonUpperLeft.y = 0.0f;
    m_buttonLowerRight.x = 0.0f;
    m_buttonLowerRight.y = 0.0f;

    // Turn on the mouse cursor.
    CoreWindow::GetForCurrentThread()->PointerCursor = ref new CoreCursor(CoreCursorType::Arrow, 0);
}

//----------------------------------------------------------------------

XMFLOAT3 MoveLookController::Velocity()
{
    return m_velocity;
}

//----------------------------------------------------------------------

XMFLOAT3 MoveLookController::LookDirection()
{
    XMFLOAT3 lookDirection;

    float r = cosf(m_pitch);              // In the plane
    lookDirection.y = sinf(m_pitch);      // Vertical
    lookDirection.z = r * cosf(m_yaw);    // Fwd-back
    lookDirection.x = r * sinf(m_yaw);    // Left-right

    return lookDirection;
}

//----------------------------------------------------------------------

float MoveLookController::Pitch()
{
    return m_pitch;
}

//----------------------------------------------------------------------

void MoveLookController::Pitch(_In_ float pitch)
{
    m_pitch = pitch;
}

//----------------------------------------------------------------------

float MoveLookController::Yaw()
{
    return m_yaw;
}

//----------------------------------------------------------------------

void MoveLookController::Yaw(_In_ float yaw)
{
    m_yaw = yaw;
}

//----------------------------------------------------------------------

void MoveLookController::Active(_In_ bool active)
{
    ResetState();

    if (active)
    {
        m_state = MoveLookControllerState::Active;
        // Turn the mouse cursor off (hidden).
        CoreWindow::GetForCurrentThread()->PointerCursor = nullptr;
    }
    else
    {
        m_state = MoveLookControllerState::None;
        // Turn the mouse cursor on.
        auto window = CoreWindow::GetForCurrentThread();
        if (window)
        {
            // Protect case where there isn't a window associated with the current thread.
            // This happens on initialization.
            window->PointerCursor = ref new CoreCursor(CoreCursorType::Arrow, 0);
        }
    }
}

//----------------------------------------------------------------------

bool MoveLookController::Active()
{
    if (m_state == MoveLookControllerState::Active)
    {
        return true;
    }
    else
    {
        return false;
    }
}

//----------------------------------------------------------------------

void MoveLookController::AutoFire(_In_ bool autoFire)
{
    m_autoFire = autoFire;
}

//----------------------------------------------------------------------

bool MoveLookController::AutoFire()
{
    return m_autoFire;
}

//----------------------------------------------------------------------

void MoveLookController::Update()
{
    UpdateGameController();

    if (m_moveInUse)
    {
        // Move control.
        XMFLOAT2 pointerDelta;

        pointerDelta.x = m_movePointerPosition.x - m_moveFirstDown.x;
        pointerDelta.y = m_movePointerPosition.y - m_moveFirstDown.y;

        // Figure out the command from the virtual joystick.
        XMFLOAT3 commandDirection = XMFLOAT3(0.0f, 0.0f, 0.0f);
        if (fabsf(pointerDelta.x) > 16.0f)         // leave 32 pixel-wide dead spot for being still
            m_moveCommand.x -= pointerDelta.x/fabsf(pointerDelta.x);

        if (fabsf(pointerDelta.y) > 16.0f)
            m_moveCommand.y -= pointerDelta.y/fabsf(pointerDelta.y);
    }

    // Poll our state bits set by the keyboard input events.
    if (m_forward)
    {
        m_moveCommand.y += 1.0f;
    }
    if (m_back)
    {
        m_moveCommand.y -= 1.0f;
    }
    if (m_left)
    {
        m_moveCommand.x += 1.0f;
    }
    if (m_right)
    {
        m_moveCommand.x -= 1.0f;
    }
    if (m_up)
    {
        m_moveCommand.z += 1.0f;
    }
    if (m_down)
    {
        m_moveCommand.z -= 1.0f;
    }

    // Make sure that 45 deg cases are not faster.
    if (fabsf(m_moveCommand.x) > 0.1f ||
        fabsf(m_moveCommand.y) > 0.1f ||
        fabsf(m_moveCommand.z) > 0.1f)
    {
        XMStoreFloat3(&m_moveCommand, XMVector3Normalize(XMLoadFloat3(&m_moveCommand)));
    }

    // Rotate command to align with our direction (world coordinates).
    XMFLOAT3 wCommand;
    wCommand.x =  m_moveCommand.x * cosf(m_yaw) - m_moveCommand.y * sinf(m_yaw);
    wCommand.y =  m_moveCommand.x * sinf(m_yaw) + m_moveCommand.y * cosf(m_yaw);
    wCommand.z =  m_moveCommand.z;

    // Scale for sensitivity adjustment.
    // Our velocity is based on the command, y is up.
    m_velocity.x = -wCommand.x * MOVEMENT_GAIN;
    m_velocity.z =  wCommand.y * MOVEMENT_GAIN;
    m_velocity.y =  wCommand.z * MOVEMENT_GAIN;

    // Clear the movement input accumulator for use during next frame.
    m_moveCommand = XMFLOAT3(0.0f, 0.0f, 0.0f);
}

//----------------------------------------------------------------------

void MoveLookController::UpdateGameController()
{
    if (!m_isControllerConnected)
    {
        // Check for controller connection by trying to get the capabilties.
        DWORD capsResult = XInputGetCapabilities(0, XINPUT_FLAG_GAMEPAD, &m_xinputCaps);
        if (capsResult != ERROR_SUCCESS)
        {
            return;
        }
        // The device is connected.
        m_isControllerConnected = true;
        m_xinputStartButtonInUse = false;
        m_xinputTriggerInUse = false;
    }

    DWORD stateResult = XInputGetState(0, &m_xinputState);
    if (stateResult != ERROR_SUCCESS)
    {
        // The device is no longer connected.
        m_isControllerConnected = false;
    }

    switch (m_state)
    {
    case MoveLookControllerState::WaitForInput:
        if (m_xinputState.Gamepad.wButtons & XINPUT_GAMEPAD_START)
        {
            m_xinputStartButtonInUse = true;
        }
        else if (m_xinputStartButtonInUse)
        {
            // Trigger one time only on button release.
            m_xinputStartButtonInUse = false;
            m_buttonPressed = true;
        }
        break;

    case MoveLookControllerState::Active:
        if (m_xinputState.Gamepad.wButtons & XINPUT_GAMEPAD_START)
        {
            m_xinputStartButtonInUse = true;
        }
        else if (m_xinputStartButtonInUse)
        {
            // Trigger one time only on button release.
            m_xinputStartButtonInUse = false;
            m_pausePressed = true;
        }
        // Use the Right Thumb joystick on the XBox controller to control
        // the eye point position control.
        // The controller input goes from -32767 to 32767.   We will normalize
        // this from -1 to 1 and keep a dead spot in the middle to avoid drift.

        if (m_xinputState.Gamepad.sThumbLX > XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE ||
            m_xinputState.Gamepad.sThumbLX < -XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE)
        {
            float x = (float)m_xinputState.Gamepad.sThumbLX/32767.0f;
            m_moveCommand.x -= x / fabsf(x);
        }

        if (m_xinputState.Gamepad.sThumbLY > XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE ||
            m_xinputState.Gamepad.sThumbLY < -XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE)
        {
            float y = (float)m_xinputState.Gamepad.sThumbLY/32767.0f;
            m_moveCommand.y += y / fabsf(y);
        }

        // Use the Left Thumb Joystick on the XBox controller to control
        // the look at control.
        // The controller input goes from -32767 to 32767.   We will normalize
        // this from -1 to 1 and keep a dead spot in the middle to avoid drift.
        XMFLOAT2 pointerDelta;
        if (m_xinputState.Gamepad.sThumbRX > XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE ||
            m_xinputState.Gamepad.sThumbRX < -XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE)
        {
            pointerDelta.x = (float)m_xinputState.Gamepad.sThumbRX/32767.0f;
            pointerDelta.x = pointerDelta.x * pointerDelta.x * pointerDelta.x;
        }
        else
        {
            pointerDelta.x = 0.0f;
        }
        if (m_xinputState.Gamepad.sThumbRY > XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE ||
            m_xinputState.Gamepad.sThumbRY < -XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE)
        {
            pointerDelta.y = (float)m_xinputState.Gamepad.sThumbRY/32767.0f;
            pointerDelta.y = pointerDelta.y * pointerDelta.y * pointerDelta.y;
        }
        else
        {
            pointerDelta.y = 0.0f;
        }

        XMFLOAT2 rotationDelta;
        rotationDelta.x = pointerDelta.x *  0.08f;       // scale for control sensitivity
        rotationDelta.y = pointerDelta.y *  0.08f;

        // Update our orientation based on the command.
        m_pitch += rotationDelta.y;
        m_yaw   += rotationDelta.x;

        // Limit pitch to straight up or straight down.
        m_pitch = __max(-XM_PI / 2.0f, m_pitch);
        m_pitch = __min(+XM_PI / 2.0f, m_pitch);

        // Check the state of the A button.  This is used to indicate fire control.

        if (m_xinputState.Gamepad.bRightTrigger > XINPUT_GAMEPAD_TRIGGER_THRESHOLD)
        {
            if (!m_autoFire && !m_xinputTriggerInUse)
            {
                m_firePressed = true;
            }
            m_xinputTriggerInUse = true;
        }
        else
        {
            m_xinputTriggerInUse = false;
        }
        break;
    }
}
```

> **Note**  
This article is for Windows 10 developers writing Universal Windows Platform (UWP) apps. If you’re developing for Windows 8.x or Windows Phone 8.x, see the [archived documentation](http://go.microsoft.com/fwlink/p/?linkid=619132).

 

## Related topics


[Create a simple UWP game with DirectX](tutorial--create-your-first-metro-style-directx-game.md)

 

 




