---
description: This topic explains the dependency property system that is available when you write a Windows Runtime app using C++, C#, or Visual Basic along with XAML definitions for UI.
title: Dependency properties overview
ms.assetid: AD649E66-F71C-4DAA-9994-617C886FDA7E
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# Dependency properties overview

This topic explains the dependency property system that is available when you write a Windows Runtime app using C++, C#, or Visual Basic along with XAML definitions for UI.

## What is a dependency property?

A dependency property is a specialized type of property. Specifically it's a property where the property's value is tracked and influenced by a dedicated property system that is part of the Windows Runtime.

In order to support a dependency property, the object that defines the property must be a [**DependencyObject**](/uwp/api/Windows.UI.Xaml.DependencyObject) (in other words a class that has the **DependencyObject** base class somewhere in its inheritance). Many of the types you use for your UI definitions for a UWP app with XAML will be a **DependencyObject** subclass, and will support dependency properties. However, any type that comes from a Windows Runtime namespace that doesn't have "XAML" in its name won't support dependency properties; properties of such types are ordinary properties that won't have the property system's dependency behavior.

The purpose of dependency properties is to provide a systemic way to compute the value of a property based on other inputs (other properties, events and states that occur within your app while it runs). These other inputs might include:

- External input such as user preference
- Just-in-time property determination mechanisms such as data binding, animations and storyboards
- Multiple-use templating patterns such as resources and styles
- Values known through parent-child relationships with other elements in the object tree

A dependency property represents or supports a specific feature of the programming model for defining a Windows Runtime app with XAML for UI and C#, Microsoft Visual Basic or Visual C++ component extensions (C++/CX) for code. These features include:

- Data binding
- Styles
- Storyboarded animations
- "PropertyChanged" behavior; a dependency property can be implemented to provide callbacks that can propagate changes to other dependency properties
- Using a default value that comes from property metadata
- General property system utility such as [**ClearValue**](/uwp/api/windows.ui.xaml.dependencyobject.clearvalue) and metadata lookup

## Dependency properties and Windows Runtime properties

Dependency properties extend basic Windows Runtime property functionality by providing a global, internal property store that backs all of the dependency properties in an app at run time. This is an alternative to the standard pattern of backing a property with a private field that's private in the property-definition class. You can think of this internal property store as being a set of property identifiers and values that exist for any particular object (so long as it's a [**DependencyObject**](/uwp/api/Windows.UI.Xaml.DependencyObject)). Rather than being identified by name, each property in the store is identified by a [**DependencyProperty**](/uwp/api/Windows.UI.Xaml.DependencyProperty) instance. However, the property system mostly hides this implementation detail: you can usually access dependency properties by using a simple name (the programmatic property name in the code language you're using, or an attribute name when you're writing XAML).

The base type that provides the underpinnings of the dependency property system is [**DependencyObject**](/uwp/api/Windows.UI.Xaml.DependencyObject). **DependencyObject** defines methods that can access the dependency property, and instances of a **DependencyObject** derived class internally support the property store concept we mentioned earlier.

Here is a summation of the terminology that we use in the documentation when discussing dependency properties:

| Term | Description |
|------|-------------|
| Dependency property | A property that exists on a [**DependencyProperty**](/uwp/api/Windows.UI.Xaml.DependencyProperty) identifier (see below). Usually this identifier is available as a static member of the defining **DependencyObject** derived class. |
| Dependency property identifier | A constant value to identify the property, it is typically public and read-only. |
| Property wrapper | The callable **get** and **set** implementations for a Windows Runtime property. Or, the language-specific projection of the original definition. A **get** property wrapper implementation calls [**GetValue**](/uwp/api/windows.ui.xaml.dependencyobject.getvalue), passing the relevant dependency property identifier. |

The property wrapper is not just convenience for callers, it also exposes the dependency property to any process, tool or projection that uses Windows Runtime definitions for properties.

The following example defines a custom dependency property as defined for C#, and shows the relationship of the dependency property identifier to the property wrapper.

```csharp
public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
  "Label",
  typeof(string),
  typeof(ImageWithLabelControl),
  new PropertyMetadata(null)
);


public string Label
{
    get { return (string)GetValue(LabelProperty); }
    set { SetValue(LabelProperty, value); }
}
```

> [!NOTE]
> The preceding example is not intended as the complete example for how to create a custom dependency property. It is intended to show dependency property concepts for anyone that prefers learning concepts through code. For a more complete explanation of this example, see [Custom dependency properties](custom-dependency-properties.md).

