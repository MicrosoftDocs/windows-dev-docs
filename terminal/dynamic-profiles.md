---
title: Windows Terminal Dynamic Profiles
description: Learn about dynamic profiles in Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: concept
---

# Dynamic profiles in Windows Terminal

Windows Terminal will automatically create Windows Subsystem for Linux (WSL) and PowerShell profiles for you if you have these shells installed on your machine. This makes it easier for you to have all of your shells included in the terminal without having to locate their executable files. These profiles are generated with the `source` property, which tells the terminal where to locate the proper executable.

Upon installing the terminal, it will set PowerShell as your default profile. To learn how to change your default profile, visit the [Global settings page](./customize-settings/global-settings.md).

![Windows Terminal dynamic profiles](./images/dynamic-profiles.png)
_Configuration: [Light Theme](./custom-terminal-gallery/frosted-glass-theme.md)_

## Installing a new shell after installing Windows Terminal

Regardless of whether a new shell is installed before or after your terminal installation, the terminal will create a new profile for the newly installed shell.

## Hide a profile

To hide a profile from your terminal dropdown menu, add the `hidden` property to the profile object in your settings.json file and set it to `true`.

```json
"hidden": true
```

If you delete a dynamically-created profile, the terminal will automatically regenerate the profile and replace it in your settings.json file.

## Prevent a profile from being generated

To prevent a dynamic profile from being generated, you can add the profile generator to the `disabledProfileSources` array in your global settings. More information on this setting can be found on the [Global settings page](./customize-settings/global-settings.md#disable-dynamic-profiles).

```json
"disabledProfileSources": ["Windows.Terminal.Wsl", "Windows.Terminal.Azure", "Windows.Terminal.PowershellCore"]
```
