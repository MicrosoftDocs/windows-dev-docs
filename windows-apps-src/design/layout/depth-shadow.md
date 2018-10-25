---
author: serenaz
description: Z-depth, or relative depth, and shadow are two ways to incorporate depth into your app to help users focus naturally and efficiently.
title: Z-depth and shadow for UWP apps
template: detail.hbs
ms.author: sezhen
ms.date: 02/12/2018
ms.topic: article


keywords: windows 10, uwp
pm-contact: chigy
design-contact: balrayit
ms.localizationpriority: medium
---

# Z-depth and shadow

![true depth](images/elevation-shadow/depth.svg)

The Fluent Depth System uses physical concepts like 3D positioning, light, and shadow to reinvent how digital UI can be perceived in a more layered, physical environment. Z-depth, or relative depth, and shadow are two ways to incorporate depth into your UWP app.

## What is z-depth?

Z-depth is the distance between two surfaces along the z-axis, and it illustrates how close an object is to the viewer.

![z-depth](images/elevation-shadow/elevation.svg)

### Why use z-depth?

In the physical world, we tend to focus on objects that are closer to us. We can apply this spatial instinct to digital UI, as well. For example, if you bring an element closer to the user, then the user will instinctively focus on the element. By moving UI elements closer in z-axis, you can establish visual hierarchy between objects, helping users complete tasks naturally and efficiently in your app. 

![z-depth in content menu](images/elevation-shadow/whyelevation.svg)

In addition to providing meaningful visual hierarchy, z-depth also allows you to create experiences that flow seamlessly from 2D to 3D environments, scaling your app across all devices and form factors. 

![z-depth in 2d to 3d](images/elevation-shadow/elevation-2d3d.svg)

### How is z-depth perceived?

Based on how we perceive depth in the physical world, here are several techniques that can be used to show proximity in digital UI.

