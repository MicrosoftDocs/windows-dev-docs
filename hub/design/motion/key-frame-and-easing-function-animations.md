---
title: Key-frame animations and easing function animations
ms.assetid: D8AF24CD-F4C2-4562-AFD7-25010955D677
description: Linear key-frame animations, key-frame animations with a KeySpline value, or easing functions are three different techniques for approximately the same scenario.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Key-frame animations and easing function animations



Linear key-frame animations, key-frame animations with a **KeySpline** value, or easing functions are three different techniques for approximately the same scenario: creating a storyboarded animation that's a bit more complex, and that uses a nonlinear animation behavior from a starting state to an end state.

## Prerequisites

Make sure you've read the [Storyboarded animations](storyboarded-animations.md) topic. This topic builds on the animation concepts that were explained in [Storyboarded animations](storyboarded-animations.md) and won't go over them again. For example, [Storyboarded animations](storyboarded-animations.md) describes how to target animations, storyboards as resources, the [**Timeline**](/uwp/api/Windows.UI.Xaml.Media.Animation.Timeline) property values such as [**Duration**](/uwp/api/windows.ui.xaml.media.animation.timeline.duration), [**FillBehavior**](/uwp/api/windows.ui.xaml.media.animation.timeline.fillbehavior), and so on.

## Animating using key-frame animations

Key-frame animations permit more than one target value that is reached at a point along the animation timeline. In other words, each key frame can specify a different intermediate value, and the last key frame reached is the final animation value. By specifying multiple values to animate, you can make more complex animations. Key-frame animations also enable different interpolation logic, which are each implemented as a different **KeyFrame** subclass per animation type. Specifically, each key-frame animation type has a **Discrete**, **Linear**, **Spline** and **Easing** variation of its **KeyFrame** class for specifying its key frames. For example, to specify an animation that targets a [**Double**](/dotnet/api/system.double) and uses key frames, you could declare key frames with [**DiscreteDoubleKeyFrame**](/uwp/api/Windows.UI.Xaml.Media.Animation.DiscreteDoubleKeyFrame), [**LinearDoubleKeyFrame**](/uwp/api/Windows.UI.Xaml.Media.Animation.LinearDoubleKeyFrame), [**SplineDoubleKeyFrame**](/uwp/api/Windows.UI.Xaml.Media.Animation.SplineDoubleKeyFrame), and [**EasingDoubleKeyFrame**](/uwp/api/Windows.UI.Xaml.Media.Animation.EasingDoubleKeyFrame). You can use any and all of these types within a single **KeyFrames** collection, to change the interpolation each time a new key frame is reached.

For interpolation behavior, each key frame controls the interpolation until its **KeyTime** time is reached. Its **Value** is reached at that time also. If there are more key frames beyond, the value then becomes the starting value for the next key frame in a sequence.

At the start of the animation, if no key frame with **KeyTime** of "0:0:0" exists, the starting value is whatever the non-animated value of the property is. This is similar to how a **From**/**To**/**By** animation acts if there is no **From**.

The duration of a key-frame animation is implicitly the duration equal to the highest **KeyTime** value set in any of its key frames. You can set an explicit [**Duration**](/uwp/api/windows.ui.xaml.media.animation.timeline.duration) if you want, but be careful it's not shorter than a **KeyTime** in your own key frames or you'll cut off part of the animation.

In addition to [**Duration**](/uwp/api/windows.ui.xaml.media.animation.timeline.duration), you can set all the [**Timeline**](/uwp/api/Windows.UI.Xaml.Media.Animation.Timeline) based properties on a key-frame animation, like you can with a **From**/**To**/**By** animation, because the key-frame animation classes also derive from **Timeline**. These are:

