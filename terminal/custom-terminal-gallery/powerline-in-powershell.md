---
title: Windows Terminal Powerline in PowerShell Configuration
description: This is the configuration for Powerline in PowerShell.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: overview
ms.service: terminal
---

# Powerline in PowerShell in Windows Terminal

```json
    {
        "requestedTheme": "dark",
        "profiles": [
            {
                "name" : "Powershell",
                "source" : "Windows.Terminal.PowershellCore",
                "acrylicOpacity" : 0.7,
                "colorScheme" : "Campbell",
                "cursorColor" : "#FFFFFD",
                "fontFace" : "Cascadia Code PL",
                "padding" : "8, 8, 8, 8",
                "useAcrylic" : true,
                "suppressApplicationTitle": true
            }
        ]
    }
```

The prompt is styled using Powerline and is using the `Cascadia Code PL` font, which can be downloaded from the [Cascadia Code GitHub releases page](https://github.com/microsoft/cascadia-code/releases).

> [!div class="nextstepaction"]
> [Learn how to set up Powerline](./../tutorials/powerline-setup.md)