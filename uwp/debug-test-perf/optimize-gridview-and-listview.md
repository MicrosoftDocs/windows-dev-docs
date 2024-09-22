---
ms.assetid: 26DF15E8-2C05-4174-A714-7DF2E8273D32
title: ListView and GridView UI optimization
description: Improve ListView and GridView performance and startup time through UI virtualization, element reduction, and progressive updating of items.
ms.date: 02/08/2017
ms.topic: article
keywords: windows 10, uwp
ms.localizationpriority: medium
---
# ListView and GridView UI optimization


**Note**  
For more details, see the //build/ session Dramatically Increase Performance when Users Interact with Large Amounts of Data in GridView and ListView.

Improve [**ListView**](/uwp/api/Windows.UI.Xaml.Controls.ListView) and [**GridView**](/uwp/api/Windows.UI.Xaml.Controls.GridView) performance and startup time through UI virtualization, element reduction, and progressive updating of items. For data virtualization techniques, see [ListView and GridView data virtualization](listview-and-gridview-data-optimization.md).

## Two key factors in collection performance

Manipulating collections is a common scenario. A photo viewer has collections of photos, a reader has collections of articles/books/stories, and a shopping app has collections of products. This topic shows what you can do to make your app efficient at manipulating collections.

There are two key factors in performance when it comes to collections: one is the time spent by the UI thread creating items; the other is the memory used by both the raw data set and the UI elements used to render that data.

For smooth panning/scrolling, it's vital that the UI thread do an efficient and smart job of instantiating, data-binding, and laying out items.

## UI virtualization

UI virtualization is the most important improvement you can make. This means that UI elements representing the items are created on demand. For an items control bound to a 1000-item collection, it would be a waste of resources to create the UI for all the items at the same time, because they can't all be displayed at the same time. [**ListView**](/uwp/api/Windows.UI.Xaml.Controls.ListView) and [**GridView**](/uwp/api/Windows.UI.Xaml.Controls.GridView) (and other standard [**ItemsControl**](/uwp/api/Windows.UI.Xaml.Controls.ItemsControl)-derived controls) perform UI virtualization for you. When items are close to being scrolled into view (a few pages away), the framework generates the UI for the items and caches them. When it's unlikely that the items will be shown again, the framework re-claims the memory.

If you provide a custom items panel template (see [**ItemsPanel**](/uwp/api/windows.ui.xaml.controls.itemscontrol.itemspanel)) then make sure you use a virtualizing panel such as [**ItemsWrapGrid**](/uwp/api/Windows.UI.Xaml.Controls.ItemsWrapGrid) or [**ItemsStackPanel**](/uwp/api/Windows.UI.Xaml.Controls.ItemsStackPanel). If you use [**VariableSizedWrapGrid**](/uwp/api/Windows.UI.Xaml.Controls.VariableSizedWrapGrid), [**WrapGrid**](/uwp/api/Windows.UI.Xaml.Controls.WrapGrid), or [**StackPanel**](/uwp/api/Windows.UI.Xaml.Controls.StackPanel), then you will not get virtualization. Additionally, the following [**ListView**](/uwp/api/Windows.UI.Xaml.Controls.ListView) events are raised only when using an [**ItemsWrapGrid**](/uwp/api/Windows.UI.Xaml.Controls.ItemsWrapGrid) or an [**ItemsStackPanel**](/uwp/api/Windows.UI.Xaml.Controls.ItemsStackPanel): [**ChoosingGroupHeaderContainer**](/uwp/api/windows.ui.xaml.controls.listviewbase.choosinggroupheadercontainer), [**ChoosingItemContainer**](/uwp/api/windows.ui.xaml.controls.listviewbase.choosingitemcontainer), and [**ContainerContentChanging**](/uwp/api/windows.ui.xaml.controls.listviewbase.containercontentchanging).

The concept of a viewport is critical to UI virtualization because the framework must create the elements that are likely to be shown. In general, the viewport of an [**ItemsControl**](/uwp/api/Windows.UI.Xaml.Controls.ItemsControl) is the extent of the logical control. For example, the viewport of a [**ListView**](/uwp/api/Windows.UI.Xaml.Controls.ListView) is the width and height of the **ListView** element. Some panels allow child elements unlimited space, examples being [**ScrollViewer**](/uwp/api/Windows.UI.Xaml.Controls.ScrollViewer) and a [**Grid**](/uwp/api/Windows.UI.Xaml.Controls.Grid), with auto-sized rows or columns. When a virtualized **ItemsControl** is placed in a panel like that, it takes enough room to display all of its items, which defeats virtualization. Restore virtualization by setting a width and height on the **ItemsControl**.