## Dependency property value precedence

When you get the value of a dependency property, you are obtaining a value that was determined for that property through any one of the inputs that participate in the Windows Runtime property system. Dependency property value precedence exists so that the Windows Runtime property system can calculate values in a predictable way, and it's important that you be familiar with the basic precedence order too. Otherwise, you might find yourself in a situation where you're trying to set a property at one level of precedence but something else (the system, third-party callers, some of your own code) is setting it at another level, and you'll get frustrated trying to figure out which property value is used and where that value came from.

For example, styles and templates are intended to be a shared starting point for establishing property values and thus appearances of a control. But on a particular control instance you might want to change its value versus the common templated value, such as giving that control a different background color or a different text string as content. The Windows Runtime property system considers local values at higher precedence than values provided by styles and templates. That enables the scenario of having app-specific values overwrite the templates so that the controls are useful for your own use of them in app UI.

### Dependency property precedence list

The following is the definitive order that the property system uses when assigning the run-time value for a dependency property. Highest precedence is listed first. You'll find more detailed explanations just past this list.

1. **Animated values:** Active animations, visual state animations, or animations with a [**HoldEnd**](/uwp/api/Windows.UI.Xaml.Media.Animation.FillBehavior) behavior. To have any practical effect, an animation applied to a property must have precedence over the base (unanimated) value, even if that value was set locally.
1. **Local value:** A local value might be set through the convenience of the property wrapper, which also equates to setting as an attribute or property element in XAML, or by a call to the [**SetValue**](/uwp/api/windows.ui.xaml.dependencyobject.setvalue) method using a property of a specific instance. If you set a local value by using a binding or a static resource, these each act in the precedence as if a local value was set, and bindings or resource references are erased if a new local value is set.
1. **Templated properties:** An element has these if it was created as part of a template (from a [**ControlTemplate**](/uwp/api/Windows.UI.Xaml.Controls.ControlTemplate) or [**DataTemplate**](/uwp/api/Windows.UI.Xaml.DataTemplate)).
1. **Style setters:** Values from a [**Setter**](/uwp/api/Windows.UI.Xaml.Setter) within styles from page or application resources.
1. **Default value:** A dependency property can have a default value as part of its metadata.

### Templated properties

Templated properties as a precedence item do not apply to any property of an element that you declare directly in XAML page markup. The templated property concept exists only for objects that are created when the Windows Runtime applies a XAML template to a UI element and thus defines its visuals.

All the properties that are set from a control template have values of some kind. These values are almost like an extended set of default values for the control and are often associated with values you can reset later by setting the property values directly. Thus the template-set values must be distinguishable from a true local value, so that any new local value can overwrite it.

> [!NOTE]
> In some cases the template might override even local values, if the template failed to expose [{TemplateBinding} markup extension](templatebinding-markup-extension.md) references for properties that should have been settable on instances. This is usually done only if the property is really not intended to be set on instances, for example if it's only relevant to visuals and template behavior and not to the intended function or runtime logic of the control that uses the template.

### Bindings and precedence

Binding operations have the appropriate precedence for whatever scope they're used for. For example, a [{Binding}](binding-markup-extension.md) applied to a local value acts as local value, and a [{TemplateBinding} markup extension](templatebinding-markup-extension.md) for a property setter applies as a style setter does. Because bindings must wait until run-time to obtain values from data sources, the process of determining the property value precedence for any property extends into run-time as well.

Not only do bindings operate at the same precedence as a local value, they really are a local value, where the binding is the placeholder for a value that is deferred. If you have a binding in place for a property value, and you set a local value on it at run-time, that replaces the binding entirely. Similarly, if you call [**SetBinding**](/uwp/api/windows.ui.xaml.frameworkelement.setbinding) to define a binding that only comes into existence at run-time, you replace any local value you might have applied in XAML or with previously executed code.

### Storyboarded animations and base value

Storyboarded animations act on a concept of a *base value*. The base value is the value that's determined by the property system using its precedence, but omitting that last step of looking for animations. For example, a base value might come from a control's template, or it might come from setting a local value on an instance of a control. Either way, applying an animation will overwrite this base value and apply the animated value for as long as your animation continues to run.

For an animated property, the base value can still have an effect on the animation's behavior, if that animation does not explicitly specify both **From** and **To**, or if the animation reverts the property to its base value when completed. In these cases, once an animation is no longer running, the rest of the precedence is used again.

