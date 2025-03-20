---
title: IconHelpers.FromRelativePaths(String, String) Method
description: The FromRelativePaths method creates an IconInfo object from two relative paths, one for light mode and one for dark mode.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IconHelpers.FromRelativePaths(String, String) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

This method creates an [IconInfo](iconinfo.md) object from two relative paths, one for light mode and one for dark mode. This is useful for loading icons that have different appearances based on the current theme.

## Parameters

*lightIcon* **String**

The relative path to the icon for light mode. This path should point to an image file that represents the icon in a light theme.

*darkIcon* **String**

The relative path to the icon for dark mode. This path should point to an image file that represents the icon in a dark theme.

## Returns

An [IconInfo](iconinfo.md) object that contains the light and dark mode icons.
