---
title: Windows Terminal Retro Command Prompt
description: This is the configuration for a retro command prompt in Windows Terminal.
author: cinnamon-msft
ms.author: cinnamon
ms.date: 05/19/2020
ms.topic: sample
---

# Retro Command Prompt in Windows Terminal

The prompt is using the `PxPlus IBM VGA8` font, which is not included in Windows Terminal.

![Windows Terminal Retro Command Prompt](./../images/retro-command-prompt.png)

```json
    {
        "theme": "dark",
        "profiles": [
            {
                "name": "Command Prompt",
                "commandline": "cmd.exe",
                "closeOnExit" : true,
                "colorScheme" : "Retro",
                "cursorColor" : "#FFFFFF",
                "cursorShape": "filledBox",
                "fontSize" : 16,
                "padding" : "5, 5, 5, 5",
                "tabTitle" : "Command Prompt",
                "fontFace": "PxPlus IBM VGA8",
                "experimental.retroTerminalEffect": true
            }
        ],
        "schemes": [
            {
                "name": "Retro",
                "background": "#000000",
                "black": "#00ff00",
                "blue": "#00ff00",
                "brightBlack": "#00ff00",
                "brightBlue": "#00ff00",
                "brightCyan": "#00ff00",
                "brightGreen": "#00ff00",
                "brightPurple": "#00ff00",
                "brightRed": "#00ff00",
                "brightWhite": "#00ff00",
                "brightYellow": "#00ff00",
                "cyan": "#00ff00",
                "foreground": "#00ff00",
                "green": "#00ff00",
                "purple": "#00ff00",
                "red": "#00ff00",
                "white": "#00ff00",
                "yellow": "#00ff00"
            }
        ]
    }
```
