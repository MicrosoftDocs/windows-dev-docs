---
title: CopyPathCommand Class
description: The CopyPathCommand class is used to define a command that copies a file path to the clipboard.
ms.date: 03/09/2026
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CopyPathCommand Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [InvokableCommand](invokablecommand.md)

The **CopyPathCommand** class is used to define a command that copies a file path to the clipboard. It is a specialized command that provides functionality for copying a file path and displaying a result message in the Command Palette.

## Constructors

| Constructor | Description |
| :--- | :--- |
| CopyPathCommand(String) | Initializes the command with a file path, sets its name to "Copy path", and adds a copy icon. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Result | [CommandResult](commandresult.md) | What happens in the palette after the command is executed. Defaults to showing a toast with "Copied to clipboard". |

## Methods

| Method | Description |
| :--- | :--- |
| Invoke() | Copies the path to the clipboard and returns the **Result**. Shows an error toast if copying fails. |
