---
description: We explain XAML syntax rules and the terminology that describes the restrictions or choices available for XAML syntax.
title: XAML syntax guide
ms.assetid: A57FE7B4-9947-4AA0-BC99-5FE4686B611D
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# XAML syntax guide


We explain XAML syntax rules and the terminology that describes the restrictions or choices available for XAML syntax. You'll find this topic useful if you are new to using the XAML language, you want a refresher on the terminology or parts of syntax, or you are curious about how the XAML language works and want more background and context.

## XAML is XML

Extensible Application Markup Language (XAML) has a basic syntax that builds on XML, and by definition valid XAML must be valid XML. But XAML also has its own syntax concepts that extend XML. A given XML entity might be valid in plain XML, but that syntax might have a different and more complete meaning as XAML. This topic explains these XAML syntax concepts.

## XAML vocabularies

One area where XAML differs from most XML usages is that XAML is not typically enforced with a schema, such as an XSD file. That's because XAML is intended to be extensible, that's what the "X" in the acronym XAML means. Once XAML is parsed, the elements and attributes you reference in XAML are expected to exist in some backing code representation, either in the core types defined by the Windows Runtime, or in types that extend or are based off the Windows Runtime. The SDK documentation sometimes refers to the types that are already built-in to the Windows Runtime and can be used in XAML as being the *XAML vocabulary* for the Windows Runtime. Microsoft Visual Studio helps you to produce markup that's valid within this XAML vocabulary. Visual Studio can also include your custom types for XAML usage so long as the source of those types is referenced correctly in the project. For more info about XAML and custom types, see [XAML namespaces and namespace mapping](xaml-namespaces-and-namespace-mapping.md).

##  Declaring objects

Programmers often think in terms of objects and members, whereas a markup language is conceptualized as elements and attributes. In the most basic sense, an element that you declare in XAML markup becomes an object in a backing runtime object representation. To create a run-time object for your app, you declare a XAML element in the XAML markup. The object is created when the Windows Runtime loads your XAML.

A XAML file always has exactly one element serving as its root, which declares an object that will be the conceptual root of some programming structure such as a page, or the object graph of the entire run-time definition of an application.

In terms of XAML syntax, there are three ways to declare objects in XAML:

-   **Directly, using object element syntax:** This uses opening and closing tags to instantiate an object as an XML-form element. You can use this syntax to declare root objects or to create nested objects that set property values.
-   **Indirectly, using attribute syntax:** This uses an inline string value that has instructions for how to create an object. The XAML parser uses this string to set the value of a property to a newly created reference value. Support for it is limited to certain common objects and properties.
-   Using a markup extension.

This does not mean that you always have the choice of any syntax for object creation in a XAML vocabulary. Some objects can be created only by using object element syntax. Some objects can be created only by being initially set in an attribute. In fact, objects that can be created with either object element or attribute syntax are comparatively rare in XAML vocabularies. Even if both syntax forms are possible, one of the syntaxes will be more common as a matter of style.
There are also techniques you can use in XAML to reference existing objects rather than creating new values. The existing objects might be defined either in other areas of XAML, or might exist implicitly through some behavior of the platform and its application or programming models.

### Declaring an object by using object element syntax

To declare an object with object element syntax, you write tags like this: `<objectName>  </objectName>`, where *objectName* is the type name for the object you want to instantiate. Here's object element usage to declare a [**Canvas**](/uwp/api/Windows.UI.Xaml.Controls.Canvas) object:

```xml
<Canvas>
</Canvas>
```

If the object does not contain other objects, you can declare the object element by using one self-closing tag instead of an opening/closing pair: `<Canvas />`

### Containers

Many objects used as UI elements, such as [**Canvas**](/uwp/api/Windows.UI.Xaml.Controls.Canvas), can contain other objects. These are sometimes referred to as containers. The following example shows a **Canvas** container that contains one element, a [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle).

```xml
<Canvas>
  <Rectangle />
</Canvas>
```

### Declaring an object by using attribute syntax

Because this behavior is tied to property setting, we'll talk about this more in upcoming sections.

### Initialization text

For some objects you can declare new values using inner text that's used as initialization values for construction. In XAML, this technique and syntax is called *initialization text*. Conceptually, initialization text is similar to calling a constructor that has parameters. Initialization text is useful for setting initial values of certain structures.

