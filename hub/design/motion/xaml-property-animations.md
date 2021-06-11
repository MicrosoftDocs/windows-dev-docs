---
title: XAML property animations
description: Learn how to animate properties on a XAML UIElement directly by using Universal Windows Platform (UWP) composition animations.
ms.date: 09/13/2018
ms.topic: article
keywords: windows 10, uwp
pm-contact: stmoy
design-contact: jeffarn
ms.localizationpriority: medium
ms.custom: RS5
---
# Animating XAML elements with composition animations

This article introduces new properties that let you animate a XAML UIElement with the performance of composition animations and the ease of setting XAML properties.

Prior to Windows 10, version 1809, you had 2 choices to build animations in your UWP apps:

- use XAML constructs like [Storyboarded animations](storyboarded-animations.md), or the _*ThemeTransition_ and _*ThemeAnimation_ classes in the [Windows.UI.Xaml.Media.Animation](/uwp/api/windows.ui.xaml.media.animation) namespace.
- use composition animations as described in [Using the Visual Layer with XAML](../../composition/using-the-visual-layer-with-xaml.md).

Using the visual layer provides better performance than using the XAML constructs. But using [ElementCompositionPreview](/uwp/api/Windows.UI.Xaml.Hosting.ElementCompositionPreview) to get the element's underlying composition [Visual](/uwp/api/windows.ui.composition.visual) object, and then animating the Visual with composition animations, is more complex to use.

Starting in Windows 10, version 1809, you can animate properties on a UIElement directly using composition animations without the requirement to get the underlying composition Visual.

> [!NOTE]
> To use these properties on UIElement, your UWP project target version must be 1809 or later. For more info about configuring your project version, see [Version adaptive apps](../../debug-test-perf/version-adaptive-apps.md).

## Examples

<table>
<th align="left">XAML Controls Gallery<th>
<tr>
<td><img src="images/xaml-controls-gallery-app-icon.png" alt="XAML controls gallery" width="168"></img></td>
<td>
    <p>If you have the <strong style="font-weight: semi-bold">XAML Controls Gallery</strong> app installed, click here to <a href="xamlcontrolsgallery:/item/XamlCompInterop">open the app and see Animation interop in action</a>.</p>
    <ul>
    <li><a href="https://www.microsoft.com/store/productId/9MSVH128X2ZT">Get the XAML Controls Gallery app (Microsoft Store)</a></li>
    <li><a href="https://github.com/Microsoft/Xaml-Controls-Gallery">Get the source code (GitHub)</a></li>
    </ul>
</td>
</tr>
</table>

## New rendering properties replace old rendering properties

This table shows the properties you can use to modify the rendering of a UIElement, that can also be animated with a [CompositionAnimation](/uwp/api/windows.ui.composition.compositionanimation).

| Property | Type | Description |
| -- | -- | -- |
| [Opacity](/uwp/api/windows.ui.xaml.uielement.opacity) | Double | The degree of the object's opacity |
| [Translation](/uwp/api/windows.ui.xaml.uielement.translation) | Vector3 | Shift the X/Y/Z position of the element |
| [TransformMatrix](/uwp/api/windows.ui.xaml.uielement.transformmatrix) | Matrix4x4 | The transform matrix to apply to the element |
| [Scale](/uwp/api/windows.ui.xaml.uielement.scale) | Vector3 | Scale the element, centered on the CenterPoint |
| [Rotation](/uwp/api/windows.ui.xaml.uielement.rotation) | Float | Rotate the element around the RotationAxis and CenterPoint |
| [RotationAxis](/uwp/api/windows.ui.xaml.uielement.rotationaxis) | Vector3 | The axis of rotation |
| [CenterPoint](/uwp/api/windows.ui.xaml.uielement.centerpoint) | Vector3 | The center point of scale and rotation |

The TransformMatrix property value is combined with the Scale, Rotation, and Translation properties in the following order:  TransformMatrix, Scale, Rotation, Translation.