-   [**AutoReverse**](/uwp/api/windows.ui.xaml.media.animation.timeline.autoreverse): once the last key frame is reached, the frames are repeated in reverse order from the end. This doubles the apparent duration of the animation.
-   [**BeginTime**](/uwp/api/windows.ui.xaml.media.animation.timeline.begintime): delays the start of the animation. The timeline for the **KeyTime** values in the frames doesn't start counting until **BeginTime** is reached, so there's no risk of cutting off frames
-   [**FillBehavior**](/uwp/api/windows.ui.xaml.media.animation.timeline.fillbehavior): controls what happens when the last key frame is reached. **FillBehavior** has no effect on any intermediate key frames.
-   [**RepeatBehavior**](/uwp/api/windows.ui.xaml.media.animation.timeline.repeatbehaviorproperty):
    -   If set to **Forever**, then the key frames and their timeline repeat infinitely.
    -   If set to an iteration count, the timeline repeats that many times.
    -   If set to a [**Duration**](/uwp/api/Windows.UI.Xaml.Duration), the timeline repeats until that time is reached. This might truncate the animation part way through the key frame sequence, if it's not an integer factor of the timeline's implicit duration.
-   [**SpeedRatio**](/uwp/api/windows.ui.xaml.media.animation.timeline.speedratioproperty) (not commonly used)

### Linear key frames

Linear key frames result in a simple linear interpolation of the value until the frame's **KeyTime** is reached. This interpolation behavior is the most similar to the simpler **From**/**To**/**By** animations described in the [Storyboarded animations](storyboarded-animations.md) topic.

Here's how to use a key-frame animation to scale the render height of a rectangle, using linear key frames. This example runs an animation where the height of the rectangle increases slightly and linearly for the first 4 seconds, then scales rapidly for the last second until the rectangle is double the starting height.

```xml
<StackPanel>
    <StackPanel.Resources>
        <Storyboard x:Name="myStoryboard">
            <DoubleAnimationUsingKeyFrames
              Storyboard.TargetName="myRectangle"
              Storyboard.TargetProperty="(UIElement.RenderTransform).(ScaleTransform.ScaleY)">
                <LinearDoubleKeyFrame Value="1" KeyTime="0:0:0"/>
                <LinearDoubleKeyFrame Value="1.2" KeyTime="0:0:4"/>
                <LinearDoubleKeyFrame Value="2" KeyTime="0:0:5"/>
            </DoubleAnimationUsingKeyFrames>
        </Storyboard>
    </StackPanel.Resources>
</StackPanel>
```

### Discrete key frames

Discrete key frames don't use any interpolation at all. When a **KeyTime** is reached, the new **Value** is simply applied. Depending on which UI property is being animated, this often produces an animation that appears to "jump". Be certain that this is the aesthetic behavior that you really want. You can minimize the apparent jumps by increasing the number of key frames you declare, but if a smooth animation is your goal, you might be better off using linear or spline key frames instead.

> [!NOTE]
> Discrete key frames are the only way to animate a value that isn't of type [**Double**](/dotnet/api/system.double), [**Point**](/uwp/api/Windows.Foundation.Point), and [**Color**](/uwp/api/Windows.UI.Color), with a [**DiscreteObjectKeyFrame**](/uwp/api/Windows.UI.Xaml.Media.Animation.DiscreteObjectKeyFrame). We'll discuss this in more detail later in this topic.

### Spline key frames

A spline key frame creates a variable transition between values according to the value of the **KeySpline** property. This property specifies the first and second control points of a Bezier curve, which describes the acceleration of the animation. Basically, a [**KeySpline**](/uwp/api/Windows.UI.Xaml.Media.Animation.KeySpline) defines a function-over-time relationship where the function-time graph is the shape of that Bezier curve. You typically specify a **KeySpline** value in a XAML shorthand attribute string that has four [**Double**](/dotnet/api/system.double) values separated by spaces or commas. These values are "X,Y" pairs for two control points of the Bezier curve. "X" is time and "Y" is the function modifier to the value. Each value should always be between 0 and 1 inclusive. Without control point modification to a **KeySpline**, the straight line from 0,0 to 1,1 is the representation of a function over time for a linear interpolation. Your control points change the shape of that curve and thus the behavior of the function over time for the spline animation. It's probably best to see this visually as a graph. You can run the [Silverlight key-spline visualizer sample](https://samples.msdn.microsoft.com/Silverlight/SampleBrowser/index.htm#/?sref=KeySplineExample) in a browser to see how the control points modify the curve and how a sample animation runs when using it as a **KeySpline** value.

