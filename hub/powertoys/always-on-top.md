---
title: PowerToys Always On Top Utility for Windows
description: Learn how to use PowerToys Always On Top utility to pin windows above others in Windows. Keep important windows visible with customizable shortcuts and settings.
ms.date: 08/20/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, Always On Top, Win]
# Customer intent: As a Windows power user, I want to learn about the Always On Top utility in PowerToys.
---

# Always On Top utility

PowerToys Always On Top is a system-wide Windows utility that allows you to pin windows above other windows. This utility helps you keep important windows visible at all times, improving your productivity by ensuring critical information stays accessible while you work with other applications.

:::image type="content" source="images/always-on-top/always-on-top.png" alt-text="Screenshot of PowerToys Always On Top utility showing a pinned window with colored border highlighting.":::

## Pin a window

When you activate Always On Top (default: <kbd>⊞ Win</kbd>+<kbd>Ctrl</kbd>+<kbd>T</kbd>), the utility pins the active window above all other windows. The pinned window stays on top, even when you select other windows.

## Unpin a window

To unpin a window pinned by Always On Top, you can either use the activation shortcut again or close the window.

## Settings

Always On Top has the following settings:

| Setting | Description |
| :--- | :--- |
| **Activation shortcut** | The customizable keyboard command to turn on or off the always-on-top property for that window. |
| **Do not activate when Game Mode is on** | Prevents the feature from being activated when actively playing a game on the system. |
| **Show Always on Top in the title bar context menu** | Lets you turn Always on Top mode on or off from the window's title bar right-click menu. |
| **Show a border around the pinned window** | When **On**, this option shows a colored border around the pinned window.  |
| **Color mode** | Choose either **Windows default** or **Custom color** for the highlight border. |
| **Color** | The custom color of the highlight border. **Color** is only available when **Color mode** is set to **Custom color**. |
| **Opacity (%)** | The opacity of the highlight border. |
| **Thickness (px)** | The thickness of the highlight border in pixels. |
| **Enable rounded corners** | When selected, the highlight border around the pinned window will have rounded corners.  |
| **Play a sound when pinning a window** | When selected, this option plays a sound when you activate or deactivate Always On Top. |
| **Excluded apps** | Prevents you from pinning an application using Always On Top. Add an application's name to stop it from being pinned. The list will also exclude partial matches. For example, `Notepad` will prevent both Notepad.exe and Notepad++.exe from being pinned. To only prevent a specific application, add `Notepad.exe` to the excluded list. |

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
