---
title: Windows Terminal Command Palette
description: Learn how to use the command palette in Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 02/25/2021
ms.topic: how-to 
---

# How to use the command palette in Windows Terminal

The command palette lets you see which actions you can run inside Windows Terminal. More information on how actions are defined can be found on the [Actions page](./customize-settings/actions.md).

## Invoking the command palette

You can invoke the command palette by typing <kbd>Ctrl</kbd>+<kbd>Shift</kbd>+<kbd>P</kbd>. This can be customized by adding the `commandPalette` command to your key bindings.

```json
{ "command": "commandPalette", "keys": "ctrl+shift+p" }
```

## Command line mode

If you'd like to enter a `wt` command into the command palette, you can do so by deleting the `>` character in the text box. This will run the `wt` command in the current window. More information on `wt` commands can be found on the [Command line arguments page](./command-line-arguments.md).

![Windows Terminal command line mode](./images/command-palette-command-line-mode.gif)

You can add a custom key binding for invoking the command palette in the command line mode directly.

```json
{ "command": "commandPalette", "launchMode": "commandLine", "keys": "" }
```

## Adding an icon to a command

You can optionally add an icon to a command defined in your [settings.json](./install.md#settings-json-file) that appears in the command palette. This can be done by adding the `icon` property to the action. Icons can be a path to an image, a symbol from [Segoe MDL2 Assets](/windows/uwp/design/style/segoe-ui-symbol-font), or any character, including emojis.

```json
{ "icon": "C:\\Images\\my-icon.png", "name": "New tab", "command": "newTab", "keys": "ctrl+shift+t" },
{ "icon": "\uE756", "name": "New tab", "command": "newTab", "keys": "ctrl+shift+t" },
{ "icon": "⚡", "name": "New tab", "command": "newTab", "keys": "ctrl+shift+t" }
```

## Nested commands

Nested commands let you group multiple commands under one item in the command palette. The example below groups the font resize commands under one command palette item called "Change font size...".

```json
{
    "name": "Change font size...",
    "commands": [
        { "command": { "action": "adjustFontSize", "delta": 1 } },
        { "command": { "action": "adjustFontSize", "delta": -1 } },
        { "command": "resetFontSize" },
    ]
}
```

![Windows Terminal nested commands](./images/command-palette-nested-commands.gif)

## Iterable commands

Iterable commands let you create multiple commands at the same time, generated from other objects defined in your settings. Currently, you can create iterable commands for your profiles and color schemes. At runtime, these commands will be expanded to one command for each of the objects of the given type.

You can currently iterate over the following properties:

| `iterateOn` | Property | Property syntax |
| ----------- | -------- | --------------- |
| `profiles` | `name` | `"name": "${profile.name}"` |
| `profiles` | `icon` | `"icon": "${profile.icon}"` |
| `schemes` | `name` | `"name": "${scheme.name}"` |

### Example

Create a new tab command for each profile.

![Windows Terminal iterable commands](./images/command-palette-iterable-commands.gif)

```json
{
    "iterateOn": "profiles",
    "icon": "${profile.icon}",
    "name": "${profile.name}",
    "command": { "action": "newTab", "profile": "${profile.name}" }
}
```

In the above example:

- `"iterateOn": "profiles"` will generate a command for each profile.
- At runtime, the terminal will replace `${profile.icon}` with each profile's icon and `${profile.name}` with each profile's name.

If you had three profiles:

```json
"profiles": [
	{ "name": "Command Prompt", "icon": null },
	{ "name": "PowerShell", "icon": "C:\\path\\to\\icon.png" },
	{ "name": "Ubuntu", "icon": null },
]
```

The above command would behave like the following three commands:

```json
{
    "icon": null,
    "name": "Command Prompt",
    "command": { "action": "newTab", "profile": "Command Prompt" }
},
{
    "icon": "C:\\path\\to\\icon",
    "name": "PowerShell",
    "command": { "action": "newTab", "profile": "PowerShell" }
},
{
    "icon": null,
    "name": "Ubuntu",
    "command": { "action": "newTab", "profile": "Ubuntu" }
}
```

It's also possible to combine nested and iterable commands. For example, you can combine the three "new tab" commands above under a single "New tab" entry in the command palette, as shown in the image above, in the following way:

```json
{
    "name": "New tab",
    "commands": [
        {
            "iterateOn": "profiles",
            "icon": "${profile.icon}",
            "name": "${profile.name}",
            "command": { "action": "newTab", "profile": "${profile.name}" }
        }
    ]
}
```

## Hiding a command

If you would like to keep a command in your key bindings list but not have it appear in the command palette, you can hide it by setting its `name` to `null`. The example below hides the "New tab" action from the command palette.

```json
{ "name": null, "command": "newTab", "keys": "ctrl+shift+t" }
```
