---
title: ITag Interface
description: The ITag interface represents a tag in the Command Palette. Tags are used to categorize and organize commands, making them easier for users to find and execute.
ms.date: 2/7/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ITag Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **ITag** interface represents a tag in the Command Palette. Tags are used to categorize and organize commands, making them easier for users to find and execute.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Background | [OptionalColor](optionalcolor.md) | The background color of the tag. This property is used to visually distinguish the tag from other elements in the Command Palette. |
| Foreground | [OptionalColor](optionalcolor.md) | The foreground color of the tag. This property is used to set the text color of the tag, ensuring good contrast with the background color. |
| Icon | [IIconInfo](iiconinfo.md) | The icon associated with the tag. This property is used to provide a visual representation of the tag, making it easier for users to identify its purpose. |
| Text | **String** | The text displayed on the tag. This property is used to provide a label for the tag, indicating its category or purpose. |
| ToolTip | **String** | The tooltip text displayed when the user hovers over the tag. This property is used to provide additional information about the tag, helping users understand its purpose. |
