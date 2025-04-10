---
title: IFallbackCommandItem Interface
description: The IFallbackCommandItem interface represents a fallback command item in the Command Palette.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IFallbackCommandItem Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IFallbackCommandItem** interface represents a fallback command item in the Command Palette. It is used to define the properties and methods associated with a fallback command item that can be displayed in the Command Palette when no other commands match the user's input.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| FallbackHandler | [IFallbackHandler](ifallbackhandler.md) | The fallback handler associated with the command item. This property is used to define the behavior of the command item when it is selected. |
