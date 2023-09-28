---
ms.assetid: F46D5E18-10A3-4F7B-AD67-76437C77E4BC
title: Transforms overview
description: Learn how to use transforms in the Windows Runtime&\#160;API, by changing the relative coordinate systems of elements in the UI.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Transforms overview

Learn how to use transforms in the Windows Runtime API, by changing the relative coordinate systems of elements in the UI. This can be used to adjust the appearance of individual XAML elements, such as scaling, rotating, or transforming the position in x-y space.

## <span id="What_is_a_transform_"></span><span id="what_is_a_transform_"></span><span id="WHAT_IS_A_TRANSFORM_"></span>What is a transform?

A *transform* defines how to map, or transform, points from one coordinate space to another coordinate space. When a transform is applied to a UI element, it changes how that UI element is rendered to the screen as part of the UI.

Think of transforms in four broad classifications: translation, rotation, scaling and skew (or shear). For the purposes of using graphics APIs to change the appearance of UI elements, it's usually easiest to create transforms that define only one operation at a time. So the Windows Runtime defines a discrete class for each of these transform classifications:

-   [**TranslateTransform**](/uwp/api/Windows.UI.Xaml.Media.TranslateTransform): translates an element in x-y space, by setting values for [**X**](/uwp/api/windows.ui.xaml.media.translatetransform.x) and [**Y**](/uwp/api/windows.ui.xaml.media.translatetransform.y).
-   [**ScaleTransform**](/uwp/api/Windows.UI.Xaml.Media.ScaleTransform): scales the transform based on a center point, by setting values for [**CenterX**](/uwp/api/windows.ui.xaml.media.scaletransform.centerx), [**CenterY**](/uwp/api/windows.ui.xaml.media.scaletransform.centery), [**ScaleX**](/uwp/api/windows.ui.xaml.media.scaletransform.scalex) and [**ScaleY**](/uwp/api/windows.ui.xaml.media.scaletransform.scaleyproperty).
-   [**RotateTransform**](/uwp/api/Windows.UI.Xaml.Media.RotateTransform): rotates in x-y space, by setting values for [**Angle**](/uwp/api/windows.ui.xaml.media.rotatetransform.angle), [**CenterX**](/uwp/api/windows.ui.xaml.media.rotatetransform.centerx) and [**CenterY**](/uwp/api/windows.ui.xaml.media.rotatetransform.centery).
-   [**SkewTransform**](/uwp/api/Windows.UI.Xaml.Media.SkewTransform): skews or shears in x-y space, by setting values for [**AngleX**](/uwp/api/windows.ui.xaml.media.skewtransform.anglex), [**AngleY**](/uwp/api/windows.ui.xaml.media.skewtransform.angley), [**CenterX**](/uwp/api/windows.ui.xaml.media.skewtransform.centerx) and [**CenterY**](/uwp/api/windows.ui.xaml.media.scaletransform.centeryproperty).

Of these, you're likely to use [**TranslateTransform**](/uwp/api/Windows.UI.Xaml.Media.TranslateTransform) and [**ScaleTransform**](/uwp/api/Windows.UI.Xaml.Media.ScaleTransform) most often for UI scenarios.

You can combine transforms, and there are two Windows Runtime classes that support this: [**CompositeTransform**](/uwp/api/Windows.UI.Xaml.Media.CompositeTransform) and [**TransformGroup**](/uwp/api/Windows.UI.Xaml.Media.TransformGroup). In a **CompositeTransform**, transforms are applied in this order: scale, skew, rotate, translate. Use **TransformGroup** instead of **CompositeTransform** if you want the transforms applied in a different order. For more info, see [**CompositeTransform**](/uwp/api/Windows.UI.Xaml.Media.CompositeTransform).

## <span id="Transforms_and_layout"></span><span id="transforms_and_layout"></span><span id="TRANSFORMS_AND_LAYOUT"></span>Transforms and layout

