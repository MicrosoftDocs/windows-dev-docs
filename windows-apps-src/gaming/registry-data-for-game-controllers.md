---
title: Registry data for game controllers
description: Learn about the data that you can add to the PC's registry to enable your controller to be used in UWP games.
ms.assetid: 2DD0B384-8776-4599-9E52-4FC0AA682735
ms.date: 04/08/2019
ms.topic: article
keywords: windows 10, uwp, games, input, registry, custom
ms.localizationpriority: medium
---

# Registry data for game controllers

> [!NOTE]
> This topic is meant for manufacturers of Windows 10-compatible game controllers, and doesn't apply to the majority of developers.

The [Windows.Gaming.Input namespace](/uwp/api/windows.gaming.input) allows independent hardware vendors (IHVs) to add data to the PC's registry, enabling their devices to appear as [Gamepads](/uwp/api/windows.gaming.input.gamepad), [RacingWheels](/uwp/api/windows.gaming.input.racingwheel), [ArcadeSticks](/uwp/api/windows.gaming.input.arcadestick), [FlightSticks](/uwp/api/windows.gaming.input.flightstick), and [UINavigationControllers](/uwp/api/windows.gaming.input.uinavigationcontroller) as appropriate. All IHVs should add this data for their compatible controllers. By doing this, all UWP games (and any desktop games that use the WinRT API) will be able to support your game controller.

## Mapping scheme

Mappings for a device with Vendor ID (VID) **VVVV**, Product ID (PID) **PPPP**, Usage Page **UUUU**, and Usage ID **XXXX**, will be read out from this location in the registry:

`HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\VVVVPPPPUUUUXXXX`

The table below explains the expected values under the device root location:

<table>
    <tr>
        <th>Name</th>
        <th>Type</th>
        <th>Required?</th>
        <th>Info</th>
    </tr>
    <tr>
        <td>Disabled</td>
        <td>DWORD</td>
        <td>No</td>
        <td>
            <p>Indicates that this particular device should be disabled.</p>
            <ul>
                <li><b>0</b>: Device is not disabled.</li>
                <li><b>1</b>: Device is disabled.</li>
            </ul>
        </td>
    </tr>
    <tr>
        <td>Description</td>
        <td>REG_SZ
        <td>No</td>
        <td>A short description of the device.</td>
    </tr>
</table>