- **Scale** Farther objects appear smaller than closer objects of the same size. This is method is difficult to demonstrate effectively in 2D space, so it is not generally recommended. However, you can use scale and [shadow](#what-is-shadow) to create an effective simulation of objects moving closer to the user in 2D.

    ![proximity with scale](images/elevation-shadow/elevation-scale.svg)

- **Atmosphere** Objects can appear farther away and out of focus with a “smoky” overlay or other atmospheric effect.

    ![proximity with atmosphere](images/elevation-shadow/elevation-atmosphere.svg)

- **Motion** Relative speed can be used to demonstrate proximity: closer objects move more quickly than distant background objects. To learn how to implement this effect, see [Parallax](../motion/parallax.md).

    ![proximity with motion](images/elevation-shadow/elevation-motion.svg)

### Recommendations for z-depth

Reduce the number of elevated planes to provide clear visual focus. For most scenarios, two planes is enough: one for foreground items (high proximity) and another for background items (low proximity). If you have multiple elevated items that don’t overlap, group them the same plane (i.e., foreground) to reduce the number of planes.

![z-depth within an app](images/elevation-shadow/app-depth.svg)

## What is shadow?

![shadow](images/elevation-shadow/shadow.svg)

Shadow is a way to perceive elevation. When there is light above an elevated object, there is a shadow on the surface below. The higher the object, the larger and softer the shadow becomes. Note that elevated objects don’t need to have shadows, but shadows do indicate elevation.

In UWP apps, shadows should be purposeful, not aesthetic. If shadows detract from focus and productivity, then limit the use of shadow.

You can use shadows with either the ThemeShadow or DropShadow APIs.

## ThemeShadow

The ThemeShadow type can be applied to any XAML element to draw shadows appropriately based on x, y, z coordinates. ThemeShadow also automatically adjusts for other environmental specifications:

- Adapts to changes in lighting, user theme, app environment, and shell.
- Shadows elements automatically based on their elevation.
- Keeps elements in sync as they move and change elevation.
- Keeps shadows consistent throughout and across applications.

Here are examples of ThemeShadow at different elevations with the light and dark themes:

![smart shadows with light theme](images/elevation-shadow/smartshadow-light.svg)

![smart shadows with dark theme](images/elevation-shadow/smartshadow-dark.svg)

### ThemeShadow in common controls

The following common controls will automatically use ThemeShadow to cast shadows:

- [Dialogs and flyouts](../controls-and-patterns/dialogs.md)
- [NavigationView](../controls-and-patterns/navigationview.md)
- [Media transport control](../controls-and-patterns/media-playback.md)
- [Context menu](../controls-and-patterns/menus.md)
- [Command bar](../controls-and-patterns/app-bars.md)
- [AutoSuggest](../controls-and-patterns/auto-suggest-box.md), [ComboBox](https://docs.microsoft.com/uwp/api/Windows.UI.Xaml.Controls.ComboBox), [Calendar/Date/Time pickers](../controls-and-patterns/date-and-time.md), [Tooltip](../controls-and-patterns/tooltips.md)
- [Access keys](../input/access-keys.md)

### ThemeShadow in Popups

ThemeShadow automatically casts shadows when applied to any XAML element in a [Popup](/uwp/api/windows.ui.xaml.controls.primitives.popup). It will cast shadows on the app background content behind it and any other open Popups below it.

To use ThemeShadow with Popups, use the `Shadow` property to apply a ThemeShadow to a XAML element. Then, elevate the element from other elements behind it, for example by using the z component of the `Translation` property.
For most Popup UI, the recommended default elevation relative to the app background content is 32 effective pixels.

This example shows a Rectangle in a Popup casting a shadow onto the app background content and any other Popups behind it:

```xaml
<Popup>
    <Rectangle x:Name="PopupRectangle" Fill="White" Height="48" Width="96">
        <Rectangle.Shadow>
            <ThemeShadow />
        </Rectangle.Shadow>
    </Rectangle>
</Popup>
```

```csharp
// Elevate the rectangle by 32px
PopupRectangle.Translation += new Vector3(0, 0, 32);
```

![shadow from code example](images/elevation-shadow/smartshadow-example.svg)

### ThemeShadow in other elements

To cast a shadow from a XAML element that isn't in a Popup, you must explicitly specify the other elements that can receive the shadow in the `ThemeShadow.Receivers` collection.

This example shows two Buttons that cast shadows onto a Grid behind them:

```xaml
<Grid x:Name="BackgroundGrid" Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
    <Grid.Resources>
        <ThemeShadow x:Name="SharedShadow" />
    </Grid.Resources>

    <Button x:Name="Button1" Content="Button 1" Shadow="{StaticResource SharedShadow}" Margin="10" />

    <Button x:Name="Button2" Content="Button 2" Shadow="{StaticResource SharedShadow}" Margin="120" />
</Grid>
```

```csharp
/// Add BackgroundGrid as a shadow receiver and elevate the casting buttons above it
SharedShadow.Receivers.Add(BackgroundGrid);

Button1.Translation += new Vector3(0, 0, 16);
Button2.Translation += new Vector3(0, 0, 32);
```

### Performance best practices for ThemeShadow

1. Limit the number of custom receiver elements to the minimum necessary. 

2. If multiple receiver elements are at the same elevation then try to combine them by targeting a single parent element instead.

3. If multiple elements will cast the same type of shadow onto the same receiver elements then add the shadow as a shared resource and reuse it.

## Drop shadow

DropShadow is not automatically responsive to its environment and does not use light sources. For example implementations, see the [DropShadow Class](https://docs.microsoft.com/uwp/api/windows.ui.composition.dropshadow).

## Which shadow should I use?

| Property | ThemeShadow | DropShadow |
| - | - | - | - |
| **Min SDK** | RS5 | 14393 |
| **Adaptability** | Yes | No |
| **Customization** | No | Yes |
| **Light source** | Automatic (global by default, but can override per app) | None |
| **Supported in 3D environments** | Yes | No |

- Generally, we recommend using ThemeShadow, which adapts automatically to its environment.
- If you have more advanced scenarios for custom shadows, then use DropShadow, which allows for greater customization.
- For backwards compatibility, use DropShadow.
- For concerns about performance, limit the number of shadows, or use DropShadow.
- On HMDs in true 3D, use ThemeShadow. Since DropShadow draws at a specified offset from the visual it is parented to, from the side, it will look like it's floating in space. On the other hand, ThemeShadow is rendered on top of the visuals defined as receivers.
