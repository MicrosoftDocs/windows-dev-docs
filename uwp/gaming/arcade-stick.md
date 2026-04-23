---
title: Arcade stick
description: Use the Windows.Gaming.Input arcade stick APIs to detect and read arcade sticks.
ms.assetid: 2E52232F-3014-4C8C-B39D-FAC478BA3E01
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, arcade stick, input
ms.localizationpriority: medium
---
# Arcade stick

This page describes the basics of programming for arcade sticks using [Windows.Gaming.Input.ArcadeStick][arcadestick] and related APIs for the Universal Windows Platform (UWP).

By reading this page, you'll learn:

* how to gather a list of connected arcade sticks and their users
* how to detect that an arcade stick has been added or removed
* how to read input from one or more arcade sticks
* how arcade sticks behave as UI navigation devices

## Arcade stick overview

Arcade sticks are input devices valued for reproducing the feel of stand-up arcade machines and for their high-precision digital controls. Arcade sticks are the perfect input device for head-to-head-fighting and other arcade-style games, and are suitable for any game that works well with all-digital controls. Arcade sticks are supported in UWP apps for Windows 10 or Windows 11 and Xbox One apps through the [Windows.Gaming.Input][] namespace.

Arcade sticks are equipped with an 8-way digital joystick, six **Action** buttons (represented as A1-A6 in the image below), and two **Special** buttons (represented as S1 and S2); they're all-digital input devices that don't support analog controls or vibration. Arcade sticks are also equipped with **View** and **Menu** buttons used to support UI navigation but they're not intended to support gameplay commands and can't be readily accessed as joystick buttons.

![Arcade stick with 4-directional joystick, 6 action buttons (A1-A6), and 2 special buttons (S1 and S2)](images/arcade-stick-1.png)

### UI navigation

In order to ease the burden of supporting many different input devices for user interface navigation and to encourage consistency between games and devices, most _physical_ input devices simultaneously act as a separate _logical_ input device called a [UI navigation controller](ui-navigation-controller.md). The UI navigation controller provides a common vocabulary for UI navigation commands across input devices.

