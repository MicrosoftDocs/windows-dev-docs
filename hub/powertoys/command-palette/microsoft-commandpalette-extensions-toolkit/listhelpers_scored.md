---
title: ListHelpers.Scored<T> Struct
description: The Scored<T> struct represents a scored item of type T. It's used to encapsulate an item along with its score.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ListHelpers.Scored\<T\> Struct

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **Scored\<T\>** struct represents a scored item of type **T**. It's used to encapsulate an item along with its score, which indicates how well the item matches a search query.

## Fields

| Field | Type | Description |
| :--- | :--- | :--- |
| Item | **T** | The item being scored. |
| Score | **Integer** | The score of the item. A higher score indicates a better match to the search query. |
