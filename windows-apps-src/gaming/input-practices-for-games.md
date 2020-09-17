---
title: Input practices for games
description: Learn patterns and techniques for effectively using input devices in Universal Windows Platform (UWP) games.
ms.assetid: CBAD3345-3333-4924-B6D8-705279F52676
ms.date: 11/20/2017
ms.topic: article
keywords: windows 10, uwp, games, input
ms.localizationpriority: medium
---

# Input practices for games

This topic describes patterns and techniques for effectively using input devices in Universal Windows Platform (UWP) games.

By reading this topic, you'll learn:

* how to track players and which input and navigation devices they're currently using
* how to detect button transitions (pressed-to-released, released-to-pressed)
* how to detect complex button arrangements with a single test

## Choosing an input device class

There are many different types of input APIs available to you, such as [ArcadeStick](/uwp/api/windows.gaming.input.arcadestick), [FlightStick](/uwp/api/windows.gaming.input.flightstick), and [Gamepad](/uwp/api/windows.gaming.input.gamepad). How do you decide which API to use for your game?

You should choose whichever API gives you the most appropriate input for your game. For example, if you're making a 2D platform game, you can probably just use the **Gamepad** class and not bother with the extra functionality available via other classes. This would restrict the game to supporting gamepads only and provide a consistent interface that will work across many different gamepads with no need for additional code.

On the other hand, for complex flight and racing simulations, you might want to enumerate all of the [RawGameController](/uwp/api/windows.gaming.input.rawgamecontroller) objects as a baseline to make sure they support any niche device that enthusiast players might have, including devices such as separate pedals or throttle that are still used by a single player. 

From there, you can use an input class's **FromGameController** method, such as [Gamepad.FromGameController](/uwp/api/windows.gaming.input.gamepad.fromgamecontroller), to see if each device has a more curated view. For example, if the device is also a **Gamepad**, then you might want to adjust the button mapping UI to reflect that, and provide some sensible default button mappings to choose from. (This is in contrast to requiring the player to manually configure the gamepad inputs if you're only using **RawGameController**.) 

Alternatively, you can look at the vendor ID (VID) and product ID (PID) of a **RawGameController** (using [HardwareVendorId](/uwp/api/windows.gaming.input.rawgamecontroller.HardwareVendorId) and [HardwareProductId](/uwp/api/windows.gaming.input.rawgamecontroller.HardwareProductId), respectively) and provide suggested button mappings for popular devices while still remaining compatible with unknown devices that come out in the future via manual mappings by the player.

## Keeping track of connected controllers

