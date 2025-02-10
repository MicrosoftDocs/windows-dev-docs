---
title: CommandResult Class
description: 
ms.date: 2/10/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandResult Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Implements [ICommandResult](../microsoft-commandpalette-extensions/icommandresult.md)

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Args | [ICommandResultArgs](../microsoft-commandpalette-extensions/icommandresultargs.md) | |
| Kind | [CommandResultKind](../microsoft-commandpalette-extensions/commandresultkind.md) | Gets or sets the result of the command. Defaults to [CommandResultKind.Dismiss](../microsoft-commandpalette-extensions/commandresultkind.md#fields). |

## Methods

| Method | Description |
| :--- | :--- |
| [Confirm(ConfirmationArgs)](commandresult_confirm.md) | Creates a new `CommandResult` instance with `Kind` set to [CommandResultKind.Confirm](../microsoft-commandpalette-extensions/commandresultkind.md#fields) and `Args` set to [ConfirmationArgs](confirmationargs.md). |
| [Dismiss()](commandresult_dismiss.md) | Creates a new `CommandResult` instance with `Kind` set to [CommandResultKind.Dismiss](../microsoft-commandpalette-extensions/commandresultkind.md#fields). |
| [GoBack()](commandresult_goback.md) | Creates a new `CommandResult` instance with `Kind` set to [CommandResultKind.GoBack](../microsoft-commandpalette-extensions/commandresultkind.md#fields) and `Args` set to `null`. |
| [GoHome()](commandresult_gohome.md) | Creates a new `CommandResult` instance with `Kind` set to [CommandResultKind.GoHome](../microsoft-commandpalette-extensions/commandresultkind.md#fields) and `Args` set to `null`. |
| [GoToPage(GoToPageArgs)](commandresult_gotopage.md) | Creates a new `CommandResult` instance with `Kind` set to [CommandResultKind.GoToPage](../microsoft-commandpalette-extensions/commandresultkind.md#fields) and `Args` set to [GoToPageArgs](gotopageargs.md). |
| [Hide()](commandresult_hide.md) | Creates a new `CommandResult` instance with `Kind` set to [CommandResultKind.Hide](../microsoft-commandpalette-extensions/commandresultkind.md#fields) and `Args` set to `null`. |
| [KeepOpen()](commandresult_keepopen.md) | Creates a new `CommandResult` instance with `Kind` set to [CommandResultKind.KeepOpen](../microsoft-commandpalette-extensions/commandresultkind.md#fields) and `Args` set to `null`. |
| [ShowToast(String)](commandresult_showtoast_string.md) | Creates a new [CommandResult](commandresult.md) instance with `Kind` set to [CommandResultKind.ShowToast](../microsoft-commandpalette-extensions/commandresultkind.md#fields) and `Args` set to a new [ToastArgs](toastargs.md) object with its [Message](toastargs.md#properties) set to `String`. |
| [ShowToast(ToastArgs)](commandresult_showtoast_toastargs.md) | Creates a new `CommandResult` instance with `Kind` set to [CommandResultKind.ShowToast](../microsoft-commandpalette-extensions/commandresultkind.md#fields) and `Args` set to [ToastArgs](toastargs.md). |
