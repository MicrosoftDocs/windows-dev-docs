---
title: AnonymousCommand Class
description: The AnonymousCommand class is a command that can be invoked without being associated with a specific command palette item.
ms.date: 2/27/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# AnonymousCommand Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [InvokableCommand](invokablecommand.md)

The **AnonymousCommand** class is a command that can be invoked without being associated with a specific command palette item. It is typically used for commands that do not require any parameters or additional context.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [AnonymousCommand(Action)](anonymouscommand_constructor.md) | Initializes a new instance of the **AnonymousCommand** class with the specified action. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Result | [ICommandResult](../microsoft-commandpalette-extensions/icommandresult.md) | Gets the result of the command execution. |

## Methods

| Method | Description |
| :--- | :--- |
| [Invoke()](anonymouscommand_invoke.md) | Invokes the command. |
