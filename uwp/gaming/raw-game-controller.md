---
title: Raw game controller
description: Use the Windows.Gaming.Input raw game controller APIs to read input from nearly any type of game controller.
ms.assetid: 2A466C16-1F51-4D8D-AD13-704B6D3C7BEC
ms.date: 03/08/2017
ms.topic: article
keywords: windows 10, uwp, games, input, raw game controller
ms.localizationpriority: medium
---
# Raw game controller

This page describes the basics of programming for nearly any type of game controller using [Windows.Gaming.Input.RawGameController](/uwp/api/windows.gaming.input.rawgamecontroller) and related APIs for the Universal Windows Platform (UWP).

By reading this page, you'll learn:

* how to gather a list of connected raw game controllers and their users
* how to detect that a raw game controller has been added or removed
* how to get the capabilities of a raw game controller
* how to read input from a raw game controller

## Overview

A raw game controller is a generic representation of a game controller, with inputs found on many different kinds of common game controllers. These inputs are exposed as simple arrays of unnamed buttons, switches, and axes. Using a raw game controller, you can allow customers to create custom input mappings no matter what type of controller they're using.

The [RawGameController](/uwp/api/windows.gaming.input.rawgamecontroller) class is really meant for scenarios when the other input classes ([ArcadeStick](/uwp/api/windows.gaming.input.arcadestick), [FlightStick](/uwp/api/windows.gaming.input.flightstick), and so on) don't meet your needs&mdash;if you want something more generic, anticipating that customers will use many different types of game controllers, then this class is for you.

## Detect and track raw game controllers

Detecting and tracking raw game controllers works in exactly the same way as it does for gamepads, except with the [RawGameController](/uwp/api/windows.gaming.input.rawgamecontroller) class instead of the [Gamepad](/uwp/api/Windows.Gaming.Input.Gamepad) class. See [Gamepad and vibration](gamepad-and-vibration.md) for more information.

<!-- Raw game controllers are managed by the system, therefore you don't have to create or initialize them. The system provides a list of connected raw game controllers and events to notify you when a raw game controller is added or removed.

### The raw game controller list

