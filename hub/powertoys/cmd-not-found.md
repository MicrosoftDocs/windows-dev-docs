---
title: PowerToys Command Not Found Tool for PowerShell 7 on Windows
description: Learn how to use PowerToys Command Not Found tool for PowerShell 7. Automatically detect command errors and get WinGet package suggestions to install missing tools.
ms.date: 08/20/2025
ms.topic: concept-article
no-loc: [PowerToys, Windows, Command Not Found, Win]
# Customer intent: As a Windows power user, I want to learn about the Command Not Found utility in PowerToys for Windows.
---

# Command Not Found utility for PowerShell

The PowerToys Command Not Found utility is a PowerShell 7 module that detects command-line errors and suggests relevant WinGet packages to install. This tool helps Windows users quickly find and install missing commands, improving productivity and reducing troubleshooting time.

> [!IMPORTANT]
> There are some incompatibilities between Command Not Found and some PowerShell configurations. Read more about them in [issue 30818](https://github.com/microsoft/PowerToys/issues/30818) on GitHub.

:::image type="content" source="images/cmd-not-found/cmd-not-found.png" alt-text="Screenshot of PowerToys Command Not Found utility suggesting WinGet package installation in PowerShell terminal.":::

## Requirements

- [PowerShell 7](/PowerShell/scripting/install/installing-PowerShell-on-windows)
- [PowerShell Microsoft.WinGet.Client module](https://www.powershellgallery.com/packages/Microsoft.WinGet.Client)

## Install the module

To install the Command Not Found module, go to the Command Not Found page in PowerToys settings and select **Install**. Once the installation has completed, the following PowerShell 7 experimental features needed for the module to function will be enabled:

- PSFeedbackProvider
- PSCommandNotFoundSuggestion

After that, the PowerShell profile file will be appended with following block of PowerShell commands:

```psh
#34de4b3d-13a8-4540-b76d-b9e8d3851756 PowerToys CommandNotFound module
Import-Module "<powertoys install dir>/WinGetCommandNotFound.psd1"
#34de4b3d-13a8-4540-b76d-b9e8d3851756
```

> [!NOTE]
> The profile file will be created if needed. Restart PowerShell session to use the module.

## Uninstall the module

To uninstall the Command Not Found module, go to the Command Not Found page in PowerToys settings and select **Uninstall**. Once the uninstallation has completed, the block of commands previously added will be removed from the PowerShell profile file.

[!INCLUDE [install-powertoys.md](../includes/install-powertoys.md)]
