---
title: Racing Wheel and Force Feedback
description: Use the Windows.Gaming.Input racing wheel APIs to detect, determine capabilities, read, and send force feedback commands to racing wheels.
ms.assetid: 6287D87F-6F2E-4B67-9E82-3D6E51CBAFF9
ms.date: 05/09/2018
ms.topic: article
keywords: windows 10, uwp, games, racing wheel, force feedback
ms.localizationpriority: medium
---

# Racing wheel and force feedback

This page describes the basics of programming for racing wheels on Xbox One using [Windows.Gaming.Input.RacingWheel][racingwheel] and related APIs for the Universal Windows Platform (UWP).

By reading this page, you'll learn:

* how to gather a list of connected racing wheels and their users
* how to detect that a racing wheel has been added or removed
* how to read input from one or more racing wheels
* how to send force feedback commands
* how racing wheels behave as UI navigation devices

## Racing wheel overview

Racing wheels are input devices that resemble the feel of a real racecar cockpit. Racing wheels are the perfect input device for both arcade-style and simulation-style racing games that feature cars or trucks. Racing wheels are supported in Windows 10 or Windows 11 and Xbox One UWP apps by the [Windows.Gaming.Input](/uwp/api/windows.gaming.input) namespace.

Racing wheels are offered at a variety of price points, generally having more and better input and force feedback capabilities as their price points rise. All racing wheels are equipped with an analog steering wheel, analog throttle and brake controls, and some on-wheel buttons. Some racing wheels are additionally equipped with analog clutch and handbrake controls, pattern shifters, and force feedback capabilities. Not all racing wheels are equipped with the same sets of features, and may also vary in their support for certain features&mdash;for example, steering wheels might support different ranges of rotation and pattern shifters might support different numbers of gears.

### Device capabilities

Different racing wheels offer different sets of optional device capabilities and varying levels of support for those capabilities; this level of variation between a single kind of input device is unique among the devices supported by the [Windows.Gaming.Input](/uwp/api/windows.gaming.input) API. Furthermore, most devices you'll encounter will support at least some optional capabilities or other variations. Because of this, it's important to determine the capabilities of each connected racing wheel individually and to support the full variation of capabilities that makes sense for your game.

