---
title: Windows Terminal Color Schemes
description: Learn how to create color schemes for Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: how-to
ms.service: terminal
---

# Color schemes in the Windows Terminal

## Creating your own color scheme

Color schemes can be defined in the `schemes` array of your settings.json file. They can be written in the following format:

```json
{
    "name" : "",

    "cursorColor": "#",
    "selectionBackground": "#",

    "background" : "#",
    "foreground" : "#",

    "black" : "#",
    "blue" : "#",
    "cyan" : "#",
    "green" : "#",
    "purple" : "#",
    "red" : "#",
    "white" : "#",
    "yellow" : "#",
    "brightBlack" : "#",
    "brightBlue" : "#",
    "brightCyan" : "#",
    "brightGreen" : "#",
    "brightPurple" : "#",
    "brightRed" : "#",
    "brightWhite" : "#",
    "brightYellow" : "#"
},
```

Every setting aside from `name` accepts a color as a string in hex format: `"#rgb"` or `"#rrggbb"`. `cursorColor` and `selectionBackground` are optional.

<br />

___

## Included color schemes

The following color schemes come included in the Windows Terminal. If you would like to set one in a profile, you can add the `colorScheme` property with the color scheme's `name` as the value.

```json
"colorScheme": "COLOR SCHEME NAME"
```

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

### Solarized Dark

![Windows Terminal Solarized Dark color scheme](./../images/solarized-dark-color-scheme.png)

### Solarized Light

![Windows Terminal Solarized Light color scheme](./../images/solarized-light-color-scheme.png)

### Tango Dark

![Windows Terminal Tango Dark color scheme](./../images/tango-dark-color-scheme.png)

### Tango Light

![Windows Terminal Tango Light color scheme](./../images/tango-light-color-scheme.png)