In XAML layout, transforms are applied after the layout pass is complete, so available space calculations and other layout decisions have been made before the transforms are applied. Because layout comes first, you'll sometimes get unexpected results if you transform elements that are in a [**Grid**](/uwp/api/Windows.UI.Xaml.Controls.Grid) cell or similar layout container that allocates space during layout. The transformed element may appear truncated or obscured because it's trying to draw into an area that didn't calculate the post-transform dimensions when dividing space within its parent container. You may need to experiment with the transform results and adjust some settings. For example, instead of relying on adaptive layout and star sizing, you may need to change the **Center** properties or declare fixed pixel measurements for layout space to make sure the parent allots enough space.

**Migration note:**  Windows Presentation Foundation (WPF) had a **LayoutTransform** property that applied transforms prior to the layout pass. But Windows Runtime XAML doesn't support a **LayoutTransform** property. (Microsoft Silverlight didn't have this property either.)

As an alternative, the Windows Community Toolkit provides the [LayoutTransformControl](/dotnet/communitytoolkit/windows/layouttransformcontrol/layouttransformcontrol) that applies Matrix transformations on any FrameworkElement of your application.

## <span id="Applying_a_transform_to_a_UI_element"></span><span id="applying_a_transform_to_a_ui_element"></span><span id="APPLYING_A_TRANSFORM_TO_A_UI_ELEMENT"></span>Applying a transform to a UI element

When you apply a transform to an object, you typically do so to set the property [**UIElement.RenderTransform**](/uwp/api/windows.ui.xaml.uielement.rendertransform). Setting this property does not literally change the object pixel by pixel. What the property really does is apply the transform within the local coordinate space in which that object exists. Then the rendering logic and operation (post-layout) renders the combined coordinate spaces, making it look like the object has changed appearance and also potentially its layout position (if [**TranslateTransform**](/uwp/api/Windows.UI.Xaml.Media.TranslateTransform) was applied).

By default, each render transform is centered at the origin of the target object's local coordinate system—its (0,0). The only exception is a [**TranslateTransform**](/uwp/api/Windows.UI.Xaml.Media.TranslateTransform), which has no center properties to set because the translation effect is the same regardless of where it is centered. But the other transforms each have properties that set **CenterX** and **CenterY** values.

Whenever you use transforms with [**UIElement.RenderTransform**](/uwp/api/windows.ui.xaml.uielement.rendertransform), remember that there's another property on [**UIElement**](/uwp/api/Windows.UI.Xaml.UIElement) that affects how the transform behaves: [**RenderTransformOrigin**](/uwp/api/windows.ui.xaml.uielement.rendertransformorigin). What **RenderTransformOrigin** declares is whether the whole transform should apply to the default (0,0) point of an element or to some other origin point within the relative coordinate space of that element. For typical elements, (0,0) places the transform to the top left corner. Depending on what effect you want, you might choose to change **RenderTransformOrigin** rather than adjusting the **CenterX** and **CenterY** values on transforms. Note that if you apply both **RenderTransformOrigin** and **CenterX** / **CenterY** values, the results can be pretty confusing, especially if you're animating any of the values.

For hit-testing purposes, an object to which a transform is applied continues to respond to input in an expected way that's consistent to its visual appearance in x-y space. For example, if you've used a [**TranslateTransform**](/uwp/api/Windows.UI.Xaml.Media.TranslateTransform) to move a [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle) 400 pixels laterally in the UI, that **Rectangle** responds to [**PointerPressed**](/uwp/api/windows.ui.xaml.uielement.pointerpressed) events when the user presses the point where the **Rectangle** appears visually. You won't get false events if the user presses the area where the **Rectangle** was before being translated. For any z-index considerations that affect hit testing, applying a transform makes no difference; the z-index that governs which element handles input events for a point in x-y space is still evaluated using the child order as declared in a container. That order is usually the same as the order in which you declare the elements in XAML, although for child elements of a [**Canvas**](/uwp/api/Windows.UI.Xaml.Controls.Canvas) object you can adjust the order by applying the [**Canvas.ZIndex**](/previous-versions/windows/silverlight/dotnet-windows-silverlight/cc190397(v=vs.95)) attached property to child elements.

