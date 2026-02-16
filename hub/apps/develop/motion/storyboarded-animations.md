---
ms.assetid: 0CBCEEA0-2B0E-44A1-A09A-F7A939632F3A
title: Storyboarded animations
description: Learn how to use storyboarded animations to change the value of a dependency property as a function of time.
ms.date: 10/31/2025
ms.topic: article
ms.localizationpriority: medium
---
# Storyboarded animations

Storyboarded animations are not just animations in the visual sense. A storyboarded animation is a way to change the value of a dependency property as a function of time. One of the main reasons you might need a storyboarded animation that's not from the animation library is to define the visual state for a control, as part of a control template or page definition.

## Defining storyboarded animations

A storyboarded animation is a way to change the value of a dependency property as a function of time. The property you are animating is not always a property that directly affects the UI of your app. But since XAML is about defining UI for an app, usually it is a UI-related property you are animating. For example, you can animate the angle of a [**RotateTransform**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.RotateTransform), or the color value of a button's background.

One of the main reasons you might be defining a storyboarded animation is if you are a control author or are re-templating a control, and you are defining visual states. For more info, see [Storyboarded animations for visual states](/previous-versions/windows/apps/jj819808(v=win.10)).

Whether you are defining visual states or a custom animation for an app, the concepts and APIs for storyboarded animations that are described in this topic mostly apply to either.

In order to be animated, the property you are targeting with a storyboarded animation must be a *dependency property*. A dependency property is a key feature of the Windows Runtime XAML implementation. The writeable properties of most common UI elements are typically implemented as dependency properties, so that you can animate them, apply data-bound values, or apply a [**Style**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Style) and target the property with a [**Setter**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Setter). For more info about how dependency properties work, see [Dependency properties overview](/windows/apps/develop/platform/xaml/dependency-properties-overview).

Most of the time, you define a storyboarded animation by writing XAML. If you use a tool such as Microsoft Visual Studio, it will produce the XAML for you. It's possible to define a storyboarded animation using code too, but that's less common.

Let's look at a simple example. In this XAML example, the [**Opacity**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.UIElement.Opacity) property is animated on a particular [**Rectangle**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Shapes.Rectangle) object.

```xaml
<Page ...>
  <Page.Resources>
    <!-- Storyboard resource: Animates a rectangle's opacity. -->
    <Storyboard x:Name="myStoryboard">
      <DoubleAnimation
        Storyboard.TargetName="MyAnimatedRectangle"
        Storyboard.TargetProperty="Opacity"
        From="1.0" To="0.0" Duration="0:0:1"/>
    </Storyboard>
  </Page.Resources>

  <!--Page root element, UI definition-->
  <Grid>
    <Rectangle x:Name="MyAnimatedRectangle"
      Width="300" Height="200" Fill="Blue"/>
  </Grid>
</Page>
```

### Identifying the object to animate

In the previous example, the storyboard was animating the [**Opacity**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.UIElement.Opacity) property of a [**Rectangle**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Shapes.Rectangle). You don't declare the animations on the object itself. Instead, you do this within the animation definition of a storyboard. Storyboards are usually defined in XAML that's not in the immediate vicinity of the XAML UI definition of the object to animate. Instead, they're usually set up as a XAML resource.

To connect an animation to a target, you reference the target by its identifying programming name. You should always apply the [x:Name attribute](/windows/apps/develop/platform/xaml/x-name-attribute) in the XAML UI definition to name the object that you want to animate. You then target the object to animate by setting [**Storyboard.TargetName**](/dotnet/api/system.windows.media.animation.storyboard.targetname) within the animation definition. For the value of **Storyboard.TargetName**, you use the name string of the target object, which is what you set earlier and elsewhere with x:Name attribute.

### Targeting the dependency property to animate

You set a value for [**Storyboard.TargetProperty**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.storyboard.targetproperty) in the animation. This determines which specific property of the targeted object is animated.