However, an animation that specifies a **To** with a [**HoldEnd**](/uwp/api/Windows.UI.Xaml.Media.Animation.FillBehavior) behavior can override a local value until the animation is removed, even when it visually appears to be stopped. Conceptually this is like an animation that's running forever even if there is not a visual animation in the UI.

Multiple animations can be applied to a single property. Each of these animations might have been defined to replace base values that came from different points in the value precedence. However, these animations will all be running simultaneously at run time, and that often means that they must combine their values because each animation has equal influence on the value. This depends on exactly how the animations are defined, and the type of the value that is being animated.

For more info, see [Storyboarded animations](../design/motion/storyboarded-animations.md).

### Default values

Establishing the default value for a dependency property with a [**PropertyMetadata**](/uwp/api/Windows.UI.Xaml.PropertyMetadata) value is explained in more detail in the [Custom dependency properties](custom-dependency-properties.md) topic.

Dependency properties still have default values even if those default values weren't explicitly defined in that property's metadata. Unless they have been changed by metadata, default values for the Windows Runtime dependency properties are generally one of the following:

- A property that uses a run-time object or the basic **Object** type (a *reference type*) has a default value of **null**. For example, [**DataContext**](/uwp/api/windows.ui.xaml.frameworkelement.datacontext) is **null** until it's deliberately set or is inherited.
- A property that uses a basic value such as numbers or a Boolean value (a *value type*) uses an expected default for that value. For example, 0 for integers and floating-point numbers, **false** for a Boolean.
- A property that uses a Windows Runtime structure has a default value that's obtained by calling that structure's implicit default constructor. This constructor uses the defaults for each of the basic value fields of the structure. For example, a default for a [**Point**](/uwp/api/Windows.Foundation.Point) value is initialized with its **X** and **Y** values as 0.
- A property that uses an enumeration has a default value of the first defined member in that enumeration. Check the reference for specific enumerations to see what the default value is.
- A property that uses a string ([**System.String**](/dotnet/api/system.string) for .NET, [**Platform::String**](/cpp/cppcx/platform-string-class) for C++/CX) has a default value of an empty string (**""**).
- Collection properties aren't typically implemented as dependency properties, for reasons discussed further on in this topic. But if you implement a custom collection property and you want it to be a dependency property, make sure to avoid an *unintentional singleton* as described near the end of [Custom dependency properties](custom-dependency-properties.md).

## Property functionality provided by a dependency property

### Data binding

A dependency property can have its value set through applying a data binding. Data binding uses the [{Binding} markup extension](binding-markup-extension.md) syntax in XAML, [{x:Bind} markup extension](x-bind-markup-extension.md) or the [**Binding**](/uwp/api/Windows.UI.Xaml.Data.Binding) class in code. For a databound property, the final property value determination is deferred until run time. At that time the value is obtained from a data source. The role that the dependency property system plays here is enabling a placeholder behavior for operations like loading XAML when the value is not yet known, and then supplying the value at run time by interacting with the Windows Runtime data binding engine.

The following example sets the [**Text**](/uwp/api/windows.ui.xaml.controls.textblock.text) value for a [**TextBlock**](/uwp/api/Windows.UI.Xaml.Controls.TextBlock) element, using a binding in XAML. The binding uses an inherited data context and an object data source. (Neither of these is shown in the shortened example; for a more complete sample that shows context and source, see [Data binding in depth](../data-binding/data-binding-in-depth.md).)

```xaml
<Canvas>
  <TextBlock Text="{Binding Team.TeamName}"/>
</Canvas>
```

You can also establish bindings using code rather than XAML. See [**SetBinding**](/uwp/api/windows.ui.xaml.frameworkelement.setbinding).

> [!NOTE]
> Bindings like this are treated as a local value for purposes of dependency property value precedence. If you set another local value for a property that originally held a [**Binding**](/uwp/api/Windows.UI.Xaml.Data.Binding) value, you will overwrite the binding entirely, not just the binding's run-time value. {x:Bind} Bindings are implemented using generated code that will set a local value for the property. If you set a local value for a property that is using {x:Bind}, then that value will be replaced the next time the binding is evaluated, such as when it observes a property change on its source object.

### Binding sources, binding targets, the role of FrameworkElement

