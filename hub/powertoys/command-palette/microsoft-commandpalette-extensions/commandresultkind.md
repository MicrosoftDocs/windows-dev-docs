---
title: CommandResultKind Enum
description: The CommandResultKind enum is used to specify the result of a command execution in the Command Palette.
ms.date: 2/10/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandResultKind Enum

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **CommandResultKind** enum is used to specify the result of a command execution in the Command Palette. It defines the different actions that can be taken after a command is executed.

## Fields

| Field | Description |
| :--- | :--- |
| Confirm | Display a confirmation dialog to the user. |
| Dismiss | Close the Command Palette after the action is executed and dismiss the current state. On the next launch, the Command Palette will start from the main page with a blank query. |
| GoBack | Navigate to the previous page, and keep it open. |
| GoHome | Navigate back to the main page of the Command Palette and keep it open. This clears out the current stack of pages, but keeps the palette open. |
| GoToPage | Navigate to a different page in the palette. The [GoToPageArgs](../microsoft-commandpalette-extensions-toolkit/gotopageargs.md) will specify which page to navigate to. |
| Hide | Keep this page open and hide the palette. |
| KeepOpen | Do nothing. This leaves the palette in its current state, with the current page stack and query. |
| ShowToast | Display a transient desktop-level message to the user. This is especially useful for displaying confirmation that an action took place when the palette will be closed. Consider the [CopyTextCommand](../microsoft-commandpalette-extensions-toolkit/copytextcommand.md) in the helpers - this command will show a toast with the text "Copied to clipboard", then dismiss the palette. |
