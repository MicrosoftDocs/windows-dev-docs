---
description: Learn about collections and lists as representations of multiple related data items that appear together. 
title: Collections and lists
ms.assetid: C73125E8-3768-46A5-B078-FDDF42AB1077
label: Collections and Lists
template: detail.hbs
ms.date: 10/08/2019
ms.topic: article
keywords: windows 10, uwp
pm-contact: anawish
design-contact: kimsea
dev-contact: ranjeshj
doc-status: Published
ms.localizationpriority: medium
---
# Collections and lists

Collections and lists both refer to the representation of multiple related data items that appear together. Collections can be represented in multiple ways, by different collection controls (also may be referred to as collection views). Collection controls display and enable interactions with collection-based content, such as a list of contacts, a list of dates, a collection of images, and so on.

> **Important APIs**: [ListView class](/uwp/api/Windows.UI.Xaml.Controls.ListView), [GridView class](/uwp/api/Windows.UI.Xaml.Controls.GridView), [FlipView class](/uwp/api/windows.ui.xaml.controls.flipview), [TreeView class](/uwp/api/windows.ui.xaml.controls.treeview), [ItemsRepeater class](/uwp/api/microsoft.ui.xaml.controls.itemsrepeater?view=winui-2.2)

The controls covered in this article include:

- List views, which are primarily used to display text-heavy content collections
- Grid views, which are primarily used to display image-heavy content collections
- Flip views, which are primarily used to display image-heavy content collections that require exactly one item to be in focus at a time
- Tree views, which are primarily used to display text-heavy content collections in a specific hierarchy
- ItemsRepeater, which is a customizable building block to create custom collection controls

Design guidelines, features, and examples are given below for each control.

Each of these controls (with the exception of ItemsRepeater) provide built-in styling and interaction. However, to further customize the visual look of your collection view and the items inside it, a [DataTemplate](/uwp/api/Windows.UI.Xaml.DataTemplate) is used. Detailed information on data templates and customizing the look of a collection view can be found on the [Item containers and templates](./item-containers-templates.md) page.

Each of these controls (with the exception of ItemsRepeater) also have built-in behavior to allow for the selection of single or multiple items. See [Selection modes overview](selection-modes.md) to learn more.

One of the scenarios not covered in this article is displaying collections in a table or across multiple columns. If you're looking to display a collection in this format, consider using the [DataGrid control](/windows/communitytoolkit/controls/datagrid) from the [Windows Community Toolkit](/windows/communitytoolkit/). 

> **Windows 10 Fall Creators Update - Behavior change**
> By default, instead of performing selection, an active pen now scrolls/pans a list in Windows apps (like touch, touchpad, and passive pen).
> If your app depends on the previous behavior, you can override pen scrolling and revert to the previous behavior. For details, see the API reference topic for the [Scroll Viewer Class](/uwp/api/windows.ui.xaml.controls.scrollviewer).

## Examples

<table>
<th align="left">XAML Controls Gallery<th>
<tr>
<td><img src="images/xaml-controls-gallery-app-icon-sm.png" alt="XAML controls gallery"></img></td>
<td>
    <p>If you have the <strong style="font-weight: semi-bold">XAML Controls Gallery</strong> app installed, see the <a href="xamlcontrolsgallery:/item/ListView">ListView</a>, <a href="xamlcontrolsgallery:/item/GridView">GridView</a>, <a href="xamlcontrolsgallery:/item/FlipView">FlipView</a>, <a href="xamlcontrolsgallery:/item/TreeView">TreeView</a>, and <a href="xamlcontrolsgalley:/item/ItemsRepeater">ItemsRepeater</a> in action.</p>
    <ul>
    <li><a href="https://www.microsoft.com/store/productId/9MSVH128X2ZT">Get the XAML Controls Gallery app (Microsoft Store)</a></li>
    <li><a href="https://github.com/Microsoft/Xaml-Controls-Gallery">Get the source code (GitHub)</a></li>
    </ul>
</td>
</tr>
</table>

## List views

List views represent text-heavy items, typically in a single-column, vertically-stacked layout. They let you categorize items and assign group headers, drag and drop items, curate content, and reorder items.

### Is this the right control?

Use a list view to:

- Display a collection that primarily consists of text-based items, where all of the items should have the same visual and interaction behavior.
- Represent a single or categorized collection of content.
- Accommodate a variety of use cases, including the following common ones:
    - Create a list of messages or message log.
    - Create a contacts list.
    - Create the master pane in the [master/details pattern](master-details.md). A master/details pattern is often used in email apps, in which one pane (the master) has a list of selectable items while the other pane (details) has a detailed view of the selected item.
    

