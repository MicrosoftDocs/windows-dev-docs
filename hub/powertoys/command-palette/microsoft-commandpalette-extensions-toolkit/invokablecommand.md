---
title: InvokableCommand Class
description: The InvokableCommand class is a specialized command that can be invoked directly from the command palette.
ms.date: 2/11/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# InvokableCommand Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [Command](command.md)

Implements [IInvokableCommand](../microsoft-commandpalette-extensions/iinvokablecommand.md)

The **InvokableCommand** class is a specialized command that can be invoked directly from the command palette. It provides a way to execute commands without requiring additional user input or confirmation.

## Methods

| Method | Description |
| :--- | :--- |
| [Invoke()](invokablecommand_invoke.md) | Keeps the command palette open after the command is invoked. |
| [Invoke(Object)](invokablecommand_invoke_object.md) | Keeps the command palette open after the command is invoked with an object parameter. |
