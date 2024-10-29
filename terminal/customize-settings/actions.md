---
title: Windows Terminal Actions
description: Learn how to create custom actions for Windows Terminal.
author: nguyen-dows
ms.author: chrnguyen
ms.date: 05/08/2024
ms.topic: how-to
---

# Custom actions and keybindings in Windows Terminal

You can create custom actions inside Windows Terminal that give you control of how you interact with the terminal. These actions will automatically be added to the command palette.

## Action formats

Actions can be structured in the following formats:

### Commands without arguments

```json
{ "command": "commandName", "id": "User.MyCommand" }
```

For example, this default setting uses the shortcut key <kbd>Alt+F4</kbd> to close the terminal window:

```json
{ "command": "closeWindow", "id": "User.MyCloseWindow" }
```

### Commands with arguments

```json
{ "command": { "action": "commandName", "argument": "value" }, "id": "User.MyCommand" }
```

For example, this default setting uses the shortcut key <kbd>Ctrl+Shift+1</kbd> to open a new tab in the terminal based on whichever profile is listed first in your dropdown menu (typically this will open the PowerShell profile):

```json
{ "command": { "action": "newTab", "index": 0 }, "id": "User.MyNewTabAction" }
```

### Commands with command line arguments

```json
{ "command": { "action": "wt", "commandline": "value" }, "keys": "modifiers+key" }
```

For example, this default setting uses the shortcut key <kbd>Ctrl+Shift+O</kbd> to use [`wt`](../command-line-arguments.md) to open a new PowerShell tab with additional panes for Command Prompt and Ubuntu:

```json
{
  "command": 
  {
    "action": "wt",
    "commandline": "new-tab pwsh.exe ; split-pane -p \"Command Prompt\" -d C:\\ ; split-pane -p \"Ubuntu\" -H"
  },
  "keys": "ctrl+shift+o"
}
```

___

## Action properties

Actions are stored in the `actions` array and can be constructed using the following properties.

### Command

This is the command executed when the associated keys are pressed.

**Property name:** `command`

**Necessity:** Required

**Accepts:** String

### Action

This adds additional functionality to certain commands.

**Property name:** `action`

**Necessity:** Optional

**Accepts:** String

### Name

This sets the name that will appear in the command palette. If one isn't provided, the terminal will attempt to automatically generate a name.

**Property name:** `name`

**Necessity**: Optional

**Accepts:** String

### Icon

This sets the icon that displays within the command palette.

**Property name:** `icon`

**Necessity:** Optional

**Accepts:** File location as a string, or an emoji

### ID

This sets the id of this action. If one isn't provided, the terminal will generate an ID for this action. The ID is used to refer to this action when creating keybindings.

**Property name:** `id`

**Necessity**: Optional

**Accepts:** String
<br />

___

## Keybindings

Actions can be assigned keybindings by referring to them with their unique ID. For example, here is a possible `keybindings` array that assigns <kbd>Alt+F4</kbd>, <kbd>Ctrl+Shift+1</kbd> and <kbd>Ctrl+Shift+o</kbd> to the actions defined above. Multiple keybinding entries may be created for the same action.

```json
"keybindings": [
  { "keys": "alt+f4", "id": "User.MyCloseWindow" },
  { "keys": "ctrl+shift+1", "id": "User.MyNewTabAction" },
  { "keys": "ctrl+shift+o", "id": "User.MyCoolSetup"}
]
```

## Keybinding properties

Keybindings are stored in the `keybindings` array and are constructed using the following properties.

### Keys