## Element reduction per item

Keep the number of UI elements used to render your items to a reasonable minimum.

When an items control is first shown, all the elements needed to render a viewport full of items are created. Also, as items approach the viewport, the framework updates the UI elements in cached item templates with the bound data objects. Minimizing the complexity of the markup inside templates pays off in memory and in time spent on the UI thread, improving responsiveness especially while panning/scrolling. The templates in question are the item template (see [**ItemTemplate**](/uwp/api/windows.ui.xaml.controls.itemscontrol.itemtemplate)) and the control template of a [**ListViewItem**](/uwp/api/windows.ui.xaml.controls.listviewitem) or a [**GridViewItem**](/uwp/api/windows.ui.xaml.controls.gridviewitem) (the item control template, or [**ItemContainerStyle**](/uwp/api/windows.ui.xaml.controls.itemscontrol.itemcontainerstyle)). The benefit of even a small reduction in element count is multiplied by the number of items displayed.

For examples of element reduction, see [Optimize your XAML markup](optimize-xaml-loading.md).

The default control templates for [**ListViewItem**](/uwp/api/windows.ui.xaml.controls.listviewitem) and [**GridViewItem**](/uwp/api/windows.ui.xaml.controls.gridviewitem) contain a [**ListViewItemPresenter**](/uwp/api/Windows.UI.Xaml.Controls.Primitives.ListViewItemPresenter) element. This presenter is a single optimized element that displays complex visuals for focus, selection, and other visual states. If you already have custom item control templates ([**ItemContainerStyle**](/uwp/api/windows.ui.xaml.controls.itemscontrol.itemcontainerstyle)), or if in future you edit a copy of an item control template, then we recommend you use a **ListViewItemPresenter** because that element will give you optimum balance between performance and customizability in the majority of cases. You customize the presenter by setting properties on it. For example, here's markup that removes the check mark that appears by default when an item is selected, and changes the background color of the selected item to orange.

```xml
...
<ListView>
    ...
    <ListView.ItemContainerStyle>
        <Style TargetType="ListViewItem">
            <Setter Property="Template">
                <Setter.Value>
                    <ControlTemplate TargetType="ListViewItem">
                        <ListViewItemPresenter SelectionCheckMarkVisualEnabled="False" SelectedBackground="Orange"/>
                    </ControlTemplate>
                </Setter.Value>
            </Setter>
        </Style>
    </ListView.ItemContainerStyle>
</ListView>
<!-- ... -->
```

There are about 25 properties with self-describing names similar to [**SelectionCheckMarkVisualEnabled**](/uwp/api/windows.ui.xaml.controls.primitives.listviewitempresenter.selectioncheckmarkvisualenabled) and [**SelectedBackground**](/uwp/api/windows.ui.xaml.controls.primitives.listviewitempresenter.selectedbackground). Should the presenter types prove not to be customizable enough for your use case, you can edit a copy of the `ListViewItemExpanded` or `GridViewItemExpanded` control template instead. These can be found in `\Program Files (x86)\Windows Kits\10\DesignTime\CommonConfiguration\Neutral\UAP\<version>\Generic\generic.xaml`. Be aware that using these templates means trading some performance for the increase in customization.

<span id="update-items-incrementally"></span>

## Update ListView and GridView items progressively

If you're using data virtualization then you can keep [**ListView**](/uwp/api/Windows.UI.Xaml.Controls.ListView) and [**GridView**](/uwp/api/Windows.UI.Xaml.Controls.GridView) responsiveness high by configuring the control to render temporary UI elements for the items still being (down)loaded. The temporary elements are then progressively replaced with actual UI as data loads.

Also—no matter where you're loading data from (local disk, network, or cloud)—a user can pan/scroll a [**ListView**](/uwp/api/Windows.UI.Xaml.Controls.ListView) or [**GridView**](/uwp/api/Windows.UI.Xaml.Controls.GridView) so rapidly that it's not possible to render each item with full fidelity while preserving smooth panning/scrolling. To preserve smooth panning/scrolling you can choose to render an item in multiple phases in addition to using placeholders.

