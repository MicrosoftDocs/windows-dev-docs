---
title: Windows Terminal Troubleshooting
description: Learn fixes to common obstacles in the Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 09/22/2020
ms.topic: overview
ms.localizationpriority: high
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
    "startingDirectory" : "//wsl$/Ubuntu-18.04/home/<Your Ubuntu Username>"
}
```

## Setting the tab title

To have the shell automatically set your tab title, [visit the set the tab title tutorial](./tutorials/tab-title.md). If you want to set your own tab title, open the settings.json file and follow these steps:

1. In the profile for the command line of your choice, add `"suppressApplicationTitle": true` to suppress any title change events that get sent from the shell. Adding *only* this setting to your profile will set the tab title to the name of your profile.

2. If you want a custom tab title that is not the name of your profile, add `"tabTitle": "TITLE"`. Replacing "TITLE" with your preferred tab title.

## Command line arguments in PowerShell

Visit the [Command line arguments page](./command-line-arguments.md) to learn how command-line arguments operate in PowerShell.

## Command line arguments in WSL

Visit the [Command line arguments page](./command-line-arguments.md) to learn how command-line arguments operate in WSL.

## Hyperlinks don't work

As of Windows Terminal 1.4, [embedded hyperlinks](https://gist.github.com/egmontkob/eb114294efbcd5adb1944c9f3cb5feda) inside the text buffer are clickable and will open in your default browser. If a link is not a hyperlink, this means it is not an embedded hyperlink and it is simply plain text. Support for automatically detecting plain text links will arrive in a future release.

## Problem setting `startingDirectory`

If the `startingDirectory` is being ignored in your profile, first check to make sure your settings.json's syntax is correct. To help you check this syntax, `"$schema": "https://aka.ms/terminal-profiles-schema"` is automatically injected. Some applications, like [Visual Studio Code](https://code.visualstudio.com/download), can use that injected schema to validate your json file as you make edits.

If your settings are correct, you may be running a startup script that sets the starting directory of your terminal separately. For example, PowerShell has its own separate concept of profiles. If you are changing your starting directory there, it will take precedence over the setting defined in Windows Terminal.

Alternatively, if you are running a script using the `commandline` profile setting, it may be that you are setting the location there. Similar to PowerShell profiles, your commands there take precedence over the `startingDirectory` profile setting.

The purpose of `startingDirectory` is to launch a new Windows Terminal instance in the given directory. If the terminal runs any code that changes its directory, that may be a good place to take a look.

## Ctrl+= does not increase the font size

If you are using a German keyboard layout, you may run into this problem. <kbd>ctrl+=</kbd> gets deserialized as <kbd>ctrl+shift+0</kbd> if your main keyboard layout is set to German. This is the correct mapping for German keyboards.

More importantly, the app never receives the <kbd>ctrl+shift+0</kbd> keystroke. This is because <kbd>ctrl+shift+0</kbd> is reserved by Windows if you have multiple keyboard layouts active.

If you would like to disable this feature in order for `Ctrl+=` to work properly, follow the instructions for "Change Hotkeys to Switch Keyboard Layout in Windows 10" in this [blog post](https://winaero.com/blog/change-hotkeys-switch-keyboard-layout-windows-10/).

Change the 'Switch Keyboard Layout' option to 'Not Assigned' (or off of <kbd>ctrl+shift</kbd>), then select **OK** and then **Apply**. <kbd>ctrl+shift+0</kbd> should now work as a key binding and is passed through to the terminal.

On the other hand, if you do use this hotkey feature for multiple input languages, you can [configure your own custom key binding](./customize-settings/actions.md) in your settings.json file.

## The text is blurry

Some display drivers and hardware combinations do not handle scroll and/or dirty regions without blurring the data from the previous frame. To mitigate this problem, you can add a combination of [these global rendering settings](./customize-settings/global-settings.md#rendering-settings) to reduce the strain placed on your hardware caused by the terminal text renderer.

## My colors look strange! There are black bars on my screen!

> [!IMPORTANT]
> This applies only to version 1.2+ of Windows Terminal. If you are seeing color issues in Windows Terminal 1.0 or 1.1, or issues that are not captured here, please file a bug.

Windows Terminal 1.2 and beyond has an improved understanding of certain application color settings. Because of this improved understanding, we have been able to remove a number of compatibility blocks that resulted in a poor user experience. Unfortunately, there is a small number of applications that may experience issues.

We will keep this troubleshooting item up-to-date with the list of known issues and their workarounds.

### Black lines in PowerShell (5.1, 6.x, 7.0)

Terminal, when coupled with PowerShell's line editing library [PSReadline](https://www.powershellgallery.com/packages/PSReadLine), may draw black lines across the screen. These miscolored regions will extend across the screen beyond your prompt wherever there are command parameters, strings or operators.

PSReadline version **2.0.3** has been released and contains a fix for this issue. If you are using the prerelease version of PSReadline, note that a fix is not yet available.

To update to the newest version of PSReadline, please run the following command:

```powershell
Update-Module PSReadline
```

### Technical Notes

Applications that use the [`GetConsoleScreenBufferInfo` family of APIs](https://docs.microsoft.com/windows/console/getconsolescreenbufferinfoex) to retrieve the active console colors in Win32 format and then attempt to transform them into cross-platform VT sequences (for example, by transforming `BACKGROUND_RED` to `\x1b[41m`) may interfere with Terminal's ability to detect what background color the application is attempting to use.

Application developers are encouraged to choose either Windows API functions _or_ VT sequences for adjusting colors and not attempt to mix them.
