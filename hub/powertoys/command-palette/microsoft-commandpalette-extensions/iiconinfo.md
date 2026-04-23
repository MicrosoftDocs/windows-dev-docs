---
title: IIconInfo Interface
description: The IIconInfo interface represents an icon that can be used in the Command Palette.
ms.date: 2/6/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IIconInfo Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IIconInfo** interface represents an icon that can be used in the Command Palette. It is used to define icons that can be displayed in the Command Palette, such as application icons, file icons, or custom icons.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Light | [IIconData](iicondata.md) | Gets the light mode version of the icon. |
| Dark | [IIconData](iicondata.md) | Gets the dark mode version of the icon. |
