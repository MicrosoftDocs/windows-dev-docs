---
title: ListHelpers.FilterList<T>(IEnumerable<T>, String, Func<String, T, Integer>) Method
description: The ListHelpers.FilterList helper method filters a list of items based on the provided query string and scoring function.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ListHelpers.FilterList\<T\>(IEnumerable\<T\>, String, Func\<String, T, Integer\>) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **FilterList** method filters a list of items based on a query string and a scoring function. It returns a new list containing only the items that match the query, sorted by their score.

## Parameters

*items* **IEnumerable\<T\>**

The list of items to filter.

*query* **String**

The query string used to filter the items. The filtering is case-insensitive and checks if the query is contained within the item's name or description.

*scoreFunction* **Func\<String, T, Integer\>**

The function used to calculate the score of each item based on the query. The score is used to sort the filtered items.

## Returns

An **IEnumerable\<T\>** containing the filtered items that match the query, sorted by their score in descending order. The items with a higher score appear first in the list.
