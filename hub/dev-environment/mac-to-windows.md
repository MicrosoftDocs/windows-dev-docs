---
title: Moving from Mac (Unix) to Windows
description: A guide to help you transition from a Mac (Unix) to a Windows development environment, including shortcut key mapping and a brief overview of concepts that differ between Mac and Windows.
ms.topic: article
ms.subservice: windows-nodejs
keywords: Mac to Windows, shortcut key mapping, move from Unix to Windows, transition from Mac to Windows, help moving from MacBook to Surface, how to use Windows for a Macintosh user, switching from Macintosh to Windows, help changing dev environments, Mac OS X to Windows, help moving from Mac to PC 
ms.localizationpriority: medium
ms.date: 09/19/2019
---

# Guide for changing your dev environment from Mac to Windows

The following tips and control equivalents should help you in your transition between a Mac and Windows (or WSL/Linux) development environment.

For app development, the nearest equivalent to Xcode would be [Visual Studio](https://visualstudio.microsoft.com). There is also a version of [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/), if you ever feel the need to go back. For cross-platform source code editing (and a huge number of plug-ins) [Visual Studio Code](https://code.visualstudio.com/?wt.mc_id=DX_841432) is the most popular choice.

## Keyboard shortcuts

> [!TIP]
> You can use [PowerToys Keyboard Manager](../powertoys/keyboard-manager.md) to map Windows shortcuts to the shortcuts you use on a Mac.

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
| Open file browser | Command+N | WindowsKey+E |
| Minimize windows | Command+M | WindowsKey+M |
| Search | Command+Space | WindowsKey |
| Close active window | Command+W | Control+W |
| Switch current task | Command+Tab | Alt+Tab |
| Maximize a window to full screen | Control+Command+F | WindowsKey+Up |
| Save screen (Screenshot) | Command+Shift+3 | WindowsKey+Shift+S |
| Save window | Command+Shift+4 | WindowsKey+Shift+S |
| View item information or properties | Command+I | Alt+Enter |
 | Select all items | Command+A | Ctrl+A |
| Select more than one item in a list (noncontiguous) | Command, then click each item | Control, then click each item |
| Type special characters | Option+ character key | Alt+ character key|

## Trackpad shortcuts

> [!NOTE]
> Some of these shortcuts require a "Precision Trackpad", such as the trackpad on Surface devices and some other third-party laptops. 
> 
> Trackpad options are configurable on both platforms.

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

## Command-line shells and terminals

Windows supports several command-line shells and terminals which sometimes work a little differently to the Mac's BASH shell and terminal emulator apps like Terminal and iTerm.

### Windows shells

Windows has two primary command-line shells:

1. **[PowerShell](/powershell/scripting/overview)** - PowerShell is a cross-platform task automation and configuration management framework, consisting of a command-line shell and scripting language built on .NET. Using PowerShell, administrators, developers, and power-users can rapidly control and automate tasks that manage complex processes and various aspects of the environment and operating system upon which it is run. PowerShell is [fully open-source](https://github.com/powershell/powershell), and because it is cross-platform, also [available for Mac and Linux](/powershell/scripting/install/installing-powershell).

    **Mac and Linux BASH shell users**: PowerShell also supports many command-aliases that you are already familiar with. For example:
    - List the contents of the current directory, using: `ls`
    - Move files with: `mv`
    - Move to a new directory with: `cd <path>`

    Some commands and arguments are different in PowerShell vs. BASH. Learn more by entering: [`get-help`](/powershell/scripting/learn/ps101/02-help-system) in PowerShell or checkout the [compatibility aliases](/powershell/scripting/samples/appendix-1---compatibility-aliases) in the docs.

    To run PowerShell as an Administrator, enter "PowerShell" in your Windows start menu, then select "Run as Administrator."

2. **Windows Command Line (Cmd)**: Windows still ships the traditional Command Prompt (and Console – see below), providing compatibility with current and legacy MS-DOS-compatible commands and batch files. Cmd is useful when running existing/older batch files or command-line operations, but in general, users are recommended to learn and use PowerShell since Cmd is now in maintenance, and will not be receiving any improvements or new features in the future.

### Linux shells

Windows Subsystem for Linux (WSL) can now be installed to support running a Linux shell within Windows. This means that you can run **bash**, with whichever specific Linux distribution you choose, integrated right inside Windows. Using WSL will provide the kind of environment most familiar to Mac users. For example, you will **ls** to list the files in a current directory, not **dir** as you would with the traditional Windows Cmd Shell. To learn about installing and using WSL, see the [Windows Subsystem for Linux Installation Guide](/windows/wsl/install). Linux distributions that can be installed on Windows with WSL include:

1. [Ubuntu 20.04 LTS](https://www.microsoft.com/store/apps/9n6svws3rx71)
2. [Kali Linux](https://www.microsoft.com/store/apps/9PKR34TNCV07)
3. [Debian GNU/Linux](https://www.microsoft.com/store/apps/9MSVKQC78PK6)
4. [openSUSE Leap 15.1](https://www.microsoft.com/store/apps/9NJFZK00FGKV)
5. [SUSE Linux Enterprise Server 15 SP1](https://www.microsoft.com/store/apps/9PN498VPMF3Z)

Just to name a few. Find more in the [WSL install docs](/windows/wsl/install-win10#install-your-linux-distribution-of-choice) and install them directly from the [Microsoft Store](https://aka.ms/wslstore).

## Windows Terminals

In addition to many 3rd party offerings, Microsoft provides two "terminals" – GUI applications that provide access to command-line shells and applications.

1. **[Windows Terminal](/windows/terminal/)**: Windows Terminal is a new, modern, highly configurable command-line terminal application that provides very high performance, low-latency command-line user experience, multiple tabs, split window panes, custom themes and styles, multiple "profiles" for different shells or command-line apps, and considerable opportunities for you to configure and personalize many aspects of your command-line user experience.

    You can use Windows Terminal to open tabs connected to PowerShell, WSL shells (like Ubuntu or Debian), the traditional Windows Command Prompt, or any other command-line app (e.g. SSH, Azure CLI, Git Bash).

2. **[Console](/windows/console/)**: On Mac and Linux, users usually start their preferred terminal application which then creates and connects to the user's default shell (e.g. BASH).

    However, due to a quirk of history, Windows users traditionally start their shell, and Windows automatically starts and connects a GUI Console app.

    While one can still launch shells directly and use the legacy Windows Console, it's highly recommended that users instead install and use Windows Terminal to experience the best, fastest, most productive command-line experience.

## Apps and utilities

 **App** | **Mac** | **Windows** |
|---------------|--------------------|---------------------|
| Settings and Preferences | System Preferences | Settings |
| Task manager | Activity Monitor | Task Manager |
| Disk formatting | Disk Utility | Disk Management |
| Text editing | TextEdit | Notepad |
| Event viewing | Console | Event Viewer |
| Find files/apps | Command+Space | Windows key |