---
description: You can use the PropertyPath class and the string syntax to instantiate a PropertyPath value either in XAML or in code.
title: Property-path syntax
ms.assetid: FF3ECF47-D81F-46E3-BE01-C839E0398025
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Property-path syntax


You can use the [**PropertyPath**](/uwp/api/Windows.UI.Xaml.PropertyPath) class and the string syntax to instantiate a **PropertyPath** value either in XAML or in code. **PropertyPath** values are used by data binding. A similar syntax is used for targeting storyboarded animations. For both scenarios, a property path describes a traversal of one or more object-property relationships that eventually resolve to a single property.

You can set a property path string directly to an attribute in XAML. You can use the same string syntax to construct a [**PropertyPath**](/uwp/api/Windows.UI.Xaml.PropertyPath) that sets a [**Binding**](/uwp/api/Windows.UI.Xaml.Data.Binding) in code, or to set an animation target in code using [**SetTargetProperty**](/uwp/api/windows.ui.xaml.media.animation.storyboard.settargetproperty). There are two distinct feature areas in the Windows Runtime that use a property path: data binding, and animation targeting. Animation targeting doesn't create underlying Property-path syntax values in the Windows Runtime implementation, it keeps the info as a string, but the concepts of object-property traversal are very similar. Data binding and animation targeting each evaluate a property path slightly differently, so we describe property path syntax separately for each.

## Property path for objects in data binding

In Windows Runtime, you can bind to the target value of any dependency property. The source property value for a data binding doesn't have to be a dependency property; it can be a property on a business object (for example a class written in a Microsoft .NET language or C++). Or, the source object for the binding value can be an existing dependency object already defined by the app. The source can be referenced either by a simple property name, or by a traversal of the object-property relationships in the object graph of the business object.

You can bind to an individual property value, or you can bind to a target property that holds lists or collections. If your source is a collection, or if the path specifies a collection property, the data-binding engine matches the collection items of the source to the binding target, resulting in behavior such as populating a [**ListBox**](/uwp/api/Windows.UI.Xaml.Controls.ListBox) with a list of items from a data source collection without needing to anticipate the specific items in that collection.

### Traversing an object graph

The element of the syntax that denotes the traversal of an object-property relationship in an object graph is the dot (**.**) character. Each dot in a property path string indicates a division between an object (left side of the dot) and a property of that object (right side of the dot). The string is evaluated left-to-right, which enables stepping through multiple object-property relationships. Let's look at an example:

``` syntax
"{Binding Path=Customer.Address.StreetAddress1}"
```

Here's how this path is evaluated:

1.  The data context object (or a [**Source**](/uwp/api/windows.ui.xaml.data.binding.source) specified by the same [**Binding**](/uwp/api/Windows.UI.Xaml.Data.Binding)) is searched for a property named "Customer".
2.  The object that is the value of the "Customer" property is searched for a property named "Address".
3.  The object that is the value of the "Address" property is searched for a property named "StreetAddress1".

At each of these steps, the value is treated as an object. The type of the result is checked only when the binding is applied to a specific property. This example would fail if "Address" were just a string value that didn't expose what part of the string was the street address. Typically, the binding is pointing to the specific nested property values of a business object that has a known and deliberate information structure.

### Rules for the properties in a data-binding property path

-   All properties referenced by a property path must be public in the source business object.
-   The end property (the property that is the last named property in the path) must be public and must be mutable – you can't bind to static values.
-   The end property must be read/write if this path is used as the [**Path**](/uwp/api/windows.ui.xaml.data.binding.path) information for a two-way binding.

### Indexers

A property path for data-binding can include references to indexed properties. This enables binding to ordered lists/vectors, or to dictionaries/maps. Use square brackets "\[\]" characters to indicate an indexed property. The contents of these brackets can be either an integer (for ordered list) or an unquoted string (for dictionaries). You can also bind to a dictionary where the key is an integer. You can use different indexed properties in the same path with a dot separating the object-property.

