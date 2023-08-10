---
title: PowerToys Always on Top utility for Windows
description: A system-wide utility for Windows that allows you to pin windows to the top of your screen.
ms.date: 08/03/2023
ms.topic: article
no-loc: [PowerToys, Windows, Always on Top, Win]
---

# Always on Top utility

A system-wide utility for Windows to pin windows above other windows.

![AlwaysOnTop screenshot.](../images/pt-always-on-top.png)

## Toggle windows to be on top

With the activation/deactivation shortcut (default: <kbd>⊞ Win</kbd>+<kbd>Ctrl</kbd>+<kbd>T</kbd>), the active window will be placed above all non-topmost windows and should stay above them, even when the window is deactivated.

## Settings

From the Settings tab, you can configure the following options:

| Setting | Description |
| :--- | :--- |
| Activation shortcut | The customizable keyboard command to turn on or off the always-on-top property for that window. |
| Do not activate when Game Mode is on | Prevents the feature from being activated when actively playing a game on the system. |
| Color mode | Choose either **Windows default** or **Custom color** for the highlight border. |
| Color | The custom color of the highlight border. |
| Border thickness (px) | The thickness of the highlight border. Measured in pixels. |
| Play a sound | Toggle playing of a short alert chirp. Activating and deactivating use different sounds. |
| Excluded apps | Add an application's name, or part of the name, one per line. (e.g. adding `Notepad` will match both `Notepad.exe` and `Notepad++.exe`; to match only `Notepad.exe`, add the `.exe` extension) |