An example of these techniques is often seen in photo-viewing apps: even though not all of the images have been loaded and displayed, the user can still pan/scroll and interact with the collection. Or, for a "movie" item, you could show the title in the first phase, the rating in the second phase, and an image of the poster in the third phase. The user sees the most important data about each item as early as possible, and that means they're able to take action at once. Then the less important info is filled-in as time allows. Here are the platform features you can use to implement these techniques.

### Placeholders

The temporary placeholder visuals feature is on by default, and it's controlled with the [**ShowsScrollingPlaceholders**](/uwp/api/windows.ui.xaml.controls.listviewbase.showsscrollingplaceholders) property. During fast panning/scrolling, this feature gives the user a visual hint that there are more items yet to fully display while also preserving smoothness. If you use one of the techniques below then you can set **ShowsScrollingPlaceholders** to false if you prefer not to have the system render placeholders.

**Progressive data template updates using x:Phase**

Here's how to use the [x:Phase attribute](../xaml-platform/x-phase-attribute.md) with [{x:Bind}](../xaml-platform/x-bind-markup-extension.md) bindings to implement progressive data template updates.

1.  Here's what the binding source looks like (this is the data source that we'll bind to).

    ```csharp
    namespace LotsOfItems
    {
        public class ExampleItem
        {
            public string Title { get; set; }
            public string Subtitle { get; set; }
            public string Description { get; set; }
        }

        public class ExampleItemViewModel
        {
            private ObservableCollection<ExampleItem> exampleItems = new ObservableCollection<ExampleItem>();
            public ObservableCollection<ExampleItem> ExampleItems { get { return this.exampleItems; } }

            public ExampleItemViewModel()
            {
                for (int i = 1; i < 150000; i++)
                {
                    this.exampleItems.Add(new ExampleItem(){
                        Title = "Title: " + i.ToString(),
                        Subtitle = "Sub: " + i.ToString(),
                        Description = "Desc: " + i.ToString()
                    });
                }
            }
        }
    }
    ```
2.  Here's the markup that `DeferMainPage.xaml` contains. The grid view contains an item template with elements bound to the **Title**, **Subtitle**, and **Description** properties of the **MyItem** class. Note that **x:Phase** defaults to 0. Here, items will be initially rendered with just the title visible. Then the subtitle element will be data bound and made visible for all the items and so on until all the phases have been processed.
    ```xml
    <Page
        x:Class="LotsOfItems.DeferMainPage"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:lotsOfItems="using:LotsOfItems"
        mc:Ignorable="d">

        <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
            <GridView ItemsSource="{x:Bind ViewModel.ExampleItems}">
                <GridView.ItemTemplate>
                    <DataTemplate x:DataType="lotsOfItems:ExampleItem">
                        <StackPanel Height="100" Width="100" Background="OrangeRed">
                            <TextBlock Text="{x:Bind Title}"/>
                            <TextBlock Text="{x:Bind Subtitle}" x:Phase="1"/>
                            <TextBlock Text="{x:Bind Description}" x:Phase="2"/>
                        </StackPanel>
                    </DataTemplate>
                </GridView.ItemTemplate>
            </GridView>
        </Grid>
    </Page>
    ```

3.  If you run the app now and pan/scroll quickly through the grid view then you'll notice that as each new item appears on the screen, at first it is rendered as a dark gray rectangle (thanks to the [**ShowsScrollingPlaceholders**](/uwp/api/windows.ui.xaml.controls.listviewbase.showsscrollingplaceholders) property defaulting to **true**), then the title appears, followed by subtitle, followed by description.

**Progressive data template updates using ContainerContentChanging**

The general strategy for the [**ContainerContentChanging**](/uwp/api/windows.ui.xaml.controls.listviewbase.containercontentchanging) event is to use **Opacity** to hide elements that don’t need to be immediately visible. When elements are recycled, they will retain their old values so we want to hide those elements until we've updated those values from the new data item. We use the **Phase** property on the event arguments to determine which elements to update and show. If additional phases are needed, we register a callback.

1.  We'll use the same binding source as for **x:Phase**.

