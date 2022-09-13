---
title: Windows Terminal Appearance Settings
description: Learn how to customize appearance settings within Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 07/06/2022
ms.topic: how-to
---

# Appearance settings in Windows Terminal

The properties listed below affect the entire terminal window, regardless of the profile settings. These should be placed at the root of your [settings.json file](../install.md#settings-json-file).

## Language

This sets an override for the application's preferred language.

**Property name:** `language`

**Necessity:** Optional

**Accepts:** A BCP-47 language tag like `"en-US"`

<br />

## Theme

:::row:::
:::column span="":::
This sets the theme of the application. `"system"` will use the same theme as Windows.

**Property name:** `theme`

**Necessity:** Optional

**Accepts:** `"system"`, `"dark"`, `"light"`, name of custom [theme](./themes.md)

**Default value:** `"system"`

:::column-end:::
:::column span="":::
![Windows Terminal dark theme](./../images/requested-themes.gif)
_Configuration: [Powerline in PowerShell](./../custom-terminal-gallery/powerline-in-powershell.md)_

:::column-end:::
:::row-end:::

<br />

___

## Always show tabs

:::row:::
:::column span="":::
When this is set to `true`, tabs are always displayed. When it's set to `false` and `showTabsInTitlebar` is set to `false`, tabs are always displayed underneath the title bar. When this is set to `false` and `showTabsInTitlebar` is set to `false`, tabs only appear after more than one tab exists, by typing <kbd>ctrl+shift+t</kbd> or by typing the key binding assigned to `newTab`. Note that changing this setting will require starting a new terminal instance.

> [!NOTE]
> This setting has no effect when `showTabsInTitlebar` is `true`.

**Property name:** `alwaysShowTabs`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `true`

:::column-end:::
:::column span="":::
![Windows Terminal always show tabs](./../images/always-show-tabs.gif)

:::column-end:::
:::row-end:::

<br />

___

## Position of newly created tabs ([Preview](https://aka.ms/terminal-preview))

Specifies where new tabs appear in the tab row. When this is set to `"afterLastTab"`, new tabs appear at the end of the tab row. When it's set to `"afterCurrentTab"`, new tabs appear after the current tab.

**Property name:** `newTabPosition`

**Necessity:** Optional

**Accepts:** `"afterLastTab"`, `"afterCurrentTab"`

**Default value:** `"afterLastTab"`

<br />

___

## Hide the title bar

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

<br />

___

## Show acrylic in tab row

:::row:::
:::column span="":::
When this is set to `true`, the tab row is given an acrylic background at 50% opacity. When it's set to `false`, the tab row will be opaque. Note that changing this setting will require starting a new terminal instance.

**Property name:** `useAcrylicInTabRow`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `false`

:::column-end:::
:::column span="":::
![Windows Terminal acrylic in tab row](./../images/acrylic-tab-row.png)

:::column-end:::
:::row-end:::

<br />

___

## Use active terminal title as application title

When this is set to `true`, the title bar displays the title of the selected tab. When it's set to `false`, title bar displays "Windows Terminal". Note that changing this setting will require starting a new terminal instance.

**Property name:** `showTerminalTitleInTitlebar`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `true`

<br />

___

## Always on top mode

When set to true, Windows Terminal windows will launch on top of all other windows on the desktop. This state can also be toggled with the `toggleAlwaysOnTop` key binding.

**Property name:** `alwaysOnTop`

**Necessity:** Optional

**Accepts:** `true, false`

**Default value:** `false`

<br />

___

## Tab width mode

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

<br />

___

## Disable pane animations

This disables visual animations across the application when set to `true`.

**Property name:** `disableAnimations`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `false`

<br />

___

## Show close all tabs popup

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

<br />

___

## Use a background image for the entire window

When set to `true`, the background image for the currently focused profile is expanded to encompass the entire window, beneath other panes. This is an experimental feature, and its continued existence is not guaranteed.

**Property name:** `experimental.useBackgroundImageForWindow`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `false`
