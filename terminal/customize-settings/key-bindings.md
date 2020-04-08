---
title: Windows Terminal Key Bindings
description: Learn how to create custom key bindings for Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: overview
ms.service: terminal
---

# Custom key bindings in Windows Terminal

You can create custom key bindings inside the Windows Terminal that give you control of how you interact with the Terminal using your keyboard.

## Key binding properties

Key bindings can be constructed using the following properties.

### Command

The command executed when the associated keys are pressed.

**Property name:** `command`

**Necessity:** Required

**Accepts:** String

### Keys

Defines the key combinations used to call the command.

**Property name:** `keys`

**Necessity:** Required

**Accepts:** String or array[string]

### Action

Adds additional functionality to certain commands.

**Property name:** `action`

**Necessity:** Optional

**Accepts:** String

<br />

___

## Accepted Modifiers and Keys

### Modifiers
`Ctrl+`, `Shift+`, `Alt+`

### Keys
| Type | Keys |
| ---- | ---- |
| Function and Alphanumeric Keys | `f1-f24`, `a-z`, `0-9` |
| Symbols | ``` ` ```, `-`, `=`, `[`, `]`, `\`, `;`, `'`, `,`, `.`, `/` |
| Arrow Keys | `down`, `left`, `right`, `up`, `pagedown`, `pageup`, `pgdn`, `pgup`, `end`, `home`, `plus` |
| Action Keys | `tab`, `enter`, `esc`, `escape`, `space`, `backspace`, `delete`, `insert` |
| Numpad Keys | `numpad_0-numpad_9`, `numpad0-numpad9`, `numpad_add`, `numpad_plus`, `numpad_decimal`, `numpad_period`, `numpad_divide`, `numpad_minus`, `numpad_subtract`, `numpad_multiply` |

<br />

___

## Key binding formats

Keybindings can be structured in the following formats:

### Commands without arguments

```json
{ "command": "commandName", "keys": "modifiers+key" }
```

### Commands with arguments
```json
{ "command": { "action": "commandName", "argument": "value" }, "keys": "modifiers+key" }
```

<br />

___

## Application-level commands

### Close window

Close the current window and all tabs within it.

**Command name:** `closeWindow`

**Default binding:**
```json
{ "command": "closeWindow", "keys": "alt+f4" }
```

### Find

Open the search dialog box. More information on search can be found on the [Search page](./../search.md).

**Command name:** `find`

**Default binding:**
```json
{ "command": "find", "keys": "ctrl+shift+f" }
```

### Open the dropdown

Open the dropdown menu.

**Command name:** `openNewTabDropdown`

**Default binding:**
```json
{ "command": "openNewTabDropdown", "keys": "ctrl+shift+space" }
```

### Open settings file

Open the settings file.

**Command name:** `openSettings`

**Default binding:**
```json
{ "command": "openSettings", "keys": "ctrl+," }
```

### Toggle full screen

Switch between full screen and default window sizes.

**Command name:** `toggleFullscreen`

**Default bindings:**
```json
{ "command": "toggleFullscreen", "keys": "alt+enter" },
{ "command": "toggleFullscreen", "keys": "f11" }
```

<br />

___

## Tab management commands

### Close tab

Close the current tab.

**Command name:** `closeTab`

### Duplicate tab

Make a copy and open the current tab.

**Command name:** `duplicateTab`

**Default binding:**
```json
{ "command": "duplicateTab", "keys": "ctrl+shift+d" }
```

### New tab

Create a new tab. Without any arguments, this will open the default profile in a new tab.

**Command name:** `newTab`