While each controller type includes a list of connected controllers (such as [Gamepad.Gamepads](/uwp/api/windows.gaming.input.gamepad.Gamepads)), it is a good idea to maintain your own list of controllers. See [The gamepads list](gamepad-and-vibration.md#the-gamepads-list) for more information (each controller type has a similarly named section on its own topic).

However, what happens when the player unplugs their controller, or plugs in a new one? You need to handle these events, and update your list accordingly. See [Adding and removing gamepads](gamepad-and-vibration.md#adding-and-removing-gamepads) for more information (again, each controller type has a similarly named section on its own topic).

Because the added and removed events are raised asynchronously, you could get incorrect results when dealing with your list of controllers. Therefore, anytime you access your list of controllers, you should put a lock around it so that only one thread can access it at a time. This can be done with the [Concurrency Runtime](/cpp/parallel/concrt/concurrency-runtime), specifically the [critical_section class](/cpp/parallel/concrt/reference/critical-section-class), in **&lt;ppl.h&gt;**.

Another thing to think about is that the list of connected controllers will initially be empty, and takes a second or two to populate. So if you only assign the current gamepad in the start method, it will be **null**!

To rectify this, you should have a method that "refreshes" the main gamepad (in a single-player game; multiplayer games will require more sophisticated solutions). You should then call this method in both your controller added and controller removed event handlers, or in your update method.

The following method simply returns the first gamepad in the list (or **nullptr** if the list is empty). Then you just need to remember to check for **nullptr** anytime you do anything with the controller. It's up to you whether you want to block gameplay when there's no controller connected (for example, by pausing the game) or simply have gameplay continue, while ignoring input.

```cpp
#include <ppl.h>

using namespace Platform::Collections;
using namespace Windows::Gaming::Input;
using namespace concurrency;

Vector<Gamepad^>^ m_myGamepads = ref new Vector<Gamepad^>();

Gamepad^ GetFirstGamepad()
{
    Gamepad^ gamepad = nullptr;
    critical_section::scoped_lock{ m_lock };

    if (m_myGamepads->Size > 0)
    {
        gamepad = m_myGamepads->GetAt(0);
    }

    return gamepad;
}
```

Putting it all together, here is an example of how to handle input from a gamepad:

```cpp
#include <algorithm>
#include <ppl.h>

using namespace Platform::Collections;
using namespace Windows::Foundation;
using namespace Windows::Gaming::Input;
using namespace concurrency;

static Vector<Gamepad^>^ m_myGamepads = ref new Vector<Gamepad^>();
static Gamepad^          m_gamepad = nullptr;
static critical_section  m_lock{};

void Start()
{
    // Register for gamepad added and removed events.
    Gamepad::GamepadAdded += ref new EventHandler<Gamepad^>(&OnGamepadAdded);
    Gamepad::GamepadRemoved += ref new EventHandler<Gamepad^>(&OnGamepadRemoved);

    // Add connected gamepads to m_myGamepads.
    for (auto gamepad : Gamepad::Gamepads)
    {
        OnGamepadAdded(nullptr, gamepad);
    }
}

void Update()
{
    // Update the current gamepad if necessary.
    if (m_gamepad == nullptr)
    {
        auto gamepad = GetFirstGamepad();

        if (m_gamepad != gamepad)
        {
            m_gamepad = gamepad;
        }
    }

    if (m_gamepad != nullptr)
    {
        // Gather gamepad reading.
    }
}

// Get the first gamepad in the list.
Gamepad^ GetFirstGamepad()
{
    Gamepad^ gamepad = nullptr;
    critical_section::scoped_lock{ m_lock };

    if (m_myGamepads->Size > 0)
    {
        gamepad = m_myGamepads->GetAt(0);
    }

    return gamepad;
}

void OnGamepadAdded(Platform::Object^ sender, Gamepad^ args)
{
    // Check if the just-added gamepad is already in m_myGamepads; if it isn't, 
    // add it.
    critical_section::scoped_lock lock{ m_lock };
    auto it = std::find(begin(m_myGamepads), end(m_myGamepads), args);

    if (it == end(m_myGamepads))
    {
        m_myGamepads->Append(args);
    }
}

void OnGamepadRemoved(Platform::Object^ sender, Gamepad^ args)
{
    // Remove the gamepad that was just disconnected from m_myGamepads.
    unsigned int indexRemoved;
    critical_section::scoped_lock lock{ m_lock };

    if (m_myGamepads->IndexOf(args, &indexRemoved))
    {
        if (m_gamepad == m_myGamepads->GetAt(indexRemoved))
        {
            m_gamepad = nullptr;
        }

        m_myGamepads->RemoveAt(indexRemoved);
    }
}
```

## Tracking users and their devices

All input devices are associated with a [User](/uwp/api/windows.system.user) so that their identity can be linked to their gameplay, achievements, settings changes, and other activities. Users can sign in or sign out at will, and it's common for a different user to sign in on an input device that remains connected to the system after the previous user has signed out. When a user signs in or out, the [IGameController.UserChanged](/uwp/api/windows.gaming.input.igamecontroller.UserChanged) event is raised. You can register an event handler for this event to keep track of players and the devices they're using.

User identity is also the way that an input device is associated with its corresponding [UI navigation controller](ui-navigation-controller.md).

For these reasons, player input should be tracked and correlated with the [User](/uwp/api/windows.gaming.input.igamecontroller.User) property of the device class (inherited from the [IGameController](/uwp/api/windows.gaming.input.igamecontroller) interface).

The [UserGamepadPairingUWP](/samples/microsoft/xbox-atg-samples/usergamepadpairinguwp/) sample demonstrates how you can keep track of users and the devices they're using.

## Detecting button transitions

Sometimes you want to know when a button is first pressed or released; that is, precisely when the button state transitions from released to pressed or from pressed to released. To determine this, you need to remember the previous device reading and compare the current reading against it to see what's changed.

The following example demonstrates a basic approach for remembering the previous reading; gamepads are shown here, but the principles are the same for arcade stick, racing wheel, and the other input device types.

```cpp
Gamepad gamepad;
GamepadReading newReading();
GamepadReading oldReading();

// Called at the start of the game.
void Game::Start()
{
    gamepad = Gamepad::Gamepads[0];
}

// Game::Loop represents one iteration of a typical game loop
void Game::Loop()
{
    // move previous newReading into oldReading before getting next newReading
    oldReading = newReading, newReading = gamepad.GetCurrentReading();

    // process device readings using buttonJustPressed/buttonJustReleased (see below)
}
```

Before doing anything else, `Game::Loop` moves the existing value of `newReading` (the gamepad reading from the previous loop iteration) into `oldReading`, then fills `newReading` with a fresh gamepad reading for the current iteration. This gives you the information you need to detect button transitions.

The following example demonstrates a basic approach for detecting button transitions:

```cpp
bool ButtonJustPressed(const GamepadButtons selection)
{
    bool newSelectionPressed = (selection == (newReading.Buttons & selection));
    bool oldSelectionPressed = (selection == (oldReading.Buttons & selection));

    return newSelectionPressed && !oldSelectionPressed;
}

bool ButtonJustReleased(GamepadButtons selection)
{
    bool newSelectionReleased =
        (GamepadButtons.None == (newReading.Buttons & selection));

    bool oldSelectionReleased =
        (GamepadButtons.None == (oldReading.Buttons & selection));

    return newSelectionReleased && !oldSelectionReleased;
}
```

These two functions first derive the Boolean state of the button selection from `newReading` and `oldReading`, then perform Boolean logic to determine whether the target transition has occurred. These functions return **true** only if the new reading contains the target state (pressed or released, respectively) *and* the old reading does not also contain the target state; otherwise, they return **false**.

## Detecting complex button arrangements

Each button of an input device provides a digital reading that indicates whether it's pressed (down) or released (up). For efficiency, button readings aren't represented as individual boolean values; instead, they're all packed into bitfields represented by device-specific enumerations such as [GamepadButtons](/uwp/api/windows.gaming.input.gamepadbuttons). To read specific buttons, bitwise masking is used to isolate the values that you're interested in. A button is pressed (down) when its corresponding bit is set; otherwise, it's released (up).

Recall how single buttons are determined to be pressed or released; gamepads are shown here, but the principles are the same for arcade stick, racing wheel, and the other input device types.

```cpp
GamepadReading reading = gamepad.GetCurrentReading();

// Determines whether gamepad button A is pressed.
if (GamepadButtons::A == (reading.Buttons & GamepadButtons::A))
{
    // The A button is pressed.
}

// Determines whether gamepad button A is released.
if (GamepadButtons::None == (reading.Buttons & GamepadButtons::A))
{
    // The A button is released (not pressed).
}
```

As you can see, determining the state of a single button is straight-forward, but sometimes you might want to determine whether multiple buttons are pressed or released, or if a set of buttons are arranged in a particular way&mdash;some pressed, some not. Testing multiple buttons is more complex than testing single buttons&mdash;especially with the potential of mixed button state&mdash;but there's a simple formula for these tests that applies to single and multiple button tests alike.

The following example determines whether gamepad buttons A and B are both pressed:

```cpp
if ((GamepadButtons::A | GamepadButtons::B) == (reading.Buttons & (GamepadButtons::A | GamepadButtons::B))
{
    // The A and B buttons are both pressed.
}
```

The following example determines whether gamepad buttons A and B are both released:

```cpp
if ((GamepadButtons::None == (reading.Buttons & GamepadButtons::A | GamepadButtons::B))
{
    // The A and B buttons are both released (not pressed).
}
```

The following example determines whether gamepad button A is pressed while button B is released:

```cpp
if (GamepadButtons::A == (reading.Buttons & (GamepadButtons::A | GamepadButtons::B))
{
    // The A button is pressed and the B button is released (B is not pressed).
}
```

The formula that all five of these examples have in common is that the arrangement of buttons to be tested for is specified by the expression on the left-hand side of the equality operator while the buttons to be considered are selected by the masking expression on the right-hand side.

The following example demonstrates this formula more clearly by rewriting the previous example:

```cpp
auto buttonArrangement = GamepadButtons::A;
auto buttonSelection = (reading.Buttons & (GamepadButtons::A | GamepadButtons::B));

if (buttonArrangement == buttonSelection)
{
    // The A button is pressed and the B button is released (B is not pressed).
}
```

This formula can be applied to test any number of buttons in any arrangement of their states.

## Get the state of the battery

For any game controller that implements the [IGameControllerBatteryInfo](/uwp/api/windows.gaming.input.igamecontrollerbatteryinfo) interface, you can call [TryGetBatteryReport](/uwp/api/windows.gaming.input.igamecontrollerbatteryinfo.TryGetBatteryReport) on the controller instance to get a [BatteryReport](/uwp/api/windows.devices.power.batteryreport) object that provides information about the battery in the controller. You can get properties like the rate that the battery is charging ([ChargeRateInMilliwatts](/uwp/api/windows.devices.power.batteryreport.ChargeRateInMilliwatts)), the estimated energy capacity of a new battery ([DesignCapacityInMilliwattHours](/uwp/api/windows.devices.power.batteryreport.DesignCapacityInMilliwattHours)), and the fully-charged energy capacity of the current battery ([FullChargeCapacityInMilliwattHours](/uwp/api/windows.devices.power.batteryreport.FullChargeCapacityInMilliwattHours)).

For game controllers that support detailed battery reporting, you can get this and more information about the battery, as detailed in [Get battery information](../devices-sensors/get-battery-info.md). However, most game controllers don't support that level of battery reporting, and instead use low-cost hardware. For these controllers, you'll need to keep the following considerations in mind:

* **ChargeRateInMilliwatts** and **DesignCapacityInMilliwattHours** will always be **NULL**.

* You can get the battery percentage by calculating [RemainingCapacityInMilliwattHours](/uwp/api/windows.devices.power.batteryreport.RemainingCapacityInMilliwattHours) / **FullChargeCapacityInMilliwattHours**. You should ignore the values of these properties and only deal with the calculated percentage.

* The percentage from the previous bullet point will always be one of the following:

    * 100% (Full)
    * 70% (Medium)
    * 40% (Low)
    * 10% (Critical)

If your code performs some action (like drawing UI) based on the percentage of battery life remaining, make sure that it conforms to the values above. For example, if you want to warn the player when the controller's battery is low, do so when it reaches 10%.

## See also

* [Windows.System.User class](/uwp/api/windows.system.user)
* [Windows.Gaming.Input.IGameController interface](/uwp/api/windows.gaming.input.igamecontroller)
* [Windows.Gaming.Input.GamepadButtons enum](/uwp/api/windows.gaming.input.gamepadbuttons)
* [UserGamepadPairingUWP sample](/samples/microsoft/xbox-atg-samples/usergamepadpairinguwp/)