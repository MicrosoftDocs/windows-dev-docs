---
title: PowerToys Screen ruler utility for Windows
description: Screen ruler allows you to quickly measure pixels on your screen based with image edge detection.
ms.date: 04/27/2022
ms.topic: article
no-loc: [PowerToys, Windows, Screen ruler, Win]
---

# Screen ruler utility

Screen ruler allows you to quickly measure pixels on your screen based with image edge detection. This was inspired by [Pete Blois's Rooler](https://github.com/peteblois/rooler).

## How to activate

Just press <kbd>⊞ Win</kbd>+<kbd>Shift</kbd>+<kbd>T</kbd> to activate and then select which tool you want to measure with.  To exit, just hit <kbd>Esc</kbd> or click the toolbar.

## Settings

From the Settings menu, the following options can be configured:

| Setting | Description |
| :--- | :--- |
| Activation shortcut | The customizable keyboard command to turn on or off the toolbar. |
| Capture screen continuously during measuring | When off, the utility takes a single snapshot of your screen. When this is turned on, the utility will attempt real-time detection. This mode will consume more resources when in use. In addition, the UI will be slightly offset from the pointer position. |
| Per color channel edge detection | Test all color channels are within a tolerance distance from each other. Otherwise, check that the sum of all color channels differences is smaller than the tolerance. |
| Pixel tolerance for edge detection | A value between 0-255.  A higher value will provide a higher variation so it will be more forgiving with things like gradients and shadows. |
| Draw feet on the cross | Adds small feet for additional visual capture.  Note this is off when doing continuous capture. |
| Line color | The color for the line that does the measuring. |