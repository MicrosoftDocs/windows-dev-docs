---
title: IExtendedAttributesProvider Interface
description: The IExtendedAttributesProvider interface provides a map of extended properties for additional metadata in the Command Palette.
ms.date: 03/09/2026
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IExtendedAttributesProvider Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IExtendedAttributesProvider** interface provides a map of extended properties for additional metadata in the Command Palette. This can be used for providing additional metadata such as custom font families for icons.

## Methods

| Method | Returns | Description |
| :--- | :--- | :--- |
| GetProperties() | **IMap\<String, Object\>** | Returns a map of extended properties. Used for providing additional metadata such as custom font families for icons. |