## <span id="Other_transform_properties"></span><span id="other_transform_properties"></span><span id="OTHER_TRANSFORM_PROPERTIES"></span>Other transform properties

-   [**Brush.Transform**](/uwp/api/windows.ui.xaml.media.brush.transform), [**Brush.RelativeTransform**](/uwp/api/windows.ui.xaml.media.brush.relativetransform): These influence how a [**Brush**](/uwp/api/Windows.UI.Xaml.Media.Brush) uses coordinate space within the area that the **Brush** is applied to set visual properties such as foregrounds and backgrounds. These transforms aren't relevant for the most common brushes (which are typically setting solid colors with [**SolidColorBrush**](/uwp/api/Windows.UI.Xaml.Media.SolidColorBrush)) but might be occasionally useful when painting areas with an [**ImageBrush**](/uwp/api/Windows.UI.Xaml.Media.ImageBrush) or [**LinearGradientBrush**](/uwp/api/Windows.UI.Xaml.Media.LinearGradientBrush).
-   [**Geometry.Transform**](/uwp/api/windows.ui.xaml.media.geometry.transform): You might use this property to apply a transform to a geometry prior to using that geometry for a [**Path.Data**](/uwp/api/windows.ui.xaml.shapes.path.data) property value.

## <span id="Animating_a_transform"></span><span id="animating_a_transform"></span><span id="ANIMATING_A_TRANSFORM"></span>Animating a transform

[**Transform**](/uwp/api/Windows.UI.Xaml.Media.Transform) objects can be animated. To animate a **Transform**, apply an animation of a compatible type to the property you want to animate. This typically means you're using [**DoubleAnimation**](/uwp/api/Windows.UI.Xaml.Media.Animation.DoubleAnimation) or [**DoubleAnimationUsingKeyFrames**](/uwp/api/windows.ui.xaml.media.animation.doubleanimationusingkeyframes) objects to define the animation, because all of the transform properties are of type [**Double**](/dotnet/api/system.double). Animations that affect a transform that's used for a [**UIElement.RenderTransform**](/uwp/api/windows.ui.xaml.uielement.rendertransform) value are not considered to be dependent animations, even if they have a nonzero duration. For more info about dependent animations, see [Storyboarded animations](../motion/storyboarded-animations.md).

If you animate properties to produce an effect similar to a transform in terms of the net visual appearance—for example, animating the [**Width**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Width) and [**Height**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Height) of a [**FrameworkElement**](/uwp/api/Windows.UI.Xaml.FrameworkElement) rather than applying a [**TranslateTransform**](/uwp/api/Windows.UI.Xaml.Media.TranslateTransform)—such animations are almost always treated as dependent animations. You'd have to enable the animations and there could be significant performance issues with the animation, especially if you're trying to support user interaction while that object is being animated. For that reason it's preferable to use a transform and animate it rather than animating any other property where the animation would be treated as a dependent animation.

To target the transform, there must be an existing [**Transform**](/uwp/api/Windows.UI.Xaml.Media.Transform) as the value for [**RenderTransform**](/uwp/api/windows.ui.xaml.uielement.rendertransform). You typically put an element for the appropriate transform type in the initial XAML, sometimes with no properties set on that transform.

You typically use an indirect targeting technique to apply animations to the properties of a transform. For more info about indirect targeting syntax, see [Storyboarded animations](../motion/storyboarded-animations.md) and [Property-path syntax](/windows/uwp/xaml-platform/property-path-syntax).

Default styles for controls sometimes define animations of transforms as part of their visual-state behavior. For example, the visual states for [**ProgressRing**](/uwp/api/Windows.UI.Xaml.Controls.ProgressRing) use animated [**RotateTransform**](/uwp/api/Windows.UI.Xaml.Media.RotateTransform) values to "spin" the dots in the ring.

Here's a simple example of how to animate a transform. In this case, it's animating the [**Angle**](/uwp/api/windows.ui.xaml.media.rotatetransform.angle) of a [**RotateTransform**](/uwp/api/Windows.UI.Xaml.Media.RotateTransform) to spin a [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle) in place around its visual center. This example names the **RotateTransform** so doesn't need indirect animation targeting, but you could alternatively leave the transform unnamed, name the element that the transform's applied to, and use indirect targeting such as `(UIElement.RenderTransform).(RotateTransform.Angle)`.