For example, consider a business object where there is a list of "Teams" (ordered list), each of which has a dictionary of "Players" where each player is keyed by last name. An example property path to a specific player on the second team is: "Teams\[1\].Players\[Smith\]". (You use 1 to indicate the second item in "Teams" because the list is zero-indexed.)

**Note**  Indexing support for C++ data sources is limited; see [Data binding in depth](../data-binding/data-binding-in-depth.md).

### Attached properties

Property paths can include references to attached properties. Because the identifying name of an attached property already includes a dot, you must enclose any attached property name within parentheses so that the dot isn't treated as an object-property step. For example, the string to specify that you want to use [**Canvas.ZIndex**](/previous-versions/windows/silverlight/dotnet-windows-silverlight/cc190397(v=vs.95)) as a binding path is "(Canvas.ZIndex)". For more info on attached properties see [Attached properties overview](attached-properties-overview.md).

### Combining property path syntax

You can combine various elements of property path syntax in a single string. For example, you can define a property path that references an indexed attached property, if your data source had such a property.

### Debugging a binding property path

Because a property path is interpreted by a binding engine and relies on info that may be present only at run-time, you must often debug a property path for binding without being able to rely on conventional design-time or compile-time support in the development tools. In many cases the run-time result of failing to resolve a property path is a blank value with no error, because that is the by-design fallback behavior of binding resolution. Fortunately, Microsoft Visual Studio provides a debug output mode that can isolate which part of a property path that's specifying a binding source failed to resolve. For more info on using this development tool feature, see ["Debugging" section of Data binding in depth](../data-binding/data-binding-in-depth.md#debugging).

## Property path for animation targeting

Animations rely on targeting a dependency property where storyboarded values are applied when the animation runs. To identify the object where the property to be animated exists, the animation targets an element by name ([x:Name attribute](x-name-attribute.md)). It is often necessary to define a property path that starts with the object identified as the [**Storyboard.TargetName**](/dotnet/api/system.windows.media.animation.storyboard.targetname), and ends with the particular dependency property value where the animation should apply. That property path is used as the value for [**Storyboard.TargetProperty**](/previous-versions/windows/silverlight/dotnet-windows-silverlight/ms616983(v=vs.95)).

For more info on the how to define animations in XAML, see [Storyboarded animations](../design/motion/storyboarded-animations.md).

## Simple targeting

If you are animating a property that exists on the targeted object itself, and that property's type can have an animation applied directly to it (rather than to a sub-property of a property's value) then you can simply name the property being animated without any further qualification. For example, if you are targeting a [**Shape**](/uwp/api/Windows.UI.Xaml.Shapes.Shape) subclass such as [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle), and you are applying an animated [**Color**](/uwp/api/Windows.UI.Color) to the [**Fill**](/uwp/api/Windows.UI.Xaml.Shapes.Shape.Fill) property, your property path can be "Fill".

## Indirect property targeting

You can animate a property that is a sub-property of the target object. In other words, if there's a property of the target object that's an object itself, and that object has properties, you must define a property path that explains how to step through that object-property relationship. Whenever you are specifying an object where you want to animate a sub-property, you enclose the property name in parentheses, and you specify the property in *typename*.*propertyname* format. For example, to specify that you want the object value of a target object's [**RenderTransform**](/uwp/api/windows.ui.xaml.uielement.rendertransform) property, you specify "(UIElement.RenderTransform)" as the first step in the property path. This isn't yet a complete path, because there are no animations that can apply to a [**Transform**](/uwp/api/Windows.UI.Xaml.Media.Transform) value directly. So for this example, you now complete the property path so that the end property is a property of a **Transform** subclass that can be animated by a **Double** value: "(UIElement.RenderTransform).(CompositeTransform.TranslateX)"

## Specifying a particular child in a collection

To specify a child item in a collection property, you can use a numeric indexer. Use square brackets "\[\]" characters around the integer index value. You can reference only ordered lists, not dictionaries. Because a collection isn't a value that can be animated, an indexer usage can never be the end property in a property path.

