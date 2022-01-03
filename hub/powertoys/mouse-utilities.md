---
title: PowerToys Mouse utilities for Windows
description: A collection of utilities to expand the range of usage for the mouse and cursor
ms.date: 10/28/2021
ms.topic: article
ms.localizationpriority: medium
no-loc: [PowerToys, Windows]
---

# Mouse utilities

Mouse utilities are a collection of features that enhance mouse and cursor functionality on Windows. Currently, the collection consists of:

- Find My Mouse
- Mouse Highlighter

## Find My Mouse

Double press the left <kbd>ctrl</kbd> key to activate a spotlight that focuses on the cursor's position. Click the mouse or press any keyboard key to dismiss it. If you move the mouse while the spotlight is active, the spotlight will dismiss on its own shortly after the mouse stops moving.

![Screenshot of Find my mouse](../images/pt-mouse-utilities-find-my-mouse.gif)

### Find My Mouse settings

From the settings menu, the following options can be configured:

| Setting | Description |
| --- | --- |
| Do not activate when Game Mode is on | Prevents the spotlight from being used when actively playing a game on the system |
| Overlay opacity | The percentage of opacity of the spotlight animation (default: 50%) |
| Background color | The color of the spotlight backdrop (default: #000000) |
| Spotlight color | The color of the circle that centers on the cursor (default: #FFFFFF) |
| Spotlight radius | The radius of the circle that centers on the cursor - Measured in pixels (default: 100) |
| Spotlight initial zoom | The spotlight animation's zoom factor. The higher the value, the more pronounced the zoom animation as the spotlight focuses in on the cursor position. |
| Animation duration | How long it takes for the spotlight to appear/disappear - Measured in milliseconds (default: 500) |


## Mouse Highlighter

Display visual indicators when the left or right mouse buttons are clicked. By default, mouse highlighting can be turned on and off with the <kbd>Win</kbd> + <kbd>Shift</kbd> + <kbd>H</kbd> shortcut.

### Find my mouse settings

![Screenshot of Mouse Highlighter](../images/pt-mouse-highlighter.gif)

From the settings menu, the following options can be configured:

| Setting | Description |
| --- | --- |
| Activation shortcut | The customizable keyboard command to turn on or off mouse highlighting |
| Left button highlight color | The color of the highlight when the left mouse button is clicked |
| Right button highlight color | The color of the highlight when the right mouse button is clicked |
| Overlay opacity | The opacity of the highlight animation |
| Radius | The radius of the highlight animations - Measured in pixels |
| Fade delay | How long it takes before a highlight starts to disappear - Measured in milliseconds |
| Fade duration | Duration of the disappear animation - Measured in milliseconds |
