---
title: AnonymousCommand.Invoke() Method
description: The Invoke method executes the command associated with the AnonymousCommand instance.
ms.date: 2/27/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# AnonymousCommand.Invoke() Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **Invoke** method executes the command associated with the **AnonymousCommand** instance. This method does not require any parameters and can be called directly to trigger the command's action.

## Returns

An [ICommandResult](../microsoft-commandpalette-extensions/icommandresult.md) object that represents the result of the command execution. The result may contain information about the success or failure of the command, as well as any relevant data returned by the command's action.
