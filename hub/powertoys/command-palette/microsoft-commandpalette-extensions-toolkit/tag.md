---
title: Tag Class definition
description: The Tag class represents a tag that can be used in the command palette. It provides properties for customizing the appearance of the tag.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# Tag Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [BaseObservable](baseobservable.md)

Implements [ITag](../microsoft-commandpalette-extensions/itag.md)

The **Tag** class represents a tag that can be used in the command palette. It provides properties for customizing the appearance of the tag, including background color, foreground color, icon, text, and tooltip.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [Tag()](tag_constructor.md#tag-constructor) | Initializes a new instance of the **Tag** class with default values. |
| [Tag(String)](tag_constructor.md#tagstring-constructor) | Initializes a new instance of the **Tag** class with a specified text. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Background | [OptionalColor](../microsoft-commandpalette-extensions/optionalcolor.md) | The background color of the tag. |
| Foreground | [OptionalColor](../microsoft-commandpalette-extensions/optionalcolor.md) | The foreground color of the tag. |
| Icon | [IIconInfo](../microsoft-commandpalette-extensions/iiconinfo.md) | The icon associated with the tag. |
| Text | **String** | The text displayed on the tag. |
| ToolTip | **String** | The tooltip text displayed when hovering over the tag. |
