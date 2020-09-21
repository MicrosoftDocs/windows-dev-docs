---
title: Windows Terminal Panes
description: Learn how to split panes in the Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 09/22/2020
ms.topic: how-to
ms.service: terminal
---

# Panes in Windows Terminal

Panes give you the ability to run multiple command-line applications next to each other within the same tab. This minimizes the need to switch between tabs and lets you see multiple prompts at once.

## Creating a new pane

### Using the keyboard

You can either create a new vertical or horizontal pane in Windows Terminal. Splitting vertically will open a new pane to the right of the focused pane and splitting horizontally will open a new pane below the focused pane. To create a new vertical pane of your default profile, you can press the <kbd>Alt</kbd>+<kbd>Shift</kbd>+<kbd>=</kbd> key combination. For a horizontal pane of your default profile, you can use <kbd>Alt</kbd>+<kbd>Shift</kbd>+<kbd>-</kbd>.

![Windows Terminal create pane](./images/open-panes.gif)
_Configuration: [Raspberry Ubuntu](./custom-terminal-gallery/raspberry-ubuntu.md)_

If you would like to change these key bindings, you can create new ones using the `splitPane` action and `vertical`, `horizontal`, or `auto` values for the `split` property in your profiles.json file. The `auto` method will choose the direction that gives you the squarest panes. To learn more about key bindings, visit the [Actions page](./customize-settings/actions.md).

```json
{ "command": { "action": "splitPane", "split": "vertical" }, "keys": "alt+shift+=" },
{ "command": { "action": "splitPane", "split": "horizontal" }, "keys": "alt+shift+-" },
{ "command": { "action": "splitPane", "split": "auto" }, "keys": "alt+shift+d" }
```

### Using the new tab button and dropdown menu

If you'd like to open a new pane of your default profile, you can hold the <kbd>alt</kbd> key and click the new tab button. If you'd like to open a new pane through the dropdown menu, you can hold <kbd>alt</kbd> and click on your desired profile. Both of these options will `auto` split the active window or pane into a new pane of the selected profile. The `auto` split mode splits in the direction that has the longest edge to create a pane.

![Windows Terminal dropdown pane](./images/alt-click-pane.gif)

## Switching between panes

The terminal allows you to navigate between panes by using the keyboard. If you hold the <kbd>Alt</kbd> key, you can use your arrow keys to move your focus between panes. You can identify which pane is in focus by the accent color border surrounding it. Note that this accent color is set in your Windows color settings.

![Windows Terminal switch panes](./images/navigate-panes.gif)

You can customize this by adding key bindings for the `moveFocus` command and setting the `direction` to either `down`, `left`, `right` or `up`.

```json
{ "command": { "action": "moveFocus", "direction": "down" }, "keys": "alt+down" },
{ "command": { "action": "moveFocus", "direction": "left" }, "keys": "alt+left" },
{ "command": { "action": "moveFocus", "direction": "right" }, "keys": "alt+right" },
{ "command": { "action": "moveFocus", "direction": "up" }, "keys": "alt+up" }
```

## Resizing a pane

You can adjust the size of your panes by holding <kbd>Alt</kbd>+<kbd>Shift</kbd> and using your arrow keys to resize the focused pane.

![Windows Terminal resize pane](./images/resize-panes.gif)

To customize this key binding, you can add new ones using the `resizePane` action and setting the `direction` to either `down`, `left`, `right`, or `up`.

```json
{ "command": { "action": "resizePane", "direction": "down" }, "keys": "alt+shift+down" },
{ "command": { "action": "resizePane", "direction": "left" }, "keys": "alt+shift+left" },
{ "command": { "action": "resizePane", "direction": "right" }, "keys": "alt+shift+right" },
{ "command": { "action": "resizePane", "direction": "up" }, "keys": "alt+shift+up" }
```

## Closing a pane

You can close the focused pane by typing <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>W</kbd>. If you only have one pane, <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>W</kbd> will close the tab. As always, closing the last tab will close the window.

![Windows Terminal close panes](./images/close-panes.gif)

You can change which keys close the pane by adding a key binding that uses the `closePane` command.

```json
{ "command": "closePane", "keys": "ctrl+shift+w" }
```

## Customizing panes using key bindings

You can customize what opens inside a new pane depending on your custom key bindings.

### Duplicating a pane

The terminal allows you to duplicate the focused pane's profile into another pane.

![Windows Terminal duplicate panes](./images/duplicate-panes.gif)

This can be done by adding the `splitMode` property with `duplicate` as the value to a `splitPane` key binding.

```json
{ "command": { "action": "splitPane", "split": "auto", "splitMode": "duplicate" }, "keys": "alt+shift+d" }
```

[!INCLUDE [new-terminal-arguments](./new-terminal-arguments.md)]
