---
author: Jwmsft
ms.assetid: 0CBCEEA0-2B0E-44A1-A09A-F7A939632F3A
title: Storyboarded animations
description: Storyboarded animations are not just animations in the visual sense.
ms.author: jimwalk
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Storyboarded animations



Storyboarded animations are not just animations in the visual sense. A storyboarded animation is a way to change the value of a dependency property as a function of time. One of the main reasons you might need a storyboarded animation that's not from the animation library is to define the visual state for a control, as part of a control template or page definition.

## Differences with Silverlight and WPF

If you are familiar with Microsoft Silverlight or Windows Presentation Foundation (WPF), read this section; otherwise, you can skip it.

In general, creating storyboarded animations in a Windows Runtime app is like Silverlight or WPF. But there are a number of important differences:

-   Storyboarded animations are not the only way to visually animate a UI, nor are they necessarily the easiest way for app developers to do so. Rather than using storyboarded animations it's often a better design practice to use theme animations and transition animations. These can quickly create recommended UI animations without getting into the intricacies of animation property targeting. For more info see [Animations overview](xaml-animation.md).
-   In the Windows Runtime, many XAML controls include theme animations and transition animations as part of their built-in behavior. For the most part, WPF and Silverlight controls didn't have a default animation behavior.
-   Not all custom animations you create can run by default in a Windows Runtime app, if the animation system determines that the animation might cause bad performance in your UI. Animations where the system determines there could be a performance impact are called *dependent animations*. It's dependent because the clocking of your animation is directly working against the UI thread, which is also where active user input and other updates are trying to apply the runtime changes to UI. A dependent animation that's consuming extensive system resources on the UI thread can make the app appear unresponsive in certain situations. If your animation causes a layout change or otherwise has the potential to impact performance on the UI thread, you often need to explicitly enable the animation to see it run. That's what the **EnableDependentAnimation** property on specific animation classes is for. See [Dependent and independent animations](./storyboarded-animations.md#dependent-and-independent-animations) for more info.
-   Custom easing functions are not currently supported in the Windows Runtime.

## Defining storyboarded animations

