---
title: Help moving from Mac (Unix) to Windows
description: A guide to help you transition from a Mac (Unix) to a Windows development environment, including shortcut key mapping and a brief overview of concepts that differ between Mac and Windows.
author: mattwojo 
ms.author: mattwoj 
manager: jken
ms.topic: article
ms.technology: windows-nodejs
keywords: Mac to Windows, shortcut key mapping, move from Unix to Windows, transition from Mac to Windows, help moving from MacBook to Surface, how to use Windows for a Macintosh user, switching from Macintosh to Windows, help changing dev environments, Mac OS X to Windows, help moving from Mac to PC 
ms.localizationpriority: medium
ms.date: 09/19/2019
---

# Guide for changing your dev environment from Mac to Windows

The following tips and control equivalents should help you in your transition between a Mac and Windows (or WSL/Linux) development environment.

For app development, the nearest equivalent to Xcode would be [Visual Studio](https://visualstudio.microsoft.com). There is also a version of [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/), if you ever feel the need to go back. For cross-platform source code editing (and a huge number of plug-ins) [Visual Studio Code](https://code.visualstudio.com/?wt.mc_id=DX_841432) is the most popular choice.

## Keyboard shortcuts

| **Operation** | **Mac** | **Windows** |
|---------------|--------------------|---------------------|
| Copy | Command+C | Ctrl+C |
| Cut | Command+X | Ctrl+X |
| Paste | Command+V | Ctrl+V |
| Undo | Command+Z | Ctrl+Z |
| Save | Command+S | Ctrl+S |
| Open | Command+O | Ctrl+O |
| Lock computer | Command+Control+Q | WindowsKey+L |
| Show desktop | Command+F3 | WindowsKey+D |
| Minimize windows | Command+M | WindowsKey+M |
| Search | Command+Space | WindowsKey |
| Close active window | Command+W | Control+W |
| Switch current task | Command+Tab | Alt+Tab |
| Save screen (Screenshot) | Command+Shift+3 | WindowsKey+Shift+S |
| Save window | Command+Shift+4 | WindowsKey+Shift+S |
| View item information or properties | Command+I | Alt+Enter |
 | Select all items | Command+A | Ctrl+A |
| Select more than one item in a list (noncontiguous) | Command, then click each item | Control, then click each item |
| Type special characters | Option+ character key | Alt+ character key|

## Trackpad shortcuts

Note: Some of these shortcuts require a “Precision Trackpad”, such as the trackpad on Surface devices and some other third party laptops.

 **Operation** | **Mac** | **Windows** |
|---------------|--------------------|---------------------|
| Scroll | Two finger vertical swipe | Two finger vertical swipe |
| Zoom | Two finger pinch in and out | Two finger pinch in and out |
| Swipe back and forward between views | Two finger sideways swipe | Two finger sideways swipe |
| Switch virtual workspaces | Four fingers sideways swipe | Four fingers sideways swipe |
| Display currently open apps | Four fingers upward swipe | Three fingers upward swipe |
| Switch between apps | N/A | Slow three finger sideways swipe |
| Go to desktop | Spread out four fingers | Three finger swipe downwards |
| Open Cortana / Action center | Two finger slide from right | Three finger tap |
| Open extra information | Three finger tap | N/A |
|Show launchpad / start an app | Pinch with four fingers | Tap with four fingers |

Note: Trackpad options are configurable on both platforms.

## Terminal and Shell

Windows provides several alternatives to the Mac's terminal emulator.

1. The Windows Command Line

The Windows command line will accept DOS commands, and is the most commonly used command line tool on Windows. To open it: Press **WindowsKey+R** to open the **Run** box, then type **cmd** and then click **OK**. To open an administrator command line, type **cmd** and then press **Ctrl+Shift+Enter**.

2. PowerShell

[PowerShell](https://docs.microsoft.com/powershell/scripting/overview?view=powershell-6) is a "PowerShell is a task-based command-line shell and scripting language built on .NET. PowerShell helps system administrators and power-users rapidly automate tasks that manage operating systems". In other words, it's a very powerful command line, and is especially loved by system admins.

Incidentally, PowerShell is [also available for Mac](https://docs.microsoft.com/powershell/scripting/install/installing-powershell-core-on-macos?view=powershell-6).

3. Windows Subsystem for Linux (WSL)

WSL allows you to run a Linux shell within Windows. This means you can run *bash** or other shell, depending on choice and the specific Linux distro installed. Using WSL will provide the kind of environment most familiar to Mac users. For example, you will **ls** to list the files in a current directory, not **dir** as you would with the Windows command line. To learn about instaling and using WSL, see the [Windows Subsystem for Linux Installation Guide for Windows 10](https://docs.microsoft.com/windows/wsl/install-win10).

## Apps and utilities

 **App** | **Mac** | **Windows** |
|---------------|--------------------|---------------------|
| Settings and Preferences | System Preferences | Settings |
| Task manager | Activity Monitor | Task Manager |
| Disk formatting | Disk Utility | Disk Management |
| Text editing | TextEdit | Notepad |
| Event viewing | Console | Event Viewer |
| Find files/apps | Command+Space | Windows key |