For example, to specify that you want to animate the first color stop color in a [**LinearGradientBrush**](/uwp/api/Windows.UI.Xaml.Media.LinearGradientBrush) that is applied to a control's [**Background**](/uwp/api/windows.ui.xaml.controls.control.background) property, this is the property path: "(Control.Background).(GradientBrush.GradientStops)\[0\].(GradientStop.Color)". Note how the indexer is not the last step in the path, and that the last step particularly must reference the [**GradientStop.Color**](/uwp/api/windows.ui.xaml.media.gradientstop.color) property of item 0 in the collection to apply a [**Color**](/uwp/api/Windows.UI.Color) animated value to it.

## Animating an attached property

It isn't a common scenario, but it is possible to animate an attached property, so long as that attached property has a property value that matches an animation type. Because the identifying name of an attached property already includes a dot, you must enclose any attached property name within parentheses so that the dot isn't treated as an object-property step. For example, the string to specify that you want to animate the [**Grid.Row**](/dotnet/api/system.windows.controls.grid.row) attached property on an object, use the property path "(Grid.Row)".

**Note**  For this example, the value of [**Grid.Row**](/dotnet/api/system.windows.controls.grid.row) is an **Int32** property type. so you can't animate it with a **Double** animation. Instead, you'd define an [**ObjectAnimationUsingKeyFrames**](/uwp/api/Windows.UI.Xaml.Media.Animation.ObjectAnimationUsingKeyFrames) that has [**DiscreteObjectKeyFrame**](/uwp/api/Windows.UI.Xaml.Media.Animation.DiscreteObjectKeyFrame) components, where the [**ObjectKeyFrame.Value**](/uwp/api/windows.ui.xaml.media.animation.objectkeyframe.value) is set to an integer such as "0" or "1".

## Rules for the properties in an animation targeting property path

-   The assumed starting point of the property path is the object identified by a [**Storyboard.TargetName**](/dotnet/api/system.windows.media.animation.storyboard.targetname).
-   All objects and properties referenced along the property path must be public.
-   The end property (the property that is the last named property in the path) must be public, be read-write, and be a dependency property.
-   The end property must have a property type that is able to be animated by one of the broad classes of animation types ([**Color**](/uwp/api/Windows.UI.Color) animations, **Double** animations, [**Point**](/uwp/api/Windows.Foundation.Point) animations, [**ObjectAnimationUsingKeyFrames**](/uwp/api/Windows.UI.Xaml.Media.Animation.ObjectAnimationUsingKeyFrames)).

## The PropertyPath class

The [**PropertyPath**](/uwp/api/Windows.UI.Xaml.PropertyPath) class is the underlying property type of [**Binding.Path**](/uwp/api/windows.ui.xaml.data.binding.path) for the binding scenario.

Most of the time, you can apply a [**PropertyPath**](/uwp/api/Windows.UI.Xaml.PropertyPath) in XAML without using any code at all. But in some cases you may want to define a **PropertyPath** object using code and assign it to a property at run-time.

[**PropertyPath**](/uwp/api/Windows.UI.Xaml.PropertyPath) has a [**PropertyPath(String)**](/uwp/api/windows.ui.xaml.propertypath.-ctor) constructor, and doesn't have a default constructor. The string you pass to this constructor is a string that's defined using the property path syntax as we explained earlier. This is also the same string you'd use to assign [**Path**](/uwp/api/windows.ui.xaml.data.binding.path) as a XAML attribute. The only other API of the **PropertyPath** class is the [**Path**](/uwp/api/windows.ui.xaml.propertypath.path) property, which is read-only. You could use this property as the construction string for another **PropertyPath** instance.

## Related topics

* [Data binding in depth](../data-binding/data-binding-in-depth.md)
* [Storyboarded animations](../design/motion/storyboarded-animations.md)
* [{Binding} markup extension](binding-markup-extension.md)
* [**PropertyPath**](/uwp/api/Windows.UI.Xaml.PropertyPath)
* [**Binding**](/uwp/api/Windows.UI.Xaml.Data.Binding)
* [**Binding constructor**](/uwp/api/windows.ui.xaml.data.binding.-ctor)
* [**DataContext**](/uwp/api/windows.ui.xaml.frameworkelement.datacontext)