These properties don't affect the element's layout, so modifying these properties does not cause a new [Measure](/uwp/api/windows.ui.xaml.uielement.measure)/[Arrange](/uwp/api/windows.ui.xaml.uielement.arrange) pass.

These properties have the same purpose and behavior as the like-named properties on the composition [Visual](/uwp/api/windows.ui.composition.visual) class (except for Translation, which isn't on Visual).

### Example: Setting the Scale property

This example shows how to set the Scale property on a Button.

```xaml
<Button Scale="2,2,1" Content="I am a large button" />
```

```csharp
var button = new Button();
button.Content = "I am a large button";
button.Scale = new Vector3(2.0f,2.0f,1.0f);
```

### Mutual exclusivity between new and old properties

> [!NOTE]
> The **Opacity** property does not enforce the mutual exclusivity described in this section. You use the same Opacity property whether you use XAML or composition animations.

The properties that can be animated with a CompositionAnimation are replacements for several existing UIElement properties:

- [RenderTransform](/uwp/api/windows.ui.xaml.uielement.rendertransform)
- [RenderTransformOrigin](/uwp/api/windows.ui.xaml.uielement.rendertransformorigin)
- [Projection](/uwp/api/windows.ui.xaml.uielement.projection)
- [Transform3D](/uwp/api/windows.ui.xaml.uielement.transform3d)

When you set (or animate) any of the new properties, you cannot use the old properties. Conversely, if you set (or animate) any of the old properties, you cannot use the new properties.

You also cannot use the new properties if you use ElementCompositionPreview to get and manage the Visual yourself using these methods:

- [ElementCompositionPreview.GetElementVisual](/uwp/api/windows.ui.xaml.hosting.elementcompositionpreview.getelementvisual)
- [ElementCompositionPreview.SetIsTranslationEnabled](/uwp/api/windows.ui.xaml.hosting.elementcompositionpreview.setistranslationenabled)

> [!IMPORTANT]
> Attempting to mix the use of the two sets of properties will cause the API call to fail and produce an error message.

It’s possible to switch from one set of properties by clearing them, though for simplicity it's not recommended. If the property is backed by a DependencyProperty (for example, UIElement.Projection is backed by UIElement.ProjectionProperty), then call ClearValue to restore it to its "unused" state. Otherwise (for example, the Scale property), set the property to its default value.

## Animating UIElement properties with CompositionAnimation

You can animate the rendering properties listed in the table with a CompositionAnimation. These properties can also be referenced by an [ExpressionAnimation](/uwp/api/windows.ui.composition.expressionanimation).

Use the [StartAnimation](/uwp/api/windows.ui.xaml.uielement.startanimation) and [StopAnimation](/uwp/api/windows.ui.xaml.uielement.stopanimation) methods on UIElement to animate the UIElement properties.

### Example: Animating the Scale property with a Vector3KeyFrameAnimation

This example shows how to animate the scale of a Button.

```csharp
var compositor = Window.Current.Compositor;
var animation = compositor.CreateVector3KeyFrameAnimation();

animation.InsertKeyFrame(1.0f, new Vector3(2.0f,2.0f,1.0f));
animation.Duration = TimeSpan.FromSeconds(1);
animation.Target = "Scale";

button.StartAnimation(animation);
```

### Example: Animating the Scale property with an ExpressionAnimation

A page has two buttons. The second button animates to be twice as large (via scale) as the first button.

```xaml
<Button x:Name="sourceButton" Content="Source"/>
<Button x:Name="destinationButton" Content="Destination"/>
```

```csharp
var compositor = Window.Current.Compositor;
var animation = compositor.CreateExpressionAnimation("sourceButton.Scale*2");
animation.SetExpressionReferenceParameter("sourceButton", sourceButton);
animation.Target = "Scale";
destinationButton.StartAnimation(animation);
```

## Related topics

- [Storyboarded animations](storyboarded-animations.md)
- [Using the Visual Layer with XAML](../../composition/using-the-visual-layer-with-xaml.md)
- [Transforms overview](../layout/transforms.md)
