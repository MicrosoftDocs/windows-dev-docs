---
title: Windows Terminal Global Settings
description: Learn how to customize the global settings within Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 08/26/2020
ms.topic: how-to
ms.localizationpriority: high
---

# Global settings in Windows Terminal

The properties listed below affect the entire terminal window, regardless of the profile settings. These should be placed at the root of your settings.json file.

## Default profile

Set the default profile that opens by typing <kbd>ctrl+shift+t</kbd>, typing the key binding assigned to `newTab`, running `wt new-tab` without specifying a profile, or clicking the '+' icon.

**Property name:** `defaultProfile`

**Necessity:** Required

**Accepts:** GUID or profile name as a string

**Default value:** PowerShell's GUID

<br />

___

## Disable dynamic profiles

This sets which dynamic profile generators are disabled, preventing them from adding their profiles to the list of profiles on startup. For information on dynamic profiles, visit the [Dynamic profiles page](./../dynamic-profiles.md).

**Property name:** `disabledProfileSources`

**Necessity:** Optional

**Accepts:** `"Windows.Terminal.Wsl"`, `"Windows.Terminal.Azure"`, and/or `"Windows.Terminal.PowershellCore"` inside an array

**Default value:** `[]`

<br />

___

## Dark/Light theme

:::row:::
:::column span="":::
This sets the theme of the application. `"system"` will use the same theme as Windows.

**Property name:** `theme`

**Necessity:** Optional

**Accepts:** `"system"`, `"dark"`, `"light"`

**Default value:** `"system"`

:::column-end:::
:::column span="":::
![Windows Terminal dark theme](./../images/requested-themes.gif)
_Configuration: [Powerline in PowerShell](./../custom-terminal-gallery/powerline-in-powershell.md)_

:::column-end:::
:::row-end:::

<br />

___

## Tab settings

### Use tab switcher experience

:::row:::
:::column span="":::
When this is set to `true`, the `nextTab` and `prevTab` commands will use the tab switcher UI. The UI will show all the currently open tabs in a vertical list, navigable with the keyboard or mouse.

The tab switcher will open on the initial press of the actions for `nextTab` and `prevTab`, and will stay open as long as a modifier key is held down. When all modifier keys are released, the switcher will close and the highlighted tab will be focused. <kbd>tab</kbd>/<kbd>shift+tab</kbd>, the <kbd>up</kbd> and <kbd>down</kbd> arrow keys, and the `nextTab`/`prevTab` actions can be used to cycle through the switcher UI.

**Property name:** `useTabSwitcher`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `true`

:::column-end:::
:::column span="":::
![Windows Terminal tab switcher](./../images/tab-switcher.gif)

:::column-end:::
:::row-end:::

### Always show tabs

:::row:::
:::column span="":::
When this is set to `true`, tabs are always displayed. When it's set to `false` and `showTabsInTitlebar` is set to `true`, tabs are always displayed underneath the title bar. When this is set to `false` and `showTabsInTitlebar` is set to `false`, tabs only appear after more than one tab exists by typing <kbd>ctrl+shift+t</kbd> or by typing the key binding assigned to `newTab`. Note that changing this setting will require starting a new terminal instance.

**Property name:** `alwaysShowTabs`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `true`

:::column-end:::
:::column span="":::
![Windows Terminal always show tabs](./../images/always-show-tabs.gif)

:::column-end:::
:::row-end:::

### Tab width mode

:::row:::
:::column span="":::
This sets the width of the tabs. `"equal"` makes each tab the same width. `"titleLength"` sizes each tab to the length of its title. `"compact"` will shrink every inactive tab to the width of the icon, leaving the active tab more space to display its full title.

**Property name:** `tabWidthMode`

**Necessity:** Optional

**Accepts:** `"equal"`, `"titleLength"`, `"compact"`

**Default value:** `"equal"`

:::column-end:::
:::column span="":::
![Windows Terminal tab width mode](./../images/tab-width-mode.gif)

:::column-end:::
:::row-end:::

### Hide close all tabs popup

:::row:::
:::column span="":::
When this is set to `true`, closing a window with multiple tabs open _will_ require confirmation. When it's set to `false`, closing a window with multiple tabs open _will not_ require confirmation.

**Property name:** `confirmCloseAllTabs`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `true`

:::column-end:::
:::column span="":::
![Windows Terminal confirm close all tabs](./../images/confirm-close-all-tabs.png)

:::column-end:::
:::row-end:::

<br />

___

## Launch settings

### Launch on startup

When set to `true`, this enables the launch of Windows Terminal at startup. Setting this to `false` will disable the startup task entry. Note: if the Windows Terminal startup task entry is disabled either by org policy or by user action this setting will have no effect.

**Property name:** `startOnUserLogin`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `false`

### Launch mode

This defines whether the terminal will launch as maximized, full screen, or in a window. Setting this to `focus` is equivalent to launching the terminal in the `default` mode, but with the focus mode enabled. Similar, setting this to `focusMaximized` will result in launching the terminal in a maximized window w ith the focus mode enabled.

**Property name:** `launchMode`

**Necessity:** Optional

**Accepts:** `"default"`, `"maximized"`, `"fullscreen"`, `"focus"`, `"maximizedFocus"`

**Default value:** `"default"`

### Launch position

This sets the pixel position of the top left corner of the window upon first load. On a system with multiple displays, these coordinates are relative to the top left of the primary display. If an X or Y coordinate is not provided, the terminal will use the system default for that value. If `launchMode` is set to `"maximized"` (or `"maximizedFocus"`), the window will be maximized on the monitor specified by those coordinates.

**Property name:** `initialPosition`

**Necessity:** Optional

**Accepts:** Coordinates as a string in the following formats: `","`, `"#,#"`, `"#,"`, `",#"`

