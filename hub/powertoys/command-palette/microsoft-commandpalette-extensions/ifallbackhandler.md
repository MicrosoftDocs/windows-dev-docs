---
title: IFallbackHandler Interface
description: The IFallbackHandler interface is used to handle fallback scenarios in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IFallbackHandler Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IFallbackHandler** interface is used to handle fallback scenarios in the Command Palette. This is commonly used for commands that want to allow the user to search for something that they've typed that doesn't match any of the commands in the list.

## Methods

| Method | Description |
| :--- | :--- |
| [UpdateQuery(String)](ifallbackhandler_updatequery.md) | Updates the query string used for fallback handling. This method is called when the user types a query that doesn't match any of the commands in the Command Palette. |
