---
author: jwmsft
title: xDeferLoadStrategy attribute
description: xDeferLoadStrategy delays the creation of an element and its children, decreasing startup time but increasing memory usage slightly. Each element affected adds about 600 bytes to the memory usage.
ms.assetid: E763898E-13FF-4412-B502-B54DBFE2D4E4
ms.author: jimwalk
ms.date: 02/08/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
---

# x:DeferLoadStrategy attribute

\[ Updated for UWP apps on Windows 10. For Windows 8.x articles, see the [archive](http://go.microsoft.com/fwlink/p/?linkid=619132) \]

**Note**  In RS2, **x:DeferLoadStrategy** has been superceded by the **x:Load** attribute instead. x:Load="True" is the same as x:DeferLoadStrategy="Lazy", but provides the ability to unload the UI if required. For more details see [x:Load attribute](x-load-attribute.md).

**x:DeferLoadStrategy="Lazy"** is a feature that can be used to optimize the performance of the startup or tree creation scenarios of a XAML app. Using **x:DeferLoadStrategy="Lazy"** delays the creation of an element and its children, decreasing startup time and memory costs by not needing to create the element(s). This is useful to reduce the costs of elements that are not often or conditionally needed. The element will be realized when its referred to from code or VisualStateManager.

However the book keeping for deferral adds about 600 bytes to the memory usage for each element affected. The larger the element tree you defer, the more startup time you'll save, but at the cost of a greater memory footprint. Therefore it's possible to overuse this attribute to the extent that your performance decreases.

## XAML attribute usage

``` syntax
<object x:DeferLoadStrategy="Lazy" .../>
```

## Remarks

The restrictions for using **x:DeferLoadStrategy** are:

-   Requires an [x:Name](x-name-attribute.md) defined, as there needs to be a way to find the element later.
-   Only a [**UIElement**](https://msdn.microsoft.com/library/windows/apps/br208911) can be marked as deferred, with the exception of types deriving from [**FlyoutBase**](https://msdn.microsoft.com/library/windows/apps/dn279249).
-   Root elements can not be deferred in a [**Page**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.page), a [**UserControls**](https://msdn.microsoft.com/library/windows/apps/windows.ui.xaml.controls.usercontrol), nor a [**DataTemplate**](https://msdn.microsoft.com/library/windows/apps/br242348).
-   Elements in a [**ResourceDictionary**](https://msdn.microsoft.com/library/windows/apps/br208794) cannot be deferred.
-   Does not work with loose XAML loaded with [**XamlReader.Load**](https://msdn.microsoft.com/library/windows/apps/br228048).
-   Moving a parent element will clear out any elements that have not been realized.

There are several different ways to realize the deferred elements:

-   Call [**FindName**](https://msdn.microsoft.com/library/windows/apps/br208715) with the name that was defined on the element.
-   Call [**GetTemplateChild**](https://msdn.microsoft.com/library/windows/apps/br209416) with the name that was defined on the element.
-   In a [**VisualState**](https://msdn.microsoft.com/library/windows/apps/br209007), use a [**Setter**](https://msdn.microsoft.com/library/windows/apps/br208817) or **Storyboard** animation that is targeting the deferred element.
-   Target the deferred element in any **Storyboard**.
-   Use a binding that is targeting the deferred element.
-   NOTE: Once the instantiation of an element has started, it is created on the UI thread, so it could cause the UI to stutter if too much is created at once.

Once a deferred element is created by any of the methods listed above, several things will happen:

-   The [**Loaded**](https://msdn.microsoft.com/library/windows/apps/br208723) event on the element will get raised.
-   Any bindings on the element will get evaluated.
-   If the application has registered to receive property change notifications on the property containing the deferred element(s), the notification will be raised.

You can nest deferred elements, however they have to be realized from the outer-most element in.  If you try to realize a child element before the parent has been realized, an exception will be raised.

In general, the recommendation is to defer things that are not viewable in the first frame.  A good guideline for finding candidates to be deferred is to look for elements that are being created with collapsed [**Visibility**](https://msdn.microsoft.com/library/windows/apps/br208992).  Also incidental UI (that is, UI that is triggered by user interaction) is a good place to look for deferring elements.  

Be wary of deferring elements in a [**ListView**](https://msdn.microsoft.com/library/windows/apps/br242878) scenario, as it will decrease your startup time, but could also decrease your panning performance depending on what you're creating.  If you are looking to increase panning performance, please refer to the [{x:Bind} markup extension](x-bind-markup-extension.md) and [x:Phase attribute](x-phase-attribute.md) documentation.

If the [x:Phase attribute](x-phase-attribute.md) is used in conjunction with **x:DeferLoadStrategy** then, when an element or an element tree is realized, the bindings will be applied up to and including the current phase. The phase specified for **x:Phase** will not affect or control the deferral of the element. When a list item is recycled as part of panning, realized elements will behave in the same way as other active elements, and compiled bindings (**{x:Bind}** bindings) will be processed using the same rules, including phasing.

A general guideline is to measure your application before and after to make sure you are getting the performance that you want.

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
    this.FindName("DeferredGrid"); // This will realize the deferred grid
}
```