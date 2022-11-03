---
title: Windows Terminal Oh-My-Posh in PowerShell
description: This is the configuration and theme for Oh-My-Posh in PowerShell.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 08/30/2021
ms.topic: sample
---

# Oh-My-Posh in PowerShell theme for Windows Terminal

The prompt is styled using Oh-My-Posh and is using the `Caskaydia Cove Nerd Font`, which can be downloaded from [Nerd Fonts](https://www.nerdfonts.com/).

> [!div class="nextstepaction"]
> [Learn how to set up a custom prompt](./../tutorials/custom-prompt-setup.md)

![Windows Terminal Custom Prompt](./../images/custom-prompt.png)

```json
    {
        "theme": "dark",
        "profiles": [
            {
                "name" : "Powershell",
                "source" : "Windows.Terminal.PowershellCore",
                "opacity" : 50,
                "colorScheme" : "One Half Dark",
                "cursorColor" : "#FFFFFF",
                "font": 
                {
                    "face": "CaskaydiaCove Nerd Font"
                },
                "useAcrylic" : true
            }
        ]
    }
```