This next example shows three different key frames applied to an animation, with the last one being a key spline animation for a [**Double**](/dotnet/api/system.double) value ([**SplineDoubleKeyFrame**](/uwp/api/Windows.UI.Xaml.Media.Animation.SplineDoubleKeyFrame)). Note the string "0.6,0.0 0.9,0.00" applied for **KeySpline**. This produces a curve where the animation appears to run slowly at first but then rapidly reaches the value just before the **KeyTime** is reached.

```xml
<Storyboard x:Name="myStoryboard">
    <!-- Animate the TranslateTransform's X property
        from 0 to 350, then 50,
        then 200 over 10 seconds. -->
    <DoubleAnimationUsingKeyFrames
        Storyboard.TargetName="MyAnimatedTranslateTransform"
        Storyboard.TargetProperty="X"
        Duration="0:0:10" EnableDependentAnimation="True">

        <!-- Using a LinearDoubleKeyFrame, the rectangle moves 
            steadily from its starting position to 500 over 
            the first 3 seconds.  -->
        <LinearDoubleKeyFrame Value="500" KeyTime="0:0:3"/>

        <!-- Using a DiscreteDoubleKeyFrame, the rectangle suddenly 
            appears at 400 after the fourth second of the animation. -->
        <DiscreteDoubleKeyFrame Value="400" KeyTime="0:0:4"/>

        <!-- Using a SplineDoubleKeyFrame, the rectangle moves 
            back to its starting point. The
            animation starts out slowly at first and then speeds up. 
            This KeyFrame ends after the 6th second. -->
        <SplineDoubleKeyFrame KeySpline="0.6,0.0 0.9,0.00" Value="0" KeyTime="0:0:6"/>
    </DoubleAnimationUsingKeyFrames>
</Storyboard>
```

### Easing key frames

An easing key frame is a key frame where interpolation is being applied, and the function over time of the interpolation is controlled by several pre-defined mathematical formulas. You can actually produce much the same result with a spline key frame as you can with some of the easing function types, but there are also some easing functions, such as [**BackEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.BackEase), that you can't reproduce with a spline.

To apply an easing function to an easing key frame, you set the **EasingFunction** property as a property element in XAML for that key frame. For the value, specify an object element for one of the easing function types.

This example applies a [**CubicEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.CubicEase) and then a [**BounceEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.BounceEase) as successive key frames to a [**DoubleAnimation**](/uwp/api/Windows.UI.Xaml.Media.Animation.DoubleAnimation) to create a bouncing effect.

```xml
<Storyboard x:Name="myStoryboard">
    <DoubleAnimationUsingKeyFrames Duration="0:0:10"
        Storyboard.TargetProperty="Height"
        Storyboard.TargetName="myEllipse">

        <!-- This keyframe animates the ellipse up to the crest 
            where it slows down and stops. -->
        <EasingDoubleKeyFrame Value="-300" KeyTime="00:00:02">
            <EasingDoubleKeyFrame.EasingFunction>
                <CubicEase/>
            </EasingDoubleKeyFrame.EasingFunction>
        </EasingDoubleKeyFrame>

        <!-- This keyframe animates the ellipse back down and makes
            it bounce. -->
        <EasingDoubleKeyFrame Value="0" KeyTime="00:00:06">
            <EasingDoubleKeyFrame.EasingFunction>
                <BounceEase Bounces="5"/>
            </EasingDoubleKeyFrame.EasingFunction>
        </EasingDoubleKeyFrame>
    </DoubleAnimationUsingKeyFrames>
</Storyboard>
```

This is just one easing function example. We'll cover more in the next section.

## Easing functions

Easing functions allow you to apply custom mathematical formulas to your animations. Mathematical operations are often useful to produce animations that simulate real-world physics in a 2-D coordinate system. For example, you may want an object to realistically bounce or behave as though it were on a spring. You could use key frame or even **From**/**To**/**By** animations to approximate these effects but it would take a significant amount of work and the animation would be less accurate than using a mathematical formula.

