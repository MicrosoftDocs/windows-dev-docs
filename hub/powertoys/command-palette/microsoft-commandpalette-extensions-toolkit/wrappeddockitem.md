---
title: WrappedDockItem Class
description: Helper class for creating a dock band out of a set of items.
ms.date: 03/09/2026
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# WrappedDockItem Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [CommandItem](commanditem.md)

The **WrappedDockItem** class is a helper class for creating a dock band out of a set of items. This allows you to instantiate buttons as ListItems, then pass them in to create a band.

## Constructors

| Constructor | Description |
| :--- | :--- |
| WrappedDockItem(ICommand, String) | Creates a dock band from a single command. The `command` parameter is the command to wrap, and `displayTitle` is the title shown in the dock. |
| WrappedDockItem(IListItem[], String, String) | Creates a dock band from a set of list items. The `items` parameter is the list items to display as buttons, `id` is the unique identifier for the band, and `displayTitle` is the title shown in the dock. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Title | **String** | Gets the display title of the dock band. |
| Command | [ICommand](../microsoft-commandpalette-extensions/icommand.md) | Gets the backing command for the dock band (internally a ListPage). |
| Items | [IListItem[]](../microsoft-commandpalette-extensions/ilistitem.md) | Gets or sets the list items displayed in the band. |
