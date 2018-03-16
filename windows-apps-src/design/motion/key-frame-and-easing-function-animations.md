---
author: Jwmsft
title: Key-frame animations and easing function animations
ms.assetid: D8AF24CD-F4C2-4562-AFD7-25010955D677
description: Linear key-frame animations, key-frame animations with a KeySpline value, or easing functions are three different techniques for approximately the same scenario.
ms.author: jimwalk
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Key-frame animations and easing function animations



Linear key-frame animations, key-frame animations with a **KeySpline** value, or easing functions are three different techniques for approximately the same scenario: creating a storyboarded animation that's a bit more complex, and that uses a nonlinear animation behavior from a starting state to an end state.

## Prerequisites

Make sure you've read the [Storyboarded animations](storyboarded-animations.md) topic. This topic builds on the animation concepts that were explained in [Storyboarded animations](storyboarded-animations.md) and won't go over them again. For example, [Storyboarded animations](storyboarded-animations.md) describes how to target animations, storyboards as resources, the [**Timeline**](https://msdn.microsoft.com/library/windows/apps/BR210517) property values such as [**Duration**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.timeline.duration), [**FillBehavior**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.timeline.fillbehavior), and so on.

## Animating using key-frame animations

Key-frame animations permit more than one target value that is reached at a point along the animation timeline. In other words, each key frame can specify a different intermediate value, and the last key frame reached is the final animation value. By specifying multiple values to animate, you can make more complex animations. Key-frame animations also enable different interpolation logic, which are each implemented as a different **KeyFrame** subclass per animation type. Specifically, each key-frame animation type has a **Discrete**, **Linear**, **Spline** and **Easing** variation of its **KeyFrame** class for specifying its key frames. For example, to specify an animation that targets a [**Double**](https://msdn.microsoft.com/library/windows/apps/xaml/system.double.aspx) and uses key frames, you could declare key frames with [**DiscreteDoubleKeyFrame**](https://msdn.microsoft.com/library/windows/apps/BR243130), [**LinearDoubleKeyFrame**](https://msdn.microsoft.com/library/windows/apps/BR210316), [**SplineDoubleKeyFrame**](https://msdn.microsoft.com/library/windows/apps/BR210446), and [**EasingDoubleKeyFrame**](https://msdn.microsoft.com/library/windows/apps/BR210269). You can use any and all of these types within a single **KeyFrames** collection, to change the interpolation each time a new key frame is reached.

For interpolation behavior, each key frame controls the interpolation until its **KeyTime** time is reached. Its **Value** is reached at that time also. If there are more key frames beyond, the value then becomes the starting value for the next key frame in a sequence.

At the start of the animation, if no key frame with **KeyTime** of "0:0:0" exists, the starting value is whatever the non-animated value of the property is. This is similar to how a **From**/**To**/**By** animation acts if there is no **From**.

The duration of a key-frame animation is implicitly the duration equal to the highest **KeyTime** value set in any of its key frames. You can set an explicit [**Duration**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.timeline.duration) if you want, but be careful it's not shorter than a **KeyTime** in your own key frames or you'll cut off part of the animation.

In addition to [**Duration**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.timeline.duration), you can set all the [**Timeline**](https://msdn.microsoft.com/library/windows/apps/BR210517) based properties on a key-frame animation, like you can with a **From**/**To**/**By** animation, because the key-frame animation classes also derive from **Timeline**. These are:

-   [**AutoReverse**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.timeline.autoreverse): once the last key frame is reached, the frames are repeated in reverse order from the end. This doubles the apparent duration of the animation.
-   [**BeginTime**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.timeline.begintime): delays the start of the animation. The timeline for the **KeyTime** values in the frames doesn't start counting until **BeginTime** is reached, so there's no risk of cutting off frames
-   [**FillBehavior**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.timeline.fillbehavior): controls what happens when the last key frame is reached. **FillBehavior** has no effect on any intermediate key frames.
-   [**RepeatBehavior**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.timeline.repeatbehaviorproperty):
    -   If set to **Forever**, then the key frames and their timeline repeat infinitely.
    -   If set to an iteration count, the timeline repeats that many times.
    -   If set to a [**Duration**](https://msdn.microsoft.com/library/windows/apps/BR242377), the timeline repeats until that time is reached. This might truncate the animation part way through the key frame sequence, if it's not an integer factor of the timeline's implicit duration.
-   [**SpeedRatio**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.timeline.speedratioproperty) (not commonly used)

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
> Discrete key frames are the only way to animate a value that isn't of type [**Double**](https://msdn.microsoft.com/library/windows/apps/xaml/system.double.aspx), [**Point**](https://msdn.microsoft.com/library/windows/apps/BR225870), and [**Color**](https://msdn.microsoft.com/library/windows/apps/Hh673723), with a [**DiscreteObjectKeyFrame**](https://msdn.microsoft.com/library/windows/apps/BR243132). We'll discuss this in more detail later in this topic.

### Spline key frames

A spline key frame creates a variable transition between values according to the value of the **KeySpline** property. This property specifies the first and second control points of a Bezier curve, which describes the acceleration of the animation. Basically, a [**KeySpline**](https://msdn.microsoft.com/library/windows/apps/BR210307) defines a function-over-time relationship where the function-time graph is the shape of that Bezier curve. You typically specify a **KeySpline** value in a XAML shorthand attribute string that has four [**Double**](https://msdn.microsoft.com/library/windows/apps/xaml/system.double.aspx) values separated by spaces or commas. These values are "X,Y" pairs for two control points of the Bezier curve. "X" is time and "Y" is the function modifier to the value. Each value should always be between 0 and 1 inclusive. Without control point modification to a **KeySpline**, the straight line from 0,0 to 1,1 is the representation of a function over time for a linear interpolation. Your control points change the shape of that curve and thus the behavior of the function over time for the spline animation. It's probably best to see this visually as a graph. You can run the [Silverlight key-spline visualizer sample](http://samples.msdn.microsoft.com/Silverlight/SampleBrowser/index.htm#/?sref=KeySplineExample) in a browser to see how the control points modify the curve and how a sample animation runs when using it as a **KeySpline** value.

This next example shows three different key frames applied to an animation, with the last one being a key spline animation for a [**Double**](https://msdn.microsoft.com/library/windows/apps/xaml/system.double.aspx) value ([**SplineDoubleKeyFrame**](https://msdn.microsoft.com/library/windows/apps/BR210446)). Note the string "0.6,0.0 0.9,0.00" applied for **KeySpline**. This produces a curve where the animation appears to run slowly at first but then rapidly reaches the value just before the **KeyTime** is reached.

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

An easing key frame is a key frame where interpolation is being applied, and the function over time of the interpolation is controlled by several pre-defined mathematical formulas. You can actually produce much the same result with a spline key frame as you can with some of the easing function types, but there are also some easing functions, such as [**BackEase**](https://msdn.microsoft.com/library/windows/apps/BR243049), that you can't reproduce with a spline.

To apply an easing function to an easing key frame, you set the **EasingFunction** property as a property element in XAML for that key frame. For the value, specify an object element for one of the easing function types.

This example applies a [**CubicEase**](https://msdn.microsoft.com/library/windows/apps/BR243126) and then a [**BounceEase**](https://msdn.microsoft.com/library/windows/apps/BR243057) as successive key frames to a [**DoubleAnimation**](https://msdn.microsoft.com/library/windows/apps/BR243136) to create a bouncing effect.

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

-   By using an easing keyframe in a keyframe animation, as described in the previous section. Use [**EasingColorKeyFrame.EasingFunction**](https://msdn.microsoft.com/library/windows/apps/BR210267), [**EasingDoubleKeyFrame.EasingFunction**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.easingdoublekeyframe.easingfunction.aspx), or [**EasingPointKeyFrame.EasingFunction**](https://msdn.microsoft.com/library/windows/apps/BR210279).
-   By setting the **EasingFunction** property on one of the **From**/**To**/**By** animation types. Use [**ColorAnimation.EasingFunction**](https://msdn.microsoft.com/library/windows/apps/BR243075), [**DoubleAnimation.EasingFunction**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.doubleanimation.easingfunction.aspx) or [**PointAnimation.EasingFunction**](https://msdn.microsoft.com/library/windows/apps/BR210354).
-   By setting [**GeneratedEasingFunction**](https://msdn.microsoft.com/library/windows/apps/BR209037) as part of a [**VisualTransition**](https://msdn.microsoft.com/library/windows/apps/BR209034). This is specific to defining visual states for controls; for more info, see [**GeneratedEasingFunction**](https://msdn.microsoft.com/library/windows/apps/BR209037) or [Storyboards for visual states](https://msdn.microsoft.com/library/windows/apps/xaml/JJ819808).

Here is a list of the easing functions:

-   [**BackEase**](https://msdn.microsoft.com/library/windows/apps/BR243049): Retracts the motion of an animation slightly before it begins to animate in the path indicated.
-   [**BounceEase**](https://msdn.microsoft.com/library/windows/apps/BR243057): Creates a bouncing effect.
-   [**CircleEase**](https://msdn.microsoft.com/library/windows/apps/BR243063): Creates an animation that accelerates or decelerates using a circular function.
-   [**CubicEase**](https://msdn.microsoft.com/library/windows/apps/BR243126): Creates an animation that accelerates or decelerates using the formula f(t) = t3.
-   [**ElasticEase**](https://msdn.microsoft.com/library/windows/apps/BR210282): Creates an animation that resembles a spring oscillating back and forth until it comes to rest.
-   [**ExponentialEase**](https://msdn.microsoft.com/library/windows/apps/BR210294): Creates an animation that accelerates or decelerates using an exponential formula.
-   [**PowerEase**](https://msdn.microsoft.com/library/windows/apps/BR210399): Creates an animation that accelerates or decelerates using the formula f(t) = tp where p is equal to the [**Power**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.powerease.power) property.
-   [**QuadraticEase**](https://msdn.microsoft.com/library/windows/apps/BR210403): Creates an animation that accelerates or decelerates using the formula f(t) = t2.
-   [**QuarticEase**](https://msdn.microsoft.com/library/windows/apps/BR210405): Creates an animation that accelerates or decelerates using the formula f(t) = t4.
-   [**QuinticEase**](https://msdn.microsoft.com/library/windows/apps/BR210407): Create an animation that accelerates or decelerates using the formula f(t) = t5.
-   [**SineEase**](https://msdn.microsoft.com/library/windows/apps/BR210439): Creates an animation that accelerates or decelerates using a sine formula.

Some of the easing functions have their own properties. For example, [**BounceEase**](https://msdn.microsoft.com/library/windows/apps/BR243057) has two properties [**Bounces**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.bounceease.bounces.aspx) and [**Bounciness**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.bounceease.bounciness.aspx) that modify the function-over-time behavior of that particular **BounceEase**. Other easing functions such as [**CubicEase**](https://msdn.microsoft.com/library/windows/apps/BR243126) don't have properties other than the [**EasingMode**](https://msdn.microsoft.com/library/windows/apps/BR210275) property that all easing functions share, and always produce the same function-over-time behavior.

Some of these easing functions have a bit of overlap, depending on how you set properties on the easing functions that have properties. For example, [**QuadraticEase**](https://msdn.microsoft.com/library/windows/apps/BR210403) is exactly the same as a [**PowerEase**](https://msdn.microsoft.com/library/windows/apps/BR210399) with [**Power**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.powerease.power) equal to 2. And [**CircleEase**](https://msdn.microsoft.com/library/windows/apps/BR243063) is basically a default-value [**ExponentialEase**](https://msdn.microsoft.com/library/windows/apps/BR210294).

The [**BackEase**](https://msdn.microsoft.com/library/windows/apps/BR243049) easing function is unique because it can change the value outside of the normal range as set by **From**/**To** or values of key frames. It starts the animation by changing the value in the opposite direction as would be expected from a normal **From**/**To** behavior, goes back to the **From** or starting value again, and then runs the animation as normal.

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

When an easing function is applied to a **From**/**To**/**By** animation, it's changing the function- over-time characteristics of how the value interpolates between the **From** and **To** values over the [**Duration**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.timeline.duration) of the animation. Without an easing function, that would be a linear interpolation.

## <span id="Discrete_object_value_animations"></span><span id="discrete_object_value_animations"></span><span id="DISCRETE_OBJECT_VALUE_ANIMATIONS"></span>Discrete object value animations

One type of animation deserves special mention because it's the only way you can apply an animated value to properties that aren't of type [**Double**](https://msdn.microsoft.com/library/windows/apps/xaml/system.double.aspx), [**Point**](https://msdn.microsoft.com/library/windows/apps/BR225870), or [**Color**](https://msdn.microsoft.com/library/windows/apps/Hh673723). This is the key-frame animation [**ObjectAnimationUsingKeyFrames**](https://msdn.microsoft.com/library/windows/apps/BR210320). Animating using [**Object**](https://msdn.microsoft.com/library/windows/apps/xaml/system.object.aspx) values is different because there's no possibility of interpolating the values between the frames. When the frame's [**KeyTime**](https://msdn.microsoft.com/library/windows/apps/BR210342) is reached, the animated value is immediately set to the value specified in the key frame's **Value**. Because there's no interpolation, there's only one key frame you use in the **ObjectAnimationUsingKeyFrames** key frames collection: [**DiscreteObjectKeyFrame**](https://msdn.microsoft.com/library/windows/apps/BR243132).

The [**Value**](https://msdn.microsoft.com/library/windows/apps/BR210344) of a [**DiscreteObjectKeyFrame**](https://msdn.microsoft.com/library/windows/apps/BR243132) is often set using property element syntax, because the object value you are trying to set often is not expressible as a string to fill **Value** in attribute syntax. You can still use attribute syntax if you use a reference such as [StaticResource](https://msdn.microsoft.com/library/windows/apps/Mt185588).

One place you'll see an [**ObjectAnimationUsingKeyFrames**](https://msdn.microsoft.com/library/windows/apps/BR210320) used in the default templates is when a template property references a [**Brush**](/uwp/api/Windows.UI.Xaml.Media.Brush) resource. These resources are [**SolidColorBrush**](https://msdn.microsoft.com/library/windows/apps/BR242962) objects, not just a [**Color**](https://msdn.microsoft.com/library/windows/apps/Hh673723) value, and they use resources that are defined as system themes ([**ThemeDictionaries**](https://msdn.microsoft.com/library/windows/apps/BR208807)). They can be assigned directly to a **Brush**-type value such as [**TextBlock.Foreground**](https://msdn.microsoft.com/library/windows/apps/BR209665) and don't need to use indirect targeting. But because a **SolidColorBrush** is not [**Double**](https://msdn.microsoft.com/library/windows/apps/xaml/system.double.aspx), [**Point**](https://msdn.microsoft.com/library/windows/apps/BR225870), or **Color**, you have to use a **ObjectAnimationUsingKeyFrames** to use the resource.

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

You also might use [**ObjectAnimationUsingKeyFrames**](https://msdn.microsoft.com/library/windows/apps/BR210320) to animate properties that use an enumeration value. Here's another example from a named style that comes from the Windows Runtime default templates. Note how it sets the [**Visibility**](https://msdn.microsoft.com/library/windows/apps/BR208992) property that takes a [**Visibility**](https://msdn.microsoft.com/library/windows/apps/BR209006) enumeration constant. In this case you can set the value using attribute syntax. You only need the unqualified constant name from an enumeration for setting a property with an enumeration value, for example "Collapsed".

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

You can use more than one [**DiscreteObjectKeyFrame**](https://msdn.microsoft.com/library/windows/apps/BR243132) for an [**ObjectAnimationUsingKeyFrames**](https://msdn.microsoft.com/library/windows/apps/BR210320) frame set. This might be an interesting way to create a "slide show" animation by animating the value of [**Image.Source**](https://msdn.microsoft.com/library/windows/apps/BR242760), as an example scenario for where multiple object values might be useful.

 ## Related topics

* [Property-path syntax](https://msdn.microsoft.com/library/windows/apps/Mt185586)
* [Dependency properties overview](https://msdn.microsoft.com/library/windows/apps/Mt185583)
* [**Storyboard**](https://msdn.microsoft.com/library/windows/apps/BR210490)
* [**Storyboard.TargetProperty**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.media.animation.storyboard.targetpropertyproperty)