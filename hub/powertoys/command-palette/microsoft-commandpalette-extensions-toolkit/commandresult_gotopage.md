---
title: CommandResult.GoToPage(GoToPageArgs) Method
description: The GoToPage method creates a new CommandResult instance with its Kind property set to CommandResultKind.GoToPage and its Args set to args.
ms.date: 2/11/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandResult.GoToPage(GoToPageArgs) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Navigate to a different page in the palette. The [GoToPageArgs](gotopageargs.md) will specify which page to navigate to.

Creates a new [CommandResult](commandresult.md) instance with its [Kind](commandresult.md#properties) property set to [CommandResultKind.GoToPage](../microsoft-commandpalette-extensions/commandresultkind.md#fields) and its [Args](commandresult.md#properties) set to *args*.

## Parameters

*args* [GoToPageArgs](gotopageargs.md)

The arguments for the command. This should be an instance of [GoToPageArgs](gotopageargs.md) that specifies the page to navigate to.

## Returns

A [CommandResult](commandresult.md) instance.

## Example

See [Command Results](../command-results.md) for an example of how to use this.