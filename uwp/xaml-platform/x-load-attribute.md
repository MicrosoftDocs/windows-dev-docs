---
title: xLoad attribute
description: xLoad enables dynamic creation and destruction  of an element and its children, decreasing startup time and memory usage.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# x:Load attribute

You can use **x:Load** to optimize the startup, visual tree creation, and memory usage of your XAML app. Using **x:Load** has a similar visual effect to **Visibility**, except that when the element is not loaded, its memory is released and internally a small placeholder is used to mark its place in the visual tree.

The UI element attributed with x:Load can be loaded and unloaded via code, or using an [x:Bind](x-bind-markup-extension.md) expression. This is useful to reduce the costs of elements that are shown infrequently or conditionally. When you use x:Load on a container such as Grid or StackPanel, the container and all of its children are loaded or unloaded as a group.

The tracking of deferred elements by the XAML framework adds about 600 bytes to the memory usage for each element attributed with x:Load, to account for the placeholder. Therefore, it's possible to overuse this attribute to the extent that your performance actually decreases. We recommend that you only use it on elements that need to be hidden. If you use x:Load on a container, then the overhead is paid only for the element with the x:Load attribute.

> [!IMPORTANT]
> The x:Load attribute is available starting in Windows 10, version 1703 (Creators Update). The min version targeted by your Visual Studio project must be *Windows 10 Creators Update (10.0, Build 15063)* in order to use x:Load.

## XAML attribute usage

``` syntax
<object x:Load="True" .../>
<object x:Load="False" .../>
<object x:Load="{x:Bind Path.to.a.boolean, Mode=OneWay}" .../>
```

## Loading Elements

There are several different ways to load the elements:

- Use an [x:Bind](x-bind-markup-extension.md) expression to specify the load state. The expression should return **true** to load and **false** to unload the element.
- Call [**FindName**](/uwp/api/windows.ui.xaml.frameworkelement.findname) with the name that you defined on the element.
- Call [**GetTemplateChild**](/uwp/api/windows.ui.xaml.controls.control.gettemplatechild) with the name that you defined on the element.
- In a [**VisualState**](/uwp/api/Windows.UI.Xaml.VisualState), use a [**Setter**](/uwp/api/Windows.UI.Xaml.Setter) or **Storyboard** animation that targets the x:Load element.
- Target the unloaded element in any **Storyboard**.

> NOTE: Once the instantiation of an element has started, it is created on the UI thread, so it could cause the UI to stutter if too much is created at once.

Once a deferred element is created in any of the ways listed previously, several things happen:

- The [**Loaded**](/uwp/api/windows.ui.xaml.frameworkelement.loaded) event on the element is raised.
- The field for x:Name is set.
- Any x:Bind bindings on the element are applied.
- If you have registered to receive property change notifications on the property containing the deferred element(s), the notification is raised.

## Unloading elements

To unload an element:

- Use an x:Bind expression to specify the load state. The expression should return **true** to load and **false** to unload the element.
- In a Page or UserControl, call **UnloadObject** and pass in the object reference
- Call **Windows.UI.Xaml.Markup.XamlMarkupHelper.UnloadObject** and pass in the object reference

When an object is unloaded, it will be replaced in the tree with a placeholder. The object instance will remain in memory until all references have been released. The UnloadObject API on a Page/UserControl is designed to release the references held by codegen for x:Name and x:Bind. If you hold additional references in app code they will also need to be released.

When an element is unloaded, all state associated with the element will be discarded, so if using x:Load as an optimized version of Visibility, then ensure all state is applied via bindings, or is re-applied by code when the Loaded event is fired.

## Restrictions

The restrictions for using **x:Load** are:

- You must define an [x:Name](x-name-attribute.md) for the element, as there needs to be a way to find the element later.
- You can only use x:Load on types that derive from [**UIElement**](/uwp/api/Windows.UI.Xaml.UIElement) or [**FlyoutBase**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.FlyoutBase).
- You cannot use x:Load on root elements in a [**Page**](/uwp/api/windows.ui.xaml.controls.page), a [**UserControl**](/uwp/api/windows.ui.xaml.controls.usercontrol), or a [**DataTemplate**](/uwp/api/Windows.UI.Xaml.DataTemplate).
- You cannot use x:Load on elements in a [**ResourceDictionary**](/uwp/api/Windows.UI.Xaml.ResourceDictionary).
- You cannot use x:Load on loose XAML loaded with [**XamlReader.Load**](/uwp/api/windows.ui.xaml.markup.xamlreader.load).
- Moving a parent element will clear out any elements that have not been loaded.

