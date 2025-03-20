---
title: InvokableCommand.Invoke() Method
description: The Invoke method executes the command associated with the InvokableCommand instance and keeps the command palette open.
ms.date: 2/10/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# InvokableCommand.Invoke Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **Invoke** method is used to execute the command associated with the [InvokableCommand](invokablecommand.md) instance. This method is typically called when the command is triggered by the user. Keeps the command palette open after the command is invoked.

## Returns

A [CommandResult.KeepOpen()](commandresult_keepopen.md) object that indicates the command palette should remain open after the command is executed. This allows the user to continue interacting with the command palette without having to reopen it.
