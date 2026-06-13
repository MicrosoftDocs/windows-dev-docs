---
title: PowerToys Command Palette Dock
description: Learn how to use the Command Palette Dock, a persistent toolbar that provides quick access to your most-used commands and extensions from any screen edge.
ms.date: 06/08/2026
ms.topic: concept-article
no-loc: [PowerToys, Windows, Insider]
# Customer intent: Learn about the Command Palette Dock feature and how to configure it.
---

# Command Palette Dock

The Command Palette Dock is a persistent toolbar that stays visible at the edge of your screen, providing instant access to your most-used commands and extensions without needing to open the full Command Palette window.

> [!NOTE]
> The Dock feature is part of the Command Palette and requires Command Palette to be enabled and running in the background.

## Enable the Dock

To enable the Dock, open Command Palette settings and toggle the **Enable Dock** option. When enabled, the Dock appears as a toolbar anchored to the edge of your screen.

## Dock layout

The Dock is divided into three regions that organize your pinned commands:

| Region | Position | Description |
| :--- | :--- | :--- |
| **Start** | Left side (or top, for vertical docks) | Primary commands and launchers. By default, includes the Home command and WinGet launcher. |
| **Center** | Middle | Additional commands you add. Initially empty. |
| **End** | Right side (or bottom, for vertical docks) | Utility widgets. By default, includes Performance Monitor and Date/Time widgets. |

The Dock displays horizontally when anchored to the top or bottom of the screen, and vertically when anchored to the left or right.

## Multi-monitor support

On multi-monitor setups, each display gets its own Dock instance and its own set of pinned bands. You can pin different commands to different monitors. For example, keep Performance Monitor on your primary display and a set of work bookmarks on a secondary display. Open Dock edit mode on the Dock you want to change and add, remove, or rearrange bands there. You can also drag bands directly from one monitor's Dock to another to move them across displays.

## Pin commands to the Dock

There are several ways to pin commands and extensions to the Dock.

### Pin from Command Palette

The easiest way to add a command to the Dock is directly from the Command Palette:

1. Open the Command Palette and navigate to the command you want to pin.
2. Right-click the command and select **Pin to Dock**, or open the **More actions** menu and select **Pin to Dock**.

To remove a pinned command, right-click it and select **Unpin from Dock**.

### Pin from Dock edit mode

You can also manage the Dock layout by entering edit mode:

1. Right-click the Dock background and select **Edit Dock**.
2. Select the **+** button in the Start, Center, or End region to add a new command.
3. Choose from the list of available commands in the flyout.
4. Drag and drop commands to reorder them within or across regions. On multi-monitor setups you can also drag a band onto another monitor's Dock to move it between displays.
5. Select **Save** to apply your changes, or **Discard** to revert.

You can right-click individual items in edit mode to toggle **Show Titles** and **Show Subtitles**, or to **Unpin** a command from the Dock.

### Pin files and URLs by drag and drop

Drag a file or a URL onto the Dock to immediately create a Bookmark for it and pin the Bookmark in place. Files and folders are bookmarked at the path you dropped, and URLs are bookmarked at their address. Pinned folder bookmarks open the Command Palette browse experience scoped to that folder when you select them.

### Pin from Dock settings

In the Command Palette settings, the **Bands** section lists all available dock bands. Use the toggle switch next to each band to pin or unpin it from the Dock.

## Dock settings

The following settings are available for the Dock in Command Palette settings.

| Setting | Description |
| :--- | :--- |
| Enable Dock | Enable a toolbar with quick access to commands. |

### Appearance

| Setting | Description |
| :--- | :--- |
| Position | Choose which screen edge to anchor the Dock to: **Left**, **Top** (default), **Right**, or **Bottom**. |
| Theme mode | Select which theme to display: **Use system settings** (default), **Light**, or **Dark**. |
| Material | Select the visual material: **Transparent** or **Acrylic** (default). |
| Background colorization | Customize the Dock background. Options include **None**, **Accent color** (uses the Windows system accent color), **Custom color** (pick a color tint and intensity), or **Image** (set a background image with brightness, blur, and fit controls). |

When **Image** is selected as the background colorization mode, the following additional settings are available:

| Setting | Description |
| :--- | :--- |
| Background image | Browse for an image file (supports PNG, BMP, JPG, GIF, TIFF, WEBP, and other common formats). |
| Background image brightness | Adjust the brightness of the background image (–100 to 100). |
| Background image blur | Set the blur effect applied to the background image (0–50). |
| Background image fit | Choose how the background image is scaled: **Fill** or **Stretch**. |

When **Custom color** or **Image** is selected, you can also configure:

| Setting | Description |
| :--- | :--- |
| Color tint | Pick a custom color tint for the Dock background. |
| Color intensity | Adjust the intensity of the color tint (1–100). |

### Bands

The **Bands** section lists all available dock bands provided by installed extensions. Each band has a toggle switch to pin or unpin it from the Dock.

## Dock behavior

The Dock uses the Windows AppBar API to reserve screen space, so other windows don't overlap it. The Dock:

- Stays visible at all times while enabled (auto-hide is not currently supported).
- Automatically restores its position if Windows Explorer restarts.
- Cannot be resized or dragged. Its position is controlled by the **Position** setting.

## Related content

- [Command Palette overview](overview.md)
- [Extensibility overview](extensibility-overview.md)