As a UI navigation controller, arcade sticks map the [required set](ui-navigation-controller.md#required-set) of navigation commands to the joystick and **View**, **Menu**, **Action 1**, and **Action 2** buttons.

| Navigation command | Arcade stick input  |
| ------------------:| ------------------- |
|                 Up | Stick up            |
|               Down | Stick down          |
|               Left | Stick left          |
|              Right | Stick right         |
|               View | View button         |
|               Menu | Menu button         |
|             Accept | Action 1 button     |
|             Cancel | Action 2 button     |

Arcade sticks don't map any of the [optional set](ui-navigation-controller.md#optional-set) of navigation commands.

## Detect and track arcade sticks

Detecting and tracking arcade sticks works in exactly the same way as it does for gamepads, except with the [ArcadeStick][] class instead of the [Gamepad](/uwp/api/Windows.Gaming.Input.Gamepad) class. See [Gamepad and vibration](gamepad-and-vibration.md) for more information.

<!-- Arcade sticks are managed by the system, therefore you don't have to create or initialize them. The system provides a list of connected arcades sticks and events to notify you when an arcade stick is added or removed.

### The arcade sticks list

The [ArcadeStick][] class provides a static property, [ArcadeSticks][], which is a read-only list of arcade sticks that are currently connected. Because you might only be interested in some of the connected arcade sticks, it's recommended that you maintain your own collection instead of accessing them through the `ArcadeSticks` property.

The following example copies all connected arcade sticks into a new collection. Note that because other threads in the background will be accessing this collection (in the [ArcadeStickAdded][] and [ArcadeStickRemoved][] events), you need to place a lock around any code that reads or updates the collection.

```cpp
auto myArcadeSticks = ref new Vector<ArcadeStick^>();
critical_section myLock{};

for (auto arcadeStick : ArcadeStick::ArcadeSticks)
{
    // Check if the arcade stick is already in myArcadeSticks; if it isn't, add
    // it.
    critical_section::scoped_lock lock{ myLock };
    auto it = std::find(begin(myArcadeSticks), end(myArcadeSticks), arcadeStick);

    if (it == end(myArcadeSticks))
    {
        // This code assumes that you're interested in all arcade sticks.
        myArcadeSticks->Append(arcadeStick);
    }
}
```

### Adding and removing arcade sticks

When an arcade stick is added or removed the [ArcadeStickAdded][] and [ArcadeStickRemoved][] events are raised. You can register handlers for these events to keep track of the arcade sticks that are currently connected.

The following example starts tracking an arcade stick that's been added.

```cpp
ArcadeStick::ArcadeStickAdded += ref new EventHandler<ArcadeStick^>(Platform::Object^, ArcadeStick^ args)
{
    // Check if the just-added arcade stick is already in myArcadeSticks; if it
    // isn't, add it.
    critical_section::scoped_lock lock{ myLock };
    auto it = std::find(begin(myGamepads), end(myGamepads), args);

    // This code assumes that you're interested in all new arcade sticks.
    myArcadeSticks->Append(args);
}
```

The following example stops tracking an arcade stick that's been removed.

```cpp
ArcadeStick::ArcadeStickRemoved += ref new EventHandler<ArcadeStick^>(Platform::Object^, ArcadeStick^ args)
{
    unsigned int indexRemoved;

    if(myArcadeSticks->IndexOf(args, &indexRemoved))
    {
        myArcadeSticks->RemoveAt(indexRemoved);
    }
}
```

### Users and headsets

Each arcade stick can be associated with a user account to link their identity to their gameplay, and can have a headset attached to facilitate voice chat or in-game features. To learn more about working with users and headsets, see [Tracking users and their devices](input-practices-for-games.md#tracking-users-and-their-devices) and [Headset](headset.md). -->

## Reading the arcade stick

After you identify the arcade stick that you're interested in, you're ready to gather input from it. However, unlike some other kinds of input that you might be used to, arcade sticks don't communicate state-change by raising events. Instead, you take regular readings of their current state by _polling_ them.

### Polling the arcade stick

Polling captures a snapshot of the arcade stick at a precise point in time. This approach to input gathering is a good fit for most games because their logic typically runs in a deterministic loop rather than being event-driven; it's also typically simpler to interpret game commands from input gathered all at once than it is from many single inputs gathered over time.

You poll an arcade stick by calling [GetCurrentReading][]; this function returns an [ArcadeStickReading][] that contains the state of the arcade stick.

The following example polls an arcade stick for its current state.

```cpp
auto arcadestick = myArcadeSticks[0];

ArcadeStickReading reading = arcadestick->GetCurrentReading();
```

In addition to the arcade stick state, each reading includes a timestamp that indicates precisely when the state was retrieved. The timestamp is useful for relating to the timing of previous readings or to the timing of the game simulation.

### Reading the buttons

Each of the arcade stick buttons&mdash;the four directions of the joystick, six **Action** buttons, and two **Special** buttons&mdash;provides a digital reading that indicates whether it's pressed (down) or released (up). For efficiency, button readings aren't represented as individual boolean values; instead, they're all packed into a single bitfield that's represented by the [ArcadeStickButtons][] enumeration.

> [!NOTE]
> Arcade sticks are equipped with additional buttons used for UI navigation such as the **View** and **Menu** buttons. These buttons are not a part of the `ArcadeStickButtons` enumeration and can only be read by accessing the arcade stick as a UI navigation device. For more information, see [UI Navigation Device](ui-navigation-controller.md).

The button values are read from the `Buttons` property of the [ArcadeStickReading][] structure. Because this property is a bitfield, bitwise masking is used to isolate the value of the button that you're interested in. The button is pressed (down) when the corresponding bit is set; otherwise, it's released (up).

The following example determines whether the **Action 1** button is pressed.

```cpp
if (ArcadeStickButtons::Action1 == (reading.Buttons & ArcadeStickButtons::Action1))
{
    // Action 1 is pressed
}
```

The following example determines whether the **Action 1** button is released.

```cpp
if (ArcadeStickButtons::None == (reading.Buttons & ArcadeStickButtons::Action1))
{
    // Action 1 is released (not pressed)
}
```

Sometimes you might want to determine when a button transitions from pressed to released or released to pressed, whether multiple buttons are pressed or released, or if a set of buttons are arranged in a particular way&mdash;some pressed, some not. For information on how to detect these conditions, see [Detecting button transitions](input-practices-for-games.md#detecting-button-transitions) and [Detecting complex button arrangements](input-practices-for-games.md#detecting-complex-button-arrangements).

## Run the InputInterfacing sample

The [InputInterfacingUWP sample _(github)_](https://github.com/microsoft/Xbox-ATG-Samples/tree/main/UWPSamples/System/InputInterfacingUWP) demonstrates how to use arcade sticks and different kinds of input devices in tandem, as well as how these input devices behave as UI navigation controllers.

## See also

* [Windows.Gaming.Input.UINavigationController][]
* [Windows.Gaming.Input.IGameController][]
* [Input practices for games](input-practices-for-games.md)

[Windows.Gaming.Input]: /uwp/api/Windows.Gaming.Input
[Windows.Gaming.Input.IGameController]: /uwp/api/Windows.Gaming.Input.IGameController
[Windows.Gaming.Input.UINavigationController]: /uwp/api/Windows.Gaming.Input.UINavigationController
[arcadestick]: /uwp/api/Windows.Gaming.Input.ArcadeStick
[arcadesticks]: /uwp/api/Windows.Gaming.Input.ArcadeStick
[arcadestickadded]: /uwp/api/Windows.Gaming.Input.ArcadeStick
[arcadestickremoved]: /uwp/api/Windows.Gaming.Input.ArcadeStick
[getcurrentreading]: /uwp/api/Windows.Gaming.Input.ArcadeStick
[arcadestickreading]: /uwp/api/Windows.Gaming.Input.ArcadeStickReading
[arcadestickbuttons]: /uwp/api/Windows.Gaming.Input.ArcadeStickButtons