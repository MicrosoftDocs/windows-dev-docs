---
title: Flight stick
description: Use the Windows.Gaming.Input flight stick APIs to read input from flight sticks.
ms.assetid: DC633F6B-FDC9-4D6E-8401-305861F31192
ms.date: 03/06/2017
ms.topic: article
keywords: windows 10, uwp, games, input, flight stick
ms.localizationpriority: medium
---
# Flight stick

This page describes the basics of programming for Xbox One-certified flight sticks using [Windows.Gaming.Input.FlightStick](/uwp/api/windows.gaming.input.flightstick) and related APIs for the Universal Windows Platform (UWP).

By reading this page, you'll learn:

* how to gather a list of connected flight sticks and their users
* how to detect that a flight stick has been added or removed
* how to read input from one or more flight sticks
* how flight sticks behave as UI navigation devices

## Overview

Flight sticks are gaming input devices that are valued for reproducing the feel of flight sticks that would be found in a plane or spaceship's cockpit. They're the perfect input device for quick and accurate control of flight. Flight sticks are supported in Windows 10 or Windows 11 and Xbox One apps through the [Windows.Gaming.Input](/uwp/api/windows.gaming.input) namespace.

Xbox One-certified flight sticks are equipped with the following controls:

* A twistable analog joystick capable of roll, pitch, and yaw
* An analog throttle
* Two fire buttons
* An 8-way digital hat switch
* **View** and **Menu** buttons

> [!NOTE]
> The **View** and **Menu** buttons are used to support UI navigation, not gameplay commands, and therefore can't be readily accessed as joystick buttons.

### UI navigation

In order to ease the burden of supporting the different input devices for user interface navigation and to encourage consistency between games and devices, most _physical_ input devices simultaneously act as separate _logical_ input devices called [UI navigation controllers](ui-navigation-controller.md). The UI navigation controller provides a common vocabulary for UI navigation commands across input devices.