You often use an object element syntax with initialization text if you want a structure value with an **x:Key**, so it can exist in a [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary). You might do this if you share that structure value among multiple target properties. For some structures, you can't use attribute syntax to set the structure's values: initialization text is the only way to produce a useful and shareable [**CornerRadius**](/uwp/api/Windows.UI.Xaml.CornerRadius), [**Thickness**](/uwp/api/Windows.UI.Xaml.Thickness), [**GridLength**](/uwp/api/Windows.UI.Xaml.GridLength) or [**Color**](/uwp/api/Windows.UI.Color) resource.

This abbreviated example uses initialization text to specify values for a [**Thickness**](/uwp/api/Windows.UI.Xaml.Thickness), in this case specifying values that set both **Left** and **Right** to 20, and both **Top** and **Bottom** to 10. This example shows the **Thickness** created as a keyed resource, and then the reference to that resource. For more info on [**Thickness**](/uwp/api/Windows.UI.Xaml.Thickness) initialization text, see [**Thickness**](/uwp/api/Windows.UI.Xaml.Thickness).

```xml
<UserControl ...>
  <UserControl.Resources>
    <Thickness x:Key="TwentyTenThickness">20,10</Thickness>
    ....
  </UserControl.Resources>
  ...
  <Grid Margin="{StaticResource TwentyTenThickness}">
  ...
  </Grid>
</UserControl ...>
```

**Note**  Some structures can't be declared as object elements. Initialization text isn't supported and they can't be used as resources. You must use an attribute syntax in order to set properties to these values in XAML. These types are: [**Duration**](/uwp/api/Windows.UI.Xaml.Duration), [**RepeatBehavior**](/uwp/api/Windows.UI.Xaml.Media.Animation.RepeatBehavior), [**Point**](/uwp/api/Windows.Foundation.Point), [**Rect**](/uwp/api/Windows.Foundation.Rect) and [**Size**](/uwp/api/Windows.Foundation.Size).

## Setting properties

You can set properties on objects that you declared by using object element syntax. There are multiple ways to set properties in XAML:

-   By using attribute syntax.
-   By using property element syntax.
-   By using element syntax where the content (inner text or child elements) is setting the XAML content property of an object.
-   By using a collection syntax (which is usually the implicit collection syntax).

As with object declaration, this list doesn't imply that any property could be set with each of the techniques. Some properties support only one of the techniques.
Some properties support more than one form; for example, there are properties that can use property element syntax, or attribute syntax. What's possible depends both on the property and on the object type that the property uses. In the Windows Runtime API reference, you'll see the XAML usages you can use in the **Syntax** section. Sometimes there is an alternative usage that would work but would be more verbose. Those verbose usages aren't always shown because we are trying to show you the best practices or the real world scenarios for using that property in XAML. Guidance for XAML syntax is provided in the **XAML Usage** sections of reference pages for properties that can be set in XAML.

Some properties on objects cannot be set in XAML by any means, and can only be set using code. Usually these are properties that are more appropriate to work with in the code-behind, not in XAML.

A read-only property cannot be set in XAML. Even in code, the owning type would have to support some other way to set it, like a constructor overload, helper method, or calculated property support. A calculated property relies on the values of other settable properties plus sometimes an event with built-in handling; these features are available in the dependency property system. For more info on how dependency properties are useful for calculated property support, see [Dependency properties overview](dependency-properties-overview.md).

Collection syntax in XAML gives an appearance that you are setting a read-only property, but in fact you are not. See "[Collection Syntax](#collection-syntax)" later in this topic.

### Setting a property by using attribute syntax

Setting an attribute value is the typical means by which you set a property value in a markup language, for example in XML or HTML. Setting XAML attributes is similar to how you set attribute values in XML. The attribute name is specified at any point within the tags following the element name, separated from element name by at least one whitespace. The attribute name is followed by an equals sign. The attribute value is contained within a pair of quotes. The quotes can be either double quotes or single quotes so long as they match and enclose the value. The attribute value itself must be expressible as a string. The string often contains numerals, but to XAML, all attribute values are string values until the XAML parser gets involved and does some basic value conversion.

This example uses attribute syntax for four attributes to set the [**Name**](/uwp/api/windows.ui.xaml.frameworkelement.name), [**Width**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Width), [**Height**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Height), and [**Fill**](/uwp/api/Windows.UI.Xaml.Shapes.Shape.Fill) properties of a [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle) object.

