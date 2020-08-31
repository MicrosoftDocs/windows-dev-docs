---
description: Explains the concept of an attached property in XAML, and provides some examples.
title: Attached properties overview
ms.assetid: 098C1DE0-D640-48B1-9961-D0ADF33266E2
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
dev_langs:
  - csharp
  - vb
  - cpp
---
# Attached properties overview

An *attached property* is a XAML concept. Attached properties enable additional property/value pairs to be set on an object, but the properties are not part of the original object definition. Attached properties are typically defined as a specialized form of dependency property that doesn't have a conventional property wrapper in the owner type's object model.

## Prerequisites

We assume that you understand the basic concept of dependency properties, and have read [Dependency properties overview](dependency-properties-overview.md).

## Attached properties in XAML

In XAML, you set attached properties by using the syntax _AttachedPropertyProvider.PropertyName_. Here is an example of how you can set [**Canvas.Left**](/dotnet/api/system.windows.controls.canvas.left) in XAML.

```xaml
<Canvas>
  <Button Canvas.Left="50">Hello</Button>
</Canvas>
```

> [!NOTE]
> We're just using [**Canvas.Left**](/dotnet/api/system.windows.controls.canvas.left) as an example attached property without fully explaining why you'd use it. If you want to know more about what **Canvas.Left** is for and how [**Canvas**](/uwp/api/Windows.UI.Xaml.Controls.Canvas) handles its layout children, see the [**Canvas**](/uwp/api/Windows.UI.Xaml.Controls.Canvas) reference topic or [Define layouts with XAML](../design/layout/layouts-with-xaml.md).

## Why use attached properties?

Attached properties are a way to escape the coding conventions that might prevent different objects in a relationship from communicating information to each other at run time. It's certainly possible to put properties on a common base class so that each object could just get and set that property. But eventually the sheer number of scenarios where you might want to do this will bloat your base classes with shareable properties. It might even introduce cases where there might just be two of hundreds of descendants trying to use a property. That's not good class design. To address this, the attached property concept enables an object to assign a value for a property that its own class structure doesn't define. The defining class can read the value from child objects at run time after the various objects are created in an object tree.

For example, child elements can use attached properties to inform their parent element of how they are to be presented in the UI. This is the case with the [**Canvas.Left**](/dotnet/api/system.windows.controls.canvas.left) attached property. **Canvas.Left** is created as an attached property because it is set on elements that are contained within a [**Canvas**](/uwp/api/Windows.UI.Xaml.Controls.Canvas) element, rather than on the **Canvas** itself. Any possible child element then uses **Canvas.Left** and [**Canvas.Top**](/dotnet/api/system.windows.controls.canvas.top) to specify its layout offset within the **Canvas** layout container parent. Attached properties make it possible for this to work without cluttering the base element's object model with lots of properties that each apply to only one of the many possible layout containers. Instead, many of the layout containers implement their own attached property set.

To implement the attached property, the [**Canvas**](/uwp/api/Windows.UI.Xaml.Controls.Canvas) class defines a static [**DependencyProperty**](/uwp/api/Windows.UI.Xaml.DependencyProperty) field named [**Canvas.LeftProperty**](/uwp/api/windows.ui.xaml.controls.canvas.leftproperty). Then, **Canvas** provides the [**SetLeft**](/uwp/api/windows.ui.xaml.controls.canvas.setleft) and [**GetLeft**](/uwp/api/windows.ui.xaml.controls.canvas.getleft) methods as public accessors for the attached property, to enable both XAML setting and run-time value access. For XAML and for the dependency property system, this set of APIs satisfies a pattern that enables a specific XAML syntax for attached properties, and stores the value in the dependency property store.

## How the owning type uses attached properties

Although attached properties can be set on any XAML element (or any underlying [**DependencyObject**](/uwp/api/Windows.UI.Xaml.DependencyObject)), that doesn't automatically mean that setting the property produces a tangible result, or that the value is ever accessed. The type that defines the attached property typically follows one of these scenarios:

- The type that defines the attached property is the parent in a relationship of other objects. The child objects will set values for the attached property. The attached property owner type has some innate behavior that iterates through its child elements, obtains the values, and acts on those values at some point in object lifetime (a layout action, [**SizeChanged**](/uwp/api/windows.ui.xaml.frameworkelement.sizechanged), etc.)
- The type that defines the attached property is used as the child element for a variety of possible parent elements and content models, but the info isn't necessarily layout info.
- The attached property reports info to a service, not to another UI element.

For more info on these scenarios and owning types, see the "More about Canvas.Left" section of [Custom attached properties](custom-attached-properties.md).

## Attached properties in code

Attached properties don't have the typical property wrappers for easy get and set access like other dependency properties do. This is because the attached property is not necessarily part of the code-centered object model for instances where the property is set. (It is permissible, though uncommon, to define a property that is both an attached property that other types can set on themselves, and that also has a conventional property usage on the owning type.)

