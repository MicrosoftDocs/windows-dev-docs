---
title: Moving from Mac (Unix) to Windows
description: Set up a Windows development environment with current tools, shortcuts, Unix commands, WSL, WinGet, Dev Drive, and PowerToys.
author: GrantMeStrength
ms.author: jken
ms.topic: how-to
keywords: Mac to Windows, shortcut key mapping, move from Unix to Windows, transition from Mac to Windows, help moving from MacBook to Surface, how to use Windows for a Macintosh user, switching from Macintosh to Windows, Mac OS X to Windows, help moving from Mac to PC
ms.localizationpriority: medium
ms.date: 08/31/2026
---

# Moving from Mac (Unix) to Windows

This guide helps you transfer familiar macOS development workflows to Windows, Windows Subsystem for Linux (WSL), and Windows-native developer tools.

For native Windows app development, the nearest equivalent to Xcode is [Visual Studio](https://visualstudio.microsoft.com). For cross-platform source code editing and a large extension ecosystem, use [Visual Studio Code](https://code.visualstudio.com/?wt.mc_id=DX_841432). If you build Windows apps from a terminal or a cross-platform framework, the public-preview [Windows App Development CLI](../apps/dev-tools/winapp-cli/index.md) can set up SDKs, add package identity, and create MSIX packages.

## Set up Windows for development

Start with these Windows 11 features:

1. **Configure developer settings.** On Windows 11, version 25H2 and later, open **Settings > System > Advanced** to configure file extensions, hidden files, long paths, the default terminal, PowerShell scripts, sudo, Developer Mode, and Dev Drive. On earlier releases, search Settings for **For developers**. Developer Mode is primarily needed to develop, deploy, and test Windows apps; it isn't required for most web or cross-platform development. For details, see [Advanced Windows Settings](../advanced-settings/index.md).
1. **Install tools with WinGet.** [Windows Package Manager (WinGet)](/windows/package-manager/) is the Windows equivalent of a package manager such as Homebrew. Use it to search for, install, upgrade, remove, and configure applications. For example, `winget upgrade --all` updates packages that WinGet manages.
1. **Make setup repeatable.** [WinGet Configuration](/windows/package-manager/configuration/) uses a YAML file to declare packages and Windows settings. Store a configuration with your project or onboarding documentation, review its resources before running it, and apply it with:

    ```powershell
    winget configure -f <path-to-configuration-file>
    ```

1. **Consider a Dev Drive.** [Dev Drive](../dev-drive/index.md) is a ReFS volume optimized for development workloads. Use it for source repositories, package caches, and build output rather than general documents. Create one from **Settings > System > Storage > Advanced storage settings > Disks & volumes**.

## Keyboard shortcuts

> [!TIP]
> Use [PowerToys Keyboard Manager](../powertoys/keyboard-manager.md) to remap keys and shortcuts. [PowerToys Quick Accent](../powertoys/quick-accent.md) provides fast access to accented characters.

| Operation | macOS | Windows |
| --- | --- | --- |
| Copy | Command+C | Ctrl+C |
| Cut | Command+X | Ctrl+X |
| Paste | Command+V | Ctrl+V |
| Undo | Command+Z | Ctrl+Z |
| Save | Command+S | Ctrl+S |
| Open | Command+O | Ctrl+O |
| Lock computer | Command+Control+Q | Windows key+L |
| Show desktop | Command+F3 | Windows key+D |
| Open file browser | Command+N in Finder | Windows key+E |
| Minimize all windows in the front app or across the desktop | Command+Option+M (front app) | Windows key+M (desktop) |
| Search | Command+Space | Windows key+S |
| Close a document or tab | Command+W | Ctrl+W |
| Quit the active app or close the active window | Command+Q (quit app) | Alt+F4 (close window) |
| Switch apps | Command+Tab | Alt+Tab |
| Show open windows | Control+Up | Windows key+Tab |
| Switch virtual desktops | Control+Left or Control+Right | Windows key+Ctrl+Left or Windows key+Ctrl+Right |
| Open Snap layouts | N/A | Windows key+Z |
| Save a screenshot | Command+Shift+3 | Windows key+PrtScn |
| Capture a region or window | Command+Shift+4 | Windows key+Shift+S |
| View item information or properties | Command+I | Alt+Enter |
| Select all items | Command+A | Ctrl+A |
| Select noncontiguous items | Command, then select each item | Ctrl, then select each item |
| Open emoji and symbol picker | Control+Command+Space | Windows key+. |

In Windows Terminal, use Ctrl+Shift+C and Ctrl+Shift+V to copy and paste by default. Ctrl+C remains available to interrupt a running command.

## Trackpad shortcuts

> [!NOTE]
> These gestures require a precision touchpad. Configure three- and four-finger gestures under **Settings > Bluetooth & devices > Touchpad**.

| Operation | macOS | Windows |
| --- | --- | --- |
| Scroll | Two-finger vertical swipe | Two-finger vertical swipe |
| Zoom | Two-finger pinch | Two-finger pinch |
| Open a context menu | Two-finger click | Two-finger tap |
| Show open windows | Four-finger upward swipe | Three-finger upward swipe |
| Show desktop | Spread four fingers | Three-finger downward swipe |
| Switch apps | N/A | Three-finger left or right swipe |
| Switch virtual desktops | Four-finger left or right swipe | Four-finger left or right swipe |

For the current gesture list, see [Touch gestures for Windows](https://support.microsoft.com/windows/touch-gestures-for-windows-a9d28305-4818-a5df-4e2b-e5590f850741).

## Command-line shells and terminals

Windows Terminal can host PowerShell, Command Prompt, WSL distributions, SSH, Azure CLI, Git Bash, and other command-line applications.

### PowerShell

[PowerShell](/powershell/scripting/overview) is a cross-platform shell and automation language built on .NET. PowerShell 7 is open source, runs on Windows, macOS, and Linux, and installs alongside the Windows PowerShell 5.1 version included with Windows. Install the current version with WinGet:

```powershell
winget install --id Microsoft.PowerShell --source winget
```

Launch PowerShell 7 with `pwsh`. Commands such as `ls`, `mv`, and `cat` are aliases for PowerShell cmdlets, not GNU utilities. Their parameters and object-based pipeline behavior differ from Bash. Use [`Get-Help`](/powershell/scripting/learn/ps101/02-help-system) and read about [PowerShell aliases](/powershell/scripting/learn/shell/using-aliases) when translating scripts.

Command Prompt remains available for batch files and tools that depend on Cmd syntax.

### Native Unix-style tools

Many familiar commands run natively on Windows without WSL:

- **[Coreutils for Windows](../core-utils/overview.md)** provides Microsoft-maintained builds of commands such as `ls`, `cat`, `cp`, `mv`, `grep`, `find`, `head`, `tail`, `wc`, `sort`, and `uniq`. Install it with `winget install Microsoft.Coreutils`. PowerShell aliases can take precedence, so use names such as `ls.exe` or `cp.exe` when you specifically want the Coreutils command.
- **[sudo](../advanced-settings/sudo/index.md)** elevates a command through User Account Control on Windows 11, version 24H2 and later. Unlike Unix `sudo`, it doesn't run a command as another user. On Windows 11, version 25H2 and later, enable it under **Settings > System > Advanced**.
- **[curl](../curl/index.md)** and **[tar](../tar/index.md)** are included for downloading files and working with archives.
- **[Edit](../edit/index.md)** is a lightweight terminal text editor included in current Windows 11 releases. Install it on other supported systems with `winget install Microsoft.Edit`.

## Use Linux with WSL

[Windows Subsystem for Linux (WSL)](/windows/wsl/) runs a Linux environment directly on Windows and is often the most familiar option for developers coming from macOS. New installations use WSL 2 by default.

To install WSL with the default Ubuntu distribution, run the following command in an elevated PowerShell window, and then restart Windows:

```powershell
wsl --install
```

To choose a different distribution, list the available distributions and install one by name instead:

```powershell
wsl --list --online
wsl --install -d <DistributionName>
```

After installation, use the following command to update WSL:

```powershell
wsl --update
```

For the best performance with Linux tools, store projects in the Linux file system, such as `/home/<user>/project`, rather than under `/mnt/c`. From WSL, run `explorer.exe .` to open the current directory in File Explorer. Windows exposes Linux files under `\\wsl$`, and Windows and Linux commands can call each other.

Visual Studio Code can open a project inside WSL with `code .`, keeping the editor UI on Windows while extensions, terminals, and tools run in Linux. See [Developing in WSL with Visual Studio Code](/windows/wsl/tutorials/wsl-vscode) and [Working across Windows and Linux file systems](/windows/wsl/filesystems).

## Use Windows Terminal

[Windows Terminal](/windows/terminal/) is the recommended host for command-line work on Windows. It supports profiles, tabs, split panes, themes, Unicode, and GPU-accelerated text rendering. Windows Terminal is included with Windows 11 and is available from the Microsoft Store for Windows 10.

- Open its command palette with Ctrl+Shift+P.
- Split the current profile into panes with Alt+Shift+Plus or Alt+Shift+Minus.
- Create profiles for PowerShell, WSL, Command Prompt, SSH, and other tools.

The legacy Windows Console Host remains available for compatibility with older command-line applications.

## Window management and productivity

Windows 11 includes Snap layouts, Snap Assist, Snap groups, and multiple desktops. Hover over a window's maximize button or press Windows key+Z to choose a layout. Use Windows key+Tab to view Snap groups and desktops.

[Microsoft PowerToys](../powertoys/index.md) adds utilities for developers and power users. [Command Palette](../powertoys/command-palette/overview.md) is a Spotlight-like launcher for apps, commands, files, settings, WinGet packages, open windows, clipboard history, and terminal profiles. Open it with Windows key+Alt+Space. Other useful utilities include Keyboard Manager, Quick Accent, FancyZones, and Workspaces.

File Explorer combines local and cloud files, pinned locations, tabs, and recent items. Use **View > Show** to display file extensions and hidden files. The streamlined context menu keeps common commands at the top; select **Show more options** for legacy shell extensions.

## Apps and utilities

| Task | macOS | Windows |
| --- | --- | --- |
| Settings | System Settings | Settings |
| Monitor processes | Activity Monitor | Task Manager |
| Format and partition disks | Disk Utility | Disk Management |
| Edit text | TextEdit | Notepad or [Edit](../edit/index.md) |
| View system events | Console | Event Viewer |
| Find files and apps | Spotlight | Windows Search or [PowerToys Command Palette](../powertoys/command-palette/overview.md) |
| Manage packages | Homebrew | [WinGet](/windows/package-manager/) |
| Store development files | N/A | [Dev Drive](../dev-drive/index.md) |
| Run Unix command-line tools | Built in | [Coreutils](../core-utils/overview.md), [sudo](../advanced-settings/sudo/index.md), or WSL |
| Arrange windows | Mission Control | Snap layouts and multiple desktops |
| Add productivity utilities | N/A | [PowerToys](../powertoys/index.md) |
