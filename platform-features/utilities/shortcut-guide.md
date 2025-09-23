---
title: PowerToys Shortcut Guide Utility for Windows
description: Learn how to use PowerToys Shortcut Guide to display Windows key shortcuts. View common keyboard shortcuts, window positioning, and taskbar shortcuts with this Windows utility.
ms.date: 08/20/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, File Explorer]
# customer intent: As a Windows power user, I want to learn how to use the Shortcut Guide utility for Windows.
---

# Windows key shortcut guide

The PowerToys Shortcut Guide displays common keyboard shortcuts that use the Windows key. This utility helps Windows power users quickly access keyboard shortcuts for window management, taskbar navigation, and system commands by holding the Windows key.

## Get started

To open the shortcut guide, hold down the <kbd>⊞</kbd> Windows key for the time as set in the PowerToys Settings (900ms by default). An overlay will appear showing keyboard shortcuts that use the Windows key, including:

- common Windows shortcuts
- shortcuts for changing the position of the active window
- taskbar shortcuts

:::image type="content" source="../images/pt-shortcut-guide-large.png" alt-text="Screenshot of PowerToys Shortcut Guide overlay displaying Windows key keyboard shortcuts on desktop.":::

Keyboard shortcuts using the Windows key <kbd>⊞ Win</kbd> can be used while the guide is displayed. The result of those shortcuts (active window moved, arrow shortcut behavior changes etc.) will be displayed in the guide.

Pressing the shortcut key combination again will dismiss the overlay.

Tapping the Windows key will display the Windows Start menu.

> [!IMPORTANT]
> The PowerToys app must be running and Shortcut Guide must be enabled in the PowerToys settings for this feature to be used.

## Settings

These configurations can be edited from the PowerToys Settings:

| Setting | Description |
| :--- | :--- |
| Activation method | Choose your own shortcut or use the <kbd>⊞ Win</kbd> key |
| Press duration | The duration (in milliseconds) to hold down the <kbd>⊞ Win</kbd> key in order to open the shortcut guide |
| Activation shortcut | The custom shortcut used to open the shortcut guide |
| App theme | **Light**, **Dark** or **Windows default** |
| Background opacity | Opacity of the Shortcut Guide overlay |
| Excluded apps | Ignores Shortcut Guide when these apps are in focus. Add an application's name, or part of the name, one per line (e.g. adding `Notepad` will match both `Notepad.exe` and `Notepad++.exe`; to match only `Notepad.exe` add the `.exe` extension). |

:::image type="content" source="../images/pt-shortcut-guide-settings.png" alt-text="Screenshot of PowerToys Shortcut Guide settings page showing activation method and customization options.":::

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
