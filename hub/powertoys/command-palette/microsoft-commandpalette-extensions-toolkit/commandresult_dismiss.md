---
title: CommandResult.Dismiss() Method
description: The Dismiss method creates a new CommandResult instance with its Kind property set to CommandResultKind.Dismiss and its Args set to null.
ms.date: 2/10/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandResult.Dismiss() Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Close the Command Palette after the action is executed and dismiss the current state. On the next launch, the Command Palette will start from the main page with a blank query.

Creates a new [CommandResult](commandresult.md) instance with its [Kind](commandresult.md#properties) property set to [CommandResultKind.Dismiss](../microsoft-commandpalette-extensions/commandresultkind.md#fields).

## Returns

A [CommandResult](commandresult.md) instance.

## Example

See [Command Results](../command-results.md) for an example of how to use this.