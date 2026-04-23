---
title: Gamepad and vibration
description: Use the Windows.Gaming.Input gamepad APIs to detect, read, and send vibration and impulse commands to gamepads.
ms.assetid: BB03BB8E-255F-4AE8-AC43-1E519CA860FE
ms.date: 09/06/2018
ms.topic: article
keywords: windows 10, uwp, games, gamepad, vibration
ms.localizationpriority: medium
---
# Gamepad and vibration

This page describes the basics of programming for game pads using [Windows.Gaming.Input.Gamepad][game pad] and related APIs for the Universal Windows Platform (UWP).

By reading this page, you'll learn:

* how to gather a list of connected game pads and their users
* how to detect that a game pad has been added or removed
* how to read input from one or more game pads
* how to send vibration and impulse commands
* how game pads behave as UI navigation devices

## Gamepad overview

Gamepads like the Xbox Wireless Controller and Xbox Wireless Controller S are general-purpose gaming input devices. They're the standard input device on Xbox One and a common choice for Windows gamers when they don't favor a keyboard and mouse. Gamepads are supported in Windows 10 or Windows 11 and Xbox UWP apps through the [Windows.Gaming.Input][] namespace.

Xbox One gamepads are equipped with a directional pad (or D-pad); **A**, **B**, **X**, **Y**, **View**, and **Menu** buttons; left and right thumbsticks, bumpers, and triggers; and a total of four vibration motors. Both thumbsticks provide dual analog readings in the X and Y axes, and also act as a button when pressed inward. Each trigger provides an analog reading that represents how far it's pulled back.

<!-- > [!NOTE]
> The Xbox Elite Wireless Controller is equipped with four additional **Paddle** buttons on its underside. These can be used to provide redundant access to game commands that are difficult to use together (such as the right thumbstick together with any of the **A**, **B**, **X**, or **Y** buttons) or to provide dedicated access to additional commands. -->

> [!NOTE]
> `Windows.Gaming.Input.Gamepad` also supports Xbox 360 gamepads, which have the same control layout as standard Xbox One gamepads.

### Vibration and impulse triggers

Xbox One gamepads provide two independent motors for strong and subtle gamepad vibration as well as two dedicated motors for providing sharp vibration to each trigger (this unique feature is the reason that Xbox One gamepad triggers are referred to as _impulse triggers_).

> [!NOTE]
> Xbox 360 gamepads are not equipped with _impulse triggers_.