To be the source of a binding, a property does not need to be a dependency property; you can generally use any property as a binding source, although this depends on your programming language and each has certain edge cases. However, to be the target of a [{Binding} markup extension](binding-markup-extension.md) or [**Binding**](/uwp/api/Windows.UI.Xaml.Data.Binding), that property must be a dependency property. {x:Bind} does not have this requirement as it uses generated code to apply its binding values.

If you are creating a binding in code, note that the [**SetBinding**](/uwp/api/windows.ui.xaml.frameworkelement.setbinding) API is defined only for [**FrameworkElement**](/uwp/api/Windows.UI.Xaml.FrameworkElement). However, you can create a binding definition using [**BindingOperations**](/uwp/api/Windows.UI.Xaml.Data.BindingOperations) instead, and thus reference any [**DependencyObject**](/uwp/api/Windows.UI.Xaml.DependencyObject) property.

For either code or XAML, remember that [**DataContext**](/uwp/api/windows.ui.xaml.frameworkelement.datacontext) is a [**FrameworkElement**](/uwp/api/Windows.UI.Xaml.FrameworkElement) property. By using a form of parent-child property inheritance (typically established in XAML markup), the binding system can resolve a **DataContext** that exists on a parent element. This inheritance can evaluate even if the child object (which has the target property) is not a **FrameworkElement** and therefore does not hold its own **DataContext** value. However, the parent element being inherited must be a **FrameworkElement** in order to set and hold the **DataContext**. Alternatively, you must define the binding such that it can function with a **null** value for **DataContext**.

Wiring the binding is not the only thing that's needed for most data binding scenarios. For a one-way or two-way binding to be effective, the source property must support change notifications that propagate to the binding system and thus the target. For custom binding sources, this means that the property must be a dependency property, or the object must support [**INotifyPropertyChanged**](/dotnet/api/system.componentmodel.inotifypropertychanged). Collections should support [**INotifyCollectionChanged**](/dotnet/api/system.collections.specialized.inotifycollectionchanged). Certain classes support these interfaces in their implementations so that they are useful as base classes for data binding scenarios; an example of such a class is [**ObservableCollection&lt;T&gt;**](/dotnet/api/system.collections.objectmodel.observablecollection-1). For more information on data binding and how data binding relates to the property system, see [Data binding in depth](../data-binding/data-binding-in-depth.md).

> [!NOTE]
> The types listed here support Microsoft .NET data sources. C++/CX data sources use different interfaces for change notification or observable behavior, see [Data binding in depth](../data-binding/data-binding-in-depth.md).

### Styles and templates

Styles and templates are two of the scenarios for properties being defined as dependency properties. Styles are useful for setting properties that define the app's UI. Styles are defined as resources in XAML, either as an entry in a [**Resources**](/uwp/api/windows.ui.xaml.frameworkelement.resources) collection, or in separate XAML files such as theme resource dictionaries. Styles interact with the property system because they contain setters for properties. The most important property here is the [**Control.Template**](/uwp/api/windows.ui.xaml.controls.control.template) property of a [**Control**](/uwp/api/Windows.UI.Xaml.Controls.Control): it defines most of the visual appearance and visual state for a **Control**. For more info on styles, and some example XAML that defines a [**Style**](/uwp/api/Windows.UI.Xaml.Style) and uses setters, see [Styling controls](../design/controls-and-patterns/xaml-styles.md).

Values that come from styles or templates are deferred values, similar to bindings. This is so that control users can re-template controls or redefine styles. And that's why property setters in styles can only act on dependency properties, not ordinary properties.

### Storyboarded animations

You can animate a dependency property's value using a storyboarded animation. Storyboarded animations in the Windows Runtime are not merely visual decorations. It's more useful to think of animations as being a state machine technique that can set the values of individual properties or of all properties and visuals of a control, and change these values over time.

To be animated, the animation's target property must be a dependency property. Also, to be animated, the target property's value type must be supported by one of the existing [**Timeline**](/uwp/api/Windows.UI.Xaml.Media.Animation.Timeline)-derived animation types. Values of [**Color**](/uwp/api/Windows.UI.Color), [**Double**](/dotnet/api/system.double) and [**Point**](/uwp/api/Windows.Foundation.Point) can be animated using either interpolation or keyframe techniques. Most other values can be animated using discrete **Object** key frames.

When an animation is applied and running, the animated value operates at a higher precedence than any value (such as a local value) that the property otherwise has. Animations also have an optional [**HoldEnd**](/uwp/api/Windows.UI.Xaml.Media.Animation.FillBehavior) behavior that can cause animations to apply to property values even if the animation visually appears to be stopped.

