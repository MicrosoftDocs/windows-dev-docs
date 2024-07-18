---
description: Reveal Focus is a lighting effect that animates the border of focusable elements when the user moves gamepad or keyboard focus to them.
title: Reveal Focus
template: detail.hbs
ms.date: 03/25/2022
ms.topic: article
keywords: windows 10, uwp
pm-contact: chphilip
design-contact: 
dev-contact: stevenki
ms.localizationpriority: medium
---
# Reveal Focus

![hero image](images/header-reveal-focus.svg)

Reveal Focus is a lighting effect for [10-foot experiences](../devices/designing-for-tv.md), such as game consoles on television screens. It animates the border of focusable elements, such as buttons, when the user moves gamepad or keyboard focus to them. It's turned off by default, but it's simple to enable.

> **Important APIs**: [Application.FocusVisualKind property](/uwp/api/windows.ui.xaml.application.FocusVisualKind), [FocusVisualKind enum](/uwp/api/windows.ui.xaml.focusvisualkind), [Control.UseSystemFocusVisuals property](/uwp/api/Windows.UI.Xaml.Controls.Control.UseSystemFocusVisuals)

## How it works
Reveal Focus calls attention to focused elements by adding an animated glow around the element's border:

![Reveal Visual](images/traveling-focus-fullscreen-light-rf.gif)

This is especially helpful in 10-foot scenarios where the user might not be paying full attention to the entire TV screen. 

## Examples

<table>
<th align="left">WinUI 2 Gallery<th>
<tr>
<td><img src="images/xaml-controls-gallery-sm.png" alt="WinUI Gallery"></img></td>
<td>
    <p>If you have the <strong>WinUI 2 Gallery</strong> app installed, click here to <a href="winui2gallery:/item/RevealFocus">open the app and see Reveal Focus in action</a>.</p>
    <ul>
    <li><a href="https://www.microsoft.com/store/productId/9MSVH128X2ZT">Get the WinUI 2 Gallery app (Microsoft Store)</a></li>
    <li><a href="https://github.com/Microsoft/WinUI-Gallery">Get the source code (GitHub)</a></li>
    </ul>
</td>
</tr>
</table>

## How to use it

Reveal Focus is off by default. To enable it:
1. In your app's constructor, call the [AnalyticsInfo.VersionInfo.DeviceFamily](/uwp/api/windows.system.profile.analyticsversioninfo.DeviceFamily) property and check whether the current device family is `Windows.Xbox`.
2. If the device family is `Windows.Xbox`, set the [Application.FocusVisualKind](/uwp/api/windows.ui.xaml.application.FocusVisualKind) property to `FocusVisualKind.Reveal`. 

```csharp
    if(AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Xbox")
    {
        this.FocusVisualKind = FocusVisualKind.Reveal;
    }
```

After you set the **FocusVisualKind** property, the system automatically applies the Reveal Focus effect to all controls whose [UseSystemFocusVisuals](/uwp/api/Windows.UI.Xaml.Controls.Control.UseSystemFocusVisuals) property is set to **True** (the default value for most controls). 

## Why isn't Reveal Focus on by default? 
As you can see, it's fairly easy to turn on Reveal Focus when the app detects it's running on an Xbox. So, why doesn't the system just turn it on for you? Because Reveal Focus increases the size of the focus visual, which might cause issues with your UI layout. In some cases, you'll want to customize the Reveal Focus effect to optimize it for your app.

## Customizing Reveal Focus