**Default bindings:**
```json
{ "command": "newTab", "keys": "ctrl+shift+t" },
{ "command": { "action": "newTab", "index": 0 }, "keys": "ctrl+shift+1" },
{ "command": { "action": "newTab", "index": 1 }, "keys": "ctrl+shift+2" },
{ "command": { "action": "newTab", "index": 2 }, "keys": "ctrl+shift+3" },
{ "command": { "action": "newTab", "index": 3 }, "keys": "ctrl+shift+4" },
{ "command": { "action": "newTab", "index": 4 }, "keys": "ctrl+shift+5" },
{ "command": { "action": "newTab", "index": 5 }, "keys": "ctrl+shift+6" },
{ "command": { "action": "newTab", "index": 6 }, "keys": "ctrl+shift+7" },
{ "command": { "action": "newTab", "index": 7 }, "keys": "ctrl+shift+8" },
{ "command": { "action": "newTab", "index": 8 }, "keys": "ctrl+shift+9" }
```

#### Actions

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `commandLine` | Optional | Executable file name as a string | Executable run within the tab. |
| `startingDirectory` | Optional | Folder location as a string | Directory in which the tab will open. |
| `tabTitle` | Optional | String | Title of the new tab. |
| `index` | Optional | Integer | Profile that will open based on its position in the dropdown (starting at 0). |
| `profile` | Optional | Profile's name or GUID as a string | Profile that will open based on its GUID or name. |

### Open next tab

Open the tab to the right of the current one.

**Command name:** `nextTab`

**Default binding:**
```json
{ "command": "nextTab", "keys": "ctrl+tab" }
```

### Open previous tab

Open the tab to the left of the current one.

**Command name:** `prevTab`

**Default binding:**
```json
{ "command": "prevTab", "keys": "ctrl+shift+tab" }
```

### Open a specific tab

Open a specific tab depending on index.

**Command name:** `switchToTab`

**Default bindings:**
```json
{ "command": { "action": "switchToTab", "index": 0 }, "keys": "ctrl+alt+1" },
{ "command": { "action": "switchToTab", "index": 1 }, "keys": "ctrl+alt+2" },
{ "command": { "action": "switchToTab", "index": 2 }, "keys": "ctrl+alt+3" },
{ "command": { "action": "switchToTab", "index": 3 }, "keys": "ctrl+alt+4" },
{ "command": { "action": "switchToTab", "index": 4 }, "keys": "ctrl+alt+5" },
{ "command": { "action": "switchToTab", "index": 5 }, "keys": "ctrl+alt+6" },
{ "command": { "action": "switchToTab", "index": 6 }, "keys": "ctrl+alt+7" },
{ "command": { "action": "switchToTab", "index": 7 }, "keys": "ctrl+alt+8" },
{ "command": { "action": "switchToTab", "index": 8 }, "keys": "ctrl+alt+9" }
```

#### Actions

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `index` | Required | Integer | Tab that will open based on its position in the tab bar (starting at 0). |

<br />

___

## Pane management commands

### Close pane

Close the active pane. If there aren't any split panes, this will close the current tab. If there is only one tab open, this will close the window.

**Command name:** `closePane`

**Default binding:**
```json
{ "command": "closePane", "keys": "ctrl+shift+w" }
```

### Move pane focus

Focus on a different pane depending on direction.

**Command name:** `moveFocus`

**Default bindings:**
```json
{ "command": { "action": "moveFocus", "direction": "down" }, "keys": "alt+down" },
{ "command": { "action": "moveFocus", "direction": "left" }, "keys": "alt+left" },
{ "command": { "action": "moveFocus", "direction": "right" }, "keys": "alt+right" },
{ "command": { "action": "moveFocus", "direction": "up" }, "keys": "alt+up" }
```

#### Actions

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `direction` | Required | `"left"`, `"right"`, `"up"`, `"down"` | Direction in which the focus will move. |

### Resize a pane

Change the size of the active pane.

**Command name:** `resizePane`

**Default bindings:**
```json
{ "command": { "action": "resizePane", "direction": "down" }, "keys": "alt+shift+down" },
{ "command": { "action": "resizePane", "direction": "left" }, "keys": "alt+shift+left" },
{ "command": { "action": "resizePane", "direction": "right" }, "keys": "alt+shift+right" },
{ "command": { "action": "resizePane", "direction": "up" }, "keys": "alt+shift+up" }
```

