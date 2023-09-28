---
title: Windows Terminal Troubleshooting
description: Learn fixes to common obstacles in Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 03/07/2022
ms.topic: overview
---

# Troubleshooting in Windows Terminal

This guide addresses some of the common errors and obstacles you may encounter when using Windows Terminal.

## Opening the settings does nothing (or opens an unexpected application)

If you click on the "settings" button in the dropdown, the Terminal will attempt to open the settings file, `settings.json`. This will cause the OS to try and launch your configured `.json` file editor. This might be Visual Studio, or Notepad, or some other completely unexpected application. If there isn't a configured `.json` editor on your machine, then the OS will eventually show you the "How do you want to open this file" dialog.

> [!TIP]
> You can also use the settings UI to configure your settings. You can learn how to open the settings UI on the [Actions page](./customize-settings/actions.md#application-level-commands).

## Set your WSL distribution to start in the home `~` directory when launched in older versions of Windows Terminal

By default, the `startingDirectory` of a profile is `%USERPROFILE%` (`C:\Users\<YourUsername>`). This is a Windows path. For WSL distributions running a new version of Windows Terminal, the file systems can enter `~` to set this home path. In older versions of Windows Terminal, you can use `/home/<Your Ubuntu Username>` to directly refer to your home folder. For example, the following setting will launch the "Ubuntu-20.04" distribution in its home file path:

```json
{
    "name": "Ubuntu-20.04",
    "commandline" : "wsl -d Ubuntu-20.04",
    "startingDirectory" : "/home/<Your Ubuntu Username>"
}
```

If you are using a very early version of Windows Terminal, WSL may require using the `\\wsl$\` prefix when referring to a distribution's home path for the `startingDirectory` setting. For example, the following setting will launch the "Ubuntu-18.04" distribution in its home file path:

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

1. In the profile for the command line of your choice, add `"suppressApplicationTitle": true` to suppress any title change events that get sent from the shell. Adding *only* this setting to your profile will set the tab title to the name of your profile.

2. If you want a custom tab title that is not the name of your profile, add `"tabTitle": "TITLE"`. Replacing "TITLE" with your preferred tab title.

## Command line arguments in PowerShell

Visit the [Command line arguments page](./command-line-arguments.md) to learn how command-line arguments operate in PowerShell.

## Command line arguments in WSL

Visit the [Command line arguments page](./command-line-arguments.md) to learn how command-line arguments operate in WSL.

## Problem setting `startingDirectory`

If the `startingDirectory` is being ignored in your profile, first check to make sure the syntax is correct in your [settings.json file](./install.md#settings-json-file). To help you check this syntax, `"$schema": "https://aka.ms/terminal-profiles-schema"` is automatically injected. Some applications, like [Visual Studio Code](https://code.visualstudio.com/download), can use that injected schema to validate your json file as you make edits.

If your settings are correct, you may be running a startup script that sets the starting directory of your terminal separately. For example, PowerShell has its own separate concept of profiles. If you are changing your starting directory there, it will take precedence over the setting defined in Windows Terminal.

Alternatively, if you are running a script using the `commandline` profile setting, it may be that you are setting the location there. Similar to PowerShell profiles, your commands there take precedence over the `startingDirectory` profile setting.

The purpose of `startingDirectory` is to launch a new Windows Terminal instance in the given directory. If the terminal runs any code that changes its directory, that may be a good place to take a look.

## Ctrl+= does not increase the font size

If you are using a German keyboard layout, you may run into this problem. <kbd>ctrl+=</kbd> gets deserialized as <kbd>ctrl+shift+0</kbd> if your main keyboard layout is set to German. This is the correct mapping for German keyboards.

More importantly, the app never receives the <kbd>ctrl+shift+0</kbd> keystroke. This is because <kbd>ctrl+shift+0</kbd> is reserved by Windows if you have multiple keyboard layouts active.

If you would like to disable this feature in order for `Ctrl+=` to work properly, follow the instructions for "Change Hotkeys to Switch Keyboard Layout in Windows 10" in this [blog post](https://winaero.com/blog/change-hotkeys-switch-keyboard-layout-windows-10/).

Change the 'Switch Keyboard Layout' option to 'Not Assigned' (or off of <kbd>ctrl+shift</kbd>), then select **OK** and then **Apply**. <kbd>ctrl+shift+0</kbd> should now work as a key binding and is passed through to the terminal.

On the other hand, if you do use this hotkey feature for multiple input languages, you can [configure your own custom key binding](./customize-settings/actions.md) in your [settings.json file](./install.md#settings-json-file).

## The text is blurry

Some display drivers and hardware combinations do not handle scroll and/or dirty regions without blurring the data from the previous frame. To mitigate this problem, you can add a combination of [these global rendering settings](./customize-settings/rendering.md) to reduce the strain placed on your hardware caused by the terminal text renderer.

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

## Why are my emojis not appearing as icons in the jumplist?

Only images linked from a file location can be rendered as profile icons in the jumplist. Emojis are not supported for jumplist icons.

## Technical Notes

Applications that use the [`GetConsoleScreenBufferInfo` family of APIs](/windows/console/getconsolescreenbufferinfoex) to retrieve the active console colors in Win32 format and then attempt to transform them into cross-platform VT sequences (for example, by transforming `BACKGROUND_RED` to `\x1b[41m`) may interfere with Terminal's ability to detect what background color the application is attempting to use.

Application developers are encouraged to choose either Windows API functions _or_ VT sequences for adjusting colors and not attempt to mix them.

### Keyboard service warning

Starting in Windows Terminal 1.5, the Terminal will display a warning if the "Touch Keyboard and Handwriting Panel Service" is disabled. This service is needed by the operating system to properly route input events to the Terminal application (as well as many other applications on Windows). If you see this warning, you can follow these steps to re-enable the service:

1. In the run dialog, run `services.msc`

    ![services.msc in the run dialog](https://user-images.githubusercontent.com/18356694/97891741-c81eed00-1cf4-11eb-9d48-7b94fede5294.png)

2. Find the "Touch Keyboard and Handwriting Panel Service"

    ![Touch Keyboard and Handwriting Panel Service in Services.msc](https://user-images.githubusercontent.com/18356694/97891813-e1279e00-1cf4-11eb-91c8-69a5c6da6c3d.png)

3. Open the "Properties" for this service

    ![service properties](https://user-images.githubusercontent.com/18356694/97891923-03212080-1cf5-11eb-90cc-821a4fbf16ba.png)

4. Change the "startup type" to "Automatic"

    ![service startup type](https://user-images.githubusercontent.com/18356694/97892043-25b33980-1cf5-11eb-8833-a2e65a306a79.png)

5. Hit "Ok", and restart the PC.

After restarting the machine, the service should auto-start, and the dialog should no longer appear.

## Why do I see blinking or flashing when using a git bash command line?

You may notice a blinking or flashing when using a git bash command line inside Windows Terminal. This behavior is actually by design. The Terminal is obeying what git bash is telling it to do (setting bell-style to visible, causing a flash to associate with the bell response), BUT we understand this may be distracting. To fix this, open the `.inputrc` file for your Git bash with a text editor. This file will likely be located in the path `C:\Program Files\Git\etc`. To open with the Nano text editor: `nano ~/.inputrc`

Change the default:

```bash
# none, visible or audible
set bell-style visible
```

Set the bell-style to either `none` or `audible` to remove the visible flash:

```bash
set bell-style none
```

Press Ctrl + O and Ctrl + X to Save and Exit.

## How do I reset my settings in Windows Terminal back to the default settings?

To reset your settings back to the original default settings, delete your [settings.json file](./install.md#settings-json-file). This will cause Windows Terminal to regenerate a settings.json file with the original default settings.

> [!IMPORTANT]
> As of Windows Terminal version 1.10 or greater, you'll also need to delete the `state.json` file in the same directory as the `settings.json` file to fully reset the settings to the defaults.

## Why is Acrylic opacity not making my Windows Terminal background transparent?

You can set the transparency of a terminal window with the [`useAcrylic` property](./customize-settings/profile-appearance.md#transparency). There are a few reasons why your opacity setting may not be working for Acrylic, including:

- As a system-wide policy, acrylic is only enabled for the foreground window. So if you activate any other window than the Terminal, the Terminal's acrylic will turn off.
- Acrylic doesn't work if your GPU hardware does not support it. If you're running an app in a Virtual Machine (VM) or over remote desktop, acrylic likely will not work.
- Acrylic can be disabled by the operating system for a number of reasons, like being in power saver (low-battery) mode or when accessing a machine using Remote Desktop.

## Why does my mouse pointer disappear when hovering over a window and typing?

This cursor auto-hiding behavior is by design, but can be disabled in the by searching in Windows Settings for "Mouse settings" > "Additional Mouse Settings" > "Mouse Properties" > "Pointer Options" > Uncheck "Hide pointer while typing". You may need to restart your Windows Terminal in order for this change to take effect.
