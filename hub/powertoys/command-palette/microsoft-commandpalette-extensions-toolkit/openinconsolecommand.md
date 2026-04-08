---
title: OpenInConsoleCommand Class
description: The OpenInConsoleCommand class is used to define a command that opens a file or directory location in a console window (cmd.exe).
ms.date: 03/09/2026
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# OpenInConsoleCommand Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [InvokableCommand](invokablecommand.md)

The **OpenInConsoleCommand** class is used to define a command that opens a file or directory location in a console window (cmd.exe).

## Constructors

| Constructor | Description |
| :--- | :--- |
| OpenInConsoleCommand(String) | Initializes the command with a path. |

## Static Methods

| Method | Returns | Description |
| :--- | :--- | :--- |
| FromDirectory(String) | OpenInConsoleCommand | Creates a command that opens a console at the specified directory. |
| FromFile(String) | OpenInConsoleCommand | Creates a command that opens a console at the file's parent directory. |

## Methods

| Method | Description |
| :--- | :--- |
| Invoke() | Opens cmd.exe at the target directory and returns `CommandResult.Dismiss()`. |