Easing functions can be applied to animations in three ways:

-   By using an easing keyframe in a keyframe animation, as described in the previous section. Use [**EasingColorKeyFrame.EasingFunction**](/uwp/api/windows.ui.xaml.media.animation.easingcolorkeyframe.easingfunction), [**EasingDoubleKeyFrame.EasingFunction**](/uwp/api/windows.ui.xaml.media.animation.easingdoublekeyframe.easingfunction), or [**EasingPointKeyFrame.EasingFunction**](/uwp/api/windows.ui.xaml.media.animation.easingpointkeyframe.easingfunction).
-   By setting the **EasingFunction** property on one of the **From**/**To**/**By** animation types. Use [**ColorAnimation.EasingFunction**](/uwp/api/windows.ui.xaml.media.animation.coloranimation.easingfunction), [**DoubleAnimation.EasingFunction**](/uwp/api/windows.ui.xaml.media.animation.doubleanimation.easingfunction) or [**PointAnimation.EasingFunction**](/uwp/api/windows.ui.xaml.media.animation.pointanimation.easingfunction).
-   By setting [**GeneratedEasingFunction**](/uwp/api/windows.ui.xaml.visualtransition.generatedeasingfunction) as part of a [**VisualTransition**](/uwp/api/Windows.UI.Xaml.VisualTransition). This is specific to defining visual states for controls; for more info, see [**GeneratedEasingFunction**](/uwp/api/windows.ui.xaml.visualtransition.generatedeasingfunction) or [Storyboards for visual states](/previous-versions/windows/apps/jj819808(v=win.10)).

Here is a list of the easing functions:

-   [**BackEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.BackEase): Retracts the motion of an animation slightly before it begins to animate in the path indicated.
-   [**BounceEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.BounceEase): Creates a bouncing effect.
-   [**CircleEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.CircleEase): Creates an animation that accelerates or decelerates using a circular function.
-   [**CubicEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.CubicEase): Creates an animation that accelerates or decelerates using the formula f(t) = t3.
-   [**ElasticEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.ElasticEase): Creates an animation that resembles a spring oscillating back and forth until it comes to rest.
-   [**ExponentialEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.ExponentialEase): Creates an animation that accelerates or decelerates using an exponential formula.
-   [**PowerEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.PowerEase): Creates an animation that accelerates or decelerates using the formula f(t) = tp where p is equal to the [**Power**](/uwp/api/windows.ui.xaml.media.animation.powerease.power) property.
-   [**QuadraticEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.QuadraticEase): Creates an animation that accelerates or decelerates using the formula f(t) = t2.
-   [**QuarticEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.QuarticEase): Creates an animation that accelerates or decelerates using the formula f(t) = t4.
-   [**QuinticEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.QuinticEase): Create an animation that accelerates or decelerates using the formula f(t) = t5.
-   [**SineEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.SineEase): Creates an animation that accelerates or decelerates using a sine formula.

Some of the easing functions have their own properties. For example, [**BounceEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.BounceEase) has two properties [**Bounces**](/uwp/api/windows.ui.xaml.media.animation.bounceease.bounces) and [**Bounciness**](/uwp/api/windows.ui.xaml.media.animation.bounceease.bounciness) that modify the function-over-time behavior of that particular **BounceEase**. Other easing functions such as [**CubicEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.CubicEase) don't have properties other than the [**EasingMode**](/uwp/api/windows.ui.xaml.media.animation.easingfunctionbase.easingmode) property that all easing functions share, and always produce the same function-over-time behavior.

Some of these easing functions have a bit of overlap, depending on how you set properties on the easing functions that have properties. For example, [**QuadraticEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.QuadraticEase) is exactly the same as a [**PowerEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.PowerEase) with [**Power**](/uwp/api/windows.ui.xaml.media.animation.powerease.power) equal to 2. And [**CircleEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.CircleEase) is basically a default-value [**ExponentialEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.ExponentialEase).