A storyboarded animation is a way to change the value of a dependency property as a function of time. The property you are animating is not always a property that directly affects the UI of your app. But since XAML is about defining UI for an app, usually it is a UI-related property you are animating. For example, you can animate the angle of a [**RotateTransform**](https://msdn.microsoft.com/library/windows/apps/BR242932), or the color value of a button's background.

One of the main reasons you might be defining a storyboarded animation is if you are a control author or are re-templating a control, and you are defining visual states. For more info, see [Storyboarded animations for visual states](https://msdn.microsoft.com/library/windows/apps/xaml/JJ819808).

Whether you are defining visual states or a custom animation for an app, the concepts and APIs for storyboarded animations that are described in this topic mostly apply to either.

In order to be animated, the property you are targeting with a storyboarded animation must be a *dependency property*. A dependency property is a key feature of the Windows Runtime XAML implementation. The writeable properties of most common UI elements are typically implemented as dependency properties, so that you can animate them, apply data-bound values, or apply a [**Style**](https://msdn.microsoft.com/library/windows/apps/BR208849) and target the property with a [**Setter**](https://msdn.microsoft.com/library/windows/apps/BR208817). For more info about how dependency properties work, see [Dependency properties overview](https://msdn.microsoft.com/library/windows/apps/Mt185583).

Most of the time, you define a storyboarded animation by writing XAML. If you use a tool such as Microsoft Visual Studio, it will produce the XAML for you. It's possible to define a storyboarded animation using code too, but that's less common.

Let's look at a simple example. In this XAML example, the [**Opacity**](/uwp/api/Windows.UI.Xaml.UIElement.Opacity) property is animated on a particular [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle) object.

```xaml
<!-- Animates the rectangle's opacity. -->
<Storyboard x:Name="myStoryboard">
  <DoubleAnimation
    Storyboard.TargetName="MyAnimatedRectangle"
    Storyboard.TargetProperty="Opacity"
    From="1.0" To="0.0" Duration="0:0:1"/>
</Storyboard>

<!-- A different area of the XAML. -->
<Rectangle x:Name="MyAnimatedRectangle"
  Width="300" Height="200" Fill="Blue"/>
```

### Identifying the object to animate

In the previous example, the storyboard was animating the [**Opacity**](/uwp/api/Windows.UI.Xaml.UIElement.Opacity) property of a [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle). You don't declare the animations on the object itself. Instead, you do this within the animation definition of a storyboard. Storyboards are usually defined in XAML that's not in the immediate vicinity of the XAML UI definition of the object to animate. Instead, they're usually set up as a XAML resource.

To connect an animation to a target, you reference the target by its identifying programming name. You should always apply the [x:Name attribute](https://msdn.microsoft.com/library/windows/apps/Mt204788) in the XAML UI definition to name the object that you want to animate. You then target the object to animate by setting [**Storyboard.TargetName**](https://msdn.microsoft.com/library/windows/apps/Hh759823) within the animation definition. For the value of **Storyboard.TargetName**, you use the name string of the target object, which is what you set earlier and elsewhere with x:Name attribute.

### Targeting the dependency property to animate

You set a value for [**Storyboard.TargetProperty**](https://msdn.microsoft.com/library/windows/apps/Hh759824) in the animation. This determines which specific property of the targeted object is animated.

Sometimes you need to target a property that's not an immediate property of the target object, but that is nested more deeply in an object-property relationship. You often need to do this in order to drill down into a set of contributing object and property values until you can reference a property type that can be animated ([**Double**](https://msdn.microsoft.com/library/windows/apps/xaml/system.double.aspx), [**Point**](https://msdn.microsoft.com/library/windows/apps/BR225870), [**Color**](https://msdn.microsoft.com/library/windows/apps/Hh673723)). This concept is called *indirect targeting*, and the syntax for targeting a property in this way is known as a *property path*.

Here's an example. One common scenario for a storyboarded animation is to change the color of a part of an app UI or control in order to represent that the control is in a particular state. Say you want to animate the [**Foreground**](https://msdn.microsoft.com/library/windows/apps/BR209665) of a [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/BR209652), so that it turns from red to green. You'd expect that a [**ColorAnimation**](https://msdn.microsoft.com/library/windows/apps/BR243066) is involved, and that's correct. However, none of the properties on UI elements that affect the object's color are actually of type [**Color**](https://msdn.microsoft.com/library/windows/apps/Hh673723). Instead, they're of type [**Brush**](/uwp/api/Windows.UI.Xaml.Media.Brush). So what you actually need to target for animation is the [**Color**](/uwp/api/Windows.UI.Xaml.Media.SolidColorBrush.Color) property of the [**SolidColorBrush**](https://msdn.microsoft.com/library/windows/apps/BR242962) class, which is a **Brush**-derived type that's typically used for these color-related UI properties. And here's what that looks like in terms of forming a property path for your animation's property targeting:

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

- Animating the [**X**](https://msdn.microsoft.com/library/windows/apps/BR243029) value of a [**TranslateTransform**](https://msdn.microsoft.com/library/windows/apps/BR243027), as applied to a [**RenderTransform**](https://msdn.microsoft.com/library/windows/apps/BR208980): `(UIElement.RenderTransform).(TranslateTransform.X)`
- Animating a [**Color**](/uwp/api/Windows.UI.Xaml.Media.SolidColorBrush.Color) within a [**GradientStop**](https://msdn.microsoft.com/library/windows/apps/BR210078) of a [**LinearGradientBrush**](https://msdn.microsoft.com/library/windows/apps/BR210108), as applied to a [**Fill**](/uwp/api/Windows.UI.Xaml.Shapes.Shape.Fill): `(Shape.Fill).(GradientBrush.GradientStops)[0].(GradientStop.Color)`
- Animating the [**X**](https://msdn.microsoft.com/library/windows/apps/BR243029) value of a [**TranslateTransform**](https://msdn.microsoft.com/library/windows/apps/BR243027), which is 1 of 4 transforms in a [**TransformGroup**](https://msdn.microsoft.com/library/windows/apps/BR243022), as applied to a [**RenderTransform**](https://msdn.microsoft.com/library/windows/apps/BR208980):`(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)`

You'll notice some of these examples use square brackets around numbers. This is an indexer. It indicates that the property name preceding it has a collection as value, and that you want an item (as identified by a zero-based index) from within that collection.

You can also animate XAML attached properties. Always enclose the full attached property name in parentheses, for example `(Canvas.Left)`. For more info, see [Animating XAML attached properties](./storyboarded-animations.md#animating-xaml-attached-properties).

For more info on how to use a property path for indirect targeting of the property to animate, see [Property-path syntax](https://msdn.microsoft.com/library/windows/apps/Mt185586) or [**Storyboard.TargetProperty attached property**](https://msdn.microsoft.com/library/windows/apps/Hh759824).

### Animation types

The Windows Runtime animation system has three specific types that storyboarded animations can apply to:

-   [**Double**](https://msdn.microsoft.com/library/windows/apps/xaml/system.double.aspx), can be animated with any [**DoubleAnimation**](https://msdn.microsoft.com/library/windows/apps/BR243136)
-   [**Point**](https://msdn.microsoft.com/library/windows/apps/BR225870), can be animated with any [**PointAnimation**](https://msdn.microsoft.com/library/windows/apps/BR210346)
-   [**Color**](https://msdn.microsoft.com/library/windows/apps/Hh673723), can be animated with any [**ColorAnimation**](https://msdn.microsoft.com/library/windows/apps/BR243066)

There's also a generalized [**Object**](https://msdn.microsoft.com/library/windows/apps/xaml/system.object.aspx) animation type for object reference values, which we'll discuss later.

### Specifying the animated values

So far we've shown you how to target the object and the property to animate, but haven't yet described what the animation does to the property value when it runs.

The animation types we've described are sometimes referred to as **From**/**To**/**By** animations. This means that the animation is changing the value of a property, over time, using one or more of these inputs that come from the animation definition:

-   The value starts at the **From** value. If you don't specify a **From** value, the starting value is whatever value the animated property has at the time before the animation runs. This might be a default value, a value from a style or template, or a value specifically applied by a XAML UI definition or app code.
-   At the end of the animation, the value is the **To** value.
-   Or, to specify an ending value relative to the starting value, set the **By** property. You'd set this instead of the **To** property.
-   If you don't specify a **To** value or a **By** value, the ending value is whatever value the animated property has at the time before the animation runs. In this case you'd better have a **From** value because otherwise the animation won't change the value at all; its starting and ending values are both the same.
-   An animation typically has at least one of **From**, **By** or **To** but never all three.

Let's revisit the earlier XAML example and look again at the **From** and **To** values, and the **Duration**. The example is animating the [**Opacity**](/uwp/api/Windows.UI.Xaml.UIElement.Opacity) property, and the property type of **Opacity** is [**Double**](https://msdn.microsoft.com/library/windows/apps/xaml/system.double.aspx). So the animation to use here is [**DoubleAnimation**](https://msdn.microsoft.com/library/windows/apps/BR243136).

`From="1.0" To="0.0"` specifies that when the animation runs, the [**Opacity**](/uwp/api/Windows.UI.Xaml.UIElement.Opacity) property starts at a value of 1 and animates to 0. In other words, in terms of what these [**Double**](https://msdn.microsoft.com/library/windows/apps/xaml/system.double.aspx) values mean to the **Opacity** property, this animation will cause the object to start opaque and then fade to transparent.

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

`Duration="0:0:1"` specifies how long the animation lasts, that is, how fast the rectangle fades. A [**Duration**](https://msdn.microsoft.com/library/windows/apps/BR243207) property is specified in the form of *hours*:*minutes*:*seconds*. The time duration in this example is one second.

For more info about [**Duration**](https://msdn.microsoft.com/library/windows/apps/BR242377) values and the XAML syntax, see [**Duration**](https://msdn.microsoft.com/library/windows/apps/BR242377).

> [!NOTE]
> For the example we showed, if you were sure that the starting state of the object being animated has [**Opacity**](/uwp/api/Windows.UI.Xaml.UIElement.Opacity) always equal to 1, either through the default or an explicit set, you could omit the **From** value, the animation would use the implicit starting value, and the result would be the same.

### From/To/By are nullable

We mentioned previously that you can omit **From**, **To** or **By** and thus use current non-animated values as substitutes for a missing value. **From**, **To**, or **By** properties of an animation aren't of the type you might guess. For example the type of the [**DoubleAnimation.To**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.doubleanimation.easingfunction.aspx) property isn't [**Double**](https://msdn.microsoft.com/library/windows/apps/xaml/system.double.aspx). Instead, it's a [**Nullable**](https://msdn.microsoft.com/library/windows/apps/xaml/b3h38hb0.aspx) for **Double**. And its default value is **null**, not 0. That **null** value is how the animation system distinguishes that you haven't specifically set a value for a **From**, **To**, or **By** property. Visual C++ component extensions (C++/CX) doesn't have a **Nullable** type, so it uses [**IReference**](https://msdn.microsoft.com/library/windows/apps/BR225864) instead.

### Other properties of an animation

The next properties described in this section are all optional in that they have defaults that are appropriate for most animations.

### **AutoReverse**

If you don't specify either [**AutoReverse**](https://msdn.microsoft.com/library/windows/apps/BR243202) or [**RepeatBehavior**](https://msdn.microsoft.com/library/windows/apps/BR243211) on an animation, that animation will run once, and run for the time specified as the [**Duration**](https://msdn.microsoft.com/library/windows/apps/BR243207).

The [**AutoReverse**](https://msdn.microsoft.com/library/windows/apps/BR243202) property specifies whether a timeline plays in reverse after it reaches the end of its [**Duration**](https://msdn.microsoft.com/library/windows/apps/BR243207). If you set it to **true**, the animation reverses after it reaches the end of its declared [**Duration**](https://msdn.microsoft.com/library/windows/apps/BR243207), changing the value from its ending value (**To**) back to its starting value (**From**). This means that the animation effectively runs for double the time of its [**Duration**](https://msdn.microsoft.com/library/windows/apps/BR243207).

### **RepeatBehavior**

The [**RepeatBehavior**](https://msdn.microsoft.com/library/windows/apps/BR243211) property specifies either how many times a timeline plays, or a larger duration that the timeline should repeat within. By default, a timeline has an iteration count of "1x", which means it plays one time for its [**Duration**](https://msdn.microsoft.com/library/windows/apps/BR243207) and does not repeat.

You can cause the animation to run multiple iterations. For example, a value of "3x" causes the animation to run three times. Or, you can specify a different [**Duration**](https://msdn.microsoft.com/library/windows/apps/BR242377) for [**RepeatBehavior**](https://msdn.microsoft.com/library/windows/apps/BR243211). That **Duration** should be longer than the **Duration** of the animation itself to be effective. For example, if you specify a **RepeatBehavior** of "0:0:10", for an animation that has a [**Duration**](https://msdn.microsoft.com/library/windows/apps/BR243207) of "0:0:2", that animation repeats five times. If these don't divide evenly, the animation gets truncated at the time that the **RepeatBehavior** time is reached, which might be partway through. Finally, you can specify the special value "Forever", which causes the animation to run infinitely until it's deliberately stopped.

For more info about [**RepeatBehavior**](https://msdn.microsoft.com/library/windows/apps/BR210411) values and the XAML syntax, see [**RepeatBehavior**](https://msdn.microsoft.com/library/windows/apps/BR210411).

### **FillBehavior="Stop"**

By default, when an animation ends, the animation leaves the property value as the final **To** or **By**-modified value even after its duration is surpassed. However, if you set the value of the [**FillBehavior**](https://msdn.microsoft.com/library/windows/apps/BR243209) property to [**FillBehavior.Stop**](https://msdn.microsoft.com/library/windows/apps/BR210306), the value of the animated value reverts to whatever the value was before the animation was applied, or more precisely to the current effective value as determined by the dependency property system (for more info on this distinction, see [Dependency properties overview](https://msdn.microsoft.com/library/windows/apps/Mt185583)).

### **BeginTime**

By default, the [**BeginTime**](https://msdn.microsoft.com/library/windows/apps/BR243204) of an animation is "0:0:0", so it begins as soon as its containing [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/BR210490) runs. You might change this if the **Storyboard** contains more than one animation and you want to stagger the start times of the others versus an initial animation, or to create a deliberate short delay.

### **SpeedRatio**

If you have more than one animation in a [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/BR210490) you can change the time rate of one or more of the animations relative to the **Storyboard**. It's the parent **Storyboard** that ultimately controls how the [**Duration**](https://msdn.microsoft.com/library/windows/apps/BR242377) time elapses while the animations run. This property isn't used very often. For more info see [**SpeedRatio**](https://msdn.microsoft.com/library/windows/apps/BR243213).

## Defining more than one animation in a **Storyboard**

The contents of a [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/BR210490) can be more than one animation definition. You might have more than one animation if you are applying related animations to two properties of the same target object. For example, you might change both the [**TranslateX**](https://msdn.microsoft.com/library/windows/apps/BR228122) and [**TranslateY**](https://msdn.microsoft.com/library/windows/apps/BR228124) properties of a [**TranslateTransform**](https://msdn.microsoft.com/library/windows/apps/BR243027) used as the [**RenderTransform**](https://msdn.microsoft.com/library/windows/apps/BR208980) of a UI element; this will cause the element to translate diagonally. You need two different animations to accomplish that, but you might want the animations to be part of the same **Storyboard** because you always want those two animations to be run together.

The animations don't have to be the same type, or target the same object. They can have different durations, and don't have to share any property values.

When the parent [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/BR210490) runs, each of the animations within will run too.

The [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/BR210490) class actually has a lot of the same animation properties as the animation types do, because both share the [**Timeline**](https://msdn.microsoft.com/library/windows/apps/BR210517) base class. Thus, a **Storyboard** can have a [**RepeatBehavior**](https://msdn.microsoft.com/library/windows/apps/BR243211), or a [**BeginTime**](https://msdn.microsoft.com/library/windows/apps/BR243204). You don't usually set these on a **Storyboard** though unless you want all the contained animations to have that behavior. As a general rule, any **Timeline** property as set on a **Storyboard** applies to all its child animations. If let unset, the **Storyboard** has an implicit duration that's calculated from the longest [**Duration**](https://msdn.microsoft.com/library/windows/apps/BR242377) value of the contained animations. An explicitly set [**Duration**](https://msdn.microsoft.com/library/windows/apps/BR243207) on a **Storyboard** that's shorter than one of its child animations will cause that animation to get cut off, which isn't usually desirable.

A storyboard can't contain two animations that attempt to target and animate the same property on the same object. If you try this, you'll get a runtime error when the storyboard tries to run. This restriction applies even if the animations don't overlap in time because of deliberately different [**BeginTime**](https://msdn.microsoft.com/library/windows/apps/BR243204) values and durations. If you really want to apply a more complex animation timeline to the same property in a single storyboard, the way to do this is to use a key-frame animation. See [Key-frame and easing function animations](key-frame-and-easing-function-animations.md).

The animation system can apply more than one animation to the value of a property, if those inputs come from multiple storyboards. Using this behavior deliberately for simultaneously running storyboards isn't common. However it's possible that an app-defined animation that you apply to a control property will be modifying the **HoldEnd** value of an animation that was previously run as part of the control's visual state model.

## Defining a storyboard as a resource

A [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/BR210490) is the container that you put animation objects in. You typically define the **Storyboard** as a resource that is available to the object that you want to animate, either in page-level [**Resources**](https://msdn.microsoft.com/library/windows/apps/BR208740) or [**Application.Resources**](https://msdn.microsoft.com/library/windows/apps/BR242338).

This next example shows how the previous example [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/BR210490) would be contained in a page-level [**Resources**](https://msdn.microsoft.com/library/windows/apps/BR208740) definition, where the **Storyboard** is a keyed resource of the root [**Page**](https://msdn.microsoft.com/library/windows/apps/BR227503). Note the [x:Name attribute](https://msdn.microsoft.com/library/windows/apps/Mt204788). This attribute is how you define a variable name for the **Storyboard**, so that other elements in XAML as well as code can refer to the **Storyboard** later.

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
  <StackPanel>
    <Rectangle x:Name="MyAnimatedRectangle"
      Width="300" Height="200" Fill="Blue"/>
  </StackPanel>
</Page>
```

Defining resources at the XAML root of a XAML file such as page.xaml or app.xaml is a common practice for how to organize keyed resources in your XAML. You also can factor resources into separate files and merge them into apps or pages. For more info, see [ResourceDictionary and XAML resource references](https://msdn.microsoft.com/library/windows/apps/Mt187273).

> [!NOTE]
> Windows Runtime XAML supports identifying resources either using the [x:Key attribute](https://msdn.microsoft.com/library/windows/apps/Mt204787) or the [x:Name attribute](https://msdn.microsoft.com/library/windows/apps/Mt204788). Using x:Name attribute is more common for a [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/BR210490), because you'll want to reference it by variable name eventually, so that you can call its [**Begin**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.storyboard.begin) method and run the animations. If you do use [x:Key attribute](https://msdn.microsoft.com/library/windows/apps/Mt204787), you'll need to use [**ResourceDictionary**](https://msdn.microsoft.com/library/windows/apps/BR208794) methods such as the [**Item**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.resourcedictionary.item) indexer to retrieve it as a keyed resource and then cast the retrieved object to **Storyboard** to use the **Storyboard** methods.

### Storyboards for visual states

You also put your animations within a [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/BR210490) unit when you are declaring the visual state animations for a control's visual appearance. In that case the **Storyboard** elements you define go into a [**VisualState**](https://msdn.microsoft.com/library/windows/apps/BR209007) container that's nested more deeply in a [**Style**](https://msdn.microsoft.com/library/windows/apps/BR208849) (it's the **Style** that is the keyed resource). You don't need a key or name for your **Storyboard** in this case because it's the **VisualState** that has a target name that the [**VisualStateManager**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.visualstatemanager) can invoke. The styles for controls are often factored into separate XAML [**ResourceDictionary**](https://msdn.microsoft.com/library/windows/apps/BR208794) files rather than placed in a page or app **Resources** collection. For more info, see [Storyboarded animations for visual states](https://msdn.microsoft.com/library/windows/apps/xaml/JJ819808).

## Dependent and independent animations

At this point we need to introduce some important points about how the animation system works. In particular, animation interacts fundamentally with how a Windows Runtime app renders to the screen, and how that rendering uses processing threads. A Windows Runtime app always has a main UI thread, and this thread is responsible for updating the screen with current information. In addition, a Windows Runtime app has a composition thread, which is used for precalculating layouts immediately before they are shown. When you animate the UI, there's potential to cause a lot of work for the UI thread. The system must redraw large areas of the screen using fairly short time intervals between each refresh. This is necessary for capturing the latest property value of the animated property. If you're not careful, there's risk that an animation can make the UI less responsive, or will impact performance of other app features that are also on the same UI thread.

The variety of animation that is determined to have some risk of slowing down the UI thread is called a *dependent animation*. An animation not subject to this risk is an *independent animation*. The distinction between dependent and independent animations isn't just determined by animation types ([**DoubleAnimation**](https://msdn.microsoft.com/library/windows/apps/BR243136) and so on) as we described earlier. Instead, it's determined by which specific properties you are animating, and other factors like inheritance and composition of controls. There are circumstances where even if an animation does change UI, the animation can have minimal impact to the UI thread, and can instead be handled by the composition thread as an independent animation.

An animation is independent if it has any of these characteristics:

-   The [**Duration**](https://msdn.microsoft.com/library/windows/apps/BR243207) of the animation is 0 seconds (see Warning)
-   The animation targets [**UIElement.Opacity**](/uwp/api/Windows.UI.Xaml.UIElement.Opacity)
-   The animation targets a sub-property value of these [**UIElement**](https://msdn.microsoft.com/library/windows/apps/BR208911) properties: [**Transform3D**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.transform3d.aspx), [**RenderTransform**](https://msdn.microsoft.com/library/windows/apps/BR208980), [**Projection**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.projection.aspx), [**Clip**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.clip)
-   The animation targets [**Canvas.Left**](https://msdn.microsoft.com/library/windows/apps/Hh759771) or [**Canvas.Top**](https://msdn.microsoft.com/library/windows/apps/Hh759772)
-   The animation targets a [**Brush**](/uwp/api/Windows.UI.Xaml.Media.Brush) value and uses a [**SolidColorBrush**](https://msdn.microsoft.com/library/windows/apps/BR242962), animating its [**Color**](/uwp/api/Windows.UI.Xaml.Media.SolidColorBrush.Color)
-   The animation is an [**ObjectAnimationUsingKeyFrames**](https://msdn.microsoft.com/library/windows/apps/BR210320)

> [!WARNING]
> In order for your animation to be treated as independent, you must explicitly set `Duration="0"`. For example, if you remove `Duration="0"` from this XAML, the animation is treated as dependent, even though the [**KeyTime**](https://msdn.microsoft.com/library/windows/apps/BR243169) of the frame is "0:0:0".

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

If your animation doesn't meet these criteria, it's probably a dependent animation. By default, the animation system won't run a dependent animation. So during the process of developing and testing, you might not even be seeing your animation running. You can still use this animation, but you must specifically enable each such dependent animation. To enable your animation, set the **EnableDependentAnimation** property of the animation object to **true**. (Each [**Timeline**](https://msdn.microsoft.com/library/windows/apps/BR210517) subclass that represents an animation has a different implementation of the property but they're all named `EnableDependentAnimation`.)

The requirement of enabling dependent animations falling onto the app developer is a conscious design aspect of the animation system and the development experience. We want developers to be aware that animations do have a performance cost for the responsiveness of your UI. Poorly performing animations are difficult to isolate and debug in a full-scale app. So it's better to turn on only the dependent animations you really need for your app's UI experience. We didn't want to make it too easy to compromise your app's performance because of decorative animations that use a lot of cycles. For more info on performance tips for animation, see [Optimize animations and media](https://msdn.microsoft.com/library/windows/apps/Mt204774).

As an app developer, you can also choose to apply an app-wide setting that always disables dependent animations, even those where **EnableDependentAnimation** is **true**. See [**Timeline.AllowDependentAnimations**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.timeline.allowdependentanimations).

> [!TIP]
> If you are composing visual states for a control using Visual Studio, the designer will produce warnings whenever you attempt to apply a dependent animation to a visual state property.

## Starting and controlling an animation

Everything we've shown you so far doesn't actually cause an animation to run or be applied! Until the animation is started and is running, the value changes that an animation is declaring in XAML are latent and won't happen yet. You must explicitly start an animation in some way that's related to the app lifetime or the user experience. At the simplest level, you start an animation by calling the [**Begin**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.storyboard.begin) method on the [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/BR210490) that's the parent for that animation. You can't call methods from XAML directly, so whatever you do to enable your animations, you'll be doing it from code. That will either be the code-behind for the pages or components of your app, or perhaps the logic of your control if you're defining a custom control class.

Typically, you'll call [**Begin**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.storyboard.begin) and just let the animation run to its duration completion. However, you can also use [**Pause**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.storyboard.pause.aspx), [**Resume**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.storyboard.resume.aspx) and [**Stop**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.storyboard.stop) methods to control the [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/BR210490) at run-time, as well as other APIs that are used for more advanced animation control scenarios.

When you call [**Begin**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.storyboard.begin) on a storyboard that contains an animation that repeats infinitely (`RepeatBehavior="Forever"`), that animation runs until the page containing it is unloaded, or you specifically call [**Pause**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.storyboard.pause.aspx) or [**Stop**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.storyboard.stop).

### Starting an animation from app code

You can either start animations automatically, or in response to user actions. For the automatic case, you typically use an object lifetime event such as [**Loaded**](https://msdn.microsoft.com/library/windows/apps/BR208723) to act as the animation trigger. The **Loaded** event is a good event to use for this because at that point the UI is ready for interaction, and the animation won't be cut off at the beginning because another part of UI was still loading.

In this example, the [**PointerPressed**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.uielement.pointerpressed) event is attached to the rectangle so that when the user clicks the rectangle, the animation begins.

```xaml
<Rectangle PointerPressed="Rectangle_Tapped"
  x:Name="MyAnimatedRectangle"
  Width="300" Height="200" Fill="Blue"/>
```

The event handler start the [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/BR210490) (the animation) by using the [**Begin**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.storyboard.begin) method of the **Storyboard**.

```csharp
myStoryboard.Begin();
```

```cpp
myStoryboard->Begin();
```

```vb
myStoryBoard.Begin()
```

You can handle the [**Completed**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.timeline.completed.aspx) event if you want other logic to run after the animation has finished applying values. Also, for troubleshooting property system/animation interactions, the [**GetAnimationBaseValue**](https://msdn.microsoft.com/library/windows/apps/BR242358) method can be useful.

> [!TIP]
> Whenever you are coding for an app scenario where you are starting an animation from app code, you might want to review again whether an animation or transition already exists in the animation library for your UI scenario. The library animations enable a more consistent UI experience across all Windows Runtime apps, and are easier to use.

 

### Animations for visual states

The run behavior for a [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/BR210490) that's used to define a control's visual state is different from how an app might run a storyboard directly. As applied to a visual state definition in XAML, the **Storyboard** is an element of a containing [**VisualState**](https://msdn.microsoft.com/library/windows/apps/BR209007), and the state as a whole is controlled by using the [**VisualStateManager**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.visualstatemanager) API. Any animations within will run according to their animation values and [**Timeline**](https://msdn.microsoft.com/library/windows/apps/BR210517) properties when the containing **VisualState** is used by a control. For more info, see [Storyboards for visual states](https://msdn.microsoft.com/library/windows/apps/xaml/JJ819808). For visual states, the apparent [**FillBehavior**](https://msdn.microsoft.com/library/windows/apps/BR243209) is different. If a visual state is changed to another state, all the property changes applied by the previous visual state and its animations are canceled, even if the new visual state doesn't specifically apply a new animation to a property.

### **Storyboard** and **EventTrigger**

There is one way to start an animation that can be declared entirely in XAML. However, this technique isn't widely used anymore. It's a legacy syntax from WPF and early versions of Silverlight prior to [**VisualStateManager**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.visualstatemanager) support. This [**EventTrigger**](https://msdn.microsoft.com/library/windows/apps/BR242390) syntax still works in Windows Runtime XAML for import/compatibility reasons, but only works for a trigger behavior based on the [**FrameworkElement.Loaded**](https://msdn.microsoft.com/library/windows/apps/BR208723) event; attempting to trigger off other events will throw exceptions or fail to compile. For more info, see [**EventTrigger**](https://msdn.microsoft.com/library/windows/apps/BR242390) or [**BeginStoryboard**](https://msdn.microsoft.com/library/windows/apps/BR243053).

## Animating XAML attached properties

It's not a common scenario, but you can apply an animated value to a XAML attached property. For more info on what attached properties are and how they work, see [Attached properties overview](https://msdn.microsoft.com/library/windows/apps/Mt185579). Targeting an attached property requires a [property-path syntax](https://msdn.microsoft.com/library/windows/apps/Mt185586) that encloses the property name in parentheses. You can animate the built-in attached properties such as [**Canvas.ZIndex**](https://msdn.microsoft.com/library/windows/apps/Hh759773) by using an [**ObjectAnimationUsingKeyFrames**](https://msdn.microsoft.com/library/windows/apps/BR210320) that applies discrete integer values. However, an existing limitation of the Windows Runtime XAML implementation is that you cannot animate a custom attached property.

## More animation types, and next steps for learning about animating your UI

Up to now, we've shown the custom animations that are animating between two values, and then linearly interpolating the values as necessary while the animation runs. These are called **From**/**To**/**By** animations. But there's another animation type that enables you to declare intermediate values that fall between the start and end. These are called *key-frame animations*. There's also a way to alter the interpolation logic on either a **From**/**To**/**By** animation or a key-frame animation. This involves applying an easing function. For more info on these concepts, see [Key-frame and easing function animations](key-frame-and-easing-function-animations.md).

## Related topics

* [Property-path syntax](https://msdn.microsoft.com/library/windows/apps/Mt185586)
* [Dependency properties overview](https://msdn.microsoft.com/library/windows/apps/Mt185583)
* [Key-frame and easing function animations](key-frame-and-easing-function-animations.md)
* [Storyboarded animations for visual states](https://msdn.microsoft.com/library/windows/apps/xaml/JJ819808)
* [Control templates](https://msdn.microsoft.com/library/windows/apps/Mt210948)
* [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/BR210490)
* [**Storyboard.TargetProperty**](https://msdn.microsoft.com/library/windows/apps/Hh759824)
 

 