#### Actions

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `direction` | Required | `"left"`, `"right"`, `"up"`, `"down"` | Direction in which the pane will be resized. |

### Split a pane

Halve the size of the active pane and open another. Without any arguments, this will open the default profile in the new pane.

**Command name:** `splitPane`

**Default bindings:**
```json
{ "command": { "action": "splitPane", "split": "horizontal"}, "keys": "alt+shift+-" },
{ "command": { "action": "splitPane", "split": "vertical"}, "keys": "alt+shift+plus" }
```

#### Actions

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `split` | Required | `"vertical"`, `"horizontal"`, `"auto"` | How the pane will split. `"auto"` will split in the direction that provides the most surface area. |
| `commandLine` | Optional | Executable file name as a string | Executable run within the pane. |
| `startingDirectory` | Optional | Folder location as a string | Directory in which the pane will open. |
| `tabTitle` | Optional | String | Title of the tab when the new pane is focused. |
| `index` | Optional | Integer | Profile that will open based on its position in the dropdown (starting at 0). |
| `profile` | Optional | Profile's name or GUID as a string | Profile that will open based on its GUID or name. |
| `splitMode` | Optional | `"duplicate"` | Controls how the pane splits. Only accepts `"duplicate"`, which will duplicate the focused pane's profile into a new pane. |

<br />

___

## OS integration commands

### Copy

Copy the selected terminal content to your Windows Clipboard.

**Command name:** `copy`

**Default bindings:**
```json
{ "command": "copy", "keys": "ctrl+shift+c" },
{ "command": "copy", "keys": "ctrl+insert" }
```

#### Actions

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `singleLine` | Optional | `true`, `false` | When `true`, the copied content will be copied as a single line. When `false`, newlines persist from the selected text. |

### Paste

Insert the content that was copied onto the clipboard.

**Command name:** `paste`

**Default bindings:**
```json
{ "command": "paste", "keys": "ctrl+shift+v" },
{ "command": "paste", "keys": "shift+insert" }
```

<br />

___

## Scrollback commands

### Scroll down

Move the screen down.

**Command name:** `scrollDown`

**Default binding:**
```json
{ "command": "scrollDown", "keys": "ctrl+shift+down" }
```

### Scroll up

Move the screen up.

**Command name:** `scrollUp`

**Default binding:**
```json
{ "command": "scrollUp", "keys": "ctrl+shift+up" }
```

### Scroll up a whole page

Move the screen up a whole page.

**Command name:** `scrollUpPage`

**Default binding:**
```json
{ "command": "scrollUpPage", "keys": "ctrl+shift+pgup" }
```

### Scroll down a whole page

Move the screen down a whole page.

**Command name:** `scrollDownPage`

**Default binding:**
```json
{ "command": "scrollDownPage", "keys": "ctrl+shift+pgdn" }
```

<br />

___

## Visual adjustment commands

### Adjust font size

Change the text size by a specified point amount.

**Command name:** `adjustFontSize`

**Default bindings:**
```json
{ "command": { "action": "adjustFontSize", "delta": 1 }, "keys": "ctrl+=" },
{ "command": { "action": "adjustFontSize", "delta": -1 }, "keys": "ctrl+-" }
```

#### Actions

| Name | Necessity | Accepts | Description |
| ---- | --------- | ------- | ----------- |
| `delta` | Required | Integer | Amount of size change per command invocation. |

### Reset font size

Reset the text size to the default value.

**Command name:** `resetFontSize`

**Default binding:**
```json
{ "command": "resetFontSize", "keys": "ctrl+0" }
```

<br />

___

## Unbind keys

Unbind the associated keys from any command.

**Command name:** `unbound`
