---
title: IconHelpers.FromRelativePath(String) Method
description: The FromRelativePath method creates an IconInfo object from a relative path.
ms.date: 2/25/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# IconHelpers.FromRelativePath(String) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

This method creates an [IconInfo](iconinfo.md) object from a relative path. This is useful for loading icons that have different appearances based on the current theme.

## Parameters

*path* **String**

The relative path to the icon. This path should point to an image file can be used for both light and dark themes.

## Returns

An [IconInfo](iconinfo.md) object that contains the light and dark mode icons.