The [**BackEase**](/uwp/api/Windows.UI.Xaml.Media.Animation.BackEase) easing function is unique because it can change the value outside of the normal range as set by **From**/**To** or values of key frames. It starts the animation by changing the value in the opposite direction as would be expected from a normal **From**/**To** behavior, goes back to the **From** or starting value again, and then runs the animation as normal.

In an earlier example, we showed how to declare an easing function for a key-frame animation. This next sample applies an easing function to a **From**/**To**/**By** animation.

```xml
<StackPanel x:Name="LayoutRoot" Background="White">
    <StackPanel.Resources>
        <Storyboard x:Name="myStoryboard">
            <DoubleAnimation From="30" To="200" Duration="00:00:3" 
                Storyboard.TargetName="myRectangle" 
                Storyboard.TargetProperty="(UIElement.RenderTransform).(ScaleTransform.ScaleY)">
                <DoubleAnimation.EasingFunction>
                    <BounceEase Bounces="2" EasingMode="EaseOut" 
                                Bounciness="2"/>
                </DoubleAnimation.EasingFunction>
            </DoubleAnimation>
        </Storyboard>
    </StackPanel.Resources>
    <Rectangle x:Name="myRectangle" Fill="Blue" Width="200" Height="30"/>
</StackPanel>
```

When an easing function is applied to a **From**/**To**/**By** animation, it's changing the function- over-time characteristics of how the value interpolates between the **From** and **To** values over the [**Duration**](/uwp/api/windows.ui.xaml.media.animation.timeline.duration) of the animation. Without an easing function, that would be a linear interpolation.

## <span id="Discrete_object_value_animations"></span><span id="discrete_object_value_animations"></span><span id="DISCRETE_OBJECT_VALUE_ANIMATIONS"></span>Discrete object value animations

One type of animation deserves special mention because it's the only way you can apply an animated value to properties that aren't of type [**Double**](/dotnet/api/system.double), [**Point**](/uwp/api/Windows.Foundation.Point), or [**Color**](/uwp/api/Windows.UI.Color). This is the key-frame animation [**ObjectAnimationUsingKeyFrames**](/uwp/api/Windows.UI.Xaml.Media.Animation.ObjectAnimationUsingKeyFrames). Animating using [**Object**](/dotnet/api/system.object) values is different because there's no possibility of interpolating the values between the frames. When the frame's [**KeyTime**](/uwp/api/windows.ui.xaml.media.animation.objectkeyframe.keytime) is reached, the animated value is immediately set to the value specified in the key frame's **Value**. Because there's no interpolation, there's only one key frame you use in the **ObjectAnimationUsingKeyFrames** key frames collection: [**DiscreteObjectKeyFrame**](/uwp/api/Windows.UI.Xaml.Media.Animation.DiscreteObjectKeyFrame).

The [**Value**](/uwp/api/windows.ui.xaml.media.animation.objectkeyframe.value) of a [**DiscreteObjectKeyFrame**](/uwp/api/Windows.UI.Xaml.Media.Animation.DiscreteObjectKeyFrame) is often set using property element syntax, because the object value you are trying to set often is not expressible as a string to fill **Value** in attribute syntax. You can still use attribute syntax if you use a reference such as [StaticResource](../../xaml-platform/staticresource-markup-extension.md).

One place you'll see an [**ObjectAnimationUsingKeyFrames**](/uwp/api/Windows.UI.Xaml.Media.Animation.ObjectAnimationUsingKeyFrames) used in the default templates is when a template property references a [**Brush**](/uwp/api/Windows.UI.Xaml.Media.Brush) resource. These resources are [**SolidColorBrush**](/uwp/api/Windows.UI.Xaml.Media.SolidColorBrush) objects, not just a [**Color**](/uwp/api/Windows.UI.Color) value, and they use resources that are defined as system themes ([**ThemeDictionaries**](/uwp/api/windows.ui.xaml.resourcedictionary.themedictionaries)). They can be assigned directly to a **Brush**-type value such as [**TextBlock.Foreground**](/uwp/api/windows.ui.xaml.controls.textblock.foreground) and don't need to use indirect targeting. But because a **SolidColorBrush** is not [**Double**](/dotnet/api/system.double), [**Point**](/uwp/api/Windows.Foundation.Point), or **Color**, you have to use a **ObjectAnimationUsingKeyFrames** to use the resource.