### Examples

Here's a simple list view that shows a contacts list, and groups the data items alphabetically. The group headers (each letter of the alphabet in this example) can also be customized to stay "sticky", as in they will always appear at the top of the ListView while scrolling.

![A list view with grouped data](images/listview-grouped-example-resized-final.png)

This is a ListView that has been inverted to display a log of messages, with the newest messages appearing at the bottom. With an inverted ListView, items appear at the bottom of the screen with a built-in animation.

![Inverted List view](images/listview-inverted-2.png)

### Related articles
<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Topic</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p><a href="listview-and-gridview.md">List view and grid view</a></p></td>
<td align="left"><p>Learn the essentials of using a list view or grid view in your app.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="item-containers-templates.md">Item containers and templates</a></p></td>
<td align="left"><p>The items you display in a list or grid view can play a major role in the overall look of your app. Make your app look great by customizing the look of your collection items through modifying control templates and data templates.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="item-templates-listview.md">Item templates for list view</a></p></td>
<td align="left"><p>Use these example item templates for a ListView to get the look of common app types.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="inverted-lists.md">Inverted lists</a></p></td>
<td align="left"><p>Inverted lists have new items added at the bottom, like in a chat app. Follow this article's guidance to use an inverted list in your app.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="pull-to-refresh.md">Pull-to-refresh</a></p></td>
<td align="left"><p>The pull-to-refresh mechanism lets a user pull down on a list of data using touch in order to retrieve more data. Use this article to implement pull-to-refresh in your list view.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="nested-ui.md">Nested UI</a></p></td>
<td align="left"><p>Nested UI is a user interface (UI) that exposes actionable controls enclosed inside a container that a user can also take action on. For example, you might have list view item that contains a button, and the user can select the list item, or press the button nested within it. Follow these best practices to provide the best nested UI experience for your users.</p></td>
</tr>
</tbody>
</table>

## Grid views

Grid views are suited for arranging and browsing image-based content collections. A grid view layout scrolls vertically and pans horizontally. Items are in a wrapped layout, as they appear in a left-to-right, then top-to-bottom reading order.

### Is this the right control?

Use a grid view to:

- Display a content collection in which the focal point of each item is an image, and each item should have the same visual and interaction behavior.
- Display content libraries.
- Format the two content views associated with [semantic zoom](semantic-zoom.md).
- Accommodate a variety of use cases, including the following common ones:
    - Storefront-type user interface (i.e. browsing apps, songs, products)
    - Interactive photo libraries

### Examples

This example shows a typical grid view layout, in this case for browsing apps. Metadata for grid view items is usually restricted to a few lines of text and an item rating.

![Example of a grid view layout](images/controls_gridview_example02.png)

A grid view is an ideal solution for a content library, which is often used to present media such as pictures and videos. In a content library, users expect to be able to tap an item to invoke an action.

![Example of a content library](images/gridview-simple-example-final.png)

### Related articles
<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Topic</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p><a href="listview-and-gridview.md">List view and grid view</a></p></td>
<td align="left"><p>Learn the essentials of using a list view or grid view in your app.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="item-containers-templates.md">Item containers and templates</a></p></td>
<td align="left"><p>The items you display in a list or grid view can play a major role in the overall look of your app. Make your app look great by customizing the look of your collection items through modifying control templates and data templates.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="item-templates-gridview.md">Item templates for grid view</a></p></td>
<td align="left"><p>Use these example item templates for GridView to get the look of common app types.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="nested-ui.md">Nested UI</a></p></td>
<td align="left"><p>Nested UI is a user interface (UI) that exposes actionable controls enclosed inside a container that a user can also take action on. For example, you might have grid view item that contains a button, and the user can select the grid item, or press the button nested within it. Follow these best practices to provide the best nested UI experience for your users.</p></td>
</tr>
</tbody>
</table>

## Flip views

Flip views are suited for browsing image-based content collections, specifically where the desired experience is for only one image to be visible at a time. A flip view allows the user to move or "flip" through the collection items (either vertically or horizontally), having each item appear one at a time  after the user interaction.

### Is this the right control?

Use a flip view to:

- Display a small to medium (less than 25 items) collection, where the collection is made up of images with little to no metadata.
- Display items one at a time, and allow the end-user to flip through the items at their own pace.
- Accommodate a variety of use cases, including the following common ones:
    - Photo galleries
    - Product galleries or showcases

### Examples

The following two examples show a FlipView that flips horizontally and vertically, respectively.

