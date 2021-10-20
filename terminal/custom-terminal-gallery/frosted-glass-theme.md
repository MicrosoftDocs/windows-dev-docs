---
title: Windows Terminal Frosted Glass Theme
description: This is a sample configuration for a frosted glass theme.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 08/30/2021
ms.topic: sample
---

# Frosted Glass Theme in Windows Terminal

The prompt is styled using Powerline and is using the `Cascadia Code PL` font, which can be downloaded from the [Cascadia Code GitHub releases page](https://github.com/microsoft/cascadia-code/releases).

![Windows Terminal Frosted Glass Theme](./../images/frosted-glass-theme.png)

```json
    {
        "theme": "light",
        "profiles": [
            {
                "name" : "PowerShell",
                "source" : "Windows.Terminal.PowershellCore",
                "acrylicOpacity": 0.7,
                "colorScheme" : "Frost",
                "cursorColor" : "#000000",
                "fontFace" : "Cascadia Code PL",
                "useAcrylic": true
            }
        ],
        "schemes": [
            {
                "name" : "Frost",
                "background" : "#FFFFFF",
                "black" : "#3C5712",
                "blue" : "#17b2ff",
                "brightBlack" : "#749B36",
                "brightBlue" : "#27B2F6",
                "brightCyan" : "#13A8C0",
                "brightGreen" : "#89AF50",
                "brightPurple" : "#F2A20A",
                "brightRed" : "#F49B36",
                "brightWhite" : "#741274",
                "brightYellow" : "#991070",
                "cyan" : "#3C96A6",
                "foreground" : "#000000",
                "green" : "#6AAE08",
                "purple" : "#991070",
                "red" : "#8D0C0C",
                "white" : "#6E386E",
                "yellow" : "#991070"
            }
        ]
    }
```