The state machine principle is embodied by the use of storyboarded animations as part of the [**VisualStateManager**](/uwp/api/Windows.UI.Xaml.VisualStateManager) state model for controls. For more info on storyboarded animations, see [Storyboarded animations](../design/motion/storyboarded-animations.md). For more info on **VisualStateManager** and defining visual states for controls, see [Storyboarded animations for visual states](/previous-versions/windows/apps/jj819808(v=win.10)) or [Control templates](../design/controls-and-patterns/control-templates.md).

### Property-changed behavior

Property-changed behavior is the origin of the "dependency" part of dependency property terminology. Maintaining valid values for a property when another property can influence the first property's value is a difficult development problem in many frameworks. In the Windows Runtime property system, each dependency property can specify a callback that is invoked whenever its property value changes. This callback can be used to notify or change related property values, in a generally synchronous manner. Many existing dependency properties have a property-changed behavior. You can also add similar callback behavior to custom dependency properties, and implement your own property-changed callbacks. See [Custom dependency properties](custom-dependency-properties.md) for an example.

Windows 10 introduces the [**RegisterPropertyChangedCallback**](/uwp/api/windows.ui.xaml.dependencyobject.registerpropertychangedcallback) method. This enables application code to register for change notifications when the specified dependency property is changed on an instance of [**DependencyObject**](/uwp/api/windows.ui.xaml.dependencyobject).

### Default value and **ClearValue**

A dependency property can have a default value defined as part of its property metadata. For a dependency property, its default value doesn't become irrelevant after the property's been set the first time. The default value might apply again at run-time whenever some other determinant in value precedence disappears. (Dependency property value precedence is discussed in the next section.) For example, you might deliberately remove a style value or an animation that applies to a property, but you want the value to be a reasonable default after you do so. The dependency property default value can provide this value, without needing to specifically set each property's value as an extra step.

You can deliberately set a property to the default value even after you have already set it with a local value. To reset a value to be the default again, and also to enable other participants in precedence that might override the default but not a local value, call the [**ClearValue**](/uwp/api/windows.ui.xaml.dependencyobject.clearvalue) method (reference the property to clear as a method parameter). You don't always want the property to literally use the default value, but clearing the local value and reverting to the default value might enable another item in precedence that you want to act now, such as using the value that came from a style setter in a control template.

## **DependencyObject** and threading

All [**DependencyObject**](/uwp/api/Windows.UI.Xaml.DependencyObject) instances must be created on the UI thread which is associated with the current [**Window**](/uwp/api/Windows.UI.Xaml.Window) that is shown by a Windows Runtime app. Although each **DependencyObject** must be created on the main UI thread, the objects can be accessed using a dispatcher reference from other threads, by accessing the [**Dispatcher**](/uwp/api/windows.ui.xaml.dependencyobject.dispatcher) property. Then you can call methods such as [**RunAsync**](/uwp/api/windows.ui.core.coredispatcher.runasync) on the [**CoreDispatcher**](/uwp/api/Windows.UI.Core.CoreDispatcher) object, and execute your code within the rules of thread restrictions on the UI thread.

The threading aspects of [**DependencyObject**](/uwp/api/Windows.UI.Xaml.DependencyObject) are relevant because it generally means that only code that runs on the UI thread can change or even read the value of a dependency property. Threading issues can usually be avoided in typical UI code that makes correct use of **async** patterns and background worker threads. You typically only run into **DependencyObject**-related threading issues if you are defining your own **DependencyObject** types and you attempt to use them for data sources or other scenarios where a **DependencyObject** isn't necessarily appropriate.

## Related topics

### Conceptual material

- [Custom dependency properties](custom-dependency-properties.md)
- [Attached properties overview](attached-properties-overview.md)
- [Data binding in depth](../data-binding/data-binding-in-depth.md)
- [Storyboarded animations](../design/motion/storyboarded-animations.md)
- [Creating Windows Runtime components](/previous-versions/windows/apps/hh441572(v=vs.140))
- [XAML user and custom controls sample](https://code.msdn.microsoft.com/windowsapps/XAML-user-and-custom-a8a9505e)

## APIs related to dependency properties

- [**DependencyObject**](/uwp/api/Windows.UI.Xaml.DependencyObject)
- [**DependencyProperty**](/uwp/api/Windows.UI.Xaml.DependencyProperty)