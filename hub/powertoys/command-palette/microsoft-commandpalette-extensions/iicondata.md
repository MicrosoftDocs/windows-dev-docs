---
title: IIconData Interface
description: The IIconData interface represents the data for an icon that can be used in the Command Palette.
ms.date: 2/6/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IIconData Interface

## Definition

Namespace: [Microsoft.CommandPalette.Extensions](microsoft-commandpalette-extensions.md)

The **IIconData** interface represents the data for an icon that can be used in the Command Palette. It is used to define icons that can be displayed in the Command Palette, such as application icons, file icons, or custom icons.

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| Icon | **String** | Gets the icon. |
| Data | **Windows.Storage.Streams.IRandomAccessStreamReference** | Gets the data of the icon. |
