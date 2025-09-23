---
title: CopyTextCommand Class
description: The CopyTextCommand class is used to define a command that copies text to the clipboard.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CopyTextCommand Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [InvokableCommand](invokablecommand.md)

The **CopyTextCommand** class is used to define a command that copies text to the clipboard. It is a specialized command that provides functionality for copying text and displaying a result message in the Command Palette.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [CopyTextCommand(String)](copytextcommand_constructor.md) | Initializes the command with a text parameter, sets its name to "Copy", and adds an icon. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Result | [CommandResult](commandresult.md) | What happens in the palette after the command is executed. Defaults to [CommandResult.ShowToast("Copied to clipboard")](commandresult_showtoast_string.md). |
| Text | **String** | Gets and sets the text of the command. |

## Methods

| Method | Description |
| :--- | :--- |
| [Invoke()](copytextcommand_invoke.md) | Sets the clipboard text to the value of **Text** and returns the **Result**. |
