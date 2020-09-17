---
title: Relative mouse movement
description: Use relative mouse controls, which don't use the system cursor and don't return absolute screen coordinates, to track the pixel delta between mouse movements in games.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, mouse, input
ms.assetid: 08c35e05-2822-4a01-85b8-44edb9b6898f
ms.localizationpriority: medium
---
# Relative mouse movement and CoreWindow

In games, the mouse is a common control option that is familiar to many players, and is likewise essential to many genres of games, including first- and third-person shooters, and real-time strategy games. Here we discuss the implementation of relative mouse controls, which don't use the system cursor and don't return absolute screen coordinates; instead, they track the pixel delta between mouse movements.

Some apps, such as games, use the mouse as a more general input device. For example, a 3-D modeler might use mouse input to orient a 3-D object by simulating a virtual trackball; or a game might use the mouse to change the direction of the viewing camera via mouse-look controls. 

In these scenarios, the app requires relative mouse data. Relative mouse values represent how far the mouse moved since the last frame, rather than the absolute x-y coordinate values within a window or screen. Also, apps often hide the mouse cursor since the position of the cursor with respect to the screen coordinates is not relevant when manipulating a 3-D object or scene. 

When the user takes an action that moves the app into a relative 3-D object/scene manipulation mode, the app must: 
- Ignore default mouse handling.
- Enable relative mouse handling.
- Hide the mouse cursor by setting it a null pointer (nullptr). 

When the user takes an action that moves the app out of a relative 3-D object/scene manipulation mode, the app must: 
- Enable default/absolute mouse handling.
- Turn off relative mouse handling. 
- Set the mouse cursor to a non-null value (which makes it visible).

> **Note**  
With this pattern, the location of the absolute mouse cursor is preserved on entering the cursorless relative mode. The cursor re-appears in the same screen coordinate location as it was previous to enabling the relative mouse movement mode.

 

## Handling relative mouse movement


To access relative mouse delta values, register for the [MouseDevice::MouseMoved](/uwp/api/windows.devices.input.mousedevice.mousemoved) event as shown here.


```cpp


// register handler for relative mouse movement events
Windows::Devices::Input::MouseDevice::GetForCurrentView()->MouseMoved +=
        ref new TypedEventHandler<MouseDevice^, MouseEventArgs^>(this, &MoveLookController::OnMouseMoved);


```

```cpp


void MoveLookController::OnMouseMoved(
    _In_ Windows::Devices::Input::MouseDevice^ mouseDevice,
    _In_ Windows::Devices::Input::MouseEventArgs^ args
    )
{
    float2 pointerDelta;
    pointerDelta.x = static_cast<float>(args->MouseDelta.X);
    pointerDelta.y = static_cast<float>(args->MouseDelta.Y);

    float2 rotationDelta;
    rotationDelta = pointerDelta * ROTATION_GAIN;	// scale for control sensitivity

    // update our orientation based on the command
    m_pitch -= rotationDelta.y;						// mouse y increases down, but pitch increases up
    m_yaw   -= rotationDelta.x;						// yaw defined as CCW around y-axis

    // limit pitch to straight up or straight down
    float limit = (float)(M_PI/2) - 0.01f;
    m_pitch = (float) __max( -limit, m_pitch );
    m_pitch = (float) __min( +limit, m_pitch );

    // keep longitude in useful range by wrapping
    if ( m_yaw >  M_PI )
        m_yaw -= (float)M_PI*2;
    else if ( m_yaw < -M_PI )
        m_yaw += (float)M_PI*2;
}

```

The event handler in this code example, **OnMouseMoved**, renders the view based on the movements of the mouse. The position of the mouse pointer is passed to the handler as a [MouseEventArgs](/uwp/api/Windows.Devices.Input.MouseEventArgs) object. 

Skip over processing of absolute mouse data from the [CoreWindow::PointerMoved](/uwp/api/windows.ui.core.corewindow.pointermoved) event when your app changes to handling relative mouse movement values. However, only skip this input if the **CoreWindow::PointerMoved** event occurred as the result of mouse input (as opposed to touch input). The cursor is hidden by setting the [CoreWindow::PointerCursor](/uwp/api/windows.ui.core.corewindow.pointercursor) to **nullptr**. 

## Returning to absolute mouse movement

When the app exits the 3-D object or scene manipulation mode and no longer uses relative mouse movement (such as when it returns to a menu screen), return to normal processing of absolute mouse movement. At this time, stop reading relative mouse data, restart the processing of standard mouse (and pointer) events, and set [CoreWindow::PointerCursor](/uwp/api/windows.ui.core.corewindow.pointercursor) to non-null value. 

> **Note**  
When your app is in the 3-D object/scene manipulation mode (processing relative mouse movements with the cursor off), the mouse cannot invoke edge UI such as the charms, back stack, or app bar. Therefore, it is important to provide a mechanism to exit this particular mode, such as the commonly used **Esc** key.

## Related topics

* [Move-look controls for games](tutorial--adding-move-look-controls-to-your-directx-game.md) 
* [Touch controls for games](tutorial--adding-touch-controls-to-your-directx-game.md)