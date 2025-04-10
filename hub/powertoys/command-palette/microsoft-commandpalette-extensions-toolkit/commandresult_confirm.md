---
title: CommandResult.Confirm(ConfirmationArgs) Method
description: The Confirm method creates a new CommandResult instance with its Kind property set to CommandResultKind.Confirm and its Args set to args.
ms.date: 2/11/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandResult.Confirm(ConfirmationArgs) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Display a confirmation dialog to the user. The [ConfirmationArgs](confirmationargs.md) will specify the title, and description for the dialog. The primary button of the dialog will activate the [Command](command.md). If [IsPrimaryCommandCritical](confirmationargs.md#properties) is `true`, the primary button will be red, indicating that it is a destructive action.

Creates a new [CommandResult](commandresult.md) instance with its [Kind](commandresult.md#properties) property set to [CommandResultKind.Confirm](../microsoft-commandpalette-extensions/commandresultkind.md#fields) and its [Args](commandresult.md#properties) set to *args*.

## Parameters

*args* [ConfirmationArgs](confirmationargs.md)

The arguments for the command. This should be an instance of [ConfirmationArgs](confirmationargs.md) that specifies the title and description for the dialog.

## Returns

A [CommandResult](commandresult.md) instance.

## Example

See [Command Results](../command-results.md) for an example of how to use this.