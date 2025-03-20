---
title: FallbackCommandItem Class
description: The FallbackCommandItem class is used to create a command item that serves as a fallback option in the command palette.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# FallbackCommandItem Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [CommandItem](commanditem.md)

Implements [IFallbackCommandItem](../microsoft-commandpalette-extensions/ifallbackcommanditem.md), [IFallbackHandler](../microsoft-commandpalette-extensions/ifallbackhandler.md)

The **FallbackCommandItem** class is used to create a command item that serves as a fallback option in the command palette. It allows you to define a command that will be executed when no other commands match the user's input.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [FallbackCommandItem(ICommand)](fallbackcommanditem_constructor.md) | Initializes a new instance of the **FallbackCommandItem** class with the specified command. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| FallbackHandler | [IFallbackHandler](../microsoft-commandpalette-extensions/ifallbackhandler.md) | Gets or sets the fallback handler that will be used to execute the command. |

## Methods

| Method | Description |
| :--- | :--- |
| [UpdateQuery(String)](fallbackcommanditem_updatequery.md) | Updates the query string for the command item. |
