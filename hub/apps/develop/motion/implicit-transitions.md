---
title: Implicit transitions
description: Animate property changes in WinUI 3 automatically using implicit transitions — ScalarTransition, Vector3Transition, and BrushTransition.
ms.topic: how-to
ms.date: 07/06/2026
author: GrantMeStrength
ms.author: jken
ms.localizationpriority: medium
---

# Implicit transitions

Implicit transitions automatically animate changes to specific UIElement properties. Instead of writing explicit animation code, you attach a transition object to a property and WinUI 3 animates any value changes smoothly.

WinUI 3 provides three transition classes:

| Transition class | Animates |
|---|---|
| `ScalarTransition` | `Opacity`, `Rotation` |
| `Vector3Transition` | `Scale`, `Translation` |
| `BrushTransition` | `Background`; `Foreground` on elements that expose `ForegroundTransition` (for example, `TextBlock`) |

## Animate opacity changes

Attach a `ScalarTransition` to the `OpacityTransition` property to animate opacity changes:

```xaml
<Rectangle x:Name="MyRect" Width="80" Height="80" Opacity="1.0">
    <Rectangle.OpacityTransition>
        <ScalarTransition />
    </Rectangle.OpacityTransition>
</Rectangle>
```

When you change `MyRect.Opacity` in code, WinUI 3 automatically animates the change:

```csharp
MyRect.Opacity = 0.2;  // Animates from 1.0 to 0.2
```

## Animate rotation changes

Attach a `ScalarTransition` to the `RotationTransition` property:

```xaml
<Rectangle x:Name="MyRect" Width="80" Height="80">
    <Rectangle.RotationTransition>
        <ScalarTransition />
    </Rectangle.RotationTransition>
</Rectangle>
```

```csharp
// Read ActualWidth/ActualHeight in Loaded or SizeChanged — they are 0 until layout runs.
private void MyRect_Loaded(object sender, RoutedEventArgs e)
{
    MyRect.CenterPoint = new System.Numerics.Vector3(
        (float)MyRect.ActualWidth / 2,
        (float)MyRect.ActualHeight / 2,
        0f);
    MyRect.Rotation = 90;  // Animates to 90 degrees
}
```

## Animate scale changes

Attach a `Vector3Transition` to the `ScaleTransition` property:

```xaml
<Rectangle x:Name="MyRect" Width="80" Height="80">
    <Rectangle.ScaleTransition>
        <Vector3Transition />
    </Rectangle.ScaleTransition>
</Rectangle>
```

```csharp
MyRect.Scale = new System.Numerics.Vector3(1.5f, 1.5f, 1.0f);  // Animates scale up
```

## Animate translation changes

Attach a `Vector3Transition` to the `TranslationTransition` property:

```xaml
<Rectangle x:Name="MyRect" Width="80" Height="80">
    <Rectangle.TranslationTransition>
        <Vector3Transition />
    </Rectangle.TranslationTransition>
</Rectangle>
```

```csharp
MyRect.Translation = new System.Numerics.Vector3(100, 0, 0);  // Moves 100px right
```

## Animate background brush changes

Attach a `BrushTransition` to the `BackgroundTransition` property of a panel or `ContentPresenter`:

```xaml
<ContentPresenter x:Name="MyPresenter" Width="80" Height="80" Background="Blue">
    <ContentPresenter.BackgroundTransition>
        <BrushTransition />
    </ContentPresenter.BackgroundTransition>
</ContentPresenter>
```

```csharp
// Animate from Blue to Yellow when the background is replaced with a new brush
MyPresenter.Background = new SolidColorBrush(Colors.Yellow);
```

> [!NOTE]
> `BrushTransition` animates between two *separate brush instances*. Setting a new color on the existing `SolidColorBrush` object does not trigger the transition. Assign a new `SolidColorBrush` object to trigger the animation.

## Configure transition duration

By default, transitions use a platform-defined duration. You can override it with the `Duration` property:

```xaml
<Rectangle.OpacityTransition>
    <ScalarTransition Duration="0:0:0.5" />
</Rectangle.OpacityTransition>
```

The `Duration` value is a `TimeSpan` in the format `hours:minutes:seconds`, with optional fractional seconds (for example, `0:0:0.5` for 500 ms).

## Open the WinUI 3 Gallery

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see implicit transitions in action](winui3gallery://item/ImplicitTransition)

The [WinUI 3 Gallery](https://apps.microsoft.com/detail/9P3JFPWWDZRC) app includes interactive examples of most WinUI 3 controls and features.

## Related articles

- [Property animations](xaml-property-animations.md)
- [Storyboarded animations](storyboarded-animations.md)
- [XAML animations overview](xaml-animation.md)
- [Composition natural animations](../composition/natural-animations.md)