```xml
<StackPanel Margin="15">
  <StackPanel.Resources>
    <Storyboard x:Name="myStoryboard">
      <DoubleAnimation
       Storyboard.TargetName="myTransform"
       Storyboard.TargetProperty="Angle"
       From="0" To="360" Duration="0:0:5" 
       RepeatBehavior="Forever" />
    </Storyboard>
  </StackPanel.Resources>
  <Rectangle Width="50" Height="50" Fill="RoyalBlue"
   PointerPressed="StartAnimation">
    <Rectangle.RenderTransform>
      <RotateTransform x:Name="myTransform" Angle="45" CenterX="25" CenterY="25" />
    </Rectangle.RenderTransform>
  </Rectangle>
</StackPanel>
```

```xml
void StartAnimation (object sender, RoutedEventArgs e) {
    myStoryboard.Begin();
}
```

## <span id="Accounting_for_coordinate_frames_of_reference_at_run_time"></span><span id="accounting_for_coordinate_frames_of_reference_at_run_time"></span><span id="ACCOUNTING_FOR_COORDINATE_FRAMES_OF_REFERENCE_AT_RUN_TIME"></span>Accounting for coordinate frames of reference at run time

[**UIElement**](/uwp/api/Windows.UI.Xaml.UIElement) has a method named [**TransformToVisual**](/uwp/api/windows.ui.xaml.uielement.transformtovisual), which can generate a [**Transform**](/uwp/api/Windows.UI.Xaml.Media.Transform) that correlates the coordinate frames of reference for two UI elements. You can use this to compare an element to the app's default coordinate frame of reference if you pass the root visual as the first parameter. This can be useful if you've captured an input event from a different element, or if you are trying to predict layout behavior without actually requesting a layout pass.

Event data obtained from pointer events provides access to a [**GetCurrentPoint**](/uwp/api/windows.ui.input.pointerpoint.getcurrentpoint) method, where you can specify a *relativeTo* parameter to change the coordinate frame of reference to a specific element rather than the app default. This approach simply applies a translate transform internally and transforms the x-y coordinate data for you when it creates the returned [**PointerPoint**](/uwp/api/Windows.UI.Input.PointerPoint) object.

## <span id="Describing_a_transform_mathematically"></span><span id="describing_a_transform_mathematically"></span><span id="DESCRIBING_A_TRANSFORM_MATHEMATICALLY"></span>Describing a transform mathematically

A transform can be described in terms of a transformation matrix. A 3×3 matrix is used to describe the transformations in a two-dimensional, x-y plane. Affine transformation matrices can be multiplied to form any number of linear transformations, such as rotation and skew (shear), followed by translation. The final column of an affine transformation matrix is equal to (0, 0, 1), so you need to specify only the members of the first two columns in the mathematical description.

The mathematical description of a transform might be useful to you if you have a mathematical background or a familiarity with graphics-programming techniques that also use matrices to describe transformations of coordinate space. There's a [**Transform**](/uwp/api/Windows.UI.Xaml.Media.Transform)-derived class that enables you to express a transformation directly in terms of its 3×3 matrix: [**MatrixTransform**](/uwp/api/Windows.UI.Xaml.Media.MatrixTransform). **MatrixTransform** has a [**Matrix**](/uwp/api/windows.ui.xaml.media.matrixtransform.matrix) property, which holds a structure that has six properties: [**M11**](/uwp/api/windows.ui.xaml.media.matrix.m11), [**M12**](/uwp/api/windows.ui.xaml.media.matrix.m12), [**M21**](/uwp/api/windows.ui.xaml.media.matrix.m21), [**M22**](/uwp/api/windows.ui.xaml.media.matrix.m22), [**OffsetX**](/uwp/api/windows.ui.xaml.media.matrix.offsetx) and [**OffsetY**](/uwp/api/windows.ui.xaml.media.matrix.offsety). Each [**Matrix**](/uwp/api/Windows.UI.Xaml.Media.Matrix) property uses a **Double** value and corresponds to the six relevant values (columns 1 and 2) of an affine transformation matrix.

