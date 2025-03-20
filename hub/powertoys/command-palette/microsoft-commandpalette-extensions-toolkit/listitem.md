---
title: ListItem Class definition
description: The ListItem class represents an item in a list. It is used to display items in the command palette.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ListItem Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [CommandItem](commanditem.md)

Implements [IListItem](../microsoft-commandpalette-extensions/ilistitem.md)

The **ListItem** class represents an item in a list. It is used to display items in the command palette and provides properties and methods for managing the item's appearance and behavior.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [ListItem(ICommand)](listitem_constructor.md#listitemicommand-constructor) | Initializes a new instance of the **ListItem** class with the specified command. |
| [ListItem(ICommandItem)](listitem_constructor.md#listitemicommanditem-constructor) | Initializes a new instance of the **ListItem** class with the specified command item. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Details | [IDetails](../microsoft-commandpalette-extensions/idetails.md) | Provides additional details about the item. |
| Section | **String** | The section to which the item belongs. This property is used to group items in the list. |
| Tags | [ITag[]](../microsoft-commandpalette-extensions/itag.md) | The tags associated with the item. Tags are used to categorize items and can be used for filtering. |
| TextToSuggest | **String** | The text to suggest for the item. This property is used to provide suggestions for the item when the user types in the command palette. |