```xml
<Rectangle Name="rectangle1" Width="100" Height="100" Fill="Blue" />
```

### Setting a property by using property element syntax

Many properties of an object can be set by using property element syntax. A property element looks like this: `<`*object*`.`*property*`>`.

To use property element syntax, you create XAML property elements for the property that you want to set. In standard XML, this element would just be considered an element that has a dot in its name. However, in XAML, the dot in the element name identifies the element as a property element, with *property* expected to be a member of *object* in a backing object model implementation. To use property element syntax, it must be possible to specify an object element in order to "fill" the property element tags. A property element will always have some content (single element, multiple elements, or inner text); there's no point in having a self-closing property element.

In the following grammar, *property* is the name of the property that you want to set and *propertyValueAsObjectElement* is a single object element, that's expected to satisfy the value type requirements of the property.

`<`*object*`>`

`<`*object*`.`*property*`>`

*propertyValueAsObjectElement*

`</`*object*`.`*property*`>`

`</`*object*`>`

The following example uses property element syntax to set the [**Fill**](/uwp/api/Windows.UI.Xaml.Shapes.Shape.Fill) of a [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle) with a [**SolidColorBrush**](/uwp/api/Windows.UI.Xaml.Media.SolidColorBrush) object element. (Within the **SolidColorBrush**, [**Color**](/uwp/api/Windows.UI.Xaml.Media.SolidColorBrush.Color) is set as an attribute.) The parsed result of this XAML is identical to the previous XAML example that set **Fill** using attribute syntax.

```xml
<Rectangle
  Name="rectangle1"
  Width="100" 
  Height="100"
> 
  <Rectangle.Fill> 
    <SolidColorBrush Color="Blue"/> 
  </Rectangle.Fill>
</Rectangle>
```

### XAML vocabularies and object-oriented programming

Properties and events as they appear as XAML members of a Windows Runtime XAML type are often inherited from base types. Consider this example: `<Button Background="Blue" .../>`. The [**Background**](/uwp/api/windows.ui.xaml.controls.control.background) property is not an immediately declared property on the [**Button**](/uwp/api/Windows.UI.Xaml.Controls.Button) class. Instead, **Background** is inherited from the base [**Control**](/uwp/api/Windows.UI.Xaml.Controls.Control) class. In fact, if you look at the reference topic for **Button** you'll see that the members lists contain at least one inherited member from each of a chain of successive base classes: [**ButtonBase**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.ButtonBase), [**Control**](/uwp/api/Windows.UI.Xaml.Controls.Control), [**FrameworkElement**](/uwp/api/Windows.UI.Xaml.FrameworkElement), [**UIElement**](/uwp/api/Windows.UI.Xaml.UIElement), [**DependencyObject**](/uwp/api/Windows.UI.Xaml.DependencyObject). In the **Properties** list, all the read-write properties and collection properties are inherited in a XAML vocabulary sense. Events (like the various [**UIElement**](/uwp/api/Windows.UI.Xaml.UIElement) events) are inherited too.

If you use the Windows Runtime reference for XAML guidance, the element name that's shown in a syntax or even in example code is sometimes for the type that originally defines the property, because that reference topic is shared by all the possible types that inherit it from a base class. If you use Visual Studio's IntelliSense for XAML in the XML editor, the IntelliSense and its drop-downs do a great job of coalescing the inheritance and providing an accurate list of attributes that are available for setting once you've started with an object element for a class instance.

### XAML content properties

Some types define one of their properties such that the property enables a XAML content syntax. For the XAML content property of a type, you can omit the property element for that property when specifying it in XAML. Or, you can set the property to an inner text value by providing that inner text directly within the owning type's object element tags. XAML content properties support straightforward markup syntax for that property and makes the XAML more human-readable by reducing the nesting.

If a XAML content syntax is available, that syntax will be shown in the "XAML" sections of **Syntax** for that property in the Windows Runtime reference documentation. For example, the [**Child**](/uwp/api/windows.ui.xaml.controls.border.child) property page for [**Border**](/uwp/api/Windows.UI.Xaml.Controls.Border) shows XAML content syntax instead of property element syntax to set the single-object **Border.Child** value of a **Border**, like this:

```xml
<Border>
  <Button .../>
</Border>
```

