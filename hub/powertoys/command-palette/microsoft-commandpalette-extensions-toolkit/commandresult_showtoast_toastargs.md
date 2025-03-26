---
title: CommandResult.ShowToast(ToastArgs) Method
description: The ShowToast method displays a transient desktop-level message to the user, accepting a ToastArgs object.
ms.date: 2/11/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandResult.ShowToast(ToastArgs) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Display a transient desktop-level message to the user. This is especially useful for displaying confirmation that an action took place when the palette will be closed. Consider the [CopyTextCommand](copytextcommand.md) in the helpers - this command will show a toast with the text "Copied to clipboard", then dismiss the palette.

Creates a new [CommandResult](commandresult.md) instance with its [Kind](commandresult.md#properties) property set to [CommandResultKind.ShowToast](../microsoft-commandpalette-extensions/commandresultkind.md#fields) and its [Args](commandresult.md#properties) set to *args*.

## Parameters

*args* [ToastArgs](toastargs.md)

The arguments for the toast message. This includes the message to be displayed and any other relevant information.

## Returns

A [CommandResult](commandresult.md) instance.
