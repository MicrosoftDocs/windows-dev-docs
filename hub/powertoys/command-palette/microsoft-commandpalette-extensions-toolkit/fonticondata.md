---
title: FontIconData Class
description: Represents an icon that is a font glyph from a non-default font family.
ms.date: 03/09/2026
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# FontIconData Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [IconData](icondata.md)

Implements IExtendedAttributesProvider

The **FontIconData** class represents an icon that is a font glyph from a non-default font family. Command Palette defaults to Segoe Fluent Icons and Segoe MDL2 Assets for standard glyphs. Use this class only when you need a glyph from a different font.

## Constructors

| Constructor | Description |
| :--- | :--- |
| FontIconData(String, String) | Creates a font icon with the specified glyph and font family. The `glyph` parameter is the icon glyph character, and `fontFamily` is the name of the font family. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| FontFamily | **String** | Gets or sets the font family to use for rendering the icon glyph. |

## Methods

| Method | Returns | Description |
| :--- | :--- | :--- |
| GetProperties() | IDictionary\<String, Object\> | Returns a dictionary containing the font family as an extended attribute. |
