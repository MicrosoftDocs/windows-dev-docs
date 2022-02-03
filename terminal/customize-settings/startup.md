---
title: Windows Terminal Startup Settings
description: Learn how to customize startup settings within Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 02/03/2022
ms.topic: how-to
---

# Startup settings in Windows Terminal

The properties listed below affect the entire terminal window, regardless of the profile settings. These should be placed at the root of your [settings.json file](../install.md#settings-json-file).

## Default profile

Set the default profile that opens by typing <kbd>ctrl+shift+t</kbd>, typing the key binding assigned to `newTab`, running `wt new-tab` without specifying a profile, or clicking the '+' icon.

**Property name:** `defaultProfile`

**Necessity:** Required

**Accepts:** GUID or profile name as a string

**Default value:** PowerShell's GUID

<br />

___

## Default terminal application

Set the default terminal emulator in Windows for all command line applications to run inside of.

**Property name:** This modifies an OS setting and does not have a property name inside the [settings.json file](../install.md#settings-json-file).

**Necessity:** Required

**Accepts:** Any terminal emulator that appears in the dropdown

**Default value:** Windows Console Host

> [!IMPORTANT]
> This feature is only available when running Windows 10 Insider Program Dev Channel or Windows 11.

<br />

___

## Launch on machine startup

When set to `true`, this enables the launch of Windows Terminal at startup. Setting this to `false` will disable the startup task entry.

Note: if the Windows Terminal startup task entry is disabled either by org policy or by user action this setting will have no effect.

**Property name:** `startOnUserLogin`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `false`

<br />

___

## Behavior when starting a new terminal session

When set to `"defaultProfile"`, Windows Terminal will start a new session by opening a single tab with your default profile.

When set to `"persistedWindowLayout"`, this enables Windows Terminal to save the layout of open windows on close and restore all saved windows on starting a new session. Windows Terminal will save the layout of all open windows automatically to assist with restoration from crashes and will also save the layout when using the `quit` action. Additionally, closing the last open window by clicking the `X` button or using the `closeWindow` command will save the layout of that last window. 

Note: Currently, Windows Terminal will save the following information:

- Each window's position, size, and name

- The tab layout of each window, including the layout and profile of each pane, but not any contents of those panes

- If your shell is configured to report the [current working directory](../tutorials/new-tab-same-directory.md) that will be saved as well

**Property name:** `firstWindowPreference`

**Necessity:** Optional

**Accepts:** `"defaultProfile"`, `"persistedWindowLayout"`

**Default value:** `"defaultProfile"`

<br />

___

## Launch mode

This defines whether the terminal will launch as maximized, full screen, or in a window. Setting this to `focus` is equivalent to launching the terminal in the `default` mode, but with [focus mode](./actions.md#toggle-focus-mode) enabled. Similarly, setting this to `maximizedFocus` will result in launching the terminal in a maximized window with focus mode enabled.

**Property name:** `launchMode`

**Necessity:** Optional

**Accepts:** `"default"`, `"maximized"`, `"fullscreen"`, `"focus"`, `"maximizedFocus"`

**Default value:** `"default"`

<br />

___

## New instance behavior

This setting controls how new terminal instances attach to existing windows. This property is only used if the `--window,-w window` [command line argument](./../command-line-arguments.md) is not provided. This setting accepts the following possible values:

* `useNew`: Create a new window, always. This is how the terminal always behaved prior to version 1.7.
* `useExisting`: Create new tabs in the most recently used window on this desktop. If there's not an existing window on this virtual desktop, then create a new terminal window.
* `useAnyExisting`: Create new tabs in the most recently used window, regardless of which virtual desktop the window is on.

**Property name:** `windowingBehavior`

**Necessity:** Optional

**Accepts:** `"useNew"`, `"useExisting"`, `"useAnyExisting"`

**Default value:** `"useNew"`

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

## Center on launch

When set to `true`, the terminal window will auto-center itself on the display it opens on. The terminal will use the `"initialPosition"` to determine which display to open on.

This interacts with the other launch settings in the following ways:

* `"initialPosition": "x,y"`, `"centerOnLaunch": true`, `"launchMode": "default"`: center on the monitor that `x,y` is on.
* `"initialPosition": "x,y"`, `"centerOnLaunch": true`, `"launchMode": "maximized"`: maximized on the monitor that `x,y` is on (`centerOnLaunch` adds nothing).
* `"initialPosition": <omitted>`, `"centerOnLaunch": true`, `"launchMode": "default"`: center on the default monitor.
* `"initialPosition": <omitted>`, `"centerOnLaunch": true`, `"launchMode": "focus"`: center and enter focus mode on the default monitor.
* `"initialPosition": <omitted>`, `"centerOnLaunch": true`, `"launchMode": "maximized"`: maximized on the default monitor (`centerOnLaunch` adds nothing).

**Property name:** `centerOnLaunch`

**Necessity:** Optional

**Accepts:** `true`, `false`

**Default value:** `false`

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

## Startup actions

This sets the list of actions to execute on startup, allowing the terminal to launch with a custom set of tabs and panes by default. These actions will be applied only if no command line arguments were supplied. The list of actions is represented by a string with the same format as commands in the command line arguments. For more information about the commands format, visit the [Command line arguments page](./../command-line-arguments.md).

**Property name:** `startupActions`

**Necessity:** Optional

**Accepts:** String representing a list of commands to run

**Default value:** `""`
