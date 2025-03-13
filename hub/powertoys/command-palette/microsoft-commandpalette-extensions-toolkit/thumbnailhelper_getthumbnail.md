---
title: ThumbnailHelper.GetThumbnail(String) Method
description: The ThumbnailHelper.GetThumbnail(String) method retrieves the thumbnail image for a command in the Command Palette.
ms.date: 2/26/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ThumbnailHelper.GetThumbnail(String) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **ThumbnailHelper.GetThumbnail(String)** method retrieves the thumbnail image for a command in the Command Palette. This method is useful for enhancing the visual representation of commands by providing associated images.

## Parameters

**`path`** String

The path to the command for which the thumbnail is requested.

## Returns

A **Task\<IRandomAccessStream\>** that represents the thumbnail image for the specified command. If no thumbnail is found, the task will return null.
