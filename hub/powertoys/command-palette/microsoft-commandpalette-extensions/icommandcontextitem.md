---
title: ICommandContextItem Interface
description: The ICommandContextItem interface is used to represent a context menu item in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ICommandContextItem Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **ICommandContextItem** interface is used to represent a context menu item in the Command Palette. It is used to define the properties and methods that a context menu item must implement in order to be displayed in the Command Palette.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| IsCritical | **Boolean** | Makes the item red to indicate that it's critical and requires attention. |
| RequestedShortcut | [KeyChord](keychord.md) | The shortcut that was requested for this item. This property is used to define the shortcut that will be used to activate the item in the Command Palette. |

## Remarks

When displaying a **IListItem**'s default **Command** as a context menu item, a new **ICommandContextItem** is created.
