---
title: PowerToys Always On Top utility for Windows
description: A system-wide utility for Windows that allows you to pin windows to the top of your screen.
ms.date: 08/03/2023
ms.topic: article
no-loc: [PowerToys, Windows, Always On Top, Win]
---

# Always On Top utility

A system-wide Windows utility to pin windows above other windows.

![Always On Top screenshot.](../images/pt-always-on-top.png)

## Pin a window

When you activate Always On Top (default: <kbd>⊞ Win</kbd>+<kbd>Ctrl</kbd>+<kbd>T</kbd>), the utility pins the active window above all other windows. The pinned window stays on top, even when you select other windows.

## Settings

Always On Top has the following settings:

| Setting | Description |
| :--- | :--- |
| **Activation shortcut** | The customizable keyboard command to turn on or off the always-on-top property for that window. |
| **Do not activate when Game Mode is on** | Prevents the feature from being activated when actively playing a game on the system. |
| **Show a border around the pinned window** | When **On**, this option shows a colored border around the pinned window.  |
| **Color mode** | Choose either **Windows default** or **Custom color** for the highlight border. |
| **Color** | The custom color of the highlight border. **Color** is only available when **Color mode** is set to **Custom color**. |
| **Opacity (%)** | The opacity of the highlight border. |
| **Thickness (px)** | The thickness of the highlight border in pixels. |
| **Enable round corners** | When selected, the highlight border around the pinned window will have rounded corners.  |
| **Play a sound when pinning a window** | When selected, this option plays a sound when you activate or deactivate Always On Top. |
| **Excluded apps** | Prevents you from pinning an application using Always On Top. Add an application's name to stop it from being pinned. The list will also exclude partial matches. For example, `Notepad` will prevent both Notepad.exe and Notepad++.exe from being pinned. To only prevent a specific application, add `Notepad.exe` to the excluded list. |
