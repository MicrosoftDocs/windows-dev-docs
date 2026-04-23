---
title: ToastArgs Class definition
description: The ToastArgs class implements the IToastArgs interface and is used to define the arguments for a toast notification.
ms.date: 2/11/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ToastArgs Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Implements [IToastArgs](../microsoft-commandpalette-extensions/itoastargs.md)

The **ToastArgs** class implements the **IToastArgs** interface and is used to define the arguments for a toast notification. It contains properties for the message text and the result of the command.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Message | **String** | Gets or sets the text of the toast. |
| Result | [ICommandResult](../microsoft-commandpalette-extensions/icommandresult.md) | Gets or sets the result of the command. Defaults to [CommandResult.Dismiss()](commandresult_dismiss.md). |
