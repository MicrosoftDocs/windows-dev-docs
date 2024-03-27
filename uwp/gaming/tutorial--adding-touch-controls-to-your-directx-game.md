---
title: Touch controls for games
description: Learn how to add basic touch controls to your Universal Windows Platform (UWP) C++ game with DirectX.
ms.assetid: 9d40e6e4-46a9-97e9-b848-522d61e8e109
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, touch, controls, directx, input
ms.localizationpriority: medium
---
# Touch controls for games



Learn how to add basic touch controls to your Universal Windows Platform (UWP) C++ game with DirectX. We show you how to add touch-based controls to move a fixed-plane camera in a Direct3D environment, where dragging with a finger or stylus shifts the camera perspective.

You can incorporate these controls in games where you want the player to drag to scroll or pan over a 3D environment, such as a map or playfield. For example, in a strategy or puzzle game, you can use these controls to let the player view a game environment that is larger than the screen by panning left or right.

> **Note**  Our code also works with mouse-based panning controls. The pointer related events are abstracted by the Windows Runtime APIs, so they can handle either touch- or mouse-based pointer events.

 

## Objectives


-   Create a simple touch drag control for panning a fixed-plane camera in a DirectX game.

## Set up the basic touch event infrastructure


First, we define our basic controller type, the **CameraPanController**, in this case. Here, we define a controller as an abstract idea, the set of behaviors the user can perform.

The **CameraPanController** class is a regularly refreshed collection of information about the camera controller state, and provides a way for our app to obtain that information from its update loop.

```cpp
using namespace Windows::UI::Core;
using namespace Windows::System;
using namespace Windows::Foundation;
using namespace Windows::Devices::Input;
#include <directxmath.h>

// Methods to get input from the UI pointers
ref class CameraPanController
{
}
```

Now, let's create a header that defines the state of the camera controller, and the basic methods and event handlers that implement the camera controller interactions.

```cpp
ref class CameraPanController
{
private:
    // Properties of the controller object
    DirectX::XMFLOAT3 m_position;               // the position of the camera

    // Properties of the camera pan control
    bool m_panInUse;                
    uint32 m_panPointerID;          
    DirectX::XMFLOAT2 m_panFirstDown;           
    DirectX::XMFLOAT2 m_panPointerPosition;   
    DirectX::XMFLOAT3 m_panCommand;         
    
internal:
    // Accessor to set the position of the controller
    void SetPosition( _In_ DirectX::XMFLOAT3 pos );

       // Accessor to set the fixed "look point" of the controller
       DirectX::XMFLOAT3 get_FixedLookPoint();

    // Returns the position of the controller object
    DirectX::XMFLOAT3 get_Position();

public:

    // Methods to get input from the UI pointers
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

    // Set up the Controls supported by this controller
    void Initialize( _In_ Windows::UI::Core::CoreWindow^ window );

    void Update( Windows::UI::Core::CoreWindow ^window );

};  // Class CameraPanController
```

The private fields contain the current state of the camera controller. Let's review them.

-   **m\_position** is the position of the camera in the scene space. In this example, the z-coordinate value is fixed at 0. We could use a DirectX::XMFLOAT2 to represent this value, but for the purposes of this sample and future extensibility, we use a DirectX::XMFLOAT3. We pass this value through the **get\_Position** property to the app itself so it can update the viewport accordingly.
-   **m\_panInUse** is a Boolean value that indicates whether a pan operation is active; or, more specifically, whether the player is touching the screen and moving the camera.
-   **m\_panPointerID** is a unique ID for the pointer. We won't use this in the sample, but it's a good practice to associate your controller state class with a specific pointer.
-   **m\_panFirstDown** is the point on the screen where the player first touched the screen or clicked the mouse during the camera pan action. We use this value later to set a dead zone to prevent jitter when the screen is touched, or if the mouse shakes a little.
-   **m\_panPointerPosition** is the point on the screen where the player has currently moved the pointer. We use it to determine what direction the player wanted to move by examining it relative to **m\_panFirstDown**.
-   **m\_panCommand** is the final computed command for the camera controller: up, down, left, or right. Because we are working with a camera fixed to the x-y plane, this could be a DirectX::XMFLOAT2 value instead.

We use these 3 event handlers to update the camera controller state info.

-   **OnPointerPressed** is an event handler that our app calls when the players presses a finger onto the touch surface and the pointer is moved to the coordinates of the press.
-   **OnPointerMoved** is an event handler that our app calls when the player swipes a finger across the touch surface. It updates with the new coordinates of the drag path.
-   **OnPointerReleased** is an event handler that our app calls when the player removes the pressing finger from the touch surface.

Finally, we use these methods and properties to initialize, access, and update the camera controller state information.

