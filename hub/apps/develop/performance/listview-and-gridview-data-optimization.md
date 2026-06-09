---
title: ListView and GridView data virtualization
description: Improve WinUI ListView and GridView performance and startup time in Windows App SDK apps through data virtualization.
ms.date: 03/16/2026
ms.topic: article
ms.localizationpriority: medium
---
# ListView and GridView data virtualization

**Note**  
For more details, see the //build/ session Dramatically Increase Performance when Users Interact with Large Amounts of Data in GridView and ListView.

Improve [**ListView**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.listview) and [**GridView**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.gridview) performance and startup time through data virtualization. For UI virtualization, element reduction, and progressive updating of items, see [Optimize ListView and GridView performance for WinUI](optimize-gridview-and-listview.md).

A method of data virtualization is needed for a data set that is so large that it cannot or should not all be stored in memory at one time. You load an initial portion into memory (from local disk, network, or cloud) and apply UI virtualization to this partial data set. You can later load data incrementally, or from arbitrary points in the master data set (random access), on demand. Whether data virtualization is appropriate for you depends on many factors.

- The size of your data set
- The size of each item
- The source of the data set (local disk, network, or cloud)
- The overall memory consumption of your WinUI app

**Note**  Be aware that a feature is enabled by default for ListView and GridView that displays temporary placeholder visuals while the user is panning or scrolling quickly. As data is loaded, these placeholder visuals are replaced with your item template. You can turn the feature off by setting [**ListViewBase.ShowsScrollingPlaceholders**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.listviewbase.showsscrollingplaceholders) to false, but if you do so then we recommend that you use the x:Phase attribute to progressively render the elements in your item template. See [Update ListView and GridView items progressively](optimize-gridview-and-listview.md#update-items-incrementally).

Here are more details about the incremental and random-access data virtualization techniques.

## Incremental data virtualization

Incremental data virtualization loads data sequentially. A [**ListView**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.listview) that uses incremental data virtualization may be used to view a collection of a million items, but only 50 items are loaded initially. As the user pans or scrolls, the next 50 are loaded. As items are loaded, the scroll bar's thumb decreases in size. For this type of data virtualization you write a data source class that implements these interfaces.

- [**IList**](/dotnet/api/system.collections.ilist)
- [**INotifyCollectionChanged**](/dotnet/api/system.collections.specialized.inotifycollectionchanged) or [**IObservableVector&lt;T&gt;**](/uwp/api/Windows.Foundation.Collections.IObservableVector_T_)
- [**ISupportIncrementalLoading**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.isupportincrementalloading)

A data source like this is an in-memory list that can be continually extended. The items control will ask for items using the standard [**IList**](/dotnet/api/system.collections.ilist) indexer and count properties. The count should represent the number of items locally, not the true size of the dataset.

When the items control gets close to the end of the existing data, it calls [**ISupportIncrementalLoading.HasMoreItems**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.isupportincrementalloading.hasmoreitems). If you return **true**, then it calls [**ISupportIncrementalLoading.LoadMoreItemsAsync**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.isupportincrementalloading.loadmoreitemsasync), passing an advised number of items to load. Depending on where you're loading data from (local disk, network, or cloud), you may choose to load a different number of items than that advised. For example, if your service supports batches of 50 items but the items control asks for only 10, then you can load 50. Load the data from your back end, add it to your list, and raise a change notification via [**INotifyCollectionChanged**](/dotnet/api/system.collections.specialized.inotifycollectionchanged) or [**IObservableVector&lt;T&gt;**](/uwp/api/Windows.Foundation.Collections.IObservableVector_T_) so that the items control knows about the new items. Also return a count of the items you actually loaded. If you load fewer items than advised, or the items control has been panned or scrolled even further in the interim, then your data source will be called again for more items and the cycle continues. [**ISupportIncrementalLoading**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.isupportincrementalloading) remains available in Windows App SDK, so you can use the same incremental-loading pattern in a WinUI app.

## Random access data virtualization

Random access data virtualization allows loading from an arbitrary point in the data set. A [**ListView**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.controls.listview) that uses random access data virtualization to view a collection of a million items can load items 100,000-100,050. If the user then moves to the beginning of the list, the control loads items 1-50. At all times, the scroll bar's thumb indicates that the **ListView** contains a million items. The position of the scroll bar's thumb is relative to where the visible items are located in the collection's entire data set. This type of data virtualization can significantly reduce the memory requirements and load times for the collection. To enable it you need to write a data source class that fetches data on demand, manages a local cache, and implements these interfaces.

- [**IList**](/dotnet/api/system.collections.ilist)
- [**INotifyCollectionChanged**](/dotnet/api/system.collections.specialized.inotifycollectionchanged) or [**IObservableVector&lt;T&gt;**](/uwp/api/Windows.Foundation.Collections.IObservableVector_T_)
- (Optionally) [**IItemsRangeInfo**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.iitemsrangeinfo)
- (Optionally) [**ISelectionInfo**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.iselectioninfo)

[**IItemsRangeInfo**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.iitemsrangeinfo) provides information about which items the control is actively using. The items control calls this method whenever its view is changing, and includes these two sets of ranges.

- The set of items that are in the viewport.
- A set of non-virtualized items that the control is using that may not be in the viewport.
  - A buffer of items around the viewport that the items control keeps so that touch panning is smooth.
  - The focused item.
  - The first item.

By implementing [**IItemsRangeInfo**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.iitemsrangeinfo), your data source knows what items need to be fetched and cached, and when to prune data from the cache that is no longer needed. **IItemsRangeInfo** uses [**ItemIndexRange**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.itemindexrange) objects to describe a set of items based on their index in the collection. This avoids item pointers, which may not be correct or stable. **IItemsRangeInfo** is designed to be used by only a single instance of an items control because it relies on state information for that items control. If multiple items controls need access to the same data, then you need a separate instance of the data source for each. They can share a common cache, but the logic to purge from the cache will be more complicated. [**IItemsRangeInfo**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.iitemsrangeinfo) remains available in Windows App SDK, so the same random-access caching techniques apply to WinUI controls.

Here's the basic strategy for your random-access data virtualization data source.

- When asked for an item
  - If you have it available in memory, then return it.
  - If you don't have it, then return either `null` or a placeholder item.
  - Use the request for an item (or the range information from [**IItemsRangeInfo**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.iitemsrangeinfo)) to know which items are needed, and to fetch data for items from your back end asynchronously. After retrieving the data, raise a change notification via [**INotifyCollectionChanged**](/dotnet/api/system.collections.specialized.inotifycollectionchanged) or [**IObservableVector&lt;T&gt;**](/uwp/api/Windows.Foundation.Collections.IObservableVector_T_) so that the items control knows about the new item.
- (Optionally) as the items control's viewport changes, identify what items are needed from your data source via your implementation of [**IItemsRangeInfo**](/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.data.iitemsrangeinfo).

Beyond that, the strategy for when to load data items, how many to load, and which items to keep in memory is up to your application. Some general considerations to keep in mind:

- Make asynchronous requests for data; don't block the UI thread.
- Find the sweet spot in the size of the batches you fetch items in. Prefer chunky to chatty. Not so small that you make too many small requests; not so large that they take too long to retrieve.
- Consider how many requests you want to have pending at the same time. Performing one at a time is easier, but it may be too slow if turnaround time is high.
- Can you cancel requests for data?
- If using a hosted service, is there a cost per transaction?
- What kind of notifications are provided by the service when the results of a query change? Will you know if an item is inserted at index 33? If your service supports queries based on a key-plus-offset, that may be better than using just an index.
- How smart do you want to be in prefetching items? Are you going to try to track the direction and velocity of scrolling to predict which items are needed?
- How aggressive do you want to be in purging the cache? This is a tradeoff of memory versus experience.
