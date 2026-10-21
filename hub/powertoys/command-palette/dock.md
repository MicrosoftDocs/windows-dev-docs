---
title: PowerToys Command Palette Dock
description: Learn how to use the Command Palette Dock, a persistent toolbar that provides quick access to your most-used commands and extensions from any screen edge.
ms.date: 10/14/2026
ms.topic: concept-article
no-loc: [PowerToys, Windows, Insider]
# Customer intent: Learn about the Command Palette Dock feature and how to configure it.
---

# Command Palette Dock

The Command Palette Dock is a toolbar that sits on the edge of your screen so you can open your most-used commands and extensions without opening the full Command Palette window.

> [!NOTE]
> The Dock feature is part of the Command Palette and requires Command Palette to be enabled and running in the background.

## Enable the Dock

To enable the Dock, open Command Palette settings and toggle the **Enable Dock** option. When enabled, the Dock appears as a toolbar anchored to the edge of your screen. You can leave it pinned or turn on auto-hide.

## Dock layout

The Dock is divided into three regions that organize your pinned commands:

| Region | Position | Description |
| :--- | :--- | :--- |
| **Start** | Left side (or top, for vertical docks) | Primary commands and launchers. By default, includes the Home command and WinGet launcher. |
| **Center** | Middle | Additional commands you add. Initially empty. |
| **End** | Right side (or bottom, for vertical docks) | Utility widgets. By default, includes Performance Monitor, Network Speed, and Date/Time bands. |

The Dock displays horizontally when anchored to the top or bottom of the screen, and vertically when anchored to the left or right.

## Multi-monitor support

On multi-monitor setups, each display gets its own Dock instance and its own set of pinned bands. You can pin different commands to different monitors. For example, keep Performance Monitor on your primary display and a set of work bookmarks on a secondary display. Open Dock edit mode on the Dock you want to change and add, remove, or rearrange bands there. You can also drag bands directly from one monitor's Dock to another to move them across displays.

When you turn on the Dock for a display that doesn't have its own layout yet, the Dock starts with a copy of the primary display's layout. Changes you make afterward apply only to that display.

To turn the Dock on or off for a specific display, give a display a custom name, or use a different Dock position on a display, see [Monitors](#monitors).

## Navigate the Dock with the keyboard

Press <kbd>Win</kbd>+<kbd>Alt</kbd>+<kbd>J</kbd> to move keyboard focus to the Dock. If the Dock is set to auto-hide, it appears. Press the shortcut again to move to the next Dock item, or add <kbd>Shift</kbd> (<kbd>Win</kbd>+<kbd>Alt</kbd>+<kbd>Shift</kbd>+<kbd>J</kbd>) to move to the previous item. Press <kbd>Enter</kbd> or <kbd>Space</kbd> to activate the focused item, <kbd>Ctrl</kbd>+<kbd>Enter</kbd> to run its secondary command, or <kbd>Ctrl</kbd>+<kbd>K</kbd> to open its context menu. Press <kbd>Esc</kbd> to leave the Dock.

You can change the shortcut and how focus moves between Docks with the **Dock navigation** setting. The shortcut works only while the Dock is enabled.

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

## Dock settings

The following settings are available for the Dock in Command Palette settings.

| Setting | Description |
| :--- | :--- |
| Enable Dock | Enable a toolbar with quick access to commands. If the Dock is turned off for some displays, a notice links to **Manage monitors** so you can turn it on for those displays. |
| Dock navigation | Set the shortcut that focuses the Dock. Press it again to cycle through Dock items, and add <kbd>Shift</kbd> to cycle in reverse. Default: <kbd>Win</kbd>+<kbd>Alt</kbd>+<kbd>J</kbd>. |
| Focus primary dock first | Start keyboard navigation on the Dock of the primary monitor. This setting is off by default. |
| Cycle through all docks | After the last item on one Dock, continue on the next monitor's Dock, moving from left to right and wrapping around. This setting is on by default. |
| Remember last focused item | Resume from the last focused item when you return to a Dock. This setting is on by default. |

### Appearance

| Setting | Description |
| :--- | :--- |
| Position | Choose which screen edge to anchor the Dock to: **Left**, **Top** (default), **Right**, or **Bottom**. To use a different position on a specific display, see [Monitors](#monitors). |
| Size | For **Top** and **Bottom** dock positions, choose **Default** or **Compact**. Compact mode hides subtitles on dock items. |
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

Bands are the commands and widgets that you pin to the Dock. Installed extensions can provide their own bands. To add, remove, or rearrange bands, use [Dock edit mode](#pin-from-dock-edit-mode).

Built-in bands include:

- **Time & Date**: a clock band and a **Notification center** band. To create a clock for another time zone, run **Manage custom clocks** from Command Palette and select **Add custom clock**. For each clock, you can set a **Clock name**, a **Time zone**, and a **Title format** and **Subtitle format** (for example, time with seconds, the date, a relative day, or the week of the year), plus an **Additional copy format**. You can pin custom clocks to the Dock as separate bands. To change the formats of the default Dock clock, right-click it and select **Customize clock**. In the Time & Date extension settings, **Dock clock click action** controls what happens when you select the Dock clock: **Open notification center** (default) or **Open All clocks**. Formats that you define in the extension's **Custom formats** setting are also available for clocks, including ISO 8601 week number tokens.
- **Performance Monitor**: CPU usage, Memory usage, Network usage, Network speed, Disk usage, GPU usage, and Battery. The Battery band only appears on devices that have a battery. Network metrics combine traffic from all physical adapters by default. Open a network item's actions to browse adapters and set a default adapter. In the extension settings, you can choose bits, decimal bytes, or binary bytes per second for network and disk speeds.

### Monitors

The **Monitors** section lists each connected display. Use the toggle next to a display to turn the Dock on or off for that display. Per-display settings also apply when only one display is connected. Expand a display to configure:

| Setting | Description |
| :--- | :--- |
| Display name | Give the display a custom name, then select **Save**. Leave the box blank, or select **Restore default**, to use the default name. |
| Dock position | Choose the Dock position for this display: **Use default** (follows the **Position** setting), **Left**, **Top**, **Right**, or **Bottom**. |

## Behavior settings

The following behavior settings are available for the Dock.

| Setting | Description |
| :--- | :--- |
| Always stay on top | Keep the Dock above other windows, except while an app is full screen. |
| Auto-hide dock | Collapse the Dock until you hover over its screen edge. If the taskbar or another app is already using auto-hide on that edge, the Dock falls back to pinned mode instead. |

## Dock behavior

The Dock uses the Windows AppBar API to reserve screen space while it is pinned so other windows do not overlap it. The Dock also:

- Automatically restores its position if Windows Explorer restarts.
- Can't be resized or dragged. Its position is controlled by the **Position** setting.

## Related content

- [Command Palette overview](overview.md)
- [Extensibility overview](extensibility-overview.md)
