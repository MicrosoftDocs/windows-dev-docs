---
title: ClipboardHelper Class
description: 
ms.date: 2/19/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ClipboardHelper Class

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions.toolkit.md)

## Properties

| Property | Type | Description |
| :--- | :--- | :--- |
| _clipboardSupported | Boolean | Is set to `true` by default. |
| _internalClipboard | String | This is used if an external clipboard is not available. This is also useful for testing in CI. |

## Methods

| Method | Description |
| :--- | :--- |
| [GetText()](clipboardhelper_gettext.md) | |
| [SetRtf(String, String)](clipboardhelper_setrtf.md) | |
| [SetText(String)](clipboardhelper_settext.md) | |
