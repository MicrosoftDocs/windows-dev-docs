---
title: ICommandItem Interface
description: The ICommandItem interface is used to represent a command item in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ICommandItem Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **ICommandItem** interface is used to represent a command item in the Command Palette. It is used to define the properties and methods that a command item must implement in order to be displayed in the Command Palette.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Command | [ICommand](icommand.md) | The command associated with this item. This is the command that will be executed when the item is activated. |
| Icon | [IIconInfo](iiconinfo.md) | The icon associated with this item. This icon will be displayed next to the item in the Command Palette. |
| MoreCommands | [IContextItem[]](icontextitem.md) | An array of additional commands that can be displayed in a submenu. This property is used to define a list of related commands that can be executed from the Command Palette. |
| Subtitle | **String** | The subtitle associated with this item. This subtitle will be displayed below the item in the Command Palette. |
| Title | **String** | The title associated with this item. This title will be displayed as the main text of the item in the Command Palette. |

## Remarks

If an **ICommandItem** in a context menu has **MoreCommands**, then activating it will open a submenu with those items. If an **ICommandItem** in a context menu has **MoreCommands** and a non-null **Command**, then activating it will open a submenu with the **Command** first (following the same rules for building a context item from a default **Command**), followed by the items in **MoreCommands**.

When displaying a page:

- The title will be `IPage.Title ?? ICommand.Name`
- The icon will be `ICommand.Icon`
