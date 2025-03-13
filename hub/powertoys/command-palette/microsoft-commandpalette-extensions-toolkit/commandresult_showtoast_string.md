---
title: CommandResult.ShowToast(String) Method
description: 
ms.date: 2/11/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandResult.ShowToast(String) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Display a transient desktop-level message to the user. This is especially useful for displaying confirmation that an action took place when the palette will be closed. Consider the [CopyTextCommand](copytextcommand.md) in the helpers - this command will show a toast with the text "Copied to clipboard", then dismiss the palette.

Creates a new [CommandResult](commandresult.md) instance with its [Kind](commandresult.md#properties) property set to [CommandResultKind.ShowToast](../microsoft-commandpalette-extensions/commandresultkind.md#fields) and its [Args](commandresult.md#properties) set to a new [ToastArgs](toastargs.md) object with its [Message](toastargs.md#properties) set to `message`.

## Parameters

**`message`** String

## Returns

[CommandResult](commandresult.md)
