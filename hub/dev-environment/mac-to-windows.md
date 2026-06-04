---
title: Moving from Mac (Unix) to Windows
description: A guide to help you transition from a Mac (Unix) to a Windows development environment, including shortcut key mapping and a brief overview of concepts that differ between Mac and Windows.
ms.topic: how-to
ms.subservice: dev-environment
keywords: Mac to Windows, shortcut key mapping, move from Unix to Windows, transition from Mac to Windows, help moving from MacBook to Surface, how to use Windows for a Macintosh user, switching from Macintosh to Windows, help changing dev environments, Mac OS X to Windows, help moving from Mac to PC 
ms.localizationpriority: medium
ms.date: 06/04/2026
---

# Guide for changing your dev environment from Mac to Windows

The following tips and control equivalents help you transition between a Mac and Windows (or WSL/Linux) development environment.

For app development, the nearest equivalent to Xcode is [Visual Studio](https://visualstudio.microsoft.com). For cross-platform source code editing (and a huge number of plug-ins), [Visual Studio Code](https://code.visualstudio.com/?wt.mc_id=DX_841432) is the most popular choice.

## Keyboard shortcuts

> [!TIP]
> Use [PowerToys Keyboard Manager](../powertoys/keyboard-manager.md) to map Windows shortcuts to the shortcuts you use on a Mac.

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
| Save screen (Screenshot) | Command+Shift+3 | WindowsKey+PrtScn (saves to file) or PrtScn (to clipboard) |
| Save region/window (Screenshot) | Command+Shift+4 | WindowsKey+Shift+S (opens Snipping Tool) |
| View item information or properties | Command+I | Alt+Enter |
 | Select all items | Command+A | Ctrl+A |
| Select more than one item in a list (noncontiguous) | Command, then click each item | Control, then click each item |
| Type special characters | Option+ character key | Alt+ character key|

## Trackpad shortcuts

> [!NOTE]
> Some of these shortcuts require a "Precision Trackpad," such as the trackpad on Surface devices and some other third-party laptops. 
> 
> You can configure trackpad options on both platforms.

 **Operation** | **Mac** | **Windows** |
|---------------|--------------------|---------------------|
| Scroll | Two finger vertical swipe | Two finger vertical swipe |
| Zoom | Two finger pinch in and out | Two finger pinch in and out |
| Swipe back and forward between views | Two finger sideways swipe | Two finger sideways swipe |
| Switch virtual workspaces | Four fingers sideways swipe | Four fingers sideways swipe |
| Display currently open apps | Four fingers upward swipe | Three fingers upward swipe |
| Switch between apps | N/A | Slow three finger sideways swipe |
| Go to desktop | Spread out four fingers | Three finger swipe downwards |
| Open Action center | Two finger slide from right | Three finger tap |
| Open extra information | Three finger tap | N/A |
|Show launchpad / start an app | Pinch with four fingers | Tap with four fingers |

## Command-line shells and terminals

Windows supports several command-line shells and terminals. These tools sometimes work a little differently from the Mac's BASH shell and terminal emulator apps like Terminal and iTerm.

### Windows shells

Windows has two primary command-line shells:

1. **[PowerShell](/powershell/scripting/overview)** - PowerShell is a cross-platform task automation and configuration management framework. It consists of a command-line shell and scripting language built on .NET. With PowerShell, administrators, developers, and power-users can quickly control and automate tasks that manage complex processes and various aspects of the environment and operating system. PowerShell is [fully open-source](https://github.com/powershell/powershell), and because it's cross-platform, it's also [available for Mac and Linux](/powershell/scripting/install/installing-powershell).

    **Mac and Linux BASH shell users**: PowerShell also supports many command aliases that you're already familiar with. For example:
    - List the contents of the current directory with: `ls`
    - Move files with: `mv`
    - Move to a new directory with: `cd <path>`

    Some commands and arguments are different in PowerShell versus BASH. Learn more by entering: [`get-help`](/powershell/scripting/learn/ps101/02-help-system) in PowerShell or check out the [compatibility aliases](/powershell/scripting/samples/appendix-1---compatibility-aliases) in the docs.

    To run PowerShell as an administrator, enter "PowerShell" in your Windows start menu, then select **Run as Administrator**.

1. **Windows Command Line (Cmd)** - Windows still ships the traditional Command Prompt (and Console - see below), providing compatibility with current and legacy MS-DOS-compatible commands and batch files. Cmd is useful when running existing or older batch files or command-line operations. However, learn and use PowerShell since Cmd is now in maintenance and won't receive any improvements or new features in the future.

### Native Unix-style commands

If you're coming from a Mac or Linux background, you'll find many familiar commands available natively on Windows without needing WSL:

- **[Coreutils for Windows](../core-utils/overview.md)**: A native port of GNU Coreutils that brings commands like `ls`, `cat`, `cp`, `mv`, `grep`, `head`, `tail`, `wc`, `sort`, `uniq`, and more to Windows. Install via WinGet (`winget install uutils.coreutils`) or from GitHub. In PowerShell, built-in aliases may take precedence — use `ls.exe`, `cp.exe`, etc. to call the Coreutils versions directly. See the [full command list](../core-utils/commands.md).
- **[sudo](../advanced-settings/sudo/index.md)**: Run commands with elevated privileges, just like on macOS. Available on Windows 11 24H2 and later — enable it in **Settings > For Developers**.
- **[curl](../curl/index.md)** and **[tar](../tar/index.md)**: Available natively for downloading files and working with archives.
- **[Edit](../edit/index.md)**: A lightweight terminal-based text editor for quick edits from the command line. Included in Windows 11 starting with the September 2025 update, or install via WinGet (`winget install Microsoft.Edit`).

These tools mean you can use your familiar Unix workflow directly in Windows without switching to WSL for basic tasks.

### Linux shells

You can now install Windows Subsystem for Linux (WSL) to support running a Linux shell within Windows. This means that you can run **bash**, with whichever specific Linux distribution you choose, integrated right inside Windows. Using WSL provides the kind of environment most familiar to Mac users. For example, you use **ls** to list the files in a current directory, not **dir** as you would with the traditional Windows Cmd Shell. To learn about installing and using WSL, see the [Windows Subsystem for Linux Installation Guide](/windows/wsl/install). Linux distributions that you can install on Windows with WSL include:

1. [Ubuntu 24.04 LTS](https://www.microsoft.com/store/apps/9NZ3KLHXDJP5)
1. [Ubuntu 22.04 LTS](https://www.microsoft.com/store/apps/9PN20MSQ3V9KL)
1. [Kali Linux](https://www.microsoft.com/store/apps/9PKR34TNCV07)
1. [Debian GNU/Linux](https://www.microsoft.com/store/apps/9MSVKQC78PK6)
1. [openSUSE Leap 15.6](https://www.microsoft.com/store/apps/9NZJ0Z189KMR)
1. [SUSE Linux Enterprise Server 15 SP6](https://www.microsoft.com/store/apps/9PNLGLNJNRPN)

Just to name a few. Find more in the [WSL install docs](/windows/wsl/install#change-the-default-linux-distribution-installed) and install them directly from the [Microsoft Store](https://aka.ms/wslstore).

## Windows terminals

In addition to many third-party offerings, Microsoft provides two terminals. These terminals are GUI applications that provide access to command-line shells and applications.

1. **[Windows Terminal](/windows/terminal/)**: Windows Terminal is a modern, highly configurable command-line terminal application that provides very high performance, low-latency command-line user experience, multiple tabs, split window panes, custom themes and styles, multiple "profiles" for different shells or command-line apps, and considerable opportunities for you to configure and personalize many aspects of your command-line user experience. Windows Terminal comes pre-installed on Windows 11. On Windows 10, you can install it from the [Microsoft Store](https://aka.ms/terminal).

    You can use Windows Terminal to open tabs connected to PowerShell, WSL shells (like Ubuntu or Debian), the traditional Windows Command Prompt, or any other command-line app (for example, SSH, Azure CLI, Git Bash).

1. **[Console](/windows/console/)**: On macOS and Linux, users usually start their preferred terminal application, which then creates and connects to the user's default shell (for example, BASH).

    However, due to a quirk of history, Windows users traditionally start their shell, and Windows automatically starts and connects a GUI Console app.

    While you can still launch shells directly and use the legacy Windows Console, it's highly recommended that you instead install and use Windows Terminal to experience the best, fastest, most productive command-line experience.

## Apps and utilities

 **App** | **macOS** | **Windows** |
|---------------|--------------------|---------------------|
| Settings and Preferences | System Preferences | Settings |
| Task manager | Activity Monitor | Task Manager |
| Disk formatting | Disk Utility | Disk Management |
| Text editing | TextEdit | Notepad or [Edit](../edit/index.md) (terminal) |
| Event viewing | Console | Event Viewer |
| Find files/apps | Command+Space | Windows key |
| Package manager | Homebrew | [WinGet](/windows/package-manager/) |
| Developer storage | N/A | [Dev Drive](../dev-drive/index.md) |
| Unix CLI tools | Built-in (ls, cat, grep, etc.) | [Coreutils](../core-utils/overview.md) + [sudo](../advanced-settings/sudo/index.md) |
| Window management | Mission Control | Snap Layouts (WindowsKey+Z) |
| Productivity utilities | N/A | [PowerToys](../powertoys/index.md) |
