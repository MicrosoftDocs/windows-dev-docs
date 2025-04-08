---
description: The xBind markup extension is a high performance alternative to Binding. xBind - new for Windows 10 - runs in less time and less memory than Binding and supports better debugging.
title: xBind markup extension
ms.assetid: 529FBEB5-E589-486F-A204-B310ACDC5C06
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# {x:Bind} markup extension

**Note**  For general info about using data binding in your app with **{x:Bind}** (and for an all-up comparison between **{x:Bind}** and **{Binding}**), see [Data binding in depth](../data-binding/data-binding-in-depth.md).

The **{x:Bind}** markup extension—new for Windows 10—is an alternative to **{Binding}**. **{x:Bind}** runs in less time and less memory than **{Binding}** and supports better debugging.

At XAML compile time, **{x:Bind}** is converted into code that will get a value from a property on a data source, and set it on the property specified in markup. The binding object can optionally be configured to observe changes in the value of the data source property and refresh itself based on those changes (`Mode="OneWay"`). It can also optionally be configured to push changes in its own value back to the source property (`Mode="TwoWay"`).

The binding objects created by **{x:Bind}** and **{Binding}** are largely functionally equivalent. But **{x:Bind}** executes special-purpose code, which it generates at compile-time, and **{Binding}** uses general-purpose runtime object inspection. Consequently, **{x:Bind}** bindings (often referred-to as compiled bindings) have great performance, provide compile-time validation of your binding expressions, and support debugging by enabling you to set breakpoints in the code files that are generated as the partial class for your page. These files can be found in your `obj` folder, with names like (for C#) `<view name>.g.cs`.

> [!TIP]
> **{x:Bind}** has a default mode of **OneTime**, unlike **{Binding}**, which has a default mode of **OneWay**. This was chosen for performance reasons, as using **OneWay** causes more code to be generated to hookup and handle change detection. You can explicitly specify a mode to use OneWay or TwoWay binding. You can also use [x:DefaultBindMode](x-defaultbindmode-attribute.md) to change the default mode for **{x:Bind}** for a specific segment of the markup tree. The specified mode applies to any **{x:Bind}** expressions on that element and its children, that do not explicitly specify a mode as part of the binding.

**Sample apps that demonstrate {x:Bind}**

-   [{x:Bind} sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlBind)
-   [QuizGame](https://github.com/microsoft/Windows-appsample-networkhelper)
-   [XAML Controls Gallery](https://github.com/Microsoft/Xaml-Controls-Gallery)

## XAML attribute usage

``` syntax
<object property="{x:Bind}" .../>
-or-
<object property="{x:Bind propertyPath}" .../>
-or-
<object property="{x:Bind bindingProperties}" .../>
-or-
<object property="{x:Bind propertyPath, bindingProperties}" .../>
-or-
<object property="{x:Bind pathToFunction.functionName(functionParameter1, functionParameter2, ...), bindingProperties}" .../>
```

| Term | Description |
|------|-------------|
| _propertyPath_ | A string that specifies the property path for the binding. More info is in the [Property path](#property-path) section below. |
| _bindingProperties_ |
| _propName_=_value_\[, _propName_=_value_\]* | One or more binding properties that are specified using a name/value pair syntax. |
| _propName_ | The string name of the property to set on the binding object. For example, "Converter". |
| _value_ | The value to set the property to. The syntax of the argument depends on the property being set. Here's an example of a _propName_=_value_ usage where the value is itself a markup extension: `Converter={StaticResource myConverterClass}`. For more info, see [Properties that you can set with {x:Bind}](#properties-that-you-can-set-with-xbind) section below. |

## Examples

```XAML
<Page x:Class="QuizGame.View.HostView" ... >
    <Button Content="{x:Bind Path=ViewModel.NextButtonText, Mode=OneWay}" ... />
</Page>
```

This example XAML uses **{x:Bind}** with a **ListView.ItemTemplate** property. Note the declaration of an **x:DataType** value.

```XAML
  <DataTemplate x:Key="SimpleItemTemplate" x:DataType="data:SampleDataGroup">
    <StackPanel Orientation="Vertical" Height="50">
      <TextBlock Text="{x:Bind Title}"/>
      <TextBlock Text="{x:Bind Description}"/>
    </StackPanel>
  </DataTemplate>
```

## Property path

*PropertyPath* sets the **Path** for an **{x:Bind}** expression. **Path** is a property path specifying the value of the property, sub-property, field, or method that you're binding to (the source). You can mention the name of the **Path** property explicitly: `{x:Bind Path=...}`. Or you can omit it: `{x:Bind ...}`.

### Property path resolution

**{x:Bind}** does not use the **DataContext** as a default source—instead, it uses the page or user control itself. So it will look in the code-behind of your page or user control for properties, fields, and methods. To expose your view model to **{x:Bind}**, you will typically want to add new fields or properties to the code behind for your page or user control. Steps in a property path are delimited by dots (.), and you can include multiple delimiters to traverse successive sub-properties. Use the dot delimiter regardless of the programming language used to implement the object being bound to.

For example: in a page, **Text="{x:Bind Employee.FirstName}"** will look for an **Employee** member on the page and then a **FirstName** member on the object returned by **Employee**. If you are binding an items control to a property that contains an employee's dependents, your property path might be "Employee.Dependents", and the item template of the items control would take care of displaying the items in "Dependents".

For C++/CX, **{x:Bind}** cannot bind to private fields and properties in the page or data model – you will need to have a public property for it to be bindable. The surface area for binding needs to be exposed as CX classes/interfaces so that we can get the relevant metadata. The **\[Bindable\]** attribute should not be needed.

With **x:Bind**, you do not need to use **ElementName=xxx** as part of the binding expression. Instead, you can use the name of the element as the first part of the path for the binding because named elements become fields within the page or user control that represents the root binding source.

### Collections

If the data source is a collection, then a property path can specify items in the collection by their position or index. For example, "Teams\[0\].Players", where the literal "\[\]" encloses the "0" that requests the first item in a zero-indexed collection.

To use an indexer, the model needs to implement **IList&lt;T&gt;** or **IVector&lt;T&gt;** on the type of the property that is going to be indexed. (Note that IReadOnlyList&lt;T&gt; and IVectorView&lt;T&gt; do not support the indexer syntax.) If the type of the indexed property supports **INotifyCollectionChanged** or **IObservableVector** and the binding is OneWay or TwoWay, then it will register and listen for change notifications on those interfaces. The change detection logic will update based on all collection changes, even if that doesn’t affect the specific indexed value. This is because the listening logic is common across all instances of the collection.

If the data source is a Dictionary or Map, then a property path can specify items in the collection by their string name. For example **&lt;TextBlock Text="{x:Bind Players\['John Smith'\]}" /&gt;** will look for an item in the dictionary named "John Smith". The name needs to be enclosed in quotes, and either single or double quotes can be used. Hat (^) can be used to escape quotes in strings. It's usually easiest to use alternate quotes from those used for the XAML attribute. (Note that IReadOnlyDictionary&lt;T&gt; and IMapView&lt;T&gt; do not support the indexer syntax.)

To use a string indexer, the model needs to implement **IDictionary&lt;string, T&gt;** or **IMap&lt;string, T&gt;** on the type of the property that is going to be indexed. If the type of the indexed property supports **IObservableMap** and the binding is OneWay or TwoWay, then it will register and listen for change notifications on those interfaces. The change detection logic will update based on all collection changes, even if that doesn’t affect the specific indexed value. This is because the listening logic is common across all instances of the collection.

### Attached Properties

To bind to [attached properties](./attached-properties-overview.md), you need to put the class and property name into parentheses after the dot. For example **Text="{x:Bind Button22.(Grid.Row)}"**. If the property is not declared in a Xaml namespace, then you will need to prefix it with an xml namespace, which you should map to a code namespace at the head of the document.

### Casting

Compiled bindings are strongly typed, and will resolve the type of each step in a path. If the type returned doesn’t have the member, it will fail at compile time. You can specify a cast to tell binding the real type of the object.

In the following case, **obj** is a property of type object, but contains a text box, so we can use either **Text="{x:Bind ((TextBox)obj).Text}"** or **Text="{x:Bind obj.(TextBox.Text)}"**.

The **groups3** field in **Text="{x:Bind ((data:SampleDataGroup)groups3\[0\]).Title}"** is a dictionary of objects, so you must cast it to **data:SampleDataGroup**. Note the use of the xml **data:** namespace prefix for mapping the object type to a code namespace that isn't part of the default XAML namespace.

_Note: The C#-style cast syntax is more flexible than the attached property syntax, and is the recommended syntax going forward._

#### Pathless casting

The native bind parser doesn't provide a keyword to represent `this` as a function parameter, but it does support pathless casting (for example, `{x:Bind (x:String)}`), which can be used as a function parameter. Therefore, `{x:Bind MethodName((namespace:TypeOfThis))}` is a valid way to perform what is conceptually equivalent to `{x:Bind MethodName(this)}`.

Example:

`Text="{x:Bind local:MainPage.GenerateSongTitle((local:SongItem))}"`

```xaml
<Page
    x:Class="AppSample.MainPage"
    ...
    xmlns:local="using:AppSample">

    <Grid>
        <ListView ItemsSource="{x:Bind Songs}">
            <ListView.ItemTemplate>
                <DataTemplate x:DataType="local:SongItem">
                    <TextBlock
                        Margin="12"
                        FontSize="40"
                        Text="{x:Bind local:MainPage.GenerateSongTitle((local:SongItem))}" />
                </DataTemplate>
            </ListView.ItemTemplate>
        </ListView>
    </Grid>
</Page>
```

```csharp
namespace AppSample
{
    public class SongItem
    {
        public string TrackName { get; private set; }
        public string ArtistName { get; private set; }

        public SongItem(string trackName, string artistName)
        {
            ArtistName = artistName;
            TrackName = trackName;
        }
    }

    public sealed partial class MainPage : Page
    {
        public List<SongItem> Songs { get; }
        public MainPage()
        {
            Songs = new List<SongItem>()
            {
                new SongItem("Track 1", "Artist 1"),
                new SongItem("Track 2", "Artist 2"),
                new SongItem("Track 3", "Artist 3")
            };

            this.InitializeComponent();
        }

        public static string GenerateSongTitle(SongItem song)
        {
            return $"{song.TrackName} - {song.ArtistName}";
        }
    }
}
```

## Functions in binding paths

Starting in Windows 10, version 1607, **{x:Bind}** supports using a function as the leaf step of the binding path. This is a powerful feature for databinding that enables several scenarios in markup. See [function bindings](../data-binding/function-bindings.md) for details.

## Event Binding

Event binding is a unique feature for compiled binding. It enables you to specify the handler for an event using a binding, rather than it having to be a method on the code behind. For example: **Click="{x:Bind rootFrame.GoForward}"**.

For events, the target method must not be overloaded and must also:

- Match the signature of the event.
- OR have no parameters.
- OR have the same number of parameters of types that are assignable from the types of the event parameters.

In generated code-behind, compiled binding handles the event and routes it to the method on the model, evaluating the path of the binding expression when the event occurs. This means that, unlike property bindings, it doesn’t track changes to the model.

For more info about the string syntax for a property path, see [Property-path syntax](property-path-syntax.md), keeping in mind the differences described here for **{x:Bind}**.

## Properties that you can set with {x:Bind}

**{x:Bind}** is illustrated with the *bindingProperties* placeholder syntax because there are multiple read/write properties that can be set in the markup extension. The properties can be set in any order with comma-separated *propName*=*value* pairs. Note that you cannot include line breaks in the binding expression. Some of the properties require types that don't have a type conversion, so these require markup extensions of their own nested within the **{x:Bind}**.

These properties work in much the same way as the properties of the [**Binding**](/uwp/api/Windows.UI.Xaml.Data.Binding) class.

| Property | Description |
|----------|-------------|
| **Path** | See the [Property path](#property-path) section above. |
| **Converter** | Specifies the converter object that is called by the binding engine. The converter can be set in XAML, but only if you refer to an object instance that you've assigned in a [{StaticResource} markup extension](staticresource-markup-extension.md) reference to that object in the resource dictionary. |
| **ConverterLanguage** | Specifies the culture to be used by the converter. (If you're setting **ConverterLanguage** you should also be setting **Converter**.) The culture is set as a standards-based identifier. For more info, see [**ConverterLanguage**](/uwp/api/windows.ui.xaml.data.binding.converterlanguage). |
| **ConverterParameter** | Specifies the converter parameter that can be used in converter logic. (If you're setting **ConverterParameter** you should also be setting **Converter**.) Most converters use simple logic that get all the info they need from the passed value to convert, and don't need a **ConverterParameter** value. The **ConverterParameter** parameter is for moderately advanced converter implementations that have more than one logic that keys off what's passed in **ConverterParameter**. You can write a converter that uses values other than strings but this is uncommon, see Remarks in [**ConverterParameter**](/uwp/api/windows.ui.xaml.data.binding.converterparameter) for more info. |
| **FallbackValue** | Specifies a value to display when the source or path cannot be resolved. |
| **Mode** | Specifies the binding mode, as one of these strings: "OneTime", "OneWay", or "TwoWay". The default is "OneTime". Note that this differs from the default for **{Binding}**, which is "OneWay" in most cases. |
| **TargetNullValue** | Specifies a value to display when the source value resolves but is explicitly **null**. |
| **BindBack** | Specifies a function to use for the reverse direction of a two-way binding. |
| **UpdateSourceTrigger** | Specifies when to push changes back from the control to the model in TwoWay bindings. The default for all properties except TextBox.Text is PropertyChanged; TextBox.Text is LostFocus.|

> [!NOTE]
> If you're converting markup from **{Binding}** to **{x:Bind}**, then be aware of the differences in default values for the **Mode** property.
> [**x:DefaultBindMode**](./x-defaultbindmode-attribute.md) can be used to change the default mode for x:Bind for a specific segment of the markup tree. The mode selected will apply any x:Bind expressions on that element and its children, that do not explicitly specify a mode as part of the binding. OneTime is more performant than OneWay as using OneWay will cause more code to be generated to hookup and handle the change detection.

## Remarks

Because **{x:Bind}** uses generated code to achieve its benefits, it requires type information at compile time. This means that you cannot bind to properties where you do not know the type ahead of time. Because of this, you cannot use **{x:Bind}** with the **DataContext** property, which is of type **Object**, and is also subject to change at run time.

When using **{x:Bind}** with data templates, you must indicate the type being bound to by setting an **x:DataType** value, as shown in the [Examples](#examples) section. You can also set the type to an interface or base class type, and then use casts if necessary to formulate a full expression.

Compiled bindings depend on code generation. So if you use **{x:Bind}** in a resource dictionary then the resource dictionary needs to have a code-behind class. See [Resource dictionaries with {x:Bind}](../data-binding/data-binding-in-depth.md#resource-dictionaries-with-x-bind) for a code example.

Pages and user controls that include Compiled bindings will have a "Bindings" property in the generated code. This includes the following methods:

- **Update()** - This will update the values of all compiled bindings. Any one-way/Two-Way bindings will have the listeners hooked up to detect changes.
- **Initialize()** - If the bindings have not already been initialized, then it will call Update() to initialize the bindings
- **StopTracking()** - This will unhook all listeners created for one-way and two-way bindings. They can be re-initialized using the Update() method.

> [!NOTE]
> Starting in Windows 10, version 1607, the XAML framework provides a built in Boolean to Visibility converter. The converter maps **true** to the **Visible** enumeration value and **false** to **Collapsed** so you can bind a Visibility property to a Boolean without creating a converter. Note that this is not a feature of function binding, only property binding. To use the built in converter, your app's minimum target SDK version must be 14393 or later. You can't use it when your app targets earlier versions of Windows 10. For more info about target versions, see [Version adaptive code](../debug-test-perf/version-adaptive-code.md).

**Tip**   If you need to specify a single curly brace for a value, such as in [**Path**](/uwp/api/windows.ui.xaml.data.binding.path) or [**ConverterParameter**](/uwp/api/windows.ui.xaml.data.binding.converterparameter), precede it with a backslash: `\{`. Alternatively, enclose the entire string that contains the braces that need escaping in a secondary quotation set, for example `ConverterParameter='{Mix}'`.

[**Converter**](/uwp/api/windows.ui.xaml.data.binding.converter), [**ConverterLanguage**](/uwp/api/windows.ui.xaml.data.binding.converterlanguage) and **ConverterLanguage** are all related to the scenario of converting a value or type from the binding source into a type or value that is compatible with the binding target property. For more info and examples, see the "Data conversions" section of [Data binding in depth](../data-binding/data-binding-in-depth.md).

**{x:Bind}** is a markup extension only, with no way to create or manipulate such bindings programmatically. For more info about markup extensions, see [XAML overview](xaml-overview.md).