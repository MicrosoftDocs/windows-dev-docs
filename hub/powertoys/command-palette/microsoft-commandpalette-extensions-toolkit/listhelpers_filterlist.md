---
title: ListHelpers.FilterList(IEnumerable<IListItem>, String) Method
description: The ListHelpers.FilterList helper method filters a list of items based on the provided query string.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ListHelpers.FilterList(IEnumerable\<IListItem\>, String) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **FilterList** method filters a list of items based on a query string. It returns a new list containing only the items that match the query.

## Parameters

*items* IEnumerable\<[IListItem](../microsoft-commandpalette-extensions/ilistitem.md)\>

The list of items to filter.

*query* **String**

The query string used to filter the items. The filtering is case-insensitive and checks if the query is contained within the item's name or description.

## Returns

An IEnumerable\<[IListItem](../microsoft-commandpalette-extensions/ilistitem.md)\> containing the filtered items that match the query.
