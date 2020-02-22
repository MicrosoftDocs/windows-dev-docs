---
Description: 
title: Screen readers and hardware button events
label: Screen readers and hardware button events
template: detail.hbs
ms.date: 02/20/2020
ms.topic: article
keywords: windows 10, uwp, accessibility, narrator, screen reader
ms.localizationpriority: medium
---

# Screen readers and hardware system buttons

Screen-readers, such as [Narrator](https://support.microsoft.com/en-us/help/22798/windows-10-complete-guide-to-narrator), must be able to recognize and handle hardware system button events and communicate their state to users. In some cases, the screen reader might need to handle button events exclusively and not let them bubble up to other handlers.

Beginning with Windows 10 version 2004, UWP applications can listen for and handle the **Fn** hardware system button events in the same way as other hardware buttons. Previously, this system button acted only as a modifier for how other hardware buttons reported their events and state.

> [!NOTE]
> Fn button support is OEM-specific and can include the ability to toggle/lock on or off vs. a press-and-hold key combination, and corresponding lock indicator light (which might not helpful to blind or vision-impaired users).

Fn button events are exposed through a new [SystemButtonEventController Class](/uwp/api/windows.ui.input.systembuttoneventcontroller) in the [Windows.UI.Input](/uwp/api/windows.ui.input) namespace. The SystemButtonEventController object supports the following events:

- [SystemFunctionButtonPressed](/uwp/api/windows.ui.input.systembuttoneventcontroller.systemfunctionbuttonpressed)
- [SystemFunctionButtonReleased](/uwp/api/windows.ui.input.systembuttoneventcontroller.systemfunctionbuttonreleased)
- [SystemFunctionLockChanged](/uwp/api/windows.ui.input.systembuttoneventcontroller.systemfunctionlockchanged)
- [SystemFunctionLockIndicatorChanged](/uwp/api/windows.ui.input.systembuttoneventcontroller.systemfunctionlockindicatorchanged)

> [!Important]
> The SystemButtonEventController cannot receive these events if they have already been handled by a higher priority handler.

In the following example, we show how to create a [SystemButtonEventController](/uwp/api/windows.ui.input.systembuttoneventcontroller) based on a DispatcherQueue and handle the four supported events.

Each event handler simply announces the event, while the FunctionLockIndicatorChanged handler also controls whether the app is in "Learning" mode and lets the event bubble to other handlers.

It is common for more than one of these events to fire when the Fn button is pressed. For example, pressing the Fn button on a Surface keyboard fires SystemFunctionButtonPressed, SystemFunctionLockChanged, and SystemFunctionLockIndicatorChanged at the same time.

```cppwinrt
namespace winrt
{
    using namespace Windows::System;
    using namespace Windows::UI::Input;
}

...

// Declare related members
    winrt::DispatcherQueueController _queueController;
    winrt::DispatcherQueue _queue;
    winrt::SystemButtonEventController _controller;
    winrt::event_token _fnKeyDownToken;
    winrt::event_token _fnKeyUpToken;
    winrt::event_token _fnLockToken;
    winrt::event_token _fnLockIndicatorToken;
    bool _isLearningMode = false;

...

void SetupSystemButtonEventController()
{
    // Create dispatcher queue controller and dispatcher queue
    _queueController = winrt::DispatcherQueueController::CreateOnDedicatedThread();
    _queue = _queueController.DispatcherQueue();

    // Create controller based on new created dispatcher queue
    _controller = winrt::SystemButtonEventController::CreateForDispatcherQueue(_queue);

    // Add Event Handler for each different event
    _fnKeyDownToken = _controller->FunctionButtonPressed(
        [](const winrt::SystemButtonEventController& /*sender*/, const winrt:: FunctionButtonEventArgs& args)
        {
            // Mock function to read the sentence "Fn button is pressed"
            PronounceFunctionButtonPressedMock();
            // Set Handled as true means this event is consumed by this controller
            // no more targets will receive this event
            args.Handled(true);
        });

        _fnKeyUpToken = _controller->FunctionButtonReleased(
            [](const winrt::SystemButtonEventController& /*sender*/, const winrt:: FunctionButtonEventArgs& args)
            {
                // Mock function to read the sentence "Fn button is up"
                PronounceFunctionButtonReleasedMock();
                // Set Handled as true means this event is consumed by this controller
                // no more targets will receive this event
                args.Handled(true);
            });

    _fnLockToken = _controller->FunctionLockChanged(
        [](const winrt::SystemButtonEventController& /*sender*/, const winrt:: FunctionLockChangedEventArgs& args)
        {
            // Mock function to read the sentence "Fn shift is locked/unlocked"
            PronounceFunctionLockMock(args.IsLocked());
            // Set Handled as true means this event is consumed by this controller
            // no more targets will receive this event
            args.Handled(true);
        });

    _fnLockIndicatorToken = _controller->FunctionLockIndicatorChanged(
        [](const winrt::SystemButtonEventController& /*sender*/, const winrt:: FunctionLockIndicatorChangedEventArgs& args)
        {
            // Mock function to read the sentence "Fn lock indicator is on/off"
            PronounceFunctionLockIndicatorMock(args.IsIndicatorOn());
            // In learning mode, the user is exploring the keyboard. They expect the program
            // to announce what the key they just pressed WOULD HAVE DONE, without actually
            // doing it. Therefore, handle the event when in learning mode so the key is ignored
            // by the system.
            args.Handled(_isLearningMode);
        });
}
```
