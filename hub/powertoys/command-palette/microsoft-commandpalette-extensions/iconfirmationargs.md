---
title: IConfirmationArgs Interface
description: The IConfirmationArgs interface is used to define the arguments for a confirmation dialog in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IConfirmationArgs Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

Derived [ConfirmationArgs](../microsoft-commandpalette-extensions-toolkit/confirmationargs.md)

The **IConfirmationArgs** interface is used to define the arguments for a confirmation dialog in the Command Palette. This interface is used to provide additional information or resources related to the item being displayed in the Command Palette.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Description | **String** | The description of the confirmation dialog. |
| IsPrimaryCommandCritical | **Boolean** | Indicates whether the primary command is critical. |
| PrimaryCommand | [ICommand](icommand.md) | The primary command associated with the confirmation dialog. |
| Title | **String** | The title of the confirmation dialog. |
