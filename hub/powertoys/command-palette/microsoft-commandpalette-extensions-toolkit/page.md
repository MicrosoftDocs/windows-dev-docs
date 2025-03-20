---
title: Page Class definition
description: The Page class is used to represent an additional nested page in the PowerToys Command Palette.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# Page Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Inherits [Command](command.md)

Implements [IPage](../microsoft-commandpalette-extensions/ipage.md)

Represents an additional "nested" page within the Command Palette.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| AccentColor | [OptionalColor](../microsoft-commandpalette-extensions/optionalcolor.md) | The accent color of the page. This property is used to set the color of the page header and other UI elements. |
| IsLoading | **Boolean** | Gets or sets a value indicating whether the page is loading. This property is used to show a loading indicator while the page is being loaded. |
| Title | **String** | The title of the page. This property is used to set the text displayed in the page header. |
