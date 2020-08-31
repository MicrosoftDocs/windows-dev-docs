---
title: xDeferLoadStrategy attribute
description: xDeferLoadStrategy delays the creation of an element and its children, decreasing startup time but increasing memory usage slightly. Each element affected adds about 600 bytes to the memory usage.
ms.assetid: E763898E-13FF-4412-B502-B54DBFE2D4E4
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# x:DeferLoadStrategy attribute

> [!IMPORTANT]
> Starting in Windows 10, version 1703 (Creators Update), **x:DeferLoadStrategy** is superseded by the [**x:Load attribute**](x-load-attribute.md). Using `x:Load="False"` is equivalent to `x:DeferLoadStrategy="Lazy"`, but provides the ability to unload the UI if required. See the [x:Load attribute](x-load-attribute.md) for more info.

You can use **x:DeferLoadStrategy="Lazy"** to optimize the startup or tree creation performance of your XAML app. When you use **x:DeferLoadStrategy="Lazy"**, creation of an element and its children is delayed, which decreases startup time and memory costs. This is useful to reduce the costs of elements that are shown infrequently or conditionally. The element will be realized when it's referred to from code or VisualStateManager.

However, the tracking of deferred elements by the XAML framework adds about 600 bytes to the memory usage for each element affected. The larger the element tree you defer, the more startup time you'll save, but at the cost of a greater memory footprint. Therefore, it's possible to overuse this attribute to the extent that your performance decreases.

## XAML attribute usage

``` syntax
<object x:DeferLoadStrategy="Lazy" .../>
```

## Remarks

The restrictions for using **x:DeferLoadStrategy** are:

- You must define an [x:Name](x-name-attribute.md) for the element, as there needs to be a way to find the element later.
- You can only defer types that derive from [**UIElement**](/uwp/api/Windows.UI.Xaml.UIElement) or [**FlyoutBase**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.FlyoutBase).
- You cannot defer root elements in a [**Page**](/uwp/api/windows.ui.xaml.controls.page), a [**UserControl**](/uwp/api/windows.ui.xaml.controls.usercontrol), or a [**DataTemplate**](/uwp/api/Windows.UI.Xaml.DataTemplate).
- You cannot defer elements in a [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary).
- You cannot defer loose XAML loaded with [**XamlReader.Load**](/uwp/api/windows.ui.xaml.markup.xamlreader.load).
- Moving a parent element will clear out any elements that have not been realized.

There are several different ways to realize the deferred elements:

- Call [**FindName**](/uwp/api/windows.ui.xaml.frameworkelement.findname) with the name that you defined on the element.
- Call [**GetTemplateChild**](/uwp/api/windows.ui.xaml.controls.control.gettemplatechild) with the name that you defined on the element.
- In a [**VisualState**](/uwp/api/Windows.UI.Xaml.VisualState), use a [**Setter**](/uwp/api/Windows.UI.Xaml.Setter) or **Storyboard** animation that targets the deferred element.
- Target the deferred element in any **Storyboard**.
- Use a binding that targets the deferred element.

> NOTE: Once the instantiation of an element has started, it is created on the UI thread, so it could cause the UI to stutter if too much is created at once.

Once a deferred element is created in any of the ways listed previously, several things happen:

- The [**Loaded**](/uwp/api/windows.ui.xaml.frameworkelement.loaded) event on the element is raised.
- Any bindings on the element are evaluated.
- If you have registered to receive property change notifications on the property containing the deferred element(s), the notification is raised.

You can nest deferred elements, however they have to be realized from the outer-most element in.  If you try to realize a child element before the parent has been realized, an exception is raised.

Typically, we recommend that you defer elements that are not viewable in the first frame. A good guideline for finding candidates to be deferred is to look for elements that are being created with collapsed [**Visibility**](/uwp/api/windows.ui.xaml.uielement.visibility). Also, UI that is triggered by user interaction is a good place to look for elements that you can defer.

Be wary of deferring elements in a [**ListView**](/uwp/api/Windows.UI.Xaml.Controls.ListView), as it will decrease your startup time, but could also decrease your panning performance depending on what you're creating. If you are looking to increase panning performance, see the [{x:Bind} markup extension](x-bind-markup-extension.md) and [x:Phase attribute](x-phase-attribute.md) documentation.

If the [x:Phase attribute](x-phase-attribute.md) is used in conjunction with **x:DeferLoadStrategy** then, when an element or an element tree is realized, the bindings are applied up to and including the current phase. The phase specified for **x:Phase** does not affect or control the deferral of the element. When a list item is recycled as part of panning, realized elements behave in the same way as other active elements, and compiled bindings (**{x:Bind}** bindings) are processed using the same rules, including phasing.

A general guideline is to measure the performance of your app before and after to make sure you are getting the performance that you want.

## Example

```xml
<Grid x:Name="DeferredGrid" x:DeferLoadStrategy="Lazy">
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
        <RowDefinition Height="Auto" />
    </Grid.RowDefinitions>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="Auto" />
        <ColumnDefinition Width="Auto" />
    </Grid.ColumnDefinitions>

    <Rectangle Height="100" Width="100" Fill="#F65314" Margin="0,0,4,4" />
    <Rectangle Height="100" Width="100" Fill="#7CBB00" Grid.Column="1" Margin="4,0,0,4" />
    <Rectangle Height="100" Width="100" Fill="#00A1F1" Grid.Row="1" Margin="0,4,4,0" />
    <Rectangle Height="100" Width="100" Fill="#FFBB00" Grid.Row="1" Grid.Column="1" Margin="4,4,0,0" />
</Grid>
<Button x:Name="RealizeElements" Content="Realize Elements" Click="RealizeElements_Click"/>
```

```csharp
private void RealizeElements_Click(object sender, RoutedEventArgs e)
{
    // This will realize the deferred grid.
    this.FindName("DeferredGrid");
}
```