As a UI navigation controller, a flight stick maps the [required set](ui-navigation-controller.md#required-set) of navigation commands to the joystick and **View**, **Menu**, **FirePrimary**, and **FireSecondary** buttons.

| Navigation command | Flight stick input                  |
| ------------------:| ----------------------------------- |
|                 Up | Joystick up                         |
|               Down | Joystick down                       |
|               Left | Joystick left                       |
|              Right | Joystick right                      |
|               View | **View** button                     |
|               Menu | **Menu** button                     |
|             Accept | **FirePrimary** button              |
|             Cancel | **FireSecondary** button            |

Flight sticks don't map any of the [optional set](ui-navigation-controller.md#optional-set) of navigation commands.

## Detect and track flight sticks

Detecting and tracking flight sticks works in exactly the same way as it does for gamepads, except with the [FlightStick](/uwp/api/windows.gaming.input.flightstick) class instead of the [Gamepad](/uwp/api/Windows.Gaming.Input.Gamepad) class. See [Gamepad and vibration](gamepad-and-vibration.md) for more information.

<!-- Flight sticks are managed by the system, therefore you don't have to create or initialize them. The system provides a list of connected flight sticks and events to notify you when a flight stick is added or removed.

### The flight stick list

The [FlightStick](/uwp/api/windows.gaming.input.flightstick) class provides a static property, [FlightSticks](/uwp/api/windows.gaming.input.flightstick#Windows_Gaming_Input_FlightStick_FlightSticks), which is a read-only list of flight sticks that are currently connected. Because you might only be interested in some of the connected flight sticks, we recommend that you maintain your own collection instead of accessing them through the `FlightSticks` property.

The following example copies all connected flight sticks into a new collection:

```cpp
auto myFlightSticks = ref new Vector<FlightStick^>();

for (auto flightStick : FlightStick::FlightSticks)
{
    // This code assumes that you're interested in all flight sticks.
    myFlightSticks->Append(flightStick);
}
```

### Adding and removing flight sticks

When a flight stick is added or removed, the [FlightStickAdded](/uwp/api/windows.gaming.input.flightstick#Windows_Gaming_Input_FlightStick_FlightStickAdded) and [FlightStickRemoved](/uwp/api/windows.gaming.input.flightstick#Windows_Gaming_Input_FlightStick_FlightStickRemoved) events are raised. You can register handlers for these events to keep track of the flight sticks that are currently connected.

The following example starts tracking a flight stick that's been added:

```cpp
FlightStick::FlightStickAdded += 
    ref new EventHandler<FlightStick^>([] (Platform::Object^, FlightStick^ args)
{
    // This code assumes that you're interested in all new flight sticks.
    myFlightSticks->Append(args);
});
```

The following example stops tracking a flight stick that's been removed:

```cpp
FlightStick::FlightStickRemoved += 
    ref new EventHandler<FlightStick^>([] (Platform::Object^, FlightStick^ args)
{
    unsigned int indexRemoved;

    if (myFlightSticks->IndexOf(args, &indexRemoved))
    {
        myFlightSticks->RemoveAt(indexRemoved);
    }
});
```

### Users and headsets

Each flight stick can be associated with a user account to link their identity to their gameplay, and can have a headset attached to facilitate voice chat or in-game features. To learn more about working with users and headsets, see [Tracking users and their devices](input-practices-for-games.md#tracking-users-and-their-devices) and [Headset](headset.md). -->

## Reading the flight stick

After you identify the flight stick that you're interested in, you're ready to gather input from it. However, unlike some other kinds of input that you might be used to, flight sticks don't communicate state-change by raising events. Instead, you take regular readings of their current state by _polling_ them.

### Polling the flight stick

Polling captures a snapshot of the flight stick at a precise point in time. This approach to input gathering is a good fit for most games because their logic typically runs in a deterministic loop rather than being event-driven. It's also typically simpler to interpret game commands from input gathered all at once than it is from many single inputs gathered over time.

You poll a flight stick by calling [FlightStick.GetCurrentReading](/uwp/api/windows.gaming.input.flightstick.GetCurrentReading). This function returns a [FlightStickReading](/uwp/api/windows.gaming.input.flightstickreading) that contains the state of the flight stick.

The following example polls a flight stick for its current state:

```cpp
auto flightStick = myFlightSticks->GetAt(0);
FlightStickReading reading = flightStick->GetCurrentReading();
```

In addition to the flight stick state, each reading includes a timestamp that indicates precisely when the state was retrieved. The timestamp is useful for relating to the timing of previous readings or to the timing of the game simulation.

### Reading the joystick and throttle input

The joystick provides an analog reading between -1.0 and 1.0 in the X, Y, and Z axes (roll, pitch, and yaw, respectively). For roll, a value of -1.0 corresponds to the left-most joystick position, while a value of 1.0 corresponds to the right-most position. For pitch, a value of -1.0 corresponds to the bottom-most joystick position, while a value of 1.0 corresponds to the top-most position. For yaw, a value of -1.0 corresponds to the most counterclockwise, twisted position, while a value of 1.0 corresponds to the most clockwise position.

In all axes, the value is approximately 0.0 when the joystick is in the center position, but it's normal for the precise value to vary, even between subsequent readings. Strategies for mitigating this variation are discussed later in this section.

The value of the joystick's roll is read from the [FlightStickReading.Roll](/uwp/api/windows.gaming.input.flightstickreading.Roll) property, the value of the pitch is read from the [FlightStickReading.Pitch](/uwp/api/windows.gaming.input.flightstickreading.Pitch) property, and the value of the yaw is read from the [FlightStickReading.Yaw](/uwp/api/windows.gaming.input.flightstickreading.Yaw) property:

```cpp
// Each variable will contain a value between -1.0 and 1.0.
float roll = reading.Roll;
float pitch = reading.Pitch;
float yaw = reading.Yaw;
```

When reading the joystick values, you'll notice that they don't reliably produce a neutral reading of 0.0 when the joystick is at rest in the center position; instead, they'll produce different values near 0.0 each time the joystick is moved and returned to the center position. To mitigate these variations, you can implement a small _deadzone_, which is a range of values near the ideal center position that are ignored.

One way to implement a deadzone is to determine how far the joystick has moved from the center, and ignore readings that are nearer than some distance you choose. You can compute the distance roughly&mdash;it's not exact because joystick readings are essentially polar, not planar, values&mdash;just by using the Pythagorean theorem. This produces a radial deadzone.

The following example demonstrates a basic radial deadzone using the Pythagorean theorem:

```cpp
// Choose a deadzone. Readings inside this radius are ignored.
const float deadzoneRadius = 0.1f;
const float deadzoneSquared = deadzoneRadius * deadzoneRadius;

// Pythagorean theorem: For a right triangle, hypotenuse^2 = (opposite side)^2 + (adjacent side)^2
float oppositeSquared = pitch * pitch;
float adjacentSquared = roll * roll;

// Accept and process input if true; otherwise, reject and ignore it.
if ((oppositeSquared + adjacentSquared) < deadzoneSquared)
{
    // Input accepted, process it.
}
```

### Reading the buttons and hat switch

Each of the flight stick's two fire buttons provides a digital reading that indicates whether it's pressed (down) or released (up). For efficiency, button readings aren't represented as individual boolean values&mdash;instead, they're all packed into a single bitfield that's represented by the [FlightStickButtons](/uwp/api/windows.gaming.input.flightstickbuttons) enumeration. In addition, the 8-way hat switch provides a direction packed into a single bitfield that's represented by the [GameControllerSwitchPosition](/uwp/api/windows.gaming.input.gamecontrollerswitchposition) enumeration.

> [!NOTE]
> Flight sticks are equipped with additional buttons used for UI navigation such as the **View** and **Menu** buttons. These buttons are not part of the `FlightStickButtons` enumeration and can only be read by accessing the flight stick as a UI navigation device. For more information, see [UI navigation controller](ui-navigation-controller.md).

The button values are read from the [FlightStickReading.Buttons](/uwp/api/windows.gaming.input.flightstickreading.Buttons) property. Because this property is a bitfield, bitwise masking is used to isolate the value of the button that you're interested in. The button is pressed (down) when the corresponding bit is set; otherwise, it's released (up).

The following example determines whether the **FirePrimary** button is pressed:

```cpp
if (FlightStickButtons::FirePrimary == (reading.Buttons & FlightStickButtons::FirePrimary))
{
    // FirePrimary is pressed.
}
```

The following example determines whether the **FirePrimary** button is released:

```cpp
if (FlightStickButtons::None == (reading.Buttons & FlightStickButtons::FirePrimary))
{
    // FirePrimary is released (not pressed).
}
```

Sometimes you might want to determine when a button transitions from pressed to released or released to pressed, whether multiple buttons are pressed or released, or if a set of buttons are arranged in a particular way&mdash;some pressed, some not. For information on how to detect each of these conditions, see [Detecting button transitions](input-practices-for-games.md#detecting-button-transitions) and [Detecting complex button arrangements](input-practices-for-games.md#detecting-complex-button-arrangements).

The hat switch value is read from the [FlightStickReading.HatSwitch](/uwp/api/windows.gaming.input.flightstickreading.HatSwitch) property. Because this property is also a bitfield, bitwise masking is again used to isolate the position of the hat switch.

The following example determines whether the hat switch is in the up position:

```cpp
if (GameControllerSwitchPosition::Up == (reading.HatSwitch & GameControllerSwitchPosition::Up))
{
    // The hat switch is in the up position.
}
```

The following example determines if the hat switch is in the center position:

```cpp
if (GameControllerSwitchPosition::Center == (reading.HatSwitch & GameControllerSwitchPosition::Center))
{
    // The hat switch is in the center position.
}
```

<!--## Run the InputInterfacingUWP sample

The [InputInterfacingUWP sample _(github)_](https://github.com/Microsoft/Xbox-ATG-Samples/tree/master/Samples/System/InputInterfacingUWP) demonstrates how to use flight sticks and different kinds of input devices in tandem, as well as how these input devices behave as UI navigation controllers.-->

## See also

* [Windows.Gaming.Input.UINavigationController class](/uwp/api/windows.gaming.input.uinavigationcontroller)
* [Windows.Gaming.Input.IGameController interface](/uwp/api/windows.gaming.input.igamecontroller)
* [Input practices for games](input-practices-for-games.md)