|           Column 1                                  |         Column 2                                    | Column 3 |
|---------------------------------------------|---------------------------------------------|-----|
| [**M11**](/uwp/api/windows.ui.xaml.media.matrix.m11)         | [**M12**](/uwp/api/windows.ui.xaml.media.matrix.m12)         | 0   |
| [**M21**](/uwp/api/windows.ui.xaml.media.matrix.m21)         | [**M22**](/uwp/api/windows.ui.xaml.media.matrix.m22)         | 0   |
| [**OffsetX**](/uwp/api/windows.ui.xaml.media.matrix.offsetx) | [**OffsetY**](/uwp/api/windows.ui.xaml.media.matrix.offsety) | 1   |

 

Any transform that you could describe with a [**TranslateTransform**](/uwp/api/Windows.UI.Xaml.Media.TranslateTransform), [**ScaleTransform**](/uwp/api/Windows.UI.Xaml.Media.ScaleTransform), [**RotateTransform**](/uwp/api/Windows.UI.Xaml.Media.RotateTransform), or [**SkewTransform**](/uwp/api/Windows.UI.Xaml.Media.SkewTransform) object could be described equally by a [**MatrixTransform**](/uwp/api/Windows.UI.Xaml.Media.MatrixTransform) with a [**Matrix**](/uwp/api/Windows.UI.Xaml.Media.Matrix) value. But you typically just use **TranslateTransform** and the others because the properties on those transform classes are easier to conceptualize than setting the vector components in a **Matrix**. It's also easier to animate the discrete properties of transforms; a **Matrix** is actually a structure and not a [**DependencyObject**](/uwp/api/Windows.UI.Xaml.DependencyObject), so it can't support animated individual values.

Some XAML design tools that enable you to apply transformation operations will serialize the results as a [**MatrixTransform**](/uwp/api/Windows.UI.Xaml.Media.MatrixTransform). In this case it may be best to use the same design tool again to change the transformation effect and serialize the XAML again, rather than trying to manipulate the [**Matrix**](/uwp/api/Windows.UI.Xaml.Media.Matrix) values yourself directly in the XAML.

## <span id="3-D_transforms"></span><span id="3-d_transforms"></span><span id="3-D_TRANSFORMS"></span>3-D transforms

In Windows 10, XAML introduced a new property, [**UIElement.Transform3D**](/uwp/api/windows.ui.xaml.uielement.transform3d), that can be used to create 3D effects with UI. To do this, use [**PerspectiveTransform3D**](/uwp/api/windows.ui.xaml.media.media3d.perspectivetransform3d) to add a shared 3D perspective or "camera" to your scene, and then use [**CompositeTransform3D**](/uwp/api/windows.ui.xaml.media.media3d.compositetransform3d) to transform an element in 3D space, like you would use [**CompositeTransform**](/uwp/api/Windows.UI.Xaml.Media.CompositeTransform). See [**UIElement.Transform3D**](/uwp/api/windows.ui.xaml.uielement.transform3d) for a discussion of how to implement 3D transforms.

 For simpler 3D effects that only apply to a single object, the [**UIElement.Projection**](/uwp/api/windows.ui.xaml.uielement.projection) property can be used. Using a [**PlaneProjection**](/uwp/api/Windows.UI.Xaml.Media.PlaneProjection) as the value for this property is equivalent to applying a fixed perspective transform and one or more 3D transforms to the element. This type of transform is described in more detail in [3-D perspective effects for XAML UI](3-d-perspective-effects.md).

## <span id="related_topics"></span>Related topics

* [**UIElement.Transform3D**](/uwp/api/windows.ui.xaml.uielement.transform3d)
* [3-D perspective effects for XAML UI](3-d-perspective-effects.md)
* [**Transform**](/uwp/api/Windows.UI.Xaml.Media.Transform)
 

