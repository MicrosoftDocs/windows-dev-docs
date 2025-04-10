---
title: PowerToys Shortcut Guide utility for Windows
description: A utility to display common keyboard shortcuts that use the Windows ⊞ key
ms.date: 07/31/2024
ms.topic: how-to
no-loc: [PowerToys, Windows, File Explorer]
---

# Windows key shortcut guide

This guide displays common keyboard shortcuts that use the Windows key.

## Get started

To open the shortcut guide, hold down the <kbd>⊞</kbd> Windows key for the time as set in the PowerToys Settings (900ms by default). An overlay will appear showing keyboard shortcuts that use the Windows key, including:

- common Windows shortcuts
- shortcuts for changing the position of the active window
- taskbar shortcuts

![Screenshot of shortcut overlay](../images/pt-shortcut-guide-large.png)

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
| Activation shortcut | The custom shortcut used to open the shortcut guide |
| Press duration | Time (in milliseconds) to hold down the <kbd>⊞ Win</kbd> key in order to open global Windows shortcuts or taskbar icon shortcuts |
| App theme | **Light**, **Dark** or **Windows default** |
| Background opacity | Opacity of the Shortcut Guide overlay |
| Excluded apps | Ignores Shortcut Guide when these apps are in focus. Add an application's name, or part of the name, one per line (e.g. adding `Notepad` will match both `Notepad.exe` and `Notepad++.exe`; to match only `Notepad.exe` add the `.exe` extension). |

![Shortcut Guide settings](../images/pt-shortcut-guide-settings.png)

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
