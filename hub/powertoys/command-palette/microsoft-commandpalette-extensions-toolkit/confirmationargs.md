---
title: ConfirmationArgs Class
description: 
ms.date: 2/10/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ConfirmationArgs Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

Implements [IConfirmationArgs](../microsoft-commandpalette-extensions/iconfirmationargs.md)

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Description | String | Gets or sets the description of the confirmation dialog. |
| IsPrimaryCommandCritical | Boolean | Gets or sets if the primary command is critical. |
| PrimaryCommand | [ICommand](../microsoft-commandpalette-extensions/icommand.md) | Gets or sets the primary action command to be executed when the user confirms the dialog. |
| Title | String | Gets or sets the title of the confirmation dialog. |
