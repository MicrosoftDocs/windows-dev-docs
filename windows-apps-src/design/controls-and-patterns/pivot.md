---
Description: The Pivot control enables touch-swiping between a small set of content sections.
title: Pivot
template: detail.hbs
ms.date: 06/19/2018
ms.topic: article
keywords: windows 10, uwp
pm-contact: yulikl
design-contact: kimsea
dev-contact: llongley
doc-status: Published
ms.localizationpriority: medium
---
# Pivot

The [Pivot](/uwp/api/Windows.UI.Xaml.Controls.Pivot) control enables touch-swiping between a small set of content sections.

![Default focus underlines selected header](images/pivot_focus_selectedHeader.png)

**Get the Windows UI Library**

|  |  |
| - | - |
| ![WinUI logo](images/winui-logo-64x64.png) | Windows UI Library 2.2 or later includes a new template for this control that uses rounded corners. For more info, see [Corner radius](../style/rounded-corner.md). WinUI is a NuGet package that contains new controls and UI features for Windows apps. For more info, including installation instructions, see [Windows UI Library](/uwp/toolkits/winui/). |

> **Platform APIs**: [Pivot class](/uwp/api/Windows.UI.Xaml.Controls.Pivot), [NavigationView class](/uwp/api/Windows.UI.Xaml.Controls.NavigationView)

## Examples

<table>
<th align="left">XAML Controls Gallery<th>
<tr>
<td><img src="images/xaml-controls-gallery-app-icon-sm.png" alt="XAML controls gallery"></img></td>
<td>
    <p>If you have the <strong style="font-weight: semi-bold">XAML Controls Gallery</strong> app installed, click here to <a href="xamlcontrolsgallery:/item/Pivot">open the app and see the Pivot control in action</a>.</p>
    <ul>
    <li><a href="https://www.microsoft.com/store/productId/9MSVH128X2ZT">Get the XAML Controls Gallery app (Microsoft Store)</a></li>
    <li><a href="https://github.com/Microsoft/Xaml-Controls-Gallery">Get the source code (GitHub)</a></li>
    </ul>
</td>
</tr>
</table>

The Pivot control, just like [NavigationView](navigationview.md), underlines the selected item.

![Default focus underlines selected header](images/pivot_focus_selectedHeader.png)

## Is this the right control?

To achieve common top navigation and tabs patterns, we recommend using [NavigationView](navigationview.md), which automatically adapts to different screen sizes and allows for greater customization.

However, if your navigation requires touch-swiping, we recommend using Pivot.

The other key differences between the NavigationView and Pivot controls are the default overflow behavior and the navigation API:

- Pivot carousels overflow items, while NavigationView uses a menu dropdown overflow so that users can see all items.
- Pivot handles navigation between content sections, while NavigationView allows for more control over navigation behavior.

## Use NavigationView instead of Pivot

If your app's UI uses the Pivot control, then you can convert Pivot to NavigationView with the code below.

