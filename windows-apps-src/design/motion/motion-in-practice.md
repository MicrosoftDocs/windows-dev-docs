---
description: Learn how Fluent motion fundamentals like timing, easing, directionality, and gravity come together in your app.
title: Motion in practice - animation in Windows apps
label: Motion in practice
template: detail.hbs
ms.date: 10/02/2018
ms.topic: article
keywords: windows 10, uwp
pm-contact: stmoy
design-contact: jeffarn
doc-status: Draft
ms.localizationpriority: medium
ms.custom: RS5
---
# Bringing it together

Timing, easing, directionality, and gravity work together to form the foundation of Fluent motion. Each has to be considered in the context of the others, and applied appropriately in the context of your app.

Here are 3 ways to apply Fluent motion fundamentals in your app.

:::row:::
    :::column:::
**Implicit animation**
Automatic tween and timing between values in a parameter change to achieve very simple Fluent motion using the standardized values.
    :::column-end:::
    :::column:::
**Built-in animation**
System components, such as common controls and shared motion, are "Fluent by default". Fundamentals have been applied in a manner consistent with their implied usage.
    :::column-end:::
    :::column:::
**Custom animation following guidance recommendations**
There may be times when the system does not yet provide an exact motion solution for your scenario. In those cases, use the baseline fundamental recommendations as a starting point for your experiences.
    :::column-end:::
:::row-end:::

**Transition example**

![functional animation](images/pageRefresh.gif)

:::row:::
    :::column:::
<b>Direction Forward Out:</b><br>
Fade out: 150m; Easing: Default Accelerate
<b>Direction Forward In:</b><br>
Slide up 150px: 300ms; Easing: Default Decelerate
    :::column-end:::
    :::column:::
<b>Direction Backward Out:</b><br>
Slide down 150px: 150ms; Easing: Default Accelerate
<b>Direction Backward In:</b><br>
Fade in: 300ms; Easing: Default Decelerate
    :::column-end:::
:::row-end:::

**Object example**

 ![300ms motion](images/control.gif)

:::row:::
    :::column:::
<b>Direction Expand:</b><br>
Grow: 300ms; Easing: Standard
    :::column-end:::
    :::column:::
<b>Direction Contract:</b><br>
Grow: 150ms; Easing: Default Accelerate
    :::column-end:::
:::row-end:::

## Examples

<table>
<tr>
<td><img src="images/xaml-controls-gallery-app-icon.png" alt="XAML controls gallery" width="168"></img></td>
<td>
    <p>If you have the <strong style="font-weight: semi-bold">XAML Controls Gallery</strong> app installed, click here to <a href="xamlcontrolsgallery:/item/ImplicitTransition">open the app and see Implicit Transitions in action</a>.</p>
    <ul>
    <li><a href="https://www.microsoft.com/p/xaml-controls-gallery/9msvh128x2zt">Get the XAML Controls Gallery app (Microsoft Store)</a></li>
    <li><a href="https://github.com/Microsoft/Xaml-Controls-Gallery">Get the source code (GitHub)</a></li>
    </ul>
</td>
</tr>
</table>

## Implicit Animations

> Implicit animations require Windows 10, version 1809 ([SDK 17763](https://developer.microsoft.com/windows/downloads/windows-10-sdk)) or later.

Implicit animations are a simple way to achieve Fluent motion by automatically interpolating between the old and new values during a parameter change.

You can implicitly animate changes to the following properties:

- [UIElement](/uwp/api/windows.ui.xaml.uielement)
  - **Opacity**
  - **Rotation**
  - **Scale**
  - **Translation**

- [Border](/uwp/api/windows.ui.xaml.controls.border), [ContentPresenter](/uwp/api/windows.ui.xaml.controls.contentpresenter), or [Panel](/uwp/api/windows.ui.xaml.controls.panel)
  - **Background**

Each property that can have changes implicitly animated has a corresponding _transition_ property. To animate the property, you assign a transition type to the corresponding _transition_ property. This table shows the _transition_ properties and the transition type to use for each one.

| Animated property | Transition property | Implicit transition type |
| -- | -- | -- |
| [UIElement.Opacity](/uwp/api/windows.ui.xaml.uielement.opacity) | [OpacityTransition](/uwp/api/windows.ui.xaml.uielement.opacitytransition) | [ScalarTransition](/uwp/api/windows.ui.xaml.scalartransition) |
| [UIElement.Rotation](/uwp/api/windows.ui.xaml.uielement.rotation) | [RotationTransition](/uwp/api/windows.ui.xaml.uielement.rotationtransition) | [ScalarTransition](/uwp/api/windows.ui.xaml.scalartransition) |
| [UIElement.Scale](/uwp/api/windows.ui.xaml.uielement.scale) | [ScaleTransition](/uwp/api/windows.ui.xaml.uielement.scaletransition) | [Vector3Transition](/uwp/api/windows.ui.xaml.vector3transition) |
| [UIElement.Translation](/uwp/api/windows.ui.xaml.uielement.translation) | [TranslationTransition](/uwp/api/windows.ui.xaml.uielement.translationtransition) | [Vector3Transition](/uwp/api/windows.ui.xaml.vector3transition) |
| [Border.Background](/uwp/api/windows.ui.xaml.controls.border.background) | [BackgroundTransition](/uwp/api/windows.ui.xaml.controls.border.backgroundtransition) | [BrushTransition](//uwp/api/windows.ui.xaml.uielement.brushtransition) |
| [ContentPresenter.Background](/uwp/api/windows.ui.xaml.controls.contentpresenter.background) | [BackgroundTransition](/uwp/api/windows.ui.xaml.controls.contentpresenter.backgroundtransition) | [BrushTransition](//uwp/api/windows.ui.xaml.uielement.brushtransition) |
| [Panel.Background](/uwp/api/windows.ui.xaml.controls.panel.background) | [BackgroundTransition](/uwp/api/windows.ui.xaml.controls.panel.backgroundtransition)  | [BrushTransition](//uwp/api/windows.ui.xaml.uielement.brushtransition) |

This example shows how to use the Opacity property and transition to make a button fade in when the control is enabled and fade out when it's disabled.

```xaml
<Button x:Name="SubmitButton"
        Content="Submit"
        Opacity="{x:Bind OpaqueIfEnabled(SubmitButton.IsEnabled), Mode=OneWay}">
    <Button.OpacityTransition>
        <ScalarTransition />
    </Button.OpacityTransition>
</Button>
```

```csharp
public double OpaqueIfEnabled(bool IsEnabled)
{
    return IsEnabled ? 1.0 : 0.2;
}
```

## Related articles

- [Motion overview](index.md)
- [Timing and easing](timing-and-easing.md)
- [Directionality and gravity](directionality-and-gravity.md)
