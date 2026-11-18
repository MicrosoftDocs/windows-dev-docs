---
title: PowerToys command-line tools
description: Find PowerToys command-line tools, check their prerequisites, run them from your terminal, and troubleshoot common command errors.
author: GrantMeStrength
ms.author: jken
ms.topic: overview
ms.date: 11/18/2026
---

# PowerToys command-line tools

PowerToys provides separate command-line tools for individual utilities. You can use them from a terminal or a script to resize images, inspect file locks, manage window layouts, and configure utilities. There isn't a single `powertoys` command that runs every tool.

This page helps you find and start the tools. For command syntax, options, and exit codes, follow the utility reference links.

## Available tools

The following tools have commands in the PowerToys installation's `bin` folder. The stable release, [PowerToys v0.102.3101](https://github.com/microsoft/PowerToys/releases/tag/v0.102.3101), includes the seven non-preview commands in this table. [PowerToys v0.103.3151 preview](https://github.com/microsoft/PowerToys/releases/tag/v0.103.3151) also includes Workspaces. Command availability and options depend on your installed version.

| Tool and reference | Command | What to know before you run it |
| :--- | :--- | :--- |
| [Advanced Paste](advanced-paste.md#command-line-interface) | `PowerToys.AdvancedPaste.CLI.exe` | Transform text, images, or media. AI actions need a configured provider; clipboard operations need an interactive user session. Some actions are preview-only. |
| [FancyZones](fancyzones.md#command-line-reference) | `PowerToys.FancyZones.CLI.exe` | Start PowerToys and enable FancyZones. All operational commands, including listing layouts and opening settings, need the FancyZones process running. |
| [File Locksmith](file-locksmith.md#command-line-reference) | `PowerToys.FileLocksmith.CLI.exe` | Supply file or directory paths to find processes that lock them. PowerToys doesn't need to be running. Process visibility depends on your permissions. |
| [Image Resizer](image-resizer.md#command-line-reference) | `PowerToys.ImageResizer.CLI.exe` | Supply image paths and resize options. PowerToys doesn't need to be running; preset sizes come from Image Resizer settings. |
| [Light Switch](light-switch.md#command-line-interface) | `PowerToys.LightSwitch.CLI.exe` | Enable Light Switch and keep its service running to query or change themes and scheduling. The CLI doesn't start it for you. |
| [Power Display](power-display.md#command-line-interface) | `PowerToys.PowerDisplay.CLI.exe` | Enable Power Display and keep its app running. Monitor operations also depend on the display's supported capabilities. |
| [Settings](general.md#settings-cli) | `PowerToys.Settings.CLI.exe` | Read utility enabled states. Exit PowerToys before enabling or disabling utilities; changes apply when you next start PowerToys. |
| [Workspaces (preview)](workspaces.md#command-line-interface-preview) | `PowerToys.Workspaces.CLI.exe` | Use a preview build that includes the command. Enable Workspaces before listing or launching saved workspaces; PowerToys doesn't need to be running. |

Use `--help` to see a command's supported options. Don't assume every tool supports `--version`, JSON output, or the same exit codes. Displaying help doesn't confirm that a utility's operational prerequisites are met.

### Other command-line entry points

These entry points don't have `bin` shims. Invoke them by their executable paths relative to your PowerToys installation folder:

| Tool and reference | Executable path | Use |
| :--- | :--- | :--- |
| [Awake](awake.md#command-line-interface-cli) | `PowerToys.Awake.exe` | Run Awake as a standalone application with command-line options to keep your computer awake. |
| [Peek](peek.md#use-peek-from-the-command-line) | `WinUI3Apps\PowerToys.Peek.UI.exe` | Pass file or folder paths to open a graphical preview. |
| [Microsoft DSC configuration](dsc-configure/microsoft-dsc.md) | `PowerToys.DSC.exe` | Read, test, or apply utility configuration. Follow the reference for optional DSC or WinGet integration. |

## Find and run a command

[Install PowerToys](install.md), then open a new terminal window. The installer adds the installation's `bin` folder to your user `PATH` for a per-user installation, or the system `PATH` for a machine-wide installation.

The default locations below use PowerShell environment variable syntax:

| Installation | Installation folder | Command folder on `PATH` |
| :--- | :--- | :--- |
| Per-user | `$env:LOCALAPPDATA\PowerToys` | `$env:LOCALAPPDATA\PowerToys\bin` |
| Machine-wide | `$env:ProgramFiles\PowerToys` | `$env:ProgramFiles\PowerToys\bin` |

If you chose a custom installation folder, use its `bin` subfolder instead.

In PowerShell, check which executable a command resolves to, then display its help:

```powershell
Get-Command PowerToys.FancyZones.CLI.exe
PowerToys.FancyZones.CLI.exe --help
```

You can also list the installed commands and invoke one by its full path. This example uses the default per-user installation:

```powershell
Get-ChildItem "$env:LOCALAPPDATA\PowerToys\bin\*.exe"
& "$env:LOCALAPPDATA\PowerToys\bin\PowerToys.FancyZones.CLI.exe" --help
```

For a machine-wide installation, replace `$env:LOCALAPPDATA` with `$env:ProgramFiles`. For a custom installation, substitute its actual path. The PowerShell call operator (`&`) runs the quoted executable path, including paths that contain spaces.

The executables in `bin` are launchers, also called *shims*, that forward arguments to tools elsewhere in the installation. Keep them in their installed location. Copying a shim alone doesn't copy the tool it launches. Don't add the installation root or `WinUI3Apps` folder to `PATH`; those folders contain application and runtime DLLs.

## Troubleshoot command errors

| Symptom | What to check |
| :--- | :--- |
| The shell says the command isn't recognized or wasn't found. | Open a new terminal after installation. Check that the command exists in your installation's `bin` folder and that this folder is on `PATH`. Try its full path. A preview-only command might not be included in your installed release; see [update channels](update-channel.md). |
| The launcher reports that its target executable is missing. | Use the shim from your current PowerToys installation, not a copied shim or one from an old installation. Reinstall PowerToys if the installation is incomplete. Adding folders to `PATH` doesn't restore a missing target. |
| The CLI reports a disabled utility, unavailable provider, or missing settings data. | The command was found. Follow the utility reference's prerequisites and troubleshooting guidance rather than changing `PATH`. For example, see [FancyZones missing layout data](fancyzones.md#missing-layout-data) for errors involving `layout-templates.json`. |

A command that displays help or starts successfully can still fail when you request an operation. In scripts, check the operation's exit code and output as described in its utility reference.
