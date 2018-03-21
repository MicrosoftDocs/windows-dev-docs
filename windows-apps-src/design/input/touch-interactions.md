---
author: Karl-Bridge-Microsoft
Description: Create Universal Windows Platform (UWP) apps with intuitive and distinctive user interaction experiences that are optimized for touch but are functionally consistent across input devices.
title: Touch interactions
ms.assetid: DA6EBC88-EB18-4418-A98A-457EA1DEA88A
label: Touch interactions
template: detail.hbs
keywords: touch, pointer, input, user interaction
ms.author: kbridge
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
ms.localizationpriority: medium
---

# Touch interactions


Design your app with the expectation that touch will be the primary input method of your users. If you use UWP controls, support for touchpad, mouse, and pen/stylus requires no additional programming, because UWP apps provide this for free.

However, keep in mind that a UI optimized for touch is not always superior to a traditional UI. Both provide advantages and disadvantages that are unique to a technology and application. In the move to a touch-first UI, it is important to understand the core differences between touch (including touchpad), pen/stylus, mouse, and keyboard input.

> **Important APIs**: [**Windows.UI.Xaml.Input**](https://msdn.microsoft.com/library/windows/apps/br227994), [**Windows.UI.Core**](https://msdn.microsoft.com/library/windows/apps/br208383), [**Windows.Devices.Input**](https://msdn.microsoft.com/library/windows/apps/br225648)


Many devices have multi-touch screens that support using one or more fingers (or touch contacts) as input. The touch contacts, and their movement, are interpreted as touch gestures and manipulations to support various user interactions.

The Universal Windows Platform (UWP) includes a number of different mechanisms for handling touch input, enabling you to create an immersive experience that your users can explore with confidence. Here, we cover the basics of using touch input in a UWP app.

Touch interactions require three things:

-   A touch-sensitive display.
-   The direct contact (or proximity to, if the display has proximity sensors and supports hover detection) of one or more fingers on that display.
-   Movement of the touch contacts (or lack thereof, based on a time threshold).

The input data provided by the touch sensor can be:

-   Interpreted as a physical gesture for direct manipulation of one or more UI elements (such as panning, rotating, resizing, or moving). In contrast, interacting with an element through its properties window, dialog box, or other UI affordance is considered indirect manipulation.
-   Recognized as an alternative input method, such as mouse or pen.
-   Used to complement or modify aspects of other input methods, such as smudging an ink stroke drawn with a pen.

Touch input typically involves the direct manipulation of an element on the screen. The element responds immediately to any touch contact within its hit test area, and reacts appropriately to any subsequent movement of the touch contacts, including removal.

Custom touch gestures and interactions should be designed carefully. They should be intuitive, responsive, and discoverable, and they should let users explore your app with confidence.

Ensure that app functionality is exposed consistently across every supported input device type. If necessary, use some form of indirect input mode, such as text input for keyboard interactions, or UI affordances for mouse and pen.

Remember that traditional input devices (such as mouse and keyboard), are familiar and appealing to many users. They can offer speed, accuracy, and tactile feedback that touch might not.

Providing unique and distinctive interaction experiences for all input devices will support the widest range of capabilities and preferences, appeal to the broadest possible audience, and attract more customers to your app.

## Compare touch interaction requirements

The following table shows some of the differences between input devices that you should consider when you design touch-optimized UWP apps.

<table>
<tbody><tr><th>Factor</th><th>Touch interactions</th><th>Mouse, keyboard, pen/stylus interactions</th><th>Touchpad</th></tr>
<tr><td rowspan="3">Precision</td><td>The contact area of a fingertip is greater than a single x-y coordinate, which increases the chances of unintended command activations.</td><td>The mouse and pen/stylus supply a precise x-y coordinate.</td><td>Same as mouse.</td></tr>
<tr><td>The shape  of the contact area changes throughout the movement.  </td><td>Mouse movements and pen/stylus strokes supply precise x-y coordinates. Keyboard focus is explicit.</td><td>Same as mouse.</td></tr>
<tr><td>There is no mouse cursor to assist with targeting.</td><td>The mouse cursor, pen/stylus cursor, and keyboard focus all assist with targeting.</td><td>Same as mouse.</td></tr>
<tr><td rowspan="3">Human anatomy</td><td>Fingertip movements are imprecise, because a straight-line motion with one or more fingers is difficult. This is due to the curvature of hand joints and the number of joints involved in the motion.</td><td>It's easier to perform a straight-line motion with the mouse or pen/stylus because the hand that controls them travels a shorter physical distance than the cursor on the screen.</td><td>Same as mouse.</td></tr>
<tr><td>Some areas on the touch surface of a display device can be difficult to reach due to finger posture and the user's grip on the device.</td><td>The mouse and pen/stylus can reach any part of the screen while any control should be accessible by the keyboard through tab order. </td><td>Finger posture and grip can be an issue.</td></tr>
<tr><td>Objects might be obscured by one or more fingertips or the user's hand. This is known as occlusion.</td><td>Indirect input devices do not cause  occlusion.</td><td>Same as mouse.</td></tr>
<tr><td>Object state</td><td>Touch uses a two-state model: the touch surface of a display device  is either touched (on) or not (off). There is no hover state that can trigger additional visual feedback.</td><td>
<p>A mouse, pen/stylus, and keyboard all expose a three-state model: up (off), down (on), and hover (focus).</p>
<p>Hover lets users explore and learn through tooltips  associated with UI elements. Hover and focus effects  can relay which objects are interactive and also help with targeting. 
</p>
</td><td>Same as mouse.</td></tr>
<tr><td rowspan="2">Rich interaction</td><td>Supports multi-touch: multiple input points (fingertips) on a touch surface.</td><td>Supports a single input point.</td><td>Same as touch.</td></tr>
<tr><td>Supports direct manipulation of objects through gestures such as tapping, dragging, sliding, pinching, and rotating.</td><td>No support for direct manipulation as mouse, pen/stylus, and keyboard are indirect input devices.</td><td>Same as mouse.</td></tr>
</tbody></table>



**Note**  
Indirect input has had the benefit of more than 25 years of refinement. Features such as hover-triggered tooltips have been designed to solve UI exploration specifically for touchpad, mouse, pen/stylus, and keyboard input. UI features like this have been re-designed for the rich experience provided by touch input, without compromising the user experience for these other devices.

 

## Use touch feedback

Appropriate visual feedback during interactions with your app helps users recognize, learn, and adapt to how their interactions are interpreted by both the app and the Windows platform. Visual feedback can indicate successful interactions, relay system status, improve the sense of control, reduce errors, help users understand the system and input device, and encourage interaction.

Visual feedback is critical when the user relies on touch input for activities that require accuracy and precision based on location. Display feedback whenever and wherever touch input is detected, to help the user understand any custom targeting rules that are defined by your app and its controls.


## Targeting

Targeting is optimized through:

-   Touch target sizes

    Clear size guidelines ensure that applications provide a comfortable UI that contains objects and controls that are easy and safe to target.

-   Contact geometry

    The entire contact area of the finger determines the most likely target object.

-   Scrubbing

    Items within a group are easily re-targeted by dragging the finger between them (for example, radio buttons). The current item is activated when the touch is released.

-   Rocking

    Densely packed items (for example, hyperlinks) are easily re-targeted by pressing the finger down and, without sliding, rocking it back and forth over the items. Due to occlusion, the current item is identified through a tooltip or the status bar and is activated when the touch is released.

## Accuracy

Design for sloppy interactions by using:

-   Snap-points that can make it easier to stop at desired locations when users interact with content.
-   Directional "rails" that can assist with vertical or horizontal panning, even when the hand moves in a slight arc. For more information, see [Guidelines for panning](guidelines-for-panning.md).

## Occlusion

Finger and hand occlusion is avoided through:

-   Size and positioning of UI

    Make UI elements big enough so that they cannot be completely covered by a fingertip contact area.

    Position menus and pop-ups above the contact area whenever possible.

-   Tooltips

    Show tooltips when a user maintains finger contact on an object. This is useful for describing object functionality. The user can drag the fingertip off the object to avoid invoking the tooltip.

    For small objects, offset tooltips so they are not covered by the fingertip contact area. This is helpful for targeting.

-   Handles for precision

    Where precision is required (for example, text selection), provide selection handles that are offset to improve accuracy. For more information, see [Guidelines for selecting text and images (Windows Runtime apps)](guidelines-for-textselection.md).

## Timing

Avoid timed mode changes in favor of direct manipulation. Direct manipulation simulates the direct, real-time physical handling of an object. The object responds as the fingers are moved.

A timed interaction, on the other hand, occurs after a touch interaction. Timed interactions typically depend on invisible thresholds like time, distance, or speed to determine what command to perform. Timed interactions have no visual feedback until the system performs the action.

Direct manipulation provides a number of benefits over timed interactions:

-   Instant visual feedback during interactions make users feel more engaged, confident, and in control.
-   Direct manipulations make it safer to explore a system because they are reversible—users can easily step back through their actions in a logical and intuitive manner.
-   Interactions that directly affect objects and mimic real world interactions are more intuitive, discoverable, and memorable. They don't rely on obscure or abstract interactions.
-   Timed interactions can be difficult to perform, as users must reach arbitrary and invisible thresholds.

In addition, the following are strongly recommended:

-   Manipulations should not be distinguished by the number of fingers used.
-   Interactions should support compound manipulations. For example, pinch to zoom while dragging the fingers to pan.
-   Interactions should not be distinguished by time. The same interaction should have the same outcome regardless of the time taken to perform it. Time-based activations introduce mandatory delays for users and detract from both the immersive nature of direct manipulation and the perception of system responsiveness.

    **Note**  An exception to this is where you use specific timed interactions to assist in learning and exploration (for example, press and hold).

     

-   Appropriate descriptions and visual cues have a great effect on the use of advanced interactions.


## App views


Tweak the user interaction experience through the pan/scroll and zoom settings of your app views. An app view dictates how a user accesses and manipulates your app and its content. Views also provide behaviors such as inertia, content boundary bounce, and snap points.

Pan and scroll settings of the [**ScrollViewer**](https://msdn.microsoft.com/library/windows/apps/br209527) control dictate how users navigate within a single view, when the content of the view doesn't fit within the viewport. A single view can be, for example, a page of a magazine or book, the folder structure of a computer, a library of documents, or a photo album.

Zoom settings apply to both optical zoom (supported by the [**ScrollViewer**](https://msdn.microsoft.com/library/windows/apps/br209527) control) and the [**Semantic Zoom**](https://msdn.microsoft.com/library/windows/apps/hh702601) control. Semantic Zoom is a touch-optimized technique for presenting and navigating large sets of related data or content within a single view. It works by using two distinct modes of classification, or zoom levels. This is analogous to panning and scrolling within a single view. Panning and scrolling can be used in conjunction with Semantic Zoom.

Use app views and events to modify the pan/scroll and zoom behaviors. This can provide a smoother interaction experience than is possible through the handling of pointer and gesture events.

For more info about app views, see [Controls, layouts, and text](https://msdn.microsoft.com/library/windows/apps/mt228348).

## Custom touch interactions


If you implement your own interaction support, keep in mind that users expect an intuitive experience involving direct interaction with the UI elements in your app. We recommend that you model your custom interactions on the platform control libraries to keep things consistent and discoverable. The controls in these libraries provide the full user interaction experience, including standard interactions, animated physics effects, visual feedback, and accessibility. Create custom interactions only if there is a clear, well-defined requirement and basic interactions don't support your scenario.

To provide customized touch support, you can handle various [**UIElement**](https://msdn.microsoft.com/library/windows/apps/br208911) events. These events are grouped into three levels of abstraction.

-   Static gesture events are triggered after an interaction is complete. Gesture events include [**Tapped**](https://msdn.microsoft.com/library/windows/apps/br208985), [**DoubleTapped**](https://msdn.microsoft.com/library/windows/apps/br208922), [**RightTapped**](https://msdn.microsoft.com/library/windows/apps/br208984), and [**Holding**](https://msdn.microsoft.com/library/windows/apps/br208928).

    You can disable gesture events on specific elements by setting [**IsTapEnabled**](https://msdn.microsoft.com/library/windows/apps/br208939), [**IsDoubleTapEnabled**](https://msdn.microsoft.com/library/windows/apps/br208931), [**IsRightTapEnabled**](https://msdn.microsoft.com/library/windows/apps/br208937), and [**IsHoldingEnabled**](https://msdn.microsoft.com/library/windows/apps/br208935) to **false**.

-   Pointer events such as [**PointerPressed**](https://msdn.microsoft.com/library/windows/apps/br208971) and [**PointerMoved**](https://msdn.microsoft.com/library/windows/apps/br208970) provide low-level details for each touch contact, including pointer motion and the ability to distinguish press and release events.

    A pointer is a generic input type with a unified event mechanism. It exposes basic info, such as screen position, on the active input source, which can be touch, touchpad, mouse, or pen.

-   Manipulation gesture events, such as [**ManipulationStarted**](https://msdn.microsoft.com/library/windows/apps/br208950), indicate an ongoing interaction. They start firing when the user touches an element and continue until the user lifts their finger(s), or the manipulation is canceled.

    Manipulation events include multi-touch interactions such as zooming, panning, or rotating, and interactions that use inertia and velocity data such as dragging. The information provided by the manipulation events doesn't identify the form of the interaction that was performed, but rather includes data such as position, translation delta, and velocity. You can use this touch data to determine the type of interaction that should be performed.

Here is the basic set of touch gestures supported by the UWP.

| Name           | Type                 | Description                                                                            |
|----------------|----------------------|----------------------------------------------------------------------------------------|
| Tap            | Static gesture       | One finger touches the screen and lifts up.                                            |
| Press and hold | Static gesture       | One finger touches the screen and stays in place.                                      |
| Slide          | Manipulation gesture | One or more fingers touch the screen and move in the same direction.                   |
| Swipe          | Manipulation gesture | One or more fingers touch the screen and move a short distance in the same direction.  |
| Turn           | Manipulation gesture | Two or more fingers touch the screen and move in a clockwise or counter-clockwise arc. |
| Pinch          | Manipulation gesture | Two or more fingers touch the screen and move closer together.                         |
| Stretch        | Manipulation gesture | Two or more fingers touch the screen and move farther apart.                           |

 

<!-- mijacobs: Removing for now. We don't have a real page to link to yet. 
For more info about gestures, manipulations, and interactions, see [Custom user interactions](custom-user-input-portal.md).
-->

## Gesture events


For details about individual controls, see [Controls list](https://msdn.microsoft.com/library/windows/apps/mt185406).

## Pointer events


Pointer events are raised by a variety of active input sources, including touch, touchpad, pen, and mouse (they replace traditional mouse events.)

Pointer events are based on a single input point (finger, pen tip, mouse cursor) and do not support velocity-based interactions.

Here is a list of pointer events and their related event argument.

| Event or class                                                       | Description                                                   |
|----------------------------------------------------------------------|---------------------------------------------------------------|
| [**PointerPressed**](https://msdn.microsoft.com/library/windows/apps/br208971)             | Occurs when a single finger touches the screen.               |
| [**PointerReleased**](https://msdn.microsoft.com/library/windows/apps/br208972)           | Occurs when that same touch contact is lifted.                |
| [**PointerMoved**](https://msdn.microsoft.com/library/windows/apps/br208970)                 | Occurs when the pointer is dragged across the screen.         |
| [**PointerEntered**](https://msdn.microsoft.com/library/windows/apps/br208968)             | Occurs when a pointer enters the hit test area of an element. |
| [**PointerExited**](https://msdn.microsoft.com/library/windows/apps/br208969)               | Occurs when a pointer exits the hit test area of an element.  |
| [**PointerCanceled**](https://msdn.microsoft.com/library/windows/apps/br208964)           | Occurs when a touch contact is abnormally lost.               |
| [**PointerCaptureLost**](https://msdn.microsoft.com/library/windows/apps/br208965)     | Occurs when a pointer capture is taken by another element.    |
| [**PointerWheelChanged**](https://msdn.microsoft.com/library/windows/apps/br208973)   | Occurs when the delta value of a mouse wheel changes.         |
| [**PointerRoutedEventArgs**](https://msdn.microsoft.com/library/windows/apps/hh943076) | Provides data for all pointer events.                         |

 

The following example shows how to use the [**PointerPressed**](https://msdn.microsoft.com/library/windows/apps/br208971), [**PointerReleased**](https://msdn.microsoft.com/library/windows/apps/br208972), and [**PointerExited**](https://msdn.microsoft.com/library/windows/apps/br208969) events to handle a tap interaction on a [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle) object.

First, a [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle) named `touchRectangle` is created in Extensible Application Markup Language (XAML).

```XAML
<Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
    <Rectangle Name="touchRectangle"
           Height="100" Width="200" Fill="Blue" />
</Grid>
```
Next, listeners for the [**PointerPressed**](https://msdn.microsoft.com/library/windows/apps/br208971), [**PointerReleased**](https://msdn.microsoft.com/library/windows/apps/br208972), and [**PointerExited**](https://msdn.microsoft.com/library/windows/apps/br208969) events are specified.

```cpp
MainPage::MainPage()
{
    InitializeComponent();

    // Pointer event listeners.
    touchRectangle->PointerPressed += ref new PointerEventHandler(this, &MainPage::touchRectangle_PointerPressed);
    touchRectangle->PointerReleased += ref new PointerEventHandler(this, &MainPage::touchRectangle_PointerReleased);
    touchRectangle->PointerExited += ref new PointerEventHandler(this, &MainPage::touchRectangle_PointerExited);
}
```

```cs
public MainPage()
{
    this.InitializeComponent();

    // Pointer event listeners.
    touchRectangle.PointerPressed += touchRectangle_PointerPressed;
    touchRectangle.PointerReleased += touchRectangle_PointerReleased;
    touchRectangle.PointerExited += touchRectangle_PointerExited;
}
```

```vb
Public Sub New()

    ' This call is required by the designer.
    InitializeComponent()

    ' Pointer event listeners.
    AddHandler touchRectangle.PointerPressed, AddressOf touchRectangle_PointerPressed
    AddHandler touchRectangle.PointerReleased, AddressOf Me.touchRectangle_PointerReleased
    AddHandler touchRectangle.PointerExited, AddressOf touchRectangle_PointerExited

End Sub
```

Finally, the [**PointerPressed**](https://msdn.microsoft.com/library/windows/apps/br208971) event handler increases the [**Height**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Height) and [**Width**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Width) of the [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle), while the [**PointerReleased**](https://msdn.microsoft.com/library/windows/apps/br208972) and [**PointerExited**](https://msdn.microsoft.com/library/windows/apps/br208969) event handlers set the **Height** and **Width** back to their starting values.

```cpp
// Handler for pointer exited event.
void MainPage::touchRectangle_PointerExited(Object^ sender, PointerRoutedEventArgs^ e)
{
    Rectangle^ rect = (Rectangle^)sender;

    // Pointer moved outside Rectangle hit test area.
    // Reset the dimensions of the Rectangle.
    if (nullptr != rect)
    {
        rect->Width = 200;
        rect->Height = 100;
    }
}

// Handler for pointer released event.
void MainPage::touchRectangle_PointerReleased(Object^ sender, PointerRoutedEventArgs^ e)
{
    Rectangle^ rect = (Rectangle^)sender;

    // Reset the dimensions of the Rectangle.
    if (nullptr != rect)
    {
        rect->Width = 200;
        rect->Height = 100;
    }
}

// Handler for pointer pressed event.
void MainPage::touchRectangle_PointerPressed(Object^ sender, PointerRoutedEventArgs^ e)
{
    Rectangle^ rect = (Rectangle^)sender;

    // Change the dimensions of the Rectangle.
    if (nullptr != rect)
    {
        rect->Width = 250;
        rect->Height = 150;
    }
}
```

```cs
// Handler for pointer exited event.
private void touchRectangle_PointerExited(object sender, PointerRoutedEventArgs e)
{
    Rectangle rect = sender as Rectangle;

    // Pointer moved outside Rectangle hit test area.
    // Reset the dimensions of the Rectangle.
    if (null != rect)
    {
        rect.Width = 200;
        rect.Height = 100;
    }
}
// Handler for pointer released event.
private void touchRectangle_PointerReleased(object sender, PointerRoutedEventArgs e)
{
    Rectangle rect = sender as Rectangle;

    // Reset the dimensions of the Rectangle.
    if (null != rect)
    {
        rect.Width = 200;
        rect.Height = 100;
    }
}

// Handler for pointer pressed event.
private void touchRectangle_PointerPressed(object sender, PointerRoutedEventArgs e)
{
    Rectangle rect = sender as Rectangle;

    // Change the dimensions of the Rectangle.
    if (null != rect)
    {
        rect.Width = 250;
        rect.Height = 150;
    }
}
```

```vb
' Handler for pointer exited event.
Private Sub touchRectangle_PointerExited(sender As Object, e As PointerRoutedEventArgs)
    Dim rect As Rectangle = CType(sender, Rectangle)

    ' Pointer moved outside Rectangle hit test area.
    ' Reset the dimensions of the Rectangle.
    If (rect IsNot Nothing) Then
        rect.Width = 200
        rect.Height = 100
    End If
End Sub

' Handler for pointer released event.
Private Sub touchRectangle_PointerReleased(sender As Object, e As PointerRoutedEventArgs)
    Dim rect As Rectangle = CType(sender, Rectangle)

    ' Reset the dimensions of the Rectangle.
    If (rect IsNot Nothing) Then
        rect.Width = 200
        rect.Height = 100
    End If
End Sub

' Handler for pointer pressed event.
Private Sub touchRectangle_PointerPressed(sender As Object, e As PointerRoutedEventArgs)
    Dim rect As Rectangle = CType(sender, Rectangle)

    ' Change the dimensions of the Rectangle.
    If (rect IsNot Nothing) Then
        rect.Width = 250
        rect.Height = 150
    End If
End Sub
```

## Manipulation events


Use manipulation events if you need to support multiple finger interactions in your app, or interactions that require velocity data.

You can use manipulation events to detect interactions such as drag, zoom, and hold.

Here is a list of manipulation events and related event arguments.

| Event or class                                                                                               | Description                                                                                                                               |
|--------------------------------------------------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------|
| [**ManipulationStarting event**](https://msdn.microsoft.com/library/windows/apps/br208951)                                   | Occurs when the manipulation processor is first created.                                                                                  |
| [**ManipulationStarted event**](https://msdn.microsoft.com/library/windows/apps/br208950)                                     | Occurs when an input device begins a manipulation on the [**UIElement**](https://msdn.microsoft.com/library/windows/apps/br208911).                                            |
| [**ManipulationDelta event**](https://msdn.microsoft.com/library/windows/apps/br208946)                                         | Occurs when the input device changes position during a manipulation.                                                                      |
| [**ManipulationInertiaStarting event**](https://msdn.microsoft.com/library/windows/apps/hh702425)                | Occurs when the input device loses contact with the [**UIElement**](https://msdn.microsoft.com/library/windows/apps/br208911) object during a manipulation and inertia begins. |
| [**ManipulationCompleted event**](https://msdn.microsoft.com/library/windows/apps/br208945)                                 | Occurs when a manipulation and inertia on the [**UIElement**](https://msdn.microsoft.com/library/windows/apps/br208911) are complete.                                          |
| [**ManipulationStartingRoutedEventArgs**](https://msdn.microsoft.com/library/windows/apps/hh702132)               | Provides data for the [**ManipulationStarting**](https://msdn.microsoft.com/library/windows/apps/br208951) event.                                         |
| [**ManipulationStartedRoutedEventArgs**](https://msdn.microsoft.com/library/windows/apps/hh702101)                 | Provides data for the [**ManipulationStarted**](https://msdn.microsoft.com/library/windows/apps/br208950) event.                                           |
| [**ManipulationDeltaRoutedEventArgs**](https://msdn.microsoft.com/library/windows/apps/hh702051)                     | Provides data for the [**ManipulationDelta**](https://msdn.microsoft.com/library/windows/apps/br208946) event.                                               |
| [**ManipulationInertiaStartingRoutedEventArgs**](https://msdn.microsoft.com/library/windows/apps/hh702074) | Provides data for the [**ManipulationInertiaStarting**](https://msdn.microsoft.com/library/windows/apps/br208947) event.                           |
| [**ManipulationVelocities**](https://msdn.microsoft.com/library/windows/apps/br242032)                                              | Describes the speed at which manipulations occur.                                                                                         |
| [**ManipulationCompletedRoutedEventArgs**](https://msdn.microsoft.com/library/windows/apps/hh702035)             | Provides data for the [**ManipulationCompleted**](https://msdn.microsoft.com/library/windows/apps/br208945) event.                                       |

 

A gesture consists of a series of manipulation events. Each gesture starts with a [**ManipulationStarted**](https://msdn.microsoft.com/library/windows/apps/br208950) event, such as when a user touches the screen.

Next, one or more [**ManipulationDelta**](https://msdn.microsoft.com/library/windows/apps/br208946) events are fired. For example, if you touch the screen and then drag your finger across the screen. Finally, a [**ManipulationCompleted**](https://msdn.microsoft.com/library/windows/apps/br208945) event is raised when the interaction finishes.

**Note**  If you don't have a touch-screen monitor, you can test your manipulation event code in the simulator using a mouse and mouse wheel interface.

 

The following example shows how to use the [**ManipulationDelta**](https://msdn.microsoft.com/library/windows/apps/br208946) events to handle a slide interaction on a [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle) and move it across the screen.

First, a [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle) named `touchRectangle` is created in XAML with a [**Height**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Height) and [**Width**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Width) of 200.

```XAML
<Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
    <Rectangle Name="touchRectangle"
               Width="200" Height="200" Fill="Blue" 
               ManipulationMode="All"/>
</Grid>
```

Next, a global [**TranslateTransform**](https://msdn.microsoft.com/library/windows/apps/br243027) named `dragTranslation` is created for translating the [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle). A [**ManipulationDelta**](https://msdn.microsoft.com/library/windows/apps/br208946) event listener is specified on the **Rectangle**, and `dragTranslation` is added to the [**RenderTransform**](https://msdn.microsoft.com/library/windows/apps/br208980) of the **Rectangle**.

```cpp
// Global translation transform used for changing the position of 
// the Rectangle based on input data from the touch contact.
Windows::UI::Xaml::Media::TranslateTransform^ dragTranslation;
```

```cs
// Global translation transform used for changing the position of 
// the Rectangle based on input data from the touch contact.
private TranslateTransform dragTranslation;
```

```vb
' Global translation transform used for changing the position of 
' the Rectangle based on input data from the touch contact.
Private dragTranslation As TranslateTransform
```

```cpp
MainPage::MainPage()
{
    InitializeComponent();

    // Listener for the ManipulationDelta event.
    touchRectangle->ManipulationDelta += 
        ref new ManipulationDeltaEventHandler(
            this, 
            &MainPage::touchRectangle_ManipulationDelta);
    // New translation transform populated in 
    // the ManipulationDelta handler.
    dragTranslation = ref new TranslateTransform();
    // Apply the translation to the Rectangle.
    touchRectangle->RenderTransform = dragTranslation;
}
```

```cs
public MainPage()
{
    this.InitializeComponent();

    // Listener for the ManipulationDelta event.
    touchRectangle.ManipulationDelta += touchRectangle_ManipulationDelta;
    // New translation transform populated in 
    // the ManipulationDelta handler.
    dragTranslation = new TranslateTransform();
    // Apply the translation to the Rectangle.
    touchRectangle.RenderTransform = this.dragTranslation;
}
```

```vb
Public Sub New()

    ' This call is required by the designer.
    InitializeComponent()

    ' Listener for the ManipulationDelta event.
    AddHandler touchRectangle.ManipulationDelta,
        AddressOf testRectangle_ManipulationDelta
    ' New translation transform populated in 
    ' the ManipulationDelta handler.
    dragTranslation = New TranslateTransform()
    ' Apply the translation to the Rectangle.
    touchRectangle.RenderTransform = dragTranslation

End Sub
```

Finally, in the [**ManipulationDelta**](https://msdn.microsoft.com/library/windows/apps/br208946) event handler, the position of the [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle) is updated by using the [**TranslateTransform**](https://msdn.microsoft.com/library/windows/apps/br243027) on the [**Delta**](https://msdn.microsoft.com/library/windows/apps/hh702058) property.

```cpp
// Handler for the ManipulationDelta event.
// ManipulationDelta data is loaded into the
// translation transform and applied to the Rectangle.
void MainPage::touchRectangle_ManipulationDelta(Object^ sender,
    ManipulationDeltaRoutedEventArgs^ e)
{
    // Move the rectangle.
    dragTranslation->X += e->Delta.Translation.X;
    dragTranslation->Y += e->Delta.Translation.Y;
    
}
```

```cs
// Handler for the ManipulationDelta event.
// ManipulationDelta data is loaded into the
// translation transform and applied to the Rectangle.
void touchRectangle_ManipulationDelta(object sender,
    ManipulationDeltaRoutedEventArgs e)
{
    // Move the rectangle.
    dragTranslation.X += e.Delta.Translation.X;
    dragTranslation.Y += e.Delta.Translation.Y;
}
```

```vb
' Handler for the ManipulationDelta event.
' ManipulationDelta data Is loaded into the
' translation transform And applied to the Rectangle.
Private Sub testRectangle_ManipulationDelta(
    sender As Object,
    e As ManipulationDeltaRoutedEventArgs)

    ' Move the rectangle.
    dragTranslation.X = (dragTranslation.X + e.Delta.Translation.X)
    dragTranslation.Y = (dragTranslation.Y + e.Delta.Translation.Y)

End Sub
```

## Routed events


All of the pointer events, gesture events and manipulation events mentioned here are implemented as *routed events*. This means that the event can potentially be handled by objects other than the one that originally raised the event. Successive parents in an object tree, such as the parent containers of a [**UIElement**](https://msdn.microsoft.com/library/windows/apps/br208911) or the root [**Page**](https://msdn.microsoft.com/library/windows/apps/br227503) of your app, can choose to handle these events even if the original element does not. Conversely, any object that does handle the event can mark the event handled so that it no longer reaches any parent element. For more info about the routed event concept and how it affects how you write handlers for routed events, see [Events and routed events overview](https://msdn.microsoft.com/library/windows/apps/hh758286).

## Dos and don'ts


-   Design applications with touch interaction as the primary expected input method.
-   Provide visual feedback for interactions of all types (touch, pen, stylus, mouse, etc.)
-   Optimize targeting by adjusting touch target size, contact geometry, scrubbing and rocking.
-   Optimize accuracy through the use of snap points and directional "rails".
-   Provide tooltips and handles to help improve touch accuracy for tightly packed UI items.
-   Don't use timed interactions whenever possible (example of appropriate use: touch and hold).
-   Don't use the number of fingers used to distinguish the manipulation whenever possible.


## Related articles

* [Handle pointer input](handle-pointer-input.md)
* [Identify input devices](identify-input-devices.md)

**Samples**

* [Basic input sample](http://go.microsoft.com/fwlink/p/?LinkID=620302)
* [Low latency input sample](http://go.microsoft.com/fwlink/p/?LinkID=620304)
* [User interaction mode sample](http://go.microsoft.com/fwlink/p/?LinkID=619894)
* [Focus visuals sample](http://go.microsoft.com/fwlink/p/?LinkID=619895)

**Archive Samples**

* [Input: Device capabilities sample](http://go.microsoft.com/fwlink/p/?linkid=231530)
* [Input: XAML user input events sample](http://go.microsoft.com/fwlink/p/?linkid=226855)
* [XAML scrolling, panning, and zooming sample](http://go.microsoft.com/fwlink/p/?linkid=251717)
* [Input: Gestures and manipulations with GestureRecognizer](http://go.microsoft.com/fwlink/p/?LinkID=231605)
 

 