```xml
<Style x:Key="TextButtonStyle" TargetType="Button">
    <Setter Property="Template">
        <Setter.Value>
            <ControlTemplate TargetType="Button">
                <Grid Background="Transparent">
                    <TextBlock x:Name="Text"
                        Text="{TemplateBinding Content}"/>
                    <VisualStateManager.VisualStateGroups>
                        <VisualStateGroup x:Name="CommonStates">
                            <VisualState x:Name="Normal"/>
                            <VisualState x:Name="PointerOver">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Storyboard.TargetName="Text" Storyboard.TargetProperty="Foreground">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="{StaticResource ApplicationPointerOverForegroundThemeBrush}"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
                            <VisualState x:Name="Pressed">
                                <Storyboard>
                                    <ObjectAnimationUsingKeyFrames Storyboard.TargetName="Text" Storyboard.TargetProperty="Foreground">
                                        <DiscreteObjectKeyFrame KeyTime="0" Value="{StaticResource ApplicationPressedForegroundThemeBrush}"/>
                                    </ObjectAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualState>
...
                       </VisualStateGroup>
                    </VisualStateManager.VisualStateGroups>
                </Grid>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
```

You also might use [**ObjectAnimationUsingKeyFrames**](/uwp/api/Windows.UI.Xaml.Media.Animation.ObjectAnimationUsingKeyFrames) to animate properties that use an enumeration value. Here's another example from a named style that comes from the Windows Runtime default templates. Note how it sets the [**Visibility**](/uwp/api/windows.ui.xaml.uielement.visibility) property that takes a [**Visibility**](/uwp/api/Windows.UI.Xaml.Visibility) enumeration constant. In this case you can set the value using attribute syntax. You only need the unqualified constant name from an enumeration for setting a property with an enumeration value, for example "Collapsed".

```xml
<Style x:Key="BackButtonStyle" TargetType="Button">
    <Setter Property="Template">
      <Setter.Value>
        <ControlTemplate TargetType="Button">
          <Grid x:Name="RootGrid">
            <VisualStateManager.VisualStateGroups>
              <VisualStateGroup x:Name="CommonStates">
              <VisualState x:Name="Normal"/>
...           <VisualState x:Name="Disabled">
                <Storyboard>
                  <ObjectAnimationUsingKeyFrames Storyboard.TargetName="RootGrid" Storyboard.TargetProperty="Visibility">
                    <DiscreteObjectKeyFrame Value="Collapsed" KeyTime="0"/>
                  </ObjectAnimationUsingKeyFrames>
                </Storyboard>
              </VisualState>
            </VisualStateGroup>
...
          </VisualStateManager.VisualStateGroups>
        </Grid>
      </ControlTemplate>
    </Setter.Value>
  </Setter>
</Style>
```

You can use more than one [**DiscreteObjectKeyFrame**](/uwp/api/Windows.UI.Xaml.Media.Animation.DiscreteObjectKeyFrame) for an [**ObjectAnimationUsingKeyFrames**](/uwp/api/Windows.UI.Xaml.Media.Animation.ObjectAnimationUsingKeyFrames) frame set. This might be an interesting way to create a "slide show" animation by animating the value of [**Image.Source**](/uwp/api/windows.ui.xaml.controls.image.source), as an example scenario for where multiple object values might be useful.

 ## Related topics

* [Property-path syntax](../../xaml-platform/property-path-syntax.md)
* [Dependency properties overview](../../xaml-platform/dependency-properties-overview.md)
* [**Storyboard**](/uwp/api/Windows.UI.Xaml.Media.Animation.Storyboard)
* [**Storyboard.TargetProperty**](/uwp/api/windows.ui.xaml.media.animation.storyboard.targetpropertyproperty)