If the property that is declared as the XAML content property is the **Object** type, or is type **String**, then the XAML content syntax supports what's basically inner text in the XML document model: a string between the opening and closing object tags. For example, the [**Text**](/uwp/api/windows.ui.xaml.controls.textblock.text) property page for [**TextBlock**](/uwp/api/Windows.UI.Xaml.Controls.TextBlock) shows XAML content syntax that has an inner text value to set **Text**, but the string "Text" never appears in the markup. Here's an example usage:

```xml
<TextBlock>Hello!</TextBlock>
```

If a XAML content property exists for a class, that's indicated in the reference topic for the class, in the "Attributes" section. Look for the value of the [**ContentPropertyAttribute**](/uwp/api/Windows.UI.Xaml.Markup.ContentPropertyAttribute). This attribute uses a named field "Name". The value of "Name" is the name of the property of that class that is the XAML content property. For example, on the [**Border**](/uwp/api/Windows.UI.Xaml.Controls.Border) reference page, you'll see this: ContentProperty("Name=Child").

One important XAML syntax rule we should mention is that you can't intermix the XAML content property and other property elements you set on the element. The XAML content property must be set entirely before any property elements, or entirely after. For example this is invalid XAML:

``` syntax
<StackPanel>
  <Button>This example</Button>
  <StackPanel.Resources>
    <SolidColorBrush x:Key="BlueBrush" Color="Blue"/>
  </StackPanel.Resources>
  <Button>... is illegal XAML</Button>
</StackPanel>
```

## Collection syntax

All of the syntaxes shown thus far are setting properties to single objects. But many UI scenarios require that a given parent element can have multiple child elements. For example, a UI for an input form needs several text box elements, some labels, and perhaps a "Submit" button. Still, if you were to use a programming object model to access these multiple elements, they would typically be items in a single collection property, rather than each item being the value of different properties. XAML supports multiple child elements as well as supporting a typical backing collection model by treating properties that use a collection type as implicit, and performing special handling for any child elements of a collection type.

Many collection properties are also identified as the XAML content property for the class. The combination of implicit collection processing and XAML content syntax is frequently seen in types used for control compositing, such as panels, views, or items controls. For example, the following example shows the simplest possible XAML for compositing two peer UI elements within a [**StackPanel**](/uwp/api/Windows.UI.Xaml.Controls.StackPanel).

```xml
<StackPanel>
  <TextBlock>Hello</TextBlock>
  <TextBlock>World</TextBlock>
</StackPanel>
```

### The mechanism of XAML collection syntax

It might at first appear that XAML is enabling a "set" of the read-only collection property. In reality, what XAML enables here is adding items to an existing collection. The XAML language and XAML processors implementing XAML support rely on a convention in backing collection types to enable this syntax. Typically there is a backing property such as an indexer or **Items** property that refers to specific items of the collection. Generally, that property is not explicit in the XAML syntax. For collections, the underlying mechanism for XAML parsing is not a property, but a method: specifically, the **Add** method in most cases. When the XAML processor encounters one or more object elements within a XAML collection syntax, each such object is first created from an element, then each new object is added in order to the containing collection by calling the collection's **Add** method.

When a XAML parser adds items to a collection, it is the logic of the **Add** method that determines whether a given XAML element is a permissible item child of the collection object. Many collection types are strongly typed by the backing implementation, meaning that the input parameter of **Add** expects that whatever is passed must be a type match with the **Add** parameter type.

For collection properties, be careful about when you try to specify the collection explicitly as an object element. A XAML parser will create a new object whenever it encounters an object element. If the collection property you're trying to use is read-only, this can throw a XAML parse exception. Just use the implicit collection syntax, and you won't see that exception.

## When to use attribute or property element syntax

All properties that support being set in XAML will support attribute or property element syntax for direct value setting, but potentially will not support either syntax interchangeably. Some properties do support either syntax, and some properties support additional syntax options like a XAML content property. The type of XAML syntax supported by a property depends on the type of object that the property uses as its property type. If the property type is a primitive type, such as a double (float or decimal), integer, Boolean, or string, the property always supports attribute syntax.

