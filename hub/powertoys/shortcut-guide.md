---
title: PowerToys Shortcut Guide utility for Windows
description: Learn how to use the PowerToys Shortcut Guide to look up keyboard shortcuts for Windows, PowerToys, and popular apps in a single context-aware reference window.
ms.date: 06/08/2026
ms.topic: concept-article
no-loc: [PowerToys, Windows, File Explorer, WinGet]
# customer intent: As a Windows power user, I want to learn how to use the Shortcut Guide utility for Windows.
---

# Shortcut Guide utility

The PowerToys Shortcut Guide is a searchable reference for keyboard shortcuts. It ships with built-in manifests for Windows, PowerToys, and many popular apps. When you open Shortcut Guide, it automatically shows shortcuts for the app currently in the foreground, plus PowerToys and any other background apps that have a matching manifest.

:::image type="content" source="images/shortcut-guide/settings.png" alt-text="Screenshot of the PowerToys Shortcut Guide settings page.":::

## Get started

Press <kbd>⊞ Win</kbd>+<kbd>Shift</kbd>+<kbd>?</kbd> (the default activation shortcut) to open Shortcut Guide. Press the shortcut again, press <kbd>Esc</kbd>, or select the close button to dismiss it.

Shortcut Guide opens as a regular window that you can read alongside the app you were using. The left sidebar lists the apps that are currently active (foreground app, plus matched background apps); selecting an app shows its shortcuts grouped into categories such as **File**, **Edit**, **View**, and so on. Recommended shortcuts for each app are highlighted at the top.

> [!IMPORTANT]
> PowerToys must be running and Shortcut Guide must be enabled in the PowerToys Settings for the activation shortcut to work.

## Pin shortcuts

You can pin individual shortcuts you use most often. Pinned shortcuts appear at the top of an app's shortcut list so you can find them quickly the next time you open Shortcut Guide.

## Supported apps

Shortcut Guide ships with the following built-in manifests. The exact set may change between PowerToys releases. For the current canonical list (including any apps added after this article was last updated), see the [Manifests folder in the PowerToys repository](https://github.com/microsoft/PowerToys/tree/main/src/modules/ShortcutGuide/ShortcutGuide.Ui/Assets/ShortcutGuide/Manifests).

| Category | Apps |
| :--- | :--- |
| Windows | Windows shell (desktop and global shortcuts), File Explorer, Notepad, Paint |
| PowerToys | PowerToys (always shown as a background match) |
| Microsoft 365 | Access, Excel, OneNote, Outlook, PowerPoint, Project, Publisher, Visio, Word |
| Browsers | Microsoft Edge, Google Chrome, Mozilla Firefox |
| Communication | Microsoft Teams, Slack, Discord, Telegram Desktop |
| Developer tools | Visual Studio Code, Windows Terminal, IntelliJ IDEA (Community) |
| Creative | Adobe Photoshop, Adobe Illustrator, Adobe InDesign, Adobe After Effects, Figma, GIMP, Inkscape, Blender |

> [!TIP]
> Don't see an app you use? You can [open a feature request on the PowerToys repository](https://github.com/microsoft/PowerToys/issues/new?template=feature_request.yml) asking for a built-in manifest. Please include the app name and a link to the app's official keyboard shortcut documentation so the manifest can be authored accurately. If you'd rather not wait, you can add the shortcuts yourself with a [user manifest](#add-shortcuts-for-your-own-apps).

## Add shortcuts for your own apps

Shortcut Guide reads its built-in manifests from the install folder, but it also picks up user-defined manifests from:

```
%LOCALAPPDATA%\Microsoft\WinGet\KeyboardShortcuts
```

Drop a YAML file in that folder to add shortcuts for an app that isn't covered by the built-ins. Each manifest declares the app name, the window or process filter used to detect when the app is in focus, and a list of shortcuts grouped by section. For example, a minimal manifest for Notepad looks like this:

```yaml
PackageName: +WindowsNT.Notepad
Name: Notepad
WindowFilter: "Notepad.exe"
BackgroundProcess: false
Shortcuts:
  - SectionName: File
    Properties:
      - Name: New tab
        Recommended: true
        Shortcut:
          - Win: false
            Ctrl: true
            Shift: false
            Alt: false
            Keys:
              - N
      - Name: Save
        Shortcut:
          - Win: false
            Ctrl: true
            Shift: false
            Alt: false
            Keys:
              - S
```

Set `BackgroundProcess: true` to show the app's shortcuts whenever the process is running (used, for example, for PowerToys itself, so PowerToys shortcuts are always available in the guide). For more examples, browse the [built-in manifests](https://github.com/microsoft/PowerToys/tree/main/src/modules/ShortcutGuide/ShortcutGuide.Ui/Assets/ShortcutGuide/Manifests) in the PowerToys source.

## Settings

Configure Shortcut Guide from the PowerToys Settings page:

| Setting | Description |
| :--- | :--- |
| **Enable Shortcut Guide** | Turn the utility on or off. |
| **Activation shortcut** | The customizable keyboard shortcut to open Shortcut Guide. The default is <kbd>⊞ Win</kbd>+<kbd>Shift</kbd>+<kbd>?</kbd>. |
| **App theme** | **Light**, **Dark**, or **Windows default**. |
| **Window position** | Pick which side of the screen Shortcut Guide opens on: **Left** or **Right**. |
| **Exclude apps** | Turns off Shortcut Guide when one of these apps has focus. Add one app name per line (for example, `outlook.exe`). |

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
