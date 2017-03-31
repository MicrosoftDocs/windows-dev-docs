---
author: jwmsft
title: xLoad attribute
description: xLoad enables dynamic creation/destruction  of an element and its children, decreasing startup time and memory usage. 
ms.assetid: E763898E-13FF-4412-B502-B54DBFE2D4E4
---

# x:Load attribute

**x:Load** is a feature that can be used to optimize the performance of the startup, tree creation and memory usage scenarios of a XAML app. Using **x:Load** has a similar visual effect to **Visibility**, except when the element is not loaded, its memory is released and internally a small placeholder is used to mark its place in the UI tree. The UI element attributed with x:Load can be loaded and unloaded via code, or using an x:Bind expression. This is useful to reduce the costs of elements that are not often or conditionally needed. When x:Load is used on a container such as Grid or StackPanel, then the container and all of its children will be loaded or unloaded as a group. 

However the book-keeping for x:Load adds about 600 bytes to the memory usage for each element attributed with x:Load, to account for the placeholder. Therefore it's possible to overuse this attribute to the extent that your performance actually decreases, so only use it on elements that need to be hidden. If x:Load is used on a container, then the overhead is only paid for the element with the x:Load attribute.

## XAML attribute usage

``` syntax
<object x:Load="True" .../>
<object x:Load="False" .../>
<object x:Load="{x:Bind Path.to.a.boolean, Mode=OneWay}" .../>
```

## Loading Elements

There are several different ways to load the elements:

-   Use an x:Bind expression to specify the load state. The expression should return true to load and false to unload the element.
-   Call [**FindName**](https://msdn.microsoft.com/library/windows/apps/br208715) with the name that was defined on the element.
-   Call [**GetTemplateChild**](https://msdn.microsoft.com/library/windows/apps/br209416) with the name that was defined on the element.
-   In a [**VisualState**](https://msdn.microsoft.com/library/windows/apps/br209007), use a [**Setter**](https://msdn.microsoft.com/library/windows/apps/br208817) or **Storyboard** animation that is targeting the x:Load element.
-   Target the unloaded element in any **Storyboard**.
-   NOTE: Once the instantiation of an element has started, it is created on the UI thread, so it could cause the UI to stutter if too much is created at once.

Once a deferred element is created by any of the methods listed above, several things will happen:

-   The [**Loaded**](https://msdn.microsoft.com/library/windows/apps/br208723) event on the element will get raised.
-   The field for x:Name will be set.
-   Any x:Bind bindings on the element will get evaluated.
-   If the application has registered to receive property change notifications on the property containing the deferred element(s), the notification will be raised.

## Unloading elements

To unload an element:
-   Use an x:Bind expression to specify the load state. The expression should return true to load and false to unload the element.
-   If in a Page or UserControl, call **UnloadObject** passing in the object reference
-   Call **Windows.UI.Xaml.Markup.XamlMarkupHelper.UnloadObject** passing in the object reference

When an object is unloaded, it will be replaced in the tree with a placeholder. The object instance will remain in memory until all references have been released. The UnloadObject API on a Page/UserControl is designed to release the references held by codegen for x:Name and x:Bind. If you hold additional references in app code they will also need to be released.

When an element is unloaded, all state associated with the element will be discarded, so if using x:Load as an optimized version of Visibility, then ensure all state is applied via bindings, or is re-applied by code when the Loaded event is fired.

## Restrictions

The restrictions for using **x:Load** are:

-   **x:Load** requires an [x:Name](x-name-attribute.md) defined.
-   Only elements deriving from [**UIElement**](https://msdn.microsoft.com/library/windows/apps/br208911), or [**FlyoutBase**](https://msdn.microsoft.com/library/windows/apps/dn279249) can be marked with x:Load, 
-   Root elements can not use x:Load in a [**Page**](https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.controls.page), a [**UserControl**](https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.controls.usercontrol), nor a [**DataTemplate**](https://msdn.microsoft.com/library/windows/apps/br242348).
-   Elements in a [**ResourceDictionary**](https://msdn.microsoft.com/library/windows/apps/br208794) cannot use x:Load.
-   Does not work with loose XAML loaded with [**XamlReader.Load**](https://msdn.microsoft.com/library/windows/apps/br228048).
-   Moving a parent element will clear out any elements that have not been loaded.

## Remarks

You can nest x:Load elements, however they have to be realized from the outer-most element in.  If you try to realize a child element before the parent has been realized, an exception will be raised.

In general, the recommendation is to defer loading things that are not viewable in the first frame.  A good guideline for finding candidates to be deferred is to look for elements that are being created with collapsed [**Visibility**](https://msdn.microsoft.com/library/windows/apps/br208992).  Also incidental UI (that is, UI that is triggered by user interaction) is a good place to look for deferring elements.  

Be wary of deferring elements in a [**ListView**](https://msdn.microsoft.com/library/windows/apps/br242878) scenario, as it will decrease your startup time, but could also decrease your panning performance depending on what you're creating.  If you are looking to increase panning performance, please refer to the [{x:Bind} markup extension](x-bind-markup-extension.md) and [x:Phase attribute](x-phase-attribute.md) documentation.

If the [x:Phase attribute](x-phase-attribute.md) is used in conjunction with **x:Load** then, when an element or an element tree is realized, the bindings will be applied up to and including the current phase. The phase specified for **x:Phase** will not affect or control the loading state of the element. When a list item is recycled as part of panning, realized elements will behave in the same way as other active elements, and compiled bindings (**{x:Bind}** bindings) will be processed using the same rules, including phasing.

A general guideline is to measure your application before and after to make sure you are getting the performance that you want.

## Example

```xml
<Grid x:Name="DeferredGrid" x:Load="False">
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
    <Rectangle Height="100" Width="100" Fill="#FFBB00" Grid.Row="1" Grid.Column="1" Margin="4,4,0,0" x:Load="{x:Bind (x:Boolean)Choice1.IsChecked, Mode=OneWay}" x:Name="one" />
    <Rectangle Height="100" Width="100" Fill="#AABBCC" Grid.Row="1" Grid.Column="1" Margin="4,4,0,0" x:Load="{x:Bind Not(Choice1.IsChecked), Mode=OneWay}" x:Name="two"/>
</Grid>
<Button x:Name="RealizeElements" Content="Realize Elements" Click="RealizeElements_Click"/>
<CheckBox x:Name="Choice1" Content="Swap Elements" />
```

```csharp
private void RealizeElements_Click(object sender, RoutedEventArgs e)
{
    this.FindName("DeferredGrid"); // This will realize the deferred grid
}

private bool Not(bool? value) { return !(value==true); }
```

