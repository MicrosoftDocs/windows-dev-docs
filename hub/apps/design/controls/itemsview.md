---
description: Displays items in a collection, such as photos in an album or items in a product catalog.
title: Guidelines for items view controls
label: Items view
template: detail.hbs
ms.date: 10/10/2023
ms.topic: article
doc-status: Published
ms.localizationpriority: medium
---
# Items view

Use an items view to display a collection of data items, such as photos in an album or items in a product catalog.

The items view is similar to the list view and grid view controls, and you can use it in most cases where you would use those controls. One advantage of the items view is its ability to switch the layout on the fly while preserving item selection.

The items view control is built using the [ItemsRepeater](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsrepeater), [ScrollView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.scrollview), [ItemContainer](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemcontainer), and [ItemCollectionTransitionProvider](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemcollectiontransitionprovider) components, so it offers the unique ability to plug in custom [Layout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.layout) or [ItemCollectionTransitionProvider](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemcollectiontransitionprovider) implementations. The item view's inner ScrollView control allows scrolling and zooming of the items. It also offers features unavailable in the [ScrollViewer](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.scrollviewer) control used by the list view and grid view, like the ability to control the animation during programmatic scrolls.

Like the list view and grid view controls, the items view can use UI and data virtualization; handle keyboard, mouse, pen, and touch input; and has built-in accessibility support.

## Is this the right control?

Use an items view to:

- Display a collection where all of the items should have the same visual and interaction behavior.
- Display a content collection with the ability to switch between list, grid, and custom layouts.
- Accommodate a variety of use cases, including the following common ones:
  - Storefront-type user interface (i.e. browsing apps, songs, products)
  - Interactive photo libraries
  - Contacts list

## Create an items view

> [!div class="checklist"]
>
> - Important APIs: [ItemsView class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview), [ItemsSource property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.itemssource), [ItemTemplate property](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.itemtemplate), [LinedFlowLayout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.linedflowlayout), [StackLayout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.stacklayout), [UniformGridLayout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.uniformgridlayout)

> [!div class="nextstepaction"]
> [Open the WinUI 3 Gallery app and see the ItemsView in action](winui3gallery:/item/ItemsView).

[!INCLUDE [winui-3-gallery](../../../includes/winui-3-gallery.md)]

An [ItemsView](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview) can show a collection of items of any type. To populate the view, set the [ItemsSource](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemscontrol.itemssource) property to a data source.

> [!NOTE]
> Unlike other collection controls (those that derive from [ItemsControl](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemscontrol)), ItemsView does not have an [Items](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemscontrol.items) property that you can add data items to directly.

## Set the items source

You typically use an items view to display data from a source such as a database or the internet. To populate an items view from a data source, you set its [ItemsSource](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.itemssource) property to a collection of data items.

### Set ItemsSource in code

Here, the [ItemsSource](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.itemssource) is set in code directly to an instance of a collection.

```csharp
 // Data source.
 List<String> itemsList = new List<string>();
 itemsList.Add("Item 1");
 itemsList.Add("Item 2");

 // Create a new ItemsView and set the data source.
 ItemsView itemsView1 = new ItemsView();
 itemsView1.ItemsSource = itemsList;

 // Add the ItemsView to a parent container in the visual tree.
 rootGrid.Children.Add(itemsView1);
```

### Bind ItemsSource in XAML

You can also bind the [ItemsSource](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.itemssource) property to a collection in XAML. For more info, see [Data binding with XAML](/windows/uwp/data-binding/data-binding-quickstart).

> [!IMPORTANT]
> When you use the [x:Bind markup extension](/windows/uwp/xaml-platform/x-bind-markup-extension) in a DataTemplate, you have to specify the data type (`x:DataType`) on the data template.

Here, the ItemsSource is bound to a collection of custom data objects (of type `Photo`).

```xaml
<ItemsView ItemsSource="{x:Bind Photos}">

</ItemsView>
```

```csharp
public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
        Photos = new ObservableCollection<Photo>();
        PopulatePhotos();
    }

    public ObservableCollection<Photo> Photos
    {
        get; private set;
    }


    private void PopulatePhotos()
    {
        // Populate the Photos collection. 
    }
}

public class Photo
{
    public BitmapImage PhotoBitmapImage { get; set; }
    public string Title { get; set; }
    public int Likes { get; set; }
}
```

## Specify the look of the items

By default, a data item is displayed in the items view as the string representation of the data object it's bound to. You typically want to show a more rich presentation of your data. To specify exactly how items in the items view are displayed, you create a [DataTemplate](/windows/windows-app-sdk/api/winrt/microsoft.UI.Xaml.DataTemplate). The XAML in the DataTemplate defines the layout and appearance of controls used to display an individual item. The controls in the layout can be bound to properties of a data object, or have static content defined inline. The DataTemplate is assigned to the [ItemTemplate](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemscontrol.itemtemplate) property of the ItemsView control.