There are two ways to set an attached property in code: use the property-system APIs, or use the XAML pattern accessors. These techniques are pretty much equivalent in terms of their end result, so which one to use is mostly a matter of coding style.

### Using the property system

Attached properties for the Windows Runtime are implemented as dependency properties, so that the values can be stored in the shared dependency-property store by the property system. Therefore attached properties expose a dependency property identifier on the owning class.

To set an attached property in code, you call the [**SetValue**](/uwp/api/windows.ui.xaml.dependencyobject.setvalue) method, and pass the [**DependencyProperty**](/uwp/api/Windows.UI.Xaml.DependencyProperty) field that serves as the identifier for that attached property. (You also pass the value to set.)

To get the value of an attached property in code, you call the [**GetValue**](/uwp/api/windows.ui.xaml.dependencyobject.getvalue) method, again passing the [**DependencyProperty**](/uwp/api/Windows.UI.Xaml.DependencyProperty) field that serves as the identifier.

### Using the XAML accessor pattern

A XAML processor must be able to set attached property values when XAML is parsed into an object tree. The owner type of the attached property must implement dedicated accessor methods named in the form **Get**_PropertyName_ and **Set**_PropertyName_. These dedicated accessor methods are also one way to get or set the attached property in code. From a code perspective, an attached property is similar to a backing field that has method accessors instead of property accessors, and that backing field can exist on any object rather than having to be specifically defined.

The next example shows how you can set an attached property in code via the XAML accessor API. In this example, `myCheckBox` is an instance of the [**CheckBox**](/uwp/api/Windows.UI.Xaml.Controls.CheckBox) class. The last line is the code that actually sets the value; the lines before that just establish the instances and their parent-child relationship. The uncommented last line is the syntax if you use the property system. The commented last line is the syntax if you use the XAML accessor pattern.

```csharp
    Canvas myC = new Canvas();
    CheckBox myCheckBox = new CheckBox();
    myCheckBox.Content = "Hello";
    myC.Children.Add(myCheckBox);
    myCheckBox.SetValue(Canvas.TopProperty,75);
    //Canvas.SetTop(myCheckBox, 75);
```

```vb
    Dim myC As Canvas = New Canvas()
    Dim myCheckBox As CheckBox= New CheckBox()
    myCheckBox.Content = "Hello"
    myC.Children.Add(myCheckBox)
    myCheckBox.SetValue(Canvas.TopProperty,75)
    ' Canvas.SetTop(myCheckBox, 75)
```

```cppwinrt
Canvas myC;
CheckBox myCheckBox;
myCheckBox.Content(winrt::box_value(L"Hello"));
myC.Children().Append(myCheckBox);
myCheckBox.SetValue(Canvas::TopProperty(), winrt::box_value(75));
// Canvas::SetTop(myCheckBox, 75);
```

```cpp
    Canvas^ myC = ref new Canvas();
    CheckBox^ myCheckBox = ref new CheckBox();
    myCheckBox->Content="Hello";
    myC->Children->Append(myCheckBox);
    myCheckBox->SetValue(Canvas::TopProperty,75);
    // Canvas::SetTop(myCheckBox, 75);
```

## Custom attached properties

For code examples of how to define custom attached properties, and more info about the scenarios for using an attached property, see [Custom attached properties](custom-attached-properties.md).

## Special syntax for attached property references

The dot in an attached property name is a key part of the identification pattern. Sometimes there are ambiguities when a syntax or situation treats the dot as having some other meaning. For example, a dot is treated as an object-model traversal for a binding path. In most cases involving such ambiguity, there is a special syntax for an attached property that enables the inner dot still to be parsed as the _owner_**.**_property_ separator of an attached property.

- To specify an attached property as part of a target path for an animation, enclose the attached property name in parentheses ("()")—for example, "(Canvas.Left)". For more info, see [Property-path syntax](property-path-syntax.md).

> [!WARNING]
> An existing limitation of the Windows Runtime XAML implementation is that you cannot animate a custom attached property.

- To specify an attached property as the target property for a resource reference from a resource file to **x:Uid**, use a special syntax that injects a code-style, fully qualified **using:** declaration inside square brackets ("\[\]"), to create a deliberate scope break. For example, assuming there exists an element `<TextBlock x:Uid="Title" />`, the resource key in the resource file that targets the **Canvas.Top** value on that instance is "Title.\[using:Windows.UI.Xaml.Controls\]Canvas.Top". For more info on resource files and XAML, see [Quickstart: Translating UI resources](/previous-versions/windows/apps/hh965329(v=win.10)).

## Related topics

- [Custom attached properties](custom-attached-properties.md)
- [Dependency properties overview](dependency-properties-overview.md)
- [Define layouts with XAML](../design/layout/layouts-with-xaml.md)
- [Quickstart: Translating UI resources](/previous-versions/windows/apps/hh943060(v=win.10))
- [**SetValue**](/uwp/api/windows.ui.xaml.dependencyobject.setvalue)
- [**GetValue**](/uwp/api/windows.ui.xaml.dependencyobject.getvalue)