**Default value:** `","`

### Columns on first launch

This is the number of character columns displayed in the window upon first load. If `launchMode` is set to `"maximized"` (or `"maximizedFocus"`), this property is ignored.

**Property name:** `initialCols`

**Necessity:** Optional

**Accepts:** Integer

**Default value:** `120`

### Rows on first launch

This is the number of rows displayed in the window upon first load. If `launchMode` is set to `"maximized"` (or `"maximizedFocus"`), this property is ignored.

**Property name:** `initialRows`

**Necessity:** Optional

**Accepts:** Integer

**Default value:** `30`

### Always on top mode

When set to true, Windows Terminal windows will launch on top of all other windows on the desktop. This state can also be toggled with the `toggleAlwaysOnTop` key binding.

**Property name:** `alwaysOnTop`

**Necessity:** Optional

**Accepts:** `true, false`

**Default value:** `false`

<br />

___

## Title bar settings

### Show/Hide the title bar

:::row:::
:::column span="":::
When this is set to `true`, the tabs are moved into the title bar and the title bar disappears. When it's set to `false`, the title bar sits above the tabs. Note that changing this setting will require starting a new terminal instance.

**Property name:** `showTabsInTitlebar`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `true`

:::column-end:::
:::column span="":::
![Windows Terminal show tabs in title bar](./../images/show-tabs-in-title-bar.gif)

:::column-end:::
:::row-end:::

### Set the text in the title bar

When this is set to `true`, the title bar displays the title of the selected tab. When it's set to `false`, title bar displays "Windows Terminal". Note that changing this setting will require starting a new terminal instance.

**Property name:** `showTerminalTitleInTitlebar`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `true`

<br />

___

## Selection settings

### Copy after selection is made

When this is set to `true`, a selection is immediately copied to your clipboard upon creation. The right-click on your mouse will always paste in this case. When it's set to `false`, the selection persists and awaits further action. Using your mouse to right-click will copy the selection.

**Property name:** `copyOnSelect`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `false`

### Copy formatting

When this is set to `true`, the color and font formatting of the selected text is also copied to your clipboard. When it's set to `false`, only plain text is copied to your clipboard. You can also specify which formats you would like to copy.

**Property name:** `copyFormatting`

**Necessity:** Optional

**Accepts:** `true`, `false`, `"all"`, `"none"`, `"html"`, `"rtf"`

**Default value:** `false`

### Word delimiters

This determines the word delimiters used in a double-click selection. Word delimiters are characters that specify where the boundary is between two words. The most common examples are spaces, semicolons, commas, and periods.

**Property name:** `wordDelimiters`

**Necessity:** Optional

**Accepts:** Characters as a string

**Default value:** <code>&nbsp;&#x2f;&#x5c;&#x28;&#x29;&#x22;&#x27;&#x2d;&#x3a;&#x2c;&#x2e;&#x3b;&#x3c;&#x3e;&#x7e;&#x21;&#x40;&#x23;&#x24;&#x25;&#x5e;&#x26;&#x2a;&#x7c;&#x2b;&#x3d;&#x5b;&#x5d;&#x7b;&#x7d;&#x7e;&#x3f;│</code><br>_(`│` is `U+2502 BOX DRAWINGS LIGHT VERTICAL`)_

<br />

___

## Paste warnings

### Warn when the text to paste is very large

When this is set to `true`, trying to paste text with more than 5 KiB of characters will display a dialog asking you whether to continue or not with the paste. When it's set to `false`, the dialog is not shown and instead the text is pasted right away. If you often right-click on the terminal by accident after having selected a lot of text, this might be useful to prevent the terminal from becoming unresponsive while the program connected to the terminal receives the clipboard's content.

**Property name:** `largePasteWarning`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `true`

### Warn when the text to paste contains multiple lines

When this is set to `true`, trying to paste text with multiple lines will display a dialog asking you whether to continue or not with the paste. When it's set to `false`, the dialog is not shown and instead the text is pasted right away. In most shells, one line corresponds to one command so if you paste text that contains the "new line" character into a shell, one or more command(s) might be executed automatically upon paste, without you having time to validate the commands. This can be useful if you often copy and paste commands from untrusted websites.

**Property name:** `multiLinePasteWarning`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `true`

<br />

___

## Scroll speed

This is the number of rows to scroll at a time with the mouse wheel. This will override the system setting if the value is not zero or `"system"`.

**Property name:** `rowsToScroll`

**Necessity:** Optional

**Accepts:** Integer

**Default value:** `"system"`

> [!CAUTION]
> The `rowsToScroll` setting is no longer available versions 1.2 and later. Windows Terminal will use the value configured in the system Mouse settings panel.

<br />

___

## Window resize behavior

:::row:::
:::column span="":::
When this is set to `true`, the window will snap to the nearest character boundary on resize. When it's set to `false`, the window will resize "smoothly".

**Property name:** `snapToGridOnResize`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `true`

:::column-end:::
:::column span="":::
![Windows Terminal snap to grid on resize](./../images/snap-to-grid-on-resize.gif)

:::column-end:::
:::row-end:::

<br />

___

## Rendering settings

If you are thinking about changing the rendering settings, additional information is provided on the [Troubleshooting page](./../troubleshooting.md#the-text-is-blurry) to help guide you.

### Screen redrawing

When this set to `true`, the terminal will redraw the entire screen each frame. When set to `false`, it will render only the updates to the screen between frames.

**Property name:** `experimental.rendering.forceFullRepaint`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `false`

### Software rendering

When this is set to `true`, the terminal will use the software renderer (a.k.a. WARP) instead of the hardware one.

**Property name:** `experimental.rendering.software`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `false`
