---
title: UI navigation controller
description: Use the Windows.Gaming.Input UI navigation controller APIs to detect and read different kinds of input devices for UI navigation.
ms.assetid: 5A14926D-8C2E-4DE8-AAFB-BEEB9BFE91A5
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp, games, ui, navigation
ms.localizationpriority: medium
---
# UI navigation controller

This page describes the basics of programming for UI navigation devices using [Windows.Gaming.Input.UINavigationController][uinavigationcontroller] and related APIs for the Universal Windows Platform (UWP).

By reading this page, you'll learn:
* How to gather a list of connected UI navigation devices and their users
* How to detect that a navigation device has been added or removed
* How to read input from one or more UI navigation devices
* How gamepads and arcade sticks behave as navigation devices

## UI navigation controller overview

Almost all games have at least some user interface that's separate from gameplay, even if its just pregame menus or in-game dialogs. Players need to be able to navigate this UI using whichever input device they've chosen, but it burdens developers to add specific support for each kind of input device and can also introduce inconsistencies between games and input devices that confuse players. For these reasons the [UINavigationController][] API was created.

UI navigation controllers are _logical_ input devices that exist to provide a vocabulary of common UI Navigation commands that can be supported by a variety of _physical_ input devices. A _UI navigation controller_ is just a different way of looking at a physical input device; we use _navigation device_ to refer to any physical input device being viewed as a navigation controller. By programming against a navigation device rather than specific input devices, developers avoid the burden of supporting different input devices and achieve consistency by default.

Because the number and variety of controls supported by each kind of input device can be so different and because certain input devices might want to support a richer set of navigation commands, the navigation controller interface divides the vocabulary of commands into a _required set_ containing the most common and essential commands, and an _optional set_ containing convenient but nonessential commands. All navigation devices support every command in the _required set_ and may support all, some, or none of the commands in the _optional set_.

### Required set

Navigation devices must support all navigation commands in the _required set_; these are the directional (up, down, left, and right), view, menu, accept, and cancel commands.