This defines the key combinations used to call the command. Keys can have any number of modifiers with one key. Accepted modifiers and keys are listed [below](#accepted-modifiers).

If the action does not have keys, it will appear in the command palette but cannot be invoked with the keyboard.

**Property name:** `keys`

**Necessity:** Required

**Accepts:** String or array[string]

### ID

This is the ID of the action to be invoked when this keybinding is pressed.

**Property name:** `id`

**Necessity:** Required

**Accepts:** String

___

### Accepted Modifiers

`ctrl+`, `shift+`, `alt+`, `win+`

> [!NOTE]
> While the `Windows` key is supported as a modifier, the system reserves most <kbd>Win+&lt;key&gt;</kbd> key bindings. If the OS has reserved that key binding, the terminal will never receive that binding.

### Modifier keys

| Type | Keys |
| ---- | ---- |
| Function and alphanumeric keys | `f1-f24`, `a-z`, `0-9` |
| Symbols | ``` ` ```, `plus`, `-`, `=`, `[`, `]`, `\`, `;`, `'`, `,`, `.`, `/` |
| Arrow keys | `down`, `left`, `right`, `up`, `pagedown`, `pageup`, `pgdn`, `pgup`, `end`, `home` |
| Action keys | `tab`, `enter`, `esc`, `escape`, `space`, `backspace`, `delete`, `insert`, `app`, `menu`  |
| Numpad keys | `numpad_0-numpad_9`, `numpad0-numpad9`, `numpad_add`, `numpad_plus`, `numpad_decimal`, `numpad_period`, `numpad_divide`, `numpad_minus`, `numpad_subtract`, `numpad_multiply` |
| Browser keys | `browser_back`, `browser_forward`, `browser_refresh`, `browser_stop`, `browser_search`, `browser_favorites`, `browser_home` |

**Note:** `=` and `plus` are equivalents. The latter must not be confused with `numpad_plus`.

___

## Application-level commands

### Quit

This closes all open terminal windows. A confirmation dialog will appear in the current window to ensure you'd like to close all windows.

**Command name:** `quit`

**Default ID:**

```json
{ "command": "quit", "id": "Terminal.Quit" }
```

### Close window

:::row:::
:::column span="":::
This closes the current window and all tabs within it. If `confirmCloseAllTabs` is set to `true`, a confirmation dialog will appear to ensure you'd like to close all your tabs. More information on this setting can be found on the [Appearance page](./appearance.md#show-close-all-tabs-popup).

**Command name:** `closeWindow`

**Default ID:**

```json
{ "command": "closeWindow", "id": "Terminal.CloseWindow" }
```

**Default binding:**

```json
{ "keys": "alt+f4", "id": "Terminal.CloseWindow" }
```

:::column-end:::
:::column span="":::
![Windows Terminal confirm close all tabs](./../images/confirm-close-all-tabs.png)

:::column-end:::
:::row-end:::

### Find

This opens the search dialog box. More information on search can be found on the [Search page](./../search.md).

**Command name:** `find`

**Default ID:**

```json
{ "command": "find", "id": "Terminal.FindText" }
```

**Default binding:**

```json
{ "keys": "ctrl+shift+f", "id": "Terminal.FindText" }
```

### Find next/previous search match

This lets you navigate through your search matches.

**Command name:** `findMatch`

**Default IDs:**

```json
{ "command": { "action": "findMatch", "direction": "next" }, "id": "Terminal.FindNextMatch" },
{ "command": { "action": "findMatch", "direction": "prev" }, "id": "Terminal.FindPrevMatch" }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `direction` | Required | `"next"`, `"prev"` | The direction to navigate through search results. |

### Open the dropdown

This opens the dropdown menu.

**Command name:** `openNewTabDropdown`

**Default ID:**

```json
{ "command": "openNewTabDropdown", "id": "Terminal.OpenNewTabDropdown" }
```

**Default binding:**

```json
{ "keys": "ctrl+shift+space", "id": "Terminal.OpenNewTabDropdown" }
```

### Open settings files

This opens either the settings UI, custom settings file ([`settings.json`](../install.md#settings-json-file)), or default settings file (`defaults.json`), depending on the `target` field.
Without the `target` field, the custom settings file will be opened.

**Command name:** `openSettings`

**Default IDs:**

```json
{ "command": { "action": "openSettings", "target": "settingsUI" }, "id": "Terminal.OpenSettingsUI" },
{ "command": { "action": "openSettings", "target": "settingsFile" }, "id": "Terminal.OpenSettingsFile" },
{ "command": { "action": "openSettings", "target": "defaultsFile" }, "keys": "Terminal.OpenDefaultSettingsFile" }
```

**Default bindings:**
```json
{ "keys": "ctrl+,", "id": "Terminal.OpenSettingsUI" },
{ "keys": "ctrl+shift+,", "id": "Terminal.OpenSettingsFile" },
{ "keys": "ctrl+alt+,", "id": "Terminal.OpenDefaultSettingsFile" }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `target` | Optional | `"settingsFile"`, `"defaultsFile"`, `"settingsUI"`, `"allFiles"` | The settings file to open. |

### Open system menu

Opens the system menu at the top left corner of the window.

**Command name:** `openSystemMenu`

**Default ID:**

```json
{ "command": "openSystemMenu", "id": "Terminal.OpenSystemMenu" }
```

**Default binding:**

```json
{ "keys": "alt+space", "id": "Terminal.OpenSystemMenu" }
```

### Toggle full screen

This allows you to switch between full screen and default window sizes.

**Command name:** `toggleFullscreen`

**Default ID**
```json
{ "command": "toggleFullscreen", "id": "Terminal.ToggleFullscreen" }
```

**Default bindings:**

```json
{ "keys": "alt+enter", "id": "Terminal.ToggleFullscreen" },
{ "keys": "f11", "id": "Terminal.ToggleFullscreen" }
```

### Toggle focus mode

This allows you to enter "focus mode", which hides the tabs and title bar.

**Command name:** `toggleFocusMode`

**Default ID:**

```json
{ "command": "toggleFocusMode", "id": "Terminal.ToggleFocusMode" }
```

### Toggle always on top mode

This allows you toggle the "always on top" state of the window. When in "always on top" mode, the window will appear on top of all other non-topmost windows.

**Command name:** `toggleAlwaysOnTop`

**Default ID:**

```json
{ "command": "toggleAlwaysOnTop", "id": "Terminal.ToggleAlwaysOnTop" }
```

### Send input

Send arbitrary text input to the shell.
As an example the input `"text\n"` will write "text" followed by a newline to the shell.

ANSI escape sequences may be used, but escape codes like `\x1b` must be written as `\u001b`.
For instance `"\u001b[A"` will behave as if the up arrow button had been pressed.

**Command name:** `sendInput`

**Default binding:**

_This command is not currently bound in the default settings_.

```json
{ "command": { "action": "sendInput", "input": "\u001b[A" } }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `input` | Required | String | The text input to feed into the shell. |

<br />

___

## Tab management commands

### Close tab

This closes the tab at a given index. If no index is provided, use the focused tab's index.

**Command name:** `closeTab`

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `index` | Optional | Integer | Position of the tab to close. |

### Close all other tabs

This closes all tabs except for the one at an index. If no index is provided, use the focused tab's index.

**Command name:** `closeOtherTabs`

**Default ID:**

```json
{ "command": "closeOtherTabs", "id": "Terminal.CloseOtherTabs" }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `index` | Optional | Integer | Position of the tab to be kept open. |

### Close tabs after index

This closes the tabs following the tab at an index. If no index is provided, use the focused tab's index.

**Command name:** `closeTabsAfter`

**Default ID:**

```json
{ "command": "closeTabsAfter", "id": "Terminal.CloseTabsAfter" }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `index` | Optional | Integer | Position of the last tab to be kept open. |

### Duplicate tab

This makes a copy of the current tab's profile and directory and opens it. This does not include modified/added ENV VARIABLES.

**Command name:** `duplicateTab`

**Default ID:**

```json
{ "command": "duplicateTab", "id": "Terminal.DuplicateTab" }
```

**Default binding:**

```json
{ "keys": "ctrl+shift+d", "id": "Terminal.DuplicateTab" }
```

### New tab

This creates a new tab. Without any arguments, this will open the default profile in a new tab. If an index is not specified, the default profile's equivalent setting will be used. If the index doesn't map to a profile, the keys are passed directly to the terminal (or ignored if no keys were used to invoke the action).

**Command name:** `newTab`

**Default IDs:**

```json
{ "command": "newTab", "id": "Terminal.OpenNewTab" },
{ "command": { "action": "newTab", "index": 0 }, "id": "Terminal.OpenNewTabProfile0" },
{ "command": { "action": "newTab", "index": 1 }, "id": "Terminal.OpenNewTabProfile1" },
{ "command": { "action": "newTab", "index": 2 }, "id": "Terminal.OpenNewTabProfile2" },
{ "command": { "action": "newTab", "index": 3 }, "id": "Terminal.OpenNewTabProfile3" },
{ "command": { "action": "newTab", "index": 4 }, "id": "Terminal.OpenNewTabProfile4" },
{ "command": { "action": "newTab", "index": 5 }, "id": "Terminal.OpenNewTabProfile5" },
{ "command": { "action": "newTab", "index": 6 }, "id": "Terminal.OpenNewTabProfile6" },
{ "command": { "action": "newTab", "index": 7 }, "id": "Terminal.OpenNewTabProfile7" },
{ "command": { "action": "newTab", "index": 8 }, "id": "Terminal.OpenNewTabProfile8" }
```

**Default bindings:**

```json
{ "keys": "ctrl+shift+t", "id": "Terminal.OpenNewTab" },
{ "keys": "ctrl+shift+1", "id": "Terminal.OpenNewTabProfile0" },
{ "keys": "ctrl+shift+2", "id": "Terminal.OpenNewTabProfile1" },
{ "keys": "ctrl+shift+3", "id": "Terminal.OpenNewTabProfile2" },
{ "keys": "ctrl+shift+4", "id": "Terminal.OpenNewTabProfile3" },
{ "keys": "ctrl+shift+5", "id": "Terminal.OpenNewTabProfile4" },
{ "keys": "ctrl+shift+6", "id": "Terminal.OpenNewTabProfile5" },
{ "keys": "ctrl+shift+7", "id": "Terminal.OpenNewTabProfile6" },
{ "keys": "ctrl+shift+8", "id": "Terminal.OpenNewTabProfile7" },
{ "keys": "ctrl+shift+9", "id": "Terminal.OpenNewTabProfile8" }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `commandline` | Optional | Executable file name as a string | Executable run within the tab. |
| `startingDirectory` | Optional | Folder location as a string | Directory in which the tab will open. |
| `elevate` | Optional | `true`, `false`, `null` | Overrides the [`elevate`](./profile-general.md#automatically-run-as-administrator) property of the profile. When omitted, this action will behave according to the profile's `elevate` setting. When set to `true` or `false`, this action will behave as though the profile was set with `"elevate": true` or `"elevate": false` (respectively). |
| `tabTitle` | Optional | String | Title of the new tab. |
| `index` | Optional | Integer | Profile that will open based on its position in the dropdown (starting at 0). |
| `profile` | Optional | Profile's name or GUID as a string | Profile that will open based on its GUID or name. |
| `colorScheme` | Optional | The name of a color scheme as a string | The scheme to use instead of the profile's set `colorScheme` |
| `suppressApplicationTitle` | Optional | `true`, `false` | When set to `false`, applications can change the tab title by sending title change messages. When set to `true`, these messages are suppressed. If not provided, the behavior is inherited from the profile's settings. In order to enter a new tab title and have that title persist, this must be set to true. |

### Open next tab

This opens the tab to the right of the current one.

**Command name:** `nextTab`

**Default ID:**

```json
{ "command": "nextTab", "id": "Terminal.NextTab" }
```

**Default binding:**

```json
{ "keys": "ctrl+tab", "id": "Terminal.NextTab" }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `tabSwitcherMode` | Optional | `"mru"`, `"inOrder"`, `"disabled"` | Move to the next tab using `"tabSwitcherMode"`. If no mode is provided, use the globally defined one. |

### Open previous tab

This opens the tab to the left of the current one.

**Command name:** `prevTab`

**Default ID:**

```json
{ "command": "prevTab", "id": "Terminal.PrevTab" }
```

**Default binding:**

```json
{ "keys": "ctrl+shift+tab", "id": "Terminal.PrevTab" }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `tabSwitcherMode` | Optional | `"mru"`, `"inOrder"`, `"disabled"` | Move to the previous tab using `"tabSwitcherMode"`. If no mode is provided, use the globally defined one. |

### Tab search

:::row:::
:::column span="":::
This opens the tab search box.

**Command name:** `tabSearch`

**Default binding:**

_This command is not currently bound in the default settings_.

```json
{"command": "tabSearch"}
```

:::column-end:::
:::column span="":::
![Windows Terminal tab search](./../images/tab-search.gif)

:::column-end:::
:::row-end:::

### Open a specific tab

This opens a specific tab depending on the index.

**Command name:** `switchToTab`

**Default IDs:**

```json
{ "command": { "action": "switchToTab", "index": 0 }, "id": "Terminal.SwitchToTab0" },
{ "command": { "action": "switchToTab", "index": 1 }, "id": "Terminal.SwitchToTab1" },
{ "command": { "action": "switchToTab", "index": 2 }, "id": "Terminal.SwitchToTab2" },
{ "command": { "action": "switchToTab", "index": 3 }, "id": "Terminal.SwitchToTab3" },
{ "command": { "action": "switchToTab", "index": 4 }, "id": "Terminal.SwitchToTab4" },
{ "command": { "action": "switchToTab", "index": 5 }, "id": "Terminal.SwitchToTab5" },
{ "command": { "action": "switchToTab", "index": 6 }, "id": "Terminal.SwitchToTab6" },
{ "command": { "action": "switchToTab", "index": 7 }, "id": "Terminal.SwitchToTab7" }
```

**Default bindings:**

```json
{ "keys": "ctrl+alt+1", "id": "Terminal.SwitchToTab0" },
{ "keys": "ctrl+alt+2", "id": "Terminal.SwitchToTab1" },
{ "keys": "ctrl+alt+3", "id": "Terminal.SwitchToTab2" },
{ "keys": "ctrl+alt+4", "id": "Terminal.SwitchToTab3" },
{ "keys": "ctrl+alt+5", "id": "Terminal.SwitchToTab4" },
{ "keys": "ctrl+alt+6", "id": "Terminal.SwitchToTab5" },
{ "keys": "ctrl+alt+7", "id": "Terminal.SwitchToTab6" },
{ "keys": "ctrl+alt+8", "id": "Terminal.SwitchToTab7" }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `index` | Required | Integer | Tab that will open based on its position in the tab bar (starting at 0). |

### Rename tab

This command can be used to rename a tab to a specific string.

**Command name:** `renameTab`

**Default binding:**

_This command is not currently bound in the default settings_.

```json
// Rename a tab to "Foo"
{ "command": { "action": "renameTab", "title": "Foo" } }

// Reset the tab's name
{ "command": { "action": "renameTab", "title": null } }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `title` | Optional | String | The new title to use for this tab. If omitted, this command will revert the tab title back to its original value. |

### Open tab rename text box

This command changes the tab title into a text field that lets you edit the title for the current tab. Clearing the text field will reset the tab title back to the default for the current shell instance.

**Command name:** `openTabRenamer`

**Default ID:**

```json
{ "command": "openTabRenamer", "id": "Terminal.OpenTabRenamer" }
```

### Change tab color

This command can be used to change the color of a tab to a specific value.

**Command name:** `setTabColor`

**Default binding:**

_This command is not currently bound in the default settings_.

```json
// Change the tab's color to a bright magenta
{ "command": { "action": "setTabColor", "color": "#ff00ff" } }

// Reset the tab's color
{ "command": { "action": "setTabColor", "color": null } }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `color` | Optional | String, in hex format: `"#rgb"` or `"#rrggbb"` | The new color to use for this tab. If omitted, this command will revert the tab's color back to its original value. |

### Open tab color picker

This command can be used to open the color picker for the active tab. The color picker can be used to set a color for the tab at runtime.

**Command name:** `openTabColorPicker`

**Default ID:**

```json
{ "command": "openTabColorPicker", "id": "Terminal.OpenTabColorPicker" }
```

### Move tab

This command moves the tab "backward" and "forward", which is equivalent to "left" and "right" in left-to-right UI.

**Command name:** `moveTab`

**Default IDs:**

```json
// Move tab backward (left in LTR)
{ "command": { "action": "moveTab", "direction": "backward" }, "id": "Terminal.MoveTabBackward" }

// Move tab forward (right in LTR)
{ "command": { "action": "moveTab", "direction": "forward" }, "id": "Terminal.MoveTabForward" }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `direction` | Required | `"backward"`, `"forward"` | Direction in which the tab will move. |
| `window` | Optional | A window ID | See below |

`window` is optional, and follows the same format as the `--window-id` argument to the `wt.exe` command line. If it's omitted, then this will move the tab within the current window. If provided, it may either be the integer ID of a window, or the name of a window. It also accepts the following reserved values:
* `"new"` or `-1`: Always run this command in a new window
* `"last"` or `0`: Always run this command in the most recently used window

If no window exists with the given `window` ID, then a new window will be created with that id/name.

### Broadcast input

This command will toggle "broadcast mode" for a pane. When broadcast mode is enabled, all input sent to the pane will be sent to all panes in the same tab. This is useful for sending the same input to multiple panes at once.

As with any action, you can also invoke "broadcast mode" by search for "Toggle broadcast input to all panes" in the Command palette. 

**Command name:** `toggleBroadcastInput`

**Default ID:**

```json
{ "command": "toggleBroadcastInput", "id": "Terminal.ToggleBroadcastInput" }
```

:::column span="":::

![Broadcast Input](../images/broadcast-input.gif)

:::column-end:::

### Open context menu

This command will open the "right-click" context menu for the active pane. This menu has context-relevant actions for managing panes, copying and pasting, and more. This action does not require the `experimental.rightClickContextMenu` setting to be enabled.

**Command name:** `showContextMenu`

**Default ID:**

```json
{ "command": "showContextMenu", "id": "Terminal.ShowContextMenu" }
```

### Open about dialog

This command will open the about dialog for the terminal. This dialog contains information about the terminal, including the version number, the license, and more.

**Command name:** `openAbout`

**Default ID:**

```json
{ "command": "openAbout", "id": "Terminal.OpenAboutDialog" }
```

> [!IMPORTANT]
> This feature is only available in [Windows Terminal Preview](https://aka.ms/terminal-preview).

### Search the web

Attempts to open a browser window with a search for the selected text. This does nothing if there's no text selected. If the `queryUrl` parameter is not provided, the `searchWebDefaultQueryUrl` setting will be used instead. If the `queryUrl` parameter is provided, a `%s` in the string will be replaced by the selected text.

**Command name:** `searchWeb`

**Default ID:**

```json
{ "command": { "action": "searchWeb" }, "id": "Terminal.SearchWeb" },
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `queryUrl` | Required | String | URL to use to search with. A `%s` in this string will be replaced by the selected text. If omitted, will default to the `searchWebDefaultQueryUrl` setting.  |

> [!IMPORTANT]
> This feature is only available in [Windows Terminal Preview](https://aka.ms/terminal-preview).

<br />

___

## Window management commands

### New window

This creates a new window. Without any arguments, this will open the default profile in a new window (regardless of the setting of `windowingBehavior`). If an action is not specified, the default profile's equivalent setting will be used.

**Command name:** `newWindow`

**Default ID:**

```json
{ "command": "newWindow", "id": "Terminal.OpenNewWindow" },
```

**Default binding:**

```json
{ "keys": "ctrl+shift+n", "id": "Terminal.OpenNewWindow" },
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `commandline` | Optional | Executable file name as a string | Executable run within the tab. |
| `startingDirectory` | Optional | Folder location as a string | Directory in which the window will open. |
| `tabTitle` | Optional | String | Title of the window tab. |
| `index` | Optional | Integer | Profile that will open based on its position in the dropdown (starting at 0). |
| `profile` | Optional | Profile's name or GUID as a string | Profile that will open based on its GUID or name. |
| `suppressApplicationTitle` | Optional | `true`, `false` | When set to `false` allows applications to change tab title by sending title change messages. When set to `true` suppresses these messages. If not provided, the behavior is inherited from profile settings. |

### Rename window

This command can be used to rename a window to a specific string.

**Command name:** `renameWindow`

**Default binding:**

_This command is not currently bound in the default settings_.

```json
// Rename a window to "Foo"
{ "command": { "action": "renameWindow", "name": "Foo" } }

// Reset the window's name
{ "command": { "action": "renameWindow", "name": null } }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `name` | Optional | String | The new name to use for this window. If omitted, this command will revert the window name back to its original value. |

### Open window rename dialog

This command changes displays a popup window that lets you edit the name for the current window. Clearing the text field will reset the window name.

**Command name:** `openWindowRenamer`

**Default ID:**

```json
{ "command": "openWindowRenamer", "id": "Terminal.OpenWindowRenamer" }
```

### Identify window

This pops up an overlay on the focused window that displays the window's name and index.

**Command name:** `identifyWindow`

**Default ID:**

```json
{"command": "identifyWindow", "id": "Terminal.IdentifyWindow" },
```

### Identify windows

This pops up an overlay on all windows that displays each window's name and index.

**Command name:** `identifyWindows`

**Default binding:**

_This command is not currently bound in the default settings_.

```json
{ "command": "identifyWindows" },
```

<br />

___

## Pane management commands

### Split a pane

This halves the size of the active pane and opens another. Without any arguments, this will open the default profile in the new pane. If an action is not specified, the default profile's equivalent setting will be used.

**Command name:** `splitPane`

**Default IDs:**

```json
{ "command": { "action": "splitPane", "splitMode": "duplicate", "split": "auto" }, "id": "Terminal.DuplicatePaneAuto" },
{ "command": { "action": "splitPane", "split": "up" }, "id": "Terminal.SplitPaneUp" },
{ "command": { "action": "splitPane", "split": "down" }, "id": "Terminal.SplitPaneDown" },
{ "command": { "action": "splitPane", "split": "left" }, "id": "Terminal.SplitPaneLeft" },
{ "command": { "action": "splitPane", "split": "right" }, "id": "Terminal.SplitPaneRight" },
{ "command": { "action": "splitPane", "splitMode": "duplicate", "split": "down" }, "id": "Terminal.DuplicatePaneDown" },
{ "command": { "action": "splitPane", "splitMode": "duplicate", "split": "right" }, "id": "Terminal.DuplicatePaneRight" }
```

**Default bindings:**

```json
{ "keys": "alt+shift+d", "id": "Terminal.DuplicatePaneAuto" },
{ "keys": "alt+shift+-", "id": "Terminal.DuplicatePaneDown" },
{ "keys": "alt+shift+plus", "id": "Terminal.DuplicatePaneRight" }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `split` | Required | `"vertical"`, `"horizontal"`, `"auto"`, `"up"`, `"right"`, `"down"`, `"left"` | How the pane will split. `"auto"` will split in the direction that provides the most surface area. |
| `commandline` | Optional | Executable file name as a string | Executable run within the pane. |
| `startingDirectory` | Optional | Folder location as a string | Directory in which the pane will open. |
| `elevate` | Optional | `true`, `false`, `null` | Overrides the [`elevate`](./profile-general.md#automatically-run-as-administrator) property of the profile. When omitted, this action will behave according to the profile's `elevate` setting. When set to `true` or `false`, this action will behave as though the profile was set with `"elevate": true` or `"elevate": false` (respectively). |
| `tabTitle` | Optional | String | Title of the tab when the new pane is focused. |
| `index` | Optional | Integer | Profile that will open based on its position in the dropdown (starting at 0). |
| `profile` | Optional | Profile's name or GUID as a string | Profile that will open based on its GUID or name. |
| `colorScheme` | Optional | The name of a color scheme as a string | The scheme to use instead of the profile's set `colorScheme` |
| `suppressApplicationTitle` | Optional | `true`, `false` | When set to `false`, applications can change the tab title by sending title change messages. When set to `true`, these messages are suppressed. If not provided, the behavior is inherited from the profile's settings. |
| `splitMode` | Optional | `"duplicate"` | Controls how the pane splits. Only accepts `"duplicate"`, which will duplicate the focused pane's profile into a new pane. |
| `size` | Optional | Float | Specify how large the new pane should be, as a fraction of the current pane's size. `1.0` would be "all of the current pane", and `0.0` is "None of the parent". Defaults to `0.5`. |

### Close pane

This closes the active pane. If there aren't any split panes, this will close the current tab. If there is only one tab open, this will close the window.

**Command name:** `closePane`

**Default ID:**

```json
{ "command": "closePane", "id": "Terminal.ClosePane" }
```

**Default binding:**

```json
{ "keys": "ctrl+shift+w", "id": "Terminal.ClosePane" }
```

### Move pane focus

This changes focus to a different pane depending on the direction. Setting the `direction` to `"previous"` will move focus to the most recently used pane.

**Command name:** `moveFocus`

**Default IDs:**

```json
{ "command": { "action": "moveFocus", "direction": "down" }, "id": "Terminal.MoveFocusDown" },
{ "command": { "action": "moveFocus", "direction": "left" }, "id": "Terminal.MoveFocusLeft" },
{ "command": { "action": "moveFocus", "direction": "right" }, "id": "Terminal.MoveFocusRight" },
{ "command": { "action": "moveFocus", "direction": "up" }, "id": "Terminal.MoveFocusUp" },
{ "command": { "action": "moveFocus", "direction": "previous" }, "id": "Terminal.MoveFocusPrevious" }
```

**Default bindings:**

```json
{ "keys": "alt+down", "id": "Terminal.MoveFocusDown" },
{ "keys": "alt+left", "id": "Terminal.MoveFocusLeft" },
{ "keys": "alt+right", "id": "Terminal.MoveFocusRight" },
{ "keys": "alt+up", "id": "Terminal.MoveFocusUp" },
{ "keys": "ctrl+alt+left", "id": "Terminal.MoveFocusPrevious" }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `direction` | Required | `"left"`, `"right"`, `"up"`, `"down"`, `"previous"`, `"previousInOrder"`, `"nextInOrder"`, `"first"`, `"parent"`, `"child"` | Direction in which the focus will move. |

Accepted `direction` values
* `up`, `down`, `left`, or `right` move focus in the given direction.
* `first` moves focus to the first leaf pane in the tree.
* `previous` moves the focus to the most recently used pane before the current pane.
* `nextInOrder`, `previousInOrder` moves the focus to the next or previous pane in order of creation.
* `parent` moves the focus to select the parent pane of the current pane. This enables the user to select multiple panes at once
* `child` moves the focus to the first child pane of this pane.

### Move pane

Move the currently active pane to a different tab in the window.

**Command name:** `movePane`

**Default IDs:**

```json
{ "command": { "action": "movePane", "index": 0 }, "id": "Terminal.MovePaneToTab0" },
{ "command": { "action": "movePane", "index": 1 }, "id": "Terminal.MovePaneToTab1" },
{ "command": { "action": "movePane", "index": 2 }, "id": "Terminal.MovePaneToTab2" },
{ "command": { "action": "movePane", "index": 3 }, "id": "Terminal.MovePaneToTab3" },
{ "command": { "action": "movePane", "index": 4 }, "id": "Terminal.MovePaneToTab4" },
{ "command": { "action": "movePane", "index": 5 }, "id": "Terminal.MovePaneToTab5" },
{ "command": { "action": "movePane", "index": 6 }, "id": "Terminal.MovePaneToTab6" },
{ "command": { "action": "movePane", "index": 7 }, "id": "Terminal.MovePaneToTab7" },
{ "command": { "action": "movePane", "index": 8 }, "id": "Terminal.MovePaneToTab8" }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `index` | Required | number | The zero-indexed index of the tab to move to |

### Swap panes

Swap the position of two panes in a tab. This operates on the active pane, and a target pane, as designated by the `direction` parameter.

**Command name:** `swapPane`

**Default IDs:**

```json
{ "command": { "action": "swapPane", "direction": "down" }, "id": "Terminal.SwapPaneDown" },
{ "command": { "action": "swapPane", "direction": "left" }, "id": "Terminal.SwapPaneLeft" },
{ "command": { "action": "swapPane", "direction": "right" }, "id": "Terminal.SwapPaneRight" },
{ "command": { "action": "swapPane", "direction": "up" }, "id": "Terminal.SwapPaneUp" },
{ "command": { "action": "swapPane", "direction": "previous"}, "id": "Terminal.SwapPanePrevious" },
{ "command": { "action": "swapPane", "direction": "previousInOrder"}, "id": "Terminal.SwapPanePreviousInOrder" },
{ "command": { "action": "swapPane", "direction": "nextInOrder"}, "id": "Terminal.SwapPaneNextInOrder" },
{ "command": { "action": "swapPane", "direction": "first" }, "id": "Terminal.SwapPaneFirst" }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `direction` | Required | `"left"`, `"right"`, `"up"`, `"down"`, `"previous"`, `"previousInOrder"`, `"nextInOrder"`, `"first"`, `"parent"`, `"child"` | Direction in which the focus will move. |

Accepted `direction` values (these are the same values as the `moveFocus` command)
* `up`, `down`, `left`, or `right`: Swap the active pane with the one in the given direction.
* `first`: Swap the active pane with the first leaf pane in the tree.
* `previous`: Swap the active pane with the most recently used pane before the current pane.
* `nextInOrder`, `previousInOrder`: Swap the active pane with the next or previous pane in order of creation.
* `parent`: Does nothing.
* `child`: Does nothing.

### Zoom a pane

:::row:::
:::column span="":::
This expands the focused pane to fill the entire contents of the window.

**Command name:** `togglePaneZoom`

**Default ID:**

```json
{ "command": "togglePaneZoom", "id": "Terminal.TogglePaneZoom" }
```

:::column-end:::
:::column span="":::
![Windows Terminal toggle pane zoom](./../images/toggle-pane-zoom.gif)

:::column-end:::
:::row-end:::

### Resize a pane

This changes the size of the active pane.

**Command name:** `resizePane`

**Default IDs:**

```json
{ "command": { "action": "resizePane", "direction": "down" }, "id": "Terminal.ResizePaneDown" },
{ "command": { "action": "resizePane", "direction": "left" }, "id": "Terminal.ResizePaneLeft" },
{ "command": { "action": "resizePane", "direction": "right" }, "id": "Terminal.ResizePaneRight" },
{ "command": { "action": "resizePane", "direction": "up" }, "id": "Terminal.ResizePaneUp" }
```

**Default bindings:**

```json
{ "keys": "alt+shift+down", "id": "Terminal.ResizePaneDown" },
{ "keys": "alt+shift+left", "id": "Terminal.ResizePaneLeft" },
{ "keys": "alt+shift+right", "id": "Terminal.ResizePaneRight" },
{ "keys": "alt+shift+up", "id": "Terminal.ResizePaneUp" }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `direction` | Required | `"left"`, `"right"`, `"up"`, `"down"` | Direction in which the pane will be resized. |

### Mark a pane as read-only

You can mark a pane as read-only, which will prevent input from going into the text buffer. If you attempt to close or input text into a read-only pane, the terminal will display a popup warning instead.

**Command name:** `toggleReadOnlyMode`

**Default ID:**

```json
{ "command": "toggleReadOnlyMode", "id": "Terminal.ToggleReadOnlyMode" }
```

You can enable read-only mode on a pane. This works similarly to toggling, however, will not switch state if triggered again.

**Command name:** `enableReadOnlyMode`

**Default ID:**

```json
{ "command": "enableReadOnlyMode", "id": "Terminal.EnableReadOnlyMode" }
```

You can disable read-only mode on a pane. This works similarly to toggling, however, will not switch state if triggered again.

**Command name:** `disableReadOnlyMode`

**Default ID:**

```json
{ "command": "disableReadOnlyMode", "id": "Terminal.DisableReadOnlyMode" }
```

### Restart a pane

This command will manually restart the `commandline` in the active pane. This is especially useful for scenarios like `ssh`, where you might want to restart a connection without closing the pane.

Note that this will terminate the process in the pane, if it is currently running.

**Command name:** `restartConnection`

**Default ID:**

```json
{ "command": "restartConnection", "id": "Terminal.RestartConnection" }
```

<br />

___

## Clipboard integration commands

### Copy

This copies the selected terminal content to your clipboard. If no selection exists, the key chord is sent directly to the terminal.

**Command name:** `copy`

**Default ID:**

```json
{ "command": { "action": "copy", "singleLine": false }, "id": "Terminal.CopyToClipboard" }
```

**Default bindings:**

```json
{ "keys": "ctrl+c", "id": "Terminal.CopyToClipboard" },
{ "keys": "ctrl+shift+c", "id": "Terminal.CopyToClipboard" },
{ "keys": "ctrl+insert", "id": "Terminal.CopyToClipboard" },
{ "keys": "enter", "id": "Terminal.CopyToClipboard" }
```

#### Parameters

 | Name | Necessity | Accepts | Description |
 | ---- | --------- | ------- | ----------- |
 | `singleLine` | Optional | `true`, `false` | When `true`, the copied content will be copied as a single line. When `false`, newlines persist from the selected text. |
 | `copyFormatting` | Optional | `true`, `false`, `"all"`, `"none"`, `"html"`, `"rtf"` | When `true`, the color and font formatting of the selected text is also copied to your clipboard. When `false`, only plain text is copied to your clipboard. You can also specify which formats you would like to copy. When `null`, the global `"copyFormatting"` behavior is inherited. |

### Paste

This inserts the content that was copied onto the clipboard.

**Command name:** `paste`

**Default ID:**

```json
{ "command": "paste", "id": "Terminal.PasteFromClipboard" }
```

**Default bindings:**

```json
{ "keys": "ctrl+v", "id": "Terminal.PasteFromClipboard" },
{ "keys": "ctrl+shift+v", "id": "Terminal.PasteFromClipboard" },
{ "keys": "shift+insert", "id": "Terminal.PasteFromClipboard" }
```

### Expand selection to word

If a selection exists, this expands the selection to fully encompass any words partially selected.

**Command name:** `expandSelectionToWord`

**Default ID:**

```json
{ "command": "expandSelectionToWord", "id": "Terminal.ExpandSelectionToWord" }
```

### Select all

This selects all of the content in the text buffer.

**Command name:** `selectAll`

**Default ID:**

```json
{ "command": "selectAll", "id": "Terminal.SelectAll" }
```

**Default binding:**

```json
{ "keys": "ctrl+shift+a", "id": "Terminal.SelectAll" }
```

### Mark mode

This toggles mark mode. Mark mode is a mode where you can use the keyboard to create a selection at the cursor's position in the terminal.

**Command name:** `markMode`

**Default ID:**

```json
{ "command": "markMode", "id": "Terminal.ToggleMarkMode" }
```

**Default binding:**

```json
{ "keys": "ctrl+shift+m", "id": "Terminal.ToggleMarkMode" }
```

### Switch selection marker

When modifying a selection using the keyboard, you are moving one end of the selection around. You can use this action to switch to the other selection marker.

**Command name:** `switchSelectionEndpoint`

**Default ID:**

```json
{ "command": "switchSelectionEndpoint", "id": "Terminal.SwitchSelectionEndpoint" },
```

### Toggle block selection

Makes the existing selection a block selection, meaning that the selected area is a rectangle, as opposed to wrapping to the beginning and end of each line.

**Command name:** `toggleBlockSelection`

**Default ID:**

```json
{ "command": "toggleBlockSelection", "id": "Terminal.ToggleBlockSelection" },
```
<br />

___

## Scrollback commands

### Scroll up

This scrolls the screen up by the number of rows defined by `"rowsToScroll"`. If `"rowsToScroll"` is not provided, it will scroll up the amount defined by the system default, which is the same amount as mouse scrolling.

**Command name:** `scrollUp`

**Default ID:**

```json
{ "command": "scrollUp", "id": "Terminal.ScrollUp" }
```

**Default binding:**

```json
{ "keys": "ctrl+shift+up", "id": "Terminal.ScrollUp" }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `rowsToScroll` | Optional | Integer | The number of rows to scroll. |

### Scroll down

This scrolls the screen down by the number of rows defined by `"rowsToScroll"`. If `"rowsToScroll"` is not provided, it will scroll down the amount defined by the system default, which is the same amount as mouse scrolling.

**Command name:** `scrollDown`

**Default ID:**

```json
{ "command": "scrollDown", "id": "Terminal.ScrollDown" }
```

**Default binding:**

```json
{ "keys": "ctrl+shift+down", "id": "Terminal.ScrollDown" }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `rowsToScroll` | Optional | Integer | The number of rows to scroll. |

### Scroll up a whole page

This scrolls the screen up by a whole page, which is the height of the window.

**Command name:** `scrollUpPage`

**Default ID:**

```json
{ "command": "scrollUpPage", "id": "Terminal.ScrollUpPage" }
```

**Default binding:**

```json
{ "keys": "ctrl+shift+pgup", "id": "Terminal.ScrollUpPage" }
```

### Scroll down a whole page

This scrolls the screen down by a whole page, which is the height of the window.

**Command name:** `scrollDownPage`

**Default ID:**

```json
{ "command": "scrollDownPage", "id": "Terminal.ScrollDownPage" }
```

**Default binding:**

```json
{ "keys": "ctrl+shift+pgdn", "id": "Terminal.ScrollDownPage" }
```

### Scroll to the earliest history

This scrolls the screen up to the top of the input buffer.

**Command name:** `scrollToTop`

**Default ID:**

```json
{ "command": "scrollToTop", "id": "Terminal.ScrollToTop" }
```

**Default binding:**

```json
{ "keys": "ctrl+shift+home", "id": "Terminal.ScrollToTop" }
```

### Scroll to the latest history

This scrolls the screen down to the bottom of the input buffer.

**Command name:** `scrollToBottom`

**Default ID:**

```json
{ "command": "scrollToBottom", "id": "Terminal.ScrollToBottom" }
```

**Default binding:**

```json
{ "keys": "ctrl+shift+end", "id": "Terminal.ScrollToBottom" }
```

<br />

### Clear buffer

This action can be used to manually clear the terminal buffer. This is useful for scenarios where you're not sitting at a command-line shell prompt and can't easily run `Clear-Host`/`cls`/`clear`.

**Command name:** `clearBuffer`

**Default ID:**

```json
{ "command": { "action": "clearBuffer", "clear": "all" }, "id": "Terminal.ClearBuffer" }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `clear` | Optional | `"screen"`, `"scrollback"`, `"all"` | What part of the screen to clear. <ul><li>`"screen"`: Clear the terminal viewport content. Leaves the scrollback untouched. Moves the cursor row to the top of the viewport (unmodified).</li><li>`"scrollback"`: Clear the scrollback. Leaves the viewport untouched.</li><li>`"all"` (_default_): Clear the scrollback and the visible viewport. Moves the cursor row to the top of the viewport. </li></ul> |


<br />
___

## Visual adjustment commands

### Adjust font size

This changes the text size by a specified point amount.

**Command name:** `adjustFontSize`

**Default IDs:**

```json
{ "command": { "action": "adjustFontSize", "delta": 1 }, "id": "Terminal.IncreaseFontSize" },
{ "command": { "action": "adjustFontSize", "delta": -1 }, "id": "Terminal.DecreaseFontSize" }
```

**Default bindings:**

```json
{ "keys": "ctrl+plus", "id": "Terminal.IncreaseFontSize" },
{ "keys": "ctrl+minus", "id": "Terminal.DecreaseFontSize" },
{ "keys": "ctrl+numpad_plus", "id": "Terminal.IncreaseFontSize" },
{ "keys": "ctrl+numpad_minus", "id": "Terminal.DecreaseFontSize" }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `delta` | Required | Integer | Amount of size change per command invocation. |

### Reset font size

This resets the text size to the default value.

**Command name:** `resetFontSize`

**Default ID:**

```json
{ "command": "resetFontSize", "id": "Terminal.ResetFontSize" }
```

**Default bindings:**

```json
{ "keys": "ctrl+0", "id": "Terminal.ResetFontSize" },
{ "keys": "ctrl+numpad_0", "id": "Terminal.ResetFontSize" }
```

### Adjust opacity

This changes the opacity of the window. If `relative` is set to true, it will adjust the opacity relative to the current opacity. Otherwise, it will set the opacity directly to the given `opacity`

**Command name:** `adjustOpacity`

**Default bindings:**

```json
{ "command": { "action": "adjustOpacity", "relative": false, "opacity": 0 } },
{ "command": { "action": "adjustOpacity", "relative": false, "opacity": 25 } },
{ "command": { "action": "adjustOpacity", "relative": false, "opacity": 50 } },
{ "command": { "action": "adjustOpacity", "relative": false, "opacity": 75 } },
{ "command": { "action": "adjustOpacity", "relative": false, "opacity": 100 } }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `opacity` | Optional | Integer | How opaque the terminal should become or how much the opacity should be changed by, depending on the value of `relative` |
| `relative` | Optional | Boolean | If true, then adjust the current opacity by the given `opacity` parameter. If false, set the opacity to exactly that value. |

### Toggle pixel shader effects

This toggles any pixel shader effects enabled in the terminal. If the user specified a valid shader with `experimental.pixelShaderPath`, this action will toggle that shader on/off. This will also toggle the "retro terminal effect", which is enabled with the profile setting `experimental.retroTerminalEffect`.

**Command name:** `toggleShaderEffects`

**Default ID:**

```json
{ "command": "toggleShaderEffects", "id": "Terminal.ToggleShaderEffects" }
```

> [!CAUTION]
> The `toggleRetroEffect` action is no longer available in versions 1.6 and later. It is recommended that you use `toggleShaderEffects` instead.

### Set the color scheme

Changes the active color scheme.

**Command name:** `setColorScheme`

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `colorScheme` | Required | String | The `name` of the color scheme to apply. |

**Example declaration:**

```json
{ "command": { "action": "setColorScheme", "colorScheme": "Campbell" }, "id": "User.SetSchemeToCampbell" }
```

### Add scroll mark

Adds a scroll mark to the text buffer. If there's a selection, the mark is placed at the selection, otherwise it's placed at the cursor row.

**Command name:** `addMark`

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `color` | Optional | String, in hex format: `"#rgb"` or `"#rrggbb"` | The color of the mark. |

**Example declaration:**

```json
{ "command": { "action": "addMark", "color": "#ff00ff" }, "id": "User.AddMark" }
```

> [!IMPORTANT]
> This action became stable in v1.21. Before that version, it was only available in [Windows Terminal Preview](https://aka.ms/terminal-preview)

### Scroll to mark

Scrolls to the scroll mark in the given direction. For more info, see [Scroll marks](../customize-settings/profile-advanced.md#scroll-marks-preview) and [Shell Integration](../tutorials/shell-integration.md).

**Command name:** `scrollToMark`

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `direction` | Required | `"first"`, `"previous"`, `"next"`, `"last"` | The direction in which to scroll. |

**Example declaration:**

```json
{ "command": { "action": "scrollToMark", "direction": "previous" }, "id": "User.ScrollToMark" }
```

> [!IMPORTANT]
> This action became stable in v1.21. Before that version, it was only available in [Windows Terminal Preview](https://aka.ms/terminal-preview)

### Clear mark

Clears scroll mark at the current position, either at a selection if there is one or at the cursor position. This is an experimental feature, and its continued existence is not guaranteed.

**Command name:** `clearMark`

**Example declaration:**

```json
{ "command": { "action": "clearMark" }, "id": "User.ClearMark" }
```

> [!IMPORTANT]
> This action became stable in v1.21. Before that version, it was only available in [Windows Terminal Preview](https://aka.ms/terminal-preview)

### Clear all marks

Clears all scroll marks in the text buffer. This is an experimental feature, and its continued existence is not guaranteed.

**Command name:** `clearAllMarks`

**Example declaration:**

```json
{ "command": { "action": "clearAllMarks" }, "id": "User.ClearAllMarks" }
```

> [!IMPORTANT]
> This action became stable in v1.21. Before that version, it was only available in [Windows Terminal Preview](https://aka.ms/terminal-preview)

<br />
___

## Suggestions

### Open suggestions menu

:::row:::
:::column span="":::

This allows the user to open the suggestions menu. The entries in the suggestions menu are controlled by the `source` property. The suggestions menu behaves much like the command palette. Typing in the text box will filter the results to only show entries that match the text. Pressing `enter` will execute the selected entry. Pressing `esc` will close the menu.

:::column-end:::
:::column span="":::

![Suggestions UI](../images/SuggestionsUI.gif)

:::column-end:::
:::row-end:::

**Command name:** `showSuggestions`

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `source` | Required | any number of `"recentCommands"`, `"tasks"`, or `"all"` | Which suggestion sources to use to populate this menu. See below for a description of each.  |
| `useCommandline` | Optional | Boolean | If [shell integration](./../tutorials/shell-integration.md) is enabled, and this is `true`, the suggestions menu will be pre-populated with the contents of the current commandline. Defaults to `true` |

#### Suggestion sources

The following suggestion sources are supported:

* `"recentCommands"`: This will populate the suggestions menu with the most recently used commands. These are powered by shell integration, so they will only be available if you have your shell configured to support shell integration. See [Shell Integration](./../tutorials/shell-integration.md) for more information.
* `"tasks"`: This will populate the suggestions menu with all of the `sendInput` actions from your settings.
* `"all"`: Use all suggestion sources.

These values can be used by themselves as a string parameter value, or combined as an array. For example:
```json
{ "command": { "action": "showSuggestions", "source": ["recentCommands", "tasks"] } },
{ "command": { "action": "showSuggestions", "source": "all" } },
{ "command": { "action": "showSuggestions", "source": "recentCommands" } },
```

In the above example, the first two commands will open the suggestions menu with both recent commands and tasks. The third command will open the suggestions menu with only recent commands.

> [!IMPORTANT]
> This feature is only available in [Windows Terminal Preview](https://aka.ms/terminal-preview).

<br />
___

## Buffer exporting

### Export buffer

This allows the user to export the text of the buffer to a file. If the file doesn't exist, it will be created. If the file already exists, its contents will be replaced with the Terminal buffer text.

**Command name:** `exportBuffer`

**Default ID:**

```json
{ "command": { "action": "exportBuffer" }, "id": "Terminal.ExportBuffer" }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `path` | Optional | String | If provided, then the Terminal will export the buffer contents to the given file. Otherwise, the terminal will open a file picker to choose the file to export to. |


<br />
___

## Global commands

### Global summon

This is a special action that works globally in the OS, rather than only in the context of the terminal window. When pressed, this action will summon the terminal window. Which window is summoned, where the window is summoned to, and how the window behaves when summoning it, is controlled by the properties on this action.

**Notes**
* Any keys bound to `globalSummon` actions in the terminal will not work in other applications while the terminal is running - they will always focus the terminal window.

* If another running application already registered for the given `keys` using the `RegisterHotKey` API, the terminal will be unable to listen for those key strokes.

* Elevated and unelevated instances of the terminal will not be able to both register for the same keys. The same applies to both Preview and Stable versions of the terminal - the first one to be launched will always win.

* These key strokes will only work when an instance of the terminal is already running. To launch the terminal automatically on login, see [`startOnUserLogin`](./startup.md#launch-on-machine-startup).

**Command name:** `globalSummon`

**Default binding:**

_This command is not currently bound in the default settings_.

```json
{ "command": { "action": "globalSummon" } }
```

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `desktop` | Optional | `any`, `toCurrent`, `onCurrent` | This controls how the terminal should interact with virtual desktops.<ul><li>`"any"`: Leave the window on whichever desktop it's already on - will switch to that desktop as the window is activated.</li><li>`"toCurrent"` (_default_): Move the window to the current virtual desktop.</li><li>`"onCurrent"`: Only summon the window if it's already on the current virtual desktop.</li></ul> |
| `monitor` | Optional | `any`, `toCurrent`, `toMouse` | This controls the monitor that the window will be summoned from/to.<ul><li>`"any"`: Summon the most recently used window, regardless of which monitor it's currently on.</li><li>`"toCurrent"`: Summon the most recently used window to the monitor with the current foreground window.</li><li>`"toMouse"` (_default_): Summon the most recently used window to the monitor where the mouse cursor is.</li></ul> |
| `name` | Optional | String | When omitted (_default_), use `monitor` and `desktop` to find the appropriate most-recently-used window to summon. When provided, summon the window whose name or ID matches the given `name` value. If no such window exists, then create a new window with that name. |
| `dropdownDuration` | Optional | Integer | Defaults to `0`. When provided with a positive number, "slide" the window in from the top of the screen using an animation that lasts `dropdownDuration` milliseconds. `200` is a reasonable value for this setting.  |
| `toggleVisibility` | Optional | `true`, `false` | Defaults to `true`. When `true`, pressing the assigned keys for this action will dismiss (minimize) the window when the window is currently the foreground window. When `false`, pressing the assigned keys will only ever bring the window to the foreground. |

When `name` is provided _with_ `monitor` or `desktop`, `name` behaves in the following
ways:
  * `desktop`
    - `"any"`: Go to the desktop the given window is already on.
    - `"toCurrent"`: If the window is on another virtual desktop, then move it to the currently active one.
    - `"onCurrent"`: If the window is on another virtual desktop, then move it to the currently active one.
  * `monitor`
    - `"any"`: Leave the window on the monitor it is already on.
    - `"toCurrent"`: If the window is on another monitor, then move it to the monitor with the current foreground window.
    - `"toMouse"`: If the window is on another monitor, then move it to the monitor with the mouse cursor on it.

The `desktop` and `monitor` properties can be combined in the following ways:

| Combinations | **`"desktop": "any"`** | **`"desktop": "toCurrent"`**| **`"desktop": "onCurrent"`**| Not included |
| ------------ | ---------------------- | --------------------------- | --------------------------- | ------------ |
| **`"monitor": "any"`**| Go to the desktop the window is on (leave position alone) | Move the window to this desktop (leave position alone) | If there isn't one on this desktop:<ul><li>Create a new one in the default position</li></ul>Else:<ul><li>Activate the one on this desktop (don't move it)</li></ul> | Summon the MRU window |
| **`"monitor": "toCurrent"`**| Go to the desktop the window is on, move to the monitor with the foreground window | Move the window to this desktop, move to the monitor with the foreground window | If there isn't one on this desktop:<ul><li>Create a new one</li></ul>Else:<ul><li>Activate the one on this desktop, move to the monitor with the foreground window</li></ul> | Summon the MRU window TO the monitor with the foreground window |
| **`"monitor": "toMouse"`**| Go to the desktop the window is on, move to the monitor with the mouse | Move the window to this desktop, move to the monitor with the mouse | If there isn't one on this desktop:<ul><li>Create a new one</li></ul>Else:<ul><li>Activate the one on this desktop, move to the monitor with the mouse</li></ul> | Summon the MRU window TO the monitor with the mouse |
| **Not included** | Leave where it is | Move to current desktop | On current desktop only | N/A |

#### Examples

```jsonc

// Summon the most recently used (MRU) window, to the current virtual desktop,
// to the monitor the mouse cursor is on, without an animation. If the window is
// already in the foreground, then minimize it.
{ "command": { "action": "globalSummon" }, "id": "User.MyGlobalSummon" },

// Summon the MRU window, by going to the virtual desktop the window is
// currently on. Move the window to the monitor the mouse is on.
{ "command": { "action": "globalSummon", "desktop": "any" }, "id": "User.MyGlobalSummonAnyDesktop" },

// Summon the MRU window to the current desktop, leaving the position of the window untouched.
{ "command": { "action": "globalSummon", "monitor": "any" }, "id": "User.MyGlobalSummonAnyMonitor" },

// Summon the MRU window, by going to the virtual desktop the window is
// currently on, leaving the position of the window untouched.
{ "command": { "action": "globalSummon", "desktop": "any", "monitor": "any" }, "id": "User.MyGlobalSummonAnywhere" },

// Summon the MRU window with a dropdown duration of 200ms.
{ "command": { "action": "globalSummon", "dropdownDuration": 200 }, "id": "User.MyGlobalSummonDrop" },

// Summon the MRU window. If the window is already in the foreground, do nothing.
{ "command": { "action": "globalSummon", "toggleVisibility": false }, "id": "User.MyGlobalSummonIfNotVisible" },

// Summon the window named "_quake". If no window with that name exists, then create a new window.
{ "command": { "action": "globalSummon", "name": "_quake" }, "id": "User.MyGlobalSummonQuake" }
```

### Open the quake mode window

:::row:::
:::column span="":::
This action is a special variation of the [`globalSummon`](#global-commands) action. It specifically summons the [quake window](../tips-and-tricks.md#quake-mode). It is a shorthand for the following `globalSummon` action:

```json
{
    "id": "User.MySummonQuake",
    "command": {
        "action": "globalSummon",
        "name": "_quake",
        "dropdownDuration": 200,
        "toggleVisibility": true,
        "monitor": "toMouse",
        "desktop": "toCurrent"
    }
}
```

If you'd like to change the behavior of the `quakeMode` action, we recommended creating a new `globalSummon` entry in `actions` with the settings you prefer.

**Command name:** `quakeMode`

**Default ID:**

```json
{ "command": "quakeMode", "id": "Terminal.QuakeMode" }
```

:::column-end:::
:::column span="":::
![Windows Terminal quake mode](./../images/quake-mode.gif)

:::column-end:::
:::row-end:::

<br />

___

## Run multiple actions

This action allows the user to bind multiple sequential actions to one command. These actions do not support IDs.

**Command name:** `multipleActions`

#### Parameters

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `actions` | Required | Array of Actions | The list of `action` to run. |

#### Example

```jsonc
{ "name": "Create My Layout", "command": {
    "action": "multipleActions",
    "actions": [
        // Create a new tab with 3 panes
        { "action": "newTab", "tabTitle": "Work", "colorScheme": "One Half Dark" },
        { "action": "splitPane", "split": "vertical", "profile": "Windows PowerShell", "tabTitle": "Work", "colorScheme": "Campbell Powershell", },
        { "action": "splitPane", "split": "horizontal", "profile": "Windows PowerShell", "tabTitle": "Work", "colorScheme": "Campbell Powershell", },

        // Create a second tab
        { "action": "newTab", "tabTitle": "Misc"},

        // Go back to the first tab and zoom the first pane
        { "action": "prevTab", "tabSwitcherMode": "disabled" },
        { "action": "moveFocus", "direction": "first"},
        "togglePaneZoom"
        ]
}}
```

<br />

___

## Unbind keys (disable keybindings)

You can disable keybindings or "unbind" the associated keys from any command. This may be necessary when using underlying terminal applications (such as VIM). The unbound key will pass to the underlying terminal.

**Command name:** `unbound`

**Example using unbound:**

For example, to unbind the shortcut keys <kbd>Alt+Shift+-</kbd>" and <kbd>Alt+Shift+=</kbd>", include these commands in the `actions` section of your [settings.json file](../install.md#settings-json-file).

```json
{
    "keybindings": [
        { "id": "unbound", "keys": "alt+shift+-" },
        { "id": "unbound", "keys": "alt+shift+=" }
    ]
}
```

**Example using null:**

You can also unbind a keystroke that is bound by default to an action by setting `"id"` to `null`. This will also allow the keystroke to associate with the command line application setting instead of performing the default action.

```json
{
   "id" : null, "keys" : ["ctrl+v"]
}
```

**Use-case scenario:**

Windows Terminal uses the shortcut key binding <kbd>Ctrl+V</kbd> as the paste command. When working with a WSL command line, you may want to use a Linux application such as Vim to edit files. However, Vim relies on the <kbd>Ctrl+V</kbd> key binding to use [blockwise Visual mode](http://vimdoc.sourceforge.net/htmldoc/visual.html#CTRL-V). This key binding will be blocked, with the Windows Terminal paste command taking priority, unless the `unbound` setting is adjusted in your settings.json file so that the key binding will associate with the Vim command line app, rather than with the Windows Terminal binding.