> [!IMPORTANT]
> The root element of the DataTemplate must be an [ItemContainer](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemcontainer); otherwise, an exception is thrown. ItemContainer is an independent primitive control that is used by ItemsView to show the selection states and other visualizations of an individual item in the item collection.

In this example, the DataTemplate is defined in the `Page.Resources` [ResourceDictionary](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.resourcedictionary). It includes an [Image](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.image) control to show the picture and an overlay that contains the image title and number of likes it's received.

```xaml
 <Page.Resources>
     <DataTemplate x:Key="PhotoItemTemplate" x:DataType="local:Photo">
         <ItemContainer AutomationProperties.Name="{x:Bind Title}">
             <Grid>
                 <Image Source="{x:Bind PhotoBitmapImage, Mode=OneWay}" 
                        Stretch="UniformToFill" MinWidth="70"
                        HorizontalAlignment="Center" VerticalAlignment="Center"/>
                 <StackPanel Orientation="Vertical" Height="40" Opacity=".75"
                             VerticalAlignment="Bottom" Padding="5,1,5,1" 
                             Background="{ThemeResource SystemControlBackgroundBaseMediumBrush}">
                     <TextBlock Text="{x:Bind Title}" 
                                Foreground="{ThemeResource SystemControlForegroundAltHighBrush}"/>
                     <StackPanel Orientation="Horizontal">
                         <TextBlock Text="{x:Bind Likes}"
                                    Style="{ThemeResource CaptionTextBlockStyle}"
                                    Foreground="{ThemeResource SystemControlForegroundAltHighBrush}"/>
                         <TextBlock Text=" Likes"
                                    Style="{ThemeResource CaptionTextBlockStyle}"
                                    Foreground="{ThemeResource SystemControlForegroundAltHighBrush}"/>
                     </StackPanel>
                 </StackPanel>
             </Grid>
         </ItemContainer>
     </DataTemplate>
 </Page.Resources>

 <Grid x:Name="rootGrid">
     <ItemsView ItemsSource="{x:Bind Photos}" Width="480" Height="400" 
                ItemTemplate="{StaticResource PhotoItemTemplate}">
     </ItemsView>
 </Grid>
```

Here's what the item layout defined by the data template looks like.

:::image type="content" source="images/itemsview/itemsview-item-template.png" alt-text="A photo of bicycles with an overlay that contains the image title and number of likes it's received.":::

## Change the layout of items

When you add items to an ItemsView control, it automatically wraps each item in an [ItemContainer](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemcontainer) and then lays out all of the item containers. How these item containers are laid out depends on the [Layout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.layout) property of the control.

You can modify the layout of items by adjusting the properties on the current layout, or you can completely replace the current layout with a different layout. You can use one of the layouts described next, or you can derive a custom layout from the [Layout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.layout) class.

### StackLayout

By default, ItemsView uses a [StackLayout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.stacklayout), which produces a vertical list, shown here with default property settings and a simple image template.

:::image type="content" source="images/itemsview/itemsview-stack-default.png" alt-text="A collection of photos shown in a vertical list.":::

This XAML sets the spacing between items in the StackLayout to 5px.

```xaml
<ItemsView ItemsSource="{x:Bind Photos}" Width="480" Height="400" 
           ItemTemplate="{StaticResource PhotoItemTemplate}">
    <ItemsView.Layout>
        <StackLayout Spacing="5"/>
    </ItemsView.Layout>
</ItemsView>
```

StackLayout provides properties to control:

- whether the layout is vertical or horizontal ([Orientation](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.stacklayout.orientation))
- the spacing of items ([Spacing](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.stacklayout.spacing))

