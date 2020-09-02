---
description: Provides a means to specify the source of a binding in terms of a relative relationship in the run-time object graph.
title: RelativeSource markup extension
ms.assetid: B87DEF36-BE1F-4C16-B32E-7A896BD09272
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# {RelativeSource} markup extension


Provides a means to specify the source of a binding in terms of a relative relationship in the run-time object graph.

## XAML attribute usage (Self mode)

``` syntax
<Binding RelativeSource="{RelativeSource Self}" .../>
-or-
<object property="{Binding RelativeSource={RelativeSource Self} ...}" .../>
```

## XAML attribute usage (TemplatedParent mode)

``` syntax
<Binding RelativeSource="{RelativeSource TemplatedParent}" .../>
-or-
<object property="{Binding RelativeSource={RelativeSource TemplatedParent} ...}" .../>
```

## XAML values

| Term | Description |
|------|-------------|
| {RelativeSource Self} | Produces a [<strong>Mode</strong>](/uwp/api/windows.ui.xaml.data.relativesource.mode) value of <strong>Self</strong>. The target element should be used as the source for this binding. This is useful for binding one property of an element to another property on the same element. |
| {RelativeSource TemplatedParent} | Produces a [<strong>ControlTemplate</strong>](/uwp/api/Windows.UI.Xaml.Controls.ControlTemplate) that is applied as the source for this binding. This is useful for applying runtime information to bindings at the template level. | 

## Remarks

A [**Binding**](/uwp/api/Windows.UI.Xaml.Data.Binding) can set [**Binding.RelativeSource**](/uwp/api/windows.ui.xaml.data.binding.relativesource) either as an attribute on a **Binding** object element or as a component within a [{Binding} markup extension](binding-markup-extension.md). This is why two different XAML syntaxes are shown.

**RelativeSource** is similar to [{Binding} markup extension](binding-markup-extension.md).  It is a markup extension that is capable of returning instances of itself, and supporting a string-based construction that essentially passes an argument to the constructor. In this case, the argument being passed is the [**Mode**](/uwp/api/windows.ui.xaml.data.relativesource.mode) value.

The **Self** mode is useful for binding one property of an element to another property on the same element, and is a variation on [**ElementName**](/uwp/api/windows.ui.xaml.data.binding.elementname) binding but does not require naming and then self-referencing the element. If you bind one property of an element to another property on the same element, either the properties must use the same property type, or you must also use a [**Converter**](/uwp/api/windows.ui.xaml.data.binding.converter) on the binding to convert the values. For example, you could use [**Height**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Height) as a source for [**Width**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Width) without conversion, but you'd need a converter to use [**IsEnabled**](/uwp/api/windows.ui.xaml.controls.control.isenabled) as a source for [**Visibility**](/uwp/api/Windows.UI.Xaml.Visibility).

Here's an example. This [**Rectangle**](/uwp/api/Windows.UI.Xaml.Shapes.Rectangle) uses a [{Binding} markup extension](binding-markup-extension.md) so that its [**Height**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Height) and [**Width**](/uwp/api/Windows.UI.Xaml.FrameworkElement.Width) are always equal and it renders as a square. Only the Height is set as a fixed value. For this **Rectangle** its default [**DataContext**](/uwp/api/windows.ui.xaml.frameworkelement.datacontext) is **null**, not **this**. So to establish the data context source to be the object itself (and enable binding to its other properties) we use the `RelativeSource={RelativeSource Self}` argument in the {Binding} markup extension usage.

```XML
<Rectangle
  Fill="Orange" Width="200"
  Height="{Binding RelativeSource={RelativeSource Self}, Path=Width}"
/>
```

Another use of `RelativeSource={RelativeSource Self}` is as a way to set an object's [**DataContext**](/uwp/api/windows.ui.xaml.frameworkelement.datacontext) to itself.  For example, you may see this technique in some of the SDK examples where the [**Page**](/uwp/api/Windows.UI.Xaml.Controls.Page) class has been extended with a custom property that's already providing a ready-to-go view model for its own data binding such as: `<common:LayoutAwarePage ... DataContext="{Binding DefaultViewModel, RelativeSource={RelativeSource Self}}">`

**Note**  The XAML usage for **RelativeSource** shows only the usage for which it is intended: setting a value for [**Binding.RelativeSource**](/uwp/api/windows.ui.xaml.data.binding.relativesource) in XAML as part of a binding expression. Theoretically, other usages are possible if setting a property where the value is [**RelativeSource**](/uwp/api/Windows.UI.Xaml.Data.RelativeSource).

## Related topics

* [XAML overview](xaml-overview.md)
* [Data binding in depth](../data-binding/data-binding-in-depth.md)
* [{Binding} markup extension](binding-markup-extension.md)
* [**Binding**](/uwp/api/Windows.UI.Xaml.Data.Binding)
* [**RelativeSource**](/uwp/api/Windows.UI.Xaml.Data.RelativeSource)