![Horizontal flip view](images/controls_flipview_horizonal.jpg)

![Vertical Flip view](images/controls_flipview_vertical.jpg)

### Related articles
<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Topic</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p><a href="flipview.md">Flip view</a></p></td>
<td align="left"><p>Learn the essentials of using a flip view in your app, along with how to customize the look of your items within a flip view.</p></td>
</tr>
</tbody>
</table>

## Tree views

Tree views are suited for displaying text-based collections that have an important hierarchy that needs to be showcased. Tree view items are collapsible/expandable, are shown in a visual hierarchy, can be supplmented with icons, and can be dragged and dropped between tree views. Tree views allow for N-level nesting.

### Is this the right control?

Use a tree view to:

- Display a collection of nested items whose context and meaning is dependent on a hierarchy or specific organizational chain.
- Accommodate a variety of use cases, including the following common ones:
    - File browser
    - Company organizational chart

### Examples

Here is an example of a tree view that represents a file explorer, and displays many different nested items supplemented by icons.

![Tree view with icons](images/treeview-icons.png)

### Related articles
<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Topic</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p><a href="tree-view.md">Tree view</a></p></td>
<td align="left"><p>Learn the essentials of using a tree view in your app, along with how to customize the look and interaction behavior of your items within a tree view.</p></td>
</tr>
</tbody>
</table>

## ItemsRepeater

ItemsRepeater is different from the rest of the collection controls shown on this page because it does not provide any styling or interaction out-of-box, i.e. when simply placed on a page without defining any properties. ItemsRepeater is rather a building block that you as a developer can use to create your own custom collections control, specifically one that cannot be achieved by using the other controls in this article. ItemsRepeater is a data-driven and high-performance panel that can be tailored to fit your exact needs.

### Is this the right control?

Use an ItemsRepeater if:

- You have a specific user interface and user experience in mind that cannot be created using existing collection controls.
- You have an existing data source for your items (such as data pulled from the internet, a database, or a pre-existing collection in your code-behind).

### Examples

The following three examples are all ItemsRepeater controls that are bound to the same data source (a collection of numbers). The collection of numbers is represented in three ways, with each of the ItemsRepeaters below using a different custom [Layout](/uwp/api/microsoft.ui.xaml.controls.layout) and a different custom [ItemTemplate](/uwp/api/microsoft.ui.xaml.controls.itemsrepeater.itemtemplate?view=winui-2.2).

![ItemsRepeater with horizontal bars](images/itemsrepeater-1.png)
![ItemsRepeater with vertical bars](images/itemsrepeater-2.png)
![ItemsRepeater with circular representation](images/itemsrepeater-3.png)

### Related articles
<table>
<colgroup>
<col width="50%" />
<col width="50%" />
</colgroup>
<thead>
<tr class="header">
<th align="left">Topic</th>
<th align="left">Description</th>
</tr>
</thead>
<tbody>
<tr class="odd">
<td align="left"><p><a href="items-repeater.md">ItemsRepeater</a></p></td>
<td align="left"><p>Learn the essentials of using an ItemsRepeater in your app, along with how to implement all of the necessary interaction and visual components for your collection view.</p></td>
</tr>
</tbody>
</table>


## Globalization and localization checklist

<table>
<tr>
<th>Wrapping</th><td>Allow two lines for the list label.</td>
</tr>
<tr>
<th>Horizontal expansion</th><td>Make sure fields can accomdation text expansion and are scrollable.</td>
</tr>
<tr>
<th>Vertical spacing</th><td>Use non-Latin characters for vertical spacing to ensure non-Latin scripts will display properly.</td>
</tr>
</table>

## Get the sample code

- [XAML Controls Gallery sample](https://github.com/Microsoft/Xaml-Controls-Gallery) - See all the XAML controls in an interactive format.

## Related articles

**Design and UX Guidelines**
- [Master/details](master-details.md)
- [Nav pane](navigationview.md)
- [Semantic zoom](semantic-zoom.md)
- [Drag and drop](https://docs.microsoft.com/windows/uwp/app-to-app/drag-and-drop)
- [Thumbnail images](../../files/thumbnails.md)

**API reference**
- [ListView class](/uwp/api/Windows.UI.Xaml.Controls.ListView)
- [GridView class](/uwp/api/Windows.UI.Xaml.Controls.GridView)
- [ComboBox class](/uwp/api/Windows.UI.Xaml.Controls.ComboBox)
- [ListBox class](/uwp/api/Windows.UI.Xaml.Controls.ListBox)