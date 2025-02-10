---
title: CopyTextCommand Class
description: 
ms.date: 2/10/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CopyTextCommand Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Implements [InvokableCommand](invokablecommand.md)

## Constructors

| Constructor | Description |
| :--- | :--- |
| [CopyTextCommand(String)](copytextcommand_constructor.md) | Initializes the command with a text parameter, sets its name to "Copy", and adds an icon. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Result | [CommandResult](commandresult.md) | What happens in the palette after the command is executed. Defaults to [CommandResult.Dismiss()](commandresult_dismiss.md). |
| Text | String | Gets and sets the text of the command. |

## Methods

| Method | Description |
| :--- | :--- |
| [Invoke()](copytextcommand_invoke.md) | Sets the clipboard text to the value of `Text` and returns the `Result`. |
