---
title: ICommandProvider.GetCommand(String) Method
description: The GetCommand method is used to retrieve a command by its ID.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ICommandProvider.GetCommand(String) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **GetCommand** method is used to retrieve a command by its ID. This method is used to get a command that has been registered with the Command Palette.

## Parameters

*id* **String**

The ID of the command to retrieve. The ID is a unique identifier for the command and is used to identify the command in the Command Palette.

## Returns

An [ICommand](icommand.md) that contains the command that was registered with the Command Palette. If the command is not found, this method returns null.
