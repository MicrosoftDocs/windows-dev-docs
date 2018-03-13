---
author: muhsinking
Description: Lists display and enable interaction with collection-based content.
title: Lists
ms.assetid: C73125E8-3768-46A5-B078-FDDF42AB1077
label: Lists
template: detail.hbs
ms.author: mukin
ms.date: 05/19/2017
ms.topic: article
ms.prod: windows
ms.technology: uwp
keywords: windows 10, uwp
pm-contact: predavid
design-contact: kimsea
dev-contact: ranjeshj
doc-status: Published
ms.localizationpriority: medium
---
# Lists

Lists display and enable interactions with collection-based content. The four list patterns covered in this article include:

-   List views, which are primarily used to display text-heavy content collections
-   Grid views, which are primarily used to display image-heavy content collections
-   Drop-down lists, which let users choose one item from an expanding list
-   List boxes, which let users choose one item or multiple items from a box that can be scrolled

Design guidelines, features, and examples are given for each list pattern.

> **Important APIs**: [ListView class](https://msdn.microsoft.com/library/windows/apps/br242878), [GridView class](https://msdn.microsoft.com/library/windows/apps/br242705), [ComboBox class](https://msdn.microsoft.com/library/windows/apps/br209348)


> <div id="main">
> <strong>Windows 10 Fall Creators Update - Behavior change</strong>
> </div>
> By default, instead of performing selection, an active pen now scrolls/pans a list in UWP apps (like touch, touchpad, and passive pen).
> If your app depends on the previous behavior, you can override pen scrolling and revert to the previous behavior. See the [Scroll​Viewer Class] (https://docs.microsoft.com/uwp/api/windows.ui.xaml.controls.scrollviewer) API reference topic for details.

## List views

List views let you categorize items and assign group headers, drag and drop items, curate content, and reorder items.

### Is this the right control?

Use a list view to:

-   Display a content collection that primarily consists of text.
-   Navigate a single or categorized collection of content.
-   Create the master pane in the [master/details pattern](master-details.md). A master/details pattern is often used in email apps, in which one pane (the master) has a list of selectable items while the other pane (details) has a detailed view of the selected item.

### Examples

Here's a simple list view showing grouped data on a phone.

![A list view with grouped data](images/simple-list-view-phone.png)

### Recommendations

-   Items within a list should have the same behavior.
-   If your list is divided into groups, you can use [semantic zoom](semantic-zoom.md) to make it easier for users to navigate through grouped content.

### List view articles
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
<td align="left"><p>The items you display in a list or grid can play a major role in the overall look of your app. Modify control templates and data templates to define the look of the items and make your app look great.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="item-templates-listview.md">Item templates for list view</a></p></td>
<td align="left"><p>Use these example item templates for a ListView to get the look of common app types.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="inverted-lists.md">Inverted lists</a></p></td>
<td align="left"><p>Inverted lists have new items added at the bottom, like in a chat app. Follow this guidance to use an inverted list in your app.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="pull-to-refresh.md">Pull-to-refresh</a></p></td>
<td align="left"><p>The pull-to-refresh pattern lets a user pull down on a list of data using touch in order to retrieve more data. Use this guidance to implement pull-to-refresh in your list view.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="nested-ui.md">Nested UI</a></p></td>
<td align="left"><p>Nested UI is a user interface (UI) that exposes actionable controls enclosed inside a container that a user can also take action on. For example, you might have list view item that contains a button, and the user can select the list item, or press the button nested within it. Follow these best practices to provide the best nested UI experience for your users.</p></td>
</tr>
</tbody>
</table>

## Grid views

Grid views are suited for arranging and browsing image-based content collections. A grid view layout scrolls vertically and pans horizontally. Items are laid out in a left-to-right, then top-to-bottom reading order.

### Is this the right control?

Use a list view to:

-   Display a content collection that primarily consists of images.
-   Display content libraries.
-   Format the two content views associated with [semantic zoom](semantic-zoom.md).

### Examples

This example shows a typical grid view layout, in this case for browsing apps. Metadata for grid view items is usually restricted to a few lines of text and an item rating.

![Example of a grid view layout](images/controls_gridview_example02.png)

A grid view is an ideal solution for a content library, which is often used to present media such as pictures and videos. In a content library, users expect to be able to tap an item to invoke an action.

![Example of a content library](images/controls_list_contentlibrary.png)

### Recommendations

-   Items within a list should have the same behavior.
-   If your list is divided into groups, you can use [semantic zoom](semantic-zoom.md) to make it easier for users to navigate through grouped content.

### Grid view articles
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
<td align="left"><p>The items you display in a list or grid can play a major role in the overall look of your app. Modify control templates and data templates to define the look of the items and make your app look great.</p></td>
</tr>
<tr class="odd">
<td align="left"><p><a href="item-templates-gridview.md">Item templates for grid view</a></p></td>
<td align="left"><p>Use these example item templates for GridView to get the look of common app types.</p></td>
</tr>
<tr class="even">
<td align="left"><p><a href="nested-ui.md">Nested UI</a></p></td>
<td align="left"><p>Nested UI is a user interface (UI) that exposes actionable controls enclosed inside a container that a user can also take action on. For example, you might have list view item that contains a button, and the user can select the list item, or press the button nested within it. Follow these best practices to provide the best nested UI experience for your users.</p></td>
</tr>
</tbody>
</table>

## Drop-down lists

Drop-down lists, also known as combo boxes, start in a compact state and expand to show a list of selectable items. The selected item is always visible, and non-visible items can be brought into view when the user taps the combo box to expand it.

### Is this the right control?

-   Use a drop-down list to let users select a single value from a set of items that can be adequately represented with single lines of text.
-   Use a list or grid view instead of a combo box to display items that contain multiple lines of text or images.
-   When there are fewer than five items, consider using [radio buttons](radio-button.md) (if only one item can be selected) or [check boxes](checkbox.md) (if multiple items can be selected).
-   Use a combo box when the selection items are of secondary importance in the flow of your app. If the default option is recommended for most users in most situations, showing all the items by using a list view might draw more attention to the options than necessary. You can save space and minimize distraction by using a combo box.

### Examples

A combo box in its compact state can show a header.

![Example of a drop-down list in its compact state](images/combo_box_collapsed.png)

Although combo boxes expand to support longer string lengths, avoid excessively long strings that are difficult to read.

![Example of a drop-down list with long text string](images/combo_box_listitemstate.png)

If the collection in a combo box is long enough, a scroll bar will appear to accommodate it. Group items logically in the list.

![Example of a scroll bar in a drop-down list](images/combo_box_scroll.png)

### Recommendations

-   Limit the text content of combo box items to a single line.
-   Sort items in a combo box in the most logical order. Group together related options and place the most common options at the top. Sort names in alphabetical order, numbers in numerical order, and dates in chronological order.
-   To make a combo box that live updates while the user is using the arrow keys (like a Font selection drop-down), set SelectionChangedTrigger to “Always”.  

### Text Search

Combo boxes automatically support search within their collections. As users type characters on a physical keyboard while focused on an open or closed combo box, candidates matching the user's string are brought into view. This functionality is especially helpful when navigating a long list. For example, when interacting with a drop-down containing a list of states, users can press the “w” key to bring “Washington” into view for quick selection.


## List boxes

A list box allows the user to choose either a single item or multiple items from a collection. List boxes are similar to drop-down lists, except that list boxes are always open—there is no compact (non-expanded) state for a list box. Items in the list can be scrolled if there isn't space to show everything.

### Is this the right control?

-   A list box can be useful when items in the list are important enough to prominently display, and when there's enough screen real estate, to show the full list.
-   A list box should draw the user's attention to the full set of alternatives in an important choice. By contrast, a drop-down list initially draws the user's attention to the selected item.
-   Avoid using a list box if:
    -   There is a very small number of items for the list. A single-select list box that always has the same 2 options might be better presented as [radio buttons](radio-button.md). Also consider using radio buttons when there are 3 or 4 static items in the list.
    -   The list box is single-select and it always has the same 2 options where one can be implied as not the other, such as "on" and "off." Use a single check box or a toggle switch.
    -   There is a very large number of items. A better choice for long lists are grid view and list view. For very long lists of grouped data, semantic zoom is preferred.
    -   The items are contiguous numerical values. If that's the case, consider using a [slider](slider.md).
    -   The selection items are of secondary importance in the flow of your app or the default option is recommended for most users in most situations. Use a drop-down list instead.

### Recommendations

-   The ideal range of items in a list box is 3 to 9.
-   A list box works well when its items can dynamically vary.
-   If possible, set the size of a list box so that its list of items don't need to be panned or scrolled.
-   Verify that the purpose of the list box, and which items are currently selected, is clear.
-   Reserve visual effects and animations for touch feedback, and for the selected state of items.
-   Limit the list box item's text content to a single line. If the items are visuals, you can customize the size. If an item contains multiple lines of text or images, instead use a grid view or list view.
-   Use the default font unless your brand guidelines indicate to use another.
-   Don't use a list box to perform commands or to dynamically show or hide other controls.

## Selection mode

Selection mode lets users select and take action on a single item or on multiple items. It can be invoked through a context menu, by using CTRL+click or SHIFT+click on an item, or by rolling-over a target on an item in a gallery view. When selection mode is active, check boxes appear next to each list item, and actions can appear at the top or the bottom of the screen.

There are three selection modes:

-   Single: The user can select only one item at a time.
-   Multiple: The user can select multiple items without using a modifier.
-   Extended: The user can select multiple items with a modifier, such as holding down the SHIFT key.

Tapping anywhere on an item selects it. Tapping on the command bar action affects all selected items. If no item is selected, command bar actions should be inactive, except for "Select All".

Selection mode doesn't have a light dismiss model; tapping outside of the frame in which selection mode is active won't cancel the mode. This is to prevent accidental deactivation of the mode. Clicking the back button dismisses the multi-select mode.

Show a visual confirmation when an action is selected. Consider displaying a confirmation dialog for certain actions, especially destructive actions such as delete.

Selection mode is confined to the page in which it is active, and can't affect any items outside of that page.

The entry point to selection mode should be juxtaposed against the content it affects.

For command bar recommendations, see [guidelines for command bars](app-bars.md).

## Globalization and localization checklist

<table>
<tr>
<th>Wrapping</th><td>Allow two lines for the list label.</td>
</tr>
<tr>
<th>Horizontal expansion</th><td>Make sure fields can accomdation text expension and are scrollable.</td>
</tr>
<tr>
<th>Vertical spacing</th><td>Use non-Latin chracters for vertical spacing to ensure non-Latin scripts will display properly.</td>
</tr>
</table>


## Related articles

- [Hub](hub.md)
- [Master/details](master-details.md)
- [Nav pane](navigationview.md)
- [Semantic zoom](semantic-zoom.md)
- [Drag and drop](https://msdn.microsoft.com/windows/uwp/app-to-app/drag-and-drop)
- [Thumbnail images](../../files/thumbnails.md)

**For developers**
- [ListView class](https://msdn.microsoft.com/library/windows/apps/br242878)
- [GridView class](https://msdn.microsoft.com/library/windows/apps/br242705)
- [ComboBox class](https://msdn.microsoft.com/library/windows/apps/br209348)
- [ListBox class](https://msdn.microsoft.com/library/windows/apps/br242868)