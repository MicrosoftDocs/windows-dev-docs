---
title: CommandResult.KeepOpen() Method
description: The KeepOpen method does nothing and leaves the palette in its current state.
ms.date: 2/11/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandResult.KeepOpen() Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Do nothing. This leaves the palette in its current state, with the current page stack and query.

> [!NOTE]
> If the command navigates to another application, the palette's default behavior is to hide itself when it loses focus. When the user next activates the Command Palette, it will be in the same state as when it was hidden.

Creates a new [CommandResult](commandresult.md) instance with its [Kind](commandresult.md#properties) property set to [CommandResultKind.KeepOpen](../microsoft-commandpalette-extensions/commandresultkind.md#fields) and its [Args](commandresult.md#properties) set to `null`.

## Returns

A [CommandResult](commandresult.md) instance.
