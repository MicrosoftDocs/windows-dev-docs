---
title: Windows Terminal Startup Settings
description: Learn how to customize startup settings within Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 1/28/2021
ms.topic: how-to
ms.localizationpriority: high
---

# Startup settings in Windows Terminal

The properties listed below affect the entire terminal window, regardless of the profile settings. These should be placed at the root of your settings.json file.

## Default profile

Set the default profile that opens by typing <kbd>ctrl+shift+t</kbd>, typing the key binding assigned to `newTab`, running `wt new-tab` without specifying a profile, or clicking the '+' icon.

**Property name:** `defaultProfile`

**Necessity:** Required

**Accepts:** GUID or profile name as a string

**Default value:** PowerShell's GUID

<br />

___

## Launch on machine startup

When set to `true`, this enables the launch of Windows Terminal at startup. Setting this to `false` will disable the startup task entry. Note: if the Windows Terminal startup task entry is disabled either by org policy or by user action this setting will have no effect.

**Property name:** `startOnUserLogin`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `false`

<br />

___

## Launch mode

This defines whether the terminal will launch as maximized, full screen, or in a window. Setting this to `focus` is equivalent to launching the terminal in the `default` mode, but with [focus mode](./actions.md#toggle-focus-mode) enabled. Similarly, setting this to `maximizedFocus` will result in launching the terminal in a maximized window with focus mode enabled.

**Property name:** `launchMode`

**Necessity:** Optional

**Accepts:** `"default"`, `"maximized"`, `"fullscreen"`, `"focus"`, `"maximizedFocus"`

**Default value:** `"default"`

> [!IMPORTANT]
> The `"focus"` and `"maximizedFocus"` modes are only available in [Windows Terminal Preview](https://aka.ms/terminal-preview/).

<br />

___

## Launch size

### Columns on first launch

This is the number of character columns displayed in the window upon first load. If `launchMode` is set to `"maximized"` or `"maximizedFocus"`, this property is ignored.

**Property name:** `initialCols`

**Necessity:** Optional

**Accepts:** Integer

**Default value:** `120`

### Rows on first launch

This is the number of rows displayed in the window upon first load. If `launchMode` is set to `"maximized"` or `"maximizedFocus"`, this property is ignored.

**Property name:** `initialRows`

**Necessity:** Optional

**Accepts:** Integer

**Default value:** `30`

<br />

___

## Launch position

This sets the pixel position of the top left corner of the window upon first load. On a system with multiple displays, these coordinates are relative to the top left of the primary display. If an X or Y coordinate is not provided, the terminal will use the system default for that value. If `launchMode` is set to `"maximized"` or `"maximizedFocus"`, the window will be maximized on the monitor specified by those coordinates.

**Property name:** `initialPosition`

**Necessity:** Optional

**Accepts:** Coordinates as a string in the following formats: `","`, `"#,#"`, `"#,"`, `",#"`

**Default value:** `","`

<br />

___

## Disable dynamic profiles

This sets which dynamic profile generators are disabled, preventing them from adding their profiles to the list of profiles on startup. For information on dynamic profiles, visit the [Dynamic profiles page](./../dynamic-profiles.md).

**Property name:** `disabledProfileSources`

**Necessity:** Optional

**Accepts:** `"Windows.Terminal.Wsl"`, `"Windows.Terminal.Azure"`, and/or `"Windows.Terminal.PowershellCore"` inside an array

**Default value:** `[]`
