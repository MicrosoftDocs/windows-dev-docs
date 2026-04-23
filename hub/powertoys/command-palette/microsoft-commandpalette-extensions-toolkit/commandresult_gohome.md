---
title: CommandResult.GoHome() Method
description: The GoHome method creates a new CommandResult instance with its Kind property set to CommandResultKind.GoHome and its Args set to null.
ms.date: 2/11/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandResult.GoHome() Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Navigate back to the main page of the Command Palette and keep it open. This clears out the current stack of pages, but keeps the palette open.

> [!NOTE]
> If the command navigates to another application, the palette's default behavior is to hide itself when it loses focus. That will behave the same as [Dismiss](commandresult_dismiss.md) in that case.

Creates a new [CommandResult](commandresult.md) instance with its [Kind](commandresult.md#properties) property set to [CommandResultKind.GoHome](../microsoft-commandpalette-extensions/commandresultkind.md#fields) and its [Args](commandresult.md#properties) set to `null`.

## Returns

A [CommandResult](commandresult.md) instance.

## Example

See [Command Results](../command-results.md) for an example of how to use this.