---
ms.assetid: 386faf59-8f22-2e7c-abc9-d04216e78894
title: Composition animations
description: Many composition object and effect properties can be animated using key frame and expression animations allowing properties of a UI element to change over time or based on a calculation.
ms.date: 10/10/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Composition animations

The Windows.UI.Composition APIs allows you to create, animate, transform and manipulate compositor objects in a unified API layer. Composition animations provide a powerful and efficient way to run animations in your application UI. They have been designed from the ground up to ensure that your animations run at 60 FPS independent of the UI thread and to give you the flexibility to build amazing experiences using not only time, but input and other properties, to drive animations.

## Motion in Windows

Think of motion design like a movie. Seamless transitions keep you focused on the story, and bring experiences to life. We can invite that feeling into our designs, leading people from one task to the next with cinematic ease. Motion is often the differentiating factor between a User Interface and a User Experience.

As a fundamental building block of the Windows UI Platform, CompositionAnimations provide a powerful and efficient way to create motion experiences in your application’s UI. The animation engine has been designed from the ground up to ensure that your motion runs at 60 FPS, independent of the UI thread. These animations are designed to provide the flexibility to build innovative motion experiences based on time, input, and other properties.

### Examples of motion

Here are some examples of motion in an app.

Here, an app uses a connected animation to animate an item image as it “continues” to become part of the header of the next page. The effect helps maintain user context across the transition.

![An example of Connected Animation](images/animation/connected-animation-example.gif)

Here, a visual parallax effect moves different objects at different rates when the UI scrolls or pans to create a feeling of depth, perspective, and movement.

![An example of parallax with a list and background image](images/animation/parallax-example.gif)

## Using CompositionAnimations to create Motion

To generate motion in UI, developers can access animations in either XAML or the Visual Layer. Animations at the Visual Layer provide developers with a series of benefits:

- Performance – instead of the traditional UI Thread-bound animation, animations on the Windows UI platform operate on an independent thread at 60 FPS, enabling smooth motion experiences.
- Templating Model – animations in the Windows UI layer are templates, meaning can use a single animation on multiple objects and tweak properties or parameters without worrying of obstructing previous uses.
- Customization – the Windows UI layer not only makes it easy to make beautiful UI, but with a full range of animation types, possible to create new and amazing experiences with a gradient of customizations

As a developer creating experiences at the Windows UI layer, you have access to a variety of animation concepts to bring your designs to life. You can use any of these concepts to animate a property or subchannel component (when applicable) of any CompositionObject.

> [!NOTE]
> Not all properties of a CompositionObject are animatable. Refer to the documentation of the individual CompositionObject to determine whether a property is animatable.

> [!NOTE]
> The term _subchannel_ refers to a component form of a property. For example, the X, or XY subchannel of a Vector3 Offset property.

| Animation concept | Description |
| ----------------- | ----------- |
| [Time-based motion with KeyFrameAnimations](time-animations.md)  | KeyFrameAnimations are used to directly control the entirety of a motion experience over a period of time. Developers describing a motion’s start, end, interpolation in between, and duration in a traditional keyframed fashion. |
| [Relative motion with ExpressionAnimations](relation-animations.md)  | ExpressionAnimations are used to describe how a motion of one object’s property should be driven relative to another object’s property. Developers define a mathematical equation that defines the reference-based relationship. |
| ImplicitAnimations | These animations are trigger-based and are defined separately from core app logic. ImplicitAnimations are used to describe how and when animations occur as a response to direct property changes. |
| [Input-driven motion with Input Animations](input-driven-animations.md)  | Input Animations covers a set of scenarios that enable developers to describe manipulation-based motion via touch or other input modalities. These animations are driven based on active user input or gestures. |
| [Physics-based motion with NaturalMotionAnimations](natural-animations.md)  | NaturalMotionAnimations are used to describe natural and familiar motion experiences based on real-world force driven motion. Rather than defining time, developers define characteristics of the motion (for example, damping ratio for Springs) |