2.  Here's the markup that `MainPage.xaml` contains. The grid view declares a handler to its [**ContainerContentChanging**](/uwp/api/windows.ui.xaml.controls.listviewbase.containercontentchanging) event, and it contains an item template with elements used to display the **Title**, **Subtitle**, and **Description** properties of the **MyItem** class. To get the maximum performance benefits of using **ContainerContentChanging**, we don't use bindings in the markup but we instead assign values programmatically. The exception here is the element displaying the title, which we consider to be in phase 0.
    ```xml
    <Page
        x:Class="LotsOfItems.MainPage"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:lotsOfItems="using:LotsOfItems"
        mc:Ignorable="d">

        <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
            <GridView ItemsSource="{x:Bind ViewModel.ExampleItems}" ContainerContentChanging="GridView_ContainerContentChanging">
                <GridView.ItemTemplate>
                    <DataTemplate x:DataType="lotsOfItems:ExampleItem">
                        <StackPanel Height="100" Width="100" Background="OrangeRed">
                            <TextBlock Text="{x:Bind Title}"/>
                            <TextBlock Opacity="0"/>
                            <TextBlock Opacity="0"/>
                        </StackPanel>
                    </DataTemplate>
                </GridView.ItemTemplate>
            </GridView>
        </Grid>
    </Page>
    ```
3.  Lastly, here's the implementation of the [**ContainerContentChanging**](/uwp/api/windows.ui.xaml.controls.listviewbase.containercontentchanging) event handler. This code also shows how we add a property of type **RecordingViewModel** to **MainPage** to expose the binding source class from the class that represents our page of markup. As long as you don't have any [{Binding}](../xaml-platform/binding-markup-extension.md) bindings in your data template, then mark the event arguments object as handled in the first phase of the handler to hint to the item that it needn't set a data context.
    ```csharp
    namespace LotsOfItems
    {
        /// <summary>
        /// An empty page that can be used on its own or navigated to within a Frame.
        /// </summary>
        public sealed partial class MainPage : Page
        {
            public MainPage()
            {
                this.InitializeComponent();
                this.ViewModel = new ExampleItemViewModel();
            }

            public ExampleItemViewModel ViewModel { get; set; }

            // Display each item incrementally to improve performance.
            private void GridView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
            {
                if (args.Phase != 0)
                {
                    throw new System.Exception("We should be in phase 0, but we are not.");
                }

                // It's phase 0, so this item's title will already be bound and displayed.

                args.RegisterUpdateCallback(this.ShowSubtitle);

                args.Handled = true;
            }

            private void ShowSubtitle(ListViewBase sender, ContainerContentChangingEventArgs args)
            {
                if (args.Phase != 1)
                {
                    throw new System.Exception("We should be in phase 1, but we are not.");
                }

                // It's phase 1, so show this item's subtitle.
                var templateRoot = args.ItemContainer.ContentTemplateRoot as StackPanel;
                var textBlock = templateRoot.Children[1] as TextBlock;
                textBlock.Text = (args.Item as ExampleItem).Subtitle;
                textBlock.Opacity = 1;

                args.RegisterUpdateCallback(this.ShowDescription);
            }

            private void ShowDescription(ListViewBase sender, ContainerContentChangingEventArgs args)
            {
                if (args.Phase != 2)
                {
                    throw new System.Exception("We should be in phase 2, but we are not.");
                }

                // It's phase 2, so show this item's description.
                var templateRoot = args.ItemContainer.ContentTemplateRoot as StackPanel;
                var textBlock = templateRoot.Children[2] as TextBlock;
                textBlock.Text = (args.Item as ExampleItem).Description;
                textBlock.Opacity = 1;
            }
        }
    }
    ```

4.  If you run the app now and pan/scroll quickly through the grid view then you'll see the same behavior as for **x:Phase**.

## Container-recycling with heterogeneous collections

In some applications, you need to have different UI for different types of item within a collection. This can create a situation where it is impossible for virtualizing panels to reuse/recycle the visual elements used to display the items. Recreating the visual elements for an item during panning undoes many of the performance wins provided by virtualization. However, a little planning can allow virtualizing panels to reuse the elements. Developers have a couple of options depending on their scenario: the [**ChoosingItemContainer**](/uwp/api/windows.ui.xaml.controls.listviewbase.choosingitemcontainer) event, or an item template selector. The **ChoosingItemContainer** approach has better performance.

**The ChoosingItemContainer event**