-   **Initialize** is an event handler that our app calls to initialize the controls and attach them to the [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) object that describes your display window.
-   **SetPosition** is a method that our app calls to set the (x, y, and z) coordinates of your controls in the scene space. Note that our z-coordinate is 0 throughout this tutorial.
-   **get\_Position** is a property that our app accesses to get the current position of the camera in the scene space. You use this property as the way of communicating the current camera position to the app.
-   **get\_FixedLookPoint** is a property that our app accesses to get the current point toward which the controller camera is facing. In this example, it is locked normal to the x-y plane.
-   **Update** is a method that reads the controller state and updates the camera position. You continually call this &lt;something&gt; from the app's main loop to refresh the camera controller data and the camera position in the scene space.

Now, you have here all the components you need to implement touch controls. You can detect when and where the touch or mouse pointer events have occurred, and what the action is. You can set the position and orientation of the camera relative to the scene space, and track the changes. Finally, you can communicate the new camera position to the calling app.

Now, let's connect these pieces together.

## Create the basic touch events


The Windows Runtime event dispatcher provides 3 events we want our app to handle:

-   [**PointerPressed**](/uwp/api/windows.ui.core.corewindow.pointerpressed)
-   [**PointerMoved**](/uwp/api/windows.ui.core.corewindow.pointermoved)
-   [**PointerReleased**](/uwp/api/windows.ui.core.corewindow.pointerreleased)

These events are implemented on the [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) type. We assume that you have a **CoreWindow** object to work with. For more info, see [How to set up your UWP C++ app to display a DirectX view](/previous-versions/windows/apps/hh465077(v=win.10)).

As these events fire while our app is running, the handlers update the camera controller state info defined in our private fields.

First, let's populate the touch pointer event handlers. In the first event handler, **OnPointerPressed**, we get the x-y coordinates of the pointer from the [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) that manages our display when the user touches the screen or clicks the mouse.

**OnPointerPressed**

```cpp
void CameraPanController::OnPointerPressed(
                                           _In_ CoreWindow^ sender,
                                           _In_ PointerEventArgs^ args)
{
    // Get the current pointer position.
    uint32 pointerID = args->CurrentPoint->PointerId;
    DirectX::XMFLOAT2 position = DirectX::XMFLOAT2( args->CurrentPoint->Position.X, args->CurrentPoint->Position.Y );

    auto device = args->CurrentPoint->PointerDevice;
    auto deviceType = device->PointerDeviceType;
    

       if ( !m_panInUse )   // If no pointer is in this control yet.
    {
       m_panFirstDown = position;                   // Save the location of the initial contact.
       m_panPointerPosition = position;
       m_panPointerID = pointerID;              // Store the id of the pointer using this control.
       m_panInUse = TRUE;
    }
    
}
```

We use this handler to let the current **CameraPanController** instance know that camera controller should be treated as active by setting **m\_panInUse** to TRUE. That way, when the app calls **Update** , it will use the current position data to update the viewport.

Now that we've established the base values for the camera movement when the user touches the screen or click-presses in the display window, we must determine what to do when the user either drags the screen press or moves the mouse with button pressed.

The **OnPointerMoved** event handler fires whenever the pointer moves, at every tick that the player drags it on the screen. We need to keep the app aware of the current location of the pointer, and this is how we do it.

**OnPointerMoved**

```cpp
void CameraPanController::OnPointerMoved(
                                        _In_ CoreWindow ^sender,
                                        _In_ PointerEventArgs ^args)
{
    uint32 pointerID = args->CurrentPoint->PointerId;
    DirectX::XMFLOAT2 position = DirectX::XMFLOAT2( args->CurrentPoint->Position.X, args->CurrentPoint->Position.Y );

    m_panPointerPosition = position;
}
```

Finally, we need to deactivate the camera pan behavior when the player stops touching the screen. We use **OnPointerReleased**, which is called when [**PointerReleased**](/uwp/api/windows.ui.core.corewindow.pointerreleased) is fired, to set **m\_panInUse** to FALSE and turn off the camera pan movement, and set the pointer ID to 0.

**OnPointerReleased**

```cpp
void CameraPanController::OnPointerReleased(
                                             _In_ CoreWindow ^sender,
                                             _In_ PointerEventArgs ^args)
{
    uint32 pointerID = args->CurrentPoint->PointerId;
    DirectX::XMFLOAT2 position = DirectX::XMFLOAT2( args->CurrentPoint->Position.X, args->CurrentPoint->Position.Y );

    m_panInUse = FALSE;
    m_panPointerID = 0;
}
```

## Initialize the touch controls and the controller state


Let's hook the events and initialize all the basic state fields of the camera controller.

**Initialize**