You can also use attribute syntax to set a property if the object type you use to set that property can be created by processing a string. For primitives, this is always the case, the type conversion is built in to the parser. However, certain other object types can also be created by using a string specified as an attribute value, rather than an object element within a property element. For this to work, there has to be an underlying type conversion, supported either by that particular property or supported generally for all values that use that property type. The string value of the attribute is used to set properties that are important for the initialization of the new object value. Potentially, a specific type converter can also create different subclasses of a common property type, depending on how it uniquely processes information in the string. Object types that support this behavior will have a special grammar listed in the syntax section of the reference documentation. As an example, the XAML syntax for [**Brush**](/uwp/api/Windows.UI.Xaml.Media.Brush) shows how an attribute syntax can be used to create a new [**SolidColorBrush**](/uwp/api/Windows.UI.Xaml.Media.SolidColorBrush) value for any property of type **Brush** (and there are many **Brush** properties in Windows Runtime XAML).

## XAML parsing logic and rules

Sometime's it's informative to read the XAML in a similar way to how a XAML parser must read it: as a set of string tokens encountered in a linear order. A XAML parser must interpret these tokens under a set of rules that are part of the definition of how XAML works.

Setting an attribute value is the typical means by which you set a property value in a markup language, for example in XML or HTML. In the following syntax, *objectName* is the object you want to instantiate, *propertyName* is the name of the property that you want to set on that object, and *propertyValue* is the value to set.

```xml
<objectName propertyName="propertyValue" .../>

-or-

<objectName propertyName="propertyValue">

...<!--element children -->

</objectName>
```

Either syntax enables you to declare an object and set a property on that object. Although the first example is a single element in markup, there are actually discrete steps here with regard to how a XAML processor parses this markup.

First, the presence of the object element indicates that a new *objectName* object must be instantiated. Only after such an instance exists can the instance property *propertyName* be set on it.

Another rule of XAML is that attributes of an element must be able to be set in any order. For example, there's no difference between `<Rectangle Height="50" Width="100" />` and `<Rectangle Width="100"  Height="50" />`. Which order you use is a matter of style.

**Note**  XAML designers often promote ordering conventions if you use design surfaces other than the XML editor, but you can freely edit that XAML later, to reorder the attributes or introduce new ones.

## Attached properties

XAML extends XML by adding a syntax element known as an *attached property*. Similar to the property element syntax, the attached property syntax contains a dot, and the dot holds special meaning to XAML parsing. Specifically, the dot separates the provider of the attached property, and the property name.

In XAML, you set attached properties by using the syntax *AttachedPropertyProvider*.*PropertyName* Here is an example of how you can set the attached property [**Canvas.Left**](/dotnet/api/system.windows.controls.canvas.left) in XAML:

```xml
<Canvas>
  <Button Canvas.Left="50">Hello</Button>
</Canvas>
```

You can set the attached property on elements that don't have a property of that name in the backing type, and in that way they function somewhat like a global property, or an attribute defined by a different XML namespace like the **xml:space** attribute.

In Windows Runtime XAML you'll see attached properties that support these scenarios:

-   Child elements can inform parent container panels how they should behave in layout: [**Canvas**](/uwp/api/Windows.UI.Xaml.Controls.Canvas), [**Grid**](/uwp/api/Windows.UI.Xaml.Controls.Grid), [**VariableSizedWrapGrid**](/uwp/api/Windows.UI.Xaml.Controls.VariableSizedWrapGrid).
-   Control usages can influence behavior of an important control part that comes from the control template: [**ScrollViewer**](/uwp/api/Windows.UI.Xaml.Controls.ScrollViewer), [**VirtualizingStackPanel**](/uwp/api/Windows.UI.Xaml.Controls.VirtualizingStackPanel).
-   Using a service that's available in a related class, where the service and the class that uses it don't share inheritance: [**Typography**](/uwp/api/Windows.UI.Xaml.Documents.Typography), [**VisualStateManager**](/uwp/api/Windows.UI.Xaml.VisualStateManager), [**AutomationProperties**](/uwp/api/Windows.UI.Xaml.Automation.AutomationProperties), [**ToolTipService**](/uwp/api/Windows.UI.Xaml.Controls.ToolTipService).
-   Animation targeting: [**Storyboard**](/uwp/api/Windows.UI.Xaml.Media.Animation.Storyboard).

For more info, see [Attached properties overview](attached-properties-overview.md).

## Literal "{" values