The [RawGameController](/uwp/api/windows.gaming.input.rawgamecontroller) class provides a static property, [RawGameControllers](/uwp/api/windows.gaming.input.rawgamecontroller#Windows_Gaming_Input_RawGameController_RawGameControllers), which is a read-only list of raw game controllers that are currently connected. Because you might only be interested in some of the connected raw game controllers, we recommend that you maintain your own collection instead of accessing them through the **RawGameControllers** property.

The following example copies all connected raw game controllers into a new collection:

```cpp
auto myRawGameControllers = ref new Vector<RawGameController^>();

for (auto rawGameController : RawGameController::RawGameControllers)
{
    // This code assumes that you're interested in all raw game controllers.
    myRawGameControllers->Append(rawGameController);
}
```

### Adding and removing raw game controllers

When a raw game controller is added or removed, the [RawGameControllerAdded](/uwp/api/windows.gaming.input.rawgamecontroller#Windows_Gaming_Input_RawGameController_RawGameControllerAdded) and [RawGameControllerRemoved](/uwp/api/windows.gaming.input.rawgamecontroller#Windows_Gaming_Input_RawGameController_RawGameControllerRemoved) events are raised. You can register handlers for these events to keep track of the raw game controllers that are currently connected.

The following example starts tracking a raw game controller that's been added:

```cpp
RawGameController::RawGameControllerAdded += 
    ref new EventHandler<RawGameController^>(
        [] (Platform::Object^, RawGameController^ args)
{
    // This code assumes that you're interested in all new raw game controllers.
    myRawGameControllers->Append(args);
});
```

The following example stops tracking a raw game controller that's been removed:

```cpp
RawGameController::RawGameControllerRemoved +=
    ref new EventHandler<RawGameController^>(
        [] (Platform::Object^, RawGameController^ args)
{
    unsigned int indexRemoved;

    if (myRawGameControllers->IndexOf(args, &indexRemoved))
    {
        myRawGameControllers->RemoveAt(indexRemoved);
    }
});
```

### Users and headsets

Each raw game controller can be associated with a user account to link their identity to their gameplay, and can have a headset attached to facilitate voice chat or in-game features. To learn more about working with users and headsets, see [Tracking users and their devices](input-practices-for-games.md#tracking-users-and-their-devices) and [Headset](headset.md). -->

## Get the capabilities of a raw game controller

After you identify the raw game controller that you're interested in, you can gather information on the capabilities of the controller. You can get the number of buttons on the raw game controller with [RawGameController.ButtonCount](/uwp/api/windows.gaming.input.rawgamecontroller.ButtonCount), the number of analog axes with [RawGameController.AxisCount](/uwp/api/windows.gaming.input.rawgamecontroller.AxisCount), and the number of switches with [RawGameController.SwitchCount](/uwp/api/windows.gaming.input.rawgamecontroller.SwitchCount). Additionally, you can get the type of a switch using [RawGameController.GetSwitchKind](/uwp/api/windows.gaming.input.rawgamecontroller#Windows_Gaming_Input_RawGameController_GetSwitchKind_System_Int32_).

The following example gets the input counts of a raw game controller:

```cpp
auto rawGameController = myRawGameControllers->GetAt(0);
int buttonCount = rawGameController->ButtonCount;
int axisCount = rawGameController->AxisCount;
int switchCount = rawGameController->SwitchCount;
```

The following example determines the type of each switch:

```cpp
for (uint32_t i = 0; i < switchCount; i++)
{
    GameControllerSwitchKind mySwitch = rawGameController->GetSwitchKind(i);
}
```

## Reading the raw game controller

After you know the number of inputs on a raw game controller, you're ready to gather input from it. However, unlike some other kinds of input that you might be used to, a raw game controller doesn't communicate state-change by raising events. Instead, you take regular readings of its current state by _polling_ it.

### Polling the raw game controller

Polling captures a snapshot of the raw game controller at a precise point in time. This approach to input gathering is a good fit for most games because their logic typically runs in a deterministic loop rather than being event-driven. It's also typically simpler to interpret game commands from input gathered all at once than it is from many single inputs gathered over time.

You poll a raw game controller by calling [RawGameController.GetCurrentReading](/uwp/api/windows.gaming.input.rawgamecontroller#Windows_Gaming_Input_RawGameController_GetCurrentReading_System_Boolean___Windows_Gaming_Input_GameControllerSwitchPosition___System_Double___). This function populates arrays for buttons, switches, and axes that contain the state of the raw game controller.

The following example polls a raw game controller for its current state:

```cpp
Platform::Array<bool>^ currentButtonReading =
    ref new Platform::Array<bool>(buttonCount);

Platform::Array<GameControllerSwitchPosition>^ currentSwitchReading =
    ref new Platform::Array<GameControllerSwitchPosition>(switchCount);

Platform::Array<double>^ currentAxisReading = ref new Platform::Array<double>(axisCount);

rawGameController->GetCurrentReading(
    currentButtonReading,
    currentSwitchReading,
    currentAxisReading);
```

There is no guarantee of which position in each array will hold which input value among different types of controllers, so you'll need to check which input is which using the methods [RawGameController.GetButtonLabel](/uwp/api/windows.gaming.input.rawgamecontroller#Windows_Gaming_Input_RawGameController_GetButtonLabel_System_Int32_) and [RawGameController.GetSwitchKind](/uwp/api/windows.gaming.input.rawgamecontroller#Windows_Gaming_Input_RawGameController_GetSwitchKind_System_Int32_).

**GetButtonLabel** will tell you the text or symbol that's printed on the physical button, rather than the button's function&mdash;therefore, it's best used as an aid for UI for cases where you want to give the player hints about which buttons perform which functions. **GetSwitchKind** will tell you the type of switch (that is, how many positions it has), but not the name.

There is no standardized way to get the label of an axis or switch, so you'll need to test these yourself to determine which input is which.

If you have a specific controller that you want to support, you can get the [RawGameController.HardwareProductId](/uwp/api/windows.gaming.input.rawgamecontroller.HardwareProductId) and [RawGameController.HardwareVendorId](/uwp/api/windows.gaming.input.rawgamecontroller.HardwareVendorId) and check if they match that controller. The position of each input in each array is the same for every controller with the same **HardwareProductId** and **HardwareVendorId**, so you don't have to worry about your logic potentially being inconsistent among different controllers of the same type.

In addition to the raw game controller state, each reading returns a timestamp that indicates precisely when the state was retrieved. The timestamp is useful for relating to the timing of previous readings or to the timing of the game simulation.

### Reading the buttons and switches

Each of the raw game controller's buttons provides a digital reading that indicates whether it's pressed (down) or released (up). Button readings are represented as individual Boolean values in a single array. The label for each button can be found using [RawGameController.GetButtonLabel](/uwp/api/windows.gaming.input.rawgamecontroller#Windows_Gaming_Input_RawGameController_GetButtonLabel_System_Int32_) with the index of the Boolean value in the array. Each value is represented as a [GameControllerButtonLabel](/uwp/api/windows.gaming.input.gamecontrollerbuttonlabel).

The following example determines whether the **XboxA** button is pressed:

```cpp
for (uint32_t i = 0; i < buttonCount; i++)
{
    if (currentButtonReading[i])
    {
        GameControllerButtonLabel buttonLabel = rawGameController->GetButtonLabel(i);

        if (buttonLabel == GameControllerButtonLabel::XboxA)
        {
            // XboxA is pressed.
        }
    }
}
```

Sometimes you might want to determine when a button transitions from pressed to released or released to pressed, whether multiple buttons are pressed or released, or if a set of buttons are arranged in a particular way&mdash;some pressed, some not. For information on how to detect each of these conditions, see [Detecting button transitions](input-practices-for-games.md#detecting-button-transitions) and [Detecting complex button arrangements](input-practices-for-games.md#detecting-complex-button-arrangements).

Switch values are provided as an array of [GameControllerSwitchPosition](/uwp/api/windows.gaming.input.gamecontrollerswitchposition). Because this property is a bitfield, bitwise masking is used to isolate the direction of the switch.

The following example determines whether each switch is in the up position:

```cpp
for (uint32_t i = 0; i < switchCount; i++)
{
    if (GameControllerSwitchPosition::Up ==
        (currentSwitchReading[i] & GameControllerSwitchPosition::Up))
    {
        // The switch is in the up position.
    }
}
```

### Reading the analog inputs (sticks, triggers, throttles, and so on)

An analog axis provides a reading between 0.0 and 1.0. This includes each dimension on a stick such as X and Y for standard sticks or even X, Y, and Z axes (roll, pitch, and yaw, respectively) for flight sticks.

The values can represent analog triggers, throttles, or any other type of analog input. These values are not provided with labels, so we suggest that your code is tested with a variety of input devices to ensure that they match correctly with your assumptions.

In all axes, the value is approximately 0.5 for a stick when it is in the center position, but it's normal for the precise value to vary, even between subsequent readings; strategies for mitigating this variation are discussed later in this section.

The following example shows how to read the analog values from an Xbox controller:

```cpp
// Xbox controllers have 6 axes: 2 for each stick and one for each trigger.
float leftStickX = currentAxisReading[0];
float leftStickY = currentAxisReading[1];
float rightStickX = currentAxisReading[2];
float rightStickY = currentAxisReading[3];
float leftTrigger = currentAxisReading[4];
float rightTrigger = currentAxisReading[5];
```

When reading the stick values, you'll notice that they don't reliably produce a neutral reading of 0.5 when at rest in the center position; instead, they'll produce different values near 0.5 each time the stick is moved and returned to the center position. To mitigate these variations, you can implement a small _deadzone_, which is a range of values near the ideal center position that are ignored.

One way to implement a deadzone is to determine how far the stick has moved from the center, and ignore readings that are nearer than some distance you choose. You can compute the distance roughly&mdash;it's not exact because stick readings are essentially polar, not planar, values&mdash;just by using the Pythagorean theorem. This produces a radial deadzone.

The following example demonstrates a basic radial deadzone using the Pythagorean theorem:

```cpp
// Choose a deadzone. Readings inside this radius are ignored.
const float deadzoneRadius = 0.1f;
const float deadzoneSquared = deadzoneRadius * deadzoneRadius;

// Pythagorean theorem: For a right triangle, hypotenuse^2 = (opposite side)^2 + (adjacent side)^2
float oppositeSquared = leftStickY * leftStickY;
float adjacentSquared = leftStickX * leftStickX;

// Accept and process input if true; otherwise, reject and ignore it.
if ((oppositeSquared + adjacentSquared) < deadzoneSquared)
{
    // Input accepted, process it.
}
```

<!--## Run the RawGameControllerUWP sample

The [RawGameControllerUWP sample (GitHub)](TODO: Link) demonstrates how to use raw game controllers. TODO: More information-->

## See also

* [Input for games](input-for-games.md)
* [Input practices for games](input-practices-for-games.md)
* [Windows.Gaming.Input namespace](/uwp/api/windows.gaming.input)
* [Windows.Gaming.Input.RawGameController class](/uwp/api/windows.gaming.input.rawgamecontroller)
* [Windows.Gaming.Input.IGameController interface](/uwp/api/windows.gaming.input.igamecontroller)
* [Windows.Gaming.Input.IGameControllerBatteryInfo interface](/uwp/api/windows.gaming.input.igamecontrollerbatteryinfo)