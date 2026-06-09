---
title: PowerToys Shortcut Guide Utility for Windows
description: Learn how to use PowerToys Shortcut Guide to display shortcuts of Windows an other applications. View common keyboard shortcuts, window positioning, and taskbar shortcuts with this Windows utility.
ms.date: 10/28/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, File Explorer]
# customer intent: As a Windows power user, I want to learn how to use the Shortcut Guide utility for Windows.
---

# Shortcut Guide V2

The PowerToys Shortcut Guide displays shortcuts for Windows and third-party apps.

:::image type="content" source="../images/pt-shortcut-guide.png" alt-text="Screenshot of PowerToys Shortcut Guide showing shortcuts for window management and virtual desktops.":::

## Currently supported applications

Currently the following applications are supported out-of-box:
- Windows Explorer
- Windows Shell (Desktop)
- Microsoft PowerToys
- Notepad
- Adobe After Effects
- Adobe Illustrator
- Adobe InDesign
- Adobe Photoshop
- Blender
- Discord
- Figma
- GIMP
- Google Chrome
- Inkscape
- JetBrains IntelliJ IDEA
- Microsoft Access
- Microsoft Edge
- Microsoft Excel
- Microsoft OneNote
- Microsoft Outlook
- Microsoft Paint
- Microsoft PowerPoint
- Microsoft Project
- Microsoft Publisher
- Microsoft Teams
- Microsoft Visio
- Microsoft Word
- Mozilla Firefox
- Slack
- Telegram
- Terminal
- Visual Studio Code


> [!NOTE]
> Applications can add their own manifests containing the to be displayed shortcuts according to the [manifest specification](https://aka.ms/powertoys-sg-manifest).

## Get started

Invoking the Shortcut Guide is as simple as pressing the shortcut (by default: Win + Shift + / (on U.S. Keyboards)). This brings up a window with the Shortcuts for the currently focused window. If Shortcut Guide doesn't know of any shortcuts for the currently focused window, it defaults to the shortcuts defined in Windows.

The Overview page shows some recommended shortcuts, your pinned shortcuts, and if it's the Windows overview page, then Taskbar shortcuts (described below) are also shown.

:::image type="content" source="../images/pt-shortcut-guide-main-window.png" alt-text="Screenshot of PowerToys Shortcut Guide showing the overview page with pinned shortcuts.":::

On the left-hand side you can choose between all the currently supported applications that are running and view their shortcuts. On the top you can choose between different categories of shortcuts (like Window Management, Virtual Desktops, etc.) of the currently selected application.

To close Shortcut Guide either press the close button in the top right corner, press the Escape key, or click outside of the Shortcut Guide window.

> [!IMPORTANT]
> The PowerToys app must be running and Shortcut Guide must be enabled in the PowerToys settings for this feature to be used.

### Taskbar shortcuts

If the overview page of Windows is shown, the Taskbar shortcuts are also displayed at the bottom of the window. These shortcuts allow quick access to the pinned applications on the Taskbar by pressing a shortcut (for instance <kbd>Win + [Number]</kbd>), where <kbd>[Number]</kbd> corresponds to the position of the application on the Taskbar (from left to right, starting with 1). These numbers are displayed on top of the application icons.

:::image type="content" source="../images/pt-shortcut-guide-taskbar-numbers.png" alt-text="Screenshot of PowerToys Shortcut Guide showing taskbar shortcuts at the bottom of the window.":::

### Pinning/Unpinning shortcuts

You can right-click on any shortcut and select "Pin" to pin it to the overview page of the currently selected application or "Unpin" to remove it from the overview page.

:::image type="content" source="../images/pt-shortcut-guide-pin.png" alt-text="Screenshot of PowerToys Shortcut Guide showing the context menu to pin a shortcut.":::
:::image type="content" source="../images/pt-shortcut-guide-unpin.png" alt-text="Screenshot of PowerToys Shortcut Guide showing the context menu to unpin a shortcut.":::

## Settings

These configurations can be edited from the PowerToys Settings:

| Setting | Description |
| :--- | :--- |
| Activation shortcut | The custom shortcut used to open Shortcut Guide |
| App theme | **Light**, **Dark** or **Windows default** |
| Excluded apps | Ignores Shortcut Guide when these apps are in focus. Add an application's name, or part of the name, one per line (e.g. adding `Notepad` will match both `Notepad.exe` and `Notepad++.exe`; to match only `Notepad.exe` add the `.exe` extension). |

:::image type="content" source="images/shortcut-guide/settings.png" alt-text="Screenshot of PowerToys Shortcut Guide settings page showing activation method and customization options.":::

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
