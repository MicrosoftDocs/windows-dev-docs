---
title: Windows Terminal tips and tricks
description: In this page, you will find tips and tricks to help improve your Windows Terminal experience.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 10/08/2021
ms.topic: how-to
ms.localizationpriority: high
---

# Windows Terminal tips and tricks

## On first launch

When you first install Windows Terminal, you will be greeted with a Windows PowerShell prompt. Windows Terminal ships with Windows PowerShell, Command Prompt, and Azure Cloud Shell profiles by default.

In addition to these profiles, if you have any [Windows Subsystem for Linux (WSL)](/windows/wsl) distributions installed, the terminal will automatically create profiles for those distributions as well. If you would like to install additional WSL distributions on your machine, you can do so after installing terminal and on your next terminal launch, the profiles for those distributions will automatically appear. These profiles will have the Linux Tux image as their icon.

> [!NOTE]
> You can change the icon of each WSL distribution if desired. Specific distribution icons do not come shipped inside the terminal but can be downloaded and assigned using the terminal settings.

## View default settings

Windows Terminal comes with a large set of default settings, including [color schemes](./customize-settings/color-schemes.md) and [keyboard shortcuts](./customize-settings/actions.md) (now called "Custom actions"). If you’d like to view the default settings file, you can hold <kbd>Alt</kbd> and click on the Settings button inside the dropdown menu.

## Default profile settings

Windows Terminal enables you to apply a setting to every profile without having to duplicate the setting for each profile entry. This can be done by adding a setting inside the "defaults" array inside the [profiles](./customize-settings/profile-general.md) object. Learn more about [General profile settings](./customize-settings/profile-general.md), [Appearance profile settings](./customize-settings/profile-appearance.md), and [Advanced profile settings](./customize-settings/profile-advanced.md).

```json
"profiles":
    {
        "defaults":
        {
            // Put settings here that you want to apply to all profiles.
            "fontFace": "Cascadia Code"
        },
        "list":
        []
    }
```

## Rename a tab

You can right click on a tab and select Rename Tab to rename a tab for that terminal session. Clicking this option in the context menu will change your tab title into a text field, where you can then edit the title. If you'd like to set the tab title for that profile for every terminal instance, you can learn more in the [Tab title tutorial](./tutorials/tab-title.md).

![Windows Terminal tab rename](./images/tab-rename.gif)

## Color a tab

You can right-click on a tab and select **Color**... to color the tab for that terminal session. You can select from a predefined list of colors or you can select **Custom...** to pick any color using the color picker or the RGB/HSV or hex fields.

![Windows Terminal tab color](./images/tab-color.png)

> [!TIP]
> Use the hex field to set your tab to the same color as your background color for a seamless look.

The `tabColor` can be set as part of a profile. See [Profile - Appearance: Tab color](./customize-settings/profile-appearance.md#tab-color). For example:

```json
 {
            "guid": "{1234abc-abcd-1234-12ab-1234abc}",
            "name": "Windows PowerShell",
            "background": "#012456",
            "tabColor": "#012456",
        },
```

The `tabColor` cannot be set as part of a color scheme. Additionally, while it is possible to [set the tab title from the commandline](./tutorials/tab-title.md) with escape sequences, it currently isn't possible to set the tab color in this way.

## Mouse interaction

There are several ways to interact with Windows Terminal using a mouse.

### Zoom with the mouse

You can zoom the text window of Windows Terminal (making the text size larger or smaller) by holding <kbd>Ctrl</kbd> and scrolling. The zoom will persist for that terminal session. If you want to change your font size, you can learn more about the font size feature on the [Profile - Appearance page](./customize-settings/profile-appearance.md#text).

### Adjust background opacity with the mouse

You can adjust the opacity of the background by holding <kbd>Ctrl</kbd>+<kbd>Shift</kbd> and scrolling. The opacity will persist for that terminal session. If you want to change your acrylic opacity for a profile, you can learn more about acrylic background effects on the [Profile - Appearance page](./customize-settings/profile-appearance.md#transparency).

> [!NOTE]
> In [Windows Terminal Preview](https://aka.ms/terminal-preview) version 1.12, changing the background opacity with the mouse wheel will use vintage-style opacity by default, unless `useAcrylic` is set to true in your settings. Prior to 1.12, the terminal would always use acrylic for transparency.

### Open a hyperlink

You can open a hyperlink from inside Windows Terminal with your mouse using <kbd>ctrl</kbd> + click.

### Drag and drop file/folder to open

You can drag and drop a file or folder over the New Tab button to open your default profile with that given file or folder. By default, this will open a new tab. You can hold <kbd>Alt</kbd> to open a new pane in your current tab or hold <kbd>Shift</kbd> to open a new window.

![Windows Terminal drag and drop](./images/drag-and-drop.gif)

### Copy/paste

You can right-click with your mouse to copy and paste text within Windows Terminal using your clipboard storage.

Windows Terminal also includes a **[copyOnSelect](./customize-settings/interaction.md#automatically-copy-selection-to-clipboard)** setting that can be set to `true` in order for any text selected with your mouse to be immediately copied to your clipboard. The right-click on your mouse will always paste in this case.

### Virtual Terminal and WSL mouse support

Windows Terminal supports mouse input in Windows Subsystem for Linux (WSL) applications as well as Windows applications that use virtual terminal (VT) input. This means applications such as [tmux](https://github.com/tmux/tmux/wiki) and [Midnight Commander](https://www.linuxhelp.com/how-to-install-midnight-commander-in-linux) will recognize when you select items in the Terminal window. If an application is in mouse mode, you can hold down <kbd>shift</kbd> to make a selection instead of sending VT input.

## Send input commands with a key binding

Windows Terminal gives you the ability to send input to your shell with a key binding. This can be done with the following structure inside the `"actions"` array of your settings.json file.

```json
{ "command": {"action": "sendInput", "input": ""}, "keys": "" }
```

You can also add a `"name": ""` value if desired.

### Clear your screen

Sending input to the shell with a keyboard shortcut can be useful for commands you run often. One example would be clearing your screen:

```json
{ "command": {"action": "sendInput", "input": "clear\r"}, "keys": "alt+k", "name": "clear terminal" }
```

### Navigate to parent directory

Navigating to the parent directory with a key binding may also be helpful.

```json
{ "command": {"action": "sendInput", "input": "cd ..\r"}, "keys": "ctrl+up" }
```

You can also use this functionality to run builds or test scripts.

## Quake mode

"Quake mode" is the name for the special mode the terminal enters when naming a window `_quake`. When a window is in quake mode:

* The terminal is automatically snapped to the top half of the monitor.

* The window can no longer be resized horizontally or from the top. It can only be resized on the bottom.

* The window automatically enters focus mode (note that you may have multiple tabs in focus mode).

* When [`windowingBehavior`](./customize-settings/startup.md#new-instance-behavior) is set to `"useExisting"` or `"useAnyExisting"`, they will ignore the existence of the `_quake` window.

* The window will be hidden from the taskbar and from <kbd>Alt</kbd>+<kbd>Tab</kbd>.

* Only one window may be the quake mode window at a time.

The quake mode window can be created either by binding the `quakeMode` action, or by manually running the command line:

```console
wt -w _quake
```

> [!NOTE]
> If you don't have a [`quakeMode`](./customize-settings/actions.md#global-commands) action bound and minimize the quake window, you'll need to go into Task Manager to be able to exit that terminal window!
