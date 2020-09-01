---
description: The Binding markup extension is converted at XAML load time into an instance of the Binding class.
title: Binding markup extension'
ms.assetid: 3BAFE7B5-AF33-487F-9AD5-BEAFD65D04C3
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
ms.custom: RS5
---
# {Binding} markup extension


> [!NOTE]
> A new binding mechanism is available for Windows 10, which is optimized for performance and developer productivity. See [{x:Bind} markup extension](x-bind-markup-extension.md).

> [!NOTE]
> For general info about using data binding in your app with **{Binding}** (and for an all-up comparison between **{x:Bind}** and **{Binding}**), see [Data binding in depth](../data-binding/data-binding-in-depth.md).

The **{Binding}** markup extension is used to data bind properties on controls to values coming from a data source such as code. The **{Binding}** markup extension is converted at XAML load time into an instance of the [**Binding**](/uwp/api/Windows.UI.Xaml.Data.Binding) class. This binding object gets a value from a property on a data source, and pushes it to the property on the control. The binding object can optionally be configured to observe changes in the value of the data source property and update itself based on those changes. It can also optionally be configured to push changes to the control value back to the source property. The property that is the target of a data binding must be a dependency property. For more info, see [Dependency properties overview](dependency-properties-overview.md).

**{Binding}** has the same dependency property precedence as a local value, and setting a local value in imperative code removes the effect of any **{Binding}** set in markup.

## XAML attribute usage


``` syntax
<object property="{Binding}" .../>
-or-
<object property="{Binding propertyPath}" .../>
-or-
<object property="{Binding bindingProperties}" .../>
-or-
<object property="{Binding propertyPath, bindingProperties}" .../>
```

