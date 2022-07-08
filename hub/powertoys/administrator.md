---
title: PowerToys administrator mode for Windows
description: For PowerToys to work with an app running in elevated admin mode, PowerToys must be running in administrator mode as well.
ms.date: 04/27/2022
ms.topic: article
ms.localizationpriority: medium
no-loc: [PowerToys, Windows, FancyZones, Fancy, Zone, Zones]
---

# PowerToys running with administrator permissions

If you're running any application as an administrator (also referred to as elevated permissions), PowerToys may not work correctly when the elevated applications are in focus or trying to interact with a PowerToys feature like FancyZones. This can be addressed by also running PowerToys as an administrator.


## Options

There are two options for PowerToys to support applications running as administrator (with elevated permissions):

1. **Recommended**: PowerToys will display a prompt when an elevated process is detected. Open PowerToys Settings. Inside the General page, select <kbd>Restart as administrator</kbd>.
2. Enable **Always run as administrator** in the PowerToys Settings.


## Support for admin mode with PowerToys

PowerToys only needs elevated administrator permission when interacting with other applications that are running in administrator mode. If those applications are in focus, PowerToys may not function unless it is elevated as well.

These are the two scenarios PowerToys will not work in:

- Intercepting certain types of keyboard strokes
- Resizing / moving windows

### Affected PowerToys utilities

Admin mode permissions may be required in the following scenarios:

- FancyZones
  - Snapping an elevated window (e.g. Task Manager) into a Fancy Zone
  - Moving the elevated window to a different zone
- Shortcut guide
  - Display shortcut
- Keyboard remapper
  - Key to key remapping
  - Global level shortcuts remapping
  - App-targeted shortcuts remapping
- PowerToys Run
  - Display shortcut


## Run as administrator: elevated processes explained

Windows applications run in _User mode_ by default. To run an application in _Administrative mode_ or as an _elevated process_ means that app will run with additional access to the operating system.

The simplest way to run an app or program in administrative mode is to right-click the program and select <kbd>Run as administrator</kbd>. If the current user is not an administrator, Windows will ask for the administrator username and password.

Most apps do not need to run with elevated permission. A common scenario, however, for requiring administrator permission would be to run certain PowerShell commands or edit the registry.

If you see this User Account Control prompt, the application is requesting administrator level elevated permission:

![Windows UAC elevated permission prompt screenshot.](../images/pt-admin-prompt.png)

In the case of an elevated command line, typically the text "Administrator" will be included in the title bar.

![Windows Powershell and Command Line with elevated permissions screenshot.](../images/pt-admin-terminal.png)