Your device installer should add this data to the registry (either via setup or an [INF file](https://docs.microsoft.com/windows-hardware/drivers/install/inf-files)).

Subkeys under the device root location are detailed in the following sections.

### Gamepad

The table below lists the required and optional subkeys under the **Gamepad** subkey:

<table>
    <tr>
        <th>Subkey</th>
        <th>Required?</th>
        <th>Info</th>
    </tr>
    <tr>
        <td>Menu</td>
        <td>Yes</td>
        <td rowspan="18" style="vertical-align: middle;">See <a href="#button-mapping">Button mapping</a></td>
    </tr>
    <tr>
        <td>View</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>A</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>B</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>X</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>Y</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>LeftShoulder</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>RightShoulder</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>LeftThumbstickButton</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>RightThumbstickButton</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>DPadUp</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>DPadDown</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>DPadLeft</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>DPadRight</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>Paddle1</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Paddle2</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Paddle3</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Paddle4</td>
        <td>No</td>
    </tr>
    <tr>
        <td>LeftTrigger</td>
        <td>Yes</td>
        <td rowspan="6" style="vertical-align: middle;">See <a href="#axis-mapping">Axis mapping</a></td>
    </tr>
    <tr>
        <td>RightTrigger</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>LeftThumbstickX</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>LeftThumbstickY</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>RightThumbstickX</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>RightThumbstickY</td>
        <td>Yes</td>
    </tr>
</table>

> [!NOTE]
> If you add your game controller as a supported **Gamepad**, we highly recommend that you also add it as a supported **UINavigationController**.

### RacingWheel

The table below lists the required and optional subkeys under the **RacingWheel** subkey:

<table>
    <tr>
        <th>Subkey</th>
        <th>Required?</th>
        <th>Info</th>
    </tr>
    <tr>
        <td>PreviousGear</td>
        <td>Yes</td>
        <td rowspan="30" style="vertical-align: middle;">See <a href="#button-mapping">Button mapping</a></td>
    </tr>
    <tr>
        <td>NextGear</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>DPadUp</td>
        <td>No</td>
    </tr>
    <tr>
        <td>DPadDown</td>
        <td>No</td>
    </tr>
    <tr>
        <td>DPadLeft</td>
        <td>No</td>
    </tr>
    <tr>
        <td>DPadRight</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Button1</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Button2</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Button3</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Button4</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Button5</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Button6</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Button7</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Button8</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Button9</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Button10</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Button11</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Button12</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Button13</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Button14</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Button15</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Button16</td>
        <td>No</td>
    </tr>
    <tr>
        <td>FirstGear</td>
        <td>No</td>
    </tr>
    <tr>
        <td>SecondGear</td>
        <td>No</td>
    </tr>
    <tr>
        <td>ThirdGear</td>
        <td>No</td>
    </tr>
    <tr>
        <td>FourthGear</td>
        <td>No</td>
    </tr>
    <tr>
        <td>FifthGear</td>
        <td>No</td>
    </tr>
    <tr>
        <td>SixthGear</td>
        <td>No</td>
    </tr>
    <tr>
        <td>SeventhGear</td>
        <td>No</td>
    </tr>
    <tr>
        <td>ReverseGear</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Wheel</td>
        <td>Yes</td>
        <td rowspan="5" style="vertical-align: middle;">See <a href="#axis-mapping">Axis mapping</a></td>
    </tr>
    <tr>
        <td>Throttle</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>Brake</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>Clutch</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Handbrake</td>
        <td>No</td>
    </tr>
    <tr>
        <td>MaxWheelAngle</td>
        <td>Yes</td>
        <td>See <a href="#properties-mapping">Properties mapping</a></td>
    </tr>
</table>

### ArcadeStick

The table below lists the required and optional subkeys under the **ArcadeStick** subkey:

<table>
    <tr>
        <th>Subkey</th>
        <th>Required?</th>
        <th>Info</th>
    </tr>
    <tr>
        <td>Action 1</td>
        <td>Yes</td>
        <td rowspan="12" style="vertical-align: middle;">See <a href="#button-mapping">Button mapping</a></td>
    </tr>
    <tr>
        <td>Action2</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>Action3</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>Action4</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>Action5</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>Action6</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>Special1</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>Special2</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>StickUp</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>StickDown</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>StickLeft</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>StickRight</td>
        <td>Yes</td>
    </tr>
</table>

### FlightStick

The table below lists the required and optional subkeys under the **FlightStick** subkey:

<table>
    <tr>
        <th>Subkey</th>
        <th>Required?</th>
        <th>Info</th>
    </tr>
    <tr>
        <td>FirePrimary</td>
        <td>Yes</td>
        <td rowspan="2" style="vertical-align: middle;">See <a href="#button-mapping">Button mapping</a></td>
    </tr>
    <tr>
        <td>FireSecondary</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>Roll</td>
        <td>Yes</td>
        <td rowspan="4" style="vertical-align: middle;">See <a href="#axis-mapping">Axis mapping</a></td>
    </tr>
    <tr>
        <td>Pitch</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>Yaw</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>Throttle</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>HatSwitch</td>
        <td>Yes</td>
        <td>See <a href="#switch-mapping">Switch mapping</a></td>
    </tr>
</table>

### UINavigation

The table below lists the required and optional subkeys under **UINavigation** subkey:

<table>
    <tr>
        <th>Subkey</th>
        <th>Required?</th>
        <th>Info</th>
    </tr>
    <tr>
        <td>Menu</td>
        <td>Yes</td>
        <td rowspan="24" style="vertical-align: middle;">See <a href="#button-mapping">Button mapping</a></td>
    </tr>
    <tr>
        <td>View</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>Accept</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>Cancel</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>PrimaryUp</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>PrimaryDown</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>PrimaryLeft</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>PrimaryRight</td>
        <td>Yes</td>
    </tr>
    <tr>
        <td>Context1</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Context2</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Context3</td>
        <td>No</td>
    </tr>
    <tr>
        <td>Context4</td>
        <td>No</td>
    </tr>
    <tr>
        <td>PageUp</td>
        <td>No</td>
    </tr>
    <tr>
        <td>PageDown</td>
        <td>No</td>
    </tr>
    <tr>
        <td>PageLeft</td>
        <td>No</td>
    </tr>
    <tr>
        <td>PageRight</td>
        <td>No</td>
    </tr>
    <tr>
        <td>ScrollUp</td>
        <td>No</td>
    </tr>
    <tr>
        <td>ScrollDown</td>
        <td>No</td>
    </tr>
    <tr>
        <td>ScrollLeft</td>
        <td>No</td>
    </tr>
    <tr>
        <td>ScrollRight</td>
        <td>No</td>
    </tr>
    <tr>
        <td>SecondaryUp</td>
        <td>No</td>
    </tr>
    <tr>
        <td>SecondaryDown</td>
        <td>No</td>
    </tr>
    <tr>
        <td>SecondaryLeft</td>
        <td>No</td>
    </tr>
    <tr>
        <td>SecondaryRight</td>
        <td>No</td>
    </tr>
</table>

For more information about UI navigation controllers and the above commands, see [UI navigation controller](https://docs.microsoft.com/windows/uwp/gaming/ui-navigation-controller).

## Keys

The following sections explain the contents of each of the subkeys under the **Gamepad**, **RacingWheel**, **ArcadeStick**, **FlightStick**, and **UINavigation** keys.

### Button mapping

The table below lists the values that are needed to map a button. For example, if pressing **DPadUp** on the game controller, the mapping for **DPadUp** should contain the **ButtonIndex** value (**Source** is **Button**). If **DPadUp** needs to be mapped from a switch position, then the **DPadUp** mapping should contain the values **SwitchIndex** and **SwitchPosition** (**Source** is **Switch**).

<table>
    <tr>
        <th>Source</th>
        <th>Value name</th>
        <th>Value type</th>
        <th>Required?</th>
        <th>Value info</th>
    </tr>
    <tr>
        <td>Button</td>
        <td>ButtonIndex</td>
        <td>DWORD</td>
        <td>Yes</td>
        <td>Index in the <b>RawGameController</b> button array.</td>
    </tr>
    <tr>
        <td rowspan="4" style="vertical-align: middle;">Axis</td>
        <td>AxisIndex</td>
        <td>DWORD</td>
        <td>Yes</td>
        <td>Index in the <b>RawGameController</b> axis array.</td>
    </tr>
    <tr>
        <td>Invert</td>
        <td>DWORD</td>
        <td>No</td>
        <td>Indicates that the axis value should be inverted before the <b>Threshold Percent</b> and <b>DebouncePercent</b> factors are applied.</td>
    </tr>
    <tr>
        <td>ThresholdPercent</td>
        <td>DWORD</td>
        <td>Yes</td>
        <td>Indicates the axis position at which the mapped button value transitions between the pressed and released states. The valid range of values is 0 to 100. The button is considered pressed if the axis value is greater than or equal to this value.</td>
    </tr>
    <tr>
        <td>DebouncePercent</td>
        <td>DWORD</td>
        <td>Yes</td>
        <td>
            <p>Defines the size of a window around the <b>ThresholdPercent</b> value, which is used to debounce the reported button state. The valid range of values is 0 to 100. Button state transitions can only occur when the axis value crosses the upper or lower boundaries of the debounce window. For example, a <b>ThresholdPercent</b> of 50 and <b>DebouncePercent</b> of 10 results in debounce boundaries at 45% and 55% of the full-range axis values. The button can't transition to the pressed state until the axis value reaches 55% or above, and it can't transition back to the released state until the axis value reaches 45% or below.</p>
            <p>The computed debounce window boundaries are clamped between 0% and 100%. For example, a threshold of 5% and a debounce window of 20% would result in the debounce window boundaries falling at 0% and 15%. The button state for axis values of 0% and 100% are always reported as released and pressed, respectively, regardless of the threshold and debounce values.</p>
        </td>
    </tr>
    <tr>
        <td rowspan="3" style="vertical-align: middle;">Switch</td>
        <td>SwitchIndex</td>
        <td>DWORD</td>
        <td>Yes</td>
        <td>Index in the <b>RawGameController</b> switch array.</td>
    </tr>
    <tr>
        <td>SwitchPosition</td>
        <td>REG_SZ</td>
        <td>Yes</td>
        <td>
            <p>Indicates the switch position that will cause the mapped button to report that it's being pressed. The position values can be one of these strings:</p>
            <ul>
                <li>Up</li>
                <li>UpRight</li>
                <li>Right</li>
                <li>DownRight</li>
                <li>Down</li>
                <li>DownLeft</li>
                <li>Left</li>
                <li>UpLeft</li>
            </ul>
        </td>
    </tr>
    <tr>
        <td>IncludeAdjacent</td>
        <td>DWORD</td>
        <td>No</td>
        <td>Indicates that adjacent switch positions will also cause the mapped button to report that it's being pressed.</td>
    </tr>
</table>

### Axis mapping

The table below lists the values that are needed to map an axis:

<table>
    <tr>
        <th>Source</th>
        <th>Value name</th>
        <th>Value type</th>
        <th>Required?</th>
        <th>Value info</th>
    </tr>
    <tr>
        <td rowspan="2" style="vertical-align: middle;">Button</td>
        <td>MaxValueButtonIndex</td>
        <td>DWORD</td>
        <td>Yes</td>
        <td>
            <p>Index in the <b>RawGameController</b> button array which gets translated to the mapped unidirectional axis value.</p>
            <table>
                <tr>
                    <th>MaxButton</th>
                    <th>AxisValue</th>
                </tr>
                <tr>
                    <td>FALSE</td>
                    <td>0.0</td>
                </tr>
                <tr>
                    <td>TRUE</td>
                    <td>1.0</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>MinValueButtonIndex</td>
        <td>DWORD</td>
        <td>No</td>
        <td>
            <p>Indicates that the mapped axis is bidirectional. Values of <b>MaxButton</b> and <b>MinButton</b> are combined into a single bidirectional axis as shown below.</p>
            <table>
                <tr>
                    <th>MinButton</th>
                    <th>MaxButton</th>
                    <th>AxisValue</th>
                </tr>
                <tr>
                    <td>FALSE</td>
                    <td>FALSE</td>
                    <td>0.5</td>
                </tr>
                <tr>
                    <td>FALSE</td>
                    <td>TRUE</td>
                    <td>1.0</td>
                </tr>
                <tr>
                    <td>TRUE</td>
                    <td>FALSE</td>
                    <td>0.0</td>
                </tr>
                <tr>
                    <td>TRUE</td>
                    <td>TRUE</td>
                    <td>0.5</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td rowspan="2" style="vertical-align: middle;">Axis</td>
        <td>AxisIndex</td>
        <td>DWORD</td>
        <td>Yes</td>
        <td>Index in the <b>RawGameController</b> axis array.</td>
    </tr>
    <tr>
        <td>Invert</td>
        <td>DWORD</td>
        <td>No</td>
        <td>Indicates that the mapped axis value should be inverted before it's returned.</td>
    </tr>
    <tr>
        <td rowspan="3" style="vertical-align: middle;">Switch</td>
        <td>SwitchIndex</td>
        <td>DWORD</td>
        <td>Yes</td>
        <td>Index in the <b>RawGameController</b> switch array.
    </tr>
    <tr>
        <td>MaxValueSwitchPosition</td>
        <td>REG_SZ</td>
        <td>Yes</td>
        <td>
            <p>One of the following strings:</p>
            <ul>
                <li>Up</li>
                <li>UpRight</li>
                <li>Right</li>
                <li>DownRight</li>
                <li>Down</li>
                <li>DownLeft</li>
                <li>Left</li>
                <li>UpLeft</li>
            </ul>
            <p>It indicates the position of the switch that causes the mapped axis value to be reported as 1.0. The opposing direction of <b>MaxValueSwitchPosition</b> is treated as 0.0. For example, if <b>MaxValueSwitchPosition</b> is <b>Up</b>, the axis value translation is shown below:</p>
            <table>
                <tr>
                    <th>Switch position</th>
                    <th>AxisValue</th>
                </tr>
                <tr>
                    <td>Up</td>
                    <td>1.0</td>
                </tr>
                <tr>
                    <td>Center</td>
                    <td>0.5</td>
                </tr>
                <tr>
                    <td>Down</td>
                    <td>0.0</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>IncludeAdjacent</td>
        <td>DWORD</td>
        <td>No</td>
        <td>
            <p>Indicates that adjacent switch positions will also cause the mapped axis to report 1.0. In the above example, if <b>IncludeAdjacent</b> is set, then the axis translation is done as follows:</p>
            <table>
                <tr>
                    <th>Switch position</th>
                    <th>AxisValue</th>
                </tr>
                <tr>
                    <td>Up</td>
                    <td>1.0</td>
                </tr>
                <tr>
                    <td>UpRight</td>
                    <td>1.0</td>
                </tr>
                <tr>
                    <td>UpLeft</td>
                    <td>1.0</td>
                </tr>
                <tr>
                    <td>Center</td>
                    <td>0.5</td>
                </tr>
                <tr>
                    <td>Down</td>
                    <td>0.0</td>
                </tr>
                <tr>
                    <td>DownRight</td>
                    <td>0.0</td>
                </tr>
                <tr>
                    <td>DownLeft</td>
                    <td>0.0</td>
                </tr>
            </table>
        </td>
    </tr>
</table>

### Switch mapping

Switch positions can be mapped either from a set of buttons in the buttons array of the **RawGameController** or from an index in the switches array. Switch positions can't be mapped from axes.

<table>
    <tr>
        <th>Source</th>
        <th>Value name</th>
        <th>Value type</th>
        <th>Value info</th>
    </tr>
    <tr>
        <td rowspan="10" style="vertical-align: middle;">Button</td>
        <td>ButtonCount</td>
        <td>DWORD</td>
        <td>2, 4, or 8</td>
    </tr>
    <tr>
        <td>SwitchKind</td>
        <td>REG_SZ</td>
        <td><b>TwoWay</b>, <b>FourWay</b>, or <b>EightWay</b>
    </tr>
    <tr>
        <td>UpButtonIndex</td>
        <td>DWORD</td>
        <td rowspan="8" style="vertical-align: middle;">See <a href="#buttonindex-values">*ButtonIndex values</a></td>
    </tr>
    <tr>
        <td>DownButtonIndex</td>
        <td>DWORD</td>
    </tr>
    <tr>
        <td>LeftButtonIndex</td>
        <td>DWORD</td>
    </tr>
    <tr>
        <td>RightButtonIndex</td>
        <td>DWORD</td>
    </tr>
    <tr>
        <td>UpRightButtonIndex</td>
        <td>DWORD</td>
    </tr>
    <tr>
        <td>DownRightButtonIndex</td>
        <td>DWORD</td>
    </tr>
    <tr>
        <td>DownLeftButtonIndex</td>
        <td>DWORD</td>
    </tr>
    <tr>
        <td>UpLeftButtonIndex</td>
        <td>DWORD</td>
    </tr>
    <tr>
        <td rowspan="9" style="vertical-align: middle;">Axis</td>
        <td>SwitchKind</td>
        <td>REG_SZ</td>
        <td><b>TwoWay</b>, <b>FourWay</b>, or <b>EightWay</b></td>
    </tr>
    <tr>
        <td>XAxisIndex</td>
        <td>DWORD</td>
        <td rowspan="2" style="vertical-align: middle;"><b>YAxisIndex</b> is always present. <b>XAxisIndex</b> is only present when <b>SwitchKind</b> is <b>FourWay</b> or <b>EightWay</b>.</td>
    </tr>
    <tr>
        <td>YAxisIndex</td>
        <td>DWORD</td>
    </tr>
    <tr>
        <td>XDeadZonePercent</td>
        <td>DWORD</td>
        <td rowspan="2" style="vertical-align: middle;">Indicate the size of the dead zone around the center position of the axes.</td>
    </tr>
    <tr>
        <td>YDeadZonePercent</td>
        <td>DWORD</td>
    </tr>
    <tr>
        <td>XDebouncePercent</td>
        <td>DWORD</td>
        <td rowspan="2" style="vertical-align: middle;">Define the size of the windows around the upper and lower dead zone limits, which are used to de-bounce the reported switch state.</td>
    </tr>
    <tr>
        <td>YDebouncePercent</td>
        <td>DWORD</td>
    </tr>
    <tr>
        <td>XInvert</td>
        <td>DWORD</td>
        <td rowspan="2" style="vertical-align: middle;">Indicate that the corresponding axis values should be inverted before the dead zone and debounce window calculations are applied.</td>
    </tr>
    <tr>
        <td>YInvert</td>
        <td>DWORD</td>
    </tr>
    <tr>
        <td rowspan="3" style="vertical-align: middle;">Switch</td>
        <td>SwitchIndex</td>
        <td>DWORD</td>
        <td>Index in the <b>RawGameController</b> switch array.
    </tr>
    <tr>
        <td>Invert</td>
        <td>DWORD</td>
        <td>Indicates that the switch reports its positions in a counter-clockwise order, rather than the default clockwise order.</td>
    </tr>
    <tr>
        <td>PositionBias</td>
        <td>DWORD</td>
        <td>
            <p>Shifts the starting point of how positions are reported by the specified amount. <b>PositionBias</b> is always counted clockwise from the original starting point, and is applied before the order of values is reversed.</p>
            <p>For example, a switch that reports positions starting with <b>DownRight</b> in counter-clockwise order can be normalized by setting the <b>Invert</b> flag and specifying a <b>PositionBias</b> of 5:</p>
            <table>
                <tr>
                    <th>Position</th>
                    <th>Reported value</th>
                    <th>After PositionBias and Invert flags</th>
                </tr>
                <tr>
                    <td>DownRight</td>
                    <td>0</td>
                    <td>3</td>
                </tr>
                <tr>
                    <td>Right</td>
                    <td>1</td>
                    <td>2</td>
                </tr>
                <tr>
                    <td>UpRight</td>
                    <td>2</td>
                    <td>1</td>
                </tr>
                <tr>
                    <td>Up</td>
                    <td>3</td>
                    <td>0</td>
                </tr>
                <tr>
                    <td>UpLeft</td>
                    <td>4</td>
                    <td>7</td>
                </tr>
                <tr>
                    <td>Left</td>
                    <td>5</td>
                    <td>6</td>
                </tr>
                <tr>
                    <td>DownLeft</td>
                    <td>6</td>
                    <td>5</td>
                </tr>
                <tr>
                    <td>Down</td>
                    <td>7</td>
                    <td>4</td>
                </tr>
            </table>
    </tr>
</table>

#### *ButtonIndex values

\*ButtonIndex values index into the **RawGameController**'s button array:

<table>
    <tr>
        <th>ButtonCount</th>
        <th>SwitchKind</th>
        <th>RequiredMappings</th>
    </tr>
    <tr>
        <td>2</td>
        <td>TwoWay</td>
        <td>
            <ul>
                <li>UpButtonIndex</li>
                <li>DownButtonIndex</li>
            </ul>
        </td>
    </tr>
    <tr>
        <td>4</td>
        <td>FourWay</td>
        <td>
            <ul>
                <li>UpButtonIndex</li>
                <li>DownButtonIndex</li>
                <li>LeftButtonIndex</li>
                <li>RightButtonIndex</li>
            </ul>
        </td>
    </tr>
    <tr>
        <td>4</td>
        <td>EightWay</td>
        <td>
            <ul>
                <li>UpButtonIndex</li>
                <li>DownButtonIndex</li>
                <li>LeftButtonIndex</li>
                <li>RightButtonIndex</li>
            </ul>
        </td>
    </tr>
    <tr>
        <td>8</td>
        <td>EightWay</td>
        <td>
            <ul>
                <li>UpButtonIndex</li>
                <li>DownButtonIndex</li>
                <li>LeftButtonIndex</li>
                <li>RightButtonIndex</li>
                <li>UpRightButtonIndex</li>
                <li>DownRightButtonIndex</li>
                <li>DownLeftButtonIndex</li>
                <li>UpLeftButtonIndex</li>
            </ul>
        </td>
    </tr>
</table>

### Properties mapping

These are static mapping values for different mapping types.

<table>
    <tr>
        <th>Mapping</th>
        <th>Value name</th>
        <th>Value type</th>
        <th>Value info</th>
    </tr>
    <tr>
        <td>RacingWheel</td>
        <td>MaxWheelAngle</td>
        <td>DWORD</td>
        <td>Indicates the maximum physical wheel angle supported by the wheel in a single direction. For example, a wheel with a possible rotation of -90 degrees to 90 degrees would specify 90.</td>
    </tr>
</table>

## Labels

Labels should be present under the **Labels** key under the device root. **Labels** can have 3 subkeys: **Buttons**, **Axes**, and **Switches**.

### Button labels

The **Buttons** key maps each of the button positions in the **RawGameController**'s buttons array to a string. Each string is mapped internally to the corresponding [GameControllerButtonLabel](https://docs.microsoft.com/uwp/api/windows.gaming.input.gamecontrollerbuttonlabel) enum value. For example, if a gamepad has ten buttons and the order in which the **RawGameController** parses out the buttons and presents them in the buttons report is like this:

```cpp
Menu,               // Index 0
View,               // Index 1
LeftStickButton,    // Index 2
RightStickButton,   // Index 3
LetterA,            // Index 4
LetterB,            // Index 5
LetterX,            // Index 6
LetterY,            // Index 7
LeftBumper,         // Index 8
RightBumper         // Index 9
```

The labels should appear in this order under the **Buttons** key:

<table>
    <tr>
        <th>Name</th>
        <th>Value (type: REG_SZ)</th>
    </tr>
    <tr>
        <td>Button0</td>
        <td>Menu</td>
    </tr>
    <tr>
        <td>Button1</td>
        <td>View</td>
    </tr>
    <tr>
        <td>Button2</td>
        <td>LeftStickButton</td>
    </tr>
    <tr>
        <td>Button3</td>
        <td>RightStickButton</td>
    </tr>
    <tr>
        <td>Button4</td>
        <td>LetterA</td>
    </tr>
    <tr>
        <td>Button5</td>
        <td>LetterB</td>
    </tr>
    <tr>
        <td>Button6</td>
        <td>LetterX</td>
    </tr>
    <tr>
        <td>Button7</td>
        <td>LetterY</td>
    </tr>
    <tr>
        <td>Button8</td>
        <td>LeftBumper</td>
    </tr>
    <tr>
        <td>Button9</td>
        <td>RightBumper</td>
    </tr>
</table>

### Axis labels

The **Axes** key will map each of the axis positions in the **RawGameController**'s axis array to one of the labels listed in [GameControllerButtonLabel Enum](https://docs.microsoft.com/uwp/api/windows.gaming.input.gamecontrollerbuttonlabel) just like the button labels. See the example in [Button labels](#button-labels).

### Switch labels

The **Switches** key maps switch positions to labels. The values follow this naming convention: to label a position of a switch, whose index is *x* in the **RawGameController**'s switch array, add these values under the **Switches** subkey:

* SwitchxUp
* SwitchxUpRight
* SwitchxRight
* SwitchxDownRight
* SwitchxDown
* SwitchxDownLeft
* SwitchxUpLeft
* SwitchxLeft

The following table shows an example set of labels for switch positions of a 4-way switch which shows up at index 0 in the **RawGameController**:

<table>
    <tr>
        <th>Name</th>
        <th>Value (type: REG_SZ)</th>
    </tr>
    <tr>
        <td>Switch0Up</td>
        <td>XboxUp</td>
    </tr>
    <tr>
        <td>Switch0Right</td>
        <td>XboxRight</td>
    </tr>
    <tr>
        <td>Switch0Down</td>
        <td>XboxDown</td>
    </tr>
    <tr>
        <td>Switch0Left</td>
        <td>XboxLeft</td>
    </tr>
</table>

<!--### Label strings

* XboxBack
* XboxStart
* XboxMenu
* XboxView
* XboxUp
* XboxDown
* XboxLeft
* XboxRight
* XboxA
* XboxB
* XboxX
* XboxY
* XboxLeftBumper
* XboxLeftTrigger
* XboxLeftStickButton
* XboxRightBumper
* XboxRightTrigger
* XboxRightStickButton
* XboxPaddle1
* XboxPaddle2
* XboxPaddle3
* XboxPaddle4
* Mode
* Select
* Menu
* View
* Back
* Start
* Options
* Share
* Up
* Down
* Left
* Right
* LetterA
* LetterB
* LetterC
* LetterL
* LetterR
* LetterX
* LetterY
* LetterZ
* Cross
* Circle
* Square
* Triangle
* LeftBumper
* LeftTrigger
* LeftStickButton
* Left1
* Left2
* Left3
* RightBumper
* RightTrigger
* RightStickButton
* Right1
* Right2
* Right3
* Paddle1
* Paddle2
* Paddle3
* Paddle4
* Plus
* Minus
* DownLeftArrow
* DialLeft
* DialRight
* Suspension-->

## Example registry file

To show how all of these mappings and values come together, here is an example registry file for a generic **RacingWheel**:

```text
Windows Registry Editor Version 5.00

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004]
"Description" = "Example Wheel Device"

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\Labels\Buttons]
"Button0" = "LetterA"
"Button1" = "LetterB"
"Button2" = "LetterX"
"Button3" = "LetterY"
"Button6" = "Menu"
"Button7" = "View"
"Button8" = "RightStickButton"
"Button9" = "LeftStickButton"

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\Labels\Switches]
"Switch0Down" = "Down"
"Switch0Left" = "Left"
"Switch0Right" = "Right"
"Switch0Up" = "Up"

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel]
"MaxWheelAngle" = dword:000001c2

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\Brake]
"AxisIndex" = dword:00000002
"Invert" = dword:00000001

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\Button1]
"ButtonIndex" = dword:00000000

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\Button2]
"ButtonIndex" = dword:00000001

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\Button3]
"ButtonIndex" = dword:00000002

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\Button4]
"ButtonIndex" = dword:00000003

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\Button5]
"ButtonIndex" = dword:00000009

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\Button6]
"ButtonIndex" = dword:00000008

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\Button7]
"ButtonIndex" = dword:00000007

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\Button8]
"ButtonIndex" = dword:00000006

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\Clutch]
"AxisIndex" = dword:00000003
"Invert" = dword:00000001

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\DPadDown]
"IncludeAdjacent" = dword:00000001
"SwitchIndex" = dword:00000000
"SwitchPosition" = "Down"

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\DPadLeft]
"IncludeAdjacent" = dword:00000001
"SwitchIndex" = dword:00000000
"SwitchPosition" = "Left"

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\DPadRight]
"IncludeAdjacent" = dword:00000001
"SwitchIndex" = dword:00000000
"SwitchPosition" = "Right"

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\DPadUp]
"IncludeAdjacent" = dword:00000001
"SwitchIndex" = dword:00000000
"SwitchPosition" = "Up"

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\FifthGear]
"ButtonIndex" = dword:00000010

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\FirstGear]
"ButtonIndex" = dword:0000000c

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\FourthGear]
"ButtonIndex" = dword:0000000f

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\NextGear]
"ButtonIndex" = dword:00000004

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\PreviousGear]
"ButtonIndex" = dword:00000005

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\ReverseGear]
"ButtonIndex" = dword:0000000b

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\SecondGear]
"ButtonIndex" = dword:0000000d

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\SixthGear]
"ButtonIndex" = dword:00000011

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\ThirdGear]
"ButtonIndex" = dword:0000000e

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\Throttle]
"AxisIndex" = dword:00000001
"Invert" = dword:00000001

[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\GameInput\Devices\1234567800010004\RacingWheel\Wheel]
"AxisIndex" = dword:00000000
"Invert" = dword:00000000
```

## See also

* [Windows.Gaming.Input Namespace](/uwp/api/windows.gaming.input)
* [Windows.Gaming.Input.Custom Namespace](/uwp/api/windows.gaming.input.custom)
* [INF Files](/windows-hardware/drivers/install/inf-files)