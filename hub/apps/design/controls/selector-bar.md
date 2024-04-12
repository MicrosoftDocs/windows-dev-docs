---
description: The SelectorBar control enables switching between a small set of content sections.
title: SelectorBar
template: detail.hbs
ms.date: 03/29/2024
ms.topic: article
doc-status: Published
ms.localizationpriority: medium
---

# Selector bar

A selector bar lets a user switch between a small number of different sets or views of data. One item at a time can be selected.

:::image type="content" source="images/selector-bar.png" alt-text="Selector bar with nodes Recent, Shared, Favorites. The Recent node is selected, as indicated by a blue line under the text.":::

When a user selects an item in the selector bar, you typically change the view by either:

- navigating between different pages in your app.
- changing the data shown in a collection control.

The selector bar is a light-weight control that supports an icon and text. It's intended to present a limited number of options so it does not rearrange items to adapt to different window sizes.

## Is this the right control?

Use a SelectorBar when you want to let a user navigate between a limited number of views or pages and only one option can be selected at once.

Some examples include:

- Switching between "Recent," "Shared," and "Favorites" pages, where each page displays a unique list of content.
- Switching between "All," "Unread," "Flagged," and "Urgent" views, where each view displays a uniquely filtered list of email items.

### When should a different control be used?

There are some scenarios where another control may be more appropriate to use.

- Use [NavigationView](navigationview.md) when you require consistent, top-level app navigation that adapts to different window sizes.
- Use [TabView](tab-view.md) when the user should be able to open, close, rearrange, or tear off new views of the content.
- Use [PipsPager](pipspager.md) when you need regular pagination of a single data view.
- Use [RadioButtons](radio-button.md) when an option is not selected by default, and context is unrelated to page navigation.

## UWP and WinUI 2

> [!IMPORTANT]
> The SelectorBar control is not available for UWP and WinUI 2. For alternatives, see [NavigationView](navigationview.md) or [TabView](tab-view.md).

## Create a SelectorBar control

> [!div class="checklist"]
>
> - **Important APIs**: [SelectorBar class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.selectorbar), [Items property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.selectorbar.items), [SelectionChanged event](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.selectorbar.selectionchanged),  [SelectorBarItem class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.selectorbaritem)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the SelectorBar in action](winui3gallery:/item/SelectorBar).

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

This XAML creates a basic SelectorBar control with 3 sections of content.

```xaml
<SelectorBar x:Name="SelectorBar">
    <SelectorBarItem x:Name="SelectorBarItemRecent" 
                     Text="Recent" Icon="Clock"/>
    <SelectorBarItem x:Name="SelectorBarItemShared" 
                     Text="Shared" Icon="Share"/>
    <SelectorBarItem x:Name="SelectorBarItemFavorites" 
                     Text="Favorites" Icon="Favorite"/>
</SelectorBar>
```

This shows how to add a SelectorBarItem in code.

```csharp
SelectorBarItem newItem = new SelectorBarItem()
{
    Text = "New Item",
    Icon = new SymbolIcon(Symbol.Add)
};
selectorBar.Items.Add(newItem);
```

### SelectorBar items

You populate the SelectorBar [Items](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.selectorbar.items) collection with [SelectorBarItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.selectorbaritem) objects. You can do this directly in XAML or in code. Because it's intended to display a limited number of options, SelectorBar does not have an `ItemsSource` property for binding to an external collection of items.

### Item content

The SelectorBarItem class provides [Text](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.selectorbaritem.text) and [Icon](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.selectorbaritem.icon) properties that you use to set the content of your selector bar. You can set one or both properties; however, we recommend that you set the `Text` property to make the item more meaningful.

The `Icon` property takes an [IconElement](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.iconelement), so you can use any of these derived icon types:

- [AnimatedIcon](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.animatedicon)
- [BitmapIcon](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.bitmapicon)
- [FontIcon](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.fonticon)
- [IconSourceElement](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.iconsourceelement)
- [ImageIcon](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.imageicon)
- [PathIcon](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.pathicon)
- [SymbolIcon](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.symbolicon)

> [!NOTE]
> SelectorBarItem inherits the [Child](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemcontainer.child) property from ItemContainer. You can use this property to set the content, but we don't recommend this. Content set this way will not get the styling and visual states provided by the SelectorBarItem control template.

### Item selection

You can use the [SelectedItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.selectorbar.selecteditem) property to get or set the SelectorBar's active item. This is synchronized with the SelectorBarItem's [IsSelected](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemcontainer.isselected) property. If you set either property, the other is updated automatically.

Whenever the SelectorBar gets focus and `SelectedItem` is `null`, `SelectedItem` is automatically set to the first focusable instance in the [Items](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.selectorbar.items) collection, if any exists.

Whenever the selected item is removed from the `Items` collection, the `SelectedItem` property is set to `null`. If `SelectedItem` is set to `null` while the SelectorBar has focus, SelectorBar will have no item selected but keeps focus.