For more information, see [Determining racing wheel capabilities](#determining-racing-wheel-capabilities).

### Force feedback

Some racing wheels offer true force feedback&mdash;that is, they can apply actual forces on an axis of control such as their steering wheel&mdash;not just simple vibration. Games use this ability to create a greater sense of immersion (_simulated crash damage_, "road feel") and to increase the challenge of driving well.

For more information, see [Force feedback overview](#force-feedback-overview).

### UI navigation

In order to ease the burden of supporting the different input devices for user interface navigation and to encourage consistency between games and devices, most _physical_ input devices simultaneously act as a separate _logical_ input device called a [UI navigation controller](ui-navigation-controller.md). The UI navigation controller provides a common vocabulary for UI navigation commands across input devices.

Due to their unique focus on analog controls and the degree of variation between different racing wheels, they're typically equipped with a digital D-pad, **View**, **Menu**, **A**, **B**, **X**, and **Y** buttons that resemble those of a [gamepad](gamepad-and-vibration.md); these buttons aren't intended to support gameplay commands and can't be readily accessed as racing wheel buttons.

As a UI navigation controller, racing wheels map the [required set](ui-navigation-controller.md#required-set) of navigation commands to the left thumbstick, D-pad, **View**, **Menu**, **A**, and **B** buttons.

| Navigation command | Racing wheel input |
| ------------------:| ------------------ |
|                 Up | D-pad up           |
|               Down | D-pad down         |
|               Left | D-pad left         |
|              Right | D-pad right        |
|               View | View button        |
|               Menu | Menu button        |
|             Accept | A button           |
|             Cancel | B button           |

Additionally, some racing wheels might map some of the [optional set](ui-navigation-controller.md#optional-set) of navigation commands to other inputs they support, but command mappings can vary from device to device. Consider supporting these commands as well, but make sure that these commands are not essential to navigating your game's interface.

| Navigation command | Racing wheel input    |
| ------------------:| --------------------- |
|            Page Up | _varies_              |
|          Page Down | _varies_              |
|          Page Left | _varies_              |
|         Page Right | _varies_              |
|          Scroll Up | _varies_              |
|        Scroll Down | _varies_              |
|        Scroll Left | _varies_              |
|       Scroll Right | _varies_              |
|          Context 1 | X Button (_commonly_) |
|          Context 2 | Y Button (_commonly_) |
|          Context 3 | _varies_              |
|          Context 4 | _varies_              |

## Detect and track racing wheels

Detecting and tracking racing wheels works in exactly the same way as it does for gamepads, except with the [RacingWheel](/uwp/api/windows.gaming.input.racingwheel) class instead of the [Gamepad](/uwp/api/Windows.Gaming.Input.Gamepad) class. See [Gamepad and vibration](gamepad-and-vibration.md) for more information.

<!-- Racing wheels are managed by the system, therefore you don't have to create or initialize them. The system provides a list of connected racing wheels and events to notify you when a racing wheel is added or removed.

### The racing wheels list

The [RacingWheel](/uwp/api/windows.gaming.input.racingwheel) class provides a static property, [RacingWheels](/uwp/api/windows.gaming.input.racingwheel.racingwheels#Windows_Gaming_Input_RacingWheel_RacingWheels), which is a read-only list of racing wheels that are currently connected. Because you might only be interested in some of the connected racing wheels, it's recommended that you maintain your own collection instead of accessing them through the `RacingWheels` property.

The following example copies all connected racing wheels into a new collection.
```cpp
auto myRacingWheels = ref new Vector<RacingWheel^>();

for (auto racingwheel : RacingWheel::RacingWheels)
{
    // This code assumes that you're interested in all racing wheels.
    myRacingWheels->Append(racingwheel);
}
```

### Adding and removing racing wheels

When a racing wheel is added or removed the [RacingWheelAdded](/uwp/api/windows.gaming.input.racingwheel.racingwheeladded) and [RacingWheelRemoved](/uwp/api/windows.gaming.input.racingwheel.racingwheelremoved) events are raised. You can register handlers for these events to keep track of the racing wheels that are currently connected.

The following example starts tracking an racing wheels that's been added.
```cpp
RacingWheel::RacingWheelAdded += ref new EventHandler<RacingWheel^>(Platform::Object^, RacingWheel^ args)
{
    // This code assumes that you're interested in all new racing wheels.
    myRacingWheels->Append(args);
}
```

The following example stops tracking a racing wheel that's been removed.
```cpp
RacingWheel::RacingWheelRemoved += ref new EventHandler<RacingWheel^>(Platform::Object^, RacingWheel^ args)
{
    unsigned int indexRemoved;

	if(myRacingWheels->IndexOf(args, &indexRemoved))
	{
        myRacingWheels->RemoveAt(indexRemoved);
    }
}
```

### Users and headsets

Each racing wheel can be associated with a user account to link their identity to their gameplay, and can have a headset attached to facilitate voice chat or in-game features. To learn more about working with users and headsets, see [Tracking users and their devices](input-practices-for-games.md#tracking-users-and-their-devices) and [Headset](headset.md). -->

## Reading the racing wheel

After you identify the racing wheels that you're interested in, you're ready to gather input from them. However, unlike some other kinds of input that you might be used to, racing wheels don't communicate state-change by raising events. Instead, you take regular readings of their current states by _polling_ them.

### Polling the racing wheel

Polling captures a snapshot of the racing wheel at a precise point in time. This approach to input gathering is a good fit for most games because their logic typically runs in a deterministic loop rather than being event-driven; it's also typically simpler to interpret game commands from input gathered all at once than it is from many single inputs gathered over time.

You poll a racing wheel by calling [GetCurrentReading](/uwp/api/windows.gaming.input.racingwheel.getcurrentreading#Windows_Gaming_Input_RacingWheel_GetCurrentReading); this function returns a [RacingWheelReading](/uwp/api/windows.gaming.input.racingwheelreading) that contains the state of the racing wheel.

The following example polls a racing wheel for its current state.

```cpp
auto racingwheel = myRacingWheels[0];

RacingWheelReading reading = racingwheel->GetCurrentReading();
```

In addition to the racing wheel state, each reading includes a timestamp that indicates precisely when the state was retrieved. The timestamp is useful for relating to the timing of previous readings or to the timing of the game simulation.

### Determining racing wheel capabilities

Many of the racing wheel controls are optional or support different variations even in the required controls, so you have to determine the capabilities of each racing wheel individually before you can process the input gathered in each reading of the racing wheel.

The optional controls are the handbrake, clutch, and pattern shifter; you can determine whether a connected racing wheel supports these controls by reading the [HasHandbrake](/uwp/api/windows.gaming.input.racingwheel.hashandbrake#Windows_Gaming_Input_RacingWheel_HasHandbrake), [HasClutch](/uwp/api/windows.gaming.input.racingwheel.hasclutch#Windows_Gaming_Input_RacingWheel_HasClutch), and [HasPatternShifter](/uwp/api/windows.gaming.input.racingwheel.haspatternshifter#Windows_Gaming_Input_RacingWheel_HasPatternShifter) properties of the racing wheel, respectively. The control is supported if the value of the property is **true**; otherwise it's not supported.

```cpp
if (racingwheel->HasHandbrake)
{
    // the handbrake is supported
}

if (racingwheel->HasClutch)
{
    // the clutch is supported
}

if (racingwheel->HasPatternShifter)
{
    // the pattern shifter is supported
}
```

Additionally, the controls that may vary are the steering wheel and pattern shifter. The steering wheel can vary by the degree of physical rotation that the actual wheel can support, while the pattern shifter can vary by the number of distinct forward gears it supports. You can determine the greatest angle of rotation the actual wheel supports by reading the `MaxWheelAngle` property of the racing wheel; its value is the maximum supported physical angle in degrees clock-wise (positive) which is likewise supported in the counter-clock-wise direction (negative degrees). You can determine the greatest forward gear the pattern shifter supports by reading the `MaxPatternShifterGear` property of the racing wheel; its value is the highest forward gear supported, inclusive&mdash;that is, if its value is 4, then the pattern shifter supports reverse, neutral, first, second, third, and fourth gears.

```cpp
auto maxWheelDegrees = racingwheel->MaxWheelAngle;
auto maxShifterGears = racingwheel->MaxPatternShifterGear;
```

Finally, some racing wheels support force feedback through the steering wheel. You can determine whether a connected racing wheel supports force feedback by reading the [WheelMotor](/uwp/api/windows.gaming.input.racingwheel.wheelmotor#Windows_Gaming_Input_RacingWheel_WheelMotor) property of the racing wheel. Force feedback is supported if `WheelMotor` is not **null**; otherwise it's not supported.

```cpp
if (racingwheel->WheelMotor != nullptr)
{
    // force feedback is supported
}
```

For information on how to use the force feedback capability of racing wheels that support it, see [Force feedback overview](#force-feedback-overview).

### Reading the buttons

Each of the racing wheel buttons&mdash;the four directions of the D-pad, the **Previous Gear** and **Next Gear** buttons, and 16 additional buttons&mdash;provides a digital reading that indicates whether it's pressed (down) or released (up). For efficiency, button readings aren't represented as individual boolean values; instead they're all packed into a single bitfield that's represented by the [RacingWheelButtons](/uwp/api/windows.gaming.input.racingwheelbuttons) enumeration.

> [!NOTE]
> Racing wheels are equipped with additional buttons used for UI navigation such as the **View** and **Menu** buttons. These buttons are not a part of the `RacingWheelButtons` enumeration and can only be read by accessing the racing wheel as a UI navigation device. For more information, see [UI Navigation Device](ui-navigation-controller.md).

The button values are read from the `Buttons` property of the [RacingWheelReading](/uwp/api/windows.gaming.input.racingwheelreading) structure. Because this property is a bitfield, bitwise masking is used to isolate the value of the button that you're interested in. The button is pressed (down) when the corresponding bit is set; otherwise, it's released (up).

The following example determines whether the **Next Gear** button is pressed.

```cpp
if (RacingWheelButtons::NextGear == (reading.Buttons & RacingWheelButtons::NextGear))
{
    // Next Gear is pressed
}
```

The following example determines whether the Next Gear button is released.

```cpp
if (RacingWheelButtons::None == (reading.Buttons & RacingWheelButtons::NextGear))
{
    // Next Gear is released (not pressed)
}
```

Sometimes you might want to determine when a button transitions from pressed to released or released to pressed, whether multiple buttons are pressed or released, or if a set of buttons are arranged in a particular way&mdash;some pressed, some not. For information on how to detect these conditions, see [Detecting button transitions](input-practices-for-games.md#detecting-button-transitions) and [Detecting complex button arrangements](input-practices-for-games.md#detecting-complex-button-arrangements).

### Reading the wheel

The steering wheel is a required control that provides an analog reading between -1.0 and +1.0. A value of -1.0 corresponds to the left-most wheel position; a value of +1.0 corresponds to the right-most position. The value of the steering wheel is read from the `Wheel` property of the [RacingWheelReading](/uwp/api/windows.gaming.input.racingwheelreading) structure.

```cpp
float wheel = reading.Wheel;  // returns a value between -1.0 and +1.0.
```

Although wheel readings correspond to different degrees of physical rotation in the actual wheel depending on the range of rotation supported by the physical racing wheel, you don't usually want to scale the wheel readings; wheels that support greater degrees of rotation just provide greater precision.

### Reading the throttle and brake

The throttle and brake are required controls that each provide analog readings between 0.0 (fully released) and 1.0 (fully pressed) represented as floating-point values. The value of the throttle control is read from the `Throttle` property of the [RacingWheelReading](/uwp/api/windows.gaming.input.racingwheelreading) struct; the value of the brake control is read from the `Brake` property.

```cpp
float throttle = reading.Throttle;  // returns a value between 0.0 and 1.0
float brake    = reading.Brake;     // returns a value between 0.0 and 1.0
```

### Reading the handbrake and clutch

The handbrake and clutch are optional controls that each provide analog readings between 0.0 (fully released) and 1.0 (fully engaged) represented as floating-point values. The value of the handbrake control is read from the `Handbrake` property of the [RacingWheelReading](/uwp/api/windows.gaming.input.racingwheelreading) struct; the value of the clutch control is read from the `Clutch` property.

```cpp
float handbrake = 0.0;
float clutch = 0.0;

if(racingwheel->HasHandbrake)
{
    handbrake = reading.Handbrake;  // returns a value between 0.0 and 1.0
}

if(racingwheel->HasClutch)
{
    clutch = reading.Clutch;        // returns a value between 0.0 and 1.0
}
```

### Reading the pattern shifter

The pattern shifter is an optional control that provides a digital reading between -1 and [MaxPatternShifterGear](/uwp/api/windows.gaming.input.racingwheel.maxpatternshiftergear) represented as a signed integer value. A value of -1 or 0 correspond to the _reverse_ and _neutral_ gears, respectively; increasingly positive values correspond to greater forward gears up to **MaxPatternShifterGear**, inclusive. The value of the pattern shifter is read from the [PatternShifterGear](/uwp/api/windows.gaming.input.racingwheelreading.patternshiftergear) property of the [RacingWheelReading](/uwp/api/windows.gaming.input.racingwheelreading) struct.

```cpp
if (racingwheel->HasPatternShifter)
{
    gear = reading.PatternShifterGear;
}
```

> [!NOTE]
> The pattern shifter, where supported, exists alongside the required **Previous Gear** and **Next Gear** buttons which also affect the current gear of the player's car. A simple strategy for unifying these inputs where both are present is to ignore the pattern shifter (and clutch) when a player chooses an automatic transmission for their car, and to ignore the **Previous** and **Next Gear** buttons when a player chooses a manual transmission for their car only if their racing wheel is equipped with a pattern shifter control. You can implement a different unification strategy if this isn't suitable for your game.

## Run the InputInterfacing sample

The [InputInterfacingUWP](https://github.com/microsoft/Xbox-ATG-Samples/tree/main/UWPSamples/System/InputInterfacingUWP) sample app on GitHub demonstrates how to use racing wheels and different kinds of input devices in tandem; as well as how these input devices behave as UI navigation controllers.

## Force feedback overview

Many racing wheels have force feedback capability to provide a more immersive and challenging driving experience. Racing wheels that support force feedback are typically equipped with a single motor that applies force to the steering wheel along a single axis, the axis of wheel rotation. Force feedback is supported in Windows 10 or Windows 11 and Xbox One UWP apps through the [Windows.Gaming.Input.ForceFeedback](/uwp/api/windows.gaming.input.forcefeedback) namespace.

> [!NOTE]
> The force feedback APIs are capable of supporting several axes of force, but no racing wheel currently supports any feedback axis other than that of wheel rotation.

## Using force feedback

These sections describe the basics of programming force feedback effects for racing wheels. Feedback is applied using effects, which are first loaded onto the force feedback device and then can be started, paused, resumed, and stopped in a manner similar to sound effects; however, you must first determine the feedback capabilities of the racing wheel.

### Determining force feedback capabilities

You can determine whether a connected racing wheel supports force feedback by reading the [WheelMotor](/uwp/api/windows.gaming.input.racingwheel.wheelmotor#Windows_Gaming_Input_RacingWheel_WheelMotor) property of the racing wheel. Force feedback isn't supported if `WheelMotor` is **null**; otherwise, force feedback is supported and you can proceed to determine the specific feedback capabilities of the motor, such as the axes it can affect.

```cpp
if (racingwheel->WheelMotor != nullptr)
{
    auto axes = racingwheel->WheelMotor->SupportedAxes;

    if(ForceFeedbackEffectAxes::X == (axes & ForceFeedbackEffectAxes::X))
    {
        // Force can be applied through the X axis
    }

    if(ForceFeedbackEffectAxes::Y == (axes & ForceFeedbackEffectAxes::Y))
    {
        // Force can be applied through the Y axis
    }

    if(ForceFeedbackEffectAxes::Z == (axes & ForceFeedbackEffectAxes::Z))
    {
        // Force can be applied through the Z axis
    }
}
```

### Loading force feedback effects

Force feedback effects are loaded onto the feedback device where they are "played" autonomously at the command of your game. A number of basic effects are provided; custom effects can be created through a class that implements the [IForceFeedbackEffect](/uwp/api/windows.gaming.input.forcefeedback.iforcefeedbackeffect) interface.

| Effect class         | Effect description                                                                     |
| -------------------- | -------------------------------------------------------------------------------------- |
| ConditionForceEffect | An effect that applies variable force in response to current sensor within the device. |
| ConstantForceEffect  | An effect that applies constant force along a vector.                                  |
| PeriodicForceEffect  | An effect that applies variable force defined by a waveform, along a vector.           |
| RampForceEffect      | An effect that applies a linearly increasing/decreasing force along a vector.          |

```cpp
using FFLoadEffectResult = ForceFeedback::ForceFeedbackLoadEffectResult;

auto effect = ref new Windows.Gaming::Input::ForceFeedback::ConstantForceEffect();
auto time = TimeSpan(10000);

effect->SetParameters(Windows::Foundation::Numerics::float3(1.0f, 0.0f, 0.0f), time);

// Here, we assume 'racingwheel' is valid and supports force feedback

IAsyncOperation<FFLoadEffectResult>^ request
    = racingwheel->WheelMotor->LoadEffectAsync(effect);

auto loadEffectTask = Concurrency::create_task(request);

loadEffectTask.then([this](FFLoadEffectResult result)
{
    if (FFLoadEffectResult::Succeeded == result)
    {
        // effect successfully loaded
    }
    else
    {
        // effect failed to load
    }
}).wait();
```

### Using force feedback effects

Once loaded, effects can all be started, paused, resumed, and stopped synchronously by calling functions on the `WheelMotor` property of the racing wheel, or individually by calling functions on the feedback effect itself. Typically, you should load all the effects that you want to use onto the feedback device before gameplay begins and then use their respective `SetParameters` functions to update the effects as gameplay progresses.

```cpp
if (ForceFeedbackEffectState::Running == effect->State)
{
    effect->Stop();
}
else
{
    effect->Start();
}
```

Finally, you can asynchronously enable, disable, or reset the entire force feedback system on a particular racing wheel whenever you need.

## See also

* [Windows.Gaming.Input.UINavigationController](/uwp/api/windows.gaming.input.uinavigationcontroller)
* [Windows.Gaming.Input.IGameController](/uwp/api/windows.gaming.input.igamecontroller)
* [Input practices for games](input-practices-for-games.md)

[Windows.Gaming.Input]: /uwp/api/Windows.Gaming.Input
[Windows.Gaming.Input.UINavigationController]: /uwp/api/Windows.Gaming.Input.UINavigationController
[Windows.Gaming.Input.IGameController]: /uwp/api/Windows.Gaming.Input.IGameController
[racingwheel]: /uwp/api/Windows.Gaming.Input.RacingWheel
[racingwheels]: /uwp/api/Windows.Gaming.Input.RacingWheel
[racingwheeladded]: /uwp/api/Windows.Gaming.Input.RacingWheel
[racingwheelremoved]: /uwp/api/Windows.Gaming.Input.RacingWheel
[haspatternshifter]: /uwp/api/Windows.Gaming.Input.RacingWheel
[hashandbrake]: /uwp/api/Windows.Gaming.Input.RacingWheel
[hasclutch]: /uwp/api/Windows.Gaming.Input.RacingWheel
[maxpatternshiftergear]: /uwp/api/Windows.Gaming.Input.RacingWheel
[maxwheelangle]: /uwp/api/Windows.Gaming.Input.RacingWheel
[getcurrentreading]: /uwp/api/Windows.Gaming.Input.RacingWheel
[wheelmotor]: /uwp/api/Windows.Gaming.Input.RacingWheel
[racingwheelreading]: /uwp/api/Windows.Gaming.Input.RacingWheelReading
[racingwheelbuttons]: /uwp/api/Windows.Gaming.Input.RacingWheelButtons