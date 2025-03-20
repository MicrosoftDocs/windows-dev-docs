---
title: ItemsChangedEventArgs Class
description: The ItemsChangedEventArgs class represents the event arguments for the items changed event in the Command Palette Extensions Toolkit.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ItemsChangedEventArgs Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Implements [IItemsChangedEventArgs](../microsoft-commandpalette-extensions/iitemschangedeventargs.md)

The **ItemsChangedEventArgs** class is used to represent the event arguments for the items changed event in the Command Palette Extensions Toolkit. It contains information about the total number of items that have changed.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [ItemsChangedEventArgs(Integer)](itemschangedeventargs_constructor.md) | Initializes a new instance of the **ItemsChangedEventArgs** class with the specified total number of items. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| TotalItems | **Integer** | Gets the total number of items that have changed. This property is read-only. |
