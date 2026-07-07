---
title: XAML and Composition interoperability
description: Use Composition animations directly on WinUI 3 UIElement instances to create spring and expression animations without Visual layer extraction.
ms.topic: how-to
ms.date: 07/06/2026
author: GrantMeStrength
ms.author: jken
ms.localizationpriority: medium
---

# XAML and Composition interoperability

WinUI 3 XAML elements are backed by the Windows Composition visual layer. In WinUI 3, you can run `CompositionAnimation` objects directly on `UIElement` instances by using `UIElement.StartAnimation`. This differs from UWP, where you first needed to extract the underlying visual by using `ElementCompositionPreview.GetElementVisual`.

This interop lets you apply spring animations, expression animations, and other Composition animation types to WinUI 3 elements.

> [!div class="checklist"]
>
> - **Important APIs**: [CompositionTarget.GetCompositorForCurrentThread](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.compositiontarget.getcompositorforcurrentthread), [UIElement.StartAnimation](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.startanimation)

## Get the Compositor

Use `CompositionTarget.GetCompositorForCurrentThread()` to get the `Compositor` for the current XAML UI thread:

```csharp
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml.Media;

Compositor compositor = CompositionTarget.GetCompositorForCurrentThread();
```

Call this method from the UI thread. The `Compositor` lets you create animations, brushes, and effects.

## Run a spring animation on a UIElement

Create a `SpringVector3NaturalMotionAnimation` that targets the `Scale` property, and then call `UIElement.StartAnimation` to run it:

```csharp
using System.Numerics;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

private readonly Compositor _compositor = CompositionTarget.GetCompositorForCurrentThread();
private SpringVector3NaturalMotionAnimation? _springAnimation;

private SpringVector3NaturalMotionAnimation CreateOrUpdateSpringAnimation(float finalValue)
{
    _springAnimation ??= _compositor.CreateSpringVector3Animation();
    _springAnimation.Target = "Scale";
    _springAnimation.FinalValue = new Vector3(finalValue, finalValue, 1.0f);

    return _springAnimation;
}

private void Element_PointerEntered(object sender, PointerRoutedEventArgs e)
{
    var springAnimation = CreateOrUpdateSpringAnimation(1.5f);
    (sender as UIElement)?.StartAnimation(springAnimation);
}

private void Element_PointerExited(object sender, PointerRoutedEventArgs e)
{
    var springAnimation = CreateOrUpdateSpringAnimation(1.0f);
    (sender as UIElement)?.StartAnimation(springAnimation);
}
```

The `SpringVector3NaturalMotionAnimation` uses spring physics. The element can overshoot slightly and settle into place, which creates a natural feel.

## Tune spring behavior

Control the spring behavior by setting `DampingRatio` and `Period`:

```csharp
_springAnimation.DampingRatio = 0.6f;
_springAnimation.Period = TimeSpan.FromMilliseconds(50);
```

| `DampingRatio` | Effect |
|---|---|
| `< 1.0` | Underdamped. The element bounces before it settles. |
| `= 1.0` | Critically damped. The element reaches the target without overshoot. |
| `> 1.0` | Overdamped. The element approaches the target more slowly. |

## Use expression animations

Expression animations use mathematical expressions to continuously drive a property from another value. In WinUI 3, you can start an expression animation directly on a `UIElement`:

```csharp
using System.Numerics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

var compositor = CompositionTarget.GetCompositorForCurrentThread();
var source = compositor.CreatePropertySet();
source.InsertVector3("Offset", new Vector3(40.0f, 0.0f, 0.0f));

var expressionAnimation = compositor.CreateExpressionAnimation("source.Offset");
expressionAnimation.SetReferenceParameter("source", source);
expressionAnimation.Target = "Translation";

myElement.StartAnimation(expressionAnimation);
```

For more information about expression animations, see [Relation-based animations](relation-animations.md).

## WinUI 3 and UWP differences

In UWP, you had to extract a `Visual` from an element before you could run a Composition animation on it:

```csharp
// UWP pattern
var visual = ElementCompositionPreview.GetElementVisual(myElement);
visual.StartAnimation("Scale", springAnimation);
```

In WinUI 3, you can call `StartAnimation` directly on the `UIElement`:

```csharp
// WinUI 3 pattern
myElement.StartAnimation(springAnimation);
```

## Open the WinUI 3 Gallery

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see XAML and Composition interop in action](winui3gallery://item/XamlCompInterop)

The [WinUI 3 Gallery](https://apps.microsoft.com/detail/9P3JFPWWDZRC) app includes interactive examples of WinUI 3 controls and features.

## Related articles

- [Natural animations](natural-animations.md)
- [Spring animations](spring-animations.md)
- [Relation-based animations](relation-animations.md)
- [Visual layer overview](visual-layer.md)