Setting `SelectedItem` to an element that is not currently in the `Items` collection throws an exception.

There is no `SelectedIndex` property, but you can get the index of the `SelectedItem` like this:

```csharp
int currentSelectedIndex = 
    selectorBar.Items.IndexOf(selectorBar.SelectedItem);
```

### Selection changed

Handle the [SelectionChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.selectorbar.selectionchanged) event to respond to the users selection and change what is shown to the user. The `SelectionChanged` event is raised when an item is selected in any of these ways:

- UI Automation
- Tab focus (and a new item is selected)
- Left and right navigation within the SelectorBar
- Tapped event through mouse or touch
- Programmatic selection (through either the [SelectorBar.SelectedItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.selectorbar.selecteditem) property or SelectorBarItem's [IsSelected](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemcontainer.isselected) property).

When a user selects an item, you typically change the view by either navigating between different pages in your app or changing the data shown in a collection control. Examples of both are shown here.

### Navigate with transition animations

> [!TIP]
> You can find these examples in the SelectorBar page of the [WinUI Gallery app](#get-the-sample-code). Use the WinUI Gallery app to run and view the full code.

This example demonstrates handling the [SelectionChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.selectorbar.selectionchanged) event to navigate between different pages. The navigation uses the [SlideNavigationTransitionEffect](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.media.animation.slidenavigationtransitioneffect) to slide the pages in from the left or right, as appropriate.

```xaml
<SelectorBar x:Name="SelectorBar2" 
             SelectionChanged="SelectorBar2_SelectionChanged">
    <SelectorBarItem x:Name="SelectorBarItemPage1" Text="Page1" 
                     IsSelected="True" />
    <SelectorBarItem x:Name="SelectorBarItemPage2" Text="Page2" />
    <SelectorBarItem x:Name="SelectorBarItemPage3" Text="Page3" />
    <SelectorBarItem x:Name="SelectorBarItemPage4" Text="Page4" />
    <SelectorBarItem x:Name="SelectorBarItemPage5" Text="Page5" />
</SelectorBar>

<Frame x:Name="ContentFrame" IsNavigationStackEnabled="False" />
```

```csharp
int previousSelectedIndex = 0;

private void SelectorBar2_SelectionChanged
             (SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
{
    SelectorBarItem selectedItem = sender.SelectedItem;
    int currentSelectedIndex = sender.Items.IndexOf(selectedItem);
    System.Type pageType;

    switch (currentSelectedIndex)
    {
        case 0:
            pageType = typeof(SamplePage1);
            break;
        case 1:
            pageType = typeof(SamplePage2);
            break;
        case 2:
            pageType = typeof(SamplePage3);
            break;
        case 3:
            pageType = typeof(SamplePage4);
            break;
        default:
            pageType = typeof(SamplePage5);
            break;
    }

    var slideNavigationTransitionEffect = 
            currentSelectedIndex - previousSelectedIndex > 0 ? 
                SlideNavigationTransitionEffect.FromRight : 
                SlideNavigationTransitionEffect.FromLeft;

    ContentFrame.Navigate(pageType, null, new SlideNavigationTransitionInfo() 
                            { Effect = slideNavigationTransitionEffect });

    previousSelectedIndex = currentSelectedIndex;
}
```

### Display different collections in an ItemsView

This example shows how to change the data source of an [ItemsView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview) when the user selects an option in the SelectorBar.

```xaml
<SelectorBar x:Name="SelectorBar3" 
             SelectionChanged="SelectorBar3_SelectionChanged">
    <SelectorBarItem x:Name="SelectorBarItemPink" Text="Pink"
                     IsSelected="True"/>
    <SelectorBarItem x:Name="SelectorBarItemPlum" Text="Plum"/>
    <SelectorBarItem x:Name="SelectorBarItemPowderBlue" Text="PowderBlue"/>
</SelectorBar>

<ItemsView x:Name="ItemsView3" 
           ItemTemplate="{StaticResource ColorsTemplate}"/>
    <ItemsView.Layout>
        <UniformGridLayout/>
    </ItemsView.Layout>
</ItemsView/>
```

```csharp
private void SelectorBar3_SelectionChanged
             (SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
{
    if (sender.SelectedItem == SelectorBarItemPink)
    {
        ItemsView3.ItemsSource = PinkColorCollection;
    }
    else if (sender.SelectedItem == SelectorBarItemPlum)
    {
        ItemsView3.ItemsSource = PlumColorCollection;
    }
    else
    {
        ItemsView3.ItemsSource = PowderBlueColorCollection;
    }
}
```


## Get the sample code

- [WinUI Gallery sample](https://github.com/Microsoft/WinUI-Gallery) - See all the XAML controls in an interactive format.

## Related topics

- [SelectorBar class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.selectorbar)
- [Navigation design basics](../basics/navigation-basics.md)
