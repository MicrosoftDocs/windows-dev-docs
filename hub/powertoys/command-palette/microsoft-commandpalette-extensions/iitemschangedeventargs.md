---
title: IItemsChangedEventArgs Interface
description: The IItemsChangedEventArgs interface represents the arguments for the event that is raised when the items in the Command Palette change.
ms.date: 2/6/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IItemsChangedEventArgs Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IItemsChangedEventArgs** interface represents the arguments for the event that is raised when the items in the Command Palette change. It provides information about the number of items that have been added, removed, or modified in the list.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| TotalItems | **Int32** | Gets the number of items changed. |