Sometimes you need to target a property that's not an immediate property of the target object, but that is nested more deeply in an object-property relationship. You often need to do this in order to drill down into a set of contributing object and property values until you can reference a property type that can be animated ([**Double**](/dotnet/api/system.double), [**Point**](/uwp/api/Windows.Foundation.Point), [**Color**](/uwp/api/Windows.UI.Color)). This concept is called *indirect targeting*, and the syntax for targeting a property in this way is known as a *property path*.

Here's an example. One common scenario for a storyboarded animation is to change the color of a part of an app UI or control in order to represent that the control is in a particular state. Say you want to animate the [**Foreground**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.textblock.foreground) of a [**TextBlock**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.TextBlock), so that it turns from red to green. You'd expect that a [**ColorAnimation**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.ColorAnimation) is involved, and that's correct. However, none of the properties on UI elements that affect the object's color are actually of type [**Color**](/uwp/api/Windows.UI.Color). Instead, they're of type [**Brush**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Brush). So what you actually need to target for animation is the [**Color**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.SolidColorBrush.Color) property of the [**SolidColorBrush**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.SolidColorBrush) class, which is a **Brush**-derived type that's typically used for these color-related UI properties. And here's what that looks like in terms of forming a property path for your animation's property targeting:

```xaml
<Storyboard x:Name="myStoryboard">
  <ColorAnimation
    Storyboard.TargetName="tb1"
    Storyboard.TargetProperty="(TextBlock.Foreground).(SolidColorBrush.Color)"
    From="Red" To="Green"/>
</Storyboard>
```

Here's how to think of this syntax in terms of its parts:

- Each set of () parentheses encloses a property name.
- Within the property name, there's a dot, and that dot separates a type name and a property name, so that the property you're identifying is unambiguous.
- The dot in the middle, the one that's not inside parentheses, is a step. This is interpreted by the syntax to mean, take the value of the first property (which is an object), step into its object model, and target a specific sub-property of the first property's value.

Here's a list of animation targeting scenarios where you'll probably be using indirect property targeting, and some property path strings that approximates the syntax you'll use:

- Animating the [**X**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.translatetransform.x) value of a [**TranslateTransform**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.TranslateTransform), as applied to a [**RenderTransform**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.rendertransform): `(UIElement.RenderTransform).(TranslateTransform.X)`
- Animating a [**Color**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.SolidColorBrush.Color) within a [**GradientStop**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.GradientStop) of a [**LinearGradientBrush**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.LinearGradientBrush), as applied to a [**Fill**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Shapes.Shape.Fill): `(Shape.Fill).(GradientBrush.GradientStops)[0].(GradientStop.Color)`
- Animating the [**X**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.translatetransform.x) value of a [**TranslateTransform**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.TranslateTransform), which is 1 of 4 transforms in a [**TransformGroup**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.TransformGroup), as applied to a [**RenderTransform**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.rendertransform):`(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)`

You'll notice some of these examples use square brackets around numbers. This is an indexer. It indicates that the property name preceding it has a collection as value, and that you want an item (as identified by a zero-based index) from within that collection.

