---
author: scottmill
ms.assetid: 386faf59-8f22-2e7c-abc9-d04216e78894
title: Composition animations
description: Many composition object and effect properties can be animated using key frame and expression animations allowing properties of a UI element to change over time or based on a calculation.
ms.author: scotmi
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---
# Composition animations

\[ Updated for UWP apps on Windows 10. For Windows 8.x articles, see the [archive](http://go.microsoft.com/fwlink/p/?linkid=619132) \]

The Windows.UI.Composition WinRT API allows you to create, animate, transform and manipulate compositor objects in a unified API layer. 
Composition animations provide a powerful and efficient way to run animations in your application UI. 
They have been designed from the ground up to ensure that your animations run at 60 FPS independent of the UI thread 
and to give you the flexibility to build amazing experiences using not only time, but input and other properties, to drive animations.
This topic provides an overview of the functionality available that allows you to animate properties of the Composition object.
This document assumes you are familiar with the basics of the Visual Layer structure. For more information, [see here](./composition-visual-tree.md). 
There are two types of Composition Animations: **KeyFrame Animations**, and **Expression Animations**  

![Animation types](./images/composition-animation-types.png)  
   
 
## Types of Composition Animations
**KeyFrame Animations** provide your traditional time-driven, *frame-by-frame* animation experiences. 
Developers can explicitly define *control points* describing values an animating property needs to be at specific points in the animation timeline. 
More importantly you are able to use Easing Functions (otherwise called Interpolators) to describe how to transition between these control points.  

**Implicit Animations** are a type of animation that allows developers to define reusable individual animations or a series of animations separately from the core app logic. Implicit animations let developers create animation *templates* and hook them up with triggers. These triggers are property changes that result from explicit assignments. Developers can define a template as a single animation or an animation group. Animation groups are a collection of animation templates that can be started together either explicitly or with a trigger. Implicit animations remove the need for you to create explicit KeyFrameAnimations every time you want to change the value of a property and see it animate.

**Expression Animations** are a type of animation introduced in the Visual Layer with the Windows 10 November Update (Build 10586). 
The idea behind expression animations is a developer can create mathematical relationships between [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx) properties and discrete values that will get evaluated and updated every frame. 
Developers can reference properties on Composition objects or property sets, use mathematical function helpers and even reference input to derive these mathematical relationships. 
Expressions make experiences like parallax and sticky headers possible and smooth on the Windows platform.  

## Why Composition Animations?
**Performance**  
 When building Universal Windows applications, most developer code runs on the UI thread. 
 To ensure that the animations run smoothly across the different device categories, 
 the system performs the animation calculations and work on an independent thread in order to maintain 60 FPS. 
 This means developers can count on the system to provide smooth animations while their applications perform other complex operations for advanced user experiences.    
 
**Possibilities**  
The goal for composition animations in the Visual Layer is to make it easy to create beautiful UIs. We want to provide developers different types of animations that make it easy to build their amazing ideas.
 
   

**Templating**  
 All composition animations in the Visual Layer are templates – this means that developers can use an animation on multiple objects without the need to create separate animations. 
This allows developers to use the same animation and tweak properties or parameters to meet some other needs without the worry of obstructing the previous uses.  

You can check out our //BUILD talks for [Expression Animations](https://channel9.msdn.com/events/Build/2016/P486), [Interactive Experiences](https://channel9.msdn.com/Events/Build/2016/P405), [Implicit Animations](https://channel9.msdn.com/events/Build/2016/P484), and [Connected Animations](https://channel9.msdn.com/events/Build/2016/P485) to see some examples of what is possible.

You can also check out the [Composition GitHub](http://go.microsoft.com/fwlink/?LinkID=789439) for samples on how to use the APIs and high fidelity samples of the APIs in action.
 
## What can you animate with Composition Animations?
Composition animations can be applied to most properties of composition objects such as [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx), and **InsetClip**. 
You can also apply composition animations to composition effects and property sets. **When choosing what to animate, take note of the type – use 
this to determine what type of KeyFrame animation you construct or what type your expression must resolve to.**  
 
### Visual
|Animatable Visual Properties|	Type|
|------|------|
|AnchorPoint|	Vector2|
|CenterPoint|	Vector3|
|Offset|	Vector3|
|Opacity|	Scalar|
|Orientation|	Quaternion|
|RotationAngle|	Scalar|
|RotationAngleInDegrees|	Scalar|
|RotationAxis|	Vector3|
|Scale|	Vector3|
|Size|	Vector2|
|TransformMatrix*|	Matrix4x4|
*If you want to animate the entire TransformMatrix property as a Matrix4x4, you need to use an ExpressionAnimation to do so. 
Otherwise, you can target individual cells of the matrix and can use either a KeyFrame or ExpressionAnimation there.  

### InsetClip
|Animatable InsetClip Properties|	Type|
|-------------------------------|-------|
|BottomInset|	Scalar|
|LeftInset|	Scalar|
|RightInset|	Scalar|
|TopInset|	Scalar|

## Visual Sub Channel Properties
In addition to being able to animate properties of [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx), you are also able to target the *sub channel* components of these properties for animations as well. 
For example, say you simply want to animate the X Offset of a [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx) rather than the entire Offset. 
The animation can either target the Vector3 Offset property, or the Scalar X component of the Offset property. 
In addition to being able to target an individual sub channel component of a property, you are also able to target multiple components. 
For example, you can target the X and Y component of Scale.

|Animatable Visual Sub Channel Properties|	Type|
|----------------------------------------|------|
|AnchorPoint.x, y|Scalar|
|AnchorPoint.xy|Vector2|
|CenterPoint.x, y, z|Scalar|
|CenterPoint.xy, xz, yz|Vector2|
|Offset.x, y, z|Scalar|
|Offset.xy, xz, yz|Vector2|
|RotationAxis.x, y, z|Scalar|
|RotationAxis.xy, xz, yz|Vector2|
|Scale.x, y, z|Scalar|
|Scale.xy, xz, yz|Vector2|
|Size.x, y|Scalar|
|Size.xy|Vector2|
|TransformMatrix._11 ... TransformMatrix._NN,|Scalar|
|TransformMatrix._11_12 ... TransformMatrix._NN_NN|Vector2|
|TransformMatrix._11_12_13 ... TransformMatrix._NN_NN_NN|Vector3|
|TransformMatrix._11_12_13_14|Vector4|
|Color*|	Colors (Windows.UI)|

*Animating the Color subchannel of the Brush property is a bit different. You attach StartAnimation() to the Visual.Brush, and declare the property to animate in the parameter as "Color". 
(More details about animating color discussed later)

## Property Sets and Effects
In addition to animating properties of [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx) and InsetClip, you are also able to animate properties in a PropertySet or an Effect. 
For property sets, you define a property and store it in a Composition Property Set – that property can later be the target of an animation (and also be referenced simultaneously 
in another). This will be discussed in more detail in the following sections.  

For Effects, you are able to define graphical effects using the Composition Effects APIs (See here for the [Effects Overview](./composition-effects.md). 
In addition to defining Effects, you are also able to animate the property values of the Effect. 
This is done by targeting the properties component of the Brush property on Sprite Visuals.

## Quick Formula: Getting Started with Composition Animations
Before diving into the details on how to construct and use the different types of animations, below is a quick, high level formula for how to put together Composition Animations.  
1.	Decide which property, sub channel property or Effect you want to animate - make note of the type.  
2.	Create a new object for your animation – this will either be a KeyFrame or Expression Animation.  
	*  For KeyFrame animations, make sure you create a KeyFrame Animation type that matches the type of property you want to animate.  
	*  There is only a single type of Expression Animation.  
3.	Define the content for animation – Insert your Keyframes or define the Expression string  
	*  For KeyFrame animations, make sure the value of your KeyFrames are the same type as the property you want to animate.  
	*  For Expression animations, make sure your Expression string will resolve to the same type as the property you want to animate.  
4.	Start your animation on the [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx) whose property you want to animate – call StartAnimation and include as parameters:   the name of the property you want to animate (in string form) and the object for your animation.  

```cs
// KeyFrame Animation Example to target Opacity property
// Step 2 - Create your animation object
var animation = _compositor.CreateScalarKeyFrameAnimation();
// Step 3 - Define Content
animation.InsertKeyFrameAnimation(1.0f, 0.2f); 
// Step 4 - Attach animation to Visual property and start animation
_targetVisual.StartAnimation("Opacity", animation); 
  
// Expression Animation Example to target Opacity property
// Step 2 - Create your animation object
var expression = _compositor.CreateExpressionAnimation(); 
// Step 3 - Define Content (you can also define the string as part of the expression object
// declaration)
expression.Expression = "targetVisual.Offset.X / windowWidth";
expression.SetReferenceParameter("targetVisual", _target);
expression.SetScalarParameter("windowWidth", _xSizeWindow);
// Step 4 - Attach animation to Visual property and start animation
_targetVisual.StartAnimation("Opacity", expression);

```

## Using KeyFrame Animations
KeyFrame Animations are time-based animations that use one or more key frames to specify how the animated value should change over time. 
The frames represent markers or control points, allowing you to define what the animated value should be at a specific time.  
 
### Creating your animation and defining KeyFrames
To construct a KeyFrame Animation, use the constructor method of your Compositor object that correlates to the type of the property you wish to animate. 
The different types of KeyFrame Animation are:
*	ColorKeyFrameAnimation
*	QuaternionKeyFrameAnimation
*	ScalarKeyFrameAnimation
*	Vector2KeyFrameAnimation
*	Vector3KeyFrameAnimation
*	Vector4KeyFrameAnimation  

An example that creates a Vector3 KeyFrame Animation:     
```cs
var animation = _compositor.CreateVector3KeyFrameAnimation(); 
```

Each KeyFrame animation is constructed by inserting individual KeyFrame segments that define two components (with an optional third)  
*	Time: normalized progress state of the KeyFrame between 0.0 – 1.0
*	Value: specific value of the animating value at the time state
*	(Optional) Easing function: function to describe interpolation between previous and current KeyFrame (discussed later in this topic)  

An example that inserts a KeyFrame at the halfway point of the animation:
```cs
animation.InsertKeyFrame(0.5f, new Vector3(50.0f, 80.0f, 0.0f));
```

**Note:** When animating color with KeyFrame Animations, there are a few additional things to keep in mind:
1.	You attach StartAnimation to the Visual.Brush, instead of [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx), with **Color** as the property parameter you wish to animate.
2.	The "value" component of the KeyFrame is defined by the Colors object off of the Windows.UI namespace.
3.	You have the option to define the color space that the interpolation will go through by setting the InterpolationColorSpace property. Possible values include:
    *	CompositionColorSpace.Rgb
    *	CompositionColorSpace.Hsl


## KeyFrame Animation Properties
Once you've defined your KeyFrame Animation and the individual KeyFrames, you are able to define multiple properties off of your animation:
*	DelayTime – time before an animation starts after StartAnimation() is called
*	Duration – duration of the animation
*	IterationBehavior – count or infinite repeat behavior for an animation
*	IterationCount – number of finite times a KeyFrame Animation will repeat
*	KeyFrame Count – read of how many KeyFrames in a particular KeyFrame Animation
*	StopBehavior – specifies the behavior of an animating property value when StopAnimation is called  
*   Direction – specifies the direction of the animation for playback  

An example that sets the Duration of the animation to 5 seconds:  
```cs
animation.Duration = TimeSpan.FromSeconds(5);
```

## Easing Functions
Easing functions (CompositionEasingFunction) indicate how intermediate values progress from the previous key frame value to the current key frame value. 
If you do not provide an easing function for the KeyFrame, a default curve will be used.  
There are two types of easing functions supported:
*	Linear
*	Cubic Bezier  
*   Step  

Cubic Beziers are a parametric function frequently used to describe smooth curves that can be scaled. When using with Composition KeyFrame Animations, you define two control points that are Vector2 objects. These control points are used to define the shape of the curve. It is recommended to use similar sites such as [this](http://cubic-bezier.com/#0,-0.01,.48,.99) to visualize how the two control points construct the curve for a Cubic Bezier.

To create an easing function, utilize the constructor method off your Compositor object. Two examples below that create a Linear easing function and a basic Cubic Bezier easing function.    
```cs
var linear = _compositor.CreateLinearEasingFunction();
var easeIn = _compositor.CreateCubicBezierEasingFunction(new Vector2(0.5f, 0.0f), new Vector2(1.0f, 1.0f));
var step = _compositor.CreateStepEasingFunction();
```
To add your easing function into your KeyFrame, simply add in the third parameter to the KeyFrame when inserting into the Animation.   
An example that adds in a easeIn easing function with the KeyFrame:  
```cs
animation.InsertKeyFrame(0.5f, new Vector3(50.0f, 80.0f, 0.0f), easeIn);
```

## Starting and Stopping KeyFrame Animations
After you have defined your animation and KeyFrames, you are ready to hook up your animation. When starting your animation, you specify the [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx) to be animated, 
the target property to be animated and a reference to the animation. 
You do so by calling the StartAnimation() function. Remember that calling StartAnimation() on a property will disconnect and remove any previously running animations.  
**Note:** The reference to the property you choose to animate is in the form of a string.  

An example that sets and starts an animation on the [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx)’s Offset property:  
```cs
targetVisual.StartAnimation("Offset", animation);
```  

If you want to target a sub channel property, you add the subchannel into the string defining the property you want to animate. 
In the examples above, the syntax would change to StartAnimation("Offset.X, animation2), where animation2 is a ScalarKeyFrameAnimation.  

After starting your animation, you also have the ability to stop it before it finishes. This is done by using the StopAnimation() function.  
An example that stops an animation on the [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx)’s Offset property:    
```cs
targetVisual.StopAnimation("Offset");
```

You also have the ability to define the behavior of the animation when it is explicitly stopped. To do so, you define the Stop Behavior property off your animation. There are three options:
*	LeaveCurrentValue: The animation will mark the value of the animated property to be the last calculated value of the animation
*	SetToFinalValue: The animation will mark the value of the animated property to be the value of the final keyframe
*	SetToInitialValue: The animation will mark the value of the animated property to be the value of the first keyframe  

An example that sets the StopBehavior property for a KeyFrame Animation:  
```cs
animation.StopBehavior = AnimationStopBehavior.LeaveCurrentValue;
```

## Animation Completion Events
With KeyFrame Animations, developers can use an Animation Batches to aggregate when a select animation (or group of animations) have completed. 
Only KeyFrame animation completion events can be batched. Expressions do not have a definite end so they do not fire a completion event. 
If an Expression animation is started within a batch, the animation will execute as expected and it will not affect when the batch fires.    

A batch completion event fires when all animations within the batch have completed. 
The time it takes for a batch’s event to fire depends on the longest or most delayed animation in the batch.
 Aggregating end states is useful when you need to know when groups of select animations complete in order to schedule some other work.  

Batches will dispose once the completion event is fired. You can also call Dispose() at any time to release the resource early. 
You may want to manually dispose the batch object if a batched animation is ended early and you do not wish to pick up the completion event. 
If an animation is interrupted or canceled the completion event will fire and count towards the batch it was set in. 
This is demonstrated in the Animation_Batch SDK sample on the [Windows/Composition GitHub](http://go.microsoft.com/fwlink/p/?LinkId=789439).  
 
## Scoped batches
To aggregate a specific group of animations or target a single animation’s completion event, you create a Scoped batch.    
```cs
CompositionScopedBatch myScopedBatch = _compositor.CreateScopedBatch(CompositionBatchTypes.Animation);
``` 
After creating a Scoped batch, all started animations aggregate until the batch is explicitly suspended or ended using the Suspend or End function.    

Calling the Suspend function stops aggregating animates end states until Resume is called. This allows you to explicitly exclude content from a given batch.  

In the example below, the animation targeting the Offset property of VisualA will not be included in the batch:  
```cs
myScopedBatch.Suspend();
VisualA.StartAnimation("Offset", myAnimation);
myScopeBatch.Resume();
```

In order to complete your batch you must call End(). Without an End call, the batch will remain open forever-collecting objects.  
 
The following code snippet and diagram below shows an example of how the Batch will aggregate animations to track end states. 
Note that in this example, Animations 1, 3, and 4 will have end states tracked by this Batch, but Animation 2 will not.  
```cs
myScopedBatch.End();
CompositionScopedBatch myScopedBatch = 	_compositor.CreateScopedBatch(CompositionBatchTypes.Animation);
// Start Animation1
[…]
myScopedBatch.Suspend();
// Start Animation2 
[…]
myScopedBatch.Resume();
// Start Animation3
[…]
// Start Animation4
[…]
myScopedBatch.End();
```  
![The scoped batch contains animation one, animation three, and animation four while animation two is excluded from the scoped batch](./images/composition-scopedbatch.png)
 
## Batching a single animation's completion event
If you want to know when a single animation ends, you need to create a Scoped batch that will include just the animation you are targeting. 
For example:  
```cs
CompositionScopedBatch myScopedBatch = 	_compositor.CreateScopedBatch(CompositionBatchTypes.Animation);
Visual.StartAnimation("Opacity", myAnimation);
myScopedBatch.End();
```

## Retrieving a batch's completion event

When batching an animation or multiple animations, you will retrieve the batch’s completion event the same way. 
You register the event-handling method for the Completed event of the targeted batch.  

```cs
myScopedBatch.Completed += OnBatchCompleted;
``` 

## Batch states
There are two properties you can use to determine the state of an existing batch; IsActive and IsEnded.  

The IsActive property returns true if a targeted batch is open to aggregating animations. IsActive will return false when a batch is suspended or ended.   

The IsEnded property returns true when you cannot add an animation to that specific batch. A batch will be ended when you call explicitly call End() for a specific batch.  
 
## Using Expression Animations
Expression Animations are a new type of animation the Composition Team introduced with the November Update for Windows 10 (10586). 
At a high level, Expression Animations are based on a mathematical equation/relationship between discrete values and references to other Composition object properties. 
In contrast to KeyFrame Animations that use an interpolator function (Cubic Bezier, Quad, Quintic, etc.) to describe how the value changes over time, 
Expression Animations use a mathematical equation to define how the animated value is calculated each frame. 
It’s important to point out that Expression Animations do not have a defined duration – once started, 
they will run and use the mathematical equation to determine the value of the animating property until they are explicitly stopped.

**So why are Expression Animations useful?** 
The real power of Expression Animations comes from their ability to create a mathematical relationship that includes references to parameters or properties on other objects. 
This means you can have an equation referencing values of properties on other Composition objects, local variables, or even shared values in Composition Property Sets. 
Because of this reference model, and that the equation is evaluated every frame, if the values that define an equation change, so will the output of the equation. 
This opens up bigger possibilities beyond traditional KeyFrame Animations where values must be discrete and pre-defined. 
For example, experiences like Sticky Headers and Parallax can be easily described using Expression Animations.

**Note:** We use the terms "Expression" or "Expression String" as reference to your mathematical equation that defines your Expression Animation object.

## Creating and Attaching your Expression Animation
Before we jump into the syntax of creating Expression Animations, there are a few core principles to mention:  
*	Expression Animations use a defined mathematical equation to determine the value of the animating property every frame.
*	The mathematical equation is inputted into the Expression as a string.
*	The output of the mathematical equation must resolve to the same type as the property you plan to animate. If they don't match, you will get an error when the Expression gets calculated. If your equation resolves to Nan (number/0), the system will use the last previously calculated value.
*	Expression Animations have an *infinite lifetime* – they will continue to run until they are stopped.  

To create your Expression Animation, simply use the constructor off your Composition object, where you define your Mathematical expression.  
 
An example of the constructor where a very basic expression is defined that sums two Scalar values together (We will dive into more complicated expressions in the next section):  
```cs
var expression = _compositor.CreateExpressionAnimation("0.2 + 0.3");
```
Similar to KeyFrame Animations, once you have defined your Expression Animation, you need to attach it to the Visual and declare the property you wish the animation to animate. 
Below, we continue with the above example and attach our Expression Animation to the [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx)’s Opacity property (A Scalar type):  
```cs
targetVisual.StartAnimation("Opacity", expression);
```

## Components of your Expression String
The example in the previous section demonstrated two simple Scalar values being added together. 
Although this is a valid example of Expressions, it does not fully demonstrate the potential of what you can do with Expressions. 
One thing to note about the example above is that because these are discrete values, every frame the equation will resolve to 0.5 and never change throughout the lifetime of the animation. 
The real potential of Expressions comes from defining a mathematical relationship in which the values could change periodically or all the time.  
 
Let’s walk through the different pieces that can make up these types of Expressions.  

### Operators, Precedence and Associativity
The Expression string supports usage of typical operators you would expect to describe mathematical relationships between different components of the equation:  

|Category|	Operators|
|--------|-----------|
|Unary|	-|
|Multiplicative|	* /|
|Additive|	+ -|
|Mod| %|  

Similarly, when the Expression is evaluated, it will adhere to operator precedence and associativity as defined in the C# Language specification. 
Put another way, it will adhere to basic order of operations.  

In the example below, when evaluated, the parentheses will be resolved first before resolving the rest of the equation based on order of operations:  
```cs
"(5.0 * (72.4 – 36.0) + 5.0" // (5.0 * 36.4 + 5) -> (182 + 5) -> 187
```

### Property Parameters
Property parameters are one of the most powerful components of Expression Animations. 
In the expression string, you can reference values of properties from other objects such as [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx), Composition Property Set or other C# objects.   

To use these in an Expression string, you simply need to define the references as parameters to the Expression Animation. 
This is done by mapping the string used in the Expression to the actual object. This allows the system when evaluating the equation to know what to inspect to calculate the value. 
There are different types of parameters that correlate to the type of the object you wish to include in the equation:  

|Type|	Function to create parameter|
|----|------------------------------|
|Scalar|	SetScalarParameter(String ref, Scalar obj)|
|Vector|	SetVector2Parameter(String ref, Vector2 obj)<br/>SetVector3Parameter(String ref, Vector3 obj)<br/>SetVector4Parameter(String ref, Vector4 obj)|
|Matrix|	SetMatrix3x2Parameter(String ref, Matrix3x2 obj)<br/>SetMatrix4x4Parameter(String ref, Matrix4x4 obj)|
|Quaternion|	SetQuaternionParameter(String ref, Quaternion obj)|
|Color|	SetColorParameter(String ref, Color obj)|
|CompositionObject|	SetReferenceParameter(String ref, Composition object obj)|
|Boolean| SetBooleanParameter(String ref, Boolean obj)|  

In the example below, we create an Expression Animation that will reference the Offset of two other Composition [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx)s and a basic System.Numerics Vector3 object.  
```cs
var commonOffset = new Vector3(25.0, 17.0, 10.0);
var expression = _compositor.CreateExpressionAnimation("SomeOffset / ParentOffset + additionalOffset);
expression.SetVector3Parameter("SomeOffset", childVisual.Offset);
expression.SetVector3Parameter("ParentOffset", parentVisual.Offset);
expression.SetVector3Parameter("additionalOffset", commonOffset);
```

Additionally, you can reference a value in a Property Set from an expression using the same model described above. 
Composition Property Sets are a useful way to store data used by animations, and are useful for creating sharable, reusable data that isn’t tied to the lifetime of any other Composition objects. 
Property Set values can be referenced in an expression similar to other property references. (Property Sets are discussed in more detail in a later section)  

We can modify the example directly above, such that a property set is used to define the commonOffset instead of a local variable:
```cs
_sharedProperties = _compositor.CreatePropertySet();
_sharedProperties.InsertVector3("commonOffset", offset);
var expression = _compositor.CreateExpressionAnimation("SomeOffset / ParentOffset + sharedProperties.commonOffset");
expression.SetVector3Parameter("SomeOffset", childVisual.Offset);
expression.SetVector3Parameter("ParentOffset", parentVisual.Offset);
expression.SetReferenceParameter("sharedProperties", _sharedProperties);
```

Finally, when referencing properties of other objects, it also possible to reference the subchannel properties either in the Expression string or as part of the reference parameter.  
 
In the example below, we reference the x subchannel of Offset properties from two [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx)s – one in the Expression string itself and the other when creating the parameter reference.
Notice that when referencing the X component of Offset, we change our parameter type to a Scalar Parameter instead of a Vector3 like in the previous example:  
```cs
var expression = _compositor.CreateExpressionAnimation("xOffset/ ParentOffset.X");
expression.SetScalarParameter("xOffset", childVisual.Offset.X);
expression.SetVector3Parameter("ParentOffset", parentVisual.Offset);
```

### Expression Helper Functions and Constructors
In addition to having access to Operators and Property Parameters, you can leverage a list of mathematical functions to use in their expressions. 
These functions are provided to perform calculations and operations on different types that you would similarly do with System.Numerics objects.  

An example below creates an Expression targeted towards Scalars that takes advantage of the Clamp helper function:  
```cs
var expression = _compositor.CreateExpressionAnimation("Clamp((scroller.Offset.y * -1.0) – container.Offset.y, 0, container.Size.y – header.Size.y)"
```

In addition to a list of Helper functions, you are also able to use built-in Constructor methods inside an Expression string that will generate an instance of 
that type based on the provided parameters.  

An example below creates an Expression that defines a new Vector3 in the Expression string:  
```cs
var expression = _compositor.CreateExpressionAnimation("Offset / Vector3(targetX, targetY, targetZ");
```

You can find the full extensive list of helper functions and constructors in the Appendix section, or for each type in the list below:  
*	[Scalar](#scalar)
*	[Vector2](#vector2)
*	[Vector3](#vector3)
*	[Matrix3x2](#matrix3x2)
*	[Matrix4x4](#matrix4x4)
*	[Quaternion](#quaternion)
*	[Color](#color)  

### Expression Keywords
You can take advantage of special "keywords" that are treated differently when the Expression string is evaluated. 
Because they are considered "keywords" they can’t be used as the string parameter portion of their Property references.  
 
|Keyword|	Description|
|-------|--------------|
|This.StartingValue| Provides a reference to the original starting value of the property that is being animated.|
|This.CurrentValue|	Provides a reference to the currently "known" value of the property|
|Pi| Provides a keyword reference to the value of PI|

An example below that demonstrates using the this.StartingValue keyword:  
```cs
var expression = _compositor.CreateExpressionAnimation("this.StartingValue + delta");
```

### Expressions with Conditionals
In addition to supporting mathematical relationships using operators, property references, and functions and constructors, 
you can also create an expression that contains a ternary operator:  
```
(condition ? ifTrue_expression : ifFalse_expression)
```

Conditional statements enable you to write expressions such that based on a particular condition, 
different mathematical relationships will be used by the system to calculate the value of the animating property. 
Ternary operators can be nested as the expressions for the true or false statements.  

The following conditional operators are supported in the condition statement: 
*	Equals (==)
*	Not Equals (!=)
*	Less than (<)
*	Less than or equal to (<=)
*	Great than (>)
*	Great than or equal to (>=)  

The following conjunctions are supported as operators or functions in the condition statement:
*	Not: ! / Not(bool1)
*	And: && / And(bool1, bool2)
*	Or: || / Or(bool1, bool2)  

Below is an example of an Expression Animation using a conditional.  
```cs
var expression = _compositor.CreateExpressionAnimation("target.Offset.x > 50 ? 0.0f + (target.Offset.x / parent.Offset.x) : 1.0f");
```

## Expression KeyFrames
Earlier in this document, we described how you create KeyFrame Animations and introduced you to Expression Animations and all the different pieces 
that you can use to make up the Expression string. What if you wanted the power from Expressions Animations but wanted time interpolation provided by KeyFrame Animations? 
The answer is Expression KeyFrames!  

Instead of defining a discrete value for each control points in the KeyFrame Animation, you can have the value be an Expression string. 
In this situation, the system will use the expression string to calculate what the value of the animating property should be at the given point in the timeline. 
The system will then simply interpolate to this value like in a normal keyframe animation.    

You don’t need to create special animations to use Expression KeyFrames – just insert an ExpressionKeyFrame into your standard KeyFrame animation, 
provide the time and your expression string as the value. The example below demonstrates this, using an Expression string as the value for one of the KeyFrames:   
```cs
var animation = _compositor.CreateScalarKeyFrameAnimation();
animation.InsertExpressionKeyFrame(0.25, "VisualBOffset.X / VisualAOffset.Y");
animation.InsertKeyFrame(1.00f, 0.8f);
```

## Expression Sample
The code below shows an example of setting up an expression animation for a basic Parallax experience that pulls input values from the Scroll Viewer.
```cs
// Get scrollviewer
ScrollViewer myScrollViewer = ThumbnailList.GetFirstDescendantOfType<ScrollViewer>();
_scrollProperties = ElementCompositionPreview.GetScrollViewerManipulationPropertySet(myScrollViewer);

// Setup the expression
_parallaxExpression = compositor.CreateExpressionAnimation();
_parallaxExpression.SetScalarParameter("StartOffset", 0.0f);
_parallaxExpression.SetScalarParameter("ParallaxValue", 0.5f);
_parallaxExpression.SetScalarParameter("ItemHeight", 0.0f);
_parallaxExpression.SetReferenceParameter("ScrollManipulation", _scrollProperties);
_parallaxExpression.Expression = "(ScrollManipulation.Translation.Y + StartOffset - (0.5 * 	ItemHeight)) * ParallaxValue - (ScrollManipulation.Translation.Y + StartOffset - (0.5 	* ItemHeight))";
```

## Animating With Property Sets
Composition Property Sets provide you with the ability to store values that can be shared across multiple animations and are not tied to the lifetime of another Composition object. 
Property Sets are extremely useful to store common values and then easily reference them later on in animations. 
You can also use Property Sets to store data based on application logic to drive an expression.  

To create a property set, use the constructor method off your Compositor object:  
```cs
_sharedProperties = _compositor.CreatePropertySet();
```

Once you’ve created your Property Set, you can add a property and value to it:  
```cs
_sharedProperties.InsertVector3("NewOffset", offset);
```

Similar to what we’ve seen earlier, we can reference this property set value in an Expression Animation:  
```cs
var expression = _compositor.CreateExpressionAnimation("this.target.Offset + sharedProperties.NewOffset");
expression.SetReferenceParameter("sharedProperties", _sharedProperties);
targetVisual.StartAnimation("Offset", expression);
```

Property set values can also be animated. This is done by attaching the animation to the PropertySet object, and then referring to the property name in the string. 
Below, we animate the NewOffset property in the property set using a KeyFrame Animation.  
```cs
var keyFrameAnimation = _compositor.CreateVector3KeyFrameAnimation()
keyFrameAnimation.InsertKeyFrame(0.5f, new Vector3(25.0f, 68.0f, 0.0f);
keyFrameAnimation.InsertKeyFrame(1.0f, new Vector3(89.0f, 125.0f, 0.0f);
_sharedProperties.StartAnimation("NewOffset", keyFrameAnimation);
```


You might be wondering if this code executed in an app, what happens to the animated property value the Expression Animation is attached to. 
In this situation, the expression would initially output to a value, however, once the KeyFrame Animation begins to animate the Property in the Property Set, 
the Expression value will update as well, since the equation is calculated every frame. This is the beauty of Property Sets with Expression and KeyFrame Animations!  
 
## ManipulationPropertySet
In addition to utilizing Composition Property Sets, a developer is also able to gain access to the ManipulationPropertySet that allows access to properties off of a XAML ScrollViewer. These properties can then be used and referenced in an Expression Animation to power experiences like Parallax and Sticky Headers. Note: You can grab the ScrollViewer of any XAML control (ListView, GridView, etc.) that has scrollable content and use that ScrollViewer to get the ManipulationPropertySet for those scrollable controls.  

In your Expression, you are able to reference the following properties of the Scroll Viewer:  

|Property| Type|  
|--------|-----|  
|Translation| Vector3|  
|Pan| Vector3|  
|Scale| Vector3|  
|CenterPoint| Vector3|  
|Matrix| Matrix4x4|  

To get a reference to the ManipulationPropertySet, utilize the GetScrollViewerManipulationPropertySet method of ElementCompositionPreview.  
```csharp
CompositionPropertySet manipPropSet = ElementCompositionPreview.GetScrollViewerManipulationPropertySet(myScroller);
```

Once you have a reference to this property set, you can reference properties of the Scroll Viewer that are found in the property set. First step is to create the reference parameter.  
```csharp
ExpressionAnimation exp = compositor.CreateExpressionAnimation();
exp.SetReferenceParameter("ScrollManipulation", manipPropSet);
```

After setting up the reference parameter, you can reference the ManipulationPropertySet properties in the Expression.  
```csharp
exp.Expression = "ScrollManipulation.Translation.Y / ScrollBounds";
_target.StartAnimation("Opacity", exp);
```

## Using Implicit Animations  
Animations are a great way for you to describe a behavior to your users. There are multiple ways you can animate your content, but all of the methods discussed so far require you to explicitly *Start* your animation. Although this allows you to have complete control to define when an animation will begin, it becomes difficult to manage when an animation is needed every time a property value will be changed. This occurs quite often when applications have separated the app “personality” that defines the animations from the app “logic” that defines core components and infrastructure of the app. Implicit animations provide an easier and cleaner way to define the animation separately from the core app logic. You can hook these animations up to run with specific property change triggers.

### Setting up your ImplicitAnimationCollection  
Implicit animations are defined by other **CompositionAnimation** objects (**KeyFrameAnimation** or **ExpressionAnimation**). The **ImplicitAnimationCollection** represents the  set of **CompositionAnimation** objects that will start when the property change *trigger* is met. Note when defining animations, make sure to set the **Target** property, this defines the [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx) property the animation will target when it is started. The property of **Target** can only be a [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx) property that is animatable.
In the code snippet below, a single **Vector3KeyFrameAnimation** is created and defined as part of the **ImplicitAnimationCollection**. The **ImplicitAnimationCollection** is then attached to the **ImplicitAnimation** property of [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx), such that when the trigger is met, the animation will start.  
```csharp
Vector3KeyFrameAnimation animation = _compositor.CreateVector3KeyFrameAnimation();
animation.DelayTime =  TimeSpan.FromMilliseconds(index);
animation.InsertExpressionKeyFrame(1.0f, "this.FinalValue");
animation.Target = "Offset";
ImplicitAnimationCollection implicitAnimationCollection = compositor.CreateImplicitAnimationCollection();

visual.ImplicitAnimations = implicitAnimationCollection;
```


### Triggering when the ImplicitAnimation starts  
Trigger is the term used to describe when animations will start implicitly. Currently triggers are defined as changes to any of the animatable properties on [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx) – these changes occur through explicit sets on the property. For example, by placing an **Offset** trigger on an **ImplicitAnimationCollection**, and associating an animation with it, updates to the **Offset** of the targeted [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx) will animate to its new value using the animation in the collection.  
From the example above, we add this additional line to set the trigger to the **Offset** property of the target [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx).  
```csharp
implicitAnimationCollection["Offset"] = animation;
```  
Note that an **ImplicitAnimationCollection** can have multiple triggers. This means that the implicit animation or group of animations can get started for changes to different properties. In the example above, the developer can potentially add a trigger for other properties such as Opacity.  
###this.FinalValue     
In the first implicit example, we used an ExpressionKeyFrame for the “1.0” KeyFrame and assigned the expression of **this.FinalValue** to it. **this.FinalValue** is a reserved keyword in the expression language that provides differentiating behavior for implicit animations. **this.FinalValue** binds the value set on the API property to the animation. This helps in creating true templates. **this.FinalValue** is not useful in explicit animations, as the API property is set instantly, whereas in case of implicit animations it is deferred.  
 
## Using Animation Groups  
**CompositionAnimationGroup** provides an easy way for developers to group a list of animations that can be be used with implicit or explicit animations.   
### Creating and Populating Animation Groups  
The **CreateAnimationGroup** method of the of the Compositor object enables developers to create an Animation Group:  
```sharp
CompositionAnimationGroup animationGroup = _compositor.CreateAnimationGroup();
animationGroup.Add(animationA);
animationGroup.Add(animationB);
```   
Once the group is created, individual animations can be added to the animation group. Remember, that you do not need to explicitly start the individual animations – these will all get started when either **StartAnimationGroup** is called for the explicit scenario or when the trigger is met for the implicit one.  
Note, ensure that the animations that are added to the group have their **Target** property defined. This will define what property of the target [Visual](https://msdn.microsoft.com/library/windows/apps/windows.ui.composition.visual.aspx) they will animate.

### Using Animation Groups with Implicit Animations  
Developers can create implicit animations such that when a trigger is met, a set of animations in the form of an animation group are started. In this case, define the animation group as the set of animations that start when the trigger is met.  
```csharp
implicitAnimationCollection["Offset"] = animationGroup;
```   
### Using Animation Groups with Explicit Animations  
Developers can create explicit animations such that the individual animations added will start when **StartAnimationGroup** is called. Note, that in this **StartAnimation** call, there is no targeted property for the group as individual animations could be targeting different properties. Ensure that the target property for each animation is set.  
```csharp
visual.StartAnimationGroup(AnimationGroup);
```  

### E2E sample 
This example shows animating the Offset property implicitly when a new value is set.  
```csharp 
class PropertyAnimation
{
	PropertyAnimation(Compositor compositor, SpriteVisual heroVisual, SpriteVisual listVisual)
	{
		// Define ImplicitAnimationCollection
		ImplicitAnimationCollection implicitAnimations = 
		compositor.CreateImplicitAnimationCollection();

		// Trigger animation when the “Offset” property changes.
		implicitAnimations["Offset"] = CreateAnimation(compositor);

		// Assign ImplicitAnimations to a visual. Unlike Visual.Children,    
		// ImplicitAnimations can be shared by multiple visuals so that they 
		// share the same implicit animation behavior (same as Visual.Clip).
		heroVisual.ImplicitAnimations = implicitAnimations;

		// ImplicitAnimations can be shared among visuals 
		listVisual.ImplicitAnimations = implicitAnimations;

		listVisual.Offset = new Vector3(20f, 20f, 20f);
	}

	Vector3KeyFrameAnimation CreateAnimation(Compositor compositor)
	{
		Vector3KeyFrameAnimation animation = compositor.CreateVector3KeyFrameAnimation();
		animation.InsertExpressionKeyFrame(0f, "this.StartingValue");
		animation.InsertExpressionKeyFrame(1f, "this.FinalValue");
		animation.Target = “Offset”;
		animation.Duration = TimeSpan.FromSeconds(0.25);
		return animation;
	}
}
```   

 
 
## Appendix
### Expression Functions by Structure Type
### Scalar  

|Function and Constructor Operations| Description|  
|-----------------------------------|--------------|  
|Abs(Float value)| Returns a Float representing the absolute value of the float parameter|  
|Clamp(Float value, Float min, Float max)| Returns a  float value that is either greater than min and less than max or min if the value is less than min or max if the value is greater than max|  
|Max (Float value1, Float value2)| Returns the greater float between value1 and value2.|  
|Min (Float value1, Float value2)| Returns the lesser float between value1 and value2.|  
|Lerp(Float value1, Float value2, Float progress)| Returns a float that represents the calculated linear interpolation calculation between the two Scalar values based on the progress (Note: Progress is between 0.0 and 1.0)|  
|Slerp(Float value1, Float value2, Float progress)|	Returns a Float that represents the calculated spherical interpolation between the two Float values based on the progress (Note: progress is between 0.0 and 1.0)|  
|Mod(Float value1, Float value2)| Returns the Float remainder resulting from the division of value1 and value2|  
|Ceil(Float value)| Returns the Float parameter rounded to next greater whole number|  
|Floor(Float value)| Returns the Float parameter to the next lesser whole number|  
|Sqrt(Float value)|	Returns the square root of the Float parameter|  
|Square(Float value)| Returns the square of the Float parameter|  
|Sin(Float value1)| Returns the Sin of the Float parameter|
|Asin(Float value2)| Returns the ArcSin of the Float parameter|
|Cos(Float value1)| Returns the Cos of the Float parameter|
|ACos(Float value2)| Returns the ArcCos of the Float parameter|
|Tan(Float value1)| Returns the Tan of the Float parameter|
|ATan(Float value2)| Returns the ArcTan of the Float parameter|
|Round(Float value)| Returns the Float parameter rounded to the nearest whole number|
|Log10(Float value)| Returns the Log (base 10) result of the Float parameter|
|Ln(Float value)| Returns the Natural Log result of the Float parameter|
|Pow(Float value, Float power)|	Returns the result of the Float parameter raised to a particular power|
|ToDegrees(Float radians)| Returns the Float parameter converted into Degrees|
|ToRadians(Float degrees)| Returns the Float parameter converted into Radians|

### Vector2  

|Function and Constructor Operations|	Description|
|-----------------------------------|--------------|
|Abs (Vector2 value)|	Returns a Vector2 with absolute value applied to each component|
|Clamp (Vector2 value1, Vector2 min, Vector2 max)|	Returns a Vector2 that contains the clamped values for each respective component|
|Max (Vector2 value1, Vector2 value2)|	Returns a Vector2 that has performed a Max on each of the corresponding components from value1 and value2|
|Min (Vector2 value1, Vector2 value2)|	Returns a Vector2 that has performed a Min on each of the corresponding components from value1 and value2|
|Scale(Vector2 value, Float factor)|	Returns a Vector2 with each component of the vector multiplied by the scaling factor.|
|Transform(Vector2 value, Matrix3x2 matrix)|	Returns a Vector2 resulting from the linear transformation between a Vector2 and a Matrix3x2 (aka multiplying a vector by a matrix).|
|Lerp(Vector2 value1, Vector2 value2, Float progress)|	Returns a Vector2 that represents the calculated linear interpolation calculation between the two Vector2 values based on the progress (Note: Progress is between 0.0 and 1.0)|
|Length(Vector2 value)|	Returns a Float value representing the length/magnitude of the Vector2|
|LengthSquared(Vector2)|	Returns a Float value representing the square of the length/magnitude of a Vector2|
|Distance(Vector2 value1, Vector2 value2)|	Returns a Float value representing the distance between two Vector2 values|
|DistanceSquared(Vector2 value1, Vector2 value2)|	Returns a Float value representing the square of the distance between two Vector2 values|
|Normalize(Vector2 value)|	Returns a Vector2 representing the unit vector of the parameter where all components have been normalized|
|Vector2(Float x, Float y)|	Constructs a Vector2 using two Float parameters|

### Vector3  

|Function and Constructor Operations|	Description|
|-----------------------------------|--------------|
|Abs (Vector3 value)|	Returns a Vector3 with absolute value applied to each component|
|Clamp (Vector3 value1, Vector3 min, Vector3 max)|	Returns a Vector3 that contains the clamped values for each respective component|
|Max (Vector3 value1, Vector3 value2)|	Returns a Vector3 that has performed a Max on each of the corresponding components from value1 and value2|
|Min (Vector3 value1, Vector3 value2)|	Returns a Vector3 that has performed a Min on each of the corresponding components from value1 and value2|
|Scale(Vector3 value, Float factor)|	Returns a Vector3 with each component of the vector multiplied by the scaling factor.|
|Lerp(Vector3 value1, Vector3 value2, Float progress)|	Returns a Vector3 that represents the calculated linear interpolation calculation between the two Vector3 values based on the progress (Note: Progress is between 0.0 and 1.0)|
|Length(Vector3 value)|	Returns a Float value representing the length/magnitude of the Vector3|
|LengthSquared(Vector3)|	Returns a Float value representing the square of the length/magnitude of a Vector3|
|Distance(Vector3 value1, Vector3 value2)|	Returns a Float value representing the distance between two Vector3 values|
|DistanceSquared(Vector3 value1, Vector3 value2)|	Returns a Float value representing the square of the distance between two Vector3 values|
|Normalize(Vector3 value)|	Returns a Vector3 representing the unit vector of the parameter where all components have been normalized|
|Vector3(Float x, Float y, Float z)|	Constructs a Vector3 using three Float parameters|

### Vector4  

|Function and Constructor Operations|	Description|
|-----------------------------------|--------------|
|Abs (Vector4 value)|	Returns a Vector3 with absolute value applied to each component|
|Clamp (Vector4 value1, Vector4 min, Vector4 max)|	Returns a Vector4 that contains the clamped values for each respective component|
|Max (Vector4 value1 Vector4 value2)|	Returns a Vector4 that has performed a Max on each of the corresponding components from value1 and value2|
|Min (Vector4 value1 Vector4 value2)|	Returns a Vector4 that has performed a Min on each of the corresponding components from value1 and value2|
|Scale(Vector3 value, Float factor)|	Returns a Vector3 with each component of the vector multiplied by the scaling factor.|
|Transform(Vector4 value, Matrix4x4 matrix)|	Returns a Vector4 resulting from the linear transformation between a Vector4 and a Matrix4x4 (aka multiplying a vector by a matrix).|
|Lerp(Vector4 value1, Vector4 value2, Float progress)|	Returns a Vector4 that represents the calculated linear interpolation calculation between the two Vector4 values based on the progress (Note: progress is between 0.0 and 1.0)|
|Length(Vector4 value)|	Returns a Float value representing the length/magnitude of the Vector4|
|LengthSquared(Vector4)|	Returns a Float value representing the square of the length/magnitude of a Vector4|
|Distance(Vector4 value1, Vector4 value2)|	Returns a Float value representing the distance between two Vector4 values|
|DistanceSquared(Vector4 value1, Vector4 value2)|	Returns a Float value representing the square of the distance between two Vector4 values|
|Normalize(Vector4 value)|	Returns a Vector4 representing the unit vector of the parameter where all components have been normalized|
|Vector4(Float x, Float y, Float z, Float w)| 	Constructs a Vector4 using four Float parameters|

### Matrix3x2  

|Function and Constructor Operations|	Description|
|-----------------------------------|--------------|
|Scale(Matrix3x2 value, Float factor)|	Returns a Matrix3x2 with each component of the matrix multiplied by the scaling factor.|
|Inverse(Matrix 3x2 value)|	Returns a Matrix3x2 object that represents the reciprocal matrix|
|Matrix3x2(Float M11, Float M12, Float M21, Float M22, Float M31, Float M32)|	Constructs a Matrix3x2 using 6 Float parameters|
|Matrix3x2.CreateFromScale(Vector2 scale)|	Constructs a Matrix3x2 from a Vector2   representing scale<br/>\[scale.X, 0.0<br/> 0.0, scale.Y<br/> 0.0, 0.0 \]|
|Matrix3x2.CreateFromTranslation(Vector2 translation)|	Constructs a Matrix3x2 from a Vector2 representing translation<br/>\[1.0, 0.0,<br/> 0.0, 1.0,<br/> translation.X, translation.Y\]|  
|Matrix3x2.CreateSkew(Float x, Float y, Vector2 centerpoint)| Constructs a Matrix3x2 from two Float and a Vector2 representing skew<br/>\[1.0, Tan(y),<br/>Tan(x), 1.0,<br/>-centerpoint.Y * Tan(x), -centerpoint.X * Tan(y)\]|  
|Matrix3x2.CreateRotation(Float radians)| Constructs a Matrix3x2 from a rotation in radians<br/>\[Cos(radians), Sin(radians),<br/>-Sin(radians), Cos(radians),<br/>0.0, 0.0 \]|   
|Matrix3x2.CreateTranslation(Vector2 translation)| Same as CreateFromTranslation|      
|Matrix3x2.CreateScale(Vector2 scale)| Same as CreateFromScale|    

	
### Matrix4x4  

|Function and Constructor Operations|	Description|
|-----------------------------------|--------------|
|Scale(Matrix4x4 value, Float factor)|	Returns a Matrix 4x4 with each component of the matrix multiplied by the scaling factor.|
|Inverse(Matrix4x4)|	Returns a Matrix4x4 object that represents the reciprocal matrix|
|Matrix4x4(Float M11, Float M12, Float M13, Float M14,<br/>Float M21, Float M22, Float M23, Float M24,<br/>	   Float M31, Float M32, Float M33, Float M34,<br/>	   Float M41, Float M42, Float M43, Float M44)|	Constructs a Matrix4x4 using 16 Float parameters|
|Matrix4x4.CreateFromScale(Vector3 scale)|	Constructs a Matrix4x4 from a Vector3 representing scale<br/>\[scale.X, 0.0, 0.0, 0.0,<br/> 0.0, scale.Y, 0.0, 0.0,<br/> 0.0, 0.0, scale.Z, 0.0,<br/> 0.0, 0.0, 0.0, 1.0\]|
|Matrix4x4.CreateFromTranslation(Vector3 translation)|	Constructs a Matrix4x4 from a Vector3 representing translation<br/>\[1.0, 0.0, 0.0, 0.0,<br/> 0.0, 1.0, 0.0, 0.0,<br/> 0.0, 0.0, 1.0, 0.0,<br/> translation.X, translation.Y, translation.Z, 1.0\]|
|Matrix4x4.CreateFromAxisAngle(Vector3 axis, Float angle)|	Constructs a Matrix4x4 from a Vector3 axis and a Float representing an angle|
|Matrix4x4(Matrix3x2 matrix)| Constructs a Matrix4x4 using a Matrix3x2<br/>\[matrix.11, matrix.12, 0, 0,<br/>matrix.21, matrix.22, 0, 0,<br/>0, 0, 1, 0,<br/>matrix.31, matrix.32, 0, 1\]|  
|Matrix4x4.CreateTranslation(Vector3 translation)| Same as CreateFromTranslation|  
|Matrix4x4.CreateScale(Vector3 scale)| Same as CreateFromScale|  


### Quaternion  

|Function and Constructor Operations|	Description|
|-----------------------------------|--------------|
|Slerp(Quaternion value1, Quaternion value2, Float progress)|	Returns a Quaternion that represents the calculated spherical interpolation between the two Quaternion values based on the progress (Note: progress is between 0.0 and 1.0)|
|Concatenate(Quaternion value1 Quaternion value2)|	Returns a Quaternion representing a concatenation of two Quaternions (aka a Quaternion that represents a combined two individual rotations)|
|Length(Quaternion value)|	Returns a Float value representing the length/magnitude of the Quaternion.|
|LengthSquared(Quaternion)|	Returns a Float value representing the square of the length/magnitude of a Quaternion|
|Normalize(Quaternion value)|	Returns a Quaternion whose components have been normalized|
|Quaternion.CreateFromAxisAngle(Vector3 axis, Scalar angle)|	Constructs a Quaternion from a Vector3 axis and a Scalar representing an angle|
|Quaternion(Float x, Float y, Float z, Float w)|	Constructs a Quaternion from four Float values|

### Color

|Function and Constructor Operations|	Description|
|-----------------------------------|--------------|
|ColorLerp(Color colorTo, Color colorFrom, Float progress)|	Returns a Color object that represents the calculated linear interpolation value between two color objects based on a given progress. (Note: Progress is between 0.0 and 1.0)|
|ColorLerpRGB(Color colorTo, Color colorFrom, Float progress)|	Returns a Color object that represents the calculated linear interpolation value between two objects based on a given progress in the RGB color space.|
|ColorLerpHSL(Color colorTo, Color colorFrom, Float progress)|	Returns a Color object that represents the calculated linear interpolation value between two objects based on a given progress in the HSL color space.|
|ColorArgb(Float a, Float r, Float g, Float b)|	Constructs an object representing Color defined by ARGB components|
|ColorHsl(Float h, Float s, Float l)|	Constructs an object representing Color defined by HSL components (Note: Hue is defined from 0 and 2pi)|




