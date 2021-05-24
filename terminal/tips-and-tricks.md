---
title: Windows Terminal tips and tricks
description: In this page, you will find tips and tricks to help improve your Windows Terminal experience.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/25/2021
ms.topic: how-to
ms.localizationpriority: high
---

# Windows Terminal tips and tricks

## Rename a tab

You can right click on a tab and select Rename Tab to rename a tab for that terminal session. Clicking this option in the context menu will change your tab title into a text field, where you can then edit the title. If you'd like to set the tab title for that profile for every terminal instance, you can learn more in the [Tab title tutorial](./tutorials/tab-title.md).

![Windows Terminal tab rename](./images/tab-rename.gif)

## Color a tab

You can right click on a tab and select Color... to color the tab for that terminal session. You can select from a predefined list of colors or you can click Custom... to pick any color using the color picker or the RGB/HSV or hex fields.

![Windows Terminal tab color](./images/tab-color.png)

> [!TIP]
> Use the hex field to set your tab to the same color as your background color for a seamless look.

> [!NOTE]
> While it is possible to [set the tab title from the commandline](./tutorials/tab-title.md) with escape sequences, it currently isn't possible to set the tab color in this way.

## Mouse interaction

There are several ways to interact with Windows Terminal using a mouse.

### Zoom with the mouse

You can zoom the text window of Windows Terminal (making the text size larger or smaller) by holding <kbd>ctrl</kbd> and scrolling. The zoom will persist for that terminal session. If you want to change your font size, you can learn more about the font size feature on the [Profile - Appearance page](./customize-settings/profile-appearance.md#text).

### Adjust background opacity with the mouse

You can adjust the opacity of the background by holding <kbd>ctrl+shift</kbd> and scrolling. The opacity will persist for that terminal session. If you want to change your acrylic opacity for a profile, you can learn more about acrylic background effects on the [Profile - Appearance page](./customize-settings/profile-appearance.md#acrylic).

### Open a hyperlink

You can open a hyperlink from inside Windows Terminal with your mouse using <kbd>ctrl</kbd> + click.

### Copy/paste

You can right-click with your mouse to copy and paste text within Windows Terminal using your clipboard storage.

Windows Terminal also includes a **[copyOnSelect](./customize-settings/interaction.md#automatically-copy-selection-to-clipboard)** setting that can be set to `true` in order for any text selected with your mouse to be immediately copied to your clipboard. The right-click on your mouse will always paste in this case.

### Virtual Terminal and WSL mouse support

Windows Terminal supports mouse input in Windows Subsystem for Linux (WSL) applications as well as Windows applications that use virtual terminal (VT) input. This means applications such as [tmux](https://github.com/tmux/tmux/wiki) and [Midnight Commander](https://www.linuxhelp.com/how-to-install-midnight-commander-in-linux) will recognize when you select items in the Terminal window. If an application is in mouse mode, you can hold down <kbd>shift</kbd> to make a selection instead of sending VT input.

## Quake mode

"Quake mode" is the name for the special mode the terminal enters when naming a window `_quake`. When a window is in quake mode:

* The terminal is automatically snapped to the top half of the monitor.

* The window can no longer be resized horizontally or from the top. It can only be resized on the bottom.

* The window automatically enters focus mode (note that you may have multiple tabs in focus mode).

* When [`windowingBehavior`](./customize-settings/startup.md#new-instance-behavior) is set to `"useExisting"` or `"useAnyExisting"`, they will ignore the existence of the `_quake` window.

* The window will be hidden from the taskbar and from <kbd>Alt</kbd>+<kbd>Tab</kbd>.

* Only one window may be the quake mode window at a time.

The quake mode window can be created either by binding the `quakeMode` action, or by manually running the command line:

```
wt -w _quake
```

> [!NOTE]
> If you don't have a [`quakeMode`](./customize-settings/actions.md#global-commands) action bound and minimize the quake window, you'll need to go into Task Manager to be able to exit that terminal window!
