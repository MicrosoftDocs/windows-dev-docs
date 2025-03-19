---
title: IToastArgs Interface
description: The IToastArgs interface is used to define the arguments for a toast notification in the Command Palette.
ms.date: 2/10/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IToastArgs Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

Derived [ToastArgs](../microsoft-commandpalette-extensions-toolkit/toastargs.md)

The **IToastArgs** interface is used to define the arguments for a toast notification in the Command Palette. It provides properties to specify the message and the result of the command associated with the toast.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Message | **String** | The message to be displayed in the toast notification. |
| Result | [ICommandResult](icommandresult.md) | The result of the command associated with the toast notification. This property is used to provide additional information about the command's execution. |
