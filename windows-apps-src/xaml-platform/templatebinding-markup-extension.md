---
author: jwmsft
description: Links the value of a property in a control template to the value of some other exposed property on the templated control. TemplateBinding can only be used within a ControlTemplate definition in XAML.
title: TemplateBinding markup extension
ms.assetid: FDE71086-9D42-4287-89ED-8FBFCDF169DC
ms.author: jimwalk
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
localizationpriority: medium
---

# {TemplateBinding} markup extension


Links the value of a property in a control template to the value of some other exposed property on the templated control. **TemplateBinding** can only be used within a [**ControlTemplate**](https://msdn.microsoft.com/library/windows/apps/br209391) definition in XAML.

## XAML attribute usage

``` syntax
<object propertyName="{TemplateBinding sourceProperty}" .../>
```

## XAML attribute usage (for Setter property in template or style)

``` syntax
<Setter Property="propertyName" Value="{TemplateBinding sourceProperty}" .../>
```

## XAML values

| Term | Description |
|------|-------------|
| propertyName | The name of the property being set in the setter syntax. This must be a dependency property. |
| sourceProperty | The name of another dependency property that exists on the type being templated. |

## Remarks

Using **TemplateBinding** is a fundamental part of how you define a control template, either if you are a custom control author or if you are replacing a control template for existing controls. For more info, see [Quickstart: Control templates](https://msdn.microsoft.com/library/windows/apps/xaml/hh465374).

It's fairly common for *propertyName* and *targetProperty* to use the same property name. In this case, a control might define a property on itself and forward the property to an existing and intuitively named property of one of its component parts. For example, a control that incorporates a [**TextBlock**](https://msdn.microsoft.com/library/windows/apps/br209652) in its compositing, which is used to display the control's own **Text** property, might include this XAML as a part in the control template: `<TextBlock Text="{TemplateBinding Text}" .... />`

The types used as the value for the source property and the target property must match. There's no opportunity to introduce a converter when you're using **TemplateBinding**. Failing to match values results in an error when parsing the XAML. If you need a converter you can use the verbose syntax for a template binding such as: `{Binding RelativeSource={RelativeSource TemplatedParent}, Converter="..." ...}`

Attempting to use a **TemplateBinding** outside of a [**ControlTemplate**](https://msdn.microsoft.com/library/windows/apps/br209391) definition in XAML will result in a parser error.

You can use **TemplateBinding** for cases where the templated parent value is also deferred as another binding. The evaluation for **TemplateBinding** can wait until any required runtime bindings have values.

A **TemplateBinding** is always a one-way binding. Both properties involved must be dependency properties.

**TemplateBinding** is a markup extension. Markup extensions are typically implemented when there is a requirement to escape attribute values to be other than literal values or handler names, and the requirement is more global than just putting type converters on certain types or properties. All markup extensions in XAML use the "{" and "}" characters in their attribute syntax, which is the convention by which a XAML processor recognizes that a markup extension must process the attribute.

**Note**  In the Windows Runtime XAML processor implementation, there is no backing class representation for **TemplateBinding**. **TemplateBinding** is exclusively for use in XAML markup. There isn't a straightforward way to reproduce the behavior in code.

## Related topics

* [Quickstart: Control templates](https://msdn.microsoft.com/library/windows/apps/xaml/hh465374)
* [Data binding in depth](https://msdn.microsoft.com/library/windows/apps/mt210946)
* [**ControlTemplate**](https://msdn.microsoft.com/library/windows/apps/br209391)
* [XAML overview](xaml-overview.md)
* [Dependency properties overview](dependency-properties-overview.md)
 

