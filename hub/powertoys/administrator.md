---
title: PowerToys administrator mode for Windows 10
description: For PowerToys to work with an app running in elevated admin mode, PowerToys must be run in administrator mode as well.
ms.date: 05/28/2021
ms.topic: article
ms.localizationpriority: medium
no-loc: [PowerToys, Windows, FancyZones, Fancy, Zone, Zones]
---

# PowerToys running with administrator elevated permissions

If you're running any application as an administrator (also referred to as elevated permissions), PowerToys may not work correctly when the elevated applications are in focus or trying to interact with a PowerToys feature like FancyZones. This can be addressed by also running PowerToys as an administrator.

## Options

There are two options for PowerToys to support applications running as administrator (with elevated permissions):

- **[Recommended]**: PowerToys will display a prompt when an elevated process is detected. Open **PowerToys Settings**. Inside the **General** tab, select "Restart as administrator".

- Enable "Always run as administrator" in the **PowerToys Settings**.

## Run as administrator elevated processes explained

Windows applications run in **User mode** by default. To run an application in **Administrative mode** or as an *elevated process* means that app will run with additional access to the operating system.

The simplest way to run an app or program in administrative mode is to right-click the program and select **Run as administrator**. If the current user is not an administrator, Windows will ask for the administrator username and password.

Most apps do not need to run with elevated permission. A common scenario, however, for requiring administrator permission would be to run certain PowerShell commands or edit the registry.

If you see this prompt (User Access Control prompt), the application is requesting administrator level elevated permission:

![Windows elevated permission prompt screenshot](../images/pt-admin-prompt.png)

In the case of an elevated command line, typically the title "Administrator" will be appended to the title bar.

![Windows admin command line screenshot](../images/pt-admin-terminal.png)

## Support for admin mode with PowerToys

PowerToys only needs elevated administrator permission when interacting with other applications that are running in administrator mode. If those applications are in focus, PowerToys may not function unless it is elevated as well.

These are the two scenarios we will not work in:

- Intercepting certain types of keyboard strokes
- Resizing / Moving windows

### Affected PowerToys utilities

Admin mode permissions may be required in the following scenarios:

- FancyZones
  - Snapping an elevated window (e.g. command prompt) into a Fancy Zone
  - Moving the elevated window to a different zone
- Shortcut guide
  - Display shortcut
- Keyboard remapper
  - Key to key remapping
  - Global level shortcuts remapping
  - App-targeted shortcuts remapping
- PowerToys Run
  - Display shortcut