[**ChoosingItemContainer**](/uwp/api/windows.ui.xaml.controls.listviewbase.choosingitemcontainer) is an event that allows you to provide an item (**ListViewItem**/**GridViewItem**) to the [**ListView**](/uwp/api/Windows.UI.Xaml.Controls.ListView)/[**GridView**](/uwp/api/Windows.UI.Xaml.Controls.GridView) whenever a new item is needed during start-up or recycling. You can create a container based on the type of data item the container will display (shown in the example below). **ChoosingItemContainer** is the higher-performing way to use different data templates for different items. Container caching is something that can be achieved using **ChoosingItemContainer**. For example, if you have five different templates, with one template occurring an order of magnitude more often than the others, then ChoosingItemContainer allows you not only to create items at the ratios needed but also to keep an appropriate number of elements cached and available for recycling. [**ChoosingGroupHeaderContainer**](/uwp/api/windows.ui.xaml.controls.listviewbase.choosinggroupheadercontainer) provides the same functionality for group headers.

```csharp
// Example shows how to use ChoosingItemContainer to return the correct
// DataTemplate when one is available. This example shows how to return different 
// data templates based on the type of FileItem. Available ListViewItems are kept
// in two separate lists based on the type of DataTemplate needed.
private void ListView_ChoosingItemContainer
    (ListViewBase sender, ChoosingItemContainerEventArgs args)
{
    // Determines type of FileItem from the item passed in.
    bool special = args.Item is DifferentFileItem;

    // Uses the Tag property to keep track of whether a particular ListViewItem's 
    // datatemplate should be a simple or a special one.
    string tag = special ? "specialFiles" : "simpleFiles";

    // Based on the type of datatemplate needed return the correct list of 
    // ListViewItems, this could have also been handled with a hash table. These 
    // two lists are being used to keep track of ItemContainers that can be reused.
    List<UIElement> relevantStorage = special ? specialFileItemTrees : simpleFileItemTrees;

    // args.ItemContainer is used to indicate whether the ListView is proposing an 
    // ItemContainer (ListViewItem) to use. If args.Itemcontainer, then there was a 
    // recycled ItemContainer available to be reused.
    if (args.ItemContainer != null)
    {
        // The Tag is being used to determine whether this is a special file or 
        // a simple file.
        if (args.ItemContainer.Tag.Equals(tag))
        {
            // Great: the system suggested a container that is actually going to 
            // work well.
        }
        else
        {
            // the ItemContainer's datatemplate does not match the needed 
            // datatemplate.
            args.ItemContainer = null;
        }
    }

    if (args.ItemContainer == null)
    {
        // see if we can fetch from the correct list.
        if (relevantStorage.Count > 0)
        {
            args.ItemContainer = relevantStorage[0] as SelectorItem;
        }
        else
        {
            // there aren't any (recycled) ItemContainers available. So a new one 
            // needs to be created.
            ListViewItem item = new ListViewItem();
            item.ContentTemplate = this.Resources[tag] as DataTemplate;
            item.Tag = tag;
            args.ItemContainer = item;
        }
    }
}
```

**Item template selector**

An item template selector ([**DataTemplateSelector**](/uwp/api/Windows.UI.Xaml.Controls.DataTemplateSelector)) allows an app to return a different item template at runtime based on the type of the data item that will be displayed. This makes development more productive, but it makes UI virtualization more difficult because not every item template can be reused for every data item.

When recycling an item (**ListViewItem**/**GridViewItem**), the framework must decide whether the items that are available for use in the recycle queue (the recycle queue is a cache of items that are not currently being used to display data) have an item template that will match the one desired by the current data item. If there are no items in the recycle queue with the appropriate item template then a new item is created, and the appropriate item template is instantiated for it. If, on other hand, the recycle queue contains an item with the appropriate item template then that item is removed from the recycle queue and is used for the current data item. An item template selector works in situations where only a small number of item templates are used and there is a flat distribution throughout the collection of items that use different item templates.

When there is an uneven distribution of items that use different item templates then new item templates will likely need to be created during panning, and this negates many of the gains provided by virtualization. Additionally, an item template selector only considers five possible candidates when evaluating whether a particular container can be reused for the current data item. So you should carefully consider whether your data is appropriate for use with an item template selector before using one in your app. If your collection is mostly homogeneous then the selector is returning the same type most (possibly all) of the time. Just be aware of the price you're paying for the rare exceptions to that homogeneity, and consider whether using [**ChoosingItemContainer**](/uwp/api/windows.ui.xaml.controls.listviewbase.choosingitemcontainer) (or two items controls) is preferable.

 
