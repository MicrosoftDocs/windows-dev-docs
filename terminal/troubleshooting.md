---
title: Windows Terminal Troubleshooting
description: Learn fixes to common obstacles in Windows Terminal.
ms.date: 11/10/2025
ms.topic: overview
---

# Troubleshooting in Windows Terminal

This guide addresses some of the common errors and obstacles you might encounter when using Windows Terminal.

## Opening the settings does nothing (or opens an unexpected application)

If you select the **settings** button in the dropdown, the Terminal tries to open the settings file, `settings.json`. This action causes the OS to launch your configured `.json` file editor. This editor might be Visual Studio, Notepad, or some other unexpected application. If your machine doesn't have a configured `.json` editor, the OS eventually shows you the **How do you want to open this file** dialog.

> [!TIP]
> You can also use the settings UI to configure your settings. You can learn how to open the settings UI on the [Actions page](./customize-settings/actions.md#application-level-commands).

## Set your WSL distribution to start in the home `~` directory when launched in older versions of Windows Terminal

By default, the `startingDirectory` of a profile is `%USERPROFILE%` (`C:\Users\<YourUsername>`). This path is a Windows path. For WSL distributions running a new version of Windows Terminal, the file systems can enter `~` to set this home path. In older versions of Windows Terminal, you can use `/home/<Your Ubuntu Username>` to directly refer to your home folder. For example, the following setting launches the "Ubuntu-20.04" distribution in its home file path:

```json
{
    "name": "Ubuntu-20.04",
    "commandline" : "wsl -d Ubuntu-20.04",
    "startingDirectory" : "/home/<Your Ubuntu Username>"
}
```

If you're using a very early version of Windows Terminal, WSL might require using the `\\wsl$\` prefix when referring to a distribution's home path for the `startingDirectory` setting. For example, the following setting launches the "Ubuntu-18.04" distribution in its home file path:

```json
{
    "name": "Ubuntu-18.04",
    "commandline" : "wsl -d Ubuntu-18.04",
    "startingDirectory" : "//wsl$/Ubuntu-18.04/home/<Your Ubuntu Username>"
}
```

> [!IMPORTANT]
> On newer versions of Windows, `startingDirectory` can accept Linux-style paths.

## Setting the tab title

To have the shell automatically set your tab title, [visit the set the tab title tutorial](./tutorials/tab-title.md). If you want to set your own tab title, open the [settings.json file](./install.md#settings-json-file) and follow these steps:

1. In the profile for the command line of your choice, add `"suppressApplicationTitle": true` to suppress any title change events that the shell sends. Adding *only* this setting to your profile sets the tab title to the name of your profile.

1. If you want a custom tab title that isn't the name of your profile, add `"tabTitle": "TITLE"`. Replace "TITLE" with your preferred tab title.

## Command line arguments in PowerShell

To learn how command-line arguments work in PowerShell, see the [Command line arguments page](./command-line-arguments.md).

## Command line arguments in WSL

To learn how command-line arguments work in WSL, see the [Command line arguments page](./command-line-arguments.md).

## Problem setting `startingDirectory`

If your profile ignores the `startingDirectory` setting, first check the syntax in your [settings.json file](./install.md#settings-json-file). To help you check this syntax, `"$schema": "https://aka.ms/terminal-profiles-schema"` is automatically injected. Some applications, like [Visual Studio Code](https://code.visualstudio.com/download), use that injected schema to validate your JSON file as you make edits.

If your settings are correct, you might be running a startup script that sets the starting directory of your terminal separately. For example, PowerShell has its own separate concept of profiles. If you change your starting directory in a PowerShell profile, it takes precedence over the setting defined in Windows Terminal.

Alternatively, if you run a script by using the `commandline` profile setting, you might be setting the location in that script. Similar to PowerShell profiles, your commands in the script take precedence over the `startingDirectory` profile setting.

The purpose of `startingDirectory` is to launch a new Windows Terminal instance in the given directory. If the terminal runs any code that changes its directory, check that code.

## Ctrl+= doesn't increase the font size

If you use a German keyboard layout, you might run into this problem. <kbd>Ctrl+=</kbd> gets deserialized as <kbd>Ctrl+Shift+0</kbd> if your main keyboard layout is set to German. This mapping is correct for German keyboards.

More importantly, the app never receives the <kbd>Ctrl+Shift+0</kbd> keystroke. This issue happens because Windows reserves <kbd>Ctrl+Shift+0</kbd> if you have multiple keyboard layouts active.

If you want to disable this feature so that `Ctrl+=` works properly, follow the instructions for "Change Hotkeys to Switch Keyboard Layout in Windows 10" in this [blog post](https://winaero.com/blog/change-hotkeys-switch-keyboard-layout-windows-10/).

Change the 'Switch Keyboard Layout' option to 'Not Assigned' (or off of <kbd>Ctrl+Shift</kbd>), then select **OK** and then **Apply**. <kbd>Ctrl+Shift+0</kbd> should now work as a key binding and is passed through to the terminal.

On the other hand, if you use this hotkey feature for multiple input languages, you can [configure your own custom key binding](./customize-settings/actions.md) in your [settings.json file](./install.md#settings-json-file).

## The text is blurry

Some display drivers and hardware combinations don't handle scroll and dirty regions without blurring the data from the previous frame. To mitigate this problem, add a combination of [these global rendering settings](./customize-settings/rendering.md) to reduce the strain placed on your hardware caused by the terminal text renderer.

## My colors look strange! There are black bars on my screen!

> [!IMPORTANT]
> This issue applies only to version 1.2+ of Windows Terminal. If you see color issues in Windows Terminal 1.0 or 1.1, or issues that aren't captured here, please file a bug.

Windows Terminal 1.2 and later versions have an improved understanding of certain application color settings. Because of this improved understanding, we removed a number of compatibility blocks that resulted in a poor user experience. Unfortunately, a small number of applications might experience issues.

We keep this troubleshooting item up-to-date with the list of known issues and their workarounds.

### Black lines in PowerShell (5.1, 6.x, 7.0)

Terminal, when coupled with PowerShell's line editing library [PSReadline](https://www.powershellgallery.com/packages/PSReadLine), might draw black lines across the screen. These miscolored regions extend across the screen beyond your prompt wherever there are command parameters, strings, or operators.

PSReadline version **2.0.3** contains a fix for this issue. If you're using the prerelease version of PSReadline, note that a fix isn't yet available.

To update to the newest version of PSReadline, run the following command:

```powershell
Update-Module PSReadline
```

## Why aren't my emojis appearing as icons in the jumplist?

Only images linked from a file location can be rendered as profile icons in the jumplist. The jumplist doesn't support emojis for icons.

## Technical notes

Applications that use the [`GetConsoleScreenBufferInfo` family of APIs](/windows/console/getconsolescreenbufferinfoex) to retrieve the active console colors in Win32 format and then try to transform them into cross-platform VT sequences (for example, by transforming `BACKGROUND_RED` to `\x1b[41m`) might interfere with Terminal's ability to detect what background color the application is trying to use.

Choose either Windows API functions _or_ VT sequences for adjusting colors. Don't mix them.

### Keyboard service warning

Starting in Windows Terminal 1.5, the Terminal displays a warning if the "Touch Keyboard and Handwriting Panel Service" is disabled. The operating system needs this service to properly route input events to the Terminal application (as well as many other applications on Windows). If you see this warning, follow these steps to re-enable the service:

1. In the run dialog, run `services.msc`

    ![services.msc in the run dialog](https://user-images.githubusercontent.com/18356694/97891741-c81eed00-1cf4-11eb-9d48-7b94fede5294.png)

1. Find the "Touch Keyboard and Handwriting Panel Service"

    ![Touch Keyboard and Handwriting Panel Service in Services.msc](https://user-images.githubusercontent.com/18356694/97891813-e1279e00-1cf4-11eb-91c8-69a5c6da6c3d.png)

1. Open the "Properties" for this service

    ![service properties](https://user-images.githubusercontent.com/18356694/97891923-03212080-1cf5-11eb-90cc-821a4fbf16ba.png)

1. Change the "startup type" to "Automatic"

    ![service startup type](https://user-images.githubusercontent.com/18356694/97892043-25b33980-1cf5-11eb-8833-a2e65a306a79.png)

1. Select **Ok**, and restart the PC.

After restarting the machine, the service auto-starts, and the dialog no longer appears.

## Why do I see blinking or flashing when using a Git Bash command line?

You might notice blinking or flashing when using a Git Bash command line inside Windows Terminal. This behavior is actually by design. The Terminal obeys what Git Bash tells it to do (setting `bell-style` to `visible`, causing a flash to associate with the bell response), but this behavior can be distracting. To fix this issue, open the `.inputrc` file for your Git Bash with a text editor. This file is likely located in the path `C:\Program Files\Git\etc`. To open the file with the Nano text editor, use the command: `nano ~/.inputrc`.

Change the default:

```bash
# none, visible or audible
set bell-style visible
```

Set the `bell-style` to either `none` or `audible` to remove the visible flash:

```bash
set bell-style none
```

Press Ctrl + O and Ctrl + X to save and exit.

## How do I reset my settings in Windows Terminal back to the default settings?

To reset your settings back to the original default settings, delete your [settings.json file](./install.md#settings-json-file). This action causes Windows Terminal to regenerate a `settings.json` file with the original default settings.

> [!IMPORTANT]
> As of Windows Terminal version 1.10 or greater, you also need to delete the `state.json` file in the same directory as the `settings.json` file to fully reset the settings to the defaults.

## Why is Acrylic opacity not making my Windows Terminal background transparent?

You can set the transparency of a terminal window with the [`useAcrylic` property](./customize-settings/profile-appearance.md#transparency). There are a few reasons why your opacity setting might not work for Acrylic, including:

- As a system-wide policy, acrylic is only enabled for the foreground window. If you activate any other window than the Terminal, the Terminal's acrylic turns off.
- Acrylic doesn't work if your GPU hardware doesn't support it. If you're running an app in a virtual machine (VM) or over remote desktop, acrylic likely doesn't work.
- The operating system disables acrylic for a number of reasons, like being in power saver (low-battery) mode or when accessing a machine by using Remote Desktop.

## Why does my mouse pointer disappear when I hover over a window and start typing?

This cursor auto-hiding behavior is by design, but you can disable it. Search in Windows Settings for **Mouse settings** > **Additional Mouse Settings** > **Mouse Properties** > **Pointer Options** > and uncheck **Hide pointer while typing**. You might need to restart your Windows Terminal for this change to take effect.
