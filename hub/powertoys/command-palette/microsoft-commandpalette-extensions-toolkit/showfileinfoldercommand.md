---
title: ShowFileInFolderCommand Class
description: The ShowFileInFolderCommand class is used to define a command that opens File Explorer and selects the specified file.
ms.date: 03/09/2026
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ShowFileInFolderCommand Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [InvokableCommand](invokablecommand.md)

The **ShowFileInFolderCommand** class is used to define a command that opens File Explorer and selects the specified file.

## Constructors

| Constructor | Description |
| :--- | :--- |
| ShowFileInFolderCommand(String) | Initializes the command with a file path. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Result | [CommandResult](commandresult.md) | What happens in the palette after the command is executed. Defaults to `CommandResult.Dismiss()`. |

## Methods

| Method | Description |
| :--- | :--- |
| Invoke() | Opens explorer.exe with `/select` pointing to the file and returns the **Result**. |
