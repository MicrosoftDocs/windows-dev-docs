---
title: Gamepad and remote control interactions in Windows apps
description: Learn how to optimize your Windows App SDK desktop app for input from Xbox gamepad and remote control devices using XY focus navigation.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
ms.date: 07/08/2026
---

# Gamepad and remote control interactions

Build interaction experiences in your Windows App SDK app that work well with gamepad and remote control input, in addition to mouse, keyboard, and touch.

## Overview

Gamepad and remote control are the primary input devices for the Xbox 10-foot experience. Your Windows app should be usable and accessible through these input types as well as the traditional PC input types.

See [Designing for Xbox and TV](/windows/apps/design/devices/designing-for-tv) for general design guidance for the 10-foot experience.

## Key APIs

- [Windows.Gaming.Input](/uwp/api/windows.gaming.input)
- [Windows.Gaming.Input.Gamepad](/uwp/api/Windows.Gaming.Input.Gamepad)
- [XYFocusKeyboardNavigation](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.xyfocuskeyboardnavigation)

## Gamepad and remote control buttons

The following table lists hardware buttons and which input device supports them.

| Button | Gamepad | Remote control |
|---|---|---|
| A/Select | Yes | Yes |
| B/Back | Yes | Yes |
| D-pad | Yes | Yes |
| Menu | Yes | Yes |
| View | Yes | Yes |
| X and Y | Yes | No |
| Left/right stick | Yes | No |
| Left/right triggers | Yes | No |
| Left/right bumpers | Yes | No |

## Built-in input mapping

Windows automatically maps keyboard input to gamepad and remote control:

| Keyboard | Gamepad/remote |
|---|---|
| Arrow keys | D-pad (also left stick on gamepad) |
| Spacebar | A/Select button |
| Enter | A/Select button |
| Escape | B/Back button |

## XY focus navigation

XY focus navigation lets users navigate your app's UI in four directions (up, down, left, right) using the D-pad or left stick.

### Enable XY navigation

XY focus navigation is built into the XAML framework. Set `XYFocusKeyboardNavigation` on a container to enable it:

```xml
<StackPanel XYFocusKeyboardNavigation="Enabled">
    <Button Content="Button 1"/>
    <Button Content="Button 2"/>
    <Button Content="Button 3"/>
</StackPanel>
```

### Override directional navigation

Use the `XYFocusUp`, `XYFocusDown`, `XYFocusLeft`, and `XYFocusRight` properties to override the default navigation behavior:

```xml
<Button Content="Button 1"
        XYFocusRight="{x:Bind Button2}"/>
<Button x:Name="Button2" Content="Button 2"
        XYFocusLeft="{x:Bind Button1}"/>
```

## Focus visual

The focus visual is a border that highlights the currently focused UI element. Use the built-in focus visual to help users navigate with gamepad or remote control.

In WinUI 3, customize the focus visual by setting properties on `FrameworkElement`:

```xml
<Button Content="My Button"
        FocusVisualPrimaryBrush="Red"
        FocusVisualPrimaryThickness="2"
        FocusVisualSecondaryBrush="Yellow"
        FocusVisualSecondaryThickness="1"/>
```

## Focus engagement

Focus engagement requires the user to press the A/Select button to interact with a focused control. This prevents accidental activation while navigating with the D-pad.

Set `IsFocusEngagementEnabled` on controls that benefit from this behavior:

```xml
<Slider IsFocusEngagementEnabled="True"/>
```

When focus engagement is enabled, the user presses A/Select to "engage" the control, interacts with it, and then presses B to "disengage."

## Mouse mode

For app experiences where XY focus navigation isn't practical (such as maps or drawing apps), enable mouse mode. This lets users navigate freely with a gamepad, similar to using a mouse.

> [!NOTE]
> UWP exposed an app-wide `Application.RequiresPointerMode` property for this scenario. `Microsoft.UI.Xaml.Application` in WinUI 3 doesn't have an equivalent property, so set `RequiresPointer` on each `Page` instead.

To enable mouse mode for a specific page, set `RequiresPointer` on the `Page`:

```xml
<Page RequiresPointer="WhenEngaged">
    <!-- Page content -->
</Page>
```

## CommandBar and ContextFlyout

Use `ContextFlyout` to provide context menus accessible via the Menu button on gamepad:

```xml
<GridView>
    <GridView.ContextFlyout>
        <MenuFlyout>
            <MenuFlyoutItem Text="Open"/>
            <MenuFlyoutItem Text="Delete"/>
        </MenuFlyout>
    </GridView.ContextFlyout>
</GridView>
```

## Best practices

- Test your app with both 2-foot (desktop) and 10-foot (TV) experiences.
- Ensure all functionality is accessible with D-pad navigation alone.
- Use `XYFocusKeyboardNavigation` on containers to enable directional navigation.
- Place the most important content and controls at the top or beginning of your layout.
- Don't rely on buttons unique to one input device for critical interactions.

## Related articles

- [Designing for Xbox and TV](/windows/apps/design/devices/designing-for-tv)
- [Keyboard interactions](/windows/apps/design/input/keyboard-interactions)
- [Focus navigation](/windows/apps/design/input/focus-navigation)
- [XInput (game controller input)](/windows/desktop/xinput/getting-started-with-xinput) — Low-level API for Xbox controller input including vibration, thumbstick deadzone, and trigger feedback
- [Windows.Gaming.Input](/uwp/api/windows.gaming.input) — WinRT API for gamepad, racing wheel, and flight stick input
- [Microsoft Game Development Kit (GDK)](https://github.com/microsoft/GDK) — Full game development platform for PC and Xbox