The directional commands are intended for primary [XY-focus navigation](../design/input/gamepad-and-remote-interactions.md#xy-focus-navigation-and-interaction) between single UI elements. The view and menu commands are intended for displaying gameplay information (often momentary, sometimes modally) and for switching between gameplay and menu contexts, respectively. The accept and cancel commands are intended for affirmative (yes) and negative (no) responses, respectively.

The following table summarizes these commands and their intended uses, with examples.
| Command | Intended use
| -------:| ---------------
|      Up | XY-focus navigation up
|    Down | XY-focus navigation down
|    Left | XY-focus navigation left
|   Right | XY-focus navigation right
|    View | Display gameplay info _(scoreboard, game stats, objectives, world or area map)_
|    Menu | Primary menu / Pause  _(settings, status, equipment, inventory, pause)_
|  Accept | Affirmative response  _(accept, advance, confirm, start, yes)_
|  Cancel | Negative response     _(reject, reverse, decline, stop, no)_


### Optional set

Navigation devices may also support all, some, or none of the navigation commands in the _optional set_; these are the paging (up, down, left, and right), scrolling (up, down, left, and right), and contextual (context 1-4) commands.

The contextual commands are explicitly intended for application-specific commands and navigation shortcuts. The paging and scrolling commands are intended for quick navigation between pages or groups of UI elements and for fine-grained navigation within UI elements, respectively.

The following table summarizes these commands and their intended uses.
|     Command | Intended use
| -----------:| ------------
|      PageUp | Jump upward (to upper/previous vertical page or group)
|    PageDown | Jump downward (to lower/next vertical page or group)
|    PageLeft | Jump left (to leftward/previous horizontal page or group)
|   PageRight | Jump right (to rightward/next horizontal page or group)
|    ScrollUp | Scroll up (within focused UI element or scrollable group)
|  ScrollDown | Scroll down (within focused UI element or scrollable group)
|  ScrollLeft | Scroll left (within focused UI element or scrollable group)
| ScrollRight | Scroll right (within focused UI element or scrollable group)
|    Context1 | Primary context action
|    Context2 | Secondary context action
|    Context3 | Third context action
|    Context4 | Fourth context action

> **Note**    Although a game is free to respond to any command with an actual function that is different than its intended use, surprising behavior should be avoided. In particular, don't change the actual function of a command if you need its intended use, try to assign novel functions to the command that makes the most sense, and assign counterpart functions to counterpart commands such as PageUp/PageDown. Finally, consider which commands are supported by each kind of input device and which controls they are mapped to, making sure that critical commands are accessible from every device.

## Gamepad, arcade stick, and racing wheel navigation

All input devices supported by the Windows.Gaming.Input namespace are UI navigation devices.

The following table summarizes how the _required set_ of navigation commands map to various input devices.

| Navigation command | Gamepad input                       | Arcade stick input | Racing Wheel input |
| ------------------:| ----------------------------------- | ------------------ | ------------------ |
|                 Up | Left thumbstick up / D-pad up       | Stick up           | D-pad up           |
|               Down | Left thumbstick down / D-pad down   | Stick down         | D-pad down         |
|               Left | Left thumbstick left / D-pad left   | Stick left         | D-pad left         |
|              Right | Left thumbstick right / D-pad right | Stick right        | D-pad right        |
|               View | View button                         | View button        | View button        |
|               Menu | Menu button                         | Menu button        | Menu button        |
|             Accept | A button                            | Action 1 button    | A button           |
|             Cancel | B button                            | Action 2 button    | B button           |

The following table summarizes how the _optional set_ of navigation commands map to various input devices.

| Navigation command | Gamepad input          | Arcade stick input | Racing Wheel input    |
| ------------------:| ---------------------- | ------------------ | --------------------- |
|             PageUp | Left trigger           | _not supported_    | _varies_              |
|           PageDown | Right trigger          | _not supported_    | _varies_              |
|           PageLeft | Left bumper            | _not supported_    | _varies_              |
|          PageRight | Right bumper           | _not supported_    | _varies_              |
|           ScrollUp | Right thumbstick up    | _not supported_    | _varies_              |
|         ScrollDown | Right thumbstick down  | _not supported_    | _varies_              |
|         ScrollLeft | Right thumbstick left  | _not supported_    | _varies_              |
|        ScrollRight | Right thumbstick right | _not supported_    | _varies_              |
|           Context1 | X button               | _not supported_    | X button (_commonly_) |
|           Context2 | Y button               | _not supported_    | Y button (_commonly_) |
|           Context3 | Left thumbstick press  | _not supported_    | _varies_              |
|           Context4 | Right thumbstick press | _not supported_    | _varies_              |


## Detect and track UI navigation controllers

Although UI navigation controllers are logical input devices, they are a representation of a physical device
and are managed by the system in the same way. You don't have to create or initialize them; the system provides a list of connected UI navigation controllers and events to notify you when a UI Navigation controller is added or removed.

### The UI navigation controllers list

The [UINavigationController][] class provides a static property, [UINavigationControllers][], which is a read-only list of UI navigation devices that are currently connected. Because you might only be interested in some of the connected navigation devices, its recommended that you maintain your own collection instead of accessing them through the `UINavigationControllers` property.

The following example copies all connected UI navigation controllers into a new collection.
```cpp
auto myNavigationControllers = ref new Vector<UINavigationController^>();

for (auto device : UINavigationController::UINavigationControllers)
{
    // This code assumes that you're interested in all navigation controllers.
    myNavigationControllers->Append(device);
}
```

### Adding and removing UI navigation controllers

When a UI navigation controller is added or removed the [UINavigationControllerAdded][] and [UINavigationControllerRemoved][] events are raised. You can register an event handler for these events to keep track of the navigation devices that are currently connected.

The following example starts tracking a UI navigation device that's been added.
```cpp
UINavigationController::UINavigationControllerAdded += ref new EventHandler<UINavigationController^>(Platform::Object^, UINavigationController^ args)
{
    // This code assumes that you're interested in all new navigation controllers.
    myNavigationControllers->Append(args);
}
```

The following example stops tracking an arcade stick that's been removed.
```cpp
UINavigationController::UINavigationControllerRemoved += ref new EventHandler<UINavigationController^>(Platform::Object^, UINavigationController^ args)
{
    unsigned int indexRemoved;

    if(myNavigationControllers->IndexOf(args, &indexRemoved))
	{
        myNavigationControllers->RemoveAt(indexRemoved);
    }
}
```

### Users and headsets

Each navigation device can be associated with a user account to link their identity to their input, and can have a headset attached to facilitate voice chat or navigation features. To learn more about working with users and headsets, see [Tracking users and their devices](input-practices-for-games.md#tracking-users-and-their-devices) and [Headset](headset.md).

## Reading the UI navigation controller

After you identify the UI navigation device that you're interested in, you're ready to gather input from it. However, unlike some other kinds of input that you might be used to, navigation devices don't communicate state-change by raising events. Instead, you take regular readings of their current state by _polling_ them.

### Polling the UI navigation controller

Polling captures a snapshot of the navigation device at a precise point in time. This approach to input gathering is a good fit for most games because their logic typically runs in a deterministic loop rather than being event-driven; its also typically simpler to interpret game commands from input gathered all at once than it is from many single inputs gathered over time.

You poll a navigation device by calling [UINavigationController.GetCurrentReading][getcurrentreading]; this function returns a [UINavigationReading][] that contains the state of the navigation device.

```cpp
auto navigationController = myNavigationControllers[0];

UINavigationReading reading = navigationController->GetCurrentReading();
```

### Reading the buttons

Each of the UI navigation buttons provide a boolean reading that corresponds to whether its pressed (down), or released (up). For efficiency, button readings aren't represented as individual boolean values; instead they're all packed into one of two bitfields represented by the [RequiredUINavigationButtons][] and [OptionalUINavigationButtons][] enumerations.

The buttons belonging to the _required set_ are read from the `RequiredButtons` property of the [UINavigationReading][] structure; the buttons belonging to the _optional set_ are read from the `OptionalButtons` property. Because these properties are bitfields, bitwise masking is used to isolate the value of the button that you're interested in. The button is pressed (down) when the corresponding bit is set; otherwise its released (up).

The following example determines whether the Accept button in the _required set_ is pressed.
```cpp
if (RequiredUINavigationButtons::Accept == (reading.RequiredButtons & RequiredUINavigationButtons::Accept))
{
    // Accept is pressed
}
```

The following example determines whether the Accept button in the _required set_ is released.
```cpp
if (RequiredUINavigationButtons::None == (reading.RequiredButtons & RequiredUINavigationButtons::Accept))
{
    // Accept is released (not pressed)
}
```

Be sure to use the `OptionalButtons` property and `OptionalUINavigationButtons` enumeration when reading buttons in the _optional set_.

The following example determines whether the Context 1 button in the _optional set_ is pressed.
```cpp
if (OptionalUINavigationButtons::Context1 == (reading.OptionalButtons & OptionalUINavigationButtons::Context1))
{
    // Context 1 is pressed
}
```

Sometimes you might want to determine when a button transitions from pressed to released or released to pressed, whether multiple buttons are pressed or released, or if a set of buttons are arranged in a particular way--some pressed, some not. For information on how to detect these conditions, see [Detecting button transitions](input-practices-for-games.md#detecting-button-transitions) and [Detecting complex button arrangements](input-practices-for-games.md#detecting-complex-button-arrangements).


## Run the UI navigation controller sample

The [InputInterfacingUWP sample _(github)_](https://github.com/Microsoft/Xbox-ATG-Samples/tree/master/UWPSamples/System/InputInterfacingUWP) demonstrates how the different input devices behave as UI navigation controllers.

## See also
[Windows.Gaming.Input.Gamepad][]
[Windows.Gaming.Input.ArcadeStick][]
[Windows.Gaming.Input.RacingWheel][]
[Windows.Gaming.Input.IGameController][]


[Windows.Gaming.Input]: /uwp/api/Windows.Gaming.Input
[Windows.Gaming.Input.Gamepad]: /uwp/api/Windows.Gaming.Input.Gamepad
[Windows.Gaming.Input.Arcadestick]: /uwp/api/Windows.Gaming.Input.ArcadeStick
[Windows.Gaming.Input.Racingwheel]: /uwp/api/Windows.Gaming.Input.RacingWheel
[Windows.Gaming.Input.IGameController]: /uwp/api/Windows.Gaming.Input.IGameController
[uinavigationcontroller]: /uwp/api/Windows.Gaming.Input.UINavigationController
[uinavigationcontrollers]: /uwp/api/Windows.Gaming.Input.UINavigationController
[uinavigationcontrolleradded]: /uwp/api/Windows.Gaming.Input.UINavigationController
[uinavigationcontrollerremoved]: /uwp/api/Windows.Gaming.Input.UINavigationController
[getcurrentreading]: /uwp/api/Windows.Gaming.Input.UINavigationController
[uinavigationreading]: /uwp/api/Windows.Gaming.Input.UINavigationReading
[requireduinavigationbuttons]: /uwp/api/Windows.Gaming.Input.RequiredUINavigationButtons
[optionaluinavigationbuttons]: /uwp/api/Windows.Gaming.Input.OptionalUINavigationButtons