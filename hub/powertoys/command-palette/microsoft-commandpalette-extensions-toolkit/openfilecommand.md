---
title: OpenFileCommand Class
description: The OpenFileCommand class is used to define a command that opens a file using the system's default application.
ms.date: 03/09/2026
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# OpenFileCommand Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [InvokableCommand](invokablecommand.md)

The **OpenFileCommand** class is used to define a command that opens a file using the system's default application.

## Constructors

| Constructor | Description |
| :--- | :--- |
| OpenFileCommand(String) | Initializes the command with a file path, sets its name to "Open", and adds a file icon. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Result | [CommandResult](commandresult.md) | What happens in the palette after the command is executed. Defaults to `CommandResult.Dismiss()`. |

## Methods

| Method | Description |
| :--- | :--- |
| Invoke() | Opens the file using shell execute and returns the **Result**. |
