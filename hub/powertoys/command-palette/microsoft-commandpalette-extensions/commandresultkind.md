---
title: CommandResultKind Enum
description: 
ms.date: 2/6/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# CommandResultKind Enum

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

## Fields

| Field | Description |
| :--- | :--- |
| Confirm | Display a confirmation dialog. |
| Dismiss | Reset the palette to the main page and dismiss. |
| GoBack | Go back one level. |
| GoHome | Go back to the main page and keep it open. |
| GoToPage | Go to another page. [IGoToPageArgs](igotopageargs.md) will define where. |
| Hide | Keep this page open and hide the palette. |
| KeepOpen | Do nothing. |
| ShowToast | Display a transient message to the user. |