```cpp
void CameraPanController::Initialize( _In_ CoreWindow^ window )
{

    // Start receiving touch/mouse events.
    window->PointerPressed += 
    ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(this, &CameraPanController::OnPointerPressed);

    window->PointerMoved += 
    ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(this, &CameraPanController::OnPointerMoved);

    window->PointerReleased += 
    ref new TypedEventHandler<CoreWindow^, PointerEventArgs^>(this, &CameraPanController::OnPointerReleased);


    // Initialize the state of the controller.
    m_panInUse = FALSE;             
    m_panPointerID = 0;

    //  Initialize this as it is reset on every frame.
    m_panCommand = DirectX::XMFLOAT3( 0.0f, 0.0f, 0.0f );

}
```

**Initialize** takes a reference to the app's [**CoreWindow**](/uwp/api/Windows.UI.Core.CoreWindow) instance as a parameter and registers the event handlers we developed to the appropriate events on that **CoreWindow**.

## Getting and setting the position of the camera controller


Let's define some methods to get and set the position of the camera controller in the scene space.

```cpp
void CameraPanController::SetPosition( _In_ DirectX::XMFLOAT3 pos )
{
    m_position = pos;
}

// Returns the position of the controller object
DirectX::XMFLOAT3 CameraPanController::get_Position()
{
    return m_position;
}

DirectX::XMFLOAT3 CameraPanController::get_FixedLookPoint()
{
    // For this sample, we don't need to use the trig functions because our
    // look point is fixed. 
    DirectX::XMFLOAT3 result= m_position;
    result.z += 1.0f;
    return result;    

}
```

**SetPosition** is a public method that we can call from our app if we need to set the camera controller position to a specific point.

**get\_Position** is our most important public property: it's the way our app gets the current position of the camera controller in the scene space so it can update the viewport accordingly.

**get\_FixedLookPoint** is a public property that, in this example, obtains a look point that is normal to the x-y plane. You can change this method to use the trigonometric functions, sin and cos, when calculating the x, y, and z coordinate values if you want to create more oblique angles for the fixed camera.

## Updating the camera controller state information


Now, we perform our calculations that convert the pointer coordinate info tracked in **m\_panPointerPosition** into new coordinate info respective of our 3D scene space. Our app calls this method every time we refresh the main app loop. In it we compute the new position information we want to pass to the app which is used to update the view matrix before projection into the viewport.

```cpp

void CameraPanController::Update( CoreWindow ^window )
{
    if ( m_panInUse )
    {
        pointerDelta.x = m_panPointerPosition.x - m_panFirstDown.x;
        pointerDelta.y = m_panPointerPosition.y - m_panFirstDown.y;

        if ( pointerDelta.x > 16.0f )        // Leave 32 pixel-wide dead spot for being still.
            m_panCommand.x += 1.0f;
        else
            if ( pointerDelta.x < -16.0f )
                m_panCommand.x += -1.0f;

        if ( pointerDelta.y > 16.0f )        
            m_panCommand.y += 1.0f;
        else
            if (pointerDelta.y < -16.0f )
                m_panCommand.y += -1.0f;
    }

       DirectX::XMFLOAT3 command = m_panCommand;
   
    // Our velocity is based on the command.
    DirectX::XMFLOAT3 Velocity;
    Velocity.x =  command.x;
    Velocity.y =  command.y;
    Velocity.z =  0.0f;

    // Integrate
    m_position.x = m_position.x + Velocity.x;
    m_position.y = m_position.y + Velocity.y;
    m_position.z = m_position.z + Velocity.z;

    // Clear the movement input accumulator for use during the next frame.
    m_panCommand = DirectX::XMFLOAT3( 0.0f, 0.0f, 0.0f );

}
```

Because we don't want touch or mouse jitter to make our camera panning jerky, we set a dead zone around the pointer with a diameter of 32 pixels. We also have a velocity value, which in this case is 1:1 with the pixel traversal of the pointer past the dead zone. You can adjust this behavior to slow down or speed up the rate of movement.

## Updating the view matrix with the new camera position


We can now obtain a scene space coordinate that our camera is focused on, and which is updated whenever you tell your app to do so (every 60 seconds in the main app loop, for example). This pseudocode suggests the calling behavior you can implement:

```cpp
 myCameraPanController->Update( m_window ); 

 // Update the view matrix based on the camera position.
 myCamera->MyMethodToComputeViewMatrix(
        myController->get_Position(),        // The position in the 3D scene space.
        myController->get_FixedLookPoint(),      // The point in the space we are looking at.
        DirectX::XMFLOAT3( 0, 1, 0 )                    // The axis that is "up" in our space.
        );  
```

Congratulations! You've implemented a simple set of camera panning touch controls in your game.


 

 

 