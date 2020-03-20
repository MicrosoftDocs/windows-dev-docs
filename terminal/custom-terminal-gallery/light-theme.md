---
title: Windows Terminal Light Theme Configuration
description: This is the configuration for light theme.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: overview
ms.service: terminal
---

# Light Theme in Windows Terminal

```json
    {
        "requestedTheme": "light",
        "profiles": [
            {
                "name" : "Powershell",
                "source" : "Windows.Terminal.PowershellCore",
                "acrylicOpacity": 0.7,
                "colorScheme" : "Cinnamon",
                "cursorColor" : "#000000",
                "fontFace" : "Cascadia Code PL",
                "padding" : "8, 8, 8, 8",
                "suppressApplicationTitle": true,
                "useAcrylic": true
            }
        ],
        "schemes": [
            {
                "name" : "Cinnamon",
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
