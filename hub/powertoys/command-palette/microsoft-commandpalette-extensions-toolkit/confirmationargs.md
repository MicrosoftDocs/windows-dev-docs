---
title: ConfirmationArgs Class
description: The ConfirmationArgs class is used to create a confirmation dialog and allows you to specify its details in the Command Palette.
ms.date: 2/10/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ConfirmationArgs Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Implements [IConfirmationArgs](../microsoft-commandpalette-extensions/iconfirmationargs.md)

The **ConfirmationArgs** class is used to create a confirmation dialog in the Command Palette. It allows you to specify the details for the confirmation dialog. This class is useful when you want to prompt the user for confirmation before executing a potentially destructive action.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Description | **String** | Gets or sets the description of the confirmation dialog. |
| IsPrimaryCommandCritical | **Boolean** | Gets or sets if the primary command is critical. |
| PrimaryCommand | [ICommand](../microsoft-commandpalette-extensions/icommand.md) | Gets or sets the primary action command to be executed when the user confirms the dialog. |
| Title | **String** | Gets or sets the title of the confirmation dialog. |