This XAML creates a NavigationView with 3 sections of content, like the example Pivot in [Create a pivot control](#create-a-pivot-control).

```xaml
<NavigationView x:Name="rootNavigationView" Header="Category Title"
 ItemInvoked="NavView_ItemInvoked">
    <NavigationView.MenuItems>
        <NavigationViewItem Content="Section 1" x:Name="Section1Content" />
        <NavigationViewItem Content="Section 2" x:Name="Section2Content" />
        <NavigationViewItem Content="Section 3" x:Name="Section3Content" />
    </NavigationView.MenuItems>
</NavigationView>

<Page x:Class="AppName.Section1Page">
    <TextBlock Text="Content of section 1."/>
</Page>

<Page x:Class="AppName.Section2Page">
    <TextBlock Text="Content of section 2."/>
</Page>

<Page x:Class="AppName.Section3Page">
    <TextBlock Text="Content of section 3."/>
</Page>
```

NavigationView provides more control over navigation customization and requires corresponding code-behind. To accompany the above XAML, use the following code-behind:

```csharp
private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
{
    FrameNavigationOptions navOptions = new FrameNavigationOptions();
    navOptions.TransitionInfoOverride = new SlideNavigationTransitionInfo() {
         SlideNavigationTransitionDirection=args.RecommendedNavigationTransitionInfo
    };

    navOptions.IsNavigationStackEnabled = False;

    switch (item.Name)
    {
        case "Section1Content":
            ContentFrame.NavigateToType(typeof(Section1Page), null, navOptions);
            break;

        case "Section2Content":
            ContentFrame.NavigateToType(typeof(Section2Page), null, navOptions);
            break;

        case "Section3Content":
            ContentFrame.NavigateToType(typeof(Section3Page), null, navOptions);
            break;
    }  
}
```

This code mimics the Pivot control's built-in navigation experience, minus the touch-swiping experience between content sections. However, as you can see, you could also customize several points, including the animated transition, navigation parameters, and stack capabilities.

## Create a pivot control

This code creates a basic Pivot control with 3 sections of content.

```xaml
<Pivot x:Name="rootPivot" Title="Category Title">
    <PivotItem Header="Section 1">
        <!--Pivot content goes here-->
        <TextBlock Text="Content of section 1."/>
    </PivotItem>
    <PivotItem Header="Section 2">
        <!--Pivot content goes here-->
        <TextBlock Text="Content of section 2."/>
    </PivotItem>
    <PivotItem Header="Section 3">
        <!--Pivot content goes here-->
        <TextBlock Text="Content of section 3."/>
    </PivotItem>
</Pivot>
```

### Pivot items

Pivot is an [ItemsControl](/uwp/api/Windows.UI.Xaml.Controls.ItemsControl), so it can contain a collection of items of any type. Any item you add to the Pivot that is not explicitly a [PivotItem](/uwp/api/Windows.UI.Xaml.Controls.PivotItem) is implicitly wrapped in a PivotItem. Because a Pivot is often used to navigate between pages of content, it's common to populate the [Items](/uwp/api/windows.ui.xaml.controls.itemscontrol.items) collection directly with XAML UI elements. Or, you can set the [ItemsSource](/uwp/api/windows.ui.xaml.controls.itemscontrol.itemssource) property to a data source. Items bound in the ItemsSource can be of any type, but if they aren't explicitly PivotItems, you must define an [ItemTemplate](/uwp/api/windows.ui.xaml.controls.itemscontrol.itemtemplate) and [HeaderTemplate](/uwp/api/windows.ui.xaml.controls.pivot.headertemplate) to specify how the items are displayed.

You can use the [SelectedItem](/uwp/api/windows.ui.xaml.controls.pivot.selecteditem) property to get or set the Pivot's active item. Use the [SelectedIndex](/uwp/api/windows.ui.xaml.controls.pivot.selectedindex) property to get or set the index of the active item.

### Pivot headers

You can use the [LeftHeader](/uwp/api/windows.ui.xaml.controls.pivot.leftheader) and [RightHeader](/uwp/api/windows.ui.xaml.controls.pivot.rightheader) properties to add other controls to the Pivot header.

For example, you can add a [CommandBar](./app-bars.md) in the Pivot's RightHeader.

```xaml
<Pivot>
    <Pivot.RightHeader>
        <CommandBar>
                <AppBarButton Icon="Add"/>
                <AppBarSeparator/>
                <AppBarButton Icon="Edit"/>
                <AppBarButton Icon="Delete"/>
                <AppBarSeparator/>
                <AppBarButton Icon="Save"/>
        </CommandBar>
    </Pivot.RightHeader>
</Pivot>
```

### Pivot interaction

The control features these touch gesture interactions:

- Tapping on a pivot item header navigates to that header's section content.
- Swiping left or right on a pivot item header navigates to the adjacent section.
- Swiping left or right on section content navigates to the adjacent section.

The control comes in two modes:

**Stationary**

- Pivots are stationary when all pivot headers fit within the allowed space.
- Tapping on a pivot label navigates to the corresponding page, though the pivot itself will not move. The active pivot is highlighted.

**Carousel**

- Pivots carousel when all pivot headers don't fit within the allowed space.
- Tapping a pivot label navigates to the corresponding page, and the active pivot label will carousel into the first position.
- Pivot items in a carousel loop from last to first pivot section.

> **Note** Pivot headers should not carousel in a [10ft environment](../devices/designing-for-tv.md). Set the [IsHeaderItemsCarouselEnabled](/uwp/api/Windows.UI.Xaml.Controls.Pivot.IsHeaderItemsCarouselEnabled) property to **false** if your app will run on Xbox.

## Recommendations

- Avoid using more than 5 headers when using carousel (round-trip) mode, as looping more than 5 can become confusing.

## Get the sample code

- [XAML Controls Gallery sample](https://github.com/Microsoft/Xaml-Controls-Gallery) - See all the XAML controls in an interactive format.

## Related topics

- [Pivot class](/uwp/api/Windows.UI.Xaml.Controls.Pivot)
- [Navigation design basics](../basics/navigation-basics.md)