You can also animate XAML attached properties. Always enclose the full attached property name in parentheses, for example `(Canvas.Left)`. For more info, see [Animating XAML attached properties](./storyboarded-animations.md#animating-xaml-attached-properties).

For more info on how to use a property path for indirect targeting of the property to animate, see [Property-path syntax](/windows/apps/develop/platform/xaml/property-path-syntax).

### Animation types

The Windows Runtime animation system has three specific types that storyboarded animations can apply to:

- [**Double**](/dotnet/api/system.double), can be animated with any [**DoubleAnimation**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.DoubleAnimation)
- [**Point**](/uwp/api/Windows.Foundation.Point), can be animated with any [**PointAnimation**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.PointAnimation)
- [**Color**](/uwp/api/Windows.UI.Color), can be animated with any [**ColorAnimation**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.ColorAnimation)

There's also a generalized [**Object**](/dotnet/api/system.object) animation type for object reference values, which we'll discuss later.

### Specifying the animated values

So far we've shown you how to target the object and the property to animate, but haven't yet described what the animation does to the property value when it runs.

The animation types we've described are sometimes referred to as **From**/**To**/**By** animations. This means that the animation is changing the value of a property, over time, using one or more of these inputs that come from the animation definition:

- The value starts at the **From** value. If you don't specify a **From** value, the starting value is whatever value the animated property has at the time before the animation runs. This might be a default value, a value from a style or template, or a value specifically applied by a XAML UI definition or app code.
- At the end of the animation, the value is the **To** value.
- Or, to specify an ending value relative to the starting value, set the **By** property. You'd set this instead of the **To** property.
- If you don't specify a **To** value or a **By** value, the ending value is whatever value the animated property has at the time before the animation runs. In this case you'd better have a **From** value because otherwise the animation won't change the value at all; its starting and ending values are both the same.
- An animation typically has at least one of **From**, **By** or **To** but never all three.

Let's revisit the earlier XAML example and look again at the **From** and **To** values, and the **Duration**. The example is animating the [**Opacity**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.UIElement.Opacity) property, and the property type of **Opacity** is [**Double**](/dotnet/api/system.double). So the animation to use here is [**DoubleAnimation**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.DoubleAnimation).

`From="1.0" To="0.0"` specifies that when the animation runs, the [**Opacity**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.UIElement.Opacity) property starts at a value of 1 and animates to 0. In other words, in terms of what these [**Double**](/dotnet/api/system.double) values mean to the **Opacity** property, this animation will cause the object to start opaque and then fade to transparent.

```xaml
...
<Storyboard x:Name="myStoryboard">
  <DoubleAnimation
    Storyboard.TargetName="MyAnimatedRectangle"
    Storyboard.TargetProperty="Opacity"
    From="1.0" To="0.0" Duration="0:0:1"/>
</Storyboard>
...
```

`Duration="0:0:1"` specifies how long the animation lasts, that is, how fast the rectangle fades. A [**Duration**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.duration) property is specified in the form of *hours*:*minutes*:*seconds*. The time duration in this example is one second.

For more info about [**Duration**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Duration) values and the XAML syntax, see [**Duration**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Duration).

> [!NOTE]
> For the example we showed, if you were sure that the starting state of the object being animated has [**Opacity**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.UIElement.Opacity) always equal to 1, either through the default or an explicit set, you could omit the **From** value, the animation would use the implicit starting value, and the result would be the same.

### From/To/By are nullable

We mentioned previously that you can omit **From**, **To** or **By** and thus use current non-animated values as substitutes for a missing value. **From**, **To**, or **By** properties of an animation aren't of the type you might guess. For example the type of the [**DoubleAnimation.To**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.doubleanimation.easingfunction) property isn't [**Double**](/dotnet/api/system.double). Instead, it's a [**Nullable**](/dotnet/api/system.nullable-1) for **Double**. And its default value is **null**, not 0. That **null** value is how the animation system distinguishes that you haven't specifically set a value for a **From**, **To**, or **By** property.

### Other properties of an animation

The next properties described in this section are all optional in that they have defaults that are appropriate for most animations.

### **AutoReverse**

If you don't specify either [**AutoReverse**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.autoreverse) or [**RepeatBehavior**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.repeatbehavior) on an animation, that animation will run once, and run for the time specified as the [**Duration**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.duration).

The [**AutoReverse**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.autoreverse) property specifies whether a timeline plays in reverse after it reaches the end of its [**Duration**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.duration). If you set it to **true**, the animation reverses after it reaches the end of its declared [**Duration**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.duration), changing the value from its ending value (**To**) back to its starting value (**From**). This means that the animation effectively runs for double the time of its [**Duration**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.duration).

### **RepeatBehavior**

The [**RepeatBehavior**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.repeatbehavior) property specifies either how many times a timeline plays, or a larger duration that the timeline should repeat within. By default, a timeline has an iteration count of "1x", which means it plays one time for its [**Duration**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.duration) and does not repeat.

You can cause the animation to run multiple iterations. For example, a value of "3x" causes the animation to run three times. Or, you can specify a different [**Duration**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Duration) for [**RepeatBehavior**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.repeatbehavior). That **Duration** should be longer than the **Duration** of the animation itself to be effective. For example, if you specify a **RepeatBehavior** of "0:0:10", for an animation that has a [**Duration**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.duration) of "0:0:2", that animation repeats five times. If these don't divide evenly, the animation gets truncated at the time that the **RepeatBehavior** time is reached, which might be partway through. Finally, you can specify the special value "Forever", which causes the animation to run infinitely until it's deliberately stopped.

For more info about [**RepeatBehavior**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.RepeatBehavior) values and the XAML syntax, see [**RepeatBehavior**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.RepeatBehavior).

### **FillBehavior="Stop"**

By default, when an animation ends, the animation leaves the property value as the final **To** or **By**-modified value even after its duration is surpassed. However, if you set the value of the [**FillBehavior**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.fillbehavior) property to [**FillBehavior.Stop**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.FillBehavior), the value of the animated value reverts to whatever the value was before the animation was applied, or more precisely to the current effective value as determined by the dependency property system (for more info on this distinction, see [Dependency properties overview](/windows/apps/develop/platform/xaml/dependency-properties-overview)).

### **BeginTime**

By default, the [**BeginTime**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.begintime) of an animation is "0:0:0", so it begins as soon as its containing [**Storyboard**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.Storyboard) runs. You might change this if the **Storyboard** contains more than one animation and you want to stagger the start times of the others versus an initial animation, or to create a deliberate short delay.

### **SpeedRatio**

If you have more than one animation in a [**Storyboard**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.Storyboard) you can change the time rate of one or more of the animations relative to the **Storyboard**. It's the parent **Storyboard** that ultimately controls how the [**Duration**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Duration) time elapses while the animations run. This property isn't used very often. For more info see [**SpeedRatio**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.speedratio).

## Defining more than one animation in a **Storyboard**

The contents of a [**Storyboard**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.Storyboard) can be more than one animation definition. You might have more than one animation if you are applying related animations to two properties of the same target object. For example, you might change both the [**TranslateX**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.compositetransform.translatex) and [**TranslateY**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.compositetransform.translatey) properties of a [**TranslateTransform**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.TranslateTransform) used as the [**RenderTransform**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.rendertransform) of a UI element; this will cause the element to translate diagonally. You need two different animations to accomplish that, but you might want the animations to be part of the same **Storyboard** because you always want those two animations to be run together.

The animations don't have to be the same type, or target the same object. They can have different durations, and don't have to share any property values.

When the parent [**Storyboard**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.Storyboard) runs, each of the animations within will run too.

The [**Storyboard**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.Storyboard) class actually has a lot of the same animation properties as the animation types do, because both share the [**Timeline**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.Timeline) base class. Thus, a **Storyboard** can have a [**RepeatBehavior**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.repeatbehavior), or a [**BeginTime**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.begintime). You don't usually set these on a **Storyboard** though unless you want all the contained animations to have that behavior. As a general rule, any **Timeline** property as set on a **Storyboard** applies to all its child animations. If let unset, the **Storyboard** has an implicit duration that's calculated from the longest [**Duration**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Duration) value of the contained animations. An explicitly set [**Duration**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.duration) on a **Storyboard** that's shorter than one of its child animations will cause that animation to get cut off, which isn't usually desirable.

A storyboard can't contain two animations that attempt to target and animate the same property on the same object. If you try this, you'll get a runtime error when the storyboard tries to run. This restriction applies even if the animations don't overlap in time because of deliberately different [**BeginTime**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.begintime) values and durations. If you really want to apply a more complex animation timeline to the same property in a single storyboard, the way to do this is to use a key-frame animation. See [Key-frame and easing function animations](key-frame-and-easing-function-animations.md).

The animation system can apply more than one animation to the value of a property, if those inputs come from multiple storyboards. Using this behavior deliberately for simultaneously running storyboards isn't common. However it's possible that an app-defined animation that you apply to a control property will be modifying the **HoldEnd** value of an animation that was previously run as part of the control's visual state model.

## Defining a storyboard as a resource

A [**Storyboard**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.Storyboard) is the container that you put animation objects in. You typically define the **Storyboard** as a resource that is available to the object that you want to animate, either in page-level [**Resources**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.frameworkelement.resources) or [**Application.Resources**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.resources).

This next example shows how the previous example [**Storyboard**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.Storyboard) would be contained in a page-level [**Resources**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.frameworkelement.resources) definition, where the **Storyboard** is a keyed resource of the root [**Page**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Controls.Page). Note the [x:Name attribute](/windows/apps/develop/platform/xaml/x-name-attribute). This attribute is how you define a variable name for the **Storyboard**, so that other elements in XAML as well as code can refer to the **Storyboard** later.

```xaml
<Page ...>
  <Page.Resources>
    <!-- Storyboard resource: Animates a rectangle's opacity. -->
    <Storyboard x:Name="myStoryboard">
      <DoubleAnimation
        Storyboard.TargetName="MyAnimatedRectangle"
        Storyboard.TargetProperty="Opacity"
        From="1.0" To="0.0" Duration="0:0:1"/>
    </Storyboard>
  </Page.Resources>
  <!--Page root element, UI definition-->
  <Grid>
    <Rectangle x:Name="MyAnimatedRectangle"
      Width="300" Height="200" Fill="Blue"/>
  </Grid>
</Page>
```

Defining resources at the XAML root of a XAML file such as page.xaml or app.xaml is a common practice for how to organize keyed resources in your XAML. You also can factor resources into separate files and merge them into apps or pages. For more info, see [ResourceDictionary and XAML resource references](../../develop/platform/xaml/xaml-resource-dictionary.md).

> [!NOTE]
> Windows Runtime XAML supports identifying resources either using the [x:Key attribute](/windows/apps/develop/platform/xaml/x-key-attribute) or the [x:Name attribute](/windows/apps/develop/platform/xaml/x-name-attribute). Using x:Name attribute is more common for a [**Storyboard**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.Storyboard), because you'll want to reference it by variable name eventually, so that you can call its [**Begin**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.storyboard.begin) method and run the animations. If you do use [x:Key attribute](/windows/apps/develop/platform/xaml/x-key-attribute), you'll need to use [**ResourceDictionary**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.ResourceDictionary) methods such as the [**Item**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.resourcedictionary) indexer to retrieve it as a keyed resource and then cast the retrieved object to **Storyboard** to use the **Storyboard** methods.

### Storyboards for visual states

You also put your animations within a [**Storyboard**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.Storyboard) unit when you are declaring the visual state animations for a control's visual appearance. In that case the **Storyboard** elements you define go into a [**VisualState**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.VisualState) container that's nested more deeply in a [**Style**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Style) (it's the **Style** that is the keyed resource). You don't need a key or name for your **Storyboard** in this case because it's the **VisualState** that has a target name that the [**VisualStateManager**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.visualstatemanager) can invoke. The styles for controls are often factored into separate XAML [**ResourceDictionary**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.ResourceDictionary) files rather than placed in a page or app **Resources** collection. For more info, see [Storyboarded animations for visual states](/previous-versions/windows/apps/jj819808(v=win.10)).

## Dependent and independent animations

At this point we need to introduce some important points about how the animation system works. In particular, animation interacts fundamentally with how a Windows Runtime app renders to the screen, and how that rendering uses processing threads. A Windows Runtime app always has a main UI thread, and this thread is responsible for updating the screen with current information. In addition, a Windows Runtime app has a composition thread, which is used for precalculating layouts immediately before they are shown. When you animate the UI, there's potential to cause a lot of work for the UI thread. The system must redraw large areas of the screen using fairly short time intervals between each refresh. This is necessary for capturing the latest property value of the animated property. If you're not careful, there's risk that an animation can make the UI less responsive, or will impact performance of other app features that are also on the same UI thread.

The variety of animation that is determined to have some risk of slowing down the UI thread is called a *dependent animation*. An animation not subject to this risk is an *independent animation*. The distinction between dependent and independent animations isn't just determined by animation types ([**DoubleAnimation**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.DoubleAnimation) and so on) as we described earlier. Instead, it's determined by which specific properties you are animating, and other factors like inheritance and composition of controls. There are circumstances where even if an animation does change UI, the animation can have minimal impact to the UI thread, and can instead be handled by the composition thread as an independent animation.

An animation is independent if it has any of these characteristics:

- The [**Duration**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.duration) of the animation is 0 seconds (see Warning)
- The animation targets [**UIElement.Opacity**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.UIElement.Opacity)
- The animation targets a sub-property value of these [**UIElement**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.UIElement) properties: [**Transform3D**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.transform3d), [**RenderTransform**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.rendertransform), [**Projection**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.projection), [**Clip**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.clip)
- The animation targets [**Canvas.Left**](/dotnet/api/system.windows.controls.canvas.left) or [**Canvas.Top**](/dotnet/api/system.windows.controls.canvas.top)
- The animation targets a [**Brush**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Brush) value and uses a [**SolidColorBrush**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.SolidColorBrush), animating its [**Color**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.SolidColorBrush.Color)
- The animation is an [**ObjectAnimationUsingKeyFrames**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.ObjectAnimationUsingKeyFrames)

> [!WARNING]
> In order for your animation to be treated as independent, you must explicitly set `Duration="0"`. For example, if you remove `Duration="0"` from this XAML, the animation is treated as dependent, even though the [**KeyTime**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.doublekeyframe.keytime) of the frame is "0:0:0".

```xaml
<Storyboard>
  <DoubleAnimationUsingKeyFrames
    Duration="0"
    Storyboard.TargetName="Button2"
    Storyboard.TargetProperty="Width">
    <DiscreteDoubleKeyFrame KeyTime="0:0:0" Value="200"/>
  </DoubleAnimationUsingKeyFrames>
</Storyboard>
```

If your animation doesn't meet these criteria, it's probably a dependent animation. By default, the animation system won't run a dependent animation. So during the process of developing and testing, you might not even be seeing your animation running. You can still use this animation, but you must specifically enable each such dependent animation. To enable your animation, set the **EnableDependentAnimation** property of the animation object to **true**. (Each [**Timeline**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.Timeline) subclass that represents an animation has a different implementation of the property but they're all named `EnableDependentAnimation`.)

The requirement of enabling dependent animations falling onto the app developer is a conscious design aspect of the animation system and the development experience. We want developers to be aware that animations do have a performance cost for the responsiveness of your UI. Poorly performing animations are difficult to isolate and debug in a full-scale app. So it's better to turn on only the dependent animations you really need for your app's UI experience. We didn't want to make it too easy to compromise your app's performance because of decorative animations that use a lot of cycles. For more info on performance tips for animation, see [Optimize animations and media](/windows/uwp/debug-test-perf/optimize-animations-and-media).

As an app developer, you can also choose to apply an app-wide setting that always disables dependent animations, even those where **EnableDependentAnimation** is **true**. See [**Timeline.AllowDependentAnimations**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.allowdependentanimations).

> [!TIP]
> If you're using the Animation Pane in Blend for Visual Studio 2019, whenever you attempt to apply a dependent animation to a visual state property, warnings will be displayed in the designer.
> Warnings will not show in the build output or Error List.
> If you're editing XAML by hand, the designer will not show a warning.
> At runtime when debugging, the Output pane's Debug output will show a warning that the animation is not independent and will be skipped.


## Starting and controlling an animation

Everything we've shown you so far doesn't actually cause an animation to run or be applied! Until the animation is started and is running, the value changes that an animation is declaring in XAML are latent and won't happen yet. You must explicitly start an animation in some way that's related to the app lifetime or the user experience. At the simplest level, you start an animation by calling the [**Begin**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.storyboard.begin) method on the [**Storyboard**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.Storyboard) that's the parent for that animation. You can't call methods from XAML directly, so whatever you do to enable your animations, you'll be doing it from code. That will either be the code-behind for the pages or components of your app, or perhaps the logic of your control if you're defining a custom control class.

Typically, you'll call [**Begin**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.storyboard.begin) and just let the animation run to its duration completion. However, you can also use [**Pause**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.storyboard.pause), [**Resume**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.storyboard.resume) and [**Stop**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.storyboard.stop) methods to control the [**Storyboard**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.Storyboard) at run-time, as well as other APIs that are used for more advanced animation control scenarios.

When you call [**Begin**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.storyboard.begin) on a storyboard that contains an animation that repeats infinitely (`RepeatBehavior="Forever"`), that animation runs until the page containing it is unloaded, or you specifically call [**Pause**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.storyboard.pause) or [**Stop**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.storyboard.stop).

### Starting an animation from app code

You can either start animations automatically, or in response to user actions. For the automatic case, you typically use an object lifetime event such as [**Loaded**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.frameworkelement.loaded) to act as the animation trigger. The **Loaded** event is a good event to use for this because at that point the UI is ready for interaction, and the animation won't be cut off at the beginning because another part of UI was still loading.

In this example, the [**PointerPressed**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.uielement.pointerpressed) event is attached to the rectangle so that when the user clicks the rectangle, the animation begins.

```xaml
<Rectangle PointerPressed="Rectangle_Tapped"
  x:Name="MyAnimatedRectangle"
  Width="300" Height="200" Fill="Blue"/>
```

The event handler start the [**Storyboard**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.Storyboard) (the animation) by using the [**Begin**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.storyboard.begin) method of the **Storyboard**.

```csharp
myStoryboard.Begin();
```

```cppwinrt
myStoryboard().Begin();
```

You can handle the [**Completed**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.completed) event if you want other logic to run after the animation has finished applying values. Also, for troubleshooting property system/animation interactions, the [**GetAnimationBaseValue**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.dependencyobject.getanimationbasevalue) method can be useful.

> [!TIP]
> Whenever you are coding for an app scenario where you are starting an animation from app code, you might want to review again whether an animation or transition already exists in the animation library for your UI scenario. The library animations enable a more consistent UI experience across all Windows Runtime apps, and are easier to use.

### Animations for visual states

The run behavior for a [**Storyboard**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.Storyboard) that's used to define a control's visual state is different from how an app might run a storyboard directly. As applied to a visual state definition in XAML, the **Storyboard** is an element of a containing [**VisualState**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.VisualState), and the state as a whole is controlled by using the [**VisualStateManager**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.visualstatemanager) API. Any animations within will run according to their animation values and [**Timeline**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.Timeline) properties when the containing **VisualState** is used by a control. For more info, see [Storyboards for visual states](/previous-versions/windows/apps/jj819808(v=win.10)). For visual states, the apparent [**FillBehavior**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.timeline.fillbehavior) is different. If a visual state is changed to another state, all the property changes applied by the previous visual state and its animations are canceled, even if the new visual state doesn't specifically apply a new animation to a property.

### **Storyboard** and **EventTrigger**

There is one way to start an animation that can be declared entirely in XAML. However, this technique isn't widely used anymore. It's a legacy syntax from WPF and early versions of Silverlight prior to [**VisualStateManager**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.visualstatemanager) support. This [**EventTrigger**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.EventTrigger) syntax still works in Windows Runtime XAML for import/compatibility reasons, but only works for a trigger behavior based on the [**FrameworkElement.Loaded**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.frameworkelement.loaded) event; attempting to trigger off other events will throw exceptions or fail to compile. For more info, see [**EventTrigger**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.EventTrigger) or [**BeginStoryboard**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.BeginStoryboard).

## Animating XAML attached properties

It's not a common scenario, but you can apply an animated value to a XAML attached property. For more info on what attached properties are and how they work, see [Attached properties overview](/windows/apps/develop/platform/xaml/attached-properties-overview). Targeting an attached property requires a [property-path syntax](/windows/apps/develop/platform/xaml/property-path-syntax) that encloses the property name in parentheses. You can animate the built-in attached properties such as [**Canvas.ZIndex**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.canvas.zindex) by using an [**ObjectAnimationUsingKeyFrames**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.ObjectAnimationUsingKeyFrames) that applies discrete integer values. However, an existing limitation of the Windows Runtime XAML implementation is that you cannot animate a custom attached property.

## More animation types, and next steps for learning about animating your UI

Up to now, we've shown the custom animations that are animating between two values, and then linearly interpolating the values as necessary while the animation runs. These are called **From**/**To**/**By** animations. But there's another animation type that enables you to declare intermediate values that fall between the start and end. These are called *key-frame animations*. There's also a way to alter the interpolation logic on either a **From**/**To**/**By** animation or a key-frame animation. This involves applying an easing function. For more info on these concepts, see [Key-frame and easing function animations](key-frame-and-easing-function-animations.md).

## Differences with WPF

If you are familiar with Windows Presentation Foundation (WPF), read this section; otherwise, you can skip it.

In general, creating storyboarded animations in a Windows Runtime app is like WPF. But there are a number of important differences:

- Storyboarded animations are not the only way to visually animate a UI, nor are they necessarily the easiest way for app developers to do so. Rather than using storyboarded animations it's often a better design practice to use theme animations and transition animations. These can quickly create recommended UI animations without getting into the intricacies of animation property targeting. For more info see [Animations in XAML](xaml-animation.md).
- In the Windows Runtime, many XAML controls include theme animations and transition animations as part of their built-in behavior. For the most part, WPF controls didn't have a default animation behavior.
- Not all custom animations you create can run by default in a Windows Runtime app, if the animation system determines that the animation might cause bad performance in your UI. Animations where the system determines there could be a performance impact are called *dependent animations*. It's dependent because the clocking of your animation is directly working against the UI thread, which is also where active user input and other updates are trying to apply the runtime changes to UI. A dependent animation that's consuming extensive system resources on the UI thread can make the app appear unresponsive in certain situations. If your animation causes a layout change or otherwise has the potential to impact performance on the UI thread, you often need to explicitly enable the animation to see it run. That's what the **EnableDependentAnimation** property on specific animation classes is for. See [Dependent and independent animations](./storyboarded-animations.md#dependent-and-independent-animations) for more info.
- Custom easing functions are not currently supported in the Windows Runtime.

## Related topics

- [Property-path syntax](/windows/apps/develop/platform/xaml/property-path-syntax)
- [Dependency properties overview](/windows/apps/develop/platform/xaml/dependency-properties-overview)
- [Key-frame and easing function animations](key-frame-and-easing-function-animations.md)
- [Storyboarded animations for visual states](/previous-versions/windows/apps/jj819808(v=win.10))
- [Control templates](../../develop/platform/xaml/xaml-control-templates.md)
- [**Storyboard**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.Media.Animation.Storyboard)
- [**Storyboard.TargetProperty**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.storyboard.targetproperty)
