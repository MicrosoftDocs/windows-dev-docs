---
title: ListPage Class
description: 
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ListPage Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Implements [Page](page.md), [IListPage](../microsoft-commandpalette-extensions/ilistpage.md)

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| EmptyContent | [ICommandItem](../microsoft-commandpalette-extensions/icommanditem.md) | |
| Filters | [IFilters](../microsoft-commandpalette-extensions/ifilters.md) | |
| GridProperties | [IGridProperties](../microsoft-commandpalette-extensions/igridproperties.md) | |
| HasMoreItems | Boolean | |
| PlaceholderText | String | |
| SearchText | String | |
| ShowDetails | Boolean | |

## Events

| Event | Description |
| :--- | :--- |
| Windows.Foundation.TypedEventHandler<object, [IItemsChangedEventArgs](../microsoft-commandpalette-extensions/iitemschangedeventargs.md)> ItemsChanged | |

## Methods

| Method | Description |
| :--- | :--- |
| [GetItems()](listpage_getitems.md) | |
| [LoadMore()](listpage_loadmore.md) | |
| [RaiseItemsChanged(Integer)](listpage_raiseitemschanged.md) | |