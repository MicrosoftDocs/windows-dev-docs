---
title: ListPage Class definition
description: The ListPage class defines a page that displays a list of items in the PowerToys Command Palette utility.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ListPage Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Implements [Page](page.md), [IListPage](../microsoft-commandpalette-extensions/ilistpage.md)

The **ListPage** class defines a page that displays a list of items. It provides properties and methods to manage the display and interaction with the list of items.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| EmptyContent | [ICommandItem](../microsoft-commandpalette-extensions/icommanditem.md) | The content to display when the list is empty. |
| Filters | [IFilters](../microsoft-commandpalette-extensions/ifilters.md) | The filters to apply to the list of items. |
| GridProperties | [IGridProperties](../microsoft-commandpalette-extensions/igridproperties.md) | The properties for the grid layout of the list. |
| HasMoreItems | **Boolean** | Indicates if there are more items to load. |
| PlaceholderText | **String** | The placeholder for the filter on the page. |
| SearchText | **String** | The text to filter the list of items. |
| ShowDetails | **Boolean** | Indicates if the details of the items should be shown. |

## Events

| Event | Description |
| :--- | :--- |
| Windows.Foundation.TypedEventHandler\<object, [IItemsChangedEventArgs](../microsoft-commandpalette-extensions/iitemschangedeventargs.md)\> ItemsChanged | Occurs when the items in the list have changed. |

## Methods

| Method | Description |
| :--- | :--- |
| [GetItems()](listpage_getitems.md) | Returns the list of items. |
| [LoadMore()](listpage_loadmore.md) | Loads more items into the list. |
| [RaiseItemsChanged(Integer)](listpage_raiseitemschanged.md) | Raises the **ItemsChanged** event. |
