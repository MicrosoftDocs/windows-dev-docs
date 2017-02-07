---
author: Karl-Bridge-Microsoft
Description: Create Universal Windows Platform (UWP) apps with intuitive and distinctive user interaction experiences that are optimized for touch but functionally consistent across input devices.
title: Touch design guidelines
ms.assetid: 3250F729-4FDD-4AD4-B856-B8BA575C3375
label: Touch design guidelines
template: detail.hbs
redirect_url: https://msdn.microsoft.com/windows/uwp/input-and-devices/touch-interactions
---

# Touch design guidelines





Create Universal Windows Platform (UWP) apps with intuitive and distinctive user interaction experiences that are optimized for touch but functionally consistent across input devices.

## <span id="Dos_and_don_ts"></span><span id="dos_and_don_ts"></span><span id="DOS_AND_DON_TS"></span>Dos and don'ts


-   Design applications with touch interaction as the primary expected input method.
-   Provide visual feedback for interactions of all types (touch, pen, stylus, mouse, etc.)
-   Optimize targeting by adjusting touch target size, contact geometry, scrubbing and rocking.
-   Optimize accuracy through the use of snap points and directional "rails".
-   Provide tooltips and handles to help improve touch accuracy for tightly packed UI items.
-   Don't use timed interactions whenever possible (example of appropriate use: touch and hold).
-   Don't use the number of fingers used to distinguish the manipulation whenever possible.

## <span id="Additional_usage_guidance"></span><span id="additional_usage_guidance"></span><span id="ADDITIONAL_USAGE_GUIDANCE"></span>Additional usage guidance


First and foremost, design your app with the expectation that touch will be the primary input method of your users. If you use the platform controls, support for touchpad, mouse, and pen/stylus requires no additional programming, because Windows 8 provides this for free.

However, keep in mind that a UI optimized for touch is not always superior to a traditional UI. Both provide advantages and disadvantages that are unique to a technology and application. In the move to a touch-first UI, it is important to understand the core differences between touch (including touchpad), pen/stylus, mouse, and keyboard input. Do not take familiar input device properties and behaviors for granted, as touch in Windows 8 does more than simply emulate that functionality.

You will find throughout these guidelines that touch input requires a different approach to UI design.

**Compare touch interaction requirements**

The following table shows some of the differences between input devices that you should consider when you design touch-optimized Windows Store apps.

Factor
Touch interactions
Mouse, keyboard, pen/stylus interactions
Touchpad
Precision
The contact area of a fingertip is greater than a single x-y coordinate, which increases the chances of unintended command activations.
The mouse and pen/stylus supply a precise x-y coordinate.
Same as mouse.
The shape of the contact area changes throughout the movement.
Mouse movements and pen/stylus strokes supply precise x-y coordinates. Keyboard focus is explicit.
Same as mouse.
There is no mouse cursor to assist with targeting.
The mouse cursor, pen/stylus cursor, and keyboard focus all assist with targeting.
Same as mouse.
Human anatomy
Fingertip movements are imprecise, because a straight-line motion with one or more fingers is difficult. This is due to the curvature of hand joints and the number of joints involved in the motion.
It's easier to perform a straight-line motion with the mouse or pen/stylus because the hand that controls them travels a shorter physical distance than the cursor on the screen.
Same as mouse.
Some areas on the touch surface of a display device can be difficult to reach due to finger posture and the user's grip on the device.
The mouse and pen/stylus can reach any part of the screen while any control should be accessible by the keyboard through tab order.
Finger posture and grip can be an issue.
Objects might be obscured by one or more fingertips or the user's hand. This is known as occlusion.
Indirect input devices do not cause occlusion.
Same as mouse.
Object state
Touch uses a two-state model: the touch surface of a display device is either touched (on) or not (off). There is no hover state that can trigger additional visual feedback.
A mouse, pen/stylus, and keyboard all expose a three-state model: up (off), down (on), and hover (focus).

Hover lets users explore and learn through tooltips associated with UI elements. Hover and focus effects can relay which objects are interactive and also help with targeting.

Same as mouse.
Rich interaction
Supports multi-touch: multiple input points (fingertips) on a touch surface.
Supports a single input point.
Same as touch.
Supports direct manipulation of objects through gestures such as tapping, dragging, sliding, pinching, and rotating.
No support for direct manipulation as mouse, pen/stylus, and keyboard are indirect input devices.
Same as mouse.
 

**Note**  
Indirect input has had the benefit of more than 25 years of refinement. Features such as hover-triggered tooltips have been designed to solve UI exploration specifically for touchpad, mouse, pen/stylus, and keyboard input. UI features like this have been re-designed for the rich experience provided by touch input, without compromising the user experience for these other devices.

 

**Use touch feedback**

Appropriate visual feedback during interactions with your app helps users recognize, learn, and adapt to how their interactions are interpreted by both the app and Windows 8. Visual feedback can indicate successful interactions, relay system status, improve the sense of control, reduce errors, help users understand the system and input device, and encourage interaction.

Visual feedback is critical when the user relies on touch input for activities that require accuracy and precision based on location. Display feedback whenever and wherever touch input is detected, to help the user understand any custom targeting rules that are defined by your app and its controls.

**Create an immersive interaction experience**

The following techniques enhance the immersive experience of Windows Store apps.

**Targeting**

Targeting is optimized through:

-   Touch target sizes

    Clear size guidelines ensure that applications provide a comfortable UI that contains objects and controls that are easy and safe to target.

-   Contact geometry

    The entire contact area of the finger determines the most likely target object.

-   Scrubbing

    Items within a group are easily re-targeted by dragging the finger between them (for example, radio buttons). The current item is activated when the touch is released.

-   Rocking

    Densely packed items (for example, hyperlinks) are easily re-targeted by pressing the finger down and, without sliding, rocking it back and forth over the items. Due to occlusion, the current item is identified through a tooltip or the status bar and is activated when the touch is released.

**Accuracy**

Design for sloppy interactions by using:

-   Snap-points that can make it easier to stop at desired locations when users interact with content.
-   Directional "rails" that can assist with vertical or horizontal panning, even when the hand moves in a slight arc. For more information, see [Guidelines for panning](guidelines-for-panning.md).

**Occlusion**

Finger and hand occlusion is avoided through:

-   Size and positioning of UI

    Make UI elements big enough so that they cannot be completely covered by a fingertip contact area.

    Position menus and pop-ups above the contact area whenever possible.

-   Tooltips

    Show tooltips when a user maintains finger contact on an object. This is useful for describing object functionality. The user can drag the fingertip off the object to avoid invoking the tooltip.

    For small objects, offset tooltips so they are not covered by the fingertip contact area. This is helpful for targeting.

-   Handles for precision

    Where precision is required (for example, text selection), provide selection handles that are offset to improve accuracy. For more information, see [Guidelines for selecting text and images (Windows Runtime apps)](guidelines-for-textselection.md).

**Timing**

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

## <span id="related_topics"></span>Related articles

**For developers (XAML)**
* [Touch interactions](https://msdn.microsoft.com/library/windows/apps/mt185617)
* [Custom user interactions](https://msdn.microsoft.com/library/windows/apps/mt185599)
 

 