Because the opening brace symbol \{ is the opening of the markup extension sequence, you use an escape sequence to specify a literal string value that starts with "\{". The escape sequence is "\{\}". For example, to specify a string value that is a single opening brace, specify the attribute value as "\{\}\{". You can also use the alternative quotation marks (for example, a **'** within an attribute value delimited by **""**) to provide a "\{" value as a string.

**Note**  "\\}" also works if it's inside a quoted attribute.
 
## Enumeration values

Many properties in the Windows Runtime API use enumerations as values. If the member is a read-write property you can set such a property by providing an attribute value. You identify which enumeration value to use as the value of the property by using the unqualified name of the constant name . For example here's how to set [**UIElement.Visibility**](/uwp/api/windows.ui.xaml.uielement.visibility) in XAML: `<Button Visibility="Visible"/>`. Here the "Visible" as a string is directly mapped to a named constant of the [**Visibility**](/uwp/api/Windows.UI.Xaml.Visibility) enumeration, **Visible**.

-   Don't use a qualified form, it won't work. For example, this is invalid XAML: `<Button Visibility="Visibility.Visible"/>`.
-   Don't use the value of the constant. In other words, don't rely on the integer value of the enumeration that's there explicitly or implicitly depending on how the enumeration was defined. Although it might appear to work, it's a bad practice either in XAML or in code because you're relying on what could be a transient implementation detail. For example, don't do this: `<Button Visibility="1"/>`.

**Note**  In reference topics for APIs that use XAML and use enumerations, click the link to the enumeration type in the **Property value** section of **Syntax**. This links to the enumeration page where you can discover the named constants for that enumeration.

Enumerations can be flagwise, meaning that they are attributed with **FlagsAttribute**. If you need to specify a combination of values for a flagwise enumeration as a XAML attribute value, use the name of each enumeration constant, with a comma (,) between each name, and no intervening space characters. Flagwise attributes aren't common in the Windows Runtime XAML vocabulary, but [**ManipulationModes**](/uwp/api/Windows.UI.Xaml.Input.ManipulationModes) is an example where setting a flagwise enumeration value in XAML is supported.

## Interfaces in XAML

In rare cases you'll see a XAML syntax where the type of a property is an interface. In the XAML type system, a type that implements that interface is acceptable as a value when parsed. There must be a created instance of such a type available to serve as the value. You'll see an interface used as a type in the XAML syntax for [**Command**](/uwp/api/windows.ui.xaml.controls.primitives.buttonbase.command) and [**CommandParameter**](/uwp/api/windows.ui.xaml.controls.primitives.buttonbase.commandparameter) properties of [**ButtonBase**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.ButtonBase). These properties support Model-View-ViewModel (MVVM) design patterns where the **ICommand** interface is the contract for how the views and models interact.

## XAML placeholder conventions in Windows Runtime reference

If you've examined any of the **Syntax** section of reference topics for Windows Runtime APIs that can use XAML, you've probably seen that the syntax includes quite a few placeholders. XAML syntax is different than the C#, Microsoft Visual Basic or Visual C++ component extensions (C++/CX) syntax because the XAML syntax is a usage syntax. It's hinting at your eventual usage in your own XAML files, but without being over-prescriptive about the values you can use. So usually the usage describes a type of grammar that mixes literals and placeholders, and defines some of the placeholders in the **XAML Values** section.

When you see type names / element names in a XAML syntax for a property, the name that's shown is for the type that originally defines the property. But Windows Runtime XAML supports a class inheritance model for the [**DependencyObject**](/uwp/api/Windows.UI.Xaml.DependencyObject)-based classes. So you can often use an attribute on a class that's not literally the defining class, but instead derives from a class that first defined the property/attribute. For example, you can set [**Visibility**](/uwp/api/windows.ui.xaml.uielement.visibility) as an attribute on any [**UIElement**](/uwp/api/Windows.UI.Xaml.UIElement) derived class using a deep inheritance. For example: `<Button Visibility="Visible" />`. So don't take the element name shown in any XAML usage syntax too literally; the syntax may be viable for elements representing that class, and also elements that represent a derived class. In cases where it's rare or impossible for the type shown as the defining element to be in a real-world usage, that type name is deliberately lowercased in the syntax. For example, the syntax you see for **UIElement.Visibility** is :

``` syntax
<uiElement Visibility="Visible"/>
-or-
<uiElement Visibility="Collapsed"/>
```

Many XAML syntax sections include placeholders in the "Usage" that are then defined in a **XAML Values** section that's directly under the **Syntax** section.

XAML usage sections also use various generalized placeholders. These placeholders aren't redefined every time in **XAML Values**, because you'll guess or eventually learn what they represent. We think most readers would get tired of seeing them in **XAML Values** again and again so we left them out of the definitions. For reference, here's a list of some of these placeholders and what they mean in a general sense:

-   *object*: theoretically any object value, but often practically limited to certain types of objects such as a string-or-object choice, and you should check the Remarks on the reference page for more info.
-   *object* *property*: *object* *property* in combination is used for cases where the syntax being shown is the syntax for a type that can be used as an attribute value for many properties. For example, the **Xaml Attribute Usage** shown for [**Brush**](/uwp/api/Windows.UI.Xaml.Media.Brush) includes: <*object* *property*="*predefinedColorName*"/>
-   *eventhandler*: This appears as the attribute value for every XAML syntax shown for an event attribute. What you're supplying here is the function name for an event handler function. That function must be defined in the code-behind for the XAML page. At the programming level, that function must match the delegate signature of the event that you're handling, or your app code won't compile. But that's really a programming consideration, not a XAML consideration, so we don't try to hint anything about the delegate type in the XAML syntax. If you want to know which delegate you should be implementing for an event, that's in the **Event information** section of the reference topic for the event, in a table row that's labeled **Delegate**.
-   *enumMemberName*: shown in attribute syntax for all enumerations. There's a similar placeholder for properties that use an enumeration value, but it usually prefixes the placeholder with a hint of the enumeration's name. For example, the syntax shown for [**FrameworkElement.FlowDirection**](/uwp/api/windows.ui.xaml.frameworkelement.flowdirection) is <*frameworkElement***FlowDirection**="*flowDirectionMemberName*"/>. If you're on one of those property reference pages, click the link to the enumeration type that appears in the **Property Value** section, next to the text **Type:**. For the attribute value of a property that uses that enumeration, you can use any string that is listed in the **Member** column of the **Members** list.
-   *double*, *int*, *string*, *bool*: These are primitive types known to the XAML language. If you're programming using C# or Visual Basic, these types are projected to Microsoft .NET equivalent types such as [**Double**](/dotnet/api/system.double), [**Int32**](/dotnet/api/system.int32), [**String**](/dotnet/api/system.string) and [**Boolean**](/dotnet/api/system.boolean), and you can use any members on those .NET types when you work with your XAML-defined values in .NET code-behind. If you're programming using C++/CX, you'll use the C++ primitive types but you can also consider these equivalent to types defined by the [**Platform**](/cpp/cppcx/platform-namespace-c-cx) namespace, for example [**Platform::String**](/cpp/cppcx/platform-string-class). There will sometimes be additional value restrictions for particular properties. But you'll usually see these noted in a **Property value** section or Remarks section and not in a XAML section, because any such restrictions apply both to code usages and XAML usages.

## Tips and tricks, notes on style

-   Markup extensions in general are described in the main [XAML overview](xaml-overview.md). But the markup extension that most impacts the guidance given in this topic is the [StaticResource](staticresource-markup-extension.md) markup extension (and related [ThemeResource](themeresource-markup-extension.md)). The function of the StaticResource markup extension is to enable factoring your XAML into reusable resources that come from a XAML [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary). You almost always define control templates and related styles in a **ResourceDictionary**. You often define the smaller parts of a control template definition or app-specific style in a **ResourceDictionary** too, for example a [**SolidColorBrush**](/uwp/api/Windows.UI.Xaml.Media.SolidColorBrush) for a color that your app uses more than once for different parts of UI. By using a StaticResource, any property that would otherwise require a property element usage to set can now be set in attribute syntax. But the benefits of factoring XAML for reuse go beyond just simplifying the page-level syntax. For more info, see [ResourceDictionary and XAML resource references](../design/controls-and-patterns/resourcedictionary-and-xaml-resource-references.md).
-   You'll see several different conventions for how white space and line feeds are applied in XAML examples. In particular, there are different conventions for how to break up object elements that have a lot of different attributes set. That's just a matter of style. The Visual Studio XML editor applies some default style rules when you edit XAML, but you can change these in the settings. There are a small number of cases where the white space in a XAML file is considered significant; for more info see [XAML and whitespace](xaml-and-whitespace.md).

## Related topics

* [XAML overview](xaml-overview.md)
* [XAML namespaces and namespace mapping](xaml-namespaces-and-namespace-mapping.md)
* [ResourceDictionary and XAML resource references](../design/controls-and-patterns/resourcedictionary-and-xaml-resource-references.md)
 