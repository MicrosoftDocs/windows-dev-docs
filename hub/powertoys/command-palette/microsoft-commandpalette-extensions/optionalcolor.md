---
title: OptionalColor Struct
description: The OptionalColor struct represents a color that can be either specified or not specified.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# OptionalColor Struct

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **OptionalColor** struct represents an optional color value that can be used in the Command Palette. It is designed to encapsulate a color value along with a flag indicating whether the value is present or not. This is useful for scenarios where a color may or may not be specified, allowing for more flexible and dynamic UI designs.

## Fields

| Field | Type | Description |
| :--- | :--- | :--- |
| Color | [Microsoft.CommandPalette.Extensions.Color](color.md) | The color value. This field is used to store the actual color when it is specified. |
| HasValue | **Boolean** | Indicates whether the color value is present or not. |
