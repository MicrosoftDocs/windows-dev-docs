---
title: OpenWithCommand Class
description: The OpenWithCommand class is used to define a command that opens the Windows "Open with" dialog for a file.
ms.date: 03/09/2026
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# OpenWithCommand Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [InvokableCommand](invokablecommand.md)

The **OpenWithCommand** class is used to define a command that opens the Windows "Open with" dialog for a file.

## Constructors

| Constructor | Description |
| :--- | :--- |
| OpenWithCommand(String) | Initializes the command with a file path. |

## Methods

| Method | Description |
| :--- | :--- |
| Invoke() | Opens the "Open with" dialog and returns `CommandResult.GoHome()`. |