You can customize the Reveal Focus effect by modifying the focus visual properties for each control: [FocusVisualPrimaryThickness](/uwp/api/windows.ui.xaml.frameworkelement.FocusVisualPrimaryThickness), [FocusVisualSecondaryThickness](/uwp/api/windows.ui.xaml.frameworkelement.FocusVisualSecondaryThickness), [FocusVisualPrimaryBrush](/uwp/api/windows.ui.xaml.frameworkelement.FocusVisualPrimaryBrush), and [FocusVisualSecondaryBrush](/uwp/api/windows.ui.xaml.frameworkelement.FocusVisualSecondaryBrush). These properties let you customize the color and thickness of the focus rectangle. (They're the same properties you use for creating [High Visibility focus visuals](../input/guidelines-for-visualfeedback.md#high-visibility-focus-visuals).) 

But before you start customizing it, it's helpful to know a little more about the components that make up Reveal Focus.

There are three parts to the default Reveal Focus visuals: the primary border, the secondary border and the Reveal glow. The primary border is **2px** thick, and runs around the *outside* of the secondary border. The secondary border is **1px** thick and runs around the *inside* of the primary border. The Reveal Focus glow has a thickness proportional to the thickness of the primary border and runs around the *outside* of the primary border.

In addition to the static elements, Reveal Focus visuals feature an animated light that pulsates when at rests and moves in the direction of focus when moving focus.

![Reveal Focus layers](images/reveal-breakdown.svg)

## Customize the border thickness

To change the thickness of the border types of a control, use these properties:

| Border type | Property |
| --- | --- |
| Primary, Glow   | [FocusVisualPrimaryThickness](/uwp/api/windows.ui.xaml.frameworkelement.FocusVisualPrimaryThickness)<br/> (Changing the primary border changes the thickness of the glow proportionately.)   |
| Secondary   | [FocusVisualSecondaryThickness](/uwp/api/windows.ui.xaml.frameworkelement.FocusVisualSecondaryThickness)   |


This example changes the border thickness of a button's focus visual:

```xaml
<Button FocusVisualPrimaryThickness="2" FocusVisualSecondaryThickness="1"/>
```

## Customize the margin

The margin is the space between the control's visual bounds and the start of the focus visuals secondary border. The default margin is 1px away from the control bounds. You can edit this margin on a per-control basis by changing the [FocusVisualMargin](/uwp/api/windows.ui.xaml.frameworkelement.FocusVisualMargin) property:

```xaml
<Button FocusVisualPrimaryThickness="2" FocusVisualSecondaryThickness="1" FocusVisualMargin="-3"/>
```

A negative margin pushes the border away from the center of the control, and a positive margin moves the border closer to the center of the control.

## Customize the color

To change color of the Reveal Focus visual, use the [FocusVisualPrimaryBrush](/uwp/api/windows.ui.xaml.frameworkelement.FocusVisualPrimaryBrush) and [FocusVisualSecondaryBrush](/uwp/api/windows.ui.xaml.frameworkelement.FocusVisualSecondaryBrush) properties.

| Property | Default resource | Default resource value |
| ---- | ---- | --- | 
| [FocusVisualPrimaryBrush](/uwp/api/windows.ui.xaml.frameworkelement.FocusVisualPrimaryBrush) | SystemControlRevealFocusVisualBrush  | SystemAccentColor |
| [FocusVisualSecondaryBrush](/uwp/api/windows.ui.xaml.frameworkelement.FocusVisualSecondaryBrush)  | SystemControlFocusVisualSecondaryBrush  | SystemAltMediumColor |

(The FocusPrimaryBrush property only defaults to the **SystemControlRevealFocusVisualBrush** resources when **FocusVisualKind** is set to **Reveal**. Otherwise, it uses **SystemControlFocusVisualPrimaryBrush**.)


To change the color of the focus visual of an individual control, set the properties directly on the control. This example overrides the focus visual colors of a button.

```xaml

<!-- Specifying a color directly -->
<Button
    FocusVisualPrimaryBrush="DarkRed"
    FocusVisualSecondaryBrush="Pink"/>

<!-- Using theme resources -->
<Button
    FocusVisualPrimaryBrush="{ThemeResource SystemBaseHighColor}"
    FocusVisualSecondaryBrush="{ThemeResource SystemAccentColor}"/>    
```

To change the color of all focus visuals in your entire app, override the **SystemControlRevealFocusVisualBrush** and  **SystemControlFocusVisualSecondaryBrush** resources with your own definition:

```xaml
<!-- App.xaml -->
<Application.Resources>

    <!-- Override Reveal Focus default resources. -->
    <SolidColorBrush x:Key="SystemControlRevealFocusVisualBrush" Color="{ThemeResource SystemBaseHighColor}"/>
    <SolidColorBrush x:Key="SystemControlFocusVisualSecondaryBrush" Color="{ThemeResource SystemAccentColor}"/>
</Application.Resources>
```

For more information on modifying the focus visual's color, see [Color Branding & Customizing](../input/guidelines-for-visualfeedback.md#color-branding--customizing).


## Show just the glow

If you'd like to use only the glow without the primary or secondary focus visual, simply set the control's [FocusVisualPrimaryBrush](/uwp/api/windows.ui.xaml.frameworkelement.FocusVisualPrimaryBrush) property to `Transparent` and the [FocusVisualSecondaryThickness](/uwp/api/windows.ui.xaml.frameworkelement.FocusVisualSecondaryThickness)  to `0`. In this case, the glow will adopt the color of the control background to provide a borderless feel. You can modify the thickness of the glow using [FocusVisualPrimaryThickness](/uwp/api/windows.ui.xaml.frameworkelement.FocusVisualPrimaryThickness).

```xaml

<!-- Show just the glow -->
<Button
    FocusVisualPrimaryBrush="Transparent"
    FocusVisualSecondaryThickness="0" />    
```


## Use your own focus visuals

Another way to customize Reveal Focus is to opt out of the system-provided focus visuals by drawing your own using visual states. To learn more, see the [Focus visuals sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlFocusVisuals).


## Reveal Focus and the Fluent Design System

Reveal Focus is a Fluent Design System component that adds light to your app. To learn more about the Fluent Design system and its other components, see the [Fluent Design overview](../index.md).

## Related articles

- [Designing for Xbox and TV](../devices/designing-for-tv.md)
- [Gamepad and remote control interactions](../input/gamepad-and-remote-interactions.md)
- [Focus visuals sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlFocusVisuals)
- [Composition Effects](/windows/uwp/composition/composition-effects)
- [Science in the System: Fluent Design and Depth](https://medium.com/microsoft-design/science-in-the-system-fluent-design-and-depth-fb6d0f23a53f)
- [Science in the System: Fluent Design and Light](https://medium.com/microsoft-design/the-science-in-the-system-fluent-design-and-light-94a17e0b3a4f)
