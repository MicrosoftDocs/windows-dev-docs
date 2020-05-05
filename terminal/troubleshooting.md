---
title: Windows Terminal Troubleshooting
description: Learn fixes to common obstacles in the Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: overview
ms.service: terminal
---

# Troubleshooting in Windows Terminal

This guide addresses some of the common errors and obstacles you may encounter when using Windows Terminal.

## Set your WSL distribution to start in the home `~` directory when launched

By default, the `startingDirectory` of a profile is `%USERPROFILE%` (`C:\Users\<YourUsername>`). This is a Windows path. For WSL, however, you may want to use the WSL home path instead. `startingDirectory` only accepts a Windows-style path, so setting it to start within a WSL distribution requires a prefix.

Beginning in Windows 10 version 1903, the file systems of WSL distributions can be addressed using the `\\wsl$\` prefix. For any WSL distribution with the name `DistroName`, use `\\wsl$\DistroName` as a Windows path that points to the root of that distribution's file system.

For example, the following setting will launch the "Ubuntu-18.04" distribution in its home file path:

```json
{
    "name": "Ubuntu-18.04",
    "commandline" : "wsl -d Ubuntu-18.04",
    "startingDirectory" : "//wsl$/Ubuntu-18.04/home/<Your Ubuntu Username>",
}
```

## Setting the tab title

To have the shell automatically set your tab title, [visit the set the tab title tutorial](./tutorials/tab-title.md). If you want to set your own tab title, open the settings.json file and follow these steps:

1. In the profile for the command line of your choice, add `"suppressApplicationTitle": true` to suppress any title change events that get sent from the shell. Adding *only* this setting to your profile will set the tab title to the name of your profile.

2. If you want a custom tab title that is not the name of your profile, add `"tabTitle": "TITLE"`. Replacing "TITLE" with your preferred tab title.

## Command line arguments in PowerShell

PowerShell uses a semicolon `;` to delimit statements. To interpret a semicolon `;` as a command delimiter for `wt` command line arguments, you need to escape semicolon characters using backticks `` ` ``. PowerShell also has the stop parsing operator (`--%`), which instructs it to stop interpreting anything after it and just pass it on verbatim. For more information on command line arguments, visit the [Command line arguments page](./command-line-arguments.md).

## Command line arguments in WSL

Execution aliases do not work in WSL. If you want to use `wt.exe` from a WSL setting, you can spawn it from CMD directly by running the following:

```sh
$ cmd.exe /c "wt.exe"
```

The `/c` option makes CMD run the command, then terminate.

## Problem setting `startingDirectory`

If the `startingDirectory` is being ignored in your profile, first check to make sure your settings.json's syntax is correct. To help you check this syntax, `"$schema": "https://aka.ms/terminal-profiles-schema"` is automatically injected. Some applications, like [Visual Studio Code](https://code.visualstudio.com/download), can use that injected schema to validate your json file as you make edits.

If your settings are correct, you may be running a startup script that sets the starting directory of your terminal separately. For example, PowerShell has its own separate concept of profiles. If you are changing your starting directory there, it will take precedence over the setting defined in Windows Terminal.

Alternatively, if you are running a script using the `commandline` profile setting, it may be that you are setting the location there. Similar to PowerShell profiles, your commands there take precedence over the `startingDirectory` profile setting.

The purose of `startingDirectory` is to launch a new Windows Terminal instance in the given directory. If the terminal runs any code that changes its directory, that may be a good place to take a look.

## Ctrl+= does not increase the font size

If you are using a German keyboard layout, you may run into this problem. `ctrl+=` gets deserialized as `ctrl+shift+0` if your main keyboard layout is set to German. This is the correct mapping for German keyboards.

More importantly, the app never receives the `ctrl+shift+0` keystroke. This is because `ctrl+shift+0` is reserved by Windows if you have multiple keyboard layouts active.

If you would like to disable this feature in order for `Ctrl+=` to work properly, follow the instructions for "Change Hotkeys to Switch Keyboard Layout in Windows 10" in this [blog post](https://winaero.com/blog/change-hotkeys-switch-keyboard-layout-windows-10/).

Change the 'Switch Keyboard Layout' option to 'Not Assigned' (or off of `ctrl+shift`), then select **OK** and then **Apply**. `ctrl+shift+0` should now work as a key binding and is passed through to the terminal.

On the other hand, if you do use this hotkey feature for multiple input languages, you can [configure your own custom key binding](./key-bindings.md) in your settings.json file.
