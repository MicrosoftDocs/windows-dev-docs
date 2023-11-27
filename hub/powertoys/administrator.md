---
title: PowerToys administrator mode for Windows
description: For PowerToys to work with an app running in elevated admin mode, PowerToys must be running in administrator mode as well.
ms.date: 08/03/2023
ms.topic: article
ms.localizationpriority: medium
no-loc: [PowerToys, Windows, FancyZones, Fancy, Zone, Zones]
---

# PowerToys running with administrator permissions

When running any application as an administrator (also referred to as elevated permissions), PowerToys may not work correctly when the elevated applications are in focus or trying to interact with a PowerToys feature like FancyZones. This can be addressed by also running PowerToys as administrator.

## Options

There are two options for PowerToys to support applications running as administrator (with elevated permissions):

1. **Recommended**: PowerToys will display a notification when an elevated process is detected. Open PowerToys Settings. On the General tab, select **Restart as administrator**.
2. Enable **Always run as administrator** in the PowerToys Settings.

## Support for admin mode with PowerToys

PowerToys needs elevated administrator permission when writing protected system settings or when interacting with other applications that are running in administrator mode. If those applications are in focus, PowerToys may not function unless it is elevated as well.

These are the two scenarios PowerToys will not work in:

- Intercepting certain types of keyboard strokes
- Resizing / moving windows

### Affected PowerToys utilities

Admin mode permissions may be required in the following scenarios:

- Always On Top
  - Pin windows that are elevated
- File Locksmith
  - End elevated processes
- FancyZones
  - Snapping an elevated window (e.g. Task Manager) into a Fancy Zone
  - Moving the elevated window to a different zone
- Hosts file editor
- Shortcut guide
  - Display shortcut
- Keyboard remapper
  - Key to key remapping
  - Global level shortcuts remapping
  - App-targeted shortcuts remapping
- PowerToys Run
  - Use shortcut
- Registry Preview
  - Write keys to the registry
- Video Conference Mute

## Run as administrator: elevated processes explained

Windows applications run in _User mode_ by default. To run an application in _Administrative mode_ or as an _elevated process_ means that app will run with additional access to the operating system. Most apps do not need to run with elevated permission. A common scenario, however, for requiring administrator permission would be to run certain PowerShell commands or edit the registry.

The simplest way to run an app or program in administrative mode is to right-click the program and select **Run as administrator**. If the current user is not an administrator, Windows will ask for the administrator username and password.

If you see this User Account Control prompt, the application is requesting administrator level elevated permission:

![Windows UAC elevated permission prompt screenshot.](../images/pt-admin-prompt.png)

In the case of an elevated command line, typically the text "Administrator" will be included in the title bar.

![Windows Powershell and Command Line with elevated permissions screenshot.](../images/pt-admin-terminal.png)
