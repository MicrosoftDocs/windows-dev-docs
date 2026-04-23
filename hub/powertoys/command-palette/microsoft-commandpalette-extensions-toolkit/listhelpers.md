---
title: ListHelpers Class definition
description: The ListHelpers class provides static methods for working with lists of items in the command palette.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ListHelpers Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **ListHelpers** class provides static methods for working with lists of items in the command palette. It includes methods for filtering, scoring, and updating lists of items.

## Methods

| Method | Description |
| :--- | :--- |
| [FilterList(IEnumerable\<IListItem\>, String)](listhelpers_filterlist.md) | Filters a list of items based on a search string. |
| [FilterList\<T\>(IEnumerable\<T\>, String, Func\<String, T, Integer\>)](listhelpers_filterlist_t.md) | Filters a list of items based on a search string and a scoring function. |
| [InPlaceUpdateList\<T\>(IList\<T\>, IEnumerable\<T\>)](listhelpers_inplaceupdatelist.md) | Updates a list of items in place. |
| [ScoreListItem(String, ICommandItem)](listhelpers_scorelistitem.md) | Scores a list item based on a search string and a command item. |

## Structs

| Struct | Description |
| :--- | :--- |
| [ScoredListItem](listhelpers_scoredlistitem.md) | Represents a scored list item. |
| [Scored\<T\>](listhelpers_scored.md) | Represents a scored item of type **T**. |
