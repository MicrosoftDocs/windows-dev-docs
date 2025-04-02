---
title: ClipboardHelper.SetRtf(String, String) Method
description: The SetRtf method sets the RTF (Rich Text Format) data on the clipboard.
ms.date: 2/19/2025
ms.topic: reference
no-loc: [PowerToys, Windows, Insider]
---

# ClipboardHelper.SetRtf(String, String) Method

## Definition

Namespace: [Microsoft.CommandPalette.Extensions.Toolkit](microsoft-commandpalette-extensions-toolkit.md)

The **SetRtf** method sets the RTF (Rich Text Format) data on the clipboard. This is useful for applications that need to copy formatted text to the clipboard for pasting into other applications.

## Parameters

*plainText* **String**

The plain text representation of the RTF data. This is the text that will be displayed when the RTF data is pasted into applications that do not support RTF.

*rtfText* **String**

The RTF data to set on the clipboard. This is the formatted text that will be pasted into applications that support RTF. The RTF data should be a valid RTF string, including formatting information such as font styles, colors, and sizes.
