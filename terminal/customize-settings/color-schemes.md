---
title: Windows Terminal Color Schemes
description: Learn how to create color schemes for Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 7/28/2020
ms.topic: how-to
ms.service: terminal
ms.localizationpriority: high
---

# Color schemes in Windows Terminal

Windows Terminal lets you define your own color schemes, either by using the built-in preset schemes, or by creating your own scheme from scratch. To change schemes, you'll need to edit the settings.json file in an editor such as [Visual Studio Code](https://code.visualstudio.com/).

## Switching to a different color scheme

Launch Windows Terminal and then select the small downward-facing arrow in the title bar. This will open a pull-down menu that lists the available profiles on your system (for example, Windows PowerShell and Command Prompt) and some other options. Select **Settings**, and the settings.json file will open in your default text editor.

This file is where you can define various options per window or per profile. To demonstrate, let's change the color scheme for the Command Prompt profile.

Look down the JSON file until you find the section that includes:

```json
"commandline": "cmd.exe",
"hidden": false"
```

Change it to read:

```json
"commandline": "cmd.exe",
"hidden": false",
"colorScheme": "Tango Light"
```

Notice the extra comma in the **hidden** line. Once you save this file, Windows Terminal will update any open window. Open a Command Prompt tab if you haven't already, and you'll immediately see that the colors have changed.

## Creating your own color scheme

The "Tango Light" scheme is included as a default option, but you can create your own scheme from scratch or by copying an existing scheme.

Color schemes can be defined in the `schemes` array of your settings.json file. They are written in the following format:

```json
{
    "name" : "Campbell",

    "cursorColor": "#FFFFFF",
    "selectionBackground": "#",

    "background" : "#0C0C0C",
    "foreground" : "#CCCCCC",

    "black" : "#0C0C0C",
    "blue" : "#0037DA",
    "cyan" : "#3A96DD",
    "green" : "#13A10E",
    "purple" : "#881798",
    "red" : "#C50F1F",
    "white" : "#CCCCCC",
    "yellow" : "#C19C00",
    "brightBlack" : "#767676",
    "brightBlue" : "#3B78FF",
    "brightCyan" : "#61D6D6",
    "brightGreen" : "#16C60C",
    "brightPurple" : "#B4009E",
    "brightRed" : "#E74856",
    "brightWhite" : "#F2F2F2",
    "brightYellow" : "#F9F1A5"
},
```

Every setting, aside from `name`, accepts a color as a string in hex format: `"#rgb"` or `"#rrggbb"`. The `cursorColor` and `selectionBackground` settings are optional.

<br />

___

## Included color schemes

Windows Terminal includes these color schemes inside the defaults.json file, which can be accessed by holding <kbd>alt</kbd> and selecting the settings button. 


### Campbell

![Windows Terminal Campbell color scheme](./../images/campbell-color-scheme.png)

### Campbell Powershell

![Windows Terminal Campbell Powershell color scheme](./../images/campbell-powershell-color-scheme.png)

### Vintage

![Windows Terminal Vintage color scheme](./../images/vintage-color-scheme.png)

### One Half Dark

![Windows Terminal One Half Dark color scheme](./../images/one-half-dark-color-scheme.png)

### One Half Light

![Windows Terminal One Half Light color scheme](./../images/one-half-light-color-scheme.png)

### Tango Dark

![Windows Terminal Tango Dark color scheme](./../images/tango-dark-color-scheme.png)

### Tango Light

![Windows Terminal Tango Light color scheme](./../images/tango-light-color-scheme.png)


## More schemes

For more schemes, see the [Custom Terminal Gallery](../custom-terminal-gallery/custom-schemes.md) section.