| Term | Description |
|------|-------------|
| *propertyPath* | A string that specifies the property path for the binding. More info is in the [Property path](#property-path) section below. |
| *bindingProperties* | *propName*=*value*\[, *propName*=*value*\]*<br/>One or more binding properties that are specified using a name/value pair syntax. |
| *propName* | The string name of the property to set on the [**Binding**](/uwp/api/Windows.UI.Xaml.Data.Binding) object. For example, "Converter". |
| *value* | The value to set the property to. The syntax of the argument depends on the property of [Properties of the Binding class that can be set with {Binding}](#properties-of-the-binding-class-that-can-be-set-with-binding) section below. |

## Property path

[**Path**](/uwp/api/windows.ui.xaml.data.binding.path) describes the property that you're binding to (the source property). Path is a positional parameter, which means you can use the parameter name explicitly (`{Binding Path=EmployeeID}`), or you can specify it as the first unnamed parameter (`{Binding EmployeeID}`).

The type of [**Path**](/uwp/api/windows.ui.xaml.data.binding.path) is a property path, which is a string that evaluates to a property or sub-property of either your custom type or a framework type. The type can be, but does not need to be, a [**DependencyObject**](/uwp/api/Windows.UI.Xaml.DependencyObject). Steps in a property path are delimited by dots (.), and you can include multiple delimiters to traverse successive sub-properties. Use the dot delimiter regardless of the programming language used to implement the object being bound to.

For example, to bind UI to an employee object's first name property, your property path might be "Employee.FirstName". If you are binding an items control to a property that contains an employee's dependents, your property path might be "Employee.Dependents", and the item template of the items control would take care of displaying the items in "Dependents".

If the data source is a collection, then a property path can specify items in the collection by their position or index. For example, "Teams\[0\].Players", where the literal "\[\]" encloses the "0" that specifies the first item in a collection.

When using an [**ElementName**](/uwp/api/windows.ui.xaml.data.binding.elementname) binding to an existing [**DependencyObject**](/uwp/api/Windows.UI.Xaml.DependencyObject), you can use attached properties as part of the property path. To disambiguate an attached property so that the intermediate dot in the attached property name is not considered a step into a property path, put parentheses around the owner-qualified attached property name; for example, `(AutomationProperties.Name)`.

A property path intermediate object is stored as a [**PropertyPath**](/uwp/api/Windows.UI.Xaml.PropertyPath) object in a run-time representation, but most scenarios won't need to interact with a **PropertyPath** object in code. You can usually specify the binding info you need using XAML.

For more info about the string syntax for a property path, property paths in animation feature areas, and constructing a [**PropertyPath**](/uwp/api/Windows.UI.Xaml.PropertyPath) object, see [Property-path syntax](property-path-syntax.md).

## Properties of the Binding class that can be set with {Binding}


**{Binding}** is illustrated with the *bindingProperties* placeholder syntax because there are multiple read/write properties of a [**Binding**](/uwp/api/Windows.UI.Xaml.Data.Binding) that can be set in the markup extension. The properties can be set in any order with comma-separated *propName*=*value* pairs. Some of the properties require types that don't have a type conversion, so these require markup extensions of their own nested within the **{Binding}**.

| Property | Description |
|----------|-------------|
| [**Path**](/uwp/api/windows.ui.xaml.data.binding.path) | See the [Property path](#property-path) section above. |
| [**Converter**](/uwp/api/windows.ui.xaml.data.binding.converter) | Specifies a converter object that is called by the binding engine. The converter can be set in markup using the [{StaticResource} markup extension](staticresource-markup-extension.md) to reference to that object from a resource dictionary. |
| [**ConverterLanguage**](/uwp/api/windows.ui.xaml.data.binding.converterlanguage) | Specifies the culture to be used by the converter. (If you're setting [**Converter**](/uwp/api/windows.ui.xaml.data.binding.converter).) The culture is set as a standards-based identifier. For more info, see [**ConverterLanguage**](/uwp/api/windows.ui.xaml.data.binding.converterlanguage) |
| [**ConverterParameter**](/uwp/api/windows.ui.xaml.data.binding.converterparameter) | Specifies a converter parameter that can be used in converter logic. (If you're setting [**Converter**](/uwp/api/windows.ui.xaml.data.binding.converter).) Most converters use simple logic that get all the info they need from the passed value to convert, and don't need a **ConverterParameter** value. The **ConverterParameter** parameter is for more complex converter implementations that have conditional logic that keys off what's passed in **ConverterParameter**. You can write a converter that uses values other than strings but this is uncommon, see Remarks in **ConverterParameter** for more info. |
| [**ElementName**](/uwp/api/windows.ui.xaml.data.binding.elementname) | Specifies a data source by referencing another element in the same XAML construct that has a **Name** property or [x:Name attribute](x-name-attribute.md). This is often use to share related values or use sub-properties of one UI element to provide a specific value for another element, for example in a XAML control template. |
| [**FallbackValue**](/uwp/api/windows.ui.xaml.data.binding.fallbackvalue) | Specifies a value to display when the source or path cannot be resolved. |
| [**Mode**](/uwp/api/windows.ui.xaml.data.binding.mode) | Specifies the binding mode, as one of these values: "OneTime", "OneWay", or "TwoWay". These correspond to the constant names of the [**BindingMode**](/uwp/api/Windows.UI.Xaml.Data.BindingMode) enumeration. The default is "OneWay". Note that this differs from the default for **{x:Bind}**, which is "OneTime". | 
| [**RelativeSource**](/uwp/api/windows.ui.xaml.data.binding.relativesource) | Specifies a data source by describing the position of the binding source relative to the position of the binding target. This is most often used in bindings within XAML control templates. Setting the [{RelativeSource} markup extension](relativesource-markup-extension.md). |
| [**Source**](/uwp/api/windows.ui.xaml.data.binding.source) | Specifies the object data source. Within the **Binding** markup extension, the [**Source**](/uwp/api/windows.ui.xaml.data.binding.source) property requires an object reference, such as a [{StaticResource} markup extension](staticresource-markup-extension.md) reference. If this property is not specified, the acting data context specifies the source. It's more typical to not specify a Source value in individual bindings, and instead to rely on the shared **DataContext** for multiple bindings. For more info see [**DataContext**](/uwp/api/windows.ui.xaml.frameworkelement.datacontext) or [Data binding in depth](../data-binding/data-binding-in-depth.md). |
| [**TargetNullValue**](/uwp/api/windows.ui.xaml.data.binding.targetnullvalue) | Specifies a value to display when the source value resolves but is explicitly **null**. |
| [**UpdateSourceTrigger**](/uwp/api/windows.ui.xaml.data.binding.updatesourcetrigger) | Specifies the timing of binding source updates. If unspecified, the default is **Default**. |

**Note**  If you're converting markup from **{x:Bind}** to **{Binding}**, then be aware of the differences in default values for the **Mode** property.

[**Converter**](/uwp/api/windows.ui.xaml.data.binding.converter), [**ConverterLanguage**](/uwp/api/windows.ui.xaml.data.binding.converterlanguage) and **ConverterLanguage** are all related to the scenario of converting a value or type from the binding source into a type or value that is compatible with the binding target property. For more info and examples, see the "Data conversions" section of [Data binding in depth](../data-binding/data-binding-in-depth.md).

> [!NOTE]
> Starting in Windows 10, version 1607, the XAML framework provides a built in Boolean to Visibility converter. The converter maps **true** to the **Visible** enumeration value and **false** to **Collapsed** so you can bind a Visibility property to a Boolean without creating a converter. To use the built in converter, your app's minimum target SDK version must be 14393 or later. You can't use it when your app targets earlier versions of Windows 10. For more info about target versions, see [Version adaptive code](../debug-test-perf/version-adaptive-code.md).

[**Source**](/uwp/api/windows.ui.xaml.data.binding.source), [**RelativeSource**](/uwp/api/windows.ui.xaml.data.binding.relativesource), and [**ElementName**](/uwp/api/windows.ui.xaml.data.binding.elementname) specify a binding source, so they are mutually exclusive.

**Tip**  If you need to specify a single curly brace for a value, such as in [**Path**](/uwp/api/windows.ui.xaml.data.binding.path) or [**ConverterParameter**](/uwp/api/windows.ui.xaml.data.binding.converterparameter), then precede it with a backslash: `\{`. Alternatively, enclose the entire string that contains the braces that need escaping in a secondary quotation set, for example `ConverterParameter='{Mix}'`.

## Examples

```XML
<!-- binding a UI element to a view model -->    
<Page ... >
    <Page.DataContext>
        <local:BookstoreViewModel/>
    </Page.DataContext>

    <GridView ItemsSource="{Binding BookSkus}" SelectedItem="{Binding SelectedBookSku, Mode=TwoWay}" ... />
</Page>
```

```XML
<!-- binding a UI element to another UI element -->
<Page ... >
    <Page.Resources>
        <local:S2Formatter x:Key="GradeConverter"/>
    </Page.Resources>

    <Slider x:Name="sliderValueConverter" ... />
    <TextBox Text="{Binding Path=Value, ElementName=sliderValueConverter,
        Mode=OneWay,
        Converter={StaticResource GradeConverter}}"/>
</Page>
```

The second example sets four different [**Binding**](/uwp/api/Windows.UI.Xaml.Data.Binding) properties: [**ElementName**](/uwp/api/windows.ui.xaml.data.binding.elementname), [**Path**](/uwp/api/windows.ui.xaml.data.binding.path), [**Mode**](/uwp/api/windows.ui.xaml.data.binding.mode) and [**Converter**](/uwp/api/windows.ui.xaml.data.binding.converter). **Path** in this case is shown explicitly named as a **Binding** property. The **Path** is evaluated to a data binding source that is another object in the same run-time object tree, a [**Slider**](/uwp/api/Windows.UI.Xaml.Controls.Slider) named `sliderValueConverter`.

Note how the [**Converter**](/uwp/api/windows.ui.xaml.data.binding.converter) property value uses another markup extension, [{StaticResource} markup extension](staticresource-markup-extension.md), so there are two nested markup extension usages here. The inner one is evaluated first, so that once the resource is obtained there's a practical [**IValueConverter**](/uwp/api/Windows.UI.Xaml.Data.IValueConverter) (a custom class that's instantiated by the `local:S2Formatter` element in resources) that the binding can use.

## Tools support

Microsoft IntelliSense in Microsoft Visual Studio displays the properties of the data context while authoring **{Binding}** in the XAML markup editor. As soon as you type "{Binding", data context properties appropriate for [**Path**](/uwp/api/windows.ui.xaml.data.binding.path) are displayed in the dropdown. IntelliSense also helps with the other properties of [**Binding**](/uwp/api/Windows.UI.Xaml.Data.Binding). For this to work, you must have either the data context or the design-time data context set in the markup page. **Go To Definition** (F12) also works with **{Binding}**. Alternatively, you can use the data binding dialog.

 