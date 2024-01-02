---
title: PowerToys Command Not Found for Windows
description: A PowerShell 7 module that detects an error thrown by a command and suggests a relevant WinGet package to install, if available.
ms.date: 02/01/2024
ms.topic: article
no-loc: [PowerToys, Windows, Command Not Found, Win]
---

# Command Not Found utility

A PowerShell 7 module that detects an error thrown by a command and suggests a relevant WinGet package to install, if available.

![AlwaysOnTop screenshot.](../images/pt-cmd-not-found.png)

## Requirements
 - [PowerShell 7](https://learn.microsoft.com/en-us/PowerShell/scripting/install/installing-PowerShell-on-windows?view=PowerShell-7.4)
 - [PowerShell Microsoft.WinGet.Client module](https://www.powershellgallery.com/packages/Microsoft.WinGet.Client)

## Install the module

To install the Command Not Found module, navigate to PowerToys settings Command Not Found page and press Install button. On module install, PowerShell 7 experimental features needed for the module to function will be enabled:
 - PSFeedbackProvider
 - PSCommandNotFoundSuggestion

After that, PowerShell profile file will be appended with following block of PowerShell commands:
```
#34de4b3d-13a8-4540-b76d-b9e8d3851756 PowerToys CommandNotFound module
Import-Module "<powertoys install dir>/WinGetCommandNotFound.psd1"
#34de4b3d-13a8-4540-b76d-b9e8d3851756
```

Note: If there is no profile file at the time of installing the module, profile file will be created. After installing the module, restart PowerShell session to use the module.

## Uninstall the module

To uninstall the Command Not Found module, navigate to PowerToys settings Command Not Found page and press Uninstall button. On module uninstall, previously added block of commands will be removed from PowerShell profile file. 