## Remarks

You can use x:Load on nested elements, however they have to be realized from the outer-most element in.  If you try to realize a child element before the parent has been realized, an exception is raised.

Typically, we recommend that you defer elements that are not viewable in the first frame. A good guideline for finding candidates to be deferred is to look for elements that are being created with collapsed [**Visibility**](/uwp/api/windows.ui.xaml.uielement.visibility). Also, UI that is triggered by user interaction is a good place to look for elements that you can defer.

Be wary of deferring elements in a [**ListView**](/uwp/api/Windows.UI.Xaml.Controls.ListView), as it will decrease your startup time, but could also decrease your panning performance depending on what you're creating. If you are looking to increase panning performance, see the [{x:Bind} markup extension](x-bind-markup-extension.md) and [x:Phase attribute](x-phase-attribute.md) documentation.

If the [x:Phase attribute](x-phase-attribute.md) is used in conjunction with **x:Load** then, when an element or an element tree is realized, the bindings are applied up to and including the current phase. The phase specified for **x:Phase** does affect or control the loading state of the element. When a list item is recycled as part of panning, realized elements will behave in the same way as other active elements, and compiled bindings (**{x:Bind}** bindings) are processed using the same rules, including phasing.

A general guideline is to measure the performance of your app before and after to make sure you are getting the performance that you want.

To minimize changes in behavior (aside from performance) when adding **x:Load** to an element,
**x:Bind** bindings are calculated at their normal times, as if no elements used **x:Load**.
For example, OneTime **x:Bind** bindings are calculated when the root element loads.
If the element is not realized at the time the **x:Bind** binding is calculated,
then the calculated value is saved and applied to the element when it loads.
This behavior may be surprising if you expected **x:Bind** bindings to be calculated when
the element is realized.

## Example

```xml
<StackPanel>
    <Grid x:Name="DeferredGrid" x:Load="False">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto" />
            <RowDefinition Height="Auto" />
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="Auto" />
            <ColumnDefinition Width="Auto" />
        </Grid.ColumnDefinitions>

        <Rectangle Height="100" Width="100" Fill="Orange" Margin="0,0,4,4"/>
        <Rectangle Height="100" Width="100" Fill="Green" Grid.Column="1" Margin="4,0,0,4"/>
        <Rectangle Height="100" Width="100" Fill="Blue" Grid.Row="1" Margin="0,4,4,0"/>
        <Rectangle Height="100" Width="100" Fill="Gold" Grid.Row="1" Grid.Column="1" Margin="4,4,0,0"
                   x:Name="one" x:Load="{x:Bind (x:Boolean)CheckBox1.IsChecked, Mode=OneWay}"/>
        <Rectangle Height="100" Width="100" Fill="Silver" Grid.Row="1" Grid.Column="1" Margin="4,4,0,0"
                   x:Name="two" x:Load="{x:Bind Not(CheckBox1.IsChecked), Mode=OneWay}"/>
    </Grid>

    <Button Content="Load elements" Click="LoadElements_Click"/>
    <Button Content="Unload elements" Click="UnloadElements_Click"/>
    <CheckBox x:Name="CheckBox1" Content="Swap Elements" />
</StackPanel>
```

```csharp
// This is used by the bindings between the rectangles and check box.
private bool Not(bool? value) { return !(value==true); }

private void LoadElements_Click(object sender, RoutedEventArgs e)
{
    // This will load the deferred grid, but not the nested
    // rectangles that have x:Load attributes.
    this.FindName("DeferredGrid"); 
}

private void UnloadElements_Click(object sender, RoutedEventArgs e)
{
     // This will unload the grid and all its child elements.
     this.UnloadObject(DeferredGrid);
}
```
