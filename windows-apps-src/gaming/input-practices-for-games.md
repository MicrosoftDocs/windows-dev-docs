---
author: mithom
title: Input practices for games
description: Learn patterns and techniques for using input devices effectively.
ms.assetid: CBAD3345-3333-4924-B6D8-705279F52676
ms.author: wdg-dev-content
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp, games, input
---

# Input practices for games

This page describes patterns and techniques for effectively using input devices in Universal Windows Platform (UWP) games.

By reading this page, you'll learn:
* how to track players and which input and navigation devices they're currently using
* how to detect button transitions (pressed-to-released, released-to-pressed)
* how to detect complex button arrangements with a single test

## Tracking users and their devices

All input devices are associated with a [User][Windows.System.User] so that their identity can be linked to their gameplay, achievements, settings changes, and other activities. Users can sign in or sign out at will and its common for a different user to sign in on input device that remains connected to the system after the previous user has signed out. When a user signs in or out the [IGameController.UserChanged][] event is raised. You can register an event handler for this event to keep track of players and the devices they're using.

User identity is also the way that an input device is associated with its corresponding UI navigation controller.

For these reasons, player input should be tracked and correlated by using the [User][igamecontroller.user] property of the device class (inherited from the [IGameController][] interface).

The [UserGamepadPairingUWP _(github)_](
https://github.com/Microsoft/Xbox-ATG-Samples/tree/master/Samples/System/UserGamepadPairingUWP) sample demonstrates how you can keep track of users and the devices they're using.

## Detecting button transitions

Sometimes you want to know when a button is first pressed or released; that is, precisely when the button state transitions from released to pressed or from pressed to released. To determine this, you need to remember the previous device reading and compare the current reading against it to see what's changed.

The following example demonstrates a basic approach for remembering the previous reading; gamepads are shown here, but the principles are the same for arcade stick, racing wheel, and UI navigation buttons

```cpp
GamepadReading newReading();
GamepadReading oldReading();

// Game::Loop represents one iteration of a typical game loop
void Game::Loop()
{
    // move previous newReading into oldReading before getting next newReading
    oldReading = newReading, newReading = gamepad.CurrentReading();

    // process device readings using buttonJustPressed/buttonJustReleased
}
```

Before doing anything else, `Game::Loop` moves the existing value of `newReading` (the gamepad reading from the previous loop iteration) into `oldReading`, then fills `newReading` with a fresh gamepad reading for the current iteration. This gives you the information you need to detect button transitions.

The following example demonstrates a basic approach for detecting button transitions.

```cpp
bool buttonJustPressed(const gamepadButtons selection)
{
	bool newSelectionPressed = (selection == (newReading.Buttons & selection));
    bool oldSelectionPressed = (selection == (oldReading.Buttons & selection));

	return newSelectionPressed && !oldSelectionPressed;
}

bool buttonJustReleased(gamepadButtons selection)
{
	bool newSelectionReleased = (gamepadButtons.None == (newReading.Buttons & selection));
    bool oldSelectionReleased = (gamepadButtons.None == (oldReading.Buttons & selection));

	return newSelectionReleased && !oldSelectionReleased;
}
```

These two functions first derive the boolean state of the button selection from `newReading` and `oldReading`, then perform boolean logic to determine whether the target transition has occurred. These functions return _true_ only if the new reading contains the target state (pressed or released, respectively) *and* the old reading does not also contain the target state; otherwise, they return _false_.


## Detecting complex button arrangements

Each button of an input device provides a digital reading that indicates whether its presseded (down), or released (up). For efficiency, button readings aren't represented as individual boolean values; instead, they're all packed into bitfields represented by device-specific enumerations such as [GamepadButtons][]. To read specific buttons, bitwise masking is used to isolate the values that you're interested in. Buttons are pressed (down) when their corresponding bit is set; otherwise its released (up).

Recall how single buttons are determined to be pressed or released; gamepads are shown here but the principles are the same for arcade stick, racing wheel, and UI navigation buttons.

```cpp
// determines whether gamepad button A is pressed
if (GamepadButtons::A == (reading.Buttons & GamepadButtons::A))
{
    // button A is pressed
}

// determines whether gamepad button A is released
if (GamepadButtons::None == (reading.Buttons & GamepadButtons::A))
{
    // button A is released (not pressed)
}
```

As you can see, determining the state of a single button is straight-forward, but sometimes you might want to determine whether multiple buttons are pressed or released, or if a set of buttons are arranged in a particular way--some pressed, some not. Testing multiple buttons is more complex than testing single buttons--especially with the potential of mixed button state--but there's a simple formula for these tests that applies to single and multiple button tests alike.

The following example determines whether gamepad buttons A and B are both pressed.

```cpp
if ((GamepadButtons::A | GamepadButtons::B) == (reading.Buttons & (GamepadButtons::A | GamepadButtons::B))
{
    // buttons A and B are pressed
}
```

The following example determines whether gamepad buttons A and B are both released.

```cpp
if ((GamepadButtons::None == (reading.Buttons & GamepadButtons::A | GamepadButtons::B))
{
    // buttons A and B are released (not pressed)
}
```

The following example determines whether gamepad button A is pressed while button B is released.

```cpp
if (GamepadButtons::A == (reading.Buttons & (GamepadButtons::A | GamepadButtons::B))
{
    // button A is pressed and button B is released (button B is not pressed)
}
```

The formula that all five of these examples have in common is that the arrangement of buttons to be tested for is specified by the expression on the left-hand side of the equality operator while the buttons to be considered are selected by the masking expression on the right-hand side.

The following example demonstrates this formula more clearly by rewriting the previous example.

```cpp
auto buttonArrangement = GamepadButtons::A;
auto buttonSelection = (reading.Buttons & (GamepadButtons::A | GamepadButtons::B));

if (buttonArrangement == buttonSelection)
{
    // button A is pressed and button B is released (button B is not pressed)
}
```

This formula can be applied to test any number of buttons in any arrangement of their states.



[Windows.System.User]: https://msdn.microsoft.com/library/windows/apps/windows.system.user.aspx
[igamecontroller]: https://msdn.microsoft.com/library/windows/apps/windows.gaming.input.igamecontroller.aspx
[igamecontroller.user]: https://msdn.microsoft.com/library/windows/apps/windows.gaming.input.igamecontroller.user.aspx
[igamecontroller.userchanged]: https://msdn.microsoft.com/library/windows/apps/windows.gaming.input.igamecontroller.userchanged.aspx
[gamepadbuttons]: https://msdn.microsoft.com/library/windows/apps/windows.gaming.input.gamepadbuttons.aspx
