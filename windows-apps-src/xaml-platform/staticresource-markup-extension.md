---
description: Provides a value for any XAML attribute by evaluating a reference to an already defined resource. Resources are defined in a ResourceDictionary, and a StaticResource usage references the key of that resource in the ResourceDictionary.
title: StaticResource markup extension
ms.assetid: D50349B5-4588-4EBD-9458-75F629CCC395
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# {StaticResource} markup extension


Provides a value for any XAML attribute by evaluating a reference to an already defined resource. Resources are defined in a [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary), and a **StaticResource** usage references the key of that resource in the **ResourceDictionary**.

## XAML attribute usage

``` syntax
<object property="{StaticResource key}" .../>
```

## XAML values

| Term | Description |
|------|-------------|
| key | The key for the requested resource. This key is initially assigned by the [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary). A resource key can be any string defined in the XamlName Grammar. |

## Remarks

**StaticResource** is a technique for obtaining values for a XAML attribute that are defined elsewhere in a XAML resource dictionary. Values might be placed in a resource dictionary because they are intended to be shared by multiple property values, or because a XAML resource dictionary is used as a XAML packaging or factoring technique. An example of a XAML packaging technique is the theme dictionary for a control. Another example is merged resource dictionaries used for resource fallback.

**StaticResource** takes one argument, which specifies the key for the requested resource. A resource key is always a string in Windows Runtime XAML. For more info on how the resource key is initially specified, see [x:Key attribute](x-key-attribute.md).

The rules by which a **StaticResource** resolves to an item in a resource dictionary are not described in this topic. That depends on whether the reference and the resource both exist in a template, whether merged resource dictionaries are used, and so on. For more info on how to define resources and properly use a [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary), including sample code, see [ResourceDictionary and XAML resource references](../design/controls-and-patterns/resourcedictionary-and-xaml-resource-references.md).

**Important**  
A **StaticResource** must not attempt to make a forward reference to a resource that is defined lexically further within the XAML file. Attempting to do so is not supported. Even if the forward reference doesn't fail, trying to make one carries a performance penalty. For best results, adjust the composition of your resource dictionaries so that forward references are avoided.

Attempting to specify a **StaticResource** to a key that cannot resolve throws a XAML parse exception at run time. Design tools may also offer warnings or errors.

In the Windows Runtime XAML processor implementation, there is no backing class representation for **StaticResource** functionality. **StaticResource** is exclusively for use in XAML. The closest equivalent in code is to use the collection API of a [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary), for example calling [**Contains**](/uwp/api/windows.ui.xaml.resourcedictionary.contains) or [**TryGetValue**](/uwp/api/windows.ui.xaml.resourcedictionary.trygetvalue).

[{ThemeResource} markup extension](themeresource-markup-extension.md) is a similar markup extension that references named resources in another location. The difference is that {ThemeResource} markup extension has the ability to return different resources depending on the system theme that's active. For more info see [{ThemeResource} markup extension](themeresource-markup-extension.md).

**StaticResource** is a markup extension. Markup extensions are typically implemented when there is a requirement to escape attribute values to be other than literal values or handler names, and the requirement is more global than just putting type converters on certain types or properties. All markup extensions in XAML use the "\{" and "\}" characters in their attribute syntax, which is the convention by which a XAML processor recognizes that a markup extension must process the attribute.

### An example {StaticResource} usage

This example XAML is taken from the [XAML data binding sample](https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/XamlBind).

```xml
<StackPanel Margin="5">
    <!-- Add converter as a resource to reference it from a Binding. --> 
    <StackPanel.Resources>
        <local:S2Formatter x:Key="GradeConverter"/>
    </StackPanel.Resources>
    <TextBlock Style="{StaticResource BasicTextStyle}" Text="Percent grade:" Margin="5" />
    <Slider x:Name="sliderValueConverter" Minimum="1" Maximum="100" Value="70" Margin="5"/>
    <TextBlock Style="{StaticResource BasicTextStyle}" Text="Letter grade:" Margin="5"/>
    <TextBox x:Name="tbValueConverterDataBound"
      Text="{Binding ElementName=sliderValueConverter, Path=Value, Mode=OneWay,  
        Converter={StaticResource GradeConverter}}" Margin="5" Width="150"/> 
</StackPanel> 
```

This particular example creates an object that's backed by a custom class, and creates it as a resource in a [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary). To be a valid resource, this `local:S2Formatter` element must also have an **x:Key** attribute value. The value of the attribute is set to "GradeConverter".

The resource is then requested just a bit further into the XAML, where you see `{StaticResource GradeConverter}`.

Note how the {StaticResource} markup extension usage is setting a property of another markup extension [{Binding} markup extension](binding-markup-extension.md), so there's two nested markup extension usages here. The inner one is evaluated first, so that the resource is obtained first and can be used as a value. This same example is also shown in {Binding} markup extension.

## Design-time tools support for the **{StaticResource}** markup extension

Microsoft Visual Studio 2013 can include possible key values in the Microsoft IntelliSense dropdowns when you use the **{StaticResource}** markup extension in a XAML page. For example, as soon as you type "{StaticResource", any of the resource keys from the current lookup scope are displayed in the IntelliSense dropdowns. In addition to the typical resources you'd have at page level ([**FrameworkElement.Resources**](/uwp/api/windows.ui.xaml.frameworkelement.resources)) and app level ([**Application.Resources**](/uwp/api/windows.ui.xaml.application.resources)), you also see [XAML theme resources](../design/controls-and-patterns/xaml-theme-resources.md), and resources from any extensions your project is using.

Once a resource key exists as part of any **{StaticResource}** usage, the **Go To Definition** (F12) feature can resolve that resource and show you the dictionary where it's defined. For the theme resources, this goes to generic.xaml for design time.

## Related topics

* [ResourceDictionary and XAML resource references](../design/controls-and-patterns/resourcedictionary-and-xaml-resource-references.md)
* [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary)
* [x:Key attribute](x-key-attribute.md)
* [{ThemeResource} markup extension](themeresource-markup-extension.md)