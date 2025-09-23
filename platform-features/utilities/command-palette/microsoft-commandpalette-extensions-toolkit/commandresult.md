---
title: CommandResult Class
description: The CommandResult class indicates what the Command Palette should do after a command is executed. This allows commands to control the flow of the palette.
ms.date: 2/11/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandResult Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Indicates what the Command Palette should do after a command is executed. This allows commands to control the flow of the palette.

Implements [ICommandResult](../microsoft-commandpalette-extensions/icommandresult.md)

The **CommandResult** class is used to specify the result of a command execution in the Command Palette. It provides various methods to control the behavior of the Command Palette after a command is executed. This class is useful for managing navigation, displaying messages, and controlling the state of the Command Palette.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Args | [ICommandResultArgs](../microsoft-commandpalette-extensions/icommandresultargs.md) | Gets or sets the arguments associated with the command result. This can include additional data needed for the command result. |
| Kind | [CommandResultKind](../microsoft-commandpalette-extensions/commandresultkind.md) | Gets or sets the result of the command. Defaults to [CommandResultKind.Dismiss](../microsoft-commandpalette-extensions/commandresultkind.md#fields). |

## Methods

| Method | Description |
| :--- | :--- |
| [Confirm(ConfirmationArgs)](commandresult_confirm.md) | Display a confirmation dialog to the user. |
| [Dismiss()](commandresult_dismiss.md) | Close the Command Palette after the action is executed and dismiss the current state. On the next launch, the Command Palette will start from the main page with a blank query. |
| [GoBack()](commandresult_goback.md) | Navigate to the previous page, and keep it open. |
| [GoHome()](commandresult_gohome.md) | Navigate back to the main page of the Command Palette and keep it open. This clears out the current stack of pages, but keeps the palette open. |
| [GoToPage(GoToPageArgs)](commandresult_gotopage.md) | Navigate to a different page in the palette. The [GoToPageArgs](gotopageargs.md) will specify which page to navigate to. |
| [Hide()](commandresult_hide.md) | Creates a new **CommandResult** instance with **Kind** set to [CommandResultKind.Hide](../microsoft-commandpalette-extensions/commandresultkind.md#fields) and **Args** set to `null`. |
| [KeepOpen()](commandresult_keepopen.md) | Do nothing. This leaves the palette in its current state, with the current page stack and query. |
| [ShowToast(String)](commandresult_showtoast_string.md) | Display a transient desktop-level message to the user. Creates a new **CommandResult** with **Args** set to a new [ToastArgs](toastargs.md) object with its [Message](toastargs.md#properties) set to **String**. |
| [ShowToast(ToastArgs)](commandresult_showtoast_toastargs.md) | Display a transient desktop-level message to the user. Creates a new **CommandResult** instance with **Args** set to [ToastArgs](toastargs.md). |

## Example

See [Command Results](../command-results.md) for an example of how to use this.