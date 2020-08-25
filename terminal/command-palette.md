---
title: Windows Terminal Command Palette
description: Learn how to use the command palette in the Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 08/26/2020
ms.topic: how-to 
ms.service: terminal
---

# How to use the command palette in Windows Terminal ([Preview](https://aka.ms/terminal-preview/))

The command palette lets you see which commands you can run inside Windows Terminal.

## Invoking the command palette

You can invoke the command palette by typing `ctrl+shift+p`. This can be customized by adding the `commandPalette` command to your key bindings.

```json
{ "command": "commandPalette", "keys": "ctrl+shift+p" }
```

## Command line mode

If you'd like to enter a `wt` command into the command palette, you can do so by starting with the `>` character. This will run the `wt` command in the current window.

## Defining commands

There are three different ways you can define a command in Windows Terminal.

1. With a value for `keys` without a `name`

This is the behavior that is documented on the [key bindings page](./customize-settings/key-bindings.md). These commands will **not** appear in the command palette.

```json
{ "command": "newTab", "keys": "ctrl+shift+t" }
```

2. Without a value for `keys` with a `name`

These commands will be added to the command palette, however they cannot be invoked solely with the keyboard.

```json
{ "icon": null, "name": "New Tab", "action": "newTab" }
```

3. With a value for `keys` with a `name`

These commands will be added to the command palette and can be invoked with the keyboard.

```json
{ "icon": null, "name": "New Tab", "command": "newTab", "keys": "ctrl+shift+t" }
```

### Nested commands

Nested commands let you invoke multiple commands with just one command. These will appear as single items in the command palette and can be invoked with one key binding. This can be done with the following syntax:

### Iterable commands

Iterable commands let you iterate over an array defined in your settings.json file. Possible arrays to iterate over include `profiles` and `schemes`. These will create individual items in the command palette, one for each iteration. Below is an example of iterating over each profile and creating a new tab for each one.

```json
{
    "name": { "key": "NewTabParentCommandName" },
    "commands": [
        {
            "iterateOn": "profiles",
            "icon": "${profile.icon}",
            "name": "${profile.name}",
            "command": { "action": "newTab", "profile": "${profile.name}" }
        }
    ]
},
```
