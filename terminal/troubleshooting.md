---
title: Windows Terminal Troubleshooting
description: Learn fixes to common obstacles in the Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: troubleshooting
ms.service: terminal
---

# Troubleshooting in Windows Terminal

Here are some common errors/obstacles you may encounter when using Windows Terminal.

## How do I launch WSL in the `~` directory?

By default, the `startingDirectory` of a profile is `%USERPROFILE%` (`C:\Users\<YourUsername>`). This is a Windows path. However, for WSL, you might want to use the WSL home path instead. `startingDirectory` only accepts a Windows-style path, so setting it to start within the WSL distribution can be a little tricky.

Fortunately, with Windows 1903, the file systems of WSL distros can easily be addressed using the `\\wsl$\` prefix. For any WSL distro whose name is `DistroName`, you can use `\\wsl$\DistroName` as a Windows path that points to the root of that distro's file system.

For example, the following works as a profile to launch the "Ubuntu-18.04" distro in its home path:

```json
{
    "name": "Ubuntu-18.04",
    "commandline" : "wsl -d Ubuntu-18.04",
    "startingDirectory" : "//wsl$/Ubuntu-18.04/home/<Your Ubuntu Username>",
}
```

## How do I set my tab title?

If you'd like to have the shell automatically set your tab title, [visit the tab title tutorial](./tutorials/tab-title.md). If you want to set your tab title in your settings file, use the following steps:

1. Add `"suppressApplicationTitle": true` to the profile you want to suppress any title change events that get sent from the shell. Having only this setting added to your profile will set your tab title to the name of your profile.

2. If you want a custom tab title that is not the name of your profile, you can add `"tabTitle": "TITLE"` where TITLE is replaced with your preferred tab title.

## Command line arguments in PowerShell

Visit the [Command line arguments page](./command-line-arguments.md) to learn how command line arguments operate in PowerShell.

## Command line arguments in WSL

Visit the [Command line arguments page](./command-line-arguments.md) to learn how command line arguments operate in WSL.

## My profile's `startingDirectory` setting is being ignored

First, check to make sure your settings.json's syntax is correct. We automatically inject `"$schema": "https://aka.ms/terminal-profiles-schema"` at the top of your settings.json to help with that. Some applications, like Visual Studio Code, use the attached schema to validate your json as you make edits.

If your settings are correct, it may be that you are running a startup script that sets the starting directory of your Terminal separately. For example, PowerShell has its own separate concept of profiles. If you are changing your starting directory there, it will take precedence over the one defined in Windows Terminal.

Alternatively, if you are running a script using the `commandline` profile setting, it may be that you are setting the location there. Similar to PowerShell profiles, your commands there take precedence over the `startingDirectory` profile setting.

In the end, `startingDirectory` launches a new Terminal instance in the given directory. If the Terminal runs any code that changes its directory, that may be a good place to take a look.

## `Ctrl+=` does not increase the font size

If you are using a German keyboard layout, you may run into this problem. <kbd>ctrl+=</kbd> gets deserialized as <kbd>ctrl+shift+0</kbd> if your main keyboard layout is set to German. This is the correct mapping for German keyboards.

More importantly, the app never receives the <kbd>ctrl+shift+0</kbd> keystroke, however. This is because <kbd>ctrl+shift+0</kbd> is reserved by Windows if you have multiple keyboard layouts active.

If you would like to disable this feature to get it to work properly, follow the instructions for "Change Hotkeys to Switch Keyboard Layout in Windows 10" in this [blog post](https://winaero.com/blog/change-hotkeys-switch-keyboard-layout-windows-10/).
Change the 'Switch Keyboard Layout' option to 'Not Assigned' (or off of <kbd>ctrl+shift</kbd>), then click OK and then Apply. <kbd>ctrl+shift+0</kbd> should now work as a key binding and is passed through to the Terminal.

On the other hand, if you do use this hotkey feature for multiple input languages, you can configure your own custom key binding in your settings.json file.
