---
title: PowerToys Screen ruler utility for Windows
description: Screen ruler allows you to quickly measure pixels on your screen based on image edge detection.
ms.date: 08/03/2023
ms.topic: article
no-loc: [PowerToys, Windows, Screen ruler, Win]
---

# Screen ruler utility

![Screen ruler utility](../images/pt-screen-ruler.png)

Screen ruler allows you to quickly measure pixels on your screen, based on image edge detection. This was inspired by [Pete Blois's Rooler](https://github.com/peteblois/rooler).

## How to activate

Use <kbd>⊞ Win</kbd>+<kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>M</kbd> to activate and select which tool you want to measure with. To close, use <kbd>Esc</kbd> or select &#9587; in the toolbar.

## How to use

- Bounds (dashed square symbol): This is a bounding box. Click and drag with your mouse. If you hold <kbd>Shift</kbd>, the box(es) will stay in place until you cancel the interaction.
- Spacing (&#9547;): Measure horizontal and vertical spacing at the same time. Select the symbol and move your mouse to your target location.
- Horizontal (&#9473;): Measure only horizontal spacing. Select the symbol and move your mouse pointer to your target location.
- Vertical (&#9475;): Measure only vertical spacing. Select the symbol and move your mouse pointer to your target location.
- Cancel interaction: <kbd>Esc</kbd>, &#9587; or mouse click. Upon clicking the primary mouse button, the measurement is copied to the clipboard.

The controls on the toolbar can also be selected via <kbd>Ctrl</kbd>+<kbd>1</kbd>/<kbd>2</kbd>/<kbd>3</kbd>/<kbd>4</kbd>.

> [!TIP]
> Scroll up with the mouse wheel to increase the threshold for pixel difference by 15 units per wheel tick. Effectively the measuring line can become longer. Scroll down to reverse.

## Settings

From the Settings menu, the following options can be configured:

| Setting | Description |
| :--- | :--- |
| Activation shortcut | The customizable keyboard command to turn the toolbar on or off. |
| Capture screen continuously during measuring | When off, the utility takes a single snapshot of your screen. When this is turned on, the utility will attempt real-time detection. Continuous mode will consume more resources when in use. |
| Per color channel edge detection | Test if all color channels are within a tolerance distance from each other. Otherwise, check that the sum of all color channels differences is smaller than the tolerance. |
| Pixel tolerance for edge detection | A value between 0-255. A higher value will provide a higher variation so it will be more forgiving with things like gradients and shadows. |
| Draw feet on the cross | Adds small, serif-like "feet" for additional visual recognition. |
| Line color | The color for the line that does the measuring. |

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
