---
title: ColorHelpers.FromRgb(Byte, Byte, Byte) Method
description: The FromRgb method creates a color from the specified RGB values.
ms.date: 2/19/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ColorHelpers.FromRgb(Byte, Byte, Byte) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **FromRgb** method creates a color from the specified RGB values. This is useful when you want to specify a color using its red, green, and blue components.

## Parameters

*r* **Byte**

The red component of the color. This value should be between `0` and `255`.

*g* **Byte**

The green component of the color. This value should be between `0` and `255`.

*b* **Byte**

The blue component of the color. This value should be between `0` and `255`.

## Returns

An [OptionalColor](../microsoft-commandpalette-extensions/optionalcolor.md) object that represents the color created from the specified RGB values. This object can be used in various UI elements, such as command items and icons, to specify the color of the element.
