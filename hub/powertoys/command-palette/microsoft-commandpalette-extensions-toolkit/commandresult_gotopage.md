---
title: CommandResult.GoToPage(GoToPageArgs) Method
description: 
ms.date: 2/11/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandResult.GoToPage(GoToPageArgs) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Navigate to a different page in the palette. The [GoToPageArgs](gotopageargs.md) will specify which page to navigate to.

Creates a new [CommandResult](commandresult.md) instance with its [Kind](commandresult.md#properties) property set to [CommandResultKind.GoToPage](../microsoft-commandpalette-extensions/commandresultkind.md#fields) and its [Args](commandresult.md#properties) set to `args`.

## Parameters

**`args`** [GoToPageArgs](gotopageargs.md)

## Returns

[CommandResult](commandresult.md)