For more information, see [Vibration and impulse triggers overview](#vibration-and-impulse-triggers-overview).

### Thumbstick deadzones

A thumbstick at rest in the center position would ideally produce the same, neutral reading in the X and Y axes every time. However, due to mechanical forces and the sensitivity of the thumbstick, actual readings in the center position only approximate the ideal neutral value and can vary between subsequent readings. For this reason, you must always use a small _deadzone_&mdash;a range of values near the ideal center position that are ignored&mdash;to compensate for manufacturing differences, mechanical wear, or other gamepad issues.

Larger deadzones offer a simple strategy for separating intentional input from unintentional input.

For more information, see [Reading the thumbsticks](#reading-the-thumbsticks).

### UI navigation

In order to ease the burden of supporting the different input devices for user interface navigation and to encourage consistency between games and devices, most _physical_ input devices simultaneously act as a separate _logical_ input device called a [UI navigation controller](ui-navigation-controller.md). The UI navigation controller provides a common vocabulary for UI navigation commands across input devices.

As a UI navigation controller, gamepads map the [required set](ui-navigation-controller.md#required-set) of navigation commands to the left thumbstick, D-pad, **View**, **Menu**, **A**, and **B** buttons.

| Navigation command | Gamepad input                       |
| ------------------:| ----------------------------------- |
|                 Up | Left thumbstick up / D-pad up       |
|               Down | Left thumbstick down / D-pad down   |
|               Left | Left thumbstick left / D-pad left   |
|              Right | Left thumbstick right / D-pad right |
|               View | View button                         |
|               Menu | Menu button                         |
|             Accept | A button                            |
|             Cancel | B button                            |

Additionally, gamepads map all of the [optional set](ui-navigation-controller.md#optional-set) of navigation commands to the remaining inputs.

| Navigation command | Gamepad input          |
| ------------------:| ---------------------- |
|            Page Up | Left trigger           |
|          Page Down | Right trigger          |
|          Page Left | Left bumper            |
|         Page Right | Right bumper           |
|          Scroll Up | Right thumbstick up    |
|        Scroll Down | Right thumbstick down  |
|        Scroll Left | Right thumbstick left  |
|       Scroll Right | Right thumbstick right |
|          Context 1 | X Button               |
|          Context 2 | Y Button               |
|          Context 3 | Left thumbstick press  |
|          Context 4 | Right thumbstick press |

## Detect and track gamepads

Gamepads are managed by the system, therefore you don't have to create or initialize them. The system provides a list of connected gamepads and events to notify you when a gamepad is added or removed.

### The gamepads list

The [Gamepad][] class provides a static property, [Gamepads][], which is a read-only list of gamepads that are currently connected. Because you might only be interested in some of the connected gamepads, it's recommended that you maintain your own collection instead of accessing them through the `Gamepads` property.

The following example copies all connected gamepads into a new collection. Note that because other threads in the background will be accessing this collection (in the [GamepadAdded][] and [GamepadRemoved][] events), you need to place a lock around any code that reads or updates the collection.

```cpp
auto myGamepads = ref new Vector<Gamepad^>();
critical_section myLock{};

for (auto gamepad : Gamepad::Gamepads)
{
    // Check if the gamepad is already in myGamepads; if it isn't, add it.
    critical_section::scoped_lock lock{ myLock };
    auto it = std::find(begin(myGamepads), end(myGamepads), gamepad);

    if (it == end(myGamepads))
    {
        // This code assumes that you're interested in all gamepads.
        myGamepads->Append(gamepad);
    }
}
```

```cs
private readonly object myLock = new object();
private List<Gamepad> myGamepads = new List<Gamepad>();
private Gamepad mainGamepad;

private void GetGamepads()
{
    lock (myLock)
    {
        foreach (var gamepad in Gamepad.Gamepads)
        {
            // Check if the gamepad is already in myGamepads; if it isn't, add it.
            bool gamepadInList = myGamepads.Contains(gamepad);

            if (!gamepadInList)
            {
                // This code assumes that you're interested in all gamepads.
                myGamepads.Add(gamepad);
            }
        }
    }   
}
```

### Adding and removing gamepads

When a gamepad is added or removed, the [GamepadAdded][] and [GamepadRemoved][] events are raised. You can register handlers for these events to keep track of the gamepads that are currently connected.

The following example starts tracking a gamepad that's been added.

```cpp
Gamepad::GamepadAdded += ref new EventHandler<Gamepad^>(Platform::Object^, Gamepad^ args)
{
    // Check if the just-added gamepad is already in myGamepads; if it isn't, add
    // it.
    critical_section::scoped_lock lock{ myLock };
    auto it = std::find(begin(myGamepads), end(myGamepads), args);

    if (it == end(myGamepads))
    {
        // This code assumes that you're interested in all new gamepads.
        myGamepads->Append(args);
    }
}
```

```cs
Gamepad.GamepadAdded += (object sender, Gamepad e) =>
{
    // Check if the just-added gamepad is already in myGamepads; if it isn't, add
    // it.
    lock (myLock)
    {
        bool gamepadInList = myGamepads.Contains(e);

        if (!gamepadInList)
        {
            myGamepads.Add(e);
        }
    }
};
```

The following example stops tracking a gamepad that's been removed. You'll also need to handle what happens to the gamepads that you're tracking when they're removed; for example, this code only tracks input from one gamepad, and simply sets it to `nullptr` when it's removed. You'll need to check every frame if your gamepad is active, and update which gamepad you're gathering input from when controllers are connected and disconnected.

```cpp
Gamepad::GamepadRemoved += ref new EventHandler<Gamepad^>(Platform::Object^, Gamepad^ args)
{
    unsigned int indexRemoved;
    critical_section::scoped_lock lock{ myLock };

    if(myGamepads->IndexOf(args, &indexRemoved))
    {
        if (m_gamepad == myGamepads->GetAt(indexRemoved))
        {
            m_gamepad = nullptr;
        }

        myGamepads->RemoveAt(indexRemoved);
    }
}
```

```cs
Gamepad.GamepadRemoved += (object sender, Gamepad e) =>
{
    lock (myLock)
    {
        int indexRemoved = myGamepads.IndexOf(e);

        if (indexRemoved > -1)
        {
            if (mainGamepad == myGamepads[indexRemoved])
            {
                mainGamepad = null;
            }

            myGamepads.RemoveAt(indexRemoved);
        }
    }
};
```

See [Input practices for games](input-practices-for-games.md) for more information.

### Users and headsets

Each gamepad can be associated with a user account to link their identity to their gameplay, and can have a headset attached to facilitate voice chat or in-game features. To learn more about working with users and headsets, see [Tracking users and their devices](input-practices-for-games.md#tracking-users-and-their-devices) and [Headset](headset.md).

## Reading the gamepad

After you identify the gamepad that you're interested in, you're ready to gather input from it. However, unlike some other kinds of input that you might be used to, gamepads don't communicate state-change by raising events. Instead, you take regular readings of their current state by _polling_ them.

### Polling the gamepad

Polling captures a snapshot of the navigation device at a precise point in time. This approach to input gathering is a good fit for most games because their logic typically runs in a deterministic loop rather than being event-driven; it's also typically simpler to interpret game commands from input gathered all at once than it is from many single inputs gathered over time.

You poll a gamepad by calling [GetCurrentReading][]; this function returns a [GamepadReading][] that contains the state of the gamepad.

The following example polls a gamepad for its current state.

```cpp
auto gamepad = myGamepads[0];

GamepadReading reading = gamepad->GetCurrentReading();
```

```cs
Gamepad gamepad = myGamepads[0];

GamepadReading reading = gamepad.GetCurrentReading();
```

In addition to the gamepad state, each reading includes a timestamp that indicates precisely when the state was retrieved. The timestamp is useful for relating to the timing of previous readings or to the timing of the game simulation.

### Reading the thumbsticks

Each thumbstick provides an analog reading between -1.0 and +1.0 in the X and Y axes. In the X axis, a value of -1.0 corresponds to the left-most thumbstick position; a value of +1.0 corresponds to right-most position. In the Y axis, a value of -1.0 corresponds to the bottom-most thumbstick position; a value of +1.0 corresponds to the top-most position. In both axes, the value is approximately 0.0 when the stick is in the center position, but it's normal for the precise value to vary, even between subsequent readings; strategies for mitigating this variation are discussed later in this section.

The value of the left thumbstick's X axis is read from the `LeftThumbstickX` property of the [GamepadReading][] structure; the value of the Y axis is read from the `LeftThumbstickY` property. The value of the right thumbstick's X axis is read from the `RightThumbstickX` property; the value of the Y axis is read from the `RightThumbstickY` property.

```cpp
float leftStickX = reading.LeftThumbstickX;   // returns a value between -1.0 and +1.0
float leftStickY = reading.LeftThumbstickY;   // returns a value between -1.0 and +1.0
float rightStickX = reading.RightThumbstickX; // returns a value between -1.0 and +1.0
float rightStickY = reading.RightThumbstickY; // returns a value between -1.0 and +1.0
```

```cs
double leftStickX = reading.LeftThumbstickX;   // returns a value between -1.0 and +1.0
double leftStickY = reading.LeftThumbstickY;   // returns a value between -1.0 and +1.0
double rightStickX = reading.RightThumbstickX; // returns a value between -1.0 and +1.0
double rightStickY = reading.RightThumbstickY; // returns a value between -1.0 and +1.0
```

When reading the thumbstick values, you'll notice that they don't reliably produce a neutral reading of 0.0 when the thumbstick is at rest in the center position; instead, they'll produce different values near 0.0 each time the thumbstick is moved and returned to the center position. To mitigate these variations, you can implement a small _deadzone_, which is a range of values near the ideal center position that are ignored. One way to implement a deadzone is to determine how far from center the thumbstick has moved, and ignoring the readings that are nearer than some distance you choose. You can compute the distance roughly&mdash;it's not exact because thumbstick readings are essentially polar, not planar, values&mdash;just by using the Pythagorean theorem. This produces a radial deadzone.

The following example demonstrates a basic radial deadzone using the Pythagorean theorem.

```cpp
float leftStickX = reading.LeftThumbstickX;   // returns a value between -1.0 and +1.0
float leftStickY = reading.LeftThumbstickY;   // returns a value between -1.0 and +1.0

// choose a deadzone -- readings inside this radius are ignored.
const float deadzoneRadius = 0.1;
const float deadzoneSquared = deadzoneRadius * deadzoneRadius;

// Pythagorean theorem -- for a right triangle, hypotenuse^2 = (opposite side)^2 + (adjacent side)^2
auto oppositeSquared = leftStickY * leftStickY;
auto adjacentSquared = leftStickX * leftStickX;

// accept and process input if true; otherwise, reject and ignore it.
if ((oppositeSquared + adjacentSquared) > deadzoneSquared)
{
    // input accepted, process it
}
```

```cs
double leftStickX = reading.LeftThumbstickX;   // returns a value between -1.0 and +1.0
double leftStickY = reading.LeftThumbstickY;   // returns a value between -1.0 and +1.0

// choose a deadzone -- readings inside this radius are ignored.
const double deadzoneRadius = 0.1;
const double deadzoneSquared = deadzoneRadius * deadzoneRadius;

// Pythagorean theorem -- for a right triangle, hypotenuse^2 = (opposite side)^2 + (adjacent side)^2
double oppositeSquared = leftStickY * leftStickY;
double adjacentSquared = leftStickX * leftStickX;

// accept and process input if true; otherwise, reject and ignore it.
if ((oppositeSquared + adjacentSquared) > deadzoneSquared)
{
    // input accepted, process it
}
```

Each thumbstick also acts as a button when pressed inward; for more information on reading this input, see [Reading the buttons](#reading-the-buttons).

### Reading the triggers

The triggers are represented as floating-point values between 0.0 (fully released) and 1.0 (fully depressed). The value of the left trigger is read from the `LeftTrigger` property of the [GamepadReading][] structure; the value of the right trigger is read from the `RightTrigger` property.

```cpp
float leftTrigger  = reading.LeftTrigger;  // returns a value between 0.0 and 1.0
float rightTrigger = reading.RightTrigger; // returns a value between 0.0 and 1.0
```

```cs
double leftTrigger = reading.LeftTrigger;  // returns a value between 0.0 and 1.0
double rightTrigger = reading.RightTrigger; // returns a value between 0.0 and 1.0
```

### Reading the buttons

Each of the gamepad buttons&mdash;the four directions of the D-pad, left and right bumpers, left and right thumbstick press, **A**, **B**, **X**, **Y**, **View**, and **Menu**&mdash;provides a digital reading that indicates whether it's pressed (down) or released (up). For efficiency, button readings aren't represented as individual boolean values; instead, they're all packed into a single bitfield that's represented by the [GamepadButtons][] enumeration.

<!-- > [!NOTE]
> The Xbox Elite Wireless Controller is equipped with four additional **paddle** buttons on its underside. These buttons are also represented in the `GamepadButtons` enumeration and their values are read in the same way as the standard gamepad buttons. -->

The button values are read from the `Buttons` property of the [GamepadReading][] structure. Because this property is a bitfield, bitwise masking is used to isolate the value of the button that you're interested in. The button is pressed (down) when the corresponding bit is set; otherwise, it's released (up).

The following example determines whether the A button is pressed.

```cpp
if (GamepadButtons::A == (reading.Buttons & GamepadButtons::A))
{
    // button A is pressed
}
```

```cs
if (GamepadButtons.A == (reading.Buttons & GamepadButtons.A))
{
    // button A is pressed
}
```

The following example determines whether the A button is released.

```cpp
if (GamepadButtons::None == (reading.Buttons & GamepadButtons::A))
{
    // button A is released
}
```

```cs
if (GamepadButtons.None == (reading.Buttons & GamepadButtons.A))
{
    // button A is released
}
```

Sometimes you might want to determine when a button transitions from pressed to released or released to pressed, whether multiple buttons are pressed or released, or if a set of buttons is arranged in a particular way&mdash;some pressed, some not. For information on how to detect each of these conditions, see [Detecting button transitions](input-practices-for-games.md#detecting-button-transitions) and [Detecting complex button arrangements](input-practices-for-games.md#detecting-complex-button-arrangements).

## Run the gamepad input sample

The [GamepadUWP sample _(github)_](https://github.com/Microsoft/Xbox-ATG-Samples/tree/master/UWPSamples/System/GamepadUWP) demonstrates how to connect to a gamepad and read its state.

## Vibration and impulse triggers overview

The vibration motors inside a gamepad are for providing tactile feedback to the user. Games use this ability to create a greater sense of immersion, to help communicate status information (such as taking damage), to signal proximity to important objects, or for other creative uses.

Xbox One gamepads are equipped with a total of four independent vibration motors. Two are large motors located in the gamepad body; the left motor provides rough, high-amplitude vibration, while the right motor provides gentler, more subtle vibration. The other two are small motors, one inside each trigger, that provide sharp bursts of vibration directly to the user's trigger fingers; this unique ability of the Xbox One gamepad is the reason its triggers are referred to as _impulse triggers_. By orchestrating these motors together, a wide range of tactile sensations can be produced.

## Using vibration and impulse

Gamepad vibration is controlled through the [Vibration][] property of the [Gamepad][] class. `Vibration` is an instance of the [GamepadVibration][] structure which is made up of four floating point values; each value represents the intensity of one of the motors.

Although the members of the `Gamepad.Vibration` property can be modified directly, it's recommended that you initialize a separate `GamepadVibration` instance to the values you want, and then copy it into the `Gamepad.Vibration` property to change the actual motor intensities all at once.

The following example demonstrates how to change the motor intensities all at once.

```cpp
// get the first gamepad
Gamepad^ gamepad = Gamepad::Gamepads->GetAt(0);

// create an instance of GamepadVibration
GamepadVibration vibration;

// ... set vibration levels on vibration struct here

// copy the GamepadVibration struct to the gamepad
gamepad.Vibration = vibration;
```

```cs
// get the first gamepad
Gamepad gamepad = Gamepad.Gamepads[0];

// create an instance of GamepadVibration
GamepadVibration vibration = new GamepadVibration();

// ... set vibration levels on vibration struct here

// copy the GamepadVibration struct to the gamepad
gamepad.Vibration = vibration;
```

### Using the vibration motors

The left and right vibration motors take floating point values between 0.0 (no vibration) and 1.0 (most intense vibration). The intensity of the left motor is set by the `LeftMotor` property of the [GamepadVibration][] structure; the intensity of the right motor is set by the `RightMotor` property.

The following example sets the intensity of both vibration motors and activates gamepad vibration.

```cpp
GamepadVibration vibration;
vibration.LeftMotor = 0.80;  // sets the intensity of the left motor to 80%
vibration.RightMotor = 0.25; // sets the intensity of the right motor to 25%
gamepad.Vibration = vibration;
```

```cs
GamepadVibration vibration = new GamepadVibration();
vibration.LeftMotor = 0.80;  // sets the intensity of the left motor to 80%
vibration.RightMotor = 0.25; // sets the intensity of the right motor to 25%
mainGamepad.Vibration = vibration;
```

Remember that these two motors are not identical so setting these properties to the same value doesn't produce the same vibration in one motor as in the other. For any value, the left motor produces a stronger vibration at a lower frequency than the right motor which&mdash;for the same value&mdash;produces a gentler vibration at a higher frequency. Even at the maximum value, the left motor can't produce the high frequencies of the right motor, nor can the right motor produce the high forces of the left motor. Still, because the motors are rigidly connected by the gamepad body, players don't experience the vibrations fully independently even though the motors have different characteristics and can vibrate with different intensities. This arrangement allows for a wider, more expressive range of sensations to be produced than if the motors were identical.

### Using the impulse triggers

Each impulse trigger motor takes a floating point value between 0.0 (no vibration) and 1.0 (most intense vibration). The intensity of the left trigger motor is set by the `LeftTrigger` property of the [GamepadVibration][] structure; the intensity of the right trigger is set by the `RightTrigger` property.

The following example sets intensity of both impulse triggers and activates them.

```cpp
GamepadVibration vibration;
vibration.LeftTrigger = 0.75;  // sets the intensity of the left trigger to 75%
vibration.RightTrigger = 0.50; // sets the intensity of the right trigger to 50%
gamepad.Vibration = vibration;
```

```cs
GamepadVibration vibration = new GamepadVibration();
vibration.LeftTrigger = 0.75;  // sets the intensity of the left trigger to 75%
vibration.RightTrigger = 0.50; // sets the intensity of the right trigger to 50%
mainGamepad.Vibration = vibration;
```

Unlike the others, the two vibration motors inside the triggers are identical so they produce the same vibration in either motor for the same value. However, because these motors are not rigidly connected in any way, players experience the vibrations independently. This arrangement allows for fully independent sensations to be directed to both triggers simultaneously, and helps them to convey more specific information than the motors in the gamepad body can.

## Run the gamepad vibration sample

The [GamepadVibrationUWP sample _(github)_](https://github.com/Microsoft/Xbox-ATG-Samples/tree/master/UWPSamples/System/GamepadVibrationUWP) demonstrates how the gamepad vibration motors and impulse triggers are used to produce a variety of effects.

## See also

* [Windows.Gaming.Input.UINavigationController][]
* [Windows.Gaming.Input.IGameController][]
* [Input practices for games](input-practices-for-games.md)

[Windows.Gaming.Input]: /uwp/api/Windows.Gaming.Input
[Windows.Gaming.Input.UINavigationController]: /uwp/api/Windows.Gaming.Input.UINavigationController
[Windows.Gaming.Input.IGameController]: /uwp/api/Windows.Gaming.Input.IGameController
[gamepad]: /uwp/api/Windows.Gaming.Input.Gamepad
[gamepads]: /uwp/api/Windows.Gaming.Input.Gamepad
[gamepadadded]: /uwp/api/Windows.Gaming.Input.Gamepad
[gamepadremoved]: /uwp/api/Windows.Gaming.Input.Gamepad
[getcurrentreading]: /uwp/api/Windows.Gaming.Input.Gamepad
[vibration]: /uwp/api/Windows.Gaming.Input.Gamepad
[gamepadreading]: /uwp/api/Windows.Gaming.Input.GamepadReading
[gamepadbuttons]: /uwp/api/Windows.Gaming.Input.GamepadButtons
[gamepadvibration]: /uwp/api/Windows.Gaming.Input.GamepadVibration