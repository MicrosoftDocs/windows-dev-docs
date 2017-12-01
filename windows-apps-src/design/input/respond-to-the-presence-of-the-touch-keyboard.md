---
author: Karl-Bridge-Microsoft
Description: Learn how to tailor the UI of your app when showing or hiding the touch keyboard.
title: Respond to the presence of the touch keyboard
ms.assetid: 70C6130E-23A2-4F9D-88E7-7060062DA988
label: Respond to the presence of the touch keyboard
template: detail.hbs
keywords: keyboard, accessibility, navigation, focus, text, input, user interactions
ms.author: kbridge
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
---

# Respond to the presence of the touch keyboard

Learn how to tailor the UI of your app when showing or hiding the touch keyboard.

<div class="important-apis" >
<b>Important APIs</b><br/>
<ul>
<li>[**AutomationPeer**](https://msdn.microsoft.com/library/windows/apps/br209185)</li>
<li>[**InputPane**](https://msdn.microsoft.com/library/windows/apps/br242255)</li>
</ul>
</div> 

![the touch keyboard in default layout mode](images/keyboard/default.png)

<sup>The touch keyboard in default layout mode</sup>

The touch keyboard enables text entry for devices that support touch. Universal Windows Platform (UWP) text input controls invoke the touch keyboard by default when a user taps on an editable input field. The touch keyboard typically remains visible while the user navigates between controls in a form, but this behavior can vary based on the other control types within the form.

To support corresponding touch keyboard behavior in a custom text input control that does not derive from a standard text input control, you must use the [**AutomationPeer**](https://msdn.microsoft.com/library/windows/apps/br209185) class to expose your controls to Microsoft UI Automation and implement the correct UI Automation control patterns. See [Keyboard accessibility](https://msdn.microsoft.com/library/windows/apps/mt244347) and [Custom automation peers](https://msdn.microsoft.com/library/windows/apps/mt297667).

Once this support has been added to your custom control, you can respond appropriately to the presence of the touch keyboard.

**Prerequisites:  **

This topic builds on [Keyboard interactions](keyboard-interactions.md).

You should have a basic understanding of standard keyboard interactions, handling keyboard input and events, and UI Automation.

If you're new to developing Universal Windows Platform (UWP) apps, have a look through these topics to get familiar with the technologies discussed here.

-   [Create your first app](https://msdn.microsoft.com/library/windows/apps/bg124288)
-   Learn about events with [Events and routed events overview](https://msdn.microsoft.com/library/windows/apps/mt185584)

**User experience guidelines:  **

For helpful tips about designing a useful and engaging app optimized for keyboard input, see [Keyboard design guidelines](https://msdn.microsoft.com/library/windows/apps/hh972345) .

## Touch keyboard and a custom UI


Here are a few basic recommendations for custom text input controls.

-   Display the touch keyboard throughout the entire interaction with your form.

-   Ensure that your custom controls have the appropriate UI Automation [**AutomationControlType**](https://msdn.microsoft.com/library/windows/apps/br209182) for the keyboard to persist when focus moves from a text input field while in the context of text entry. For example, if you have a menu that's opened in the middle of a text-entry scenario, and you want the keyboard to persist, the menu must have the **AutomationControlType** of Menu.

-   Don't manipulate UI Automation properties to control the touch keyboard. Other accessibility tools rely on the accuracy of UI Automation properties.

-   Ensure that users can always see the input field that they're interacting with.

    Because the touch keyboard occludes a large portion of the screen, the UWP ensures that the input field with focus scrolls into view as a user navigates through the controls on the form, including controls that are not currently in view.

    When customizing your UI, provide similar behavior on the appearance of the touch keyboard by handling the [**Showing**](https://msdn.microsoft.com/library/windows/apps/br242262) and [**Hiding**](https://msdn.microsoft.com/library/windows/apps/br242260) events exposed by the [**InputPane**](https://msdn.microsoft.com/library/windows/apps/br242255) object.

    ![a form with and without the touch keyboard showing](images/touch-keyboard-pan1.png)

    In some cases, there are UI elements that should stay on the screen the entire time. Design the UI so that the form controls are contained in a panning region and the important UI elements are static. For example:

    ![a form that contains areas that should always stay in view](images/touch-keyboard-pan2.png)

## Handling the Showing and Hiding events


Here's an example of attaching event handlers for the [**showing**](https://msdn.microsoft.com/library/windows/apps/br242262) and [**hiding**](https://msdn.microsoft.com/library/windows/apps/br242260) events of the touch keyboard.

```CSharp
public class MyApplication
{
    public MyApplication()
    {
        // Grab the input pane for the main application window and attach
        // touch keyboard event handlers.
        Windows.UI.ViewManagement.GetForCurrentView().Showing  
            += new EventHandler(_OnInputPaneShowing);
        Windows.UI.ViewManagement.GetForCurrentView().Hiding 
            += new EventHandler(_OnInputPaneHiding);
    }

    private void _OnInputPaneShowing(object sender, InputPaneVisibilityEventArgs eventArgs)
    {
        // If the size of this window is going to be too small, the app uses 
        // the Showing event to begin some element removal animations.
        if (eventArgs.OccludedRect.Top < 400)
        {
            _StartElementRemovalAnimations();

            // Don't use framework scroll- or visibility-related 
            // animations that might conflict with the app's logic.
            eventArgs.EnsuredFocusedElementInView = true; 
        }
    }

    private void _OnInputPaneHiding(object sender, InputPaneVisibilityEventArgs eventArgs)
    {
        if (_ResetToDefaultElements())
        {
            eventArgs.EnsuredFocusedElementInView = true; 
        }
    }

    private void _StartElementRemovalAnimations()
    {
        // This function starts the process of removing elements 
        // and starting the animation.
    }

    private void _ResetToDefaultElements()
    {
        // This function resets the window's elements to their default state.
    }
}
```

## Related articles

* [Keyboard interactions](keyboard-interactions.md)
* [Keyboard accessibility](https://msdn.microsoft.com/library/windows/apps/mt244347)
* [Custom automation peers](https://msdn.microsoft.com/library/windows/apps/mt297667)


**Archive samples**
* [Input: Touch keyboard sample](http://go.microsoft.com/fwlink/p/?linkid=246019)
* [Responding to the appearance of the on-screen keyboard sample](http://go.microsoft.com/fwlink/p/?linkid=231633)
* [XAML text editing sample](http://go.microsoft.com/fwlink/p/?LinkID=251417)
* [XAML accessibility sample](http://go.microsoft.com/fwlink/p/?linkid=238570)

