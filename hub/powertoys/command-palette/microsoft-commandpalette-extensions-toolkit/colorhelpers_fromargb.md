---
title: ColorHelpers.FromArgb(Byte, Byte, Byte, Byte) Method
description: The FromArgb method creates a color from the specified ARGB values.
ms.date: 2/19/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ColorHelpers.FromArgb(Byte, Byte, Byte, Byte) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **FromArgb** method creates a color from the specified ARGB values. This is useful when you want to specify a color using its alpha, red, green, and blue components.

## Parameters

*a* **Byte**

The alpha component of the color. This value should be between `0` and `255`, where `0` is fully transparent and `255` is fully opaque.

*r* **Byte**

The red component of the color. This value should be between `0` and `255`.

*g* **Byte**

The green component of the color. This value should be between `0` and `255`.

*b* **Byte**

The blue component of the color. This value should be between `0` and `255`.

## Returns

An [OptionalColor](../microsoft-commandpalette-extensions/optionalcolor.md) object that represents the color created from the specified ARGB values. This object can be used in various UI elements, such as command items and icons, to specify the color of the element.
