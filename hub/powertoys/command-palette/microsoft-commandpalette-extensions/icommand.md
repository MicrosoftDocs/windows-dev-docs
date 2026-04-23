---
title: ICommand Interface
description: Action a user can take within the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ICommand Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

Action a user can take within the Command Palette.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Icon | [IIconInfo](iiconinfo.md) | Gets the icon of the command. |
| Id | **String** | Gets the ID of the command. This is optional but can help support more efficient command lookup in [ICommandProvider.GetCommand()](icommandprovider_getcommand.md). |
| Name | **String** | Gets the name of the command. |

## Example

See the [Add a command](../adding-commands.md) page for an example of how to use this interface.