You can use the layout in combination with the ItemTemplate to give your collection a variety of looks to suit your needs. For example, the [WinUI Gallery sample](https://github.com/Microsoft/WinUI-Gallery) changes the ItemTemplate used with the StackLayout to look like this.

:::image type="content" source="images/itemsview/itemsview-stack.png" alt-text="A collection of small photos shown in a vertical list with several rows of text below each photo.":::

### LinedFlowLayout

The [LinedFlowLayout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.linedflowlayout) positions elements sequentially from left to right, then top to bottom, in a wrapping layout. Use this layout to display an item collection where the items have a fixed height but a variable width. We recommend it for image based collections. This layout also has built-in animations that play when the collection has items added or removed, and when the view is resized.

Here's an items view that shows a collection of photos in a lined flow layout.

```xaml
<ItemsView Width="500" Height="400" HorizontalAlignment="Left"
           ItemTemplate="{StaticResource LinedFlowLayoutItemTemplate}">
    <ItemsView.Layout>
        <LinedFlowLayout ItemsStretch="Fill" 
                         LineHeight="160" 
                         LineSpacing="5"
                         MinItemSpacing="5"/>
    </ItemsView.Layout>
</ItemsView>
```

:::image type="content" source="images/itemsview/itemsview-lined-flow.png" alt-text="A collection of photos shown in a lined flow layout where each item is the same height, but the width varies based on the original size of the aspect ratio of the photo.":::

LinedFlowLayout provides properties to control:

- the size and spacing of items ([LineHeight](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.linedflowlayout.lineheight), [LineSpacing](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.linedflowlayout.linespacing), [MinItemSpacing](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.linedflowlayout.minitemspacing))
- the arrangement of items ([ItemsJustification](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.linedflowlayout.itemsjustification), [ItemsStretch](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.linedflowlayout.itemsstretch))

### UniformGridLayout

The [UniformGridLayout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.uniformgridlayout) positions elements sequentially from left to right or top to bottom (depending on the [Orientation](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.uniformgridlayout.orientation)) in a wrapping layout. Each item is the same size.

Here's an items view that shows a collection of photos in a uniform grid layout.

```xaml
<ItemsView ItemsSource="{x:Bind Photos}" Width="480" Height="400"
           ItemTemplate="{StaticResource PhotoItemTemplate}">
    <ItemsView.Layout>
        <UniformGridLayout MaximumRowsOrColumns="3" 
                           MinRowSpacing="5" 
                           MinColumnSpacing="5"/>
    </ItemsView.Layout>
</ItemsView>
```

:::image type="content" source="images/itemsview/itemsview-uniform-grid.png" alt-text="A collection of photos shown in a uniform grid layout where each item is the same size.":::

UniformGridLayout provides properties to control:

- whether the layout is row or column based ([Orientation](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.uniformgridlayout.orientation))
- the number of rows or columns ([MaximumRowsOrColumns](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.uniformgridlayout.maximumrowsorcolumns))
- the size and spacing of items ([MinItemHeight](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.uniformgridlayout.minitemheight), [MinItemWidth](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.uniformgridlayout.minitemwidth), [MinColumnSpacing](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.uniformgridlayout.mincolumnspacing), [MinRowSpacing](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.uniformgridlayout.minrowspacing))
- the arrangement of items ([ItemsJustification](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.uniformgridlayout.itemsjustification), [ItemsStretch](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.uniformgridlayout.itemsstretch))

> [!TIP]
> Use the interactive demo in the [WinUI 3 Gallery app](winui3gallery:/item/ItemsView) to see the effect of these properties in real time.

## Item selection and interaction

You can choose from a variety of ways to let users interact with an items view. By default, users can select a single item. You can change the [SelectionMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.selectionmode) property to enable multi-selection or to disable selection. You can set the [IsItemInvokedEnabled](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.isiteminvokedenabled) property so that users click an item to invoke an action instead of selecting the item.

This table shows the ways a user can interact with an items view, and how you can respond to the interaction.

| To enable this interaction: | Use these settings: | Handle this event: | Use this property to get the selected item: |
| --- |--- |--- |--- |
| No interaction | [SelectionMode](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.selectionmode)="None"<br>[IsItemInvokedEnabled](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.isiteminvokedenabled)="False" | N/A | N/A |
| Single selection | SelectionMode="Single"<br>IsItemInvokedEnabled="False" | [SelectionChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.selectionchanged) | [SelectedItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.selecteditem) |
| Multiple selection | SelectionMode="Multiple"<br>IsItemInvokedEnabled="False" | [SelectionChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.selectionchanged) | [SelectedItems](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.selecteditems) |
| Extended selection | SelectionMode="Extended"<br>IsItemInvokedEnabled="False" | [SelectionChanged](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.selectionchanged) | [SelectedItems](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.selecteditems) |
| Click | SelectionMode="None"<br>IsItemInvokedEnabled="True" | [ItemInvoked](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.iteminvoked) | N/A |

> [!NOTE]
> You can enable IsItemInvokedEnabled to raise an ItemInvoked event while SelectionMode is also set to `Single`, `Multiple`, or `Extended`. If you do this, the ItemInvoked event is raised first, and then the SelectionChanged event is raised. In some cases (for example, if you go to another page in the ItemInvoked event handler), the SelectionChanged event isn't raised and the item isn't selected.

You can set these properties in XAML or in code, as shown here:

```xaml
<ItemsView x:Name="myItemsView" SelectionMode="None" IsItemInvokedEnabled="True"/>
```

```csharp
myItemsView.SelectionMode = ItemsViewSelectionMode.None;
myItemsView.IsItemInvokedEnabled = true;
```

### Read-only

You can set the SelectionMode property to [ItemsViewSelectionMode.None](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsviewselectionmode) to disable item selection. This puts the control in read-only mode, so that it's used for displaying data, but not for interacting with it. That is, the item selection is disabled, but the control itself is not.

> [!NOTE]
> Items can still be selected and deselected programmatically, but not through user interaction.

### Single selection

This table describes keyboard, mouse, and touch interactions when SelectionMode is set to `Single`.

| Modifier key | Interaction |
| -------------|------------ |
| None | <li>Users can select a single item by using the space bar, mouse clicks, or taps. |
| Ctrl | <li>Users can deselect a single item by using the space bar, mouse clicks, or taps.<li>By using the arrow keys, users can move the focus independent of the selection. |

When SelectionMode is set to `Single`, you can get the selected data item from the [SelectedItem](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.selecteditem) property. If no item is selected, SelectedItem is `null`.

If you try to set an item that's not in the items collection as SelectedItem, the operation is ignored and SelectedItem is `null`.

The default selection indicator for `Single` selection looks like this.

:::image type="content" source="images/itemsview/itemsview-selection-single.png" alt-text="An image in an items view with a blue border around it to indicate selection next to an unselected image without a border.":::

### Multiple selection

This table describes the keyboard, mouse, and touch interactions when SelectionMode is set to `Multiple`.

| Modifier key | Interaction |
| -------------|------------ |
| None | <li>Users can select multiple items by using the space bar, mouse clicks, or taps to select the focused item.<li>By using the arrow keys, users can move the focus independent of their selection. |
| Shift | <li>Users can select multiple contiguous items by clicking or tapping the first item in the selection and then clicking or tapping the last item in the selection.<li>By using the arrow keys, users can select contiguous items starting with the item that's selected when they select the Shift key. |

The default selection indicator for `Multiple` selection looks like this.

:::image type="content" source="images/itemsview/itemsview-selection-multiple.png" alt-text="An image in an items view with a blue border around it and a checked check box overlayed to indicate selection next to an unselected image with no border and an unchecked check box.":::

### Extended selection

This table describes the keyboard, mouse, and touch interactions when SelectionMode is set to `Extended`.

| Modifier key | Interaction |
-------------|------------ |
| None | <li>The behavior is the same as `Single` selection.
Ctrl | <li>Users can select multiple items by using the space bar, mouse clicks, or taps to select the focused item.<li>By using the arrow keys, users can move the focus independent of the selection. |
| Shift | <li>Users can select multiple contiguous items by clicking or tapping the first item in the selection and then clicking or tapping the last item in the selection.<li>By using the arrow keys, users can select contiguous items starting with the item that's selected when they select the Shift key. |

When SelectionMode is set to `Multiple` or `Extended`, you can get the selected data items from the [SelectedItems](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.selecteditems) property.

The SelectedItem and SelectedItems properties are synchronized. For example, if you set SelectedItem to `null`, SelectedItems is empty. In multi-select mode, SelectedItem contains the item that was selected first.

The default selection indicator for `Extended` selection is the same as for `Single` selection and looks like this.

:::image type="content" source="images/itemsview/itemsview-selection-extended.png" alt-text="Two images in an items view, each with a blue border around it to indicate selection.":::

### Manage item selection programmatically

> [!NOTE]
> These selection methods ignore the SelectionMode property and have an effect even when SelectionMode is `Single` or `None`.

Sometimes, you might need to manipulate the ItemsView item selection programmatically. For example, you might display a Select all button to let users select all items in a list. In this case, it's usually not very efficient to add and remove items from the SelectedItems collection one by one. It's more efficient to use the [Select](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.select), [SelectAll](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.selectall), [Deselect](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.deselect), and [InvertSelection](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview.invertselection) methods to modify the selection than to use the SelectedItems property.

> [!TIP]
> You can select all items in a collection by calling the SelectAll method. There is no corresponding method to deselect all items. However, you can deselect all items by calling SelectAll followed immediately by InvertSelection.

## Get the sample code

- [WinUI Gallery sample](https://github.com/Microsoft/WinUI-Gallery) - See all the XAML controls in an interactive format.

## Related articles

- [Guidelines for lists](lists.md)
- [ItemsView class](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.itemsview)
- [StackLayout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.stacklayout)
- [UniformGridLayout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.uniformgridlayout)
- [LinedFlowLayout](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.linedflowlayout)