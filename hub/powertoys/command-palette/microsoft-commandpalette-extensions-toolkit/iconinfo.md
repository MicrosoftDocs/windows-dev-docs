---
title: IconInfo Class
description: The IconInfo class is used to define icons for commands in the Command Palette.
ms.date: 2/27/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IconInfo Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

Implements [IIconInfo](../microsoft-commandpalette-extensions/iiconinfo.md)

The **IconInfo** class is used to define icons for commands in the Command Palette. It allows you to specify different icons for light and dark modes, ensuring that your icons are visually appealing and consistent with the user's theme preferences.

## Constructors

| Constructor | Description |
| :--- | :--- |
| [IconInfo(IconData, IconData)](iconinfo_constructor.md#iconinfoicondata-icondata-constructor) | Initializes the icon with a dark mode and a light mode version. |
| [IconInfo(String)](iconinfo_constructor.md#iconinfostring-constructor) | Initializes the icon with one version of the icon, used for both light and dark modes. |

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Dark | [IconData](icondata.md) | Gets or sets the dark mode version of the icon. |
| Light | [IconData](icondata.md) | Gets or sets the light mode version of the icon. |
