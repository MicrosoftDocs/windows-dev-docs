---
title: ListHelpers.ScoredListItem Struct
description: The ScoredListItem struct represents a scored list item. It's used to encapsulate a list item along with its score.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ListHelpers.ScoredListItem Struct

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **ScoredListItem** struct represents a scored list item. It's used to encapsulate a list item along with its score, which indicates how well the item matches a search query.

## Fields

| Field | Type | Description |
| :--- | :--- | :--- |
| ListItem | [IListItem](../microsoft-commandpalette-extensions/ilistitem.md) | The list item being scored. |
| Score | **Integer** | The score of the list item. A higher score indicates a better match to the search query. |
