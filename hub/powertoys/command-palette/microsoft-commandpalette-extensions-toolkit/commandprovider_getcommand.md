---
title: CommandProvider.GetCommand(String) Method
description: The GetCommand method retrieves a command by its unique identifier.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandProvider.GetCommand(String) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **GetCommand** method retrieves a command by its unique identifier. This method is used to access a specific command within the command provider, allowing for operations such as executing or updating the command.

## Parameters

*id* **String**

The unique identifier of the command to retrieve. This identifier is used to locate the specific command within the command provider's collection of commands.

## Returns

An [ICommand](../microsoft-commandpalette-extensions/icommand.md) object representing the command with the specified identifier. If no command with the specified identifier exists, this method returns null.
