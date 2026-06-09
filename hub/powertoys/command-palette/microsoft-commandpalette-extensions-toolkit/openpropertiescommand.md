---
title: OpenPropertiesCommand Class
description: The OpenPropertiesCommand class is used to define a command that opens the Windows file properties dialog for a file.
ms.date: 03/09/2026
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# OpenPropertiesCommand Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [InvokableCommand](invokablecommand.md)

The **OpenPropertiesCommand** class is used to define a command that opens the Windows file properties dialog for a file.

## Constructors

| Constructor | Description |
| :--- | :--- |
| OpenPropertiesCommand(String) | Initializes the command with a file path. |

## Methods

| Method | Description |
| :--- | :--- |
| Invoke() | Shows the file properties dialog and returns `CommandResult.Dismiss()`. |
